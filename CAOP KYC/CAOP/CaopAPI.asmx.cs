using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BLL;
using System.Web.Script.Services;
using System.Web.Script.Serialization;


namespace CAOP
{
    /// <summary>
    /// Summary description for CaopAPI
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CaopAPI : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<BasicInformations> GetNextOfKinCif()
        {
            CIF cif = new CIF(1);

            return cif.GeteCifsByRole(Roles.BRANCH_OPERATOR.ToString(), false);

        }

    }
}
