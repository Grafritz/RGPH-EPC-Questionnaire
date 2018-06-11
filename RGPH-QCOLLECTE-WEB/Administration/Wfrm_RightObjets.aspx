<%@ Page Title=".:: Sécurité - Droits des Tâches ::." Language="VB" MasterPageFile="~/MasterPages/MasterPage_Security.master"
    AutoEventWireup="false" CodeFile="Wfrm_RightObjets.aspx.vb" Inherits="Parametres_Administration_Wfrm_RightObjets" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="Msg" Namespace="BunnyBear" Assembly="msgBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>
            <asp:Label ID="Label_Utilisateur" runat="server" Text="DROITS DES TACHES UTILISATEURS " />
            <small> <asp:Label ID="LabelNumeroDeReference" runat="server" /></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i> Accueil</a></li>
            <li class="active">Droits des Tâches d'Utilisateur</li>
        </ol>
     </section>
    <section class="content" style="">
    <div id="Div_CorpsFille">
        <%--<ajaxToolkit:ToolkitScriptManager runat="Server" AsyncPostBackTimeout="600" EnablePartialRendering="true"
            EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />--%>
        <Msg:msgBox ID="Dialogue" runat="server" />
        <asp:UpdatePanel ID="UpdatePanel_First" runat="server">
            <ContentTemplate>
                <%--<h2 class="art-postheader" style="margin-bottom: 5px;">
                    <asp:Label ID="Label_Object" runat="server" Text="Droits des Tâches"></asp:Label></h2>--%>
                <div style="display: inline; float: left; padding-right: 10px;">
                    <asp:UpdateProgress ID="updProgHeader" runat="server" AssociatedUpdatePanelID="UpdatePanel_First">
                        <ProgressTemplate>
                            <div style="cursor: wait; position: absolute; margin-left: -210px; margin-top: -80px;
                                width: 970px; height: 700px; background-color: transparent; padding: 20px;">
                                <div style="margin-left: auto; margin-right: auto; width: 110px; height: 110px;">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/ajax-loader.gif" AlternateText="Waiting..."
                                        Style="height: 100px; width: 101px" />
                                </div>
                            </div>
                            <div>
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/spinner.gif" AlternateText="Waiting..." />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </div>
                <asp:Panel runat="server" ID="Panel_Msg" Visible="false">
                    <div class="alert alert-warning">
                        <asp:Image ID="Image_Msg" runat="server" />
                        <asp:Label ID="Label_Msg" runat="server" Text=""></asp:Label>
                    </div>
                </asp:Panel>
                <div id="Div_First">
                    <asp:Panel ID="Panel_First" runat="server">
                        <table style="width: 100%; height: 100%;">
                            <tr valign="top" style="height: 30px;">
                                <td valign="middle" align="right" style="width: 45%;" >
                                    <asp:Label ID="Label1" runat="server" Text="Tâches"></asp:Label>
                                    <asp:DropDownList ID="DDL_ListeALLTache" runat="server" CssClass="ComboField" Width="95%"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td valign="middle" align="center" style="width: 10%;">
                                    &nbsp;
                                </td>
                                <td valign="middle" align="left" style="width: 45%;">
                                       
                                </td>
                            </tr>
                            <tr valign="top" style="height: 30px;">
                                <td valign="middle" align="right" style="width: 45%;">                            
                                    <asp:Label ID="Label2" runat="server" Text="Modules"></asp:Label>
                                    <asp:DropDownList ID="DDLModule" runat="server" CssClass="ComboField" Width="95%"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td valign="middle" align="center" style="width: 10%;">
                                    &nbsp;
                                </td>
                                <td valign="middle" align="left" style="width: 45%;">  
                                </td>
                            </tr>
                            <tr valign="top" style="height: 30px;">
                                <td valign="middle" align="right" style="width: 45%;">
                                    <asp:Label ID="Label3" runat="server" Text="Type d'Objets"></asp:Label>
                                    <asp:DropDownList ID="DLL_ListeObjectType" runat="server" CssClass="ComboField" Width="95%"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td valign="middle" align="center" style="width: 10%;">
                                    &nbsp;
                                </td>
                                <td valign="middle" align="left" style="width: 45%;">  
                                </td>
                            </tr>
                            <tr valign="top" style="height: 100%;">
                                <td valign="top" align="right" style="width: 45%;">
                                    <asp:Label ID="Label4" runat="server" Text="Objet(s) Disponibles par Type d'Objet"></asp:Label>
                                    <asp:ListBox ID="LstAvailObjects" TabIndex="2" runat="server" CssClass="TextField"
                                        BackColor="#FFFFFF" ForeColor="Black" Height="300px" BorderStyle="solid" Width="95%"
                                        BorderWidth="1" SelectionMode="Multiple"></asp:ListBox>
                                </td>
                                <td valign="middle" align="center" style="width: 10%;">
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr valign="middle">
                                            <td valign="middle" align="center">
                                                <asp:Button ID="btnSendOne" runat="server" CssClass="ButtonStyle" Width="25px" Text=">">
                                                </asp:Button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="middle" align="center">
                                                <asp:Button ID="btnSendAll" runat="server" CssClass="ButtonStyle" Width="25px" Text=">>">
                                                </asp:Button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="middle" align="center">
                                                <asp:Button ID="btnRemoveAll" runat="server" CssClass="ButtonStyle" Width="25px"
                                                    Text="<<"></asp:Button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="middle" align="center">
                                                <asp:Button ID="btnRemoveOne" runat="server" CssClass="ButtonStyle" Width="25px"
                                                    Text="<"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="top" align="left" style="width: 45%;">
                                    <asp:Label ID="Label5" runat="server" Text="Objet(s) Assigné(s) à la Tâche"></asp:Label>
                                    <asp:ListBox ID="lstAffectObjects" TabIndex="3" runat="server" CssClass="TextField"
                                        BackColor="#FFFFFF" ForeColor="Black" Height="300px" BorderStyle="solid" Width="95%"
                                        BorderWidth="1" SelectionMode="Multiple"></asp:ListBox>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
     </section>
</asp:Content>
