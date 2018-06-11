<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/DashboardCZMasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="GestionQuestionnaire_Default" %>

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
            function Open_Window(page, _width, _height) {
                var largeurEcran = (screen.width - _width) / 2;
                var hauteurEcran = (screen.height - _height) / 2;
                window.open(page, 'APIDashBord', ' width=' + _width + ', height=' + _height + ', top=' + hauteurEcran + ', left=' + largeurEcran + ', toolbar=0, location=0, directories=0, status=0, scrollbars=1, resizable=0, copyhistory=0, menuBar=0')
            }
            function OpenWindow(page) {
                window.open(page, '_self')
            }
            function OpenWindowBlank(page) {
                window.open(page, '_blank')
            }
        </script>
    </telerik:RadCodeBlock>
    <%--<telerik:RadScriptManager ID="RadScriptManager1"  runat="server"> </telerik:RadScriptManager>--%>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <%--<telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel_First" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            <%--<telerik:AjaxSetting AjaxControlID="rbtnClearFilters">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rdgQuestions_Reponses" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
    <input type='hidden' id='radGridClickedRowIndex' name='radGridClickedRowIndex' />
    <div class="container-fluid" id="pcont">
        <div class="mail-inbox">
            <section class="page-head" id="PageHeader" runat="server">
                <h3>
                    <i class="fa fa-dashboard"></i>
                    <asp:Label ID="Label_Titre" runat="server" Text=" Questions  Reponses" />
                    <small id="OL_SeeAllData" runat="server">
                        <asp:Label ID="Label_SousTitre" runat="server" />
                    </small>
                </h3>
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

                    <div class="stats_bar" id="Section_DIVSession" runat="server">
                        <div class="butpro butstyle" runat="server" id="DIV_Module_ModuleSession">
                            <div class="sub">
                                <h2>MODULE</h2>
                                <span>
                                    <asp:Literal ID="Literal_ModuleSession" runat="server" Text="Exporter" /></span>
                            </div>
                            <div class="stat">
                                <asp:Literal ID="Literal_ModuleSession2" runat="server" Text="Exporter" />
                            </div>
                        </div>
                        <div class="butpro butstyle" runat="server" id="DIV_Question_Module_Session">
                            <div class="sub">
                                <h2>QUESTION / MODULE</h2>
                                <span>
                                    <asp:Literal ID="Literal_Question_ModuleSession" runat="server" Text="Exporter" /></span>
                            </div>
                            <div class="stat">
                                <asp:Literal ID="Literal_Question_ModuleSession2" runat="server" Text="Exporter" />
                            </div>
                        </div>
                        <div class="butpro butstyle" runat="server" id="DIV_Module_QuestionSession">
                            <div class="sub stat-blue">
                                <h2>QUESTIONS</h2>
                                <span>
                                    <asp:Literal ID="Literal_QuestionSession" runat="server" Text="Exporter" /></span>
                            </div>
                            <div class="stat">
                                <asp:Literal ID="Literal_QuestionSession2" runat="server" Text="Exporter" />
                            </div>
                        </div>

                        <div class="butpro butstyle" runat="server" id="DIV_Module_ReponseSession">
                            <div class="sub">
                                <h2>RÉPONSES</h2>
                                <span>
                                    <asp:Literal ID="Literal_ReponseSession" runat="server" Text="Exporter" /></span>
                            </div>
                            <div class="stat">
                                <asp:Literal ID="Literal_ReponseSessionq" runat="server" Text="Exporter" />
                            </div>
                        </div>
                    </div>
                    <div class="stats_bar" id="Section0" runat="server">
                        <div class="butpro butstyle" runat="server" id="DIV_Module_JSON_DATA">
                            <div class="sub">
                                <span id="total_clientes">Module</span>
                            </div>
                            <div class="stat">
                                Export JSON data
                            </div>
                        </div>

                        <div class="butpro butstyle" runat="server" id="DIV_CategorieQuestion">
                            <div class="sub">
                                <span>Catégorie Question</span>
                            </div>
                            <div class="stat">
                                Export JSON data
                            </div>
                        </div>

                        <div class="butpro butstyle stat-blue" runat="server" id="DIV_Questions">
                            <div class="sub">
                                <span>Questions</span>
                            </div>
                            <div class="stat">
                                Export JSON data
                            </div>
                        </div>

                        <div class="butpro butstyle" runat="server" id="DIV_Reponses">
                            <div class="sub">
                                <span>Réponses</span>
                            </div>
                            <div class="stat">
                                Export JSON data
                            </div>
                        </div>

                        <div class="butpro butstyle" runat="server" id="DIV_FormulaireCollecte">
                            <div class="sub">
                                <span>Formulaire Collecte</span>
                            </div>
                            <div class="stat">
                                Export JSON data
                            </div>
                        </div>
                    </div>
                    <section class="page-head" id="Section1" runat="server">
                        <h3>
                            <i class="fa fa-dashboard"></i>
                            <asp:CheckBox runat="server" ID="CBX_Questions" Text="Questions" AutoPostBack="true" Checked="true" />
                            <asp:CheckBox runat="server" ID="CBX_Reponse" Text="Réponses" AutoPostBack="true" Checked="true" />
                            <asp:Label ID="Label1" runat="server" Text="  / Module" />
                            <small id="Small1" runat="server">
                                <asp:Label ID="Label2" runat="server" />
                            </small>
                        </h3>
                        <br />
                        <div class="col-sm-4 col-md-4" runat="server" id="DIVaa_TypeModule" visible="false">
                            <div class="block">
                                <div class="header no-border">
                                    <h2>Type Module</h2>
                                </div>
                                <div class="content">
                                    <asp:CheckBoxList ID="CheckBoxList_TypeModule" runat="server" />
                                </div>
                            </div>
                            <div class="clear"></div>
                        </div>
                        <div class="col-sm-8 col-md-8">
                            <asp:LinkButton ID="LinkButton_ExporterQuestionsEtReponses2" runat="server" CssClass="btn btn-success" ValidationGroup="GPSave">
                                <i class="fa fa-save"></i>
                                <asp:Literal ID="Literal_ExporterQuestionsEtReponses2" runat="server" Text="Exporter" />
                            </asp:LinkButton><telerik:RadGrid ID="RadGrid1" AllowPaging="false" AllowSorting="false" PageSize="50"
                                runat="server" AutoGenerateColumns="False" GridLines="None" AllowFilteringByColumn="false"
                                Culture="fr-FR" ShowGroupPanel="false"
                                EnableViewState="true" AllowMultiRowSelection="false" GroupingSettings-CaseSensitive="false">
                                <ExportSettings HideStructureColumns="true" />
                                <MasterTableView CommandItemDisplay="None" GridLines="None" DataKeyNames="ID" NoDetailRecordsText="Pas d'enregistrement"
                                    NoMasterRecordsText="Pas d'enregistrement">
                                    <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowExportToExcelButton="true"
                                        ExportToExcelText="Exporter en excel" />
                                    <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ID" UniqueName="ID" Display="false" />
                                        <telerik:GridTemplateColumn HeaderText="Type Module" UniqueName="Type_Module">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox_TypeModule" runat="server" Text='<%#Bind("TypeModuleSTR") %>' />
                                                <asp:TextBox ID="txt_IDTypeModule" runat="server" Text='<%#Bind("ID") %>' Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Formulaire(Module)" UniqueName="Module">
                                            <ItemTemplate>
                                                <asp:CheckBoxList ID="CheckBoxList_Module_Formulaire" runat="server" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>
                                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
                                </MasterTableView>
                                <GroupingSettings CaseSensitive="False" />
                                <ClientSettings AllowDragToGroup="false" AllowColumnsReorder="false">
                                    <%--<ClientEvents OnRowContextMenu="RowContextMenu" OnRowDblClick="RowDblClick" />--%>
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default" />
                                <PagerStyle PageSizeControlType="RadComboBox" />
                                <FilterMenu EnableImageSprites="False"></FilterMenu>
                            </telerik:RadGrid>
                            <div class="clear"></div>
                        </div>
                        <div class="clear"></div>
                        <br />
                        <asp:LinkButton ID="LinkButton_ExporterQuestionsEtReponses" runat="server" CssClass="btn btn-success" ValidationGroup="GPSave">
                            <i class="fa fa-save"></i>
                            <asp:Literal ID="Literal_ExporterQuestionsEtReponses" runat="server" Text="Exporter" />
                        </asp:LinkButton><asp:Label ID="label_Module" runat="server"></asp:Label>
                    </section>
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
            <telerik:RadMenuItem Visible="true" Value="Editer" Text="Editer" ImageUrl="~/images/_edit.png" HoveredImageUrl="~/images/_edit.png" />
            <telerik:RadMenuItem Visible="true" Value="Delete" Text="Supprimer" ImageUrl="~/images/delete.png" HoveredImageUrl="~/images/delete.png" />
        </Items>
    </telerik:RadContextMenu>
    <input id="txtWindowPage" type="hidden" />
    <style type="text/css">
        .colorprimary, .colorinfo, .colorwarning, .colorsuccess, .colordanger {
            border-radius: 3px;
            -webkit-border-radius: 3px;
            color: #FFF;
        }

        .colorprimary {
            background-color: #4e9dff;
            border-color: #357ebd;
        }

        .colorinfo {
            background-color: #5BC0DE;
            border-color: #28a1c4;
        }

        .colorwarning {
            border-color: #D68000;
            background: #FC9700;
        }

        .colorsuccess {
            border-color: #54A754;
            background: #60C060;
        }

        .colordanger {
            border-color: #ca452e;
            background: #da4932;
        }
    </style>
</asp:Content>

