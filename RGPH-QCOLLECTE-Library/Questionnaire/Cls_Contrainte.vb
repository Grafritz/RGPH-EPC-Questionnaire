REM Generate By [GENERIC 12] Application *******
REM  Class Cls_Contrainte

REM Date:18-Jan-2018 11:38 AM
Imports Microsoft
Imports System.Data
Imports System.Collections.Generic
Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer

Public Class Cls_Contrainte
    Implements IGeneral

#Region "Attribut"
    Private _id As Long

    Private _Code As Integer
    Private _Contrainte As String
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

    Public Sub New(ByVal Code As Integer)
        Read_Code(Code)
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
    Public Property Code As Integer
        Get
            Return _Code
        End Get
        Set(ByVal Value As Integer)
            If _Code <> Value Then
                _isdirty = True
                _Code = Value
            End If
        End Set
    End Property

    <AttributLogData(True, 3)>
    Public Property Contrainte As String
        Get
            Return _Contrainte
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_Contrainte)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _Contrainte = Trim(Value)
            End If
        End Set
    End Property

    Public ReadOnly Property CodeEtContrainte As String
        Get
            Return _Code & " - " & _Contrainte
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
        _id = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_Insert_Contrainte", _Code, _Contrainte, usr))
        Return _id
    End Function

    Public Function Update(ByVal usr As String) As Integer Implements IGeneral.Update
        _LogData = GetObjectString()
        Return SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_Update_Contrainte", _id, _Code, _Contrainte, usr)
    End Function

    Private Sub SetProperties(ByVal dr As DataRow)
        _id = TypeSafeConversion.NullSafeLong(dr("ID"))
        _Code = TypeSafeConversion.NullSafeInteger(dr("Code"))
        _Contrainte = TypeSafeConversion.NullSafeString(dr("Contrainte"))
    End Sub

    Private Sub BlankProperties()
        _id = 0
        _Code = 0
        _Contrainte = ""
        _isdirty = False
    End Sub

    Public Function Read(ByVal _idpass As Long) As Boolean Implements IGeneral.Read
        Try
            If _idpass <> 0 Then
                Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Select_Contrainte_ByID", _idpass)

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

    Public Function Read_Code(ByVal Code As Integer) As Boolean
        Try

            'If Code <> 0 Then
            Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Select_Contrainte_Code", Code)

                If ds.Tables(0).Rows.Count < 1 Then
                    BlankProperties()
                    Return False
                End If

                SetProperties(ds.Tables(0).Rows(0))
            'Else
            '    BlankProperties()
            'End If

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub Delete() Implements IGeneral.Delete
        Try
            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SP_Delete_Contrainte", _id)

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

    Public Shared Function SearchAll() As List(Of Cls_Contrainte)
        Try
            Dim objs As New List(Of Cls_Contrainte)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_Contrainte")
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_Contrainte

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
    Private Function FoundAlreadyExist_Code(ByVal _value As Integer) As Boolean
        Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Select_Contrainte_Code", _value)
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


    Private Sub Validation()

        'If _Code = 0 Then
        '    Throw (New Rezo509Exception("  Code Obligatoire."))
        'End If

        If _Contrainte = "" Then
            Throw (New Rezo509Exception("  Contrainte Obligatoire"))
        End If

        'If Len(_Contrainte) > 100 Then
        'Throw (New Rezo509Exception(" Trop de caractères insérés pour  Contrainte  (la longueur doit être inférieure a 100 caractères.  )"))
        'End If


        If FoundAlreadyExist_Code(Code) Then
            Throw (New Rezo509Exception("Ce Code est déjà enregistré."))
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
        Return LogData(New Cls_Contrainte(Me.ID))
    End Function

    Function LogData(obj As Cls_Contrainte) As String
        Return LogStringBuilder.BuildLogStringHTML(obj)
    End Function

    Function LogData() As String
        Return LogStringBuilder.BuildLogStringHTML(Me)
    End Function
#End Region

End Class


