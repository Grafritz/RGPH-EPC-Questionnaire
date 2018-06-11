<%@ Page Title=" Sécurité - activiés Utilisateur" Language="VB" MasterPageFile="~/MasterPages/MasterPage_Security.master"
    AutoEventWireup="false" CodeFile="Wfrm_ActiviteUtilisateur.aspx.vb" Inherits="Parametres_Administration_Wfrm_ActiviteUtilisateur" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <input type="hidden" id="radGridClickedRowIndex" name="radGridClickedRowIndex" />
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PanelContenuBig" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <%--<telerik:AjaxSetting AjaxControlID="DDL_Vacation">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadTreeViewClasse" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            <telerik:AjaxSetting AjaxControlID="RadGrid_1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel_Msg" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="Panel_First2" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <%--<telerik:AjaxSetting AjaxControlID="Btn_Search">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel_Msg" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="Panel_First2" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            <%--<telerik:AjaxSetting AjaxControlID="LinkButton_Search">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PanelContenuBig" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
    <section class="content-header">
        <h1>
            <asp:Label ID="Label_Utilisateur" runat="server" Text="ACTIVITES UTILISATEURS " />
            <small>
                <asp:Label ID="LabelNumeroDeReference" runat="server" /></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Accueil</a></li>
            <li class="active">ACTIVITES UTILISATEURS</li>
        </ol>
    </section>
    <section class="content" style="">
        <div id="Div_CorpsFille">
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
                <div id="Statistique" style="margin: 0px;">
                    <div class="input-group input-group-lgsss">
                        <span class="input-group-addon has-error"><b><i class="fa fa-filter"></i></b></span>
                        <asp:DropDownList ID="DDL_Utilisateur" CssClass="form-control has-error ComboField" runat="server" Width="100%" Style="text-transform: none"></asp:DropDownList>
                        <%--<asp:TextBox runat="server" ID="txtSerach" CssClass="form-control has-error" Style="width: 300px;"
                                    placeholder="Recherche Institution par mot-clés"></asp:TextBox>--%>
                        <span class="input-group-addon has-error">
                            <b class="fa fa-folder-o"></b>
                        </span>
                        <asp:DropDownList ID="DDL_ActiviteUser" CssClass="form-control has-error ComboField" runat="server" Width="100%" Style="text-transform: none"></asp:DropDownList>
                        <span class="input-group-addon has-error">
                            <b class="fa fa-calendar"></b>
                        </span>
                        <telerik:RadDatePicker ID="txtDateDebut" Width="100%" runat="server" DateInput-DateFormat="dd/MM/yyyy"
                            MinDate="1900-01-01" Skin="MetroTouch">
                            <Calendar runat="server" Skin="MetroTouch" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                            </Calendar>
                            <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelWidth="40%">
                            </DateInput>
                        </telerik:RadDatePicker>
                        <span class="input-group-addon has-error">
                            <b class="fa fa-calendar"></b>
                        </span>
                        <telerik:RadDatePicker ID="txtDateFin" Width="100%" runat="server" DateInput-DateFormat="dd/MM/yyyy"
                            MinDate="1900-01-01" Skin="MetroTouch">
                            <Calendar runat="server" Skin="Metro" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                            </Calendar>
                            <DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelWidth="40%">
                            </DateInput>
                        </telerik:RadDatePicker>
                        <span class="input-group-btn">
                            <asp:LinkButton ID="Btn_Search" runat="server" CssClass="btn btn-primary btn-flat">
                            <i class="fa fa-search"></i>
                            </asp:LinkButton>
                        </span>
                    </div>
                </div>
                <br style="clear: both;" />
                <!-- GRIDVIEW -->
                <div id="Div_List">
                    <asp:Panel ID="Panel_List" runat="server">

                        <asp:Label ID="Label_DetailSearch" runat="server" Text="ACTIVITES UTILISATEURS" Style="font-weight: 700; color: #800000"></asp:Label>

                        <asp:Label ID="Label_Result" runat="server" Text="Aucun Résultat..." Visible="false"
                            EnableViewState="false" Style="font-weight: 700; font-size: large; color: #800000"></asp:Label>
                        <telerik:RadGrid ID="RadGrid_1" AllowPaging="True" AllowSorting="True" PageSize="20"
                            runat="server" AutoGenerateColumns="False" GridLines="None" CellSpacing="0" AllowFilteringByColumn="true"
                            EnableViewState="true" AllowMultiRowSelection="false"
                            GroupingSettings-CaseSensitive="false">
                            <MasterTableView GridLines="None" CommandItemDisplay="Top" DataKeyNames="ID" NoDetailRecordsText="Pas d'enregistrement"
                                NoMasterRecordsText="Pas d'enregistrement">
                                <CommandItemSettings ShowAddNewRecordButton="false" AddNewRecordText="Nouvelle Inscription"
                                    AddNewRecordImageUrl="~/Images/_inscription.gif" ShowExportToExcelButton="true" ShowExportToCsvButton="true" ShowExportToWordButton="true" ShowExportToPdfButton="true"
                                    ShowRefreshButton="false" />
                                <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ID" UniqueName="ID" Display="false" />
                                    <telerik:GridTemplateColumn Visible="true" ShowFilterIcon="false" AllowFiltering="false" HeaderText="#" UniqueName="Compteur"
                                        HeaderStyle-Width="17px"
                                        HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label Visible="true" ID="lbOrder" runat="server" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn HeaderText="" UniqueName="Actions" ShowFilterIcon="false" AllowFiltering="false">
                                        <ItemTemplate>
                                            <div class="btn-group">
                                                <button type="button" class="btn btn-sm btn-primary dropdown-toggle" data-toggle="dropdown">Actions</button>
                                                <button type="button" class="btn btn-sm btn-primary dropdown-toggle" data-toggle="dropdown">
                                                    <span class="caret"></span>
                                                    <span class="sr-only">Toggle Dropdown</span></button><ul class="dropdown-menu" role="menu">
                                                        <li data-placement="bottom" data-toggle="tooltip" data-original-title="Supprimer Activité">
                                                            <asp:LinkButton ID="LinkButton_Delete" ToolTip="Supprimer Activité" runat="server"
                                                                CommandName="delete">  
                                                                    <i class="fa fa-trash-o"></i>
                                                                    Supprimer
                                                            </asp:LinkButton>
                                                        </li>
                                                    </ul>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="110px" />
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>


                                    <telerik:GridTemplateColumn DataField="USER_NAME" UniqueName="USER_NAME" SortExpression="USER_NAME"
                                        HeaderText="Compte utilisateur"
                                        FilterControlAltText="Filter Compte utilisateur" FilterControlWidth="99%" ShowFilterIcon="false"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                        <FilterTemplate>
                                            <telerik:RadComboBox ID="rcmb_USER_NAME" runat="server" Culture="fr-FR" AppendDataBoundItems="true"
                                                DataSourceID="SQL_DS_USER_NAME" DataTextField="USER_NAME" DataValueField="USER_NAME"
                                                SelectedValue='<%# CType(Container, GridItem).OwnerTableView.GetColumn("USER_NAME").CurrentFilterValue%>'
                                                OnClientSelectedIndexChanged="BrandName_USER_NAME_ComboIndexChanged">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="-Tous les Utilisateurs-" />
                                                </Items>
                                            </telerik:RadComboBox>
                                            <telerik:RadScriptBlock ID="RadScriptBlockUSER_NAME" runat="server">
                                                <script type="text/javascript">
                                                    function BrandName_USER_NAME_ComboIndexChanged(sender, args) {
                                                        var tableView = $find("<%# CType(Container, GridItem).OwnerTableView.ClientID %>");
                                                        tableView.filter("USER_NAME", args.get_item().get_value(), "EqualTo");
                                                    }
                                                </script>
                                            </telerik:RadScriptBlock>
                                        </FilterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LabelUSER_NAME" runat="server" Text='<%# Bind("USER_NAME")%>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn DataField="TYPE_ACTIVITE" UniqueName="TYPE_ACTIVITE" SortExpression="TYPE_ACTIVITE"
                                        HeaderText="Activités"
                                        FilterControlAltText="Filter Activités" FilterControlWidth="99%" ShowFilterIcon="false"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                        <FilterTemplate>
                                            <telerik:RadComboBox ID="rcmb_TYPE_ACTIVITE" Width="100%" runat="server" Culture="fr-FR" AppendDataBoundItems="true"
                                                DataSourceID="SQL_DS_TYPE_ACTIVITE" DataTextField="TYPE_ACTIVITE" DataValueField="TYPE_ACTIVITE"
                                                SelectedValue='<%# CType(Container, GridItem).OwnerTableView.GetColumn("TYPE_ACTIVITE").CurrentFilterValue%>'
                                                OnClientSelectedIndexChanged="BrandName_TYPE_ACTIVITE_ComboIndexChanged">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="-Toutes les Activités-" />
                                                </Items>
                                            </telerik:RadComboBox>
                                            <telerik:RadScriptBlock ID="RadScriptBlockTYPE_ACTIVITE" runat="server">
                                                <script type="text/javascript">
                                                    function BrandName_TYPE_ACTIVITE_ComboIndexChanged(sender, args) {
                                                        var tableView = $find("<%# CType(Container, GridItem).OwnerTableView.ClientID %>");
                                                        tableView.filter("TYPE_ACTIVITE", args.get_item().get_value(), "EqualTo");
                                                    }
                                                </script>
                                            </telerik:RadScriptBlock>
                                        </FilterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label_TYPE_ACTIVITE" runat="server" Text='<%# Bind("TYPE_ACTIVITE")%>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn DataField="DESCRIPTION_ACTIVITE_Search" UniqueName="DESCRIPTION_ACTIVITE" SortExpression="DESCRIPTION_ACTIVITE"
                                        HeaderText="Descriptions Activité"
                                        FilterControlAltText="Filter Descriptions Activité" FilterControlWidth="99%" ShowFilterIcon="false"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_DESCRIPTION_ACTIVITE" runat="server" Text='<%# Bind("DESCRIPTION_ACTIVITE")%>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn DataField="IP_USER" UniqueName="IP_USER" SortExpression="IP_USER"
                                        HeaderText="Activités"
                                        FilterControlAltText="Filter Activités" FilterControlWidth="99%" ShowFilterIcon="false"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                        <FilterTemplate>
                                            <telerik:RadComboBox ID="rcmb_IP_USER" Width="100%" runat="server" Culture="fr-FR" AppendDataBoundItems="true"
                                                DataSourceID="SQL_DS_IP_USER" DataTextField="IP_USER" DataValueField="IP_USER"
                                                SelectedValue='<%# CType(Container, GridItem).OwnerTableView.GetColumn("IP_USER").CurrentFilterValue%>'
                                                OnClientSelectedIndexChanged="BrandName_IP_USER_ComboIndexChanged">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="-Toutes les Activités-" />
                                                </Items>
                                            </telerik:RadComboBox>
                                            <telerik:RadScriptBlock ID="RadScriptBlockIP_USER" runat="server">
                                                <script type="text/javascript">
                                                    function BrandName_IP_USER_ComboIndexChanged(sender, args) {
                                                        var tableView = $find("<%# CType(Container, GridItem).OwnerTableView.ClientID %>");
                                                        tableView.filter("IP_USER", args.get_item().get_value(), "EqualTo");
                                                    }
                                                </script>
                                            </telerik:RadScriptBlock>
                                        </FilterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label_IP_USER" runat="server" Text='<%# Bind("IP_USER")%>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridDateTimeColumn DataField="DATE_ACTIVITE" HeaderText="Postuler le" FilterControlWidth="110px"
                                        SortExpression="DATE_ACTIVITE" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                                        DataFormatString="{0:dd MMM yyyy}" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                        <HeaderStyle HorizontalAlign="Center" Width="16px" />
                                        <ItemStyle HorizontalAlign="Center" Width="16px" />
                                    </telerik:GridDateTimeColumn>

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
                                <%--<ClientEvents OnRowContextMenu="RowContextMenu" OnRowDblClick="RowDblClick"></ClientEvents>--%>
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                            <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
                            </HeaderContextMenu>
                            <FilterMenu EnableImageSprites="False">
                            </FilterMenu>
                        </telerik:RadGrid>
                        <div style="text-align: right;">
                            <asp:LinkButton ID="LinkButton_DeleteALL" CssClass="btn btn-danger" ToolTip="SUPPRIMER TOUTES LES ACTIVITES" runat="server">  
                                <i class="fa fa-trash-o"></i>
                                SUPPRIMER TOUTES LES ACTIVITES
                            </asp:LinkButton>
                        </div>
                        <asp:SqlDataSource ID="SQL_DS_USER_NAME" runat="server"
                            ConnectionString="<%$ ConnectionStrings:ConnectionStringDataBase %>"
                            SelectCommand="SELECT DISTINCT [USER_ID] AS [USER_NAME] FROM dbo.TBL_SR_USER_ACTIVITE ORDER BY [User_ID] ASC"></asp:SqlDataSource>
                        <asp:SqlDataSource ID="SQL_DS_TYPE_ACTIVITE" runat="server"
                            ConnectionString="<%$ ConnectionStrings:ConnectionStringDataBase %>"
                            SelectCommand="SELECT DISTINCT Activity AS TYPE_ACTIVITE FROM dbo.TBL_SR_USER_ACTIVITE ORDER BY Activity ASC"></asp:SqlDataSource>
                        <asp:SqlDataSource ID="SQL_DS_IP_USER" runat="server"
                            ConnectionString="<%$ ConnectionStrings:ConnectionStringDataBase %>"
                            SelectCommand="SELECT DISTINCT IPUser AS IP_USER FROM dbo.TBL_SR_USER_ACTIVITE ORDER BY IPUser ASC"></asp:SqlDataSource>
                    </asp:Panel>
                </div>
            </asp:Panel>
        </div>
    </section>
</asp:Content>
