Imports Microsoft
Imports System.Data
Imports System.Collections.Generic
Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer
Imports Telerik.Web.UI
Imports RGPH_QCOLLECTE_Library

Partial Class Frm_SR_PRIVILEGE_SERVICESListing
    Inherits Cls_BasePage ' LA CLASSE DE LA PAGE HERITE DE CETTE CLASSE DANS LE CAS OU NOUS AVONS UNE APPLICATION WEB multilingue


#Region "ATTRIBUTS"
    Private _message As String  ' VARIABLE SERVANT A LA RECUPERATION DE TOUS LES MESSAGES D'ECHECS OU DE SUCCES

    REM DEFINITION ET INITIALISATION DES CONSTANTE POUR LA SECURITE
    Private Const Nom_page As String = "PAGE-LISTING-PRODUITS-SERVICES-REZO509"  ' POUR LA PAGE
    Private Const Btn_Save As String = "Bouton-SAVE-PRODUITS-SERVICES-REZO509"       ' POUR LE BOUTON D'ENREGISTREMENT
    Private Const Btn_Edit As String = "Bouton-EDIT-PRODUITS-SERVICES-REZO509"       ' POUR LE BOUTON DE MODIFICATION
    Private Const Btn_Delete As String = "Bouton-DELETE-PRODUITS-SERVICES-REZO509"   ' POUR LE BOUTON DE SUPPRESSION

    Dim User_Connected As Cls_User          ' INSTANCE DE LA CLASSE UTILISATEUR - UTILISER POUR L'UTILISATEUR EN SESSION 
    Dim Is_Acces_Page As Boolean = True     ' LA VARIABLE SERVANT DE TEST POUR DONNEER L'ACCES A LA PAGE
    Dim GetOut As Boolean = False           ' LA VARIABLE SERVANT DE TEST POUR REDIRIGER L'UTILISATEUR VERS LA PAGE DE CONNEXION
    Private IS_SendMail As Boolean = True   ' PAS TROP IMPORTANT...
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Expires = -1
        Panel_Msg.Visible = False

        SYSTEME_SECURITE()  ' APPEL A LA METHODE SERVANT A TESTER LES COMPOSANTS DE LA PAGE Y COMPRIS LA PAGE ELLE MEME 

        '--- Si l'utilisateur n'a Access a la page les informations ne sont pas charger dans la Page_Load 
        If Is_Acces_Page Then
            If Not IsPostBack Then
                rbtnAddSR_PRIVILEGE_SERVICES.Attributes.Add("onclick", "javascript:void(ShowAddUpdateForm('Frm_SR_PRIVILEGE_SERVICESADD.aspx', 750, 400)); return false;")
                'BtnADDNew.Attributes.Add("onclick", "javascript:Open_Window('Frm_SR_PRIVILEGE_SERVICESADD.aspx', '_self',500,400); return false;") 
                BindGrid()
            End If
        End If
    End Sub

