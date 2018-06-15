<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/DCP.Master" CodeBehind="CollectionReports.aspx.cs" Inherits="DailyCollectionAndPayments.CollectionReports" %>

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
                <h4 style="color: brown; margin-top: 1px">Collection Statement</h4>
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
    <asp:Panel ID="pnlCollectionReport" HorizontalAlign="Center" runat="server">
        <h4 align="center" style="color: brown">Daily Collection Statement (In Lakhs)</h4>
        <asp:GridView ID="gvCollectionReport" runat="server" GridLines="Both" style="margin-top: 20px" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
            <FooterStyle BackColor="White" ForeColor="#000066"/>
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White"/>
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left"/>
            <RowStyle ForeColor="#000066"/>
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White"/>
            <SortedAscendingCellStyle BackColor="#F1F1F1"/>
            <SortedAscendingHeaderStyle BackColor="#007DBB"/>
            <SortedDescendingCellStyle BackColor="#CAC9C9"/>
            <SortedDescendingHeaderStyle BackColor="#00547E"/>
        </asp:GridView>
    </asp:Panel>
</asp:Content>