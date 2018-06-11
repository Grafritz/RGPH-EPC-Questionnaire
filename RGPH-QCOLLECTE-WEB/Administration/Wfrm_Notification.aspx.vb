Imports RGPH_QCOLLECTE_Library
Imports BRAIN_DEVLOPMENT

Partial Class Administration_Wfrm_Notification
    Inherits Cls_BasePage

    Private _message As String = ""
    Private Info As String = ""
    Private _out As Boolean = False
    Private Const Nom_page As String = "Wfrm_User.aspx"
    Private Const Btn_SaveEdit As String = "Btn_SaveEditUser"

    Private User_Connected As Cls_User
    Public User_Connected_ID As Long = 0
    Private Is_Acces_Page As Boolean = True
    Private GetOut As Boolean = False
    Private IsSendMail As Boolean = True

    Dim SESSION_EMAI_LIST As String = "qqqq" '[Global].SESSION_EMAIL_LIST

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Panel_Msg.Visible = False

            '--- Si l'utilisateur a Acces a la page 
            SYSTEME_SECURITE()

            If Is_Acces_Page Then
                If Not IsPostBack Then
                    Btn_Annuler.Attributes.Add("onclick", "javascript:void(closeWindow());")
                    LOADAll_DATA()
                End If
            End If
        Catch ex As Rezo509Exception
            _message = ex.Message
            MessageToShow(_message)
            _message = " Methode -> [ Page_Load ] :" & ex.Message
            [Global].WriteError(_message, User_Connected)
        End Try
    End Sub

#Region "SECURITE"
    Public Sub SYSTEME_SECURITE()
        Try
            User_Connected = [Global].KeepUserContinuesToWork(User_Connected)

            If Session([Global].GLOBAL_SESSION) IsNot Nothing Then
                User_Connected = CType(Session([Global].GLOBAL_SESSION), Cls_User)

                Me.Session("User_Connected_ID") = User_Connected.ID
                User_Connected_ID = User_Connected.ID

                SESSION_EMAI_LIST = "SESSION_EMAI_LIST" + User_Connected.ID.ToString

                If Not Cls_Privilege.VerifyRightOnObject(Nom_page, User_Connected.ID_Group) Then
                    _message = [Global].NO_ACCES_PAGE
                    MessageToShow(_message)
                    Is_Acces_Page = False

                    Panel_NotificationSendEmail.Visible = False
                    '--- Sorry vous n'avez pas Acces a la page 
                    'RemoveHandler Btn_SendNotification.Click, AddressOf Btn_PosterAnnonce_Click
                    'RemoveHandler Btn_Annuler_Annonces.Click, AddressOf Btn_Annuler_Annonces_Click
                Else
                    '---  Okey vous avez acces a la page ---'
                    Dim _check As Boolean = Cls_Privilege.VerifyRightOnObject(Btn_SaveEdit, User_Connected.ID_Group)

                    Btn_SendNotification.Visible = _check
                    REM Privilege
                    'RadGrid_1.MasterTableView.Columns.FindByUniqueNameSafe("editer").Visible = _check
                    'RadGrid_1.MasterTableView.Columns.FindByUniqueNameSafe("delete").Visible = Cls_Privilege.VerifyRightOnObject(Btn_Delete, User_Connected.ID_Group)
                End If
            End If
        Catch ex As Rezo509Exception
            _message = ex.Message
            MessageToShow("SYSTEME_SECURITE: " + _message)
            GetOut = True
        End Try

        If Session([Global].GLOBAL_SESSION) Is Nothing Then
            '-- Session expirée --'
            GetOut = True
        Else
            Try
                User_Connected = CType(Session([Global].GLOBAL_SESSION), Cls_User)
                If Not (GlobalFunctions.IsUserStillConnected(User_Connected) And GlobalFunctions.IsUserStillActive(User_Connected)) Then
                    User_Connected.Set_Status_ConnectedUser(False)
                    User_Connected.Activite_Utilisateur_InRezo("Forced Log Off", "Forced to Log Off", Request.UserHostAddress)

                    Session.RemoveAll()
                    _message = "Session expirée."
                    MessageToShow(_message)
                    Is_Acces_Page = True
                    GetOut = True
                End If
            Catch ex As Rezo509Exception
                _message = "Session expirée."
                MessageToShow(_message)
                GetOut = True
            End Try
        End If
        If GetOut Then
            Response.Redirect([Global].PAGE_LOGIN)
        End If
    End Sub
