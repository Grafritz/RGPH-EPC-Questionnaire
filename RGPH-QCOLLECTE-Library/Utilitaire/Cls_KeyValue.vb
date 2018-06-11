Public Class Cls_KeyValue

#Region "Attribut"
    Private _FieldName As String
    Private _FieldValue As String
#End Region

#Region "New"
    Public Sub New(ByVal _FieldName As String, ByVal _FieldValue As String)
        Me._FieldName = _FieldName
        Me._FieldValue = _FieldValue
    End Sub
#End Region

#Region "Properties"
    Public Property FieldName As String
        Get
            Return _FieldName
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_FieldName)) <> LCase(Trim(Value)) Then
                _FieldName = Trim(Value)
            End If
        End Set
    End Property

    Public Property FieldValue As String
        Get
            Return _FieldValue
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_FieldValue)) <> LCase(Trim(Value)) Then
                _FieldValue = Trim(Value)
            End If
        End Set
    End Property

    Public ReadOnly Property FieldName_FieldValue As String
        Get
            Return _FieldName & " : " & _FieldValue & ""
        End Get
    End Property

    Public ReadOnly Property FieldValueFieldName As String
        Get
            Return _FieldValue & " : " & _FieldName & ""
        End Get
    End Property
#End Region

End Class
