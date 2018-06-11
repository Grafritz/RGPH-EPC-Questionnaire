' @Brain Development
' Mardi 08-09-2015

Imports System.Data
Imports System.Collections.Generic
Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer
Imports Telerik.Web.UI
Imports System.Data.SqlClient
Imports RGPH_QCOLLECTE_Library


Partial Class MasterPage_DashboardCZMasterPage
    Inherits System.Web.UI.MasterPage

#Region "ATRIBUTS"
    'Dim _message As String = ""
    'Private Const Nom_page As String = "DashBoardReunion.aspx"
    'Private Const Btn_Save As String = "Btn_SaveREUNION"
    'Private Const Btn_GestionParticipant As String = "Btn_GestionParticipant"
    'Private Const PageName_GestionParticipant As String = "PAGE_GestionParticipant"
    'Private Const Btn_UpdateReunion As String = "Btn_EditREUNION"

    'Private Const MyInstitutions As String = "MyInstitutions.aspx"

    Dim User_Connected As Cls_User
    Dim Is_Acces_Page As Boolean = True
    Dim GetOut As Boolean = False
    Private IS_SendMail As Boolean = True


    Public PrenomParticipant As String = ""
    Public NomCompletParticipant As String = ""
    Public NomCompletEtPostParticipant As String = ""
    Public PhotoParticipant As String = "../AdminLTE-master/img/avatar3.png"
    Public EmailParticipant As String = ""

    'Public LabelUserOnline As String = ""
    'Public LabelNbrUserOnlineText As String = ""
    'Public LabelNbrUserOnline As String = "0"
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Expires = -1

        '' CType(Me.Master.FindControl("MenuVerticalPresentation").FindControl("HyperLink_MyInstitution"), HyperLink).CssClass = "active"
        SYSTEME_SECURITE()
        'If Is_Acces_Page Then
        '    If Not IsPostBack Then
        '        CheckPrivilegeUser()
        '    End If
        'End If
        'If Now.Month = 12 Then
        '    'Style_Division(bodyMasterPage, "background-image:url('../images/Bg_christmas.jpg')", "style")
        'End If
    End Sub

#Region "SECURITE"
    Public Sub SYSTEME_SECURITE()
        Try
            User_Connected = [Global].KeepUserContinuesToWork(User_Connected)
            If Session([Global].GLOBAL_SESSION) IsNot Nothing Then
                User_Connected = CType(Session([Global].GLOBAL_SESSION), Cls_User)
                'GetOnlineUsers()
                ''HyperLinkChat1.Attributes.Add("onclick", "javascript:void(Open_Window('../MessengerCopie.aspx','Messenger2',290,500));")
            Else
                htmlMasterPage.Attributes.Add("class", "lockscreen")
                bodyMasterPage.Attributes.Add("class", "texture")
                Head_Nav_Menu.Visible = False
                Head_Nav_MenuSous.Visible = False
                Head_Nav_MenuVertical.Visible = False
            End If

            If Session([Global].GLOBAL_SESSION) IsNot Nothing Then
                PhotoParticipant = "../Show_Image.aspx?Action=User&ID=" & User_Connected.ID
                ImageParticipant.ImageUrl = PhotoParticipant
                ImageParticipant2.ImageUrl = PhotoParticipant
                'LabelPrenomParticipant.Text = "Hello, " & User_Connected.ReunionParticipant.Prenom

                NomCompletParticipant = User_Connected.NOMCOMPLET
                NomCompletEtPostParticipant = User_Connected.NOMCOMPLET '& " - " & User_Connected.Poste
                EmailParticipant = User_Connected.GROUPE_STR & "<br />" & User_Connected.Username

                'ImageParticipant.ImageUrl = "~/Show_Image.aspx?Action=User&ID=" & User_Connected.ID
                LabelPrenomParticipant.Text = "Hi, " & User_Connected.Prenom
                LabelPrenomParticipant2.Text = "" & User_Connected.NOMCOMPLET


                'Dim check As Boolean = False
                'check = Cls_Privilege.VerifyRightOnObject(MyInstitutions, User_Connected.ID_Group)
                'LI_HyperLink_MyInstitutions1.Visible = False
                'li_HyperLink_MyInstitutions.Visible = check
                'If check Then
                '    HyperLink_MyInstitutions1.Text = "<i class='fa fa-building-o'></i><span> INSTITUTIONS</span>" 'String.Format([Global].Label_Icon_Text_Badge_Counter, "fa fa-building-o", "INSTITUTIONS", "label label-primary pull-right", "") ' Cls_Institution.Get_Count_ByIDUser(User_Connected.ID))
                '    HyperLink_MyInstitutions.Text = "<i class='fa fa-building-o'></i><span> INSTITUTIONS</span>" 'String.Format([Global].Label_Icon_Text_Badge_Counter, "fa fa-building-o", "INSTITUTIONS", "label label-primary pull-right", "") ' Cls_Institution.Get_Count_ByIDUser(User_Connected.ID))
                'End If
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Rezo509Exception
            'MessageToShow(ex.Message)
        Catch ex As Exception
            'MessageToShow(ex.Message)
            '[Global].WriteError(ex, User_Connected)
        End Try
    End Sub
