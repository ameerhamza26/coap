using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class AccOpen
    {

        public int BI_ID { get; set; }
        public AccountOpenTypes AccountOpen { get; set; }
        public int UserId { get; set; }


        public AccOpen(int BI_ID, AccountOpenTypes acc)
        {
            this.BI_ID = BI_ID;
            this.AccountOpen = acc;
        }

        public AccOpen(int UserId)
        {
            this.UserId = UserId;
        }

        public List<AccountNatureCurrency> GetPendingAccounts()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                USERS usr = db.USERS.FirstOrDefault(b => b.USER_ID == this.UserId);
                BRANCHES br = db.BRANCHES.FirstOrDefault(b => b.BRANCH_ID == usr.PARENT_ID);
                List<AccountNatureCurrency> PendingAccounts = db.ACCOUNT_NATURE_CURRENCY.Where(b => b.STATUS == "SAVED" && b.USERID == this.UserId && b.BRANCH_CODE == br.BRANCH_CODE)
                     .OrderByDescending(b=>b.LAST_UPDATED) .Select(b => new AccountNatureCurrency { ID = b.ID, ACCOUNT_NUMBER = b.ACCOUNT_NUMBER,LAST_UPDATED= b.LAST_UPDATED, ACCOUNT_TITLE = b.ACCOUNT_TITLE, STATUS=b.STATUS, ACCOUNT_OPEN_TYPE = new AccountOpenType { ID = b.ACCOUNT_OPEN_TYPE, NAME = db.ACCOUNT_OPEN_TYPE.FirstOrDefault(t=> t.ID == b.ACCOUNT_OPEN_TYPE).NAME  } }).ToList();

                return PendingAccounts;
            }
        }


        public bool CheckIfCompleted()
        {
            bool completed = false;
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (AccountOpen == AccountOpenTypes.INDIVIDUAL)
                {
                    completed = (from b in db.ACCOUNT_NATURE_CURRENCY
                                 join i in db.APPLICANT_INFORMATION on b.ID equals i.BI_ID
                                 join j in db.ADDRESS_INFORMATION on b.ID equals j.BI_ID
                                 join k in db.OPERATING_INSTRUCTIONS on b.ID equals k.BI_ID
                                 join l in db.NEXT_OF_KIN_INFO on b.ID equals l.BI_ID
                                 join m in db.KNOW_YOUR_CUSTOMER on b.ID equals m.BI_ID
                                 join n in db.CERTIFICATE_DEPOSIT_INFO on b.ID equals n.BI_ID
                                 join o in db.DOCUMENT_REQUIRED on b.ID equals o.BI_ID
                                 where b.ID == this.BI_ID
                                 select b.ID).Any();
                }
                else
                {
                    completed = (from b in db.ACCOUNT_NATURE_CURRENCY
                                 join i in db.ACCOUNT_CONTACT_INFO on b.ID equals i.BI_ID
                                 join j in db.ACCOUNT_AUTHORIZED_PERSONS on b.ID equals j.BI_ID
                                 join wa in db.WHO_AUTHORIZED on b.ID equals wa.BI_ID
                                 join k in db.OPERATING_INSTRUCTIONS on b.ID equals k.BI_ID
                                 join m in db.KNOW_YOUR_CUSTOMER on b.ID equals m.BI_ID
                                 join n in db.CERTIFICATE_DEPOSIT_INFO on b.ID equals n.BI_ID
                                 join o in db.DOCUMENT_REQUIRED on b.ID equals o.BI_ID                                 
                                 where b.ID == this.BI_ID
                                 select b.ID).Any();


                }
                return completed;
            }

        }

        public AccountOpenTypes GetAccountOpenType(int ID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                string AccTypeNAme = db.ACCOUNT_OPEN_TYPE.FirstOrDefault(c => c.ID == (db.ACCOUNT_NATURE_CURRENCY.FirstOrDefault(b => b.ID == ID).ACCOUNT_OPEN_TYPE)).NAME;
                if (AccTypeNAme == "INDIVIDUAL")
                    return AccountOpenTypes.INDIVIDUAL;
                else if (AccTypeNAme == "GOVERNMENT")
                    return AccountOpenTypes.GOVERNMENT;
                else if (AccTypeNAme == "OFFICE")
                    return AccountOpenTypes.OFFICE;
                else
                    return AccountOpenTypes.BUSINESS;
            }
        }

        public List<AccountNatureCurrency> GetAccountRegion(bool BCode, bool TANo, bool ATitle, string criteria, int RegionID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                List<int> BranchUsers = new List<int>();
                List<AccountNatureCurrency> SubmittedAccounts = new List<AccountNatureCurrency>();
                int ANO  = -1;
                var branches = db.BRANCHES.Where(b => (b.CATEGORY_ID == 1 || b.CATEGORY_ID == 2) && b.REGION_ID == RegionID).Select(b => b.BRANCH_ID).ToList();
                BranchUsers = db.USERS.Where(u => branches.Contains(u.PARENT_ID) && u.USER_TYPE == "BRANCH").Select(u => u.USER_ID).ToList();

                var Accounts = db.ACCOUNT_NATURE_CURRENCY.Where(b => b.STATUS != Status.SAVED.ToString() && BranchUsers.Contains((int)b.USERID));

                if (BCode)
                    Accounts = Accounts.Where(b => b.BRANCH_CODE == criteria);
                else if (TANo)
                {
                    try
                    {
                        ANO = Convert.ToInt32(criteria);

                    }
                    catch (Exception e)
                    {
                        ANO = -1;
                    }
                    finally
                    {
                        Accounts = Accounts.Where(b => b.ID == ANO);
                    }
                }
                else
                {
                    Accounts = Accounts.Where(b => b.ACCOUNT_TITLE.Contains(criteria));
                }

                SubmittedAccounts = Accounts.OrderByDescending(b => b.LAST_UPDATED).Select(b => new AccountNatureCurrency { ID = b.ID, ACCOUNT_NUMBER = b.ACCOUNT_NUMBER, ACCOUNT_TITLE = b.ACCOUNT_TITLE, STATUS = b.STATUS, LAST_UPDATED = b.LAST_UPDATED, ACCOUNT_OPEN_TYPE = new AccountOpenType { ID = (int)b.ACCOUNT_OPEN_TYPE, NAME = db.ACCOUNT_OPEN_TYPE.FirstOrDefault(t => t.ID == b.ACCOUNT_OPEN_TYPE).NAME }, PROFILE_ACCOUNT_NO = b.PROFILE_ACCOUNT_NO, BRANCH_CODE = b.BRANCH_CODE }).ToList();
                return SubmittedAccounts;
            }
        }


        public List<AccountNatureCurrency> GetRejectedAccountsByRole(string UserRole, bool Region)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                List<int> BranchUsers = new List<int>();
                List<AccountNatureCurrency> SubmittedCifs = new List<AccountNatureCurrency>();

                if (UserRole == Roles.BRANCH_OPERATOR.ToString())
                {

                    BranchUsers.Add(this.UserId);
                    USERS usr = db.USERS.FirstOrDefault(b => b.USER_ID == this.UserId);
                    BRANCHES br = db.BRANCHES.FirstOrDefault(b => b.BRANCH_ID == usr.PARENT_ID);
                    SubmittedCifs = db.ACCOUNT_NATURE_CURRENCY.Where(b => b.STATUS == Status.REJECTEBY_COMPLIANCE_MANAGER.ToString() && BranchUsers.Contains((int)b.USERID) && b.BRANCH_CODE == br.BRANCH_CODE)
                        .OrderByDescending(b => b.LAST_UPDATED).Select(b => new AccountNatureCurrency { ID = b.ID, ACCOUNT_NUMBER = b.ACCOUNT_NUMBER, ACCOUNT_TITLE = b.ACCOUNT_TITLE, LAST_UPDATED = b.LAST_UPDATED, STATUS = b.STATUS, ACCOUNT_OPEN_TYPE = new AccountOpenType { ID = (int)b.ACCOUNT_TYPE, NAME = db.ACCOUNT_OPEN_TYPE.FirstOrDefault(t => t.ID == b.ACCOUNT_OPEN_TYPE).NAME }, PROFILE_ACCOUNT_NO = b.PROFILE_ACCOUNT_NO, BRANCH_CODE = b.BRANCH_CODE }).ToList();

                }
                else
                {
                    if (Region)
                    {
                        var branches = db.BRANCHES.Where(b => (b.CATEGORY_ID == 1 || b.CATEGORY_ID == 2) && b.REGION_ID == db.USERS.FirstOrDefault(u => u.USER_ID == this.UserId).PARENT_ID).Select(b => b.BRANCH_ID).ToList();
                        BranchUsers = db.USERS.Where(u => branches.Contains(u.PARENT_ID) && u.USER_TYPE == "BRANCH").Select(u => u.USER_ID).ToList();
                    }
                    else
                    {
                        BranchUsers = db.USERS.Where(u => u.USER_TYPE == "BRANCH" && u.PARENT_ID == (db.USERS.FirstOrDefault(l => l.USER_ID == this.UserId).PARENT_ID)).Select(u => u.USER_ID).ToList();
                    }



                    if (UserRole == Roles.COMPLIANCE_OFFICER.ToString())
                    {

                        SubmittedCifs = db.ACCOUNT_NATURE_CURRENCY.Where(b => b.STATUS == Status.REJECTED_BY_BRANCH_MANAGER.ToString() && BranchUsers.Contains((int)b.USERID))
                        .OrderByDescending(b => b.LAST_UPDATED).Select(b => new AccountNatureCurrency { ID = b.ID, ACCOUNT_NUMBER = b.ACCOUNT_NUMBER, ACCOUNT_TITLE = b.ACCOUNT_TITLE, STATUS = b.STATUS, LAST_UPDATED = b.LAST_UPDATED, ACCOUNT_OPEN_TYPE = new AccountOpenType { ID = (int)b.ACCOUNT_OPEN_TYPE, NAME = db.ACCOUNT_OPEN_TYPE.FirstOrDefault(t => t.ID == b.ACCOUNT_OPEN_TYPE).NAME }, PROFILE_ACCOUNT_NO = b.PROFILE_ACCOUNT_NO, BRANCH_CODE = b.BRANCH_CODE }).ToList();
                    }
                    else if (UserRole == Roles.BRANCH_MANAGER.ToString())
                    {
                        SubmittedCifs = db.ACCOUNT_NATURE_CURRENCY.Where(b => b.STATUS == Status.REJECTED_BY_BRANCH_MANAGER.ToString() && BranchUsers.Contains((int)b.USERID))
                        .OrderByDescending(b => b.LAST_UPDATED).Select(b => new AccountNatureCurrency { ID = b.ID, ACCOUNT_NUMBER = b.ACCOUNT_NUMBER, ACCOUNT_TITLE = b.ACCOUNT_TITLE, STATUS = b.STATUS, LAST_UPDATED = b.LAST_UPDATED, ACCOUNT_OPEN_TYPE = new AccountOpenType { ID = (int)b.ACCOUNT_OPEN_TYPE, NAME = db.ACCOUNT_OPEN_TYPE.FirstOrDefault(t => t.ID == b.ACCOUNT_OPEN_TYPE).NAME }, PROFILE_ACCOUNT_NO = b.PROFILE_ACCOUNT_NO, BRANCH_CODE = b.BRANCH_CODE }).ToList();
                    }
                }

                return SubmittedCifs;
            }
        }

        public List<AccountNatureCurrency> GetInProcessAccountsByRole(string UserRole, bool Region)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                List<int> BranchUsers = new List<int>();
                List<AccountNatureCurrency> SubmittedCifs = new List<AccountNatureCurrency>();

                if (UserRole == Roles.BRANCH_OPERATOR.ToString())
                {

                    BranchUsers.Add(this.UserId);
                    USERS usr = db.USERS.FirstOrDefault(b => b.USER_ID == this.UserId);
                    BRANCHES br = db.BRANCHES.FirstOrDefault(b => b.BRANCH_ID == usr.PARENT_ID);
                    SubmittedCifs = db.ACCOUNT_NATURE_CURRENCY.Where(b => b.STATUS != Status.SAVED.ToString() && b.PROFILE_STATUS != "POSTED" && BranchUsers.Contains((int)b.USERID) && b.BRANCH_CODE == br.BRANCH_CODE)
                        .OrderByDescending(b => b.LAST_UPDATED).Select(b => new AccountNatureCurrency { ID = b.ID, ACCOUNT_NUMBER = b.ACCOUNT_NUMBER, ACCOUNT_TITLE = b.ACCOUNT_TITLE, LAST_UPDATED=b.LAST_UPDATED, STATUS = b.STATUS, ACCOUNT_OPEN_TYPE = new AccountOpenType { ID = (int)b.ACCOUNT_TYPE, NAME = db.ACCOUNT_OPEN_TYPE.FirstOrDefault(t => t.ID == b.ACCOUNT_OPEN_TYPE).NAME }, PROFILE_ACCOUNT_NO = b.PROFILE_ACCOUNT_NO, BRANCH_CODE = b.BRANCH_CODE }).ToList();

                }
                else
                {
                    if (Region)
                    {
                        var branches = db.BRANCHES.Where(b => (b.CATEGORY_ID ==1|| b.CATEGORY_ID ==2)&& b.REGION_ID == db.USERS.FirstOrDefault(u => u.USER_ID == this.UserId).PARENT_ID).Select(b => b.BRANCH_ID).ToList();
                        BranchUsers = db.USERS.Where(u => branches.Contains(u.PARENT_ID) && u.USER_TYPE == "BRANCH").Select(u => u.USER_ID).ToList();
                    }
                    else
                    {
                        BranchUsers = db.USERS.Where(u => u.USER_TYPE == "BRANCH" && u.PARENT_ID == (db.USERS.FirstOrDefault(l => l.USER_ID == this.UserId).PARENT_ID)).Select(u => u.USER_ID).ToList();
                    }



                    if (UserRole == Roles.COMPLIANCE_OFFICER.ToString())
                    {

                        SubmittedCifs = db.ACCOUNT_NATURE_CURRENCY.Where(b => b.STATUS != Status.SAVED.ToString() && b.PROFILE_STATUS != "POSTED" && BranchUsers.Contains((int)b.USERID))
                        .OrderByDescending(b => b.LAST_UPDATED).Select(b => new AccountNatureCurrency { ID = b.ID, ACCOUNT_NUMBER = b.ACCOUNT_NUMBER, ACCOUNT_TITLE = b.ACCOUNT_TITLE, STATUS = b.STATUS, LAST_UPDATED = b.LAST_UPDATED, ACCOUNT_OPEN_TYPE = new AccountOpenType { ID = (int)b.ACCOUNT_OPEN_TYPE, NAME = db.ACCOUNT_OPEN_TYPE.FirstOrDefault(t => t.ID == b.ACCOUNT_OPEN_TYPE).NAME }, PROFILE_ACCOUNT_NO = b.PROFILE_ACCOUNT_NO, BRANCH_CODE = b.BRANCH_CODE }).ToList();
                    }
                    else if (UserRole == Roles.BRANCH_MANAGER.ToString())
                    {
                        //SubmittedCifs = db.ACCOUNT_NATURE_CURRENCY.Where(b => b.STATUS != Status.SAVED.ToString() && b.STATUS != Status.SUBMITTED.ToString() && b.STATUS != Status.REJECTEBY_COMPLIANCE_MANAGER.ToString() && b.PROFILE_STATUS != "POSTED" && BranchUsers.Contains((int)b.USERID))
                        //.OrderByDescending(b => b.LAST_UPDATED).Select(b => new AccountNatureCurrency { ID = b.ID, ACCOUNT_NUMBER = b.ACCOUNT_NUMBER, ACCOUNT_TITLE = b.ACCOUNT_TITLE, STATUS = b.STATUS, LAST_UPDATED = b.LAST_UPDATED, ACCOUNT_OPEN_TYPE = new AccountOpenType { ID = (int)b.ACCOUNT_OPEN_TYPE, NAME = db.ACCOUNT_OPEN_TYPE.FirstOrDefault(t => t.ID == b.ACCOUNT_OPEN_TYPE).NAME }, PROFILE_ACCOUNT_NO = b.PROFILE_ACCOUNT_NO, BRANCH_CODE = b.BRANCH_CODE }).ToList();

                        SubmittedCifs = db.ACCOUNT_NATURE_CURRENCY.Where(b => b.STATUS != Status.SAVED.ToString() && b.PROFILE_STATUS != "POSTED" && BranchUsers.Contains((int)b.USERID))
                        .OrderByDescending(b => b.LAST_UPDATED).Select(b => new AccountNatureCurrency { ID = b.ID, ACCOUNT_NUMBER = b.ACCOUNT_NUMBER, ACCOUNT_TITLE = b.ACCOUNT_TITLE, STATUS = b.STATUS, LAST_UPDATED = b.LAST_UPDATED, ACCOUNT_OPEN_TYPE = new AccountOpenType { ID = (int)b.ACCOUNT_OPEN_TYPE, NAME = db.ACCOUNT_OPEN_TYPE.FirstOrDefault(t => t.ID == b.ACCOUNT_OPEN_TYPE).NAME }, PROFILE_ACCOUNT_NO = b.PROFILE_ACCOUNT_NO, BRANCH_CODE = b.BRANCH_CODE }).ToList();
                    }
                }

                return SubmittedCifs;
            }
        }

        public List<AccountNatureCurrency> GetAccountsByRole(string UserRole, bool Region)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                List<int> BranchUsers = new List<int>();
                List<AccountNatureCurrency> SubmittedCifs = new List<AccountNatureCurrency>();

                if (UserRole == Roles.BRANCH_OPERATOR.ToString())
                {

                    BranchUsers.Add(this.UserId);
                    USERS usr = db.USERS.FirstOrDefault(b => b.USER_ID == this.UserId);
                    BRANCHES br = db.BRANCHES.FirstOrDefault(b => b.BRANCH_ID == usr.PARENT_ID);
                    SubmittedCifs = db.ACCOUNT_NATURE_CURRENCY.Where(b => b.STATUS != Status.SAVED.ToString() &&  BranchUsers.Contains((int)b.USERID) && b.BRANCH_CODE == br.BRANCH_CODE)
                        .OrderByDescending(b => b.LAST_UPDATED).Select(b => new AccountNatureCurrency { ID = b.ID, ACCOUNT_NUMBER = b.ACCOUNT_NUMBER, ACCOUNT_TITLE = b.ACCOUNT_TITLE, LAST_UPDATED = b.LAST_UPDATED, STATUS = b.STATUS, ACCOUNT_OPEN_TYPE = new AccountOpenType { ID = (int)b.ACCOUNT_TYPE, NAME = db.ACCOUNT_OPEN_TYPE.FirstOrDefault(t => t.ID == b.ACCOUNT_OPEN_TYPE).NAME }, PROFILE_ACCOUNT_NO = b.PROFILE_ACCOUNT_NO }).ToList();

                }
                else
                {
                    if (Region)
                    {
                        var branches = db.BRANCHES.Where(b => (b.CATEGORY_ID == 1 || b.CATEGORY_ID == 2) && b.REGION_ID == db.USERS.FirstOrDefault(u => u.USER_ID == this.UserId).PARENT_ID).Select(b => b.BRANCH_ID).ToList();
                        BranchUsers = db.USERS.Where(u => branches.Contains(u.PARENT_ID) && u.USER_TYPE == "BRANCH").Select(u => u.USER_ID).ToList();
                    }
                    else
                    {
                        BranchUsers = db.USERS.Where(u => u.USER_TYPE == "BRANCH" && u.PARENT_ID == (db.USERS.FirstOrDefault(l => l.USER_ID == this.UserId).PARENT_ID)).Select(u => u.USER_ID).ToList();
                    }



                    if (UserRole == Roles.COMPLIANCE_OFFICER.ToString())
                    {

                        SubmittedCifs = db.ACCOUNT_NATURE_CURRENCY.Where(b => b.STATUS != Status.SAVED.ToString() && BranchUsers.Contains((int)b.USERID))
                        .OrderByDescending(b => b.LAST_UPDATED).Select(b => new AccountNatureCurrency { ID = b.ID, ACCOUNT_NUMBER = b.ACCOUNT_NUMBER, ACCOUNT_TITLE = b.ACCOUNT_TITLE, STATUS = b.STATUS, LAST_UPDATED = b.LAST_UPDATED, ACCOUNT_OPEN_TYPE = new AccountOpenType { ID = (int)b.ACCOUNT_OPEN_TYPE, NAME = db.ACCOUNT_OPEN_TYPE.FirstOrDefault(t => t.ID == b.ACCOUNT_OPEN_TYPE).NAME }, PROFILE_ACCOUNT_NO = b.PROFILE_ACCOUNT_NO }).ToList();
                    }
                    else if (UserRole == Roles.BRANCH_MANAGER.ToString())
                    {
                        SubmittedCifs = db.ACCOUNT_NATURE_CURRENCY.Where(b => b.STATUS != Status.SAVED.ToString() && b.STATUS != Status.SUBMITTED.ToString() && b.STATUS != Status.REJECTEBY_COMPLIANCE_MANAGER.ToString() &&  BranchUsers.Contains((int)b.USERID))
                        .OrderByDescending(b => b.LAST_UPDATED).Select(b => new AccountNatureCurrency { ID = b.ID, ACCOUNT_NUMBER = b.ACCOUNT_NUMBER, ACCOUNT_TITLE = b.ACCOUNT_TITLE, STATUS = b.STATUS, LAST_UPDATED = b.LAST_UPDATED, ACCOUNT_OPEN_TYPE = new AccountOpenType { ID = (int)b.ACCOUNT_OPEN_TYPE, NAME = db.ACCOUNT_OPEN_TYPE.FirstOrDefault(t => t.ID == b.ACCOUNT_OPEN_TYPE).NAME }, PROFILE_ACCOUNT_NO = b.PROFILE_ACCOUNT_NO }).ToList();
                    }
                }

                return SubmittedCifs;
            }
        }

        public List<AccountNatureCurrency> GetAccountByNumber(string AccountNumber, string BranchCode)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var ACCOUNTS = db.ACCOUNT_NATURE_CURRENCY.Where(b => b.PROFILE_ACCOUNT_NO == AccountNumber.ToString() && b.STATUS == Status.APPROVED_BY_BRANCH_MANAGER.ToString() && b.BRANCH_CODE == BranchCode).Select(a => new AccountNatureCurrency() { ID = a.ID, ACCOUNT_ENTRY_DATE = (DateTime)a.ACCOUNT_ENTRY_DATE, ACCOUNT_TITLE = a.ACCOUNT_TITLE, INITIAL_DEPOSIT = a.INITIAL_DEPOSIT, ACCOUNT_TYPE = new AccountType() { ID = (int)a.ACCOUNT_TYPE, Name = db.ACCOMODATION_TYPES.FirstOrDefault(t => t.ID == a.ACCOUNT_TYPE).Name }, PROFILE_ACCOUNT_NO = a.PROFILE_ACCOUNT_NO, ACCOUNT_MODE_DETAIL = (int) a.ACCOUNT_OPEN_TYPE }).ToList();
                return ACCOUNTS;
            }
        }


        public void ChangeStatus(Status status, User LogedUser)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {

                STATUS_LOG newSlog = new STATUS_LOG();
                newSlog.USERID = LogedUser.USER_ID;
                newSlog.BID = this.BI_ID;
                newSlog.OLD_STATUS = db.ACCOUNT_NATURE_CURRENCY.FirstOrDefault(b => b.ID == this.BI_ID).STATUS;
                newSlog.NEW_STATUS = status.ToString();
                newSlog.LOG_DATETIME = DateTime.Now;
                newSlog.LOG_TYPE = "ACCOUNT";
                db.STATUS_LOG.Add(newSlog);
                db.SaveChanges();

                var BI = db.ACCOUNT_NATURE_CURRENCY.FirstOrDefault(b => b.ID == this.BI_ID);
                BI.STATUS = status.ToString();
                BI.LAST_UPDATED = DateTime.Now;
                db.SaveChanges();
            }
        }

        public bool CheckStatus(int BId, string status)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.ACCOUNT_NATURE_CURRENCY.Where(b => b.ID == BId && b.STATUS == status).Any();
            }
        }

        public bool CheckStatus(int BId, string[] Statuses)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.ACCOUNT_NATURE_CURRENCY.Where(b => b.ID == BId && Statuses.Contains(b.STATUS)).Any();
            }
        }

        public void DelAccountManager(int AccountId, int UserId)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
 
            }
        }
        public void DelAccount(int AccountId,int UserID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                ACCOUNT_DELTE_LOG AD = new ACCOUNT_DELTE_LOG();
                var ANC = db.ACCOUNT_NATURE_CURRENCY.FirstOrDefault(a => a.ID == AccountId);
                if (ANC != null)
                {
                    AD.ACCOUNT_ENTRY_DATE = ANC.ACCOUNT_ENTRY_DATE;
                    AD.TEMP_AC_NO = ANC.ID.ToString();
                    AD.BRANCH_CODE = ANC.BRANCH_CODE;
                    AD.ACCOUNT_TITLE = ANC.ACCOUNT_TITLE;
                    AD.INITIAL_DEPOSIT = ANC.INITIAL_DEPOSIT;
                    AD.ACCOUNT_MODE = ANC.ACCOUNT_MODE_DETAIL;
                    AD.ACCOUNT_TYPE = ANC.ACCOUNT_TYPE;
                    AD.CREATED_USER = ANC.USERID;
                    AD.DELETED_USER = UserID;
                    AD.DELETE_DATETIME = DateTime.Now;

                    db.ACCOUNT_NATURE_CURRENCY.Remove(ANC);
                }
                    

                var AI = db.APPLICANT_INFORMATION.FirstOrDefault(a => a.BI_ID == AccountId);
                if (AI != null)
                    db.APPLICANT_INFORMATION.Remove(AI);

                //var AIC = db.APPLICANT_INFORMATION_CIFS.Where(a => a.BI_ID == AccountId).ToList();
                //if (AIC.Count > 0)
                //{
                //    int CIFID = Convert.ToInt32(db.APPLICANT_INFORMATION_CIFS.FirstOrDefault(a => a.BI_ID == AccountId && a.IS_PRIMARY_ACCOUNT_HOLDER == 1).CUSTOMER_CIF_NO);
                //    AD.CNIC = db.BASIC_INFORMATIONS.FirstOrDefault(b => b.ID == CIFID).CNIC;
                //    db.APPLICANT_INFORMATION_CIFS.RemoveRange(AIC);
                //}

                AD.CNIC = "";
                  

                var ADD_INFO = db.ADDRESS_INFORMATION.FirstOrDefault(a => a.BI_ID == AccountId);
                if (ADD_INFO != null)
                    db.ADDRESS_INFORMATION.Remove(ADD_INFO);

                var Emails = db.EMAILS.Where(e => e.BI_ID == AccountId);
                if (Emails != null)
                    db.EMAILS.RemoveRange(Emails);

                var OI = db.OPERATING_INSTRUCTIONS.FirstOrDefault(o => o.BI_ID == AccountId);
                if (OI != null)
                    db.OPERATING_INSTRUCTIONS.Remove(OI);

                var NOK = db.NEXT_OF_KIN_INFO.FirstOrDefault(n => n.BI_ID == AccountId);
                if (NOK != null)
                    db.NEXT_OF_KIN_INFO.Remove(NOK);

                var KYC = db.KNOW_YOUR_CUSTOMER.FirstOrDefault(k => k.BI_ID == AccountId);
                if (KYC != null)
                    db.KNOW_YOUR_CUSTOMER.Remove(KYC);


                var KYC_TMODE = db.KNOW_CUSTOMER_TRANSACTIONS_MODE.Where(k => k.BI_ID == AccountId);
                if (KYC_TMODE != null)
                    db.KNOW_CUSTOMER_TRANSACTIONS_MODE.RemoveRange(KYC_TMODE);

                var KYC_BenefEntityE = db.KYC_BENEFICIAL_ENTITY.Where(k => k.BID == AccountId);
                if (KYC_BenefEntityE != null)
                    db.KYC_BENEFICIAL_ENTITY.RemoveRange(KYC_BenefEntityE);

                var KYC_POA = db.KYC_PURPOSE_OF_AC.Where(k => k.BID == AccountId);
                if (KYC_POA != null)
                    db.KYC_PURPOSE_OF_AC.RemoveRange(KYC_POA);

                var KYC_SOF = db.KYC_SOURCE_OF_FUND.Where(k => k.BID == AccountId);
                if (KYC_SOF != null)
                    db.KYC_SOURCE_OF_FUND.RemoveRange(KYC_SOF);

                var KYC_GCP = db.KYC_GEOGRAPHIES_COUNTER_PARTIES.Where(k => k.BID == AccountId);
                if (KYC_GCP != null)
                    db.KYC_GEOGRAPHIES_COUNTER_PARTIES.RemoveRange(KYC_GCP);

                var KYC_ECP = db.KYC_EXPECTED_COUNTER_PARTIES.Where(k => k.BID == AccountId);
                if (KYC_ECP != null)
                    db.KYC_EXPECTED_COUNTER_PARTIES.RemoveRange(KYC_ECP);

                var CDI = db.CERTIFICATE_DEPOSIT_INFO.FirstOrDefault(d => d.BI_ID == AccountId);
                if (CDI != null)
                    db.CERTIFICATE_DEPOSIT_INFO.Remove(CDI);

                var Doc = db.DOCUMENT_REQUIRED.Where(d => d.BI_ID == AccountId);
                if (Doc != null)
                    db.DOCUMENT_REQUIRED.RemoveRange(Doc);

                var DocReq = db.DOCUMENT_REQUIRED.Where(d => d.BI_ID == AccountId);
                if (DocReq != null)
                    db.DOCUMENT_REQUIRED.RemoveRange(DocReq);

                var aap = db.ACCOUNT_AUTHORIZED_PERSONS.FirstOrDefault(a => a.BI_ID == AccountId);
                if (aap != null)
                    db.ACCOUNT_AUTHORIZED_PERSONS.Remove(aap);

                var who = db.WHO_AUTHORIZED.Where(w => w.BI_ID == AccountId);
                if (who != null)
                    db.WHO_AUTHORIZED.RemoveRange(who);

                var Doc_Req = db.DOCUMENT_REQUIRED.FirstOrDefault(d => d.BI_ID == AccountId);
                if (Doc_Req != null)
                    db.DOCUMENT_REQUIRED.Remove(Doc_Req);

                db.ACCOUNT_DELTE_LOG.Add(AD);

                db.SaveChanges();
            }
        }
    }
}