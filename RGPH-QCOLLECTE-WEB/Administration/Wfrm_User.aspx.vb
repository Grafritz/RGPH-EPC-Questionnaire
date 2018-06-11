' @Brain Development
' Saturday 21-09-2013

Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports Telerik.Web.UI
Imports RGPH_QCOLLECTE_Library

Partial Class Parametres_Administration_Wfrm_User
    Inherits Cls_BasePage

    Dim _message As String = ""
    'Private Const Nom_page As String = "PAGE-GESTION-UTILISATEUR"
    'Private Const Btn_SaveEditUser As String = "Bouton-Save-Edit-Utilisateur"
    'Private Const Btn_DeleteUser As String = "Bouton-Delete-Utilisateur"
    'Private Const Btn_ResetPassWordUser As String = "Bouton-Reset-PassWord-Utilisateur"
    Private Const ActionChangeUsername As String = "Btn_SaveEditUser" '"Action-Change-Username"

    'Private Const Nom_pageUtilisateur As String = "Wfrm_User.aspx"
    Private Const Btn_SaveEditUser As String = "Btn_SaveEditUser"
    Private Const Btn_DeleteUser As String = "Btn_DeleteUser"
    Private Const Btn_ResetPassWordUser As String = "Btn_ResetPassWordUser"
    Private check_btnResetPassWordUser As Boolean = False
    Private check_SendEmail As Boolean = False

    Private Const Btn_SavePRODUITS As String = "Bouton-SAVE-PRODUITS-SERVICES-REZO509"       ' POUR LE BOUTON D'ENREGISTREMENT
    Private Const Btn_EditPRODUITS As String = "Bouton-EDIT-PRODUITS-SERVICES-REZO509"       ' POUR LE BOUTON DE MODIFICATION
    Private Const Btn_DeletePRODUITS As String = "Bouton-DELETE-PRODUITS-SERVICES-REZO509"   ' POUR LE BOUTON DE SUPPRESSION

    Dim User_Connected As Cls_User
    Dim Is_Acces_Page As Boolean = True
    Dim GetOut As Boolean = False
    Dim IS_Search As Boolean = False
    Dim IS_Trouve As Boolean = False
    Private IDGroupe As Long = 0
    Private IS_SendMail As Boolean = True

    Dim GenerateRandomCode As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'CType(Me.Master.FindControl("MenuParametre").FindControl("HyperLink_Wfrm_User"), HyperLink).CssClass = "active"
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Expires = -1
            Panel_Msg.Visible = False

            SYSTEME_SECURITE()

            If Request.QueryString("IDGroupe") IsNot Nothing AndAlso IsNumeric(Request.QueryString("IDGroupe")) Then
                IDGroupe = Request.QueryString("IDGroupe")
            Else
                IDGroupe = 0
            End If

            '--- Si l'utilisateur a Acces a la page 
            If Is_Acces_Page Then
                If Not IsPostBack Then
                    'Me.ViewState("sortfield") = "ID"
                    'Me.ViewState("sortdirection") = "DESC"
                    Load_Data()
                    NewToDo()

                End If

                'rbtnAddSR_PRIVILEGE_SERVICES.Attributes.Add("onclick", "javascript:void(ShowAddUpdateForm('Frm_SR_PRIVILEGE_SERVICESADD.aspx?IDUser=" & obj.ID & "', 850, 400)); return false;")

            End If
        Catch ex As Threading.ThreadAbortException
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
            CType(Page.Master.FindControl("DashMenu_Securite").FindControl("liPAGE_GESTION_UTILISATEUR"), HtmlControl).Attributes.Add("class", "active")

            If Session([Global].GLOBAL_SESSION) IsNot Nothing Then
                User_Connected = CType(Session([Global].GLOBAL_SESSION), Cls_User)
                If Not Cls_Privilege.VerifyRightOnObject([Global].Nom_pageUtilisateur, User_Connected.IdGroupeuser) Then
                    _message = [Global].NO_ACCES_PAGE
                    MessageToShow(_message)
                    'Is_Acces_Page = False

                    Is_Acces_Page = False
                    Panel_First.Visible = False
                    Panel_First2.Visible = False
                    Panel_First.Visible = False
                    Panel_List.Visible = False
                    '--- Sorry vous n'avez pas Acces a la page 
                    RemoveHandler Btn_Save.Click, AddressOf Btn_Save_Click

                Else
                    '---  Okey vous avez acces a la page ---'
                    Dim check As Boolean = Cls_Privilege.VerifyRightOnObject(Btn_SaveEditUser, User_Connected.IdGroupeuser)
                    check_SendEmail = check
                    LinkButton_AddNew.Visible = check
                    Btn_Save.Visible = check
                    RadGrid_1.MasterTableView.Columns.FindByUniqueNameSafe("UPDATE").Visible = check
                    'RadGrid_1.MasterTableView.Columns.FindByUniqueNameSafe("SEND_EMAIL").Visible = check

                    check_btnResetPassWordUser = Cls_Privilege.VerifyRightOnObject(Btn_ResetPassWordUser, User_Connected.IdGroupeuser)
                    Btn_Reset_MotdePasse.Visible = check
                    'RadGrid_1.MasterTableView.Columns.FindByUniqueNameSafe("RESET_PASSWORD").Visible = check
                    'RadGrid_1.MasterTableView.Columns.FindByUniqueNameSafe("DISCONNECT_USER").Visible = check
                    'RadGrid_1.MasterTableView.Columns.FindByUniqueNameSafe("IsValideAccount").Visible = check_btnResetPassWordUser

                    RadGrid_1.MasterTableView.Columns.FindByUniqueNameSafe("DELETE").Visible = Cls_Privilege.VerifyRightOnObject(Btn_DeleteUser, User_Connected.IdGroupeuser)

                    'rbtnAddSR_PRIVILEGE_SERVICES.Visible = Cls_Privilege.VerifyRightOnObject(Btn_SavePRODUITS, User_Connected.IdGroupeuser)
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
                        _message = "Session expirée..."
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
                LinkButton_AddNew.Visible = False


                Panel_First2.Visible = False
                Panel_First.Visible = False
                Panel_List.Visible = False

                Is_Acces_Page = False
                Panel_First.Visible = False
                LoginWUC.Visible = True
                'Response.Redirect([Global].PAGE_LOGIN)
                Session([Global].GLOBAL_PAGENAME) = System.Web.HttpContext.Current.Request.Url.ToString()
                FormLoginModal.Attributes.Add("style", "perspective: none;")
                FormLoginModal.Attributes.Add("class", "md-modal colored-header custom-width md-effect-12 md-show")
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub
#End Region

