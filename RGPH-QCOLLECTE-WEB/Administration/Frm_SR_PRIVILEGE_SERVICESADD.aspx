<%@ Page Title="Produits / Services par Utilisateur" Language="VB" MasterPageFile="~/MasterPages/MasterPage_Security.master" AutoEventWireup="false"
    CodeFile="Frm_SR_PRIVILEGE_SERVICESADD.aspx.vb" Inherits="Frm_SR_PRIVILEGE_SERVICESADD" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function closeWindow() {
                GetRadWindow().BrowserWindow.refreshMe();
                GetRadWindow().close();
            }
            function CloseAndRefreshListeSR_PRIVILEGE_SERVICES() {
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
    <%--<telerik:RadScriptManager ID="RadScriptManager1"  runat="server"></telerik:RadScriptManager>--%>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSave">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel_Msg" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="Panel_First" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
    <section class="content-header">
        <h1>
            <i class="fa fa-briefcase"></i>
            <asp:Label ID="LabelTitre" runat="server" Text="Produits / Services par Utilisateur" />
            <small>
                <asp:Label ID="LabelSousTitre" runat="server" /></small>
        </h1>
        <%--<ol class="breadcrumb"> 
           <li><a href="#"><i class="fa fa-dashboard"></i>Accueil</a></li>
           <li class="active">SR_PRIVILEGE_SERVICES</li>
           <asp:SiteMapPath ID="sitmape111" runat="server" ></asp:SiteMapPath>
       </ol>  --%>
    </section>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-warning" ShowMessageBox="true" ShowSummary="true" />

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
        <asp:Panel ID="Panel_First" runat="server" CssClass="panel panel-default panel-body">
            <div class="box-body">
                <div class="form-group">
                    <div class="col-sm-5">
                        <label>
                            Compte Utilisateur
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_User" runat="server" ControlToValidate="rcmb_User"
                            ErrorMessage="Le User est obligatoire !" SetFocusOnError="true" Display="Dynamic" Text="*" />
                        </label>
                        <telerik:RadComboBox ID="rcmb_User" Width="100%" runat="server">
                        </telerik:RadComboBox>
                    </div>
                    <div class="col-sm-5">
                        <label>
                            Produits/Services :
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_Services" runat="server" ControlToValidate="rcmb_Services"
                            ErrorMessage="Le Services est obligatoire !" SetFocusOnError="true" Display="Dynamic" Text="*" />
                        </label>
                        <telerik:RadComboBox ID="rcmb_Services" Width="100%" runat="server">
                        </telerik:RadComboBox>
                    </div>
                    <div class="col-sm-2">
                        <asp:CheckBox ID="chk_ActifYN" runat="server" CssClass="text-green" Text="Actif(Y/N)" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-3">
                        <label>
                            Date Ajout :
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_DateAjout" runat="server" ControlToValidate="rdp_DateAjout"
                            ErrorMessage="La  Date Ajout est obligatoire !" SetFocusOnError="true" Display="Dynamic" Text="*" />
                        </label>
                        <telerik:RadDatePicker ID="rdp_DateAjout" Width="100%" runat="server" DateInput-DateFormat="dd/MM/yyyy"
                            MinDate="1900-01-01" ToolTip="Cliquer sur le bouton pour choisir une date">
                        </telerik:RadDatePicker>
                    </div>
                    <div class="col-sm-3">
                        <label>
                            Date Limite : 
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_DateLimite" runat="server" ControlToValidate="rdp_DateLimite"
                            ErrorMessage="La  Date Limite est obligatoire !" SetFocusOnError="true" Display="Dynamic" Text="*" />
                        </label>
                        <telerik:RadDatePicker ID="rdp_DateLimite" Width="100%" runat="server" DateInput-DateFormat="dd/MM/yyyy"
                            MinDate="1900-01-01" ToolTip="Cliquer sur le bouton pour choisir une date">
                        </telerik:RadDatePicker>
                    </div>
                </div>
                <div style="clear: both;"></div>
            </div>

            <div style="clear: both;"></div>
            <div class="box-footer">
                <br />
                <br />
                <asp:LinkButton ID="btnSave" runat="server" CssClass="btn btn-primary">
                    <i class="fa fa-save" ></i> Enregistrer
                </asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="btnCancel" CausesValidation="false" runat="server"
                    CssClass="btn btn-primary">
                    <i class="fa  fa-reply-all" ></i> Annuler
                </asp:LinkButton>
            </div>
        </asp:Panel>
        <input id="txtWindowPage" type="hidden" />
        <asp:TextBox ID="txt_CodeSR_PRIVILEGE_SERVICES_Hid" runat="server" Text="0" Visible="False" Width="1px"></asp:TextBox>
    </section>
</asp:Content>

