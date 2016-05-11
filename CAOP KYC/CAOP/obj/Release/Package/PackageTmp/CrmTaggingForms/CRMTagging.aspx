<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.Master" CodeBehind="CRMTagging.aspx.cs" Inherits="CAOP.CrmTaggingForms.CRMTagging" %>

<asp:Content ID="Body" ContentPlaceHolderID="Body" runat="server">

    <div class="content">

        <h3>Account Tagging for CRM (Profile &amp; Islamic Branches only)
                        </h3>
        <div class="row">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <div class="col-md-4">
        <div class="form-group">
            <label  class="lblReview">Branch Code: *</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtBranchCode"  ClientIDMode="Static" CssClass="form-control"   runat="server"></asp:TextBox>
       </div>
          <div class="form-group">
            <label  class="lblReview">Account No. (NEW): *</label>
            <asp:TextBox ID="txtAccount" TextMode="MultiLine"  ClientIDMode="Static" CssClass="form-control"  runat="server" Height="89px" Width="330px"></asp:TextBox>
       </div>
       <div class="form-group">
       </div>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnTagAccount" runat="server" CssClass="btn btn-primary" Text="Tag Account"
                                Width="168px" OnClientClick="return isNumberKey(); return checkAccountTagging(document.getElementById('txtBranchCode').value,'Branch Code',document.getElementById('txtAccount').value,'Account No');"
                                OnClick="btnTagAccount_Click" />
                        <br />
                <div style="overflow: auto; height: 300px; width: 900px; margin-top: 20px">
                     <asp:GridView ID="gdTaggedAccounts"   CssClass="table" runat="server" Width="98%" EmptyDataText="No account tagged"
                                    Font-Names="Verdana" >
                                    <EmptyDataRowStyle Font-Bold="True" ForeColor="#CC0000" />
                                    <HeaderStyle Font-Bold="True" Font-Names="Verdana" />
                                    <AlternatingRowStyle BackColor="#FF6600" Font-Names="Verdana" />
                                </asp:GridView>

                </div>
                               



            </div>
        </div>
       
    </div>
</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .form-control {}
    </style>
</asp:Content>
