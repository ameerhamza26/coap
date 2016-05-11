<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.Master" CodeBehind="PendingCIF.aspx.cs" Inherits="CAOP.PendingCIF" %>

<asp:Content ID="Head" ContentPlaceHolderID="head" runat="server">
    <title>Pending CIF's</title>
    <script src="Assets/js/bs.pagination.js"></script>
</asp:Content>



<asp:Content ID="Body" ContentPlaceHolderID="Body" runat="server">


    <asp:GridView class="table" ShowHeaderWhenEmpty="true" PagerStyle-CssClass="bs-pagination" ID="grdPCif" runat="server" AllowPaging="true" PageSize="15" AutoGenerateColumns="false" OnPageIndexChanging="grdPCif_PageIndexChanging" OnRowDataBound="grdPCif_RowDataBound">
    
    <Columns>
       
        <asp:TemplateField HeaderText="CIF ID" Visible="false">
            <ItemTemplate>
                <asp:Label ID="btnCifID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
          <asp:TemplateField HeaderText="Name">
            <ItemTemplate>
                <asp:Label ID="lblName" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="OFFICE NAME">
            <ItemTemplate>
                <asp:Label ID="lblOName" runat="server" Text='<%# Bind("NAME_OFFICE") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="IDENTITY">
            <ItemTemplate>
                <asp:Label ID="lblCnic" runat="server" Text='<%# Bind("CNIC") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

     
          <asp:TemplateField HeaderText="CIF TYPE">
            <ItemTemplate>
                <asp:Label ID="lblCifType" runat="server" Text='<%# Bind("CIF_TYPE") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="NTN">
            <ItemTemplate>
                <asp:Label ID="lblNTN" runat="server" Text='<%# Bind("NTN") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="STATUS" Visible="false">
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
                 <asp:LinkButton ID="lblDel" CssClass="btn btn-primary" runat="server" Text="DELETE" OnClick="lblDel_Click"></asp:LinkButton>
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