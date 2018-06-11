<%@ Page Title="Produits et Services par Utilisateur" Language="VB" MasterPageFile="~/MasterPages/MasterPage_Security.master" AutoEventWireup="false" 
    MaintainScrollPositionOnPostback="true" CodeFile="Frm_SR_PRIVILEGE_SERVICESListing.aspx.vb" Inherits="Frm_SR_PRIVILEGE_SERVICESListing" %>

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

            function refreshMe() {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Reload");
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
            <telerik:AjaxSetting AjaxControlID="rdgSR_PRIVILEGE_SERVICES">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel_Msg" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="rdgSR_PRIVILEGE_SERVICES" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rbtnClearFilters">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rdgSR_PRIVILEGE_SERVICES" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
    <section class="content-header">
        <h1>
           <i class="fa fa-briefcase" ></i> <asp:Label ID="LabelTitre" runat="server" Text="Produits et Services par Utilisateur" />
            <small>
                <asp:Label ID="LabelSousTitre" runat="server" Text="" /></small>
        </h1>
        <%--<ol class="breadcrumb"> 
           <li><a href="#"><i class="fa fa-dashboard"></i>Accueil</a></li>
           <li class="active">SR_PRIVILEGE_SERVICES</li>
           <asp:SiteMapPath ID="sitmape111" runat="server" ></asp:SiteMapPath> 
       </ol> --%>
    </section>
    <asp:Panel runat="server" ID="Panel_Msg" Visible="false">
        <div class="alert alert-warning">
            <asp:Image ID="Image_Msg" runat="server" />
            <asp:Label ID="Label_Msg" runat="server" Text=""></asp:Label>
        </div>
    </asp:Panel>

    <section class="content">
        <asp:Panel ID="Panel_First" runat="server" CssClass="panel panel-default panel-body">
            <asp:LinkButton ID="rbtnAddSR_PRIVILEGE_SERVICES" runat="server" CssClass="btn btn-primary" Style="margin-bottom: 5px;">
                <i class="fa fa-plus-square-o"></i> Ajouter un Privilège </asp:LinkButton>
            <telerik:RadGrid ID="rdgSR_PRIVILEGE_SERVICES" AllowPaging="True" AllowSorting="True" PageSize="20"
                runat="server" AutoGenerateColumns="False" GridLines="None" AllowFilteringByColumn="true"
                EnableViewState="true" AllowMultiRowSelection="false" GroupingSettings-CaseSensitive="false">
                <ExportSettings HideStructureColumns="true" />
                <MasterTableView CommandItemDisplay="None" GridLines="None" DataKeyNames="ID" NoDetailRecordsText="Pas d'enregistrement"
                    NoMasterRecordsText="Pas d'enregistrement">
                    <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowExportToExcelButton="true"
                        ExportToExcelText="Exporter en excel" />
                    <Columns>
                        <telerik:GridBoundColumn DataField="ID" UniqueName="ID" Display="false">
                            <ItemStyle Width="1px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="#" UniqueName="Compteur" AllowFiltering="false">
                            <ItemTemplate>
                                <asp:Label Visible="true" ID="lbOrder" runat="server" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="16px" />
                            <ItemStyle HorizontalAlign="Center" Width="16px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn DataField="Nomcomplet_et_Username" UniqueName="ID_User" HeaderText=" Utilisateur"
                            FilterControlAltText="Filter Prénom, Nom, Compte Utilisateur column" FilterControlWidth="95%" ShowFilterIcon="false"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            <ItemTemplate>
                                <asp:Label ID="LabelNomcomplet_et_Username" runat="server" Text='<%# Bind("Nomcomplet_et_Username") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="DescriptionService" UniqueName="NomService" HeaderText=" Produits/Services"
                            FilterControlAltText="Filter Produits/Services column" FilterControlWidth="95%" ShowFilterIcon="false"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DateAjout" UniqueName="DateAjout" HeaderText=" Date Ajout"
                            FilterControlAltText="Filter DateAjout column" FilterControlWidth="95%" ShowFilterIcon="false"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle HorizontalAlign="Center" Width="16px" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" Width="16px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DateLimite" UniqueName="DateLimite" HeaderText=" Date Limite"
                            FilterControlAltText="Filter DateLimite column" FilterControlWidth="95%" ShowFilterIcon="false"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle HorizontalAlign="Center" Width="16px" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" Width="16px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ActifYN" UniqueName="ActifYN" HeaderText=" Actif Y N"
                            FilterControlAltText="Filter ActifYN column" FilterControlWidth="95%" ShowFilterIcon="false"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            <HeaderStyle HorizontalAlign="Center" Width="16px" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" Width="16px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandArgument="ID" CommandName="editer" UniqueName="editer"
                            DataTextField="ID" ImageUrl="~/images/_edit.png"
                            HeaderText="">
                            <HeaderStyle HorizontalAlign="Center" Width="16px" />
                            <ItemStyle HorizontalAlign="Center" Width="16px" />
                        </telerik:GridButtonColumn>
                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="delete" DataTextField="ID"
                            ImageUrl="~/images/delete.png"
                            UniqueName="delete" HeaderText="" ConfirmDialogType="RadWindow" ConfirmText="Voulez-vous vraiment supprimer ce Produit et SERVICES?"
                            ConfirmTitle="Attention!">
                            <HeaderStyle HorizontalAlign="Center" Width="16px" />
                            <ItemStyle HorizontalAlign="Center" Width="16px" />
                        </telerik:GridButtonColumn>
                    </Columns>
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                    </ExpandCollapseColumn>
                </MasterTableView>
                <ClientSettings>
                    <ClientEvents></ClientEvents>
                    <Selecting AllowRowSelect="true" />
                </ClientSettings>
                <HeaderContextMenu />
                <FilterMenu EnableImageSprites="False">
                </FilterMenu>
            </telerik:RadGrid>
            <telerik:RadWindowManager ID="RadWindowManager1" runat="server" VisibleStatusbar="false"
                EnableViewState="false">
                <Windows>
                    <telerik:RadWindow ID="AddUpdateDialog" runat="server" Title="" Left="75px" ReloadOnShow="true"
                        ShowContentDuringLoad="false" Modal="true" OnClientClose="RadWindowClosing" Behaviors="Reload, Pin, Minimize, Move, Resize, Maximize, Close"
                        EnableShadow="true" OnClientResizeEnd="RadWindowClientResizeEnd" />
                </Windows>
            </telerik:RadWindowManager>
            <telerik:RadContextMenu ID="ContextMenu" runat="server" OnClientItemClicked=""
                EnableRoundedCorners="true" EnableShadows="true">
                <Items>
                    <telerik:RadMenuItem Text="Editer" />
                </Items>
            </telerik:RadContextMenu>
        </asp:Panel>
        <input id="txtWindowPage" type="hidden" />
    </section>
</asp:Content>

