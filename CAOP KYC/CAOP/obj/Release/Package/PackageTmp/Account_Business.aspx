<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.Master" CodeBehind="Account_Business.aspx.cs" Inherits="CAOP.Account_Business" %>


<%@ Register Src="~/UserControls/ReviewControl.ascx" TagName="Review" TagPrefix="Rev" %>


<asp:Content ID="Head" ContentPlaceHolderID="head" runat="server">
    <title>Account opening Portal</title>
    <script src="Assets/js/jquery.quicksearch.js"></script>

    <script type="text/javascript">
       function doPostBack(o) {
            __doPostBack(o.id, '');
        }

       function select_Business_cif(a,b,c,d,e)
       {
           document.getElementById("CiCustomerCif").value = a;

           document.getElementById("CiName").value = b;

           document.getElementById("CiSalesTaxNo").value = c;

           document.getElementById("CiRegistrationNo").value = d;

          

           var AgencySelect =  document.getElementById("CiListRegistrationIssueAgency");

           for (var i = 0; i < AgencySelect.options.length; i++) {
               if (AgencySelect.options[i].text == e) {
                   AgencySelect.options[i].selected = true;
                   
               }
           }


           $('#SearchBusinessCIF').modal('hide');
       }

         function select_cif(a,b,c) {
            //alert("hw");
           // alert(a + " " + b + " " + c);

            document.getElementById("AuthCustomerCif").value = a;
            document.getElementById("AuthCustomerName").value = b;
            document.getElementById("AuthCustomerCNIC").value = c;


    <%--        var grid1 = document.getElementById("<%= searchCIF.ClientID%>");  
            for (i = 1; i < grid1.rows.length ; i++) 
            {
                var cellValue = grid1.rows[i].cells[3].value;
                alert(cellValue);
            }--%>
            
              

            $('#SearchCIF').modal('hide');
         }

        function select_who_cif(a,b,c,d,e,f)
        {
            document.getElementById("WhoTxtCifNo").value = a;
            document.getElementById("WhoTxtNAme").value = b + ' ' + c + ' ' + d;
            document.getElementById("WhoTxtIdentityNo").value = e;
            document.getElementById("WhoTxtIdentityType").value = f;
           
            $('#SearchCifWho').modal('hide');
        }

        function RefreshUpdatePanel() {
            __doPostBack('<%= SearchCIDModal.ClientID %>', '');
        };

       

        function Search_Gridview(strKey, strGV) {
            // alert("hello");
            var strData = strKey.value.toLowerCase().split(" ");
            alert(strData);
            var tblData = document.getElementById(strGV);
            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }
    </script>

</asp:Content>

