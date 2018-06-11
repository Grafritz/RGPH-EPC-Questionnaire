<%@ Page Title="Rezo509 | Produits / SERVICES" Language="VB" MasterPageFile="~/MasterPages/MasterPage_Security.master" AutoEventWireup="false" CodeFile="Frm_SR_SERVICESADD.aspx.vb" Inherits="Frm_SR_SERVICESADD" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function closeWindow() {
                GetRadWindow().BrowserWindow.refreshMe();
                GetRadWindow().close();
            }
            function CloseAndRefreshListeSR_SERVICES() {
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
            <i class="fa fa-briefcase"></i> <asp:Label ID="LabelTitre" runat="server" Text=" SERVICES" />
            <small>
                <asp:Label ID="LabelSousTitre" runat="server" /></small>
        </h1>
        <%--<ol class="breadcrumb"> 
           <li><a href="#"><i class="fa fa-dashboard"></i>Accueil</a></li>
           <li class="active">SR_SERVICES</li>
           <asp:SiteMapPath ID="sitmape111" runat="server" ></asp:SiteMapPath>
       </ol>  --%>
    </section>
    <asp:Panel runat="server" ID="Panel_Msg" Visible="false">
        <div class="alert alert-warning">
            <asp:Image ID="Image_Msg" runat="server" />
            <asp:Label ID="Label_Msg" runat="server" Text=""></asp:Label>
        </div>
    </asp:Panel>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-warning" ShowMessageBox="true" ShowSummary="true" />

    <section class="content">
        <asp:Panel ID="Panel_First" runat="server" CssClass="panel panel-default panel-body">
            <table border="0" cellpadding="4" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 30%;" align="right">
                        <asp:Label ID="lbl_NomService" Text="Nom Service : " runat="server" />
                    </td>
                    <td style="width: 70%;" align="left">
                        <telerik:RadTextBox ID="rtxt_NomService" CssClass="form-control"  Width="80%" runat="server">
                        </telerik:RadTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_NomService" runat="server" ControlToValidate="rtxt_NomService"
                            ErrorMessage="Le Nom du Service est obligatoire !" SetFocusOnError="true" Display="Dynamic" Text="*" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%;" align="right">
                        <asp:Label ID="lbl_DescriptionService" Text="Description Service : " runat="server" />
                    </td>
                    <td style="width: 70%;" align="left">
                        <telerik:RadTextBox ID="rtxt_DescriptionService" CssClass="form-control" Width="80%" runat="server">
                        </telerik:RadTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_DescriptionService" runat="server" ControlToValidate="rtxt_DescriptionService"
                            ErrorMessage="Le Description du Service est obligatoire !" SetFocusOnError="true" Display="Dynamic" Text="*" />
                    </td>
                </tr>
                <tr>
                    <td class="TextAlignRight">
                        <asp:Label ID="Label3" runat="server" Text="Type d'Objet :" Style="font-weight: 700"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DDL_TypeObjet" runat="server" CssClass="ComboField" Width="200px">
                            <asp:ListItem Value="Action" Selected="True">ACTION/BOUTON/LIEN</asp:ListItem>
                            <asp:ListItem Value="Page">PAGE</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td class="TextAlignRight" colspan="2">
                        <hr style="margin: 2px;" />
                    </td>
                </tr>

                <tr>
                    <td class="TextAlignRight">
                        <asp:Label ID="Label2" runat="server" Text="Module :" Style="font-weight: 700"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DDLModule" runat="server" CssClass="ComboField" Width="333px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="DDLModule"
                            Text="*" ErrorMessage="Module obligatoire" ValidationGroup="GPSave" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%;" align="right"></td>
                    <td style="width: 70%;" align="left">
                        <telerik:RadButton ID="btnSave" runat="server" Text="Sauvegarder">
                            <Icon PrimaryIconCssClass="rbSave" PrimaryIconLeft="4" PrimaryIconTop="4"></Icon>
                        </telerik:RadButton>
                        &nbsp;
       <telerik:RadButton ID="btnCancel" runat="server" CausesValidation="False" Text="Annuler">
           <Icon PrimaryIconCssClass="rbCancel" PrimaryIconLeft="4" PrimaryIconTop="4"></Icon>
       </telerik:RadButton>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <input id="txtWindowPage" type="hidden" />
        <asp:TextBox ID="txt_CodeSR_SERVICES_Hid" runat="server" Text="0" Visible="False" Width="1px"></asp:TextBox>
    </section>
</asp:Content>

