<%@ Page Language="VB" %>

<%@ Import Namespace="RGPH_QCOLLECTE_Library" %>

<%@ Import Namespace="BRAIN_DEVLOPMENT" %>


<!DOCTYPE html>

<script runat="server">
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("ID") IsNot Nothing Then
            If Request.QueryString("Action") IsNot Nothing Then
                Dim Action As String = Request.QueryString("Action")
                Select Case Action
                    Case "User"
                        Show_Image_Utilisateur()
                End Select
            End If
        ElseIf Request.QueryString("UserName") IsNot Nothing Then
            If Request.QueryString("Action") IsNot Nothing Then
                Dim Action As String = Request.QueryString("Action")
                Select Case Action
                    Case "UserName"
                        Show_Image_UtilisateurByEmail()
                    Case "ForChat"
                        Show_Image_UtilisateurByEmail2()
                End Select
            End If
        ElseIf Request.QueryString("Email") IsNot Nothing Then
            If Request.QueryString("Action") IsNot Nothing Then
                Dim Action As String = Request.QueryString("Action")
                Select Case Action
                    Case "User"
                        Show_Image_Utilisateur()
                End Select
            End If
        Else
            Show_Image_Avatar()
        End If
    End Sub
         
    Private Sub Show_Image_UtilisateurByEmail()
        Dim _Username As String = TypeSafeConversion.NullSafeString(Request.QueryString("UserName"))
        Dim obj As New Cls_User(_Username)
        If obj.Photo IsNot Nothing Then
            Dim bytes() As Byte = obj.Photo
            Response.Buffer = True
            Response.Charset = ""
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "image/jpg"
            Response.AddHeader("content-disposition", "attachment;filename=" & obj.ID & ".jpg")
            If bytes IsNot Nothing Then
                Response.BinaryWrite(bytes)
            End If
            Response.Flush()
            Response.End()
        Else
            Response.Redirect("~/images/no-avatar.jpg")
        End If
    End Sub
         
    Private Sub Show_Image_UtilisateurByEmail2()
        Dim _Username As String = TypeSafeConversion.NullSafeString(Request.QueryString("UserName")).Trim.Replace("User:", "").Trim
        Dim obj As New Cls_User(_Username)
        'Response.Write (_Username & "<hr />")
        If obj.Photo IsNot Nothing Then
            Dim bytes() As Byte = obj.Photo
            Response.Buffer = True
            Response.Charset = ""
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "image/jpg"
            Response.AddHeader("content-disposition", "attachment;filename=" & obj.ID & ".jpg")
            If bytes IsNot Nothing Then
                Response.BinaryWrite(bytes)
            End If
            Response.Flush()
            Response.End()
            'Response.Write(obj.NOM_COMPLET_AND_USERANAME )
        Else
            'Response.Write("<hr />NADA")
            Response.Redirect("~/images/no-avatar.jpg")
        End If
    End Sub
    
    Private Sub Show_Image_Utilisateur()
        Dim _ID As Long = TypeSafeConversion.NullSafeLong(Request.QueryString("ID"))
        Dim obj As New Cls_User(_ID)
        
        If obj.Photo IsNot Nothing Then
            Dim bytes() As Byte = obj.Photo

            Response.Buffer = True
            Response.Charset = ""
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "image/jpg"
            Response.AddHeader("content-disposition", "attachment;filename=" & Now.ToString("YYYYmmddtish") & ".jpg")
            If bytes IsNot Nothing Then
                Response.BinaryWrite(bytes)
            End If
            Response.Flush()
            Response.End()
        Else
            Response.Redirect("~/images/no-avatar.jpg")
        End If
    End Sub
    
    Private Sub Show_Image_Avatar()
       Response.Redirect("~/images/no-avatar.jpg")
    End Sub
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       
    </div>
    </form>
</body>
</html>
