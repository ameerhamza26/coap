<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.Master" CodeFile="TagProfileForCRM.aspx.cs"
    Inherits="Default2" %>


<%@ OutputCache Duration="1" Location="None" VaryByParam="None" %>

<asp:Content ID="Head" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <link href="~/css/SiteLayout.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="~/JS_LogUsers.js"></script>
    <script type="text/javascript" src="javascript/common.js"></script>
    <script type="text/javascript">

        function windowState() {
            window.status = "";
            return true;
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

        function ConfirmMigrate() {
            var answer = Confirm("Do you want to Delete Existing Records?<br> Upload New Matrix?")
            return false;
        }

        function checkAccountTagging(value, control, value1, control1) {

            if (value == "") {
                alert('Please enter a value for ' + control);
                return false;
            }
            else if (value1 == "") {
                alert('Please enter a value for ' + control1);
                return false;
            }
            else { return true; }
        }
        
    </script>

    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Body" ContentPlaceHolderID="Body" runat="server">

    <table align="center" width="998px">
        <tr>
            <td>
             
            </td>
        </tr>
        <tr>
            <td>
                <table align="center" class="OuterTable" width="998px">
                    <tr>
                        <td align="left" class="HeaderRowSmall" colspan="2">
                            Account Tagging for CRM (Profile &amp; Islamic Branches only)
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#FFCC99" style="">
                            <b>Branch Code : </b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBranchCode" runat="server" Width="208px"></asp:TextBox>
                            <asp:Label ID="lblMandatory5" runat="server" CssClass="ErrorMessage" Text="*"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#FFCC99" style="">
                            <b>Account No. (NEW) : </b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAccount" runat="server" Width="760px" TextMode="MultiLine" OnTextChanged="txtAccount_TextChanged"
                                Height="89px"></asp:TextBox>
                            <br />
                            <asp:Label ID="lblMandatory6" runat="server" CssClass="ErrorMessage" Text="* For updating multile accouts for same branch, please enter accounts with comma(,)&nbsp; *Max 10 accounts"
                                Font-Bold="True" Font-Size="Small"></asp:Label>
                            <br />
                            <asp:Label ID="lblMandatory7" runat="server" CssClass="ErrorMessage" Text="Please check in below grid that all accounts properly tagged"
                                Font-Bold="True" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnTagAccount" runat="server" CssClass="Button" Text="Tag Account"
                                Width="168px" OnClientClick="return isNumberKey(); return checkAccountTagging(document.getElementById('txtBranchCode').value,'Branch Code',document.getElementById('txtAccount').value,'Account No');"
                                OnClick="btnTagAccount_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div style="overflow-y: scroll; text-align: left; width:990px">
                                <asp:GridView ID="gdTaggedAccounts" runat="server" Width="98%" EmptyDataText="No account tagged"
                                    Font-Names="Verdana">
                                    <EmptyDataRowStyle Font-Bold="True" ForeColor="#CC0000" />
                                    <HeaderStyle Font-Bold="True" Font-Names="Verdana" />
                                    <AlternatingRowStyle BackColor="#FF6600" Font-Names="Verdana" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    </asp:Content>
