' @Brain Development
' www.tdb.rezo509.com
' Jeudo 12-09-2013

Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer
Imports Microsoft.VisualBasic
Imports System.Collections
Imports System.Configuration
Imports System.Collections.Generic
Imports System
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.Mail
Imports System.Web.Security
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls

'Imports System.Collections
Imports System.ComponentModel
Imports System.Web.SessionState
Imports System.Security.Principal
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports RGPH_QCOLLECTE_Library

Public Class [Global]
    Inherits System.Web.HttpApplication
    Public Const Global_APP_NAME_SIGLE = "RGPH - QUESTIONNAIRE"

    Public Const GLOBAL_Langue_Preferer = "Langue_Preferer"
    Public Const GLOBAL_Francais = "fr-FR"
    Public Const GLOBAL_Anglais = "en-US"
    Public Const GLOBAL_Espagnol = "es-ES"
    Public Const GLOBAL_Creole = "Ht-HT"

    Public Const GLOBAL_URL_PAGE = "GLOBAL_URL_PAGE-SYGED"
    Public Const GLOBAL_PAGENAME = "GLOBAL_PAGENAME-SYGED"
    Public Const GLOBAL_SESSION = "User_Conneted-QUESTIONNAIRE"
    Public Const GLOBAL_COOKIES = "User_Conneted-QUESTIONNAIRE"

    Public Const GLOBAL_COOKIES_USERNAME = "COOKIES_USERNAME"
    Public Const GLOBAL_COOKIES_IDUSER = "IDUSERIHSI"

    Public Shared PAGE_LOGIN As String = "~/Login.aspx?RedirectUrl=" & System.Web.HttpContext.Current.Request.Url.ToString()
    Public Shared PAGE_LOGIN_2 As String = "~/Login.aspx"
    Public Const NO_ACCES_PAGE As String = "Désolé vous n'avez pas accès à ce service. Contactez l'administrateur."
    Public Const MAIL_NOT_SENT As String = "Désolé, Une erreur est survenue, vérifiez votre connexion internet ..."

    Public Shared SMTPServer As String = System.Configuration.ConfigurationManager.AppSettings("SMTPServer")
    Public Shared EmailFrom As String = System.Configuration.ConfigurationManager.AppSettings("EmailFrom")
    Public Shared EmailPassword As String = SqlHelperParameterCache.DecryptGbl(System.Configuration.ConfigurationManager.AppSettings("EmailPassword"))


    Public Shared PathLogo As String = "<img src='http://portducaphaitien.rezo509.com/images/logo_pch_transparent.png' alt='' border='0' style='width:200px;' />"

    REM POUR TOUTES LES PAGES
    Public Const Label_GrandTitre_Right = "{0} <small class='badge  {1}'>{2}</small>"
    Public Const Label_Icon_Text = "<i class='fa {0}'></i> {1} "
    Public Const Label_Icon_Text_Badge_Counter = "<i class='fa {0}'></i> {1} <small class='{2}'>{3}</small>"
    Public Const Label_GrandTitreMenuVertical = "{0} <small class='badge pull-right {1}'>{2}</small>"
    Public Const Label_GrandTitreMenuVertical_CleanZone = "<span class='{1}'>{2}</span> {0} "
    Public Const Label_SousTitreMenuVertical1 = "<i class='fa fa-angle-double-right'></i><i class='fa {0}'></i>{1}"
    Public Const Label_SousTitreMenuVertical4 = "<i class='fa fa-angle-double-right'></i><i class='fa {0}'></i>{1}<small class='badge pull-right {2}'>{3}</small>"
    Public Const Label_SousTitreMenuVertical3 = "<i class='fa fa-angle-double-right'></i>{0} <small class='badge pull-right {1}'>{2}</small>"
    Public Const Label_StyleTitreMenuTDB = "<h2 style='margin: 0px 0px;'>{1}</h2> <p>{0}</p>"

    Public Const Msg_Confirmation_Supprimee_Information = "Êtes-vous sûr de vouloir supprimer cette information ?"
    Public Const Msg_Information_Supprimee_Avec_Succes = "Informations supprimée avec succès <i class='fa fa-thumbs-o-up' ></i>"
    Public Const Msg_Enregistrement_Effectue = " Enregistrement effectué avec succès <i class='fa fa-thumbs-o-up' ></i>"
    Public Const Msg_Demande_Effectue = " Demande tranférée avec succès <i class='fa fa-thumbs-o-up' ></i>"

    Public Const Nom_pageUtilisateur As String = "Wfrm_User.aspx"
    Public Const Msg_Thread_Abandonne = "Le thread a été abandonné."
    Public Const Msg_Thread_Abandonne_Anglais = "Thread was being aborted."

    'Public Shared PathLogo As String = "<img src='" & LinkSite & "images/logo_TDB.png' alt='' border='0' style='width:200px;' />"
    'Public Shared PathLogoReunion As String = "<img src='" & LinkSite & "images/logo_SDR.png' alt='' border='0' style='width:200px;' />"
    'Public Shared PathImageCalendrier As String = "<img src='" & LinkSite & "images/schedule32.png' alt='' border='0' style='width:30px;' />"

