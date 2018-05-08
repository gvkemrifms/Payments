<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/DCP.Master" CodeBehind="CollectionReports.aspx.cs" Inherits="DailyCollectionAndPayments.CollectionReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <legend align="center" style="color:white">Collection Report</legend>
    <table align="center">
        <tr>
             <td>
                <asp:Button runat="server"  id="btnShowReport"  class="form-submit-button" Text="ShowReport" OnClick="btnsubmit_Click" ClientIDMode="static" EnableViewState="True"  OnClientClick="if(!Validations()) return false;"></asp:Button>
            </td>

            <td>
                <asp:Button runat="server" Text="ExportExcel" class="form-reset-button" OnClick="btntoExcel_Click"></asp:Button>
            </td>
        </tr>
    </table>
    <div align="center">
        <asp:GridView ID="gvCollectionReport" runat="server" style="margin-top:20px" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
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