#Region "Other Method - SAVE"

    Private Sub MessageToShow(ByVal _message As String, Optional ByVal E_or_S As String = "E")
        Panel_Msg.Visible = True
        GlobalFunctions.Message_Image(Image_Msg, E_or_S)
        Label_Msg.Text = _message
        'RadAjaxManager1.ResponseScripts.Add("alert('" & _message.Replace("'", "\'") & "');")
        Dialogue.alert([Global].GetTextFromHtml(_message))
        If E_or_S = "S" Then
            Style_Division(DIV_Msg, "alert alert-success alert-dismissable")
            Style_Division(Icon_Msg, "fa  fa-thumbs-up")
        Else
            Style_Division(DIV_Msg, "alert alert-danger alert-dismissable")
            Style_Division(Icon_Msg, "fa fa-thumbs-down")
        End If
    End Sub

    Public Sub NewToDo()
        Panel_First.Visible = False
        Panel_List.Visible = True
        txt_ID.Text = "0"
        txtPrenom.Text = ""
        txtNom.Text = ""
        txtEmailCompte.Text = ""
        txtTelephone.Text = ""
        DDLGroupeUtilisateur.SelectedIndex = -1

        CBXActifyn.Checked = True
        Image_participant.ImageUrl = "~/Show_Image.aspx?Action=" & [Global].PHOTO_UTILISATEUR & "&ID=0"

        'DIV_LISTEProduitsServices.Visible = False
        'DIV_ListePlan.Visible = False
        'LabelLastip.Text = ""
        'LabelLastlogin.Text = ""

        Btn_Reset_MotdePasse.Visible = False

    End Sub

    Public Sub EnableControls()
        'If Cls_Privilege.VerifyRightOnObject(Btn_DeleteUser, User_Connected.IdGroupeuser) Then
        '    GridView_List.Columns(GridView_List.Columns.Count - 1).Visible = True
        'End If
        'If Cls_Privilege.VerifyRightOnObject(Btn_SaveEditUser, User_Connected.IdGroupeuser) Then
        '    GridView_List.Columns(GridView_List.Columns.Count - 2).Visible = True
        '    'Btn_Nouveau.Visible = True
        '    'LinkButton_AddNew_Annonce.Visible = True
        'End If
    End Sub

    Public Sub DisableControls()
        'GridView_List.Columns(GridView_List.Columns.Count - 1).Visible = False
        'GridView_List.Columns(GridView_List.Columns.Count - 2).Visible = False
        'Btn_Nouveau.Visible = False
        'LinkButton_AddNew_Annonce.Visible = False
    End Sub

    Public Sub Clear_Field()
        txt_ID.Text = "0"
        txtPrenom.Text = ""
        txtNom.Text = ""
        txtEmailCompte.Text = ""
        txtTelephone.Text = ""
        DDLGroupeUtilisateur.SelectedIndex = -1
    End Sub

    Private Sub Load_DataUser(ByVal IDUser As Long)
        Try
            txtEmailCompte.Enabled = Cls_Privilege.VerifyRightOnObject(ActionChangeUsername, User_Connected.IdGroupeuser)

            Dim obj As New Cls_User(IDUser)
            If obj.ID > 0 Then
                txt_ID.Text = obj.ID
                txtPrenom.Text = obj.Prenom
                txtNom.Text = obj.Nom
                txtEmailCompte.Text = obj.Username
                txtTelephone.Text = obj.Telephone
                'RCBPoste.Text = obj.Poste
                'labelIdFirebaseToken.Text = obj.IdFirebaseToken
                ddl_Ecole.SelectedIndex = ddl_Ecole.Items.IndexOf(ddl_Ecole.Items.FindByValue(obj.ID_Ecole))
                DDLGroupeUtilisateur.SelectedIndex = DDLGroupeUtilisateur.FindItemIndexByValue(obj.IdGroupeuser)

                CBXActifyn.Checked = obj.Actifyn
                Image_participant.ImageUrl = "~/Show_Image.aspx?Action=" & [Global].PHOTO_UTILISATEUR & "&ID=" & obj.ID

                'LabelLastip.Text = obj.Lastip
                'LabelLastlogin.Text = obj.Lastlogin

                DDL_Sexe.SelectedIndex = DDL_Sexe.Items.IndexOf(DDL_Sexe.Items.FindByValue(obj.Sexe))
                DDL_Pays.SelectedIndex = DDL_Pays.Items.IndexOf(DDL_Pays.Items.FindByValue(obj.IdPaysadresse))

                ControlPays(obj.IdPaysadresse)
                DDL_Departement.SelectedIndex = DDL_Departement.Items.IndexOf(DDL_Departement.Items.FindByValue(obj.IdDepartement))

                Load_Commune_ByDepartement(obj.IdDepartement)
                DDL_Commune.SelectedIndex = DDL_Commune.Items.IndexOf(DDL_Commune.Items.FindByValue(obj.IdCommune))

                txt_Adresse.Text = obj.Adresserue

                Btn_Reset_MotdePasse.Visible = True
                Btn_Reset_MotdePasse.Visible = Cls_Privilege.VerifyRightOnObject(Btn_ResetPassWordUser, User_Connected.IdGroupeuser)

                'DIV_LISTEProduitsServices.Visible = True
                'DIV_ListePlan.Visible = True
                Btn_Annuler.Text = "<i class='fa  fa-arrows-alt' ></i> Fermer"
                'rbtnAddPlan.Attributes.Add("onclick", "javascript:void(ShowAddUpdateForm('../GestionPlansEtServices/Frm_PlanRezo509UserADD.aspx?IDUser=" & obj.ID & "', 800, 450)); return false;")
                'rbtnAddSR_PRIVILEGE_SERVICES.Attributes.Add("onclick", "javascript:void(ShowAddUpdateForm('Frm_SR_PRIVILEGE_SERVICESADD.aspx?IDUser=" & obj.ID & "', 800, 450)); return false;")
                'BindGridPlan()
                'rdgSR_PRIVILEGE_SERVICES.Rebind()
            End If
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub

    Private Sub SAVE1()
        Try
            Dim obj As New Cls_User(CLng(txt_ID.Text))
            'obj.USERNAME = txt_UserName.Text
            'obj.Nom = Txt_NomComplet.Text
            'obj.Actifyn = CBX_Actif.Checked
            'obj.Confirmer = CBX_Confirmation.Checked
            'obj.IdGroupeuser = DDL_GroupUtilisateur.SelectedValue
            'If obj.ID = 0 Then
            '    If txt_MotdePasse.Text.Length > 0 Then
            '        obj.PASSWORD = txt_MotdePasse.Text
            '    Else
            '        obj.PASSWORD = txt_UserName.Text
            '    End If
            'End If

            obj.Save(User_Connected.Username)
            Count_DATAUser()
            _message = "Enregistrement effectuée"
            MessageToShow(_message, "S")
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub

    Private Sub SAVE()
        Try
            Dim obj As New Cls_User(TypeSafeConversion.NullSafeLong(txt_ID.Text))
            'obj.Username = txtEmailCompte.Text
            obj.Prenom = txtPrenom.Text.Trim
            obj.Nom = txtNom.Text.Trim
            obj.Telephone = txtTelephone.Text.Trim
            obj.Sexe = TypeSafeConversion.NullSafeString(DDL_Sexe.SelectedValue)
            obj.Mustchangepassword = CBXMustChangePassword.Checked
            obj.Actifyn = CBXActifyn.Checked
            'obj.Poste = RCBPoste.Text
            obj.IdPaysadresse = TypeSafeConversion.NullSafeLong(DDL_Pays.SelectedValue)

            obj.IdDepartement = TypeSafeConversion.NullSafeLong(DDL_Departement.SelectedValue)
            obj.IdCommune = TypeSafeConversion.NullSafeLong(DDL_Commune.SelectedValue)

            If obj.ID = 0 Then ' Si c'est une creation de compte
                obj.Username = txtEmailCompte.Text
                If txt_MotdePasse.Text.Length > 0 Then
                    obj.Password = txt_MotdePasse.Text
                    GenerateRandomCode = txt_MotdePasse.Text
                Else
                    obj.Password = txtEmailCompte.Text
                    GenerateRandomCode = txtEmailCompte.Text
                End If

                If CBXGenererLeMotDePasse.Checked Then
                    GenerateRandomCode = GlobalFunctions.GenerateRandomCode
                    obj.Password = GenerateRandomCode
                End If
            Else
                If Cls_Privilege.VerifyRightOnObject(ActionChangeUsername, User_Connected.IdGroupeuser) Then
                    obj.Username = txtEmailCompte.Text
                End If
            End If

            obj.ID_Ecole = TypeSafeConversion.NullSafeLong(ddl_Ecole.SelectedValue)
            obj.IdGroupeuser = TypeSafeConversion.NullSafeLong(DDLGroupeUtilisateur.SelectedValue)

            ' PHOTO
            If FileUpload1.FileName.Length > 0 AndAlso FileUpload1.PostedFile IsNot Nothing Then
                Is_Good_Extension(FileUpload1)
                Dim strLen As Integer = CInt(FileUpload1.PostedFile.InputStream.Length)
                Dim strArr(strLen) As Byte
                'rem Read stream into byte array.
                FileUpload1.PostedFile.InputStream.Read(strArr, 0, strLen)
                obj.Photo = strArr
            End If

            obj.Save(User_Connected.Username)
            If obj.ID > 0 Then
                Btn_Annuler.Text = "<i class='fa  fa-arrows-alt' ></i> Fermer"

                'DIV_ListePlan.Visible = True
                'rbtnAddPlan.Attributes.Add("onclick", "javascript:void(ShowAddUpdateForm('../GestionPlansEtServices/Frm_PlanRezo509UserADD.aspx?IDUser=" & obj.ID & "',800, 450)); return false;")

                'DIV_LISTEProduitsServices.Visible = True
                'DIV_ListePlan.Visible = True
                'rbtnAddSR_PRIVILEGE_SERVICES.Attributes.Add("onclick", "javascript:void(ShowAddUpdateForm('Frm_SR_PRIVILEGE_SERVICESADD.aspx?IDUser=" & obj.ID & "', 800, 450)); return false;")
                If CBX_SendAccountAndPassword.Checked Then
                    Send_Notification_Account(obj, IIf(CLng(txt_ID.Text) > 0, "Mise à jour de votre compte ", "Enregistrement d'un nouvel utilisateur "))
                End If
                MessageToShow("Enregistrement effectué avec succès", "S")
            End If
            txt_ID.Text = obj.ID
            Image_participant.ImageUrl = "~/Show_Image.aspx?Action=" & [Global].PHOTO_UTILISATEUR & "&ID=" & obj.ID
            User_Connected.Activite_Utilisateur_InRezo(IIf(CLng(txt_ID.Text) > 0, "UPDATE ", "ADD ") & "Utilisateur", obj.ID & " - " & obj.Username, Request.UserHostAddress)
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub
#End Region

