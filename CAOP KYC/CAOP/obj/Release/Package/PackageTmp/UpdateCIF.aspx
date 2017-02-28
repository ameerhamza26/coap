<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.Master" CodeBehind="UpdateCIF.aspx.cs" Inherits="CAOP.UpdateCif" %>

<asp:Content ID="Head" ContentPlaceHolderID="head" runat="server">
    <title>Search CIF's</title>
    <script src="Assets/js/bs.pagination.js"></script>
        <style>
       .lblDiv{
           text-align:center;
           margin-top:15px;
       }
        </style>
</asp:Content>



<asp:Content ID="Body" ContentPlaceHolderID="Body" runat="server">

    <asp:ScriptManager runat="server"></asp:ScriptManager>

    <h3 style="margin-top: 105px">Search CIF</h3>
    <div class="row" >
            <div class="col-md-3">
                <div class="form-group">
                   
                    <asp:RadioButton  ID="radioCNIC" Text="CNIC" GroupName="CRITERIA" Checked="true" runat="server" />
                    <asp:RadioButton  ID="radioNIC" Text="NIC" GroupName="CRITERIA" Checked="true" runat="server" />
                     <asp:RadioButton  ID="radioNTN" Text="NTN" GroupName="CRITERIA" Checked="true" runat="server" />
                     <asp:RadioButton  ID="radioName" Text="NAME" GroupName="CRITERIA" Checked="true" runat="server" />
                     <asp:TextBox ID="txtCif" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" style="margin-top: 20px" runat="server"  CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />                 
                 </div>
             </div>
        </div>

     <asp:UpdatePanel ID="UpdatePaneSearch" UpdateMode="Conditional" runat="server">
            <Triggers>
                 <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click"/>
             </Triggers>

         <ContentTemplate>          

    <asp:GridView class="table" ShowHeaderWhenEmpty="true" PagerStyle-CssClass="bs-pagination" ID="grdPCif" runat="server" AllowPaging="true" PageSize="15" AutoGenerateColumns="false" OnPageIndexChanging="grdPCif_PageIndexChanging" OnRowDataBound="grdPCif_RowDataBound">
    
    <Columns>
       
        <asp:TemplateField HeaderText="CIF ID">
            <ItemTemplate>
                <asp:Label ID="btnCifID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="CNIC">
            <ItemTemplate>
                <asp:Label ID="lblCnic" runat="server" Text='<%# Bind("CNIC") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Name">
            <ItemTemplate>
                <asp:Label ID="lblName" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Office Name">
            <ItemTemplate>
                <asp:Label ID="lblOName" runat="server" Text='<%# Bind("NAME_OFFICE") %>'></asp:Label>
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
        <asp:TemplateField HeaderText="STATUS">
            <ItemTemplate>
                <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("STATUS") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="">
            <ItemTemplate>
                 <asp:LinkButton ID="lbledit" runat="server" Text="OPEN"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        
                                       
    </Columns>
    
   
    </asp:GridView>
             <div class="lblDiv">
                <asp:Label ID="updateCifNotFoundMsg" runat="server" Visible="false" Text="CIF must be incorporated in AOS and Approved by Branch Manager in order to update"></asp:Label>
            </div>
                 </ContentTemplate>
     </asp:UpdatePanel>

</asp:Content>