<asp:Content ID="Body" ContentPlaceHolderID="Body" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
    <div class="content">
        <div id="alerts">
            <%-- <div class="alert alert-success"><button type="button" class="close" data-dismiss="alert">
               &times;</button>Information has been saved</div>--%>
        </div>

        <!-- Modal -->
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <%--<div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title"></h4>
        </div>--%>
                    <div class="modal-body">
                        <p>Are you sure you want to Reset all fields?</p>
                    </div>
                    <div class="modal-footer">
                        <button id="InResetBasicInfoModal" type="reset" class="btn btn-primary" value="Reset" data-toggle="modal" data-target="#myModal">Reset</button>

                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>

        <!-- Modal -->
        <div class="modal fade" id="SearchBusinessCIF" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">




                    <div class="modal-body">

                        
                        <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="Panel2" runat="server"></asp:Panel>

                                <label class="lblReview">Search Entity CIF:</label>
                         <asp:TextBox ID="SearchBusinessCifText"  ClientIDMode="Static" CssClass="form-control" runat="server" ></asp:TextBox>
                                                        <div class="control-group">
                            <label class="lblReview control-label" >Search by:</label>
                            <div class="controls"> 
                            <asp:RadioButton ID="RadioButton1" Text="Name" Style="margin-right: 5px" Checked="True" GroupName="SearchEntityCifGroup" runat="server" />
                            <asp:RadioButton ID="RadioButton2" Text="Sales Tax No" GroupName="SearchEntityCifGroup" runat="server" />
                            <asp:RadioButton ID="RadioButton3" Text="NTN"  GroupName="SearchEntityCifGroup" runat="server" />
                            <asp:RadioButton ID="RadioButton4" Text="Registration No" GroupName="SearchEntityCifGroup" runat="server" />
                            
                            </div>
                        </div>
                                <asp:Button ID="Search_BusinessCIF" OnClick="Search_BusinessCIF_Click" runat="server" CssClass="btn btn-primary" Text="Search" />

                                <asp:GridView class="table" PagerStyle-CssClass="bs-pagination" ID="SearchBusinessCif" runat="server" AllowPaging="true" PageSize="15" OnSelectedIndexChanged="SearchBusinessCif_SelectedIndexChanged" AutoGenerateColumns="false" OnPageIndexChanging="SearchBusinessCif_PageIndexChanging" OnRowDataBound="SearchBusinessCif_RowDataBound" OnDataBound="SearchBusinessCif_DataBound">


                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>


                                               <asp:LinkButton ID="SelectBusinessCif" runat="server" CommandArgument='<%# Bind("ID") %>' Text="Select" OnClick="SelectBusinessCif_Click" OnClientClick='<%# String.Format("return select_Business_cif({0},\"{1}\",\"{2}\",\"{3}\",\"{4}\");",Eval("ID"),Eval("NAME_OFFICE"),Eval("SALES_TAX_NO"),Eval("REG_NO"),Eval("Issuing_Agency")) %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblId" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        

                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="server" Text='<%# Bind("NAME_OFFICE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SALES_TAX_NO">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSaleTax" runat="server" Text='<%# Bind("SALES_TAX_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="REG_NO">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRegNo" runat="server" Text='<%# Bind("REG_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Issuing_Agency">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIssueAgency" runat="server" Text='<%# Bind("Issuing_Agency") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    


                                    </Columns>


                                </asp:GridView>



                            </ContentTemplate>

                            <Triggers>
                                <%--<asp:AsyncPostBackTrigger ControlID="SearchCIDModal"/>--%>
                            </Triggers>

                        </asp:UpdatePanel>





                    </div>
                    <div class="modal-footer">

                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>



        <div class="modal fade" id="SearchCIF" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">




                    <div class="modal-body">

                        
                        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="Panel1" runat="server"></asp:Panel>

                                <label class="lblReview">Search Data by Identity:</label>
                         <asp:TextBox ID="SearchCIDModal"  ClientIDMode="Static" CssClass="form-control" runat="server"  ></asp:TextBox>
                                <asp:Button ID="Search" OnClick="Search_Click" runat="server" CssClass="btn btn-primary" Text="Search" />

                                <asp:GridView class="table" PagerStyle-CssClass="bs-pagination" ID="searchCIF" runat="server" AllowPaging="true" PageSize="15" OnSelectedIndexChanged="searchCIF_SelectedIndexChanged" AutoGenerateColumns="false" OnPageIndexChanging="searchCIF_PageIndexChanging" OnRowDataBound="searchCIF_RowDataBound" OnDataBound="searchCIF_DataBound">


                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>


                                               <asp:LinkButton ID="Select" runat="server" CommandArgument='<%# Bind("ID") %>' Text="Select" OnClick="Select_Click" OnClientClick='<%# String.Format("return select_cif({0},\"{1}\",\"{2}\");",Eval("ID"),Eval("NAME"),Eval("CNIC")) %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblId" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        

                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Identity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCnic" runat="server" Text='<%# Bind("CNIC") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>


                                </asp:GridView>



                            </ContentTemplate>

                            <Triggers>
                                <%--<asp:AsyncPostBackTrigger ControlID="SearchCIDModal"/>--%>
                            </Triggers>

                        </asp:UpdatePanel>





                    </div>
                    <div class="modal-footer">

                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>



  <div class="modal fade" id="SearchCifWho" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-body">
                      
                        <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="Panel3" runat="server"></asp:Panel>

                                <label class="lblReview">Search Data by IDENTITY:</label>
                                <asp:TextBox ID="txtWhoIdentityNo"  ClientIDMode="Static" CssClass="form-control" runat="server"  ></asp:TextBox>
                                <asp:Button ID="BtnWhoSearch" Style="margin-top: 5px" OnClick="BtnWhoSearch_Click" runat="server" CssClass="btn btn-primary" Text="Search" />

                                <asp:GridView class="table" PagerStyle-CssClass="bs-pagination" ID="GridWhoCif" runat="server"  AutoGenerateColumns="false">

                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                            <asp:LinkButton ID="Select" runat="server" CommandArgument='<%# Bind("ID") %>' Text="Select"  OnClientClick='<%# String.Format("return select_who_cif({0},\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\");",Eval("ID"),Eval("FIRST_NAME"),Eval("MIDDLE_NAME"),Eval("LAST_NAME"),Eval("CNIC"),Eval("PRIMARY_DOCUMENT_TYPE")) %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblId" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                        
                                        <asp:TemplateField HeaderText="First Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfName" runat="server" Text='<%# Bind("FIRST_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Middle Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbfmName" runat="server" Text='<%# Bind("MIDDLE_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbllName" runat="server" Text='<%# Bind("LAST_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Identity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdentity" runat="server" Text='<%# Bind("CNIC") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Identity Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIType" runat="server" Text='<%# Bind("PRIMARY_DOCUMENT_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                     

                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>                        
                       </asp:UpdatePanel>


                    </div>
                    <div class="modal-footer">

                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>

       



        <h3>ACCOUNT OPENING FORM - Entity</h3>
        <hr />
        <div class="row">
            <div class="col-md-3">
                <ul id="List1" class="nav nav-tabs-justified nav-menu">
                    <li id="accountNature"><a id="InaccountNatureAnchor" data-toggle="tab" href="#sectiona">A/C Nature & Currency</a></li>
                    <li id="ContactInformation" style="display: none"><a id="InContactInformationAnchor" data-toggle="tab" href="#sectionb">Contact Information</a></li>
                    <li id="AuthorizedPerson" style="display: none"><a id="InAuthorizedPersonAnchor" data-toggle="tab" href="#sectionc">Authorized Persons</a></li>
                    <li id="WhoAuthorized" style="display: none"><a id="InWhoAuthorizedAnchor" data-toggle="tab" href="#sectiond">Who Authorized</a></li>
                    <li id="OperatingInstruction" style="display: none"><a id="InOperatingInstructionAnchor" data-toggle="tab" href="#sectione">Operating Instructions</a></li>

                    <li id="KnowYourCustomer" style="display: none"><a id="InKnowYourCustomerAnchor" data-toggle="tab" href="#sectionf">Know Your Customer</a></li>

                    <li id="CertDepositInfo" style="display: none"><a id="InCertDepositInfoAnchor" data-toggle="tab" href="#sectiong">Certificate Deposit Info</a></li>
                    <li id="DocumentRequired" style="display: none"><a id="InDocumentRequiredAnchor" data-toggle="tab" href="#sectionh">Document Required</a></li>

                </ul>
            </div>
            <!--end of col-md-3-->


            <div class="col-md-6">
                <div class="tab-content">
                    <div id="sectiona" class="tab-pane fade in active">
                        <h3>Account Nature and Currency</h3>

                        <asp:CheckBox runat="server" ID="AcCnicVerifiedCheck" Text="1. Are all CNIC of authorized person(s), Shareholder(s) and beneficial owner(s) of customer verified" />
                        <br />
                        <div class="form-group" style="display: none">
                            <label class="lblReview">Account Opening Type</label>
                            <asp:DropDownList ID="AcListAccountOpen" Enabled="false" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="AcListAccountOpen_SelectedIndexChanged" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>

                        <div class="form-group" style="display: none">
                            <label class="lblReview">Account Entry Date</label>
                            <div class="input-group date-control">
                                <asp:TextBox ID="AcEntryDate" ClientIDMode="Static" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </button>
                                </span>
                            </div>
                        </div>

                        <div class="form-group" style="display: none">
                            <label class="lblReview">GL Code</label>
                            <asp:DropDownList ID="AcListGlCode" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>

                        <div class="form-group" style="display: none">
                            <label class="lblReview">SL Code</label>
                            <asp:DropDownList ID="AcListSlCode" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>

                        <asp:UpdatePanel ID="UpdatePanelAccountType" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <div class="form-group">
                                <label class="lblReview">Account Class:</label>
                                <asp:DropDownList ID="AcListAccountClass" AutoPostBack="true" ClientIDMode="Static" CssClass="form-control" runat="server" OnSelectedIndexChanged="AcListAccountClass_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label class="lblReview">Account Group:</label>
                                <asp:DropDownList ID="AcListAccountGroup" AutoPostBack="true" ClientIDMode="Static" CssClass="form-control" runat="server" OnSelectedIndexChanged="AcListAccountGroup_SelectedIndexChanged"></asp:DropDownList>
                            </div>

                            <div class="form-group">
                                <label class="lblReview">Account Type: *</label>
                                <asp:DropDownList ID="AcListAccountType" AutoPostBack="true" ClientIDMode="Static" CssClass="form-control" runat="server" OnSelectedIndexChanged="AcListAccountType_SelectedIndexChanged"></asp:DropDownList>
                                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorAccountType" ValidationGroup="AcValidationGroup" runat="server" ControlToValidate="AcListAccountType" ForeColor="Red" Font-Bold="true" ErrorMessage="Account Type is Required"></asp:RequiredFieldValidator>
                            </div>
                             <div class="form-group">
                                <label class="lblReview">Account Modes: *</label>
                                <asp:DropDownList ID="AcListAccountMode" ClientIDMode="Static" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AcListAccountMode_SelectedIndexChanged"></asp:DropDownList>
                                 <asp:RequiredFieldValidator Display="Dynamic" InitialValue="0" ID="RequiredFieldValidatorAccountMode" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="AcValidationGroup" ControlToValidate="AcListAccountMode" ErrorMessage="Account Mode is Required"></asp:RequiredFieldValidator>
                            </div>

                             <div class="control-group" style="display: none">
                            <label class="lblReview control-label">Account Mode:</label>
                            <div class="controls">   
                            <asp:RadioButton ID="AcAccountModeRadio1" Text="Single" Style="margin-right: 15px"  GroupName="AcAccountModeRadioGroup" runat="server" />
                            <asp:RadioButton ID="AcAccountModeRadio2" Text="Joint" Checked="True" GroupName="AcAccountModeRadioGroup" runat="server" />
                            </div>
                        </div>

                        </ContentTemplate>                       
                        </asp:UpdatePanel>

                        

                        <div class="form-group">
                            <label class="lblReview">Currency: *</label>
                            <asp:DropDownList ID="AcListCurrency" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator InitialValue="0" Display="Dynamic" ID="RequiredFieldValidatorCurrency" ValidationGroup="AcValidationGroup" runat="server" ControlToValidate="AcListCurrency" ForeColor="Red" Font-Bold="true" ErrorMessage="Currency is Required"></asp:RequiredFieldValidator>
                             <asp:CustomValidator  ID="CustomValidatorCurrency" Display="Dynamic" ForeColor="Red" Font-Bold="true" runat="server"  ErrorMessage="Currency Cant Be Pakistani Rs" ValidationGroup="AcValidationGroup" OnServerValidate="CustomValidatorCurrency_ServerValidate" ></asp:CustomValidator>
                        </div>

                        <div class="form-group" style="display: none">
                            <label class="lblReview">Account Number: *</label>
                            <asp:TextBox ID="AcAccountNumber" ClientIDMode="Static" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                            <asp:RequiredFieldValidator Enabled="false" Display="Dynamic" ID="AcValidatorAccountNumber" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="AcValidationGroup" ControlToValidate="AcAccountNumber" ErrorMessage="Account Number is Required"></asp:RequiredFieldValidator>

                        </div>

                        <div class="form-group">
                            <label class="lblReview">Account Title: *</label>
                            <asp:TextBox ID="AcAccountTitle" ClientIDMode="Static" CssClass="form-control" runat="server" MaxLength="40"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="AcValidatorAccountTitle" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="AcValidationGroup" ControlToValidate="AcAccountTitle" ErrorMessage="Account Title is Required"></asp:RequiredFieldValidator>

                        </div>

                        <div class="form-group">
                            <label class="lblReview">Initial Deposit: *</label>
                            <asp:TextBox ID="AcInitialDeposit" ClientIDMode="Static" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="AcValidatorInitialDeposit" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="AcValidationGroup" ControlToValidate="AcInitialDeposit" ErrorMessage="Initial Deposit is Required"></asp:RequiredFieldValidator>

                        </div>
                        <div class="form-group" style="display: none">
                            <label class="lblReview">Product: *</label>
                             <asp:DropDownList ID="AcDdlProducts" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                           <%--  <asp:RequiredFieldValidator InitialValue="0" Display="Dynamic" ID="RequiredFieldValidatorProduct" ValidationGroup="AcValidationGroup" runat="server" ControlToValidate="AcDdlProducts" ForeColor="Red" Font-Bold="true" ErrorMessage="Product is Required"></asp:RequiredFieldValidator>--%>
                        </div>
                       

                        <div class="control-group" style="display: none">
                            <label class="lblReview  control-label">Minor Account:</label>

                            <asp:RadioButton ID="AcMinorAccountRadio1" Text="Yes" Style="margin-right: 15px"  GroupName="AcMinorAccountRadioGroup" runat="server" />
                            <asp:RadioButton ID="AcMinorAccountRadio2" Text="No" Checked="True" GroupName="AcMinorAccountRadioGroup" runat="server" />

                        </div>

                        <div class="form-group">
                            <asp:Button ID="AcSubmitButton" runat="server" Text="SAVE" CssClass="btn btn-primary" ValidationGroup="AcValidationGroup" OnClick="AcSubmitButton_Click" />
                             <asp:Button ID="btnUpdateAc" Style="display: none" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary" ValidationGroup="AcValidationGroup" OnClick="btnUpdateAc_Click" />
                             <asp:Button ID="btnSubmitACa" Visible="false"  ClientIDMode="Static" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmitACa_Click"/>

                        
                            
                            <button id="AcResetButton" onclick="openModal()" type="button" class="btn btn-primary" value="Reset">Reset</button>

                        </div>


                    </div>


                    <div id="sectionb" class="tab-pane fade">
                        <h3>Contact Information</h3>
                        <br />

                        <div class="form-group">
                             <asp:Button ID="CiSearchCifButton" style="margin-bottom: 5px; float:right" runat="server" Text="Search Entity CIF" data-toggle="modal" OnClientClick="return false;" data-target="#SearchBusinessCIF" CssClass="btn btn-primary" OnClick="CiSearchCifButton_Click"/>
                            <label class="lblReview">ID No: *</label>
                            <asp:TextBox ID="CiCustomerCif" Enabled="false" ClientIDMode="Static"  CssClass="form-control col-md-3" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="CiCustomerCifValidator" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="CiValidationGroup" ControlToValidate="CiCustomerCif" ErrorMessage="CIF No is Required"></asp:RequiredFieldValidator>
                           

                        </div>


                                   <div class="form-group">
                            <label class="lblReview">Name:</label>
                            <asp:TextBox ID="CiName" Enabled="false" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>
                                           <div class="form-group">
                            <label class="lblReview">National Tax No:</label>
                            <asp:TextBox ID="CiNationTaxNo"  ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>
                        <div class="form-group">
                            <label class="lblReview">Sales Tax No:</label>
                            <asp:TextBox ID="CiSalesTaxNo" Enabled="false" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>

                        <div class="form-group">
                            <label class="lblReview">Registration No:</label>
                            <asp:TextBox ID="CiRegistrationNo" Enabled="false" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>
                         <div class="form-group">
                            <label class="lblReview">Registration Issuing Agency:</label>
                            <asp:DropDownList ID="CiListRegistrationIssueAgency" Enabled="false" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                         <div class="form-group" style="display: none">
                            <label class="lblReview">Natue of Business:</label>
                            <asp:DropDownList ID="CiListNatureOfBusiness" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>

                        <h3>Address Information / Correspondence Address:</h3>

                        <br />
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Always">
                             <ContentTemplate>
			                    <div class="form-group">
                                            <label class="lblReview">Country: *</label>
                                            <asp:DropDownList ID="CiListCountry" ClientIDMode="Static" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="CiListCountry_SelectedIndexChanged"></asp:DropDownList>
                                             <asp:RequiredFieldValidator   Display="Dynamic" InitialValue="0" ID="CiRequiredFieldValidatorCountry" ValidationGroup="CiValidationGroup" runat="server" ControlToValidate="CiListCountry" ForeColor="Red" Font-Bold="true" ErrorMessage="Country is Required"></asp:RequiredFieldValidator>
                                        </div>
                                         <div class="form-group">
                                            <label class="lblReview">Provinces: *</label>
                                            <asp:DropDownList ID="CiListProvince" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                                             <asp:RequiredFieldValidator Display="Dynamic" InitialValue="0" ID="RequiredFieldValidatorProvince" ValidationGroup="CiValidationGroup" runat="server" ControlToValidate="CiListProvince" ForeColor="Red" Font-Bold="true" ErrorMessage="Province is Required"></asp:RequiredFieldValidator>
                                        </div>
                                         <div class="form-group">
                                            <label class="lblReview">City: *</label>
                                            <asp:DropDownList ID="CiListCity" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                                             <asp:RequiredFieldValidator Display="Dynamic" InitialValue="0" ID="RequiredFieldValidatorCity" ValidationGroup="CiValidationGroup" runat="server" ControlToValidate="CiListCity" ForeColor="Red" Font-Bold="true" ErrorMessage="City is Required"></asp:RequiredFieldValidator>
                                        </div>
			                </ContentTemplate>                              
                               </asp:UpdatePanel>
                        
                       
                        <div  class="form-group">
                            <label class="lblReview">Address Line 1: *</label>
                            <asp:TextBox ID="CiTxtBuilding" CssClass="form-control" runat="server" MaxLength="40"></asp:TextBox>
                             <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" ValidationGroup="CiValidationGroup" runat="server" ControlToValidate="CiTxtBuilding" ForeColor="Red" Font-Bold="true" ErrorMessage="Address Line 1 is Required"></asp:RequiredFieldValidator>
                        </div>
                         <div  class="form-group">
                            <label class="lblReview">Address Line 2:</label>
                            <asp:TextBox ID="CiTxtFloor" CssClass="form-control" runat="server" MaxLength="40"></asp:TextBox>
                        </div>
                         <div  class="form-group">
                            <label class="lblReview">Address Line 3:</label>
                            <asp:TextBox ID="CiTxtStreet" CssClass="form-control" runat="server" MaxLength="40"></asp:TextBox>
                        </div>
                        <div  class="form-group">
                            <label class="lblReview">District:</label>
                            <asp:TextBox ID="CiTxtDistrict" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                         <div class="form-group">
                            <label class="lblReview">P.O Box:</label>
                            <asp:TextBox ID="CiPoBox" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                       
                        <div class="form-group">
                            <label class="lblReview">Postal Code:</label>
                            <asp:TextBox ID="CiPostalCode" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                       
                        
                        <div class="form-group">
                            <label class="lblReview">Tel (Office):</label>
                            <asp:TextBox ID="CiTelOffice" TextMode="Number" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                             <asp:CustomValidator  ID="CustomValidatorContact" Display="Dynamic" ForeColor="Red" Font-Bold="true" ValidationGroup="CiValidationGroup" runat="server" ErrorMessage="Atleast One Contact No is Required"   ClientValidationFunction="doCustomValidateContact" OnServerValidate="CustomValidatorContact_ServerValidate" ></asp:CustomValidator>
                        </div>

                        <div class="form-group">
                            <label class="lblReview">Tel (Residence):</label>
                            <asp:TextBox ID="CiTelResidence" TextMode="Number" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>


                        <div class="form-group">
                            <label class="lblReview">Mobile No:</label>
                            <asp:TextBox ID="CiMobileNo" TextMode="Number" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>

                        <div class="form-group">
                            <label class="lblReview">Fax No:</label>
                            <asp:TextBox ID="CiFaxNo" TextMode="Number" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>
                        <div class="form-group">
                            <label class="lblReview">SMS Alert Required:</label>
                            <asp:DropDownList ID="CiListSmsAlert" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                          <div class="form-group">
                            <label class="lblReview">Web Address(URL):</label>
                            <asp:TextBox ID="CiWebAddress" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>
                        <br />
                        <h3>Add Emails</h3>
                        
                         <div class="form-group">
                            <label class="lblReview">Required E-Statement on Email:</label>
                            <asp:CheckBox runat="server" AutoPostBack="true" ID="CiRequiredEstateCheckbox" Text="YES" OnCheckedChanged="CiRequiredEstateCheckbox_CheckedChanged" />
                        </div>

                        <div class="form-group">
                            <label class="lblReview">Email:</label>
                            <asp:TextBox ID="CiEmail" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                            <asp:UpdatePanel ID="UpdatePanelCNICValidation" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                   <asp:RequiredFieldValidator Enabled="false" Display="Dynamic" ID="RequiredFieldValidatorEmail" ValidationGroup="CiValidationGroup" runat="server" ControlToValidate="CiEmail" ForeColor="Red" Font-Bold="true" ErrorMessage="Email is Required"></asp:RequiredFieldValidator>
                                </ContentTemplate>
                                <Triggers>
	                                <asp:AsyncPostBackTrigger ControlID="CiRequiredEstateCheckbox" EventName="CheckedChanged" />
                                </Triggers>
                             </asp:UpdatePanel>
                             
                        </div>                 

                        <h3 style="display: none">Group Information (If Applicable)</h3>
                         <div class="form-group" style="display: none">
                            <label class="lblReview">Group CIF No:</label>
                            <asp:TextBox ID="CiGroupCif" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>
                         <div class="form-group" style="display: none">
                            <label class="lblReview">Group Name:</label>
                            <asp:TextBox ID="CiGroupName" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>


                         <div class="form-group">
                            <asp:Button ID="CiSubmitButton" runat="server" Text="SAVE" CssClass="btn btn-primary" ValidationGroup="CiValidationGroup" OnClick="CiSubmitButton_Click" />

                                  <asp:Button ID="btnUpdateCi" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary" ValidationGroup="CiValidationGroup" OnClick="btnUpdateCi_Click" />
                             <asp:Button ID="btnSubmitACb" Visible="false"  ClientIDMode="Static" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmitACa_Click"/>


                            <button id="CiResetButton" onclick="openModal()" type="button" class="btn btn-primary" value="Reset">Reset</button>

                        </div>
                    </div>




                    <div id="sectionc" class="tab-pane fade">
                        <h3>Authorized Persons</h3>
                          <asp:Button ID="AuthSearchCifButton" runat="server" Style="margin-bottom: 5px; float: right" Text="Search CIF" data-toggle="modal" OnClientClick="return false;" data-target="#SearchCIF" CssClass="btn btn-primary" OnClick="AuthSearchCifButton_Click" />
                          <div class="form-group">

                            <label class="lblReview">CIF No: *</label>
                            <asp:TextBox ID="AuthCustomerCif" Enabled="false" ClientIDMode="Static" CssClass="form-control col-md-3" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="AuthCustomerCifValidator" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="AuthValidationGroup" ControlToValidate="AuthCustomerCif" ErrorMessage="Customer CIF No is Required"></asp:RequiredFieldValidator>                         
                        </div>


                        <div class="form-group">
                            <label class="lblReview">Customer Name: *</label>
                            <asp:TextBox ID="AuthCustomerName" Enabled="false" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="AuthCustomerNameValidator" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="AuthValidationGroup" ControlToValidate="AuthCustomerName" ErrorMessage="Customer Name is Required"></asp:RequiredFieldValidator>

                        </div>

                        <div class="form-group">
                            <label class="lblReview">Customer Identity: *</label>
                            <asp:TextBox ID="AuthCustomerCNIC" Enabled="false" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="AuthCustomerCNICValidator" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="AuthValidationGroup" ControlToValidate="AuthCustomerCNIC" ErrorMessage="Customer CNIC is Required"></asp:RequiredFieldValidator>

                        </div>


                        <div class="control-group">
                            <label class="lblReview control-label" >Applicant in Negative List:</label>
                            <div class="controls"> 
                            <asp:RadioButton ID="AuthApplicantNegativeRadio1" Text="Yes" Style="margin-right: 5px" Checked="True" GroupName="ApplicantNegativeRadioGroup" runat="server" />
                            <asp:RadioButton ID="AuthApplicantNegativeRadio2" Text="No" GroupName="ApplicantNegativeRadioGroup" runat="server" />
                            </div>
                        </div>

                        <div class="control-group">
                             <label class="lblReview control-label" >Power of Attorny:</label>
                             <div class="controls"> 
                            <asp:RadioButton ID="AuthPowerAttornyRadio1" Text="Yes" Checked="True" GroupName="PowerAttornyRadioGroup" runat="server" />
                            <asp:RadioButton ID="AuthPowerAttornyRadio2" Text="No" GroupName="PowerAttornyRadioGroup" runat="server" />
                            </div>
                        </div>

                          <div class="control-group">
                           <label class="lblReview control-label" >Signature Authority:</label>
                             <div class="controls"> 
                                <asp:RadioButton ID="AuthSignatureRadio1" Text="Yes" Checked="True" GroupName="SignatureRadioGroup" runat="server" />
                                <asp:RadioButton ID="AuthSignatureRadio2" Text="No" GroupName="SignatureRadioGroup" runat="server" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="lblReview">Applicant Status:</label>
                            <asp:DropDownList ID="AuthListApplicantStatus" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator InitialValue="0" Display="Dynamic" ID="RequiredFieldValidatorApplicantStatus" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="AuthValidationGroup" ControlToValidate="AuthListApplicantStatus" ErrorMessage="Applicant Status is Required"></asp:RequiredFieldValidator>                         
                        </div>
                          


                         <div class="row" >
                            <div class="col-lg-12">

                               <asp:UpdatePanel ID="UpdatePanelGridCifs" runat="server" UpdateMode="Always">
                                   <ContentTemplate>
                                     <asp:GridView class="table" ShowHeaderWhenEmpty="true"    ID="GridViewAccountCifs" runat="server"  AutoGenerateColumns="false">
                                         <Columns>
                                             <asp:TemplateField HeaderText="CIF NO">
                                            <ItemTemplate>
                                                 <asp:Label ID="CUSTOMER_CIF_NO" runat="server" Text='<%# Bind("CUSTOMER_CIF_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                     
                                        <asp:TemplateField HeaderText="NAME">
                                            <ItemTemplate>
                                                 <asp:Label ID="CUSTOMER_CIF_NAME" runat="server" Text='<%# Bind("CUSTOMER_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Identity No">
                                            <ItemTemplate>
                                                 <asp:Label ID="CUSTOMER_IDENTITY" runat="server" Text='<%# Bind("CUSTOMER_IDENTITY") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="NEGATIVE LIST">
                                            <ItemTemplate>
                                                <asp:Label ID="NEG_LIST" runat="server" Text='<%# Bind("NEG_LIST") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>  
                                        <asp:TemplateField HeaderText="POWER OF ATTORNY">
                                            <ItemTemplate>
                                                <asp:Label ID="POWER_OF_ATTORNY" runat="server" Text='<%# Bind("POWER_OF_ATTORNY") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SIGNATURE AUTHORITY">
                                            <ItemTemplate>
                                                <asp:Label ID="SIGNATURE_AUTHORITY" runat="server" Text='<%# Bind("SIGNATURE_AUTHORITY") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                       

                                        <asp:TemplateField HeaderText="APPLICANT STATUS">
                                            <ItemTemplate>
                                                <asp:Label ID="APPLICANT_SHARE" runat="server" Text='<%# Bind("APPLICANT_STATUS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button runat="server" ID="btnDelete" Text="DELETE" OnClick="btnDelete_Click"></asp:Button>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         </Columns>
                                     </asp:GridView>
                                   </ContentTemplate>
                                    <Triggers>
	                                    <asp:AsyncPostBackTrigger ControlID="btnGridAddCif" EventName="Click" />
                                    </Triggers>
                               </asp:UpdatePanel>
                                         
                            </div>

                            <asp:Button ID="btnGridAddCif" ValidationGroup="AuthValidationGroup" CausesValidation="true" CssClass="btn btn-primary" Style="float: right" OnClick="btnGridAddCif_Click"  runat="server" Text="Add CIF" />
                             <asp:CustomValidator Display="Dynamic" Font-Bold="true" ForeColor="Red" ID="CustomValidatorOneCustomers" ValidationGroup="AuValidationGroupt" runat="server" ErrorMessage="Atleast one Customer is required"  ClientValidationFunction="OneCustomer" EnableClientScript="true" OnServerValidate="CustomValidatorOneCustomers_ServerValidate" ></asp:CustomValidator>                                                                                 
                        </div>

                         <div class="form-group">
                            <asp:Button ID="AuthSubmitButton" runat="server" Text="SAVE" CssClass="btn btn-primary" ValidationGroup="AuValidationGroupt" OnClick="AuthSubmitButton_Click" />
                            <asp:Button ID="btnUpdateAuth" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary" ValidationGroup="AuValidationGroupt" OnClick="btnUpdateAuth_Click" />
                            <asp:Button ID="btnSubmitACc" Visible="false"  ClientIDMode="Static" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmitACa_Click"/>
                            <button id="AuthResetButton" onclick="openModal()" type="button" class="btn btn-primary" value="Reset">Reset</button>
                        </div>





                    </div>

                    <div id="sectiond" class="tab-pane fade">
                        <h3>Who Authorized</h3>
                        
                        <asp:Button ID="btnSearchCifWho" Text="SEARCH CIF"  data-toggle="modal" OnClientClick="return false;" data-target="#SearchCifWho" CssClass="btn btn-primary" Style="float: right; margin-bottom: 5px"  runat="server" />
                         <div class="form-group">
                            <label class="lblReview">CIF NO: *</label>
                            <asp:TextBox ID="WhoTxtCifNo" Enabled="false" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidatorWhoCif" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="ValidationGroupWho" ControlToValidate="WhoTxtCifNo" ErrorMessage="Cif No is Required"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group">
                            <label class="lblReview">Name: *</label>
                            <asp:TextBox ID="WhoTxtNAme" Enabled="false" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidatorWhoName" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="ValidationGroupWho" ControlToValidate="WhoTxtNAme" ErrorMessage="Identity Type is Required"></asp:RequiredFieldValidator>
                        </div>

                         <div class="form-group">
                            <label class="lblReview">Identity Type: *</label>
                            <asp:TextBox ID="WhoTxtIdentityType" Enabled="false" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidatorWhoIdentityType" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="ValidationGroupWho" ControlToValidate="WhoTxtIdentityType" ErrorMessage="Identity Type is Required"></asp:RequiredFieldValidator>
                        </div>

                          <div class="form-group">
                            <label class="lblReview">Identity No: *</label>
                            <asp:TextBox ID="WhoTxtIdentityNo" Enabled="false" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidatorIdentityNo" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="ValidationGroupWho" ControlToValidate="WhoTxtIdentityNo" ErrorMessage="Identity No is Required"></asp:RequiredFieldValidator>
                         </div>

                         <div class="form-group">
                            <label class="lblReview">Reference document No: *</label>
                            <asp:TextBox ID="WhoTxtRefNo"  ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidatorRefNo" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="ValidationGroupWho" ControlToValidate="WhoTxtRefNo" ErrorMessage="Reference document No is Required"></asp:RequiredFieldValidator>
                         </div>
                         <div class="form-group" >
                            <label class="lblReview">Reference document date: *</label>
                            <div class="input-group date-control">
                                <asp:TextBox ID="WhoTxtRefDate" ClientIDMode="Static" runat="server"  CssClass="form-control"></asp:TextBox>
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </button>
                                </span>
                            </div>
                        </div>
                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidatorRefDate" runat="server" ErrorMessage="Reference document date  must Be in DD-MM-YYYY FORMAT" ForeColor="Red" Font-Bold="true" ControlToValidate="WhoTxtRefDate" ValidationGroup="ValidationGroupWho" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)[0-9]{2}$"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator   ID="RequiredFieldValidatorRefDate"  runat="server" Display="Dynamic" ControlToValidate="WhoTxtRefDate" ForeColor="Red" Font-Bold="true" ValidationGroup="ValidationGroupWho" ErrorMessage="Reference document date is Required"></asp:RequiredFieldValidator>

                        <div class="row" >
                            <div class="col-lg-12">

                               <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always">
                                   <ContentTemplate>
                                     <asp:GridView class="table" ShowHeaderWhenEmpty="true"    ID="GridWhoCifs" runat="server"  AutoGenerateColumns="false">
                                         <Columns>
                                             <asp:TemplateField HeaderText="CIF NO">
                                            <ItemTemplate>
                                                 <asp:Label ID="CIF_NO" runat="server" Text='<%# Bind("CIF_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                             <asp:TemplateField HeaderText="NAME">
                                            <ItemTemplate>
                                                 <asp:Label ID="NAME" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                             <asp:TemplateField HeaderText="IDENTITY NO">
                                            <ItemTemplate>
                                                 <asp:Label ID="IDENTITY_NO" runat="server" Text='<%# Bind("IDENTITY_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                     
                                        <asp:TemplateField HeaderText="REFERENCE DOCUMENT NO">
                                            <ItemTemplate>
                                                <asp:Label ID="REFERENCE_DOCUMENT_NO" runat="server" Text='<%# Bind("REFERENCE_DOCUMENT_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="REFERENCE DOCUMENT DATE">
                                            <ItemTemplate>
                                                <asp:Label ID="REFERENCE_DOCUMENT_DATE" runat="server" Text='<%# Bind("REFERENCE_DOCUMENT_DATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                       
                                        
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button runat="server" ID="btnDeleteCifWho" Text="DELETE" OnClick="btnDeleteCifWho_Click"></asp:Button>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         </Columns>
                                     </asp:GridView>
                                   </ContentTemplate>
                                    <Triggers>
	                                    <asp:AsyncPostBackTrigger ControlID="WhoBtnAddGrid" EventName="Click" />
                                    </Triggers>
                               </asp:UpdatePanel>
                                         
                            </div>

                            <asp:Button ID="WhoBtnAddGrid" ValidationGroup="ValidationGroupWho" CausesValidation="true" CssClass="btn btn-primary" Style="float: right" OnClick="WhoBtnAddGrid_Click"  runat="server" Text="Add CIF" />    
                            <asp:CustomValidator Display="Dynamic" Font-Bold="true" ForeColor="Red" ID="CustomValidatorWHoOne" ValidationGroup="WHO" runat="server" ErrorMessage="Atleast one Customer is required"  ClientValidationFunction="OneCustomerWho" EnableClientScript="true" OnServerValidate="CustomValidatorWHoOne_ServerValidate" ></asp:CustomValidator>                                                                                                        
                        </div>

                        <div class="form-group">
                             <asp:Button ID="WhoBtnSave" runat="server" Text="SAVE" CssClass="btn btn-primary" ValidationGroup="WHO" OnClick="WhoBtnSave_Click" />
                            <asp:Button ID="WhoBtnUpdate" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary" ValidationGroup="WHO" OnClick="WhoBtnUpdate_Click"/>
                            <asp:Button ID="WhoBtnSubmit" Visible="false"  ClientIDMode="Static" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="WhoBtnSubmit_Click" />
                            <button id="WhoBtnReset" onclick="openModal()" type="button" class="btn btn-primary" value="Reset">Reset</button>
                        </div>
                       

                    </div>

                    <div id="sectione" class="tab-pane fade">
                        <h3>Operating Instrucitons</h3>

                        <asp:UpdatePanel ID="UpdatePanelOperatinInstructions" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                             <div class="form-group">
                            <label class="lblReview">Authority To Operate: *</label>
                            <asp:DropDownList ID="AuListAuthority" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator  ID="AuRequiredFieldValidatorListAuthority" InitialValue="0" runat="server" Display="Dynamic" ControlToValidate="AuListAuthority" ForeColor="Red" Font-Bold="true" ValidationGroup="AuValidationGroup" ErrorMessage="Authority To Operate is Required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Description (If Any Other):</label>
                            <asp:TextBox ID="AuDescriptionOther" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>

                        <div class="control-group">
                            <label class="lblReview control-label">Zakat Deduction:</label>
                            <div class="controls"> 
                            <asp:RadioButton ID="ZakatDeductionRadio1" Text="Yes" Style="margin-right: 15px"  GroupName="ZakatDeductionRadioGroup" runat="server" AutoPostBack="true" OnCheckedChanged="ZakatDeductionRadio1_CheckedChanged" />
                            <asp:RadioButton ID="ZakatDeductionRadio2" Text="No" Checked="True" GroupName="ZakatDeductionRadioGroup" runat="server" AutoPostBack="true" OnCheckedChanged="ZakatDeductionRadio1_CheckedChanged" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="lblReview">Zakat Exemption Type:</label>
                            <asp:DropDownList ID="AuListZakatExemption" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator Enabled="false"  ID="RequiredFieldValidatorAuZakatExemptionType" InitialValue="0" runat="server" Display="Dynamic" ControlToValidate="AuListZakatExemption" ForeColor="Red" Font-Bold="true" ValidationGroup="AuValidationGroup" ErrorMessage="Zakat Exemption Type is Required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Exemption Reason Detail:</label>
                            <asp:TextBox ID="AuExempReasonDetail" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>

                        <div class="form-group">
                            <label class="lblReview">Account Statement Frequency:</label>

                            <asp:RadioButtonList runat="server" ID="AuListAccountFrequenct"></asp:RadioButtonList>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Description (If Hold Mail):</label>
                            <asp:TextBox ID="AuDescrHoldMail" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>

                        <div class="control-group">
                            <label class="lblReview control-label">ATM Card Required:</label>
                            <div class="controls">  
                            <asp:RadioButton ID="AtmRequiredRadio1" Text="Yes" Style="margin-right: 15px"  AutoPostBack="true"  GroupName="AtmRequiredRadioGroup" runat="server" OnCheckedChanged="AtmRequiredRadio1_CheckedChanged" />
                            <asp:RadioButton ID="AtmRequiredRadio2" Text="No" Checked="True" AutoPostBack="true" GroupName="AtmRequiredRadioGroup" runat="server" OnCheckedChanged="AtmRequiredRadio1_CheckedChanged" />
                            </div>
                        </div>


                        <div class="form-group">
                            <label class="lblReview">Customer Name on ATM Card:</label>
                            <asp:TextBox ID="AuCustomerNameAtm" MaxLength="15" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                             <asp:RequiredFieldValidator Enabled="false"  ID="RequiredFieldValidatorAtmName"  runat="server" Display="Dynamic" ControlToValidate="AuCustomerNameAtm" ForeColor="Red" Font-Bold="true" ValidationGroup="AuValidationGroup" ErrorMessage="Customer Name on ATM Card is Required"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group">
                            <label class="lblReview">E-Statement Required:</label>
                            <asp:DropDownList ID="AuListEstatement" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                        <div class="control-group">
                            <label class="lblReview control-label">Mobile Banking Required:</label>
                            <div class="controls">       
                            <asp:RadioButton ID="MobileBankRequirRadio1" Text="Yes" Style="margin-right: 15px"   GroupName="MobileBankRequirRadioGroup" runat="server" AutoPostBack="true" OnCheckedChanged="MobileBankRequirRadio1_CheckedChanged" />
                            <asp:RadioButton ID="MobileBankRequirRadio2" Text="No" Checked="True" GroupName="MobileBankRequirRadioGroup" runat="server" AutoPostBack="true" OnCheckedChanged="MobileBankRequirRadio1_CheckedChanged" />
                            </div>
                        </div>


                        <div class="form-group">
                            <label class="lblReview">Mobile No (Mobile Banking):</label>
                            <asp:TextBox ID="AuMobileNo" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Enabled="false"  ID="RequiredFieldValidatorAuMobile"  runat="server" Display="Dynamic" ControlToValidate="AuMobileNo" ForeColor="Red" Font-Bold="true" ValidationGroup="AuValidationGroup" ErrorMessage="Mobile No is Required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="control-group">
                            <label class="lblReview control-label">IBT Allowed:</label>
                            <div class="controls"> 
                            <asp:RadioButton ID="IBTAllowRadio1" Text="Yes" Style="margin-right: 15px"   GroupName="IBTAllowRadioGroup" runat="server" />
                            <asp:RadioButton ID="IBTAllowRadio2" Text="No" GroupName="IBTAllowRadioGroup" Checked="True" runat="server" />
                            </div>
                        </div>

                       <div class="control-group">
                            <label class="lblReview control-label">Is Profit Applicable:</label>
                           <div class="controls"> 
                            <asp:RadioButton ID="IsProfitAppRadio1" Text="Yes" Style="margin-right: 15px"   GroupName="IsProfitAppRadioGroup" runat="server" />
                            <asp:RadioButton ID="IsProfitAppRadio2" Text="No" GroupName="IsProfitAppRadioGroup" Checked="True" runat="server" />
                               </div>
                        </div>

                       <div class="control-group">
                            <label class="lblReview control-label">Is FED Exempted:</label>
                           <div class="controls"> 
                            <asp:RadioButton ID="IsFedRadio1" Text="Yes" Style="margin-right: 15px"   GroupName="IsFedRadioGroup" runat="server" AutoPostBack="true" OnCheckedChanged="IsFedRadio1_CheckedChanged" />
                            <asp:RadioButton ID="IsFedRadio2" Text="No" Checked="True" GroupName="IsFedRadioGroup" runat="server" AutoPostBack="true" OnCheckedChanged="IsFedRadio1_CheckedChanged" />
                               </div>
                        </div>

                        <div class="form-group">
                            <label class="lblReview">Expiry Date (If Exempted): </label>
                            <div class="input-group date-control">
                                <asp:TextBox ID="AuExpDateExempted" ClientIDMode="Static" runat="server"  CssClass="form-control"></asp:TextBox>
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </button>
                                </span>
                            </div>
                        </div>
                         <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidatorExpiryFed" runat="server" ErrorMessage="Expiry Date  must Be in DD-MM-YYYY FORMAT" ForeColor="Red" Font-Bold="true" ControlToValidate="AuExpDateExempted" ValidationGroup="AuValidationGroup" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)[0-9]{2}$"></asp:RegularExpressionValidator>
                         <asp:RequiredFieldValidator Enabled="false"  ID="RequiredFieldValidatorExpiryFed"  runat="server" Display="Dynamic" ControlToValidate="AuExpDateExempted" ForeColor="Red" Font-Bold="true" ValidationGroup="AuValidationGroup" ErrorMessage="Expiry Date is Required"></asp:RequiredFieldValidator>

                        <div class="form-group">
                            <label class="lblReview">Applicable Profit Rate:</label>

                            <asp:RadioButton ID="AppProfitRateRadio1" Text="Bank Rate" Style="margin-right: 15px"  Checked="True" GroupName="AppProfitRateRadioGroup" runat="server" />
                            <asp:RadioButton ID="AppProfitRateRadio2" Text="Special Rate" GroupName="AppProfitRateRadioGroup" runat="server" />

                        </div>

                        <div class="form-group">
                            <label class="lblReview">Special Profit Rate Value (in %age):</label>
                            <asp:TextBox ID="AuSpecicalProfitValue" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>

                        <div class="form-group" style="display: none">
                            <label class="lblReview">Profit Payment: *</label>
                            <asp:DropDownList ID="AuListProfitPayment" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                         <%--     <asp:RequiredFieldValidator  ID="RequiredFieldValidatorAuProfitPayment" InitialValue="0" runat="server" Display="Dynamic" ControlToValidate="AuListProfitPayment" ForeColor="Red" Font-Bold="true" ValidationGroup="AuValidationGroup" ErrorMessage="Profit Payment is Required"></asp:RequiredFieldValidator> --%>
                        </div>


                        <div class="control-group">
                            <label class="lblReview control-label" >WHT Deducted on Profit:</label>
                            <div class="controls">  
                            <asp:RadioButton ID="WhtProfitRadio1" Text="Yes" Style="margin-right: 15px"   GroupName="WhtProfitRadioGroup" runat="server" AutoPostBack="true" OnCheckedChanged="WhtProfitRadio1_CheckedChanged" />
                            <asp:RadioButton ID="WhtProfitRadio2" Text="No" Checked="True" GroupName="WhtProfitRadioGroup" runat="server" AutoPostBack="true" OnCheckedChanged="WhtProfitRadio1_CheckedChanged" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Expiry Date (If No): </label>
                            <div class="input-group date-control">
                                <asp:TextBox ID="AuExpDateProfit" ClientIDMode="Static" runat="server"  CssClass="form-control"></asp:TextBox>
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </button>
                                </span>
                            </div>
                        </div>
                         <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidatorExpProfit" runat="server" ErrorMessage="Expiry Date  must Be in DD-MM-YYYY FORMAT" ForeColor="Red" Font-Bold="true" ControlToValidate="AuExpDateProfit" ValidationGroup="AuValidationGroup" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)[0-9]{2}$"></asp:RegularExpressionValidator>
                         <asp:RequiredFieldValidator Enabled="false"  ID="RequiredFieldValidatorExpProfit"  runat="server" Display="Dynamic" ControlToValidate="AuExpDateProfit" ForeColor="Red" Font-Bold="true" ValidationGroup="AuValidationGroup" ErrorMessage="Expiry Date is Required"></asp:RequiredFieldValidator>

                        <div class="control-group">
                            <label class="lblReview control-label" >WHT Deducted on Transactions:</label>
                            <div class="controls">   
                            <asp:RadioButton ID="WhtTransactionRadio1" Text="Yes" Style="margin-right: 15px"   GroupName="WhtTransactionRadioGroup" runat="server" AutoPostBack="true" OnCheckedChanged="WhtTransactionRadio1_CheckedChanged" />
                            <asp:RadioButton ID="WhtTransactionRadio2" Text="No" Checked="True" GroupName="WhtTransactionRadioGroup" runat="server" AutoPostBack="true" OnCheckedChanged="WhtTransactionRadio1_CheckedChanged" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Expiry Date (If No): </label>
                            <div class="input-group date-control">
                                <asp:TextBox ID="AuExpDateTrans" ClientIDMode="Static" runat="server"  CssClass="form-control"></asp:TextBox>
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </button>
                                </span>
                            </div>
                        </div>
                         <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidatorExpTans" runat="server" ErrorMessage="Expiry Date  must Be in DD-MM-YYYY FORMAT" ForeColor="Red" Font-Bold="true" ControlToValidate="AuExpDateTrans" ValidationGroup="AuValidationGroup" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)[0-9]{2}$"></asp:RegularExpressionValidator>
                         <asp:RequiredFieldValidator Enabled="false"  ID="RequiredFieldValidatorExpTrans"  runat="server" Display="Dynamic" ControlToValidate="AuExpDateTrans" ForeColor="Red" Font-Bold="true" ValidationGroup="AuValidationGroup" ErrorMessage="Expiry Date is Required"></asp:RequiredFieldValidator>
                        </ContentTemplate>
                        </asp:UpdatePanel>

                       
                        <div class="form-group">
                            <asp:Button ID="AuSubmitButton" runat="server" Text="SAVE" CssClass="btn btn-primary" ValidationGroup="AuValidationGroup" OnClick="AuSubmitButton_Click" />
                                 <asp:Button ID="btnUpdateAu" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary" ValidationGroup="AuValidationGroup" OnClick="btnUpdateAu_Click" />
                             <asp:Button ID="btnSubmitACe" Visible="false"  ClientIDMode="Static" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmitACa_Click"/>
                            <button id="AuResetButton" onclick="openModal()" type="button" class="btn btn-primary" value="Reset">Reset</button>

                        </div>

                    </div>

                    <div id="sectionf" class="tab-pane fade">
                        <h3>Know Your Customer</h3>

                        <asp:UpdatePanel ID="UpdatePanelKnowYourCustomer" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                 <div class="form-group">
                            <label class="lblReview">Customer Type: *</label>
                            <asp:DropDownList ID="KnListCustomerType" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator EnableClientScript="True"  ID="RequiredFieldValidatorKnCustomerType" ControlToValidate="KnListCustomerType" InitialValue="0" ErrorMessage="Customer Type is Required" runat="server" Display="Dynamic"  ForeColor="Red" Font-Bold="true" ValidationGroup="KnValidationGroup" ></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Description (If Referred):</label>
                            <asp:TextBox ID="KnDescrIfRefered" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>
                        <div class="form-group" style="display: none">
                            <label class="lblReview">Education:</label>
                            <asp:DropDownList ID="KnListEducation" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                           <%--  <asp:RequiredFieldValidator Enabled="false"  ID="RequiredFieldValidatorEducation" ControlToValidate="KnListEducation" InitialValue="0" ErrorMessage="Education is Required" runat="server" Display="Dynamic"  ForeColor="Red" Font-Bold="true" ValidationGroup="KnValidationGroup" ></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Purpose of Account: *</label>
                            <div style="overflow-x: hidden; overflow-y: auto; border: 1px #808080 solid; max-height: 215px; height: auto; height: 215px">
                                <asp:CheckBoxList ID="KnListPurposeOfAccount" AutoPostBack="true" ClientIDMode="Static" runat="server" RepeatColumns="2" OnSelectedIndexChanged="KnListPurposeOfAccount_SelectedIndexChanged"></asp:CheckBoxList>                                
                            </div>
                            <asp:CustomValidator  runat="server" ID="RequiredValidatorPOA"
                                    ClientValidationFunction="REQPOA" 
                                     Display="Dynamic" ForeColor="Red" Font-Bold="true" ValidationGroup="KnValidationGroup"
                                    ErrorMessage="Purpose of Account is Required" EnableClientScript="True"></asp:CustomValidator>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Description (If Other):</label>
                            <asp:TextBox ID="KnDescrOther" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                             <asp:RequiredFieldValidator Enabled="false" EnableClientScript="True"  Display="Dynamic" ID="ReqValidatorPurposeAccountOther" ControlToValidate="KnDescrOther" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="Description (If Other) is Required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Source of Funds: *</label>
                            <div style="overflow-x: hidden; overflow-y: auto; border: 1px #808080 solid; max-height: 215px; height: auto; height: 215px">
                                <asp:CheckBoxList ID="KnListSourceOfFunds" ClientIDMode="Static" runat="server" RepeatColumns="2" AutoPostBack="true" OnSelectedIndexChanged="KnListSourceOfFunds_SelectedIndexChanged"></asp:CheckBoxList>                                
                            </div>
                            <asp:CustomValidator  runat="server" ID="ReqValidatorSOF"
                                    ClientValidationFunction="REQSOF" 
                                     Display="Dynamic" ForeColor="Red" Font-Bold="true" ValidationGroup="KnValidationGroup"
                                    ErrorMessage="Source of Fund is Required" EnableClientScript="True"></asp:CustomValidator>
                            <%--<asp:DropDownList ID="KnListSourceOfFunds" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                              <asp:RequiredFieldValidator InitialValue="0" Display="Dynamic" ID="RequiredFieldValidatorSourceFunds" ControlToValidate="KnListSourceOfFunds" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="Source of Funds is Required"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Description (if Other):</label>
                            <asp:TextBox ID="KnDescrOfSource" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                             <asp:RequiredFieldValidator Enabled="false" EnableClientScript="True"  Display="Dynamic" ID="ReqValidatorSourceDesc" ControlToValidate="KnDescrOfSource" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="Description (If Other) is Required"></asp:RequiredFieldValidator>
                        </div>
                      <div class="control-group">
                           <label class="lblReview control-label" >Service Charges Exempted:</label>
                          <div class="controls"> 
                            <asp:RadioButton ID="ServiceExemptedRadio1" Text="Yes" Style="margin-right: 15px" GroupName="ServiceExemptedRadioGroup" runat="server" AutoPostBack="true" OnCheckedChanged="ServiceExemptedRadio1_CheckedChanged" />
                            <asp:RadioButton ID="ServiceExemptedRadio2" Text="No" Checked="True"  GroupName="ServiceExemptedRadioGroup" runat="server"  AutoPostBack="true" OnCheckedChanged="ServiceExemptedRadio1_CheckedChanged" />
                        </div>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Service Charges Exempt Code:</label>
                            <asp:DropDownList ID="KnListSerExemptCode" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator Enabled="false" ID="RequiredFieldValidatorExemptCode" ControlToValidate="KnListSerExemptCode" InitialValue="0" ErrorMessage="Service Charges Exempt Code is Required" runat="server" Display="Dynamic"  ForeColor="Red" Font-Bold="true" ValidationGroup="KnValidationGroup" ></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Reason (If Exempted):</label>
                            <asp:TextBox ID="KnReasonExempted" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>

                        <div class="form-group">
                            <label class="lblReview">Expected Monthly Income:</label>
                            <asp:TextBox ID="KnExpectedMonthlyIncome" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>

                       <div class="form-group">
                            <label class="lblReview">Normal Mode of Transactions: *</label>
                            <asp:CheckBoxList ID="KnListModeOfTransaction" runat="server" RepeatColumns="2" AutoPostBack="true" OnSelectedIndexChanged="KnListModeOfTransaction_SelectedIndexChanged"></asp:CheckBoxList>
                             <asp:CustomValidator EnableClientScript="True" runat="server" ID="RequiredValidatorMOT"
                                    ClientValidationFunction="ValidateModuleList" 
                                     Display="Dynamic" ForeColor="Red" Font-Bold="true" ValidationGroup="KnValidationGroup"
                                    ErrorMessage="Please Select Atleast one Transaction"></asp:CustomValidator>
                        </div>

                        <div class="form-group">
                            <label class="lblReview">Description (if Other):</label>
                            <asp:TextBox ID="KnOtherModeTrans" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                             <asp:RequiredFieldValidator Enabled="false"  Display="Dynamic" ID="ReqValidatorMotOther" ControlToValidate="KnOtherModeTrans" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="Description (If Other) is Required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group" style="display: none">
                            <label class="lblReview">MAX Transaction Amount DR:</label>
                            <asp:TextBox ID="KnMaxAmountDR" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>
                        <div class="form-group" style="display: none">
                            <label class="lblReview">MAX Transaction Amount CR:</label>
                            <asp:TextBox ID="KnMaxAmountCR" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>
                        <div class="form-group" style="display: none">
                            <label class="lblReview"> Manager:</label>
                            <asp:DropDownList ID="KnddlManager" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                          <%--  <asp:RequiredFieldValidator Enabled="false" ID="RequiredFieldValidatorKnManager" ControlToValidate="KnddlManager" InitialValue="0" ErrorMessage="Manager is Required" runat="server" Display="Dynamic"  ForeColor="Red" Font-Bold="true" ValidationGroup="KnValidationGroup" ></asp:RequiredFieldValidator>--%>

                        </div>
                        <div class="form-group" style="display: none">
                            <label class="lblReview">Relationship Manager:</label>
                            <asp:TextBox ID="KnRelationshipManager" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>
                        <div class="control-group" style="display: none">
                            <label class="lblReview control-label">Occupation Verified:</label>
                            <div class="controls"> 
                            <asp:RadioButton ID="OccupyVerifyRadio1" Text="Yes"  Style="margin-right: 15px"  GroupName="OccupyVerifyRadioGroup" runat="server" />
                            <asp:RadioButton ID="OccupyVerifyRadio2" Text="No" Checked="True" GroupName="OccupyVerifyRadioGroup" runat="server" />
                            </div>
                        </div>
                          <div class="control-group" style="display: none">
                             <label class="lblReview control-label">Address Verified:</label>
                               <div class="controls"> 
                            <asp:RadioButton ID="RadioButtonAddressVerifiedYes" Text="Yes"  Style="margin-right: 15px"  GroupName="AddressVerified" runat="server" />
                            <asp:RadioButton ID="RadioButtonAddressVerifiedNo" Text="No" Checked="True" GroupName="AddressVerified" runat="server" />
                                </div>
                        </div>
                        <div class="form-group" style="display: none">
                            <label class="lblReview">Address Verified:</label>
                            <asp:DropDownList ID="KnListAddresVerified" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                        <div class="form-group" style="display: none">
                            <label class="lblReview">Means Of Verification:</label>
                            <asp:DropDownList ID="KnListMeansOfVerification" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                            <%--<asp:RequiredFieldValidator  ID="RequiredFieldValidatorKnMeanVerification" ControlToValidate="KnListMeansOfVerification" InitialValue="0" ErrorMessage="Means Of Verification is Required" runat="server" Display="Dynamic"  ForeColor="Red" Font-Bold="true" ValidationGroup="KnValidationGroup" ></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="form-group" style="display: none">
                            <label class="lblReview">Means of Verification(Other):</label>
                            <asp:TextBox ID="KnMeanVerifyOther" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>
                        <div class="control-group" style="display: none">
                            <label class="lblReview control-label" >Is Verification Satisfactory:</label>
                            <div class="controls">  
                            <asp:RadioButton ID="IsVeriSatiRadio1" Text="Yes" Style="margin-right:15px"  GroupName="IsVeriSatiRadioGroup" runat="server" />
                            <asp:RadioButton ID="IsVeriSatiRadio2" Text="No" Checked="True" GroupName="IsVeriSatiRadioGroup" runat="server" />
                            </div>
                        </div>
                        <div class="form-group" style="display: none">
                            <label class="lblReview">Detail (If not Satisfactory):</label>
                            <asp:TextBox ID="KnDetailNotSatis" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>
                        <div class="form-group" style="display: none">
                            <label class="lblReview">Country (Home Remittance):</label>
                            <asp:DropDownList ID="KnListCountHomeRemit" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                           <%-- <asp:RequiredFieldValidator  ID="RequiredFieldValidatorCountyHRemitance" ControlToValidate="KnListCountHomeRemit" InitialValue="0" ErrorMessage="Country  is Required" runat="server" Display="Dynamic"  ForeColor="Red" Font-Bold="true" ValidationGroup="KnValidationGroup" ></asp:RequiredFieldValidator>--%>
                        </div>
                         <div class="form-group">
                            <label class="lblReview">Expected Monthly No Of Debit Transactions: *</label>
                            <asp:TextBox ID="KnNODT" TextMode="Number" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorNODT" ControlToValidate="KnNODT" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="No Of Debit Transactions is Required"></asp:RequiredFieldValidator> 
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Expected Monthly Debit Amount – PKR Equivalent: *</label>
                            <asp:TextBox ID="KnPEDT" TextMode="Number" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorPEDT" ControlToValidate="KnPEDT" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="PKR Equivalent Debit Transactions is Required"></asp:RequiredFieldValidator> 
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Expected Monthly No of Credit Transactions: **</label>
                            <asp:TextBox ID="KnNOCT" TextMode="Number" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorNOCT" ControlToValidate="KnNOCT" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="No of Credit Transactions is Required"></asp:RequiredFieldValidator> 
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Expected Monthly Credit Amount – PKR Equivalent: *</label>
                            <asp:TextBox ID="KnPECT" TextMode="Number" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorPECT" ControlToValidate="KnPECT" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="PKR Equivalent Credit Transactions is Required"></asp:RequiredFieldValidator> 
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Expected Types of Counter Parties: *</label>
                            <div style="overflow-x: hidden; overflow-y: auto; border: 1px #808080 solid; max-height: 215px; height: auto; height: 215px">
                                <asp:CheckBoxList AutoPostBack="true" ID="KnListECP" runat="server" RepeatColumns="2" OnSelectedIndexChanged="KnListECP_SelectedIndexChanged" ></asp:CheckBoxList>
                            </div>
                             <asp:CustomValidator runat="server" ID="CustomValidatorECP"
                                    ClientValidationFunction="ValidateModuleListECP" 
                                     Display="Dynamic" ForeColor="Red" Font-Bold="true" ValidationGroup="KnValidationGroup"
                                    ErrorMessage="Expected Types of Counter Parties is Required"></asp:CustomValidator>
                        </div>

                         <div class="form-group">
                            <label class="lblReview">Description (if Other):</label>
                                <asp:TextBox ID="KntxtDescETCP"  ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                               <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorDescETCP" Enabled="false" ControlToValidate="KntxtDescETCP" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="Description (if Other) is Required"></asp:RequiredFieldValidator> 
                         </div>

                         <div class="form-group">
                            <label class="lblReview">Geographies Involved of Counter Parties: *</label>
                            <div style="overflow-x: hidden; overflow-y: auto; border: 1px #808080 solid; max-height: 215px; height: auto; height: 215px">
                                <asp:CheckBoxList ID="KnListGCP" AutoPostBack="true" runat="server" RepeatColumns="2" OnSelectedIndexChanged="KnListGCP_SelectedIndexChanged" ></asp:CheckBoxList>
                            </div>
                             <asp:CustomValidator runat="server" ID="CustomValidatorGCP"
                                    ClientValidationFunction="ValidateModuleListGCP" 
                                     Display="Dynamic" ForeColor="Red" Font-Bold="true" ValidationGroup="KnValidationGroup"
                                    ErrorMessage="Geographies Involved of Counter Parties is Required"></asp:CustomValidator>
                            </div>

                         <div class="form-group">
                            <label class="lblReview">Description (If Outside Pakistan):</label>
                                <asp:TextBox ID="KntxtDescGCP"  ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                               <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorDescGCP" Enabled="false" ControlToValidate="KntxtDescGCP" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="Description (if Other) is Required"></asp:RequiredFieldValidator> 
                         </div>
                        <div class="form-group">
                            <label class="lblReview">Real Beneficiery of A/C: *</label>
                            <asp:DropDownList ID="KnListRealBenef" ClientIDMode="Static" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="KnListRealBenef_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator  ID="RequiredFieldValidatorKnRealBenfc" EnableClientScript="True" ControlToValidate="KnListRealBenef" InitialValue="0" ErrorMessage="Real Beneficiery of A/C is Required" runat="server" Display="Dynamic"  ForeColor="Red" Font-Bold="true" ValidationGroup="KnValidationGroup" ></asp:RequiredFieldValidator>

                        </div>
                        <div class="form-group">
                            <label class="lblReview">Name (If Other):</label>
                            <asp:TextBox ID="KnNameOther" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator   Display="Dynamic" ID="RequiredFieldValidatorBName" ControlToValidate="KnNameOther" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="ENTITY" ErrorMessage="Name is Required" ></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Identity Document Type:</label>
                            <asp:DropDownList ID="knListDocType" ClientIDMode="Static" CssClass="form-control" runat="server" ></asp:DropDownList>
                            <asp:RequiredFieldValidator  InitialValue="0" Display="Dynamic" ID="RequiredFieldValidatorDocType" ControlToValidate="knListDocType" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="ENTITY" ErrorMessage="Identity Document Type is Required" ></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Identity Number:</label>
                            <asp:TextBox ID="KnCnicOther" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidatorIdNumber" ControlToValidate="KnCnicOther" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="ENTITY" ErrorMessage="Identity Number is Required" ></asp:RequiredFieldValidator>
                        </div>
                         <div class="form-group">
                            <label class="lblReview">Expiry Date: (DD-MM-YYYY)</label>
                            <asp:TextBox ID="KntxtExpiry" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidatorExpiry" ControlToValidate="KntxtExpiry" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="ENTITY" ErrorMessage="Expiry Date is Required" ></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidatorExpiry" runat="server" ErrorMessage="Date of Birth must Be in DD-MM-YYYY FORMAT" ForeColor="Red" Font-Bold="true" ControlToValidate="KntxtExpiry" ValidationGroup="ENTITY" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)[0-9]{2}$"></asp:RegularExpressionValidator>
                         </div>
                        <div class="form-group">
                            <label class="lblReview">Place Of Birth:</label>
                            <asp:TextBox ID="KnTxtPOB" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                             <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidatorPOB" ControlToValidate="KnTxtPOB" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="ENTITY" ErrorMessage="Place of Birth is Required" ></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">US Person / Citizen:</label>
                             <asp:DropDownList ID="knListUs" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator  InitialValue="0" Display="Dynamic" ID="RequiredFieldValidatorUS" ControlToValidate="knListUs" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="ENTITY" ErrorMessage="US Person / Citizen is Required" ></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Percentage Ownsership:</label>
                            <asp:TextBox ID="KnTxtPercentageOwn" TextMode="Number" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                             <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidatorPOWN" ControlToValidate="KnTxtPercentageOwn" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="ENTITY" ErrorMessage="Percentage Ownsership is Required" ></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group">
                            <label class="lblRAC">Reason of Opening Account with NBP: *</label>
                            <asp:DropDownList ID="KnListRAC"  ClientIDMode="Static" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="KnListRAC_SelectedIndexChanged"></asp:DropDownList>
                             <asp:RequiredFieldValidator InitialValue="0" Display="Dynamic" ID="RequiredFieldValidatorRAC" ControlToValidate="KnListRAC" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="ENTITY" ErrorMessage="Reason of Opening Account with NBP is Required" ></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label class="lblTxtRAC">Details (If Other):</label>
                            <asp:TextBox ID="knTextRACDetail" ClientIDMode="Static" CssClass="form-control" runat="server" ></asp:TextBox>
                             <asp:RequiredFieldValidator Enabled="false" Display="Dynamic" ID="RequiredFieldValidatorRACDetail" ControlToValidate="knTextRACDetail" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="ENTITY" ErrorMessage="Detail is Required"></asp:RequiredFieldValidator>
                        </div>

                        <div class="row">
                            <div class="col-lg-12">
                                 <asp:GridView class="table" ShowHeaderWhenEmpty="true"    ID="GrdBeneficial" runat="server"  AutoGenerateColumns="false">
                                         <Columns>
                                             <asp:TemplateField HeaderText="NAME">
                                            <ItemTemplate>
                                                 <asp:Label ID="NAME" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                     
                                      
                                        <asp:TemplateField HeaderText="IDENTITY DOCUMENT">
                                            <ItemTemplate>
                                                <asp:Label ID="IDENTITY_DOCUMENT" runat="server" Text='<%# Bind("IDENTITY_DOCUMENT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                       

                                        <asp:TemplateField HeaderText="IDENTITY NUMBER">
                                            <ItemTemplate>
                                                <asp:Label ID="IDENTITY_NUMBER" runat="server" Text='<%# Bind("IDENTITY_NUMBER") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EXPIRY DATE">
                                            <ItemTemplate>
                                                <asp:Label ID="EXPIRY_DATE" runat="server" Text='<%# Bind("EXPIRY_DATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="PLACE OF BIRTH">
                                            <ItemTemplate>
                                                <asp:Label ID="POB" runat="server" Text='<%# Bind("POB") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="US OWNER">
                                            <ItemTemplate>
                                                <asp:Label ID="US" runat="server" Text='<%# Bind("US") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="OWNERSHIP %">
                                            <ItemTemplate>
                                                <asp:Label ID="OWNERSHIP" runat="server" Text='<%# Bind("OWNERSHIP") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button runat="server" ID="btnDelete" Text="DELETE" OnClick="btnDelete_Click1"></asp:Button>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         </Columns>
                                     </asp:GridView>
                            </div>
                             <asp:Button ID="BtnAddBGrid" Visible="false" runat="server" Style="float: right" Text="ADD" ValidationGroup="ENTITY" CssClass="btn btn-primary" OnClick="BtnAddBGrid_Click" />
                             <asp:CustomValidator Enabled="false" Display="Dynamic" Font-Bold="true" ForeColor="Red" ID="CustomValidatorBeneficial" ValidationGroup="KnValidationGroup" runat="server" ErrorMessage="Atleast one Beneficial Owner is required"  ClientValidationFunction="OneBeneficial" EnableClientScript="true" ></asp:CustomValidator>                       
                        </div>

                            </ContentTemplate>
                            </asp:UpdatePanel>

                       

                        <div class="form-group">
                            <asp:Button ID="KnSubmitButton" runat="server" Text="SAVE" ValidationGroup="KnValidationGroup"   CssClass="btn btn-primary" OnClick="KnSubmitButton_Click" />
                             <asp:Button ID="btnUpdateKn" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CausesValidation="true" CssClass="btn btn-primary" ValidationGroup="KnValidationGroup" OnClick="btnUpdateKn_Click"/>
                             <asp:Button ID="btnSubmitACf" Visible="false"  ClientIDMode="Static" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmitACa_Click"/>



                            <button id="KnResetButton" onclick="openModal()" type="button" class="btn btn-primary" value="Reset">Reset</button>

                        </div>




                    </div>

                    <div id="sectiong" class="tab-pane fade">
                        <h3>Certificate Deposit Info</h3>
                        <br />
                         <div class="form-group">
                            <label class="lblReview">Certificate Period:</label>
                            <asp:TextBox ID="CdCertPeriod" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>
                        <div class="form-group">
                            <label class="lblReview">Expiry Date:</label>
                            <div class="input-group date-control">
                                <asp:TextBox ID="CdExpDate" ClientIDMode="Static" runat="server"  CssClass="form-control"></asp:TextBox>
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </button>
                                </span>
                            </div>
                        </div>

                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidatorExp" runat="server" ErrorMessage="Expiry Date must Be in DD-MM-YYYY FORMAT" ForeColor="Red" Font-Bold="true" ControlToValidate="CdExpDate" ValidationGroup="CdValidationGroup" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)[0-9]{2}$"></asp:RegularExpressionValidator>
                       
                         <div class="control-group">
                            <label class="lblReview control-label">Auto Roll Over On Expiry:</label>
                            <div class="controls"> 
                            <asp:RadioButton ID="AutoRollExpiryRadio1" Style="margin-right: 15px" Text="Yes" GroupName="AutoRollExpiryRadioGroup" runat="server" />
                            <asp:RadioButton ID="AutoRollExpiryRadio2" Text="No"  Checked="True" GroupName="AutoRollExpiryRadioGroup" runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Special Instructions (If Any):</label>
                            <asp:TextBox ID="CdSpecialInstr" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>
                        <div class="form-group">
                            <label class="lblReview">Profit Account Type:</label>
                            <asp:DropDownList ID="CdListProfitAccount" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                           <%-- <asp:RequiredFieldValidator  InitialValue="0" Display="Dynamic" ID="RequiredFieldValidatorCdProfitAccount" ControlToValidate="CdListProfitAccount" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="CdValidationGroup" ErrorMessage="Profit Account Type is Required"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Profit Account Number:</label>
                            <asp:TextBox ID="CdProfitAccountNumber" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                             <%-- <asp:RequiredFieldValidator   Display="Dynamic" ID="RequiredFieldValidatorProfitAccNumber" ControlToValidate="CdProfitAccountNumber" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="CdValidationGroup" ErrorMessage="Profit Account Number is Required"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="form-group" style="display: none">
                            <label class="lblReview">Transaction Type:</label>
                            <asp:DropDownList ID="CdListTransactionType" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                       <div class="form-group" style="display: none">
                            <label class="lblReview">Cheque Prefix:</label>
                            <asp:TextBox ID="CdChequePrefix" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>
                       <div class="form-group" style="display: none">
                            <label class="lblReview">Cheque Number:</label>
                            <asp:TextBox ID="ChChequeNumber" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>

                        <div class="form-group">
                            <label class="lblReview">Certificate Number:</label>
                            <asp:TextBox ID="CdCertNumber" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>

                        <div class="form-group">
                            <label class="lblReview">Certificate Amount:</label>
                            <asp:TextBox ID="CdCertAmount" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                             <%--  <asp:RequiredFieldValidator   Display="Dynamic" ID="RequiredFieldValidatorCertAmount" ControlToValidate="CdCertAmount" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="CdValidationGroup" ErrorMessage="Certificate Amount is Required"></asp:RequiredFieldValidator>--%>
                        </div>

                        <div class="form-group">
                            <label class="lblReview">Mark-up Rate:</label>
                            <asp:TextBox ID="CdMarkupRate" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                          <asp:RegularExpressionValidator ID="MarkupRegex1" ForeColor="Red" runat="server" ValidationGroup="CdValidationGroup" ValidationExpression="((\d+)((\.\d{1,2})?))$"
