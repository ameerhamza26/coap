using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data.Entity;

namespace BLL
{
   public class CIF
    {

        public int BI_ID { get; set; }
        public CifType CifType { get; set; }
        public int UserId { get; set; }
        public CIF(int BI_ID, CifType cifType)
       {
           this.BI_ID = BI_ID;
           this.CifType = cifType;
       }

        public CIF(int UserId)
        {
            this.UserId = UserId;
        }

       public bool CheckCifCompleted()
       {
            bool Completed = false; 
           using (CAOPDbContext db = new CAOPDbContext())
           {
               if (CifType == CifType.INDIVIDUAL || CifType == CifType.MINOR)
               {
                   Completed = (from b in db.BASIC_INFORMATIONS
                                join i in db.IDENTITIES on b.ID equals i.BI_ID
                                join c in db.CONTACT_INFOS on b.ID equals c.BI_ID
                                join e in db.EMPLOYMENT_INFORMATIONS on b.ID equals e.BI_ID
                                join m in db.MISCELLANEOUS_INFORMATIONS on b.ID equals m.BI_ID
                                join r in db.BANKING_RELATIONSHPS on b.ID equals r.BI_ID
                                join f in db.FATCAS on b.ID equals f.BI_ID
                                where b.ID == this.BI_ID
                                select b.ID).Any();
               }
               else if (CifType == CifType.NEXT_OF_KIN)
               {
                   Completed = (from b in db.BASIC_INFORMATIONS
                                join i in db.IDENTITIES on b.ID equals i.BI_ID
                                join c in db.CONTACT_INFOS on b.ID equals c.BI_ID
                                where b.ID == this.BI_ID
                                select b.ID).Any();
               }
               else if (CifType == CifType.BUSINESS)
               {
                   Completed = (from b in db.BASIC_INFORMATIONS
                                join c in db.CONTACT_INFOS on b.ID equals c.BI_ID
                                join h in db.HEAD_CONTACT_INFOS on b.ID equals h.BI_ID
                                join e in db.BUSINESS_DETAIL on b.ID equals e.BI_ID
                                join m in db.MISCELLANEOUS_INFORMATIONS on b.ID equals m.BI_ID
                                join r in db.BANKING_RELATIONSHPS on b.ID equals r.BI_ID
                                join s in db.SHAREHOLDER_INFORMATION on b.ID equals s.BID
                                join f in db.FATCAS on b.ID equals f.BI_ID
                                where b.ID == this.BI_ID 
                                select b.ID).Any();
               }
               return Completed;
           }
       }


  

