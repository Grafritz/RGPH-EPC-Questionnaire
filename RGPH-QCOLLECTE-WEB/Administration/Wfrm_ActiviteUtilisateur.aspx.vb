' @Brain Development
' Saturday 21-09-2013


Imports RGPH_QCOLLECTE_Library
Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports Telerik.Web.UI


Partial Class Parametres_Administration_Wfrm_ActiviteUtilisateur
    Inherits Cls_BasePage

#Region "ATTRIBUTS"
    Dim _message As String = ""
    'Private Const Nom_page As String = "PAGE-ACTIVITES-UTILISATEUR"
    'Private Const Btn_SaveEditUser As String = "Btn_SaveEditUser"
    'Private Const Btn_DeleteUser As String = "Btn_DeleteUser"
    'Private Const Btn_ResetPassWordUser As String = "Btn_ResetPassWordUser"

    Private Const Nom_page As String = "Wfrm_ActiviteUtilisateur.aspx"
    Private Const Btn_SaveEditUser As String = "Btn_SaveEditUser"
    Private Const Btn_DeleteUser As String = "Btn_DeleteUser"
    Private Const Btn_ResetPassWordUser As String = "Btn_ResetPassWordUser"
    Private Const Btn_DeleteActiviteUtilisateur As String = "Bouton-DELETE-ACTIVITES-UTILISATEUR"

    Dim User_Connected As Cls_User
    Dim Is_Acces_Page As Boolean = True
    Dim GetOut As Boolean = False
    Dim IS_Search As Boolean = False
    Dim IS_Trouve As Boolean = False
    Private IDGroupe As Long = 0
#End Region

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Expires = -1
            Label_Msg.Text = ""
            Panel_Msg.Visible = False

            SYSTEME_SECURITE()

            '--- Si l'utilisateur a Acces a la page 
            If Is_Acces_Page Then
                If Not IsPostBack Then
                    LinkButton_DeleteALL.Attributes.Add("onclick", "return confirm('Êtes-vous sûr de vouloir supprimer toutes les activitées ?')")

                    'Me.ViewState("sortfield") = "ID"
                    'Me.ViewState("sortdirection") = "DESC"

                    txtDateDebut.SelectedDate = DateAdd(DateInterval.Month, -1, Now) '.ToString("MM/dd/yyyy")
                    txtDateFin.SelectedDate = DateAdd(DateInterval.Day, 1, Now) '.ToString("MM/dd/yyyy")

                    COUNT_ALL_ActiviteUser()
                    LoadUtilisateur_Inlog()
                    LoadAll_ActivityUser_Inlog()

                    BindGridActivites() 'LoadData_GridView_List()
                    NewToDo()
                End If
            End If
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub

