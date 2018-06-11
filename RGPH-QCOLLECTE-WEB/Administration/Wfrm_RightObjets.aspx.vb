' @Brain Development
' Saturday 21-09-2013

Imports RGPH_QCOLLECTE_Library
Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Partial Class Parametres_Administration_Wfrm_RightObjets
    Inherits System.Web.UI.Page

    Dim _message As String = ""
    'Private Const Nom_page As String = "PAGE-PRIVILEGES-DES-TACHES-UTILISATEURS"
    Private Const btnSendPrivilege As String = "Bouton-Ajouter-Privilege-des-taches-utilisateur"
    Private Const btnRemovePrivilege As String = "Bouton-Remove-Privilege-des-taches-utilisateur"

    Private Const Nom_page As String = "Wfrm_RightObjets.aspx"

    Dim User_Connected As Cls_User
    Dim Is_Acces_Page As Boolean = True
    Dim GetOut As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Expires = -1
        Panel_Msg.Visible = False

        SECURITE_SYSTEM()

        If Is_Acces_Page Then
            If Not IsPostBack Then
                'Me.ViewState("sortfield") = "ID"
                'Me.ViewState("sortdirection") = "DESC"
                LikeFirstTime(True)
                LoadModule()
                FillListes(DDL_ListeALLTache.Items(DDL_ListeALLTache.SelectedIndex).Value, DDLModule.Items(DDLModule.SelectedIndex).Value, DLL_ListeObjectType.Items(DLL_ListeObjectType.SelectedIndex).Value)
            End If
        End If

    End Sub


#Region "SECURITE SYSTEME"
    Public Sub SECURITE_SYSTEM()

        User_Connected = [Global].KeepUserContinuesToWork(User_Connected)

        CType(Page.Master.FindControl("DashMenu_Securite").FindControl("liPANEL_GESTION_SECURITE"), HtmlControl).Attributes.Add("class", "active treeview")
        CType(Page.Master.FindControl("DashMenu_Securite").FindControl("liPAGE_PRIVILEGES_DES_TACHES_UTILISATEURS"), HtmlControl).Attributes.Add("class", "active")

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