#Region "Load DATA"

    Private Sub Load_Data()
        LoadAll_Pays()
        LoadAll_Departement()
        LOAD_ALL_Poste()

        Count_DATAUser()
        LoadData_GridView_List()
        Load_Group_Utilisateur()
        FillComboEcole()
        'Try

        '    'Dim obj As New Cls_User(TypeSafeConversion.NullSafeLong(txt_ID.Text))
        '    'If obj.ID > 0 Then
        '    '    txt_ID.Text = obj.ID
        '    '    txtPrenom.Text = obj.Prenom
        '    '    txtNom.Text = obj.Nom
        '    '    txtEmailCompte.Text = obj.Email
        '    '    txtTelephone.Text = obj.Telephone
        '    '    RCBPoste.Text = obj.Poste
        '    '    DDLGroupeUtilisateur.SelectedIndex = DDLGroupeUtilisateur.FindItemIndexByValue(obj.IdGroupeuser)

        '    '    CBXActifyn.Checked = obj.Actifyn
        '    '    'txtSkypeName.Text = obj.Skypename
        '    '    Image_participant.ImageUrl = "~/Show_Image.aspx?Action=User&ID=" & obj.ID

        '    '' ''LabelLastip.Text = obj.Lastip
        '    '' ''LabelLastlogin.Text = obj.Lastlogin
        '    ''DDL_Sexe.SelectedIndex = DDL_Sexe.Items.IndexOf(DDL_Sexe.Items.FindByValue(obj.Sexe))
        '    ' ''Label_DDL_Sexe.Text = obj.Sexe
        '    ' ''Control(DDL_Sexe)
        '    ''DDL_Pays.SelectedIndex = DDL_Pays.Items.IndexOf(DDL_Pays.Items.FindByValue(obj.ID_PAYS))
        '    ' ''Label_DDL_Pays.Text = obj.PAYS_STR
        '    ' ''Control(DDL_Pays)
        '    ''ControlPays(obj.ID_PAYS)
        '    ''DDL_Departement.SelectedIndex = DDL_Departement.Items.IndexOf(DDL_Departement.Items.FindByValue(obj.COMMUNE_OBJ.ID_DEPATEMENT))
        '    ' ''Label_DDL_Departement.Text = obj.COMMUNE_OBJ.DEPARTEMENT_STR
        '    ' ''Control(DDL_Departement)
        '    ''Load_Commune_ByDepartement(obj.COMMUNE_OBJ.ID_DEPATEMENT)
        '    ''DDL_Commune.SelectedIndex = DDL_Commune.Items.IndexOf(DDL_Commune.Items.FindByValue(obj.ID_COMMUNE))
        '    ' ''Label_DDL_Commune.Text = obj.COMMUNE_STR
        '    ' ''Control(DDL_Commune)
        '    ''txt_Adresse.Text = obj.ADRESSE
        '    ' ''Label_txt_Adresse.Text = obj.ADRESSE
        '    ' ''ontrol(txt_Adresse)
        '    'End If

        'Catch ex As Exception
        '    _message = ex.Message
        '    MessageToShow(_message)
        '    _message = " Methode -> [ Load_Data ] :" & ex.Message
        '    [Global].WriteError(_message, User_Connected)
        'End Try
    End Sub

    Private Sub LoadData_GridView_List(Optional ByVal _refresh As Boolean = True)
        Dim objs As List(Of Cls_User)
        Dim _ret As Long = 0
        Try
            objs = Cls_User.SearchAll
            RadGrid_1.DataSource = objs
            If _refresh Then
                RadGrid_1.DataBind()
            End If
            _ret = objs.Count
            Label_Titre.Text = "UTILISATEURS <small class=""badge  bg-blue"">" & _ret & "</small>"
            If _ret <= 0 Then
                ' Panel_NewAnnonce.Visible = True
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub

    Private Sub Count_DATAUser()
        'Dim CompteAdmin As String = "Compte Admin: " & Cls_User.Count_AllUser_ByGroupe(1)
        'Dim CompteEntreprise As String = "Compte Entreprise: " & Cls_User.Count_AllUser_ByGroupe(2)
        'Dim CompteOperateur As String = "Compte Opérateur: " & Cls_User.Count_AllUser_ByGroupe(3)
        'Dim CompteAbonne As String = "Compte Abonnée: " & Cls_User.Count_AllUser_ByGroupe(4)

        'Label_Utilisateur.Text = "UTILISATEURS ( TOTAL: " & Cls_User.Count_AllUser_ByGroupe(0) & " )"
        'Label_CompteAdmin.Text = CompteAdmin
        'Label_CompteAdmin2.Text = CompteAdmin

        'Label_CompteEntreprise.Text = CompteEntreprise

        'LabelCompteOperateur.Text = CompteOperateur
        'LabelCompteOperateur2.Text = CompteOperateur

        'Label_CompteAbonne.Text = CompteAbonne
        'Label_CompteAbonne2.Text = CompteAbonne
    End Sub

    Private Sub Load_Group_Utilisateur()
        Dim obj As New List(Of Cls_GroupeUser)
        Try
            obj = Cls_GroupeUser.SearchAll
            DDLGroupeUtilisateur.Items.Clear()
            DDLGroupeUtilisateur.DataTextField = "GROUPE_DESCRIPTION"
            DDLGroupeUtilisateur.DataValueField = "ID"
            DDLGroupeUtilisateur.DataSource = obj
            DDLGroupeUtilisateur.DataBind()

            ''DDLGroupeUtilisateur.SelectedIndex = DDLGroupeUtilisateur.FindItemIndexByValue([Global]._GroupeEnum.Participant)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub

    Public Sub LOAD_ALL_Poste()
        'Try
        '    Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_ListAll_Poste_User")
        '    If ds IsNot Nothing Then
        '        RCBPoste.DataTextField = "Poste"
        '        RCBPoste.DataValueField = "Poste"
        '        RCBPoste.DataSource = ds
        '        RCBPoste.DataBind()
        '    Else

        '    End If
        'Catch ex As Threading.ThreadAbortException
        'Catch ex As Rezo509Exception
        '    MessageToShow(ex.Message)
        'Catch ex As Exception
        '    MessageToShow(ex.Message)
        '    [Global].WriteError(ex, User_Connected)
        'End Try
    End Sub

    Private Sub LoadAll_Pays()
        'Dim obj As New List(Of Cls_Pays)
        'Try
        '    obj = Cls_Pays.SearchAll("1")
        '    DDL_Pays.Items.Clear()
        '    DDL_Pays.DataTextField = "NomFrGb"
        '    DDL_Pays.DataValueField = "ID"
        '    DDL_Pays.DataSource = obj
        '    DDL_Pays.DataBind()

        '    DDL_Pays.Items.Insert(0, New ListItem("Sélectionnez votre pays", 0))
        '    DDL_Pays.SelectedIndex = -1
        'Catch ex As Threading.ThreadAbortException
        'Catch ex As Rezo509Exception
        '    MessageToShow(ex.Message)
        'Catch ex As Exception
        '    MessageToShow(ex.Message)
        '    [Global].WriteError(ex, User_Connected)
        'End Try
    End Sub

    Private Sub LoadAll_Departement()
        'Dim obj As New List(Of Cls_Departement)
        'Try
        '    obj = Cls_Departement.SearchAll
        '    DDL_Departement.Items.Clear()
        '    DDL_Departement.DataTextField = "DEPARTEMENT"
        '    DDL_Departement.DataValueField = "ID"
        '    DDL_Departement.DataSource = obj
        '    DDL_Departement.DataBind()

        '    DDL_Departement.Items.Insert(0, New ListItem("Sélectionnez un Département", 0))

        '    DDL_Departement.SelectedIndex = -1
        'Catch ex As Threading.ThreadAbortException
        'Catch ex As Rezo509Exception
        '    MessageToShow(ex.Message)
        'Catch ex As Exception
        '    MessageToShow(ex.Message)
        '    [Global].WriteError(ex, User_Connected)
        'End Try
    End Sub

    Private Sub Load_Commune_ByDepartement(ByVal IDDepartement As Long)
        'Dim obj As New List(Of Cls_Commune)
        'Try
        '    obj = Cls_Commune.SearchAll_Commune_Bydepartement(IDDepartement)
        '    DDL_Commune.Items.Clear()
        '    DDL_Commune.DataTextField = "COMMUNE"
        '    DDL_Commune.DataValueField = "ID"
        '    DDL_Commune.DataSource = obj
        '    DDL_Commune.DataBind()

        '    DDL_Commune.Items.Insert(0, New ListItem("Sélectionnez Ville / Commune", -1))
        '    DDL_Commune.SelectedIndex = -1
        'Catch ex As Threading.ThreadAbortException
        'Catch ex As Rezo509Exception
        '    MessageToShow(ex.Message)
        'Catch ex As Exception
        '    MessageToShow(ex.Message)
        '    [Global].WriteError(ex, User_Connected)
        'End Try
    End Sub

    Private Sub FillComboEcole()
        Try
            '    Dim objs1 As List(Of Cls_Ecole) = Cls_Ecole.SearchAll
            '    With ddl_Ecole
            '        .Items.Clear()
            '        .DataValueField = "ID"
            '        .DataTextField = "NomEcole"
            '        .DataSource = objs1
            '        .DataBind()

            '        .Items.Insert(0, New ListItem("- Choisir  (" & objs1.Count & ") -", 0))
            '        .SelectedIndex = -1
            '    End With
        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub

