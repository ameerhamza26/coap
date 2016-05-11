using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BioMetricClasses;
using System.Configuration;

namespace CAOP
{
    public partial class BioMetric : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string link = ConfigurationManager.AppSettings[1];
            clsSkillOrbitObject NadraData = Session["clsSkillOrbitObject"] as clsSkillOrbitObject;
            string IframeLink = "";
                
            IframeLink = String.Format(link + "?CNIC={0}&TOTAccount={1}&ContactNumber={2}&UserId={3}&BranchCode={4}&NameOfArea={5}",
                                        NadraData.CNIC,NadraData.TOTAccount,NadraData.ContactNumber,NadraData.UserId,NadraData.BranchCode,NadraData.NameOfArea);
            if (NadraData.AccountId != null)
                IframeLink += "&ID=" + NadraData.AccountId + "&CIF=" + NadraData.CIF;

            iframe.Src = IframeLink;
        }

       
    }
}