<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.Master"  CodeBehind="InProcessAccounts.aspx.cs" Inherits="CAOP.AccountForms.InProcessAccounts" %>

<asp:Content ID="Head" ContentPlaceHolderID="head" runat="server">
    <title>CAOP</title>
    <script src="Assets/js/bs.pagination.js"></script>
</asp:Content>



<asp:Content ID="Body" ContentPlaceHolderID="Body" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>

     <asp:UpdatePanel ID="UpdatePaneSearch" UpdateMode="Conditional" runat="server">
          <ContentTemplate> 

    <asp:GridView class="table" ShowHeaderWhenEmpty="true" Visible="true" PagerStyle-CssClass="bs-pagination" ID="grdPCif" runat="server" AllowPaging="true" PageSize="15" AutoGenerateColumns="false" OnPageIndexChanging="grdPCif_PageIndexChanging" OnRowDataBound="grdPCif_RowDataBound">
    
    <Columns>

            <asp:TemplateField HeaderText="Branch Code" >
            <ItemTemplate>
                <asp:Label ID="BtnBranch" runat="server" Text='<%# Bind("BRANCH_CODE") %>'></asp:Label>
                
            </ItemTemplate>
        </asp:TemplateField>
       
        <asp:TemplateField HeaderText="Temporary Account Number" >
            <ItemTemplate>
                <asp:Label ID="btnID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText=" Profile Account Number">
            <ItemTemplate>
                <asp:Label ID="lblpAccount" runat="server" Text='<%# Bind("PROFILE_ACCOUNT_NO") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="ACCOUNT TITLE">
            <ItemTemplate>
                <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("ACCOUNT_TITLE") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="ACCOUNT TYPE">
            <ItemTemplate>
                <asp:Label ID="lblAccountType" runat="server" Text='<%# Bind("ACCOUNT_OPEN_TYPE") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="STATUS">
            <ItemTemplate>
                <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("STATUS") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

            <asp:TemplateField HeaderText="LAST UPDATED">
            <ItemTemplate>
                <asp:Label ID="lblLastUpdated" runat="server" Text='<%# Bind("LAST_UPDATED") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="">
            <ItemTemplate>
                 <asp:LinkButton ID="lbledit" CssClass="btn btn-primary" runat="server" Text="OPEN"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>

                                       
    </Columns>
    
    
    </asp:GridView>

</ContentTemplate>
     </asp:UpdatePanel>

</asp:Content>