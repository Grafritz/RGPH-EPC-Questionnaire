<%@ Page Title=".:: Sécurité - Tâches Utilisateur ::." Language="VB" MasterPageFile="~/MasterPages/MasterPage_Security.master"
    AutoEventWireup="false" CodeFile="Wfrm_Taches_User.aspx.vb" Inherits="Parametres_Administration_Wfrm_Droit_User" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="Msg" Namespace="BunnyBear" Assembly="msgBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>
            <asp:Label ID="Label_Utilisateur" runat="server" Text="TACHES UTILISATEURS " />
            <small> <asp:Label ID="LabelNumeroDeReference" runat="server" /></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i> Accueil</a></li>
            <li class="active">Tâches Utilisateurs</li>
        </ol>
     </section>
    <section class="content" style="">
    <div id="Div_CorpsFille" >
        <Msg:msgBox ID="Dialogue" runat="server" />
        <%--<ajaxToolkit:ToolkitScriptManager runat="Server" AsyncPostBackTimeout="600" EnablePartialRendering="true"
            EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />--%>
        <asp:UpdatePanel ID="UpdatePanel_First" runat="server">
            <ContentTemplate>
                <%--<h2 class="art-postheader" style="margin-bottom: 5px;">
                    <asp:Label ID="Label_Object" runat="server" Text="Tâches Utilisateur"></asp:Label></h2>--%>
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
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/spinner.gif" AlternateText="Waiting..." />
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
                        <table style="width: 100%;">
                            <%--<tr>
                                    <td colspan="2"> <asp:Label ID="Label_Msg" runat="server" Text=""></asp:Label></td>
                                </tr>--%>
                            <tr>
                                <td class="TextAlignRight">
                                    <asp:Label ID="Label1" runat="server" Text="Nom Tâche :"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_ID" runat="server" Visible="false" Width="0px" />
                                    <asp:TextBox ID="txt_Description_Taches" runat="server" CssClass="TextField" Width="333px" />
                                    <asp:RequiredFieldValidator runat="server" ID="NREG_DDL_txt_Description_Taches" ControlToValidate="txt_Description_Taches"
                                        Text="*" ErrorMessage="Description Tache obligatoire" ValidationGroup="GPSave" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="Btn_Save" runat="server" ValidationGroup="GPSave" Text="Enregistrer"
                                        CssClass="ButtonStyle" />
                                    &nbsp;
                                    <asp:Button ID="Btn_Annuler" runat="server" Text="Annuler" CssClass="ButtonStyle"
                                        CausesValidation="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="GPSave" />
                    </asp:Panel>
                </div>
                <div id="Div_List">
                    <asp:Panel ID="Panel_List" runat="server">
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <asp:Button ID="Btn_Nouveau" runat="server" Text="Nouvel ajout" CssClass="ButtonStyle"
                                        CausesValidation="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView_List" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        CellPadding="4" CssClass="GridViewCss" DataKeyNames="ID" ForeColor="#333333"
                                        GridLines="None" PageSize="20" AllowSorting="True" Width="100%">
                                        <RowStyle CssClass="RowStyle" BackColor="#F7F6F3" ForeColor="#333333" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" BackColor="#E2DED6" Font-Bold="True"
                                            ForeColor="#333333" />
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle" BackColor="White" ForeColor="#284775" />
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle CssClass="FooterStyle" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle CssClass="HeaderStyle" ForeColor="White" BackColor="#5D7B9D" Font-Bold="True" />
                                        <PagerStyle CssClass="PagerStyle" ForeColor="White" BackColor="#284775" HorizontalAlign="Center" />
                                        <PagerStyle CssClass="DataPager" />
                                        <PagerSettings FirstPageImageUrl="~/Images/first.png" LastPageImageUrl="~/Images/Last.png"
                                            Position="TopAndBottom" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="No" HeaderStyle-Width="20">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbOrder" runat="server"></asp:Label>
                                                    <asp:Label ID="lbID" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="20px" />
                                            </asp:TemplateField>
                                            <asp:BoundField ReadOnly="true"  DataField="DESCRIPTION_TACHE" HeaderText="Description Tâches" SortExpression="DESCRIPTION_TACHE" />
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Btn_Edit" runat="server" CommandName="Edit" ImageUrl="~/Images/_edit.png" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="16"  />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Del" ItemStyle-HorizontalAlign="Center"  HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Btn_Del" runat="server" CommandName="Delete" ImageUrl="~/Images/delete.png" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="16"  />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td class="TextAlignRight">
                                    <asp:Panel ID="PagingPane" runat="server" Width="100%">
                                        Nbre de lignes par page : &nbsp;
                                        <asp:DropDownList ID="DDL_PageSize" runat="server" AutoPostBack="True" CssClass="ComboField">
                                            <asp:ListItem Selected="True" Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="40">40</asp:ListItem>
                                            <asp:ListItem Value="60">60</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="30000">Toutes</asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                        <div>
                            <table style="width: 100%;">
                                <tr>
                                    <td class="TextAlignRight" valign="middle">
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
     </section>
</asp:Content>
