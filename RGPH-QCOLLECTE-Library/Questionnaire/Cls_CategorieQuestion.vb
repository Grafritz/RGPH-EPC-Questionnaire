REM Generate By [GENERIC 12] Application *******
REM  Class Cls_CategorieQuestion

REM Date:18-Jan-2018 11:46 AM
Imports Microsoft
Imports System.Data
Imports System.Collections.Generic
Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer

Public Class Cls_CategorieQuestion
    Implements IGeneral

#Region "Attribut"
    Private _id As Long

    Private _CodeCategorie As String
    Private _CategorieQuestion As String
    Private _DetailsCategorie As String
    Private _SousDetailsCategorie As String
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

    Public Sub New(ByVal CodeCategorie As String)
        Read_CodeCategorie(CodeCategorie)
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
    Public Property CodeCategorie As String
        Get
            Return _CodeCategorie
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_CodeCategorie)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _CodeCategorie = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 3)>
    Public Property CategorieQuestion As String
        Get
            Return _CategorieQuestion
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_CategorieQuestion)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _CategorieQuestion = Trim(Value)
            End If
        End Set
    End Property

    Public ReadOnly Property CodeEtCategorieQuestion As String
        Get
            Return _CodeCategorie & " - " & _CategorieQuestion
        End Get
    End Property

    <AttributLogData(True, 4)>
    Public Property DetailsCategorie As String
        Get
            Return _DetailsCategorie
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_DetailsCategorie)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _DetailsCategorie = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 5)>
    Public Property SousDetailsCategorie As String
        Get
            Return _SousDetailsCategorie
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_SousDetailsCategorie)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _SousDetailsCategorie = Trim(Value)
            End If
        End Set
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
        _id = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_Insert_CategorieQuestion", _CodeCategorie, _CategorieQuestion, _DetailsCategorie, _SousDetailsCategorie, usr))
        Return _id
    End Function

    Public Function Update(ByVal usr As String) As Integer Implements IGeneral.Update
        _LogData = GetObjectString()
        Return SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_Update_CategorieQuestion", _id, _CodeCategorie, _CategorieQuestion, _DetailsCategorie, _SousDetailsCategorie, usr)
    End Function

    Private Sub SetProperties(ByVal dr As DataRow)
        _id = TypeSafeConversion.NullSafeLong(dr("ID"))
        _CodeCategorie = TypeSafeConversion.NullSafeString(dr("CodeCategorie"))
        _CategorieQuestion = TypeSafeConversion.NullSafeString(dr("CategorieQuestion"))
        _DetailsCategorie = TypeSafeConversion.NullSafeString(dr("DetailsCategorie"))
        _SousDetailsCategorie = TypeSafeConversion.NullSafeString(dr("SousDetailsCategorie"))
    End Sub

    Private Sub BlankProperties()
        _id = 0
        _CodeCategorie = ""
        _CategorieQuestion = ""
        _DetailsCategorie = ""
        _SousDetailsCategorie = ""
        _isdirty = False
    End Sub

    Public Function Read(ByVal _idpass As Long) As Boolean Implements IGeneral.Read
        Try
            If _idpass <> 0 Then
                Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Select_CategorieQuestion_ByID", _idpass)

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

    Public Function Read_CodeCategorie(ByVal CodeCategorie As String) As Boolean
        Try

            If CodeCategorie <> "" Then
                Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Select_CategorieQuestion_CodeCategorie", CodeCategorie)

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
            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SP_Delete_CategorieQuestion", _id)

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

    Public Shared Function SearchAll() As List(Of Cls_CategorieQuestion)
        Try
            Dim objs As New List(Of Cls_CategorieQuestion)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_CategorieQuestion")
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_CategorieQuestion

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
    Private Function FoundAlreadyExist_CodeCategorie(ByVal _value As String) As Boolean
        Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Select_CategorieQuestion_CodeCategorie", _value)
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

        If _CodeCategorie = "" Then
            Throw (New Rezo509Exception("  Code Categorie Obligatoire"))
        End If

        'If Len(_CodeCategorie) > 100 Then
        'Throw (New Rezo509Exception(" Trop de caractères insérés pour  Code Categorie  (la longueur doit être inférieure a 100 caractères.  )"))
        'End If

        If _CategorieQuestion = "" Then
            Throw (New Rezo509Exception("  Categorie Question Obligatoire"))
        End If

        'If Len(_CategorieQuestion) > -1 Then
        'Throw (New Rezo509Exception(" Trop de caractères insérés pour  Categorie Question  (la longueur doit être inférieure a -1 caractères.  )"))
        'End If

        'If _DetailsCategorie = "" Then
        '    Throw (New Rezo509Exception("  Details Categorie Obligatoire"))
        'End If

        'If Len(_DetailsCategorie) > -1 Then
        'Throw (New Rezo509Exception(" Trop de caractères insérés pour  Details Categorie  (la longueur doit être inférieure a -1 caractères.  )"))
        'End If

        'If _SousDetailsCategorie = "" Then
        '    Throw (New Rezo509Exception("  Sous Details Categorie Obligatoire"))
        'End If

        'If Len(_SousDetailsCategorie) > -1 Then
        'Throw (New Rezo509Exception(" Trop de caractères insérés pour  Sous Details Categorie  (la longueur doit être inférieure a -1 caractères.  )"))
        'End If


        If FoundAlreadyExist_CodeCategorie(CodeCategorie) Then
            Throw (New Rezo509Exception("Ce CodeCategorie est déjà enregistré."))
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
        Return LogData(New Cls_CategorieQuestion(Me.ID))
    End Function

    Function LogData(obj As Cls_CategorieQuestion) As String
        Return LogStringBuilder.BuildLogStringHTML(obj)
    End Function

    Function LogData() As String
        Return LogStringBuilder.BuildLogStringHTML(Me)
    End Function
#End Region

End Class


