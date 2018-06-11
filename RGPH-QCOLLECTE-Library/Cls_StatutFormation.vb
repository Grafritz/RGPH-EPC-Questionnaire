
Imports Microsoft
Imports System.Data
Imports System.Collections.Generic
Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer

Public Class Cls_StatutFormation
    Implements IGeneral

#Region "Attribut"
    Private _id As Long

    Private _Statut As String
    Private _isdirty As Boolean
    Private _LogData As String

#End Region

#Region "New"
    Public Sub New()
        BlankProperties()
    End Sub

    Public Sub New(ByVal _idOne As Long)
        Read(_idOne)
    End Sub

#End Region

#Region "Properties"
    <AttributLogData(True, 1)> _
    Public ReadOnly Property ID() As Long Implements IGeneral.ID
        Get
            Return _id
        End Get
    End Property

    <AttributLogData(True, 2)> _
    Public Property Statut As String
        Get
            Return _Statut
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_Statut)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _Statut = Trim(Value)
            End If
        End Set
    End Property

    ReadOnly Property IsDataDirty() As Boolean
        Get
            Return _isdirty
        End Get
    End Property

    'Public ReadOnly Property LogData() As String
    '    Get
    '        Return _LogData
    '    End Get
    'End Property
#End Region

#Region " Db Access "
    Public Function Insert(ByVal usr As String) As Integer Implements IGeneral.Insert
        _LogData = ""
        _id = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_InsertStatut", _Statut, usr))
        Return _id
    End Function

    Public Function Update(ByVal usr As String) As Integer Implements IGeneral.Update
        _LogData = ""
        '_LogData = GetObjectString()
        Return SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_UpdateStatut", _id, _Statut, usr)
    End Function

    Private Sub SetProperties(ByVal dr As DataRow)

        _id = TypeSafeConversion.NullSafeLong(dr("ID"))
        _Statut = TypeSafeConversion.NullSafeString(dr("Statut"))

    End Sub

    Private Sub BlankProperties()

        _id = 0
        _Statut = ""
        _isdirty = False

    End Sub

    Public Function Read(ByVal _idpass As Long) As Boolean Implements IGeneral.Read
        Try

            If _idpass <> 0 Then
                Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_SelectStatut_ByID", _idpass)

                If ds.Tables(0).Rows.Count < 1 Then
                    BlankProperties()
                    Return False
                End If

                SetProperties(ds.tables(0).rows(0))
            Else
                BlankProperties()
            End If
            Return True

        Catch ex As Exception

            Throw ex

        End Try

    End Function

    Public Sub Delete() Implements IGeneral.Delete
        Try
            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SP_DeleteStatut", _id)

        Catch ex As SqlClient.SqlException
            Throw New System.Exception(ex.ErrorCode)
        End Try
    End Sub

    Public Function Refresh() As Boolean Implements IGeneral.Refresh
        If _id = 0 Then
            Return False
        Else
            Read(_id)
            Return True
        End If
    End Function

    Public Function Save(ByVal usr As String) As Integer Implements IGeneral.Save
        Dim val As Integer = 0
        If _isdirty Then
            Validation()

            If _id = 0 Then
                val = Insert(usr)
            Else
                If _id > 0 Then
                    val = Update(usr)
                Else
                    val = _id = 0
                    Return False
                End If
            End If
        End If

        _isdirty = False
        Return val
    End Function
#End Region

#Region " Search "
    Public Function Search() As System.Collections.ICollection Implements IGeneral.Search
        Return SearchAll()
    End Function

    Public Shared Function SearchAll() As List(Of Cls_StatutFormation)
        Try
            Dim objs As New List(Of Cls_StatutFormation)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_Statut")
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_StatutFormation

                obj.SetProperties(r)

                objs.Add(obj)
            Next r
            Return objs

        Catch ex As Exception

            Throw ex

        End Try

    End Function



#End Region

#Region " Other Methods "
    Private Sub Validation()

        If _Statut = "" Then
            Throw (New System.Exception(" statut obligatoire"))
        End If

        If Len(_Statut) > 100 Then
            Throw (New System.Exception(" Trop de caractères insérés pourstatut  (la longueur doit être inférieure a 100 caractères.  )"))
        End If


    End Sub

    Public Function Encode(ByVal str As Byte()) As String
        Return Convert.ToBase64String(str)
    End Function

    Public Function Decode(ByVal str As String) As Byte()
        Dim decbuff As Byte() = Convert.FromBase64String(str)
        Return decbuff
    End Function

    Public Function GetObjectString() As String Implements IGeneral.GetObjectString
        Return LogData(New Cls_StatutFormation(Me.ID))
    End Function

    Function LogData(obj As Cls_StatutFormation) As String
        Return LogStringBuilder.BuildLogStringHTML(obj)
    End Function

    Function LogData() As String
        Return LogStringBuilder.BuildLogStringHTML(Me)
    End Function
#End Region

End Class


