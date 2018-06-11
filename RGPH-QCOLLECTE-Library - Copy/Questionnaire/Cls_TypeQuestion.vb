REM Generate By [GENERIC 12] Application *******
REM  Class Cls_TypeQuestion

REM Date:18-Jan-2018 12:14 PM
Imports Microsoft
Imports System.Data
Imports System.Collections.Generic
Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer

Public Class Cls_TypeQuestion
    Implements IGeneral

#Region "Attribut"
    Private _id As Long

    Private _TypeQuestion As String
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

    Public Sub New(ByVal TypeQuestion As String)
        Read_TypeQuestion(TypeQuestion)
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
    Public Property TypeQuestion As String
        Get
            Return _TypeQuestion
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_TypeQuestion)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _TypeQuestion = Trim(Value)
            End If
        End Set
    End Property

    Public ReadOnly Property IDEtTypeQuestion As String
        Get
            Return _id & " - " & _TypeQuestion
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
        _id = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_Insert_TypeQuestion", _TypeQuestion, usr))
        Return _id
    End Function

    Public Function Update(ByVal usr As String) As Integer Implements IGeneral.Update
        _LogData = GetObjectString()
        Return SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_Update_TypeQuestion", _id, _TypeQuestion, usr)
    End Function

    Private Sub SetProperties(ByVal dr As DataRow)
        _id = TypeSafeConversion.NullSafeLong(dr("ID"))
        _TypeQuestion = TypeSafeConversion.NullSafeString(dr("TypeQuestion"))
    End Sub

    Private Sub BlankProperties()
        _id = 0
        _TypeQuestion = "N/A"
        _isdirty = False
    End Sub

    Public Function Read(ByVal _idpass As Long) As Boolean Implements IGeneral.Read
        Try
            If _idpass <> 0 Then
                Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Select_TypeQuestion_ByID", _idpass)

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

    Public Function Read_TypeQuestion(ByVal TypeQuestion As String) As Boolean
        Try

            If TypeQuestion <> "" Then
                Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Select_TypeQuestion_TypeQuestion", TypeQuestion)

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
            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SP_Delete_TypeQuestion", _id)

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

    Public Shared Function SearchAll() As List(Of Cls_TypeQuestion)
        Try
            Dim objs As New List(Of Cls_TypeQuestion)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_TypeQuestion")
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_TypeQuestion

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
    Private Function FoundAlreadyExist_TypeQuestion(ByVal _value As String) As Boolean
        Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Select_TypeQuestion_TypeQuestion", _value)
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

        If _TypeQuestion = "" Then
            Throw (New Rezo509Exception("  Type Question Obligatoire"))
        End If

        'If Len(_TypeQuestion) > 200 Then
        'Throw (New Rezo509Exception(" Trop de caractères insérés pour  Type Question  (la longueur doit être inférieure a 200 caractères.  )"))
        'End If


        If FoundAlreadyExist_TypeQuestion(TypeQuestion) Then
            Throw (New Rezo509Exception("Ce TypeQuestion est déjà enregistré."))
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
        Return LogData(New Cls_TypeQuestion(Me.ID))
    End Function

    Function LogData(obj As Cls_TypeQuestion) As String
        Return LogStringBuilder.BuildLogStringHTML(obj)
    End Function

    Function LogData() As String
        Return LogStringBuilder.BuildLogStringHTML(Me)
    End Function
#End Region

End Class


