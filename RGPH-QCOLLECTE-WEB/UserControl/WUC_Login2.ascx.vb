' @Brain Development
' Jeudi 06-07-2017

Imports BRAIN_DEVLOPMENT
Imports System.IO
Imports RGPH_QCOLLECTE_Library

Partial Class UserControl_WUC_Login2
    Inherits System.Web.UI.UserControl


    Private _message As String
    Dim Cookies As HttpCookie
    Dim User_Connected As Cls_User
    Private _IsSendGOOD As Boolean = False

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Panel_Msg.Visible = False
        'DIV_ImageUser.Visible = False
        If Not IsPostBack Then
            CBX_GarderSession.Text = [Global].Label_GarderSession
            LinkButton_CeNestPasMonCompte.Text = [Global].Label_CeNestPasMonCompte
            'HyperLinkAskPassword.Text = [Global].Label_AskPassword
            Username_Connected()
        End If
    End Sub

    Public Sub Username_Connected()
        Try
            If Session([Global].GLOBAL_SESSION) IsNot Nothing Then
                User_Connected = CType(Session([Global].GLOBAL_SESSION), Cls_User)

                Dim userName As String = User_Connected.Username
                Dim user() As String = userName.Split(",")
                If user.Count > 1 Then
                    userName = user(0)
                End If

                txt_UserName.Text = userName
                txt_UserName.Font.Bold = True
                txt_UserName.Attributes.Add("readonly", "readonly")
                txt_MotdePasse.Focus()
                LinkButton_CeNestPasMonCompte.Visible = True
            Else
                Dim CoookiesSupported As Boolean = Request.Browser.Cookies
                If Not CoookiesSupported Then
                    ' Response.Write("<script> alert('Désolé !!! Votre navigateur ne supporte les cookies'); </script>")
                    _message = "Désolé !!! Votre navigateur ne supporte les cookies"
                    MessageToShow(_message)
                End If

                Cookies = Request.Cookies([Global].GLOBAL_COOKIES)
                If Cookies IsNot Nothing Then
                    Dim RedirectUrl As String = ""

                    Dim userName As String = Cookies([Global].GLOBAL_COOKIES_USERNAME)
                    Dim user() As String = userName.Split(",")
                    If user.Count > 2 Then
                        userName = user(0)
                    End If

                    txt_UserName.Text = userName
                    txt_UserName.Font.Bold = True
                    txt_UserName.Attributes.Add("readonly", "readonly")
                    txt_MotdePasse.Focus()
                    LinkButton_CeNestPasMonCompte.Visible = True
                Else
                    txt_UserName.Focus()
                    LinkButton_CeNestPasMonCompte.Visible = False
                End If
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
        End Try

    End Sub

#Region "Other Method - MessageToShow"
    Private Sub MessageToShow(ByVal _message As String, Optional ByVal E_or_S As String = "E")
        Panel_Msg.Visible = True
        GlobalFunctions.Message_Image(Image_Msg, E_or_S)
        Label_Msg.Text = _message
        'RadAjaxManager1.ResponseScripts.Add("alert('" & [Global].GetTextFromHtml(_message).Replace("'", "\'") & "');")
        Dialogue.alert([Global].GetTextFromHtml(_message))
        If E_or_S = "S" Then
            Style_Division(DIV_Msg, "alert alert-success alert-dismissable")
            Style_Division(Icon_Msg, "fa  fa-thumbs-up")
        Else
            Style_Division(DIV_Msg, "alert alert-danger alert-dismissable")
            Style_Division(Icon_Msg, "fa fa-thumbs-down")
        End If
    End Sub
