using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExtensionMethods;
using BLL;

namespace CAOP.MasterPages
{
    public partial class Default : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            User LogedUser =  Session["User"] as User;
            if (Session.Count < 1)
            {
                Response.Redirect("~/Login.aspx");
            }
            else if (LogedUser.CheckFirstLoginFlag(LogedUser.USER_ID))
            {

                throw new UnauthorizedAccessException();
            }
            else
                if (!IsPostBack)
                    SetUserHeader();
        }



        private void SetUserHeader()
        {
          User LogedUser =  Session["User"] as User;

          lblDate.Text = DateTime.Now.ToLongDateString();
          lblUser.Text = LogedUser.DISPLAY_NAME;

          if (LogedUser.Region != null)
              lblRegion.Text = LogedUser.Region.NAME;
          else
              lblRegion.Text = "";

          if (LogedUser.USER_TYPE == UserType.Branch)
          {
              lblBranch.Text = LogedUser.Branch.NAME;
              lblRole.Text = LogedUser.Role.Name.Replace("_", " ");
          }
          else
              lblBranch.Text = "";

          
          

          SetRoles(LogedUser);
        }

        public void Logout()
        {
            Session.Clear();
            Response.Redirect("~/Login.aspx");
        }

        protected void Unnamed_ServerClick(object sender, EventArgs e)
        {
            Logout();
        }

        private void SetRoles(User LogedUser)
        {
            if (LogedUser.Permissions.CheckAccess(Permissions.CIF, Rights.Create))
            {
                NewCif.Visible = true;
                PCif.Visible = true;
               
                EC.Visible = true;

                NewAccount.Visible = true;
                PendingAccount.Visible = true;

               

            }

            if (LogedUser.Role.Name.ToLower() == Roles.BRANCH_OPERATOR.ToString().ToLower())
            {
                LOTF.Visible = true;
                AOF.Visible = true;
                BV.Visible = true;
                crmtagging.Visible = true;
            }

            if (LogedUser.Role.Name.ToLower() == Roles.BRANCH_MANAGER.ToString().ToLower())
            {
                DIPC.Visible = true;
                DIPA.Visible = true;
            }

            if (LogedUser.Permissions.CheckAccess(Permissions.CIF, Rights.Read))
                Cifmenu.Visible = true;

            if (LogedUser.Permissions.CheckAccess(Permissions.AC, Rights.Read))
                 accountmenu.Visible = true;

            if (LogedUser.Permissions.CheckAccess(Permissions.USER, Rights.Create))
                usermenu.Visible = true;

        }

        protected void Main_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Main.aspx");
        }
    }
}