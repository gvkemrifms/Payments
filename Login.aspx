<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DailyCollectionAndPayments.Login" %>

<!DOCTYPE html>
<!--[if IE 9]>         <html class="no-js lt-ie10"> <![endif]-->
<!--[if gt IE 9]><!-->
<html class="no-js">
<!--<![endif]-->
<head>
    <meta charset="utf-8">

    <title>:::GVK EMRI:::</title>

    <meta name="description" content="AppUI is a Web App Bootstrap Admin Template created by pixelcave and published on Themeforest">
    <meta name="author" content="pixelcave">
    <meta name="robots" content="noindex, nofollow">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css">
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css">
    <meta name="viewport" content="width=device-width,initial-scale=1,maximum-scale=1.0">

    <!-- Icons -->
    <!-- The following icons can be replaced with your own, they are used by desktop and mobile browsers -->
    <link rel="shortcut icon" href="img/favicon.png">
    <link rel="apple-touch-icon" href="img/icon57.png" sizes="57x57">
    <link rel="apple-touch-icon" href="img/icon72.png" sizes="72x72">
    <link rel="apple-touch-icon" href="img/icon76.png" sizes="76x76">
    <link rel="apple-touch-icon" href="img/icon114.png" sizes="114x114">
    <link rel="apple-touch-icon" href="img/icon120.png" sizes="120x120">
    <link rel="apple-touch-icon" href="img/icon144.png" sizes="144x144">
    <link rel="apple-touch-icon" href="img/icon152.png" sizes="152x152">
    <link rel="apple-touch-icon" href="img/icon180.png" sizes="180x180">
    <!-- END Icons -->

    <!-- Stylesheets -->
    <!-- Bootstrap is included in its original form, unaltered -->
    <link href="css/bootstrap.min.css" rel="stylesheet" />

    <!-- Related styles of various icon packs and plugins -->
    <link href="css/plugins.css" rel="stylesheet" />

    <!-- The main stylesheet of this template. All Bootstrap overwrites are defined in here -->
    <link href="css/main.css" rel="stylesheet" />

    <!-- Include a specific file here from css/themes/ folder to alter the default theme of the template -->

    <!-- The themes stylesheet of this template (for using specific theme color in individual elements - must included last) -->
    <link href="css/themes.css" rel="stylesheet" />
    <!-- END Stylesheets -->

    <!-- Modernizr (browser feature detection library) -->
    <script src="js/vendor/modernizr-2.8.3.min.js"></script>
</head>
<body>
    <!-- Login Container -->
    <div id="login-container">
        <!-- Login Header -->
        <h1 class="h2 text-light text-center push-top-bottom animation-slideDown">
            <i class="fa fa-ambulance"></i>&nbsp<strong>Welcome to GVK EMRI</strong>
        </h1>
        <!-- END Login Header -->

        <!-- Login Block -->
        <div class="block animation-fadeInQuickInv">
            <!-- Login Title -->
            <div class="block-title">
                <div class="block-options pull-right">
                    <a href="#" class="btn btn-effect-ripple btn-primary" data-toggle="tooltip" data-placement="left" title="Forgot your password?"><i class="fa fa-exclamation-circle"></i></a>
                    <a href="#" class="btn btn-effect-ripple btn-primary" data-toggle="tooltip" data-placement="left" title="Create new account"><i class="fa fa-plus"></i></a>
                </div>
                <h2>Please Login</h2>
            </div>
            <!-- END Login Title -->

            <!-- Login Form -->
            <form id="formlogin" runat="server" class="form-horizontal">
                <div class="form-group">
                    <div class="col-xs-12">
                        <asp:TextBox ID="txtloginemail" runat="server" placeholder="Your email.." class="form-control"></asp:TextBox>
                        <%--<input type="text" id="login-email" name="login-email" class="form-control" placeholder="Your email..">--%>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-xs-12">
                        <asp:TextBox ID="txtPassword" runat="server" placeholder="Your Password.." class="form-control" TextMode="Password"></asp:TextBox>
                    </div>
                </div>
                <%--<div class="form-group">
                    <div class="col-xs-12" id="divconvox_agentID" runat="server" style="display: none;">
                        <asp:TextBox ID="txtStationId" runat="server" placeholder="Station ID.." class="form-control" ></asp:TextBox>
                    </div>
                </div>   --%>             
                <div class="form-group form-actions">
                    <div class="col-xs-8">    
                        <asp:Label ID="lblError" runat="server" class="csscheckbox csscheckbox-primary"></asp:Label>                    
                    </div>
                    <div class="col-xs-4 text-right">
                        <asp:Button runat="server" CssClass="btn btn-effect-ripple btn-sm btn-primary" ID="btnsubmit" Text="Let's Go" OnClick="btnsubmit_Click" />
                    </div>
                </div>
            </form>
            <!-- END Login Form -->
        </div>
        <!-- END Login Block -->

        <!-- Footer -->
        <footer class="text-muted text-center animation-pullUp">
            <small>&copy; <a href="#" target="_blank">GVK EMRI</a></small>
        </footer>
        <!-- END Footer -->
    </div>
    <!-- END Login Container -->

    <!-- jQuery, Bootstrap, jQuery plugins and Custom JS code -->
    <script src="scripts/jquery-3.3.1.min.js"></script>
    <script src="js/bootstrap.js"></script>
    <script src="js/plugins.js"></script>
    <script src="js/app.js"></script>

    <!-- Load and execute javascript code used only in this page -->
    <script src="js/pages/readyLogin.js"></script>
    <script>$(function () { ReadyLogin.init(); });</script>
</body>
</html>
