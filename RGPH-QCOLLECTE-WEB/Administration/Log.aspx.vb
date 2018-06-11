Imports System.IO
Imports RGPH_QCOLLECTE_Library
Imports BRAIN_DEVLOPMENT
Imports Telerik.Web.UI

Partial Class Administration_Log
    Inherits Cls_BasePage

    Dim _message As String = ""
    Private Const Nom_page As String = "Wfrm_User.aspx"
    Dim User_Connected As Cls_User
    Dim Is_Acces_Page As Boolean = True
    Dim GetOut As Boolean = False
    Dim IS_Search As Boolean = False
    Dim IS_Trouve As Boolean = False
    Private IDGroupe As Long = 0
    Private IS_SendMail As Boolean = True

    Protected Sub Page_PreLoad(sender As Object, e As EventArgs) Handles Me.PreLoad
        Try
            'CType(Me.Master.FindControl("MenuParametre").FindControl("HyperLink_Wfrm_User"), HyperLink).CssClass = "active"
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Expires = -1
            Panel_Msg.Visible = False

            SYSTEME_SECURITE()

            '--- Si l'utilisateur a Acces a la page 
            If Is_Acces_Page Then
                If Not IsPostBack Then
                    BindGrid()
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

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Try
            'If Request.QueryString("SECURITY") IsNot Nothing Then
            '    Page.MasterPageFile = "~/MasterPage/MasterPage_Security.master"
            '    CType(Page.Master.FindControl("DashMenu_Securite").FindControl("liPANEL_GESTION_SECURITE"), HtmlControl).Attributes.Add("class", "active treeview")
            '    CType(Page.Master.FindControl("DashMenu_Securite").FindControl("liLogErreur"), HtmlControl).Attributes.Add("class", "active")

            'Else
            '    Page.MasterPageFile = "~/MasterPage/MasterPage_Account.master"
            '    CType(Page.Master.FindControl("liLogErreur"), HtmlControl).Attributes.Add("class", "activeSousMenu")
            'End If
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub
    Public Sub SYSTEME_SECURITE()
        Try
            User_Connected = [Global].KeepUserContinuesToWork(User_Connected)
            
            CType(Page.Master.FindControl("DashMenu_Securite").FindControl("liPANEL_GESTION_SECURITE"), HtmlControl).Attributes.Add("class", "active treeview")
            CType(Page.Master.FindControl("DashMenu_Securite").FindControl("liLogErreur"), HtmlControl).Attributes.Add("class", "active")

            If Session([Global].GLOBAL_SESSION) IsNot Nothing Then
                User_Connected = CType(Session([Global].GLOBAL_SESSION), Cls_User)
                If Not Cls_Privilege.VerifyRightOnObject(Nom_page, User_Connected.IdGroupeuser) Then
                    _message = [Global].NO_ACCES_PAGE
                    MessageToShow(_message)
                    'Is_Acces_Page = False
                    Panel_First.Visible = False
                Else

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
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub
#End Region

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

    Public Sub ImageInDir()
        Try
            Dim _Path As String = "~/log"
            Dim filesindirectory As String() = Directory.GetFiles(Server.MapPath(_Path))
            Dim images As List(Of String) = New List(Of String)(filesindirectory.Count())

            'Dim logs As List(Of Rezo509Logs)

            For Each item As String In filesindirectory
                'logs.Add(New Rezo509Logs("", "" & System.IO.Path.GetFileName(item)))
                'images.Insert(String.Format("{0}", System.IO.Path.GetFileName(item)), String.Format(_Path & "/{0}", System.IO.Path.GetFileName(item)))
                'images.Add(String.Format(_Path & "/{0}", System.IO.Path.GetFileName(item)))
                images.Add(String.Format("{0}", System.IO.Path.GetFileName(item)))
            Next
            'RadGrid_1.DataSource = logs
            'If _refresh Then
            'RadGrid_1.DataBind()
            'End If

            If images.Count <= 0 Then
                LabelSelectIcon.Text = "Répertoire image vide..."
            End If
            'RadListBoxImage.DataValueField = ""
            RadListBoxLogs.DataSource = images
            RadListBoxLogs.DataBind()

            DirectoryListe()
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub

    Public Sub DirectoryListe()
        Try
            Dim _Path As String = Server.MapPath("~/log")
            Dim directoryInfo As New DirectoryInfo(_Path)
            Dim logs As New List(Of Cls_Rezo509Logs)

            Dim images As List(Of String) = New List(Of String)(directoryInfo.GetFiles().Count())
            For Each _file In directoryInfo.GetFiles()
                Dim nameFile As String = _file.Name
                Dim FullName As String = _file.FullName
                Dim size As Long = _file.Length
                Dim CreationTime As DateTime = _file.CreationTime
                Dim item As String = String.Format("{0,-12:N0} {1,-20:g} {2} / {3}", size, CreationTime, nameFile, FullName)

                Dim obj As New Cls_Rezo509Logs
                obj.NameFile = nameFile
                obj.FullNameFile = FullName
                obj.SizeFile = size
                obj.CreationTimeFile = CreationTime
                logs.Add(obj)

                'images.Add(String.Format("{0}", item))

            Next _file


            'Dim images As List(Of String) = New List(Of String)(filesindirectory.Count())
            'Dim logs As List(Of Rezo509Logs)
            'For Each item As String In filesindirectory
            '    'logs.Add(New Rezo509Logs("", "" & System.IO.Path.GetFileName(item)))
            '    'images.Insert(String.Format("{0}", System.IO.Path.GetFileName(item)), String.Format(_Path & "/{0}", System.IO.Path.GetFileName(item)))
            '    'images.Add(String.Format(_Path & "/{0}", System.IO.Path.GetFileName(item)))
            '    images.Add(String.Format("{0}", System.IO.Path.GetFileName(item)))
            'Next
            RadGrid_1.DataSource = logs
            'If _refresh Then
            RadGrid_1.DataBind()
            'End If

            'If images.Count <= 0 Then
            '    LabelSelectIcon.Text = "Répertoire image vide..."
            'End If
            ''RadListBoxImage.DataValueField = ""
            'RadListBoxLogs.DataSource = images
            'RadListBoxLogs.DataBind()
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub

