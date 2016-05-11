using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CAOP.UserControls
{
    public partial class ReviewControl : System.Web.UI.UserControl
    {

        private bool reviewer = true;
        public bool Reviewer 
        {
            get 
            {
                return reviewer;
            }

            set
            {
                reviewer = value;
            }
        }

        private bool TypeAccount = false;
        public bool Type
        {
            get
            {
                return TypeAccount;
            }

            set
            {
                TypeAccount = value;
            }
        }


        List<ReviewGrid> gr;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                // for emty grid header visibility
                gr = new List<ReviewGrid>();
                UpdateGrid();

                if (!reviewer)
                {
                    CommentDiv.Visible = false;
                    CifReview cifreview = new CifReview();
                    AccountReview AccountReview = new AccountReview();
                    if (!TypeAccount)
                        gr = ConvertToReviewGrid(cifreview.GetReviews(Convert.ToInt32(Session["BID"])));
                    else
                        gr = ConvertToReviewGrid(AccountReview.GetReviews(Convert.ToInt32(Session["BID"])));
                    UpdateGrid();

                }
                   
            }

            CheckDoubleGrid();
        }

        private List<ReviewGrid> ConvertToReviewGrid(List<CifReview> reviews)
        {
            // int Rec, string Tab, string Field, string FieldID, string Comment

            int rec = 1;
            var DbReviews = reviews.Select(r => new ReviewGrid(rec++, r.TAB, r.FNAME, r.FID, r.COMMENT)).ToList();
            return DbReviews;
        }
        private List<ReviewGrid> ConvertToReviewGrid(List<AccountReview> reviews)
        {
            // int Rec, string Tab, string Field, string FieldID, string Comment

            int rec = 1;
            var DbReviews = reviews.Select(r => new ReviewGrid(rec++, r.TAB, r.FNAME, r.FID, r.COMMENT)).ToList();
            return DbReviews;
        }

        protected void grdReview_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grdReview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnAddComment_Click(object sender, EventArgs e)
        {
            int recid = grdReview.Rows.Count;
            string tab = txtTabNo.Text;
            string fieldName = txtField.Text;
            string comment = txtDetail.Text;
            string fieldid = "";

            ReviewGrid newCommet = new ReviewGrid(recid, tab, fieldName, fieldid, comment);
            gr = Session["ReviewGrid"] as List<ReviewGrid>;
            gr.Add(newCommet);
            UpdateGrid();

            btnAR.Text = "Revert";
           // ResetFields();
        }

        protected void UpdateGrid()
        {
            grdReview.DataSource = gr;
            grdReview.DataBind();
            Session["ReviewGrid"] = gr;
        }

        protected void btnAR_Click(object sender, EventArgs e)
        {
            gr = Session["ReviewGrid"] as List<ReviewGrid>;
            
                int BID = (int)Session["BID"];
                User logedUser = Session["User"] as User;
                CIF cif = new CIF(logedUser.USER_ID);
                AccOpen account = new AccOpen(logedUser.USER_ID);
                cif.BI_ID = BID;
                account.BI_ID = BID;

                    if (gr.Count == 0)
                    {
                        if (logedUser.Role.Name == Roles.COMPLIANCE_OFFICER.ToString())
                        {
                            if (!TypeAccount)
                                cif.ChangeStatus(Status.APPROVED_BY_COMPLIANCE_MANAGER, logedUser);
                            else
                                account.ChangeStatus(Status.APPROVED_BY_COMPLIANCE_MANAGER, logedUser);
                        }
                        else if (logedUser.Role.Name == Roles.BRANCH_MANAGER.ToString())
                        {
                            if (!TypeAccount)
                                cif.ChangeStatus(Status.APPROVED_BY_BRANCH_MANAGER, logedUser);
                            else
                                account.ChangeStatus(Status.APPROVED_BY_BRANCH_MANAGER, logedUser);
                        }
                        Redirect();
                    }
                    else
                    {
                        List<CifReview> newreviews = new List<CifReview>();
                        List<AccountReview> newreviewsAccount = new List<AccountReview>();

                        if (!TypeAccount)
                        {
                            foreach (var n in gr)
                            {
                                CifReview newReview = new CifReview()
                                {
                                    BID = BID,
                                    TAB = n.Tab,
                                    FNAME = n.Field,
                                    FID = n.FieldID,
                                    DATEC = n.Date,
                                    USERID = logedUser.USER_ID,
                                    COMMENT = n.Comment,
                                    ACTIVE = true

                                };
                                newreviews.Add(newReview);
                            }

                            CifReview rdb = new CifReview();
                            rdb.AddRviewerComents(newreviews, BID);
                        }
                        else
                        {
                            foreach (var n in gr)
                            {
                                AccountReview newReview = new AccountReview()
                                {
                                    BID = BID,
                                    TAB = n.Tab,
                                    FNAME = n.Field,
                                    FID = n.FieldID,
                                    DATEC = n.Date,
                                    USERID = logedUser.USER_ID,
                                    COMMENT = n.Comment,
                                    ACTIVE = true

                                };
                                newreviewsAccount.Add(newReview);
                            }

                            AccountReview rdb = new AccountReview();
                            rdb.AddRviewerComents(newreviewsAccount, BID);
                        }
                       


                    if (logedUser.Role.Name == Roles.COMPLIANCE_OFFICER.ToString())
                    {
                        if (!TypeAccount)
                            cif.ChangeStatus(Status.REJECTEBY_COMPLIANCE_MANAGER, logedUser);
                        else
                            account.ChangeStatus(Status.REJECTEBY_COMPLIANCE_MANAGER, logedUser);
                    }
                    else if (logedUser.Role.Name == Roles.BRANCH_MANAGER.ToString())
                    {
                        if (!TypeAccount)
                            cif.ChangeStatus(Status.REJECTED_BY_BRANCH_MANAGER, logedUser);
                        else
                            account.ChangeStatus(Status.REJECTED_BY_BRANCH_MANAGER, logedUser);
                    }
                      

                        Redirect();
                     }
        }

        protected void ResetFields()
        {
            txtDetail.Text = "";
            txtField.Text = "";
            txtTabNo.Text = "";
            txtFieldId.Text = "";
        }

        private void Redirect()
        {
            if (!TypeAccount)
                Response.Redirect("CifAccount.aspx");
            else
                Response.Redirect("AccountList.aspx");
        }


        private void CheckDoubleGrid()
        {
            User LogedUser = Session["User"] as User;
            CIF cif = new CIF(LogedUser.USER_ID);
            AccOpen account = new AccOpen(LogedUser.USER_ID);
           
            if (LogedUser.Role.Name == Roles.COMPLIANCE_OFFICER.ToString())
            {
                int BID = (int)Session["BID"];
              

                if (!TypeAccount)
                {
                    if (cif.CheckStatus(BID, Status.REJECTED_BY_BRANCH_MANAGER.ToString()))
                    {
                        CifReview cifreview = new CifReview();
                        List<ReviewGrid> GRpREV;
                        GRpREV = ConvertToReviewGrid(cifreview.GetReviews(Convert.ToInt32(Session["BID"])));                       
                        grdReviewPrev.DataSource = GRpREV;
                        grdReviewPrev.DataBind();
                        BMRH.Visible = true;
                    }
                }
                else
                {
                    if (account.CheckStatus(BID, Status.REJECTED_BY_BRANCH_MANAGER.ToString()))
                    {
                        AccountReview accountreview = new AccountReview();
                        List<ReviewGrid> GRpREV;                       
                        GRpREV = ConvertToReviewGrid(accountreview.GetReviews(Convert.ToInt32(Session["BID"])));
                        grdReviewPrev.DataSource = GRpREV;
                        grdReviewPrev.DataBind();
                        BMRH.Visible = true;
                    }
                }

               
            }
        }

    }

    public class ReviewGrid
    {
        public int Rec { get; set; }
        public string Tab { get; set; }

        public string Field { get; set; }

        public string FieldID { get; set; }

        public string Comment { get; set; }

        public DateTime Date { get; set; }

        public ReviewGrid(int Rec, string Tab, string Field, string FieldID, string Comment)
        {
            this.Rec = Rec;
            this.Tab = Tab;
            this.Field = Field;
            this.FieldID = FieldID;
            this.Comment = Comment;
            this.Date = DateTime.Now; 
        }
    }
}