#End Region

#Region "GridView Events"
    Protected Sub RadGrid_1_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid_1.ItemCommand
        Try
            Dim _id As Long = TypeSafeConversion.NullSafeLong(e.CommandArgument)
            Select Case e.CommandName
                Case "UPDATE"
                    Panel_First.Visible = True
                    DisableControls()

                    Load_DataUser(_id)
                Case "DELETE"
                    Dim obj As New Cls_User(_id)
                    'obj.Read(_id)
                    obj.Delete()
                    'LoadData_GridView_List()
                    RadGrid_1.Rebind()
                Case "RESET_PASSWORD"
                    Dim obj As New Cls_User(_id)
                    Reset_Password(obj.ID)
                Case "DISCONNECT_USER"
                    Dim obj As New Cls_User(_id)
                    'obj.Set_Status_IsForcedOut(True)

                    Cls_User.Disconnect_User_ById(_id)
                    'LoadData_GridView_List()
                    RadGrid_1.Rebind()
                    obj.Activite_Utilisateur_InRezo("Force Log Off", "Force " & obj.NOMCOMPLET & " to Log Off by" & User_Connected.Username, Request.UserHostAddress)
                    MessageToShow("L'utilisateur '<b>" & obj.NOMCOMPLET & "</b>' a été forcé de se reconnecter.", "S")
                    'Case "IsValideAccount"
                    '    Dim obj As New Cls_User(_id)
                    '    obj.Set_ValiditeCompte(Not obj.IsValideAccount)
                    '    RadGrid_1.Rebind()
                    '    obj.Activite_Utilisateur_InRezo("Set Validite Compte", "Rendre " & IIf(obj.IsValideAccount, " Invalide", " Valide") & "Compte: " & obj.Username & " / " & obj.NOMCOMPLET & " to Log Off by" & User_Connected.Username, Request.UserHostAddress)
                    '    'MessageToShow("L'utilisateur '<b>" & obj.NOMCOMPLET & "</b>' a été forcé de se reconnecter.", "S")

                Case "SEND_ANDROID_NOTIFICATION"
                    'Case "SEND_EMAIL"
                    'Panel_NotificationSendEmail.Visible = True
                    'Dim obj As New Cls_User(_id)
                    'Dim Email As String = ""
                    'If obj.ID > 0 Then
                    '    If txt_EmailTo.Text.Trim = "" Then
                    '        txt_EmailTo.Text = obj.Username
                    '        Me.Session("EMAIL") = obj.Username
                    '        Label_EmailDestination.Text = CLng(Label_EmailDestination.Text) + 1
                    '    Else
                    '        If Me.Session("EMAIL") <> obj.Username Then
                    '            txt_EmailTo.Text &= "," & obj.Username
                    '            Me.Session("EMAIL") = obj.Username
                    '            Label_EmailDestination.Text = CLng(Label_EmailDestination.Text) + 1
                    '        End If
                    '    End If
                    '    'txt_EmailTo.Text = Email.Replace("", "")
                    'End If
                    ''MessageToShow("SEND_EMAIL to " & obj.NOM_COMPLET_or_USERANAME, "S")
            End Select
        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub

    Protected Sub RadGrid_1_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid_1.ItemDataBound
        Try
            Dim cmditem As GridCommandItem = TryCast(e.Item, GridCommandItem)
            If (cmditem IsNot Nothing) Then
                ''Dim btnAdd As LinkButton = CType(cmditem.FindControl("InitInsertButton"), LinkButton)
                ''btnAdd.Attributes.Add("onclick", "javascript:ShowAddUpdateForm('Frm_TypeReunionADD.aspx', 650, 350); return false;")
                ''btnAdd.Visible = Cls_Privilege.VerifyRightOnObject(Btn_SavePARTICIPANT, User_Connected.IdGroupeuser)
            End If

            Dim gridDataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            If (gridDataItem IsNot Nothing) Then
                Dim item As GridDataItem = gridDataItem
                CType(item.FindControl("lbOrder"), Label).Text = RadGrid_1.PageSize * RadGrid_1.CurrentPageIndex + (item.RowIndex / 2)

                Dim imageediter As ImageButton = CType(item("UPDATE").Controls(0), ImageButton)
                imageediter.CommandArgument = CType(DataBinder.Eval(e.Item.DataItem, "ID"), String)
                'imageediter.Attributes.Add("onclick", "javascript:ShowAddUpdateForm('DashGestionParticipantADD.aspx?ID=" & CType(DataBinder.Eval(e.Item.DataItem, "ID"), String) & "&Action=EDIT_PARTICIPANT', 750, 450); return false;")
                'imageediter.Attributes.Add("onclick", "javascript:ShowAddUpdateForm('Frm_ParticipantADD.aspx?ID=" & CType(DataBinder.Eval(e.Item.DataItem, "ID"), String) & "&Action=EDIT_PARTICIPANT', 750, 450); return false;")

                Dim imagedelete As ImageButton = CType(item("DELETE").Controls(0), ImageButton)
                imagedelete.CommandArgument = CType(DataBinder.Eval(e.Item.DataItem, "ID"), String)

                Dim LI_Btn_Disconnect As HtmlControl = CType(item.FindControl("LI_Btn_Disconnect"), HtmlControl)
                Dim Btn_Disconnect As LinkButton = CType(item.FindControl("Btn_Disconnect"), LinkButton)
                Btn_Disconnect.Visible = CType(DataBinder.Eval(e.Item.DataItem, "Connecteryn"), Boolean)
                LI_Btn_Disconnect.Visible = check_btnResetPassWordUser

                Dim BtnResetPassword As LinkButton = CType(item.FindControl("Btn_ResetPassword"), LinkButton)
                BtnResetPassword.Visible = check_btnResetPassWordUser

                Dim Btn_SendEmail As LinkButton = CType(item.FindControl("Btn_SendEmail"), LinkButton)
                Btn_SendEmail.Visible = check_SendEmail

                'Dim Btn_IsValideAccount As ImageButton = CType(item.FindControl("Btn_IsValideAccount"), ImageButton)
                'Btn_IsValideAccount.ImageUrl = "~/images/img_succes.png"
                'Btn_IsValideAccount.ToolTip = "Rendre Invalide"
                'If (Not CType(DataBinder.Eval(e.Item.DataItem, "IsValideAccount"), Boolean)) Then
                '    Btn_IsValideAccount.ImageUrl = "~/images/cancel.png"
                '    Btn_IsValideAccount.ToolTip = "Rendre valide"
                'End If
                Dim AnotherEmail As String = ""
                'If Session([Global].SESSION_EMAIL_LIST) IsNot Nothing Then
                '    AnotherEmail = "&AnotherEmail=1"
                'End If
                Btn_SendEmail.Attributes.Add("onclick", "javascript:void(ShowAddUpdateForm('../GestionMailling/CampagneADD.aspx?IDUser=" & CType(DataBinder.Eval(e.Item.DataItem, "ID"), Long) & AnotherEmail & "',900,750)); return false;")


                Dim LinkButton_SendNotificationAndroid As LinkButton = CType(item.FindControl("LinkButton_SendNotificationAndroid"), LinkButton)
                Dim IdFirebaseToken As String = CType(DataBinder.Eval(e.Item.DataItem, "IdFirebaseToken"), String)
                LinkButton_SendNotificationAndroid.Visible = False
                If Not IdFirebaseToken.Trim.Equals("") Then
                    LinkButton_SendNotificationAndroid.Visible = True
                End If
                'LinkButton_SendNotificationAndroid.Attributes.Add("onclick", "javascript:void(ShowAddUpdateForm('../GestionMailling/CampagneADD.aspx?IDUser=" & CType(DataBinder.Eval(e.Item.DataItem, "ID"), Long) & AnotherEmail & "',900,750)); return false;")
                LinkButton_SendNotificationAndroid.Attributes.Add("onclick", "javascript:void(ShowAddUpdateForm('../FireBaseNotification/Frm_FireBaseNotification.aspx?" & [Global].ID_USER & "=" & CType(DataBinder.Eval(e.Item.DataItem, "ID"), Long) & "', 650, 600)); return false;")
                REM Privilege
                'imageediter.Visible = Cls_Privilege.VerifyRightOnObject(Btn_SaveEditUser, User_Connected.IdGroupeuser)
                'imagedelete.Visible = Cls_Privilege.VerifyRightOnObject(Btn_DeleteUser, User_Connected.IdGroupeuser)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub

    Private Sub RadGrid_1_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid_1.NeedDataSource
        If IsPostBack Then
            LoadData_GridView_List(False)
        End If
    End Sub