#Region "VARIABLE MESSAGED"
    Public Const DATA_MODULE = "DATA_MODULE"
    Public Const DATA_QUESTION_MODULE = "DATA_QUESTION_MODULE"
    Public Const DATA_QUESTION = "DATA_QUESTION"
    Public Const DATA_REPONSE = "DATA_REPONSE"
    Public Const DATA_CATEGORIE_QUESTION = "DATA_CATEGORIE_QUESTION"
    Public Const DATA_MODULE_QUESTION_REPONSE_PAR_MODULE = "DATA_MODULE_QUESTION_REPONSE_PAR_MODULE"
    Public Const DATA_MODULE_QUESTION_SESSION = "DATA_MODULE_QUESTION_SESSION"
    Public Const DATA_MODULE_REPONSES_SESSION = "DATA_MODULE_REPONSES_SESSION"
    Public Const DATA_MODULE_SESSION = "DATA_MODULE_SESSION"
    Public Const DATA_QUESTION_MODULE_SESSION = "DATA_QUESTION_MODULE_SESSION"



    Public Const PARAM_ID_PAIEMENT = "IDP"
    Public Const PHOTO_ECOLE = "PHOTO_ECOLE"
    Public Const PHOTO_UTILISATEUR = "PHOTO_UTILISATEUR"
    Public Const PHOTO_ELEVE = "PHOTO_ELEVE"
    Public Const PHOTO_ELEVE_INSCRIT = "PHOTO_ELEVE_INSCRIT"
    Public Const PHOTO_PROFESSEUR = "PHOTO_PROFESSEUR"
    Public Const htmlMasterPage = "htmlMasterPage"
    Public Const bodyMasterPage = "bodyMasterPage"
    Public Const ACTION = "ACTION"
    Public Const PAGE_MERE = "PAGE_MERE"
    Public Const HideMenuHeader = "HideMenuHeader"
    Public Const Head_Nav_Menu = "Head_Nav_Menu"
    Public Const Head_Nav_MenuVertical = "Head_Nav_MenuVertical"

    Public Const EDIT_MATERIEL_DIDACTIQUE_PROFESSEUR_RETOURNE = "Bouton-EDIT-MATERIEL-DIDACTIQUE-PROFESSEUR-RETOURNE"
    Public Const EDIT_MATERIEL_DIDACTIQUE_ETUDIANT_RETOURNE = "Bouton-EDIT-MATERIEL-DIDACTIQUE-ETUDIANT-RETOURNE"
    Public Const PRE_ID_PERS_RESP_ETUDIANT = "PRE_ID_PERS_RESP_ETUDIANT"
    Public Const PARAM_ID_ETUDIANT = "ID_ETUDIANT"
    Public Const PARAM_ID_PROFESSEUR = "ID_PROFESSEUR"
    Public Const ID_USER = "ID_USER"
    Public Const PARAM_ID_CLASSE = "ID_CLASSE"
    REM PAGE Login.aspx
    Public Const Label_SeConnecter = "<i class='fa fa-lock'></i>  Se Connecter"
    Public Const Label_GarderSession = "Garder ma session ouverte"
    Public Const Label_CeNestPasMonCompte = "<i class='fa fa-unlock'></i> Connecter avec un compte différent"
    Public Const Label_AskPassword = "<i class=""fa fa-unlock-alt""></i> J'ai oublié mon mot de passe"

    Public Const Accueil_Default = 1
    Public Const GestionEtudiant_Frm_EtudiantListing = 2
    Public Const GestionEtudiant_Frm_Etudiant = 3
    Public Const GestionProfesseurListing = 4

    Public Shared URL_PCH As String = getServerName_ForOnline("PCH")