#Region "Load DATA"
    Private Sub BindGrid(Optional ByVal _refresh As Boolean = True)
        Dim _ret As Long = 0
        Try
            Dim _Path As String = Server.MapPath("~/log")
            Dim directoryInfo As New DirectoryInfo(_Path)
            Dim logs As New List(Of Cls_Rezo509Logs)

            For Each _file In directoryInfo.GetFiles()
                Dim nameFile As String = _file.Name
                Dim FullName As String = _file.FullName
                Dim size As Long = _file.Length
                Dim CreationTime As DateTime = _file.CreationTime
                Dim item As String = String.Format("{0,-12:N0} {1,-20:g} {2} / {3}", size, CreationTime, nameFile, FullName)

                Dim obj As New Cls_Rezo509Logs
                obj.NameFile = nameFile
                obj.FullNameFile = FullName
                obj.SizeFile = size
                obj.CreationTimeFile = CreationTime
                logs.Add(obj)
            Next _file

            RadGrid_1.DataSource = logs
            If _refresh Then
                RadGrid_1.DataBind()
            End If
            _ret = logs.Count
            Label_Titre.Text = String.Format([Global].Label_GrandTitre_Right, "LOG Erreur", "bg-yellow", _ret)
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub
#End Region

#Region "RADGRID EVENTS"
    Protected Sub RadGrid_1_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid_1.ItemCommand
        Try
            If e.CommandName = Telerik.Web.UI.RadGrid.ExportToExcelCommandName Then
                RadGrid_1.ExportSettings.ExportOnlyData = True
                RadGrid_1.GridLines = GridLines.Both
                RadGrid_1.ExportSettings.IgnorePaging = True
                RadGrid_1.ExportSettings.OpenInNewWindow = False
                RadGrid_1.ExportSettings.FileName = "Liste des Parametres"
                RadGrid_1.MasterTableView.Columns(0).Visible = False
                RadGrid_1.MasterTableView.ExportToExcel()
            End If

            Select Case e.CommandName
                Case "editer"
                    If System.IO.File.Exists(e.CommandArgument) Then
                        Dim directoryInfo As New DirectoryInfo(e.CommandArgument)
                        txt_ContenuLog.Visible = True
                        Label_SousTitre.Text = directoryInfo.Name
                        txt_ContenuLog.Text = IO.File.ReadAllText(directoryInfo.FullName)
                    Else
                        Throw New Rezo509Exception("Le répertoire [ " & e.CommandArgument & " ] n'existe pas")
                    End If
                Case "delete"
                    If System.IO.File.Exists(e.CommandArgument) Then
                        txt_ContenuLog.Visible = False
                        Dim directoryInfo As New DirectoryInfo(e.CommandArgument)
                        System.IO.File.Delete(directoryInfo.FullName)
                        MessageToShow("Delete: " & directoryInfo.Name, "S")
                        txt_ContenuLog.Visible = False
                        BindGrid()
                        'RadGrid_1.Rebind()
                    Else
                        Throw New Rezo509Exception("Le répertoire [ " & e.CommandArgument & " ] n'existe pas")
                    End If
            End Select
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub

    Protected Sub RadGrid_1_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid_1.ItemDataBound
        Try
            Dim gridDataItem = TryCast(e.Item, GridDataItem)
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                'Dim _lnk As HyperLink = DirectCast(gridDataItem.FindControl("hlk"), HyperLink)
                'Dim _lbl_ID As Label = DirectCast(gridDataItem.FindControl("lbl_ID"), Label)
                '_lnk.Attributes.Clear()
                '_lnk.Attributes.Add("onclick", "javascript:void(ShowAddUpdateForm('Frm_DomaineActiviteADD.aspx?ID=" & CLng(_lbl_ID.Text) & "', 750, 400));")
            End If

            If (gridDataItem IsNot Nothing) Then
                Dim item As GridDataItem = gridDataItem
                CType(item.FindControl("lbOrder"), Label).Text = RadGrid_1.PageSize * RadGrid_1.CurrentPageIndex + (item.RowIndex / 2)

                Dim imagedelete As ImageButton = CType(item("delete").Controls(0), ImageButton)
                Dim imageediter As ImageButton = CType(item("editer").Controls(0), ImageButton)
                imagedelete.ToolTip = "Effacer"
                imageediter.ToolTip = "Editer"
                imagedelete.CommandArgument = CType(DataBinder.Eval(e.Item.DataItem, "FullNameFile"), String)
                imageediter.CommandArgument = CType(DataBinder.Eval(e.Item.DataItem, "FullNameFile"), String)
            End If
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub

    Protected Sub RadGrid_1_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid_1.NeedDataSource
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
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub
#End Region

    Protected Sub RadListBoxLogs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadListBoxLogs.SelectedIndexChanged
        Try
            Dim _Path As String = Server.MapPath("~/log")
            'Dim filesindirectory As String() = Directory.GetFiles(Server.MapPath(_Path))
            If RadListBoxLogs.SelectedIndex > -1 Then
                'MessageToShow("" & RadListBoxLogs.SelectedItem.Text)
                'Label_ContenuLog.Text = String.Format("{0}", _Path & "\" & RadListBoxLogs.SelectedItem.Text)
                txt_ContenuLog.Text = IO.File.ReadAllText(String.Format("{0}", _Path & "\" & RadListBoxLogs.SelectedItem.Text))

                'Using sr As New StreamReader(_Path & "\" & RadListBoxLogs.SelectedItem.Text, True)
                '    Label_ContenuLog2.Text = sr.ReadToEnd
                'End Using


            End If
        Catch ex As Rezo509Exception
            MessageToShow(ex.Message)
        Catch ex As Exception
            MessageToShow(ex.Message)
            [Global].WriteError(ex, User_Connected)
        End Try
    End Sub
End Class