#End Region

#Region "EVENTS BUTTON"
    Private Sub ControlPays(ByVal IDPays As Long)
        If IDPays > 0 Then
            If IDPays = 1 Then
                TR_Departement.Visible = True
                TR_Commune.Visible = True
            Else
                TR_Departement.Visible = False
                TR_Commune.Visible = False
            End If
        End If
    End Sub

    Protected Sub DDL_Pays_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDL_Pays.SelectedIndexChanged
        ControlPays(CLng(DDL_Pays.SelectedValue))
    End Sub

    Protected Sub DDL_Departement_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DDL_Departement.SelectedIndexChanged
        Load_Commune_ByDepartement(CLng(DDL_Departement.SelectedValue))
    End Sub

    Protected Sub Btn_Save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_Save.Click
        Try
            SAVE()
            'Clear_Field()
            'Panel_First.Visible = False
            'EnableControls()
            LoadData_GridView_List()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub

    Protected Sub Btn_Annuler_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_Annuler.Click
        NewToDo()
        EnableControls()
    End Sub

    ''Protected Sub Btn_Nouveau_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton_AddNew_Annonce.Click 'Btn_Nouveau.Click,
    ''    NewToDo()
    ''    Panel_First.Visible = True
    ''    DisableControls()
    ''    Clear_Field()
    ''End Sub

    Private Sub Reset_Password(ByVal IdUser As Long)
        Try
            Dim obj As New Cls_User(IdUser)
            If txt_MotdePasse.Text.Length > 0 Then
                obj.Password = txt_MotdePasse.Text
                GenerateRandomCode = txt_MotdePasse.Text
            Else
                obj.Password = txtEmailCompte.Text
                GenerateRandomCode = txtEmailCompte.Text
            End If

            If CBXGenererLeMotDePasse.Checked Then
                GenerateRandomCode = GlobalFunctions.GenerateRandomCode
                obj.Password = GenerateRandomCode
            End If
            obj.Mustchangepassword = True
            obj.ResetPassword(obj.Username)
            obj.Save(User_Connected.Username)
            If CBX_SendAccountAndPassword.Checked Then
                Send_Notification_Account(obj, "Mot de passe réinitialiser")
            End If
            _message = "Mot de passe utilisateur <b>" & obj.Civilite & " " & obj.NOM_COMPLET_AND_USERANAME & "</b> vient d'être réinitialisé." & _
                "<br />L'utilisateur '<b>" & obj.Username & "</b>'  doit changer son Mot de passe."
            MessageToShow(_message, "S")
        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub

    Protected Sub Btn_Reset_MotdePasse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_Reset_MotdePasse.Click
        Reset_Password(CLng(txt_ID.Text))
    End Sub

    Protected Sub Btn_Search_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_Search.Click
        IS_Search = True
        LoadData_GridView_List()
        'If Not IS_Trouve Then
        '    Label_Result.Visible = True
        'End If
    End Sub

    'Protected Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
    '    Panel_NotificationSendEmail.Visible = False
    '    txt_EmailTo.Text = ""
    'End Sub

    'Protected Sub Btn_SendNotification_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_SendNotification.Click

    'End Sub

    Protected Sub LinkButton_AddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton_AddNew.Click
        txt_ID.Text = "0"
        Panel_First.Visible = True
        'DIV_ListePlan.Visible = False
        'DIV_LISTEProduitsServices.Visible = False
        DisableControls()
    End Sub

