REM Generate By [GENERIC 12] Application *******
REM  Class Cls_Questions_Reponses

REM Date:18-Jan-2018 02:07 PM
Imports Microsoft
Imports System.Data
Imports System.Collections.Generic
Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer

Public Class Cls_Questions_Reponses
    Implements IGeneral

#Region "Attribut"
    Private _id As Long

    Private _CodeQuestion As String
    Private __CodeQuestion As Cls_Questions
    Private _CodeUniqueReponse As String
    Private _CodeReponse As String
    Private _LibelleReponse As String
    Private _EstEnfant As Boolean
    Private _AvoirEnfant As Boolean
    Private _CodeParent As String
    Private __CodeParent As Cls_Questions
    Private _QPrecedent As String
    Private _QSuivant As String
    Private __QSuivant As Cls_Questions
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

    Public Sub New(ByVal CodeUniqueReponse As String)
        Read_CodeUniqueReponse(CodeUniqueReponse)
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


    Public ReadOnly Property CodeQuestionSTR As String
        Get
            Return CodeQuestionOBJ.CodeQuestion
        End Get
    End Property

    Public ReadOnly Property CodeEtLibelleQuestionSTR As String
        Get
            Return CodeQuestionSTR & ".- " & CodeQuestionOBJ.Libelle
        End Get
    End Property

    <AttributLogData(True, 3)>
    Public Property CodeUniqueReponse As String
        Get
            Return _CodeUniqueReponse
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_CodeUniqueReponse)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _CodeUniqueReponse = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 4)>
    Public Property CodeReponse As String
        Get
            Return _CodeReponse
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_CodeReponse)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _CodeReponse = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 5)>
    Public Property LibelleReponse As String
        Get
            Return _LibelleReponse
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_LibelleReponse)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _LibelleReponse = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 6)>
    Public Property EstEnfant As Boolean
        Get
            Return _EstEnfant
        End Get
        Set(ByVal Value As Boolean)
            If _EstEnfant <> Value Then
                _isdirty = True
                _EstEnfant = Value
            End If
        End Set
    End Property

    Public ReadOnly Property EstEnfant_Image As String
        Get
            Return Cls_Statut.Statut_Image(_EstEnfant)
        End Get
    End Property

    <AttributLogData(True, 7)>
    Public Property AvoirEnfant As Boolean
        Get
            Return _AvoirEnfant
        End Get
        Set(ByVal Value As Boolean)
            If _AvoirEnfant <> Value Then
                _isdirty = True
                _AvoirEnfant = Value
            End If
        End Set
    End Property

    Public ReadOnly Property AvoirEnfant_Image As String
        Get
            Return Cls_Statut.Statut_Image(_AvoirEnfant)
        End Get
    End Property

    <AttributLogData(True, 8)>
    Public Property CodeParent As String
        Get
            Return _CodeParent
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_CodeParent)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _CodeParent = Trim(Value)
            End If
        End Set
    End Property

    Public Property CodeParentOBJ As Cls_Questions
        Get
            If Not (__CodeParent Is Nothing) Then
                If (__CodeParent.ID = 0) Or (__CodeParent.ID <> _CodeParent) Then
                    __CodeParent = New Cls_Questions(_CodeParent)
                End If
            Else
                __CodeParent = New Cls_Questions(_CodeParent)
            End If

            Return __CodeParent
        End Get
        Set(ByVal value As Cls_Questions)
            If value Is Nothing Then
                _isdirty = True
                _CodeParent = 0
            Else
                If __CodeParent.ID <> value.ID Then
                    _isdirty = True
                    _CodeParent = value.ID
                End If
            End If
        End Set
    End Property


    Public ReadOnly Property CodeParentSTR As String
        Get
            Return _CodeParent
        End Get
    End Property

    <AttributLogData(True, 9)>
    Public Property QPrecedent As String
        Get
            Return _QPrecedent
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_QPrecedent)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _QPrecedent = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 10)>
    Public Property QSuivant As String
        Get
            Return _QSuivant
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_QSuivant)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _QSuivant = Trim(Value)
            End If
        End Set
    End Property

    Public Property QSuivantOBJ As Cls_Questions
        Get
            If Not (__QSuivant Is Nothing) Then
                If (__QSuivant.ID = 0) Or (__QSuivant.ID <> _QSuivant) Then
                    __QSuivant = New Cls_Questions(_QSuivant)
                End If
            Else
                __QSuivant = New Cls_Questions(_QSuivant)
            End If

            Return __QSuivant
        End Get
        Set(ByVal value As Cls_Questions)
            If value Is Nothing Then
                _isdirty = True
                _QSuivant = 0
            Else
                If __QSuivant.ID <> value.ID Then
                    _isdirty = True
                    _QSuivant = value.ID
                End If
            End If
        End Set
    End Property


    Public ReadOnly Property QSuivantSTR As String
        Get
            Return QSuivantOBJ.CodeQuestion
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
        _id = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_Insert_Questions_Reponses", _CodeQuestion, _CodeUniqueReponse, _CodeReponse, _LibelleReponse, _EstEnfant, _AvoirEnfant, _CodeParent, _QPrecedent, _QSuivant, usr))
        Return _id
    End Function

    Public Function Update(ByVal usr As String) As Integer Implements IGeneral.Update
        _LogData = GetObjectString()
        Return SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_Update_Questions_Reponses", _id, _CodeQuestion, _CodeUniqueReponse, _CodeReponse, _LibelleReponse, _EstEnfant, _AvoirEnfant, _CodeParent, _QPrecedent, _QSuivant, usr)
    End Function

    Private Sub SetProperties(ByVal dr As DataRow)
        _id = TypeSafeConversion.NullSafeLong(dr("ID"))
        _CodeQuestion = TypeSafeConversion.NullSafeString(dr("CodeQuestion"))
        _CodeUniqueReponse = TypeSafeConversion.NullSafeString(dr("CodeUniqueReponse"))
        _CodeReponse = TypeSafeConversion.NullSafeString(dr("CodeReponse"))
        _LibelleReponse = TypeSafeConversion.NullSafeString(dr("LibelleReponse"))
        _EstEnfant = TypeSafeConversion.NullSafeBoolean(dr("EstEnfant"))
        _AvoirEnfant = TypeSafeConversion.NullSafeBoolean(dr("AvoirEnfant"))
        _CodeParent = TypeSafeConversion.NullSafeString(dr("CodeParent"))
        _QPrecedent = TypeSafeConversion.NullSafeString(dr("QPrecedent"))
        _QSuivant = TypeSafeConversion.NullSafeString(dr("QSuivant"))
    End Sub

    Private Sub BlankProperties()
        _id = 0
        _CodeQuestion = ""
        __CodeQuestion = Nothing
        _CodeUniqueReponse = ""
        _CodeReponse = ""
        _LibelleReponse = ""
        _EstEnfant = False
        _AvoirEnfant = False
        _CodeParent = ""
        __CodeParent = Nothing
        _QPrecedent = ""
        _QSuivant = ""
        __QSuivant = Nothing
        _isdirty = False
    End Sub

    Public Function Read(ByVal _idpass As Long) As Boolean Implements IGeneral.Read
        Try
            If _idpass <> 0 Then
                Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Select_Questions_Reponses_ByID", _idpass)

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

    Public Function Read_CodeUniqueReponse(ByVal CodeUniqueReponse As String) As Boolean
        Try

            If CodeUniqueReponse <> "" Then
                Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Select_Questions_Reponses_CodeUniqueReponse", CodeUniqueReponse)

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
            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SP_Delete_Questions_Reponses", _id)

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

    Public Shared Function SearchAll(Optional ByVal IDModule As Long = 0) As List(Of Cls_Questions_Reponses)
        Try
            Dim objs As New List(Of Cls_Questions_Reponses)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_Questions_Reponses", IDModule)
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_Questions_Reponses

                obj.SetProperties(r)

                objs.Add(obj)
            Next r
            Return objs

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function SearchAllBy_CodeQuestion(ByVal _codequestion As String) As List(Of Cls_Questions_Reponses)
        Try
            Dim objs As New List(Of Cls_Questions_Reponses)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_Questions_Reponses_CodeQuestion", _codequestion)
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_Questions_Reponses

                obj.SetProperties(r)

                objs.Add(obj)
            Next r
            Return objs
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function SearchAllBy_CodeParent(ByVal _codeparent As String) As List(Of Cls_Questions_Reponses)
        Try
            Dim objs As New List(Of Cls_Questions_Reponses)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_Questions_Reponses_CodeParent", _codeparent)
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_Questions_Reponses

                obj.SetProperties(r)

                objs.Add(obj)
            Next r
            Return objs
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function SearchAll_ReponseParent_ByCodeQuestion(ByVal _codequestion As String) As List(Of Cls_Questions_Reponses)
        Try
            Dim objs As New List(Of Cls_Questions_Reponses)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_Questions_Reponses_Parent_CodeQuestion", _codequestion)
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_Questions_Reponses
                obj.SetProperties(r)
                objs.Add(obj)
            Next r
            Return objs
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function SearchAllBy_QSuivant(ByVal _qsuivant As String) As List(Of Cls_Questions_Reponses)
        Try
            Dim objs As New List(Of Cls_Questions_Reponses)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_Questions_Reponses_QSuivant", _qsuivant)
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_Questions_Reponses

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
    Private Function FoundAlreadyExist_CodeUniqueReponse(ByVal _value As String) As Boolean
        Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Select_Questions_Reponses_CodeUniqueReponse", _value)
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

        If _CodeQuestion = "" Then
            Throw (New Rezo509Exception("  Code Question Obligatoire"))
        End If

        'If Len(_CodeQuestion) > 100 Then
        'Throw (New Rezo509Exception(" Trop de caractères insérés pour  Code Question  (la longueur doit être inférieure a 100 caractères.  )"))
        'End If

        If _CodeUniqueReponse = "" Then
            Throw (New Rezo509Exception("  Code Unique Reponse Obligatoire"))
        End If

        'If Len(_CodeUniqueReponse) > 100 Then
        'Throw (New Rezo509Exception(" Trop de caractères insérés pour  Code Unique Reponse  (la longueur doit être inférieure a 100 caractères.  )"))
        'End If

        If _CodeReponse = "" Then
            Throw (New Rezo509Exception("  Code Reponse Obligatoire"))
        End If

        'If Len(_CodeReponse) > 100 Then
        'Throw (New Rezo509Exception(" Trop de caractères insérés pour  Code Reponse  (la longueur doit être inférieure a 100 caractères.  )"))
        'End If

        If _LibelleReponse = "" Then
            Throw (New Rezo509Exception("  Libelle Reponse Obligatoire"))
        End If

        'If Len(_LibelleReponse) > -1 Then
        'Throw (New Rezo509Exception(" Trop de caractères insérés pour  Libelle Reponse  (la longueur doit être inférieure a -1 caractères.  )"))
        'End If

        'If _CodeParent = "" Then
        '    Throw (New Rezo509Exception("  Code Parent Obligatoire"))
        'End If

        'If Len(_CodeParent) > 100 Then
        'Throw (New Rezo509Exception(" Trop de caractères insérés pour  Code Parent  (la longueur doit être inférieure a 100 caractères.  )"))
        'End If

        'If _QPrecedent = "" Then
        '    Throw (New Rezo509Exception("  Q Precedent Obligatoire"))
        'End If

        'If Len(_QPrecedent) > 100 Then
        'Throw (New Rezo509Exception(" Trop de caractères insérés pour  Q Precedent  (la longueur doit être inférieure a 100 caractères.  )"))
        'End If

        'If _QSuivant = "" Then
        '    Throw (New Rezo509Exception("  Q Suivant Obligatoire"))
        'End If

        'If Len(_QSuivant) > 100 Then
        'Throw (New Rezo509Exception(" Trop de caractères insérés pour  Q Suivant  (la longueur doit être inférieure a 100 caractères.  )"))
        'End If


        If FoundAlreadyExist_CodeUniqueReponse(CodeUniqueReponse) Then
            Throw (New Rezo509Exception("Ce CodeUniqueReponse est déjà enregistré."))
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
        Return LogData(New Cls_Questions_Reponses(Me.ID))
    End Function

    Function LogData(obj As Cls_Questions_Reponses) As String
        Return LogStringBuilder.BuildLogStringHTML(obj)
    End Function

    Function LogData() As String
        Return LogStringBuilder.BuildLogStringHTML(Me)
    End Function
#End Region

End Class


