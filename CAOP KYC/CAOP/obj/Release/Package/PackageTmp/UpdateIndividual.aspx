<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.Master" CodeBehind="UpdateIndividual.aspx.cs" Inherits="CAOP.UpdateIndividual" %>

<%@ Register Src="~/UserControls/ReviewControl.ascx" TagName="Review" TagPrefix="Rev" %>


<asp:Content ID="Head" ContentPlaceHolderID="head" runat="server">
    <title>Account opening Portal</title>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
   
</asp:Content>

<asp:Content ID="Body" ContentPlaceHolderID="Body" runat="server">
    <div class="content">

        <div id="alerts">
           <%-- <div class="alert alert-success"><button type="button" class="close" data-dismiss="alert">
               &times;</button>Information has been saved</div>--%>
        </div>

              <!-- Modal -->
                        <div class="modal fade" id="myModal" role="dialog">
                            <div class="modal-dialog">

                                <!-- Modal content-->
                                <div class="modal-content">
                                    <%--<div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"></h4>
        </div>--%>
                                    <div class="modal-body">
                                        <p>Are you sure you want to Reset all fields?</p>
                                    </div>
                                    <div class="modal-footer">
                                         <button id="InResetBasicInfoModal" type="reset" class="btn btn-primary" value="Reset" data-toggle="modal" data-target="#myModal">Reset</button>

                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                    </div>
                                </div>

                            </div>
                        </div>
           <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <h3>Individual CIF Form</h3>
        <hr />
        <div class="row">
            <div class="col-md-3">
                <ul id="List1" class="nav nav-tabs-justified nav-menu">
                    <li id="basicInformation"><a id="InBasicInfoAnchor" data-toggle="tab" href="#sectiona">Basic Information</a></li>
                    <li id="otherIdentity" style="display: none"><a id="InOtherIdentityAnchor" data-toggle="tab" href="#sectionb">CNIC / Other Identity</a></li>
                    <li id="contactInformation" style="display: none"><a id="InContactInfoAnchor" data-toggle="tab" href="#sectionc">Address / Contact Information</a></li>
                    <li id="employmentInformation" style="display: none"><a id="InEmployInfoAnchor" data-toggle="tab" href="#sectiond">Employment Information</a></li>
                    <li id="miscInfo" style="display: none"><a id="InMiscInfoAnchor" data-toggle="tab" href="#sectione">Miscellaneous Information</a></li>
                    <li id="bankRelation" style="display: none"><a id="InBankRelationAnchor" data-toggle="tab" href="#sectionf">Banking Relationship</a></li>
                    <li id="fatcaIdentification" style="display: none"><a id="InFatcaIdentAnchor" data-toggle="tab" href="#sectiong">FATCA(U.S Person Identification)</a></li>

                </ul>
                <!-- nav nav-tabs -->
            </div>
            <!-- col-md-3 -->
            <div class="col-md-6">

                <div class="tab-content">
                    <div id="sectiona"  class="tab-pane fade in active">

                        <input type="text" style="display: none"  value="1" id=""/>
                        <h3>Basic Information</h3>

                        <div class="form-group" style="display: none">
                            <label class="lblReview"> CIF Type</label>
                            <asp:DropDownList ID="lstCifType" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="lstCifType_SelectedIndexChanged" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>

                         <asp:UpdatePanel ID="UpdatePanelCNICValidation" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>

                       
                                <div class="form-group">
                                    <label  class="lblReview">Primary ID Document Type: *</label>
                                    <asp:DropDownList ID="lstPrimaryDocumentType" AutoPostBack="true" ClientIDMode="Static" CssClass="form-control" runat="server" OnSelectedIndexChanged="lstPrimaryDocumentType_SelectedIndexChanged" ></asp:DropDownList>
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorPrimaryDocumentType" runat="server" ControlToValidate="lstPrimaryDocumentType" InitialValue="0" ErrorMessage="Primary Document Type is Required" ForeColor="Red" Font-Bold="true" ValidationGroup="BiValidationGroup" />
                                     <asp:CustomValidator  ID="CustomValidatorNonResident" Display="Dynamic" ForeColor="Red" Font-Bold="true" runat="server"  ErrorMessage="Only Passport can be selected for Non-Resident." ValidationGroup="BiValidationGroup" OnServerValidate="CustomValidatorNonResident_ServerValidate" ></asp:CustomValidator>
                                </div>
                            
                        
                         <div class="row">
                            <div class="col-md-6">                         
                                <div class="form-group">
                                    <label  class="lblReview">IDENTITY No: *</label>    
                                                                   
							            <asp:TextBox ID="txtCnic"  ClientIDMode="Static" CssClass="form-control" AutoPostBack="true" runat="server" OnTextChanged="txtCnic_TextChanged"></asp:TextBox>
                                        <asp:RegularExpressionValidator  Enabled="false" Display="Dynamic" ID="RegularExpressionValidatorCnic" runat="server" ErrorMessage="The CNIC must be in correct format e.g xxxxx-xxxxxxx-x" ForeColor="Red" Font-Bold="true" ControlToValidate="txtCnic" ValidationGroup="BiValidationGroup" ValidationExpression="^\d{5}-\d{7}-\d{1}$" ></asp:RegularExpressionValidator>                                  
							            <asp:CustomValidator  ID="CustomValidatorCNIC" Display="Dynamic" ForeColor="Red" Font-Bold="true" runat="server"  ErrorMessage="CNIC Number Should be Unique" ValidationGroup="BiValidationGroup" OnServerValidate="CustomValidatorCNIC_ServerValidate" ></asp:CustomValidator>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorCNIC" ValidationGroup="BiValidationGroup" runat="server" ControlToValidate="txtCnic" ForeColor="Red" Font-Bold="true" ErrorMessage="IDENTITY is Required"></asp:RequiredFieldValidator>
                                       <label runat="server" style="color: red" visible="false" id="CnicBiometric">CNIC is Required</label>                                                                     
                               </div>
                          </div>
                             <div class="col-md-6">
                                  <asp:Button ID="btnBioMetricVerify" Visible="false" Style="margin-top: 17px" ClientIDMode="Static" runat="server" ValidationGroup="Bio" Text="VERIFY BIOMETRIC" CssClass="btn btn-lg btn-primary" OnClick="btnBioMetricVerify_Click" />
                            </div>

                        </div>

                                         </ContentTemplate>
                                    <Triggers>
                                        <%--<asp:AsyncPostBackTrigger ControlID="lstPrimaryDocumentType" EventName="SelectedIndexChanged" />--%>
                                       
                                    </Triggers>
                                     </asp:UpdatePanel>

                        <div runat="server" id="BmData" visible="false">                       
                            <div class="form-group">
                                <label  class="lblReview">Contact No(For BioMetric Verification): *</label>
                                <asp:TextBox ID="BmTxtContactNo" TextMode="Number" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox> 
                                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorBmContact" ValidationGroup="Bio" runat="server" ControlToValidate="BmTxtContactNo" ForeColor="Red" Font-Bold="true" ErrorMessage="Contact No is Required"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Customer Type: *</label>
                            <asp:DropDownList ID="LstCustomerType" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorCustType" ValidationGroup="BiValidationGroup" runat="server" ControlToValidate="LstCustomerType" InitialValue="0" ForeColor="Red" Font-Bold="true" ErrorMessage="Customer Type is Required"></asp:RequiredFieldValidator>
                        </div>
                       
                        <div class="form-group">
                            <label  class="lblReview">Title(Name): *</label>
                            <asp:DropDownList ID="lstTitle" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator7" ValidationGroup="BiValidationGroup" runat="server" ControlToValidate="lstTitle" InitialValue="0" ForeColor="Red" Font-Bold="true" ErrorMessage="Title(Name) is Required"></asp:RequiredFieldValidator>
                        </div>
                       
                        <div class="form-group">
                            <label  class="lblReview">First Name: *</label>
                            <asp:TextBox ID="txtFirstName" MaxLength="17" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorFirstName" ValidationGroup="BiValidationGroup" runat="server" ControlToValidate="txtFirstName" ForeColor="Red" Font-Bold="true" ErrorMessage="First Name is Required"></asp:RequiredFieldValidator>
                        </div>
                         <div class="form-group">
                            <label  class="lblReview">Middle Name: </label>
                            <asp:TextBox ID="txtMiddleName" MaxLength="17" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>                          
                        </div>
                          <div class="form-group">
                            <label  class="lblReview">Last Name: </label>
                            <asp:TextBox ID="txtLastName" MaxLength="20" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>                          
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Title (Father/ Husband Name): *</label>
                            <asp:DropDownList ID="lstTitleFather" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator8" ValidationGroup="BiValidationGroup" runat="server" ControlToValidate="lstTitleFather" InitialValue="0" ForeColor="Red" Font-Bold="true" ErrorMessage="Title (Father/ Husband Name) is Required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Father/ Husband Name: *</label>
                            <asp:TextBox ID="txtFatherName" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorFatherName" runat="server" ValidationGroup="BiValidationGroup" ForeColor="Red" Font-Bold="true" ControlToValidate="txtFatherName" ErrorMessage="Father / Husband Name is Required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Father / Husband CNIC:</label>
                            <asp:TextBox ID="txtFatherCnic" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                              <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator2" runat="server" ErrorMessage="The CNIC must be in correct format e.g xxxxx-xxxxxxx-x" ForeColor="Red" Font-Bold="true" ControlToValidate="txtFatherCnic" ValidationGroup="BiValidationGroup" ValidationExpression="^\d{5}-\d{7}-\d{1}$"></asp:RegularExpressionValidator>

                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Father / Husband / Guardian CIF No:</label>
                            <asp:TextBox ID="txtFatherCif"  ClientIDMode="Static" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Mother Name: *</label>
                            <asp:TextBox ID="txtMotherName" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatortxtMotherName" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="BiValidationGroup" ControlToValidate="txtMotherName" ErrorMessage="Mother Name is Required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Mother CNIC:</label>
                            <asp:TextBox ID="txtMotherCnic" ClientIDMode="Static" CssClass="form-control" runat="server" ></asp:TextBox>
                              <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator3" runat="server" ErrorMessage="The CNIC must be in correct format e.g xxxxx-xxxxxxx-x" ForeColor="Red" Font-Bold="true" ControlToValidate="txtMotherCnic" ValidationGroup="BiValidationGroup" ValidationExpression="^\d{5}-\d{7}-\d{1}$"></asp:RegularExpressionValidator>

                        </div>
                        <div class="form-group" style="display:none">
                            <label  class="lblReview">Mother Old Cnic</label>
                            <asp:TextBox ID="txtMotherCnicOld" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                              <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator4" runat="server" ErrorMessage="The CNIC must be in correct format e.g xxxxx-xxxxxxx-x" ForeColor="Red" Font-Bold="true" ControlToValidate="txtMotherCnicOld" ValidationGroup="BiValidationGroup" ValidationExpression="^\d{5}-\d{7}-\d{1}$"></asp:RegularExpressionValidator>

                            
                             </div>
                        <div class="form-group">
                            <label>Date of Birth (DD-MM-YYYY):  *</label>
                            <div class="input-group date-control">
                                <asp:TextBox ID="txtDOB"  ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </button>
                                </span>
                            </div>                        
                        </div>
                        <div>
                            <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidatorBirth" runat="server" ErrorMessage="Date of Birth must Be in DD-MM-YYYY FORMAT" ForeColor="Red" Font-Bold="true" ControlToValidate="txtDOB" ValidationGroup="BiValidationGroup" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)[0-9]{2}$"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorBirth" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="BiValidationGroup" ControlToValidate="txtDOB" ErrorMessage="Date of Birth is required"></asp:RequiredFieldValidator>
                            <asp:CustomValidator  ID="CustomValidatorDate" Display="Dynamic" ForeColor="Red" Font-Bold="true" runat="server"  ErrorMessage="Invalid Date" ValidationGroup="BiValidationGroup" OnServerValidate="CustomValidatorDate_ServerValidate" ></asp:CustomValidator>
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Country of Birth: *</label>
                            <asp:DropDownList ID="lstCOB" ClientIDMode="Static"  CssClass="form-control" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidatorCountBirth" InitialValue="0" Display="Dynamic" ForeColor="Red" Font-Bold="true" runat="server" ControlToValidate="lstCOB"  ValidationGroup="BiValidationGroup" ErrorMessage="Country of Birth is Required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Place of Birth: (City or District) *</label>
                            <asp:TextBox ID="txtBithPlace" ClientIDMode="Static"  CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorBirthPlace" Display="Dynamic" ForeColor="Red" Font-Bold="true" runat="server" ControlToValidate="txtBithPlace"  ValidationGroup="BiValidationGroup" ErrorMessage="Place Of Birth is Required"></asp:RequiredFieldValidator>
                        </div>                        
                        <div class="form-group">
                            <label  class="lblReview">Marital Status:</label>
                            <asp:DropDownList ID="lstMartialStatus" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Gender: *</label>
                            <asp:DropDownList ID="lstGender" ClientIDMode="Static"  CssClass="form-control" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorGender" runat="server" ControlToValidate="lstGender" InitialValue="0" ErrorMessage="Gender is Required" ForeColor="Red" Font-Bold="true" ValidationGroup="BiValidationGroup" />
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Religion: *</label>
                            <asp:DropDownList ID="lstReligion" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorReligion" runat="server" ControlToValidate="lstReligion" InitialValue="0" ErrorMessage="Religion is Required" ForeColor="Red" Font-Bold="true" ValidationGroup="BiValidationGroup" />
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Resident / Non-Resident: *</label>
                            <asp:DropDownList ID="lstResident" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorResident" runat="server" ControlToValidate="lstResident" InitialValue="0" ErrorMessage="Resident / Non-Resident is Required" ForeColor="Red" Font-Bold="true" ValidationGroup="BiValidationGroup" />
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Nationality: *</label>
                            <div style="overflow-x: hidden; overflow-y: auto; border: 1px #808080 solid; max-height: 215px; height: auto; height: 215px">
                                <asp:CheckBoxList ID="lstNationality" ClientIDMode="Static" runat="server" RepeatColumns="2"></asp:CheckBoxList>
                                

                            </div>
                            <asp:CustomValidator  runat="server" ID="RequiredValidatorNationality"
                                    ClientValidationFunction="ValidateModuleList" 
                                     Display="Dynamic" ForeColor="Red" Font-Bold="true" ValidationGroup="BiValidationGroup"
                                    ErrorMessage="Please Select Atleast one Nationality"></asp:CustomValidator>

                            <asp:CustomValidator  runat="server" ID="CustomValidatorMaxNationality"
                                    ClientValidationFunction="MaxNationality" 
                                     Display="Dynamic" ForeColor="Red" Font-Bold="true" ValidationGroup="BiValidationGroup"
                                    ErrorMessage="Maximum two Nationalities are allowed"></asp:CustomValidator>

                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Expected Monthly Income (PKR): *</label>
                            <asp:TextBox ID="txtIncome" ClientIDMode="Static" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorIncome" runat="server" ForeColor="Red" Font-Bold="true" ControlToValidate="txtIncome" ValidationGroup="BiValidationGroup" ErrorMessage="Expected Monthly Income is Required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Counry Of Residence: *</label>
                            <asp:DropDownList ID="lstCOR" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorCResidence" runat="server" ForeColor="Red" Font-Bold="true" InitialValue="0" ControlToValidate="lstCOR" ValidationGroup="BiValidationGroup" ErrorMessage="Country Of Residence is Required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Does Customer Deal In: </label>
                            <asp:DropDownList ID="lstCustomerDeals" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                          <div class="form-group">
                            <label  class="lblReview">LSO Officer Code: </label>
                            <asp:DropDownList ID="lstOfficerCode" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>

                         <div class="form-group">
                             <label  class="lblReview">All Documents Verified: </label>
                             <br />
                             <asp:CheckBox runat="server" ID="chkDocument"   Text="YES" />
                                    
                         </div>


                        <div class="form-group">
                            <asp:Button ID="btnSubmitBaisc" ClientIDMode="Static" runat="server" Text="SAVE" CssClass="btn btn-primary" ValidationGroup="BiValidationGroup" OnClick="btnSubmitBaisc_Click" />
                            <%--  <asp:Button ID="btnResetBasic" ClientIDMode="Static" runat="server" Text="Reset" CssClass="btn btn-primary" OnClick="btnResetBasic_Click" />
                            --%>
                            <button id="InResetBasicInfo" onclick="openModal()" type="button" class="btn btn-primary" value="Reset">Reset</button>
                             <asp:Button ID="btnUpdateBasic" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary" ValidationGroup="BiValidationGroup" OnClick="btnUpdateBasic_Click" />
                             <asp:Button ID="btnSubmitCifa" Visible="false"  ClientIDMode="Static" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmitCif_Click" />

                        </div>                 
                    </div>


                    <div id="sectionb" class="tab-pane fade">
                        <h3>IDENTITY Information</h3>
                        <div class="form-group">
                            <label  class="lblReview">IDENTITY:</label>
                            <asp:TextBox ID="OitxtCnic" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Date of Issue(DD-MM-YYYY)</label>
                            <div class="input-group date-control">
                                <asp:TextBox ID="OiDateIssue"  max="" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </button>
                                </span>
                            </div>
                        </div>
                          <asp:RegularExpressionValidator ControlToValidate="OiDateIssue" Display="Dynamic" ID="RegularExpressionValidatorIdentityIssue" runat="server" ErrorMessage="Date of Issue must Be in DD-MM-YYYY FORMAT" ForeColor="Red" Font-Bold="true"  ValidationGroup="OiValidationGroup" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)[0-9]{2}$"></asp:RegularExpressionValidator>
                        <div class="form-group">
                            <label  class="lblReview">Expiry Date: (DD-MM-YYYY) *</label>
                            <div class="input-group date-control">
                                <asp:TextBox ID="OiExpDate" runat="server"  CssClass="form-control"></asp:TextBox>
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </button>
                                </span>
                            </div>
                        </div>
                         <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorExpiryDate" runat="server" ForeColor="Red" Font-Bold="true"  ValidationGroup="OiValidationGroup" ControlToValidate="OiExpDate" ErrorMessage="Expiry Date is required"></asp:RequiredFieldValidator>
                           <asp:RegularExpressionValidator ControlToValidate="OiExpDate" Display="Dynamic" ID="RegularExpressionValidatorIdentityExpiry" runat="server" ErrorMessage="Date of Issue must Be in DD-MM-YYYY FORMAT" ForeColor="Red" Font-Bold="true"  ValidationGroup="OiValidationGroup" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)[0-9]{2}$"></asp:RegularExpressionValidator>
                        <div class="form-group">
                            <label  class="lblReview">Country Of Issue: </label>
                            <asp:DropDownList ID="OiListICIssue" InitialValue="0" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                         <div class="form-group">
                            <label  class="lblReview">Place Of Issue: </label>
                           <asp:TextBox ID="OiTxtPlaceIssueCnic"  CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group" style="display:none">
                            <label  class="lblReview">Identification Mark: *</label>
                            <asp:TextBox ID="OiTxtIdentMark" Text="" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Enabled="false" ID="OiRequiredFieldValidatorTxtIdentMark" runat="server" Display="Dynamic" ControlToValidate="OiTxtIdentMark" ForeColor="Red" Font-Bold="true" ValidationGroup="OiValidationGroup" ErrorMessage="Identification Mark is Required"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group" >
                            <label  class="lblReview">Family No: </label>
                            <asp:TextBox ID="OiTxtFamilyNo"  CssClass="form-control" runat="server" MaxLength="6"></asp:TextBox>
                            
                        </div>
                        <h3 style="display:none">CNIC Token Information (For Expired CNICs) *</h3>
                        <div class="form-group" style="display:none">
                            <label  class="lblReview">Token No:</label>
                            <asp:TextBox ID="OiTxtTokenNo" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Enabled="false" Display="Dynamic" ID="OiRequiredFieldValidatorTxtTokenNo" runat="server" ControlToValidate="OiTxtTokenNo" ForeColor="Red" Font-Bold="true" ValidationGroup="OiValidationGroup" ErrorMessage="Token No is Required"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group" style="display:none">
                            <label  class="lblReview">Token Issue Date(DD-MM-YYYY)</label>
                            <div class="input-group date-control">
                                <asp:TextBox ID="OiTxtTokenIssueDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </button>
                                </span>
                            </div>
                        </div>

                       
                        <div class="form-group">
                            <label  class="lblReview">NTN:</label>
                            <asp:TextBox ID="OiTxtNTN" CssClass="form-control" runat="server"></asp:TextBox>
                           
                            
                        </div>

                        <div class="form-group" style="display:none">
                            <label  class="lblReview">Old NIC:</label>
                            <asp:TextBox ID="OiTxtOldNic" CssClass="form-control" runat="server"></asp:TextBox>                           
                             <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator5" runat="server" ErrorMessage="The CNIC must be in correct format e.g xxxxx-xxxxxxx-x" ForeColor="Red" Font-Bold="true" ControlToValidate="OiTxtOldNic" ValidationGroup="OiValidationGroup" ValidationExpression="^\d{5}-\d{7}-\d{1}$"></asp:RegularExpressionValidator>

                        </div>

                        <h3>Any other Identity</h3>
                        <div class="form-group">
                            <label  class="lblReview">Identity Type:</label>
                            <asp:DropDownList ID="OiListIdentType" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Identity No:</label>
                            <asp:TextBox ID="OiTxtIdentNo" CssClass="form-control" runat="server"></asp:TextBox>
                          <%--  <asp:RequiredFieldValidator ID="OiRequiredFieldValidatorTxtIdentNo" Enabled="true" runat="server" ControlToValidate="OiTxtIdentNo" ForeColor="Red" Font-Bold="true" ValidationGroup="OiValidationGroup" ErrorMessage="Identification No is Required"></asp:RequiredFieldValidator>--%>
                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Country of Issue:</label>
                            <asp:DropDownList ID="OiListCountryOfIssue" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>

                        <div class="form-group">
                            <label>Issue Date(DD-MM-YYYY):</label>
                            <div class="input-group date-control">
                                <asp:TextBox ID="OiDateIssue2" runat="server"  CssClass="form-control"></asp:TextBox>
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </button>
                                </span>
                            </div>
                        </div>
                           <asp:RegularExpressionValidator ControlToValidate="OiDateIssue2" Display="Dynamic" ID="RegularExpressionValidatorOtherIdentityIssue" runat="server" ErrorMessage="Issue Date must Be in DD-MM-YYYY FORMAT" ForeColor="Red" Font-Bold="true"  ValidationGroup="OiValidationGroup" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)[0-9]{2}$"></asp:RegularExpressionValidator>
                        <div class="form-group">
                            <label  class="lblReview">Place of Issue:</label>
                            <asp:TextBox ID="OiPlaceOfIssue" CssClass="form-control" runat="server"></asp:TextBox>
                         <%--  <asp:RequiredFieldValidator ID="OiRequiredFieldValidatorPlaceOfIssue" runat="server" ControlToValidate="OiPlaceOfIssue" ForeColor="Red" Font-Bold="true" ValidationGroup="OiValidationGroup" ErrorMessage="Place of Issue is Required"></asp:RequiredFieldValidator> --%>  
                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Expiry Date(DD-MM-YYYY)</label>
                            <div class="input-group date-control">
                                <asp:TextBox ID="OiExpDate2" runat="server"  CssClass="form-control"></asp:TextBox>
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </button>
                                </span>
                            </div>
                        </div>
                        <asp:RegularExpressionValidator ControlToValidate="OiExpDate2" Display="Dynamic" ID="RegularExpressionValidatorOtherIdentityExp" runat="server" ErrorMessage="Expiry Date must Be in DD-MM-YYYY FORMAT" ForeColor="Red" Font-Bold="true"  ValidationGroup="OiValidationGroup" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)[0-9]{2}$"></asp:RegularExpressionValidator>
                        <div class="form-group">
                            <asp:Button ID="OiButtonSubmit" runat="server" Text="SAVE" CssClass="btn btn-primary" ValidationGroup="OiValidationGroup" OnClick="OiButtonSubmit_Click" />
                            <%--                        <asp:Button ID="OiButtonReset" ClientIDMode="Static" runat="server" Text="Reset" CssClass="btn btn-primary" OnClick="OiButtonReset_Click" />--%>
                            <button id="InResetOtherIdentity" onclick="openModal()" type="button" class="btn btn-primary" value="Reset">Reset</button>
                             <asp:Button ID="btnUpdateIdentity" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary" ValidationGroup="OiValidationGroup" OnClick="btnUpdateIdentity_Click" />
                             <asp:Button ID="btnSubmitCifb" Visible="false"  ClientIDMode="Static" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmitCif_Click" />

                        </div>

                    </div>

                    <div id="sectionc" class="tab-pane fade">
                        <h3>Permanent Address Information</h3>

                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                         <ContentTemplate>
			             <div class="form-group">
                            <label  class="lblReview">Country: *</label>
                            <asp:DropDownList ID="CiListCountryCode" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="CiListCountryCode_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="CiRequiredFieldValidatorPermanentCountryCode" Enabled="true" runat="server" InitialValue="0" ControlToValidate="CiListCountryCode" ForeColor="Red" Font-Bold="true" ValidationGroup="CiValidationGroup" ErrorMessage="Country is Required"></asp:RequiredFieldValidator>                            
                        </div>
                         <div class="form-group">
                            <label  class="lblReview">Province: *</label>
                            <asp:DropDownList ID="CiListProvince" CssClass="form-control" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorPermanentProvice" Enabled="true" runat="server" InitialValue="0" ControlToValidate="CiListProvince" ForeColor="Red" Font-Bold="true" ValidationGroup="CiValidationGroup" ErrorMessage="Province is Required"></asp:RequiredFieldValidator>
                        </div>
                          <div class="form-group">
                            <label  class="lblReview">City: *</label>
                            <asp:DropDownList ID="CiListCity" CssClass="form-control" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorPermenentCity" Enabled="true" runat="server" InitialValue="0" ControlToValidate="CiListCity" ForeColor="Red" Font-Bold="true" ValidationGroup="CiValidationGroup" ErrorMessage="City is Required"></asp:RequiredFieldValidator>
                        </div>
			            </ContentTemplate>                              
                           </asp:UpdatePanel>


                       
                       
                         <div class="form-group">
                            <label  class="lblReview">Address Line 1: *</label>
                            <asp:TextBox ID="CiTxtBuilding" MaxLength="40" CssClass="form-control" runat="server"></asp:TextBox> 
                             <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorTxtBuildingt" runat="server" ControlToValidate="CiTxtBuilding" ForeColor="Red" Font-Bold="true" ValidationGroup="CiValidationGroup" ErrorMessage="Address Line 1 is Required"></asp:RequiredFieldValidator>                            
                        </div>
                         <div class="form-group">
                            <label  class="lblReview">Address Line 2:</label>
                            <asp:TextBox ID="CiTxtFloor" MaxLength="40" CssClass="form-control" runat="server"></asp:TextBox>                             
                        </div>
                         <div class="form-group">
                            <label  class="lblReview">Address Line 3:</label>
                            <asp:TextBox ID="CiTxtStreet" MaxLength="40" CssClass="form-control" runat="server"></asp:TextBox>
                             
                        </div>
                       
                        
                         <div class="form-group">
                            <label  class="lblReview">District:</label>
                            <asp:TextBox ID="CiTxtDistrict"  CssClass="form-control" runat="server"></asp:TextBox>                             
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Post Office Box:</label>
                            <asp:TextBox ID="CiTxtPostBox" CssClass="form-control" runat="server"></asp:TextBox>                             
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Postal Code:</label>
                            <asp:TextBox ID="CiTxtPotalCode" CssClass="form-control" MaxLength="10" runat="server"></asp:TextBox>                             
                        </div>
                      

                     
                    

                        <h3>Present / Current Residential Address</h3>

                     

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <div class="form-group">
                                    <label>Same as Permanent</label>
                                    <asp:CheckBox ID="chkSW" Text="" runat="server" AutoPostBack="true" OnCheckedChanged="chkSW_CheckedChanged" />


                                </div>
                          <div class="form-group">
                            <label  class="lblReview">Country Code: *</label>
                            <asp:DropDownList ID="CiListCountryCodePre" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="CiListCountryCodePre_SelectedIndexChanged"></asp:DropDownList>
                             <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" Enabled="true" runat="server" InitialValue="0" ControlToValidate="CiListCountryCodePre" ForeColor="Red" Font-Bold="true" ValidationGroup="CiValidationGroup" ErrorMessage="Country is Required"></asp:RequiredFieldValidator>
                        </div>
                         <div class="form-group">
                            <label  class="lblReview">Province: *</label>
                            <asp:DropDownList ID="CiListProvincePre" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorProvicePreset" Enabled="true" runat="server" InitialValue="0" ControlToValidate="CiListProvincePre" ForeColor="Red" Font-Bold="true" ValidationGroup="CiValidationGroup" ErrorMessage="Province is Required"></asp:RequiredFieldValidator>
                        </div>
                            <div class="form-group">
                            <label  class="lblReview">City: *</label>
                            <asp:DropDownList ID="CiListCityPre" CssClass="form-control" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorCityPresent" Enabled="true" runat="server" InitialValue="0" ControlToValidate="CiListCityPre" ForeColor="Red" Font-Bold="true" ValidationGroup="CiValidationGroup" ErrorMessage="City is Required"></asp:RequiredFieldValidator>
                        </div>
                        
                         <div class="form-group">
                            <label  class="lblReview">Address Line 1: *</label>
                            <asp:TextBox ID="CiTxtBuildingPre" CssClass="form-control" runat="server" MaxLength="40"></asp:TextBox>    
                               <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorBuildingPer" runat="server" ControlToValidate="CiTxtBuildingPre" ForeColor="Red" Font-Bold="true" ValidationGroup="CiValidationGroup" ErrorMessage="Address Line 1 is Required"></asp:RequiredFieldValidator>                         
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Address Line 2:</label>
                            <asp:TextBox ID="CiTxtFloorPre" MaxLength="40" CssClass="form-control" runat="server"></asp:TextBox>                             
                        </div>
                         <div class="form-group">
                            <label  class="lblReview">Address Line 3:</label>
                            <asp:TextBox ID="CiTxtStreetPre" MaxLength="40" CssClass="form-control" runat="server"></asp:TextBox>
                           
                        </div>
                       
                        
                         <div class="form-group">
                            <label  class="lblReview">District:</label>
                            <asp:TextBox ID="CiTxtDistrictPre" CssClass="form-control" runat="server"></asp:TextBox>                             
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Post Office Box:</label>
                            <asp:TextBox ID="CiTxtPostBoxPre" CssClass="form-control" runat="server"></asp:TextBox>                             
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Postal Code:</label>
                            <asp:TextBox ID="CiTxtPotalCodePre" CssClass="form-control" MaxLength="10" runat="server"></asp:TextBox>                             
                        </div>
                       

                        </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="chkSW" />
                            </Triggers>
                        </asp:UpdatePanel>


                        <%-- <div class="form-group">
                                <label>Same as Permanent</label>                            
                                <asp:CheckBox ID="CiCheckBoxPermanent" CssClass="form-control" runat="server" AutoPostBack="false" />
                            </div>--%>



                        <h3>Contact Numbers</h3>
                        <div class="form-group">
                            <label  class="lblReview">Contact No. Office</label>
                            <asp:TextBox ID="CiContactNoOffice" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:CustomValidator  ID="CustomValidatorContact" Display="Dynamic" ForeColor="Red" Font-Bold="true" ValidationGroup="CiValidationGroup" runat="server" ErrorMessage="Atleast One Contact No is Required" ControlToValidate="CiContactNoOffice"  ClientValidationFunction="doCustomValidateContact" ValidateEmptyText="true"  ></asp:CustomValidator>
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Contact No. Residence:</label>
                            <asp:TextBox ID="CiContactNoResidence" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Mobile No:</label>
                            <asp:TextBox ID="CiMobileNo" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Fax No:</label>
                            <asp:TextBox ID="CiFaxNo" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Email:</label>
                            <asp:TextBox ID="CiEmail" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <asp:Button ID="CiSubmitButton" runat="server" Text="SAVE" CssClass="btn btn-primary" ValidationGroup="CiValidationGroup" OnClick="CiSubmitButton_Click" />
                            <button id="InResetContactInfo" onclick="openModal()" type="button"  class="btn btn-primary" value="Reset">Reset</button>
                             <asp:Button ID="btnUpdateContactInfo" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary" ValidationGroup="CiValidationGroup" OnClick="btnUpdateContactInfo_Click" />
                             <asp:Button ID="btnSubmitCifc" Visible="false"  ClientIDMode="Static" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmitCif_Click" />

                        </div>

                    </div>
                    <div id="sectiond" class="tab-pane fade">

                        <asp:UpdatePanel ID="UpdatePanelEmpInfo" runat="server" UpdateMode="Always">
                        <ContentTemplate>

                        <div class="form-group">
                            <label  class="lblReview">Employment Detail: *</label>
                            <asp:DropDownList AutoPostBack="true" ID="EiListEmployDetail" CssClass="form-control" runat="server" OnSelectedIndexChanged="EiListEmployDetail_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="EiRequiredFieldValidatorListEmployDetail" InitialValue="0" runat="server" ControlToValidate="EiListEmployDetail" ForeColor="Red" Font-Bold="true" ValidationGroup="EiValidationGroup" ErrorMessage="Employment Detail is Required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Description: (If other)</label>
                            <asp:TextBox ID="txtDescEmpDetail" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>
                          <div class="form-group">
                              <label  class="lblReview">Employer Group: </label>
                              <asp:DropDownList AutoPostBack="true" ID="EiListEmpGrp" CssClass="form-control" runat="server" OnSelectedIndexChanged="EiListEmpGrp_SelectedIndexChanged"></asp:DropDownList>
                              <asp:RequiredFieldValidator Enabled="false" Display="Dynamic" ID="EiReqValidatorEGrp" InitialValue="0" runat="server" ControlToValidate="EiListEmpGrp" ForeColor="Red" Font-Bold="true" ValidationGroup="EiValidationGroup" ErrorMessage="Employer Group is Required"></asp:RequiredFieldValidator>
                          </div>
                         <div class="form-group">
                              <label  class="lblReview">Employer Sub-Group : </label>
                              <asp:DropDownList AutoPostBack="true" ID="EiListEmpSubGrp" CssClass="form-control" runat="server" OnSelectedIndexChanged="EiListEmpSubGrp_SelectedIndexChanged"></asp:DropDownList>
                              <asp:RequiredFieldValidator Enabled="false" Display="Dynamic" ID="EiReqValidatorESubGrp" InitialValue="0" runat="server" ControlToValidate="EiListEmpGrp" ForeColor="Red" Font-Bold="true" ValidationGroup="EiValidationGroup" ErrorMessage="Employer Sub-Group is Required"></asp:RequiredFieldValidator>
                          </div>
                          <div class="form-group">
                              <label  class="lblReview">Employer Number: </label>
                              <asp:DropDownList ID="EiListEmpNum" CssClass="form-control" runat="server"></asp:DropDownList>
                              <asp:RequiredFieldValidator Enabled="false" Display="Dynamic" ID="EiReqValidatorENum" InitialValue="0" runat="server" ControlToValidate="EiListEmpNum" ForeColor="Red" Font-Bold="true" ValidationGroup="EiValidationGroup" ErrorMessage="Employer Number is Required"></asp:RequiredFieldValidator>
                          </div>
                        <div class="form-group">
                             <label  class="lblReview">Consumer Segment: *</label>
                            <asp:DropDownList ID="EiListConsumer" CssClass="form-control" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorConsumer" InitialValue="0" runat="server" ControlToValidate="EiListConsumer" ForeColor="Red" Font-Bold="true" ValidationGroup="EiValidationGroup" ErrorMessage="Consumer Segment is Required"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Department:</label>
                            <asp:TextBox ID="EiDepartment" CssClass="form-control" runat="server" MaxLength="40"></asp:TextBox>
                        </div>

                        <div class="control-group">
                            <label class="lblReview control-label" >Retired Employee:</label>
                            <div class="controls">   
                                <asp:RadioButton ID="EiRadioButton2" Text="Yes" Style="margin-right: 15px" GroupName="EiRadioGroup1" runat="server" />
                            <asp:RadioButton ID="EiRadio1" Text="No"  Checked="true" GroupName="EiRadioGroup1" runat="server" />
                            
                        </div>
                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Designation:</label>
                            <asp:TextBox ID="EiDesignation" CssClass="form-control" runat="server" MaxLength="20"></asp:TextBox>
                        </div>

                        <div class="form-group" style="display: none">
                            <label  class="lblReview">Employee ID:</label>
                            <asp:TextBox ID="EiEmployeeID" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>


                        <div class="form-group">
                            <label  class="lblReview">PF No:</label>
                            <asp:TextBox ID="EiPFNo" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>


                        <div class="form-group">
                            <label  class="lblReview">PPO No:</label>
                            <asp:TextBox ID="EiPPONo" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Employer:</label>
                           <asp:TextBox ID="EiTxtEmployer" CssClass="form-control" runat="server" MaxLength="40" ></asp:TextBox>

                        </div>

                        <div class="form-group" style="display: none">
                            <label  class="lblReview">Employer:</label>
                            <asp:DropDownList ID="EiListEmployerCode" CssClass="form-control" runat="server"></asp:DropDownList>

                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Employer /Business Address: *</label>
                            <asp:TextBox ID="EiEmpBusAddr" CssClass="form-control" runat="server"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmpAddress" runat="server" ControlToValidate="EiEmpBusAddr" ForeColor="Red" Font-Bold="true" ValidationGroup="EiValidationGroup" ErrorMessage="Employer /Business Address is Required"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Employer Country: *</label>
                            <asp:DropDownList ID="EiListCountryEmpBus" CssClass="form-control" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmpBus" InitialValue="0" runat="server" ControlToValidate="EiListCountryEmpBus" ForeColor="Red" Font-Bold="true" ValidationGroup="EiValidationGroup" ErrorMessage="Employer Country is Required"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group" style="display: none">
                            <label  class="lblReview">Pak Army Rank Code:</label>
                            <asp:DropDownList ID="EiListPakArmy" CssClass="form-control" runat="server"></asp:DropDownList>

                        </div>

                         </ContentTemplate>                           
                        </asp:UpdatePanel>

                        <div class="form-group">
                            <asp:Button ID="EiSubmitButton" runat="server" Text="SAVE" CssClass="btn btn-primary" ValidationGroup="EiValidationGroup" OnClick="EiSubmitButton_Click" />
                            <button id="InResetEmployInfo" onclick="openModal()" type="button" class="btn btn-primary" value="Reset">Reset</button>
                             <asp:Button ID="btnUpdateEi" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary" ValidationGroup="EiValidationGroup" OnClick="btnUpdateEi_Click" />
                             <asp:Button ID="btnSubmitCifd" Visible="false"  ClientIDMode="Static" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmitCif_Click" />
                        </div>
                    </div>
                    <!-- tab-content -->

                    <div id="sectione" class="tab-pane fade">

                        <div class="form-group">
                            <label  class="lblReview">Education: *</label>
                            <asp:DropDownList ID="MiListEducation" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="MiRequiredFieldValidatorListEducation" Display="Dynamic" runat="server" InitialValue="0" ControlToValidate="MiListEducation" ForeColor="Red" Font-Bold="true" ValidationGroup="MiValidationGroup" ErrorMessage="Education is Required"></asp:RequiredFieldValidator>


                        </div>

                        <div class="form-group" style="display: none">
                            <label  class="lblReview">Social Status:</label>
                            <asp:TextBox ID="MiSocialStatus" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Accomodation Type: *</label>
                            <asp:DropDownList ID="MiListAccomType" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator InitialValue="0"  ID="MiRequiredFieldValidatorListAccomType" Display="Dynamic" runat="server" ControlToValidate="MiListAccomType" ForeColor="Red" Font-Bold="true" ValidationGroup="MiValidationGroup" ErrorMessage="Accomodation Type is Required"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group" style="display: none">
                            <label  class="lblReview">Accomodation Type Description:</label>
                            <asp:TextBox ID="MiAccomTypeDescr" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Transportation Type:</label>
                            <asp:DropDownList ID="MiListTransportType" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Source of Fund: *</label>
                            <asp:DropDownList ID="MiListSOF" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator InitialValue="0"  ID="RequiredFieldValidatorMISOF" Display="Dynamic" runat="server" ControlToValidate="MiListSOF" ForeColor="Red" Font-Bold="true" ValidationGroup="MiValidationGroup" ErrorMessage="Source of Fund is Required"></asp:RequiredFieldValidator>
                        </div>

                        <div class="control-group">
                            <label class="lblReview control-label" >Blind/Visually Impaired:</label>
                            <div class="controls">  
                            <asp:RadioButton ID="MiBlindVisualRadio2" Text="Yes"  GroupName="MiRadioGroup1" runat="server" />
                            <asp:RadioButton ID="MiBlindVisualRadio1" Text="No" Checked="True" Style="margin-right: 15px"  GroupName="MiRadioGroup1" runat="server" />
                            
                           </div>
                        </div>

                        <asp:UpdatePanel ID="UpdatePanelAccountType" runat="server" UpdateMode="Always">
                        <ContentTemplate>

                            <div class="form-group">
                                <label  class="lblReview">Politically Exposed: *</label>
                                <asp:DropDownList ID="MiListPoliticExposed" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="MiListPoliticExposed_SelectedIndexChanged"></asp:DropDownList>
                                 <asp:RequiredFieldValidator InitialValue="0"  ID="RequiredFieldValidatorPEP" Display="Dynamic" runat="server" ControlToValidate="MiListPoliticExposed" ForeColor="Red" Font-Bold="true" ValidationGroup="MiValidationGroup" ErrorMessage="Politically Exposed is Required"></asp:RequiredFieldValidator>
                            </div>

                            <div class="control-group">
                            <label class="lblReview control-label">Nature:</label>
                                <div class="controls">  
                                    <asp:RadioButton ID="MiRadPepSingle" Style="margin-right: 15px" Text="SELF" GroupName="PEP" runat="server"  />
                                    <asp:RadioButton ID="MiRadPepLinked" Text="LINKED" GroupName="PEP" runat="server"  />
                                </div>
                            </div>

                            <div class="form-group">
                                <label  class="lblReview">Position / Realtionship:</label>
                                 <asp:TextBox Enabled="false" ID="MiPepRelation" CssClass="form-control" runat="server"></asp:TextBox>
                                 <asp:RequiredFieldValidator Enabled="false" ID="RequiredFieldValidatorMiPepRelation" Display="Dynamic" runat="server" ControlToValidate="MiPepRelation" ForeColor="Red" Font-Bold="true" ValidationGroup="MiValidationGroup" ErrorMessage="Position / Realtionship is Required"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group">
                                <label  class="lblReview">Sources Of Income and Wealth:</label>
                                 <asp:TextBox ID="MiTxtPED" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                 <asp:RequiredFieldValidator Enabled="false" ID="RequiredFieldValidatorMiPepDesc" Display="Dynamic" runat="server" ControlToValidate="MiTxtPED" ForeColor="Red" Font-Bold="true" ValidationGroup="MiValidationGroup" ErrorMessage="Sources Of Income and Wealth is Required"></asp:RequiredFieldValidator>
                            </div>

                         </ContentTemplate>                           
                        </asp:UpdatePanel>

                        <div class="control-group">
                            <label class="lblReview control-label" >Parda Nasheen Lady:</label>
                            <div class="controls">  
                             <asp:RadioButton ID="MiPardaRadio2" Text="Yes" GroupName="MiRadioGroup2" runat="server" />
                            <asp:RadioButton ID="MiPardaRadio1" Text="No" Checked="True" Style="margin-right: 15px" GroupName="MiRadioGroup2" runat="server" />
                            
                            </div>
                        </div>

                        <div class="form-group" style="display: none">
                            <label  class="lblReview">Monthly Turn Over (Debit): *</label>
                            <asp:DropDownList ID="MiListMonthTurnOverDebit" CssClass="form-control" runat="server"></asp:DropDownList>
                         <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" InitialValue="0" ControlToValidate="MiListMonthTurnOverDebit" ForeColor="Red" Font-Bold="true" ValidationGroup="MiValidationGroup" ErrorMessage="Monthly Turn Over (Debit) is Required"></asp:RequiredFieldValidator>--%>

                        </div>

                        <div class="form-group" style="display: none">
                            <label  class="lblReview">Monthly Turn Over (Credit): *</label>
                            <asp:DropDownList ID="MiListMonthTurnOverCredit" CssClass="form-control" runat="server"></asp:DropDownList>
                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" InitialValue="0" ControlToValidate="MiListMonthTurnOverCredit" ForeColor="Red" Font-Bold="true" ValidationGroup="MiValidationGroup" ErrorMessage="Monthly Turn Over (Credit) is Required"></asp:RequiredFieldValidator>--%>

                        </div>

                        <div class="form-group" style="display: none">
                            <label  class="lblReview">Average No. of Cash Deposits(Monthly): *</label>
                            <asp:DropDownList ID="MiListAvgNoOfCashDeposits" CssClass="form-control" runat="server"></asp:DropDownList>
                         <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" runat="server" InitialValue="0" ControlToValidate="MiListAvgNoOfCashDeposits" ForeColor="Red" Font-Bold="true" ValidationGroup="MiValidationGroup" ErrorMessage="Average No. of Cash Deposits(Monthly) is Required"></asp:RequiredFieldValidator>--%>

                        </div>

                        <div class="form-group" style="display: none">
                            <label  class="lblReview">Average No. of No Cash Deposits(Monthly): *</label>
                            <asp:DropDownList ID="MiListAvgNoOfNonCashDeposits" CssClass="form-control" runat="server"></asp:DropDownList>
                          <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" runat="server" InitialValue="0" ControlToValidate="MiListAvgNoOfNonCashDeposits" ForeColor="Red" Font-Bold="true" ValidationGroup="MiValidationGroup" ErrorMessage="Average No. of No Cash Deposits(Monthly) is Required"></asp:RequiredFieldValidator>--%>

                        </div>



                        <div class="form-group">
                            <label  class="lblReview">Total Asset Value:</label>
                            <asp:TextBox ID="MiTotalAsset" CssClass="form-control" TextMode="Number" runat="server" OnTextChanged="MiTotalAsset_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>


                        <div class="form-group">
                            <label  class="lblReview">Liabilities:</label>
                            <asp:TextBox ID="MiLiabilities" CssClass="form-control" TextMode="Number" runat="server" OnTextChanged="MiLiabilities_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>


                        <div class="form-group">
                            <label  class="lblReview">Net Worth:</label>
                             <asp:UpdatePanel ID="UpdatePanelCalc" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
	                                <asp:TextBox ID="MiNetWorth" CssClass="form-control" TextMode="Number" ReadOnly="true" runat="server" ></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
	                                <asp:AsyncPostBackTrigger ControlID="MiTotalAsset" EventName="TextChanged" />
                                     <asp:AsyncPostBackTrigger ControlID="MiLiabilities" EventName="TextChanged" />
                                </Triggers>
                                 </asp:UpdatePanel>


                            
                        </div>

                        <h3>Country of Tax</h3>
                        <div class="form-group">
                            <label  class="lblReview">Country of Tax:</label>
                            <div style="overflow-x: hidden; overflow-y: auto; border: 1px #808080 solid; max-height: 215px; height: auto; height: 215px">
                                <asp:CheckBoxList ID="MiListCountryOfTax" runat="server" RepeatColumns="2"></asp:CheckBoxList>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Button ID="MiSubmitButton" runat="server" Text="SAVE" CssClass="btn btn-primary" ValidationGroup="MiValidationGroup" OnClick="MiSubmitButton_Click" />
                            <button id="InResetMiscInfo" onclick="openModal()" type="button" class="btn btn-primary" value="Reset">Reset</button>
                             <asp:Button ID="btnUpdateMi" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary" ValidationGroup="MiValidationGroup" OnClick="btnUpdateMi_Click" />
                             <asp:Button ID="btnSubmitCife"  Visible="false" ClientIDMode="Static" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmitCif_Click" />
                        </div>


                    </div>

                    <div id="sectionf" class="tab-pane fade">
                        <h3>NBP Relationship</h3>
                        <div class="form-group">
                            <label  class="lblReview">Branch Information:</label>
                            <asp:DropDownList ID="BrListBranchInfo" CssClass="form-control" runat="server"></asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ID="BrRequiredFieldValidatorListBranchInfo" runat="server" ControlToValidate="BrListBranchInfo" ForeColor="Red" Font-Bold="true" ValidationGroup="BrValidationGroup" ErrorMessage="Branch Information is Required"></asp:RequiredFieldValidator>--%>


                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Account Type:</label>
                            <asp:DropDownList ID="BrListAccountType" CssClass="form-control" runat="server"></asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ID="BrRequiredFieldValidatorListAccountType" runat="server" ControlToValidate="BrListAccountType" ForeColor="Red" Font-Bold="true" ValidationGroup="BrValidationGroup" ErrorMessage="Account Type is Required"></asp:RequiredFieldValidator>--%>


                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Account Number:</label>
                            <asp:TextBox ID="BrAccountNumber" CssClass="form-control" runat="server"></asp:TextBox>
                           <%-- <asp:RequiredFieldValidator ID="BrRequiredFieldValidatorAccountNumber" runat="server" ControlToValidate="BrAccountNumber" ForeColor="Red" Font-Bold="true" ValidationGroup="BrValidationGroup" ErrorMessage="Account Number is Required"></asp:RequiredFieldValidator>--%>


                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Account Title:</label>
                            <asp:TextBox ID="BrAccountTitle" CssClass="form-control" runat="server"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="BrRequiredFieldValidatorAccountTitle" runat="server" ControlToValidate="BrAccountTitle" ForeColor="Red" Font-Bold="true" ValidationGroup="BrValidationGroup" ErrorMessage="Account Title is Required"></asp:RequiredFieldValidator>--%>

                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Relationship Since:</label>
                            <asp:TextBox ID="BrRelationShipSince" CssClass="form-control" runat="server"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="BrRequiredFieldValidatorRelationShipSince" runat="server" ControlToValidate="BrRelationShipSince" ForeColor="Red" Font-Bold="true" ValidationGroup="BrValidationGroup" ErrorMessage="Relatioship Since is Required"></asp:RequiredFieldValidator>--%>

                        </div>

                        <h3>Other Bank Relationship</h3>

                        <div class="form-group">
                            <label  class="lblReview">Bank Name:</label>
                            <asp:DropDownList ID="BrListOtherBranchCode" CssClass="form-control" runat="server"></asp:DropDownList>
                           <%-- <asp:RequiredFieldValidator ID="BrRequiredFieldValidatorListOtherBranchCode" runat="server" ControlToValidate="BrListOtherBranchCode" ForeColor="Red" Font-Bold="true" ValidationGroup="BrValidationGroup" ErrorMessage="Bank Code is Required"></asp:RequiredFieldValidator>--%>


                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Branch Name:</label>
                            <asp:TextBox ID="BrOtherBranchName" CssClass="form-control" runat="server"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="BrRequiredFieldValidatorOtherBranchName" runat="server" ControlToValidate="BrOtherBranchName" ForeColor="Red" Font-Bold="true" ValidationGroup="BrValidationGroup" ErrorMessage="Branch Name is Required"></asp:RequiredFieldValidator>--%>


                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Account Number:</label>
                            <asp:TextBox ID="BrOtherAccountNumber" CssClass="form-control" runat="server"></asp:TextBox>
                           <%-- <asp:RequiredFieldValidator ID="BrRequiredFieldValidatorOtherAccountNumber" runat="server" ControlToValidate="BrOtherAccountNumber" ForeColor="Red" Font-Bold="true" ValidationGroup="BrValidationGroup" ErrorMessage="Account Number is Required"></asp:RequiredFieldValidator>--%>

                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Account Title:</label>
                            <asp:TextBox ID="BrOtherAccountTitle" CssClass="form-control" runat="server"></asp:TextBox>
                           <%-- <asp:RequiredFieldValidator ID="BrRequiredFieldValidatorOtherAccountTitle" runat="server" ControlToValidate="BrOtherAccountTitle" ForeColor="Red" Font-Bold="true" ValidationGroup="BrValidationGroup" ErrorMessage="Account Title is Required"></asp:RequiredFieldValidator>--%>


                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Relationship Since:</label>
                            <asp:TextBox ID="BrOtherRelationshipSince" CssClass="form-control" runat="server"></asp:TextBox>
                            
                        </div>



                        <div class="form-group">
                            <asp:Button ID="BrSubmitButton" runat="server" Text="SAVE" CssClass="btn btn-primary" ValidationGroup="BrValidationGroup" OnClick="BrSubmitButton_Click" />
                            <button id="InResetBankRelation" onclick="openModal()" type="button" class="btn btn-primary" value="Reset">Reset</button>
                             <asp:Button ID="btnUpdateBr" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary" ValidationGroup="BrValidationGroup" OnClick="btnUpdateBr_Click" />
                             <asp:Button ID="btnSubmitCiff" Visible="false"  ClientIDMode="Static" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmitCif_Click" />
                        </div>

                    </div>

                    <div id="sectiong" class="tab-pane fade">
                        <asp:UpdatePanel ID="UpdatePanelFatca" runat="server" UpdateMode="Always">
                        <ContentTemplate>

                        <div class="form-group">
                            <label  class="lblReview">Resident:</label>
                            <asp:DropDownList ID="PiListResident" Enabled="False" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Citizen:</label>
                            <asp:DropDownList ID="PiListCitizen" Enabled="False" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Country of Birth in USA:</label>
                            <asp:DropDownList ID="PiListCountBirthUsa" Enabled="False" CssClass="form-control" runat="server"></asp:DropDownList>

                        </div>


                        <div class="form-group">
                            <label  class="lblReview">Address Country in USA:</label>
                            <asp:DropDownList ID="PiListAddCountUsa" Enabled="False" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>


                        <div class="form-group">
                            <label  class="lblReview">Phone No in USA: *</label>
                            <asp:DropDownList ID="PiListPhoneNoUsa" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="PiListPhoneNoUsa_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorPFatca" InitialValue="0" runat="server" ControlToValidate="PiListPhoneNoUsa" ForeColor="Red" Font-Bold="true" ValidationGroup="IndiFAtca" ErrorMessage="Phone No in USA is Required"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Contact No. Office:</label>
                            <asp:TextBox ID="PiContactOffice" CssClass="form-control" runat="server"></asp:TextBox>
                              <asp:CustomValidator Enabled="false"  ID="CustomValidatorOneNumberFAtca" Display="Dynamic" ForeColor="Red" Font-Bold="true" ValidationGroup="IndiFAtca" runat="server" ErrorMessage="Atleast One Contact No is Required"  ClientValidationFunction="doCustomValidateContactFatca" ValidateEmptyText="true"  ></asp:CustomValidator>
                        </div>


                        <div class="form-group">
                            <label  class="lblReview">Contact No. Residence:</label>
                            <asp:TextBox ID="PiContactResidence" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Mobile No:</label>
                            <asp:TextBox ID="PiMobileNo" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>


                        <div class="form-group">
                            <label  class="lblReview">Fax No:</label>
                            <asp:TextBox ID="PiFaxNo" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Residence Card (Green Card):</label>
                            <asp:DropDownList ID="PiListResidenceCard" CssClass="form-control" runat="server"></asp:DropDownList>


                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Transfer of Funds From/To USA: *</label>
                            <asp:DropDownList ID="PiListTransferOfFundsUSA" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorFundFAtca" InitialValue="0" runat="server" ControlToValidate="PiListTransferOfFundsUSA" ForeColor="Red" Font-Bold="true" ValidationGroup="IndiFAtca" ErrorMessage="Transfer of Funds From/To USA is Required"></asp:RequiredFieldValidator>

                        </div>

                        <div class="form-group">
                            <label  class="lblReview">FATCA Classification: *</label>
                            <asp:DropDownList ID="PiListFatcaClass" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="PiListFatcaClass_SelectedIndexChanged"></asp:DropDownList>
                             <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorFClassi" InitialValue="0" runat="server" ControlToValidate="PiListFatcaClass" ForeColor="Red" Font-Bold="true" ValidationGroup="IndiFAtca" ErrorMessage="FATCA Classification is Required"></asp:RequiredFieldValidator>
                        </div>
                         <div class="form-group" style="display: none">
                            <label  class="lblReview">TIN TYPE:</label>
                            <asp:DropDownList ID="PiListTinType" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                         <div class="form-group" style="display: none">
                            <label  class="lblReview">TIN:</label>
                            <asp:TextBox ID="PiTxtTin" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                         <div class="form-group">
                            <label  class="lblReview">Fatca Documentation Obtained Date: (DD-MM-YYYY)</label>
                            <div class="input-group date-control">
                                <asp:TextBox ID="PiTxtFatcaDocumentDate"  ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </button>
                                </span>
                            </div>
                        </div>
                        <asp:RequiredFieldValidator Enabled="false" Display="Dynamic" ID="RequiredFieldValidatorFDDate"  runat="server" ControlToValidate="PiTxtFatcaDocumentDate" ForeColor="Red" Font-Bold="true" ValidationGroup="IndiFAtca" ErrorMessage="Fatca Documentation Obtained Date is Required"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ValidationGroup="IndiFAtca" Display="Dynamic" ID="RegularExpressionValidatorFatcaDate" runat="server" ErrorMessage="Fatca Documentation Obtained Date must Be in DD-MM-YYYY FORMAT" ForeColor="Red" Font-Bold="true" ControlToValidate="PiTxtFatcaDocumentDate" ValidationExpression="^\d{2}-\d{2}-\d{4}$"></asp:RegularExpressionValidator>                       
                        <div class="form-group">
                            <label  class="lblReview">US Tax ID Type:</label>
                            <asp:DropDownList ID="PiListUsTaxIdType" CssClass="form-control" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator InitialValue="0" Enabled="false" Display="Dynamic" ID="RequiredFieldValidatorFTax"  runat="server" ControlToValidate="PiListUsTaxIdType" ForeColor="Red" Font-Bold="true" ValidationGroup="IndiFAtca" ErrorMessage="US Tax ID Type is Required"></asp:RequiredFieldValidator>

                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Tax No:</label>
                            <asp:TextBox ID="PiTaxNo" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator  Enabled="false" Display="Dynamic" ID="RequiredFieldValidatorFTaxNo"  runat="server" ControlToValidate="PiTaxNo" ForeColor="Red" Font-Bold="true" ValidationGroup="IndiFAtca" ErrorMessage="Tax No is Required"></asp:RequiredFieldValidator>

                        </div>

                        <div class="form-group">
                            <label  class="lblReview">FATCA Documentation:</label>
                            <div id="FatcaDiv" style="overflow-x: auto; overflow-y: auto; border: 1px #808080 solid; max-height: 215px; height: auto; height: 215px">
                                <asp:CheckBoxList ID="PiListFatcaDocumentation" Style="max-width: 300px;" ClientIDMode="Static" runat="server">
                                </asp:CheckBoxList>
                            </div>
                             <asp:CustomValidator Enabled="false"  runat="server" ID="CustomValidatorFatcaDoc"
                                    ClientValidationFunction="FatcaDocument" 
                                     Display="Dynamic" ForeColor="Red" Font-Bold="true" ValidationGroup="IndiFAtca"
                                    ErrorMessage="Please Select Atleast one Document"></asp:CustomValidator>
                        </div>

                           
	                 </ContentTemplate>                           
                </asp:UpdatePanel>



                        <div class="form-group">
                            <asp:Button ID="PiSubmitButton" ValidationGroup="IndiFAtca" runat="server" Text="SAVE" CssClass="btn btn-primary" OnClick="PiSubmitButton_Click" />
                            <button id="InResetFatca" onclick="openModal()" type="button" class="btn btn-primary" value="Reset">Reset</button>
                            <asp:Button ID="btnPiUpdate" ValidationGroup="IndiFAtca" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary"  OnClick="btnPiUpdate_Click" />
                             <asp:Button ID="btnSubmitCifg" Visible="false"  ClientIDMode="Static" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmitCif_Click" />
                        </div>
                    </div>

                </div>
                <!-- col-md-6 -->
                <div class="col-md-3">
                        <div class="form-group">
                           
                        </div>

                </div>
            </div>
            <!-- row -->
        </div>
        
    </div>

   <script type="text/javascript">
       $(document).ready(function () {

           Date.prototype.toDateInputValue = (function () {
               var local = new Date(this);
               local.setMinutes(this.getMinutes() - this.getTimezoneOffset());
               return local.toJSON().slice(0, 10);
           });

           $('#txtDOB').attr("max", new Date().toDateInputValue());

           $('#OiDateIssue').attr("max", new Date().toDateInputValue());

           $('#txtCnic').keyup(function (e)
           {
               var DocumentType = $('#lstPrimaryDocumentType option:selected').text();
               if (DocumentType == "CNIC")
               {
                   var regex = new RegExp("[^0-9-\b]+$");
                   var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                   if (e.keyCode != 189)
                   {
                       if (regex.test(str)) {
                           var cnic = $('#txtCnic').val();
                           $('#txtCnic').val(cnic.substring(0, cnic.length - 1))
                       }
                   }
                  
                   

               }

           });
       });