#End Region

#Region ""

    REM SAVE Photo
    Protected Sub Btn_ChargerPhoto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_ChargerPhoto.Click
        Try
            ' --- Save --- '
            Dim obj As New Cls_User(TypeSafeConversion.NullSafeLong(txt_ID.Text))
            ' PHOTO
            If FileUpload1.FileName.Length > 0 AndAlso FileUpload1.PostedFile IsNot Nothing Then
                Is_Good_Extension(FileUpload1)
                Dim strLen As Integer = CInt(FileUpload1.PostedFile.InputStream.Length)
                Dim strArr(strLen) As Byte
                ' Read stream into byte array.
                FileUpload1.PostedFile.InputStream.Read(strArr, 0, strLen)
                obj.Photo = strArr
            Else
                IS_SendMail = False
                Throw New Rezo509Exception("Téléchargez la photo à partir de votre ordinateur.<br />-La taille ne doit pas dépassée <b>100Kb</b>.<br /><a href='#' class='LinkSite' target='_blank'><i>Technique pour compresser une Image</i></a>.")
            End If
            obj.Save(User_Connected.Username)
            User_Connected.Activite_Utilisateur_InRezo(IIf(CLng(txt_ID.Text) > 0, "UPDATE ", "ADD ") & "Photo utilisateur", obj.ID & " - " & obj.NOMCOMPLET, Request.UserHostAddress)

            txt_ID.Text = obj.ID
            Image_participant.ImageUrl = "~/Show_Image.aspx?Action=" & [Global].PHOTO_UTILISATEUR & "&ID=" & obj.ID
        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub

    REM verification de l'extention du fichier de la Photo
    Public Sub Is_Good_Extension(ByVal _FileUpload_ As FileUpload)
        Dim Liste_Ext() As String = Split(".jpg,.png,.bmp,.jpeg,.gif", ",")
        Dim Ext_Ok As Boolean = False
        REM 1Kb = 1024b |-|  50Kb = 51200b  |-|  1Mb = 1024Kb  |-|   1Mb = 1048576 byte
        If _FileUpload_.PostedFile.ContentLength > 204800 Then ' Taille limite 200Kb
            IS_SendMail = False
            Throw New Rezo509Exception("La taille de l'image est trop volumineuse ! Taille limite 200 Kilo-octet [100Kb]")
        End If
        Dim _GetExtension As String = System.IO.Path.GetExtension(_FileUpload_.FileName).ToLower

        For i As Integer = 0 To Liste_Ext.Length - 1
            If _GetExtension.Trim.ToLower = Liste_Ext(i).Trim.ToLower Then
                Ext_Ok = True
                Exit For
            End If
        Next
        If Not Ext_Ok Then
            IS_SendMail = False
            Throw New Rezo509Exception("Ce fichier n'est pas une image de type (gif, jpg, jpeg, png, bmp)...")
        End If
    End Sub