#End Region

    Private Sub Connexion(ByVal txt_UserName As String, ByVal txt_MotdePasse As String)
        Dim _err As String = ""
        Try
            Dim usern As String() = txt_UserName.Replace(";;;", ",,,").Split(",,,")
            If usern.Count > 1 Then
                'User_Connected = New Cls_User(txt_UserName, txt_MotdePasse)
                User_Connected = New Cls_User(1, usern(0), txt_MotdePasse)
            Else
                User_Connected = New Cls_User(txt_UserName, txt_MotdePasse)
            End If

            'User_Connected = New Cls_User(txt_UserName, txt_MotdePasse)

            If User_Connected.ActifYN Then
                'If Not User_Connected.Confirmer Then
                '_message = "Confirmez votre adresse e-mail avant de pourvoir vous connecter. <br /><br />Un e-mail de confirmation a été envoyé à <b>" & User_Connected.Username & "</b>"
                'MessageToShow(_message)
                REM ENVOI EMAIL DE CONFIRMATION
                'Send_InfoCompte_ForConfirmation(User_Connected, User_Connected.DETAILS_USER_HTML, "Veuillez confirmer l'adresse e-mail de votre compte Rezo509  ")

                'DIV_ImageUser.Visible = True
                'Image_ImageUser.ImageUrl = "~/Show_Image.aspx?Action=User&ID=" & User_Connected.ID
                'Else
                'CheckLicence()

                'FormsAuthentication.SetAuthCookie(txt_UserName, True)
                'FormsAuthentication.RedirectFromLoginPage(txt_UserName, True)

                User_Connected.Set_Status_ConnectedUser(True)
                User_Connected.Set_Status_IsForcedOut(False)

                Session([Global].GLOBAL_SESSION) = Nothing
                Session.Add([Global].GLOBAL_SESSION, User_Connected)
                If CBX_GarderSession.Checked Then
                    REM Creation de Cookies
                    Dim cookie As HttpCookie
                    cookie = New HttpCookie([Global].GLOBAL_COOKIES)
                    cookie.Values.Add([Global].GLOBAL_COOKIES_IDUSER, User_Connected.ID)
                    cookie.Values.Add([Global].GLOBAL_COOKIES_USERNAME, User_Connected.Username)
                    Response.Cookies.Add(cookie)

                    'FormsAuthentication.SetAuthCookie(txt_UserName, True)
                    'FormsAuthentication.RedirectFromLoginPage(txt_UserName, True)
                End If

                User_Connected.Activite_Utilisateur_InRezo("Login", "Login, System Entry", Request.UserHostName)
                User_Connected.Trace_LastLogin(Request.UserHostName)

                REM PLAN REZO509
                'Dim plan As New Cls_PL_Rezo509PlanUser
                'plan.SelectPlanUser_ByIDUser(User_Connected.ID)
                'If plan.ID > 0 Then
                '    Session([Global].GLOBAL_SESSION_PLAN) = Nothing
                '    Session.Add([Global].GLOBAL_SESSION_PLAN, plan)
                'End If

                'If User_Connected.ID_Group = Cls_User.Groupe_Utilisateur.Institution Then
                '    If Cls_Institution.Get_Count_ByIDUser(User_Connected.ID) = 0 Then
                '        Response.Redirect("~/Presentations/MyInstitutionListing.aspx", True)
                '    End If
                'End If

                'REM si l'utilisateur doit change son mot de passe
                'If User_Connected.Mustchangepassword Then
                '    'Response.Redirect("~/Presentations/DashProfilUtilisateur.aspx?page=tab_FicheMotDePasse", True)
                'End If

                'If Request.QueryString("RedirectUrl") IsNot Nothing Then
                '    'Response.Redirect(Request.QueryString("RedirectUrl"), True)
                'End If

                'Dim PageDefault As String = User_Connected.GroupeUser.PageDefault
                'Response.Redirect(PageDefault, True)

                'Panel_Msg.Visible = True

                If Session([Global].GLOBAL_PAGENAME) IsNot Nothing Then
                    Response.Redirect(Session([Global].GLOBAL_PAGENAME), True)
                End If
                'FormAddCampagne.Attributes.Add("style", "perspective: none;")
                'FormAddCampagne.Attributes.Add("class", "md-modal colored-header custom-width md-effect-12 md-show")
                'End If

            Else
                _message = "Votre compte n'est pas active "
                'If Not User_Connected.Confirmer Then
                '    _message &= "Verifiez votre email "
                'End If
                Session.Remove([Global].GLOBAL_SESSION)
                MessageToShow(_message)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex)
        End Try
    End Sub

    Protected Sub Btn_Connexion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_connection.Click
        Connexion(txt_UserName.Text, txt_MotdePasse.Text)
    End Sub

    Protected Sub LinkButton_CeNestPasMonCompte_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton_CeNestPasMonCompte.Click
        Try
            Cookies = Request.Cookies([Global].GLOBAL_COOKIES)
            Response.Cookies([Global].GLOBAL_COOKIES).Expires = DateTime.Now.AddDays(-1)
            Session.RemoveAll()
            Session([Global].GLOBAL_SESSION) = Nothing
            'Session([Global].GLOBAL_SESSION_PARTICIPANT) = Nothing
            Response.Redirect("~/Login.aspx", True)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex)
        End Try
    End Sub

End Class
