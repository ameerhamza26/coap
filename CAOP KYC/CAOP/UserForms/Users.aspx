<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.Master" CodeBehind="Users.aspx.cs" Inherits="CAOP.Users" %>

<asp:Content ID="Head" ContentPlaceHolderID="head" runat="server">

    </asp:Content>



<asp:Content ID="Body" ContentPlaceHolderID="Body" runat="server">

     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>




     <h3 style="margin-top: 105px">Search User</h3>
    <div class="row" >

            <div class="col-md-6">
                <div class="form-group">
                   
                    <asp:RadioButton  ID="radioUName" Text="User Name" GroupName="CRITERIA" Checked="true" runat="server" />
                    <asp:RadioButton  ID="radioEmail" Text="Email" GroupName="CRITERIA"  runat="server" />
                     <asp:RadioButton  ID="radioSapId" Text="Sap ID" GroupName="CRITERIA"  runat="server" />
                     <asp:RadioButton  ID="radioBCode" Text="Branch Code" GroupName="CRITERIA"  runat="server" />
                    <asp:RadioButton  ID="radioRegion" Text="Region" GroupName="CRITERIA" Checked="true" runat="server" />
                     <asp:TextBox ID="txtUserSearch" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" style="margin-top: 20px" runat="server"  CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click"/>                 
                 </div>
             </div>
        </div>

     <asp:UpdatePanel ID="UpdatePaneSearch" UpdateMode="Conditional" runat="server">
            <Triggers>
                 <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click"/>
             </Triggers>

         <ContentTemplate>          

    <asp:GridView class="table" ShowHeaderWhenEmpty="true" PagerStyle-CssClass="bs-pagination" ID="grdPUser" runat="server" AllowPaging="true" PageSize="15" AutoGenerateColumns="false" OnRowDataBound="grdPUser_RowDataBound" >
    
    <Columns>
       
      <asp:TemplateField HeaderText="" Visible="false">
            <ItemTemplate>
                <asp:Label ID="lblID" runat="server" Text='<%# Bind("USER_ID") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="USER NAME">
            <ItemTemplate>
                <asp:Label ID="lblUName" runat="server" Text='<%# Bind("USER_NAME") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="EMAIL">
            <ItemTemplate>
                <asp:Label ID="lblEMAIL" runat="server" Text='<%# Bind("EMAIL") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="SAPID">
            <ItemTemplate>
                <asp:Label ID="lblSAPID" runat="server" Text='<%# Bind("SAPID") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>


          <asp:TemplateField HeaderText="USER ROLE">
            <ItemTemplate>
                <asp:Label ID="lblUserRole" runat="server" Text='<%# Bind("Role") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
      
          <asp:TemplateField HeaderText="">
            <ItemTemplate>
                 <asp:LinkButton ID="lblCPass" runat="server" CssClass="btn btn-primary" Text="CHANGE PASSWORD"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
          
        <asp:TemplateField HeaderText="">
            <ItemTemplate>
                 <asp:LinkButton ID="lbledit" runat="server" CssClass="btn btn-primary" Text="EDIT"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        
                                       
    </Columns>
    
   
    </asp:GridView>
       </ContentTemplate>
     </asp:UpdatePanel>


  
</asp:Content>