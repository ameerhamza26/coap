<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPages/Default.Master" CodeBehind="UpdateGovernment.aspx.cs" Inherits="CAOP.UpdateGovernment" %>
<%@ Register Src="~/UserControls/ReviewControl.ascx" TagName="Review" TagPrefix="Rev" %>

<asp:Content ID="Head" ContentPlaceHolderID="head" runat="server">
    <title>Account opening Portal</title>

    <script src="Assets/js/alert.js"></script>

</asp:Content>

<asp:Content ID="Body" ContentPlaceHolderID="Body" runat="server">
    <div class="content">
        <asp:ScriptManager  runat="server"></asp:ScriptManager>
        <div id="alerts"></div>

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



        <h3>Government CIF Form</h3>
        <hr />

        <div class="row">
            <div class="col-md-3">
                <ul id="List1" class="nav nav-tabs-justified nav-menu">
                    <li id="BubasicInformation"><a id="BusBasicInfoAnchor" data-toggle="tab" href="#sectiona">Company Information</a></li>
                    <li id="BucontactInformation" style="display: none"><a id="BusContactInfoAnchor" data-toggle="tab" href="#sectionb">Address / Contact Information</a></li>
                    <li id="BuheadContactInfo" style="display: none"><a id="BusHeadContactInfoAnchor" data-toggle="tab" href="#sectionc">Head Office Address / Contact Information</a></li>
                    <li id="BubusinessDetail" style="display: none"><a id="BusBusinessDetailAnchor" data-toggle="tab" href="#sectiond">Business Detail</a></li>
                    <li id="BubankRelationship" style="display: none"><a id="BusBankRelationAnchor" data-toggle="tab" href="#sectione">Banking Relationship</a></li>
                    <li id="BufinanceInformation" style="display: none"><a id="BusFinanceInfoAnchor" data-toggle="tab" href="#sectionf">Financial Information</a></li>
                    <li id="BushareHolderInformation" style="display: none"><a id="BusShareHolderAnchor" data-toggle="tab" href="#sectiong">Directors Information</a></li>
                    <li id="BusFatca" style="display: none" ><a id="BusFatcaAnchor"  data-toggle="tab" href="#sectionh">FATCA</a></li>

                </ul>
            </div>

            <div class="col-md-6">
                <div class="tab-content">
                    <div id="sectiona" class="tab-pane fade in active">


                        <h3>Company Information</h3>
                        <div class="form-group" style="display: none">
                            <label  class="lblReview">Individual CIF Type</label>
                            <asp:DropDownList ID="BiListCifType" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="BiListCifType_SelectedIndexChanged" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Company / Business / Govt. Office Name: *</label>
                            <asp:TextBox ID="BiCompany" ClientIDMode="Static" CssClass="form-control" runat="server" MaxLength="40"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="BiRequiredFieldValidatorCompany" ValidationGroup="BiValidationGroup" runat="server" ControlToValidate="BiCompany" ForeColor="Red" Font-Bold="true" ErrorMessage="This Field is Required"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group">
                            <label  class="lblReview">NTN:</label>
                            <asp:TextBox ID="BiNtn" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                           <%-- <asp:CustomValidator ValidateEmptyText="true" ControlToValidate="BiNtn" Display="Dynamic" ClientValidationFunction="AnyOneReg" ForeColor="Red" Font-Bold="true" ValidationGroup="BiValidationGroup" ErrorMessage="NTN,Sales Tax No or Registration No, Any One is Required" ID="CustomValidator1" runat="server"></asp:CustomValidator>--%>
                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Sales Tax Registration No:</label>
                            <asp:TextBox ID="BiSalesTax" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>                            
                        </div>

                        <asp:UpdatePanel ID="UpdatePanelRegNo" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                        <div class="form-group">
                            <label  class="lblReview">Issuing Agency: </label>
                            <asp:DropDownList ID="BiListIssueAgency" AutoPostBack="true" ClientIDMode="Static" CssClass="form-control" runat="server" OnSelectedIndexChanged="BiListIssueAgency_SelectedIndexChanged"></asp:DropDownList>
                            <%-- <asp:RequiredFieldValidator Display="Dynamic" InitialValue="0"  ID="RequiredFieldValidatorIssuingAgency" ValidationGroup="BiValidationGroup" runat="server" ControlToValidate="BiListIssueAgency" ForeColor="Red" Font-Bold="true" ErrorMessage="Issuing Agency is Required"></asp:RequiredFieldValidator>--%>
                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Registration No:</label>
                            <asp:TextBox MaxLength="15" ID="BiRegistrationNo" ClientIDMode="Static" AutoPostBack="true" CssClass="form-control" runat="server" OnTextChanged="BiRegistrationNo_TextChanged"></asp:TextBox>                                                     
							<%-- <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidatorReg" ValidationGroup="BiValidationGroup" runat="server" ControlToValidate="BiRegistrationNo" ForeColor="Red" Font-Bold="true" ErrorMessage="Registration No is Required"></asp:RequiredFieldValidator>--%>
							</ContentTemplate>
                            <%--<Triggers>
                                <asp:AsyncPostBackTrigger ControlID="BiListIssueAgency" EventName="SelectedIndexChanged" />
                            </Triggers>--%>
                        </asp:UpdatePanel>
                            
                                                  
                           
                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Date of Registration(DD-MM-YYYY): </label>
                            <div class="input-group date-control">
                                <asp:TextBox ID="BiDateOfRegistration" ClientIDMode="Static" runat="server"  CssClass="form-control"></asp:TextBox>
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </button>
                                </span>
                            </div>
                        </div>
