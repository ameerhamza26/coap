<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.Master" CodeBehind="ChangeUserPass.aspx.cs" Inherits="CAOP.UserForms.ChangeUserPass" %>

<asp:Content ID="Body" ContentPlaceHolderID="Body" runat="server">
 <div class="content">

        <h3>Change Password</h3>
        <div class="row">
         <div class="col-md-4">
       
          <div class="form-group">
            <label  class="lblReview">New Password: *</label>
            <asp:TextBox ID="txtNewPass" TextMode="Password"  ClientIDMode="Static" CssClass="form-control"  runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorNewPass" runat="server" ControlToValidate="txtNewPass" ErrorMessage="New Password is Required" ForeColor="Red" Font-Bold="true" ValidationGroup="PASS" />
              <asp:RegularExpressionValidator Display="Dynamic" ID="Regex4" runat="server" ControlToValidate="txtNewPass"
                ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}"
            ErrorMessage="Password must contain: Minimum 8 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character" ForeColor="Red"  Font-Bold="true" ValidationGroup="PASS" />
       </div>
       <div class="form-group">
            <label  class="lblReview">Confirm Password: *</label>
            <asp:TextBox ID="txtPassConfirm" TextMode="Password"  ClientIDMode="Static" CssClass="form-control"   runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorConfirmPass" runat="server" ControlToValidate="txtPassConfirm" ErrorMessage="Confirm Password is Required" ForeColor="Red" Font-Bold="true" ValidationGroup="PASS" />
           <asp:CompareValidator Display="Dynamic" ID="CompareValidatorPass" ControlToValidate="txtPassConfirm"  ControlToCompare="txtNewPass" runat="server" ErrorMessage="Password Must Match" ForeColor="Red" Font-Bold="true" ValidationGroup="PASS"></asp:CompareValidator>
       </div>
         <asp:Button ID="btnSavePass" ClientIDMode="Static" runat="server" Text="SAVE" CssClass="btn btn-primary" ValidationGroup="PASS" OnClick="btnSavePass_Click" />

            </div>
        </div>
       
    </div>

    </asp:Content>


