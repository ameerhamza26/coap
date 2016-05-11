<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" MasterPageFile="~/MasterPages/Default.Master" Inherits="CAOP.UserForms.AddUser" %>

<asp:Content ID="Head" ContentPlaceHolderID="head" runat="server">

    </asp:Content>



<asp:Content ID="Body" ContentPlaceHolderID="Body" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
       <%-- <div class="row">
            <div class="col-md-3">
                <ul id="List1" class="nav nav-tabs-justified nav-menu">
                     <li id="accountNature"><a id="InaccountNatureAnchor" data-toggle="tab" href="#sectiona">A/C Nature & Currency</a></li>                 

                </ul>
            </div>


            <div class="col-md-6">
        <div class="tab-content">
            <div id="sectiona" class="tab-pane fade in active">--%>
                <h3 style="margin-top: 100px; margin-left: 30px">USER INFORMATION</h3>
                        <asp:UpdatePanel ID="UpdatePanelCNICValidation" runat="server" UpdateMode="Always">
                        <ContentTemplate>

                            <div class="row" style="margin-left: 20px; margin-bottom: 10px">
            <div class="col-md-4">
                <div class="form-group">
                    <label >User Name: *</label>
                    <asp:TextBox ID="txtUName" Enabled="false" ClientIDMode="Static" CssClass="form-control" runat="server" ></asp:TextBox>
                      <asp:RequiredFieldValidator Display="Dynamic"  ID="RequiredFieldValidatorUName" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="CREATE" ControlToValidate="txtUName" ErrorMessage="User Name is Required"></asp:RequiredFieldValidator>
                </div>
                  <div class="form-group">
                    <label >Name: *</label>
                    <asp:TextBox ID="txtName" ClientIDMode="Static" CssClass="form-control" runat="server" ></asp:TextBox>
                    <asp:RequiredFieldValidator Display="Dynamic"  ID="RequiredFieldValidatorName" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="CREATE" ControlToValidate="txtName" ErrorMessage="Name is Required"></asp:RequiredFieldValidator>
                </div>
                 <div class="form-group">
                    <label >Email: *</label>
                    <asp:TextBox ID="txtemail" TextMode="Email" ClientIDMode="Static" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtemail_TextChanged" ></asp:TextBox>
                      <asp:RequiredFieldValidator Display="Dynamic"  ID="RequiredFieldValidatorEmail" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="CREATE" ControlToValidate="txtemail" ErrorMessage="Email is Required"></asp:RequiredFieldValidator>
                     <asp:CustomValidator  ID="CustomValidatorEmailUnique" Display="Dynamic" ForeColor="Red" Font-Bold="true" runat="server"  ErrorMessage="Email Should be Unique" ValidationGroup="CREATE" OnServerValidate="CustomValidatorEmailUnique_ServerValidate" ></asp:CustomValidator>
                     <asp:CustomValidator  ID="CustomValidatorValid" Display="Dynamic" ForeColor="Red" Font-Bold="true" runat="server"  ErrorMessage="Email Should be Valid" ValidationGroup="CREATE" OnServerValidate="CustomValidatorValid_ServerValidate" ></asp:CustomValidator>
                </div>
                  <div class="form-group">
                    <label >Designation:</label>
                    <asp:TextBox ID="txtdesig"  ClientIDMode="Static" CssClass="form-control" runat="server" ></asp:TextBox>
                </div>
                <div class="form-group">
                    <label >Sap ID: *</label>
                    <asp:TextBox ID="txtSap"  ClientIDMode="Static" CssClass="form-control" runat="server" ></asp:TextBox>
                    <asp:RequiredFieldValidator Display="Dynamic"  ID="RequiredFieldValidatorSapId" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="CREATE" ControlToValidate="txtSap" ErrorMessage="Sap ID is Required"></asp:RequiredFieldValidator>
                    <asp:CustomValidator  ID="CustomValidatorSapIdUnique" Display="Dynamic" ForeColor="Red" Font-Bold="true" runat="server"  ErrorMessage="Sap ID Should be Unique" ValidationGroup="CREATE" OnServerValidate="CustomValidatorSapIdUnique_ServerValidate" ></asp:CustomValidator>
                </div>
                 <div class="form-group">
                    <label >Branch Code: *</label>
                    <asp:TextBox ID="txtBranchCode"  ClientIDMode="Static" CssClass="form-control" runat="server" ></asp:TextBox>
                     <asp:RequiredFieldValidator  Display="Dynamic"  ID="RequiredFieldValidatorBranchCode" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="CREATE" ControlToValidate="txtBranchCode" ErrorMessage="Branch Code is Required"></asp:RequiredFieldValidator>
                      <asp:CustomValidator  ID="CustomValidatorBranchCode" Display="Dynamic" ForeColor="Red" Font-Bold="true" runat="server"  ErrorMessage="Branch Code  is Wrong" ValidationGroup="CREATE" OnServerValidate="CustomValidatorBranchCode_ServerValidate" ></asp:CustomValidator>
                </div>
                 <div class="form-group">
                    <label >User Role: *</label>
                    <asp:DropDownList ID="ddlUserRole" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                     <asp:RequiredFieldValidator InitialValue="0"  Display="Dynamic"  ID="RequiredFieldValidatorRole"  runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="CREATE" ControlToValidate="ddlUserRole" ErrorMessage="User Role is Required"></asp:RequiredFieldValidator>
                </div>
                 <div class="form-group">
                    <label >Regions: *</label>
                    <asp:DropDownList Enabled="false" ID="ddlRegions" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator Enabled="false"  Display="Dynamic"  ID="RequiredFieldValidatorRegions" InitialValue="0" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="CREATE" ControlToValidate="ddlRegions" ErrorMessage="Region is Required"></asp:RequiredFieldValidator>
                </div>

                <div class="form-group">
                    <label  class="lblReview">Region User: </label>
                    <br />
                    <asp:CheckBox runat="server" ID="chkRegion" AutoPostBack="true" OnCheckedChanged="chkRegion_CheckedChanged" Text="YES" />                                   
                </div>
                 <asp:Button ID="btnCreate" runat="server" Text="SAVE" ValidationGroup="CREATE" CssClass="btn btn-primary" OnClick="btnCreate_Click" />
                </div>
                                </div>

    </ContentTemplate>
    </asp:UpdatePanel>
          <%-- </div>
        </div>
    </div>--%>









         </div>

    
    
</asp:Content>