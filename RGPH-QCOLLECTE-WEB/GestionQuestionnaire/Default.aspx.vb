REM Generate By [GENERIC 12] Application *******
REM  Class Frm_Questions_Reponses

REM Date:18-Jan-2018 02:07 PM
Imports Microsoft
Imports System.Data
Imports System.Collections.Generic
Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer
Imports Telerik.Web.UI
Imports RGPH_QCOLLECTE_Library

Partial Class GestionQuestionnaire_Default
    Inherits Cls_BasePage ' LA CLASSE DE LA PAGE HERITE DE CETTE CLASSE DANS LE CAS OU NOUS AVONS UNE APPLICATION WEB multilingue


#Region "ATTRIBUTS"
    Private _message As String  ' VARIABLE SERVANT A LA RECUPERATION DE TOUS LES MESSAGES D'ECHECS OU DE SUCCES

    REM DEFINITION ET INITIALISATION DES CONSTANTE POUR LA SECURITE
    Private Const Nom_page As String = "PAGE-API-GENERATE-CODE-JSON"  ' POUR LA PAGE
    Private Const Btn_Save As String = "Bouton-SAVE-STATUT"       ' POUR LE BOUTON D'ENREGISTREMENT
    Private Const Btn_Edit As String = "Bouton-EDIT-STATUT"       ' POUR LE BOUTON DE MODIFICATION
    Private Const Btn_Delete As String = "Bouton-DELETE-STATUT"   ' POUR LE BOUTON DE SUPPRESSION

    Dim User_Connected As Cls_User          ' INSTANCE DE LA CLASSE UTILISATEUR - UTILISER POUR L'UTILISATEUR EN SESSION 
    Dim Is_Acces_Page As Boolean = True     ' LA VARIABLE SERVANT DE TEST POUR DONNEER L'ACCES A LA PAGE
    Dim GetOut As Boolean = False           ' LA VARIABLE SERVANT DE TEST POUR REDIRIGER L'UTILISATEUR VERS LA PAGE DE CONNEXION
    Dim PAGE_MERE As Long = 0 ' PAS TROP IMPORTANT...
    Dim PAGE_TITLE As String = ""
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Expires = -1
        Panel_Msg.Visible = False
        PAGE_TITLE = " Tableau de Bord"
        Page.Title = [Global].Global_APP_NAME_SIGLE & " | " & PAGE_TITLE

        SYSTEME_SECURITE()  ' APPEL A LA METHODE SERVANT A TESTER LES COMPOSANTS DE LA PAGE Y COMPRIS LA PAGE ELLE MEME 

        '--- Si l'utilisateur n'a Access a la page les informations ne sont pas charger dans la Page_Load 
        If Is_Acces_Page Then
            If Not IsPostBack Then
                'Dim _action As String = TypeSafeConversion.NullSafeString(Request.QueryString([Global].ACTION))
                'If _action.Equals([Global].DATA_MODULE_QUESTION_REPONSE_PAR_MODULE) Then
                '    Section0.Visible = False
                '    'Section1.Visible = False
                'Else
                Section_DIVSession.Visible = False

                    Label_Titre.Text = PAGE_TITLE
                    'Btn_ADD_Questions_Reponses.Attributes.Add("onclick", "javascript:void(ShowAddUpdateForm('Frm_Questions_ReponsesADD.aspx?" & [Global].ACTION & "=" & [Global].HideMenuHeader & "', 950, 650)); return false;")
                    'Btn_ADD_Questions_Reponses.Attributes.Add("onclick", "javascript:Open_Window('Frm_Questions_ReponsesADD.aspx', '_self',500,400); return false;") 
                    'BindGrid()

                    DIV_Module_JSON_DATA.Attributes.Add("onclick", "javascript:OpenWindow('ExportJsonData.aspx?" & [Global].ACTION & "=" & [Global].DATA_MODULE & "'); return false;")
                    DIV_CategorieQuestion.Attributes.Add("onclick", "javascript:OpenWindow('ExportJsonData.aspx?" & [Global].ACTION & "=" & [Global].DATA_CATEGORIE_QUESTION & "'); return false;")
                    DIV_Questions.Attributes.Add("onclick", "javascript:OpenWindow('ExportJsonData.aspx?" & [Global].ACTION & "=" & [Global].DATA_QUESTION & "'); return false;")
                    DIV_Reponses.Attributes.Add("onclick", "javascript:OpenWindow('ExportJsonData.aspx?" & [Global].ACTION & "=" & [Global].DATA_REPONSE & "'); return false;")
                    DIV_FormulaireCollecte.Attributes.Add("onclick", "javascript:OpenWindow('ExportJsonData.aspx?" & [Global].ACTION & "=" & [Global].DATA_QUESTION_MODULE & "'); return false;")

                    FillCombo_TypeModule()
                    BindGrid()
                    setValueButon()
                'End If
            End If
            End If
    End Sub


