<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/DCP.Master" CodeBehind="DailyPaymentsReport.aspx.cs" Inherits="DailyCollectionAndPayments.DailyPaymentsReport" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  
        <script type="text/javascript"> 
            function UserDeleteConfirmation() {
                if (confirm("Are you sure you want to delete this Record?"))
                    return true;
                else
                    return false;
            }
        $(function () {
         $('#<%=txtDate.ClientID%>').datepicker({
                showOn:'both',
                changeMonth:true,
                changeYear:true,
                buttonText:'Cal',
                maxDate: 0
            });
             $('#<%= ddlState.ClientID %>').select2({
                disable_search_threshold: 5, search_contains: true, minimumResultsForSearch: 20,
                placeholder: "Select an option"
            });
            $('#<%= ddlProject.ClientID %>').select2({
                disable_search_threshold: 5, search_contains: true, minimumResultsForSearch: 20,
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
                  var ddlpaymenttype = $('#<%= ddlSelectPayment.ClientID %> option:selected').text().toLowerCase();
             if (ddlpaymenttype === '--select--')
                 return alert("Please select PaymentType");
             var amount = $('#<%= txtAmount.ClientID %>').val();
             if (amount == '')
                 return alert('Please enter  Amount')
                   var datepick = $('#<%= txtDate.ClientID %>').val();
                 if (datepick == '')
                 return alert('Please enter  Date')
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
     <legend align="center" style="color:brown;margin-top:-50px"> Daily Payments</legend>  
          <br />
           
    <table align="center" style="margin-top:20px">
        <tr>
            <td>
                State<span style="color:red">*</span>
            </td>
            <td>
                <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true" Width="150px"></asp:DropDownList>
            </td>
           
            </tr>
         
        <tr>
            <td>
                 Project<span style="color:red;">*</span>
            </td>
            <td>
                <asp:DropDownList ID="ddlProject"  runat="server"  Width="150px"></asp:DropDownList>
            </td>
            
            </tr>
      
        <tr>
            <td>
                PaymentType<span style="color:red">*</span>
            </td>
            <td>
                <asp:DropDownList ID="ddlSelectPayment" CssClass="search_3"  runat="server"  Width="150px"></asp:DropDownList>
            </td>
        </tr>
 
        <tr>
            <td>
                Date<span style="color:red">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtDate" runat="server" onkeypress="return false" oncut="return false" onpaste="return false" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Amount<span style="color:red">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtAmount"  runat="server" placeholder="Enter Amount" onkeypress="return numericOnly(this)"></asp:TextBox>
            </td>
        </tr>

        <tr>
           
            <td >
                <asp:Button ID="btnSave" runat="server" CssClass="form-submit-button" Text="Save" style="margin-top:20px;margin-left:100px" OnClientClick="if(!Validations()) return false" OnClick="btnSave_Click" />
            </td>
            <td>
                <asp:Button ID="btnReset" runat="server" CssClass="form-reset-button" Text="Reset" style="margin-top:20px" OnClick="btnReset_Click" />
            </td>
        </tr>
    </table>
    <br />
    <div align="center" style="margin-top:25px;width:1000px;margin-left:100px">
     <asp:GridView ID="gvPayments" runat="server" BackColor="White" AutoGenerateColumns="False" OnRowDeleting="gvPayments_RowDeleting"  BorderColor="#CCCCCC" BorderStyle="None" OnRowEditing="gvPayments_RowEditing" BorderWidth="1px" CellPadding="3" OnSelectedIndexChanged="gvPayments_SelectedIndexChanged" GridLines="Both" width="1000px" >
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
 <Columns>
       <asp:TemplateField HeaderText="PID">  
                    <ItemTemplate>  
                        <asp:Label ID="P_ID" runat="server" Text='<%#Eval("p_id") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>
      <asp:TemplateField HeaderText="STATE">  
                    <ItemTemplate>  
                        <asp:Label ID="lblStateName" runat="server" Text='<%#Eval("state_name") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="PROJECT">  
                    <ItemTemplate>  
                        <asp:Label ID="lblProjectName" runat="server" Text='<%#Eval("project_name") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:TextBox ID="txtProjectName" runat="server" Text='<%#Eval("project_name") %>'></asp:TextBox>  
                    </EditItemTemplate>  
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="USER">  
                    <ItemTemplate>  
                        <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("user_name") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:TextBox ID="txtUserName" runat="server" Text='<%#Eval("user_name") %>'></asp:TextBox>  
                    </EditItemTemplate>  
                </asp:TemplateField>  
             <asp:TemplateField HeaderText="AMOUNT">  
                    <ItemTemplate>  
                        <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("amount") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:TextBox ID="txtAmount" runat="server" Text='<%#Eval("amount") %>'></asp:TextBox>  
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
                                           <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" Width="20px" CommandName="" ImageUrl="~/images/edit1.png" OnClick="btnEdit_Click" Text=""  ToolTip="" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
              <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                           <asp:ImageButton ID="btnDelete" runat="server" CausesValidation="false"  Width="20px" CommandName="Delete" ImageUrl="~/images/delete.png" Text="" OnClientClick="return UserDeleteConfirmation();"  ToolTip="" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                </Columns>
       </asp:GridView>
    </div>
    </asp:Content>
