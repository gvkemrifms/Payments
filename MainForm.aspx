<%@ Page Title="" Language="C#" MasterPageFile="~/DCP.Master" AutoEventWireup="true" CodeBehind="MainForm.aspx.cs" Inherits="DailyCollectionAndPayments.MainForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
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
            margin-left: 20%;
        }

        #MainContent_menuBar ul {
            list-style: none;
            margin: 0;
            padding: 0;
            width: auto;
            margin-left: 20%;
            height: 30px;
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
    </style>
    <div class="navbar navbar-inverse set-radius-zero" style="height: 65px;">
        <div class="navbar-header" style="width:100%;">

            <table style="">
                <tr>
                    <td>
                        <asp:Image ID="gvkemriimg" runat="server" Style="margin-top: -1px; margin-left: -1px;" ImageUrl="~/img/gvk-emri.jpg" />
                    </td>
                    <td style="width: 40%; color: white; font-weight: 600;">
                        <h3>Daily Collections & Payments</h3>
                    </td>
                    <td style="width: 20%; align-content: flex-end; text-align: center;">
                        <h5 style="color: white;">Welcome&nbsp;&nbsp;<asp:Label ID="LblUserName" Font-Bold="true" ForeColor="YellowGreen" runat="server" Text=""></asp:Label>
                        </h5>
                    </td>
                    <td style="text-align: right; width: 5%">
                        <asp:Button ID="btnLogout" runat="server" CssClass="btn btn-default"
                            OnClick="btnLogout_Click" Text="Sign Out" Style="border-radius: 8px;" />
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <div class="MenuBar">
        <asp:Menu ID="menuBar" runat="server" Orientation="Horizontal" Width="100%" Style="color: white" OnMenuItemClick="menuBar_MenuItemClick"
            StaticSelectedStyle-CssClass="active"
            DynamicMenuStyle-CssClass="dropdown-menu">
            <LevelMenuItemStyles>
                <asp:MenuItemStyle CssClass="main_menu" />
                <asp:MenuItemStyle CssClass="level_menu" />
            </LevelMenuItemStyles>
            <StaticSelectedStyle CssClass="selected" />
        </asp:Menu>
    </div>
    <div class="" style="height: 550px; width: 100%;">
        <iframe id="uriIFrame" runat="server" style="height: 100%; width: 100%; border: 0;"></iframe>
    </div>
</asp:Content>