#Region "SECURITE"
    Public Sub SYSTEME_SECURITE()
        Try
            User_Connected = [Global].KeepUserContinuesToWork(User_Connected)

            'CType(Page.Master.FindControl("HyperLink_Dashboard"), HtmlControl).Attributes.Add("class", "active ")
            'CType(Page.Master.FindControl("i_Questions_Reponses"), HtmlControl).Attributes.Add("class", "fa fa-folder-open fa-lg ")
            CType(Page.Master.FindControl("LI_Dashboard"), HtmlControl).Attributes.Add("class", "parent open")
            CType(Page.Master.FindControl("LI_Default"), HtmlControl).Attributes.Add("class", "active")

            LiteralStyleCSS.Text = ""
            If Request.QueryString([Global].ACTION) IsNot Nothing Then
                Select Case Request.QueryString([Global].ACTION)
                    Case [Global].HideMenuHeader
                        CType(Page.Master.FindControl([Global].Head_Nav_Menu), HtmlControl).Visible = False
                        CType(Page.Master.FindControl([Global].Head_Nav_MenuVertical), HtmlControl).Visible = False
                        Dim StyleCss As String = "<style type=""text/css""> #cl-wrapper { padding-top: 0px; } </style>"
                        LiteralStyleCSS.Text = StyleCss
                    Case Else
                        'span_SaveInfo_CloseAfter.Visible = False
                        'Btn_SaveInfo_CloseAfter.Visible = False
                End Select
            Else
                'span_SaveInfo_CloseAfter.Visible = False
                'Btn_SaveInfo_CloseAfter.Visible = False
            End If

            If Session([Global].GLOBAL_SESSION) IsNot Nothing Then
                User_Connected = CType(Session([Global].GLOBAL_SESSION), Cls_User)
                If Not Cls_Privilege.VerifyRightOnObject(Nom_page, User_Connected.IdGroupeuser) Then    ' VERIFICATION SI L'UTILISATEUR N'A PAS ACCES A LA PAGE
                    _message = [Global].NO_ACCES_PAGE
                    MessageToShow(_message)
                    Is_Acces_Page = False

                    Panel_First.Visible = False
                Else    ' SI L'UTILISATEUR A ACCES A LA PAGE ON VERIFIE POUR LES BOUTONS ET LES LIENS
                    '---  Okey vous avez acces a la page ---'
                    'Dim _check As Boolean = Cls_Privilege.VerifyRightOnObject(Btn_Save, User_Connected.IdGroupeuser)
                    'Btn_ADD_Questions_Reponses.Visible = _check
                    'rdgQuestions_Reponses.MasterTableView.Columns.FindByUniqueNameSafe("editer").Visible = _check
                    If Request.QueryString([Global].ACTION) IsNot Nothing Then
                        If Request.QueryString([Global].ACTION).Equals([Global].HideMenuHeader) Then
                            'Btn_ADD_Questions_Reponses.Visible = _check
                        End If
                    End If
                    '_check = Cls_Privilege.VerifyRightOnObject(Btn_Delete, User_Connected.IdGroupeuser)
                    'rdgQuestions_Reponses.MasterTableView.Columns.FindByUniqueNameSafe("delete").Visible = _check
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
                        '_message = "Session expirée."
                        'MessageToShow(_message)
                        Is_Acces_Page = True
                    End If
                Catch ex As Exception
                    GetOut = True
                    '_message = "Session expirée."
                    'MessageToShow(_message)
                End Try
            End If

            If GetOut Then ' REDIRECTIONNEMENT DE L'UTILISATUER OU PAS.
                CType(Page.Master.FindControl([Global].htmlMasterPage), HtmlControl).Attributes.Add("class", "lockscreen")
                CType(Page.Master.FindControl([Global].bodyMasterPage), HtmlControl).Attributes.Add("class", "texture")
                CType(Page.Master.FindControl([Global].Head_Nav_Menu), HtmlControl).Visible = False
                CType(Page.Master.FindControl([Global].Head_Nav_MenuVertical), HtmlControl).Visible = False

                Is_Acces_Page = False
                PageHeader.Attributes.Add("style", "visibility:hidden;")
                Panel_First.Visible = False
                LoginWUC.Visible = True
                Session([Global].GLOBAL_PAGENAME) = System.Web.HttpContext.Current.Request.Url.ToString()
                'Response.Redirect([Global].PAGE_LOGIN)
            End If

        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            Is_Acces_Page = False
            Panel_First.Visible = False
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub
#End Region

