using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using System.Web.UI.WebControls;

namespace ExtensionMethods
{
    public static class ListExtensions
    {

        public static string RemoveNull(string Value)
        {
            if (Value == null)
                return "";
            else
                return Value;
        }

        public static void SetDropdownValue(int? value,DropDownList ddl )
        {
            if (value == null)
            {
                ddl.Items.FindByText("Select").Selected = true;
            }
            else
            {
                ddl.Items.FindByValue(value.ToString()).Selected = true;
            }
        }

        public static int? getSelectedValue(DropDownList ddl)
        {
            return ddl.SelectedIndex == 0 ? (int?)null : Convert.ToInt32(ddl.SelectedValue);
        }


        public static bool CheckAccess(this IEnumerable<Permission> userPermissions, Permissions givenPermission, Rights givenRight)
        {
           
            bool HavePermission = userPermissions.Where(up => up.CurrentPermission == givenPermission).Any();
            bool Allow = false;

            if (HavePermission)
            {
                var CheckedGivenPermission = userPermissions.Where(up => up.CurrentPermission == givenPermission);
                switch (givenRight)
                {
                    case Rights.Create:
                        Allow = CheckedGivenPermission.Where(cp => cp.Create == true).Any();
                        break;
                    case Rights.Read:
                        Allow = CheckedGivenPermission.Where(cp => cp.Read == true).Any();
                        break;
                    case Rights.Delete:
                        Allow = CheckedGivenPermission.Where(cp => cp.Delete == true).Any();
                        break;
                    case Rights.Update:
                        Allow = CheckedGivenPermission.Where(cp => cp.Update == true).Any();
                        break;

                }
            }


            return Allow;
           
            

        }
    }
    
}