#End Region

#Region "NOTIFICATION EMAIL"
    REM Mail Notification to Admin
    Public Sub Send_Notification_Account(ByVal _ObjUser As Cls_User, Optional ByVal _Sujet As String = "Création Compte")
        Dim SMTPServer As String = [Global].SMTPServer
        Dim EmailFrom As String = [Global].EmailFrom
        Dim EmailPassword As String = [Global].EmailPassword

        Dim _SendTo As String = System.Configuration.ConfigurationManager.AppSettings("EmailToCopyTo")
        Dim _template As String

        Try
            Dim Courrier As New Cls_SendMail(SMTPServer, EmailFrom, EmailPassword)

            _template = "<h3 style='color:#0366FF;'>" & _Sujet & "</h3>"
            _template &= "<b> Date :</b>" & Now.ToString("dd-MM-yyyy") & "<hr />"
            _template &= "<b>Compte:  </b>" & _ObjUser.Username
            If CBX_SendAccountAndPassword.Checked Then
                _template &= "<br /><b>Mot de passe: </b>" & GenerateRandomCode & ""
            End If
            _template &= "<hr /><b>Prénom/Nom:  </b>" & _ObjUser.NOMCOMPLET
            _template &= "<br /><b>Téléphone: </b> " & _ObjUser.Telephone


            Courrier.From = EmailFrom
            Courrier.Subject = _Sujet
            Courrier.SendTo = _ObjUser.Username
            If CBX_SendAccountAndPassword.Checked Then
                Courrier.BCC = User_Connected.Username
            End If

            Courrier.DispalyName = "Administration"
            Courrier.Body = _template
            Courrier.MailMessage_2()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub
#End Region

#Region "PLAN REZO509"

#Region "Load DATA"
    Private Sub BindGridPlan(Optional ByVal _refresh As Boolean = True)
        '    Dim objs As List(Of Cls_PL_Rezo509PlanUser)
        '    Dim _ret As Long = 0
        '    Try
        '        objs = Cls_PL_Rezo509PlanUser.SearchAllByUser(TypeSafeConversion.NullSafeLong(txt_ID.Text))
        '        RadGrid_Plan.DataSource = objs
        '        If _refresh Then
        '            RadGrid_Plan.DataBind()
        '        End If
        '        _ret = objs.Count
        '        rbtnAddPlan.Visible = IIf(_ret > 0, False, True)
        '        LabelTitrePlan.Text = "Plans Rezo509 <small class=""badge  bg-blue"">" & _ret & "</small>"
        '    Catch ex As Threading.ThreadAbortException
        '    Catch ex As Rezo509Exception
        '        MessageToShow(ex.Message)
        '    Catch ex As Exception
        '        MessageToShow(ex.Message)
        '        [Global].WriteError(ex, User_Connected)
        '    End Try
    End Sub
#End Region

#Region "RADGRID EVENTS"
    'Protected Sub RadGrid_Plan_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid_Plan.ItemCommand
    '    Try
    '        If e.CommandName = Telerik.Web.UI.RadGrid.ExportToExcelCommandName Then
    '            RadGrid_Plan.ExportSettings.ExportOnlyData = True
    '            RadGrid_Plan.GridLines = GridLines.Both
    '            RadGrid_Plan.ExportSettings.IgnorePaging = True
    '            RadGrid_Plan.ExportSettings.OpenInNewWindow = False
    '            RadGrid_Plan.ExportSettings.FileName = "Liste des RadGrid_Plan"
    '            RadGrid_Plan.MasterTableView.Columns(0).Visible = False
    '            RadGrid_Plan.MasterTableView.ExportToExcel()
    '        End If

    '        Select Case e.CommandName
    '            'Case "delete"
    '            '    Dim obj As New Cls_PL_Rezo509PlanUser(TypeSafeConversion.NullSafeLong(e.CommandArgument))
    '            '    obj.Delete()
    '            '    Cls_User.SetIsHavePlan_User_ById(obj.ID_User, False)
    '            '    User_Connected.Activite_Utilisateur_InRezo("DELETE Plan/Utilisateur ", obj.ID & " - Nomcomplet_et_Username:" & obj.Nomcomplet_et_Username & " - NomPlan:" & obj.NomPlan & " Prop:", Request.UserHostAddress)
    '            '    MessageToShow("Plan supprimé", "S")
    '            '    RadGrid_Plan.Rebind()
    '        End Select
    '    Catch ex As Threading.ThreadAbortException
    '    Catch ex As Rezo509Exception
    '        MessageToShow(ex.Message)
    '    Catch ex As Exception
    '        MessageToShow(ex.Message)
    '        [Global].WriteError(ex, User_Connected)
    '    End Try
    'End Sub

    'Protected Sub RadGrid_Plan_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid_Plan.ItemDataBound
    '    Try
    '        Dim gridDataItem = TryCast(e.Item, GridDataItem)
    '        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
    '            'Dim _lnk As HyperLink = DirectCast(gridDataItem.FindControl("hlk"), HyperLink)
    '            'Dim _lbl_ID As Label = DirectCast(gridDataItem.FindControl("lbl_ID"), Label)
    '            '_lnk.Attributes.Clear()
    '            '_lnk.Attributes.Add("onclick", "javascript:void(ShowAddUpdateForm('Frm_SR_PRIVILEGE_SERVICESADD.aspx?ID=" & CLng(_lbl_ID.Text) & "', 750, 400));")
    '        End If

    '        If (gridDataItem IsNot Nothing) Then
    '            Dim item As GridDataItem = gridDataItem
    '            CType(item.FindControl("lbOrder"), Label).Text = rdgSR_PRIVILEGE_SERVICES.PageSize * rdgSR_PRIVILEGE_SERVICES.CurrentPageIndex + (item.RowIndex / 2)

    '            Dim imagedelete As ImageButton = CType(item("delete").Controls(0), ImageButton)
    '            Dim imageediter As ImageButton = CType(item("editer").Controls(0), ImageButton)
    '            imagedelete.ToolTip = "Effacer"
    '            imageediter.ToolTip = "Editer"
    '            imagedelete.CommandArgument = CType(DataBinder.Eval(e.Item.DataItem, "ID"), String)
    '            imageediter.Attributes.Add("onclick", "javascript:void(ShowAddUpdateForm('../GestionPlansEtServices/Frm_PlanRezo509UserADD.aspx?IDUser=" & TypeSafeConversion.NullSafeLong(txt_ID.Text) & "&ID=" & CType(DataBinder.Eval(e.Item.DataItem, "ID"), Long) & "',800, 450));")
    '            REM Privilege
    '            imageediter.Visible = Cls_Privilege.VerifyRightOnObject(Btn_SavePRODUITS, User_Connected.IdGroupeuser)
    '            imagedelete.Visible = Cls_Privilege.VerifyRightOnObject(Btn_DeletePRODUITS, User_Connected.IdGroupeuser)
    '        End If
    '    Catch ex As Threading.ThreadAbortException
    '    Catch ex As Rezo509Exception
    '        MessageToShow(ex.Message)
    '    Catch ex As Exception
    '        MessageToShow(ex.Message)
    '        [Global].WriteError(ex, User_Connected)
    '    End Try
    'End Sub

    'Protected Sub RadGrid_Plan_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid_Plan.NeedDataSource
    '    If IsPostBack Then
    '        BindGridPlan(False)
    '    End If
    'End Sub

    Protected Sub RadAjaxManager1_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
        Try
            Select Case e.Argument
                Case "Reload"
                    BindGrid(True)
                Case "ReloadPlan"
                    BindGridPlan(True)
            End Select
        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub

