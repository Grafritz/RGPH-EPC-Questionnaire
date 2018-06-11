' @Brain Development
' Saturday 21-09-2013

Imports RGPH_QCOLLECTE_Library
Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Partial Class Parametres_Administration_Wfrm_UserConnected
    Inherits Cls_BasePage

#Region "ATTRIBUTS"
    Dim _message As String = ""
    'Private Const Nom_page As String = "PAGE-UTILISATEURS-CONNECTES"

    Private Const Nom_page As String = "Wfrm_UserConnected.aspx"
    Private Const Btn_SaveEditUser As String = "Btn_SaveEditUser"
    Private Const Btn_DeleteUser As String = "Btn_DeleteUser"
    Private Const Btn_ResetPassWordUser As String = "Btn_ResetPassWordUser"

    Dim User_Connected As Cls_User
    Dim Is_Acces_Page As Boolean = True
    Dim GetOut As Boolean = False
    Dim IS_Search As Boolean = False
    Dim IS_Trouve As Boolean = False
    Private IDGroupe As Long = 0
#End Region

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Expires = -1
        Panel_Msg.Visible = False

        SYSTEME_SECURITE()

        If Request.QueryString("IDGroupe") IsNot Nothing AndAlso IsNumeric(Request.QueryString("IDGroupe")) Then
            IDGroupe = Request.QueryString("IDGroupe")
        Else
            IDGroupe = 0
        End If

        '--- Si l'utilisateur a Acces a la page 
        If Is_Acces_Page Then
            If Not IsPostBack Then
                Me.ViewState("sortfield") = "ID"
                Me.ViewState("sortdirection") = "DESC"
                Load_Group_Utilisateur()
                If Request.QueryString("IDGroupe") IsNot Nothing AndAlso IsNumeric(Request.QueryString("IDGroupe")) Then
                    DDL_GroupUtilisateur.SelectedValue = TypeSafeConversion.NullSafeLong(Request.QueryString("IDGroupe"))
                End If
                LoadData_GridView_List()
                Count_DATAUser()
                NewToDo()
            End If
        End If
    End Sub

