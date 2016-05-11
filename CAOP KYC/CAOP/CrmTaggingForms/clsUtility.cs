using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Security.Cryptography;
using System.Configuration;
using System.Web.UI;

namespace Profile
{
    public class clsUtility
    {        
        public static void WriteError(string Message)
        {
            StreamWriter write = null;
            try
            {
                string Path = HttpContext.Current.Request.ApplicationPath;
                string AppPath = HttpContext.Current.Request.MapPath(Path);
                
                Path = AppPath + "\\Log";
                if (Directory.Exists(Path) == false)
                    Directory.CreateDirectory(Path);
                Path = Path + "\\Log_" + DateTime.Now.ToString("dd_MMM_yyyy") + ".txt";
                write = new StreamWriter(Path, true);
                write.WriteLine(DateTime.Now + " ---- " + Message);
                write.Close();
            }
            catch (Exception)
            {

            }
            finally
            {
                if (write != null)
                    write.Close();
            }

        }

        public static void WriteLog(string Message)
        {
            StreamWriter write = null;
            try
            {
                string Path = HttpContext.Current.Request.ApplicationPath;
                string AppPath = HttpContext.Current.Request.MapPath(Path);

                Path = AppPath + "\\RequestResponseLog";
                if (Directory.Exists(Path) == false)
                    Directory.CreateDirectory(Path);
                Path = Path + "\\RequestResponseLog_" + DateTime.Now.ToString("dd_MMM_yyyy") + ".txt";
                write = new StreamWriter(Path, true);
                write.WriteLine(DateTime.Now + " ---- " + Message);
                write.Close();
            }
            catch (Exception)
            {

            }
            finally
            {
                if (write != null)
                    write.Close();
            }

        }

        public static void RemoveFile(string FilePath)
        {            
            try
            {
                File.Delete(FilePath);
            }
            catch (Exception)
            {

            }
        }
     
        public static void WriteToFile(string Data,string FilePath)
        {
            StreamWriter write=null;
            try
            {
                write = new StreamWriter(FilePath,true);
                write.WriteLine(Data);
                write.Close();
            }
            catch (Exception)
            {
                if (write==null)
                  write.Close();
            }
        }

        public static string GetDate()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString;
            clsDataBaseSql mobjDatabase = new clsDataBaseSql();
            string Sql = "select replace(CONVERT(varchar(11),getdate(),113),' ','-')";
            return mobjDatabase.ExecuteScalar(ConnectionString, Sql);
        }

        public static void ShowPopUpMsg(string msg,Control Page)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("alert('");
            sb.Append(msg.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'"));
            sb.Append("');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showalert", sb.ToString(), true);
        }

        public static void ActivityLog(string userName,string Action)
        {            
            try
            {
                return;
                string ConnectionString = ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString;
                clsDataBaseSql mobjDatabase = new clsDataBaseSql();
                Action = Action.Replace("\n", "\\n").Replace("\r", "").Replace("'", "");
                string Sql = "Insert into Z_ActivityLog values('" + userName + "',getdate(),'" + Action + "','"+HttpContext.Current.Session.SessionID+ "')";
                mobjDatabase.ExecuteCommandNoLog(ConnectionString, Sql);
            }
            catch (Exception ex)
            {
               WriteError("Could not write log UserName="+userName+",date="+DateTime.Now.ToString("")+",Action="+Action+",SessionID="+ HttpContext.Current.Session.SessionID+", ErrorMessage = "+ex.Message);
            }

        }
    }
}
