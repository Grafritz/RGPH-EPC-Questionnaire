<%@ Page Title=" Questions" Language="VB" MasterPageFile="~/MasterPages/DashboardCZMasterPage.master" AutoEventWireup="false" 
    MaintainScrollPositionOnPostback="true" CodeFile="Frm_QuestionsListing.aspx.vb" Inherits="Frm_QuestionsListing" %>

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
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Reload");
            }

            function RadWindowClientResizeEnd() {
                var manager = GetRadWindowManager();
                var window1 = manager.getActiveWindow();
                window1.center();
                var bounds = window1.getWindowBounds();
                window1.moveTo(bounds.x + 'px', "50px");
            }

            var listItemIndex = null;
            function MenuItemClicked(sender, eventArgs) {
                var clickedItemValue = eventArgs.get_item().get_value();
                var rdGrid = $find("<%=rdgQuestions.ClientID %>");
                var _id = rdGrid.get_masterTableView().get_dataItems()[listItemIndex].get_element().cells[0].innerHTML
                switch (clickedItemValue) {
                    case "Editer":
                        ShowAddUpdateFormMaximized('Frm_QuestionsADD.aspx?ID=' + _id + '&ACTION=HideMenuHeader', 950, 550); break;
                    case "Delete":
                        ShowAddUpdateFormMaximized('Frm_QuestionsADD.aspx?ID=' + _id + '&ACTION=HideMenuHeader', 950, 550); break;
                    default:
                        break;
                }
            }

            function RowContextMenu(sender, eventArgs) {
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
            }

            function RowDblClick(sender, eventArgs) {
                var index = eventArgs.get_itemIndexHierarchical();
                document.getElementById("radGridClickedRowIndex").value = index;
                listItemIndex = index;
                var rdGrid = $find("<%=rdgQuestions.ClientID %>");
                var _id = rdGrid.get_masterTableView().get_dataItems()[listItemIndex].get_element().cells[0].innerHTML
                ShowAddUpdateFormMaximized('Frm_QuestionsADD.aspx?ID=' + _id + '&ACTION=HideMenuHeader', 950, 550);
            }

            function refreshMe() {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Reload");
            }

            function closeWindow() {
                GetRadWindow().BrowserWindow.refreshMe();
                GetRadWindow().close();
            }

            function CloseAndRefreshListe() {
                GetRadWindow().BrowserWindow.refreshMe();
                GetRadWindow().close();
            }

            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)
                return oWindow;
            }

        </script>
    </telerik:RadCodeBlock>
    <%--<telerik:RadScriptManager ID="RadScriptManager1"  runat="server"> </telerik:RadScriptManager>--%>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel_First" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rdgQuestions">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel_Msg" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="rdgQuestions" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="DDL_TypeModule">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel_Msg" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="rdgQuestions" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="PageHeader" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbtnClearFilters">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rdgQuestions" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
    <input type='hidden' id='radGridClickedRowIndex' name='radGridClickedRowIndex' />
    <div class="container-fluid" id="pcont">
        <div class="mail-inbox">
            <section class="page-head" id="PageHeader" runat="server">
                <h3>
                    <i class="fa fa-dashboard"></i>
                    <asp:Label ID="Label_Titre" runat="server" Text=" Questions" />
                    <small id="OL_SeeAllData" runat="server">
                        <asp:Label ID="Label_SousTitre" runat="server" />
                    </small>
                </h3>
                <!--<ol class="breadcrumb"> 
    <li><a href="#"><i class="fa fa-dashboard"></i>Accueil</a></li>
    <li class="active"> Questions</li>
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

                <asp:Panel runat="server" ID="Panel_First" Style="margin: 5px;">
                    <div class="form-group">
                        <div class="col-sm-4">
                            <asp:LinkButton ID="Btn_ADD_Questions" runat="server" CssClass="btn btn-primary" CausesValidation="false">
                                <i class="fa fa-plus-circle" ></i>  Ajouter  Questions
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-4">
                            <asp:DropDownList ID="DDL_TypeModule" AutoPostBack="true" CssClass="form-control" Width="50%" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="pull-right box-tools">
                        <div class="btn-group">
                            <button type="button" class="btn btn-warning dropdown-toggle" data-toggle="dropdown">
                                Colonnes Tableau <span class="caret"></span>
                            </button>
                            <ul class="dropdown-menu" role="menu">
                                <li>
                                    <asp:CheckBox ID="CBX_DetailsQuestion" runat="server" Text="Details Question"
                                        CssClass="StyleCbxLabel" AutoPostBack="true" Checked="true" ></asp:CheckBox></li>
                                  <li>
                                    <asp:CheckBox ID="CBX_IndicationsQuestion" runat="server" Text="Indications Question"
                                        CssClass="StyleCbxLabel"  AutoPostBack="true"></asp:CheckBox></li>
                                <li>
                                    <asp:CheckBox ID="CBX_CodeCategorie" runat="server" Text="Code Categorie"
                                        CssClass="StyleCbxLabel" AutoPostBack="true" Checked="true" ></asp:CheckBox></li>
                                <li class="divider"></li>
                                <li>
                                    <asp:CheckBox ID="CBX_CaratereAccepte" runat="server" Text="Caratere Accepte"
                                        CssClass="StyleCbxLabel" AutoPostBack="true"></asp:CheckBox></li>
                                <li>
                                    <asp:CheckBox ID="CBX_NbreValeurMinimal" runat="server" Text="Nbre Valeur Minimal"
                                        CssClass="StyleCbxLabel" AutoPostBack="true"></asp:CheckBox></li>
                                <li>
                                    <asp:CheckBox ID="CBX_NbreCaratereMaximal" runat="server" Text="Nbre Caratere Maximal"
                                        CssClass="StyleCbxLabel" AutoPostBack="true"></asp:CheckBox></li>
                            </ul>
                        </div>
                        <asp:LinkButton ID="rbtnClearFilters" runat="server" CssClass="btn btn-sm btn-default" CausesValidation="false"> 
                            <i class="fa fa-ban on fa-filter" ></i> Clear Filters
                        </asp:LinkButton>
                    </div>
                    <div style="clear: both;"></div>

                    <div style="overflow: auto;">
                        <telerik:RadGrid ID="rdgQuestions" AllowPaging="True" AllowSorting="True" PageSize="40"
                            runat="server" AutoGenerateColumns="False" GridLines="None" AllowFilteringByColumn="true"
                            Culture="fr-FR" ShowGroupPanel="True"
                            EnableViewState="true" AllowMultiRowSelection="false" GroupingSettings-CaseSensitive="false">
                            <ExportSettings HideStructureColumns="true" />
                            <MasterTableView CommandItemDisplay="Top" GridLines="None" DataKeyNames="ID" NoDetailRecordsText="Pas d'enregistrement"
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
                                    <telerik:GridBoundColumn DataField="CodeQuestion" UniqueName="CodeQuestion" HeaderText=" Code Question"
                                        FilterControlAltText="Filter CodeQuestion column" FilterControlWidth="95%" ShowFilterIcon="false"
                                        AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn DataField="Libelle" UniqueName="Libelle" HeaderText=" Libelle"
                                        FilterControlAltText="Filter Libelle column" FilterControlWidth="95%" ShowFilterIcon="false"
                                        AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                        <ItemTemplate>
                                            <asp:Literal ID="LiteralIsHaveSpecification1" runat="server" Text='<%#Bind("IsHaveSpecification") %>' />
                                            <asp:Literal ID="LiteralLibelle1" runat="server" Text='<%#Bind("Libelle") %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="DetailsQuestion" UniqueName="DetailsQuestion" HeaderText=" Details Question"
                                        FilterControlAltText="Filter DetailsQuestion column" FilterControlWidth="95%" ShowFilterIcon="false"
                                        AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="IndicationsQuestion" UniqueName="IndicationsQuestion" HeaderText=" Indications Question"
                                        FilterControlAltText="Filter IndicationsQuestion column" FilterControlWidth="95%" ShowFilterIcon="false"
                                        AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="false" >
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CodeCategorieSTR" UniqueName="CodeCategorie" HeaderText=" Code Categorie"
                                        FilterControlAltText="Recherche par  Code Categorie" FilterControlWidth="95%" ShowFilterIcon="false"
                                        AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NomChamps" UniqueName="NomChamps" HeaderText=" Nom Champs"
                                        FilterControlAltText="Filter NomChamps column" FilterControlWidth="95%" ShowFilterIcon="false"
                                        AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TypeQuestionSTR" UniqueName="TypeQuestion" HeaderText=" Type Question"
                                        FilterControlAltText="Recherche par  Type Question" FilterControlWidth="95%" ShowFilterIcon="false"
                                        AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CaratereAccepteSTR" UniqueName="CaratereAccepte" HeaderText=" Caratere Accepte"
                                        FilterControlAltText="Recherche par  Caratere Accepte" FilterControlWidth="95%" ShowFilterIcon="false"
                                        AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NbreValeurMinimal" UniqueName="NbreValeurMinimal" HeaderText=" Nbre Valeur Minimal"
                                        FilterControlAltText="Filter NbreValeurMinimal column" FilterControlWidth="95%" ShowFilterIcon="false"
                                        AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NbreCaratereMaximal" UniqueName="NbreCaratereMaximal" HeaderText=" Nbre Caratere Maximal"
                                        FilterControlAltText="Filter NbreCaratereMaximal column" FilterControlWidth="95%" ShowFilterIcon="false"
                                        AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn DataField="EstSautReponse" UniqueName="EstSautReponse" HeaderText=" Est Saut Reponse"
                                        FilterControlAltText="Filter EstSautReponse column" FilterControlWidth="95%" ShowFilterIcon="false"
                                        AllowFiltering="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                        <ItemTemplate>
                                            <asp:Image ID="EstSautReponse_Image" runat="server" ImageUrl='<%#Bind("EstSautReponse_Image") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="16px" />
                                        <ItemStyle HorizontalAlign="Center" Width="16px" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="QPrecedent" UniqueName="QPrecedent" HeaderText=" Q Precedent"
                                        FilterControlAltText="Filter QPrecedent column" FilterControlWidth="95%" ShowFilterIcon="false"
                                        AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="QSuivant" UniqueName="QSuivant" HeaderText=" Q Suivant"
                                        FilterControlAltText="Filter QSuivant column" FilterControlWidth="95%" ShowFilterIcon="false"
                                        AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
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
                                <ClientEvents OnRowContextMenu="RowContextMenu" OnRowDblClick="RowDblClick" />
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                            <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default" />
                            <PagerStyle PageSizeControlType="RadComboBox" />
                            <FilterMenu EnableImageSprites="False"></FilterMenu>
                        </telerik:RadGrid>
                    </div>
                </asp:Panel>
                <!-- FORM LOGIN -->
                <BRAIN:CULogin2 runat="server" ID="LoginWUC" Visible="false" />
                <div class="md-overlay"></div>
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
    <telerik:RadContextMenu ID="ContextMenu" runat="server" OnClientItemClicked="MenuItemClicked" EnableRoundedCorners="true" EnableShadows="true">
        <Items>
            <telerik:RadMenuItem Visible="true" Value="Editer" Text="Visualiser / Modifier" ImageUrl="~/images/_edit.png" HoveredImageUrl="~/images/_edit.png" />
            <%--<telerik:RadMenuItem Visible="true" Value="Delete" Text="Supprimer" ImageUrl="~/images/delete.png" HoveredImageUrl="~/images/delete.png" />--%>
        </Items>
    </telerik:RadContextMenu>
    <input id="txtWindowPage" type="hidden" />
    <style type="text/css">
        .StyleCbxLabel {
            margin-left: 10px;
            font-size: 10px;
            vertical-align: middle;
        }
    </style>
</asp:Content>

