' @Brain Develpment
' 11-08-2011
Imports System.Collections.Generic
Public Interface IGeneral
    ReadOnly Property ID() As Long

    Function Insert(ByVal usr As String) As Integer
    Function Update(ByVal usr As String) As Integer
    Function Read(ByVal idval As Long) As Boolean
    Sub Delete()
    Function Refresh() As Boolean
    Function Save(ByVal usr As String) As Integer
    Function Search() As ICollection
    Function GetObjectString() As String
End Interface
