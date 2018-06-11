<%@ Page Language="VB" %>

<%@ Import Namespace="RGPH_QCOLLECTE_Library" %>

<!DOCTYPE html>
<script runat="server">
    Dim Cookies As HttpCookie
    Dim _User As Cls_User

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Request.QueryString("Action") IsNot Nothing) Then
            Select Case Request.QueryString("Action").ToString.ToUpper
                Case "UTILISATEUR"
                    Logout_UTILISATEUR()
            End Select
        End If
        
        If (Request.QueryString("Login") IsNot Nothing) Then
            Response.Redirect("~/Login.aspx")
        Else
            Cookies = Request.Cookies([Global].GLOBAL_COOKIES)
            Response.Cookies([Global].GLOBAL_COOKIES).Expires = DateTime.Now.AddDays(-1)
            Session.RemoveAll()
            Session([Global].GLOBAL_SESSION) = Nothing
            Response.Redirect("~/Login.aspx")
        End If
        
    End Sub
        
    Private Sub Logout_UTILISATEUR()
        Try
            If Session([Global].GLOBAL_SESSION) IsNot Nothing Then
                _User = CType(Session([Global].GLOBAL_SESSION), Cls_User)
                
                _User.Set_Status_ConnectedUser(False)
                _User.Set_Status_IsForcedOut(True)
                
                Cls_User.Disconnect_User_ById(_User.ID)
                _User.Activite_Utilisateur_InRezo("Log Off", "Log Off, System Exit", "Server")
                
                Session([Global].GLOBAL_SESSION) = Nothing
                Response.Redirect("~/Login.aspx")
            End If
        Catch ex As Exception

        End Try
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
