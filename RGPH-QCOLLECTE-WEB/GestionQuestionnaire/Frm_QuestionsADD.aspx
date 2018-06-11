<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/DashboardCZMasterPage.master" AutoEventWireup="false" CodeFile="Frm_QuestionsADD.aspx.vb"
    Inherits="GestionQuestionnaire_Frm_QuestionsADD" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function closeWindow() {
                GetRadWindow().BrowserWindow.refreshMe();
                GetRadWindow().close();
            }
            function CloseAndRefreshListeQuestion() {
                GetRadWindow().BrowserWindow.refreshMe();
                GetRadWindow().close();
            }
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)
                return oWindow;
            }
            //------------ POSSIBILITE DE REPONSE -----------//

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
                //$find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("refreshListeReponse");
            }

            function refreshListeReponse() {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("refreshListeReponse");
            }
            function refreshListeSpecificationControle() {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("refreshListeSpecificationControle");
            }


            <%--var listItemIndex = null;
            function MenuItemClicked(sender, eventArgs) {
                var clickedItemValue = eventArgs.get_item().get_value();
                var rdGrid = $find("<%=rdgQuestions_Reponses.ClientID %>");
                var _id = rdGrid.get_masterTableView().get_dataItems()[listItemIndex].get_element().cells[0].innerHTML
                switch (clickedItemValue) {
                    case "Editer":
                        ShowAddUpdateFormMaximized('Frm_Questions_ReponsesADD.aspx?ID=' + _id + '&ACTION=HideMenuHeader', 950, 550); break;
                    case "Delete":
                        ShowAddUpdateFormMaximized('Frm_Questions_ReponsesADD.aspx?ID=' + _id + '&ACTION=HideMenuHeader', 950, 550); break;
                    default:
                        break;
                }
            }--%>

            <%--function RowContextMenu(sender, eventArgs) {
                var menu = $find("<%= ContextMenu.ClientID %>");
                var evt = eventArgs.get_domEvent();
                if (evt.target.tagName == "INPUT" || evt.target.tagName == "A") { return; }
                var index = eventArgs.get_itemIndexHierarchical();
                document.getElementById("radGridClickedRowIndex").value = index;
                listItemIndex = index;
                sender.get_masterTableView().selectItem(sender.get_masterTableView().get_dataItems()[index].get_element(), true);
                menu.show(evt);
                evt.cancelBubble = true;
                evt.returnValue = false;
                if (evt.stopPropagation) {
                    evt.stopPropagation();
                    evt.preventDefault();
                }
            }--%>

           <%-- function RowDblClick(sender, eventArgs) {
                var index = eventArgs.get_itemIndexHierarchical();
                document.getElementById("radGridClickedRowIndex").value = index;
                listItemIndex = index;
                var rdGrid = $find("<%=rdgQuestions_Reponses.ClientID %>");
                var _id = rdGrid.get_masterTableView().get_dataItems()[listItemIndex].get_element().cells[0].innerHTML
                ShowAddUpdateFormMaximized('Frm_Questions_ReponsesADD.aspx?ID=' + _id + '&ACTION=HideMenuHeader', 950, 550);
            }--%>
        </script>
    </telerik:RadCodeBlock>
    <%--<telerik:RadScriptManager ID="RadScriptManager1"  runat="server"></telerik:RadScriptManager>--%>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel_Msg" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="Panel_First" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="Btn_SaveInfo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel_Msg" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="Panel_First" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
    <div class="container-fluid" id="pcont">
        <div class="mail-inbox">
            <section class="page-head" id="PageHeader" runat="server">
                <h3>
                    <i class="fa fa-dashboard"></i>
                    <asp:Label ID="Label_Titre" runat="server" Text="Questions" />
                    <small id="OL_SeeAllData" runat="server">
                        <asp:Label ID="Label_SousTitre" runat="server" />
                    </small>
                </h3>
                <!--<ol class="breadcrumb"> 
    <li><a href="#"><i class="fa fa-dashboard"></i>Accueil</a></li>
    <li class="active">Questions</li>
