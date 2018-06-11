<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="shortcut icon" href="images/favicon.png">

    <title>
        RGPH - QUESTIONNAIRE : LOGIN
    </title>
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,400italic,700,800' rel='stylesheet' type='text/css'>
    <link href='http://fonts.googleapis.com/css?family=Raleway:300,200,100' rel='stylesheet' type='text/css'>

    <!-- Bootstrap core CSS -->
    <link href="CleanZone/js/bootstrap/dist/css/bootstrap.css" rel="stylesheet">

    <link rel="stylesheet" href="CleanZone/fonts/font-awesome-4/css/font-awesome.min.css">

    <!-- Custom styles for this template -->
    <link href="CleanZone/css/style.css" rel="stylesheet" />

</head>
<body class="texture">
    <%--<form id="form1" runat="server"></form>--%>
    <div>
        <Msg:msgBox ID="Dialogue" runat="server" />

    </div>

    <div id="cl-wrapper" class="login-container">

        <div class="middle-login">
            <div class="block-flat">
                <%--<div style="text-align: center;">
                    <asp:Image runat="server" ID="imagesss" Width="100px" ImageUrl="~/images/logo_rgph.png" />
                </div>--%>
                <div class="header">
                    <h3 class="text-center">
                        <%--<img class="logo-img" src="images/logo.png" alt="" />--%>RGPH - QUESTIONNAIRE</h3>
                </div>
                <div>
                    <form id="form1" runat="server" style="margin-bottom: 0px !important;" class="form-horizontal">
                        <div class="content">
                            <%--<h4 class="title">Login Access</h4>--%>
                            <asp:ValidationSummary runat="server" ID="VSummary1" ShowMessageBox="true" ShowSummary="true" />
                            <asp:Panel runat="server" ID="Panel_Msg" Visible="false">
                                <div class="alert alert-warning">
                                    <asp:Image ID="Image_Msg" runat="server" />
                                    <asp:Label ID="Label_Msg" runat="server" Text=""></asp:Label>
                                </div>
                            </asp:Panel>
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="fa fa-user"></i></span>
                                        <asp:TextBox runat="server" placeholder="Compte Utilisateur" ID="username" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="fa fa-lock"></i></span>
                                        <asp:TextBox TextMode="Password" runat="server" placeholder="Mot de passe" ID="password" class="form-control"></asp:TextBox>
                                    </div>
                                    <asp:CheckBox ID="CBX_GarderSession" Visible="true" Checked="true" runat="server" Text="Garder ma session sur cet ordinateur"
                                        Style="font-size: 12px;" CssClass="icheck" />
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="password" Text="*"
                                            SetFocusOnError="true" ErrorMessage="Mot de passe Obligatoire" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator runat="server" ID="RFV1" ControlToValidate="username" Text="*"
                                            SetFocusOnError="true" ErrorMessage="Compte Utilisateur Obligatoire" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                        </div>
                        <div class="foot">
                            <asp:LinkButton CssClass="btn btn-primary" runat="server" ID="btn_connection">
                                <i  class="fa fa-user"></i> Connexion
                            </asp:LinkButton>
                            <asp:LinkButton ID="LinkButton_CeNestPasMonCompte" CausesValidation="false" runat="server" CssClass="btn btn-info"> 
                                <i  class="fa fa-user"></i> Connecter avec un compte différent</asp:LinkButton>
                        </div>
                    </form>
                </div>
            </div>
            <div class="text-center out-links"><a href="#">&copy; 2018 IHSI Developper par Jean Fritz DUVERSEAU</a></div>
        </div>

    </div>


    <script src="CleanZone/js/jquery.js"></script>
    <script type="text/javascript" src="CleanZone/js/behaviour/general.js"></script>

    <!-- Bootstrap core JavaScript
================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="CleanZone/js/behaviour/voice-commands.js"></script>
    <script src="CleanZone/js/bootstrap/dist/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="CleanZone/js/jquery.flot/jquery.flot.js"></script>
    <script type="text/javascript" src="CleanZone/js/jquery.flot/jquery.flot.pie.js"></script>
    <script type="text/javascript" src="CleanZone/js/jquery.flot/jquery.flot.resize.js"></script>
    <script type="text/javascript" src="CleanZone/js/jquery.flot/jquery.flot.labels.js"></script>
</body>
</html>
