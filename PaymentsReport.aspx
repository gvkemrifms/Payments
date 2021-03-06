﻿<%@ Page Language="C#" MasterPageFile="~/DCP.Master" AutoEventWireup="true" CodeBehind="PaymentsReport.aspx.cs" EnableEventValidation="false" Inherits="DailyCollectionAndPayments.PaymentsReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style >
        .ui-widget {
            font-family: Verdana,Arial,sans-serif;
            font-size: .8em;
        }

        .ui-widget-content {
            background: #F9F9F9;
            border: 1px solid #90d93f;
            color: #222222;
        }

        .ui-dialog {
            left: 0;
            outline: 0 none;
            padding: 0 !important;
            position: absolute;
            top: 0;
        }

        #success {
            padding: 0;
            margin: 0; 
        }

        .ui-dialog .ui-dialog-content {
            background: none repeat scroll 0 0 transparent;
            border: 0 none;
            overflow: auto;
            position: relative;
            padding: 0 !important;
        }

        .ui-widget-header {
            background: #b0de78;
            border: 0;
            color: #fff;
            font-weight: normal;
        }

        .ui-dialog .ui-dialog-titlebar {
            padding: 0.1em .5em;
            position: relative;
            font-size: 1em;
        }
    </style>
    <script type="text/javascript">

        $(function() {
            $('#<%= ddlMonth.ClientID %>,#<%= ddlYear.ClientID %>').select2({
                disable_search_threshold: 5,
                search_contains: true,
                minimumResultsForSearch: 2,
                placeholder: "Select an option"
            });          
        });
        function DisplayToolTip(i) {
             //alert(i+"  "+"(Selected). Please click on Payments Button to get Daywise Payments Report");
             document.getElementById('<%=myHiddenField.ClientID%>').value = i;
            
         <%--   var formname = '<%=this.Page.Form.Name %>';
            formname.submit();--%>
        }
        function mouseIn() {
            document.body.style.cursor = 'pointer';
        }
        function mouseOut() {
            document.body.style.cursor = 'auto';
        }
        function popup() {
            $("#divDialog").dialog({
                width: 700,
                height: 350,
                modal: true,
                opacity: .7,
                closeOnEscape: true,
                draggable: true,
                resizable: true
            });
        };
       
    </script>
        <asp:HiddenField ID="myHiddenField" runat="server" />
    <table align="center">
        <tr>
            <td>
                <h4 style="color: brown; margin-top: 1px">Payments Statement</h4>
            </td>
        </tr>
        <br/>
    </table>
    <table align="center" style="margin-top: 30px">
        <tr>
            <td>
                <asp:Label ID="lblMonth" runat="server" Text="Month"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlMonth" Width="150px" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlYear" Width="150px" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>

            <td>
                <asp:Button runat="server" id="btnShowReport" class="form-submit-button" Text="Report" ClientIDMode="static" EnableViewState="True"  OnClick="btnShowReport_Click" OnClientClick="if (!Validations()) return false;"></asp:Button>
            </td>
           
            <td>
                <asp:Button runat="server" Text="Excel Report" class="form-reset-button" OnClick="ExportToExcel_Click"></asp:Button>
            </td>
            
        </tr>
    </table>
    <br/>
    <div id="divDialog" title="Daywise payments Report" align="center">
        <asp:PlaceHolder ID="PlaceHolder1" runat="server" ></asp:PlaceHolder>
    </div>  
    <br/>
    <asp:Panel ID="lblPaymentReport" runat="server">
        <h4 align="center" style="color: brown">Daily Payments Statement (Rs. In Lakhs)</h4>
        <asp:GridView ID="gvPaymentsReport" runat="server" style="flex-wrap: nowrap; margin-top: 20px; width: 90%" EmptyDataText="No Records Found"   OnRowDataBound="gvPaymentsReport_RowDataBound1"  BackColor="LightGoldenrodYellow" AllowSorting="True" GridLines="both" OnSorting="gvPaymentsReport_Sorting" BorderColor="Tan"  BorderWidth="1px" CellPadding="2" ForeColor="Black">
            <RowStyle Wrap="False"/>
            <AlternatingRowStyle BackColor="PaleGoldenrod" />
            <EmptyDataRowStyle Wrap="False"/>
            <FooterStyle BackColor="Tan" Wrap="false"/>
            <HeaderStyle BackColor="Tan" Font-Bold="True" Wrap="false"/>
            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center"/>
            <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite"/>
            <SortedAscendingCellStyle BackColor="#FAFAE7"/>
            <SortedAscendingHeaderStyle BackColor="#DAC09E"/>
            <SortedDescendingCellStyle BackColor="#E1DB9C"/>
            <SortedDescendingHeaderStyle BackColor="#C2A47B"/>


        </asp:GridView>
    </asp:Panel>
</asp:Content>