#End Region

#Region "Other Method - MessageToShow"
    Private Sub MessageToShow(ByVal _message As String, Optional ByVal E_or_S As String = "E")
        Panel_Msg.Visible = True
        GlobalFunctions.Message_Image(Image_Msg, E_or_S)
        Label_Msg.Text = _message

        Dialogue.alert([Global].GetTextFromHtml(_message))
        'RadAjaxManager1.ResponseScripts.Add("alert('" & _message.Replace("'", "\'") & "');")
        If E_or_S = "S" Then
            Style_Division(DIV_Msg, "alert alert-success alert-dismissable")
            Style_Division(Icon_Msg, "fa  fa-thumbs-up")
        Else
            Style_Division(DIV_Msg, "alert alert-danger alert-dismissable")
            Style_Division(Icon_Msg, "fa fa-thumbs-down")
        End If
    End Sub
#End Region

#Region "LOAD All DATA"
    Public Sub LOADAll_DATA()
        LoadInfoUser()
    End Sub

#End Region

    Private Sub LoadInfoUser()
        Try
            'Session(SESSION_EMAI_LIST) = Nothing

            If Request.QueryString("ID") IsNot Nothing Then
                Dim _id As Long = TypeSafeConversion.NullSafeLong(Request.QueryString("ID"))

                REM list email vide
                Dim userList As New List(Of Cls_User)
                Dim userListNew As New List(Of Cls_User)

                Dim obj As New Cls_User(_id)
                Dim email As String = ""
                If obj.ID > 0 Then
                    'Label_EmailDestination.Text = obj.NOM_COMPLET_AND_USERANAME
                    'txt_Notes.Text = obj.NOM_COMPLET_AND_USERANAME
                    ' Add to Session
                    If Session(SESSION_EMAI_LIST) Is Nothing Then
                        userListNew.Add(obj)
                        Me.Session(SESSION_EMAI_LIST) = userListNew
                    Else
                        userList = CType(Session(SESSION_EMAI_LIST), List(Of Cls_User))
                        If userList IsNot Nothing Then
                            Dim isAddNew As Boolean = True
                            Session(SESSION_EMAI_LIST) = Nothing
                            For Each item As Cls_User In userList
                                userListNew.Add(item)
                                If item.ID = obj.ID Then
                                    isAddNew = False
                                    'userListNew.Add(obj)
                                    'Else
                                    'userListNew.RemoveAt(item.ID)
                                End If
                            Next
                            If isAddNew Then
                                userListNew.Add(obj)
                            End If
                            Session(SESSION_EMAI_LIST) = userListNew
                        End If
                    End If
                    '' Extraction des informations de la session
                    Dim count As Integer = 0
                    If Me.Session(SESSION_EMAI_LIST) IsNot Nothing Then
                        userList = CType(Session(SESSION_EMAI_LIST), List(Of Cls_User))
                        If userList IsNot Nothing Then
                            email = ""
                            For Each item As Cls_User In userList
                                If email.Equals("") Then
                                    email = item.NOM_COMPLET_AND_USERANAME
                                Else
                                    email &= ", " + item.NOM_COMPLET_AND_USERANAME
                                End If
                                count += 1
                            Next
                        End If
                    End If
                    Label_EmailDestination.Text = "" + count.ToString
                    txt_EmailTo.Text = email.ToString
                    txt_Notes.Text = email.ToString
                End If
            End If
        Catch ex As Rezo509Exception
            _message = ex.Message
            MessageToShow(_message)
            _message = " Methode -> [ LoadInfoUser ] :" & ex.Message
            [Global].WriteError(_message, User_Connected)
        End Try
    End Sub

    Protected Sub Btn_SendNotification_Click(sender As Object, e As EventArgs) Handles Btn_SendNotification.Click
        Me.Session(SESSION_EMAI_LIST) = Nothing
    End Sub

    Protected Sub Btn_Annuler_Click(sender As Object, e As EventArgs) Handles Btn_Annuler.Click

    End Sub
End Class
