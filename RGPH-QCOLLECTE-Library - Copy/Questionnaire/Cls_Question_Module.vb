REM Generate By [GENERIC 12] Application *******
REM  Class Cls_Question_Module

REM Date:19-Jan-2018 12:26 PM
Imports Microsoft
Imports System.Data
Imports System.Collections.Generic
Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer

Public Class Cls_Question_Module
    Implements IGeneral

#Region "Attribut"
    Private _id As Long

    Private _CodeModule As String
    Private __CodeModule As Cls_Module
    Private _CodeQuestion As String
    Private __CodeQuestion As Cls_Questions
    Private _EstDebut As Boolean
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

    Public Sub New(ByVal _CodeModule As String)
        Read_ByCodeModule_EstDebut(_CodeModule)
    End Sub

#End Region

#Region "Properties"
    <AttributLogData(True, 1)>
    Public ReadOnly Property ID() As Long Implements IGeneral.ID
        Get
            Return _id
        End Get
    End Property

    Public ReadOnly Property Ordre As Long
        Get
            Return _id
        End Get
    End Property

    <AttributLogData(True, 2)>
    Public Property CodeModule As String
        Get
            Return _CodeModule
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_CodeModule)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _CodeModule = Trim(Value)
            End If
        End Set
    End Property

    Public Property CodeModuleOBJ As Cls_Module
        Get
            If Not (__CodeModule Is Nothing) Then
                If (__CodeModule.CodeModule = "") Or (__CodeModule.CodeModule <> _CodeModule) Then
                    __CodeModule = New Cls_Module(_CodeModule)
                End If
            Else
                __CodeModule = New Cls_Module(_CodeModule)
            End If

            Return __CodeModule
        End Get
        Set(ByVal value As Cls_Module)
            If value Is Nothing Then
                _isdirty = True
                _CodeModule = ""
            Else
                If __CodeModule.CodeModule <> value.CodeModule Then
                    _isdirty = True
                    _CodeModule = value.CodeModule
                End If
            End If
        End Set
    End Property


    Public ReadOnly Property CodeModuleSTR As String
        Get
            Return CodeModuleOBJ.CodeModule & " - " & CodeModuleOBJ.NomModule
        End Get
    End Property

    <AttributLogData(True, 3)>
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

    Public Property CodeQuestionOBJ As Cls_Questions
        Get
            If Not (__CodeQuestion Is Nothing) Then
                If (__CodeQuestion.CodeQuestion = "") Or (__CodeQuestion.CodeQuestion <> _CodeQuestion) Then
                    __CodeQuestion = New Cls_Questions(_CodeQuestion)
                End If
            Else
                __CodeQuestion = New Cls_Questions(_CodeQuestion)
            End If

            Return __CodeQuestion
        End Get
        Set(ByVal value As Cls_Questions)
            If value Is Nothing Then
                _isdirty = True
                _CodeQuestion = ""
            Else
                If __CodeQuestion.CodeQuestion <> value.CodeQuestion Then
                    _isdirty = True
                    _CodeQuestion = value.CodeQuestion
                End If
            End If
        End Set
    End Property


    Public ReadOnly Property CodeQuestionEtLibelle As String
        Get
            Return CodeQuestionOBJ.CodeQuestionEtLibelle
        End Get
    End Property

    <AttributLogData(True, 4)>
    Public Property EstDebut As Boolean
        Get
            Return _EstDebut
        End Get
        Set(ByVal Value As Boolean)
            If _EstDebut <> Value Then
                _isdirty = True
                _EstDebut = Value
            End If
        End Set
    End Property

    Public ReadOnly Property EstDebut_Image As String
        Get
            Return Cls_Statut.Statut_Image(_EstDebut)
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
        _id = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_Insert_Question_Module", _CodeModule, _CodeQuestion, _EstDebut, usr))
        Return _id
    End Function

    Public Function Update(ByVal usr As String) As Integer Implements IGeneral.Update
        _LogData = GetObjectString()
        Return SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_Update_Question_Module", _id, _CodeModule, _CodeQuestion, _EstDebut, usr)
    End Function

    Private Sub SetProperties(ByVal dr As DataRow)
        _id = TypeSafeConversion.NullSafeLong(dr("Ordre"))
        _CodeModule = TypeSafeConversion.NullSafeString(dr("CodeModule"))
        _CodeQuestion = TypeSafeConversion.NullSafeString(dr("CodeQuestion"))
        _EstDebut = TypeSafeConversion.NullSafeBoolean(dr("EstDebut"))
    End Sub

    Private Sub BlankProperties()
        _id = 0
        _CodeModule = ""
        __CodeModule = Nothing
        _CodeQuestion = ""
        __CodeQuestion = Nothing
        _EstDebut = False
        _isdirty = False
    End Sub

    Public Function Read(ByVal _idpass As Long) As Boolean Implements IGeneral.Read
        Try
            If _idpass <> 0 Then
                Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Select_Question_Module_ByID", _idpass)

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

    Public Function Read_ByCodeModule_EstDebut(ByVal _CodeModule As String) As Boolean
        Try
            If _CodeModule <> "" Then
                Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Select_Question_Module_ByCodeModule_EstDebut", _CodeModule)

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
            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SP_Delete_Question_Module", _id)

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

    Public Shared Function SearchAll() As List(Of Cls_Question_Module)
        Try
            Dim objs As New List(Of Cls_Question_Module)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_Question_Module")
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_Question_Module

                obj.SetProperties(r)

                objs.Add(obj)
            Next r
            Return objs

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Shared Function SearchAllBy_CodeModule(ByVal _codemodule As String) As List(Of Cls_Question_Module)
        Try
            Dim objs As New List(Of Cls_Question_Module)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_Question_Module_CodeModule", _codemodule)
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_Question_Module

                obj.SetProperties(r)

                objs.Add(obj)
            Next r
            Return objs
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function SearchAllBy_CodeQuestion(ByVal _codequestion As String) As List(Of Cls_Question_Module)
        Try
            Dim objs As New List(Of Cls_Question_Module)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_Question_Module_CodeQuestion", _codequestion)
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_Question_Module
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

        If _CodeModule = "" Then
            Throw (New Rezo509Exception("  Code Module Obligatoire"))
        End If

        'If Len(_CodeModule) > 100 Then
        'Throw (New Rezo509Exception(" Trop de caractères insérés pour  Code Module  (la longueur doit être inférieure a 100 caractères.  )"))
        'End If

        If _CodeQuestion = "" Then
            Throw (New Rezo509Exception("  Code Question Obligatoire"))
        End If

        'If Len(_CodeQuestion) > 100 Then
        'Throw (New Rezo509Exception(" Trop de caractères insérés pour  Code Question  (la longueur doit être inférieure a 100 caractères.  )"))
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
        Return LogData(New Cls_Question_Module(Me.ID))
    End Function

    Function LogData(obj As Cls_Question_Module) As String
        Return LogStringBuilder.BuildLogStringHTML(obj)
    End Function

    Function LogData() As String
        Return LogStringBuilder.BuildLogStringHTML(Me)
    End Function
#End Region

End Class


