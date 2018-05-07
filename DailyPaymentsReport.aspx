<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/DCP.Master" CodeBehind="DailyPaymentsReport.aspx.cs" Inherits="DailyCollectionAndPayments.DailyPaymentsReport" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  
        <script type="text/javascript"> 
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
                Select State<span style="color:red">*</span>
            </td>
            <td>
                <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true" Width="150px"></asp:DropDownList>
            </td>
           
            </tr>
         
        <tr>
            <td>
                Select Project<span style="color:red;">*</span>
            </td>
            <td>
                <asp:DropDownList ID="ddlProject"  runat="server"  Width="150px"></asp:DropDownList>
            </td>
            
            </tr>
      
        <tr>
            <td>
                Select PaymentType<span style="color:red">*</span>
            </td>
            <td>
                <asp:DropDownList ID="ddlSelectPayment" CssClass="search_3"  runat="server"  Width="150px"></asp:DropDownList>
            </td>
        </tr>
 
        <tr>
            <td>
                Select Date<span style="color:red">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtDate" runat="server" onkeypress="return false" oncut="return false" onpaste="return false" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Select Amount<span style="color:red">*</span>
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
     <asp:GridView ID="gvPayments" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnSelectedIndexChanged="gvPayments_SelectedIndexChanged" GridLines="Both" width="1000px" >
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
                    <asp:CommandField  ShowSelectButton="true" ControlStyle-ForeColor="Blue" SelectText="Edit" HeaderText="Edit" />
                  <asp:CommandField ShowDeleteButton="true" ControlStyle-ForeColor="Blue" SelectText="Delete" HeaderText="Delete" />
                </Columns>
       </asp:GridView>
    </div>
    </asp:Content>
