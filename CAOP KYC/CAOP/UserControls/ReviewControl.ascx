<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReviewControl.ascx.cs" Inherits="CAOP.UserControls.ReviewControl" %>

<style>
.pull-right {
  float: right !important;
}
</style>

<script>
    $('.lblReview').click(function (obj) {
        
        $('#Body_rev_txtField').val($(this).html());              
        $('#Body_rev_txtFieldId').val($(this).next().attr('id'));
        
        var tab = $(this).parents('.tab-pane').attr('id');

        if (tab == "sectiona")
            tab = "1";
        else if (tab == "sectionb")
            tab = "2";
        else if (tab == "sectionc")
            tab = "3";
        else if (tab == "sectiond")
            tab = "4";
        else if (tab == "sectione")
            tab = "5";
        else if (tab == "sectionf")
            tab = "6";
        else if (tab == "sectiong")
            tab = "7";
        else if (tab == "sectionh")
            tab = "8";
        else if (tab == "sectioni")
            tab = "9";
        else if (tab == "sectionj")
            tab = "10";
        $('#Body_rev_txtTabNo').val(tab);
        window.scrollTo(0, document.body.scrollHeight);
    });
</script>



<div class="row" style="margin-top: 15px">
     <div class="col-lg-12">
    <div class="panel panel-success">
    <div class="panel-heading">Review</div>
    <div class="panel-body">

 <div runat="server" id="CommentDiv">

      <asp:UpdatePanel runat="server" ID="updatePane_AR" UpdateMode="Conditional" ClientIDMode="AutoID" >
          <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnAddComment" EventName="Click"/>
           </Triggers>
             <ContentTemplate>
                 <asp:Button ID="btnAR" runat="server"  Text="Approve" CssClass="btn btn-primary pull-right"  OnClick="btnAR_Click" />
            </ContentTemplate>
          </asp:UpdatePanel>
       <div class="container" >
           <div class="row">
               <div class="col-lg-3"></div>
               <div class="col-lg-6">
                    <div style="outline: 3px solid black ; float:none; padding: 25px; margin-bottom: 40px">
                         <label >Please Add Comments / Errors: - </label>
                        <div class="row" >
                            <div class="col-lg-8">
                                 <div class="form-group">

                                    <label>Tab No: </label>
                                     <asp:TextBox ID="txtTabNo" CssClass="form-control" runat="server"></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidatorTabNo" Display="Dynamic" runat="server" ControlToValidate="txtTabNo" ForeColor="Red" Font-Bold="true" ValidationGroup="Review" ErrorMessage="Tab No is Required"></asp:RequiredFieldValidator>
                                </div>

                                 <div class="form-group">
                                    <label>Field Name: </label>
                                     <asp:TextBox ID="txtField" CssClass="form-control" runat="server"></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidatorField" Display="Dynamic" runat="server" ControlToValidate="txtField" ForeColor="Red" Font-Bold="true" ValidationGroup="Review" ErrorMessage="Field Name is Required"></asp:RequiredFieldValidator>
                                </div>

                                             

                                <div class="form-group">
                                    <label>Error Comment Detail: </label>
                                    <asp:TextBox ID="txtDetail" TextMode="MultiLine" Rows="7" Columns="20" CssClass="form-control" runat="server"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidatorComment" Display="Dynamic" runat="server" ControlToValidate="txtDetail" ForeColor="Red" Font-Bold="true" ValidationGroup="Review" ErrorMessage="Error Comment Detail is Required"></asp:RequiredFieldValidator>
                                </div>
                                 <asp:TextBox ID="txtFieldId" style="display: none" CssClass="form-control" runat="server"></asp:TextBox>
                                

                                 <asp:Button ID="btnAddComment" runat="server"  Text="Add Error Comments" CssClass="btn btn-primary" ValidationGroup="Review" OnClick="btnAddComment_Click" />
                                 <asp:Button ID="btnReset" Visible="false" Type="reset" runat="server"  Text="Reset" CssClass="btn btn-primary" />
                               
                                 
                            </div>
                        </div>  
                        
                                                     
                    </div>
                </div>
          </div>
      </div>
 </div>
   

         <div class="row">
             <div class="col-lg-12">

                  <asp:UpdatePanel runat="server" ID="updatePanel_Comment" UpdateMode="Conditional" ClientIDMode="AutoID" >
                      <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="btnAddComment" EventName="Click"/>
                       </Triggers>
                      
                      
                      <ContentTemplate>

                    <asp:GridView  class="table" PagerStyle-CssClass="bs-pagination" ID="grdReview" ShowHeaderWhenEmpty="true" runat="server" AllowPaging="true" PageSize="15" AutoGenerateColumns="false" OnRowDataBound="grdReview_RowDataBound" OnPageIndexChanging="grdReview_PageIndexChanging"  >    
                    <Columns> 
                                        
                            <asp:TemplateField HeaderText="Record No">
                            <ItemTemplate>
                                <asp:Label ID="lblRec" runat="server" Text='<%# Bind("Rec") %>'></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tab No">
                            <ItemTemplate>
                                <asp:Label ID="lblTab" runat="server" Text='<%# Bind("Tab") %>'></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>
                                              
                            <asp:TemplateField HeaderText="Field Name">
                            <ItemTemplate>
                                <asp:Label ID="lblField" runat="server" Text='<%# Bind("Field") %>'></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Comment">
                            <ItemTemplate>
                                <asp:Label ID="lblCmnt" runat="server" Text='<%# Bind("Comment") %>'></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Comment" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblDate" runat="server" Text='<%# Bind("FieldID") %>'></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>
                                       
                    </Columns>        
                    </asp:GridView>

                    </ContentTemplate>
                       </asp:UpdatePanel>
              </div>
            </div>

       <%-- Grid for old Reviews--%>

        <div class="row">
             <div class="col-lg-12">
                <h3 runat="server" id="BMRH" visible="false">Manager Review</h3>

                    <asp:GridView   class="table" PagerStyle-CssClass="bs-pagination" ID="grdReviewPrev" runat="server"  AutoGenerateColumns="false"    >    
                    <Columns> 
                                        
                            <asp:TemplateField HeaderText="Record No">
                            <ItemTemplate>
                                <asp:Label ID="lblRec" runat="server" Text='<%# Bind("Rec") %>'></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tab No">
                            <ItemTemplate>
                                <asp:Label ID="lblTab" runat="server" Text='<%# Bind("Tab") %>'></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>
                                              
                            <asp:TemplateField HeaderText="Field Name">
                            <ItemTemplate>
                                <asp:Label ID="lblField" runat="server" Text='<%# Bind("Field") %>'></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Comment">
                            <ItemTemplate>
                                <asp:Label ID="lblCmnt" runat="server" Text='<%# Bind("Comment") %>'></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Comment" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblDate" runat="server" Text='<%# Bind("FieldID") %>'></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>
                                       
                    </Columns>        
                    </asp:GridView>
                
              </div>
            </div>

     </div>
    </div>
</div>
</div>
   

