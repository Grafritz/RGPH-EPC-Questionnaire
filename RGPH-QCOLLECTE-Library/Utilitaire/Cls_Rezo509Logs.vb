Public Class Cls_Rezo509Logs

#Region "ATTRIBUTS"
    Private _id As Long
    Private _NameFile As String
    Private _FullNameFile As String
    Private _SizeFile As Long
    Private _CreationTimeFile As DateTime
#End Region

#Region "CONSTRUCTEURS"
    Public Sub New()
        BlankProperties()
    End Sub

    'Public Sub New(ByVal _idOne As Long)
    '    Read(_idOne)
    'End Sub

    'Public Sub New(ByVal dr As Data.DataRow)
    '    Me.SetProperties(dr)
    'End Sub
#End Region

#Region "PROPRIETES"
    Public ReadOnly Property ID() As Long
        Get
            Return _id
        End Get
    End Property

    Public Property NameFile As String
        Get
            Return _NameFile
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_NameFile)) <> LCase(Trim(Value)) Then
                _NameFile = Trim(Value)
            End If
        End Set
    End Property

    Public Property FullNameFile() As String
        Get
            Return _FullNameFile
        End Get
        Set(ByVal Value As String)
            If _FullNameFile <> Value Then
                _FullNameFile = Value
            End If
        End Set
    End Property

    Public Property SizeFile() As Long
        Get
            Return _SizeFile
        End Get
        Set(ByVal Value As Long)
            If _sizeFile <> Value Then
                _sizeFile = Value
            End If
        End Set
    End Property

    Public Property CreationTimeFile() As DateTime
        Get
            Return _CreationTimeFile
        End Get
        Set(ByVal Value As DateTime)
            If _CreationTimeFile <> Value Then
                _CreationTimeFile = Value
            End If
        End Set
    End Property
    #End Region

    Private Sub BlankProperties()
        _id = 0
        _NameFile = ""
        _FullNameFile = ""
        _sizeFile = 0
        _CreationTimeFile = Now
    End Sub
End Class
