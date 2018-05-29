<%@Page Language="C#" AutoEventWireup="true" MasterPageFile="~/DCP.Master" CodeBehind="ActualExpectedCollectionReports.aspx.cs" Inherits="DailyCollectionAndPayments.ActualExpectedCollectionReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function() {
            $('#<%= ddlmonth.ClientID %>').select2({
                disable_search_threshold: 5,
                search_contains: true,
                minimumResultsForSearch: 2,
                placeholder: "Select an option"
            });
            $('#<%= ddlyear.ClientID %>').select2({
                disable_search_threshold: 5,
                search_contains: true,
                minimumResultsForSearch: 2,
                placeholder: "Select an option"
            });
        });
    </script>
    <legend align="center" style="color: brown">Actual And Expected Collection Report</legend>
    <table align="center" style="margin-top: 10px">
        <tr>
            <td>
                Month<span style="color: red">*</span>
            </td>
            <td>
                <asp:DropDownList ID="ddlmonth" runat="server" Width="150px"></asp:DropDownList>
            </td>

        </tr>

        <tr>
            <td>
                Year<span style="color: red;">*</span>
            </td>
            <td>
                <asp:DropDownList ID="ddlyear" runat="server" Width="150px"></asp:DropDownList>
            </td>

        </tr>
        <br/>
        <tr>
            <td>
                <asp:Button ID="btnShowReport" runat="server" CssClass="form-submit-button" Text="Show Report" OnClick="btnShowReport_OnClick"/>
            </td>

        </tr>
    </table>
    <br/>
    <div align="center">
        <asp:GridView ID="gvActualCollectionReport" runat="server" EmptyDataText="No Records Found" style="flex-wrap: nowrap" AutoGenerateColumns="False"
                      CssClass="gridview"
                      CellPadding="1" BorderColor="#CCCCCC" border-width="1px"
                      Width="1300px"
                      OnRowCommand="gvActualCollectionReport_OnRowCommand" BackColor="White" BorderStyle="None">
            <Columns>
                <asp:TemplateField HeaderText="StateProjects">
                    <ItemTemplate>
                        <asp:Label ID="lblStateProject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "stateproject") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Expected(1-10)">
                    <ItemTemplate>
                        <asp:Label ID="lblExpectedFirst10DaysCollection" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Expected1to10") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actual(1-10)">
                    <ItemTemplate>
                        <asp:Label ID="lblActual10DaysCollection" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Actual1to10") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Send Email">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkSendEmail1to10" runat="server" CommandName="1to10" CssClass="form-submit-button" CommandArgument=" <%# Container.DataItemIndex %>"
                                        Text="Send Email">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Expected(11-20)">
                    <ItemTemplate>
                        <asp:Label ID="lblExpected20DaysCollection" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Expected11to20") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Actual(11-20)">
                    <ItemTemplate>
                        <asp:Label ID="lblActual20DaysCollection" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Actual11to20") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Send Email">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkSendEmail11to20" runat="server" CommandName="11to20" CssClass="form-submit-button" CommandArgument=" <%# Container.DataItemIndex %>" Text="Send Email">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Expected(21-EndOftheMonth)">
                    <ItemTemplate>
                        <asp:Label ID="lblExpectedMonthEndCollection" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Expected21to31") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actual(21-EndOftheMonth)">
                    <ItemTemplate>
                        <asp:Label ID="lblActualMonthEndCollection" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Actual21to31") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Send Email">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkSendEmail21toend" runat="server" CommandName="21toend" CssClass="form-submit-button" CommandArgument=" <%# Container.DataItemIndex %>"
                                        Text="Send Email">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Expected Total">
                    <ItemTemplate>
                        <asp:Label ID="lblEstimatedTotal" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ExpectedTotal") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actual Total">
                    <ItemTemplate>
                        <asp:Label ID="lblActualTotal" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ActualTotal") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Send Email">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkSendEmail" runat="server" CommandName="send" CssClass="form-submit-button" CommandArgument=" <%# Container.DataItemIndex %>"
                                        Text="Send Email" OnClick="lnkSendEmail_OnClick">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle CssClass="footerStylegrid" BackColor="White" ForeColor="#000066"/>
            <PagerStyle CssClass="pagerStylegrid" BackColor="White" ForeColor="#000066" HorizontalAlign="Left"/>
            <RowStyle ForeColor="#000066"/>
            <SelectedRowStyle CssClass="selectedRowStyle" BackColor="#669999" Font-Bold="True" ForeColor="White"/>
            <HeaderStyle CssClass="headerStyle" BackColor="#006699" Font-Bold="True" ForeColor="White"/>
            <SortedAscendingCellStyle BackColor="#F1F1F1"/>
            <SortedAscendingHeaderStyle BackColor="#007DBB"/>
            <SortedDescendingCellStyle BackColor="#CAC9C9"/>
            <SortedDescendingHeaderStyle BackColor="#00547E"/>
        </asp:GridView>
    </div>
</asp:Content>