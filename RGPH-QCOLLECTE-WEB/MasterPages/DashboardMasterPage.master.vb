Imports RGPH_QCOLLECTE_Library

Partial Class MasterPage_DashboardMasterPage
    Inherits System.Web.UI.MasterPage

#Region "ATRIBUTS"
    Dim _message As String = ""
    Private Const PANEL_GESTION_SECURITE As String = "PANEL-GESTION-SECURITE"
    Private Const PAGE_GESTION_UTILISATEUR As String = "PAGE-GESTION-UTILISATEUR"
    Private Const PAGE_GESTION_GROUPE_UTILISATEUR As String = "PAGE-GESTION-GROUPE-UTILISATEUR"
    Private Const PAGE_ACTIVITES_UTILISATEUR As String = "PAGE-ACTIVITES-UTILISATEUR"
    Private Const PAGE_PRIVILEGE_GROUPE As String = "PAGE-PRIVILEGE-GROUPE"
    Private Const PAGE_MODULES As String = "PAGE-MODULES"
    Private Const PAGE_GESTION_OBJET As String = "PAGE-GESTION-OBJET"
    Private Const PAGE_PRIVILEGES_DES_TACHES_UTILISATEURS As String = "PAGE-PRIVILEGES-DES-TACHES-UTILISATEURS"
    Private Const PAGE_GESTION_TACHES_UTILISATEURS As String = "PAGE-GESTION-TACHES-UTILISATEURS"
    Private Const PAGE_UTILISATEURS_CONNECTES As String = "PAGE-UTILISATEURS-CONNECTES"


    Dim User_Connected As Cls_User
    Dim Is_Acces_Page As Boolean = True
    Dim GetOut As Boolean = False
    Private IS_SendMail As Boolean = True
#End Region

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Expires = -1
        SYSTEME_SECURITE()
    End Sub


#Region "SECURITE"
    Public Sub SYSTEME_SECURITE()
        If Session([Global].GLOBAL_SESSION) IsNot Nothing Then
            User_Connected = CType(Session([Global].GLOBAL_SESSION), Cls_User)

            ImageParticipant.ImageUrl = "~/Show_Image.aspx?Action=User&ID=" & User_Connected.ID
            LabelPrenomParticipant.Text = " " & User_Connected.Prenom & " " & User_Connected.Nom

            '_check = Cls_Privilege.VerifyRightOnObject(PANEL_GESTION_SECURITE, User_Connected.IdGroupeuser)
            'liPANEL_GESTION_SECURITE.Visible = _check
            'If _check Then
            '    'liPANEL_GESTION_SECURITE.Visible = Cls_Privilege.VerifyRightOnObject(PANEL_GESTION_SECURITE, User_Connected.IdGroupeuser)
            '    liPAGE_GESTION_UTILISATEUR.Visible = Cls_Privilege.VerifyRightOnObject(PAGE_GESTION_UTILISATEUR, User_Connected.IdGroupeuser)
            '    liPAGE_GESTION_GROUPE_UTILISATEUR.Visible = Cls_Privilege.VerifyRightOnObject(PAGE_GESTION_GROUPE_UTILISATEUR, User_Connected.IdGroupeuser)
            '    liPAGE_ACTIVITES_UTILISATEUR.Visible = Cls_Privilege.VerifyRightOnObject(PAGE_ACTIVITES_UTILISATEUR, User_Connected.IdGroupeuser)
            '    liPAGE_PRIVILEGE_GROUPE.Visible = Cls_Privilege.VerifyRightOnObject(PAGE_PRIVILEGE_GROUPE, User_Connected.IdGroupeuser)
            '    liPAGE_MODULES.Visible = Cls_Privilege.VerifyRightOnObject(PAGE_MODULES, User_Connected.IdGroupeuser)
            '    liPAGE_GESTION_OBJET.Visible = Cls_Privilege.VerifyRightOnObject(PAGE_GESTION_OBJET, User_Connected.IdGroupeuser)
            '    liPAGE_PRIVILEGES_DES_TACHES_UTILISATEURS.Visible = Cls_Privilege.VerifyRightOnObject(PAGE_PRIVILEGES_DES_TACHES_UTILISATEURS, User_Connected.IdGroupeuser)
            '    liPAGE_GESTION_TACHES_UTILISATEURS.Visible = Cls_Privilege.VerifyRightOnObject(PAGE_GESTION_TACHES_UTILISATEURS, User_Connected.IdGroupeuser)
            '    liPAGE_UTILISATEURS_CONNECTES.Visible = Cls_Privilege.VerifyRightOnObject(PAGE_UTILISATEURS_CONNECTES, User_Connected.IdGroupeuser)
            'End If
        End If
    End Sub
#End Region

End Class

