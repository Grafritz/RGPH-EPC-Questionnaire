Imports RGPH_QCOLLECTE_Library

Partial Class Login
    Inherits Cls_BasePage

    Private _message As String
    Dim Cookies As HttpCookie
    Dim User_Connected As Cls_User

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Username_Connected()
        End If
        If Not IsPostBack Then
            'Load_SESSION_Informations_ByDefault()
            '' If Session([Global].Global_ECOLE_DEFAULT) Is Nothing Then
            'Load_Informations_ByDefault()
            ''End If
        End If
        '    Connexion("duverseau.jeanfritz@gmail.com", "admin")
    End Sub

    Public Sub Username_Connected()
        Try
            If Session([Global].GLOBAL_SESSION) IsNot Nothing Then
                User_Connected = CType(Session([Global].GLOBAL_SESSION), Cls_User)
                User_Connected.Activite_Utilisateur_InRezo("Login", "Login, System Entry", Request.UserHostName)
                User_Connected.Trace_LastLogin(Request.UserHostName)

                'ConnectedLoginChat(User_Connected.Username)

                REM si l'utilisateur doit change son mot de passe
                'If User_Connected.MustChangePassword Then
                '    Response.Redirect("~/Motdepasse.aspx?Action=Mustchangepassword")
                'End If

                ''FormsAuthentication.SetAuthCookie(User_Conneted.Username, False)
                ''FormsAuthentication.RedirectFromLoginPage(User_Conneted.Username, False)

                'If Request.QueryString("RedirectUrl") IsNot Nothing Then
                '    Response.Redirect(Request.QueryString("RedirectUrl"))
                'End If

                'Dim PageDefault As String = User_Connected.GroupeUser.PageDefault
                'Response.Redirect(PageDefault)
            End If

            ' Dim User_Conneted As Cls_User
            Dim CoookiesSupported As Boolean = Request.Browser.Cookies
            If Not CoookiesSupported Then
                ' Response.Write("<script> alert('Désolé !!! Votre navigateur ne supporte les cookies'); </script>")
                _message = "Désolé !!! Votre navigateur ne supporte les cookies"
                MessageToShow(_message)
            End If

            If Request.QueryString("SESSION") IsNot Nothing Then
                MessageToShow(Request.QueryString("SESSION"))
            End If

            Cookies = Request.Cookies([Global].GLOBAL_COOKIES)
            If Cookies IsNot Nothing Then
                Dim RedirectUrl As String = ""
                'If Request.QueryString("RedirectUrl") IsNot Nothing Then
                '    RedirectUrl = "?RedirectUrl=" & Request.QueryString("RedirectUrl")
                'End If
                'Response.Redirect("~/LockScreen.aspx" & RedirectUrl)
                username.Text = Cookies([Global].GLOBAL_COOKIES_USERNAME)
                'username.BackColor = Drawing.Color.Transparent
                'username.BorderColor = Drawing.Color.Transparent
                username.Font.Bold = True
                username.Enabled = False
                password.Focus()
                LinkButton_CeNestPasMonCompte.Visible = True
            Else
                username.Focus()
                LinkButton_CeNestPasMonCompte.Visible = False
            End If
        Catch ex As Exception
            MessageToShow(ex.Message)
        End Try

    End Sub

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

    Private Sub Connexion(ByVal txt_UserName As String, ByVal txt_MotdePasse As String)
        Dim _err As String = ""
        Try
            User_Connected = New Cls_User(txt_UserName, txt_MotdePasse)

            If User_Connected.ActifYN Then

                ' ''FormsAuthentication.RedirectFromLoginPage(txt_UserName, True)
                ''FormsAuthentication.SetAuthCookie(txt_UserName, False)
                ''FormsAuthentication.RedirectFromLoginPage(txt_UserName, False)

                User_Connected.Set_Status_ConnectedUser(True)
                User_Connected.Set_Status_IsForcedOut(False)

                Session.Add([Global].GLOBAL_SESSION, User_Connected)
                If CBX_GarderSession.Checked Then
                    REM Creation de Cookies
                    Dim cookie As HttpCookie
                    cookie = New HttpCookie([Global].GLOBAL_COOKIES)
                    cookie.Values.Add([Global].GLOBAL_COOKIES_IDUSER, User_Connected.ID)
                    cookie.Values.Add([Global].GLOBAL_COOKIES_USERNAME, User_Connected.Username)
                    Response.Cookies.Add(cookie)
                End If

                User_Connected.Activite_Utilisateur_InRezo("Login", "Login, System Entry", Request.UserHostName)
                User_Connected.Trace_LastLogin(Request.UserHostName)

                'ConnectedLoginChat(txt_UserName)

                REM si l'utilisateur doit change son mot de passe
                'If User_Connected.Mustchangepassword Then
                '    Response.Redirect("~/Motdepasse.aspx?Action=Mustchangepassword")
                'End If

                If Request.QueryString("RedirectUrl") IsNot Nothing Then
                    Response.Redirect(Request.QueryString("RedirectUrl"))
                End If

                'Dim PageDefault As String = User_Connected.GroupeUser.PageDefault
                'If Not PageDefault.Trim.Equals("") Then
                '    Response.Redirect(PageDefault)
                'Else
                Response.Redirect("~/GestionQuestionnaire/Frm_QuestionsListing.aspx")
                'End If

                'Panel_Msg.Visible = True
            Else
                _message = "Votre compte n'est pas active "
                'If Not User_Connected.Confirmer Then
                '    _message &= "Verifiez votre email "
                'End If
                Session.Remove([Global].GLOBAL_SESSION)
                MessageToShow(_message)
            End If
        Catch ex As Exception
            MessageToShow(ex.Message)
        End Try
    End Sub

    Protected Sub btn_connection_Click(sender As Object, e As EventArgs) Handles btn_connection.Click
        Connexion(username.Text, password.Text)
    End Sub

    Protected Sub LinkButton_CeNestPasMonCompte_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton_CeNestPasMonCompte.Click
        Cookies = Request.Cookies([Global].GLOBAL_COOKIES)
        Response.Cookies([Global].GLOBAL_COOKIES).Expires = DateTime.Now.AddDays(-1)
        Session.RemoveAll()
        Session([Global].GLOBAL_SESSION) = Nothing
        Response.Redirect("~/Login.aspx")
    End Sub
End Class
