<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.Master" AutoEventWireup="true" CodeBehind="UpdateOffice.aspx.cs" Inherits="CAOP.UpdateOffice" %>
<%@ Register Src="~/UserControls/ReviewControl.ascx" TagName="Review" TagPrefix="Rev" %>


<asp:Content ID="Body" ContentPlaceHolderID="Body" runat="server">  
    <div class="content">
        <asp:ScriptManager  runat="server"></asp:ScriptManager>

                 <div id="alerts"></div>

         <!-- Modal -->
    <div class="modal fade" id="myModal" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">                                  
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

        <hr />
        <div class="row">
             <div class="col-md-3">
                 <ul class="nav nav-tabs-justified nav-menu">
                    <li class="active"><a data-toggle="tab" href="#sectiona">OFFICE CIF</a></li>    
                 </ul>                    
            </div>

             <div class="col-md-6">
            <div class="tab-content">                
                <div id="sectiona" class="tab-pane fade in active">
                         <h3>Office Form</h3>
                    <div class="form-group">
                        <label class="lblReview">CIF Type:</label>    
                        <asp:DropDownList ID="OffListCifType" Enabled="false"  CssClass="form-control" runat="server"></asp:DropDownList>                                                     
                    </div>
                    <div class="form-group">
                        <label class="lblReview">Office Name:</label>    
                        <asp:TextBox ID="OfftxtName" CssClass="form-control"  runat="server"></asp:TextBox>
                    </div>
                    <h3>Who Authorized</h3>

                      <div class="form-group">
                        <label class="lblReview">Name:</label>    
                        <asp:TextBox ID="Offtxt" CssClass="form-control"  runat="server"></asp:TextBox>
                      </div>
                       <div class="form-group">
                        <label class="lblReview">Designation:</label>    
                        <asp:TextBox ID="OffDesignation" CssClass="form-control" MaxLength="20"  runat="server"></asp:TextBox>
                      </div>

                      <h3>Permanent Address</h3>
                        <div class="form-group">
                            <label  class="lblReview">Country: *</label>
                            <asp:DropDownList ID="OffListCountry" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator Display="Dynamic" InitialValue="0" ID="RequiredFieldValidatorListCountry" runat="server" ControlToValidate="OffListCountry" ForeColor="Red" Font-Bold="true" ValidationGroup="BiValidationGroup" ErrorMessage="Country is Required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">City: *</label>
                            <asp:DropDownList ID="OffListCity" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" InitialValue="0" ID="RequiredFieldValidator3" runat="server" ControlToValidate="OffListCity" ForeColor="Red" Font-Bold="true" ValidationGroup="BiValidationGroup" ErrorMessage="City is Required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Province: *</label>
                            <asp:DropDownList ID="OffListProvince" CssClass="form-control" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorGovProvince" Enabled="true" runat="server" InitialValue="0" ControlToValidate="OffListProvince" ForeColor="Red" Font-Bold="true" ValidationGroup="BiValidationGroup" ErrorMessage="Province is Required"></asp:RequiredFieldValidator>
                        </div>
                         <div class="form-group">
                            <label  class="lblReview">Address Line 1: *</label>
                            <asp:TextBox ID="OffTxtBuilding" CssClass="form-control" runat="server" MaxLength="40"></asp:TextBox> 
                             <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorTxtBuildingt" runat="server" ControlToValidate="OffTxtBuilding" ForeColor="Red" Font-Bold="true" ValidationGroup="BiValidationGroup" ErrorMessage="Address Line 1 is Required"></asp:RequiredFieldValidator>                            
                        </div>
                         <div class="form-group">
                            <label  class="lblReview">Address Line 2:</label>
                            <asp:TextBox ID="OffTxtFloor" CssClass="form-control" runat="server" MaxLength="40"></asp:TextBox>                             
                        </div>
                         <div class="form-group">
                            <label  class="lblReview">Address Line 3:</label>
                            <asp:TextBox ID="OffTxtStreet" CssClass="form-control" runat="server" MaxLength="40"></asp:TextBox>                            
                        </div>                                               
                         <div class="form-group">
                            <label  class="lblReview">District:</label>
                            <asp:TextBox ID="OffTxtDistrict" CssClass="form-control" runat="server"></asp:TextBox>                             
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">PO Box:</label>
                            <asp:TextBox ID="OffPoBox" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>                        
                        <div class="form-group">
                            <label  class="lblReview">Postal Code:</label>
                            <asp:TextBox ID="OffPostalCode" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                      <h3>Present Address</h3>
                        <div class="form-group">
                            <label  class="lblReview">Country: *</label>
                            <asp:DropDownList ID="OffListCountryPre" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator Display="Dynamic" InitialValue="0" ID="RequiredFieldValidator1" runat="server" ControlToValidate="OffListCountryPre" ForeColor="Red" Font-Bold="true" ValidationGroup="BiValidationGroup" ErrorMessage="Country is Required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">City: *</label>
                            <asp:DropDownList ID="OffListCityPre" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator Display="Dynamic" InitialValue="0" ID="RequiredFieldValidator2" runat="server" ControlToValidate="OffListCityPre" ForeColor="Red" Font-Bold="true" ValidationGroup="BiValidationGroup" ErrorMessage="City is Required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">Province: *</label>
                            <asp:DropDownList ID="OffListProvincePre" CssClass="form-control" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator4" Enabled="true" runat="server" InitialValue="0" ControlToValidate="OffListProvincePre" ForeColor="Red" Font-Bold="true" ValidationGroup="BiValidationGroup" ErrorMessage="Province is Required"></asp:RequiredFieldValidator>
                        </div>
                         <div class="form-group">
                            <label  class="lblReview">Address Line 1: *</label>
                            <asp:TextBox ID="OffTxtBuildingPre" CssClass="form-control" runat="server" MaxLength="40"></asp:TextBox> 
                             <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator5" runat="server" ControlToValidate="OffTxtBuildingPre" ForeColor="Red" Font-Bold="true" ValidationGroup="BiValidationGroup" ErrorMessage="Address Line 1 is Required"></asp:RequiredFieldValidator>                            
                        </div>
                         <div class="form-group">
                            <label  class="lblReview">Address Line 2:</label>
                            <asp:TextBox ID="OffTxtFloorPre" CssClass="form-control" runat="server" MaxLength="40"></asp:TextBox>                             
                        </div>
                         <div class="form-group">
                            <label  class="lblReview">Address Line 3:</label>
                            <asp:TextBox ID="OffTxtStreetPre" CssClass="form-control" runat="server" MaxLength="40"></asp:TextBox>                            
                        </div>                                               
                         <div class="form-group">
                            <label  class="lblReview">District:</label>
                            <asp:TextBox ID="OffTxtDistrictPre" CssClass="form-control" runat="server"></asp:TextBox>                             
                        </div>
                        <div class="form-group">
                            <label  class="lblReview">PO Box:</label>
                            <asp:TextBox ID="OffPoBoxPre" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>                        
                        <div class="form-group">
                            <label  class="lblReview">Postal Code:</label>
                            <asp:TextBox ID="OffPostalCodePre" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>


                        <h3>Contact Numbers</h3>
                        <div class="form-group">
                            <label  class="lblReview">Tel (Off):</label>
                            <asp:TextBox ID="OfficeNo" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                              <asp:CustomValidator ValidateEmptyText="true" ControlToValidate="OfficeNo" Display="Dynamic" ClientValidationFunction="AnyOneContact" ForeColor="Red" Font-Bold="true" ValidationGroup="BiValidationGroup" ErrorMessage="Any One Contact is Required" ID="CustomValidator2" runat="server" ></asp:CustomValidator>
                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Mobile No:</label>
                            <asp:TextBox ID="MobileNo" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Fax No:</label>
                            <asp:TextBox ID="FaxNo" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                     <div class="form-group">
                            <asp:Button ID="btnUpdateOfc" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary" ValidationGroup="BiValidationGroup" OnClick="btnUpdateOfc_Click"  />
                            <asp:Button ID="btnSubmitOfc" Visible="false" ClientIDMode="Static" runat="server" Text="SUBMIT" CssClass="btn btn-primary"  OnClick="btnSubmitOfc_Click" />
                            <asp:Button ID="btnSubmitSaveOfc" ClientIDMode="Static" runat="server" Text="SAVE" CssClass="btn btn-primary" ValidationGroup="BiValidationGroup" OnClick="btnSubmitSaveOfc_Click" />
                         
                    </div>

                </div>
            </div>
         </div>


        </div>

     
</div>
     <Rev:Review ID="rev" runat="server" Visible="false" />

        <script>
            function AnyOneContact(source, args) {

                args.IsValid = false;

                if (document.getElementById('<% =OfficeNo.ClientID %>').value.length > 0) {
                args.IsValid = true;
            }
            if (document.getElementById('<% =MobileNo.ClientID %>').value.length > 0) {
                args.IsValid = true;
            }
            if (document.getElementById('<% =FaxNo.ClientID %>').value.length > 0) {
                args.IsValid = true;
            }
        }
    </script>
</asp:Content>