#Region "LOAD DATA"
    Private Sub LoadModule()
        Try
            Dim objs As List(Of Cls_SR_MODULES)
            objs = Cls_SR_MODULES.SearchAll_Actif

            DDLModule.DataTextField = "NomModule"
            DDLModule.DataValueField = "ID"
            DDLModule.DataSource = objs
            DDLModule.DataBind()
            'DDLModule.Items.Insert(0, New ListItem("Selectionnez le module", ""))
        Catch ex As Rezo509Exception
            ErreurLog.WriteError("METHODE -> LoadModule" & ex.Message)
            MessageToShow(ex.Message)
        End Try
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
        Dim tmp As Cls_RightCollections
        Dim tmp1 As Cls_TypeObjetCollections

        Try
            tmp = (New Cls_Right).Search()
            If tmp.Count > 0 Then
                DDL_ListeALLTache.DataSource = tmp
                DDL_ListeALLTache.DataTextField = "DESCRIPTION_TACHE"
                DDL_ListeALLTache.DataValueField = "ID"
                DDL_ListeALLTache.DataBind()

                DDL_ListeALLTache.SelectedIndex = 0

                tmp1 = Cls_TypeObjet.SearchDistinctType
                If tmp1.Count > 0 Then
                    DLL_ListeObjectType.DataSource = tmp1
                    DLL_ListeObjectType.DataTextField = "NomType"
                    DLL_ListeObjectType.DataValueField = "NomType"
                    DLL_ListeObjectType.DataBind()

                    DLL_ListeObjectType.SelectedIndex = 0

                    'FillListes(DDL_ListeALLTache.Items(DDL_ListeALLTache.SelectedIndex).Value, DLL_ListeObjectType.Items(DLL_ListeObjectType.SelectedIndex).Value)
                Else
                    Label_Msg.Text = "Pas d'Objet déjà créé."
                    '_out = True
                End If
            Else
                Label_Msg.Text = "Pas de Tâche déjà créée."
            End If
        Catch ex As Rezo509Exception
            Label_Msg.Text = ex.Message
        End Try
    End Sub

    Private Sub FillListes(ByVal IDTache As Long, ByVal IDObjet As String)
        Dim tmp As Cls_PrivilegeCollections
        Dim tmp1 As Cls_PrivilegeCollections

        Try
            tmp = Cls_Privilege.SearchObjectsByTypeAndRight(IDObjet, IDTache)
            If tmp.Count > 0 Then
                lstAffectObjects.DataSource = tmp
                lstAffectObjects.DataTextField = "DESCRIPTION_OBJET"
                lstAffectObjects.DataValueField = "ID"
                lstAffectObjects.DataBind()
            Else
                lstAffectObjects.Items.Clear()
            End If

            tmp1 = Cls_Privilege.SearchObjetsNotAssignToRight(IDObjet, IDTache)
            If tmp1.Count > 0 Then
                LstAvailObjects.DataSource = tmp1
                LstAvailObjects.DataTextField = "DESCRIPTION_OBJET"
                LstAvailObjects.DataValueField = "ID"
                LstAvailObjects.DataBind()
            Else
                LstAvailObjects.Items.Clear()
            End If
        Catch ex As Rezo509Exception
            Label_Msg.Text = ex.Message
        End Try
    End Sub

    Private Sub FillListes(ByVal IDTache As Long, ByVal IDModule As Long, ByVal IDObjet As String)
        Dim tmp As Cls_PrivilegeCollections
        Dim tmp1 As Cls_PrivilegeCollections

        Try
            tmp = Cls_Privilege.SearchObjectsByTypeAndRight(IDObjet, IDModule, IDTache)
            If tmp.Count > 0 Then
                lstAffectObjects.DataSource = tmp
                lstAffectObjects.DataTextField = "DESCRIPTION_OBJET"
                lstAffectObjects.DataValueField = "ID"
                lstAffectObjects.DataBind()
            Else
                lstAffectObjects.Items.Clear()
            End If

            tmp1 = Cls_Privilege.SearchObjetsNotAssignToRight(IDObjet, IDModule, IDTache)
            If tmp1.Count > 0 Then
                LstAvailObjects.DataSource = tmp1
                LstAvailObjects.DataTextField = "DESCRIPTION_OBJET"
                LstAvailObjects.DataValueField = "ID"
                LstAvailObjects.DataBind()
            Else
                LstAvailObjects.Items.Clear()
            End If
        Catch ex As Rezo509Exception
            Label_Msg.Text = ex.Message
        End Try
    End Sub

    Private Sub dplstRight_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DDL_ListeALLTache.SelectedIndexChanged
        'FillListes(DDL_ListeALLTache.Items(DDL_ListeALLTache.SelectedIndex).Value, DLL_ListeObjectType.Items(DLL_ListeObjectType.SelectedIndex).Value)
        FillListes(DDL_ListeALLTache.Items(DDL_ListeALLTache.SelectedIndex).Value, DDLModule.Items(DDLModule.SelectedIndex).Value, DLL_ListeObjectType.Items(DLL_ListeObjectType.SelectedIndex).Value)
    End Sub

    Private Sub dplstObjectType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DLL_ListeObjectType.SelectedIndexChanged
        'FillListes(DDL_ListeALLTache.Items(DDL_ListeALLTache.SelectedIndex).Value, DLL_ListeObjectType.Items(DLL_ListeObjectType.SelectedIndex).Value)
        FillListes(DDL_ListeALLTache.Items(DDL_ListeALLTache.SelectedIndex).Value, DDLModule.Items(DDLModule.SelectedIndex).Value, DLL_ListeObjectType.Items(DLL_ListeObjectType.SelectedIndex).Value)
    End Sub

    Protected Sub DDLModule_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLModule.SelectedIndexChanged
        FillListes(DDL_ListeALLTache.Items(DDL_ListeALLTache.SelectedIndex).Value, DDLModule.Items(DDLModule.SelectedIndex).Value, DLL_ListeObjectType.Items(DLL_ListeObjectType.SelectedIndex).Value)
    End Sub

