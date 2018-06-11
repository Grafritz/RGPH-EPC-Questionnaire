' @Brain Development
' Saturday 21-09-2013

Imports RGPH_QCOLLECTE_Library
Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Partial Class Parametres_Administration_Groupe_Right
    Inherits System.Web.UI.Page

    Dim _message As String = ""
    'Private Const Nom_page As String = "PAGE-PRIVILEGE-GROUPE"
    Private Const Nom_page As String = "Wfrm_Groupe_Right.aspx"
    Private Const btnSendPrivilege As String = "Bouton-Ajouter-Privilege"
    Private Const btnRemovePrivilege As String = "Bouton-Remove-Privilege"


    Dim User_Connected As Cls_User
    Dim Is_Acces_Page As Boolean = True
    Dim GetOut As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Expires = -1
        Label_Msg.Text = ""

        SECURITE_SYSTEM()

        If Is_Acces_Page Then
            If Not IsPostBack Then
                Me.ViewState("sortfield") = "ID"
                Me.ViewState("sortdirection") = "DESC"
                LikeFirstTime(True)
            End If
        End If

    End Sub



#Region "SECURITE SYSTEME"
    Public Sub SECURITE_SYSTEM()

        User_Connected = [Global].KeepUserContinuesToWork(User_Connected)

        CType(Page.Master.FindControl("DashMenu_Securite").FindControl("liPANEL_GESTION_SECURITE"), HtmlControl).Attributes.Add("class", "active treeview")
        CType(Page.Master.FindControl("DashMenu_Securite").FindControl("liPAGE_PRIVILEGE_GROUPE"), HtmlControl).Attributes.Add("class", "active")

        If Session([Global].GLOBAL_SESSION) IsNot Nothing Then
            User_Connected = CType(Session([Global].GLOBAL_SESSION), Cls_User)
            If Not Cls_Privilege.VerifyRightOnObject(Nom_page, User_Connected.IdGroupeuser) Then
                MessageToShow([Global].NO_ACCES_PAGE)
                Is_Acces_Page = False

                'UpdatePanel_First.Visible = False
                Panel_First.Visible = False
                '--- Sorry vous n'avez pas Acces a la page
            Else
                btnSendOne.Visible = Cls_Privilege.VerifyRightOnObject(btnSendPrivilege, User_Connected.IdGroupeuser)
                btnSendAll.Visible = Cls_Privilege.VerifyRightOnObject(btnSendPrivilege, User_Connected.IdGroupeuser)
                btnRemoveOne.Visible = Cls_Privilege.VerifyRightOnObject(btnRemovePrivilege, User_Connected.IdGroupeuser)
                btnRemoveAll.Visible = Cls_Privilege.VerifyRightOnObject(btnRemovePrivilege, User_Connected.IdGroupeuser)
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

    Private Sub LikeFirstTime(ByVal withchanges As Boolean)
        Dim tmp As Cls_GroupeUserCollections

        Try
            tmp = (New Cls_GroupeUser).Search()
            If tmp.Count > 0 Then
                dplstGroup.DataSource = tmp
                dplstGroup.DataTextField = "GROUPE_DESCRIPTION"
                dplstGroup.DataValueField = "ID"
                dplstGroup.DataBind()

                dplstGroup.SelectedIndex = 0
                FillListes(CLng(dplstGroup.Items(dplstGroup.SelectedIndex).Value))
            Else
                Label_Msg.Text = "Pas de Groupe déjà créé."
                Dialogue.alert("Pas de Groupe déjà créé.")
            End If
        Catch ex As Rezo509Exception
            Label_Msg.Text = ex.Message
            Dialogue.alert(ex.Message)
        End Try
    End Sub

    Private Sub FillListes(ByVal idcmd As Long)
        Dim tmp As Cls_RightCollections
        Dim tmp1 As Cls_RightCollections

        Try
            tmp = Cls_Right.SearchRightsByGroup(idcmd)
            If tmp.Count > 0 Then
                lstAffectObjects.DataSource = tmp
                lstAffectObjects.DataTextField = "DESCRIPTION_TACHE"
                lstAffectObjects.DataValueField = "ID"
                lstAffectObjects.DataBind()
            Else
                lstAffectObjects.Items.Clear()
            End If

            tmp1 = Cls_Right.SearchRightsNotAssignToGroup(idcmd)
            If tmp1.Count > 0 Then
                LstAvailObjects.DataSource = tmp1
                LstAvailObjects.DataTextField = "DESCRIPTION_TACHE"
                LstAvailObjects.DataValueField = "ID"
                LstAvailObjects.DataBind()
            Else
                LstAvailObjects.Items.Clear()
            End If
        Catch ex As Rezo509Exception
            Label_Msg.Text = ex.Message
            Dialogue.alert(ex.Message)
        End Try
    End Sub

    Private Sub dplstGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dplstGroup.SelectedIndexChanged
        FillListes(CLng(dplstGroup.Items(dplstGroup.SelectedIndex).Value))
    End Sub

    Private Sub btnSendOne_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendOne.Click
        Try
            Dim _item As ListItem
            Dim obj As Cls_GroupeUser
            Dim obj1 As Cls_Right

            If LstAvailObjects.SelectedIndex >= 0 And dplstGroup.SelectedIndex >= 0 Then
                obj = New Cls_GroupeUser(CLng(dplstGroup.Items(dplstGroup.SelectedIndex).Value))
                For Each _item In LstAvailObjects.Items
                    If _item.Selected Then
                        obj1 = New Cls_Right(CLng(_item.Value))

                        obj.addRights(obj1, "Grafritz")
                        'obj.addRights(obj1, User_Conneted.USERNAME)
                    End If
                Next

                FillListes(CLng(dplstGroup.Items(dplstGroup.SelectedIndex).Value))
            Else
                Dialogue.alert("Pas de Tâche sélectionnée pour affectation OU pas de Groupe.")
                Label_Msg.Text = "Pas de Tâche sélectionnée pour affectation OU pas de Groupe."
            End If
        Catch ex As Rezo509Exception
            Label_Msg.Text = ex.Message
            Dialogue.alert(ex.Message)
        End Try
    End Sub

    Private Sub btnSendAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendAll.Click
        Try
            If LstAvailObjects.Items.Count > 0 And dplstGroup.SelectedIndex >= 0 Then
                Dim i As Long
                Dim obj As New Cls_GroupeUser(CLng(dplstGroup.Items(dplstGroup.SelectedIndex).Value))
                Dim obj1 As Cls_Right

                For i = 0 To (LstAvailObjects.Items.Count - 1)
                    obj1 = New Cls_Right(CLng(LstAvailObjects.Items(i).Value))

                    obj.addRights(obj1, "Grafritz")
                    'obj.addRights(obj1, _currentUser.Login)
                Next

                FillListes(CLng(dplstGroup.Items(dplstGroup.SelectedIndex).Value))
            Else
                Label_Msg.Text = "Pas de Tâche disponible pour affectation OU pas de Groupe."
                Dialogue.alert("Pas de Tâche disponible pour affectation OU pas de Groupe.")
            End If
        Catch ex As Rezo509Exception
            Label_Msg.Text = ex.Message
            Dialogue.alert(ex.Message)
        End Try
    End Sub

    Private Sub btnRemoveAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveAll.Click
        Try
            If lstAffectObjects.Items.Count > 0 And dplstGroup.SelectedIndex >= 0 Then
                Dim i As Long
                Dim obj As New Cls_GroupeUser(CLng(dplstGroup.Items(dplstGroup.SelectedIndex).Value))
                Dim obj1 As Cls_Right

                For i = 0 To (lstAffectObjects.Items.Count - 1)
                    obj1 = New Cls_Right(CLng(lstAffectObjects.Items(i).Value))

                    obj.removeRights(obj1)
                Next

                FillListes(CLng(dplstGroup.Items(dplstGroup.SelectedIndex).Value))
            Else
                Label_Msg.Text = "Pas de Tâche disponible pour suppression OU pas de Groupe."
                Dialogue.alert("Pas de Tâche disponible pour suppression OU pas de Groupe.")
            End If
        Catch ex As Rezo509Exception
            Label_Msg.Text = ex.Message
            Dialogue.alert(ex.Message)
        End Try
    End Sub

    Private Sub btnRemoveOne_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveOne.Click
        Try
            Dim _item As ListItem
            Dim obj As Cls_GroupeUser
            Dim obj1 As Cls_Right

            If lstAffectObjects.SelectedIndex >= 0 And dplstGroup.SelectedIndex >= 0 Then
                obj = New Cls_GroupeUser(CLng(dplstGroup.Items(dplstGroup.SelectedIndex).Value))
                For Each _item In lstAffectObjects.Items
                    If _item.Selected Then
                        obj1 = New Cls_Right(CLng(_item.Value))

                        obj.removeRights(obj1)
                    End If
                Next

                FillListes(CLng(dplstGroup.Items(dplstGroup.SelectedIndex).Value))
            Else
                Label_Msg.Text = "Pas de Tâche sélectionnée pour suppression OU pas de Groupe."
                Dialogue.alert("Pas de Tâche sélectionnée pour suppression OU pas de Groupe.")
            End If
        Catch ex As Rezo509Exception
            Label_Msg.Text = ex.Message
            Dialogue.alert(ex.Message)
        End Try
    End Sub

End Class