#End Region

#End Region

#Region "PRODUITS / SERVICES"

#Region "Load DATA"
    Private Sub BindGrid(Optional ByVal _refresh As Boolean = True)
        'Dim objs As List(Of Cls_SR_PRIVILEGE_SERVICES)
        'Dim _ret As Long = 0
        'Try
        '    objs = Cls_SR_PRIVILEGE_SERVICES.SearchAllByUser(TypeSafeConversion.NullSafeLong(txt_ID.Text))
        '    rdgSR_PRIVILEGE_SERVICES.DataSource = objs
        '    If _refresh Then
        '        rdgSR_PRIVILEGE_SERVICES.DataBind()
        '    End If
        '    _ret = objs.Count
        '    LabelTitreProduit.Text = "Produits / Services par Utilisateur <small class=""badge  bg-blue"">" & _ret & "</small>"
        'Catch ex As Threading.ThreadAbortException
        'Catch ex As Rezo509Exception
        '    MessageToShow(ex.Message)
        'Catch ex As Exception
        '    MessageToShow(ex.Message)
        '    [Global].WriteError(ex, User_Connected)
        'End Try
    End Sub
#End Region

#Region "RADGRID EVENTS"
    'Protected Sub rdgSR_PRIVILEGE_SERVICES_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rdgSR_PRIVILEGE_SERVICES.ItemCommand
    '    Try
    '        If e.CommandName = Telerik.Web.UI.RadGrid.ExportToExcelCommandName Then
    '            rdgSR_PRIVILEGE_SERVICES.ExportSettings.ExportOnlyData = True
    '            rdgSR_PRIVILEGE_SERVICES.GridLines = GridLines.Both
    '            rdgSR_PRIVILEGE_SERVICES.ExportSettings.IgnorePaging = True
    '            rdgSR_PRIVILEGE_SERVICES.ExportSettings.OpenInNewWindow = False
    '            rdgSR_PRIVILEGE_SERVICES.ExportSettings.FileName = "Liste des SR_PRIVILEGE_SERVICES"
    '            rdgSR_PRIVILEGE_SERVICES.MasterTableView.Columns(0).Visible = False
    '            rdgSR_PRIVILEGE_SERVICES.MasterTableView.ExportToExcel()
    '        End If

    '        Select Case e.CommandName
    '            Case "delete"
    '                Dim obj As New Cls_SR_PRIVILEGE_SERVICES(TypeSafeConversion.NullSafeLong(e.CommandArgument))
    '                obj.Delete()
    '                Cls_User.SetIsHaveOtherService_User_ById(obj.ID_User, False)
    '                User_Connected.Activite_Utilisateur_InRezo("DELETE Produits / Services par Utilisateur ", obj.ID & " - Nomcomplet_et_Username:" & obj.Nomcomplet_et_Username & " - Nom Service:" & obj.NomService & " Prop:", Request.UserHostAddress)
    '                MessageToShow("Produits / Services supprimé", "S")
    '                rdgSR_PRIVILEGE_SERVICES.Rebind()
    '        End Select
    '    Catch ex As Threading.ThreadAbortException
    '    Catch ex As Rezo509Exception
    '        MessageToShow(ex.Message)
    '    Catch ex As Exception
    '        MessageToShow(ex.Message)
    '        [Global].WriteError(ex, User_Connected)
    '    End Try
    'End Sub

    'Protected Sub rdgSR_PRIVILEGE_SERVICES_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rdgSR_PRIVILEGE_SERVICES.ItemDataBound
    '    Try
    '        Dim gridDataItem = TryCast(e.Item, GridDataItem)
    '        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
    '            'Dim _lnk As HyperLink = DirectCast(gridDataItem.FindControl("hlk"), HyperLink)
    '            'Dim _lbl_ID As Label = DirectCast(gridDataItem.FindControl("lbl_ID"), Label)
    '            '_lnk.Attributes.Clear()
    '            '_lnk.Attributes.Add("onclick", "javascript:void(ShowAddUpdateForm('Frm_SR_PRIVILEGE_SERVICESADD.aspx?ID=" & CLng(_lbl_ID.Text) & "', 750, 400));")
    '        End If

    '        If (gridDataItem IsNot Nothing) Then
    '            Dim item As GridDataItem = gridDataItem
    '            CType(item.FindControl("lbOrder"), Label).Text = rdgSR_PRIVILEGE_SERVICES.PageSize * rdgSR_PRIVILEGE_SERVICES.CurrentPageIndex + (item.RowIndex / 2)

    '            Dim imagedelete As ImageButton = CType(item("delete").Controls(0), ImageButton)
    '            Dim imageediter As ImageButton = CType(item("editer").Controls(0), ImageButton)
    '            imagedelete.ToolTip = "Effacer ce Produits / Services"
    '            imageediter.ToolTip = "Editer ce Produits / Services"
    '            imagedelete.CommandArgument = CType(DataBinder.Eval(e.Item.DataItem, "ID"), String)
    '            imageediter.Attributes.Add("onclick", "javascript:void(ShowAddUpdateForm('Frm_SR_PRIVILEGE_SERVICESADD.aspx?IDUser=" & TypeSafeConversion.NullSafeLong(txt_ID.Text) & "&ID=" & CType(DataBinder.Eval(e.Item.DataItem, "ID"), Long) & "',800, 450));")
    '            REM Privilege
    '            imageediter.Visible = Cls_Privilege.VerifyRightOnObject(Btn_SavePRODUITS, User_Connected.IdGroupeuser)
    '            imagedelete.Visible = Cls_Privilege.VerifyRightOnObject(Btn_DeletePRODUITS, User_Connected.IdGroupeuser)
    '        End If
    '    Catch ex As Threading.ThreadAbortException
    '    Catch ex As Rezo509Exception
    '        MessageToShow(ex.Message)
    '    Catch ex As Exception
    '        MessageToShow(ex.Message)
    '        [Global].WriteError(ex, User_Connected)
    '    End Try
    'End Sub

    'Protected Sub rdgSR_PRIVILEGE_SERVICES_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rdgSR_PRIVILEGE_SERVICES.NeedDataSource
    '    If IsPostBack Then
    '        BindGrid(False)
    '    End If
    'End Sub
#End Region

#End Region

End Class
