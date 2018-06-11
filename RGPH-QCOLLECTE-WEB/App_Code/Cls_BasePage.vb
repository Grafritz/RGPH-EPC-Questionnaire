Imports System
Imports System.Data
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Threading
Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System.Configuration

Public Class Cls_BasePage
    Inherits System.Web.UI.Page '

    Public Sub New()

    End Sub

    Public ReadOnly Property CurrentCulture() As String
        Get
            Return getCurrentCulture(Server, Session)
        End Get
    End Property

    Public ReadOnly Property CurrentRessource() As String
        Get
            If Session("SelectResource") IsNot Nothing Then
                If System.IO.File.Exists(Session("SelectResource").ToString()) Then
                    Return Session("SelectResource").ToString()
                End If
            End If
            Return Server.MapPath("App_GlobalResources\\Resource.fr-FR.resx")
        End Get
    End Property
    
    Protected Overrides Sub InitializeCulture()
        If Not String.IsNullOrEmpty(CurrentCulture) Then
            Try
                Thread.CurrentThread.CurrentCulture = New CultureInfo("fr-FR") 'New CultureInfo(CurrentCulture)
                Thread.CurrentThread.CurrentUICulture = New CultureInfo(CurrentCulture) 'Thread.CurrentThread.CurrentCulture
            Catch ex As Exception
                Throw
            End Try
        End If
    End Sub

    Public Shared Function getCurrentCulture(ByVal pserver As System.Web.HttpServerUtility, ByVal psession As System.Web.SessionState.HttpSessionState) As String
        If psession("Langue_Preferer") IsNot Nothing Then
            Return psession("Langue_Preferer").ToString()
        End If
        psession("Langue_Preferer") = "fr-FR"
        psession("SelectResource") = pserver.MapPath("App_GlobalResources\\Resource.fr-FR.resx")
        Return psession("Langue_Preferer").ToString()
    End Function


End Class

Public Class Enumeration
    Public Enum _Groupe
        Administration = 1
        SecretaireReunion = 2
        Participant = 3
        President_de_reunion = 4
    End Enum

    Public Function getGroupe_Str(ByVal _idGroupe As Long) As String
        Dim Val As String = ""
        Select Case _idGroupe
            Case _Groupe.Administration
                Val = "Administration"
            Case _Groupe.SecretaireReunion
                Val = "Secrétaire de réunion"
            Case _Groupe.Participant
                Val = "Participant"
            Case _Groupe.President_de_reunion
                Val = "Président de réunion"
        End Select
        Return Val
    End Function

End Class