#Region "SECURITE"
    Public Sub SYSTEME_SECURITE()

        User_Connected = [Global].KeepUserContinuesToWork(User_Connected)

        CType(Page.Master.FindControl("DashMenu_Securite").FindControl("liPANEL_GESTION_SECURITE"), HtmlControl).Attributes.Add("class", "active treeview")
        CType(Page.Master.FindControl("DashMenu_Securite").FindControl("liPAGE_UTILISATEURS_CONNECTES"), HtmlControl).Attributes.Add("class", "active")

        If Session([Global].GLOBAL_SESSION) IsNot Nothing Then
            User_Connected = CType(Session([Global].GLOBAL_SESSION), Cls_User)
            If Not Cls_Privilege.VerifyRightOnObject(Nom_page, User_Connected.IdGroupeuser) Then
                _message = [Global].NO_ACCES_PAGE
                MessageToShow(_message)
                Is_Acces_Page = False

                Panel_First2.Visible = False
                'UpdatePanel_First.Visible = False
                'Panel_List.Visible = False
                '--- Sorry vous n'avez pas Acces a la page 
                'RemoveHandler Btn_Save.Click, AddressOf Btn_Save_Click
                ''RemoveHandler Btn_Nouveau.Click, AddressOf Btn_Nouveau_Click
                'RemoveHandler LinkButton_AddNew_Annonce.Click, AddressOf Btn_Nouveau_Click
                'RemoveHandler Btn_Annuler.Click, AddressOf Btn_Annuler_Click
                'RemoveHandler Btn_Reset_MotdePasse.Click, AddressOf Btn_Reset_MotdePasse_Click

                'RemoveHandler GridView_List.RowEditing, AddressOf GridView_List_RowEditing
                'RemoveHandler GridView_List.RowDeleting, AddressOf GridView_List_RowDeleting
                'RemoveHandler GridView_List.RowDataBound, AddressOf GridView_List_RowDataBound
                RemoveHandler GridView_List.PageIndexChanging, AddressOf GridView_List_PageIndexChanging

                RemoveHandler DDL_PageSize.SelectedIndexChanged, AddressOf DDL_PageSize_SelectedIndexChanged

            Else
                '---  Okey vous avez acces a la page ---'
                'Btn_Nouveau.Visible = Cls_Privilege.VerifyRightOnObject(Btn_SaveEditUser, User_Conneted.IdGroupeuser)
                'LinkButton_AddNew_Annonce.Visible = Cls_Privilege.VerifyRightOnObject(Btn_SaveEditUser, User_Conneted.IdGroupeuser)
                'Btn_Save.Visible = Cls_Privilege.VerifyRightOnObject(Btn_SaveEditUser, User_Conneted.IdGroupeuser)
                'Btn_Annuler.Visible = Cls_Privilege.VerifyRightOnObject(Btn_SaveEditUser, User_Conneted.IdGroupeuser)
                'Btn_Reset_MotdePasse.Visible = Cls_Privilege.VerifyRightOnObject(Btn_ResetPassWordUser, User_Conneted.IdGroupeuser)

                'GridView_List.Columns(GridView_List.Columns.Count - 1).Visible = Cls_Privilege.VerifyRightOnObject(Btn_DeleteUser, User_Conneted.IdGroupeuser)
                'GridView_List.Columns(GridView_List.Columns.Count - 2).Visible = Cls_Privilege.VerifyRightOnObject(Btn_SaveEditUser, User_Conneted.IdGroupeuser)
            End If
        End If

        If Session([Global].GLOBAL_SESSION) Is Nothing Then
            '-- Session expirée --'
            GetOut = True
            'Session([Global].GLOBAL_PAGENAME) = "~/Parametres/Administration/" & Nom_page
        Else
            Try
                User_Connected = CType(Session([Global].GLOBAL_SESSION), Cls_User)
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

        If GetOut Then
            Response.Redirect([Global].PAGE_LOGIN)
        End If
    End Sub
#End Region

#Region "Other Method - SAVE"

    Private Sub MessageToShow(ByVal _message As String, Optional ByVal E_or_S As String = "E")
        Panel_Msg.Visible = True
        GlobalFunctions.Message_Image(Image_Msg, E_or_S)
        Label_Msg.Text = _message
        Dialogue.alert(_message)
        If E_or_S = "S" Then
            Label_Msg.ForeColor = Drawing.Color.Green
        Else
            Label_Msg.ForeColor = Drawing.Color.Red
        End If
    End Sub

    Public Sub NewToDo()
        'Panel_First.Visible = False
        'Panel_List.Visible = True
        'txt_ID.Text = "0"
        'txt_UserName.Text = ""
        'Btn_Reset_MotdePasse.Visible = False
    End Sub

    Public Sub EnableControls()
        'If Cls_Privilege.VerifyRightOnObject(Btn_DeleteUser, User_Conneted.IdGroupeuser) Then
        '    GridView_List.Columns(GridView_List.Columns.Count - 1).Visible = True
        'End If
        'If Cls_Privilege.VerifyRightOnObject(Btn_SaveEditUser, User_Conneted.IdGroupeuser) Then
        '    GridView_List.Columns(GridView_List.Columns.Count - 2).Visible = True
        '    'Btn_Nouveau.Visible = True
        '    LinkButton_AddNew_Annonce.Visible = True
        'End If
    End Sub

    Public Sub DisableControls()
        'GridView_List.Columns(GridView_List.Columns.Count - 1).Visible = False
        'GridView_List.Columns(GridView_List.Columns.Count - 2).Visible = False
        ''Btn_Nouveau.Visible = False
        'LinkButton_AddNew_Annonce.Visible = False
    End Sub

    Public Sub Clear_Field()

    End Sub

