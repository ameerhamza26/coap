<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPages/Default.Master" CodeBehind="InProcess.aspx.cs" Inherits="CAOP.CifForms.InProcess" %>

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
        <asp:TemplateField HeaderText="BRANCH CODE">
            <ItemTemplate>
                <asp:Label ID="btnBranch" runat="server" Text='<%# Bind("BRANCH_CODE") %>'></asp:Label>
                
            </ItemTemplate>
        </asp:TemplateField>
       
        <asp:TemplateField HeaderText="CIF NO" Visible="false" >
            <ItemTemplate>
                <asp:Label ID="btnCifID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="PROFILE CIF NO" >
            <ItemTemplate>
                <asp:Label ID="btnCifnumprofile" runat="server" Text='<%# Bind("PROFILE_CIF_NO") %>'></asp:Label>
                
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="IDENTITY">
            <ItemTemplate>
                <asp:Label ID="lblCnic" runat="server" Text='<%# Bind("CNIC") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="NAME">
            <ItemTemplate>
                <asp:Label ID="lblName" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="OFFICE NAME">
            <ItemTemplate>
                <asp:Label ID="lblOName" runat="server" Text='<%# Bind("NAME_OFFICE") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="CIF TYPE">
            <ItemTemplate>
                <asp:Label ID="lblCifType" runat="server" Text='<%# Bind("CIF_TYPE") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="NTN" Visible="false">
            <ItemTemplate>
                <asp:Label ID="lblNTN" runat="server" Text='<%# Bind("NTN") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
           <asp:TemplateField HeaderText="RISK CATEGORY">
            <ItemTemplate>
                <asp:Label ID="lblRCategory" runat="server" Text='<%# Bind("RISK_CATEGORY") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
          <asp:TemplateField HeaderText="RISK SCORE">
            <ItemTemplate>
                <asp:Label ID="lblRScore" runat="server" Text='<%# Bind("RISK_SCORE") %>'></asp:Label>
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