<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.Master" CodeBehind="PendingAccounts.aspx.cs" Inherits="CAOP.PendingAccounts" %>


<asp:Content ID="Head" ContentPlaceHolderID="head" runat="server">
    <title>Pending Accounts</title>
    <script src="Assets/js/bs.pagination.js"></script>
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="Body" runat="server">


    <asp:GridView ShowHeaderWhenEmpty="true" class="table" PagerStyle-CssClass="bs-pagination" ID="grdPAccounts" runat="server" AllowPaging="true" PageSize="15" AutoGenerateColumns="false" OnPageIndexChanging="grdPAccounts_PageIndexChanging" OnRowDataBound="grdPAccounts_RowDataBound">
    
    <Columns>
       
        <asp:TemplateField HeaderText="Temporary Account Number">
            <ItemTemplate>
                
                <asp:Label ID="btnID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Account Number" Visible="false">
            <ItemTemplate>
                <asp:Label ID="lblAccountNumber" runat="server" Text='<%# Bind("ACCOUNT_NUMBER") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Account Title">
            <ItemTemplate>
                <asp:Label ID="lblAccountTitle" runat="server" Text='<%# Bind("ACCOUNT_TITLE") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
          <asp:TemplateField HeaderText="Account Type">
            <ItemTemplate>
                <asp:Label ID="lblAccountType" runat="server" Text='<%# Bind("ACCOUNT_OPEN_TYPE") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="STATUS">
            <ItemTemplate>
                <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("STATUS") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Last Updated">
            <ItemTemplate>
                <asp:Label ID="lblLastUpdated" runat="server" Text='<%# Bind("LAST_UPDATED") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

         <asp:TemplateField HeaderText="">
            <ItemTemplate>
                 <asp:LinkButton ID="lbldel" CssClass="btn btn-primary" runat="server" Text="DELETE" OnClick="lbldel_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="">
            <ItemTemplate>
                 <asp:LinkButton ID="lbledit" CssClass="btn btn-primary" runat="server" Text="OPEN"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>

                                       
    </Columns>
    
    
    </asp:GridView>


</asp:Content>