#Region "SECURITE"
    Public Sub SYSTEME_SECURITE()

        User_Connected = [Global].KeepUserContinuesToWork(User_Connected)

        CType(Page.Master.FindControl("DashMenu_Securite").FindControl("LI_PARAMETRES"), HtmlControl).Attributes.Add("class", "active treeview")
        CType(Page.Master.FindControl("DashMenu_Securite").FindControl("LI_SR_PRIVILEGE_SERVICESListing"), HtmlControl).Attributes.Add("class", "active")

        If Session([Global].GLOBAL_SESSION) IsNot Nothing Then
            User_Connected = CType(Session([Global].GLOBAL_SESSION), Cls_User)
            If Not Cls_Privilege.VerifyRightOnObject(Nom_page, User_Connected.IdGroupeuser) Then    ' VERIFICATION SI L'UTILISATEUR N'A PAS ACCES A LA PAGE
                _message = [Global].NO_ACCES_PAGE
                MessageToShow(_message)
                Is_Acces_Page = False

                Panel_First.Visible = False
            Else    ' SI L'UTILISATEUR A ACCES A LA PAGE ON VERIFIE POUR LES BOUTONS ET LES LIENS
                '---  Okey vous avez acces a la page ---'
                rbtnAddSR_PRIVILEGE_SERVICES.Visible = Cls_Privilege.VerifyRightOnObject(Btn_Save, User_Connected.IdGroupeuser)
            End If
        End If

        If Session([Global].GLOBAL_SESSION) Is Nothing Then
            '-- Session expirée --'
            GetOut = True
        Else
            Try
                User_Connected = CType(Session([Global].GLOBAL_SESSION), Cls_User)  ' ON VERIFIE SI LÚTILISATEUR A ETE FORCE DE SE CONNECTER 'PAR L'ADM
                If Not (GlobalFunctions.IsUserStillConnected(User_Connected) And GlobalFunctions.IsUserStillActive(User_Connected)) Then
                    User_Connected.Set_Status_ConnectedUser(False)
                    User_Connected.Activite_Utilisateur_InRezo("Forced Log Off", "Forced to Log Off", Request.UserHostAddress)

                    GetOut = True
                    Session.RemoveAll()
                    _message = "Session expirée."
                    MessageToShow(_message)
                    Is_Acces_Page = True
                End If
            Catch ex As Rezo509Exception
                GetOut = True
                _message = "Session expirée."
                MessageToShow(_message)
            End Try
        End If

        If GetOut Then ' REDIRECTIONNEMENT DE L'UTILISATUER OU PAS.
            Response.Redirect([Global].PAGE_LOGIN)
        End If
    End Sub
#End Region

#Region "Other Method"
    Private Sub MessageToShow(ByVal _message As String, Optional ByVal E_or_S As String = "E")
        Panel_Msg.Visible = True
        GlobalFunctions.Message_Image(Image_Msg, E_or_S)
        Label_Msg.Text = _message
        RadAjaxManager1.ResponseScripts.Add("alert('" & _message.Replace("'", "\'") & "');")
        If E_or_S = "S" Then
            Label_Msg.ForeColor = Drawing.Color.Green
        Else
            Label_Msg.ForeColor = Drawing.Color.Red
        End If
    End Sub
#End Region

#Region "Load DATA"
    Private Sub BindGrid(Optional ByVal _refresh As Boolean = True)
        Dim objs As List(Of Cls_SR_PRIVILEGE_SERVICES)
        Dim _ret As Long = 0
        Try
            objs = Cls_SR_PRIVILEGE_SERVICES.SearchAll
            rdgSR_PRIVILEGE_SERVICES.DataSource = objs
            If _refresh Then
                rdgSR_PRIVILEGE_SERVICES.DataBind()
            End If
            _ret = objs.Count
            LabelTitre.Text = "Produits / Services par Utilisateur <small class=""badge  bg-blue"">" & _ret & "</small>"
        Catch ex As Rezo509Exception
            _message = ex.Message
            MessageToShow(_message)
            _message = " Methode -> [ BindGrid ] :" & ex.Message
            [Global].WriteError(_message, User_Connected)
        End Try
    End Sub
#End Region