#Region "Other Method"
#End Region

#Region "Other Method"
    Private Sub MessageToShow(ByVal _message As String, Optional ByVal E_or_S As String = "E", Optional ByVal ShowPopUp As Boolean = True)
        Panel_Msg.Visible = True
        GlobalFunctions.Message_Image(Image_Msg, E_or_S)
        Label_Msg.Text = _message
        If ShowPopUp Then
            RadAjaxManager1.ResponseScripts.Add("alert('" & [Global].GetTextFromHtml(_message).Replace("'", "\'") & "');")
            'Dialogue.alert([Global].GetTextFromHtml(_message))
        End If
        If E_or_S = "S" Then
            Style_Division(DIV_Msg, "alert alert-success alert-dismissable")
            Style_Division(Icon_Msg, "fa  fa-thumbs-up")
        Else
            Style_Division(DIV_Msg, "alert alert-danger alert-dismissable")
            Style_Division(Icon_Msg, "fa  fa-thumbs-down")
        End If
    End Sub
#End Region

#Region "Load DATA"

    Private Sub FillCombo_TypeModule()
        Try
            Dim objs1 As List(Of Cls_TypeModule) = Cls_TypeModule.SearchAll
            With CheckBoxList_TypeModule
                .DataSource = objs1
                .DataValueField = "ID"
                .DataTextField = "TypeModuleSTR"
                .DataBind()
                '.Items.Insert(0, New ListItem(" - TOUS LES TYPE (TOTAL:" & objs1.Count & ") - ", 0))
                '.SelectedIndex = -1
                '.Items.Sort()
                '.Items.Insert(0, New RadComboBoxItem(" - Choisir -", ""))
                '.SelectedIndex = 0
                '.EmptyMessage = "- Choisir -"
            End With
        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub

    Private Sub FillCombo_Module(CheckBoxList_Module_Formulaire As CheckBoxList, ByVal ID_TypeModule As Long)
        Try
            Dim objs1 As List(Of Cls_Module) = Cls_Module.SearchAllBy_TypeModule(ID_TypeModule)
            With CheckBoxList_Module_Formulaire
                .DataSource = objs1
                .DataValueField = "CodeModule"
                .DataTextField = "Code_NomModuleSTR"
                .DataBind()
            End With
        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub

    Private Sub BindGrid(Optional ByVal _refresh As Boolean = True)
        Dim objs As List(Of Cls_TypeModule)
        Dim _ret As Long = 0
        Try
            objs = Cls_TypeModule.SearchAll
            RadGrid1.DataSource = objs
            If _refresh Then
                RadGrid1.DataBind()
            End If
            _ret = objs.Count
            'Label_Titre.Text = PAGE_TITLE & "  <small class=""badge badge-primary"">" & _ret & "</small>"
        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub

    Private Sub SetAll_DATA_MODULE_QUESTION_OU_REPONSE()
        Dim _questionList As New List(Of Cls_Questions)
        Dim _ReponseList As New List(Of Cls_Questions_Reponses)
        Dim onElementCheck As Boolean = False
        'Dim ValJson As String = ""
        Try
            REM Test pour voir si la case Question est cocher
            'If CBX_Questions.Checked Then
            REM Parcourt CheckBoxList Formulaire
            For Each _item As ListItem In CheckBoxList_TypeModule.Items
                Dim _qList As List(Of Cls_Questions)
                If _item.Selected Then
                    'onElementCheck = True

                    _qList = Cls_Questions.SearchAll(TypeSafeConversion.NullSafeLong(_item.Value))
                    If _qList IsNot Nothing OrElse _qList.Count > 0 Then 'Verification pour la liste des questions
                        For Each _quest As Cls_Questions In _qList 'Parcourt de la liste des questions
                            If CBX_Questions.Checked Then
                                _questionList.Add(_quest)
                            End If
                            If CBX_Reponse.Checked Then 'Test pour voir si la case Reponse est cocher
                                Dim _repList As List(Of Cls_Questions_Reponses) = Cls_Questions_Reponses.SearchAllBy_CodeQuestion(_quest.CodeQuestion)
                                If _repList IsNot Nothing OrElse _repList.Count > 0 Then 'Verification pour la liste des Reponses
                                    For Each _reponse As Cls_Questions_Reponses In _repList 'Parcourt de la liste des Reponses
                                        _ReponseList.Add(_reponse)
                                    Next
                                End If
                            End If
                        Next
                    End If
                End If
            Next
            'End If

            REM Gestion des sessions  
            DIV_Module_QuestionSession.Visible = False
            If _questionList.Count > 0 Then
                onElementCheck = True
                Me.Session([Global].DATA_MODULE_QUESTION_SESSION) = _questionList
                DIV_Module_QuestionSession.Visible = True
                DIV_Module_QuestionSession.Attributes.Add("onclick", "javascript:OpenWindow('ExportJsonData.aspx?" & [Global].ACTION & "=" & [Global].DATA_MODULE_QUESTION_SESSION & "'); return false;")
                Literal_QuestionSession.Text = " [ " & _questionList.Count & " ]"
            End If

            DIV_Module_ReponseSession.Visible = False
            If _ReponseList.Count > 0 Then
                onElementCheck = True
                Me.Session([Global].DATA_MODULE_REPONSES_SESSION) = _ReponseList
                DIV_Module_ReponseSession.Visible = True
                DIV_Module_ReponseSession.Attributes.Add("onclick", "javascript:OpenWindow('ExportJsonData.aspx?" & [Global].ACTION & "=" & [Global].DATA_MODULE_REPONSES_SESSION & "'); return false;")
                Literal_ReponseSession.Text = "  [ " & _ReponseList.Count & " ]"
            End If

            If onElementCheck Then
                Section0.Visible = False
                Section_DIVSession.Visible = True
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub

    Private Sub SetAll_DATA_Question_Or_Reponse_From_RadGrid()
        Dim _questionList As New List(Of Cls_Questions)
        Dim _ReponseList As New List(Of Cls_Questions_Reponses)
        Dim onElementCheck As Boolean = False
        Try
            REM Test pour voir si la case Question est cocher
            REM Parcourt CheckBox du type de Module dans le RadGrid
            For Each lign As GridDataItem In RadGrid1.Items
                Dim _CheckBox_TypeModule As CheckBox = CType(lign.FindControl("CheckBox_TypeModule"), CheckBox)
                Dim _txt_IDTypeModule As TextBox = CType(lign.FindControl("txt_IDTypeModule"), TextBox)

                Dim _qList As List(Of Cls_Questions)
                REM Parcourt CheckBoxList Formulaire
                If _CheckBox_TypeModule.Checked Then
                    _qList = Cls_Questions.SearchAll(TypeSafeConversion.NullSafeLong(_txt_IDTypeModule.Text))
                    If _qList IsNot Nothing OrElse _qList.Count > 0 Then 'Verification pour la liste des questions
                        For Each _quest As Cls_Questions In _qList 'Parcourt de la liste des questions
                            If CBX_Questions.Checked Then
                                _questionList.Add(_quest)
                            End If
                            If CBX_Reponse.Checked Then 'Test pour voir si la case Reponse est cocher
                                Dim _repList As List(Of Cls_Questions_Reponses) = Cls_Questions_Reponses.SearchAllBy_CodeQuestion(_quest.CodeQuestion)
                                If _repList IsNot Nothing OrElse _repList.Count > 0 Then 'Verification pour la liste des Reponses
                                    For Each _reponse As Cls_Questions_Reponses In _repList 'Parcourt de la liste des Reponses
                                        _ReponseList.Add(_reponse)
                                    Next
                                End If
                            End If
                        Next
                    End If
                End If
            Next

            REM Gestion des sessions  
            DIV_Module_QuestionSession.Visible = False
            If _questionList.Count > 0 Then
                onElementCheck = True
                Me.Session([Global].DATA_MODULE_QUESTION_SESSION) = _questionList
                DIV_Module_QuestionSession.Visible = True
                DIV_Module_QuestionSession.Attributes.Add("onclick", "javascript:OpenWindow('ExportJsonData.aspx?" & [Global].ACTION & "=" & [Global].DATA_MODULE_QUESTION_SESSION & "'); return false;")
                Literal_QuestionSession.Text = " [ " & _questionList.Count & " ]"
            End If

            DIV_Module_ReponseSession.Visible = False
            If _ReponseList.Count > 0 Then
                onElementCheck = True
                Me.Session([Global].DATA_MODULE_REPONSES_SESSION) = _ReponseList
                DIV_Module_ReponseSession.Visible = True
                DIV_Module_ReponseSession.Attributes.Add("onclick", "javascript:OpenWindow('ExportJsonData.aspx?" & [Global].ACTION & "=" & [Global].DATA_MODULE_REPONSES_SESSION & "'); return false;")
                Literal_ReponseSession.Text = "  [ " & _ReponseList.Count & " ]"
            End If

            If onElementCheck Then
                Section0.Visible = False
                Section_DIVSession.Visible = True
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub

    Private Sub SetAll_DATA_Module_ET_Formulaire_From_RadGrid()
        Dim _ModuleList As New List(Of Cls_Module)
        Dim _QuestionModuleList As New List(Of Cls_Question_Module)
        Dim onElementCheck As Boolean = False
        Try
            REM Test pour voir si la case Question est cocher
            REM Parcourt CheckBox du type de Module dans le RadGrid
            For Each lign As GridDataItem In RadGrid1.Items
                Dim _CheckBoxList_Module_Formulaire As CheckBoxList = CType(lign.FindControl("CheckBoxList_Module_Formulaire"), CheckBoxList)

                Dim _mod As New Cls_Module
                For Each _item As ListItem In _CheckBoxList_Module_Formulaire.Items REM Parcourt CheckBoxList Formulaire
                    If _item.Selected Then
                        _mod = New Cls_Module(TypeSafeConversion.NullSafeString(_item.Value))
                        If _mod.ID > 0 Then 'Verification pour le Module
                            _ModuleList.Add(_mod)
                            REM recherche des Questions par Module
                            Dim _qmList As List(Of Cls_Question_Module) = Cls_Question_Module.SearchAllBy_CodeModule(_mod.CodeModule)
                            If _qmList IsNot Nothing OrElse _qmList.Count > 0 Then 'Verification pour la liste des Reponses
                                For Each _qmod As Cls_Question_Module In _qmList 'Parcourt de la liste des Reponses
                                    _QuestionModuleList.Add(_qmod)
                                Next
                            End If
                        End If
                    End If
                Next
            Next

            REM Gestion des sessions  
            DIV_Module_ModuleSession.Visible = False
            If _ModuleList.Count > 0 Then
                onElementCheck = True
                Me.Session([Global].DATA_MODULE_SESSION) = _ModuleList
                DIV_Module_ModuleSession.Visible = True
                DIV_Module_ModuleSession.Attributes.Add("onclick", "javascript:OpenWindow('ExportJsonData.aspx?" & [Global].ACTION & "=" & [Global].DATA_MODULE_SESSION & "'); return false;")
                Literal_ModuleSession.Text = " [ " & _ModuleList.Count & " ]"
            End If

            DIV_Question_Module_Session.Visible = False
            If _QuestionModuleList.Count > 0 Then
                onElementCheck = True
                Me.Session([Global].DATA_QUESTION_MODULE_SESSION) = _QuestionModuleList
                DIV_Question_Module_Session.Visible = True
                DIV_Question_Module_Session.Attributes.Add("onclick", "javascript:OpenWindow('ExportJsonData.aspx?" & [Global].ACTION & "=" & [Global].DATA_QUESTION_MODULE_SESSION & "'); return false;")
                Literal_Question_ModuleSession.Text = "  [ " & _QuestionModuleList.Count & " ]"
            End If

            If onElementCheck Then
                Section0.Visible = False
                Section_DIVSession.Visible = True
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

