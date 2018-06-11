Imports Microsoft
Imports System.Data
Imports System.Collections.Generic
Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer
Imports Telerik.Web.UI
Imports RGPH_QCOLLECTE_Library

Partial Class Frm_SR_SERVICESADD
    Inherits Cls_BasePage ' LA CLASSE DE LA PAGE HERITE DE CETTE CLASSE DANS LE CAS OU NOUS AVONS UNE APPLICATION WEB multilingue


#Region "ATTRIBUTS"
    Private _message As String  ' VARIABLE SERVANT A LA RECUPERATION DE TOUS LES MESSAGES D'ECHECS OU DE SUCCES

    REM DEFINITION ET INITIALISATION DES CONSTANTE POUR LA SECURITE
    Private Const Nom_page As String = "PAGE-LISTING-PRODUITS-SERVICES-REZO509"  ' POUR LA PAGE
    Private Const Btn_Save As String = "Bouton-SAVE-PRODUITS-SERVICES-REZO509"       ' POUR LE BOUTON D'ENREGISTREMENT
    Private Const Btn_Edit As String = "Bouton-EDIT-PRODUITS-SERVICES-REZO509"       ' POUR LE BOUTON DE MODIFICATION
    Private Const Btn_Delete As String = "Bouton-DELETE-PRODUITS-SERVICES-REZO509"   ' POUR LE BOUTON DE SUPPRESSION

    Dim User_Connected As Cls_User          ' INSTANCE DE LA CLASSE UTILISATEUR - UTILISER POUR L'UTILISATEUR EN SESSION 
    Dim Is_Acces_Page As Boolean = True     ' LA VARIABLE SERVANT DE TEST POUR DONNEER L'ACCES A LA PAGE
    Dim GetOut As Boolean = False           ' LA VARIABLE SERVANT DE TEST POUR REDIRIGER L'UTILISATEUR VERS LA PAGE DE CONNEXION
    Private IS_SendMail As Boolean = True   ' PAS TROP IMPORTANT...
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Expires = -1
        Panel_Msg.Visible = False

        SYSTEME_SECURITE()  ' APPEL A LA METHODE SERVANT A TESTER LES COMPOSANTS DE LA PAGE Y COMPRIS LA PAGE ELLE MEME 

        '--- Si l'utilisateur n'a Access a la page les informations ne sont pas charger dans la Page_Load 
        If Is_Acces_Page Then
            If Not IsPostBack Then
                btnCancel.Attributes.Add("onclick", "javascript:void(closeWindow());")
                'rbtnAddSR_SERVICES.Attributes.Add("onclick", "javascript:void(ShowAddUpdateForm('Frm_SR_SERVICESADD.aspx', 550, 400)); return false;")
                'BtnADDNew.Attributes.Add("onclick", "javascript:Open_Window('Frm_SR_SERVICESADD.aspx', '_self',500,400); return false;") 
                Load_ALL_DATA()
            End If
        End If
    End Sub

#Region "SECURITE"
    Public Sub SYSTEME_SECURITE()

        User_Connected = [Global].KeepUserContinuesToWork(User_Connected)

        CType(Page.Master.FindControl("DashMenu_Securite").FindControl("LI_PARAMETRES"), HtmlControl).Attributes.Add("class", "active treeview")
        CType(Page.Master.FindControl("DashMenu_Securite").FindControl("LI_SR_SERVICESListing"), HtmlControl).Attributes.Add("class", "active")

        If Session([Global].GLOBAL_SESSION) IsNot Nothing Then
            User_Connected = CType(Session([Global].GLOBAL_SESSION), Cls_User)
            If Not Cls_Privilege.VerifyRightOnObject(Nom_page, User_Connected.IdGroupeuser) Then    ' VERIFICATION SI L'UTILISATEUR N'A PAS ACCES A LA PAGE
                _message = [Global].NO_ACCES_PAGE
                MessageToShow(_message)
                Is_Acces_Page = False

                Panel_First.Visible = False
            Else    ' SI L'UTILISATEUR A ACCES A LA PAGE ON VERIFIE POUR LES BOUTONS ET LES LIENS
                '---  Okey vous avez acces a la page ---'
                btnSave.Text = "Enregistrer"
                btnSave.Visible = Cls_Privilege.VerifyRightOnObject(Btn_Save, User_Connected.IdGroupeuser)
                If Request.QueryString("ID") IsNot Nothing Then
                    btnSave.Text = "Modifier"
                    btnSave.Visible = Cls_Privilege.VerifyRightOnObject(Btn_Edit, User_Connected.IdGroupeuser)
                End If
            End If
        End If

        If Session([Global].GLOBAL_SESSION) Is Nothing Then
            '-- Session expirée --'
            GetOut = True
        Else
            Try
                User_Connected = CType(Session([Global].GLOBAL_SESSION), Cls_User)  ' ON VERIFIE SI LÚTILISATEUR A ETE FORCE DE SE CONNECTER 'PAR L'ADM
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

        If GetOut Then ' REDIRECTIONNEMENT DE L'UTILISATUER OU PAS.
            Response.Redirect([Global].PAGE_LOGIN)
        End If
    End Sub