ErrorMessage="Please enter valid integer or decimal number with 2 decimal places."
ControlToValidate="CdMarkupRate"/>
                            <%--   <asp:RequiredFieldValidator   Display="Dynamic" ID="RequiredFieldValidatorCertAmount" ControlToValidate="CdCertAmount" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="CdValidationGroup" ErrorMessage="Certificate Amount is Required"></asp:RequiredFieldValidator>--%>
                        </div>

                        <div class="form-group">
                            <label  class="lblReview">Principal Renewal Option: </label>
                            <asp:DropDownList ID="CdLstPrincipalRenewal" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>

                        <div class="form-group">
                            <asp:Button ID="CdSubmitButton" runat="server" Text="SAVE" ValidationGroup="CdValidationGroup" CssClass="btn btn-primary" OnClick="CdSubmitButton_Click"/>

                                 <asp:Button ID="btnUpdateCd" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary" ValidationGroup="CdValidationGroup" OnClick="btnUpdateCd_Click" />
                             <asp:Button ID="btnSubmitACg" Visible="false"  ClientIDMode="Static" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmitACa_Click"/>


                            <button id="CdResetButton" onclick="openModal()" type="button" class="btn btn-primary" value="Reset">Reset</button>

                        </div>





                    </div>

                    <div id="sectionh" class="tab-pane fade">
                        <h3>Documents Required:</h3>

                        <asp:GridView class="table" ID="DcGrid" AutoGenerateColumns="false" runat="server" OnPageIndexChanging="DcGrid_PageIndexChanging" OnRowDataBound="DcGrid_RowDataBound">


                            <Columns>



                                <asp:TemplateField HeaderText="Sr No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerialNumber" runat="server" Text='<%# Bind("SERIAL_NO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Please Tick">
                                    <ItemTemplate>
                                        <div class="form-group" id="GridRadioGroup">

                                            <asp:RadioButton ID="DcRadio1" Text="Yes" GroupName="DcRadioGroup" runat="server"  />
                                            <asp:RadioButton ID="DcRadio2" Text="No" Checked="true" GroupName="DcRadioGroup" runat="server"  />
                                            <asp:RadioButton ID="DcRadio3" Text="N/A" GroupName="DcRadioGroup" runat="server" />

                                        </div>


                                    </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Description" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDcId" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>


                        </asp:GridView>

                           <div class="form-group">
                            <asp:Button ID="DrSubmitButton" runat="server" Text="SAVE" CssClass="btn btn-primary" OnClick="DrSubmitButton_Click"/>
                                    <asp:Button ID="btnUpdateDr" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary" ValidationGroup="DrValidationGroup" OnClick="btnUpdateDr_Click"/>
                             <asp:Button ID="btnSubmitACh" Visible="false"  ClientIDMode="Static" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmitACa_Click"/>


                            <button id="DrResetButton" onclick="openModal()" type="button" class="btn btn-primary" value="Reset">Reset</button>

                        </div>

                    </div>





                </div>

            </div>

        </div>
    </div>

    
    <Rev:Review ID="rev" runat="server" Type="true" Visible="false" />

    <script>

        function ValidateModuleListECP(source, args) {
            var chkListModules = document.getElementById('<%= KnListECP.ClientID %>');
            var chkListinputs = chkListModules.getElementsByTagName("input");
            for (var i = 0; i < chkListinputs.length; i++) {
                if (chkListinputs[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }

        function ValidateModuleListGCP(source, args) {
            var chkListModules = document.getElementById('<%= KnListGCP.ClientID %>');
    var chkListinputs = chkListModules.getElementsByTagName("input");
    for (var i = 0; i < chkListinputs.length; i++) {
        if (chkListinputs[i].checked) {
            args.IsValid = true;
            return;
        }
    }
    args.IsValid = false;
}

        function REQPOA(source, args) {
            var chkListModules = document.getElementById('<%= KnListPurposeOfAccount.ClientID %>');
             var chkListinputs = chkListModules.getElementsByTagName("input");
             for (var i = 0; i < chkListinputs.length; i++) {
                 if (chkListinputs[i].checked) {
                     args.IsValid = true;
                     return;
                 }
             }
             args.IsValid = false;
         }

         function REQSOF(source, args) {
             var chkListModules = document.getElementById('<%= KnListSourceOfFunds.ClientID %>');
             var chkListinputs = chkListModules.getElementsByTagName("input");
             for (var i = 0; i < chkListinputs.length; i++) {
                 if (chkListinputs[i].checked) {
                     args.IsValid = true;
                     return;
                 }
             }
             args.IsValid = false;
         }

        function ValidateModuleList(source, args) {
            var chkListModules = document.getElementById('<%= KnListModeOfTransaction.ClientID %>');
                    var chkListinputs = chkListModules.getElementsByTagName("input");
                    for (var i = 0; i < chkListinputs.length; i++) {
                        if (chkListinputs[i].checked) {
                            args.IsValid = true;
                            return;
                        }
                    }
                    args.IsValid = false;
                }

        function doCustomValidateContact(source, args) {

            args.IsValid = false;

            if (document.getElementById('<% =CiTelOffice.ClientID %>').value.length > 0) {
                 args.IsValid = true;
             }
             if (document.getElementById('<% =CiTelResidence.ClientID %>').value.length > 0) {
                 args.IsValid = true;
             }
             if (document.getElementById('<% =CiMobileNo.ClientID %>').value.length > 0) {
                 args.IsValid = true;
             }
        }

        function OneCustomer(source, args) {
            var Grid1 = document.getElementById("<%=GridViewAccountCifs.ClientID%>");
             if (Grid1 == null) {
                 args.IsValid = false;
             }
             else if (Grid1.rows.length < 2) {
                 args.IsValid = false;
             }
             else {
                 args.IsValid = true;
             }
        }

        function OneCustomerWho(source, args) {
            var Grid1 = document.getElementById("<%=GridWhoCifs.ClientID%>");
              if (Grid1 == null) {
                  args.IsValid = false;
              }
              else if (Grid1.rows.length < 2) {
                  args.IsValid = false;
              }
              else {
                  args.IsValid = true;
              }
        }

        function OneBeneficial(source, args) {
            var Grid1 = document.getElementById("<%=GrdBeneficial.ClientID%>");
               if (Grid1 == null) {
                   args.IsValid = false;
               }
               else if (Grid1.rows.length < 2) {
                   args.IsValid = false;
               }
               else {
                   args.IsValid = true;
               }
           }
    </script>
   

</asp:Content>

