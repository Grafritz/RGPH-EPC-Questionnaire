' @Brain Development
' Saturday 21-09-2013

Imports RGPH_QCOLLECTE_Library
Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Partial Class Parametres_Administration_Wfrm_Droit_User
    Inherits System.Web.UI.Page

    Dim _message As String = ""
    'Private Const Nom_page As String = "PAGE-GESTION-TACHES-UTILISATEURS"
    'Private Const Btn_SaveEdit As String = "Bouton-Save-Edit-Taches"
    'Private Const Btn_Delete As String = "Bouton-Delete-Taches"

    Private Const Nom_page As String = "Wfrm_Taches_User.aspx"
    Private Const Btn_SaveEdit As String = "Btn_SaveEditTaches"
    Private Const Btn_Delete As String = "Btn_DeleteTaches"
    Dim User_Connected As Cls_User
    Dim GetOut As Boolean = False
    Dim Is_Acces_Page As Boolean = True

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Expires = -1
        Panel_Msg.Visible = False

        SECURITE_SYSTEM()

        If Is_Acces_Page Then
            If Not IsPostBack Then
                Me.ViewState("sortfield") = "ID"
                Me.ViewState("sortdirection") = "DESC"
                LoadData_GridView_List()
                NewToDo()
            End If
        End If

    End Sub


#Region "SECURITE SYSTEME"
    Public Sub SECURITE_SYSTEM()

        User_Connected = [Global].KeepUserContinuesToWork(User_Connected)

        CType(Page.Master.FindControl("DashMenu_Securite").FindControl("liPANEL_GESTION_SECURITE"), HtmlControl).Attributes.Add("class", "active treeview")
        CType(Page.Master.FindControl("DashMenu_Securite").FindControl("liPAGE_GESTION_TACHES_UTILISATEURS"), HtmlControl).Attributes.Add("class", "active")

        If Session([Global].GLOBAL_SESSION) IsNot Nothing Then
            User_Connected = CType(Session([Global].GLOBAL_SESSION), Cls_User)
            If Not Cls_Privilege.VerifyRightOnObject(Nom_page, User_Connected.IdGroupeuser) Then
                Dialogue.alert("Pas de Droit à la page Cette page.")
                Is_Acces_Page = False

                'UpdatePanel_First.Visible = False
                Panel_First.Visible = False
                Panel_List.Visible = False
                '--- Sorry vous n'avez pas Acces a la page 
                RemoveHandler Btn_Save.Click, AddressOf Btn_Save_Click
                RemoveHandler Btn_Nouveau.Click, AddressOf Btn_Nouveau_Click
                RemoveHandler Btn_Annuler.Click, AddressOf Btn_Annuler_Click

                RemoveHandler GridView_List.RowEditing, AddressOf GridView_List_RowEditing
                RemoveHandler GridView_List.RowDeleting, AddressOf GridView_List_RowDeleting
                RemoveHandler GridView_List.RowDataBound, AddressOf GridView_List_RowDataBound
                RemoveHandler GridView_List.PageIndexChanging, AddressOf GridView_List_PageIndexChanging

                RemoveHandler DDL_PageSize.SelectedIndexChanged, AddressOf DDL_PageSize_SelectedIndexChanged
            Else
                '---  Okey vous avez acces a la page ---'
                Btn_Nouveau.Visible = Cls_Privilege.VerifyRightOnObject(Btn_SaveEdit, User_Connected.IdGroupeuser)
                Btn_Save.Visible = Cls_Privilege.VerifyRightOnObject(Btn_SaveEdit, User_Connected.IdGroupeuser)
                Btn_Annuler.Visible = Cls_Privilege.VerifyRightOnObject(Btn_SaveEdit, User_Connected.IdGroupeuser)

                GridView_List.Columns(GridView_List.Columns.Count - 1).Visible = Cls_Privilege.VerifyRightOnObject(Btn_Delete, User_Connected.IdGroupeuser)
                GridView_List.Columns(GridView_List.Columns.Count - 2).Visible = Cls_Privilege.VerifyRightOnObject(Btn_SaveEdit, User_Connected.IdGroupeuser)
            End If
        End If


        If Session([Global].GLOBAL_SESSION) Is Nothing Then
            '-- Session expirée --'
            GetOut = True
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
        Panel_First.Visible = False
        Panel_List.Visible = True
        txt_ID.Text = "0"
        Clear_Field()
    End Sub

    Public Sub EnableControls()
        If Cls_Privilege.VerifyRightOnObject(Btn_Delete, User_Connected.IdGroupeuser) Then
            GridView_List.Columns(GridView_List.Columns.Count - 1).Visible = True
        End If
        If Cls_Privilege.VerifyRightOnObject(Btn_SaveEdit, User_Connected.IdGroupeuser) Then
            GridView_List.Columns(GridView_List.Columns.Count - 2).Visible = True
            Btn_Nouveau.Visible = True
        End If
    End Sub

    Public Sub DisableControls()
        GridView_List.Columns(GridView_List.Columns.Count - 1).Visible = False
        GridView_List.Columns(GridView_List.Columns.Count - 2).Visible = False
        Btn_Nouveau.Visible = False

    End Sub

    Public Sub Clear_Field()
        txt_Description_Taches.Text = String.Empty
    End Sub

    Private Sub SAVE()
        Try
            Dim obj As New Cls_Right(CLng(txt_ID.Text))
            obj.DESCRIPTION_TACHE = txt_Description_Taches.Text

            obj.Save(User_Connected.USERNAME)
            Dialogue.alert("Enregistrement effectuée")
        Catch ex As Rezo509Exception
            Label_Msg.Text = ex.Message
            ErreurLog.WriteError("SAVE Groupe Utilisateur --> " & ex.Message)
            Dialogue.alert(ex.Message)
        End Try

    End Sub

