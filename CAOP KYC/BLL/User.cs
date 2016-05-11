using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Net.Mail;

namespace BLL
{
    public class User
    {
        #region User Model
        public int USER_ID { get; set; }
        public int PARENT_ID { get; set; }
        public string USER_NAME { get; set; }
        public string DISPLAY_NAME { get; set; }
        public string EMAIL { get; set; }
        public string DESIGNATION { get; set; }
        public UserType USER_TYPE { get; set; }
        public string PASSWORD { get; set; }
        public List<Permission> Permissions { get; set; }
        public Branch Branch { get; set; }
        public Region Region { get; set; }
        public Role Role { get; set; }
        public Nullable<decimal> SAPID { get; set; }
        public Nullable<int> CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATETIME { get; set; }
        public Nullable<System.DateTime> UPDATED_DATETIME { get; set; }
        public Nullable<int> UPDATED_BY { get; set; }

        public Nullable<bool> ACTIVE { get; set; }

        public int? Category_ID { get; set; }

        #endregion

        public bool Login(string UserName, string Password)
        {
           Password = encrypt(Password);
            
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (db.USERS.Where(u => u.USER_NAME == UserName && u.PASSWORD == Password && u.ACTIVE == true).Any())
                {
                    var LogedUser = db.USERS.FirstOrDefault(u => u.USER_NAME == UserName && u.PASSWORD == Password);
                    this.USER_ID = LogedUser.USER_ID;
                    this.PARENT_ID = LogedUser.PARENT_ID;
                    this.USER_NAME = LogedUser.USER_NAME;
                    this.DISPLAY_NAME = LogedUser.DISPLAY_NAME;
                    this.EMAIL = LogedUser.EMAIL;
                    this.DESIGNATION = LogedUser.DESIGNATION;

                    if (LogedUser.USER_TYPE.ToLower() == UserType.Branch.ToString().ToLower())
                    {
                        this.USER_TYPE = UserType.Branch;
                        var LogedUserBranch = db.BRANCHES.FirstOrDefault(b => b.BRANCH_ID == this.PARENT_ID);
                        var LogedUserRegion = db.REGIONS.FirstOrDefault(r => r.REGION_ID == LogedUserBranch.REGION_ID);
                        this.Branch = new Branch(LogedUserBranch.BRANCH_ID, LogedUserBranch.NAME, LogedUserBranch.BRANCH_CODE, new Region(LogedUserRegion.REGION_ID, LogedUserRegion.NAME), LogedUserBranch.AREA);
                        this.Region = new Region(LogedUserRegion.REGION_ID, LogedUserRegion.NAME);
                    }

                    else if (LogedUser.USER_TYPE.ToLower() == UserType.Region.ToString().ToLower())
                    {
                        var LogedUserRegion = db.REGIONS.FirstOrDefault(r => r.REGION_ID == this.PARENT_ID);
                        this.Region = new Region(LogedUserRegion.REGION_ID, LogedUserRegion.NAME);
                        this.USER_TYPE = UserType.Region;

                    }
                    else if (LogedUser.USER_TYPE.ToLower() == UserType.ITOPERATION.ToString().ToLower())
                    {
                        this.USER_TYPE = UserType.ITOPERATION;
                    }

                    var UserRole = LogedUser.USERS_ROLES.FirstOrDefault();

                    var RoleName = db.ROLES.FirstOrDefault(r => r.ID == UserRole.ROLE_ID);

                    this.Role = new Role(RoleName.ID, RoleName.Role);

                    this.Permissions = new List<Permission>();


                    if (LogedUser.USERS_PERMISSIONS.Count > 0)
                    {
                        foreach (var permission in LogedUser.USERS_PERMISSIONS)
                        {
                            var RolePermission = db.ROLE_PERMISSIONS.FirstOrDefault(rp => rp.PERMISSION_ID == permission.PERMISSION_ID && rp.ROLE_ID == UserRole.ROLE_ID);

                            Permission newPermission = new Permission();
                            newPermission.Create = RolePermission.CREATE;
                            newPermission.Delete = RolePermission.DELETE;
                            newPermission.Read = RolePermission.READ;
                            newPermission.Update = RolePermission.UPDATE;

                            string perp = db.Permissions.FirstOrDefault(p => p.ID == RolePermission.PERMISSION_ID).Permission.ToLower();
                            switch (perp)
                            {
                                case "cif":
                                    newPermission.CurrentPermission = BLL.Permissions.CIF;
                                    break;
                                case "ac":
                                    newPermission.CurrentPermission = BLL.Permissions.AC;
                                    break;
                                case "user":
                                    newPermission.CurrentPermission = BLL.Permissions.USER;
                                    break;
                            }

                            this.Permissions.Add(newPermission);


                        }
                    }

                    
                    return true;
                }
                else
                    return false;


            }


        }

