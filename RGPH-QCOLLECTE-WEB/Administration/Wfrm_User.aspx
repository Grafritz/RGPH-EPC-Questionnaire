<%@ Page Title=".:: Sécurité - User ::." Language="VB" MasterPageFile="~/MasterPages/MasterPage_Security.master"
    AutoEventWireup="false" CodeFile="Wfrm_User.aspx.vb" Inherits="Parametres_Administration_Wfrm_User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function ShowAddUpdateForm(strPage, tmpW, tmpH) {
                var oWindow = window.radopen(strPage, "AddUpdateDialog");
                //oWindow.set_autoSize(true);
                oWindow.SetSize(tmpW, tmpH);
                document.getElementById("txtWindowPage").value = strPage;
                if (oWindow) {
                    if (!oWindow.isClosed()) {
                        oWindow.center();
                        var bounds = oWindow.getWindowBounds();
                        oWindow.moveTo(bounds.x + 'px', "50px");
                    }
                }
                return false;
            }

            function ShowAddUpdateFormMaximized(strPage, tmpW, tmpH) {
                var oWindow = window.radopen(strPage, "AddUpdateDialog");
                oWindow.SetSize(tmpW, tmpH);
                document.getElementById("txtWindowPage").value = strPage;
                if (oWindow) {
                    if (!oWindow.isClosed()) {
                        oWindow.center();
                        var bounds = oWindow.getWindowBounds();
                        oWindow.moveTo(bounds.x + 'px', "50px");
                    }
                }
                oWindow.maximize();
                return false;
            } // 

            function ShowAddUpdateFormAutoSize(strPage, tmpW, tmpH) {
                var oWindow = window.radopen(strPage, "AddUpdateDialog");
                oWindow.set_autoSize(true);
                document.getElementById("txtWindowPage").value = strPage;
                if (oWindow) {
                    if (!oWindow.isClosed()) {
                        oWindow.center();
                        var bounds = oWindow.getWindowBounds();
                        oWindow.moveTo(bounds.x + 'px', "50px");
                    }
                }
                return false;
            }
            function RadWindowClosing() {
                //$find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Reload");
            }

            function RadWindowClientResizeEnd() {
                var manager = GetRadWindowManager();
                var window1 = manager.getActiveWindow();
                window1.center();
                var bounds = window1.getWindowBounds();
                window1.moveTo(bounds.x + 'px', "50px");
            }

            function refreshMe() {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Reload");
            }
            function refreshPlan() {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("ReloadPlan");
            }

        </script>
    </telerik:RadCodeBlock>

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel_Produits" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="Panel_PlanBox" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGrid_Plan">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel_Msg" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid_Plan" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rdgSR_PRIVILEGE_SERVICES">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel_Msg" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="rdgSR_PRIVILEGE_SERVICES" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <%--<telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid_1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            <%--<telerik:AjaxSetting AjaxControlID="CBX_IsInAnnonce">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DDL_Utilisateur" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="DDL_Utilisateur">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid_1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" />
    <input type="hidden" id="radGridClickedRowIndex" name="radGridClickedRowIndex" />
    <input id="Hidden1" type="hidden" />
    <section class="content-header">
        <h1>
            <i class="fa fa-users"></i>
            <asp:Label ID="Label_Titre" runat="server" Text="UTILISATEURS " />
            <small>
                <asp:Label ID="LabelNumeroDeReference" runat="server" /></small>
            <asp:LinkButton ID="LinkButton_AddNew" runat="server" CausesValidation="false"
                CssClass="btn btn-info"><i class="fa fa-plus-square"></i> Nouveau</asp:LinkButton>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Accueil</a></li>
            <li class="active">Utilisateur</li>
        </ol>
    </section>
    <section class="content" style="padding-top: 0px;">
        <div id="Div_CorpsFille">
            <!-- DEBUT FORMAT TEXT AREA -->
            <script type="text/javascript" src="../JavaScript/tiny_mce/tiny_mce.js"></script>
            <script language="javascript" type="text/javascript">
                // Tiny MCE Pour les textareas
                tinyMCE.init({
                    mode: "textareas",
                    theme: "advanced",
                    //theme_advanced_buttons1: "BtnNameDest,|,bold,italic,underline,separator,strikethrough,|,forecolor,backcolor,|,justifyleft,justifycenter,justifyright, justifyfull,|,bullist,numlist,|,undo,redo,|,link,unlink",
                    theme_advanced_buttons1: "BtnNameDest,|,bold,italic,underline,strikethrough,|,forecolor,backcolor,|,justifyleft,justifycenter,justifyright,justifyfull,formatselect,fontselect,fontsizeselect|,bullist,numlist,undo,redo,link,unlink,||,emotions,iespell,media,advhr",
                    theme_advanced_buttons2: "",
                    theme_advanced_buttons3: "",
                    theme_advanced_toolbar_location: "top",
                    theme_advanced_toolbar_align: "left",
                    theme_advanced_statusbar_location: "bottom",
                    plugins: 'inlinepopups',
                    setup: function (ed) {
                        // Add a custom button
                        ed.addButton('BtnNameDest', {
                            title: 'Ajouter le Nom du destinataire',
                            image: '../Images/user_.png',
                            onclick: function () {
                                // Add you own code to execute something on click
                                ed.focus();
                                ed.selection.setContent('_NOMCOMPLET');
                            }
                        });
                    }
                });
            </script>
            <%--<ajaxToolkit:ToolkitScriptManager runat="Server" AsyncPostBackTimeout="600" EnablePartialRendering="true"
            EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />--%>
            <%--<asp:UpdatePanel ID="UpdatePanel_First" runat="server">
            <ContentTemplate>
                    <div style="display: inline; float: left; padding-right: 10px;">
                        <asp:UpdateProgress ID="updProgHeader" runat="server" AssociatedUpdatePanelID="UpdatePanel_First">
                            <ProgressTemplate>
                                <div>
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/spinner.gif" AlternateText="Waiting..." />
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>--%>
            <Msg:msgBox ID="Dialogue" runat="server" />
            <asp:Panel runat="server" ID="Panel_Msg" Visible="false">
                <div id="DIV_Msg" runat="server" class="alert alert-warning alert-dismissable">
                    <i id="Icon_Msg" runat="server" class="fa fa-warning"></i>
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                    <asp:Image ID="Image_Msg" runat="server" />
                    <asp:Label ID="Label_Msg" runat="server" Text=""></asp:Label>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="Panel_First2">
                <br style="clear: both;" />
                <div id="Div_First">
                    <asp:Panel ID="Panel_First" runat="server">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                            ShowSummary="true" CssClass="alert alert-warning" />
                        <div runat="server" id="TABLEAU_ADD">
                            <div class="panel panel-default panel-body">
                                <div class="box-body">
                                    <div class="form-group">
                                        <div class="col-sm-1">
                                            <asp:Image ID="Image_participant" runat="server" ImageUrl="~/images/no-avatar.jpg"
                                                CssClass="img-circle" Width="90px" />
                                        </div>
                                        <div class="col-sm-6" runat="server" visible="true" id="TRtxtEmailCompte">
                                            <label>
                                                <asp:Label ID="Label5" runat="server" CssClass="ClassLabel" Text="Email/Compte" Style="font-weight: 700"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmailCompte"
                                                    Display="Dynamic" ErrorMessage="Email / Compte obligatoire !" SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="REVtxtEmail" runat="server" ControlToValidate="txtEmailCompte"
                                                    ErrorMessage="Email incorrete - le format doit etre: monemail@nomdomaine.ext"
                                                    SetFocusOnError="True" Display="Dynamic" Text="*" ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$" />
                                            </label>
                                            <telerik:RadTextBox ID="txtEmailCompte" placeholder="Compte utilisateur/Email" runat="server" Width="100%" CssClass="form-control">
                                            </telerik:RadTextBox>
                                            <div style="clear: both;"></div>
                                        </div>
                                        <div class="col-sm-5" runat="server" visible="true" id="TR_MotdePasse">
                                            <label>
                                                Mot de passe</label>
                                            <telerik:RadTextBox ID="txt_MotdePasse" runat="server" placeholder="Mot de Passe" TextMode="Password" CssClass="form-control"
                                                Width="90%">
                                            </telerik:RadTextBox>
                                            <asp:LinkButton ID="Btn_Reset_MotdePasse2" runat="server" CssClass="btn btn-sm btn-warning">
                                             <i class="fa fa-refresh" ></i> </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-3">
                                            <label>
                                                <asp:Label ID="Label1" runat="server" CssClass="ClassLabel" Text="Prénom"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RFVtxtPrenom" runat="server" ControlToValidate="txtPrenom"
                                                    ErrorMessage="Prénom obligatoire !" Display="Dynamic" SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                                    TargetControlID="RFVtxtPrenom" PopupPosition="TopLeft" />
                                            </label>
                                            <telerik:RadTextBox ID="txt_ID" runat="server" Text="0" Visible="false" Width="0px">
                                            </telerik:RadTextBox>
                                            <telerik:RadTextBox ID="txtPrenom" runat="server" placeholder="Prénom" Width="100%" CssClass="form-control">
                                            </telerik:RadTextBox>
                                        </div>
                                        <div class="col-sm-3">
                                            <label>
                                                Nom
                                                    <asp:RequiredFieldValidator ID="RFVtxtNom" runat="server" ControlToValidate="txtNom"
                                                        Display="Dynamic" ErrorMessage="Nom obligatoire !" SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                                    TargetControlID="RFVtxtNom" PopupPosition="TopLeft" />
                                            </label>
                                            <telerik:RadTextBox ID="txtNom" runat="server" placeholder="Nom" Width="100%" CssClass="form-control">
                                            </telerik:RadTextBox>
                                        </div>
                                        <div class="col-sm-3" runat="server" visible="true" id="TR_MotdePasse2">
                                            <asp:CheckBox ID="CBXMustChangePassword" runat="server" CssClass="text-red" Text="Forcer à changer son mot de passe" /><br />
                                            <asp:CheckBox ID="CBXGenererLeMotDePasse" runat="server" CssClass="text-yellow" Text="Générer le mot de passe" />
                                            <div style="clear: both;"></div>
                                        </div>
                                    </div>
                                    <div style="clear: both;"></div>
                                    <div class="form-group">
                                        <div class="col-sm-1">
                                            <label>
                                                Photo
                                                    <asp:RequiredFieldValidator ID="RFVFileUpload_Photo" runat="server" ControlToValidate="FileUpload1"
                                                        Display="Dynamic" ErrorMessage="Téléchargez le logo à partir de votre ordinateur.&lt;br /&gt;-La taille ne doit pas dépassée &lt;b&gt;50Kb&lt;/b&gt;.&lt;br /&gt;&lt;a href='#' class='LinkSite' target='_blank'&gt;&lt;i&gt;Technique pour compresser une Image&lt;/i&gt;&lt;/a&gt;."
                                                        SetFocusOnError="True" Style="color: #FF0000" Text="*" ValidationGroup="GPSavePH" />
                                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender34" runat="server" Enabled="True"
                                                    HighlightCssClass="validatorCalloutHighlight" TargetControlID="RFVFileUpload_Photo"
                                                    PopupPosition="TopLeft" />
                                            </label>
                                            <asp:FileUpload ID="FileUpload1" Width="100%" runat="server" CssClass="form-control" />
                                            <asp:LinkButton ID="Btn_ChargerPhoto" Visible="false" runat="server" CssClass="btn btn-sm btn-success">
                                                    <i class="fa fa-camera-retro" ></i> </asp:LinkButton>
                                            <div style="clear: both;"></div>
                                        </div>
                                        <div class="col-sm-3">
                                            <label>
                                                <asp:Label ID="Label4" runat="server" CssClass="ClassLabel" Text="Téléphone"></asp:Label>
                                                <%--<asp:RequiredFieldValidator ID="RFVtxtTelephone" runat="server" ControlToValidate="txtTelephone"
                                                    ErrorMessage="Téléphone obligatoire !" Display="Dynamic" SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                                    TargetControlID="RFVtxtTelephone" PopupPosition="TopLeft" />--%>
                                            </label>
                                            <telerik:RadTextBox ID="txtTelephone" runat="server" placeholder="Téléphone" Width="100%" CssClass="form-control">
                                            </telerik:RadTextBox>
                                        </div>
                                        <div class="col-sm-3">
                                            <label>Sexe :</label>
                                            <asp:DropDownList ID="DDL_Sexe" runat="server" Width="100%" CssClass="form-control">
                                                <asp:ListItem Text="Masculin" Value="M" />
                                                <asp:ListItem Text="Féminin" Value="F" />
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4">
                                            <label>Poste</label>
                                            <telerik:RadComboBox ID="RCBPoste" runat="server" CssClass="form-control" EmptyMessage="Sélectionnez ou tapez le poste ici"
                                                EnableLoadOnDemand="True" EnableVirtualScrolling="True" ShowMoreResultsBox="True"
                                                Sort="Ascending" Skin="MetroTouch" ToolTip="Sélectionnez ou tapez le poste" Width="100%">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-4">
                                            <label>
                                                Pays
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1DDL_Pays" runat="server" ControlToValidate="DDL_Pays"
                                    ErrorMessage="Pays obligatoire" InitialValue="0" SetFocusOnError="true" Text="*"
                                    ValidationGroup="GPSave" />
                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender10"
                                                    TargetControlID="RequiredFieldValidator1DDL_Pays" HighlightCssClass="validatorCalloutHighlight" />
                                            </label>
                                            <asp:DropDownList ID="DDL_Pays" runat="server" CssClass="select2 form-control" Width="100%"
                                                AutoPostBack="true">
                                                <asp:ListItem Value="0" Text="- Selectionner votre pays -" />
                                                <asp:ListItem Value="1" Text="HAITI" />
                                                <asp:ListItem Value="2" Text="Autres" />
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4" runat="server" id="TR_Departement" visible="false">
                                            <label>
                                                Département
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDDL_Departement6" runat="server"
                                    ControlToValidate="DDL_Departement" ErrorMessage="Département obligatoire" InitialValue="0"
                                    SetFocusOnError="true" Text="*" ValidationGroup="GPSave"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender11"
                                                    TargetControlID="RequiredFieldValidatorDDL_Departement6" HighlightCssClass="validatorCalloutHighlight" />
                                            </label>
                                            <asp:DropDownList ID="DDL_Departement" AutoPostBack="true" runat="server" CssClass="select2 form-control"
                                                Width="100%">
                                                <asp:ListItem Value="0">- Département - </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4" id="TR_Commune" runat="server" visible="false">
                                            <label>
                                                Ville/Commune
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDDL_Commune3" runat="server"
                                    ControlToValidate="DDL_Commune" ErrorMessage="Commune obligatoire" InitialValue="0"
                                    SetFocusOnError="true" Text="*" ValidationGroup="GPSave"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender12"
                                                    TargetControlID="RequiredFieldValidatorDDL_Commune3" HighlightCssClass="validatorCalloutHighlight" />
                                            </label>
                                            <asp:DropDownList ID="DDL_Commune" runat="server" CssClass="select2 form-control" Width="100%">
                                                <asp:ListItem Value="0">- Ville / Commune - </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-12">
                                            <label>Adresse</label>
                                            <asp:TextBox ID="txt_Adresse" runat="server" Width="100%" CssClass="form-control" />
                                            <div style="clear: both;"></div>
                                        </div>
                                        <div class="col-sm-4">
                                            <label>
                                                Etablissement:
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddl_Ecole"
                                                    ErrorMessage="Pays obligatoire" InitialValue="0" SetFocusOnError="true" Text="*"
                                                    ValidationGroup="GPSave" />
                                            </label>
                                            <asp:DropDownList ID="ddl_Ecole" runat="server" CssClass="select2 form-control" Width="100%">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4" runat="server" visible="true" id="TRGroupeUtilisateur">
                                            <label>
                                                <asp:Label ID="Label14" runat="server" CssClass="ClassLabel" Text="Groupe"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DDLGroupeUtilisateur"
                                                    InitialValue="0" Display="Dynamic" ErrorMessage="Groupe obligatoire" Text="*" />
                                                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator3" runat="server"
                                                    ControlToValidate="DDLGroupeUtilisateur" ErrorMessage="Groupe obligatoire" Text="*" />
                                            </label>
                                            <telerik:RadComboBox ID="DDLGroupeUtilisateur" Skin="MetroTouch" runat="server" Width="100%">
                                                <Items>
                                                    <telerik:RadComboBoxItem runat="server" Text="Secretaire de réunion" Value="2" />
                                                    <telerik:RadComboBoxItem runat="server" Selected="True" Text="Participant" Value="3" />
                                                    <telerik:RadComboBoxItem runat="server" Text="President de réunion" Value="4" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:CheckBox ID="CBXActifyn" runat="server" Checked="true" Visible="true" Text="Actif Y/N" />
                                            <div style="clear: both;"></div>
                                        </div>
                                        <div style="clear: both;"></div>
                                        <div class="col-sm-9">
                                            <asp:CheckBox ID="CBX_SendAccountAndPassword" runat="server" CssClass="text-green" Text=" Envoyer par email les informations du compte à l'utilisateur et une copie a moi." />
                                        </div>
                                        <div class="col-sm-12">
                                            <label>IdFirebaseToken:</label>
                                            <asp:Label ID="labelIdFirebaseToken" runat="server" Width="100%" CssClass="form-control;" />
                                            <div style="clear: both;"></div>
                                        </div>
                                    </div>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="box-footer" style="text-align: center;">
                                    <br />
                                    &nbsp;
                                        <asp:LinkButton ID="Btn_Save" runat="server" CssClass="btn btn-primary">
                                            <i class="fa fa-save" ></i> Enregistrer
                                        </asp:LinkButton>
                                    &nbsp;
                                        <asp:LinkButton ID="Btn_Reset_MotdePasse" runat="server" CssClass="btn btn-warning">
                                             <i class="fa fa-refresh" ></i> Reset Mot de Passe
                                        </asp:LinkButton>
                                    &nbsp;
                                        &nbsp;
                                        <asp:LinkButton ID="Btn_Annuler" runat="server" CssClass="btn btn-danger" CausesValidation="false">
                                            <i class="fa fa-reply-all" ></i> Annuler
                                        </asp:LinkButton>
                                    &nbsp;
                                </div>
                                <!-- /.box-footer-->
                            </div>
                            <div style="clear: both;"></div>
                            <hr />
                            <!-- Références -->
                            <%--<asp:Panel class="box box-warning" runat="server" ID="Panel_PlanBox">
                                <asp:Panel class="box-header" runat="server" ID="PanelHeader_Plan">
                                    <h3 class="box-title">
                                        <i class="fa fa-briefcase"></i>
                                        <asp:Label ID="LabelTitrePlan" runat="server" Text="Plans" />
                                    </h3>
                                    <div class="box-tools pull-right">
                                        <asp:Label ID="LabelHeader_Plan" runat="server" Text="(Cliquer pour afficher form...)"></asp:Label>&nbsp;<asp:ImageButton
                                            ID="ImageButton_Plan" runat="server" ImageUrl="~/images/expand.jpg" AlternateText="(Cliquer pour afficher form...)" />
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="PanelBody_Plan" runat="server" CssClass="box-body">
                                    <div id="DIV_ListePlan" runat="server">
                                        <asp:LinkButton ID="rbtnAddPlan" runat="server" CssClass="btn btn-sm btn-warning" Style="margin-bottom: 5px;">
                                            <i class="fa fa-plus-square-o"></i> Ajouter un Plan </asp:LinkButton>
                                        <telerik:RadGrid ID="RadGrid_Plan" AllowPaging="True" AllowSorting="True" PageSize="20"
                                            runat="server" AutoGenerateColumns="False" GridLines="None" AllowFilteringByColumn="true"
                                            EnableViewState="true" AllowMultiRowSelection="false" GroupingSettings-CaseSensitive="false">
                                            <ExportSettings HideStructureColumns="true" />
                                            <MasterTableView CommandItemDisplay="None" GridLines="None" DataKeyNames="ID" NoDetailRecordsText="Pas d'enregistrement"
                                                NoMasterRecordsText="Pas d'enregistrement">
                                                <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowExportToExcelButton="true"
                                                    ExportToExcelText="Exporter en excel" />
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="ID" UniqueName="ID" Display="false">
                                                        <ItemStyle Width="1px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn HeaderText="#" UniqueName="Compteur" AllowFiltering="false">
                                                        <ItemTemplate>
                                                            <asp:Label Visible="true" ID="lbOrder" runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="16px" />
                                                        <ItemStyle HorizontalAlign="Center" Width="16px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="DateCreated" UniqueName="DateAjout" HeaderText=" Date Ajout"
                                                        FilterControlAltText="Filter DateAjout column" FilterControlWidth="95%" ShowFilterIcon="false"
                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:dd/MM/yyyy}">
                                                        <HeaderStyle HorizontalAlign="Center" Width="16px" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Center" Width="16px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="DateEffectif" UniqueName="DateEffectif" HeaderText=" Date Effectif"
                                                        FilterControlAltText="Filter DateAjout column" FilterControlWidth="95%" ShowFilterIcon="false"
                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:dd/MM/yyyy}">
                                                        <HeaderStyle HorizontalAlign="Center" Width="16px" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Center" Width="16px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="DateLimite" UniqueName="DateLimite" HeaderText=" Date Limite"
                                                        FilterControlAltText="Filter DateLimite column" FilterControlWidth="95%" ShowFilterIcon="false"
                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:dd/MM/yyyy}">
                                                        <HeaderStyle HorizontalAlign="Center" Width="16px" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Center" Width="16px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="NomPlan" UniqueName="NomPlan" HeaderText=" Plan"
                                                        FilterControlAltText="Filter Plan column" FilterControlWidth="95%" ShowFilterIcon="false"
                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Actif(Y/N)" AllowFiltering="false" SortExpression="ActifYN">
                                                        <ItemTemplate>
                                                            <asp:Image ID="LabelSTATUT_IMAGE" runat="server" ImageUrl='<%# Bind("STATUT_IMAGE")%>' Style="" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="16px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandArgument="ID" CommandName="editer" UniqueName="editer"
                                                        DataTextField="ID" ImageUrl="~/images/_edit.png"
                                                        HeaderText="">
                                                        <HeaderStyle HorizontalAlign="Center" Width="16px" />
                                                        <ItemStyle HorizontalAlign="Center" Width="16px" />
                                                    </telerik:GridButtonColumn>
                                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="delete" DataTextField="ID"
                                                        ImageUrl="~/images/delete.png"
                                                        UniqueName="delete" HeaderText="" ConfirmDialogType="RadWindow" ConfirmText="Voulez-vous vraiment supprimer ce Plan?"
                                                        ConfirmTitle="Attention!">
                                                        <HeaderStyle HorizontalAlign="Center" Width="16px" />
                                                        <ItemStyle HorizontalAlign="Center" Width="16px" />
                                                    </telerik:GridButtonColumn>
                                                </Columns>
                                                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                                                </ExpandCollapseColumn>
                                            </MasterTableView>
                                            <ClientSettings>
                                                <ClientEvents></ClientEvents>
                                                <Selecting AllowRowSelect="true" />
                                            </ClientSettings>
                                            <HeaderContextMenu />
                                            <FilterMenu EnableImageSprites="False">
                                            </FilterMenu>
                                        </telerik:RadGrid>
                                    </div>
                                </asp:Panel>
                            </asp:Panel>--%>

                            <%--<asp:Panel class="box box-success" runat="server" ID="Panel_Produits">
                                <asp:Panel class="box-header" runat="server" ID="PanelHeader_Reference">
                                    <h3 class="box-title">
                                        <i class="fa fa-briefcase"></i>
                                        <asp:Label ID="LabelTitreProduit" runat="server" Text="Produits / Services par Utilisateur  " />
                                    </h3>
                                    <div class="box-tools pull-right">
                                        <asp:Label ID="LabelHeader_Reference" runat="server" Text="(Cliquer pour afficher form...)"></asp:Label>&nbsp;<asp:ImageButton
                                            ID="ImageButton_Reference" runat="server" ImageUrl="~/images/expand.jpg" AlternateText="(Cliquer pour afficher form...)" />
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="PanelBody_Reference" runat="server" CssClass="box-body">
                                    <div id="DIV_LISTEProduitsServices" runat="server">
                                        <asp:LinkButton ID="rbtnAddSR_PRIVILEGE_SERVICES" runat="server" CssClass="btn btn-sm btn-success" Style="margin-bottom: 5px;">
                                        <i class="fa fa-plus-square-o"></i> Ajouter un Privilège </asp:LinkButton>
                                        <telerik:RadGrid ID="rdgSR_PRIVILEGE_SERVICES" AllowPaging="True" AllowSorting="True" PageSize="20"
                                            runat="server" AutoGenerateColumns="False" GridLines="None" AllowFilteringByColumn="true"
                                            EnableViewState="true" AllowMultiRowSelection="false" GroupingSettings-CaseSensitive="false">
                                            <ExportSettings HideStructureColumns="true" />
                                            <MasterTableView CommandItemDisplay="None" GridLines="None" DataKeyNames="ID" NoDetailRecordsText="Pas d'enregistrement"
                                                NoMasterRecordsText="Pas d'enregistrement">
                                                <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowExportToExcelButton="true"
                                                    ExportToExcelText="Exporter en excel" />
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="ID" UniqueName="ID" Display="false">
                                                        <ItemStyle Width="1px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn HeaderText="#" UniqueName="Compteur" AllowFiltering="false">
                                                        <ItemTemplate>
                                                            <asp:Label Visible="true" ID="lbOrder" runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="16px" />
                                                        <ItemStyle HorizontalAlign="Center" Width="16px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="DateAjout" UniqueName="DateAjout" HeaderText=" Date Ajout"
                                                        FilterControlAltText="Filter DateAjout column" FilterControlWidth="95%" ShowFilterIcon="false"
                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:dd/MM/yyyy}">
                                                        <HeaderStyle HorizontalAlign="Center" Width="16px" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Center" Width="16px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="DateLimite" UniqueName="DateLimite" HeaderText=" Date Limite"
                                                        FilterControlAltText="Filter DateLimite column" FilterControlWidth="95%" ShowFilterIcon="false"
                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:dd/MM/yyyy}">
                                                        <HeaderStyle HorizontalAlign="Center" Width="16px" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Center" Width="16px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="DescriptionService" UniqueName="NomService" HeaderText=" Produits/Services"
                                                        FilterControlAltText="Filter Produits/Services column" FilterControlWidth="95%" ShowFilterIcon="false"
                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn DataField="Nomcomplet_et_Username" UniqueName="ID_User" HeaderText=" Utilisateur"
                                                        FilterControlAltText="Filter Prénom, Nom, Compte Utilisateur column" FilterControlWidth="95%" ShowFilterIcon="false"
                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabelNomcomplet_et_Username" runat="server" Text='<%# Bind("Nomcomplet_et_Username") %>' />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Actif(Y/N)" AllowFiltering="false" SortExpression="ActifYN">
                                                        <ItemTemplate>
                                                            <asp:Image ID="LabelSTATUT_IMAGE" runat="server" ImageUrl='<%# Bind("STATUT_IMAGE")%>' Style="" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="16px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandArgument="ID" CommandName="editer" UniqueName="editer"
                                                        DataTextField="ID" ImageUrl="~/images/_edit.png"
                                                        HeaderText="">
                                                        <HeaderStyle HorizontalAlign="Center" Width="16px" />
                                                        <ItemStyle HorizontalAlign="Center" Width="16px" />
                                                    </telerik:GridButtonColumn>
                                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="delete" DataTextField="ID"
                                                        ImageUrl="~/images/delete.png"
                                                        UniqueName="delete" HeaderText="" ConfirmDialogType="RadWindow" ConfirmText="Voulez-vous vraiment supprimer ce Produit et SERVICES?"
                                                        ConfirmTitle="Attention!">
                                                        <HeaderStyle HorizontalAlign="Center" Width="16px" />
                                                        <ItemStyle HorizontalAlign="Center" Width="16px" />
                                                    </telerik:GridButtonColumn>
                                                </Columns>
                                                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                                                </RowIndicatorColumn>
                                                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                                                </ExpandCollapseColumn>
                                            </MasterTableView>
                                            <ClientSettings>
                                                <ClientEvents></ClientEvents>
                                                <Selecting AllowRowSelect="true" />
                                            </ClientSettings>
                                            <HeaderContextMenu />
                                            <FilterMenu EnableImageSprites="False">
                                            </FilterMenu>
                                        </telerik:RadGrid>
                                        <telerik:RadWindowManager ID="RadWindowManager1" IconUrl="~/images/favicon.ico" runat="server" VisibleStatusbar="false"
                                            EnableViewState="false">
                                            <Windows>
                                                <telerik:RadWindow ID="AddUpdateDialog" runat="server" IconUrl="~/images/favicon.ico" Title="" Left="75px" ReloadOnShow="true"
                                                    ShowContentDuringLoad="false" Modal="true" OnClientClose="RadWindowClosing" Behaviors="Reload, Pin, Minimize, Move, Resize, Maximize, Close"
                                                    EnableShadow="true" OnClientResizeEnd="RadWindowClientResizeEnd" />
                                            </Windows>
                                        </telerik:RadWindowManager>
                                    </div>
                                </asp:Panel>
                            </asp:Panel>--%>
                            <!-- End box box-danger -->
                            <div style="clear: both;"></div>
                        </div>
                    </asp:Panel>
                </div>

                <div id="Div_List" style="overflow: auto; padding-bottom: 100px;">
                    <asp:Panel ID="Panel_List" runat="server">
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <div style="float: right; width: 20%; display: none;">
                                        <asp:Panel ID="Panel_Search" runat="server">
                                            <div class="input-group input-group-sm">
                                                <asp:TextBox ID="txt_Search" runat="server" CssClass="form-control" placeholder="Recherche..." />
                                                <span class="input-group-btn">
                                                    <asp:LinkButton ID="Btn_Search" runat="server" CausesValidation="false" CssClass="btn btn-info btn-flat">
                                                        <i class="fa fa-search"></i> </asp:LinkButton>
                                                </span>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="RadGrid_1" AllowPaging="True" AllowSorting="True" PageSize="20"
                                        runat="server" AutoGenerateColumns="False" GridLines="None" CellSpacing="0" AllowFilteringByColumn="true"
                                        EnableViewState="true" AllowMultiRowSelection="false"
                                        GroupingSettings-CaseSensitive="false">
                                        <MasterTableView GridLines="None" CommandItemDisplay="None" DataKeyNames="ID" NoDetailRecordsText="Pas d'enregistrement"
                                            NoMasterRecordsText="Pas d'enregistrement">
                                            <CommandItemSettings ShowExportToExcelButton="False" AddNewRecordText="Ajouter nouveau" AddNewRecordImageUrl="~/images/save_add.png"
                                                ShowAddNewRecordButton="true" ShowRefreshButton="true" />
                                            <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                            <Columns>
                                                <telerik:GridTemplateColumn Visible="true" ShowFilterIcon="false" AllowFiltering="false" HeaderText="#" UniqueName="Compteur"
                                                    HeaderStyle-Width="17px"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label Visible="true" ID="lbOrder" runat="server" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <%--<telerik:GridTemplateColumn HeaderText="Validité" Visible="true" SortExpression="IsValideAccount" AllowFiltering="false" UniqueName="IsValideAccount">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="Btn_IsValideAccount" ToolTip="Compte Valide" runat="server"
                                                            CommandName="IsValideAccount" CommandArgument='<%# Bind("ID") %>' ImageUrl="~/images/img_succes.png" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="16px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>--%>
                                                <%--<telerik:GridBoundColumn HeaderText="Code" DataField="ID" UniqueName="ID" Display="true" AllowFiltering="false" />--%>

                                                <telerik:GridDateTimeColumn DataField="DateCreated" HeaderText="DateCreated" FilterControlWidth="110px"
                                                    SortExpression="DateCreated" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                                                    DataFormatString="{0:dd/MM/yyyy}" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                    <HeaderStyle HorizontalAlign="Center" Width="16px" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" Width="16px" />
                                                </telerik:GridDateTimeColumn>


                                                <telerik:GridTemplateColumn HeaderText="Infos candidats" SortExpression="NOM_COMPLET" AllowFiltering="true" UniqueName="NOM_COMPLET"
                                                    DataField="Email_Adresse_NomComplet_Telephone"
                                                    FilterControlAltText="Recherche par Email, Nom, Prénom, Adresse et téléphone" FilterControlWidth="95%" ShowFilterIcon="false"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                    <ItemTemplate>
                                                        <div runat="server" id="DIV_Image_PHOTO" style="float: left;">
                                                            <asp:Image ID="Image_PHOTO" Style="border-width: 0px; height: 45px; margin-right: 5px;" runat="server"
                                                                CssClass="img-circle" ImageUrl='<%# Eval("ID", "~/Show_Image.aspx?ID={0}&Action=User")%>' />
                                                        </div>
                                                        <div style="float: left;">
                                                            <asp:Label ID="Label_SexeIcon_FontAwesome" runat="server" Text='<%# Bind("SexeIcon_FontAwesome")%>' />
                                                            <asp:Label ID="Label_NOM_COMPLET" runat="server" Text='<%# Bind("NOMCOMPLET")%>' Style="font-weight: bold;" />

                                                            <hr style="margin: 0px; padding: 0px;" />
                                                            <a href='<%# Eval("Email", "mailto:{0}")%>'>
                                                                <i class="fa fa-envelope"></i>
                                                                <asp:Label ID="Label_ADRESSE_EMAIL" runat="server" Text='<%# Bind("Email")%>' /><br />
                                                            </a>

                                                            <hr style="margin: 0px; padding: 0px;" />
                                                            <%--<a href='<%# Eval("Telephone", "tel:{0}")%>'>
                                                                <i class="fa fa-phone"></i>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Telephone")%>' ForeColor="Black"
                                                                    Font-Italic="True" />
                                                            </a>--%>

                                                            <%--<asp:Label ID="LabelPoste1" runat="server" Text='<%# Bind("Poste") %>' Style="font-size: 11px; color: Blue;" />--%>
                                                            <%--<asp:Label ID="LabelAdresseComplet" runat="server" Text='<%# Bind("AdresseComplet") %>' Style="display: block;" />--%>
                                                            <asp:Label ID="LabelAdresseComplet1" runat="server" Text='<%# Bind("CreatedBy") %>' Style="font-size: 11px; color: red;" />
                                                        </div>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>

                                                <telerik:GridTemplateColumn DataField="GroupeStr" UniqueName="GroupeStr" SortExpression="GroupeStr" HeaderText=" Groupe"
                                                    FilterControlAltText="Filter Groupe Utilisateur column" FilterControlWidth="99%" ShowFilterIcon="false"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                    <FilterTemplate>
                                                        <telerik:RadComboBox ID="rcmb_CATEGORIE_ANNONCE" runat="server" Culture="fr-FR" AppendDataBoundItems="true"
                                                            DataSourceID="SqlDS_GROUPE_USER" DataTextField="Groupe_Description" DataValueField="Groupe_Description"
                                                            SelectedValue='<%# CType(Container, GridItem).OwnerTableView.GetColumn("GroupeStr").CurrentFilterValue%>'
                                                            OnClientSelectedIndexChanged="BrandNameCategorieAgeComboIndexChanged">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="--- ---" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                        <telerik:RadScriptBlock ID="RadScriptBlockCategorieAge" runat="server">
                                                            <script type="text/javascript">
                                                                function BrandNameCategorieAgeComboIndexChanged(sender, args) {
                                                                    var tableView = $find("<%# CType(Container, GridItem).OwnerTableView.ClientID %>");
                                                                    tableView.filter("GroupeStr", args.get_item().get_value(), "EqualTo");
                                                                }
                                                            </script>
                                                        </telerik:RadScriptBlock>
                                                    </FilterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <i>
                                                            <asp:Label ID="LabelGroupeStr2" runat="server" Text='<%# Bind("GroupeStr")%>'></asp:Label></i>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>

                                                <telerik:GridTemplateColumn HeaderText="" UniqueName="Valider" ShowFilterIcon="false" AllowFiltering="false">
                                                    <ItemTemplate>
                                                        <div class="btn-group">
                                                            <button type="button" class="btn btn-sm btn-primary dropdown-toggle" data-toggle="dropdown">Actions</button>
                                                            <button type="button" class="btn btn-sm btn-primary dropdown-toggle" data-toggle="dropdown">
                                                                <span class="caret"></span>
                                                                <span class="sr-only">Toggle Dropdown</span></button><ul class="dropdown-menu" role="menu">
                                                                    <li id="LI_Btn_Disconnect" runat="server" style="cursor: pointer;" data-placement="left" data-toggle="tooltip" data-original-title="Déconnecter l'utilisateur">
                                                                        <asp:LinkButton ID="Btn_Disconnect" ToolTip="Deconnecter" runat="server"
                                                                            CommandName="DISCONNECT_USER" CommandArgument='<%# Bind("ID") %>'>                                                            
                                                                            <i class="fa fa-keys"></i> Déconnecter
                                                                        </asp:LinkButton>
                                                                    </li>
                                                                    <li data-placement="left" data-toggle="tooltip" data-original-title="Réinitialiser le mot de passe">
                                                                        <asp:LinkButton ID="Btn_ResetPassword" ToolTip="Reset Mot de passe" runat="server"
                                                                            CommandName="RESET_PASSWORD" CommandArgument='<%# Bind("ID") %>'>                                                        
                                                                            <i class="fa fa-refresh"></i> Reset Mot de passe
                                                                        </asp:LinkButton>
                                                                    </li>
                                                                    <li class="divider"></li>
                                                                    <li data-placement="left" data-toggle="tooltip" data-original-title="Envoyer une notification sur son téléphone Android">
                                                                        <asp:LinkButton ID="LinkButton_SendNotificationAndroid" runat="server">                                        
                                                                            <i class="glyphicon glyphicon-comment"></i>                                         
                                                                            Notification Android
                                                                        </asp:LinkButton>
                                                                    </li>
                                                                    <li data-placement="left" data-toggle="tooltip" data-original-title="Envoyer un email">
                                                                        <asp:LinkButton ID="Btn_SendEmail" runat="server" CommandName="SEND_EMAIL" CommandArgument='<%# Bind("ID") %>'>                                                                    
                                                                            <i class="fa  fa-envelope"></i> Contacter par email
                                                                        </asp:LinkButton>
                                                                    </li>
                                                                </ul>
                                                        </div>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>

                                                <telerik:GridTemplateColumn HeaderText="Conf. Y/N" AllowFiltering="false" SortExpression="CONFIRMER_IMAGE">
                                                    <ItemTemplate>
                                                        <asp:Image ID="LabelCONFIRMER_IMAGE" runat="server" ImageUrl='<%# Bind("CONFIRMER_IMAGE")%>' Style="" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="16px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>

                                                <telerik:GridCheckBoxColumn DataField="Actifyn" HeaderText="Actif Y/N" UniqueName="Actifyn"
                                                    FilterControlAltText="Filter Actifyn column" HeaderStyle-Wrap="false" ShowFilterIcon="false" AllowFiltering="false">
                                                    <HeaderStyle HorizontalAlign="Center" Width="16px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridCheckBoxColumn>
                                                <%--<telerik:GridTemplateColumn Visible="true" ShowFilterIcon="false" AllowFiltering="false" UniqueName="DISCONNECT_USER">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CBX_CONNECTED_YN" Visible="false" runat="server" Checked='<%# Bind("Connecteryn") %>' />
                                                        <asp:ImageButton ID="Btn_Disconnect" ToolTip="Deconnecter" runat="server" Width="15"
                                                            CommandName="DISCONNECT_USER" CommandArgument='<%# Bind("ID") %>' ImageUrl="~/images/Keys.png" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="16px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>--%>

                                                <%--<telerik:GridTemplateColumn HeaderText="MDP" Visible="true" AllowFiltering="false" UniqueName="RESET_PASSWORD">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="Btn_ResetPassword" ToolTip="Reset Mot de passe" runat="server"
                                                            CommandName="RESET_PASSWORD" CommandArgument='<%# Bind("ID") %>' ImageUrl="~/images/view-refresh.png" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="16px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>--%>
                                                <%--<telerik:GridTemplateColumn HeaderText="Email" AllowFiltering="false" UniqueName="SEND_EMAIL">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="Btn_SendEmail" runat="server"
                                                            ToolTip="Envoyer email" ImageUrl="~/Images/email.png" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>--%>
                                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="UPDATE" DataTextField="ID"
                                                    HeaderStyle-Width="17px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                    ImageUrl="~/images/_edit.png" UniqueName="UPDATE">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridButtonColumn>
                                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="DELETE" UniqueName="DELETE" DataTextField="ID"
                                                    HeaderStyle-Width="17px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                    ImageUrl="~/images/delete.png" ConfirmDialogType="RadWindow"
                                                    ConfirmText="Êtes-vous sûr de vouloir supprimer?" ConfirmTitle="Attention!">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridButtonColumn>
                                            </Columns>
                                            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                                            </RowIndicatorColumn>
                                            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                                            </ExpandCollapseColumn>
                                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                            <EditFormSettings>
                                                <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                                </EditColumn>
                                            </EditFormSettings>

                                        </MasterTableView>
                                        <ClientSettings>
                                            <ClientEvents></ClientEvents>
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                    </telerik:RadGrid>
                                    <asp:SqlDataSource ID="SqlDS_GROUPE_USER" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringDataBase %>" SelectCommand="SELECT [ID_Group],[Groupe_Description] FROM [TBL_SR_GROUPE]"></asp:SqlDataSource>

                                    <telerik:RadWindowManager ID="RadWindowManager2" IconUrl="~/images/LogoRezo509.png" runat="server" VisibleStatusbar="false"
                                        EnableViewState="false">
                                        <Windows>
                                            <telerik:RadWindow ID="RadWindow1" runat="server" Title="" IconUrl="~/images/LogoRezo509.png" Left="75px" ReloadOnShow="true"
                                                ShowContentDuringLoad="false" Modal="true" OnClientClose="RadWindowClosing" Behaviors="Reload, Pin, Minimize, Move, Resize, Maximize, Close"
                                                EnableShadow="true" OnClientResizeEnd="RadWindowClientResizeEnd" />
                                        </Windows>
                                    </telerik:RadWindowManager>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </asp:Panel>
        </div>

        <!-- FORM LOGIN -->
        <div class="md-modal colored-header custom-width md-effect-12" id="FormLoginModal" runat="server" style="perspective: 1300px;">
            <BRAIN:CULogin2 runat="server" ID="LoginWUC" Visible="false" />
        </div>
        <div class="md-overlay"></div>

        <%--<ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_Plan" runat="Server"
            ExpandControlID="PanelHeader_Plan" CollapseControlID="PanelHeader_Plan"
            Collapsed="false" TargetControlID="PanelBody_Plan" TextLabelID="LabelHeader_Plan"
            ImageControlID="ImageButton_Plan" ExpandedText="(Cacher Form...)" CollapsedText="(Cliquer pour afficher form...)"
            ExpandedImage="~/images/collapse.jpg" CollapsedImage="~/images/expand.jpg" SuppressPostBack="true"
            SkinID="CollapsiblePanelDemo" />--%>

        <%--<ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender_References" runat="Server"
            ExpandControlID="PanelHeader_Reference" CollapseControlID="PanelHeader_Reference"
            Collapsed="false" TargetControlID="PanelBody_Reference" TextLabelID="LabelHeader_Reference"
            ImageControlID="ImageButton_Reference" ExpandedText="(Cacher Form...)" CollapsedText="(Cliquer pour afficher form...)"
            ExpandedImage="~/images/collapse.jpg" CollapsedImage="~/images/expand.jpg" SuppressPostBack="true"
            SkinID="CollapsiblePanelDemo" />--%>

        <input id="txtWindowPage" type="hidden" />
    </section>
</asp:Content>