<%--                         <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidatorDOR" runat="server" ErrorMessage="Date of Registration must Be in DD-MM-YYYY FORMAT" ForeColor="Red" Font-Bold="true" ControlToValidate="BiDateOfRegistration" ValidationGroup="BiValidationGroup" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)[0-9]{2}$"></asp:RegularExpressionValidator>
                         <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorDOR" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="BiValidationGroup" ControlToValidate="BiDateOfRegistration" ErrorMessage="Date of Registration is required"></asp:RequiredFieldValidator>--%>

                        <div class="form-group">
                            <label  class="lblReview">Date of Commencement(DD-MM-YYYY): </label>
                            <div class="input-group date-control">
                                <asp:TextBox ID="BiDateOfCommence" ClientIDMode="Static" runat="server"  CssClass="form-control"></asp:TextBox>
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </button>
                                </span>
                            </div>
                        </div>
    <%--                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidatorDOC" runat="server" ErrorMessage="Date of Commencement must Be in DD-MM-YYYY FORMAT" ForeColor="Red" Font-Bold="true" ControlToValidate="BiDateOfRegistration" ValidationGroup="BiValidationGroup" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)[0-9]{2}$"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatoDOC" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="BiValidationGroup" ControlToValidate="BiDateOfCommence" ErrorMessage="Date of Commencement is required"></asp:RequiredFieldValidator>--%>

                        <div class="form-group">
                            <label  class="lblReview">Government Type: *</label>
                             <asp:DropDownList ID="BiListGovType" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator InitialValue="0" Display="Dynamic" ID="RequiredFieldValidatorGovType" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="BiValidationGroup" ControlToValidate="BiListGovType" ErrorMessage="Government Type is required"></asp:RequiredFieldValidator>
                        </div>
                        
                        <asp:UpdatePanel ID="UpdatePanelBSegment" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                        <div class="form-group">
                            <label  class="lblReview">Age of Business (Years):</label>
                            <asp:TextBox ID="BiPastBusiness" TextMode="Number" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                         <div class="form-group">
                            <label  class="lblReview">Nature of Business Concern:</label>
                            <asp:DropDownList ID="BiListNatureOfbusiness" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                        <div class="form-group" style="display: none">
                            <label  class="lblReview">Nature of Business Description:</label>
                            <asp:TextBox ID="BiNatureOfBusinessDescr" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                         <div class="form-group">
                            <label  class="lblReview">Institution Type: </label>
                            <asp:DropDownList ID="BiListInstitutionType" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                          <%--  <asp:RequiredFieldValidator InitialValue="0"  Display="Dynamic"  ID="RequiredFieldValidatorInstitutionType" ValidationGroup="BiValidationGroup" runat="server" ControlToValidate="BiListInstitutionType" ForeColor="Red" Font-Bold="true" ErrorMessage="Institution Type is Required"></asp:RequiredFieldValidator>--%>
                        </div>
                         <div class="form-group">
                            <label  class="lblReview">SIC CODE: *</label>
                            <asp:DropDownList ID="BiListSicCode" ClientIDMode="Static" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="BiListSicCode_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" InitialValue="0" ID="RequiredFieldValidatorSicCode" ValidationGroup="BiValidationGroup"  runat="server" ControlToValidate="BiListSicCode" ForeColor="Red" Font-Bold="true" ErrorMessage="SIC CODE is Required"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Sub Industry: *</label>
                            <asp:DropDownList ID="BiListSubIndustry" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator  Display="Dynamic" InitialValue="0" ID="RequiredFieldValidatorSubIndustry" ValidationGroup="BiValidationGroup" runat="server" ControlToValidate="BiListSubIndustry" ForeColor="Red" Font-Bold="true" ErrorMessage="Sub Industry is Required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Corporate Status: </label>
                            <asp:DropDownList ID="BiListNatureOfAccount" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                              <%--  <asp:RequiredFieldValidator Display="Dynamic" InitialValue="0" ID="RequiredFieldValidatorNatureCifAccount" ValidationGroup="BiValidationGroup" runat="server" ControlToValidate="BiListNatureOfAccount" ForeColor="Red" Font-Bold="true" ErrorMessage="Corporate Status is Required"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Business Segment: *</label>
                            <asp:DropDownList ID="BiListCustomerClassification" ClientIDMode="Static" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="BiListCustomerClassification_SelectedIndexChanged"></asp:DropDownList>
                                <asp:RequiredFieldValidator Display="Dynamic" InitialValue="0" ID="RequiredFieldValidatorCClassific" ValidationGroup="BiValidationGroup"  runat="server" ControlToValidate="BiListCustomerClassification" ForeColor="Red" Font-Bold="true" ErrorMessage="Business Segment is Required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Business Type: *</label>
                            <asp:DropDownList ID="BiListBusinessType" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator  Display="Dynamic" InitialValue="0" ID="RequiredFieldValidatorBusinessType" ValidationGroup="BiValidationGroup" runat="server" ControlToValidate="BiListBusinessType" ForeColor="Red" Font-Bold="true" ErrorMessage="Business Type is Required"></asp:RequiredFieldValidator>
                        </div>
                            <div class="form-group">
                        <label  class="lblReview">Category As per NBP:</label>
                        <asp:DropDownList ID="BiListCategoryNBP" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                        <%-- <asp:RequiredFieldValidator  Display="Dynamic" InitialValue="0" ID="RequiredFieldValidatorCNBP" ValidationGroup="BiValidationGroup" runat="server" ControlToValidate="BiListCategoryNBP" ForeColor="Red" Font-Bold="true" ErrorMessage="Category As per NBP is Required"></asp:RequiredFieldValidator>--%>
                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Category As per SBP:</label>
                            <asp:DropDownList ID="BiListCategorySBP" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                            <%-- <asp:RequiredFieldValidator  Display="Dynamic" InitialValue="0" ID="RequiredFieldValidatorCSbp" ValidationGroup="BiValidationGroup" runat="server" ControlToValidate="BiListCategorySBP" ForeColor="Red" Font-Bold="true" ErrorMessage="Category As per SBP is Required"></asp:RequiredFieldValidator>--%>
                        </div>
                                                    

                        </ContentTemplate>
                       </asp:UpdatePanel>

                        

                        
                        <div class="form-group" style="display: none">
                            <label  class="lblReview">Group CIF No:</label>
                            <asp:TextBox ID="BiCifNo" ClientIDMode="Static" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                        </div>

                        
                         <div class="form-group">
                            <label  class="lblReview">Country of Incorporation: *</label>
                            <asp:DropDownList ID="BiListCountryIncorporation" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator InitialValue="0" Display="Dynamic" ID="RequiredFieldValidatorCountryincorp" ValidationGroup="BiValidationGroup" runat="server" ControlToValidate="BiListCountryIncorporation" ForeColor="Red" Font-Bold="true" ErrorMessage="Country of Incorporation is Required"></asp:RequiredFieldValidator>
                        </div>

                      

                        <div class="form-group">
                            <label  class="lblReview">Category As per Basel I & II:</label>
                            <asp:DropDownList ID="BiListCategoryBasel" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Does Customer Deal In: *</label>
                            <asp:DropDownList ID="BiListCustomerDealIn" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator InitialValue="0" Display="Dynamic" ID="RequiredFieldValidatorCustomerDeal" ValidationGroup="BiValidationGroup" runat="server" ControlToValidate="BiListCustomerDealIn" ForeColor="Red" Font-Bold="true" ErrorMessage="Does Customer Deal In is Required"></asp:RequiredFieldValidator>
                        </div>

                         <div class="form-group">
                             <label  class="lblReview">All Documents Verified: </label>
                             <br />
                             <asp:CheckBox runat="server" ID="chkDocument"   Text="YES" />
                                    
                         </div>

                        <div class="form-group">
                             <asp:Button ID="btnUpdateBasic" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary" ValidationGroup="BiValidationGroup" OnClick="btnUpdateBasic_Click" />
                             <asp:Button ID="btnSubmitCifa" Visible="false" ClientIDMode="Static" runat="server" Text="SUBMIT" CssClass="btn btn-primary" OnClick="btnSubmitCif_Click" />
                            <asp:Button ID="BibtnSubmitBaisc" ClientIDMode="Static" runat="server" Text="SAVE" CssClass="btn btn-primary" ValidationGroup="BiValidationGroup" OnClick="BibtnSubmitBaisc_Click" />
                            <button id="BusbtnResetBasicInfo" onclick="openModal()" type="button" class="btn btn-primary" value="Reset">Reset</button>
                           
                        </div>                 
                    </div>

                    <div id="sectionb" class="tab-pane fade">
                        <h3>Mailing Adress</h3>
                        <div class="form-group">
                            <label  class="lblReview">Country: *</label>
                            <asp:DropDownList ID="CiListCountry" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator Display="Dynamic" InitialValue="0" ID="BrRequiredFieldValidatorListCountryA" runat="server" ControlToValidate="CiListCountry" ForeColor="Red" Font-Bold="true" ValidationGroup="CiValidationGroup" ErrorMessage="Country is Required"></asp:RequiredFieldValidator>
                        </div>
                         <div class="form-group">
                            <label  class="lblReview">Province: *</label>
                            <asp:DropDownList ID="CiListProvince" CssClass="form-control" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorPermanentProvice" Enabled="true" runat="server" InitialValue="0" ControlToValidate="CiListProvince" ForeColor="Red" Font-Bold="true" ValidationGroup="CiValidationGroup" ErrorMessage="Province is Required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">City: *</label>
                            <asp:DropDownList ID="CiListCity" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" InitialValue="0" ID="RequiredFieldValidator3" runat="server" ControlToValidate="CiListCity" ForeColor="Red" Font-Bold="true" ValidationGroup="CiValidationGroup" ErrorMessage="City is Required"></asp:RequiredFieldValidator>
                        </div>                      
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
                            <asp:TextBox ID="CiTxtDistrict" CssClass="form-control" runat="server"></asp:TextBox>                             
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">PO Box:</label>
                            <asp:TextBox ID="CiPoBox" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>                        
                        <div class="form-group">
                            <label  class="lblReview">Postal Code:</label>
                            <asp:TextBox ID="CiPostalCode" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                        <h3>Contact Numbers</h3>
                        <div class="form-group">
                            <label  class="lblReview">Tel (Off):</label>
                            <asp:TextBox ID="CiOfficeNo" TextMode="Number" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                              <asp:CustomValidator ValidateEmptyText="true" ControlToValidate="CiOfficeNo" Display="Dynamic" ClientValidationFunction="AnyOneContact" ForeColor="Red" Font-Bold="true" ValidationGroup="CiValidationGroup" ErrorMessage="  Any One Contact is Required" ID="CustomValidator2" runat="server"></asp:CustomValidator>
                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Mobile No:</label>
                            <asp:TextBox ID="CiMobileNo" TextMode="Number"  ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Fax No:</label>
                            <asp:TextBox ID="CiFaxNo" TextMode="Number" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Email:</label>
                            <asp:TextBox ID="CiEmail" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Web:</label>
                            <asp:TextBox ID="CiWeb" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>

                         <h3>Contact Person Details</h3>

                        <div class="form-group">
                            <label  class="lblReview">Name of Person: *</label>
                            <asp:TextBox ID="CiPName" ClientIDMode="Static" CssClass="form-control" MaxLength="20" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorPName" runat="server" ControlToValidate="CiPName" ForeColor="Red" Font-Bold="true" ValidationGroup="CiValidationGroup" ErrorMessage="Name of Person is Required"></asp:RequiredFieldValidator>                            
                        </div>
                         <div class="form-group">
                            <label  class="lblReview">Designation: *</label>
                            <asp:TextBox ID="CiDesig" ClientIDMode="Static" CssClass="form-control" MaxLength="20" runat="server"></asp:TextBox>
                              <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorDesig" runat="server" ControlToValidate="CiDesig" ForeColor="Red" Font-Bold="true" ValidationGroup="CiValidationGroup" ErrorMessage="Designation is Required"></asp:RequiredFieldValidator>                            
                        </div>
                         <div class="form-group">
                            <label  class="lblReview">CNIC No: *</label>
                            <asp:TextBox ID="CiCnic" ClientIDMode="Static" CssClass="form-control"  runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator   Display="Dynamic" ID="RegularExpressionValidatorCnicCp" runat="server" ErrorMessage="The CNIC must be in correct format e.g xxxxx-xxxxxxx-x" ForeColor="Red" Font-Bold="true" ControlToValidate="CiCnic" ValidationGroup="CiValidationGroup" ValidationExpression="^\d{5}-\d{7}-\d{1}$" ></asp:RegularExpressionValidator>     
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorCnicNo" runat="server" ControlToValidate="CiCnic" ForeColor="Red" Font-Bold="true" ValidationGroup="CiValidationGroup" ErrorMessage="CNIC No is Required"></asp:RequiredFieldValidator>                                                   
                        </div>
                        <div class="form-group">
                              <label  class="lblReview">CNIC Expiry Date (DD-MM-YYYY): *</label>
                              <asp:TextBox ID="CiCnicEDate" ClientIDMode="Static" CssClass="form-control"  runat="server"></asp:TextBox>
                             <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidatorCExp" runat="server" ErrorMessage="CNIC Expiry Date must Be in DD-MM-YYYY FORMAT" ForeColor="Red" Font-Bold="true" ControlToValidate="CiCnicEDate" ValidationGroup="CiValidationGroup" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)[0-9]{2}$"></asp:RegularExpressionValidator>
                             <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorCExpiry" runat="server" ControlToValidate="CiCnicEDate" ForeColor="Red" Font-Bold="true" ValidationGroup="CiValidationGroup" ErrorMessage="CNIC Expiry Date is Required"></asp:RequiredFieldValidator>                                                   
                            
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">NTN: </label>
                             <asp:TextBox ID="CiPNtn" ClientIDMode="Static" MaxLength="15" CssClass="form-control"  runat="server"></asp:TextBox>
                        </div>

                        <div class="form-group">
                             <asp:Button ID="btnUpdateCi" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary" ValidationGroup="CiValidationGroup" OnClick="btnUpdateCi_Click" />
                             <asp:Button ID="btnSubmitCifb" Visible="false" ClientIDMode="Static" runat="server" Text="SUBMIT" CssClass="btn btn-primary" OnClick="btnSubmitCif_Click" />
                            <asp:Button ID="CiSubmitButton" ClientIDMode="Static" runat="server" Text="SAVE" CssClass="btn btn-primary" ValidationGroup="CiValidationGroup" OnClick="CiSubmitButton_Click" />
                            <button id="BusCiResetButton" onclick="openModal()" type="button" class="btn btn-primary" value="Reset">Reset</button>


                        </div>


                    </div>
                    <!--sectionb-->

                    <div id="sectionc" class="tab-pane fade">

                        <h3>Head Office Address</h3>
                        <div class="form-group">
                            <label  class="lblReview">Country: *</label>
                            <asp:DropDownList ID="HoListCountryCode" CssClass="form-control" runat="server"></asp:DropDownList> 
                             <asp:RequiredFieldValidator Display="Dynamic" InitialValue="0"  ID="RequiredFieldValidatorHOBCountryCode" runat="server" ControlToValidate="HoListCountryCode" ForeColor="Red" Font-Bold="true" ValidationGroup="HoValidationGroup" ErrorMessage="Country is Required"></asp:RequiredFieldValidator>                                               
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Province: *</label>
                            <asp:DropDownList ID="HoListProvince" CssClass="form-control" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorHoProvince" Enabled="true" runat="server" InitialValue="0" ControlToValidate="HoListProvince" ForeColor="Red" Font-Bold="true" ValidationGroup="HoValidationGroup" ErrorMessage="Province is Required"></asp:RequiredFieldValidator>
                        </div>
                         <div class="form-group">
                            <label  class="lblReview">City: *</label>
                             <asp:DropDownList ID="HoListCity" CssClass="form-control" runat="server"></asp:DropDownList>  
                             <asp:RequiredFieldValidator Display="Dynamic" InitialValue="0"  ID="RequiredFieldValidatorHOBCity" runat="server" ControlToValidate="HoListCity" ForeColor="Red" Font-Bold="true" ValidationGroup="HoValidationGroup" ErrorMessage="City is Required"></asp:RequiredFieldValidator>                                                                        
                        </div>
                         
                         <div class="form-group">
                            <label  class="lblReview">Address Line 1: *</label>
                            <asp:TextBox ID="HoTxtBuilding" MaxLength="40" CssClass="form-control" runat="server"></asp:TextBox>         
                              <asp:RequiredFieldValidator Display="Dynamic"  ID="RequiredFieldValidatorTxtBuilding" runat="server" ControlToValidate="HoTxtBuilding" ForeColor="Red" Font-Bold="true" ValidationGroup="HoValidationGroup" ErrorMessage="Address Line 1 is Required"></asp:RequiredFieldValidator>                    
                        </div>

                         <div class="form-group">
                            <label  class="lblReview">Address Line 2:</label>
                            <asp:TextBox ID="HoTxtFloor" MaxLength="40" CssClass="form-control" runat="server"></asp:TextBox>                             
                        </div>
                         <div class="form-group">
                            <label  class="lblReview">Address Line 3:</label>
                            <asp:TextBox ID="HoTxtStreet" MaxLength="40" CssClass="form-control" runat="server"></asp:TextBox>
                            
                        </div>
                       
                         <div class="form-group">
                            <label  class="lblReview">District:</label>
                            <asp:TextBox ID="HoTxtDistrict" CssClass="form-control" runat="server"></asp:TextBox>                             
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Post Office Box:</label>
                            <asp:TextBox ID="HoTxtPostBox" CssClass="form-control" runat="server"></asp:TextBox>                             
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Postal Code:</label>
                            <asp:TextBox ID="HoTxtPotalCode" CssClass="form-control" runat="server"></asp:TextBox>                             
                        </div>


                         <h3>Registered/Legal Address</h3>
                        <div class="form-group">
                            <label  class="lblReview">Country: *</label>
                            <asp:DropDownList ID="HoListCountryCodeReg" CssClass="form-control" runat="server"></asp:DropDownList>     
                             <asp:RequiredFieldValidator Display="Dynamic" InitialValue="0"  ID="RequiredFieldValidator1" runat="server" ControlToValidate="HoListCountryCodeReg" ForeColor="Red" Font-Bold="true" ValidationGroup="HoValidationGroup" ErrorMessage="Country is Required"></asp:RequiredFieldValidator>                                                                      
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Province: *</label>
                            <asp:DropDownList ID="HoListProvinceReg" CssClass="form-control" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorHpProvinceReg" Enabled="true" runat="server" InitialValue="0" ControlToValidate="HoListProvinceReg" ForeColor="Red" Font-Bold="true" ValidationGroup="HoValidationGroup" ErrorMessage="Province is Required"></asp:RequiredFieldValidator>
                        </div>
                         <div class="form-group">
                            <label  class="lblReview">City: *</label>
                            <asp:DropDownList ID="HoListCityReg" CssClass="form-control" runat="server"></asp:DropDownList>               
                              <asp:RequiredFieldValidator Display="Dynamic" InitialValue="0"  ID="RequiredFieldValidator2" runat="server" ControlToValidate="HoListCityReg" ForeColor="Red" Font-Bold="true" ValidationGroup="HoValidationGroup" ErrorMessage="City is Required"></asp:RequiredFieldValidator>                                                                                    
                        </div>                       
                         <div class="form-group">
                            <label  class="lblReview">Address Line 1: *</label>
                            <asp:TextBox ID="HoTxtBuildingReg" MaxLength="40" CssClass="form-control" runat="server"></asp:TextBox>         
                              <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorTxtBuildingReg" runat="server" ControlToValidate="HoTxtBuildingReg" ForeColor="Red" Font-Bold="true" ValidationGroup="HoValidationGroup" ErrorMessage="Address Line 1 is Required"></asp:RequiredFieldValidator>                    
                        </div>
                         <div class="form-group">
                            <label  class="lblReview">Address Line 2:</label>
                            <asp:TextBox ID="HoTxtFloorReg" MaxLength="40" CssClass="form-control" runat="server"></asp:TextBox>                             
                        </div>
                         <div class="form-group">
                            <label  class="lblReview">Address Line 3:</label>
                            <asp:TextBox ID="HoTxtStreetReg" MaxLength="40" CssClass="form-control" runat="server"></asp:TextBox>
                            
                        </div>
                       
                         <div class="form-group">
                            <label  class="lblReview">District:</label>
                            <asp:TextBox ID="HoTxtDistrictReg" CssClass="form-control" runat="server"></asp:TextBox>                             
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Post Office Box:</label>
                            <asp:TextBox ID="HoTxtPostBoxReg" CssClass="form-control" runat="server"></asp:TextBox>                             
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Postal Code:</label>
                            <asp:TextBox ID="HoTxtPotalCodeReg" CssClass="form-control" runat="server"></asp:TextBox>                             
                        </div>
                     

                        <h3>Contact Numbers</h3>
                        <div class="form-group">
                            <label  class="lblReview">Tel (Off):</label>
                            <asp:TextBox ID="HoOfficeNo" TextMode="Number" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                             <asp:CustomValidator ValidateEmptyText="true" ControlToValidate="HoOfficeNo" Display="Dynamic" ClientValidationFunction="AnyOneContactHead" ForeColor="Red" Font-Bold="true" ValidationGroup="HoValidationGroup" ErrorMessage="  Any One Contact is Required" ID="CustomValidator3" runat="server"></asp:CustomValidator>

                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Mobile No:</label>
                            <asp:TextBox ID="HoMobileNo" TextMode="Number" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>

                        <div class="form-group">
                            <label class="lblReview">Fax No:</label>
                            <asp:TextBox ID="HoFaxNo" TextMode="Number" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>

                        <div class="form-group">
                            <label class="lblReview">Email:</label>
                            <asp:TextBox ID="HoEmail" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>




                        <div class="form-group">
                             <asp:Button ID="btnUpdateHo" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary" ValidationGroup="HoValidationGroup" OnClick="btnUpdateHo_Click" />
                             <asp:Button ID="btnSubmitCifc" Visible="false" ClientIDMode="Static" runat="server" Text="SUBMIT" CssClass="btn btn-primary" OnClick="btnSubmitCif_Click" />
                            <asp:Button ID="HoSubmitButton" ClientIDMode="Static" runat="server" Text="SAVE" CssClass="btn btn-primary" ValidationGroup="HoValidationGroup" OnClick="HoSubmitButton_Click" />
                            <button id="BusHoResetButton" onclick="openModal()" type="button" class="btn btn-primary" value="Reset">Reset</button>

                        </div>


                    </div>
                    <!--sectionc-->

                    <div id="sectiond" class="tab-pane fade">
                        <div class="form-group" style="display: none">
                            <label class="lblReview">Information Type:</label>
                            <asp:DropDownList ID="BdListInformationType" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>


                        <div class="form-group" style="display: none">
                            <label class="lblReview">Information Type Description:</label>
                            <asp:TextBox ID="BdInfoDescr" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>

                        <div class="form-group">
                            <label class="lblReview">Main Business Line Activity: </label>
                            <asp:TextBox ID="BdTextMBLA" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                        <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" Display="Dynamic" runat="server" ControlToValidate="BdTextMBLA" ForeColor="Red" Font-Bold="true" ValidationGroup="BdValidationGroup" ErrorMessage="Main Business Line Activity is Required"></asp:RequiredFieldValidator>                    --%>

                        </div>

                        <div class="form-group">
                            <label class="lblReview">Major Suppliers: </label>
                            <asp:TextBox ID="BdTextMS" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                           <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator12" Display="Dynamic" runat="server" ControlToValidate="BdTextMS" ForeColor="Red" Font-Bold="true" ValidationGroup="BdValidationGroup" ErrorMessage="Major Suppliers is Required"></asp:RequiredFieldValidator>                    --%>
                        </div>

                        <div class="form-group">
                            <label class="lblReview">Major Customers: </label>
                            <asp:TextBox ID="BdTextMC" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                          <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator13" Display="Dynamic" runat="server" ControlToValidate="BdTextMC" ForeColor="Red" Font-Bold="true" ValidationGroup="BdValidationGroup" ErrorMessage="Major Customers is Required"></asp:RequiredFieldValidator>                    --%>
                        </div>

                        <asp:UpdatePanel ID="UpdatePanelBusiness" runat="server" UpdateMode="Always">
	                    <ContentTemplate>
                        <div class="form-group">
                            <label class="lblReview">Busines In Cities of Pakistan:</label>
                            <div style="overflow-x: hidden; overflow-y: auto; border: 1px #808080 solid; max-height: 215px; height: auto; height: 215px">
                                <asp:CheckBoxList ID="BdListBusinessInCities" ClientIDMode="Static" runat="server" RepeatColumns="2"></asp:CheckBoxList>
                            </div>
                            <%-- <asp:CustomValidator Enabled="false"  ID="CustomValidatorBusinessCities" ClientValidationFunction="CheckBussCities" Display="Dynamic" ForeColor="Red" Font-Bold="true" runat="server"  ErrorMessage="City is Required" ValidationGroup="BdValidationGroup" OnServerValidate="CustomValidatorBusinessCities_ServerValidate" ></asp:CustomValidator>--%>
                        </div>

                        <div class="form-group">
                            <label class="lblReview">Business In Countries:</label>
                            <div style="overflow-x: hidden; overflow-y: auto; border: 1px #808080 solid; max-height: 215px; height: auto; height: 215px">
                                <asp:CheckBoxList ID="BdListBusinessInCountry" AutoPostBack="true" ClientIDMode="Static" runat="server" RepeatColumns="2" OnSelectedIndexChanged="BdListBusinessInCountry_SelectedIndexChanged"></asp:CheckBoxList>
                            </div>
                            <%-- <asp:CustomValidator  ID="CustomValidatorBusinessCountries" ClientValidationFunction="CheckBussCountry" Display="Dynamic" ForeColor="Red" Font-Bold="true" runat="server"  ErrorMessage="Country is Required" ValidationGroup="BdValidationGroup" OnServerValidate="CustomValidatorBusinessCountries_ServerValidate" ></asp:CustomValidator>--%>
                        </div>


	                    </ContentTemplate>                           
                        </asp:UpdatePanel>

                       


                        <div class="form-group">
                            <asp:Button ID="btnUpdateBd" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary" ValidationGroup="BdValidationGroup" OnClick="btnUpdateBd_Click" />
                             <asp:Button ID="btnSubmitCifd" Visible="false" ClientIDMode="Static" runat="server" Text="SUBMIT" CssClass="btn btn-primary" OnClick="btnSubmitCif_Click" />
                            <asp:Button ID="BdSubmitButton" ClientIDMode="Static" runat="server" Text="SAVE" CssClass="btn btn-primary" ValidationGroup="BdValidationGroup" OnClick="BdSubmitButton_Click" />
                            <button id="BusBdResetButton" onclick="openModal()" type="button" class="btn btn-primary" value="Reset">Reset</button>

                        </div>



                    </div>
                    <!--sectiond-->

                    <div id="sectione" class="tab-pane fade">
                        <h3>NBP Relationship</h3>
                        <div class="form-group">
                            <label class="lblReview">Branch Information:</label>
                            <asp:DropDownList ID="BrListBranchInfo" CssClass="form-control" runat="server"></asp:DropDownList>
                          <%--  <asp:RequiredFieldValidator ID="BrRequiredFieldValidatorListBranchInfo" runat="server" ControlToValidate="BrListBranchInfo" ForeColor="Red" Font-Bold="true" ValidationGroup="BrValidationGroup" ErrorMessage="Branch Information is Required"></asp:RequiredFieldValidator>--%>


                        </div>

                        <div class="form-group">
                            <label class="lblReview">Account Type:</label>
                            <asp:DropDownList ID="BrListAccountType" CssClass="form-control" runat="server"></asp:DropDownList>
                           <%-- <asp:RequiredFieldValidator ID="BrRequiredFieldValidatorListAccountType" runat="server" ControlToValidate="BrListAccountType" ForeColor="Red" Font-Bold="true" ValidationGroup="BrValidationGroup" ErrorMessage="Account Type is Required"></asp:RequiredFieldValidator>--%>


                        </div>
                        <div class="form-group">
                            <label class="lblReview">Account Number:</label>
                            <asp:TextBox ID="BrAccountNumber" CssClass="form-control" runat="server"></asp:TextBox>
                           <%-- <asp:RequiredFieldValidator ID="BrRequiredFieldValidatorAccountNumber" runat="server" ControlToValidate="BrAccountNumber" ForeColor="Red" Font-Bold="true" ValidationGroup="BrValidationGroup" ErrorMessage="Account Number is Required"></asp:RequiredFieldValidator>--%>


                        </div>

                        <div class="form-group">
                            <label class="lblReview">Account Title:</label>
                            <asp:TextBox ID="BrAccountTitle" CssClass="form-control" runat="server"></asp:TextBox>
                           <%-- <asp:RequiredFieldValidator ID="BrRequiredFieldValidatorAccountTitle" runat="server" ControlToValidate="BrAccountTitle" ForeColor="Red" Font-Bold="true" ValidationGroup="BrValidationGroup" ErrorMessage="Account Title is Required"></asp:RequiredFieldValidator>--%>

                        </div>

                        <div class="form-group">
                            <label class="lblReview">Relationship Since:</label>
                            <asp:TextBox ID="BrRelationShipSince" CssClass="form-control" runat="server"></asp:TextBox>
                         <%--   <asp:RequiredFieldValidator ID="BrRequiredFieldValidatorRelationShipSince" runat="server" ControlToValidate="BrRelationShipSince" ForeColor="Red" Font-Bold="true" ValidationGroup="BrValidationGroup" ErrorMessage="Relatioship Since is Required"></asp:RequiredFieldValidator>--%>

                        </div>

                        <h3>Other Bank Relationship</h3>

                        <div class="form-group">
                            <label class="lblReview">Branch Code:</label>
                            <asp:DropDownList ID="BrListOtherBranchCode" CssClass="form-control" runat="server"></asp:DropDownList>
                           <%-- <asp:RequiredFieldValidator ID="BrRequiredFieldValidatorListOtherBranchCode" runat="server" ControlToValidate="BrListOtherBranchCode" ForeColor="Red" Font-Bold="true" ValidationGroup="BrValidationGroup" ErrorMessage="Bank Code is Required"></asp:RequiredFieldValidator>--%>


                        </div>

                        <div class="form-group">
                            <label class="lblReview">Branch Name:</label>
                            <asp:TextBox ID="BrOtherBranchName" CssClass="form-control" runat="server"></asp:TextBox>
                          <%--  <asp:RequiredFieldValidator ID="BrRequiredFieldValidatorOtherBranchName" runat="server" ControlToValidate="BrOtherBranchName" ForeColor="Red" Font-Bold="true" ValidationGroup="BrValidationGroup" ErrorMessage="Branch Name is Required"></asp:RequiredFieldValidator>--%>


                        </div>

                        <div class="form-group">
                            <label class="lblReview">Account Number:</label>
                            <asp:TextBox ID="BrOtherAccountNumber" CssClass="form-control" runat="server"></asp:TextBox>
                          <%--  <asp:RequiredFieldValidator ID="BrRequiredFieldValidatorOtherAccountNumber" runat="server" ControlToValidate="BrOtherAccountNumber" ForeColor="Red" Font-Bold="true" ValidationGroup="BrValidationGroup" ErrorMessage="Account Number is Required"></asp:RequiredFieldValidator>--%>

                        </div>




                        <div class="form-group">
                            <label class="lblReview">Account Title:</label>
                            <asp:TextBox ID="BrOtherAccountTitle" CssClass="form-control" runat="server"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="BrRequiredFieldValidatorOtherAccountTitle" runat="server" ControlToValidate="BrOtherAccountTitle" ForeColor="Red" Font-Bold="true" ValidationGroup="BrValidationGroup" ErrorMessage="Account Title is Required"></asp:RequiredFieldValidator>--%>


                        </div>

                        <div class="form-group">
                            <label class="lblReview">Relationship Since:</label>
                            <asp:TextBox ID="BrOtherRelationshipSince" CssClass="form-control" runat="server"></asp:TextBox>
                           <%-- <asp:RequiredFieldValidator ID="BrRequiredFieldValidatorOtherRelationshipSince" runat="server" ControlToValidate="BrOtherRelationshipSince" ForeColor="Red" Font-Bold="true" ValidationGroup="BrValidationGroup" ErrorMessage="Relationship Since is Required"></asp:RequiredFieldValidator>--%>



                        </div>


                        <div class="form-group">
                             <asp:Button ID="btnUpdateBr" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary" ValidationGroup="BdValidationGroup" OnClick="btnUpdateBr_Click" />
                             <asp:Button ID="btnSubmitCife" Visible="false" ClientIDMode="Static" runat="server" Text="SUBMIT" CssClass="btn btn-primary" OnClick="btnSubmitCif_Click" />
                            <asp:Button ID="BrSubmitButton" ClientIDMode="Static" runat="server" Text="SAVE" CssClass="btn btn-primary" ValidationGroup="BrValidationGroup" OnClick="BrSubmitButton_Click" />
                            <button id="BusBrResetButton" onclick="openModal()" type="button" class="btn btn-primary" value="Reset">Reset</button>

                        </div>

                    </div>
                    <!--sectione-->

                    <div id="sectionf" class="tab-pane fade">

                        <div class="form-group">
                            <label class="lblReview">Total Asset Value: </label>
                            <asp:TextBox ID="FiTotalAssetValue" TextMode="Number" AutoPostBack="true" ClientIDMode="Static" CssClass="form-control" runat="server" OnTextChanged="FiTotalAssetValue_TextChanged"></asp:TextBox>
                          <%--  <asp:RequiredFieldValidator ControlToValidate="FiTotalAssetValue" Display="Dynamic"  ID="RequiredFieldValidator9" runat="server"  ForeColor="Red" Font-Bold="true" ValidationGroup="FiValidationGroup" ErrorMessage="Total Asset Value is Required"></asp:RequiredFieldValidator>--%>
                        </div>

                        <div class="form-group">
                            <label class="lblReview">Liabilities: </label>
                            <asp:TextBox ID="FiLiabilities" TextMode="Number" AutoPostBack="true" ClientIDMode="Static" CssClass="form-control" runat="server" OnTextChanged="FiLiabilities_TextChanged"></asp:TextBox>
                           <%--  <asp:RequiredFieldValidator ControlToValidate="FiLiabilities" Display="Dynamic"  ID="RequiredFieldValidator10" runat="server"  ForeColor="Red" Font-Bold="true" ValidationGroup="FiValidationGroup" ErrorMessage="Liabilities is Required"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Net Worth:</label>
                            <asp:UpdatePanel ID="UpdatePanelNetFincancalBusiness" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:TextBox ID="FiNetWorth" ReadOnly="true" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
	                                <asp:AsyncPostBackTrigger ControlID="FiTotalAssetValue" EventName="TextChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="FiLiabilities" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>

                            

                        </div>


                        <div class="form-group" style="display: none">
                            <label class="lblReview">Monthly Turn Over (Debit): *</label>
                            <asp:DropDownList ID="FiListMonthTurnOverDebit" CssClass="form-control" runat="server"></asp:DropDownList>
                            <%-- <asp:RequiredFieldValidator ControlToValidate="FiListMonthTurnOverDebit" Display="Dynamic" InitialValue="0" ID="RequiredFieldValidator4" runat="server"  ForeColor="Red" Font-Bold="true" ValidationGroup="FiValidationGroup" ErrorMessage="Monthly Turn Over (Debit) is Required"></asp:RequiredFieldValidator>--%>

                        </div>

                        <div class="form-group" style="display: none">
                            <label class="lblReview">Monthly Turn Over (Credit): *</label>
                            <asp:DropDownList ID="FiListMonthTurnOverCredit" CssClass="form-control" runat="server"></asp:DropDownList>
                          <%--  <asp:RequiredFieldValidator ControlToValidate="FiListMonthTurnOverCredit" Display="Dynamic" InitialValue="0" ID="RequiredFieldValidator5" runat="server"  ForeColor="Red" Font-Bold="true" ValidationGroup="FiValidationGroup" ErrorMessage="Monthly Turn Over (Credit) is Required"></asp:RequiredFieldValidator>--%>

                        </div>

                        <div class="form-group" style="display: none">
                            <label class="lblReview">Average No. of Cash Deposits(Monthly): *</label>
                            <asp:DropDownList ID="FiListAvgNoOfCashDeposits" CssClass="form-control" runat="server"></asp:DropDownList>
                           <%--  <asp:RequiredFieldValidator ControlToValidate="FiListAvgNoOfCashDeposits" Display="Dynamic" InitialValue="0" ID="RequiredFieldValidator6" runat="server"  ForeColor="Red" Font-Bold="true" ValidationGroup="FiValidationGroup" ErrorMessage="Average No. of Cash Deposits(Monthly) is Required"></asp:RequiredFieldValidator>--%>

                        </div>

                        <div class="form-group" style="display: none">
                            <label class="lblReview">Average No. of No Cash Deposits(Monthly): *</label>
                            <asp:DropDownList ID="FiListAvgNoOfNonCashDeposits" CssClass="form-control" runat="server"></asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ControlToValidate="FiListAvgNoOfNonCashDeposits" Display="Dynamic" InitialValue="0" ID="RequiredFieldValidator7" runat="server"  ForeColor="Red" Font-Bold="true" ValidationGroup="FiValidationGroup" ErrorMessage="Average No. of No Cash Deposits(Monthly) is Required"></asp:RequiredFieldValidator>--%>

                        </div>


                        <div class="form-group">
                            <label class="lblReview">Gross Sale: </label>
                            <asp:TextBox ID="FiGrossSale" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <%-- <asp:RequiredFieldValidator Display="Dynamic" ID="BrRequiredFieldValidatorGrossAle" runat="server" ControlToValidate="FiGrossSale" ForeColor="Red" Font-Bold="true" ValidationGroup="FiValidationGroup" ErrorMessage="Gross Sale is Required"></asp:RequiredFieldValidator>--%>

                        </div>

                        <div class="form-group">
                            <label class="lblReview">Frequency of Gross Sale:</label>
                            <asp:DropDownList ID="FiListFrequencyOfSale" CssClass="form-control" runat="server"></asp:DropDownList>
                             <%-- <asp:RequiredFieldValidator ControlToValidate="FiListFrequencyOfSale" Display="Dynamic" InitialValue="0" ID="RequiredFieldValidator8" runat="server"  ForeColor="Red" Font-Bold="true" ValidationGroup="FiValidationGroup" ErrorMessage="Frequency of Gross Sale is Required"></asp:RequiredFieldValidator>--%>

                        </div>


                        <div class="form-group">
                             <asp:Button ID="btnUpdateFi" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary" ValidationGroup="FiValidationGroup" OnClick="btnUpdateFi_Click" />
                             <asp:Button ID="btnSubmitCiff" Visible="false" ClientIDMode="Static" runat="server" Text="SUBMIT" CssClass="btn btn-primary" OnClick="btnSubmitCif_Click" />
                            <asp:Button ID="FiSubmitButton" ClientIDMode="Static" runat="server" Text="SAVE" CssClass="btn btn-primary" ValidationGroup="FiValidationGroup" OnClick="FiSubmitButton_Click" />
                            <button id="BusFiResetButton" onclick="openModal()" type="button" class="btn btn-primary" value="Reset">Reset</button>

                        </div>

                    </div>
                    <!--sectionf-->
             
                    <div id="sectiong" class="tab-pane fade">


                        <div class="form-group">
                            <label class="lblReview">Name: </label>
                            <asp:TextBox ID="ShName" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="ShName" Display="Dynamic" ID="RequiredFieldValidatorSName" runat="server"  ForeColor="Red" Font-Bold="true" ValidationGroup="ShValidationGroup" ErrorMessage="Name is Required"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group">
                            <label class="lblReview">Address: </label>
                            <asp:TextBox ID="ShAddress" MaxLength="120"  ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                             <asp:RequiredFieldValidator ControlToValidate="ShAddress" Display="Dynamic" ID="RequiredFieldValidatorSAddress" runat="server"  ForeColor="Red" Font-Bold="true" ValidationGroup="ShValidationGroup" ErrorMessage="Address is Required"></asp:RequiredFieldValidator>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanelAccountType" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <div class="form-group">
                                <label class="lblReview">Identity Type: </label>
                                <asp:DropDownList ID="ShIdentityType" CssClass="form-control" runat="server" OnSelectedIndexChanged="ShIdentityType_SelectedIndexChanged"></asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="ShIdentityType" InitialValue="0" Display="Dynamic" ID="RequiredFieldValidatorIType" runat="server"  ForeColor="Red" Font-Bold="true" ValidationGroup="ShValidationGroup" ErrorMessage="Identity Type is Required"></asp:RequiredFieldValidator>
                                 
                                 </div>
	                      
                        

                           <div class="form-group">
                             <label class="lblReview">Identity No: </label>
                             <asp:TextBox ID="ShIdentityNo"  ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                             <asp:RequiredFieldValidator ControlToValidate="ShIdentityNo" Display="Dynamic" ID="RequiredFieldValidatorINO" runat="server"  ForeColor="Red" Font-Bold="true" ValidationGroup="ShValidationGroup" ErrorMessage="Identity No is Required"></asp:RequiredFieldValidator>
                             <asp:RegularExpressionValidator  Enabled="false" Display="Dynamic" ID="RegularExpressionValidatorCnic" runat="server" ErrorMessage="The CNIC must be in correct format e.g xxxxx-xxxxxxx-x" ForeColor="Red" Font-Bold="true" ControlToValidate="ShIdentityNo" ValidationGroup="ShValidationGroup" ValidationExpression="^\d{5}-\d{7}-\d{1}$" ></asp:RegularExpressionValidator>                                  
                         </div>

                          </ContentTemplate>  
                        </asp:UpdatePanel>
                         <div class="form-group">
                            <label>Expiry Date (DD-MM-YYYY):  </label>
                            <div class="input-group date-control">
                                <asp:TextBox ID="ShExpDate"  ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </button>
                                </span>
                            </div>                        
                        </div>
                        <asp:RequiredFieldValidator ControlToValidate="ShExpDate" Display="Dynamic" ID="RequiredFieldValidatorEDate" runat="server"  ForeColor="Red" Font-Bold="true" ValidationGroup="ShValidationGroup" ErrorMessage="Expiry Date is Required"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidatorDate" runat="server" ErrorMessage="Expiry Date must Be in DD-MM-YYYY FORMAT" ForeColor="Red" Font-Bold="true" ControlToValidate="ShExpDate" ValidationGroup="ShValidationGroup" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)[0-9]{2}$"></asp:RegularExpressionValidator>
                         
                        <div class="form-group">
                            <label class="lblReview">Residence Phone: </label>
                            <asp:TextBox ID="ShResPhone"  ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>                           
                         </div>

                         <div class="form-group">
                            <label class="lblReview">Office Phone: </label>
                            <asp:TextBox ID="ShOfPhone"  ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>                           
                         </div>

                          <div class="form-group">
                            <label class="lblReview">Mobile Phone: </label>
                            <asp:TextBox ID="ShMobNo"  ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>                           
                         </div>
                          <div class="form-group">
                            <label class="lblReview">Fax No: </label>
                             <asp:TextBox ID="ShFaxNo"  ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>                           
                          </div>
                         <div class="form-group">
                            <label class="lblReview">Email: </label>
                             <asp:TextBox ID="ShEmail"  ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>                           
                          </div>
                         <div class="form-group">
                            <label class="lblReview">No Of Shares Held: </label>
                             <asp:TextBox ID="ShNoSharesHeld"  ClientIDMode="Static" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>                           
                          </div>
                         <div class="form-group">
                            <label class="lblReview">Amount of Shares Held: </label>
                             <asp:TextBox ID="ShAmountShareHeld"  ClientIDMode="Static" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>                           
                          </div>
                         <div class="form-group">
                            <label class="lblReview">Share Holder Percentage: </label>
                             <asp:TextBox ID="ShPercentage"   ClientIDMode="Static" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>                           
                             <asp:RegularExpressionValidator   Display="Dynamic" ID="RegularExpressionValidatorSHPercen" runat="server" ErrorMessage="The Percentage must be in range of 1 to 100" ForeColor="Red" Font-Bold="true" ControlToValidate="ShPercentage" ValidationGroup="ShValidationGroup" ValidationExpression="^[1-9][0-9]?$|^100$" ></asp:RegularExpressionValidator>                                  
                         </div>
                         <div class="form-group">
                            <label class="lblReview">Net Worth: </label>
                             <asp:TextBox ID="ShNetWorth"   ClientIDMode="Static" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>                           
                         </div>
                        <div class="form-group">
                            <label class="lblReview">Status Of Director: </label>
                            <asp:DropDownList ID="ShDirectorStatus" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator InitialValue="0" ControlToValidate="ShDirectorStatus" Display="Dynamic" ID="RequiredFieldValidatorDStatus" runat="server"  ForeColor="Red" Font-Bold="true" ValidationGroup="ShValidationGroup" ErrorMessage="Director Status is Required"></asp:RequiredFieldValidator>
                         </div>

                        
                        <div class="row" id="gridSH">
                            <div class="col-lg-12">

                                <asp:UpdatePanel ID="UpdatePanelGridSH" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                     <asp:GridView class="table" ShowHeaderWhenEmpty="true"   ID="GridViewSH" runat="server"  AutoGenerateColumns="false" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="NAME">
                                            <ItemTemplate>
                                                 <asp:Label ID="NAME" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="IDENTITY TYPE">
                                            <ItemTemplate>
                                                <asp:Label ID="IDENTITY_TYPE" runat="server" Text='<%# Bind("IDENTITY_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="IDENTITY NO">
                                            <ItemTemplate>
                                                <asp:Label ID="IDENTITY_NO" runat="server" Text='<%# Bind("IDENTITY_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="EXPIRY DATE">
                                            <ItemTemplate>
                                                <asp:Label ID="IDENTITY_EXPIRY_DATE" runat="server" Text='<%# Bind("IDENTITY_EXPIRY_DATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                     

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button runat="server" ID="btnDeleteSH" Text="DELETE" OnClick="btnDeleteSH_Click"></asp:Button>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>

                                </asp:GridView> 
                                    </ContentTemplate>
                                    <Triggers>
	                                    <asp:AsyncPostBackTrigger ControlID="btnGridAddSH" EventName="Click" />
                                    </Triggers>
                                 </asp:UpdatePanel>
                                        
                            </div>

                            <asp:Button ID="btnGridAddSH"  ValidationGroup="ShValidationGroup" CssClass="btn btn-primary" Style="float: right" OnClick="btnGridAddSH_Click"  runat="server" Text="ADD" />
                             <%--<asp:CustomValidator Display="Dynamic" Font-Bold="true" ForeColor="Red" ID="CustomValidatorMoreShareHolder" ValidationGroup="SH" runat="server" ErrorMessage="Atleast One Share Holder is Required"  ClientValidationFunction="CheckShareHolder" EnableClientScript="true" OnServerValidate="CustomValidatorMoreShareHolder_ServerValidate" ></asp:CustomValidator>--%>
                            </div>


                        <div class="form-group">
                            <asp:Button ID="btnUpdateSh" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary" ValidationGroup="SH" OnClick="btnUpdateSh_Click" />
                             <asp:Button ID="btnSubmitCifg" Visible="false" ClientIDMode="Static" runat="server" Text="SUBMIT" CssClass="btn btn-primary" OnClick="btnSubmitCif_Click" />
                            <asp:Button ID="ShSubmitButton" ClientIDMode="Static" runat="server" Text="SAVE" CssClass="btn btn-primary" ValidationGroup="SH" OnClick="ShSubmitButton_Click" />
                            <button id="BusShResetButton" onclick="openModal()" type="button" class="btn btn-primary" value="Reset">Reset</button>

                        </div>


                    </div>


                <div id="sectionh" class="tab-pane fade">
                     <asp:UpdatePanel ID="UpdatePanelFatca" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                           
                            <div class="form-group">
                                <label  class="lblReview">Country of Incorporation:</label>
                                <asp:DropDownList ID="PiListCI" Enabled="False" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label  class="lblReview">Country of Business/Operations:</label>
                                <asp:DropDownList ID="PiListCBO" Enabled="False" CssClass="form-control" runat="server"></asp:DropDownList>
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
                                <label  class="lblReview">Transfer of Funds From/To USA: *</label>
                                <asp:DropDownList ID="PiListTransferOfFundsUSA" CssClass="form-control" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorFundFAtca" InitialValue="0" runat="server" ControlToValidate="PiListTransferOfFundsUSA" ForeColor="Red" Font-Bold="true" ValidationGroup="IndiFAtca" ErrorMessage="Transfer of Funds From/To USA is Required"></asp:RequiredFieldValidator>
                             </div>
                            <div class="form-group">
                                <label  class="lblReview">FATCA Classification: *</label>
                                <asp:DropDownList ID="PiListFatcaClass" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="PiListFatcaClass_SelectedIndexChanged"></asp:DropDownList>
                                 <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorFClassi" InitialValue="0" runat="server" ControlToValidate="PiListFatcaClass" ForeColor="Red" Font-Bold="true" ValidationGroup="IndiFAtca" ErrorMessage="FATCA Classification is Required"></asp:RequiredFieldValidator>
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

                            <div class="form-group">
                                <asp:Button ID="PiSubmitButton" ValidationGroup="IndiFAtca" runat="server" Text="SAVE" CssClass="btn btn-primary" OnClick="PiSubmitButton_Click" />
                                <button id="InResetFatca" onclick="openModal()" type="button" class="btn btn-primary" value="Reset">Reset</button>
                                <asp:Button ID="btnPiUpdate" ValidationGroup="IndiFAtca" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary"  OnClick="btnPiUpdate_Click" />
                                <asp:Button ID="Button1" Visible="false"  ClientIDMode="Static" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmitCif_Click" />
                            </div>

	                         </ContentTemplate>                           
                        </asp:UpdatePanel>

                </div>


                </div>

            </div>
        </div>

  <%--  </div>--%>
     <Rev:Review ID="rev" runat="server" Visible="false" />

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

         function CheckShareHolder(source, args) {
             var Grid1 = document.getElementById("<%=GridViewSH.ClientID%>");
             if (Grid1 == null) {
                 args.IsValid = false;
             }
             else if (Grid1.rows.length < 2) {
                 args.IsValid = false;
             }
             else {
                 args.IsValid = true;
             }
         }

         function CheckBussCountry(source, args) {
             var chkListModules = document.getElementById('<%= BdListBusinessInCountry.ClientID %>');
             var chkListinputs = chkListModules.getElementsByTagName("input");
             for (var i = 0; i < chkListinputs.length; i++) {
                 if (chkListinputs[i].checked) {
                     args.IsValid = true;
                     return;
                 }
             }
             args.IsValid = false;
         }

         function CheckBussCities(source, args) {
             var chkListModules = document.getElementById('<%= BdListBusinessInCities.ClientID %>');
             var chkListinputs = chkListModules.getElementsByTagName("input");
             for (var i = 0; i < chkListinputs.length; i++) {
                 if (chkListinputs[i].checked) {
                     args.IsValid = true;
                     return;
                 }
             }
             args.IsValid = false;
         }

         function AnyOneReg(source, args) {

             args.IsValid = false;

             if (document.getElementById('<% =BiNtn.ClientID %>').value.length > 0) {
                 args.IsValid = true;
             }
             if (document.getElementById('<% =BiSalesTax.ClientID %>').value.length > 0) {
                 args.IsValid = true;
             }
             if (document.getElementById('<% =BiRegistrationNo.ClientID %>').value.length > 0) {
                 args.IsValid = true;
             }
         }

         function AnyOneContact(source, args) {

             args.IsValid = false;

             if (document.getElementById('<% =CiOfficeNo.ClientID %>').value.length > 0) {
                 args.IsValid = true;
             }
             if (document.getElementById('<% =CiMobileNo.ClientID %>').value.length > 0) {
                 args.IsValid = true;
             }
             if (document.getElementById('<% =CiFaxNo.ClientID %>').value.length > 0) {
                 args.IsValid = true;
             }
         }
         function AnyOneContactHead(source, args) {

             args.IsValid = false;

             if (document.getElementById('<% =HoOfficeNo.ClientID %>').value.length > 0) {
                 args.IsValid = true;
             }
             if (document.getElementById('<% =HoMobileNo.ClientID %>').value.length > 0) {
                 args.IsValid = true;
             }
             if (document.getElementById('<% =HoFaxNo.ClientID %>').value.length > 0) {
                 args.IsValid = true;
             }
         }

</script>

</asp:Content>