#End Region

#Region "Load DATA"
    Private Sub LoadData_GridView_List()
        Dim objs As New List(Of Cls_User)
        Try
            objs = Cls_User.SearchAll_UsersConnected(IDGroupe)
            BrainBaseArrayList.ApplySortSTR(objs, Me.ViewState("sortfield"), Me.ViewState("sortdirection"))
            GridView_List.DataSource = objs
            GridView_List.DataBind()
            If objs.Count = 0 Then
                Label_Result.Visible = True
                PagingPane.Visible = False
            Else
                Label_Result.Visible = False
                PagingPane.Visible = True
            End If
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
            ErreurLog.WriteError("[ " & User_Connected.USERNAME & " ] Methode -> [ LoadData_GridView_List Utilisateur ] " & ex.Message)
        End Try
    End Sub

    Private Sub Count_DATAUser()
        'Label_Utilisateur.Text = "UTILISATEURS CONNECTÉS ( TOTAL:" & Cls_User.Count_AllUser_Connected_ByGroupe(0) & " )"
        'Label_CompteAdmin.Text = "Compte Administrateur: " & Cls_User.Count_AllUser_Connected_ByGroupe(1)
        'Label_CompteEntreprise.Text = "Entrepreneur: " & Cls_User.Count_AllUser_Connected_ByGroupe(2)
        'LabelcompteOperateur.Text = "Investisseur: " & Cls_User.Count_AllUser_Connected_ByGroupe(3)
        'Label_CompteAbonne.Text = "Entrepreneur & Investisseur: " & Cls_User.Count_AllUser_Connected_ByGroupe(4)
    End Sub

    Private Sub Load_Group_Utilisateur()
        Dim obj As New List(Of Cls_GroupeUser)
        Try
            obj = Cls_GroupeUser.SearchAll
            DDL_GroupUtilisateur.Items.Clear()
            DDL_GroupUtilisateur.DataTextField = "GROUPE_DESCRIPTION"
            DDL_GroupUtilisateur.DataValueField = "ID"
            DDL_GroupUtilisateur.DataSource = obj
            DDL_GroupUtilisateur.DataBind()

        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
            ErreurLog.WriteError("[ " & User_Connected.USERNAME & " ] Methode -> [ Load_Group_Utilisateur Utilisateur ] " & ex.Message)
        End Try
    End Sub
#End Region

#Region "EVENTS BUTTON"
    Protected Sub Btn_DisconnectAll_Utilisateur_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles Btn_DisconnectAll_Utilisateur.Click
        Try
            Cls_User.DisconnectAll()
            LoadData_GridView_List()
            Count_DATAUser()
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
            ErreurLog.WriteError("[ " & User_Connected.USERNAME & " ] Methode -> [ Btn_DisconnectAll_Utilisateur_Click Utilisateur ] " & ex.Message)
        End Try
    End Sub
#End Region