#Region "RADGRID EVENTS"
    Protected Sub rdgSR_PRIVILEGE_SERVICES_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rdgSR_PRIVILEGE_SERVICES.ItemCommand
        Try
            If e.CommandName = Telerik.Web.UI.RadGrid.ExportToExcelCommandName Then
                rdgSR_PRIVILEGE_SERVICES.ExportSettings.ExportOnlyData = True
                rdgSR_PRIVILEGE_SERVICES.GridLines = GridLines.Both
                rdgSR_PRIVILEGE_SERVICES.ExportSettings.IgnorePaging = True
                rdgSR_PRIVILEGE_SERVICES.ExportSettings.OpenInNewWindow = False
                rdgSR_PRIVILEGE_SERVICES.ExportSettings.FileName = "Liste des SR_PRIVILEGE_SERVICES"
                rdgSR_PRIVILEGE_SERVICES.MasterTableView.Columns(0).Visible = False
                rdgSR_PRIVILEGE_SERVICES.MasterTableView.ExportToExcel()
            End If

            Select Case e.CommandName
                Case "delete"
                    Dim obj As New Cls_SR_PRIVILEGE_SERVICES(TypeSafeConversion.NullSafeLong(e.CommandArgument))
                    obj.Delete()
                    User_Connected.Activite_Utilisateur_InRezo("DELETE Produits / Services par Utilisateur ", obj.ID & " - Nomcomplet_et_Username:" & obj.Nomcomplet_et_Username & " - Nom Service:" & obj.NomService & " Prop:", Request.UserHostAddress)
                    MessageToShow("Produits / Services supprimé", "S")
                    rdgSR_PRIVILEGE_SERVICES.Rebind()
            End Select
        Catch ex As Rezo509Exception
            _message = ex.Message
            MessageToShow(_message)
            _message = " Methode -> [ rdgSR_PRIVILEGE_SERVICES_ItemCommand ] :" & ex.Message
            [Global].WriteError(_message, User_Connected)
        End Try
    End Sub

    Protected Sub rdgSR_PRIVILEGE_SERVICES_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rdgSR_PRIVILEGE_SERVICES.ItemDataBound
        Try
            Dim gridDataItem = TryCast(e.Item, GridDataItem)
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                'Dim _lnk As HyperLink = DirectCast(gridDataItem.FindControl("hlk"), HyperLink)
                'Dim _lbl_ID As Label = DirectCast(gridDataItem.FindControl("lbl_ID"), Label)
                '_lnk.Attributes.Clear()
                '_lnk.Attributes.Add("onclick", "javascript:void(ShowAddUpdateForm('Frm_SR_PRIVILEGE_SERVICESADD.aspx?ID=" & CLng(_lbl_ID.Text) & "', 750, 400));")
            End If

            If (gridDataItem IsNot Nothing) Then
                Dim item As GridDataItem = gridDataItem
                CType(item.FindControl("lbOrder"), Label).Text = rdgSR_PRIVILEGE_SERVICES.PageSize * rdgSR_PRIVILEGE_SERVICES.CurrentPageIndex + (item.RowIndex / 2)

                Dim imagedelete As ImageButton = CType(item("delete").Controls(0), ImageButton)
                Dim imageediter As ImageButton = CType(item("editer").Controls(0), ImageButton)
                imagedelete.ToolTip = "Effacer ce Produits / Services"
                imageediter.ToolTip = "Editer ce Produits / Services"
                imagedelete.CommandArgument = CType(DataBinder.Eval(e.Item.DataItem, "ID"), String)
                imageediter.Attributes.Add("onclick", "javascript:void(ShowAddUpdateForm('Frm_SR_PRIVILEGE_SERVICESADD.aspx?ID=" & CType(DataBinder.Eval(e.Item.DataItem, "ID"), Long) & "',800,550));")
                REM Privilege
                imageediter.Visible = Cls_Privilege.VerifyRightOnObject(Btn_Save, User_Connected.IdGroupeuser)
                imagedelete.Visible = Cls_Privilege.VerifyRightOnObject(Btn_Delete, User_Connected.IdGroupeuser)
            End If
        Catch ex As Rezo509Exception
            _message = ex.Message
            MessageToShow(_message)
            _message = " Methode -> [ rdgSR_PRIVILEGE_SERVICES_ItemDataBound ] :" & ex.Message
            [Global].WriteError(_message, User_Connected)
        End Try
    End Sub

    Protected Sub rdgSR_PRIVILEGE_SERVICES_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rdgSR_PRIVILEGE_SERVICES.NeedDataSource
        If IsPostBack Then
            BindGrid(False)
        End If
    End Sub

    Protected Sub RadAjaxManager1_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
        Try
            Select Case e.Argument
                Case "Reload"
                    BindGrid(True)
            End Select
        Catch ex As Rezo509Exception
            _message = ex.Message
            MessageToShow(_message)
            _message = " Methode -> [ RadAjaxManager1_AjaxRequest ] :" & ex.Message
            [Global].WriteError(_message, User_Connected)
        End Try
    End Sub

#End Region

End Class


