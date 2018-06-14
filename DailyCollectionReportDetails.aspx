<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/DCP.Master" CodeBehind="DailyCollectionReportDetails.aspx.cs" Inherits="DailyCollectionAndPayments.DailyCollectionReportDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 1000px">
        <table>
            <tr>
                <td>
                    <div style="margin-left: 20px; margin-right: 100px; margin-top: 25px; width: 500px">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" style="color: blue">New Collection</asp:LinkButton>
                        <asp:GridView ID="GvCollections" runat="server" BackColor="White" BorderColor="#CCCCCC" align="left" BorderStyle="None" BorderWidth="1px" CellPadding="3">
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
                    </div>
                </td>
                <td>
                    <div style="margin-top: -210px; width: 500px";align:"center">
                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" style="color: blue; margin-left: 20px">New Payment</asp:LinkButton>
                        <asp:GridView ID="GvPayments" runat="server" style="margin-left: 50px" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
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
                    </div>
                </td>
            </tr>
        </table>

    </div>

</asp:Content>