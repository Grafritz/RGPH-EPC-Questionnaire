REM Generate By [GENERIC V16] Application *******
REM  Class Cls_QuestionSpecificationControle

REM Date:02-Apr-2018 02:41 PM
Imports Microsoft
Imports System.Data
Imports System.Collections.Generic
Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer

Public Class Cls_QuestionSpecificationControle
    Implements IGeneral

#Region "Attribut"
    Private _id As Long

    Private _CodeQuestion As String
    Private _Question As Cls_Questions
    Private _Instructions As String
    Private _Controles As String
    Private _Messages As String
    Private _CreatedBy As String
    Private _DateCreated As DateTime
    Private _ModifBy As String
    Private _DateModif As DateTime
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
    Public Property CodeQuestion As String
        Get
            Return _CodeQuestion
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_CodeQuestion)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _CodeQuestion = Trim(Value)
            End If
        End Set
    End Property

    Public Property QuestionOBJ As Cls_Questions
        Get
            If Not (_Question Is Nothing) Then
                If (_Question.ID = 0) Or (_Question.ID <> _CodeQuestion) Then
                    _Question = New Cls_Questions(_CodeQuestion)
                End If
            Else
                _Question = New Cls_Questions(_CodeQuestion)
            End If

            Return _Question
        End Get
        Set(ByVal value As Cls_Questions)
            If value Is Nothing Then
                _isdirty = True
                _CodeQuestion = 0
            Else
                If _Question.ID <> value.ID Then
                    _isdirty = True
                    _CodeQuestion = value.ID
                End If
            End If
        End Set
    End Property


    'Public ReadOnly Property CodeQuestion As String
    '    Get
    '        Return QuestionOBJ.CodeQuestion
    '    End Get
    'End Property

    <AttributLogData(True, 3)>
    Public Property Instructions As String
        Get
            Return _Instructions
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_Instructions)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _Instructions = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 4)>
    Public Property Controles As String
        Get
            Return _Controles
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_Controles)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _Controles = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 5)>
    Public Property Messages As String
        Get
            Return _Messages
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_Messages)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _Messages = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 6)>
    Public Property CreatedBy As String
        Get
            Return _CreatedBy
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_CreatedBy)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _CreatedBy = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 7)>
    Public Property DateCreated As DateTime
        Get
            Return _DateCreated
        End Get
        Set(ByVal Value As DateTime)
            If _DateCreated <> Value Then
                _isdirty = True
                _DateCreated = Value
            End If
        End Set
    End Property

    <AttributLogData(True, 8)>
    Public Property ModifBy As String
        Get
            Return _ModifBy
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_ModifBy)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _ModifBy = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 9)>
    Public Property DateModif As DateTime
        Get
            Return _DateModif
        End Get
        Set(ByVal Value As DateTime)
            If _DateModif <> Value Then
                _isdirty = True
                _DateModif = Value
            End If
        End Set
    End Property

    <AttributLogData(True, 10)>
    Public Property isdirty As Boolean
        Get
            Return _isdirty
        End Get
        Set(ByVal Value As Boolean)
            If _isdirty <> Value Then
                _isdirty = True
                _isdirty = Value
            End If
        End Set
    End Property

    '<AttributLogData(True, 11)>
    'Public Property LogData As String
    '    Get
    '        Return _LogData
    '    End Get
    '    Set(ByVal Value As String)
    '        If LCase(Trim(_LogData)) <> LCase(Trim(Value)) Then
    '            _isdirty = True
    '            _LogData = Trim(Value)
    '        End If
    '    End Set
    'End Property

    ReadOnly Property IsDataDirty() As Boolean
        Get
            Return _isdirty
        End Get
    End Property

#End Region