#End Region

#Region "Other Method - SAVE"
    Private Sub MessageToShow(ByVal _err As String, Optional ByVal E_or_S As String = "E")
        Panel_Msg.Visible = True
        'GlobalFunctions.Message_Image(Image_Msg, E_or_S)
        Label_Msg.Text = _err
        ''Dialogue.alert(_err)
        ''RadAjaxManager1.ResponseScripts.Add("alert('" & _err.Replace("'", "\'") & "');")
        If E_or_S = "S" Then
            Label_Msg.ForeColor = Drawing.Color.Green
        Else
            Label_Msg.ForeColor = Drawing.Color.Red
        End If
    End Sub
#End Region

    'Public Sub GetOnlineUsers()
    '    Dim ConnectionStringDataBase As String = System.Configuration.ConfigurationManager.ConnectionStrings("dbConnectionString").ToString
    '    Dim conn As New SqlConnection(ConnectionStringDataBase)
    '    Dim cmd As New SqlCommand("select * from sampleUsers where LastLoginTime>=@LastLoginTime order by LastLoginTime desc", conn)

    '    Dim paraLastLoginTime As SqlParameter = cmd.Parameters.Add("@LastLoginTime", SqlDbType.DateTime)
    '    paraLastLoginTime.Value = SamplePortal.[Global].OnlineTimeoutTime

    '    Dim ada As New SqlDataAdapter(cmd)
    '    Dim result As New DataSet()
    '    Try
    '        conn.Open()
    '        ada.Fill(result)
    '    Finally
    '        conn.Close()
    '    End Try
    '    Dim ListeUser As String = ""
    '    Dim listUser As Cls_User
    '    For Each row As DataRow In result.Tables(0).Rows
    '        listUser = New Cls_User(TypeSafeConversion.NullSafeString(row("Username").ToString()))

    '        If listUser.ID > 0 Then
    '            ListeUser += "<li>"
    '            ListeUser += "  <a href='#'>"
    '            ListeUser += "      <div class='pull-left'>"
    '            ListeUser += "          <img src='../Show_Image.aspx?ID=" + TypeSafeConversion.NullSafeString(listUser.ID).Trim + "&Action=User' width='55px' class='img-circle'  alt='' />"
    '            ListeUser += "      </div>"
    '            ListeUser += "      <h4>" + listUser.NOMCOMPLET + "</h4>"
    '            ListeUser += "      <p>" + listUser.Username + "</p>"
    '            ListeUser += "  </a>"
    '            ListeUser += "</li>"
    '        Else
    '            ListeUser += "<li>"
    '            ListeUser += "  <a href='#'>"
    '            ListeUser += "      <div class='pull-left'>"
    '            ListeUser += "          <img src='../Show_Image.aspx?UserName=" + row("Username").ToString() + "&Action=UserName' width='55px' class='img-circle'  alt='' />"
    '            ListeUser += "      </div>"
    '            ListeUser += "      <h4>" + row("Username").ToString() + "</h4>"
    '            'ListeUser += "      <p>Why not buy a new awesome theme?</p>"
    '            ListeUser += "  </a>"
    '            ListeUser += "</li>"
    '        End If


    '    Next
    '    If result.Tables(0).Rows.Count > 1 Then
    '        LabelNbrUserOnlineText = "[ " & result.Tables(0).Rows.Count & " ] Personnes sont en ligne"
    '        LabelNbrUserOnline = result.Tables(0).Rows.Count
    '    ElseIf result.Tables(0).Rows.Count = 1 Then
    '        LabelNbrUserOnlineText = "[ " & result.Tables(0).Rows.Count & " ] Personne est en ligne "
    '        LabelNbrUserOnline = result.Tables(0).Rows.Count
    '        'Else
    '        '    LabelNbrUserOnlineText.Text = "[ " & result.Tables(0).Rows.Count & " ] Personnes sont en ligne y compris vous"
    '        '    LabelNbrUserOnline.Text = result.Tables(0).Rows.Count
    '    End If
    '    LabelUserOnline = ListeUser
    '    'LabelUserOnline2.Text = ListeUser
    'End Sub

    Private Sub CheckPrivilegeUser()
        
    End Sub


End Class