#Region "SECURITE"
    Public Sub SYSTEME_SECURITE()
        Try
            User_Connected = [Global].KeepUserContinuesToWork(User_Connected)

            CType(Page.Master.FindControl("DashMenu_Securite").FindControl("liPANEL_GESTION_SECURITE"), HtmlControl).Attributes.Add("class", "active treeview")
            CType(Page.Master.FindControl("DashMenu_Securite").FindControl("liPAGE_ACTIVITES_UTILISATEUR"), HtmlControl).Attributes.Add("class", "active")

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
                    'RemoveHandler GridView_List.PageIndexChanging, AddressOf GridView_List_PageIndexChanging

                    'RemoveHandler DDL_PageSize.SelectedIndexChanged, AddressOf DDL_PageSize_SelectedIndexChanged

                Else
                    '---  Okey vous avez acces a la page ---'
                    Dim _check As Boolean = Cls_Privilege.VerifyRightOnObject(Btn_DeleteActiviteUtilisateur, User_Connected.IdGroupeuser)
                    LinkButton_DeleteALL.Visible = _check
                    RadGrid_1.MasterTableView.Columns.FindByUniqueNameSafe("Actions").Visible = _check

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

        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try

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
    Private Sub COUNT_ALL_ActiviteUser()
        Label_Utilisateur.Text = "ACTIVITES UTILISATEURS  <small class=""badge badge-primary"">" & Cls_Activite_Utilisateur.COUNT_AllActivite_Utilisateur() & "</small>"
    End Sub

    Private Sub LoadUtilisateur_Inlog()
        Try
            Dim obj As System.Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_ListAll_Utilisateur_InLog")

            DDL_Utilisateur.Items.Clear()
            DDL_Utilisateur.DataTextField = "User_ID"
            DDL_Utilisateur.DataValueField = "User_ID"
            DDL_Utilisateur.DataSource = obj
            DDL_Utilisateur.DataBind()

            DDL_Utilisateur.Items.Insert(0, New ListItem("Tous les utilisateurs", ""))
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub

    Private Sub LoadAll_ActivityUser_Inlog()
        Try
            Dim obj As System.Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_ListAll_ActivityUser_InLog")

            DDL_ActiviteUser.Items.Clear()
            DDL_ActiviteUser.DataTextField = "Activity"
            DDL_ActiviteUser.DataValueField = "Activity"
            DDL_ActiviteUser.DataSource = obj
            DDL_ActiviteUser.DataBind()

            DDL_ActiviteUser.Items.Insert(0, New ListItem("Toutes les Activités", ""))
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub

    Private Sub LoadData_GridView_List()
        'Dim objs As New List(Of Cls_Activite_Utilisateur)
        'Try
        '    objs = Cls_Activite_Utilisateur.Search_ActiviteUser_ByUser_AndDate(DDL_Utilisateur.SelectedValue, DDL_ActiviteUser.SelectedValue, txtDateDebut.SelectedDate, txtDateFin.SelectedDate)

        '    BrainBaseArrayList.ApplySortSTR(objs, Me.ViewState("sortfield"), Me.ViewState("sortdirection"))
        '    GridView_List.DataSource = objs
        '    GridView_List.DataBind()

        '    Label_DetailSearch.Text = "( TOTAL: " & objs.Count & " ) -  De:" & Convert.ToDateTime(txtDateDebut.SelectedDate).ToString("dd-MMM-yyyy") & " à " & Convert.ToDateTime(txtDateFin.SelectedDate).ToString("dd-MMM-yyyy")

        '    If objs.Count = 0 Then
        '        Label_Result.Visible = True
        '        PagingPane.Visible = False
        '    Else
        '        Label_Result.Visible = False
        '        PagingPane.Visible = True
        '    End If
        'Catch ex As Rezo509Exception
        '    MessageToShow(ex.Message)
        'Catch ex As Exception
        '    MessageToShow(ex.Message)
        '    [Global].WriteError(ex, User_Connected)
        'End Try
    End Sub

    Private Sub BindGridActivites(Optional ByVal _refresh As Boolean = True)
        Dim _ret As Long = 0
        Try
            Dim objs As New List(Of Cls_Activite_Utilisateur)
            objs = Cls_Activite_Utilisateur.Search_ActiviteUser_ByUser_AndDate(DDL_Utilisateur.SelectedValue, DDL_ActiviteUser.SelectedValue, txtDateDebut.SelectedDate, txtDateFin.SelectedDate)

            RadGrid_1.DataSource = objs
            If _refresh Then
                RadGrid_1.DataBind()
            End If
            _ret = objs.Count

            Label_DetailSearch.Text = " <small class=""badge badge-primary"">" & objs.Count & "</small> -  De:" & Convert.ToDateTime(txtDateDebut.SelectedDate).ToString("dd-MMM-yyyy") & " à " & Convert.ToDateTime(txtDateFin.SelectedDate).ToString("dd-MMM-yyyy")

            'Label_DetailSearch.Text = "Dossier Etudiants  <small class=""badge badge-primary"">" & _ret & "</small>"
        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, Nothing)
        End Try
    End Sub
#End Region

#Region "EVENTS BUTTON"

    Protected Sub Btn_Search_Click(sender As Object, e As System.EventArgs) Handles Btn_Search.Click
        IS_Search = True
        BindGridActivites() 'LoadData_GridView_List()
        'If Not IS_Trouve Then
        '    Label_Result.Visible = True
        'End If
    End Sub
#End Region

    '#Region "GridView Events"

    '    Protected Sub GridView_List_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView_List.PageIndexChanging
    '        Try
    '            GridView_List.PageIndex = e.NewPageIndex
    '            LoadData_GridView_List()
    '        Catch ex As Rezo509Exception
    '            MessageToShow(ex.Message)
    '        Catch ex As Exception
    '            MessageToShow(ex.Message)
    '            [Global].WriteError(ex, User_Connected)
    '        End Try
    '    End Sub

    '    Protected Sub GridView_List_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView_List.RowDataBound
    '        Try
    '            If e.Row.RowType = DataControlRowType.DataRow Then
    '                If GridView_List.PageIndex > 0 Then
    '                    CType(e.Row.FindControl("lbOrder"), Label).Text = GridView_List.PageSize * GridView_List.PageIndex + e.Row.RowIndex + 1
    '                Else
    '                    CType(e.Row.FindControl("lbOrder"), Label).Text = e.Row.RowIndex + 1
    '                End If

    '                'CType(e.Row.FindControl("Btn_Del"), ImageButton).Attributes.Add("onclick", "return confirm('Êtes-vous sûr de vouloir supprimer cet activité')")

    '            End If
    '        Catch ex As Rezo509Exception
    '            MessageToShow(ex.Message)
    '        Catch ex As Exception
    '            MessageToShow(ex.Message)
    '            [Global].WriteError(ex, User_Connected)
    '        End Try
    '    End Sub

    '    'Protected Sub GridView_List_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView_List.RowEditing
    '    '    Try
    '    '        Cls_User.Disconnect_User_ById(CLng(GridView_List.DataKeys(e.NewEditIndex).Value))
    '    '        LoadData_GridView_List()
    '    '    Catch ex As Exception
    '    '        MessageToShow(ex.Message)
    '    '        ErreurLog.WriteError("[ " & User_Conneted.USERNAME & " ] Methode -> [ GridView_List_RowEditing Utilisateur ] " & ex.Message)
    '    '    End Try
    '    'End Sub

    '    Protected Sub GridView_List_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView_List.Sorting
    '        Try
    '            Dim tmp As String = ""

    '            tmp = Me.ViewState("sortfield")
    '            Me.ViewState("sortfield") = e.SortExpression

    '            If tmp = Me.ViewState("sortfield") Then
    '                If Me.ViewState("sortdirection") = "ASC" Then
    '                    Me.ViewState("sortdirection") = "DESC"
    '                Else
    '                    Me.ViewState("sortdirection") = "ASC"
    '                End If
    '            Else
    '                Me.ViewState("sortdirection") = "ASC"
    '            End If
    '            LoadData_GridView_List()
    '        Catch ex As Rezo509Exception
    '            MessageToShow(ex.Message)
    '        Catch ex As Exception
    '            MessageToShow(ex.Message)
    '            [Global].WriteError(ex, User_Connected)
    '        End Try
    '    End Sub

    '    Private Sub DDL_PageSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DDL_PageSize.SelectedIndexChanged
    '        Try
    '            If DDL_PageSize.SelectedValue = 30000 Then
    '                GridView_List.AllowPaging = False
    '            Else
    '                GridView_List.AllowPaging = True
    '            End If
    '            GridView_List.PageSize = DDL_PageSize.SelectedValue
    '            LoadData_GridView_List()
    '        Catch ex As Rezo509Exception
    '            MessageToShow(ex.Message)
    '        Catch ex As Exception
    '            MessageToShow(ex.Message)
    '            [Global].WriteError(ex, User_Connected)
    '        End Try
    '    End Sub

    '#End Region