       public void ChangeStatus(Status status, User LogedUser)
       {
           using (CAOPDbContext db = new CAOPDbContext())
           {

               STATUS_LOG newSlog = new STATUS_LOG();
               newSlog.USERID = LogedUser.USER_ID;
               newSlog.BID = this.BI_ID;
               newSlog.OLD_STATUS = db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID).STATUS;
               newSlog.NEW_STATUS = status.ToString();
               newSlog.LOG_DATETIME = DateTime.Now;
               newSlog.LOG_TYPE = "CIF";
               db.STATUS_LOG.Add(newSlog);
               db.SaveChanges();
               
               var BI = db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == this.BI_ID);
               BI.STATUS = status.ToString();
                BI.LAST_UPDATED = DateTime.Now;
                db.SaveChanges();

             
              
               
           }
       }

       
       public bool CheckStatus(int BId, string status)
       {
           using (CAOPDbContext db = new CAOPDbContext())
           {
               return db.BASIC_INFORMATIONS.Where(b => b.ID == BId && b.STATUS == status).Any();
           }
       }

       public bool CheckStatus(int BId, string[] Statuses)
       {
           using (CAOPDbContext db = new CAOPDbContext())
           {
               return db.BASIC_INFORMATIONS.Where(b => b.ID == BId && Statuses.Contains(b.STATUS)).Any();
           }
       }
       public List<BasicInformations> GetPendingCifs()
       {
           using (CAOPDbContext db = new CAOPDbContext())
           {
               List<BasicInformations> PendingCifs = db.BASIC_INFORMATIONS.Where(b => b.STATUS == "SAVED" && b.UserId == this.UserId)
                     .OrderByDescending(b=> b.LAST_UPDATED).Select(b => new BasicInformations { ID = b.ID, CNIC = b.CNIC, NAME = b.NAME,NAME_OFFICE = b.NAME_OFFICE, STATUS = b.STATUS, LAST_UPDATED = b.LAST_UPDATED, NTN = b.NTN, CIF_TYPE = new CifTypes {ID  = (int) b.CIF_TYPE, Name = db.CIF_TYPES.FirstOrDefault(t => t.ID == b.CIF_TYPE).Name }  }).ToList();

               return PendingCifs;
           }
       }

       public List<BasicInformations> GeteCifsByRole(string UserRole,bool Region)
       {
           using (CAOPDbContext db = new CAOPDbContext())
           {
               List<int> BranchUsers = new List<int>();
                List<BasicInformations> SubmittedCifs = new List<BasicInformations>();

               if (UserRole == Roles.BRANCH_OPERATOR.ToString())
               {
                  
                   BranchUsers.Add(this.UserId);
                    SubmittedCifs = db.BASIC_INFORMATIONS.Where(b => b.STATUS != Status.SAVED.ToString() && BranchUsers.Contains((int)b.UserId))
                       .OrderByDescending(b => b.LAST_UPDATED ).Select(b => new BasicInformations { ID = b.ID, CNIC = b.CNIC, NAME = b.NAME,NAME_OFFICE = b.NAME_OFFICE,LAST_UPDATED = b.LAST_UPDATED ,STATUS = b.STATUS, NTN = b.NTN, CIF_TYPE = new CifTypes { ID = (int)b.CIF_TYPE, Name = db.CIF_TYPES.FirstOrDefault(t => t.ID == b.CIF_TYPE).Name },RISK_SCORE = b.RISK_SCORE, RISK_CATEGORY = b.RISK_CATEGORY, PROFILE_CIF_NO = b.PROFILE_CIF_NO , BRANCH_CODE = b.BRANCH_CODE}).ToList();

                }
               else
               {
                    if (Region)
                    {
                        var branches = db.BRANCHES.Where(b => (b.CATEGORY_ID == 1 || b.CATEGORY_ID == 2)&& b.REGION_ID == db.USERS.FirstOrDefault(u => u.USER_ID == this.UserId).PARENT_ID).Select(b => b.BRANCH_ID).ToList();
                        BranchUsers = db.USERS.Where(u => branches.Contains(u.PARENT_ID) && u.USER_TYPE == "BRANCH" ).Select(u => u.USER_ID).ToList();
                    }
                    else
                    {
                        BranchUsers = db.USERS.Where(u => u.USER_TYPE == "BRANCH" && u.PARENT_ID == (db.USERS.FirstOrDefault(l => l.USER_ID == this.UserId).PARENT_ID)).Select(u => u.USER_ID).ToList();
                    }

                  

                    if (UserRole == Roles.COMPLIANCE_OFFICER.ToString())
                    {
                        
                        SubmittedCifs = db.BASIC_INFORMATIONS.Where(b => b.STATUS != Status.SAVED.ToString() && BranchUsers.Contains((int)b.UserId))
                        .OrderByDescending(b => b.LAST_UPDATED).Select(b => new BasicInformations { ID = b.ID, CNIC = b.CNIC, NAME = b.NAME, NAME_OFFICE = b.NAME_OFFICE, STATUS = b.STATUS, LAST_UPDATED = b.LAST_UPDATED, NTN = b.NTN, CIF_TYPE = new CifTypes { ID = (int)b.CIF_TYPE, Name = db.CIF_TYPES.FirstOrDefault(t => t.ID == b.CIF_TYPE).Name }, RISK_SCORE = b.RISK_SCORE, RISK_CATEGORY = b.RISK_CATEGORY, PROFILE_CIF_NO = b.PROFILE_CIF_NO, BRANCH_CODE = b.BRANCH_CODE }).ToList();
                    }
                    else if (UserRole == Roles.BRANCH_MANAGER.ToString())
                    {
                        SubmittedCifs = db.BASIC_INFORMATIONS.Where(b => b.STATUS != Status.SAVED.ToString() && b.STATUS != Status.SUBMITTED.ToString() && BranchUsers.Contains((int)b.UserId))
                        .OrderByDescending(b => b.LAST_UPDATED).Select(b => new BasicInformations { ID = b.ID, CNIC = b.CNIC, NAME = b.NAME, NAME_OFFICE = b.NAME_OFFICE, STATUS = b.STATUS, LAST_UPDATED = b.LAST_UPDATED, NTN = b.NTN, CIF_TYPE = new CifTypes { ID = (int)b.CIF_TYPE, Name = db.CIF_TYPES.FirstOrDefault(t => t.ID == b.CIF_TYPE).Name }, RISK_SCORE = b.RISK_SCORE, RISK_CATEGORY = b.RISK_CATEGORY, PROFILE_CIF_NO = b.PROFILE_CIF_NO, BRANCH_CODE = b.BRANCH_CODE }).ToList();
                    }
               }
                                
               return SubmittedCifs;
           }
       }


        //public BasicInformations GetCif(int id)
        //{
        //    using (CAOPDbContext db = new CAOPDbContext())
        //    {

        //      var b = db.BASIC_INFORMATIONS.FirstOrDefault(c=> c.ID == id);
        //        BasicInformations basic = new BasicInformations();



        //    }

        //}


       public List<BasicInformations> GetCifsForAccountsWho(String str, Status str2)
       {
           using (CAOPDbContext db = new CAOPDbContext())
           {
               List<BasicInformations> cifs = db.BASIC_INFORMATIONS
                                        .Where(b => b.CNIC.Contains(str) && b.CIF_TYPE == 1  && b.STATUS == str2.ToString())
                                        .Select(c => new BasicInformations
                                        { 
                                            ID = c.ID,                                            
                                            PRIMARY_DOCUMENT_TYPE = new PrimaryDocumentType
                                            {
                                                ID = (int)c.DOCUMENT_TYPE_PRIMARY,
                                                Name = db.DOCUMENT_TYPES_PRIMARY.FirstOrDefault(p => p.ID == (int) c.DOCUMENT_TYPE_PRIMARY).Name
                                            },
                                            FIRST_NAME = c.FIRST_NAME,MIDDLE_NAME = c.MIDDLE_NAME,LAST_NAME = c.LAST_NAME
                                            ,CNIC = c.CNIC }).ToList();
               return cifs;
           }

       }

       public List<BasicInformations> GetCifsForGovernment(String str, Status str2)
       {
           using (CAOPDbContext db = new CAOPDbContext())
           {
               List<BasicInformations> cifs = db.BASIC_INFORMATIONS.Where(b => b.NAME_OFFICE == str && b.STATUS == str2.ToString() && b.CIF_TYPE == 4).Select(c => new BasicInformations { ID = c.ID, CIF_TYPE = new CifTypes { ID = (int)c.CIF_TYPE }, NAME_OFFICE = c.NAME_OFFICE, CNIC = c.CNIC, SALES_TAX_NO = c.SALES_TAX_NO, REG_NO = c.REG_NO, Issuing_Agency = new IssuingAgency() { ID = c.ISSUING_AGENCY, Name = db.ISSUING_AGENCY.FirstOrDefault(a => a.ID == c.ISSUING_AGENCY).NAME } }).ToList();
               return cifs;
           }

       }

        public List<BasicInformations> GetCifsForAccounts2(String str,Status str2)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                List<BasicInformations> cifs = db.BASIC_INFORMATIONS.Where(b => b.CNIC.Contains(str) && (b.CIF_TYPE == 1 || b.CIF_TYPE== 6) && (b.STATUS == str2.ToString() || b.STATUS == Status.PROFILE.ToString()) ).Select(c => new BasicInformations { ID = c.ID, CIF_TYPE = new CifTypes { ID = (int)c.CIF_TYPE }, NAME = c.NAME, CNIC = c.CNIC }).ToList();
                return cifs;
            }

        }

        public List<BasicInformations> GetCifsForMinor(String str, Status str2)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                List<BasicInformations> cifs = db.BASIC_INFORMATIONS.Where(b => b.CNIC.Contains(str) && (b.CIF_TYPE == 6) && (b.STATUS == str2.ToString() || b.STATUS == Status.PROFILE.ToString())).Select(c => new BasicInformations { ID = c.ID, CIF_TYPE = new CifTypes { ID = (int)c.CIF_TYPE }, NAME = c.NAME, CNIC = c.CNIC }).ToList();
                return cifs;
            }

        }


        public List<BasicInformations> GetCifsForAccounts3(String str, Status str2, int id)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {

                List<BasicInformations> cifs = db.BASIC_INFORMATIONS.Where(b => b.NAME_OFFICE.Contains(str) && b.STATUS == str2.ToString() && b.CIF_TYPE == 2 && b.CIF_TYPE == 4).Select(c => new BasicInformations { ID = c.ID, CIF_TYPE = new CifTypes { ID = (int)c.CIF_TYPE }, NAME_OFFICE = c.NAME_OFFICE, CNIC = c.CNIC, SALES_TAX_NO = c.SALES_TAX_NO, REG_NO = c.REG_NO, Issuing_Agency = new IssuingAgency() { ID = c.ISSUING_AGENCY, Name = db.ISSUING_AGENCY.FirstOrDefault(a => a.ID == c.ISSUING_AGENCY).NAME } }).ToList();

                if (id == 1)
                {
                    cifs = db.BASIC_INFORMATIONS.Where(b => b.NAME_OFFICE.Contains(str) && b.STATUS == str2.ToString() && (b.CIF_TYPE == 2 || b.CIF_TYPE == 4)).Select(c => new BasicInformations { ID = c.ID, CIF_TYPE = new CifTypes { ID = (int)c.CIF_TYPE }, NAME_OFFICE = c.NAME_OFFICE, CNIC = c.CNIC, SALES_TAX_NO = c.SALES_TAX_NO, REG_NO = c.REG_NO, Issuing_Agency = new IssuingAgency() { ID = c.ISSUING_AGENCY, Name = db.ISSUING_AGENCY.FirstOrDefault(a => a.ID == c.ISSUING_AGENCY).NAME } }).ToList();

                }
                else if (id ==2 )
                {
                    cifs = db.BASIC_INFORMATIONS.Where(b => b.SALES_TAX_NO.Contains(str) && b.STATUS == str2.ToString() && (b.CIF_TYPE == 2 || b.CIF_TYPE == 4)).Select(c => new BasicInformations { ID = c.ID, CIF_TYPE = new CifTypes { ID = (int)c.CIF_TYPE }, NAME_OFFICE = c.NAME_OFFICE, CNIC = c.CNIC, SALES_TAX_NO = c.SALES_TAX_NO, REG_NO = c.REG_NO, Issuing_Agency = new IssuingAgency() { ID = c.ISSUING_AGENCY, Name = db.ISSUING_AGENCY.FirstOrDefault(a => a.ID == c.ISSUING_AGENCY).NAME } }).ToList();
                }
                else if (id == 3)
                {
                    cifs = db.BASIC_INFORMATIONS.Where(b => b.NTN.Contains(str) && b.STATUS == str2.ToString() && (b.CIF_TYPE == 2 || b.CIF_TYPE == 4)).Select(c => new BasicInformations { ID = c.ID, CIF_TYPE = new CifTypes { ID = (int)c.CIF_TYPE }, NAME_OFFICE = c.NAME_OFFICE, CNIC = c.CNIC, SALES_TAX_NO = c.SALES_TAX_NO, REG_NO = c.REG_NO, Issuing_Agency = new IssuingAgency() { ID = c.ISSUING_AGENCY, Name = db.ISSUING_AGENCY.FirstOrDefault(a => a.ID == c.ISSUING_AGENCY).NAME } }).ToList();

                }
                else
                {
                    cifs = db.BASIC_INFORMATIONS.Where(b => b.REG_NO.Contains(str) && b.STATUS == str2.ToString() && (b.CIF_TYPE == 2 || b.CIF_TYPE == 4)).Select(c => new BasicInformations { ID = c.ID, CIF_TYPE = new CifTypes { ID = (int)c.CIF_TYPE }, NAME_OFFICE = c.NAME_OFFICE, CNIC = c.CNIC, SALES_TAX_NO = c.SALES_TAX_NO, REG_NO = c.REG_NO, Issuing_Agency = new IssuingAgency() { ID = c.ISSUING_AGENCY, Name = db.ISSUING_AGENCY.FirstOrDefault(a => a.ID == c.ISSUING_AGENCY).NAME } }).ToList();

                }

                return cifs;
            }

        }


        public List<BasicInformations> GetCifsForAccounts(Status status)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
               // List<BasicInformations> cifs = db.BASIC_INFORMATIONS.Where(b => b.STATUS == status.ToString()).Select(c => new BasicInformations { ID = c.ID, CIF_TYPE = new CifTypes { ID = (int)c.CIF_TYPE }, NAME = c.NAME, CNIC = c.CNIC }).ToList();
               // return cifs;

               // List<SearchCif> cif = db.BASIC_INFORMATIONS.Where(b => b.STATUS == status.ToString()).Select(c => new SearchCif { ID = c.ID, CIF})

                List<BasicInformations> cifs = new List<BasicInformations>();
                return cifs;
                

            }

        }



        public List<BasicInformations> GetCifsForAccountsBusiness(Status status)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                List<BasicInformations> cifs = db.BASIC_INFORMATIONS.Where(b => b.STATUS == status.ToString()).Select(c => new BasicInformations { ID = c.ID, CIF_TYPE = new CifTypes { ID = (int)c.CIF_TYPE }, NAME = c.NAME, CNIC = c.CNIC }).ToList();
                return cifs;
            }

        }






        public CifType GetCifType(int ID)
       {
           using (CAOPDbContext db = new CAOPDbContext())
           {
             string CifTypeNAme =   db.CIF_TYPES.FirstOrDefault(c => c.ID == (db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == ID).CIF_TYPE)).Name;
                if (CifTypeNAme == "INDIVIDUAL")
                    return CifType.INDIVIDUAL;

                else if (CifTypeNAme == "NEXT_OF_KIN")
                    return CifType.NEXT_OF_KIN;
                else if (CifTypeNAme == "GOVERNMENT")
                    return CifType.GOVERNMENT;
                else if (CifTypeNAme == "OFFICE")
                    return CifType.OFFICE;
                else if (CifTypeNAme == "MINOR")
                    return CifType.MINOR;
                else
                    return CifType.BUSINESS;
           }
       }


       public List<BasicInformations> SearchCif(string Value, bool CNIC, bool NIC, bool NTN, bool NAME)
       {
           using (CAOPDbContext db = new CAOPDbContext())
           {
               List<BasicInformations> SearchedCifs;
               if (CNIC)
               {
                SearchedCifs =   db.BASIC_INFORMATIONS.Where(b => b.CNIC == Value && b.STATUS == Status.APPROVED_BY_BRANCH_MANAGER.ToString())
                       .Select(b => new BasicInformations { ID = b.ID, CNIC = b.CNIC, NAME = b.NAME,NAME_OFFICE = b.NAME_OFFICE, STATUS = b.STATUS, NTN = b.NTN, CIF_TYPE = new CifTypes { ID = (int)b.CIF_TYPE, Name = db.CIF_TYPES.FirstOrDefault(t => t.ID == b.CIF_TYPE).Name } }).ToList();
               }
               else if (NIC)
               {
                   SearchedCifs = db.BASIC_INFORMATIONS.Where(b => b.CNIC == Value && b.STATUS == Status.APPROVED_BY_BRANCH_MANAGER.ToString())
                     .Select(b => new BasicInformations { ID = b.ID, CNIC = b.CNIC, NAME = b.NAME, NAME_OFFICE = b.NAME_OFFICE, STATUS = b.STATUS, NTN = b.NTN, CIF_TYPE = new CifTypes { ID = (int)b.CIF_TYPE, Name = db.CIF_TYPES.FirstOrDefault(t => t.ID == b.CIF_TYPE).Name } }).ToList();
               }
               else if (NTN)
               {
                   SearchedCifs = db.BASIC_INFORMATIONS.Where(b => b.NTN == Value && b.STATUS == Status.APPROVED_BY_BRANCH_MANAGER.ToString())
                     .Select(b => new BasicInformations { ID = b.ID, CNIC = b.CNIC, NAME = b.NAME, NAME_OFFICE = b.NAME_OFFICE, STATUS = b.STATUS, NTN = b.NTN, CIF_TYPE = new CifTypes { ID = (int)b.CIF_TYPE, Name = db.CIF_TYPES.FirstOrDefault(t => t.ID == b.CIF_TYPE).Name } }).ToList();
               }
               else
               {
                   SearchedCifs = db.BASIC_INFORMATIONS.Where(b => b.NAME_OFFICE.Contains(Value) && b.STATUS == Status.APPROVED_BY_BRANCH_MANAGER.ToString())
                   .Select(b => new BasicInformations { ID = b.ID, CNIC = b.CNIC, NAME = b.NAME, NAME_OFFICE = b.NAME_OFFICE, STATUS = b.STATUS, NTN = b.NTN, CIF_TYPE = new CifTypes { ID = (int)b.CIF_TYPE, Name = db.CIF_TYPES.FirstOrDefault(t => t.ID == b.CIF_TYPE).Name } }).ToList();
               }

               return SearchedCifs;
           }
       }


       public void DeleteCif(int CIFID)
       {
           using (CAOPDbContext db = new CAOPDbContext())
           {
              var BI= db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == CIFID);
               if (BI != null)
                db.BASIC_INFORMATIONS.Remove(BI);

               var NAT = db.NATIONALITIES_BASIC_INFORMATION.Where(n => n.BI_ID == CIFID);
               if (NAT != null)
                   db.NATIONALITIES_BASIC_INFORMATION.RemoveRange(NAT);

              var IDENTITIES = db.IDENTITIES.FirstOrDefault(i => i.BI_ID == CIFID);
              if (IDENTITIES != null)
              db.IDENTITIES.Remove(IDENTITIES);

              var CONTACT_INFOS = db.CONTACT_INFOS.FirstOrDefault(i => i.BI_ID == CIFID);
              if (CONTACT_INFOS != null)
              db.CONTACT_INFOS.Remove(CONTACT_INFOS);

              var EMPLOYMENT_INFORMATION = db.EMPLOYMENT_INFORMATIONS.FirstOrDefault(e => e.BI_ID == CIFID);
              if (EMPLOYMENT_INFORMATION != null)
              db.EMPLOYMENT_INFORMATIONS.Remove(EMPLOYMENT_INFORMATION);

              var MISCELLANEOUS_INFORMATION = db.MISCELLANEOUS_INFORMATIONS.FirstOrDefault(e => e.BI_ID == CIFID);
              if (MISCELLANEOUS_INFORMATION != null)
              db.MISCELLANEOUS_INFORMATIONS.Remove(MISCELLANEOUS_INFORMATION);

              var COUNTRIES_TAX_MISCELLANEOUS_INFORMATIONs = db.COUNTRIES_TAX_MISCELLANEOUS_INFORMATION.Where(e => e.BI_ID == CIFID);
              if (COUNTRIES_TAX_MISCELLANEOUS_INFORMATIONs != null)
              db.COUNTRIES_TAX_MISCELLANEOUS_INFORMATION.RemoveRange(COUNTRIES_TAX_MISCELLANEOUS_INFORMATIONs);

              var BANKING_RELATIONSHP = db.BANKING_RELATIONSHPS.FirstOrDefault(e => e.BI_ID == CIFID);
              if (BANKING_RELATIONSHP != null)
              db.BANKING_RELATIONSHPS.Remove(BANKING_RELATIONSHP);

              var FATCAS = db.FATCAS.FirstOrDefault(e => e.BI_ID == CIFID);
              if (FATCAS != null)
              db.FATCAS.Remove(FATCAS);

              var FATCADocs = db.DOCUMENTS_FATCA.Where(e => e.BI_ID == CIFID);
              if (FATCADocs != null)
              db.DOCUMENTS_FATCA.RemoveRange(FATCADocs);

               // Business

              var hcinfo = db.HEAD_CONTACT_INFOS.FirstOrDefault(e => e.BI_ID == CIFID);
              if (hcinfo != null)
              db.HEAD_CONTACT_INFOS.Remove(hcinfo);

              var BDetail = db.BUSINESS_DETAIL.FirstOrDefault(e => e.BI_ID == CIFID);
              if (BDetail != null)
              db.BUSINESS_DETAIL.Remove(BDetail);

              var BDetailCities = db.CITIES_BUSINESS_DETAILS.Where(e => e.BI_ID == CIFID);
              if (BDetailCities != null)
              db.CITIES_BUSINESS_DETAILS.RemoveRange(BDetailCities);

              var BDetailCountries = db.COUNTRIES_BUSINESS_DETAILS.Where(e => e.BI_ID == CIFID);
              if (BDetailCountries != null)
              db.COUNTRIES_BUSINESS_DETAILS.RemoveRange(BDetailCountries);

              var SH = db.SHAREHOLDER_INFORMATION.Where(e => e.BID == CIFID);
              if (SH != null)
                  db.SHAREHOLDER_INFORMATION.RemoveRange(SH);

              db.SaveChanges();


           }
       }

        public string[] SearchCifKin(int CIFID)
        {
            String[] KinCif = new String[] { "", "" };
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.BASIC_INFORMATIONS.Where(b => b.ID == CIFID).Any())
                {
                    BASIC_INFORMATIONS BI = db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == CIFID);
                    KinCif[0] = BI.NAME;
                    KinCif[1] = BI.CNIC;
                }
            }
            return KinCif;
        }

        public void DumpCif(string[] CIF, string ProfileNumber)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                Gender g = new Gender();

                BASIC_INFORMATIONS NewBi = new BASIC_INFORMATIONS();
                NewBi.DOCUMENT_TYPE_PRIMARY = db.DOCUMENT_TYPES_PRIMARY.FirstOrDefault(d => d.Name == "CNIC").ID;
                NewBi.CIF_TYPE = db.CIF_TYPES.FirstOrDefault(c => c.Name == "INDIVIDUAL").ID;
                NewBi.CNIC = CIF[0];
                NewBi.NAME = CIF[11];
                NewBi.NAME_FH = CIF[32];
                NewBi.NAME_MOTHER = CIF[33];
                if (CIF[15].Length > 0)
                    NewBi.GENDER = g.GetValueOfGender(CIF[15]);
                //  if (CIF[16].Length > 0)
                //     NewBi.MARTIAL_STATUS = Convert.ToInt32(CIF[16]);
                NewBi.DOB = CIF[12];
                NewBi.NTN = CIF[28];

                if (CIF[23].Length > 0)
                {
                    string profile = CIF[23];
                    NewBi.COUNTRY_RESIDENCE = db.COUNTRIES.FirstOrDefault(c => c.ProfileCode == profile).ID;
                }

                NewBi.PROFILE_CIF_NO = ProfileNumber;
                NewBi.BRANCH_CODE = CIF[69].PadLeft(4, '0');
                NewBi.STATUS = "PROFILE";
                NewBi.PROFILE_STATUS = "PROFILE";

                db.BASIC_INFORMATIONS.Add(NewBi);
                db.SaveChanges();

                if (CIF[20].Length > 0)
                {
                    NATIONALITIES_BASIC_INFORMATION nationality = new NATIONALITIES_BASIC_INFORMATION();
                    string profile = CIF[20];
                    nationality.BI_ID = NewBi.ID;
                    nationality.COUNTRY_ID = db.COUNTRIES.FirstOrDefault(c => c.ProfileCode == profile).ID;
                    db.NATIONALITIES_BASIC_INFORMATION.Add(nationality);
                }



                CONTACT_INFOS CI = new CONTACT_INFOS();
                CI.BI_ID = NewBi.ID;
                // Permanent Address
                CI.BIULDING_SUITE = CIF[56] + " " + CIF[57] + " " + CIF[58] + " " + CIF[59];
                if (CIF[60].Length > 0)
                {
                    string profile = CIF[60];
                    if (db.CITIES.Where(c => c.NAME == profile).Any())
                        CI.CITY_PERMANENT = db.CITIES.FirstOrDefault(c => c.NAME == profile).ID;
                }

                if (CIF[22].Length > 0)
                {
                    string profile = CIF[22];
                    CI.COUNTRY_CODE = db.COUNTRIES.FirstOrDefault(c => c.ProfileCode == profile).ID;
                }
                if (CIF[62].Length > 0)
                {
                    try 
                    {
                        CI.PROVINCE = Convert.ToInt32(CIF[62]);
                    }
                    catch(Exception e)
                    {

                    }
                    
                }
                // Present / Mailing Address
                CI.BIULDING_SUITE_PRE = CIF[48] + " " + CIF[49] + " " + CIF[50] + " " + CIF[51];
                if (CIF[55].Length > 0)
                {
                    string profile = CIF[55];
                    CI.COUNTRY_CODE_PRE = db.COUNTRIES.FirstOrDefault(c => c.ProfileCode == profile).ID;
                }
                if (CIF[52].Length > 0)
                {
                    string profile = CIF[52];
                    if (db.CITIES.Where(c => c.NAME == profile).Any())
                        CI.CITY_PRESENT = db.CITIES.FirstOrDefault(c => c.NAME == profile).ID;
                }
                if (CIF[54].Length > 0)
                {
                    try
                    {
                        CI.PROVINCE_PRE = Convert.ToInt32(CIF[54]);
                    }
                    catch (Exception e)
                    {

                    }
                    
                }

                CI.EMAIL = CIF[68];
                CI.RESIDENCE_CONTACT = CIF[63];
                CI.OFFICE_CONTACT = CIF[64];
                CI.MOBILE_NO = CIF[66];
                CI.FAX_NO = CIF[67];

                db.CONTACT_INFOS.Add(CI);

                MISCELLANEOUS_INFORMATIONS MI = new MISCELLANEOUS_INFORMATIONS();
                MI.BI_ID = NewBi.ID;
                //if (CIF[19].Length > 0)
                //    MI.EDUCATION = Convert.ToInt32(CIF[19]);
                try
                {
                    if (CIF[30].Length > 0)
                        MI.PEP = Convert.ToInt32(CIF[30]);
                }
                catch (Exception e)
                {

                }
               
                if (CIF[29] == "1")
                    MI.PARDA_NASHEEN = true;
                else
                    MI.PARDA_NASHEEN = false;


                IDENTITIES IDEN = new IDENTITIES();
                IDEN.BI_ID = NewBi.ID;
                IDEN.CNIC_DATE_ISSUE = CIF[25];
                IDEN.EXPIRY_DATE = CIF[26];
                IDEN.FAMILY_NO = CIF[27];
                db.IDENTITIES.Add(IDEN);

                if (CIF[72].Length > 0)
                {
                    try
                    {
                        EMPLOYMENT_INFORMATIONS EI = new EMPLOYMENT_INFORMATIONS();
                        EI.BI_ID = NewBi.ID;
                        EI.EMPLOYMENT_DETAIL = Convert.ToInt32(CIF[72]);
                        db.EMPLOYMENT_INFORMATIONS.Add(EI);
                    }
                    catch (Exception e)
                    {

                    }
                    

                }
                db.SaveChanges();
            }
        }
    }
}
