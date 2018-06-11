' @Brain Development
' Monday 18-08-2014

Imports RGPH_QCOLLECTE_Library
Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Partial Class Parametres_Administration_Wfrm_Module
    Inherits Cls_BasePage

    Private Const Nom_page As String = "PAGE-MODULES"
    Private Const Btn_SaveEdit As String = "Bouton-Save-Edit-Module"
    Private Const Btn_Delete As String = "Bouton-Delete-Module"
    Dim User_Connected As Cls_User
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
        CType(Page.Master.FindControl("DashMenu_Securite").FindControl("liPAGE_MODULES"), HtmlControl).Attributes.Add("class", "active")

        If Session([Global].GLOBAL_SESSION) IsNot Nothing Then
            User_Connected = CType(Session([Global].GLOBAL_SESSION), Cls_User)
            If Not Cls_Privilege.VerifyRightOnObject(Nom_page, User_Connected.IdGroupeuser) Then
                MessageToShow([Global].NO_ACCES_PAGE)
                Is_Acces_Page = False

                Panel_First.Visible = False
                Panel_List.Visible = False
                '--- Sorry vous n'avez pas Acces a la page 
                RemoveHandler Btn_Save.Click, AddressOf Btn_Save_Click
                RemoveHandler LinkButton_AddNew.Click, AddressOf LinkButton_AddNew_Click
                RemoveHandler Btn_Annuler.Click, AddressOf Btn_Annuler_Click

                RemoveHandler GridView_List.RowEditing, AddressOf GridView_List_RowEditing
                RemoveHandler GridView_List.RowDeleting, AddressOf GridView_List_RowDeleting
                RemoveHandler GridView_List.RowDataBound, AddressOf GridView_List_RowDataBound
                RemoveHandler GridView_List.PageIndexChanging, AddressOf GridView_List_PageIndexChanging

                RemoveHandler DDL_PageSize.SelectedIndexChanged, AddressOf DDL_PageSize_SelectedIndexChanged

            Else
                '---  Okey vous avez acces a la page ---'
                LinkButton_AddNew.Visible = Cls_Privilege.VerifyRightOnObject(Btn_SaveEdit, User_Connected.IdGroupeuser)
                Btn_Save.Visible = Cls_Privilege.VerifyRightOnObject(Btn_SaveEdit, User_Connected.IdGroupeuser)
                Btn_Annuler.Visible = Cls_Privilege.VerifyRightOnObject(Btn_SaveEdit, User_Connected.IdGroupeuser)

                GridView_List.Columns(GridView_List.Columns.Count - 1).Visible = Cls_Privilege.VerifyRightOnObject(Btn_Delete, User_Connected.IdGroupeuser)
                GridView_List.Columns(GridView_List.Columns.Count - 2).Visible = Cls_Privilege.VerifyRightOnObject(Btn_SaveEdit, User_Connected.IdGroupeuser)
            End If
        End If

        If Session([Global].GLOBAL_SESSION) Is Nothing Then
            '-- Session expirée --'
            Response.Redirect([Global].PAGE_LOGIN)
        Else
            Try
                User_Connected = CType(Session([Global].GLOBAL_SESSION), Cls_User)
                If Not (GlobalFunctions.IsUserStillConnected(User_Connected) And GlobalFunctions.IsUserStillActive(User_Connected)) Then
                    User_Connected.Set_Status_ConnectedUser(False)
                    User_Connected.Activite_Utilisateur_InRezo("Forced Log Off", "Forced to Log Off", Request.UserHostAddress)

                    Session.RemoveAll()
                    MessageToShow("Session expirée.")
                    Label_Msg.Text = "Session expirée."
                    Is_Acces_Page = True
                End If
            Catch ex As Rezo509Exception
                Label_Msg.Text = "Session expirée."
            End Try
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
            LinkButton_AddNew.Visible = True
        End If
    End Sub

    Public Sub DisableControls()
        GridView_List.Columns(GridView_List.Columns.Count - 1).Visible = False
        GridView_List.Columns(GridView_List.Columns.Count - 2).Visible = False
        'LinkButton_AddNew.Visible = False
    End Sub

    Public Sub Clear_Field()
        txt_Module.Text = String.Empty
        Txt_DescriptionModule.Text = String.Empty
    End Sub

    Private Sub SAVE()
        Try
            Dim obj As New Cls_SR_MODULES(CLng(txt_ID.Text))
            obj.NomModule = txt_Module.Text
            obj.Moduledescription = Txt_DescriptionModule.Text
            obj.Actifyn = Actifyn.checked

            obj.Save(User_Connected.Username)
            MessageToShow("Enregistrement effectuée", "S")
            'Clear_Field()
        Catch ex As Rezo509Exception
            Label_Msg.Text = ex.Message
            ErreurLog.WriteError("SAVE --> " & ex.Message)
            MessageToShow(ex.Message)
        End Try

    End Sub

#End Region

#Region "Load DATA"

    Private Sub LoadData_GridView_List()
        Dim objs As List(Of Cls_SR_MODULES)
        Try
            objs = (New Cls_SR_MODULES).Search
            'objs.ApplySortSTR(Me.ViewState("sortfield"), Me.ViewState("sortdirection"))

            'If Not IsPostBack Then
            Label_Object.Text = "Modules / Fonctionnalités [" & objs.Count & "]"
            'End If
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
            MessageToShow(ex.Message)
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
        Dim obj As New Cls_SR_MODULES
        Try
            obj.Read(CLng(GridView_List.DataKeys(e.RowIndex).Value))
            obj.Delete()
            LoadData_GridView_List()
        Catch ex As Rezo509Exception
            ErreurLog.WriteError(ex.Message)
            MessageToShow(ex.Message)
            Label_Msg.Text = ex.Message
            Label_Msg.ForeColor = Drawing.Color.OrangeRed
        End Try
    End Sub

    Protected Sub GridView_List_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView_List.RowEditing

        Panel_First.Visible = True
        'DisableControls()

        Dim obj As New Cls_SR_MODULES
        Try
            obj.Read(CLng(GridView_List.DataKeys(e.NewEditIndex).Value))
            txt_ID.Text = obj.ID
            txt_Module.Text = obj.NomModule
            Txt_DescriptionModule.Text = obj.Moduledescription
            Actifyn.Checked = obj.Actifyn
        Catch ex As Rezo509Exception
            ErreurLog.WriteError(ex.Message)
            MessageToShow(ex.Message)
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
            MessageToShow(ex.Message)
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
            MessageToShow(ex.Message)
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

    Protected Sub LinkButton_AddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton_AddNew.Click
        NewToDo()
        Panel_First.Visible = True
        'DisableControls()
        Clear_Field()
    End Sub

#End Region

End Class
