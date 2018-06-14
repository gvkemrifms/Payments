<%@ Page Language="C#" MasterPageFile="~/DCP.Master" AutoEventWireup="true" CodeBehind="PaymentsReport.aspx.cs" Inherits="DailyCollectionAndPayments.PaymentsReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function() {
            $('#<%= ddlMonth.ClientID %>,#<%= ddlYear.ClientID %>').select2({
                disable_search_threshold: 5,
                search_contains: true,
                minimumResultsForSearch: 2,
                placeholder: "Select an option"
            });          
        });
    </script>
    <table align="center">
        <tr>
            <td>
                <h4 style="color: brown; margin-top: 1px">Payments Reports</h4>
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
                <asp:Button runat="server" id="btnShowReport" class="form-submit-button" Text="Report" ClientIDMode="static" EnableViewState="True" OnClientClick="if (!Validations()) return false;" OnClick="btnShowReport_Click"></asp:Button>
            </td>

            <td>
                <asp:Button runat="server" Text="Excel Report" class="form-reset-button" OnClick="ExportToExcel_Click"></asp:Button>
            </td>
        </tr>
    </table>
    <br/>
    <asp:Panel ID="lblPaymentReport" runat="server">
        <h4 align="center" style="color: brown">Daily Payments Report</h4>
        <asp:GridView ID="gvPaymentsReport" runat="server" style="flex-wrap: nowrap; margin-top: 20px; width: 90%" EmptyDataText="No Records Found" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" GridLines="Both" CellPadding="3">
            <RowStyle Wrap="False"/>
            <EmptyDataRowStyle Wrap="False"/>
            <FooterStyle BackColor="White" ForeColor="#000066" Wrap="false"/>
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" Wrap="false"/>
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left"/>
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White"/>
            <SortedAscendingCellStyle BackColor="#F1F1F1"/>
            <SortedAscendingHeaderStyle BackColor="#007DBB"/>
            <SortedDescendingCellStyle BackColor="#CAC9C9"/>
            <SortedDescendingHeaderStyle BackColor="#00547E"/>

        </asp:GridView>
    </asp:Panel>
</asp:Content>