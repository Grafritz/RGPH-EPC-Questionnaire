REM Generate By [GENERIC 12] Application *******
REM  Class Cls_TypeModule

REM Date:17-Jan-2018 10:50 AM
Imports Microsoft
Imports System.Data
Imports System.Collections.Generic
Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer

Public Class Cls_TypeModule
    Implements IGeneral

#Region "Attribut"
    Private _id As Long

    Private _TypeModule As String
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
    <AttributLogData(True, 1)>
    Public ReadOnly Property ID() As Long Implements IGeneral.ID
        Get
            Return _id
        End Get
    End Property

    <AttributLogData(True, 2)>
    Public Property TypeModule As String
        Get
            Return _TypeModule
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_TypeModule)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _TypeModule = Trim(Value)
            End If
        End Set
    End Property

    Public ReadOnly Property IdEtTypeModule As String
        Get
            Return _id & " - " & _TypeModule
        End Get
    End Property

    Public ReadOnly Property TypeModuleSTR As String
        Get
            Dim col As Long = Cls_Questions.Get_CountQuestion_ByTypeModule(_id)
            Return _TypeModule & " ( <span style='color:" & IIf(col > 0, "green", "red") & ";' > " & col & " Questions</span> ) "
        End Get
    End Property

    ReadOnly Property IsDataDirty() As Boolean
        Get
            Return _isdirty
        End Get
    End Property

#End Region

#Region " Db Access "
    Public Function Insert(ByVal usr As String) As Integer Implements IGeneral.Insert
        _LogData = LogData(Me)
        _id = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_Insert_TypeModule", _TypeModule, usr))
        Return _id
    End Function

    Public Function Update(ByVal usr As String) As Integer Implements IGeneral.Update
        _LogData = GetObjectString()
        Return SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_Update_TypeModule", _id, _TypeModule, usr)
    End Function

    Private Sub SetProperties(ByVal dr As DataRow)
        _id = TypeSafeConversion.NullSafeLong(dr("ID"))
        _TypeModule = TypeSafeConversion.NullSafeString(dr("TypeModule"))
    End Sub

    Private Sub BlankProperties()
        _id = 0
        _TypeModule = ""
        _isdirty = False
    End Sub

    Public Function Read(ByVal _idpass As Long) As Boolean Implements IGeneral.Read
        Try
            If _idpass <> 0 Then
                Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Select_TypeModule_ByID", _idpass)

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
            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SP_Delete_TypeModule", _id)

        Catch ex As Exception
            Throw ex
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

    Public Shared Function SearchAll() As List(Of Cls_TypeModule)
        Try
            Dim objs As New List(Of Cls_TypeModule)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_TypeModule")
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_TypeModule
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

        If _TypeModule = "" Then
            Throw (New Rezo509Exception("  Type Module Obligatoire"))
        End If

        'If Len(_TypeModule) > 200 Then
        'Throw (New Rezo509Exception(" Trop de caractères insérés pour  Type Module  (la longueur doit être inférieure a 200 caractères.  )"))
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
        Return LogData(New Cls_TypeModule(Me.ID))
    End Function

    Function LogData(obj As Cls_TypeModule) As String
        Return LogStringBuilder.BuildLogStringHTML(obj)
    End Function

    Function LogData() As String
        Return LogStringBuilder.BuildLogStringHTML(Me)
    End Function
#End Region

End Class


