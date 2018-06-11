<%@ Page Title=".:: Rezo509 | Log ::." Language="VB" MasterPageFile="~/MasterPages/MasterPage_Security.master" AutoEventWireup="false" CodeFile="Log.aspx.vb" Inherits="Administration_Log" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel_Msg" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="PanelConfiguration" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGrid_1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel_Msg" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="PanelConfiguration" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
    <asp:Panel runat="server" ID="PanelConfiguration">
        <section class="content-header">
            <h1>
                <i class="fa fa-bookmark"></i>
                <asp:Label ID="Label_Titre" runat="server" Text="LOG " />
                <small>
                    <asp:Label ID="Label_SousTitre" runat="server" /></small>
            </h1>
            <%--<ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Accueil</a></li>
            <li class="active">Utilisateur</li>
        </ol>--%>
        </section>

        <section class="content" style="padding-top: 0px;">
            <Msg:msgBox ID="Dialogue" runat="server" />
            <asp:Panel runat="server" ID="Panel_Msg" Visible="false">
                <div id="DIV_Msg" runat="server" class="alert alert-warning alert-dismissable">
                    <i id="Icon_Msg" runat="server" class="fa fa-warning"></i>
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                    <asp:Image ID="Image_Msg" runat="server" />
                    <asp:Label ID="Label_Msg" runat="server" Text=""></asp:Label>
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel_First" runat="server">
                <asp:Label ID="LabelSelectIcon" runat="server" Visible="false" Text="Sélectionnez le log : " />
                <telerik:RadListBox ID="RadListBoxLogs" Visible="false" runat="server" AutoPostBack="true" Height="100px" Width="100%" Culture="fr-FR" SelectionMode="Single">
                    <ItemTemplate>
                        <asp:Label ID="Label_Log" runat="server" Text='<%# Container.DataItem%>'></asp:Label>
                    </ItemTemplate>
                </telerik:RadListBox>
                <asp:TextBox ID="txt_ContenuLog" runat="server" Visible="false" TextMode="MultiLine" Height="250px" Width="100%"></asp:TextBox>
                <asp:Label ID="Label_ContenuLog" runat="server"></asp:Label>

                <telerik:RadGrid ID="RadGrid_1" AllowPaging="True" AllowSorting="True" PageSize="20"
                    runat="server" AutoGenerateColumns="False" GridLines="None" CellSpacing="0" AllowFilteringByColumn="true"
                    EnableViewState="true" AllowMultiRowSelection="false"
                    GroupingSettings-CaseSensitive="false">
                    <MasterTableView GridLines="None" CommandItemDisplay="None" NoDetailRecordsText="Pas d'enregistrement"
                        NoMasterRecordsText="Pas d'enregistrement">
                        <CommandItemSettings ShowExportToExcelButton="False" AddNewRecordText="Ajouter nouveau" AddNewRecordImageUrl="~/images/save_add.png"
                            ShowAddNewRecordButton="true" ShowRefreshButton="true" />
                        <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                        <Columns>
                            <telerik:GridTemplateColumn Visible="true" ShowFilterIcon="false" AllowFiltering="false" HeaderText="#" UniqueName="Compteur"
                                HeaderStyle-Width="17px"
                                HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label Visible="true" ID="lbOrder" runat="server" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <%--<telerik:GridBoundColumn HeaderText="Code" DataField="key" UniqueName="ID" Display="false" AllowFiltering="false" />--%>
                            <%--<telerik:GridDateTimeColumn DataField="CreationTimeFile" HeaderText="CreationTimeFile" FilterControlWidth="110px"
                                    SortExpression="CreationTimeFile" PickerType="DateTimePicker" EnableTimeIndependentFiltering="true"
                                    DataFormatString="{0:dd/MM/yyyy}" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridDateTimeColumn>--%>
                            <telerik:GridBoundColumn DataField="CreationTimeFile" HeaderText="CreationTimeFile" SortExpression="CreationTimeFile" UniqueName="hCreationTimeFile"
                                FilterControlAltText="Filter CreationTimeFile column" HeaderStyle-Wrap="false" FilterControlWidth="95%"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" />
                            <telerik:GridBoundColumn DataField="NameFile" HeaderText="NameFile" SortExpression="NameFile" UniqueName="NameFile"
                                FilterControlAltText="Filter NameFile column" HeaderStyle-Wrap="false" FilterControlWidth="95%"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" />
                            <telerik:GridBoundColumn DataField="SizeFile" HeaderText="SizeFile" DataFormatString="{0,-12:N0}" SortExpression="SizeFile" UniqueName="SizeFile"
                                FilterControlAltText="Filter SizeFile column" HeaderStyle-Wrap="false" FilterControlWidth="95%"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" />

                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="editer" DataTextField="ID"
                                HeaderStyle-Width="17px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                ImageUrl="~/images/_edit.png" UniqueName="editer">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridButtonColumn>
                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="delete" UniqueName="delete" DataTextField="ID"
                                HeaderStyle-Width="17px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                ImageUrl="~/images/delete.png" ConfirmDialogType="RadWindow"
                                ConfirmText="Êtes-vous sûr de vouloir supprimer?" ConfirmTitle="Attention!">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridButtonColumn>
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
                        <ClientEvents></ClientEvents>
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                </telerik:RadGrid>
            </asp:Panel>
        </section>
    </asp:Panel>
</asp:Content>