#Region "RADGRID EVENTS"
    Protected Sub RadGrid_1_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid_1.ItemCommand
        Try
            Dim _id As Long = TypeSafeConversion.NullSafeLong(e.CommandArgument)
            Select Case e.CommandName
                Case "delete"
                    'Dim obj As New Cls_Activite_Utilisateur()
                    Cls_Activite_Utilisateur.DeleteOne(_id)
                    RadGrid_1.Rebind()
                    'MessageToShow("Retard supprimée", "S")
                Case Else
                    'NOTHING
            End Select
        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, Nothing)
        End Try
    End Sub

    Protected Sub RadGrid_1_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid_1.ItemDataBound
        Try
            Dim cmditem As GridCommandItem = TryCast(e.Item, GridCommandItem)
            If (cmditem IsNot Nothing) Then
                'Dim btnAdd As LinkButton = CType(cmditem.FindControl("InitInsertButton"), LinkButton)
                'btnAdd.Attributes.Add("onclick", "javascript:ShowAddUpdateFormMaximized('Frm_InscriptionEleve.aspx',950,600); return false;")
                'btnAdd.Visible = Cls_Privilege.VerifyRightOnObject(Btn_Save, User_Connected.IdGroupeuser)
            End If

            Dim gridDataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            If (gridDataItem IsNot Nothing) Then
                Dim item As GridDataItem = gridDataItem
                CType(item.FindControl("lbOrder"), Label).Text = RadGrid_1.PageSize * RadGrid_1.CurrentPageIndex + (item.RowIndex / 2)
                Dim _Id As Long = CType(DataBinder.Eval(e.Item.DataItem, "ID"), Long)
                'Dim imageediter As ImageButton = CType(item("Update").Controls(0), ImageButton)
                'Dim imagedelete As ImageButton = CType(item("delete").Controls(0), ImageButton)
                'imageediter.CommandArgument = CType(DataBinder.Eval(e.Item.DataItem, "ID"), String)

                Dim LinkButton_Delete As LinkButton = CType(item.FindControl("LinkButton_Delete"), LinkButton)
                LinkButton_Delete.CommandArgument = _Id
                'LinkButton_Delete.Attributes.Add("onclick", "javascript:ShowAddUpdateForm('Frm_Etudiant.aspx?ID=" & _id & "&" & [Global].ACTION & "=" & [Global].HideMenuHeader & "',950,600); return false;")

                REM Privilege
                'imageediter.Visible = Cls_Privilege.VerifyRightOnObject(Btn_Edit, User_Connected.IdGroupeuser)
                'imagedelete.Visible = Cls_Privilege.VerifyRightOnObject(Btn_Delete, User_Connected.IdGroupeuser)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, Nothing)
        End Try
    End Sub

    Private Sub RadGrid_1_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid_1.NeedDataSource
        If IsPostBack Then
            BindGridActivites(False)
        End If
    End Sub

#End Region
    Protected Sub LinkButton_DeleteALL_Click(sender As Object, e As EventArgs) Handles LinkButton_DeleteALL.Click
        Try
            Cls_Activite_Utilisateur.DeleteALL()
            RadGrid_1.Rebind()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, Nothing)
        End Try
    End Sub
End Class
