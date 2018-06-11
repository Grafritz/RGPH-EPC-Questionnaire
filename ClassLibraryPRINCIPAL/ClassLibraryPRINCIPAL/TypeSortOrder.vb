' @Brain Development
' 12-08-2011

Public Enum TypeSortOrder
    Ascending = 1
    Descending = 2
    None = 0
End Enum

Public Enum DatabaseParameter
    NomServeur
    NomDatabase
    IDUtilisateur
    MotDePasse
End Enum

Public Structure Database
    Public ServerName As String
    Public DataBaseName As String
    Public UserId As String
    Public Pass As String
End Structure

Public Class DateManipJJMMYYYY
    Public Shared Function ConvertStringToDate(ByVal str As String) As Date
        Dim _year As Integer
        Dim _day As Integer
        Dim _month As Integer
        Dim _strIndex As Integer
        Dim _strIndex1 As Integer

        _strIndex = str.IndexOf("/")
        _day = Convert.ToInt32(Left(str, _strIndex))
        _strIndex1 = str.IndexOf("/", _strIndex + 1)
        _month = Convert.ToInt32(Mid(str, _strIndex + 2, _strIndex1 - _strIndex - 1))
        _year = Convert.ToInt32(Right(str, str.Length - _strIndex1 - 1))

        Return (New Date(_year, _month, _day))
    End Function

    Public Shared Function IsDateJJMMAAAA(ByVal str As String) As Boolean
        Dim _ValReturn As Boolean = True

        Dim _year As Integer
        Dim _day As Integer
        Dim _month As Integer
        Dim _strIndex As Integer
        Dim _strIndex1 As Integer

        _strIndex = str.IndexOf("/")
        If _strIndex <> -1 Then
            _day = Convert.ToInt32(Left(str, _strIndex))
            _strIndex1 = str.IndexOf("/", _strIndex + 1)
            If _strIndex <> -1 Then
                _month = Convert.ToInt32(Mid(str, _strIndex + 2, _strIndex1 - _strIndex - 1))
                _year = Convert.ToInt32(Right(str, str.Length - _strIndex1 - 1))
            Else
                _ValReturn = False
            End If
        Else
            _ValReturn = False
        End If

        Return _ValReturn
    End Function
End Class
