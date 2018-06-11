' @Brain Development
' www.Rezo509.com
' Saturday 12-11-2011

Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Net

Public Class Cls_TypeObjet
    Implements IGeneral

#Region " Attribut "
    Private _nomtype As String
#End Region

#Region " New "
    Public Sub New()
    End Sub

    Public Sub New(ByVal _OneSTR As String)
        Read(_OneSTR)
    End Sub
#End Region

#Region " Properties "
    Public ReadOnly Property ID() As Long Implements IGeneral.ID
        Get
        End Get
    End Property

    Public ReadOnly Property NomType() As String
        Get
            Return _nomtype
        End Get
    End Property
#End Region

#Region " DB Access "
    Public Function Insert(ByVal usr As String) As Integer Implements IGeneral.Insert
    End Function

    Public Function Update(ByVal usr As String) As Integer Implements IGeneral.Update
    End Function

    Public Function Read(ByVal id As Long) As Boolean Implements IGeneral.Read
        Return True
    End Function

    Public Function Read(ByVal nom As String) As Boolean
        _nomtype = nom

        Return True
    End Function

    Public Sub Delete() Implements IGeneral.Delete
    End Sub

    Public Function Refresh() As Boolean Implements IGeneral.Refresh
    End Function

    Public Function Save(ByVal usr As String) As Integer Implements IGeneral.Save
    End Function
#End Region

#Region " Search "
    Public Function Search() As System.Collections.ICollection Implements IGeneral.Search
        Dim objs As New Cls_TypeObjetCollections
        Dim r As DataRow
        Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_ListAll_TypeObject")

        For Each r In ds.Tables(0).Rows
            Dim obj1 As New Cls_TypeObjet(Convert.ToString(r("TypeObj")))

            objs.Add(obj1)
        Next r

        Return objs
    End Function

    Public Shared Function SearchDistinctType() As System.Collections.ICollection
        Dim objs As New Cls_TypeObjetCollections
        Dim r As DataRow
        Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_ListAll_TypeObject")

        For Each r In ds.Tables(0).Rows
            Dim obj1 As New Cls_TypeObjet(Convert.ToString(r("TypeObj")))

            objs.Add(obj1)
        Next r

        Return objs
    End Function
#End Region

#Region " Other Methods "
    Public Function GetObjectString() As String Implements IGeneral.GetObjectString
        Return ""
    End Function
#End Region


End Class
