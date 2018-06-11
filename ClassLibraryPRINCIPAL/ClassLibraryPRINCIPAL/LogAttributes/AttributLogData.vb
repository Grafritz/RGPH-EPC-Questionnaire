' @Brain Development
' 13-08-2011

Imports System

<AttributeUsage(AttributeTargets.Property)> _
Public Class AttributLogData
    Inherits Attribute

#Region "Attribut"
    Private _AllowLog As Boolean
    Private _Order As Short
#End Region

#Region "Constructeur"
    Private Sub New()

    End Sub

    Public Sub New(ByVal PAllowLog As Boolean)
        _AllowLog = PAllowLog
    End Sub

    Public Sub New(ByVal PAllowLog As Boolean, ByVal POrder As Short)
        _AllowLog = PAllowLog
        _Order = POrder
    End Sub

    Public Shared Function GetClassType() As Type
        Dim temp As New AttributLogData
        Return temp.GetType
    End Function
#End Region

#Region "Property"
    Public Property AllowLog() As Boolean
        Get
            Return _AllowLog
        End Get
        Set(ByVal Value As Boolean)
            _AllowLog = True
        End Set
    End Property

    Public Property Order() As Short
        Get
            Return _Order
        End Get
        Set(ByVal Value As Short)
            _AllowLog = True
        End Set
    End Property
#End Region

End Class
