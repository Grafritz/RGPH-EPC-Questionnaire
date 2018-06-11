' @Brain Development
' 12-08-2011

Public Class TypeSafeConversion
    Public Const WILDCARD_ID As Integer = 0

    '****************************************************************
    ' NullSafeString
    '****************************************************************
    Public Shared Function NullSafeString(ByVal arg As Object, Optional ByVal returnIfEmpty As String = "") As String
        Dim returnValue As String

        If (arg Is DBNull.Value) OrElse (arg Is Nothing) OrElse (arg Is String.Empty) Then
            returnValue = returnIfEmpty
        Else
            Try
                returnValue = CStr(arg).Trim
            Catch
                returnValue = returnIfEmpty
            End Try
        End If

        Return returnValue
    End Function
    '****************************************************************
    ' NullSafeLong
    '****************************************************************
    Public Shared Function NullSafeLong(ByVal arg As Object, Optional ByVal returnIfEmpty As Long = WILDCARD_ID) As Long
        Dim returnValue As Long

        If (arg Is DBNull.Value) OrElse (arg Is Nothing) OrElse (arg Is String.Empty) Then
            returnValue = returnIfEmpty
        Else
            Try
                returnValue = CLng(arg)
            Catch
                returnValue = returnIfEmpty
            End Try
        End If

        Return returnValue
    End Function
    '****************************************************************
    ' NullSafeInteger
    '****************************************************************
    Public Shared Function NullSafeInteger(ByVal arg As Object, Optional ByVal returnIfEmpty As Integer = WILDCARD_ID) As Integer
        Dim returnValue As Integer

        If (arg Is DBNull.Value) OrElse (arg Is Nothing) OrElse (arg Is String.Empty) Then
            returnValue = returnIfEmpty
        Else
            Try
                returnValue = CInt(arg)
            Catch
                returnValue = returnIfEmpty
            End Try
        End If

        Return returnValue
    End Function
    '****************************************************************
    '   NullSafeDouble
    '****************************************************************
    Public Shared Function NullSafeDouble(ByVal arg As Object, Optional ByVal returnIfEmpty As Double = 0.0) As Double
        Dim returnValue As Double

        If (arg Is DBNull.Value) OrElse (arg Is Nothing) OrElse (arg Is String.Empty) Then
            returnValue = returnIfEmpty
        Else
            Try
                returnValue = CDbl(arg)
            Catch
                returnValue = returnIfEmpty
            End Try
        End If

        Return returnValue
    End Function
    '****************************************************************
    ' NullSafeBoolean
    '****************************************************************
    Public Shared Function NullSafeBoolean(ByVal arg As Object, Optional ByVal returnIfEmpty As Boolean = True) As Boolean
        Dim returnValue As Boolean

        If (arg Is DBNull.Value) OrElse (arg Is Nothing) OrElse (arg Is String.Empty) Then
            returnValue = False
        Else
            Try
                returnValue = CBool(arg)
            Catch
                returnValue = returnIfEmpty
            End Try
        End If

        Return returnValue
    End Function
    '****************************************************************
    ' NullSafeDate
    '****************************************************************
    Public Shared Function NullSafeDate(ByVal arg As Object, Optional ByVal returnIfEmpty As Date = Nothing) As Date
        Dim returnValue As String

        If (arg Is DBNull.Value) OrElse (arg Is Nothing) OrElse (arg Is String.Empty) Then
            returnValue = returnIfEmpty
        Else
            Try
                returnValue = CDate(arg)
            Catch
                returnValue = returnIfEmpty
            End Try

        End If
        REM 01/11/2014
        'Dim DateVal As Nullable(Of DateTime)
        'DateVal = Convert.ToDateTime(arg)
        'If DateVal.HasValue And DateVal.Value.Year < 1753 Then
        '    returnValue = Nothing
        'End If

        Return returnValue
    End Function
    '****************************************************************
    '   NullSafeDecimal
    '****************************************************************
    Public Shared Function NullSafeDecimal(ByVal arg As Object, Optional ByVal returnIfEmpty As Decimal = 0.0) As Double
        Dim returnValue As Double

        If (arg Is DBNull.Value) OrElse (arg Is Nothing) OrElse (arg Is String.Empty) Then
            returnValue = returnIfEmpty
        Else
            Try
                returnValue = CDec(arg)
            Catch
                returnValue = returnIfEmpty
            End Try
        End If

        Return returnValue
    End Function
    '****************************************************************
    ' NullSafeByte
    '****************************************************************
    'Public Shared Function NullSafeByte(ByVal arg As Object, Optional ByVal returnIfEmpty As Byte = WILDCARD_ID) As Integer
    '    Dim returnValue As Integer

    '    If (arg Is DBNull.Value) OrElse (arg Is Nothing) OrElse (arg Is String.Empty) Then
    '        returnValue = returnIfEmpty
    '    Else
    '        Try
    '            returnValue = CByte(arg)
    '        Catch
    '            returnValue = returnIfEmpty
    '        End Try
    '    End If

    '    Return returnValue
    'End Function

    REM 01/11/2014
    Public Shared Function NullSafeByte(ByVal arg As Object) As Byte()
        Dim returnValue As Byte()
        If arg IsNot DBNull.Value Then

        End If

        If (arg IsNot DBNull.Value) Then ' OrElse (arg IsNot Nothing) OrElse (arg IsNot String.Empty) Then
            returnValue = arg
        Else
            returnValue = Nothing
        End If
        Return returnValue
    End Function

    Public Shared Function NullSafeByte_Image(ByVal arg As Object, Optional ByVal returnIfEmpty As Byte = WILDCARD_ID) As Integer
        Dim returnValue As Object

        If (arg Is DBNull.Value) OrElse (arg Is Nothing) OrElse (arg Is String.Empty) Then
            returnValue = Nothing
            Return Nothing
        Else
            Try
                Return CByte(arg)
            Catch
                returnValue = Nothing
                Return Nothing
            End Try
        End If

        Return returnValue
    End Function

    Public Shared Function TranslateIntToBool(ByVal pInteger As Integer) As Boolean
        Select Case pInteger
            Case 1, -1
                Return True
            Case Else
                Return False
        End Select
    End Function

    Public Shared Function TranslateIntToBoolString(ByVal pInteger As Integer) As String
        Select Case pInteger
            Case 1, -1
                Return "Yes"
            Case Else
                Return "No"
        End Select
    End Function

    Public Shared Function TranslateIntToBoolString(ByVal pInteger As Boolean) As String
        Select Case pInteger
            Case True
                Return "1"
            Case Else
                Return "0"
        End Select
    End Function
End Class