#Region "EVENTS CONTROLS"

    Private Sub CBX_Questions_CheckedChanged(sender As Object, e As EventArgs) Handles CBX_Questions.CheckedChanged
        setValueButon()
    End Sub

    Private Sub setValueButon()
        Dim val As String = "Valider "

        val += IIf(CBX_Questions.Checked, " Question ", "")
        val += IIf(CBX_Reponse.Checked AndAlso CBX_Questions.Checked, " Et ", "")
        val += IIf(CBX_Reponse.Checked, " Réponse ", "")

        Literal_ExporterQuestionsEtReponses.Text = val
        Literal_ExporterQuestionsEtReponses2.Text = val
    End Sub

    Private Sub CBX_Reponse_CheckedChanged(sender As Object, e As EventArgs) Handles CBX_Reponse.CheckedChanged
        setValueButon()
    End Sub

    Private Sub LinkButton_ExporterQuestionsEtReponses_Click(sender As Object, e As EventArgs) Handles LinkButton_ExporterQuestionsEtReponses.Click, LinkButton_ExporterQuestionsEtReponses2.Click
        'SetAll_DATA_MODULE_QUESTION_OU_REPONSE()
        SetAll_DATA_Question_Or_Reponse_From_RadGrid()
        SetAll_DATA_Module_ET_Formulaire_From_RadGrid()
    End Sub