#End Region

#Region "Load DATA"

    Private Sub LoadData_GridView_List()
        Dim objs As Cls_RightCollections
        Try
            objs = (New Cls_Right).Search
            objs.ApplySortSTR(Me.ViewState("sortfield"), Me.ViewState("sortdirection"))

            GridView_List.DataSource = objs
            GridView_List.DataBind()
            If objs.Count = 0 Then
                'NewToDo()
                PagingPane.Visible = False
            Else
                PagingPane.Visible = True
            End If
        Catch ex As Rezo509Exception
            ErreurLog.WriteError("METHODE -> LoadData_GridView_List" & ex.Message)
            Dialogue.alert(ex.Message)
            Label_Msg.Text = ex.Message
            Label_Msg.ForeColor = Drawing.Color.OrangeRed
        End Try
    End Sub

#End Region

#Region "GridView Events"

    Protected Sub GridView_List_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView_List.PageIndexChanging
        GridView_List.PageIndex = e.NewPageIndex
        LoadData_GridView_List()
    End Sub

    Protected Sub GridView_List_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView_List.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If GridView_List.PageIndex > 0 Then
                CType(e.Row.FindControl("lbOrder"), Label).Text = GridView_List.PageSize * GridView_List.PageIndex + e.Row.RowIndex + 1
            Else
                CType(e.Row.FindControl("lbOrder"), Label).Text = e.Row.RowIndex + 1
            End If

            CType(e.Row.FindControl("Btn_Del"), ImageButton).Attributes.Add("onclick", "return confirm('Êtes-vous sûr de vouloir supprimer cet objet ')")
        End If
    End Sub

    Protected Sub GridView_List_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView_List.RowDeleting
        Dim obj As New Cls_Right
        Try
            obj.Read(CLng(GridView_List.DataKeys(e.RowIndex).Value))
            obj.Delete()
            LoadData_GridView_List()
        Catch ex As Rezo509Exception
            ErreurLog.WriteError(ex.Message)
            Dialogue.alert(ex.Message)
            Label_Msg.Text = ex.Message
            Label_Msg.ForeColor = Drawing.Color.OrangeRed
        End Try
    End Sub

    Protected Sub GridView_List_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView_List.RowEditing

        Panel_First.Visible = True
        DisableControls()

        Dim obj As New Cls_Right
        Try
            obj.Read(CLng(GridView_List.DataKeys(e.NewEditIndex).Value))
            txt_ID.Text = obj.ID
            txt_Description_Taches.Text = obj.DESCRIPTION_TACHE
        Catch ex As Rezo509Exception
            ErreurLog.WriteError(ex.Message)
            Dialogue.alert(ex.Message)
            Label_Msg.Text = ex.Message
            Label_Msg.ForeColor = Drawing.Color.OrangeRed
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
            ErreurLog.WriteError(ex.Message)
            Dialogue.alert(ex.Message)
            Label_Msg.Text = ex.Message
            Label_Msg.ForeColor = Drawing.Color.OrangeRed
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
            ErreurLog.WriteError("DDL_PageSize_SelectedIndexChanged -> " & ex.Message)
            Dialogue.alert(ex.Message)
        End Try
    End Sub

#End Region

#Region "EVENTS BUTTON"
    Protected Sub Btn_Save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_Save.Click
        SAVE()
        Panel_First.Visible = False
        EnableControls()
        LoadData_GridView_List()
    End Sub

    Protected Sub Btn_Annuler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_Annuler.Click
        NewToDo()
        EnableControls()
    End Sub

    Protected Sub Btn_Nouveau_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_Nouveau.Click
        NewToDo()
        Panel_First.Visible = True
        DisableControls()
        Clear_Field()
    End Sub


#End Region

End Class
