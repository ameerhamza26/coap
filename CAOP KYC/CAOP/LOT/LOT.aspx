<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.Master" CodeBehind="LOT.aspx.cs" Inherits="CAOP.LOT.LOT" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontroltoolkit" %>



<asp:Content ID="Body" ContentPlaceHolderID="Body" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <h3 style="margin-top: 105px">Search Account</h3>
    <div class="row" >
            <div class="col-md-3">
                <div class="form-group">
                    <label>Account Number: </label>                   
                     <asp:TextBox ID="txtAccountNumber" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" style="margin-top: 20px" runat="server"  CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />                 
                 </div>
             </div>
        </div>
   

    <asp:GridView class="table" ShowHeaderWhenEmpty="true" PagerStyle-CssClass="bs-pagination" ID="grdAccounts" runat="server"  AutoGenerateColumns="false" >
    
    <Columns>
       
        <asp:TemplateField HeaderText="" Visible="false">
            <ItemTemplate>
                <asp:Label ID="btnAccountID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="ACCOUNT NUMBER" >
            <ItemTemplate>
                <asp:Label ID="btnAccountNum" runat="server" Text='<%# Bind("PROFILE_ACCOUNT_NO") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="ACCOUNT TITLE">
            <ItemTemplate>
                <asp:Label ID="lblATitle" runat="server" Text='<%# Bind("ACCOUNT_TITLE") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="INITIAL DEPOSIT">
            <ItemTemplate>
                <asp:Label ID="lblID" runat="server" Text='<%# Bind("INITIAL_DEPOSIT") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="ACCOUNT TYPE">
            <ItemTemplate>
                <asp:Label ID="lblAType" runat="server" Text='<%# Bind("ACCOUNT_TYPE") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

          <asp:TemplateField HeaderText="ACCOUNT ENTRY DATE">
            <ItemTemplate>
                <asp:Label ID="lblCifType" runat="server" Text='<%# Bind("ACCOUNT_ENTRY_DATE") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="">
            <ItemTemplate>
                <asp:Button runat="server" ID="btnPrint" OnClick="btnPrint_Click" CssClass="btn btn-primary" Text="PRINT" />
            </ItemTemplate>
        </asp:TemplateField>
              
            
                                       
    </Columns>
    
   
    </asp:GridView>

      <%-- </ContentTemplate>
     </asp:UpdatePanel>--%>
    <rsweb:ReportViewer Visible="false" ID="ReportViewer1" runat="server"></rsweb:ReportViewer>
</asp:Content>