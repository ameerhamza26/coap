<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.Master" CodeBehind="Account_Individual.aspx.cs" Inherits="CAOP.Account_Individual" %>

<%@ Register Src="~/UserControls/ReviewControl.ascx" TagName="Review" TagPrefix="Rev" %>


<asp:Content ID="Head" ContentPlaceHolderID="head" runat="server">
    <title>Account opening Portal</title>
    <script src="Assets/js/jquery.quicksearch.js"></script>

    <script type="text/javascript">
        function doPostBack(o) {
            __doPostBack(o.id, '');
        }

        function select_cif(a, b, c) {
            //alert("hw");
            // alert(a + " " + b + " " + c);

            document.getElementById("ApCustomerCif").value = a;
            document.getElementById("ApCustomerName").value = b;
            document.getElementById("ApCustomerCNIC").value = c;


    <%--        var grid1 = document.getElementById("<%= searchCIF.ClientID%>");  
            for (i = 1; i < grid1.rows.length ; i++) 
            {
                var cellValue = grid1.rows[i].cells[3].value;
                alert(cellValue);
            }--%>



             $('#SearchCIF').modal('hide');
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
        <div class="modal fade" id="SearchCIF" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">




                    <div class="modal-body">

                        
                        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="Panel1" runat="server"></asp:Panel>

                                <label class="lblReview">Search Data By Identity:</label>
                        <asp:TextBox ID="SearchCIDModal"  ClientIDMode="Static" CssClass="form-control" runat="server"  ></asp:TextBox>
                                <asp:Button ID="Search" CssClass="btn btn-primary" OnClick="Search_Click" Style="margin : 10px" runat="server" Text="Search" />

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

                                       <%-- <asp:TemplateField HeaderText="CIF Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCifType" runat="server" Text='<%# Bind("CIF_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="IDENTITY">
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



        <h3>ACCOUNT OPENING FORM - INDIVIDUAL</h3>
        <hr />
        <div class="row">
            <div class="col-md-3">
                <ul id="List1" class="nav nav-tabs-justified nav-menu">
                    <li id="accountNature"><a id="InaccountNatureAnchor" data-toggle="tab" href="#sectiona">A/C Nature & Currency</a></li>
                    <li id="ApplicantInformation" style="display: none"><a id="InApplicantInformationAnchor" data-toggle="tab" href="#sectionb">Applicant Information</a></li>
                    <li id="AddressInformation" style="display: none"><a id="InAddressInformationAnchor" data-toggle="tab" href="#sectionc">Address Information</a></li>
                    <li id="OperatingInstruction" style="display: none"><a id="InOperatingInstructionAnchor" data-toggle="tab" href="#sectiond">Operating Instructions</a></li>
                    <li id="NextOfKin" style="display: none"><a id="InNextOfKinAnchor" data-toggle="tab" href="#sectione">Next Of Kin Info</a></li>

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

                        <asp:CheckBox runat="server" ID="AcCnicVerifiedCheck" Text="1. Is CNIC of the customer is Verified including CNIC of Beneficial owner in case of other than self" />
                        <br />
                        <div class="form-group">
                            <label >Account Opening Type</label>
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
                                <asp:DropDownList ID="AcListAccountType" ClientIDMode="Static" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AcListAccountType_SelectedIndexChanged"></asp:DropDownList>
                                 <asp:RequiredFieldValidator Display="Dynamic" InitialValue="0" ID="RequiredFieldValidatorAccountType" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="AcValidationGroup" ControlToValidate="AcListAccountType" ErrorMessage="Account Type is Required"></asp:RequiredFieldValidator>
                            </div>
                             <div class="form-group">
                                <label class="lblReview">Account Modes: *</label>
                                <asp:DropDownList ID="AcListAccountMode" ClientIDMode="Static" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AcListAccountMode_SelectedIndexChanged"></asp:DropDownList>
                                 <asp:RequiredFieldValidator Display="Dynamic" InitialValue="0" ID="RequiredFieldValidatorAccountMode" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="AcValidationGroup" ControlToValidate="AcListAccountMode" ErrorMessage="Account Mode is Required"></asp:RequiredFieldValidator>
                            </div>

                          <div class="control-group" style="display: none">
                            <label class="lblReview control-label">Account Mode:</label>
                            <div class="controls">  
                            <asp:RadioButton ID="AcAccountModeRadio1" Style="margin-right: 15px" Text="Single" Checked="True" GroupName="AcAccountModeRadioGroup" runat="server" />
                            <asp:RadioButton ID="AcAccountModeRadio2" Text="Joint" GroupName="AcAccountModeRadioGroup" runat="server" />
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
                            <asp:RequiredFieldValidator Enabled="false" ID="AcValidatorAccountNumber" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="AcValidationGroup" ControlToValidate="AcAccountNumber" ErrorMessage="Account Number is Required"></asp:RequiredFieldValidator>

                        </div>

                        <div class="form-group">
                            <label class="lblReview">Account Title: *</label>
                            <asp:TextBox ID="AcAccountTitle" ClientIDMode="Static" CssClass="form-control" runat="server" MaxLength="40"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="AcValidatorAccountTitle" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="AcValidationGroup" ControlToValidate="AcAccountTitle" ErrorMessage="Account Title is Required"></asp:RequiredFieldValidator>

                        </div>

                        <div class="form-group">
                            <label class="lblReview">Initial Deposit: *</label>
                            <asp:TextBox ID="AcInitialDeposit" TextMode="Number" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="AcValidatorInitialDeposit" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="AcValidationGroup" ControlToValidate="AcInitialDeposit" ErrorMessage="Initial Deposit is Required"></asp:RequiredFieldValidator>

                        </div>
                       

                        <div class="form-group" style="display: none">
                            <label class="lblReview">Account Mode:</label>

                            <asp:RadioButton ID="AcMinorAccountRadio1" Text="Yes" Checked="True" GroupName="AcMinorAccountRadioGroup" runat="server" />
                            <asp:RadioButton ID="AcMinorAccountRadio2" Text="No" GroupName="AcMinorAccountRadioGroup" runat="server" />

                        </div>

                        <div class="form-group">
                            <asp:Button ID="AcSubmitButton" runat="server" Text="SAVE" CssClass="btn btn-primary" ValidationGroup="AcValidationGroup" OnClick="AcSubmitButton_Click" />

                            <button id="AcResetButton" onclick="openModal()" type="button" class="btn btn-primary" value="Reset">Reset</button>
                            <asp:Button ID="btnUpdateAc" Style="display : none" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary" ValidationGroup="AcValidationGroup" OnClick="btnUpdateAc_Click" />
                             <asp:Button ID="btnSubmitACa" Visible="false"  ClientIDMode="Static" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmitACa_Click"/>

                        </div>


                    </div>


                    <div id="sectionb" class="tab-pane fade">
                        <h3>Applicant Information</h3>
                        <br />
                           <asp:Button ID="ApSearchCifButton" Style="float: right; margin-bottom: 15px" runat="server" Text="Search CIF" data-toggle="modal" OnClientClick="return false;" data-target="#SearchCIF" CssClass="btn btn-primary" OnClick="ApSearchCifButton_Click" />
                         <asp:UpdatePanel ID="UpdatePanelCNICValidation" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                        <div class="form-group">
                            
                            <label class="lblReview">ID: *</label>
                            <asp:TextBox ID="ApCustomerCif" ReadOnly="true" ClientIDMode="Static" CssClass="form-control col-md-3" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="ApCustomerCifValidator" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="ApValidationGroup" ControlToValidate="ApCustomerCif" ErrorMessage="Customer CIF No is Required"></asp:RequiredFieldValidator>                        
                        </div>

                        <div class="form-group">
                            <label class="lblReview">Customer Name: *</label>
                            <asp:TextBox ID="ApCustomerName" ReadOnly="true" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="ApCustomerNameValidator" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="ApValidationGroup" ControlToValidate="ApCustomerName" ErrorMessage="Customer Name is Required"></asp:RequiredFieldValidator>

                        </div>

                        <div class="form-group">
                            <label class="lblReview">Customer Identity: *</label>
                            <asp:TextBox ID="ApCustomerCNIC" ReadOnly="true"  ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="ApCustomerCNICValidator" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="ApValidationGroup" ControlToValidate="ApCustomerCNIC" ErrorMessage="Customer CNIC is Required"></asp:RequiredFieldValidator>
                            <asp:Label Visible="false"  ForeColor="Red" Font-Bold="true" runat="server" ID="cnicbiolbl" Text="CNIC is Required For Bio Metric Verification"></asp:Label>
                        </div>
                        
                           <asp:Button ID="btnBioMetricVerify" Style="float: right"  ClientIDMode="Static" runat="server" Text="VERIFY BIOMETRIC" CssClass="btn btn-lg btn-primary" OnClick="btnBioMetricVerify_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>    
                            

                        <div class="control-group">
                            <label class="lblReview control-label" >Is Primary A/C Holder:</label>
                            <div class="controls"> 
                            <asp:RadioButton ID="ApIsPrimaryRadio1" Text="Yes" Style="margin-right: 15px" Checked="True" GroupName="IsPrimaryRadioGroup" runat="server" />
                            <asp:RadioButton ID="ApIsPrimaryRadio2" Text="No" GroupName="IsPrimaryRadioGroup" runat="server" />
                            </div>
                        </div>

                        <div class="control-group">
                            <label class="lblReview control-label">Applicant in Negative List:</label>
                            <div class="controls"> 
                            <asp:RadioButton ID="ApApplicantNegativeRadio1" Style="margin-right: 15px" Text="Yes"  GroupName="ApplicantNegativeRadioGroup" runat="server" />
                            <asp:RadioButton ID="ApApplicantNegativeRadio2" Text="No" Checked="True" GroupName="ApplicantNegativeRadioGroup" runat="server" />
                            </div>
                        </div>

                        <div class="control-group">
                            <label class="lblReview control-label">Power of Attorny:</label>
                            <div class="controls"> 
                            <asp:RadioButton ID="ApPowerAttornyRadio1"  Style="margin-right: 15px" Text="Yes" GroupName="PowerAttornyRadioGroup" runat="server" />
                            <asp:RadioButton ID="ApPowerAttornyRadio2"  Checked="True" Text="No" GroupName="PowerAttornyRadioGroup" runat="server" />
                                </div>
                        </div>

                        <div class="control-group">
                            <label class="lblReview control-label">Signature Authority:</label>
                            <div class="controls"> 
                            <asp:RadioButton ID="ApSignatureRadio1" Style="margin-right: 15px" Text="Yes"  GroupName="SignatureRadioGroup" runat="server" />
                            <asp:RadioButton ID="ApSignatureRadio2" Text="No" GroupName="SignatureRadioGroup" Checked="True" runat="server" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="lblReview">Applicant Status: *</label>
                            <asp:DropDownList ID="ApListApplicantStatus" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                         <asp:RequiredFieldValidator Display="Dynamic" InitialValue="0" ControlToValidate="ApListApplicantStatus" ForeColor="Red" Font-Bold="true" ID="RequiredFieldValidatorApplicantStatus" ValidationGroup="ApValidationGroup" runat="server" ErrorMessage="Applicant Status is Required"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group">
                            <label class="lblReview">Relationship (If Not Primary):</label>
                            <asp:DropDownList ID="ApListRelationPrimary" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>

                        <div class="form-group">
                            <label class="lblReview">Relationship Detail (If other):</label>
                            <asp:TextBox ID="ApRelationshipDetail" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>


                        <div class="form-group">
                            <label class="lblReview">Investment Share(%):</label>
                            <asp:TextBox ID="ApInvestmentShare" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>

                        <div class="row" id="gridCif">
                            <div class="col-lg-12">

                                <asp:UpdatePanel ID="UpdatePanelGridCifs" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                             <asp:GridView class="table" ShowHeaderWhenEmpty="true"  Enabled="false" Visible="false"  ID="GridViewAccountCifs" runat="server"  AutoGenerateColumns="false" OnRowDataBound="GridViewAccountCifs_RowDataBound" OnDataBound="GridViewAccountCifs_DataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="CIF NO">
                                            <ItemTemplate>
                                                 <asp:Label ID="CUSTOMER_CIF_NO" runat="server" Text='<%# Bind("CUSTOMER_CIF_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PRIMARY ACCOUNT HOLDER">
                                            <ItemTemplate>
                                                <asp:Label ID="IS_PRIMARY_ACCOUNT_HOLDER" runat="server" Text='<%# Bind("IS_PRIMARY_ACCOUNT_HOLDER") %>'></asp:Label>
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

                                         <asp:TemplateField HeaderText="INVESTMENT SHARE">
                                            <ItemTemplate>
                                                <asp:Label ID="INVESTMENT_SHARE" runat="server" Text='<%# Bind("INVESTMENT_SHARE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="APPLICANT STATUS">
                                            <ItemTemplate>
                                                <asp:Label ID="APPLICANT_SHARE" runat="server" Text='<%# Bind("APPLICANT_STATUS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button runat="server" ID="btnDelete" Text="DELETE" OnClick="delete_Click"></asp:Button>
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

                            <asp:Button ID="btnGridAddCif"  ValidationGroup="ApValidationGroup" Visible="false" CssClass="btn btn-primary" Style="float: right" OnClick="btnGridAddCif_Click"  runat="server" Text="Add CIF" />
                             <asp:CustomValidator Display="Dynamic" Font-Bold="true" ForeColor="Red" ID="CustomValidatorJoinCustomers" ValidationGroup="JOINTACCOUNT" runat="server" ErrorMessage="Atleast Two Customer are required"  ClientValidationFunction="JointMoreCustomer" EnableClientScript="true" OnServerValidate="CustomValidatorJoinCustomers_ServerValidate" ></asp:CustomValidator>
                             <br />
                            <asp:CustomValidator Display="Dynamic" Font-Bold="true" ForeColor="Red" ID="CustomValidatorPrimaryAccount" ValidationGroup="JOINTACCOUNT" runat="server" ErrorMessage="Atleast One Customer must be primary Account holder"  ClientValidationFunction="CheckPrimaryAccountHolder" EnableClientScript="true" OnServerValidate="CustomValidatorPrimaryAccount_ServerValidate" ></asp:CustomValidator>
                            <br />
                             <asp:CustomValidator Display="Dynamic" Font-Bold="true" ForeColor="Red" ID="CustomValidatorPrimaryAccountGreater" ValidationGroup="JOINTACCOUNT" runat="server" ErrorMessage="Only One Customer can be a primary Account Holder"  ClientValidationFunction="CheckPrimaryAccountHolderSingle" EnableClientScript="true" OnServerValidate="CustomValidatorPrimaryAccountGreater_ServerValidate"></asp:CustomValidator>
                        </div>




                        <div class="form-group">
                            <asp:Button ID="ApSubmitButton" runat="server" Text="SAVE" CssClass="btn btn-primary" ValidationGroup="ApValidationGroup" OnClick="ApSubmitButton_Click" />

                            <button id="ApResetButton" onclick="openModal()" type="button" class="btn btn-primary" value="Reset">Reset</button>

                            <asp:Button ID="BtnUpdateAp" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary" ValidationGroup="ApValidationGroup" OnClick="BtnUpdateAp_Click" />
                             <asp:Button ID="btnSubmitACb" Visible="false"  ClientIDMode="Static" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmitACa_Click"/>

                        </div>

                    </div>




                    <div id="sectionc" class="tab-pane fade">
                        <h3>Mailing Address / Correspondence Address</h3>
                        
                           <asp:Button ID="btnSearchCifAdress" style="float: right" runat="server" Text="Lookup CIF" CssClass="btn btn-primary" OnClick="btnSearchCifAdress_Click" />
                        <asp:UpdatePanel runat="server" ID="updatePanel_Applications" UpdateMode="Conditional" ClientIDMode="AutoID" ChildrenAsTriggers="true" >
                            
                            <ContentTemplate>
                 
                                <br />
                                <div class="form-group">
	                                <label class="lblReview">Country: *</label>
	                                <asp:DropDownList ID="AdListCountry" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
	                                <asp:RequiredFieldValidator InitialValue="0" Display="Dynamic" ID="ReuiredAdCountry" ControlToValidate="AdListCountry" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="AdValidationGroup" ErrorMessage="Country is Required"></asp:RequiredFieldValidator>
                                </div>

                              
                            <div class="form-group">
                                <label class="lblReview">City: *</label>
                                <asp:DropDownList ID="AdListCity" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                                 <asp:RequiredFieldValidator InitialValue="0" Display="Dynamic" ID="RequiredFieldValidatorCity" ControlToValidate="AdListCity" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="AdValidationGroup" ErrorMessage="City is Required"></asp:RequiredFieldValidator>
                           </div>
                            <div class="form-group">
                                <label  class="lblReview">Province: *</label>
                                <asp:DropDownList ID="AdListProvince" CssClass="form-control" runat="server"></asp:DropDownList>
                                 <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorAdProvince" Enabled="true" runat="server" InitialValue="0" ControlToValidate="AdListProvince" ForeColor="Red" Font-Bold="true" ValidationGroup="AdValidationGroup" ErrorMessage="Province is Required"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label class="lblReview">Address Line 1: *</label>
                                <asp:TextBox ID="AdTxtBuilding" ClientIDMode="Static" MaxLength="40" CssClass="form-control" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorBuilding" ControlToValidate="AdTxtBuilding" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="AdValidationGroup" ErrorMessage="Address Line 1 is Required"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label class="lblReview">Address Line 2:</label>
                                <asp:TextBox ID="AdTxtStreet" ClientIDMode="Static" MaxLength="40" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label class="lblReview">Address Line 3:</label>
                                <asp:TextBox ID="AdTxtFloor" ClientIDMode="Static" MaxLength="40" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                           <div class="form-group">
                            <label class="lblReview">District:</label>
                              <asp:TextBox ID="AdTxtDistrict" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                          </div>
                        
                         
                        <div class="form-group">
                            <label class="lblReview">P.O Box:</label>
                            <asp:TextBox ID="AdPoBox" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>
                       
                        <div class="form-group">
                            <label class="lblReview">Postal Code:</label>
                            <asp:TextBox ID="AdPostalCode" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>                           
                        <div class="form-group">
                            <label class="lblReview">Tel (Office):</label>
                            <asp:TextBox ID="AdOffice" ClientIDMode="Static" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:CustomValidator ErrorMessage="Any one contact is required" ID="CustomValidatorAnyContact" ValidationGroup="AdValidationGroup" runat="server" Display="Dynamic" ForeColor="Red" Font-Bold="true" OnServerValidate="CustomValidatorAnyContact_ServerValidate" EnableClientScript="true" ClientValidationFunction="doCustomValidateContact"></asp:CustomValidator>
                        </div>

                        <div class="form-group">
                            <label class="lblReview">Tel (Residence):</label>
                            <asp:TextBox ID="AdResidence" ClientIDMode="Static" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>


                        <div class="form-group">
                            <label class="lblReview">Mobile No:</label>
                            <asp:TextBox ID="AdMobileNo" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                          <asp:RequiredFieldValidator Enabled="false" TextMode="Number" Display="Dynamic" ID="RequiredFieldValidatorMobile" ControlToValidate="AdMobileNo" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="AdValidationGroup" ErrorMessage="Mobile Number is Required"></asp:RequiredFieldValidator>
                          

                           

                        </div>

                        <div class="form-group">
                            <label class="lblReview">Fax No:</label>
                            <asp:TextBox ID="AdFaxNo" ClientIDMode="Static" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>
                        <div class="control-group">
                            <label class="lblReview control-label" >SMS Alert Required:</label>
                            <div class="controls"> 
                            <asp:RadioButton ID="RadioButtonSms1" Text="Yes" Style="margin-right: 15px"  GroupName="AcSMSRadio" runat="server" AutoPostBack="true" OnCheckedChanged="RadioButtonSms1_CheckedChanged" />
                            <asp:RadioButton ID="RadioButtonSms2" Text="No" Checked="True" GroupName="AcSMSRadio" runat="server" AutoPostBack="true"  OnCheckedChanged="RadioButtonSms1_CheckedChanged"/>
                            </div>
                        </div>
                        <div class="form-group" style="display: none">
                            <label class="lblReview">SMS Alert Required:</label>
                            <asp:DropDownList ID="AdListSmsRequired" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>

                        <div class="form-group">
                            <label class="lblReview">Required E-Statement on Email:</label>
                            <asp:CheckBox runat="server" ID="AdEstatementCheckbox" Text="YES" AutoPostBack="true" OnCheckedChanged="AdEstatementCheckbox_CheckedChanged" />
                         </div>

                        <br />
                        <h3>Add Emails
                        </h3>

                        <div class="form-group">
                            <label class="lblReview">Email:</label>
                            <asp:TextBox ID="AdEmail" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Enabled="false" Display="Dynamic" ID="RequiredFieldValidatorEmail" ControlToValidate="AdEmail" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="AdValidationGroup" ErrorMessage="Email is Required"></asp:RequiredFieldValidator>
                        </div>
                            </ContentTemplate>
                            <Triggers>
	                            <asp:AsyncPostBackTrigger ControlID="btnSearchCifAdress" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>

                        

                      

                        <div class="form-group">
                            <asp:Button ID="AdSubmitButton" ValidationGroup="AdValidationGroup" runat="server" Text="SAVE" CssClass="btn btn-primary" OnClick="AdSubmitButton_Click" />

                            <button id="AdResetButton" onclick="openModal()" type="button" class="btn btn-primary" value="Reset">Reset</button>

                            <asp:Button ID="BtnUpdateAd" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary" ValidationGroup="AdValidationGroup" OnClick="BtnUpdateAd_Click"/>
                             <asp:Button ID="btnSubmitACc" Visible="false"  ClientIDMode="Static" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmitACa_Click"/>


                        </div>









                    </div>

                    <div id="sectiond" class="tab-pane fade">
                        <h3>Operating Instrucitons</h3>

                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                        <div class="form-group">
                            <label class="lblReview">Authority To Operate: *</label>
                            <asp:DropDownList ID="AuListAuthority" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator InitialValue="0" Display="Dynamic" ID="RequiredFieldValidatorAuthority" ControlToValidate="AuListAuthority" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="AuValidationGroup" ErrorMessage="Authority To Operate is Required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Description (If Any Other):</label>
                            <asp:TextBox ID="AuDescriptionOther" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>

                        <div class="control-group">
                            <label class="lblReview control-label">Zakat Exempted?:</label>
                            <div class="controls"> 
                            <asp:RadioButton ID="ZakatDeductionRadio1" Style="margin-right: 15px" AutoPostBack="true" Text="Yes"  GroupName="ZakatDeductionRadioGroup" runat="server" OnCheckedChanged="ZakatDeductionRadio1_CheckedChanged" />
                            <asp:RadioButton ID="ZakatDeductionRadio2" AutoPostBack="true" Text="No" Checked="True" GroupName="ZakatDeductionRadioGroup" runat="server" OnCheckedChanged="ZakatDeductionRadio1_CheckedChanged" />
                             </div>
                        </div>

                        <div class="form-group">
                            <label class="lblReview">Zakat Exemption Type:</label>
                            <asp:DropDownList ID="AuListZakatExemption" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator Enabled="false" InitialValue="0" Display="Dynamic" ID="RequiredFieldValidatorZakatExemption" ControlToValidate="AuListZakatExemption" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="AuValidationGroup" ErrorMessage="Zakat Exemption Type is Required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Exemption Reason Detail:</label>
                            <asp:TextBox ID="AuExempReasonDetail" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>

                        <div class="form-group">
                            <label class="lblReview">Account Statement Frequency:</label>

                            <asp:CheckBoxList runat="server" ID="AuListAccountFrequenct"></asp:CheckBoxList>
                         <%--   <asp:RadioButtonList runat="server" ID="AuListAccountFrequenct"></asp:RadioButtonList>--%>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Description (If Hold Mail):</label>
                            <asp:TextBox ID="AuDescrHoldMail" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>

                        <div class="control-group">
                            <label class="lblReview control-label">ATM Card Required:</label>
                            <div class="controls">  
                            <asp:RadioButton ID="AtmRequiredRadio1" Text="Yes" Style="margin-right: 15px" AutoPostBack="true" GroupName="AtmRequiredRadioGroup" runat="server" OnCheckedChanged="AtmRequiredRadio1_CheckedChanged" />
                            <asp:RadioButton ID="AtmRequiredRadio2" Text="No" Checked="True" AutoPostBack="true"  GroupName="AtmRequiredRadioGroup" runat="server" OnCheckedChanged="AtmRequiredRadio1_CheckedChanged" />
                            </div>
                        </div>


                        <div class="form-group">
                            <label class="lblReview">Customer Name on ATM Card:</label>
                            <asp:TextBox ID="AuCustomerNameAtm" ClientIDMode="Static" MaxLength="15" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Enabled="false"  Display="Dynamic" ID="RequiredFieldValidatoratm" ControlToValidate="AuCustomerNameAtm" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="AuValidationGroup" ErrorMessage="Customer Name on ATM Card is Required"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group">
                            <label class="lblReview">E-Statement Required:</label>
                            <asp:DropDownList ID="AuListEstatement" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                        <div class="control-group">
                            <label class="lblReview control-label">Mobile Banking Required:</label>
                            <div class="controls"> 
                            <asp:RadioButton ID="MobileBankRequirRadio1" Style="margin-right: 15px" AutoPostBack="true" Text="Yes" GroupName="MobileBankRequirRadioGroup" runat="server" OnCheckedChanged="MobileBankRequirRadio1_CheckedChanged" />
                            <asp:RadioButton ID="MobileBankRequirRadio2" AutoPostBack="true" Text="No"  Checked="True" GroupName="MobileBankRequirRadioGroup" runat="server" OnCheckedChanged="MobileBankRequirRadio1_CheckedChanged" />
                            </div>
                        </div>


                        <div class="form-group">
                            <label class="lblReview">Mobile No (Mobile Banking):</label>
                            <asp:TextBox ID="AuMobileNo" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                             <asp:RequiredFieldValidator Enabled="false"  Display="Dynamic" ID="RequiredFieldValidatorMobileBanking" ControlToValidate="AuMobileNo" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="AuValidationGroup" ErrorMessage="Mobile No is Required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="control-group">
                            <label class="lblReview control-label">IBT Allowed:</label>
                            <div class="controls"> 
                            <asp:RadioButton ID="IBTAllowRadio1" Text="Yes" Style="margin-right: 15px"  GroupName="IBTAllowRadioGroup" runat="server" />
                            <asp:RadioButton ID="IBTAllowRadio2" Text="No" Checked="True" GroupName="IBTAllowRadioGroup" runat="server" />
                            </div>
                        </div>

                         <div class="control-group">
                             <label class="lblReview control-label">Is Profit Applicable:</label>
                              <div class="controls"> 
                            <asp:RadioButton ID="IsProfitAppRadio1" Text="Yes" Style="margin-right: 15px"  GroupName="IsProfitAppRadioGroup" runat="server" />
                            <asp:RadioButton ID="IsProfitAppRadio2" Text="No" Checked="True" GroupName="IsProfitAppRadioGroup" runat="server" />
                             </div>
                        </div>

                        <div class="control-group">
                            <label class="lblReview control-label">Is FED Exempted:</label>
                               <div class="controls"> 
                            <asp:RadioButton ID="IsFedRadio1" Text="Yes" AutoPostBack="true"  Style="margin-right: 15px" GroupName="IsFedRadioGroup" runat="server" OnCheckedChanged="IsFedRadio1_CheckedChanged" />
                            <asp:RadioButton ID="IsFedRadio2" Text="No" AutoPostBack="true"  Checked="True" GroupName="IsFedRadioGroup" runat="server"  OnCheckedChanged="IsFedRadio1_CheckedChanged"/>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="lblReview">Expiry Date (If Exempted): </label>
                            <div class="input-group date-control">
                                <asp:TextBox ID="AuExpDateExempted" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </button>
                                </span>
                            </div>
                        </div>
                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidatorBirth" runat="server" ErrorMessage="Expiry Date must Be in DD-MM-YYYY FORMAT" ForeColor="Red" Font-Bold="true" ControlToValidate="AuExpDateExempted" ValidationGroup="AuValidationGroup" ValidationExpression="^\d{2}-\d{2}-\d{4}$"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator Enabled="false" Display="Dynamic" ID="RequiredFieldValidatorBirth" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="AuValidationGroup" ControlToValidate="AuExpDateExempted" ErrorMessage="Expiry Date is required"></asp:RequiredFieldValidator>

                        <div class="control-group">
                            <label class="lblReview control-label">Applicable Profit Rate:</label>
                            <div class="controls"> 
                            <asp:RadioButton ID="AppProfitRateRadio1" Text="Bank Rate" Checked="True" GroupName="AppProfitRateRadioGroup" runat="server" />
                            <asp:RadioButton ID="AppProfitRateRadio2" Text="Special Rate" GroupName="AppProfitRateRadioGroup" runat="server" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="lblReview">Special Profit Rate Value (in %age):</label>
                            <asp:TextBox ID="AuSpecicalProfitValue" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>

                        <div class="form-group">
                            <label class="lblReview">Profit Payment:</label>
                            <asp:DropDownList ID="AuListProfitPayment" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>


                         <div class="control-group">
                           <label class="lblReview control-label">WHT Deducted on Profit:</label>
                             <div class="controls"> 
                            <asp:RadioButton ID="WhtProfitRadio1" Text="Yes"  GroupName="WhtProfitRadioGroup" runat="server" AutoPostBack="true" OnCheckedChanged="WhtProfitRadio1_CheckedChanged" />
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
                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidatorWhtProfitExpiry" runat="server" ErrorMessage="Expiry Date must Be in DD-MM-YYYY FORMAT" ForeColor="Red" Font-Bold="true" ControlToValidate="AuExpDateProfit" ValidationGroup="AuValidationGroup" ValidationExpression="^\d{2}-\d{2}-\d{4}$"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator Enabled="false" Display="Dynamic" ID="RequiredFieldValidatorWhtProfitExpiry" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="AuValidationGroup" ControlToValidate="AuExpDateProfit" ErrorMessage="Expiry Date is required"></asp:RequiredFieldValidator>

                        <div class="control-group">
                           <label class="lblReview control-label" >WHT Deducted on Transactions:</label>
                            <div class="controls"> 
                            <asp:RadioButton ID="WhtTransactionRadio1" Text="Yes" Checked="True"  Style="margin-right: 15px"  GroupName="WhtTransactionRadioGroup" runat="server" AutoPostBack="true" OnCheckedChanged="WhtTransactionRadio1_CheckedChanged"/>
                            <asp:RadioButton ID="WhtTransactionRadio2" Text="No"  GroupName="WhtTransactionRadioGroup" AutoPostBack="true" runat="server" OnCheckedChanged="WhtTransactionRadio1_CheckedChanged" />
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

                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidatorWhtTrans" runat="server" ErrorMessage="Expiry Date must Be in DD-MM-YYYY FORMAT" ForeColor="Red" Font-Bold="true" ControlToValidate="AuExpDateTrans" ValidationGroup="AuValidationGroup" ValidationExpression="^\d{2}-\d{2}-\d{4}$"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator Enabled="false"  Display="Dynamic" ID="RequiredFieldValidatorWhtTrans" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="AuValidationGroup" ControlToValidate="AuExpDateTrans" ErrorMessage="Expiry Date is required"></asp:RequiredFieldValidator>

                        </ContentTemplate>
                                
                        </asp:UpdatePanel>

                       

                        <div class="form-group">
                            <asp:Button ID="AuSubmitButton" runat="server" ValidationGroup="AuValidationGroup" Text="SAVE" CssClass="btn btn-primary" OnClick="AuSubmitButton_Click" />

                            <button id="AuResetButton" onclick="openModal()" type="button" class="btn btn-primary" value="Reset">Reset</button>

                            <asp:Button ID="BtnUpdateAu" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary" ValidationGroup="AuValidationGroup" OnClick="BtnUpdateAu_Click"/>
                             <asp:Button ID="BtnSubmitACd" Visible="false"  ClientIDMode="Static" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmitACa_Click"/>


                        </div>


                    </div>

                    <div id="sectione" class="tab-pane fade">
                        <h3>Next of Kin Info</h3>
                       
                        <div class="form-group" style="display: none">
                            <label class="lblReview">Next of Kin CIF No:</label>
                            <asp:TextBox ID="NkCifNo" ClientIDMode="Static" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                            <asp:Button ID="btnSearchCifKin" style="margin-top: 5px" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSearchCifKin_Click" />

                        </div>

                        <asp:UpdatePanel runat="server" ID="updatePanelSearchKin" UpdateMode="Conditional" ClientIDMode="AutoID" >
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSearchCifKin" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>

                        <div class="form-group">
                            <label class="lblReview">Next of Kin Name: *</label>
                            <asp:TextBox ID="NkName" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidatorNkName" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="NkValidationGroup" ControlToValidate="NkName" ErrorMessage="Next of Kin Name is required"></asp:RequiredFieldValidator>

                        </div>
                        <div class="form-group">
                            <label class="lblReview">Next of Kin CNIC: *</label>
                            <asp:TextBox ID="NkCNIC" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidatorNkCnic" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="NkValidationGroup" ControlToValidate="NkCNIC" ErrorMessage="Next of Kin CNIC is required"></asp:RequiredFieldValidator>
                             <asp:RegularExpressionValidator   Display="Dynamic" ID="RegularExpressionValidatorCnic" runat="server" ErrorMessage="The CNIC must be in correct format e.g xxxxx-xxxxxxx-x" ForeColor="Red" Font-Bold="true" ControlToValidate="NkCNIC" ValidationGroup="NkValidationGroup" ValidationExpression="^\d{5}-\d{7}-\d{1}$" ></asp:RegularExpressionValidator>                                  
                        </div>

                        </ContentTemplate>
                        </asp:UpdatePanel>

                        <div class="form-group">
                            <label class="lblReview">Relationship: *</label>
                            <asp:DropDownList ID="NkListRelationship" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator InitialValue="0"  Display="Dynamic" ID="RequiredFieldValidatorNkRealtion" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="NkValidationGroup" ControlToValidate="NkListRelationship" ErrorMessage="Relationship is required"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group">
                            <label class="lblReview">Relationship Detail (If Other):</label>
                            <asp:TextBox ID="NkRelationDetailOther" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <h3>Address / Contact information</h3>

                        <div class="form-group">
                             <label class="lblReview">Country: *</label>
                            <asp:DropDownList ID="NkListCountry" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator InitialValue="0"  Display="Dynamic" ID="RequiredFieldValidatorNkCountry" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="NkValidationGroup" ControlToValidate="NkListCountry" ErrorMessage="Country is required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                             <label class="lblReview">City: *</label>
                            <asp:DropDownList ID="NkListCity" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator InitialValue="0"  Display="Dynamic" ID="RequiredFieldValidatorNkCity" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="NkValidationGroup" ControlToValidate="NkListCity" ErrorMessage="City is required"></asp:RequiredFieldValidator>
                        </div>
                         <div class="form-group">
                             <label class="lblReview">Address Line 1: *</label>
                            <asp:TextBox ID="NkBuilding" MaxLength="40" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                             <asp:RequiredFieldValidator  Display="Dynamic" MaxLength="40" ID="RequiredFieldValidatorNkBuilding" runat="server" ForeColor="Red" Font-Bold="true" ValidationGroup="NkValidationGroup" ControlToValidate="NkBuilding" ErrorMessage="Address Line 1 is required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                             <label class="lblReview">Address Line 2:</label>
                            <asp:TextBox ID="NkTxtfloor" ClientIDMode="Static" MaxLength="40" CssClass="form-control" runat="server"></asp:TextBox>                           
                        </div>
                         <div class="form-group">
                             <label class="lblReview">Address Line 3:</label>
                            <asp:TextBox ID="NkTxtStreet" ClientIDMode="Static" MaxLength="40" CssClass="form-control" runat="server"></asp:TextBox>                           
                        </div>                        
                         <div class="form-group">
                             <label class="lblReview">District:</label>
                            <asp:TextBox ID="NkTxtDistrict" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>                           
                        </div>
                         <div class="form-group">
                             <label class="lblReview">Post Office:</label>
                            <asp:TextBox ID="NkTxtPostOffice" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>                           
                        </div>
                        <div class="form-group">
                             <label class="lblReview">Postal Code:</label>
                            <asp:TextBox ID="NkTxtPostalCode" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>                           
                        </div>
                          <div class="form-group">
                             <label class="lblReview">Residence No:</label>
                            <asp:TextBox ID="NktxtResidenceContact"  TextMode="Number" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox> 
                            <asp:CustomValidator ErrorMessage="Any one contact is required" ID="CustomValidatorNkAnyContact" ValidationGroup="NkValidationGroup" runat="server" Display="Dynamic" ForeColor="Red" Font-Bold="true" OnServerValidate="CustomValidatorNkAnyContact_ServerValidate" EnableClientScript="true" ClientValidationFunction="doCustomValidateContactNk"></asp:CustomValidator>                          
                        </div>
                          <div class="form-group">
                             <label class="lblReview">Office No:</label>
                            <asp:TextBox ID="NkTxtOfficeNo"  TextMode="Number" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>                           
                        </div>
                         <div class="form-group">
                             <label class="lblReview">Mobile No:</label>
                            <asp:TextBox ID="NkTxtMobileNo" TextMode="Number" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>                           
                        </div>
                        <div class="form-group">
                            <asp:Button ID="NkSubmitButton" runat="server" Text="SAVE" CssClass="btn btn-primary" ValidationGroup="NkValidationGroup" OnClick="NkSubmitButton_Click" />

                            <button id="NkResetButton" onclick="openModal()" type="button" class="btn btn-primary" value="Reset">Reset</button>
                             <asp:Button ID="BtnUpdateNK" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary" ValidationGroup="NkValidationGroup" OnClick="BtnUpdateNK_Click"/>
                             <asp:Button ID="BtnSubmitACe" Visible="false"  ClientIDMode="Static" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmitACa_Click"/>



                        </div>




                    </div>

                    <div id="sectionf" class="tab-pane fade">
                        <h3>Know Your Customer</h3>
                        <br />

                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                            <ContentTemplate>

                        <div class="form-group">
                            <label class="lblReview">Customer Type: *</label>
                            <asp:DropDownList ID="KnListCustomerType" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator InitialValue="0" Display="Dynamic" ID="RequiredFieldValidatorCustType" ControlToValidate="KnListCustomerType" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="Customer Type is Required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Description (If Referred):</label>
                            <asp:TextBox ID="KnDescrIfRefered" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>
                        <div class="form-group" style="display: none">
                            <label class="lblReview">Education: *</label>
                            <asp:DropDownList ID="KnListEducation" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator Enabled="false" InitialValue="0" Display="Dynamic" ID="RequiredFieldValidator1" ControlToValidate="KnListEducation" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="Education is Required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Purpose of Account: *</label>
                            <div style="overflow-x: hidden; overflow-y: auto; border: 1px #808080 solid; max-height: 215px; height: auto; height: 215px">
                                <asp:CheckBoxList ID="KnListPurposeOfAccount" AutoPostBack="true" ClientIDMode="Static" runat="server" RepeatColumns="2" OnSelectedIndexChanged="KnListPurposeOfAccount_SelectedIndexChanged"></asp:CheckBoxList>                                
                            </div>
                            <asp:CustomValidator  runat="server" ID="RequiredValidatorPOA"
                                    ClientValidationFunction="REQPOA" 
                                     Display="Dynamic" ForeColor="Red" Font-Bold="true" ValidationGroup="KnValidationGroup"
                                    ErrorMessage="Purpose of Account is Required"></asp:CustomValidator>
                           <%-- <asp:DropDownList ID="KnListPurposeOfAccount" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                             <asp:RequiredFieldValidator InitialValue="0" Display="Dynamic" ID="RequiredFieldValidatorPurposeAccount" ControlToValidate="KnListPurposeOfAccount" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="Purpose of account is Required"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Description (If Other):</label>
                            <asp:TextBox ID="KnDescrOther" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                             <asp:RequiredFieldValidator Enabled="false"  Display="Dynamic" ID="ReqValidatorPurposeAccountOther" ControlToValidate="KnDescrOther" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="Description (If Other) is Required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Source of Funds: *</label>
                            <div style="overflow-x: hidden; overflow-y: auto; border: 1px #808080 solid; max-height: 215px; height: auto; height: 215px">
                                <asp:CheckBoxList ID="KnListSourceOfFunds" ClientIDMode="Static" runat="server" RepeatColumns="2" AutoPostBack="true" OnSelectedIndexChanged="KnListSourceOfFunds_SelectedIndexChanged"></asp:CheckBoxList>                                
                            </div>
                            <asp:CustomValidator  runat="server" ID="ReqValidatorSOF"
                                    ClientValidationFunction="REQSOF" 
                                     Display="Dynamic" ForeColor="Red" Font-Bold="true" ValidationGroup="KnValidationGroup"
                                    ErrorMessage="Source of Fund is Required"></asp:CustomValidator>
                            <%--<asp:DropDownList ID="KnListSourceOfFunds" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                              <asp:RequiredFieldValidator InitialValue="0" Display="Dynamic" ID="RequiredFieldValidatorSourceFunds" ControlToValidate="KnListSourceOfFunds" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="Source of Funds is Required"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Description (if Other):</label>
                            <asp:TextBox ID="KnDescrOfSource" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                             <asp:RequiredFieldValidator Enabled="false"  Display="Dynamic" ID="ReqValidatorSourceDesc" ControlToValidate="KnDescrOfSource" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="Description (If Other) is Required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="control-group">
                            <label class="lblReview control-label" >Service Charges Exempted:</label>
                            <div class="controls">  
                            <asp:RadioButton ID="ServiceExemptedRadio1" Style="margin-right: 15px" Text="Yes"  GroupName="ServiceExemptedRadioGroup" runat="server" />
                            <asp:RadioButton ID="ServiceExemptedRadio2" Text="No" Checked="True" GroupName="ServiceExemptedRadioGroup" runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Service Charges Exempt Code:</label>
                            <asp:DropDownList ID="KnListSerExemptCode" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Reason (If Exempted):</label>
                            <asp:TextBox ID="KnReasonExempted" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>

                        <div class="form-group">
                            <label class="lblReview">Expected Monthly Income:</label>
                            <asp:TextBox ID="KnExpectedMonthlyIncome" ClientIDMode="Static" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>

                        </div>

                        <div class="form-group">
                            <label class="lblReview">Normal Mode of Transactions</label>
                            <asp:CheckBoxList ID="KnListModeOfTransaction" runat="server" RepeatColumns="2" AutoPostBack="true" OnSelectedIndexChanged="KnListModeOfTransaction_SelectedIndexChanged"></asp:CheckBoxList>
                             <asp:CustomValidator runat="server" ID="RequiredValidatorMOT"
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
                            <label class="lblReview">MAX Transaction Amount DR: *</label>
                            <asp:TextBox ID="KnMaxAmountDR" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                          <%--   <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidatorDrAMount" ControlToValidate="KnMaxAmountDR" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="MAX Transaction Amount DR is Required"></asp:RequiredFieldValidator>--%>

                        </div>
                        <div class="form-group" style="display: none">
                            <label class="lblReview">MAX Transaction Amount CR: *</label>
                            <asp:TextBox ID="KnMaxAmountCR" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <%--  <asp:RequiredFieldValidator  Display="Dynamic" ID="RequiredFieldValidatorCrAMount" ControlToValidate="KnMaxAmountCR" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="MAX Transaction Amount CR is Required"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="form-group" style="display: none">
                            <label class="lblReview"> Manager:</label>
                            <asp:DropDownList ID="KnddlManager" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                              <asp:RequiredFieldValidator Enabled="false" InitialValue="0" Display="Dynamic" ID="RequiredFieldValidatorManager" ControlToValidate="KnddlManager" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="Manager is Required"></asp:RequiredFieldValidator>

                        </div>
                        <div class="form-group" style="display: none">
                            <label class="lblReview">Relationship Manager:</label>
                            <asp:TextBox ID="KnRelationshipManager" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>
                        <div class="control-group" style="display: none">
                            <label class="lblReview control-label" >Occupation Verified:</label>
                            <div class="controls">  
                            <asp:RadioButton ID="OccupyVerifyRadio1" Style="margin-right: 15px" Text="Yes"  GroupName="OccupyVerifyRadioGroup" runat="server" />
                            <asp:RadioButton ID="OccupyVerifyRadio2" Text="No" Checked="True" GroupName="OccupyVerifyRadioGroup" runat="server" />
                            </div>
                        </div>
                        <div class="control-group" style="display: none">
                            <label class="lblReview control-label" >Address Verified:</label>
                            <div class="controls">  
                            <asp:RadioButton ID="RadioButtonAddressVerifiedYes" Text="Yes" Style="margin-right: 15px"  GroupName="AddressVerified" runat="server" />
                            <asp:RadioButton ID="RadioButtonAddressVerifiedNo" Text="No" Checked="True" GroupName="AddressVerified" runat="server" />
                             </div>
                        </div>
                        <div class="form-group" style="display: none">
                            <label class="lblReview">Address Verified:</label>
                            <asp:DropDownList ID="KnListAddresVerified" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                        <div class="form-group" style="display: none">
                            <label class="lblReview">Means Of Verification: *</label>
                            <asp:DropDownList ID="KnListMeansOfVerification" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                          <%--  <asp:RequiredFieldValidator InitialValue="0" Display="Dynamic" ID="RequiredFieldValidatorMeanVerification" ControlToValidate="KnListMeansOfVerification" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="Means Of Verification is Required"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="form-group" style="display: none">
                            <label class="lblReview">Means of Verification(Other):</label>
                            <asp:TextBox ID="KnMeanVerifyOther" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>
                        <div class="control-group" style="display: none">
                            <label class="lblReview control-label">Is Verification Satisfactory:</label>
                            <div class="controls"> 
                            <asp:RadioButton ID="IsVeriSatiRadio1"  Style="margin-right: 15px" Text="Yes" GroupName="IsVeriSatiRadioGroup" runat="server" />
                            <asp:RadioButton ID="IsVeriSatiRadio2" Text="No" Checked="True"  GroupName="IsVeriSatiRadioGroup" runat="server" />
                            </div>
                        </div>
                        <div class="form-group" style="display: none">
                            <label class="lblReview">Detail (If not Satisfactory):</label>
                            <asp:TextBox ID="KnDetailNotSatis" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>
                        <div class="form-group" style="display: none">
                            <label class="lblReview">Country (Home Remittance):</label>
                            <asp:DropDownList ID="KnListCountHomeRemit" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                             <%--<asp:RequiredFieldValidator Enabled="false" InitialValue="0" Display="Dynamic" ID="RequiredFieldValidatorCountryHome" ControlToValidate="KnListCountHomeRemit" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="Country (Home Remittance) is Required"></asp:RequiredFieldValidator>--%>
                        </div>
                        
                        <div class="form-group">
                            <label class="lblReview">No Of Debit Transactions: *</label>
                            <asp:TextBox ID="KnNODT" TextMode="Number" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorNODT" ControlToValidate="KnNODT" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="No Of Debit Transactions is Required"></asp:RequiredFieldValidator> 
                        </div>
                        <div class="form-group">
                            <label class="lblReview">PKR Equivalent Debit Transactions: *</label>
                            <asp:TextBox ID="KnPEDT" TextMode="Number" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorPEDT" ControlToValidate="KnPEDT" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="PKR Equivalent Debit Transactions is Required"></asp:RequiredFieldValidator> 
                        </div>
                        <div class="form-group">
                            <label class="lblReview">No of Credit Transactions: *</label>
                            <asp:TextBox ID="KnNOCT" TextMode="Number" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorNOCT" ControlToValidate="KnNOCT" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="No of Credit Transactions is Required"></asp:RequiredFieldValidator> 
                        </div>
                        <div class="form-group">
                            <label class="lblReview">PKR Equivalent Credit Transactions: *</label>
                            <asp:TextBox ID="KnPECT" TextMode="Number" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorPECT" ControlToValidate="KnPECT" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="PKR Equivalent Credit Transactions is Required"></asp:RequiredFieldValidator> 
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Expected Types of Counter Parties: *</label>
                            <div style="overflow-x: hidden; overflow-y: auto; border: 1px #808080 solid; max-height: 215px; height: auto; height: 215px">
                                <asp:CheckBoxList ID="KnListECP" runat="server" RepeatColumns="2" ></asp:CheckBoxList>
                            </div>
                             <asp:CustomValidator runat="server" ID="CustomValidatorECP"
                                    ClientValidationFunction="ValidateModuleListECP" 
                                     Display="Dynamic" ForeColor="Red" Font-Bold="true" ValidationGroup="KnValidationGroup"
                                    ErrorMessage="Expected Types of Counter Parties is Required"></asp:CustomValidator>
                        </div>

                           <div class="form-group">
                            <label class="lblReview">Geographies Involved of Counter Parties: *</label>
                            <div style="overflow-x: hidden; overflow-y: auto; border: 1px #808080 solid; max-height: 215px; height: auto; height: 215px">
                                <asp:CheckBoxList ID="KnListGCP" runat="server" RepeatColumns="2" ></asp:CheckBoxList>
                            </div>
                             <asp:CustomValidator runat="server" ID="CustomValidatorGCP"
                                    ClientValidationFunction="ValidateModuleListGCP" 
                                     Display="Dynamic" ForeColor="Red" Font-Bold="true" ValidationGroup="KnValidationGroup"
                                    ErrorMessage="Geographies Involved of Counter Parties is Required"></asp:CustomValidator>
                            </div>
                        <div class="form-group">
                            <label class="lblReview">Beneficial Ownership: *</label>
                            <asp:DropDownList ID="KnListRealBenef"  ClientIDMode="Static" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="KnListRealBenef_SelectedIndexChanged"></asp:DropDownList>
                             <asp:RequiredFieldValidator InitialValue="0" Display="Dynamic" ID="RequiredFieldValidatorBeneficiary" ControlToValidate="KnListRealBenef" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="Real Beneficiery of A/C is Required" ></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Name (If Other):</label>
                            <asp:TextBox ID="KnNameOther" ClientIDMode="Static" CssClass="form-control" runat="server" ></asp:TextBox>
                             <asp:RequiredFieldValidator   Display="Dynamic" ID="RequiredFieldValidatorBName" ControlToValidate="KnNameOther" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="Name is Required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Relationship with A/C Holder:</label>
                            <asp:DropDownList ID="KnListRelationAccountHolder" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator Enabled="false" InitialValue="0" Display="Dynamic" ID="RequiredFieldValidatorRelaWithAcc" ControlToValidate="KnListRelationAccountHolder" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="Relationship with A/C Holder is Required"></asp:RequiredFieldValidator>
                       </div>
                        <div class="form-group">
                            <label class="lblReview">Relationship Detail (If Other):</label>
                            <asp:TextBox ID="KnRelationDetailOther" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Address of Beneficial Owner:</label>
                            <asp:TextBox ID="KntxtAddress" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Enabled="false"  Display="Dynamic" ID="RequiredFieldValidatorBAddress" ControlToValidate="KntxtAddress" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="Address of Beneficial Owner is Required"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Nationality:</label>
                            <asp:DropDownList ID="knNationality" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator Enabled="false" InitialValue="0" Display="Dynamic" ID="RequiredFieldValidatorNationality" ControlToValidate="knNationality" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="Nationality is Required" ></asp:RequiredFieldValidator>
                        </div>
                       
                        <div class="form-group">
                            <label class="lblReview">Residence:</label>
                            <asp:DropDownList ID="knListResidence" ClientIDMode="Static" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="knListResidence_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator Enabled="false" InitialValue="0" Display="Dynamic" ID="RequiredFieldValidatorKnResidence" ControlToValidate="knListResidence" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="Residence is Required" ></asp:RequiredFieldValidator>
                        </div>
                        <div>
                             <label class="lblReview">Country Of Residence (If Non-Resident):</label>
                             <asp:TextBox ID="KnTxtResOther" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Enabled="false" Display="Dynamic" ID="RequiredFieldValidatorDescResi" ControlToValidate="KnTxtResOther" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="Description (If Non-Resident) is Required" ></asp:RequiredFieldValidator>
                        </div>
                         <div class="form-group">
                            <label class="lblReview">Identity Document Type:</label>
                            <asp:DropDownList ID="knListDocType" ClientIDMode="Static" CssClass="form-control" runat="server" ></asp:DropDownList>
                            <asp:RequiredFieldValidator Enabled="false" InitialValue="0" Display="Dynamic" ID="RequiredFieldValidatorDocType" ControlToValidate="knListDocType" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="Identity Document Type is Required" ></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label class="lblReview">Identity Number:</label>
                            <asp:TextBox ID="KnCnicOther" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Enabled="false" Display="Dynamic" ID="RequiredFieldValidatorIdNumber" ControlToValidate="KnCnicOther" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="Identity Number is Required" ></asp:RequiredFieldValidator>
                        </div>
                         <div class="form-group">
                            <label class="lblReview">Expiry Date: (DD-MM-YYYY)</label>
                            <asp:TextBox ID="KntxtExpiry" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Enabled="false" Display="Dynamic" ID="RequiredFieldValidatorExpiry" ControlToValidate="KntxtExpiry" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="Expiry Date is Required" ></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidatorExpiry" runat="server" ErrorMessage="Date of Birth must Be in DD-MM-YYYY FORMAT" ForeColor="Red" Font-Bold="true" ControlToValidate="KntxtExpiry" ValidationGroup="KnValidationGroup" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)[0-9]{2}$"></asp:RegularExpressionValidator>
                         </div>
                       <div class="form-group">
                            <label class="lblReview">Source of Fund: </label>
                            <asp:DropDownList ID="KnListSourceOfFund" ClientIDMode="Static" CssClass="form-control" runat="server" ></asp:DropDownList>
                             <asp:RequiredFieldValidator Enabled="false" InitialValue="0" Display="Dynamic" ID="RequiredFieldValidatorBSOF" ControlToValidate="KnListSourceOfFund" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="Source of Fund is Required" ></asp:RequiredFieldValidator>
                       </div>

                       <div class="form-group">
                            <label class="lblRAC">Reason of Opening Account with NBP: *</label>
                            <asp:DropDownList ID="KnListRAC"  ClientIDMode="Static" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="KnListRAC_SelectedIndexChanged"></asp:DropDownList>
                             <asp:RequiredFieldValidator InitialValue="0" Display="Dynamic" ID="RequiredFieldValidatorRAC" ControlToValidate="KnListRAC" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="Reason of Opening Account with NBP is Required" ></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label class="lblTxtRAC">Details (If Other):</label>
                            <asp:TextBox ID="knTextRACDetail" ClientIDMode="Static" CssClass="form-control" runat="server" ></asp:TextBox>
                             <asp:RequiredFieldValidator Enabled="false" Display="Dynamic" ID="RequiredFieldValidatorRACDetail" ControlToValidate="knTextRACDetail" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="KnValidationGroup" ErrorMessage="Detail is Required"></asp:RequiredFieldValidator>
                        </div>
                      
                      
                        
                        
                        
                      </ContentTemplate>                           
                    </asp:UpdatePanel>

                        <div class="form-group">
                            <asp:Button ID="KnSubmitButton" runat="server" Text="SAVE" ValidationGroup="KnValidationGroup"  CssClass="btn btn-primary" OnClick="KnSubmitButton_Click" />

                            <button id="KnResetButton" onclick="openModal()" type="button" class="btn btn-primary" value="Reset">Reset</button>

                              <asp:Button ID="BtnUpdateKn" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary" ValidationGroup="KnValidationGroup" OnClick="BtnUpdateKn_Click"/>
                             <asp:Button ID="BtnSubmitACf" Visible="false"  ClientIDMode="Static" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmitACa_Click"/>


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
                                <asp:TextBox ID="CdExpDate" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </button>
                                </span>
                            </div>
                        </div>
                         <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidatorExp" runat="server" ErrorMessage="Expiry Date must Be in DD-MM-YYYY FORMAT" ForeColor="Red" Font-Bold="true" ControlToValidate="CdExpDate" ValidationGroup="CdValidationGroup" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)[0-9]{2}$"></asp:RegularExpressionValidator>

                        <div class="control-group">
                            <label class="lblReview control-label" >Auto Roll Over On Expiry:</label>
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
                            <label class="lblReview">Profit Account Number: </label>
                            <asp:TextBox ID="CdProfitAccountNumber" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                           <%--  <asp:RequiredFieldValidator   Display="Dynamic" ID="RequiredFieldValidatorProfitAccNumber" ControlToValidate="CdProfitAccountNumber" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="CdValidationGroup" ErrorMessage="Profit Account Number is Required"></asp:RequiredFieldValidator>--%>
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
                            <asp:TextBox ID="CdChequeNumber" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>

                        <div class="form-group">
                            <label class="lblReview">Certificate Number: </label>
                            <asp:TextBox ID="CdCertNumber" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                        </div>

                        <div class="form-group">
                            <label class="lblReview">Certificate Amount:</label>
                            <asp:TextBox ID="CdCertAmount" TextMode="Number" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                          <%--   <asp:RequiredFieldValidator   Display="Dynamic" ID="RequiredFieldValidatorCertAmount" ControlToValidate="CdCertAmount" Font-Bold="true" ForeColor="Red" runat="server" ValidationGroup="CdValidationGroup" ErrorMessage="Certificate Amount is Required"></asp:RequiredFieldValidator>--%>
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
                            <asp:Button ID="CdSubmitButton" runat="server" Text="SAVE" ValidationGroup="CdValidationGroup" CssClass="btn btn-primary" OnClick="CdSubmitButton_Click" />

                            <button id="CdResetButton" onclick="openModal()" type="button" class="btn btn-primary" value="Reset">Reset</button>

                               <asp:Button ID="BtnUpdateCd" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary" ValidationGroup="CdValidationGroup" OnClick="BtnUpdateCd_Click"/>
                             <asp:Button ID="BtnSubmitACg" Visible="false"  ClientIDMode="Static" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmitACa_Click"/>

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

                                            <asp:RadioButton ID="DcRadio1" Text="Yes" GroupName="DcRadioGroup" runat="server"/>
                                            <asp:RadioButton ID="DcRadio2" Text="No" GroupName="DcRadioGroup"  runat="server" />
                                            <asp:RadioButton ID="DcRadio3" Text="N/A" GroupName="DcRadioGroup" runat="server"  />

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

                            <button id="DrResetButton" onclick="openModal()" type="button" class="btn btn-primary" value="Reset">Reset</button>
                                <asp:Button ID="BtnUpdateDr" Visible="false" ClientIDMode="Static" runat="server" Text="UPDATE" CssClass="btn btn-primary" ValidationGroup="DrValidationGroup" OnClick="BtnUpdateDr_Click"/>
                             <asp:Button ID="BtnSubmitACh" Visible="false"  ClientIDMode="Static" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmitACa_Click"/>


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

        function CheckPrimaryAccountHolder(source, args) {
            var Grid1 = document.getElementById("<%=GridViewAccountCifs.ClientID%>");
            var flag = false;
            // args.IsValid = false;
            console.log(flag);
            if (Grid1.rows.length > 1) {
                for (var i = 1; i < Grid1.rows.length; i++) {
                    console.log(flag);
                    if (Grid1.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML == '1') {
                        flag = true;
                        //  args.IsValid = true;
                        console.log(flag);
                    }
                }
                console.log(flag);
                if (flag)
                    args.IsValid = true;
                else
                    args.IsValid = false;

            }
        }

        function JointMoreCustomer(source, args) {
            var Grid1 = document.getElementById("<%=GridViewAccountCifs.ClientID%>");
            if (Grid1 == null) {
                args.IsValid = false;
            }
            else if (Grid1.rows.length < 3) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }



        function CheckPrimaryAccountHolderSingle(source, args) {
            var Grid1 = document.getElementById("<%=GridViewAccountCifs.ClientID%>");
            var count = 0;

            for (var i = 1; i < Grid1.rows.length; i++) {
                if (Grid1.rows[i].cells[1].getElementsByTagName("span")[0].innerHTML == '1') {
                    count++;
                }
            }
            if (count > 1)
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        function doCustomValidateContact(source, args) {

            args.IsValid = false;

            if (document.getElementById('<% =AdOffice.ClientID %>').value.length > 0) {
                args.IsValid = true;
            }
            if (document.getElementById('<% =AdResidence.ClientID %>').value.length > 0) {
                args.IsValid = true;
            }
            if (document.getElementById('<% =AdMobileNo.ClientID %>').value.length > 0) {
                args.IsValid = true;
            }
        }

        function doCustomValidateContactNk(source, args) {

            args.IsValid = false;

            if (document.getElementById('<% =NktxtResidenceContact.ClientID %>').value.length > 0) {
                args.IsValid = true;
            }
            if (document.getElementById('<% =NkTxtOfficeNo.ClientID %>').value.length > 0) {
                 args.IsValid = true;
             }
             if (document.getElementById('<% =NkTxtMobileNo.ClientID %>').value.length > 0) {
                 args.IsValid = true;
             }
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

        function IndividualAccountOpenPendingAlertBio(var1, var2, var3, var4, var5, var6, var7, mesg, success) {
           
            if (success == "1") {
                $('#alerts').append(
                '<div class="alert alert-success">' +
                    '<button type="button" class="close" data-dismiss="alert">' +
                    '&times;</button>' + mesg + '</div>');
            }
            else {
                $('#alerts').append(
                '<div class="alert alert-danger">' +
                    '<button type="button" class="close" data-dismiss="alert">' +
                    '&times;</button>' + mesg + '</div>');
            }


            document.getElementById("AcResetButton").style.visibility = "hidden";
            document.getElementById("InaccountNatureAnchor").text = "\u2714 A/C Nature & Currency";
            //alert("Heelo");
            // $('.nav-tabs a[href="#sectionb"]').tab('show');

            if (var1 == "1") {
                document.getElementById("ApResetButton").style.visibility = "hidden";
                document.getElementById("InApplicantInformationAnchor").text = "\u2714 Applicant Information";


            }



            if (var2 == "1") {
                document.getElementById("AdResetButton").style.visibility = "hidden";
                document.getElementById("InAddressInformationAnchor").text = "\u2714 Address Information";
                console.log(var2);


            }

            if (var3 == "1") {

                document.getElementById("AuResetButton").style.visibility = "hidden";
                document.getElementById("InOperatingInstructionAnchor").text = "\u2714 Operating Instructions";



            }

            if (var4 == "1") {

                document.getElementById("NkResetButton").style.visibility = "hidden";
                document.getElementById("InNextOfKinAnchor").text = "\u2714 Next Of Kin Info";


            }

            if (var5 == "1") {

                document.getElementById("KnResetButton").style.visibility = "hidden";
                document.getElementById("InKnowYourCustomerAnchor").text = "\u2714 Know Your Customer";


            }

            if (var6 == "1") {
                document.getElementById("CdResetButton").style.visibility = "hidden";
                document.getElementById("InCertDepositInfoAnchor").text = "\u2714 Certificate Deposit Info";

            }

            if (var7 == "1") {
                document.getElementById("DrResetButton").style.visibility = "hidden";
                document.getElementById("InDocumentRequiredAnchor").text = "\u2714 Document Required";

            }


        }
       
    </script>
     

</asp:Content>

