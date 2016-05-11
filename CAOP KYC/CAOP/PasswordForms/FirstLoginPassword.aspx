<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FirstLoginPassword.aspx.cs" Inherits="CAOP.PasswordForms.FirstLoginPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AOS</title>
    <link rel="shortcut icon" type="image/x-icon" href="Assets/images/hp.ico" />

    <script src="<%= Page.ResolveUrl("~")%>Assets/js/html5shiv.min.js"></script>
    <script src="<%= Page.ResolveUrl("~")%>Assets/js/respond.min.js"></script>
    <script src="<%= Page.ResolveUrl("~")%>Assets/js/jquery-1.11.3.min.js"></script>
    <script src="<%= Page.ResolveUrl("~")%>Assets/js/jquery-migrate-1.2.1.min.js"></script>
    <script src="<%= Page.ResolveUrl("~")%>Assets/js/myscript.js"></script>
    <script src="<%= Page.ResolveUrl("~")%>Assets/js/jquery-ui.min.js"></script>
    <script src="<%= Page.ResolveUrl("~")%>Assets/js/alert.js"></script>
    <script src="<%= Page.ResolveUrl("~")%>Assets/js/script.js"></script>
    <script src="<%= Page.ResolveUrl("~")%>Assets/js/script1.js"></script> 
    <link href='<%= Page.ResolveUrl("~")%>Assets/css/fonts.css' rel='stylesheet' type='text/css'/>
    <link rel="stylesheet" href="<%= Page.ResolveUrl("~")%>Assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="<%= Page.ResolveUrl("~")%>Assets/css/bootstrap-theme.min.css" />
    <link rel="stylesheet" href="<%= Page.ResolveUrl("~")%>Assets/css/style.css" />
</head>
<body>
    <form id="form1" runat="server">

   <div class="container">
    <header id="header">
        <div class="logo">
            <a href="#"><img src="<%= Page.ResolveUrl("~")%>Assets/images/logo.png"  /></a>
            ACCOUNT OPENING SYSTEM &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
        <div class="top-user-panel">
            <div class="date">
                 <asp:Label ID="lblDate" runat="server" Text="Curent Date"></asp:Label>
            </div>
            <table>
                <tr>
                    <td>User: </td> <td class="second">
                        <asp:Label ID="lblUser" runat="server" Text="User-Name"></asp:Label></td>
                    <td>Region: </td> <td> <asp:Label ID="lblRegion" runat="server" Text="Karachi"></asp:Label></td>
                </tr>
                <tr>
                    <td>Branch: </td> <td class="second"><asp:Label ID="lblBranch" runat="server" Text="NBP"></asp:Label></td>
                    <td>Role: </td> <td><asp:Label ID="lblRole" runat="server" Text="Branch Operator"></asp:Label></td>
                </tr>
            </table>
        </div>
        <nav class="menu">
            
        </nav>
    </header>
         
    
    <div>
        <div class="content">

         <h3>Change Password</h3>
        <div class="row">
            <div class="col-md-4">
        <div class="form-group">
            <label  class="lblReview">Old Password: *</label>
            <asp:TextBox ID="txtOldPass" TextMode="Password"  ClientIDMode="Static" CssClass="form-control"   runat="server"></asp:TextBox>
           <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorOldPAss" runat="server" ControlToValidate="txtOldPass" ErrorMessage="Old Password is Required" ForeColor="Red" Font-Bold="true" ValidationGroup="PASS" />
            <asp:CustomValidator Display="Dynamic" ID="CustomValidatoroldpass" runat="server" ErrorMessage="Wrong Password" ForeColor="Red" Font-Bold="true" ValidationGroup="PASS" OnServerValidate="CustomValidatoroldpass_ServerValidate" ></asp:CustomValidator>
       </div>
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
    </div>

     <footer id="footer">
        Copyright - National Bank of Pakistan.
    </footer>
</div><!-- container -->

    </form>
   

<script src="<%= Page.ResolveUrl("~")%>Assets/js/jquery-2.1.1.min.js"></script>
<script src="<%= Page.ResolveUrl("~")%>Assets/js/bootstrap.min.js"></script>
<script src="<%= Page.ResolveUrl("~")%>Assets/js/jquery-ui.min.js"></script>
<script src="<%= Page.ResolveUrl("~")%>Assets/js/jquery.validate.js"></script>
<script src="<%= Page.ResolveUrl("~")%>Assets/js/custom.js"></script>

</body>
</html>

