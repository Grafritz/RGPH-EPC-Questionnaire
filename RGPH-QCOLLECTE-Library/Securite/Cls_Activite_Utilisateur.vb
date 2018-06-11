' @Brain Development
' www.Rezo509.com
' Friday 04-11-2011

Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Net

Imports System
Imports System.Text
Imports System.Data
Imports System.Configuration

Public Class Cls_Activite_Utilisateur
    Implements IGeneral

#Region " Attribut "
    Private _id As Long

    Private _User_ID As String
    Private _user As Cls_User
    Private _Activity As String
    Private _IPUser As String
    Private _Date_Activite As Date
    Private _Description_Activite As String
#End Region

#Region " New "
    Public Sub New()
        BlankProperties()
    End Sub
#End Region

#Region " Properties "
    Public ReadOnly Property ID() As Long Implements IGeneral.ID
        Get
            Return _id
        End Get
    End Property

    Public ReadOnly Property USER_NAME() As String
        Get
            Return _User_ID
        End Get
    End Property

    Public ReadOnly Property USER_OBJET() As Cls_User
        Get
            If Not (_user Is Nothing) Then
                If (_user.ID = 0) Or (_user.ID <> _User_ID) Then
                    _user = New Cls_User(_User_ID)
                End If
            Else
                _user = New Cls_User(_User_ID)
            End If

            Return _user
        End Get
    End Property

    Public ReadOnly Property USERNAME_NOMCOMPLET() As String
        Get
            Return Me.USER_OBJET.NOMCOMPLET & " (" & Me.USER_OBJET.USERNAME & ")"
        End Get
    End Property

    Public ReadOnly Property TYPE_ACTIVITE() As String
        Get
            Return _Activity
        End Get
    End Property

    Public ReadOnly Property IP_USER() As String
        Get
            Return _IPUser
        End Get
    End Property

    Public ReadOnly Property DATE_ACTIVITE() As Date
        Get
            Return _Date_Activite
        End Get
    End Property

    Public ReadOnly Property DESCRIPTION_ACTIVITE() As String
        Get
            Return _Description_Activite
        End Get
    End Property

    Public ReadOnly Property DESCRIPTION_ACTIVITE_Search() As String
        Get
            Return Cls_Enumeration.GetTextFromHtml(_Description_Activite)
        End Get
    End Property
#End Region

#Region " DB Access "
    Public Function Insert(ByVal usr As String) As Integer Implements IGeneral.Insert
        Return Nothing
    End Function

    Public Function Update(ByVal usr As String) As Integer Implements IGeneral.Update
        Return Nothing
    End Function

    Public Function Read(ByVal id As Long) As Boolean Implements IGeneral.Read
        Return True
    End Function

    Private Sub SetProperties(ByVal r As DataRow)
        _id = TypeSafeConversion.NullSafeLong(r("ID_Activite"))

        _User_ID = TypeSafeConversion.NullSafeString(r("User_ID"))
        _Activity = TypeSafeConversion.NullSafeString(r("Activity"))
        _IPUser = TypeSafeConversion.NullSafeString(r("IPUser"))
        _Date_Activite = TypeSafeConversion.NullSafeDate(r("Date_Activite"))
        _Description_Activite = TypeSafeConversion.NullSafeString(r("Description_Activite"))
    End Sub

    Private Sub BlankProperties()
        _id = 0

        _User_ID = ""
        _Activity = ""
        _IPUser = ""
        _Date_Activite = Now
        _Description_Activite = ""
    End Sub

    Public Sub Delete() Implements IGeneral.Delete
        Try
            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SR_Delete_ALL_Activite_Utilisateur")
        Catch ex As SqlClient.SqlException
            Throw New System.Exception(ex.ErrorCode)
        End Try
    End Sub

    Public Shared Sub DeleteALL()
        Try
            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SR_Delete_ALL_Activite_Utilisateur")
        Catch ex As SqlClient.SqlException
            Throw New System.Exception(ex.ErrorCode)
        End Try
    End Sub

    Public Shared Sub DeleteOne(ByVal id As Long)
        Try
            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SR_Delete_Activite_Utilisateur_ById", id)
        Catch ex As SqlClient.SqlException
            Throw New System.Exception(ex.ErrorCode)
        End Try
    End Sub

    Public Function Refresh() As Boolean Implements IGeneral.Refresh
        Return Nothing
    End Function

    Public Function Save(ByVal usr As String) As Integer Implements IGeneral.Save
        Return Nothing
    End Function
#End Region