#Region " Db Access "
    Public Function Insert(ByVal usr As String) As Integer Implements IGeneral.Insert
        '_LogData = LogData(Me)
        _id = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_Insert_QuestionSpecificationControle", _CodeQuestion, _Instructions, _Controles, _Messages, usr)) ', _CreatedBy, _DateCreated, _ModifBy, _DateModif
        Return _id
    End Function

    Public Function Update(ByVal usr As String) As Integer Implements IGeneral.Update
        '_LogData = GetObjectString()
        Return SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_Update_QuestionSpecificationControle", _id, _CodeQuestion, _Instructions, _Controles, _Messages, usr) ', _CreatedBy, _DateCreated, _ModifBy, _DateModif
    End Function

    Private Sub SetProperties(ByVal dr As DataRow)
        _id = TypeSafeConversion.NullSafeLong(dr("ID"))
        _CodeQuestion = TypeSafeConversion.NullSafeString(dr("CodeQuestion"))
        _Instructions = TypeSafeConversion.NullSafeString(dr("Instructions"))
        _Controles = TypeSafeConversion.NullSafeString(dr("Controles"))
        _Messages = TypeSafeConversion.NullSafeString(dr("Messages"))
        _CreatedBy = TypeSafeConversion.NullSafeString(dr("CreatedBy"))
        _DateCreated = TypeSafeConversion.NullSafeDate(dr("DateCreated"))
        _ModifBy = TypeSafeConversion.NullSafeString(dr("ModifBy"))
        _DateModif = TypeSafeConversion.NullSafeDate(dr("DateModif"))
    End Sub

    Private Sub BlankProperties()
        _id = 0
        _CodeQuestion = ""
        '__CodeQuestion = Nothing
        _Instructions = ""
        _Controles = ""
        _Messages = ""
        _CreatedBy = ""
        _DateCreated = Now
        _ModifBy = ""
        _DateModif = Now
        _isdirty = False
    End Sub

    Public Function Read(ByVal _idpass As Long) As Boolean Implements IGeneral.Read
        Try
            If _idpass <> 0 Then
                Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Select_QuestionSpecificationControle_ByID", _idpass)

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
            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SP_Delete_QuestionSpecificationControle", _id)

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

    Public Shared Function SearchAll() As List(Of Cls_QuestionSpecificationControle)
        Try
            Dim objs As New List(Of Cls_QuestionSpecificationControle)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_QuestionSpecificationControle")
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_QuestionSpecificationControle

                obj.SetProperties(r)

                objs.Add(obj)
            Next r
            Return objs

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Shared Function SearchAllBy_CodeQuestion(ByVal _codequestion As String) As List(Of Cls_QuestionSpecificationControle)
        Try
            Dim objs As New List(Of Cls_QuestionSpecificationControle)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_QuestionSpecificationControle_CodeQuestion", _codequestion)
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_QuestionSpecificationControle

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

        If _CodeQuestion = "" Then
            Throw (New Rezo509Exception("  Code Question Obligatoire"))
        End If

        'If Len(_CodeQuestion) > 100 Then
        'Throw (New Rezo509Exception(" Trop de caractères insérés pour  Code Question  (la longueur doit être inférieure a 100 caractères.  )"))
        'End If

        If _Instructions = "" Then
            Throw (New Rezo509Exception("  Instructions Obligatoire"))
        End If

        'If Len(_Instructions) > -1 Then
        'Throw (New Rezo509Exception(" Trop de caractères insérés pour  Instructions  (la longueur doit être inférieure a -1 caractères.  )"))
        'End If

        'If _Controles = "" Then
        '    Throw (New Rezo509Exception("  Controles Obligatoire"))
        'End If

        'If Len(_Controles) > -1 Then
        'Throw (New Rezo509Exception(" Trop de caractères insérés pour  Controles  (la longueur doit être inférieure a -1 caractères.  )"))
        'End If

        'If _Messages = "" Then
        '    Throw (New Rezo509Exception("  Messages Obligatoire"))
        'End If

        'If Len(_Messages) > -1 Then
        'Throw (New Rezo509Exception(" Trop de caractères insérés pour  Messages  (la longueur doit être inférieure a -1 caractères.  )"))
        'End If

        'If _CreatedBy = "" Then
        '    Throw (New Rezo509Exception("  Created By Obligatoire"))
        'End If

        ''If Len(_CreatedBy) > 200 Then
        ''Throw (New Rezo509Exception(" Trop de caractères insérés pour  Created By  (la longueur doit être inférieure a 200 caractères.  )"))
        ''End If

        'If _ModifBy = "" Then
        '    Throw (New Rezo509Exception("  Modif By Obligatoire"))
        'End If

        'If Len(_ModifBy) > 200 Then
        'Throw (New Rezo509Exception(" Trop de caractères insérés pour  Modif By  (la longueur doit être inférieure a 200 caractères.  )"))
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
        Return LogData(New Cls_QuestionSpecificationControle(Me.ID))
    End Function

    Function LogData(obj As Cls_QuestionSpecificationControle) As String
        Return LogStringBuilder.BuildLogStringHTML(obj)
    End Function

    Function LogData() As String
        Return LogStringBuilder.BuildLogStringHTML(Me)
    End Function
#End Region

End Class


