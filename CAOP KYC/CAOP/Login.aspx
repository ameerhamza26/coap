<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CAOP.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta charset="UTF-8" />
    <title>Login Page</title>

    <script src="Assets/js/html5shiv.min.js"></script>
    <script src="Assets/js/respond.min.js"></script>

    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,400italic,600italic,700italic' rel='stylesheet' type='text/css' />
    <link rel="stylesheet" href="Assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="Assets/css/bootstrap-theme.min.css" />
    <link rel="stylesheet" href="Assets/css/style.css" />
</head>
<body id="login-page">
    <form id="form1" runat="server">

        <div id="login">
    <div class="login-wrapper">
        <div class="login-header">
            <div class="logo">
                <a href="#"><img src="Assets/images/logo.png" /></a>
            </div>
            <div class="title">
                <h3>User Login</h3>
            </div>
        </div><!-- login-header -->

        <div class="login-content">
                    
                    <div class="form-group">
                        <h4>User Name</h4>
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>                        
                    </div>
                    <div class="form-group">
                        <h4>Password</h4>
                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox> 
                    </div> 
                    <asp:Label ID="lblerror" runat="server" Text="Wrong username or password" Visible="false" ForeColor="Red"></asp:Label>        
                    <div class="form-group">
                        <asp:Button ID="btnLogin" runat="server" Text="submit" CssClass="btn btn-primary w100p" OnClick="btnLogin_Click" />                       
                    </div>
        </div><!-- login-content -->
    </div><!-- login -->
</div><!-- container -->

    <div>
    
    </div>
    </form>

    <script src="Assets/js/jquery-2.1.1.min.js"></script>
    <script src="Assets/js/bootstrap.min.js"></script>
</body>
</html>
