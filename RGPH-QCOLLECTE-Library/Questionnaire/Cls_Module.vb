REM Generate By [GENERIC 12] Application *******
REM  Class Cls_Module

REM Date:18-Jan-2018 10:21 AM
Imports Microsoft
Imports System.Data
Imports System.Collections.Generic
Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer

Public Class Cls_Module
    Implements IGeneral

#Region "Attribut"
    Private _id As Long

    Private _CodeModule As String
    Private _NomModule As String
    Private _TypeModule As Long
    Private __TypeModule As Cls_TypeModule
    Private _Description As String
    Private _EstActif As Boolean
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

    Public Sub New(ByVal CodeModule As String)
        Read_CodeModule(CodeModule)
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

    <AttributLogData(True, 3)>
    Public Property NomModule As String
        Get
            Return _NomModule
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_NomModule)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _NomModule = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 4)>
    Public Property TypeModule As Long
        Get
            Return _TypeModule
        End Get
        Set(ByVal Value As Long)
            If _TypeModule <> Value Then
                _isdirty = True
                _TypeModule = Value
            End If
        End Set
    End Property

    Public ReadOnly Property NomEtCodeModule As String
        Get
            Return _NomModule & " [ " & _CodeModule & " ] "
        End Get
    End Property

    Public ReadOnly Property Code_NomModuleSTR1 As String
        Get
            If _EstActif Then
                Return _NomModule & " [ <span style='color:green;'>" & _CodeModule & "</span> ] "
            Else
                Return "<span style='text-decoration: line-through;color:gray;'>" & _NomModule & " [ <span style=''>" & _CodeModule & "</span> ] </span>"
            End If
        End Get
    End Property

    Public ReadOnly Property Code_NomModuleSTR As String
        Get
            Dim col As Long = Cls_Questions.Get_CountQuestion_ByCodeModule(_CodeModule)
            Dim Qte As String = " ( <span style='color:" & IIf(col > 0, "green", "red") & ";' > " & col & " Questions</span> ) "

            If _EstActif Then
                Return _NomModule & " [ <span style='color:gray;'>" & _CodeModule & "</span> ] " & Qte
            Else
                Return "<span style='text-decoration: line-through;color:gray;'>" & _NomModule & " [ <span style=''>" & _CodeModule & "</span> ] </span>" & Qte
            End If
        End Get
    End Property

    Public Property TypeModuleOBJ As Cls_TypeModule
        Get
            If Not (__TypeModule Is Nothing) Then
                If (__TypeModule.ID = 0) Or (__TypeModule.ID <> _TypeModule) Then
                    __TypeModule = New Cls_TypeModule(_TypeModule)
                End If
            Else
                __TypeModule = New Cls_TypeModule(_TypeModule)
            End If

            Return __TypeModule
        End Get
        Set(ByVal value As Cls_TypeModule)
            If value Is Nothing Then
                _isdirty = True
                _TypeModule = 0
            Else
                If __TypeModule.ID <> value.ID Then
                    _isdirty = True
                    _TypeModule = value.ID
                End If
            End If
        End Set
    End Property


    Public ReadOnly Property TypeModuleSTR As String
        Get
            Return TypeModuleOBJ.IdEtTypeModule
        End Get
    End Property

    <AttributLogData(True, 5)>
    Public Property Description As String
        Get
            Return _Description
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_Description)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _Description = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 6)>
    Public Property EstActif As Boolean
        Get
            Return _EstActif
        End Get
        Set(ByVal Value As Boolean)
            If _EstActif <> Value Then
                _isdirty = True
                _EstActif = Value
            End If
        End Set
    End Property

    Public ReadOnly Property EstActif_Image As String
        Get
            Return Cls_Statut.Statut_Image(_EstActif)
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
        _id = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_Insert_Module", _CodeModule, _NomModule, _TypeModule, _Description, _EstActif, usr))
        Return _id
    End Function

    Public Function Update(ByVal usr As String) As Integer Implements IGeneral.Update
        _LogData = GetObjectString()
        Return SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_Update_Module", _id, _CodeModule, _NomModule, _TypeModule, _Description, _EstActif, usr)
    End Function

    Private Sub SetProperties(ByVal dr As DataRow)
        _id = TypeSafeConversion.NullSafeLong(dr("ID"))
        _CodeModule = TypeSafeConversion.NullSafeString(dr("CodeModule"))
        _NomModule = TypeSafeConversion.NullSafeString(dr("NomModule"))
        _TypeModule = TypeSafeConversion.NullSafeLong(dr("TypeModule"))
        _Description = TypeSafeConversion.NullSafeString(dr("Description"))
        _EstActif = TypeSafeConversion.NullSafeBoolean(dr("EstActif"))
    End Sub

    Private Sub BlankProperties()
        _id = 0
        _CodeModule = ""
        _NomModule = ""
        _TypeModule = 0
        __TypeModule = Nothing
        _Description = ""
        _EstActif = False
        _isdirty = False
    End Sub

    Public Function Read(ByVal _idpass As Long) As Boolean Implements IGeneral.Read
        Try
            If _idpass <> 0 Then
                Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Select_Module_ByID", _idpass)

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

    Public Function Read_CodeModule(ByVal CodeModule As String) As Boolean
        Try

            If CodeModule <> "" Then
                Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Select_Module_CodeModule", CodeModule)

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
            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SP_Delete_Module", _id)

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

    Public Shared Function SearchAll() As List(Of Cls_Module)
        Try
            Dim objs As New List(Of Cls_Module)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_Module")
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_Module

                obj.SetProperties(r)

                objs.Add(obj)
            Next r
            Return objs

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function SearchAll_Actif() As List(Of Cls_Module)
        Try
            Dim objs As New List(Of Cls_Module)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_Module_Actif")
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_Module

                obj.SetProperties(r)

                objs.Add(obj)
            Next r
            Return objs

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Shared Function SearchAllBy_TypeModule(ByVal _typemodule As Long) As List(Of Cls_Module)
        Try
            Dim objs As New List(Of Cls_Module)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_Module_TypeModule", _typemodule)
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_Module

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
    Private Function FoundAlreadyExist_CodeModule(ByVal _value As String) As Boolean
        Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Select_Module_CodeModule", _value)
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

        If _CodeModule = "" Then
            Throw (New Rezo509Exception("  Code Module Obligatoire"))
        End If

        'If Len(_CodeModule) > 100 Then
        'Throw (New Rezo509Exception(" Trop de caractères insérés pour  Code Module  (la longueur doit être inférieure a 100 caractères.  )"))
        'End If

        If _NomModule = "" Then
            Throw (New Rezo509Exception("  Nom Module Obligatoire"))
        End If

        'If Len(_NomModule) > 200 Then
        'Throw (New Rezo509Exception(" Trop de caractères insérés pour  Nom Module  (la longueur doit être inférieure a 200 caractères.  )"))
        'End If

        If _TypeModule = 0 Then
            Throw (New Rezo509Exception("  Type Module Obligatoire"))
        End If

        If _Description = "" Then
            Throw (New Rezo509Exception("  Description Obligatoire"))
        End If

        'If Len(_Description) > 400 Then
        'Throw (New Rezo509Exception(" Trop de caractères insérés pour  Description  (la longueur doit être inférieure a 400 caractères.  )"))
        'End If


        If FoundAlreadyExist_CodeModule(CodeModule) Then
            Throw (New Rezo509Exception("Ce CodeModule est déjà enregistré."))
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
        Return LogData(New Cls_Module(Me.ID))
    End Function

    Function LogData(obj As Cls_Module) As String
        Return LogStringBuilder.BuildLogStringHTML(obj)
    End Function

    Function LogData() As String
        Return LogStringBuilder.BuildLogStringHTML(Me)
    End Function
#End Region

End Class


