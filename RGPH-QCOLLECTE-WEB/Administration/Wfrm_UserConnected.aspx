<%@ Page Title="Sécurité - Utilisateurs connectés" Language="VB" MasterPageFile="~/MasterPages/MasterPage_Security.master"
    AutoEventWireup="false" CodeFile="Wfrm_UserConnected.aspx.vb" Inherits="Parametres_Administration_Wfrm_UserConnected" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="Msg" Namespace="BunnyBear" Assembly="msgBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>
            <asp:Label ID="Label_Titre" runat="server" Text="UTILISATEURS CONNECTÉS " />
            <small> <asp:Label ID="LabelNumeroDeReference" runat="server" /></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i> Accueil</a></li>
            <li class="active">Utilisateurs connectés</li>
        </ol>
     </section>
    <section class="content" style="padding-top: 0px;">
    <div id="Div_CorpsFille">
        <%--<ajaxToolkit:ToolkitScriptManager runat="Server" AsyncPostBackTimeout="600" EnablePartialRendering="true"
            EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />--%>
        <asp:UpdatePanel ID="UpdatePanel_First" runat="server">
            <ContentTemplate>
                <Msg:msgBox ID="Dialogue" runat="server" />
                <asp:Panel runat="server" ID="Panel_Msg" Visible="false">
                     <div class="alert alert-warning">
                        <asp:Image ID="Image_Msg" runat="server" Style="vertical-align: middle;" />
                        <asp:Label ID="Label_Msg" runat="server" Text=""></asp:Label>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="Panel_First2">
                    <div style="display: inline; float: left; padding-right: 10px;">
                        <asp:UpdateProgress ID="updProgHeader" runat="server" AssociatedUpdatePanelID="UpdatePanel_First">
                            <ProgressTemplate>
                                <div>
                                    <img src="~/images/spinner.gif" alt="Waiting..." />
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                    <%--<h2 class="art-postheader" style="margin-bottom: 5px;">
                        <asp:Label ID="Label_Utilisateur" runat="server" Text=""></asp:Label>
                        </h2>--%>
                        <div id="Statistique" style="margin: 0px 5px;">
                            <asp:Label ID="Label5" runat="server" Text="Legendes:" Style="font-weight: 700; float: left;" />
                            <div style="border: 1px dotted #000000; margin: 1px 5px; float: left; padding-right: 5px;
                                padding-left: 5px;">
                                <asp:DropDownList ID="DDL_GroupUtilisateur" runat="server" AutoPostBack="true" CssClass="ComboField"
                                    Width="300px">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <br style="clear: both;" />
                        <div id="Div_First" style="text-align: right; padding: 1px 10px;">
                            Déconnecter tous les utilisateurs :
                            <asp:ImageButton ID="Btn_DisconnectAll_Utilisateur" runat="server" Style="vertical-align: middle;
                                width: 30px;" ImageUrl="~/images/Logout.png" /></div>
                        <!-- GRIDVIEW -->
                        <div id="Div_List">
                            <asp:Panel ID="Panel_List" runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridView_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                CellPadding="4" CssClass="GridViewCss" DataKeyNames="ID" ForeColor="#333333"
                                                GridLines="None" PageSize="25" AllowSorting="True" Width="100%">
                                                <RowStyle CssClass="RowStyle" BackColor="#F7F6F3" ForeColor="#333333" />
                                                <SelectedRowStyle CssClass="SelectedRowStyle" BackColor="#E2DED6" 
                                                    Font-Bold="True" ForeColor="#333333" />
                                                <AlternatingRowStyle CssClass="AlternatingRowStyle" BackColor="White" 
                                                    ForeColor="#284775" />
                                                <EditRowStyle BackColor="#999999" />
                                                <FooterStyle CssClass="FooterStyle" BackColor="#5D7B9D" Font-Bold="True" 
                                                    ForeColor="White" />
                                                <HeaderStyle CssClass="HeaderStyle" ForeColor="White" BackColor="#5D7B9D" 
                                                    Font-Bold="True" />
                                                <PagerStyle CssClass="PagerStyle" ForeColor="White" BackColor="#284775" 
                                                    HorizontalAlign="Center" />
                                                <PagerStyle CssClass="DataPager" />
                                                <PagerSettings FirstPageImageUrl="~/images/first.png" 
                                                    LastPageImageUrl="~/images/Last.png" Position="TopAndBottom" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbOrder" runat="server"></asp:Label><asp:Label ID="lbID" runat="server"
                                                                Text='<%# Bind("ID") %>' Visible="false"></asp:Label></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField ReadOnly="true" DataField="USERNAME" HeaderText="Compte utilisateur"
                                                        SortExpression="USERNAME" />
                                                    <asp:BoundField ReadOnly="true" DataField="NOMCOMPLET" HeaderText="Nom Complet" SortExpression="NOMCOMPLET" />
                                                    <asp:BoundField ReadOnly="true" DataField="GROUPE_STR" HeaderText="Groupe" SortExpression="GROUPE_STR" />
                                                    <asp:BoundField ReadOnly="true" DataField="LASTIP" HeaderText="Adresse IP" />
                                                    <asp:BoundField ReadOnly="true" DataField="LASTLOGIN" HeaderText="Connecté depuis"
                                                        SortExpression="LAST_LOGIN" />
                                                    <asp:TemplateField HeaderText="Deconnecter" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="Btn_Del" runat="server" Width="20" CommandName="edit" ImageUrl="~/images/Logout.png" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                            </asp:GridView>
                                            <asp:Label ID="Label_Result" runat="server" Text="Aucun Résultat..." Visible="false"
                                                EnableViewState="false" Style="font-weight: 700; font-size: large; color: #800000"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TextAlignRight">
                                            <asp:Panel ID="PagingPane" runat="server" Width="100%">
                                                <asp:DropDownList ID="DDL_PageSize" runat="server" AutoPostBack="True" CssClass="ComboField">
                                                    <asp:ListItem Value="10">10 lignes par page</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="20">20 lignes par page</asp:ListItem>
                                                    <asp:ListItem Value="40">40 lignes par page</asp:ListItem>
                                                    <asp:ListItem Value="60">60 lignes par page</asp:ListItem>
                                                    <asp:ListItem Value="100">100 lignes par page</asp:ListItem>
                                                    <asp:ListItem Value="30000">Toutes</asp:ListItem>
                                                </asp:DropDownList>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
     </section>
</asp:Content>
