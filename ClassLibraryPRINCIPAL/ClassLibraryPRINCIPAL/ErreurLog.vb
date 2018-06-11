' @Brain Develpment
' 11-08-2011
Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Globalization
Imports System.Web
Imports BRAIN_DEVLOPMENT
Imports System

Public Class ErreurLog
    Public Shared Sub WriteError(ByVal errorMessage As String)
        Try
            Dim path As String = "~/log/" & DateTime.Today.ToString("dd-MMM-yyyy") & ".txt"
            If (Not File.Exists(System.Web.HttpContext.Current.Server.MapPath(path))) Then
                File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close()
            End If
            Using w As IO.StreamWriter = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path))
                w.WriteLine(Constants.vbCrLf & "Chemin Erreur : ")
                w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture))
                Dim err As String = "Erreur dans: " & System.Web.HttpContext.Current.Request.Url.ToString() & ". Message Erreur:" & errorMessage
                w.WriteLine(err)
                w.WriteLine("__________________________")
                w.Flush()
                w.Close()
            End Using

        Catch ex As Exception
            WriteError(ex.Message)
        End Try

    End Sub

End Class