        public void GetUser(int UserID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var User = db.USERS.FirstOrDefault(u => u.USER_ID == UserID);
                this.USER_NAME = User.USER_NAME;
                this.DISPLAY_NAME = User.DISPLAY_NAME;
                this.EMAIL = User.EMAIL;
                this.DESIGNATION = User.DESIGNATION;
                this.SAPID = User.SAPID;
                this.ACTIVE = User.ACTIVE;

                if (User.USER_TYPE.ToLower() == UserType.Branch.ToString().ToLower())
                {
                    this.USER_TYPE = UserType.Branch;
                    this.Branch = new Branch() { BRANCH_ID = User.PARENT_ID, BRANCH_CODE = db.BRANCHES.FirstOrDefault(b => b.BRANCH_ID == User.PARENT_ID).BRANCH_CODE };
                    this.Role = new Role() { ID = db.USERS_ROLES.FirstOrDefault(r => r.USER_ID == UserID).ROLE_ID };
                    
                }

                else if (User.USER_TYPE.ToLower() == UserType.Region.ToString().ToLower())
                {
                    this.USER_TYPE = UserType.Region;
                    this.Region = new Region() { REGION_ID = User.PARENT_ID };
                }
            }
        }

        public void UpdateUser(int UserID)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
               var EditedUser = db.USERS.FirstOrDefault(u => u.USER_ID == UserID);
               EditedUser.DISPLAY_NAME = this.DISPLAY_NAME;
               EditedUser.EMAIL = this.EMAIL;
               EditedUser.DESIGNATION = this.DESIGNATION;
               EditedUser.SAPID = this.SAPID;
               EditedUser.ACTIVE = this.ACTIVE;

               if (this.Region != null)
               {
                   EditedUser.USER_TYPE = UserType.Region.ToString();
                   EditedUser.PARENT_ID = this.Region.REGION_ID;
                   var UR = db.USERS_ROLES.FirstOrDefault(u => u.USER_ID == UserID);
                   UR.ROLE_ID = db.ROLES.FirstOrDefault(r => r.Role == Roles.COMPLIANCE_OFFICER.ToString()).ID;

               }
               else if (this.Branch != null)
               {
                   EditedUser.USER_TYPE = UserType.Branch.ToString();
                   EditedUser.PARENT_ID = db.BRANCHES.FirstOrDefault(b => b.BRANCH_CODE == this.Branch.BRANCH_CODE).BRANCH_ID;
                   var UR = db.USERS_ROLES.FirstOrDefault(u => u.USER_ID == UserID);
                   UR.ROLE_ID = this.Role.ID;
               }

               db.SaveChanges();
            }
        }


        public List<User> SearchUser(string SearchTxt,bool UserName, bool Email, bool SapId, bool Branch)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                List<User> Users;
                int branchid = -1;



                var query = (from user in db.USERS
                             join userrole in db.USERS_ROLES on user.USER_ID equals userrole.USER_ID
                             join role in db.ROLES on userrole.ROLE_ID equals role.ID
                             where role.Role != BLL.Roles.IT_OPERATIONS.ToString()
                             select new BLL.User()
                             {
                                 Role = new Role()
                                 {
                                     ID = userrole.ROLE_ID,
                                     Name = role.Role
                                 },
                                 USER_NAME = user.USER_NAME,
                                 EMAIL = user.EMAIL,
                                 SAPID = user.SAPID,
                                 USER_ID = user.USER_ID
                                 
                             });
                if (UserName)
                    query = query.Where(c => c.USER_NAME.Contains(SearchTxt));
                else if (Email)
                    query = query.Where(c => c.EMAIL.Contains(SearchTxt));
                else if (SapId)
                {
                    decimal sapid = Convert.ToDecimal(SearchTxt);
                    query = query.Where(c => c.SAPID == sapid );
                }
                    
                else if (Branch)
                {
                    if (SearchTxt.Length < 4)
                    {
                        int padding = 4 - SearchTxt.Length;
                        SearchTxt.PadLeft(padding, '0');

                    }

                 query =   (from user in db.USERS
                     join userrole in db.USERS_ROLES on user.USER_ID equals userrole.USER_ID
                     join role in db.ROLES on userrole.ROLE_ID equals role.ID
                     join branch in db.BRANCHES on user.PARENT_ID equals branch.BRANCH_ID
                     where user.USER_TYPE == BLL.UserType.Branch.ToString() && branch.BRANCH_CODE == SearchTxt
                     select new BLL.User()
                     {
                         Role = new Role()
                         {
                             ID = userrole.ROLE_ID,
                             Name = role.Role
                         },
                         USER_NAME = user.USER_NAME,
                         EMAIL = user.EMAIL,
                         SAPID = user.SAPID,
                         USER_ID = user.USER_ID


                     });

                 //   List<int> branches = new List<int>();
                    
                 //   branchid = db.BRANCHES.FirstOrDefault(b => b.BRANCH_CODE == SearchTxt).BRANCH_ID;
                  //  query = query.Where(u => u.)
                  //  query = query.Where(c => c.Branch.BRANCH_ID == branchid && c.USER_TYPE.ToString() == BLL.UserType.Branch.ToString().ToLower());
                }

                Users = query.ToList();
                   return Users;
               
            }
           
 
        }

        public void InsertUser(string branchcode, int CreatedById,string email, string pass)
        {
            using(CAOPDbContext db = new CAOPDbContext())
            {
                USERS newUser = new USERS();
                
                if (this.Region != null)
                {                     
                    newUser.PARENT_ID = this.Region.REGION_ID;
                    newUser.USER_TYPE = BLL.UserType.Region.ToString();
                    newUser.USER_NAME = this.USER_NAME;
                    newUser.DISPLAY_NAME = this.DISPLAY_NAME;
                    newUser.EMAIL = this.EMAIL;
                    newUser.DESIGNATION = this.DESIGNATION;
                    newUser.PASSWORD = this.PASSWORD;
                    newUser.SAPID = this.SAPID;
                    newUser.CREATED_BY = CreatedById;
                    newUser.CREATED_DATETIME = DateTime.Now;
                    newUser.FIRST_LOGIN = true;
                    newUser.ACTIVE = true;
                    db.USERS.Add(newUser);
                    db.SaveChanges();

                    USERS_ROLES newRole = new USERS_ROLES();
                    newRole.USER_ID = newUser.USER_ID;
                    newRole.ROLE_ID = db.ROLES.FirstOrDefault(r => r.Role == BLL.Roles.COMPLIANCE_OFFICER.ToString()).ID;
                    db.USERS_ROLES.Add(newRole);

                    USERS_PERMISSIONS newPerpcif = new USERS_PERMISSIONS();
                    USERS_PERMISSIONS newPerpac = new USERS_PERMISSIONS();

                    newPerpcif.PERMISSION_ID = db.Permissions.FirstOrDefault(p => p.Permission == BLL.Permissions.CIF.ToString()).ID;
                    newPerpac.PERMISSION_ID = db.Permissions.FirstOrDefault(p => p.Permission == BLL.Permissions.AC.ToString()).ID;

                    newPerpcif.USER_ID = newUser.USER_ID;
                    newPerpac.USER_ID = newUser.USER_ID;

                    db.USERS_PERMISSIONS.Add(newPerpcif);
                    db.USERS_PERMISSIONS.Add(newPerpac);

                    db.SaveChanges();


                }
                else
                {
                    newUser.PARENT_ID = db.BRANCHES.FirstOrDefault(b => b.BRANCH_CODE == branchcode).BRANCH_ID;
                    newUser.USER_TYPE = BLL.UserType.Branch.ToString();

                    newUser.USER_NAME = this.USER_NAME;
                    newUser.DISPLAY_NAME = this.DISPLAY_NAME;
                    newUser.EMAIL = this.EMAIL;
                    newUser.DESIGNATION = this.DESIGNATION;
                    newUser.PASSWORD = this.PASSWORD;
                    newUser.FIRST_LOGIN = true;
                    newUser.SAPID = this.SAPID;
                    newUser.CREATED_BY = CreatedById;
                    newUser.CREATED_DATETIME = DateTime.Now;
                    newUser.ACTIVE = true;
                    db.USERS.Add(newUser);
                    db.SaveChanges();

                    USERS_ROLES newRole = new USERS_ROLES();
                    newRole.USER_ID = newUser.USER_ID;
                    newRole.ROLE_ID = this.Role.ID;
                    db.USERS_ROLES.Add(newRole);

                    USERS_PERMISSIONS newPerpcif = new USERS_PERMISSIONS();
                    USERS_PERMISSIONS newPerpac = new USERS_PERMISSIONS();

                    newPerpcif.PERMISSION_ID = db.Permissions.FirstOrDefault(p => p.Permission == BLL.Permissions.CIF.ToString()).ID;
                    newPerpac.PERMISSION_ID = db.Permissions.FirstOrDefault(p => p.Permission == BLL.Permissions.AC.ToString()).ID;

                    newPerpcif.USER_ID = newUser.USER_ID;
                    newPerpac.USER_ID = newUser.USER_ID;

                    db.USERS_PERMISSIONS.Add(newPerpcif);
                    db.USERS_PERMISSIONS.Add(newPerpac);

                    db.SaveChanges();

                }

                sendEmail(this.DISPLAY_NAME, this.USER_NAME, decrypt(this.PASSWORD), this.EMAIL,email,pass);
            }

           
        }

        public void ChangePassword(int userid, string password)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var LogedUser = db.USERS.FirstOrDefault(u => u.USER_ID == userid);

                if ((bool) LogedUser.FIRST_LOGIN)
                    LogedUser.FIRST_LOGIN = false;
                LogedUser.PASSWORD = encrypt(password);
                db.SaveChanges();
            }
        }

        public void ChangePasswordWithEmail(int userid, string password, string remail, string rpass)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var LogedUser = db.USERS.FirstOrDefault(u => u.USER_ID == userid);

                LogedUser.PASSWORD = encrypt(password);
                db.SaveChanges();

                SendChangePasswordEmail(userid,password,remail,rpass);
            }
        }

        public bool CheckFirstLoginFlag(int userid)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return (bool) db.USERS.FirstOrDefault(u => u.USER_ID == userid).FIRST_LOGIN;
            }
        }

        public bool CheckPasswordExixts(int userid, string password)
        {
            password = encrypt(password);
            using (CAOPDbContext db = new CAOPDbContext())
            {
               return db.USERS.Where(u => u.USER_ID == userid && u.PASSWORD == password).Any();
            }
        }

        public List<Role> GetAllRoles()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var Roles = db.ROLES.Where(r => r.Role != BLL.Roles.IT_OPERATIONS.ToString()).Select(r => new Role() { ID = r.ID, Name = r.Role }).ToList();
                return Roles;
                
            }
        }

        public List<Region> GetAllRegions()
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var Regions = db.REGIONS.Select(r => new Region() { REGION_ID = r.REGION_ID, NAME = r.NAME }).ToList();
                return Regions;

            }
        }

        public bool CheckEmailExixts(string email)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.USERS.Where(u => u.EMAIL == email).Any();
            }
        }

        public bool CheckEmailExixts(string email,int userid)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.USERS.Where(u => u.EMAIL == email && u.USER_ID != userid).Any();
            }
        }

        public bool CheckSapIdExixts(decimal sapid)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.USERS.Where(u => u.SAPID == sapid).Any();
            }
        }
        public bool CheckSapIdExixts(decimal sapid, int userid)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                return db.USERS.Where(u => u.SAPID == sapid && u.USER_ID != userid).Any();
            }
        }

        public bool CheckBranchcodeExists(string branchCode)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                if (branchCode.Length < 4)
                {
                    int padding = 4 - branchCode.Length ;
                    branchCode.PadLeft(padding, '0');
                }

                return db.BRANCHES.Where(b => b.BRANCH_CODE == branchCode).Any();
            }
        }

        public string encrypt(string str)
        {
            string encodedData = "";

            if (str.Trim().Length > 0)
            {
                byte[] encData_byte = new byte[str.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(str);
                encodedData = Convert.ToBase64String(encData_byte);
            }

            return encodedData;
        }

        public string decrypt(string str)
        {
            string retString = "";

            if (str.Trim().Length > 0)
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(str);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                retString = new String(decoded_char);
            }

            return retString;
        }

        private void SendChangePasswordEmail(int userid, string newpass, string rEmail, string rpass)
        {

            USERS PassUser;
            using (CAOPDbContext db = new CAOPDbContext())
            {
                PassUser = db.USERS.FirstOrDefault(u => u.USER_ID == userid);
            }

            try
            {

                MailMessage mailObj = new MailMessage();
                mailObj.From = new MailAddress(rEmail);

                mailObj.To.Add(PassUser.EMAIL);
                mailObj.Subject = "AOS - Password Reset";
                mailObj.IsBodyHtml = true;

                string strMsg = "";
                strMsg += "Dear " + PassUser.DISPLAY_NAME.ToUpper() + ",<br/>";
                strMsg += "Your Password Has been Changed,  Dated : " + DateTime.Now.ToString() + " .<br/><br/>";
                strMsg += "User Name :<br/><span style='background-color:pink;'>" + PassUser.USER_NAME + "</span>" + "<br/>";
                strMsg += "Password :<br/><span style='background-color:pink;'>" + newpass + "</span>";
                strMsg += "<br/><br/>";

                strMsg += "<br/><br/><br/>";
                strMsg += "<em>Best Regards,<br/><b>Admin AOS</b></em>";
                strMsg += "<br/><br/><br/>";

                strMsg += "<em>Disclaimer:<br />";
                strMsg += "This message (including any attachments) is confidential and may be privileged.";
                strMsg += " If you have received it by mistake please notify the sender by return e-mail and delete this message from your system.";
                strMsg += " Any unauthorized use or dissemination of this message in whole or in part is strictly prohibited.";
                strMsg += " Please note that e-mails are susceptible to change.";
                strMsg += " National Bank of Pakistan shall not be liable for the improper or incomplete transmission of the information";
                strMsg += " contained in this communication nor for any delay in its receipt or damage to your system.";
                strMsg += " National Bank of Pakistan does not guarantee that the integrity of this communication has been maintained";
                strMsg += " or that this is free of viruses, interceptions or interference.</em>";
                mailObj.Body = strMsg;

                try
                {

                string emailID = rEmail; //web.config (aos@nbp.com.pk);
                string pwd = rpass;  //web.config(itd+1234);

                SmtpClient SMTPServer = new SmtpClient("10.10.102.38");

                System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential(emailID, pwd);

                SMTPServer.UseDefaultCredentials = false;
                SMTPServer.Credentials = SMTPUserInfo;
                SMTPServer.Send(mailObj);

                }
                catch (Exception excEmail)
                {
                   
                }
            }
            catch (Exception e)
            {
 
            }
                                             
         
        }

        private void sendEmail(string strName,string Uname, string Pass, string strEmail,string rEmail, string rpass)
    {
        try
        {
            MailMessage mailObj = new MailMessage();
            mailObj.From = new MailAddress(rEmail);

            mailObj.To.Add(strEmail);
            mailObj.Bcc.Add("afnan.ahmed@nbp.com.pk");
            mailObj.Subject = "AOS - New User Created";
            mailObj.IsBodyHtml = true;

            string strMsg = "";
            strMsg += "Dear " + strName.ToUpper() + ",<br/>";
            strMsg += "Your User Has been Created,  Dated : " + DateTime.Now.ToString() + " .<br/><br/>";
            strMsg += "User Name :<br/><span style='background-color:pink;'>" + Uname + "</span>" + "<br/>";
            strMsg += "Password :<br/><span style='background-color:pink;'>" + Pass + "</span>";
            strMsg += "<br/><br/>";

            strMsg += "<br/><br/><br/>";
            strMsg += "<em>Best Regards,<br/><b>Admin AOS</b></em>";
            strMsg += "<br/><br/><br/>";

            strMsg += "<em>Disclaimer:<br />";
            strMsg += "This message (including any attachments) is confidential and may be privileged.";
            strMsg += " If you have received it by mistake please notify the sender by return e-mail and delete this message from your system.";
            strMsg += " Any unauthorized use or dissemination of this message in whole or in part is strictly prohibited.";
            strMsg += " Please note that e-mails are susceptible to change.";
            strMsg += " National Bank of Pakistan shall not be liable for the improper or incomplete transmission of the information";
            strMsg += " contained in this communication nor for any delay in its receipt or damage to your system.";
            strMsg += " National Bank of Pakistan does not guarantee that the integrity of this communication has been maintained";
            strMsg += " or that this is free of viruses, interceptions or interference.</em>";
            mailObj.Body = strMsg;

            try
            {
                string emailID = rEmail; //web.config (aos@nbp.com.pk);
                string pwd = rpass;  //web.config(itd+1234);

                    SmtpClient SMTPServer = new SmtpClient("10.10.102.38");

                    System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential(emailID, pwd);

                    SMTPServer.UseDefaultCredentials = false;
                    SMTPServer.Credentials = SMTPUserInfo;
                    SMTPServer.Send(mailObj);
                }
                catch (Exception excEmail)
                {
                   
                }
            }
            catch (Exception exc)
            {
               
            }
    }
    }
}