#Region "GridView Events"

    Protected Sub GridView_List_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView_List.PageIndexChanging
        Try
            GridView_List.PageIndex = e.NewPageIndex
            LoadData_GridView_List()
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
            ErreurLog.WriteError("[ " & User_Connected.USERNAME & " ] Methode -> [ GridView_List_PageIndexChanging Utilisateur ] " & ex.Message)
        End Try
    End Sub

    Protected Sub GridView_List_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView_List.RowCommand
        Try
            Select Case e.CommandName
                Case "RESET_PASSWORD"
                    Dim obj As New Cls_User(CLng(e.CommandArgument))
                    'Reset_Password(obj.ID)
                Case "DISCONNECT_USER"
                    'Dim obj As New Cls_User(CLng(e.CommandArgument))
                    'obj.Set_Status_IsForcedOut(True)
                    'obj.Activite_Utilisateur_InRezo("Force Log Off", "Force " & obj.NOM_COMPLET_or_USERANAME & " to Log Off by" & User_Conneted.NOM_COMPLET_or_USERANAME, Request.UserHostAddress)
                    'MessageToShow("L'utilisateur '<b>" & obj.NOM_COMPLET_or_USERANAME & "</b>' a été forcé de se reconnecter.", "S")
                Case "SEND_EMAIL"
                    Dim obj As New Cls_User(CLng(e.CommandArgument))
                    MessageToShow("SEND_EMAIL to " & obj.NOMCOMPLET, "S")
            End Select
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
            ErreurLog.WriteError("[ " & User_Connected.USERNAME & " ] Methode -> [ GridView_List_RowCommand Utilisateur ] " & ex.Message)
            GlobalFunctions.Send_Mail_Erreur("[ " & User_Connected.USERNAME & " ] Methode -> [ GridView_List_RowCommand Utilisateur ] " & ex.Message, ex.Message)
        End Try
    End Sub

    Protected Sub GridView_List_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView_List.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                If GridView_List.PageIndex > 0 Then
                    CType(e.Row.FindControl("lbOrder"), Label).Text = GridView_List.PageSize * GridView_List.PageIndex + e.Row.RowIndex + 1
                Else
                    CType(e.Row.FindControl("lbOrder"), Label).Text = e.Row.RowIndex + 1
                End If

                CType(e.Row.FindControl("Btn_Del"), ImageButton).Attributes.Add("onclick", "return confirm('Êtes-vous sûr de vouloir déconnecté cet utilisateur')")

            End If
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
            ErreurLog.WriteError("[ " & User_Connected.USERNAME & " ] Methode -> [ GridView_List_RowDataBound Utilisateur ] " & ex.Message)
        End Try
    End Sub

    Protected Sub GridView_List_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView_List.RowEditing
        Try
            Cls_User.Disconnect_User_ById(CLng(GridView_List.DataKeys(e.NewEditIndex).Value))
            LoadData_GridView_List()
            Count_DATAUser()
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
            ErreurLog.WriteError("[ " & User_Connected.USERNAME & " ] Methode -> [ GridView_List_RowEditing Utilisateur ] " & ex.Message)
        End Try
    End Sub

    Protected Sub GridView_List_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView_List.Sorting
        Try
            Dim tmp As String = ""

            tmp = Me.ViewState("sortfield")
            Me.ViewState("sortfield") = e.SortExpression

            If tmp = Me.ViewState("sortfield") Then
                If Me.ViewState("sortdirection") = "ASC" Then
                    Me.ViewState("sortdirection") = "DESC"
                Else
                    Me.ViewState("sortdirection") = "ASC"
                End If
            Else
                Me.ViewState("sortdirection") = "ASC"
            End If
            LoadData_GridView_List()
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
            ErreurLog.WriteError("[ " & User_Connected.USERNAME & " ] Methode -> [ Sorting Utilisateur ] " & ex.Message)
        End Try
    End Sub

    Private Sub DDL_PageSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DDL_PageSize.SelectedIndexChanged
        Try
            If DDL_PageSize.SelectedValue = 30000 Then
                GridView_List.AllowPaging = False
            Else
                GridView_List.AllowPaging = True
            End If
            GridView_List.PageSize = DDL_PageSize.SelectedValue
            LoadData_GridView_List()
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
            ErreurLog.WriteError("[ " & User_Connected.USERNAME & " ] Methode -> [ PageSize Utilisateur ] " & ex.Message)
        End Try
    End Sub

#End Region

    Protected Sub DDL_GroupUtilisateur_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDL_GroupUtilisateur.SelectedIndexChanged
        Response.Redirect("Wfrm_UserConnected.aspx?IDGroupe=" & DDL_GroupUtilisateur.SelectedValue & "&Compte=" & DDL_GroupUtilisateur.SelectedItem.Text & "")
    End Sub
End Class
