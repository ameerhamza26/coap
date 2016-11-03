<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Default.Master" CodeBehind="BioMetricReport.aspx.cs" Inherits="CAOP.AOF.BioMetricReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Body" ContentPlaceHolderID="Body" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <h3 style="margin-top: 105px">Bio-Metric Verification</h3>
    <div class="row" >
            <div class="col-md-3">
                <div class="form-group">
                    <label>CNIC: (WithOut Dahses)</label>                   
                     <asp:TextBox ID="txtCnic" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" style="margin-top: 20px" runat="server"  CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />                 
                 </div>
             </div>
        </div>

    
    <rsweb:ReportViewer Visible="false" ID="ReportViewer1" runat="server"></rsweb:ReportViewer>
</asp:Content>