#End Region

    Public Shared Function getHostServerName() As String
        Dim Host As String = HttpContext.Current.Request.Url.Host
        Dim strUrl As String = ""
        Dim VirtualPath As String = HttpRuntime.AppDomainAppVirtualPath
        If Not VirtualPath.Trim.Equals("") Then
            strUrl = Host
            strUrl &= VirtualPath
        Else
            strUrl = Host
            strUrl &= VirtualPath
        End If
        Return strUrl
    End Function

    Public Shared Function getServerName_ForOnline(ByVal _APP As String) As String
        Dim strPathAndQuery As String = HttpContext.Current.Request.Url.PathAndQuery
        Dim strUrl As String = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/") '.Replace("http://localhost", "http://127.0.0.1")
        Dim VirtualPath As String = System.Web.HttpRuntime.AppDomainAppVirtualPath.Replace("/", "")
        If Not VirtualPath.Trim.Equals("") Then
            Select Case _APP.ToUpper
                Case "PCH"
                    strUrl &= "PCH/"
            End Select
        Else
            Select Case _APP.ToUpper
                Case "PCH"
                    strUrl = "http://portducaphaitien.rezo509.com/"
            End Select
        End If
        Return strUrl
    End Function

    Public Shared Function GetPath_PageMere(ByVal pageMere As Long) As String
        Dim page As String = "~/Default.aspx"
        Select Case pageMere
            Case Accueil_Default, 0
                Return "~/Default.aspx"
            Case GestionEtudiant_Frm_EtudiantListing
                Return "~/GestionEtudiant/Frm_EtudiantListing.aspx"
            Case GestionEtudiant_Frm_Etudiant
                Return "~/GestionEtudiant/Frm_Etudiant.aspx"

            Case GestionProfesseurListing
                Return "~/GestionProfesseur/Frm_ProfesseurListing.aspx"
        End Select
        Return page
    End Function

    Public Shared Sub WriteError(ByVal Message As String)
        Dim _User As String = "..."
        ErreurLog.WriteError("[" & _User & " ] : " & Message)
    End Sub

    Public Shared Sub WriteError(ByVal Message As String, ByVal User_Connected As Cls_User, Optional ByVal IS_SendMail As Boolean = True)
        Dim _User As String = "..."
        If User_Connected IsNot Nothing Then
            _User = User_Connected.Username
        End If
        ErreurLog.WriteError("[" & _User & " ] : " & Message)
        If IS_SendMail Then
            GlobalFunctions.Send_Mail_Erreur("[ " & _User & " ] : OPC ", Message)
        End If
    End Sub

    Public Shared Sub WriteError(ByVal Message As Exception, Optional ByVal User_Connected As Cls_User = Nothing, Optional ByVal IS_SendMail As Boolean = True)
        Dim _User As String = "..."
        If User_Connected IsNot Nothing Then
            _User = User_Connected.Username
        End If
        ErreurLog.WriteError("[" & _User & " ] : " & Message.Message & Chr(13) & Chr(13) & Message.ToString)
        If IS_SendMail Then
            'If ex Is ThreadAbortException Then

            'End If
            If Not Message.Message.Contains(Msg_Thread_Abandonne) OrElse Not Message.Message.Contains(Msg_Thread_Abandonne_Anglais) Then
                GlobalFunctions.Send_Mail_Erreur("[ " & _User & " ] : " & [Global].Global_APP_NAME_SIGLE & " ", Message.Message & Chr(13) & Chr(13) & Message.ToString)
            End If
        End If
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
        If Not Session(GLOBAL_SESSION) Is Nothing Then
            Try
                Dim _currentUser As Cls_User
                _currentUser = CType(Session(GLOBAL_SESSION), Cls_User)
                ErreurLog.WriteError("[Global.asax] --> Disconnecting " & _currentUser.Username)
                _currentUser.Activite_Utilisateur_InRezo("Log Off", "Log Off Automatic, System Exit", "Server")
                _currentUser.Set_Status_IsForcedOut(False)
                Cls_User.Disconnect_User_ById(_currentUser.ID)
            Catch ex As Exception
                ErreurLog.WriteError("[Global.asax] --> " & ex.Message)
            End Try
        Else
            'nada
        End If

        Session.RemoveAll()
    End Sub

    Public Shared Function KeepUserContinuesToWork(ByVal User_Connected As Cls_User) As Cls_User
        If HttpContext.Current.Session([Global].GLOBAL_SESSION) Is Nothing Then
            Dim Cookies As HttpCookie = HttpContext.Current.Request.Cookies([Global].GLOBAL_COOKIES)
            If Cookies IsNot Nothing Then
                User_Connected = New Cls_User(TypeSafeConversion.NullSafeLong(Cookies([Global].GLOBAL_COOKIES_IDUSER)))
                HttpContext.Current.Session.Add([Global].GLOBAL_SESSION, User_Connected)
            Else
                'User_Connected = New Cls_User("duverseau.jeanfritz@gmail.com", "admin")
                'HttpContext.Current.Session.Add([Global].GLOBAL_SESSION, User_Connected)
            End If
        Else
            'User_Connected = New Cls_User("duverseau.jeanfritz@gmail.com", "admin")
            'HttpContext.Current.Session.Add([Global].GLOBAL_SESSION, User_Connected)
        End If
        Return User_Connected
    End Function


    REM verification de l'extention du fichier de la Photo
    Public Shared Sub Is_Good_Extension(ByVal _FileUpload_ As FileUpload)
        Dim Liste_Ext() As String = Split(".jpg,.png,.bmp,.jpeg,.gif", ",")
        Dim Ext_Ok As Boolean = False
        REM 1Kb = 1024b  |-|  1Mb = 1024Kb  |-|   1Mb = 1048576 byte |-|  50Kb = 51200b
        If _FileUpload_.PostedFile.ContentLength > 409600 Then ' Taille limite 400Kb
            'IS_SendMail = False
            Throw New Rezo509Exception("La taille de l'image est trop volumineuse ! Taille limite 400 Kilo-octet [400Kb]")
        End If
        Dim _GetExtension As String = System.IO.Path.GetExtension(_FileUpload_.FileName).ToLower

        For i As Integer = 0 To Liste_Ext.Length - 1
            If _GetExtension.Trim.ToLower = Liste_Ext(i).Trim.ToLower Then
                Ext_Ok = True
                Exit For
            End If
        Next
        If Not Ext_Ok Then
            'IS_SendMail = False
            Throw New Rezo509Exception("Ce fichier n'est pas une image de type (gif, jpg, jpeg, png, bmp)...")
        End If
    End Sub

    Public Shared Sub Is_Good_Extension(ByVal _FileUpload_Annonce As FileUpload, ByVal Typeas As String)
        Dim listeExt As String = System.Configuration.ConfigurationManager.AppSettings("Extention_DocumentFile").ToString
        Dim Liste_Ext() As String = Split(listeExt, ",") ''Split(".jpg,.png,.bmp,.jpeg,.gif", ",")
        Dim Ext_Ok As Boolean = False
        REM 1Ko = 1024 octet  |-|  1Mo = 1024Ko  |-|   1Mo = 1048576 byte |-|  50Ko = 51200o
        Dim SizeImage As Integer = 51200 ' = 50Kb
        Dim SizeStr As String = "50 Kb"
        REM Dimension pour le document
        SizeImage = TypeSafeConversion.NullSafeLong(System.Configuration.ConfigurationManager.AppSettings("MaxSize_DocumentFile")) ' TAILLE FICHIER 5Mb = 5 000 000 bit
        SizeStr = System.Configuration.ConfigurationManager.AppSettings("MaxSize_DocumentFile_Str").ToString

        REM Taille Illimite ou pas //  Si Taille limite
        If _FileUpload_Annonce.PostedFile.ContentLength > SizeImage Then ' Taille limite 
            'IS_SendMail = False
            Throw New Rezo509Exception("La taille du document est trop volumineux ! Taille limite " & SizeStr)
        End If
        Dim _GetExtension As String = System.IO.Path.GetExtension(_FileUpload_Annonce.FileName).ToLower
        For i As Integer = 0 To Liste_Ext.Length - 1
            If _GetExtension.Trim.ToLower = Liste_Ext(i).Trim.ToLower Then
                Ext_Ok = True
                Exit For
            End If
        Next
        If Not Ext_Ok Then
            'IS_SendMail = False
            Throw New Rezo509Exception("Ce fichier n'est pas un document de type (" & listeExt & ")...")
        End If
    End Sub

    ''' <summary>Removes the tags from an HTML document.</summary>
    ''' <param name="htmlText">HTML text to parse.</param>
    ''' <returns>The text of an HTML document without tags.</returns>
    ''' <remarks></remarks>
    ''' 
    Public Shared Function GetTextFromHtml(ByVal htmlText As String) As String
        Dim output As String = Regex.Replace(htmlText, "\<[^\>]+\>", "")
        Return output
    End Function

    Public Shared Sub LoadStatut(ByVal _listElement() As String, ByVal _RCB As Telerik.Web.UI.RadComboBox)
        _RCB.Items.Clear()
        For i As Integer = 0 To _listElement.Length - 1
            With _RCB
                '.Items.Insert(0, New RadComboBoxItem(" - Choisir -", ""))
                _RCB.Items.Add(New RadComboBoxItem(_listElement(i), i + 1))
            End With
        Next
        _RCB.Items.Sort()
        _RCB.Items.Insert(0, New RadComboBoxItem(" - Choisir -", ""))
        _RCB.SelectedIndex = 0
        _RCB.EmptyMessage = "- Choisir -"
    End Sub

    Public Shared Sub LoadStatut(ByVal _list() As String, ByVal _ddl As DropDownList, Optional ByVal putDefaultValue As Boolean = False)
        _ddl.Items.Clear()
        If putDefaultValue Then
            _ddl.Items.Add(New ListItem("- Choisissez (" & _list.Length & ") -", 0))
        End If

        For i As Integer = 0 To _list.Length - 1
            _ddl.Items.Add(New ListItem(_list(i), i + 1))
        Next
    End Sub
End Class

Public Module GlobalFunctions

    Public Sub LoadStatut(ByVal _listElement() As String, ByVal _RCB As Telerik.Web.UI.RadComboBox)
        _RCB.Items.Clear()
        For i As Integer = 0 To _listElement.Length - 1
            With _RCB
                '.Items.Insert(0, New RadComboBoxItem(" - Choisir -", ""))
                _RCB.Items.Add(New RadComboBoxItem(_listElement(i), i + 1))
            End With
        Next
    End Sub

    Public Sub LoadStatut(ByVal _list() As String, ByVal _ddl As DropDownList)
        _ddl.Items.Clear()
        For i As Integer = 0 To _list.Length - 1
            _ddl.Items.Add(New ListItem(_list(i), i + 1))
        Next
    End Sub

    REM verification de l'extention du fichier de la Photo
    Public Sub Is_Good_Extension(ByVal _FileUpload_ As FileUpload)
        Dim Liste_Ext() As String = Split(".jpg,.png,.bmp,.jpeg,.gif", ",")
        Dim Ext_Ok As Boolean = False
        REM 1Kb = 1024b  |-|  1Mb = 1024Kb  |-|   1Mb = 1048576 byte |-|  50Kb = 51200b
        If _FileUpload_.PostedFile.ContentLength > 204800 Then ' Taille limite 200Kb
            'IS_SendMail = False
            Throw New Exception("La taille de l'image est trop volumineuse ! Taille limite 200 Kilo-octet [200Kb]")
        End If
        Dim _GetExtension As String = System.IO.Path.GetExtension(_FileUpload_.FileName).ToLower

        For i As Integer = 0 To Liste_Ext.Length - 1
            If _GetExtension.Trim.ToLower = Liste_Ext(i).Trim.ToLower Then
                Ext_Ok = True
                Exit For
            End If
        Next
        If Not Ext_Ok Then
            'IS_SendMail = False
            Throw New Exception("Ce fichier n'est pas une image de type (gif, jpg, jpeg, png, bmp)...")
        End If
    End Sub

    Public Sub Is_Good_Extension(ByVal _FileUpload_Annonce As FileUpload, ByVal Typeas As String)
        Dim listeExt As String = System.Configuration.ConfigurationManager.AppSettings("Extention_DocumentFile").ToString
        Dim Liste_Ext() As String = Split(listeExt, ",") ''Split(".jpg,.png,.bmp,.jpeg,.gif", ",")
        Dim Ext_Ok As Boolean = False
        REM 1Ko = 1024 octet  |-|  1Mo = 1024Ko  |-|   1Mo = 1048576 byte |-|  50Ko = 51200o
        Dim SizeImage As Integer = 51200 ' = 50Kb
        Dim SizeStr As String = "50 Kb"
        REM Dimension pour le document
        SizeImage = TypeSafeConversion.NullSafeLong(System.Configuration.ConfigurationManager.AppSettings("MaxSize_DocumentFile")) ' TAILLE FICHIER 5Mb = 5 000 000 bit
        SizeStr = System.Configuration.ConfigurationManager.AppSettings("MaxSize_DocumentFile_Str").ToString

        REM Taille Illimite ou pas //  Si Taille limite
        If _FileUpload_Annonce.PostedFile.ContentLength > SizeImage Then ' Taille limite 
            'IS_SendMail = False
            Throw New Exception("La taille du document est trop volumineux ! Taille limite " & SizeStr)
        End If
        Dim _GetExtension As String = System.IO.Path.GetExtension(_FileUpload_Annonce.FileName).ToLower
        For i As Integer = 0 To Liste_Ext.Length - 1
            If _GetExtension.Trim.ToLower = Liste_Ext(i).Trim.ToLower Then
                Ext_Ok = True
                Exit For
            End If
        Next
        If Not Ext_Ok Then
            'IS_SendMail = False
            Throw New Exception("Ce fichier n'est pas un document de type (" & listeExt & ")...")
        End If
    End Sub

    Public Function IsUserStillConnected(ByVal _user As Cls_User) As Boolean
        Dim _tmp As Cls_User
        _tmp = New Cls_User(_user.ID)
        Return Not (_tmp.Isforcedout)
    End Function

    Public Function IsUserStillActive(ByVal _user As Cls_User) As Boolean
        Dim _tmp As Cls_User
        _tmp = New Cls_User(_user.ID)
        Return _tmp.Actifyn
    End Function


    Public Sub Message_Image(ByVal ImageName As Image, ByVal TypeMessage As String, Optional ByVal Visible As Boolean = True)
        ImageName.Visible = Visible
        If TypeMessage = "E" Then
            ImageName.ImageUrl = "~/images/img_erreur.png"
        ElseIf TypeMessage = "W" Then
            ImageName.ImageUrl = "~/images/img_erreur.png"
        Else
            ImageName.ImageUrl = "~/images/img_succes.png"
        End If
    End Sub

#Region "SEND EMAIL"
    Public Sub Send_Mail_Erreur(ByVal _Titre As String, _
                                   ByVal _Message_Erreur As String, _
                                   Optional ByVal _Sujet As String = "Erreur RGPH")
        Dim _SMTPServer As String = [Global].SMTPServer
        Dim _EmailFrom As String = [Global].EmailFrom
        Dim _EmailPassword As String = [Global].EmailPassword
        Dim _SendTo As String = System.Configuration.ConfigurationManager.AppSettings("EmailToSendError")
        Dim _template As String

        Try
            Dim Courrier As New Cls_SendMail(_SMTPServer, _EmailFrom, _EmailPassword)
            Dim _From As String = _EmailFrom
            _template = "<h2 style='color:red;'>" & _Titre & "</h2>"
            _template &= "<hr /><b> Date Erreur :</b>" & Now.ToString("dd-MMM-yyyy") & ""
            _template &= "<hr /><h3>Erreur dans:</h3> " & System.Web.HttpContext.Current.Request.Url.ToString() & "."
            _template &= "<hr /><h3> Message Erreur :</h3>" & _Message_Erreur & ""
            Courrier.From = _From
            Courrier.DispalyName = "ERREUR RGPH"
            Courrier.Subject = _Sujet
            Courrier.SendTo = _SendTo
            Courrier.MailFormatIsHTML = True
            Courrier.Body = _template
            Courrier.MailMessage_2()
        Catch ex As Exception
            ErreurLog.WriteError("Une erreur est survenue [Send_Mail_Erreur] --> " & ex.Message)
        Finally

        End Try
    End Sub

#End Region

    Dim rand As New System.Random

    Public Function GenerateRandomCode(ByVal ControlToValidate As TextBox) As String
        Dim s2 As String = "REZO509-ABCDEFGHIJKLNMOPQRSTUVWXYZ0123456789"
        Dim res3 As String = ""
        Dim nb As Integer
        For j As Integer = 0 To 3
            nb = s2.Length
            nb = rand.Next(0, nb - 1)
            res3 &= s2(nb)
        Next
        ControlToValidate.Text = res3
        Return res3
    End Function

    Public Function GenerateRandomCode() As String
        Dim s2 As String = "REZO509-ABCDEFGHIJKLNMOPQRSTUVWXYZ0123456789;?@"
        Dim res3 As String = ""
        Dim nb As Integer
        For j As Integer = 0 To 7
            nb = s2.Length
            nb = rand.Next(0, nb - 1)
            res3 &= s2(nb)
        Next
        Return res3
    End Function

    Public Sub Style_Division(ByVal NameDiv As HtmlControl, ByVal _NameClassADD As String, Optional ByVal _Attributes As String = "class")
        NameDiv.Attributes.Remove(_Attributes)
        NameDiv.Attributes.Add(_Attributes, _NameClassADD)
    End Sub

    Public Sub Style_Division(ByVal NameDiv As HtmlGenericControl, ByVal NameClass_StyleADD As String, ByVal NameClass_StyleREMOVE As String, Optional ByVal ControlName As String = "class")
        NameDiv.Attributes.Remove(NameClass_StyleREMOVE)
        NameDiv.Attributes.Add(ControlName, NameClass_StyleADD)
    End Sub

    Public Sub Style_Control_ASP(ByVal Control_ASP_Name_TO_ADD_Style As Object, ByVal NAME_CLASS As String)
        Control_ASP_Name_TO_ADD_Style.CssClass = NAME_CLASS
    End Sub

    Public Sub Style_Control_ASP(ByVal Control_ASP_Name_TO_ADD_Style As Object, ByVal NAME_CLASS As String, ByVal Control_ASP_Name_TO_REMOVE_Style As Object, Optional ByVal NAME_CLASS_TOREMOVE As String = "Index_Secteur_a")
        Control_ASP_Name_TO_ADD_Style.CssClass = NAME_CLASS
        Control_ASP_Name_TO_REMOVE_Style.CssClass = NAME_CLASS_TOREMOVE
    End Sub

    'Verification de la date mm/jj/aaaa '--
    Public Function VerificationDate(ByVal DateString As String) As Boolean
        If Not IsDate(DateString) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function Verification_Date_jjmmaa_en_mmjjaa(ByVal DateString As String, Optional ByVal _Message As String = "") As String
        'Format Date Francais jour/mois/Annee changer en mois/jour/Annee
        Dim DaterDate_MiseEnService As String = Regex.Replace(DateString, "(?<dd>\d{2}).(?<mm>\d{2}).(?<yyyy>\d{4})", "${mm}/${dd}/${yyyy}")
        If VerificationDate(DaterDate_MiseEnService) Then
            Throw (New System.Exception("Format date " & _Message & " Incorrect - Format Jour/Mois/Année"))
        End If
        Return DaterDate_MiseEnService
    End Function

End Module
