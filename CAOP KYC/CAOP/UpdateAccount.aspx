<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.Master" CodeBehind="UpdateAccount.aspx.cs" Inherits="CAOP.UpdateAccount" %>


<asp:Content ID="Head" ContentPlaceHolderID="head" runat="server">
    <title>CAOP</title>
    <script src="Assets/js/bs.pagination.js"></script>
</asp:Content>



<asp:Content ID="Body" ContentPlaceHolderID="Body" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>

    <h3 style="margin-top: 105px">Search Account</h3>
    <div class="row" >
            <div class="col-md-3">
                <div class="form-group">
                   
                    <asp:RadioButton  ID="radioCNIC" Text="Profile Account Number" GroupName="CRITERIA" Checked="true" runat="server" />
                    <%--<asp:RadioButton  ID="radioNIC" Text="NIC" GroupName="CRITERIA" Checked="true" runat="server" />
                     <asp:RadioButton  ID="radioNTN" Text="NTN" GroupName="CRITERIA" Checked="true" runat="server" />
                     <asp:RadioButton  ID="radioName" Text="NAME" GroupName="CRITERIA" Checked="true" runat="server" />--%>
                     <asp:TextBox ID="txtCif" ClientIDMode="Static" CssClass="form-control" runat="server" MaxLength="12"></asp:TextBox>
                    <asp:Button ID="btnSearch" style="margin-top: 20px" runat="server"  CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click"/>                 
                 </div>
             </div>
        </div>

     <asp:UpdatePanel ID="UpdatePaneSearch" UpdateMode="Conditional" runat="server">
             <Triggers>
                 <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click"/>
             </Triggers>
          <ContentTemplate> 

     <div runat="server" id="SearchCriteria" visible="false">
    <h3 style="margin-top: 105px">Search Account</h3>
    <div class="row" >
            <div class="col-md-5">
                <div class="form-group">
                   
                    <asp:RadioButton  ID="radioBCode" AutoPostBack="true" Text="BRANCH CODE" GroupName="CRITERIA" Checked="true" runat="server"  />                    
                    <asp:RadioButton  ID="radioAno" AutoPostBack="true" Text="Temporary Account No" GroupName="CRITERIA"  runat="server" />                    
                    <asp:RadioButton  ID="radioATitle" AutoPostBack="true" Text="Account Title" GroupName="CRITERIA"  runat="server"  />                   
                    <asp:TextBox ID="txtAccount" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>                   
                    <asp:Button ID="btnSearchCriteria" style="margin-top: 20px" runat="server"  CssClass="btn btn-primary" Text="Search" OnClick="btnSearchCriteria_Click" />                 
                 </div>
             </div>
        </div>
    </div>

    <asp:GridView class="table" ShowHeaderWhenEmpty="true" Visible="true" PagerStyle-CssClass="bs-pagination" ID="grdPCif" runat="server" AllowPaging="true" PageSize="15" AutoGenerateColumns="false" OnPageIndexChanging="grdPCif_PageIndexChanging" OnRowDataBound="grdPCif_RowDataBound">
    
    <Columns>
       
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
