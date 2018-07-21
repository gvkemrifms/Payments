<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/DCP.Master" CodeBehind="DailyCollectionReport.aspx.cs" Inherits="DailyCollectionAndPayments.DailyPaymentsReport1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <script type="text/javascript">
       
        function UserDeleteConfirmation() {
            if (confirm("Are you sure you want to delete this user?"))
                return true;
            else
                return false;
        }

        $(function() {
            $('#<%= txtDate.ClientID %>').datepicker({
                dateFormat: 'dd-mm-yy',
                changeMonth: true,
                changeYear: true,
                maxDate: 0
            });
            $('#<%= ddlState.ClientID %>,#<%= ddlProject.ClientID %>').select2({
                disable_search_threshold: 5,
                search_contains: true,
                minimumResultsForSearch: 20,
                placeholder: "Select an option"
            });
        });

        function Validations() {
            var ddlstate = $('#<%= ddlState.ClientID %> option:selected').text().toLowerCase();
            if (ddlstate === '--select--')
                return alert("Please select State");
            var ddlproject = $('#<%= ddlProject.ClientID %> option:selected').text().toLowerCase();
            if (ddlproject === '--select--')
                return alert("Please select Project");
            var validDate = $('#<%= txtDate.ClientID %>').val();
            var amount = $('#<%= txtAmount.ClientID %>').val();
            if (validDate === '')
                return alert('Please enter  Date');
            if (amount === '')
                return alert('Please enter  Amount');
            return true;
        }
        function numeric_only(e) {
            var keycode;
            if (window.event || event || e) keycode = window.event.keyCode;
            else return true;
            return keycode >= 48 && keycode <= 57;
        }
    </script>
    <table align="center">
        <tr>
            <td>
                <h4 style="color: brown; margin-top: 25px">Daily Collections</h4>
            </td>
        </tr>
    </table>
    <br/>
    <table align="center" style="margin-top: 20px">
        <tr>
            <td>
                State<span style="color: red">*</span>
            </td>
            <td>
                <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true" Width="150px"></asp:DropDownList>
            </td>

        </tr>

        <tr>
            <td>
                Project<span style="color: red;">*</span>
            </td>
            <td>
                <asp:DropDownList ID="ddlProject" runat="server" Width="150px"></asp:DropDownList>
            </td>

        </tr>

        <tr>
            <td>
                Date<span style="color: red">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtDate" runat="server" onkeypress="return false" oncut="return false" onpaste="return false"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>
                Amount (In Lakhs)<span style="color: red">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtAmount" runat="server" placeholder="Enter Amount" onkeypress="return numeric_only(event)"></asp:TextBox>
            </td>
        </tr>

        <tr>

            <td >
                <asp:Button ID="btnSave" runat="server" CssClass="form-submit-button" Text="Save" style="margin-top: 20px; margin-left: 100px" OnClientClick="if (!Validations()) return false;" OnClick="btnSave_Click"/>
            </td>
            <td>
                <asp:Button ID="btnReset" runat="server" CssClass="form-reset-button" Text="Reset" style="margin-top: 20px" OnClick="btnReset_Click"/>
            </td>
        </tr>
    </table>


    <div align="center" style="margin-top: 20px; width: 1000px; margin-left: 100px">
        <asp:GridView ID="gvDailyPayments" runat="server" BackColor="White" AutoGenerateColumns="False" OnRowDeleting="gvDailyPayments_RowDeleting" OnSelectedIndexChanging="gvDailyPayments_SelectedIndexChanging" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnSelectedIndexChanged="gvDailyPayments_SelectedIndexChanged" OnRowEditing="gvDailyPayments_RowEditing" OnRowUpdating="gvDailyPayments_RowUpdating" Width="1000px">
            <FooterStyle BackColor="White" ForeColor="#000066"/>
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White"/>
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left"/>
            <RowStyle ForeColor="#000066"/>
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White"/>
            <SortedAscendingCellStyle BackColor="#F1F1F1"/>
            <SortedAscendingHeaderStyle BackColor="#007DBB"/>
            <SortedDescendingCellStyle BackColor="#CAC9C9"/>
            <SortedDescendingHeaderStyle BackColor="#00547E"/>
            <Columns>
                <asp:TemplateField HeaderText="CID">
                    <ItemTemplate>
                        <asp:Label ID="C_ID" runat="server" Text='<%#Eval("c_id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="State">
                    <ItemTemplate>
                        <asp:Label ID="lbl_ID" runat="server" Text='<%#Eval("state_name") %>'></asp:Label>

                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_StateName" runat="server" Text='<%#Eval("state_name") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PROJECT">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Name" runat="server" Text='<%#Eval("project_name") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_Name" runat="server" Text='<%#Eval("project_name") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="DATE">
                    <ItemTemplate>
                        <asp:Label ID="lbldate" runat="server" Text='<%#Eval("date") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtdate" runat="server" Text='<%#Eval("date") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="AMOUNT">
                    <ItemTemplate>
                        <asp:Label ID="lblamount" runat="server" Text='<%#Eval("amount") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtamount" runat="server" Text='<%#Eval("amount") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ENTRY DATE">
                    <ItemTemplate>
                        <asp:Label ID="lblcreatedon" runat="server" Text='<%#Eval("createdon") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtdate" runat="server" Text='<%#Eval("createdon") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" Width="20px" CommandName="" ImageUrl="~/images/edit1.png" Text="" OnClick="btnEdit_Click" ToolTip=""/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnDelete" runat="server" CausesValidation="false" Width="20px" CommandName="Delete" ImageUrl="~/images/delete.png" Text="" OnClick="btnDelete_Click" OnClientClick="return UserDeleteConfirmation();" ToolTip=""/>
                    </ItemTemplate>
                </asp:TemplateField>

                <%--<asp:CommandField  ShowSelectButton="true" ControlStyle-Width="20px" ControlStyle-Height="20px" ControlStyle-ForeColor="Blue" SelectText="Edit" ButtonType="Image" SelectImageUrl="~/images/edit1.png"  />--%>
            </Columns>

        </asp:GridView>
    </div>
</asp:Content>
