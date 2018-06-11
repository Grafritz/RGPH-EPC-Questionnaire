
Imports Microsoft
Imports System.Data
Imports System.Collections.Generic
Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer

Public Class Cls_Departement
    Implements IGeneral

#Region "Attribut"
    Private _id As String

    Private _DeptNom As String
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

    'Public Sub New(ByVal DeptNom As String)
    '    Read_DeptNom(DeptNom)
    'End Sub

    Public Sub New(ByVal DeptId As String)
        Read_DeptId(DeptId)
    End Sub

#End Region

#Region "Properties"
    <AttributLogData(True, 1)> _
    Public ReadOnly Property ID() As Long Implements IGeneral.ID
        Get
            Return _id
        End Get
    End Property

    Public ReadOnly Property ID2 As String
        Get
            Return _id
        End Get
    End Property

    <AttributLogData(True, 2)> _
    Public Property DeptNom As String
        Get
            Return _DeptNom
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_DeptNom)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _DeptNom = Trim(Value)
            End If
        End Set
    End Property

    Public Property DEPARTEMENT As String
        Get
            Return _DeptNom
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_DeptNom)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _DeptNom = Trim(Value)
            End If
        End Set
    End Property

    ReadOnly Property IsDataDirty() As Boolean
        Get
            Return _isdirty
        End Get
    End Property

    Public ReadOnly Property LogData() As String
        Get
            Return _LogData
        End Get
    End Property
#End Region

#Region " Db Access "
    Public Function Insert(ByVal usr As String) As Integer Implements IGeneral.Insert
        _LogData = ""
        _id = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_InsertDepartement", _DeptNom, usr))
        Return _id
    End Function

    Public Function Update(ByVal usr As String) As Integer Implements IGeneral.Update
        _LogData = ""
        '_LogData = GetObjectString()
        Return SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_UpdateDepartement", _id, _DeptNom, usr)
    End Function

    Private Sub SetProperties(ByVal dr As DataRow)
        _id = TypeSafeConversion.NullSafeString(dr("DeptId"))
        _DeptNom = TypeSafeConversion.NullSafeString(dr("DeptNom"))
    End Sub

    Private Sub BlankProperties()
        _id = 0
        _DeptNom = ""
        _isdirty = False
    End Sub

    Public Function Read(ByVal _idpass As Long) As Boolean Implements IGeneral.Read
        Try
            If _idpass <> 0 Then
                Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_SelectDepartement_ByID", _idpass)

                If ds.Tables(0).Rows.Count < 1 Then
                    BlankProperties()
                    Return False
                End If

                SetProperties(ds.Tables(0).Rows(0))
            Else
                BlankProperties()
            End If
            Return True

        Catch ex As Exception

            Throw ex

        End Try

    End Function

    Public Function Read_DeptNom(ByVal DeptNom As String) As Boolean
        Try

            If DeptNom <> "" Then
                Dim ds As Dataset = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_SelectDepartement_DeptNom", DeptNom)

                If ds.tables(0).Rows.Count < 1 Then
                    BlankProperties()
                    Return False
                End If

                SetProperties(ds.Tables(0).Rows(0))
            Else
                BlankProperties()
            End If

            Return True
        Catch ex As Exception

            Throw ex

        End Try

    End Function


    Public Function Read_DeptId(ByVal DeptId As String) As Boolean
        Try

            If DeptId <> "" Then
                Dim ds As Dataset = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_SelectDepartement_DeptId", DeptId)

                If ds.tables(0).Rows.Count < 1 Then
                    BlankProperties()
                    Return False
                End If

                SetProperties(ds.Tables(0).Rows(0))
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
            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SP_DeleteDepartement", _id)

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

    Public Shared Function SearchAll() As List(Of Cls_Departement)
        Try
            Dim objs As New List(Of Cls_Departement)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_Departement")
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_Departement

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
    Private Function FoundAlreadyExist_DeptNom(ByVal _value As String) As Boolean
        Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_SelectDepartement_DeptNom", _value)
        If ds.Tables(0).Rows.Count < 1 Then
            Return False
        Else
            If _id = 0 Then
                Return True
            Else
                If ds.Tables(0).Rows(0).Item("DeptId") <> _id Then
                    Return True
                Else
                    Return False
                End If
            End If
        End If
    End Function


    Private Function FoundAlreadyExist_DeptId(ByVal _value As String) As Boolean
        Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_SelectDepartement_DeptId", _value)
        If ds.Tables(0).Rows.Count < 1 Then
            Return False
        Else
            If _id = 0 Then
                Return True
            Else
                If ds.Tables(0).Rows(0).Item("DeptId") <> _id Then
                    Return True
                Else
                    Return False
                End If
            End If
        End If
    End Function


    Private Sub Validation()
        If _DeptNom = "" Then
            Throw (New System.Exception(" Département obligatoire"))
        End If

        'If Len(_DeptNom) > 140 Then
        '    Throw (New System.Exception(" Trop de caractères insérés pourdeptnom  (la longueur doit être inférieure a 140 caractères.  )"))
        'End If

        If FoundAlreadyExist_DeptNom(DeptNom) Then
            Throw (New System.Exception("Ce Département est déjà enregistré."))
        End If

        'If FoundAlreadyExist_DeptId(DeptId) Then
        '    Throw (New System.Exception("Ce DeptId est déjà enregistré."))
        'End If

    End Sub

    Public Function Encode(ByVal str As Byte()) As String
        Return Convert.ToBase64String(str)
    End Function

    Public Function Decode(ByVal str As String) As Byte()
        Dim decbuff As Byte() = Convert.FromBase64String(str)
        Return decbuff
    End Function

    Public Function GetObjectString() As String Implements IGeneral.GetObjectString
        'Dim _old As New Cls_Departement(Me.ID)
        Return Nothing 'LogStringBuilder.BuildLogStringChangesOnly(_old, Me)
    End Function

#End Region

End Class