#End Region

#Region "Other Method - MessageToShow"
    Private Sub MessageToShow(ByVal _message As String, Optional ByVal E_or_S As String = "E")
        Panel_Msg.Visible = True
        GlobalFunctions.Message_Image(Image_Msg, E_or_S)
        Label_Msg.Text = _message
        RadAjaxManager1.ResponseScripts.Add("alert('" & _message.Replace("'", "\'") & "');")
        If E_or_S = "S" Then
            Label_Msg.ForeColor = Drawing.Color.Green
        Else
            Label_Msg.ForeColor = Drawing.Color.Red
        End If
    End Sub
#End Region

#Region "Load DATA"
    Private Sub LOAD_ALL_DATA()
        LoadModule(DDLModule)
        LOAD_SR_SERVICES()
    End Sub

    Private Sub LOAD_SR_SERVICES()
        Try
            If Request.QueryString("ID") IsNot Nothing Then
                txt_CodeSR_SERVICES_Hid.Text = TypeSafeConversion.NullSafeLong(Request.QueryString("ID"))
                Dim obj As New Cls_Privilege(TypeSafeConversion.NullSafeLong(txt_CodeSR_SERVICES_Hid.Text))
                If obj.ID > 0 Then
                    btnSave.Visible = Cls_Privilege.VerifyRightOnObject(Btn_Edit, User_Connected.IdGroupeuser)
                    With obj
                        rtxt_NomService.Text = .NomService
                        rtxt_DescriptionService.Text = .DescriptionService
                        DDL_TypeObjet.SelectedIndex = DDL_TypeObjet.Items.IndexOf(DDL_TypeObjet.Items.FindByValue(obj.TYPE_OBJET))
                        DDLModule.SelectedIndex = DDLModule.Items.IndexOf(DDLModule.Items.FindByValue(obj.ID_Modules))
                    End With
                End If
            Else

            End If
        Catch ex As Rezo509Exception
            _message = ex.Message
            MessageToShow(_message)
            _message = " Methode -> [ LOAD_SR_SERVICES ] :" & ex.Message
            [Global].WriteError(_message, User_Connected)
        End Try
    End Sub

    Private Sub LoadModule(ByVal _ddl As DropDownList, Optional ByVal _IsforSearch As Boolean = False)
        Try
            Dim objs As List(Of Cls_SR_MODULES)
            objs = Cls_SR_MODULES.SearchAll_Actif
            With _ddl
                .DataTextField = "NomModule"
                .DataValueField = "ID"
                .DataSource = objs
                .DataBind()
                If _IsforSearch Then
                    .Items.Insert(0, New ListItem("Tous les modules", "0"))
                Else
                    .Items.Insert(0, New ListItem("Selectionnez le module", ""))
                End If
            End With
        Catch ex As Rezo509Exception
            ErreurLog.WriteError("METHODE -> LoadModule" & ex.Message)
            MessageToShow(ex.Message)
        End Try
    End Sub

#End Region

#Region "METHODES - SAVE"
    Private Sub SAVE_SR_SERVICES()
        Try
            Dim _id As Long = TypeSafeConversion.NullSafeLong(txt_CodeSR_SERVICES_Hid.Text)
            Dim obj As New Cls_Privilege(_id)
            With obj
                .NomService = rtxt_NomService.Text
                .DescriptionService = rtxt_DescriptionService.Text
                .TYPE_OBJET = DDL_TypeObjet.SelectedValue
                .ID_Modules = TypeSafeConversion.NullSafeLong(DDLModule.SelectedValue)
                .IsProduitsRezo509 = True
            End With
            obj.Save(User_Connected.Username)
            REM TRACE UTILUSATEUR / Trace Transaction
            User_Connected.Activite_Utilisateur_InRezo(IIf(TypeSafeConversion.NullSafeLong(_id) > 0, "ADD", "EDIT") & " SR_SERVICES", " ID:" & obj.ID, Request.UserHostAddress)
            txt_CodeSR_SERVICES_Hid.Text = obj.ID
            _message = "Sauvegarde Effectuée"
            MessageToShow(_message, "S")
            RadAjaxManager1.ResponseScripts.Add("CloseAndRefreshListeSR_SERVICES();")
        Catch ex As Rezo509Exception
            _message = ex.Message & Chr(13) & " La sauvegarde a échouée!"
            MessageToShow(_message)
            _message = " Methode -> [ SAVE_SR_SERVICES ] :" & ex.Message
            [Global].WriteError(_message, User_Connected)
        End Try
    End Sub
#End Region

#Region "EVENTS BUTTON"
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SAVE_SR_SERVICES()
    End Sub
#End Region
End Class


