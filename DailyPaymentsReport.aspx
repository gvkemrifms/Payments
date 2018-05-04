<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/DCP.Master" CodeBehind="DailyPaymentsReport.aspx.cs" Inherits="DailyCollectionAndPayments.DailyPaymentsReport1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <style type="text/css">
        .MenuBar {
            margin-top: -3%;
            background-color: black;           
        }

        .main_menu, .main_menu:hover {
            width: 175px;
            background-color: black;
            color: white;
            text-align: center;
            height: 30px;
            line-height: 25px;
            margin-right: 3px;
            font-weight: bolder;
            
        }

        #MainContent_menuBar ul {
            list-style: none;
            margin: 0;
            padding: 0;
            width: auto;
            margin-left: 0%;
            height: 30px;
            float: right;
        }

        .main_menu:hover {
            background-color: #ccc;
            font-weight: bolder;
        }

        .level_menu, .level_menu:hover {
            width: 100%;
            background-color: black;
            color: white;
            text-align: center;
            height: 30px;
            line-height: 30px;
            /*margin-top: 5px;*/
        }

            .level_menu:hover {
                background-color: #ccc;
            }

        .selected, .selected:hover {
            background-color: white;
            color: black;
        }

        .level2 {
            background-color: #C36464;
            left: 90px;
            color: white;
            text-align: left;
        }
        .level1 
        {
           
            float: right;
        }
    </style>

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
                return alert("Please select Vehicle");
            return true;
        }
    </script>
  <legend align="center" style="color:brown">Consolidated Daily Payments</legend>  
          <br />
           
    <table align="center" style="margin-top:20px">
        <tr>
            <td>
                Select State<span style="color:red">*</span>
            </td>
            <td>
                <asp:DropDownList ID="ddlState" runat="server" Width="150px"></asp:DropDownList>
            </td>
            </tr>
        <tr>
            <td>
                Select Project<span style="color:red">*</span>
            </td>
            <td>
                <asp:DropDownList ID="ddlProject" runat="server" Width="150px"></asp:DropDownList>
            </td>
            </tr>
        <tr>
            <td>
                Select Date<span style="color:red">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtDate" runat="server" onkeypress="return false" oncut="return false" onpaste="return false" placeholder="Select Date(MM/dd/YYYY)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Select Amount<span style="color:red">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtAmount" runat="server" placeholder="Enter Amount"></asp:TextBox>
            </td>
        </tr>
        <tr>
           
            <td >
                <asp:Button ID="btnSave" runat="server" Text="Save" style="margin-top:20px;margin-left:100px" />
            </td>
            <td>
                <asp:Button ID="btnReset" runat="server" Text="Reset" style="margin-top:20px" />
            </td>
        </tr>
    </table>

    
 <div align="center" style="margin-top:20px">
     <asp:GridView ID="gvDailyPayments" runat="server" BorderColor="#999999" BorderWidth="3px" OnRowDataBound="GridView_RowDataBound" BackColor="#CCCCCC" BorderStyle="Solid" CellPadding="4" CellSpacing="2" ForeColor="Black">
         <FooterStyle BackColor="#CCCCCC" />
         <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
         <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
         <RowStyle BackColor="White" />
         <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
         <SortedAscendingCellStyle BackColor="#F1F1F1" />
         <SortedAscendingHeaderStyle BackColor="#808080" />
         <SortedDescendingCellStyle BackColor="#CAC9C9" />
         <SortedDescendingHeaderStyle BackColor="#383838" />
     </asp:GridView>
 </div>
</asp:Content>
