<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/DCP.Master" CodeBehind="ChangePassword.aspx.cs" Inherits="DailyCollectionAndPayments.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
       function Validations() {
            var currentPassword = $('#<%=txtcurrentPassword.ClientID %>').val();
            var newPassword = $('#<%= txtnewPassword.ClientID %>').val();
            var confirmNewPassword = $('#<%= txtConfirmNewPassword.ClientID %>').val();
            if (currentPassword === "") {
                return alert('Please enter your current Password');
            }
            if (newPassword === "") {
                return alert("Please enter New Password");
            }
           if (newPassword.length<6) {
               return alert("Password Length should be atlest 6 charecters");
           }
            if (confirmNewPassword === "") {
                return alert("Please confirm your New Password ");
            }
           if (newPassword !== confirmNewPassword)
               return alert("Mismatch between New Password and Confirm Password");
           return true;
       }
    </script>
    <table align="center" style="margin-top: 10px">
        <tr>
            <td>
                <h4 style="color:brown">Change Password Screen</h4>
            </td>
        </tr>
    </table>
    <br/>
    <table align="center" style="margin-top:5px">
        
        <tr>
            <td>Current Password <span style="color:red">*</span> </td> 
                <td>
                    <asp:TextBox ID="txtcurrentPassword" runat="server" Text="" ></asp:TextBox>
                </td>
                  </tr>
        <tr>
            <br />

            <td style="margin-top:20px">New Password <span style="color:red">*</span>  </td>
            <td style="margin-top:20px">
                <asp:TextBox ID="txtnewPassword" type="password" runat="server" Text=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <br />

            <td style="margin-top:20px">Confim New Password <span style="color:red">*</span>  </td>
            <td style="margin-right:10px">
                <asp:TextBox ID="txtConfirmNewPassword" type="password" runat="server" Text=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSubmit" CssClass="form-submit-button" runat="server" OnClientClick="if(!Validations()) return false;" Text="Update" OnClick="btnSubmit_Click" />
            </td>
            <td>
                <asp:Button ID="btnReset" runat="server"  CssClass="form-reset-button" Text="Cancel" OnClick="btnReset_OnClick" />
            </td>
        </tr>
    </table>
    </asp:Content>