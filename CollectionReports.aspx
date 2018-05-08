<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/DCP.Master" CodeBehind="CollectionReports.aspx.cs" Inherits="DailyCollectionAndPayments.CollectionReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
         $(function () {
            $('#<%= ddlMonth.ClientID %>').select2({
                disable_search_threshold: 5, search_contains: true, minimumResultsForSearch: 2,
                placeholder: "Select an option"
            });
             $('#<%= ddlYear.ClientID %>').select2({
                disable_search_threshold: 5, search_contains: true, minimumResultsForSearch: 2,
                placeholder: "Select an option"
            });
        });
    </script>
<table align="center">
    <tr>
        <td>
            <h4 style="color:brown;margin-top:1px">Collection Reports</h4>
        </td>
    </tr>
    <br />
</table>
    <table align="center">
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
                <asp:Button runat="server"  id="btnShowReport"  class="form-submit-button" Text="ShowReport" ClientIDMode="static" EnableViewState="True"  OnClientClick="if(!Validations()) return false;" OnClick="btnShowReport_Click"></asp:Button>
            </td>

            <td>
                <asp:Button runat="server" Text="ExportExcel" class="form-reset-button" OnClick="ExportToExcel_Click"></asp:Button>
            </td>
        </tr>
    </table>
    <br />
    <div align="center">
        <asp:GridView ID="gvCollectionReport" runat="server" GridLines="Both" style="margin-top:20px" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
    </div>
    </asp:Content>