#Region " Search "
    Public Function Search() As System.Collections.ICollection Implements IGeneral.Search
        Dim objs As New List(Of Cls_Activite_Utilisateur)
        Dim r As DataRow
        Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_ListAll_Activite_Utilisateur")

        For Each r In ds.Tables(0).Rows
            Dim obj1 As New Cls_Activite_Utilisateur

            obj1.SetProperties(r)
            objs.Add(obj1)
        Next r

        Return objs
    End Function

    Public Shared Function SearchAll_Activite_Utilisateur() As List(Of Cls_Activite_Utilisateur)
        Dim objs As New List(Of Cls_Activite_Utilisateur)
        Dim r As DataRow
        Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_ListAll_Activite_Utilisateur")

        For Each r In ds.Tables(0).Rows
            Dim obj1 As New Cls_Activite_Utilisateur

            obj1.SetProperties(r)
            objs.Add(obj1)
        Next r

        Return objs
    End Function

    Public Shared Function COUNT_AllActivite_Utilisateur() As Long
        Dim Nbr As Long = 0
        Try
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_ListAll_Activite_Utilisateur")
            If ds.Tables(0).Rows.Count > 0 Then
                Nbr = ds.Tables(0).Rows.Count
            End If
            Return Nbr
        Catch ex As Rezo509Exception
            ErreurLog.WriteError(" -- >(Class:Cls_Activite_Utilisateur) - Methode:COUNTAll_Activite_Utilisateur :-->" & ex.Message)
        End Try
        Return Nbr
    End Function

    Public Shared Function SearchAll_Utilisateur_INLOG() As List(Of Cls_Activite_Utilisateur)
        Dim objs As New List(Of Cls_Activite_Utilisateur)
        Dim r As DataRow
        Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_ListAll_Utilisateur_InLog")

        For Each r In ds.Tables(0).Rows
            Dim obj1 As New Cls_Activite_Utilisateur

            obj1.SetProperties(r)
            objs.Add(obj1)
        Next r

        Return objs
    End Function

    Public Shared Function SearchAll_ActivityUser_INLOG() As List(Of Cls_Activite_Utilisateur)
        Dim objs As New List(Of Cls_Activite_Utilisateur)
        Dim r As DataRow
        Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_ListAll_ActivityUser_InLog")

        For Each r In ds.Tables(0).Rows
            Dim obj1 As New Cls_Activite_Utilisateur

            obj1.SetProperties(r)
            objs.Add(obj1)
        Next r

        Return objs
    End Function

    Public Shared Function Search_ActiviteUser_ByUser_AndDate(ByVal _User_Name As String, ByVal _Activite As String, ByVal _DateDebut As Date, ByVal _DateFin As Date) As List(Of Cls_Activite_Utilisateur)
        Dim objs As New List(Of Cls_Activite_Utilisateur)
        Dim r As DataRow
        Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_Search_ActiviteUser_ByUser_AndDate", _User_Name, _Activite, _DateDebut, _DateFin)

        For Each r In ds.Tables(0).Rows
            Dim obj1 As New Cls_Activite_Utilisateur

            obj1.SetProperties(r)
            objs.Add(obj1)
        Next r

        Return objs
    End Function

    'Public Shared Function SearchLogByUser(ByVal _tmpU As Long) As List(Of Cls_Activite_Utilisateur)
    '    Dim objs As New List(Of Cls_Activite_Utilisateur)
    '    Dim r As DataRow
    '    Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SS_ListAllLogByUser", _tmpU)

    '    For Each r In ds.Tables(0).Rows
    '        Dim obj1 As New Cls_Activite_Utilisateur

    '        obj1.SetProperties(r)
    '        objs.Add(obj1)
    '    Next r

    '    Return objs
    'End Function

    'Public Shared Function Search_LogByUserAndTypeLog(ByVal _tmpU As Long, ByVal _tmpTL As String) As List(Of Cls_Activite_Utilisateur)
    '    Dim objs As New List(Of Cls_Activite_Utilisateur)
    '    Dim r As DataRow
    '    Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SS_ListAllLogByUserAndTypeLog", _tmpU, _tmpTL)

    '    For Each r In ds.Tables(0).Rows
    '        Dim obj1 As New Cls_Activite_Utilisateur

    '        obj1.SetProperties(r)
    '        objs.Add(obj1)
    '    Next r

    '    Return objs
    'End Function

    'Public Shared Function SearchLogByUserTypeLogAndDate(ByVal _tmpU As Long, ByVal _tmpTL As String, ByVal _tmpDT As Date) As List(Of Cls_Activite_Utilisateur)
    '    Dim objs As New List(Of Cls_Activite_Utilisateur)
    '    Dim r As DataRow
    '    Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SS_ListAllLogByUserTypeLogAndDate", _tmpU, _tmpTL, _tmpDT.Date)

    '    For Each r In ds.Tables(0).Rows
    '        Dim obj1 As New Cls_Activite_Utilisateur

    '        obj1.SetProperties(r)
    '        objs.Add(obj1)
    '    Next r

    '    Return objs
    'End Function

    'Public Shared Function SearchLogByTypeLog(ByVal _tmpTL As String) As List(Of Cls_Activite_Utilisateur)
    '    Dim objs As New List(Of Cls_Activite_Utilisateur)
    '    Dim r As DataRow
    '    Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SS_ListAllLogByTypeLog", _tmpTL)

    '    For Each r In ds.Tables(0).Rows
    '        Dim obj1 As New Cls_Activite_Utilisateur

    '        obj1.SetProperties(r)
    '        objs.Add(obj1)
    '    Next r

    '    Return objs
    'End Function

    'Public Shared Function SearchLogByTypeLogAndDate(ByVal _tmpTL As String, ByVal _tmpDT As Date) As List(Of Cls_Activite_Utilisateur)
    '    Dim objs As New List(Of Cls_Activite_Utilisateur)
    '    Dim r As DataRow
    '    Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SS_ListAllLogByTypeLogAndDate", _tmpTL, _tmpDT)

    '    For Each r In ds.Tables(0).Rows
    '        Dim obj1 As New Cls_Activite_Utilisateur

    '        obj1.SetProperties(r)
    '        objs.Add(obj1)
    '    Next r

    '    Return objs
    'End Function

    'Public Shared Function SearchLogByDate(ByVal _tmpDT As Date) As List(Of Cls_Activite_Utilisateur)
    '    Dim objs As New List(Of Cls_Activite_Utilisateur)
    '    Dim r As DataRow
    '    Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SS_ListAllLogByDate", _tmpDT)

    '    For Each r In ds.Tables(0).Rows
    '        Dim obj1 As New Cls_Activite_Utilisateur

    '        obj1.SetProperties(r)

    '        objs.Add(obj1)
    '    Next r

    '    Return objs
    'End Function

#End Region

#Region " Other Methods "
    Public Function GetObjectString() As String Implements IGeneral.GetObjectString
        Return ""
    End Function
#End Region

End Class