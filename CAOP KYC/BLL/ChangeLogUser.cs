using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class ChangeLogUser
    {
        public decimal ApplyTo { get; set; }
        public string FieldName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public decimal ChangedBy { get; set; }
        public string Comments { get; set; }
        public Nullable<System.DateTime> ChangeDate { get; set; }

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
        public void MakePassLog(string NewPass, int ChangeById, int Applyto)
        {
            using (CAOPDbContext db = new CAOPDbContext())
            {
                var OldPass = db.USERS.FirstOrDefault(u => u.USER_ID == Applyto).PASSWORD;

                ChangeLog NewLog = new ChangeLog
                {
                    ApplyTo = Applyto,
                    FieldName = "PASSWORD",
                    OldValue = OldPass,
                    NewValue = encrypt(NewPass),
                    ChangedBy = ChangeById,
                    ChangeDate = DateTime.Now

                };
                db.ChangeLog.Add(NewLog);
                db.SaveChanges();
            }
        }

        public void MakeUserLog(User UpdatedUser, int ChangeById)
        {
            using(CAOPDbContext db = new CAOPDbContext())
            {
                var OldUser = db.USERS.FirstOrDefault(u => u.USER_ID == UpdatedUser.USER_ID);

                if (OldUser.DISPLAY_NAME != UpdatedUser.DISPLAY_NAME)
                {
                    ChangeLog NewLog = new ChangeLog
                    {
                        ApplyTo = UpdatedUser.USER_ID,
                        FieldName = "DISPLAY_NAME",
                        OldValue = OldUser.DISPLAY_NAME,
                        NewValue = UpdatedUser.DISPLAY_NAME,
                        ChangedBy = ChangeById,
                        ChangeDate = DateTime.Now

                    };
                    db.ChangeLog.Add(NewLog);
                }

                if (OldUser.EMAIL != UpdatedUser.EMAIL)
                {
                    ChangeLog NewLog = new ChangeLog
                    {
                        ApplyTo = UpdatedUser.USER_ID,
                        FieldName = "EMAIL",
                        OldValue = OldUser.EMAIL,
                        NewValue = UpdatedUser.EMAIL,
                        ChangedBy = ChangeById,
                        ChangeDate = DateTime.Now

                    };
                    db.ChangeLog.Add(NewLog);
                }

                if (OldUser.DESIGNATION != UpdatedUser.DESIGNATION)
                {
                    ChangeLog NewLog = new ChangeLog
                    {
                        ApplyTo = UpdatedUser.USER_ID,
                        FieldName = "DESIGNATION",
                        OldValue = OldUser.DESIGNATION,
                        NewValue = UpdatedUser.DESIGNATION,
                        ChangedBy = ChangeById,
                        ChangeDate = DateTime.Now

                    };
                    db.ChangeLog.Add(NewLog);
                }

                if (OldUser.SAPID != UpdatedUser.SAPID)
                {
                    ChangeLog NewLog = new ChangeLog
                    {
                        ApplyTo = UpdatedUser.USER_ID,
                        FieldName = "DESIGNATION",
                        OldValue = OldUser.SAPID.ToString(),
                        NewValue = UpdatedUser.SAPID.ToString(),
                        ChangedBy = ChangeById,
                        ChangeDate = DateTime.Now

                    };
                    db.ChangeLog.Add(NewLog);
                }

                int OldRoleID = db.USERS_ROLES.FirstOrDefault(r => r.USER_ID == UpdatedUser.USER_ID).ROLE_ID;
                if (UpdatedUser.Role.ID != OldRoleID)
                {
                    ChangeLog NewLog = new ChangeLog
                    {
                        ApplyTo = UpdatedUser.USER_ID,
                        FieldName = "ROLE",
                        OldValue = OldRoleID.ToString(),
                        NewValue = UpdatedUser.Role.ID.ToString(),
                        ChangedBy = ChangeById,
                        ChangeDate = DateTime.Now

                    };
                    db.ChangeLog.Add(NewLog);
                }

                db.SaveChanges();


            }
           
        }


    }
}