#End Region

#Region "ACTIONS / METHODES"

#End Region

#Region "RADGRID EVENTS"
    Protected Sub RadGrid1_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand
        Try
            Dim _id As Long = TypeSafeConversion.NullSafeLong(e.CommandArgument)
            Select Case e.CommandName
                Case "delete"
                    'Dim obj As New Cls_Contrainte(_id)
                    'obj.Delete()
                    'User_Connected.Activite_Utilisateur_InRezo("DELETE " & PAGE_TITLE, obj.LogData(obj), Request.UserHostAddress)
                    ''User_Connected.Activite_Utilisateur_InRezo("DELETE Contrainte ", obj.ID & " - Code:" & obj.Titrerapport & " Prop:", Request.UserHostAddress)
                    'MessageToShow([Global].Msg_Information_Supprimee_Avec_Succes, "S")
                    'RadGrid1.Rebind()
            End Select
        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub

    Protected Sub RadGrid1_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid1.ItemDataBound
        Try
            Dim gridDataItem = TryCast(e.Item, GridDataItem)
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                'Dim _lnk As HyperLink = DirectCast(gridDataItem.FindControl("hlk"), HyperLink)
                'Dim _lbl_ID As Label = DirectCast(gridDataItem.FindControl("lbl_ID"), Label)
                '_lnk.Attributes.Clear()
                '_lnk.Attributes.Add("onclick", "javascript:void(ShowAddUpdateForm('Frm_ContrainteADD.aspx?ID=" & CLng(_lbl_ID.Text) & "', 750, 400));")
            End If

            If (gridDataItem IsNot Nothing) Then
                Dim item As GridDataItem = gridDataItem
                'CType(item.FindControl("lbOrder"), Label).Text = RadGrid1.PageSize * RadGrid1.CurrentPageIndex + (item.RowIndex / 2)
                Dim ID_TypeModule As Long = CType(DataBinder.Eval(e.Item.DataItem, "ID"), Long)
                Dim CheckBoxList_Module_Formulaire As CheckBoxList = CType(item.FindControl("CheckBoxList_Module_Formulaire"), CheckBoxList)
                FillCombo_Module(CheckBoxList_Module_Formulaire, ID_TypeModule)
                '<%#Bind("EstActif_Image") %>'
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub

    Protected Sub RadGrid1_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        If IsPostBack Then
            BindGrid(False)
        End If
    End Sub

    Protected Sub RadAjaxManager1_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles RadAjaxManager1.AjaxRequest
        Try
            Select Case e.Argument
                Case "Reload"
                    BindGrid(True)
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



End Class
