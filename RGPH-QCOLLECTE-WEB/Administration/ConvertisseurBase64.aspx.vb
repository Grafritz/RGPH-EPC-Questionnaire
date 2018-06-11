Imports BRAIN_DEVLOPMENT

Partial Class FireBase_ConvertisseurBase64
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        LabelMessage.Text = ""
    End Sub

    Protected Sub btnEncodeBase64_Click(sender As Object, e As EventArgs) Handles btnEncodeBase64.Click
        Try
            If Not txtNormal.Text.Equals("") Then
                txtEncodeBase64.Text = EncodeBase64(txtNormal.Text)
            End If
        Catch ex As Exception
            LabelMessage.Text = ex.Message
        End Try
    End Sub

    Protected Sub BtnDecodeBase64_Click(sender As Object, e As EventArgs) Handles BtnDecodeBase64.Click
        Try
            If Not txtEncodeBase64.Text.Equals("") Then
                txtDecodeBase64.Text = DecodeBase64(txtEncodeBase64.Text)
            End If
        Catch ex As Exception
            LabelMessage.Text = ex.Message
        End Try
    End Sub

    Public Function EncodeBase64(input As String) As String
        Return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(input))
    End Function

    Public Function DecodeBase64(input As String) As String
        Return System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(input))
    End Function

    'Public Function Encode(ByVal str As Byte()) As String
    '    Return Convert.ToBase64String(str)
    'End Function

    'Public Function Decode(ByVal str As String) As Byte()
    '    Dim decbuff As Byte() = Convert.FromBase64String(str)
    '    Return decbuff
    'End Function

    Protected Sub btnEncripteurEncrypt_Click(sender As Object, e As EventArgs) Handles btnEncripteurEncrypt.Click
        Try
            If Not txtNormal.Text.Equals("") Then
                txtEncodeBase64.Text = Encripteur.Encrypt(txtNormal.Text)
            End If
        Catch ex As Exception
            LabelMessage.Text = ex.Message
        End Try
    End Sub
End Class
