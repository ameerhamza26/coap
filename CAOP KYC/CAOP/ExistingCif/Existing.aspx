<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.Master" CodeBehind="Existing.aspx.cs" Inherits="CAOP.ExistingCif.Existing" %>

<asp:Content ID="Head" ContentPlaceHolderID="head" runat="server">
    <script src="Assets/js/bs.pagination.js"></script>
</asp:Content>

<asp:Content ID="Body" ContentPlaceHolderID="Body" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>

    <h3 style="margin-top: 105px">Search CIF</h3>
    <div class="row" >
            <div class="col-md-3">
                <div class="form-group">
                   
                    <label>CNIC</label>
                     <asp:TextBox ID="txtCnic" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                     <asp:RegularExpressionValidator   Display="Dynamic" ID="RegularExpressionValidatorCnic" runat="server" ErrorMessage="The CNIC must be in correct format e.g xxxxx-xxxxxxx-x" ForeColor="Red" Font-Bold="true" ControlToValidate="txtCnic" ValidationGroup="SEARCH" ValidationExpression="^\d{5}-\d{7}-\d{1}$" ></asp:RegularExpressionValidator>                                  
                    <asp:Button ID="btnSearch" style="margin-top: 20px" runat="server" ValidationGroup="SEARCH"  CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />                 
                 </div>
             </div>
   </div>

    <asp:UpdatePanel ID="UpdatePaneSearch" UpdateMode="Conditional" runat="server">
            <Triggers>
                 <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click"/>
             </Triggers>

         <ContentTemplate>          
             <asp:Label ID="lblError" runat="server" Text="" Visible="false" ForeColor="Red" Font-Bold="true"></asp:Label>
    <asp:GridView class="table" ShowHeaderWhenEmpty="true" PagerStyle-CssClass="bs-pagination" ID="grdPCif" runat="server" AllowPaging="true" PageSize="15" AutoGenerateColumns="false" OnPageIndexChanging="grdPCif_PageIndexChanging" OnRowDataBound="grdPCif_RowDataBound">
    
    <Columns>
       
        <asp:TemplateField HeaderText="PROFILE CIF NO">
            <ItemTemplate>
                <asp:Label ID="btnCifno" runat="server" Text='<%# Bind("CIF_NO") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
          <asp:TemplateField HeaderText="CIF CREATION BRANCH">
            <ItemTemplate>
                <asp:Label ID="btnCBranch" runat="server" Text='<%# Bind("BRANCH_NAME") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="NAME">
            <ItemTemplate>
                <asp:Label ID="lblName" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="FATHER NAME">
            <ItemTemplate>
                <asp:Label ID="lblfName" runat="server" Text='<%# Bind("FATHER_NAME") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="MOTHER NAME">
            <ItemTemplate>
                <asp:Label ID="lblmName" runat="server" Text='<%# Bind("MOTHER_NAME") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

          <asp:TemplateField HeaderText="DATE OF BIRTH">
            <ItemTemplate>
                <asp:Label ID="lblDob" runat="server" Text='<%# Bind("DOB") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="EXPIRY DATE" Visible="false">
            <ItemTemplate>
                <asp:Label ID="lblEDate" runat="server" Text='<%# Bind("EXPIRY_DATE") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>       
        <asp:TemplateField HeaderText="">
            <ItemTemplate>
                 <asp:LinkButton ID="lblIncor" CssClass="btn btn-primary" runat="server" Text="INCORPORATE" OnClick="lblIncor_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        
                                       
    </Columns>
    
   
    </asp:GridView>

       </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>