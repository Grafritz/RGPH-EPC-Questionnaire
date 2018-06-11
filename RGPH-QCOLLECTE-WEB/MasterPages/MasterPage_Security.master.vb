' @Brain Development
' Mercredi 18-06-2014

Imports System.Data
Imports System.Collections.Generic
Imports RGPH_QCOLLECTE_Library
Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer
Imports Telerik.Web.UI

Partial Class MasterPage_MasterPage_Security
    Inherits System.Web.UI.MasterPage

#Region "ATRIBUTS"
    Dim _message As String = ""
    Private Const Nom_page As String = "DashBoardReunion.aspx"
    Private Const Btn_Save As String = "Btn_SaveREUNION"
    Private Const Btn_GestionParticipant As String = "Btn_GestionParticipant"
    Private Const PageName_GestionParticipant As String = "PAGE_GestionParticipant"
    Private Const Btn_UpdateReunion As String = "Btn_EditREUNION"

    Dim User_Connected As Cls_User
    Dim Is_Acces_Page As Boolean = True
    Dim GetOut As Boolean = False
    Private IS_SendMail As Boolean = True


    Public PrenomParticipant As String = ""
    Public NomCompletParticipant As String = ""
    Public NomCompletEtPostParticipant As String = ""
    Public PhotoParticipant As String = "../images/LogoRezo509.png"
    Public EmailParticipant As String = ""
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Expires = -1

        If Now.Month = 12 Then
            'Style_Division(bodyMasterPage, "background-image:url('../images/Bg_christmas.jpg')", "style")
        End If
        ' CType(Me.Master.FindControl("MenuVerticalPresentation").FindControl("HyperLink_MyInstitution"), HyperLink).CssClass = "active"
        SYSTEME_SECURITE()
        If Is_Acces_Page Then
            If Not IsPostBack Then
                'BindGrid()
                'BindGrid_RadScheduler1()
            End If
        End If
    End Sub

#Region "SECURITE"
    Public Sub SYSTEME_SECURITE()
        If Session([Global].GLOBAL_SESSION) IsNot Nothing Then
            User_Connected = CType(Session([Global].GLOBAL_SESSION), Cls_User)

            PhotoParticipant = "../Show_Image.aspx?Action=User&ID=" & User_Connected.ID
            'ImageParticipant.ImageUrl = "~/Show_Image.aspx?Action=User&ID=" & User_Connected.IdReunionparticipant
            'LabelPrenomParticipant.Text = "Hello, " & User_Connected.ReunionParticipant.Prenom

            NomCompletParticipant = User_Connected.Nomcomplet
            NomCompletEtPostParticipant = User_Connected.NOMCOMPLET '& " - " & User_Connected.Poste
            EmailParticipant = User_Connected.GROUPE_STR & "<br />" & User_Connected.Username
        Else
            DashMenu_Securite.Visible = False
        End If
    End Sub
#End Region

#Region "Other Method - SAVE"
    Private Sub MessageToShow(ByVal _err As String, Optional ByVal E_or_S As String = "E")
        Panel_Msg.Visible = True
        GlobalFunctions.Message_Image(Image_Msg, E_or_S)
        Label_Msg.Text = _err
        Dialogue.alert(_err)
        'RadAjaxManager1.ResponseScripts.Add("alert('" & _err.Replace("'", "\'") & "');")
        If E_or_S = "S" Then
            Label_Msg.ForeColor = Drawing.Color.Green
        Else
            Label_Msg.ForeColor = Drawing.Color.Red
        End If
    End Sub
#End Region

End Class