#Region "EVENTS"
    Private Sub btnSendOne_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendOne.Click
        Try
            Dim _item As ListItem
            Dim obj As Cls_Right
            Dim obj1 As Cls_Privilege
            Dim AllObject As String = ""

            If LstAvailObjects.SelectedIndex >= 0 And DDL_ListeALLTache.SelectedIndex >= 0 Then
                obj = New Cls_Right(CLng(DDL_ListeALLTache.Items(DDL_ListeALLTache.SelectedIndex).Value))
                For Each _item In LstAvailObjects.Items
                    If _item.Selected Then
                        obj1 = New Cls_Privilege(CLng(_item.Value))
                        AllObject &= "<*>" & obj1.NOM_OBJET & " * " & obj1.TYPE_OBJET

                        'obj.addObjets(obj1, "Grafritz")
                        obj.addObjets(obj1, User_Connected.Username)
                    End If
                Next
                User_Connected.Activite_Utilisateur_InRezo("Affectation d'un Groupe d'Object dans Taches", AllObject, Request.UserHostName)

                'FillListes(DDL_ListeALLTache.Items(DDL_ListeALLTache.SelectedIndex).Value, DLL_ListeObjectType.Items(DLL_ListeObjectType.SelectedIndex).Value)
                FillListes(DDL_ListeALLTache.Items(DDL_ListeALLTache.SelectedIndex).Value, DDLModule.Items(DDLModule.SelectedIndex).Value, DLL_ListeObjectType.Items(DLL_ListeObjectType.SelectedIndex).Value)
            Else
                Label_Msg.Text = "Pas d'Objet sélectionné pour affectation OU pas de Tâche."
            End If
        Catch ex As Rezo509Exception
            Label_Msg.Text = ex.Message
        End Try
    End Sub

    Private Sub btnSendAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendAll.Click
        Try
            If LstAvailObjects.Items.Count > 0 And DDL_ListeALLTache.SelectedIndex >= 0 Then
                Dim i As Long
                Dim obj As New Cls_Right(CLng(DDL_ListeALLTache.Items(DDL_ListeALLTache.SelectedIndex).Value))
                Dim obj1 As Cls_Privilege

                For i = 0 To (LstAvailObjects.Items.Count - 1)
                    obj1 = New Cls_Privilege(CLng(LstAvailObjects.Items(i).Value))

                    'obj.addObjets(obj1, "Grafritz")
                    obj.addObjets(obj1, User_Connected.Username)
                Next

                'FillListes(DDL_ListeALLTache.Items(DDL_ListeALLTache.SelectedIndex).Value, DLL_ListeObjectType.Items(DLL_ListeObjectType.SelectedIndex).Value)
                FillListes(DDL_ListeALLTache.Items(DDL_ListeALLTache.SelectedIndex).Value, DDLModule.Items(DDLModule.SelectedIndex).Value, DLL_ListeObjectType.Items(DLL_ListeObjectType.SelectedIndex).Value)
            Else
                Label_Msg.Text = "Pas d'Objet disponible pour affectation OU pas de Tâche."
            End If
        Catch ex As Rezo509Exception
            Label_Msg.Text = ex.Message
        End Try
    End Sub

    Private Sub btnRemoveAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveAll.Click
        Try
            If lstAffectObjects.Items.Count > 0 And DDL_ListeALLTache.SelectedIndex >= 0 Then
                Dim i As Long
                Dim obj As New Cls_Right(CLng(DDL_ListeALLTache.Items(DDL_ListeALLTache.SelectedIndex).Value))
                Dim obj1 As Cls_Privilege

                For i = 0 To (lstAffectObjects.Items.Count - 1)
                    obj1 = New Cls_Privilege(CLng(lstAffectObjects.Items(i).Value))

                    obj.removeObjets(obj1)
                Next

                ' FillListes(DDL_ListeALLTache.Items(DDL_ListeALLTache.SelectedIndex).Value, DLL_ListeObjectType.Items(DLL_ListeObjectType.SelectedIndex).Value)
                FillListes(DDL_ListeALLTache.Items(DDL_ListeALLTache.SelectedIndex).Value, DDLModule.Items(DDLModule.SelectedIndex).Value, DLL_ListeObjectType.Items(DLL_ListeObjectType.SelectedIndex).Value)
            Else
                Label_Msg.Text = "Pas d'Objet disponible pour suppression OU pas de Tâche."
            End If
        Catch ex As Rezo509Exception
            Label_Msg.Text = ex.Message
        End Try
    End Sub

    Private Sub btnRemoveOne_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveOne.Click
        Try
            Dim _item As ListItem
            Dim obj As Cls_Right
            Dim obj1 As Cls_Privilege

            If lstAffectObjects.SelectedIndex >= 0 And DDL_ListeALLTache.SelectedIndex >= 0 Then
                obj = New Cls_Right(CLng(DDL_ListeALLTache.Items(DDL_ListeALLTache.SelectedIndex).Value))
                For Each _item In lstAffectObjects.Items
                    If _item.Selected Then
                        obj1 = New Cls_Privilege(CLng(_item.Value))

                        obj.removeObjets(obj1)
                    End If
                Next

                ' FillListes(DDL_ListeALLTache.Items(DDL_ListeALLTache.SelectedIndex).Value, DLL_ListeObjectType.Items(DLL_ListeObjectType.SelectedIndex).Value)
                FillListes(DDL_ListeALLTache.Items(DDL_ListeALLTache.SelectedIndex).Value, DDLModule.Items(DDLModule.SelectedIndex).Value, DLL_ListeObjectType.Items(DLL_ListeObjectType.SelectedIndex).Value)
            Else
                Label_Msg.Text = "Pas d'Objet sélectionné pour suppression OU pas de Tâche."
            End If
        Catch ex As Rezo509Exception
            Label_Msg.Text = ex.Message
        End Try
    End Sub

#End Region

End Class
