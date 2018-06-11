
Imports Microsoft
Imports System.Data
Imports System.Collections.Generic
Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer

Public Class Cls_Commune
    Implements IGeneral

#Region "Attribut"
    Private _id As String

    Private _ComNom As String
    Private _DeptId As String
    Private _Departement As Cls_Departement
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

    'Public Sub New(ByVal ComNom As String)
    '    Read_ComNom(ComNom)
    'End Sub

    Public Sub New(ByVal ComID As String)
        Read_ComID(ComID)
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
    Public Property ComNom As String
        Get
            Return _ComNom
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_ComNom)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _ComNom = Trim(Value)
            End If
        End Set
    End Property

    Public Property COMMUNE As String
        Get
            Return _ComNom
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_ComNom)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _ComNom = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 3)> _
    Public Property DeptId As String
        Get
            Return _DeptId
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_DeptId)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _DeptId = Trim(Value)
            End If
        End Set
    End Property

    Public Property Departement As Cls_Departement
        Get
            If Not (_Departement Is Nothing) Then
                If (_Departement.ID = 0) Or (_Departement.ID <> _DeptId) Then
                    _Departement = New Cls_Departement(_DeptId)
                End If
            Else
                _Departement = New Cls_Departement(_DeptId)
            End If

            Return _Departement
        End Get
        Set(ByVal value As Cls_Departement)
            If Value Is Nothing Then
                _isdirty = True
                _DeptId = 0
            Else
                If _Departement.ID <> Value.ID Then
                    _isdirty = True
                    _DeptId = value.ID
                End If
            End If
        End Set
    End Property

    ReadOnly Property DepartementSTR As String
        Get
            Return Departement.DeptNom
        End Get
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
        _id = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_InsertCommune", _ComNom, _DeptId, usr))
        Return _id
    End Function

    Public Function Update(ByVal usr As String) As Integer Implements IGeneral.Update
        _LogData = ""
        '_LogData = GetObjectString()
        Return SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_UpdateCommune", _id, _ComNom, _DeptId, usr)
    End Function

    Private Sub SetProperties(ByVal dr As DataRow)
        _id = TypeSafeConversion.NullSafeString(dr("ComID"))
        _ComNom = TypeSafeConversion.NullSafeString(dr("ComNom"))
        _DeptId = TypeSafeConversion.NullSafeString(dr("DeptId"))
        '_DeptId = Nothing
    End Sub

    Private Sub BlankProperties()
        _id = 0
        _ComNom = ""
        _DeptId = ""
        _DeptId = Nothing
        _isdirty = False
    End Sub

    Public Function Read(ByVal _idpass As Long) As Boolean Implements IGeneral.Read
        Try
            If _idpass <> 0 Then
                Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_SelectCommune_ByID", _idpass)

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

    Public Function Read_ComNom(ByVal ComNom As String) As Boolean
        Try
            If ComNom <> "" Then
                Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_SelectCommune_ComNom", ComNom)

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


    Public Function Read_ComID(ByVal ComID As String) As Boolean
        Try
            If ComID <> "" Then
                Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_SelectCommune_ComID", ComID)

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


    Public Sub Delete() Implements IGeneral.Delete
        Try
            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SP_DeleteCommune", _id)

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

    Public Shared Function SearchAll() As List(Of Cls_Commune)
        Try
            Dim objs As New List(Of Cls_Commune)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_Commune")
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_Commune
                obj.SetProperties(r)
                objs.Add(obj)
            Next r
            Return objs
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function SearchAll_Commune_Bydepartement(ByVal _deptid As String) As List(Of Cls_Commune)
        Try
            Dim objs As New List(Of Cls_Commune)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_Commune_DeptId", _deptid)
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_Commune

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
    Private Function FoundAlreadyExist_ComNom(ByVal _value As String) As Boolean
        Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_SelectCommune_ComNom", _value)
        If ds.Tables(0).Rows.Count < 1 Then
            Return False
        Else
            If _id = 0 Then
                Return True
            Else
                If ds.Tables(0).Rows(0).Item("ComID") <> _id Then
                    Return True
                Else
                    Return False
                End If
            End If
        End If
    End Function


    Private Function FoundAlreadyExist_ComID(ByVal _value As String) As Boolean
        Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_SelectCommune_ComID", _value)
        If ds.Tables(0).Rows.Count < 1 Then
            Return False
        Else
            If _id = 0 Then
                Return True
            Else
                If ds.Tables(0).Rows(0).Item("ComID") <> _id Then
                    Return True
                Else
                    Return False
                End If
            End If
        End If
    End Function


    Private Sub Validation()

        If _ComNom = "" Then
            Throw (New System.Exception(" Comnune obligatoire"))
        End If

        'If Len(_ComNom) > 100 Then
        '    Throw (New System.Exception(" Trop de caractères insérés pourcomnom  (la longueur doit être inférieure a 100 caractères.  )"))
        'End If

        If _DeptId = "" Then
            Throw (New System.Exception(" deptid obligatoire"))
        End If

        'If Len(_DeptId) > 20 Then
        '    Throw (New System.Exception(" Trop de caractères insérés pourdeptid  (la longueur doit être inférieure a 20 caractères.  )"))
        'End If


        If FoundAlreadyExist_ComNom(ComNom) Then
            Throw (New System.Exception("Cette Comnune est déjà enregistrée."))
        End If

        'If FoundAlreadyExist_ComID(ComID) Then
        '    Throw (New System.Exception("Ce ComID est déjà enregistré."))
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
        'Dim _old As New Cls_Commune(Me.ID)
        Return Nothing 'LogStringBuilder.BuildLogStringChangesOnly(_old, Me)
    End Function

#End Region

End Class


