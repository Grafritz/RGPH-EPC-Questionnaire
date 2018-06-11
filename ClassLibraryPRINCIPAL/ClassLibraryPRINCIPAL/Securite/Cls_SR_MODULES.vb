REM Generate By [GENERIC 12] Application *******
REM  Class Cls_SR_MODULES

REM Date:18-Aug-2014 03h:26 PM
Imports Microsoft
Imports System.Data
Imports System.Collections.Generic
Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer

Public Class Cls_SR_MODULES
    Implements IGeneral

#Region "Attribut"
    Private _id As Long

    Private _Module As String
    Private _ModuleDescription As String
    Private _ActifYN As Boolean
    Private _isdirty As Boolean

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
    Public ReadOnly Property ID() As Long Implements IGeneral.ID
        Get
            Return _id
        End Get
    End Property

    Public Property NomModule As String
        Get
            Return _Module
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_Module)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _Module = Trim(Value)
            End If
        End Set
    End Property

    Public Property Moduledescription As String
        Get
            Return _ModuleDescription
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_ModuleDescription)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _ModuleDescription = Trim(Value)
            End If
        End Set
    End Property

    Public Property Actifyn As Boolean
        Get
            Return _ActifYN
        End Get
        Set(ByVal Value As Boolean)
            If _ActifYN <> Value Then
                _isdirty = True
                _ActifYN = Value
            End If
        End Set
    End Property

    Public ReadOnly Property ACTIF_YN_IMAGE As String
        Get
            Return Statut_Image(_ActifYN)
        End Get
    End Property

    ReadOnly Property IsDataDirty() As Boolean
        Get
            Return _isdirty
        End Get
    End Property

    Public Shared Function Statut_Image(ByVal _Statut As Boolean)
        If _statut Then
            Return "~/images/accept.png"
        Else
            Return "~/images/cancel.png"
        End If
    End Function

#End Region

#Region " Db Access "
    Public Function Insert(ByVal usr As String) As Integer Implements IGeneral.Insert
        _id = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SR_InsertSR_MODULES", _Module, _ModuleDescription, _ActifYN, usr))
        Return _id
    End Function

    Public Function Update(ByVal usr As String) As Integer Implements IGeneral.Update
        Return SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SR_UpdateSR_MODULES", _id, _Module, _ModuleDescription, _ActifYN, usr)
    End Function

    Public Sub SetProperties(ByVal dr As DataRow)
        _id = TypeSafeConversion.NullSafeLong(dr("ID"))
        _Module = TypeSafeConversion.NullSafeString(dr("Module"))
        _ModuleDescription = TypeSafeConversion.NullSafeString(dr("ModuleDescription"))
        _ActifYN = TypeSafeConversion.NullSafeBoolean(dr("ActifYN"))
    End Sub

    Public Sub BlankProperties()
        _id = 0
        _Module = ""
        _ModuleDescription = ""
        _ActifYN = False
    End Sub

    Public Function Read(ByVal _idpass As Long) As Boolean Implements IGeneral.Read
        Try
            If _idpass <> 0 Then
                Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_SelectSR_MODULES_ByID", _idpass)

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
            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SR_DeleteSR_MODULES", _id)

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
        If _isdirty Then
            Validation()

            If _id = 0 Then
                Return Insert(usr)
            Else
                If _id > 0 Then
                    Return Update(usr)
                Else
                    _id = 0
                    Return False
                End If
            End If
        End If

        _isdirty = False
    End Function
#End Region

#Region " Search "
    Public Function Search() As System.Collections.ICollection Implements IGeneral.Search
        Return SearchAll()
    End Function

    Public Shared Function SearchAll() As List(Of Cls_SR_MODULES)
        Try
            Dim objs As New List(Of Cls_SR_MODULES)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_ListAll_SR_MODULES")
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_SR_MODULES

                obj.SetProperties(r)

                objs.Add(obj)
            Next r
            Return objs

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function SearchAll_Actif() As List(Of Cls_SR_MODULES)
        Try
            Dim objs As New List(Of Cls_SR_MODULES)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_ListAll_SR_MODULES_Actif")
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_SR_MODULES

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
    Public Sub Validation()
        If _Module = "" Then
            Throw (New System.Exception(" module obligatoire"))
        End If

        'If Len(_Module) > 200 Then
        '    Throw (New System.Exception(" module trop long(ue) (la longueur doit être inférieure a 200 caractères.  )"))
        'End If

        'If _ModuleDescription = "" Then
        '    Throw (New System.Exception(" moduledescription obligatoire"))
        'End If

        'If Len(_ModuleDescription) > 400 Then
        '    Throw (New System.Exception(" moduledescription trop long(ue) (la longueur doit être inférieure a 400 caractères.  )"))
        'End If

        If FoundName() Then
            Throw (New System.Exception("Description de Droit déjà enregistrée."))
        End If

    End Sub

    Private Function FoundName() As Boolean
        Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_Select_SR_MODULES_ByName", _Module)

        If ds.Tables(0).Rows.Count < 1 Then
            Return False
        Else
            If _id = 0 Then
                Return True
            Else
                If ds.Tables(0).Rows(0).Item("ID") <> _id Then
                    Return True
                Else
                    Return False
                End If
            End If
        End If
    End Function

    Public Function GetObjectString() As String Implements IGeneral.GetObjectString
        Return Nothing
    End Function

#End Region

End Class