</ol> -->
            </section>
            <section class="content">
                <Msg:msgBox ID="Dialogue" runat="server" />
                <asp:Panel runat="server" ID="Panel_Msg" Visible="false">
                    <div id="DIV_Msg" runat="server" class="alert alert-warning alert-dismissable">
                        <i id="Icon_Msg" runat="server" class="fa fa-warning"></i>
                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                        <asp:Image ID="Image_Msg" runat="server" />
                        <asp:Label ID="Label_Msg" runat="server" Text=""></asp:Label>
                    </div>
                </asp:Panel>

                <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="GPSave" runat="server" CssClass="alert alert-danger alert-dismissable" ShowMessageBox="true" ShowSummary="true" />

                <asp:Panel ID="Panel_First" runat="server" CssClass="panel1 panel-default1 panel-body1" Style="margin: 5px;">
                    <asp:Panel runat="server" ID="PanelChoixReponse">
                        <div class="tab-container">
                            <ul class="nav nav-tabs">
                                <li class="active" id="li_Question" runat="server">
                                    <asp:LinkButton ID="LinkButton_Question" runat="server">
                                        <i id="i_Question" runat="server" class="fa fa-tasks"></i>
                                        Questions
                                        <asp:Label runat="server" ID="LabelQuestions"></asp:Label>
                                    </asp:LinkButton>
                                </li>
                                <li id="li_Reponse" runat="server">
                                    <asp:LinkButton ID="LinkButton_Reponse" runat="server">
                                        <i id="i_Reponse" runat="server" class="fa fa-tasks"></i>
                                        Réponse
                                        <asp:Label runat="server" ID="LabelReponseTitre" Text="Réponse" />
                                    </asp:LinkButton>
                                </li>
                                <li id="li_Specifications" runat="server">
                                    <asp:LinkButton ID="LinkButton_Specifications" runat="server">
                                        <i id="i_Specifications" runat="server" class="fa fa-tasks"></i>
                                        Spécifications de Controle
                                        <asp:Literal runat="server" ID="Literal_Specifications" />
                                    </asp:LinkButton>
                                </li>
                            </ul>
                            <div class="tab-content">
                                <div id="DIV_Content_Question" runat="server" class="tab-pane cont active">
                                    <div id="DIV_Panel" class="form-horizontalw group-border-dashed1w" style="border-radius: 0px;">

                                        <div class="form-group">
                                            <div class="col-sm-6">
                                                <label>
                                                    Type Module
                               <asp:RequiredFieldValidator ID="RFV_TypeModule" runat="server" ControlToValidate="DDL_TypeModule"
                                   ErrorMessage="_ Type Module Obligatoire !" SetFocusOnError="true" Display="Dynamic" Text="*"
                                   ValidationGroup="GPSave" CssClass="text-danger" />
                                                    <asp:RequiredFieldValidator ID="RFV1_TypeModule" runat="server" ControlToValidate="DDL_TypeModule"
                                                        ErrorMessage="Type Module Obligatoire !" InitialValue="0" SetFocusOnError="true" Display="Dynamic" Text="*"
                                                        ValidationGroup="GPSave" CssClass="text-danger" />
                                                </label>
                                                <asp:DropDownList ID="DDL_TypeModule" CssClass="form-control" Width="100%" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <div style="clear: both;"></div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-3">
                                                <label>
                                                    Code Question
                               <asp:RequiredFieldValidator ID="RFV_CodeQuestion" runat="server" ControlToValidate="txt_CodeQuestion"
                                   ErrorMessage="Code Question Obligatoire !" SetFocusOnError="true" Display="Dynamic" Text="*"
                                   ValidationGroup="GPSave" CssClass="text-danger" />
                                                </label>
                                                <asp:TextBox ID="txt_CodeQuestion" CssClass="form-control" Width="100%" runat="server" placeholder="Code Question..."></asp:TextBox>
                                            </div>

                                            <div class="col-sm-3">
                                                <label>
                                                    QPrécédent
                                                </label>
                                                <asp:DropDownList ID="DDL_QPrecedent" CssClass="form-control" Width="100%" runat="server">
                                                    <asp:ListItem Value="" Text="-- VIDE --" />
                                                    <asp:ListItem Value="DEBUT" Text="DEBUT" />
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-sm-3">
                                                <label>
                                                    QSuivant
                                   <asp:RequiredFieldValidator ID="RFV_QSuivant" runat="server" ControlToValidate="RCB_QSuivant"
                                       ErrorMessage="QSuivant Obligatoire !" SetFocusOnError="true" Display="Dynamic" Text="*"
                                       ValidationGroup="GPSave" CssClass="text-danger" />
                                                </label>
                                                <telerik:RadComboBox runat="server" ID="RCB_QSuivant" Skin="MetroTouch" CssClass="form-control" EmptyMessage="Sélectionnez ou Tapez la question suivante"
                                                    ShowMoreResultsBox="true" EnableLoadOnDemand="true" EnableVirtualScrolling="true" Width="100%">
                                                </telerik:RadComboBox>
                                            </div>

                                            <div class="col-sm-3">
                                                <label for="ctl00_ContentPlaceHolder1_CB_EstSautReponse">
                                                    Le saut dépend de la réponse?
                                                </label>
                                                <asp:CheckBox ID="CB_EstSautReponse" CssClass="form-control" runat="server" Text=" ( Oui / Non )" Style="border: none;" />
                                            </div>
                                            <div style="clear: both;"></div>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <label>
                                                    Libéllé
                               <asp:RequiredFieldValidator ID="RFV_Libelle" runat="server" ControlToValidate="txt_Libelle"
                                   ErrorMessage="Libelle Obligatoire !" SetFocusOnError="true" Display="Dynamic" Text="*"
                                   ValidationGroup="GPSave" CssClass="text-danger" />
                                                </label>
                                                <asp:TextBox ID="txt_Libelle" TextMode="MultiLine" CssClass="form-control" Width="100%" runat="server" placeholder="Libelle..."></asp:TextBox>
                                            </div>
                                            <div style="clear: both;"></div>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <label>
                                                    Details Question
                                                </label>
                                                <asp:TextBox ID="txt_DetailsQuestion" TextMode="MultiLine" CssClass="form-control" Width="100%" runat="server" placeholder="Details Question..."></asp:TextBox>
                                            </div>
                                            <div style="clear: both;"></div>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <label>
                                                    Indications (Aides)
                                                </label>
                                                <asp:TextBox ID="txt_IndicationsQuestion" TextMode="MultiLine" CssClass="form-control" Width="100%" runat="server" placeholder="Indications Question..."></asp:TextBox>
                                            </div>
                                            <div style="clear: both;"></div>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-sm-4">
                                                <label>
                                                    Catégorie 
                               <asp:RequiredFieldValidator ID="RFV_CodeCategorie" runat="server" ControlToValidate="DDL_CodeCategorie"
                                   ErrorMessage="_ Code Categorie Obligatoire !" SetFocusOnError="true" Display="Dynamic" Text="*"
                                   ValidationGroup="GPSave" CssClass="text-danger" />
                                                    <asp:RequiredFieldValidator ID="RFV1_CodeCategorie" runat="server" ControlToValidate="DDL_CodeCategorie"
                                                        ErrorMessage="_ Code Categorie Obligatoire !" InitialValue="0" SetFocusOnError="true" Display="Dynamic" Text="*"
                                                        ValidationGroup="GPSave" CssClass="text-danger" />
                                                </label>
                                                <asp:DropDownList ID="DDL_CodeCategorie" CssClass="form-control" Width="100%" runat="server">
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-sm-4">
                                                <label>
                                                    Nom Champs
                               <asp:RequiredFieldValidator ID="RFV_NomChamps" runat="server" ControlToValidate="txt_NomChamps"
                                   ErrorMessage="Nom Champs Obligatoire !" SetFocusOnError="true" Display="Dynamic" Text="*"
                                   ValidationGroup="GPSave" CssClass="text-danger" />
                                                </label>
                                                <asp:TextBox ID="txt_NomChamps" CssClass="form-control" Width="100%" runat="server" placeholder="Nom Champs..."></asp:TextBox>
                                            </div>

                                            <div class="col-sm-4">
                                                <label>
                                                    Type Question
                                                </label>
                                                <asp:DropDownList ID="DDL_TypeQuestion" AutoPostBack="true" CssClass="form-control" Width="100%" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <div style="clear: both;"></div>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-sm-3" id="DIV_CaratereAccepte" runat="server">
                                                <label>
                                                    Caractère Accepté
                               <asp:RequiredFieldValidator ID="RFV_CaratereAccepte" runat="server" ControlToValidate="DDL_CaratereAccepte"
                                   ErrorMessage="_ Caratere Accepte Obligatoire !" SetFocusOnError="true" Display="Dynamic" Text="*"
                                   ValidationGroup="GPSave" CssClass="text-danger" />
                                                </label>
                                                <asp:DropDownList ID="DDL_CaratereAccepte" CssClass="form-control" Width="100%" runat="server">
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-sm-3" id="DIV_NbreValeurMinimal" runat="server">
                                                <label>
                                                    Nbre Valeur Minimal
                               <asp:RequiredFieldValidator ID="RFV_NbreValeurMinimal" runat="server" ControlToValidate="txt_NbreValeurMinimal"
                                   ErrorMessage="Nbre Valeur Minimal Obligatoire !" SetFocusOnError="true" Display="Dynamic" Text="*"
                                   ValidationGroup="GPSave" CssClass="text-danger" />
                                                </label>
                                                <asp:TextBox ID="txt_NbreValeurMinimal" CssClass="form-control" Width="100%" runat="server" Text="-1" placeholder="Nbre Valeur Minimal..."></asp:TextBox>
                                            </div>

                                            <div class="col-sm-3" id="DIV_NbreCaratereMinimum" runat="server" visible="false">
                                                <label>
                                                    Nbre Caratere Minimum
                               <asp:RequiredFieldValidator ID="RFV_NbreCaratereMinimum" runat="server" ControlToValidate="txt_NbreCaratereMinimum"
                                   ErrorMessage="Nbre Caratere Minimum Obligatoire !" SetFocusOnError="true" Display="Dynamic" Text="*"
                                   ValidationGroup="GPSave" CssClass="text-danger" />
                                                </label>
                                                <asp:TextBox ID="txt_NbreCaratereMinimum" CssClass="form-control" Width="100%" runat="server" Text="-1" placeholder="Nbre Caratere Minimum..."></asp:TextBox>
                                            </div>

                                            <div class="col-sm-3" id="DIV_NbreCaratereMaximal" runat="server">
                                                <label>
                                                    Nbre Caractère Maximal
                               <asp:RequiredFieldValidator ID="RFV_NbreCaratereMaximal" runat="server" ControlToValidate="txt_NbreCaratereMaximal"
                                   ErrorMessage="Nbre Caratere Maximal Obligatoire !" SetFocusOnError="true" Display="Dynamic" Text="*"
                                   ValidationGroup="GPSave" CssClass="text-danger" />
                                                </label>
                                                <asp:TextBox ID="txt_NbreCaratereMaximal" CssClass="form-control" Width="100%" runat="server" Text="-1" placeholder="Nbre Caratere Maximal..."></asp:TextBox>
                                            </div>
                                            <div style="clear: both;"></div>
                                        </div>

                                        <div style="clear: both;"></div>

                                        <div id="DIV_SaveInfo" runat="server" style="margin: 5px 0px; text-align: left;">
                                            <span id="span_SaveInfo" runat="server">
                                                <asp:LinkButton ID="Btn_SaveInfo" runat="server" CssClass="btn btn-primary" ValidationGroup="GPSave">
                                    <i class="fa fa-save" ></i> Enregistrer
                                                </asp:LinkButton>
                                            </span>

                                            &nbsp;
                            <asp:LinkButton ID="Btn_Annuler" CausesValidation="false" runat="server" CssClass="btn btn-danger">
                                <i class="fa  fa-reply-all" ></i> Annuler
                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                                <div id="DIV_Content_Reponse" runat="server" class="tab-pane">
                                    <div runat="server" id="divLiteralMsg" visible="false" class="alert alert-warning alert-white rounded">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <div class="icon"><i class="fa fa-info-circle"></i></div>
                                        <asp:Literal ID="LiteralMsg" runat="server"></asp:Literal>
                                    </div>
                                    <asp:Panel ID="PanelListePossibiliteReponse" runat="server">
                                        <asp:LinkButton ID="LinkButton_NewReponse" runat="server" CssClass="btn btn-info" Style="margin-bottom: 5px;">
                                            <i class="fa fa-plus-square-o"></i> Ajouter élément de Réponse </asp:LinkButton>
                                        <telerik:RadGrid ID="rdgQuestions_Reponses" AllowPaging="True" AllowSorting="True" PageSize="20"
                                            runat="server" AutoGenerateColumns="False" GridLines="None" AllowFilteringByColumn="true"
                                            Culture="fr-FR" ShowGroupPanel="True"
                                            EnableViewState="true" AllowMultiRowSelection="false" GroupingSettings-CaseSensitive="false">
                                            <ExportSettings HideStructureColumns="true" />
                                            <MasterTableView CommandItemDisplay="None" GridLines="None" DataKeyNames="ID" NoDetailRecordsText="Pas d'enregistrement"
                                                NoMasterRecordsText="Pas d'enregistrement">
                                                <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowExportToExcelButton="true"
                                                    ExportToExcelText="Exporter en excel" />
                                                <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="ID" UniqueName="ID" Display="false" />
                                                    <telerik:GridTemplateColumn Visible="true" ShowFilterIcon="false" AllowFiltering="false" HeaderText="#" UniqueName="Compteur">
                                                        <ItemTemplate>
                                                            <asp:Label Visible="true" ID="lbOrder" runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="16px" />
                                                        <ItemStyle HorizontalAlign="Center" Width="16px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="CodeUniqueReponse" UniqueName="CodeUniqueReponse" HeaderText=" Code Unique Reponse"
                                                        FilterControlAltText="Filter CodeUniqueReponse column" FilterControlWidth="95%" ShowFilterIcon="false"
                                                        AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="CodeReponse" UniqueName="CodeReponse" HeaderText=" Code Reponse"
                                                        FilterControlAltText="Filter CodeReponse column" FilterControlWidth="95%" ShowFilterIcon="false"
                                                        AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="LibelleReponse" UniqueName="LibelleReponse" HeaderText=" Libelle Reponse"
                                                        FilterControlAltText="Filter LibelleReponse column" FilterControlWidth="95%" ShowFilterIcon="false"
                                                        AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="EstEnfant" UniqueName="EstEnfant" HeaderText=" Est Enfant"
                                                        FilterControlAltText="Filter EstEnfant column" FilterControlWidth="95%" ShowFilterIcon="false"
                                                        AllowFiltering="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="AvoirEnfant" UniqueName="AvoirEnfant" HeaderText=" Avoir Enfant"
                                                        FilterControlAltText="Filter AvoirEnfant column" FilterControlWidth="95%" ShowFilterIcon="false"
                                                        AllowFiltering="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="CodeParentSTR" UniqueName="CodeParent" HeaderText=" Code Parent"
                                                        FilterControlAltText="Recherche par  Code Parent" FilterControlWidth="95%" ShowFilterIcon="false"
                                                        AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="QSuivant" UniqueName="QSuivant" HeaderText=" Q Suivant"
                                                        FilterControlAltText="Recherche par  Q Suivant" FilterControlWidth="95%" ShowFilterIcon="false"
                                                        AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandArgument="ID" CommandName="editer"
                                                        DataTextField="ID" ImageUrl="~/images/_edit.png"
                                                        HeaderText="" UniqueName="editer">
                                                        <HeaderStyle HorizontalAlign="Center" Width="16px" />
                                                        <ItemStyle HorizontalAlign="Center" Width="16px" />
                                                    </telerik:GridButtonColumn>
                                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="delete" DataTextField="ID"
                                                        ImageUrl="~/images/delete.png"
                                                        UniqueName="delete" HeaderText="" ConfirmDialogType="RadWindow" ConfirmText="Voulez-vous vraiment supprimer cette information ?"
                                                        ConfirmTitle="Attention!">
                                                        <HeaderStyle HorizontalAlign="Center" Width="16px" />
                                                        <ItemStyle HorizontalAlign="Center" Width="16px" />
                                                    </telerik:GridButtonColumn>
                                                </Columns>
                                                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>
                                                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
                                            </MasterTableView>
                                            <GroupingSettings CaseSensitive="False" />
                                            <ClientSettings AllowDragToGroup="True" AllowColumnsReorder="True">
                                                <%--<ClientEvents OnRowContextMenu="RowContextMenu" OnRowDblClick="RowDblClick" />--%>
                                                <Selecting AllowRowSelect="true" />
                                            </ClientSettings>
                                            <%--<HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default" />--%>
                                            <PagerStyle PageSizeControlType="RadComboBox" />
                                            <FilterMenu EnableImageSprites="False"></FilterMenu>
                                        </telerik:RadGrid>
                                    </asp:Panel>
                                </div>

                                <div id="DIV_Content_Specifications" runat="server" class="tab-pane">
                                    <asp:Panel ID="PanelListeSpecifications" runat="server">
                                        <asp:LinkButton ID="LinkButton_NewSpecifications" runat="server" CssClass="btn btn-warning" Style="margin-bottom: 5px;">
                                            <i class="fa fa-plus-square-o"></i> Ajouter Spécifications de Controle </asp:LinkButton>
                                        <telerik:RadGrid ID="RadGridSpecifications" AllowPaging="True" AllowSorting="True" PageSize="20"
                                            runat="server" AutoGenerateColumns="False" GridLines="None" AllowFilteringByColumn="true"
                                            Culture="fr-FR" ShowGroupPanel="True"
                                            EnableViewState="true" AllowMultiRowSelection="false" GroupingSettings-CaseSensitive="false">
                                            <ExportSettings HideStructureColumns="true" />
                                            <MasterTableView CommandItemDisplay="Top" GridLines="None" DataKeyNames="ID" NoDetailRecordsText="Pas d'enregistrement"
                                                NoMasterRecordsText="Pas d'enregistrement">
                                                <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="true" ShowExportToExcelButton="false"
                                                    ExportToExcelText="Exporter en excel" />
                                                <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="ID" UniqueName="ID" Display="false" />
                                                    <telerik:GridTemplateColumn Visible="true" ShowFilterIcon="false" AllowFiltering="false" HeaderText="#" UniqueName="Compteur">
                                                        <ItemTemplate>
                                                            <asp:Label Visible="true" ID="lbOrder" runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="16px" />
                                                        <ItemStyle HorizontalAlign="Center" Width="16px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="Instructions" UniqueName="Instructions" HeaderText="Instructions"
                                                        FilterControlAltText="Filter Instructions column" FilterControlWidth="95%" ShowFilterIcon="false"
                                                        AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Controles" UniqueName="Controles" HeaderText="Controles"
                                                        FilterControlAltText="Filter Controles column" FilterControlWidth="95%" ShowFilterIcon="false"
                                                        AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Messages" UniqueName="Messages" HeaderText="Messages"
                                                        FilterControlAltText="Filter Messages column" FilterControlWidth="95%" ShowFilterIcon="false"
                                                        AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandArgument="ID" CommandName="editer"
                                                        DataTextField="ID" ImageUrl="~/images/_edit.png"
                                                        HeaderText="" UniqueName="editer">
                                                        <HeaderStyle HorizontalAlign="Center" Width="16px" />
                                                        <ItemStyle HorizontalAlign="Center" Width="16px" />
                                                    </telerik:GridButtonColumn>
                                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="delete" DataTextField="ID"
                                                        ImageUrl="~/images/delete.png"
                                                        UniqueName="delete" HeaderText="" ConfirmDialogType="RadWindow" ConfirmText="Voulez-vous vraiment supprimer cette information ?"
                                                        ConfirmTitle="Attention!">
                                                        <HeaderStyle HorizontalAlign="Center" Width="16px" />
                                                        <ItemStyle HorizontalAlign="Center" Width="16px" />
                                                    </telerik:GridButtonColumn>
                                                </Columns>
                                                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>
                                                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
                                            </MasterTableView>
                                            <GroupingSettings CaseSensitive="False" />
                                            <ClientSettings AllowDragToGroup="True" AllowColumnsReorder="True">
                                                <%--<ClientEvents OnRowContextMenu="RowContextMenu" OnRowDblClick="RowDblClick" />--%>
                                                <Selecting AllowRowSelect="true" />
                                            </ClientSettings>
                                            <%--<HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default" />--%>
                                            <PagerStyle PageSizeControlType="RadComboBox" />
                                            <FilterMenu EnableImageSprites="False"></FilterMenu>
                                        </telerik:RadGrid>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </asp:Panel>
                <!-- FORM LOGIN -->
                <BRAIN:CULogin2 runat="server" ID="LoginWUC" Visible="false" />
                <div class="md-overlay"></div>
                <asp:TextBox ID="txt_CodeQuestions_Hid" runat="server" Text="0" Visible="False" Width="1px"></asp:TextBox>
            </section>

            <asp:Literal runat="server" ID="LiteralStyleCSS"></asp:Literal>
        </div>
    </div>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" VisibleStatusbar="false" EnableViewState="false">
        <Windows>
            <telerik:RadWindow ID="AddUpdateDialog" runat="server" Title="" IconUrl="~/Images/favicon.ico" Left="75px" ReloadOnShow="true"
                ShowContentDuringLoad="false" Modal="true" OnClientClose="RadWindowClosing" Behaviors="Reload, Move, Resize, Maximize, Close"
                EnableShadow="false" OnClientResizeEnd="RadWindowClientResizeEnd" />
        </Windows>
    </telerik:RadWindowManager>

    <%--<telerik:RadContextMenu ID="ContextMenu" runat="server" OnClientItemClicked="MenuItemClicked" EnableRoundedCorners="true" EnableShadows="true">
        <Items>
            <telerik:RadMenuItem Visible="true" Value="Editer" Text="Visualiser / Modifier" ImageUrl="~/images/_edit.png" HoveredImageUrl="~/images/_edit.png" />
        </Items>
    </telerik:RadContextMenu>--%>

    <input id="txtWindowPage" type="hidden" />
</asp:Content>

