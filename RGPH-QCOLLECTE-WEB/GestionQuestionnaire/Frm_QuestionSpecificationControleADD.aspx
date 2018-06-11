<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPages/DashboardCZMasterPage.master" AutoEventWireup="false" CodeFile="Frm_QuestionSpecificationControleADD.aspx.vb" Inherits="GestionQuestionnaire_Frm_QuestionSpecificationControleADD" %>

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

            function CloseAndRefreshListeQuestionSpecificationControle() {
                GetRadWindow().BrowserWindow.refreshListeSpecificationControle();
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
    <%--<telerik:RadScriptManager ID="RadScriptManager1"  runat="server"></telerik:RadScriptManager>--%>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="Btn_SaveInfo">
           <UpdatedControls>
               <telerik:AjaxUpdatedControl ControlID="Panel_Msg" LoadingPanelID="RadAjaxLoadingPanel1" />
               <%--<telerik:AjaxUpdatedControl ControlID="Panel_First" LoadingPanelID="RadAjaxLoadingPanel1" />--%>
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
                    <asp:Label ID="Label_Titre" runat="server" Text="QuestionSpecificationControle" />
                    <small id="OL_SeeAllData" runat="server">
                        <asp:Label ID="Label_SousTitre" runat="server" />
                    </small>
                </h3>
                <!--<ol class="breadcrumb"> 
    <li><a href="#"><i class="fa fa-dashboard"></i>Accueil</a></li>
    <li class="active">QuestionSpecificationControle</li>
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

                <asp:Panel ID="Panel_First" runat="server" CssClass="panel panel-default panel-body" Style="margin: 5px;">

                    <div id="divEntete" runat="server" visible="false" class="alert alert-info">
                        <div class="form-group">
                            <div class="col-sm-2">
                                <label>
                                    Code Question :
                                </label>
                                <asp:Label ID="LabelCodeQuestion" runat="server" Text="" />
                            </div>
                            <div class="col-sm-6">
                                <label>
                                    Libelle Question :
                                </label>
                                <asp:Label ID="LabelLibelleQuestion" runat="server" Text="" />
                            </div>
                            <div class="col-sm-2">
                                <label>Type Question:</label>
                                <asp:Label ID="LabelTypeQuestion" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-sm-2">
                                <label>Est Saut Reponse:</label>
                                <asp:Label ID="LabelEstSautReponse" runat="server" Text=""></asp:Label>
                            </div>
                            <div style="clear: both;"></div>
                        </div>
                    </div>
                    <div id="DIV_Panel" class="form-horizontal group-border-dashed1" style="border-radius: 0px;">
                        <div class="form-group">
                            <label class="col-sm-3 control-label">
                                Code Question
                               <asp:RequiredFieldValidator ID="RFV_CodeQuestion" runat="server" ControlToValidate="DDL_CodeQuestion"
                                   ErrorMessage="_ Code Question Obligatoire !" SetFocusOnError="true" Display="Dynamic" Text="*"
                                   ValidationGroup="GPSave" CssClass="text-danger" />
                                <asp:RequiredFieldValidator ID="RFV1_CodeQuestion" runat="server" ControlToValidate="DDL_CodeQuestion"
                                    ErrorMessage="_ Code Question Obligatoire !" InitialValue="0" SetFocusOnError="true" Display="Dynamic" Text="*"
                                    ValidationGroup="GPSave" CssClass="text-danger" />
                            </label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="DDL_CodeQuestion" CssClass="select2" Width="100%" runat="server">
                                </asp:DropDownList>
                            </div>

                        </div>
                        <div class="form-group">
                            <div class="col-sm-12">
                                <label>
                                    Instructions
                               <asp:RequiredFieldValidator ID="RFV_Instructions" runat="server" ControlToValidate="txt_Instructions"
                                   ErrorMessage="Instructions Obligatoire !" SetFocusOnError="true" Display="Dynamic" Text="*"
                                   ValidationGroup="GPSave" CssClass="text-danger" />
                                </label>
                                <asp:TextBox ID="txt_Instructions" TextMode="MultiLine" CssClass="form-control" Text="_" Width="98%" Height="150px" runat="server" placeholder="Instructions..."></asp:TextBox>
                                <ajaxToolkit:HtmlEditorExtender ID="HtmlEditorExtender2" runat="server" TargetControlID="txt_Instructions" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-12">
                                <label>
                                    Controles
                                </label>
                                <asp:TextBox ID="txt_Controles" TextMode="MultiLine" CssClass="form-control" Width="98%" Height="250px" runat="server" placeholder="Controles..."></asp:TextBox>
                                <ajaxToolkit:HtmlEditorExtender ID="HtmlEditorExtender1" runat="server" TargetControlID="txt_Controles" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-12">
                                <label>
                                    Messages
                                </label>
                                <asp:TextBox ID="txt_Messages" TextMode="MultiLine" CssClass="form-control" Width="98%" Height="150px" runat="server" placeholder="Messages..."></asp:TextBox>
                                <ajaxToolkit:HtmlEditorExtender ID="HtmlEditorExtender_Messages" runat="server" TargetControlID="txt_Messages" />
                            </div>
                        </div>
                        <div id="DIV_SaveInfo" runat="server" style="margin: 5px 0px; text-align: left;">
                            <label class="col-sm-2 control-label">
                            </label>
                            <div class="col-sm-10">
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
                </asp:Panel>
                <!-- FORM LOGIN -->
                <BRAIN:CULogin2 runat="server" ID="LoginWUC" Visible="false" />
                <div class="md-overlay"></div>
                <asp:TextBox ID="txt_CodeQuestionSpecificationControle_Hid" runat="server" Text="0" Visible="False" Width="1px"></asp:TextBox>
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
    <input id="txtWindowPage" type="hidden" />
</asp:Content>

