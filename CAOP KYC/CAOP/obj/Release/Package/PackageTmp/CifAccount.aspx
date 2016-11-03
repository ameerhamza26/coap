<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.Master" CodeBehind="CifAccount.aspx.cs" Inherits="CAOP.CifAccount" %>

<asp:Content ID="Head" ContentPlaceHolderID="head" runat="server">
    <title>CAOP</title>
    <script src="Assets/js/bs.pagination.js"></script>
</asp:Content>



<asp:Content ID="Body" ContentPlaceHolderID="Body" runat="server">

      <asp:ScriptManager runat="server"></asp:ScriptManager>

     <asp:UpdatePanel ID="UpdatePaneSearch" UpdateMode="Conditional" runat="server">
          <ContentTemplate>  
    <div runat="server" id="SearchCriteria" visible="false">
    <h3 style="margin-top: 105px">Search CIF</h3>
    <div class="row" >
            <div class="col-md-4">
                <div class="form-group">
                   
                    <asp:RadioButton  ID="radioBCode" AutoPostBack="true" Text="BRANCH CODE" GroupName="CRITERIA" Checked="true" runat="server" OnCheckedChanged="radioCifSearch_CheckedChanged" />                    
                    <asp:RadioButton  ID="radioCNIC" AutoPostBack="true" Text="CNIC" GroupName="CRITERIA"  runat="server" OnCheckedChanged="radioCifSearch_CheckedChanged" />                    
                    <asp:RadioButton  ID="radioName" AutoPostBack="true" Text="NAME" GroupName="CRITERIA"  runat="server" OnCheckedChanged="radioCifSearch_CheckedChanged" />
                    <asp:RadioButton  ID="radioCifType" AutoPostBack="true" Text="CIF Type" GroupName="CRITERIA"  runat="server" OnCheckedChanged="radioCifSearch_CheckedChanged" />
                    <asp:TextBox ID="txtCif" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:DropDownList ID="ddlCifTypes" CssClass="form-control" Visible="false"  runat="server"></asp:DropDownList>
                    <asp:Button ID="btnSearch" style="margin-top: 20px" runat="server"  CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click"  />                 
                 </div>
             </div>
        </div>
    </div>

          

    
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