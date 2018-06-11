' @Brain Development
' www.Rezo509.com
' Saturday 10-12-2011

Imports System.Net.Mail

Public Class Cls_SendMail

#Region " Attribut "
    Private _Subject As String
    Private _Body As String
    Private _Attachment As String
    Private _DisplayName As String
    Private _SendTo As String
    Private _ReplyTo As String
    Private _Bcc As String
    Private _From As String = "info@rezo509.com"

    Private _MailFormatIsHTML As Boolean = True
    Private _smtpClient As New SmtpClient()
#End Region

#Region " New "
    Public Sub New()
        _smtpClient = New SmtpClient()
        _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network
        BlankProperties()
    End Sub

    Public Sub New(ByVal smtpserver As SmtpClient)
        _smtpClient = smtpserver
        _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network
        BlankProperties()
    End Sub

    Public Sub New(ByVal smtpserverAddress As String, ByVal mailAccount As String, ByVal mailPassword As String)
        _smtpClient.Host = smtpserverAddress
        _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network
        Dim credentials As New System.Net.NetworkCredential(mailAccount, mailPassword)
        _smtpClient.UseDefaultCredentials = False
        _smtpClient.Credentials = credentials
        BlankProperties()
    End Sub

#End Region

#Region " Properties "
    Public Property Subject() As String
        Get
            Return _Subject
        End Get
        Set(ByVal Value As String)
            _Subject = Value
        End Set
    End Property

    Public Property Body() As String
        Get
            Return _Body
        End Get
        Set(ByVal Value As String)
            _Body = Value
        End Set
    End Property

    Public Property DispalyName() As String
        Get
            Return _DisplayName
        End Get
        Set(ByVal Value As String)
            _DisplayName = Value
        End Set
    End Property

    Public Property Attachment() As String
        Get
            Return _Attachment
        End Get
        Set(ByVal Value As String)
            _Attachment = Value
        End Set
    End Property

    Public Property SendTo() As String
        Get
            Return _SendTo
        End Get
        Set(ByVal Value As String)
            _SendTo = Value
        End Set
    End Property

    Public Property ReplyTo() As String
        Get
            Return _ReplyTo
        End Get
        Set(ByVal Value As String)
            _ReplyTo = Value
        End Set
    End Property

    Public Property From() As String
        Get
            Return _From
        End Get
        Set(ByVal Value As String)
            _From = Value
        End Set
    End Property

    Public Property BCC() As String
        Get
            Return _Bcc
        End Get
        Set(ByVal Value As String)
            _Bcc = Value
        End Set
    End Property

    Public Property SMTP_CLIENT() As SmtpClient
        Get
            Return _smtpClient
        End Get
        Set(ByVal Value As SmtpClient)
            _smtpClient = Value
        End Set
    End Property

    Public Property MailFormatIsHTML() As Boolean
        Get
            Return _MailFormatIsHTML
        End Get
        Set(ByVal Value As Boolean)
            _MailFormatIsHTML = Value
        End Set
    End Property
#End Region

#Region " Methods "
    Private Sub BlankProperties()
        _Subject = ""
        _Body = ""
        _DisplayName = ""
        _Attachment = ""
        _SendTo = ""
        _ReplyTo = ""
        _From = ""
        _Bcc = ""
    End Sub

    Public Sub MailMessage()
        Dim objMail As New System.Net.Mail.MailMessage(_From, _SendTo)

        objMail.Subject = _Subject
        objMail.Body = _Body

        objMail.IsBodyHtml = _MailFormatIsHTML
        _smtpClient.Send(objMail)
    End Sub

    Public Sub MailMessage_2()
        Dim _From2 As New MailAddress(_From, _DisplayName)
        Dim _SendTo2 As New MailAddress(_SendTo)
        Dim objMail As New System.Net.Mail.MailMessage(_From2, _SendTo2)

        If _ReplyTo.Trim <> "" Then
            Dim _ReplyTo2 As New MailAddress(_ReplyTo)
            'objMail.ReplyTo = _ReplyTo2
        End If

        If _Bcc.Trim <> "" Then
            Dim _BCC2 As New MailAddress(_Bcc)
            objMail.Bcc.Add(_BCC2)
        End If


        objMail.Subject = _Subject
        objMail.Body = _Body

        objMail.IsBodyHtml = _MailFormatIsHTML
        _smtpClient.Send(objMail)
    End Sub

    Public Shared Sub SendErrorEmail(ByVal _sub As String, ByVal _bod As String)
        Dim _obj As New Cls_SendMail
        Dim _to As String = ""
        Dim _from As String = ""

        Try
            _obj.From = _from
            _obj.SendTo = _to
            _obj.Subject = _sub
            _obj.Body = _bod

            _obj.MailMessage()
        Catch ex As Exception
            'Nada
        End Try
    End Sub
#End Region

End Class