</script>

    <script>

        function FatcaDocument(source, args) {
            var chkListModules = document.getElementById('<%= PiListFatcaDocumentation.ClientID %>');
            var chkListinputs = chkListModules.getElementsByTagName("input");
            for (var i = 0; i < chkListinputs.length; i++) {
                if (chkListinputs[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }

        function doCustomValidateContactFatca(source, args) {

            args.IsValid = false;

            if (document.getElementById('<% =PiContactOffice.ClientID %>').value.length > 0) {
                 args.IsValid = true;
             }
             if (document.getElementById('<% =PiContactResidence.ClientID %>').value.length > 0) {
                args.IsValid = true;
            }
            if (document.getElementById('<% =PiMobileNo.ClientID %>').value.length > 0) {
                args.IsValid = true;
            }
        }

        function doCustomValidateContact(source, args)
        {

             args.IsValid = false;

            if (document.getElementById('<% =CiContactNoOffice.ClientID %>').value.length > 0) {
            args.IsValid = true;
            }
            if (document.getElementById('<% =CiContactNoResidence.ClientID %>').value.length > 0) {
                args.IsValid = true;
            }
            if (document.getElementById('<% =CiMobileNo.ClientID %>').value.length > 0) {
                args.IsValid = true;
            }
        }

      
        function ValidateModuleList(source, args) {
            var chkListModules = document.getElementById('<%= lstNationality.ClientID %>');
           var chkListinputs = chkListModules.getElementsByTagName("input");
           for (var i = 0; i < chkListinputs.length; i++) {
               if (chkListinputs[i].checked) {
                   args.IsValid = true;
                   return;
               }
           }
           args.IsValid = false;
        }

        function MaxNationality(source, args)
        {
            var chkListModules = document.getElementById('<%= lstNationality.ClientID %>');
            var chkListinputs = chkListModules.getElementsByTagName("input");
            var tot = 0;
            for (var i = 0; i < chkListinputs.length; i++) {
                if (chkListinputs[i].checked) {
                    tot = tot + 1;
                }
            }

            if (tot > 2)
                args.IsValid = false;
            else
                args.IsValid = true;
        }
      
</script>

    <Rev:Review ID="rev" runat="server"  Visible="false" />
    
    <!-- content -->
</asp:Content>
