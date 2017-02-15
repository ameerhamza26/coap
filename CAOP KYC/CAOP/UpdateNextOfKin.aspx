<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.Master" CodeBehind="UpdateNextOfKin.aspx.cs" Inherits="CAOP.UpdateNextOfKin" %>
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



        <h3>Next Of Kin</h3>
        <hr/>
        <div class="row">
            <div class="col-md-3">
                 <ul class="nav nav-tabs-justified nav-menu">
                    <li class="active"><a data-toggle="tab" href="#sectiona">Basic Information</a></li>    
                 </ul>
                    
            </div>

             <div class="col-md-6">
                <div class="tab-content">
                
                    <div id="sectiona" class="tab-pane fade in active">
                          <h3>Individual CIF Form (NEXT OF KIN)</h3>
                            <div class="form-group">
                                <label class="lblReview">Individual CIF Type:</label>    
                                 <asp:DropDownList ID="NKListIndividualCIF" OnSelectedIndexChanged="NKListIndividualCIF_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                                                        
                            </div>

                          <div class="form-group">
                                <label class="lblReview">CNIC: *</label>                            
                                <asp:TextBox ID="NKCnic" CssClass="form-control"  runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="Dynamic" ID="NKRequiredFieldValidatorCnic" runat="server" ControlToValidate="NKCnic" ForeColor="Red" Font-Bold="true" ValidationGroup="NKValidationGroup"  ErrorMessage="CNIC is Required"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator   Display="Dynamic" ID="RegularExpressionValidatorCnic" runat="server" ErrorMessage="The CNIC must be in correct format e.g xxxxx-xxxxxxx-x" ForeColor="Red" Font-Bold="true" ControlToValidate="NKCnic" ValidationGroup="NKValidationGroup" ValidationExpression="^\d{5}-\d{7}-\d{1}$" ></asp:RegularExpressionValidator>
                               <%-- <asp:CustomValidator ID="CustomValidatorCnic" Display="Dynamic" ForeColor="Red" Font-Bold="true" runat="server" ErrorMessage="CNIC Should be Unique" ValidationGroup="NKValidationGroup" OnServerValidate="CustomValidatorCnic_ServerValidate" ></asp:CustomValidator>--%>
                               
                          </div>

                          <div class="form-group">
                                <label class="lblReview">Title (Name): *</label>                            
                               <asp:DropDownList ID="NKListTitle" CssClass="form-control" runat="server"></asp:DropDownList>                                 
                              <asp:RequiredFieldValidator Display="Dynamic" InitialValue="0" ID="NKRequiredFieldValidatorListTitle" runat="server" ControlToValidate="NKListTitle" ForeColor="Red" Font-Bold="true" ValidationGroup="NKValidationGroup"  ErrorMessage="Title is Required"></asp:RequiredFieldValidator>
                               
                          </div>
                         
                        <div class="form-group">
                            <label  class="lblReview">First Name: *</label>
                            <asp:TextBox ID="NktxtFirstName" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorFirstName" ValidationGroup="NKValidationGroup" runat="server" ControlToValidate="NktxtFirstName" ForeColor="Red" Font-Bold="true" ErrorMessage="First Name is Required"></asp:RequiredFieldValidator>
                        </div>
                         <div class="form-group">
                            <label  class="lblReview">Middle Name: </label>
                            <asp:TextBox ID="NktxtMiddleName" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>                          
                        </div>
                          <div class="form-group">
                            <label  class="lblReview">Last Name: </label>
                            <asp:TextBox ID="NktxtLastName" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>                          
                        </div>

                         <div class="form-group">
                                <label class="lblReview">Resident / Non-Resident: *</label>                            
                               <asp:DropDownList ID="NKListResident" CssClass="form-control" runat="server"></asp:DropDownList>                               
                              <asp:RequiredFieldValidator Display="Dynamic" InitialValue="0" ID="NKRequiredFieldValidatorListResident" runat="server" ControlToValidate="NKListResident" ForeColor="Red" Font-Bold="true" ValidationGroup="NKValidationGroup"  ErrorMessage="Residence is Required"></asp:RequiredFieldValidator>
                               
                          </div>

                          <div class="form-group">
                                <label class="lblReview">Nationality: *</label>                            
                               <asp:DropDownList ID="NKListNationality" CssClass="form-control" runat="server"></asp:DropDownList>                                 
                              <asp:RequiredFieldValidator Display="Dynamic" InitialValue="0" ID="NKRequiredFieldValidatorListNationality" runat="server" ControlToValidate="NKListNationality" ForeColor="Red" Font-Bold="true" ValidationGroup="NKValidationGroup"  ErrorMessage="Nationality is Required"></asp:RequiredFieldValidator>
                               
                          </div>
                            
                       
                        <br />

                        <h3>Any Other Identity</h3>
                          <div class="form-group">
                                <label class="lblReview">Identity Type:</label>                            
                               <asp:DropDownList ID="NKListIdentityType" CssClass="form-control" runat="server"></asp:DropDownList>
                               
                          </div>
                         
                          <div class="form-group">
                                <label class="lblReview">Identity No:</label>                            
                                <asp:TextBox ID="NKIdentityNo" CssClass="form-control"  runat="server"></asp:TextBox>
                               
                          </div>

                          <div class="form-group">
                                <label class="lblReview">Country of Issue:</label>                            
                               <asp:DropDownList ID="NKListCountryIssue" CssClass="form-control" runat="server"></asp:DropDownList>
                               
                          </div>

                        <div class="form-group">
                                <label class="lblReview">Issue Date (DD-MM-YYYY):</label>
                                <div class="input-group date-control">                                 
                                    <asp:TextBox ID="NKIssueDate" runat="server"  CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-btn">
                                      <button class="btn btn-default" type="button">
                                          <span class="glyphicon glyphicon-calendar" ></span>
                                      </button>
                                    </span>
                                </div>
                            </div>
                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidatorIssueDate" runat="server" ErrorMessage="Issue Date must Be in DD-MM-YYYY FORMAT" ForeColor="Red" Font-Bold="true" ControlToValidate="NKIssueDate" ValidationGroup="NKValidationGroup" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)[0-9]{2}$"></asp:RegularExpressionValidator>
                        
                          <div class="form-group">
                                <label class="lblReview">Place of Issue:</label>                            
                                <asp:TextBox ID="NKPlaceOfIssue" CssClass="form-control"  runat="server"></asp:TextBox>
                               
                          </div>




                        <div class="form-group">
                                <label class="lblReview">Expiry Date (DD-MM-YYYY):</label>
                                <div class="input-group date-control">                                 
                                    <asp:TextBox ID="NKExpDate" runat="server"  CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-btn">
                                      <button class="btn btn-default" type="button">
                                          <span class="glyphicon glyphicon-calendar" ></span>
                                      </button>
                                    </span>
                                </div>
                            </div>
                         <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidatorExpiry" runat="server" ErrorMessage="Expiry Date must Be in DD-MM-YYYY FORMAT" ForeColor="Red" Font-Bold="true" ControlToValidate="NKExpDate" ValidationGroup="NKValidationGroup" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)[0-9]{2}$"></asp:RegularExpressionValidator>
                        <br />

                        <h3>Permanent Address Information</h3>

                        <div class="form-group">
                                <label class="lblReview">Country: *</label>                            
                               <asp:DropDownList ID="NKListCountry" CssClass="form-control" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator InitialValue="0" ID="NKRequiredFieldValidatorListCountry" runat="server" ControlToValidate="NKListCountry" ForeColor="Red" Font-Bold="true" ValidationGroup="NKValidationGroup"  ErrorMessage="Country is Required"></asp:RequiredFieldValidator>
                                 
                          </div>

                         <div class="form-group">
                                <label class="lblReview">City:</label>                            
                               <asp:DropDownList ID="NKListCity" CssClass="form-control" runat="server"></asp:DropDownList>
                               
                          </div>

                         <div class="form-group">
                                <label class="lblReview">House/Building/Suite: *</label>                            
                               <asp:TextBox ID="NkTxtBuilding" CssClass="form-control"  runat="server"></asp:TextBox>
                                 <asp:RequiredFieldValidator  ID="RequiredFieldValidatorBuilding" runat="server" ControlToValidate="NkTxtBuilding" ForeColor="Red" Font-Bold="true" ValidationGroup="NKValidationGroup"  ErrorMessage="House/Building/Suite is Required"></asp:RequiredFieldValidator>
                          </div>

                        <div class="form-group">
                                <label class="lblReview">Floor:</label>                            
                                <asp:TextBox ID="NktxtFloor" CssClass="form-control"  runat="server"></asp:TextBox>
                               
                          </div>

                          <div class="form-group">
                                <label class="lblReview">Street:</label>                            
                                <asp:TextBox ID="NkTxtStreet" CssClass="form-control"  runat="server"></asp:TextBox>
                               
                          </div>

                        <div class="form-group">
                                <label class="lblReview">District:</label>                            
                                <asp:TextBox ID="NkTxtDistrict" CssClass="form-control"  runat="server"></asp:TextBox>
                               
                          </div>

                         <div class="form-group">
                                <label class="lblReview">PO Box:</label>                            
                                <asp:TextBox ID="NKPoBox" CssClass="form-control"  runat="server"></asp:TextBox>
                               
                          </div>                          

                           <div class="form-group">
                                <label class="lblReview">Postal Code:</label>                            
                                <asp:TextBox ID="NKPostalCode" CssClass="form-control"  runat="server"></asp:TextBox>
                               
                          </div>                         

                        <br/>

                        <h3>Contact Numbers</h3>
                          <div class="form-group">
                                <label class="lblReview">Contact No. Office:</label>                            
                                <asp:TextBox ID="NKContactNoOffice" CssClass="form-control"  runat="server"></asp:TextBox>
                               <asp:CustomValidator ErrorMessage="Any one contact is required" ID="CustomValidatorAnyContact" ValidationGroup="NKValidationGroup" runat="server" Display="Dynamic" ForeColor="Red" Font-Bold="true" OnServerValidate="CustomValidatorAnyContact_ServerValidate" EnableClientScript="true" ClientValidationFunction="doCustomValidateContact"></asp:CustomValidator>
                          </div>

                        <div class="form-group">
                                <label>Contact No. Residence:</label>                            
                                <asp:TextBox ID="NKContactNoResidence" CssClass="form-control"  runat="server"></asp:TextBox>
                               
                          </div>

                        <div class="form-group">
                                <label class="lblReview">Mobile No:</label>                            
                                <asp:TextBox ID="NKMobileNo" CssClass="form-control"  runat="server"></asp:TextBox>
                               
                          </div>

                        <div class="form-group">
                                <label class="lblReview">Fax No:</label>                            
                                <asp:TextBox ID="NKFaxNo" CssClass="form-control"  runat="server"></asp:TextBox>
                               
                          </div>

                        <div class="form-group">
                                <label class="lblReview">Email:</label>                            
                                <asp:TextBox ID="NKEmail" CssClass="form-control"  runat="server"></asp:TextBox>
                               
                          </div>

                        
                            <div class="form-group">
                                 <asp:Button ID="btnUpdateNK" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary" ValidationGroup="NKValidationGroup" OnClick="btnUpdateNK_Click" />
                                 <asp:Button ID="btnSubmitCif" Visible="false" ClientIDMode="Static" runat="server" Text="SUBMIT" CssClass="btn btn-primary"  OnClick="btnSubmitCif_Click" />
                                <asp:Button ID="btnSubmitNKN" ClientIDMode="Static" runat="server" Text="SUBMIT" CssClass="btn btn-primary" ValidationGroup="NKValidationGroup" OnClick="btnSubmitNKN_Click" />
                               <button id="InResetBasicInfo" style="display: none" onclick="openModal()" type="button" class="btn btn-primary" value="Reset">Reset</button>


                            </div>
                            
                    </div>
                        
                </div>    
             </div>
            
        </div>    
    </div>    
    <Rev:Review ID="rev" runat="server" Visible="false" />


    <script>
        function doCustomValidateContact(source, args) {

            args.IsValid = false;

            if (document.getElementById('<% =NKContactNoOffice.ClientID %>').value.length > 0) {
                args.IsValid = true;
            }
            if (document.getElementById('<% =NKContactNoResidence.ClientID %>').value.length > 0) {
                 args.IsValid = true;
             }
             if (document.getElementById('<% =NKMobileNo.ClientID %>').value.length > 0) {
                 args.IsValid = true;
             }
         }
    </script>
</asp:Content>