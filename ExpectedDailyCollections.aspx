<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/DCP.Master" CodeBehind="ExpectedDailyCollections.aspx.cs" Inherits="DailyCollectionAndPayments.ExpectedDailyCollections" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <script type="text/javascript">
        $(function() {
            $('#<%= ddlState.ClientID %>').select2({
                disable_search_threshold: 5,
                search_contains: true,
                minimumResultsForSearch: 2,
                placeholder: "Select an option"
            });
            $('#<%= ddlProject.ClientID %>').select2({
                disable_search_threshold: 5,
                search_contains: true,
                minimumResultsForSearch: 2,
                placeholder: "Select an option"
            });
            $('#<%= ddldate.ClientID %>').select2({
                disable_search_threshold: 5,
                search_contains: true,
                minimumResultsForSearch: 10,
                placeholder: "Select an option"
            });
        });

        function UserDeleteConfirmation() {
            if (confirm("Are you sure you want to delete this Record?"))
                return true;
            else
                return false;
        }

        function Validations() {
            var ddlstate = $('#<%= ddlState.ClientID %> option:selected').text().toLowerCase();
            if (ddlstate === '--select--')
                return alert("Please select State");
            var ddlproject = $('#<%= ddlProject.ClientID %> option:selected').text().toLowerCase();
            if (ddlproject === '--select--')
                return alert("Please select Project");

            var amount = $('#<%= txtAmount.ClientID %>').val();

            if (amount === '')
                return alert('Please enter  Amount');
            var ddlexpecteddate = $('#<%= ddldate.ClientID %> option:selected').text().toLowerCase();
            if (ddlexpecteddate === '--select--')
                return alert("Please select date");
            return true;
        }

        function numericOnly(elementRef) {

            var keyCodeEntered = (event.which) ? event.which : (window.event.keyCode) ? window.event.keyCode : -1;
            if ((keyCodeEntered >= 48) && (keyCodeEntered <= 57)) {
                return true;
            }
            // '.' decimal point...  
            else if (keyCodeEntered === 46) {
                // Allow only 1 decimal point ('.')...  
                if ((elementRef.value) && (elementRef.value.indexOf('.') >= 0))
                    return false;
                else
                    return true;
            }
            return false;
        }
    </script>
    <table align="center">
        <tr>
            <td>
                <h4 style="color: brown; margin-top: 25px; width: 242px;">Daily Expected Collections</h4>
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
                <asp:DropDownList ID="ddlProject" runat="server" Width="150px" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
            </td>

        </tr>

        <tr>
            <td>
                Year<span style="color: red">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtYear" runat="server" placeholder="Year" ReadOnly="True" style="color: gray" onkeypress="return numericOnly(this)" OnTextChanged="txtYear_TextChanged"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>
                Month<span style="color: red">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtMonth" runat="server" ReadOnly="True" style="color: gray" placeholder="Month" onkeypress="return numericOnly(this)"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>
                Date<span style="color: red;">*</span>
            </td>
            <td>
                <asp:DropDownList ID="ddldate" runat="server" Width="150px">
                </asp:DropDownList>
            </td>

        </tr>
        <tr>
            <td>
                Amount<span style="color: red">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtAmount" runat="server" placeholder="Enter Amount" onkeypress="return numericOnly(this)"></asp:TextBox>
            </td>
        </tr>

        <tr>

            <td >
                <asp:Button ID="btnSave" runat="server" CssClass="form-submit-button" Text="Save" style="margin-left: 100px; margin-top: 20px;" OnClientClick="if (!Validations()) return false;" OnClick="btnSave_Click"/>
            </td>
            <td>
                <asp:Button ID="btnReset" runat="server" CssClass="form-reset-button" Text="Reset" style="margin-top: 20px" OnClick="btnReset_Click"/>
            </td>
        </tr>
    </table>


    <div align="center" style="margin-left: 100px; margin-top: 20px; width: 1000px;">
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
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>