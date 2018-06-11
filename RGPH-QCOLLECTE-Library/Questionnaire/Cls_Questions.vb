REM Generate By [GENERIC 12] Application *******
REM  Class Cls_Questions

REM Date:18-Jan-2018 12:29 PM
Imports Microsoft
Imports System.Data
Imports System.Collections.Generic
Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer

Public Class Cls_Questions
    Implements IGeneral

#Region "Attribut"
    Private _id As Long

    Private _CodeQuestion As String
    Private _ID_TypeModule As Long
    Private _TypeModule As Cls_TypeModule

    Private _Libelle As String
    Private _DetailsQuestion As String
    Private _IndicationsQuestion As String
    Private _CodeCategorie As String
    Private __CodeCategorie As Cls_CategorieQuestion
    Private _NomChamps As String
    Private _TypeQuestion As Long
    Private __TypeQuestion As Cls_TypeQuestion
    Private _CaratereAccepte As Integer
    Private __CaratereAccepte As Cls_Contrainte  'Cls_CaratereAccepte
    Private _NbreValeurMinimal As Integer
    Private _NbreCaratereMinimum As Integer
    Private _NbreCaratereMaximal As Integer
    Private _EstSautReponse As Boolean
    Private _QPrecedent As String
    Private _QSuivant As String
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

    Public Sub New(ByVal CodeQuestion As String)
        Read_CodeQuestion(CodeQuestion)
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

    Public Property ID_TypeModule As Long
        Get
            Return _ID_TypeModule
        End Get
        Set(ByVal Value As Long)
            If _ID_TypeModule <> Value Then
                _isdirty = True
                _ID_TypeModule = Value
            End If
        End Set
    End Property

    Public Property TypeModuleOBJ As Cls_TypeModule
        Get
            If Not (_TypeModule Is Nothing) Then
                If (_TypeModule.ID = 0) Or (_TypeModule.ID <> _ID_TypeModule) Then
                    _TypeModule = New Cls_TypeModule(_ID_TypeModule)
                End If
            Else
                _TypeModule = New Cls_TypeModule(_ID_TypeModule)
            End If

            Return _TypeModule
        End Get
        Set(ByVal value As Cls_TypeModule)
            If value Is Nothing Then
                _isdirty = True
                _ID_TypeModule = 0
            Else
                If _TypeModule.ID <> value.ID Then
                    _isdirty = True
                    _ID_TypeModule = value.ID
                End If
            End If
        End Set
    End Property


    Public ReadOnly Property TypeModuleSTR As String
        Get
            Return TypeModuleOBJ.IdEtTypeModule
        End Get
    End Property

    Public ReadOnly Property CodeQuestionEtLibelle As String
        Get
            Return _CodeQuestion & " - " & _Libelle
        End Get
    End Property

    <AttributLogData(True, 3)>
    Public Property Libelle As String
        Get
            Return _Libelle
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_Libelle)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _Libelle = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 4)>
    Public Property DetailsQuestion As String
        Get
            Return _DetailsQuestion
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_DetailsQuestion)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _DetailsQuestion = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 5)>
    Public Property IndicationsQuestion As String
        Get
            Return _IndicationsQuestion
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_IndicationsQuestion)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _IndicationsQuestion = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 6)>
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

    Public Property CodeCategorieOBJ As Cls_CategorieQuestion
        Get
            If Not (__CodeCategorie Is Nothing) Then
                If (__CodeCategorie.CodeCategorie = "") Or (__CodeCategorie.CodeCategorie <> _CodeCategorie) Then
                    __CodeCategorie = New Cls_CategorieQuestion(_CodeCategorie)
                End If
            Else
                __CodeCategorie = New Cls_CategorieQuestion(_CodeCategorie)
            End If

            Return __CodeCategorie
        End Get
        Set(ByVal value As Cls_CategorieQuestion)
            If value Is Nothing Then
                _isdirty = True
                _CodeCategorie = ""
            Else
                If __CodeCategorie.CodeCategorie <> value.CodeCategorie Then
                    _isdirty = True
                    _CodeCategorie = value.CodeCategorie
                End If
            End If
        End Set
    End Property


    Public ReadOnly Property CodeCategorieSTR As String
        Get
            Return CodeCategorieOBJ.CodeEtCategorieQuestion
        End Get
    End Property

    <AttributLogData(True, 7)>
    Public Property NomChamps As String
        Get
            Return _NomChamps
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_NomChamps)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _NomChamps = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 8)>
    Public Property TypeQuestion As Long
        Get
            Return _TypeQuestion
        End Get
        Set(ByVal Value As Long)
            If _TypeQuestion <> Value Then
                _isdirty = True
                _TypeQuestion = Value
            End If
        End Set
    End Property

    Public Property TypeQuestionOBJ As Cls_TypeQuestion
        Get
            If Not (__TypeQuestion Is Nothing) Then
                If (__TypeQuestion.ID = 0) Or (__TypeQuestion.ID <> _TypeQuestion) Then
                    __TypeQuestion = New Cls_TypeQuestion(_TypeQuestion)
                End If
            Else
                __TypeQuestion = New Cls_TypeQuestion(_TypeQuestion)
            End If

            Return __TypeQuestion
        End Get
        Set(ByVal value As Cls_TypeQuestion)
            If value Is Nothing Then
                _isdirty = True
                _TypeQuestion = 0
            Else
                If __TypeQuestion.ID <> value.ID Then
                    _isdirty = True
                    _TypeQuestion = value.ID
                End If
            End If
        End Set
    End Property

    Public ReadOnly Property TypeQuestionSTR As String
        Get
            Return TypeQuestionOBJ.IDEtTypeQuestion
        End Get
    End Property

    Public ReadOnly Property IsHaveSpecification As String
        Get
            Dim col As Long = Get_CountSpecificationControle_ByCodeQuestion(_CodeQuestion)
            Return IIf(col > 0, "<i class='fa fa-thumb-tack' style='color:red;' title='Controle de Specification: " & col & "' >" & IIf(col > 1, ":" & col, "") & "</i>", "")
        End Get
    End Property

    Public Shared Function Get_CountSpecificationControle_ByCodeQuestion(ByVal CodeQuestion As String) As Long
        Dim Nbr As Long = 0
        Try
            Nbr = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_Count_SpecificationControle_ByCodeQuestion", CodeQuestion))
            Return Nbr
        Catch ex As Rezo509Exception
            'ErreurLog.WriteError("[" & Username & "] -- >(Class:Annonce) - Methode:Get_NumberAnnonces_IsNotAnnuler_ByUser :-->" & ex.Message)
        End Try
        Return Nbr
    End Function

    <AttributLogData(True, 9)>
    Public Property CaratereAccepte As Integer
        Get
            Return _CaratereAccepte
        End Get
        Set(ByVal Value As Integer)
            If _CaratereAccepte <> Value Then
                _isdirty = True
                _CaratereAccepte = Value
            End If
        End Set
    End Property

    Public Property CaratereAccepteOBJ As Cls_Contrainte  'Cls_CaratereAccepte
        Get
            If Not (__CaratereAccepte Is Nothing) Then
                If (__CaratereAccepte.ID = 0) Or (__CaratereAccepte.ID <> _CaratereAccepte) Then
                    __CaratereAccepte = New Cls_Contrainte(_CaratereAccepte)
                End If
            Else
                __CaratereAccepte = New Cls_Contrainte(_CaratereAccepte)
            End If

            Return __CaratereAccepte
        End Get
        Set(ByVal value As Cls_Contrainte)
            If value Is Nothing Then
                _isdirty = True
                _CaratereAccepte = 0
            Else
                If __CaratereAccepte.ID <> value.ID Then
                    _isdirty = True
                    _CaratereAccepte = value.ID
                End If
            End If
        End Set
    End Property


    Public ReadOnly Property CaratereAccepteSTR As String
        Get
            Return CaratereAccepteOBJ.CodeEtContrainte
        End Get
    End Property

    <AttributLogData(True, 10)>
    Public Property NbreValeurMinimal As Integer
        Get
            Return _NbreValeurMinimal
        End Get
        Set(ByVal Value As Integer)
            If _NbreValeurMinimal <> Value Then
                _isdirty = True
                _NbreValeurMinimal = Value
            End If
        End Set
    End Property

    <AttributLogData(True, 11)>
    Public Property NbreCaratereMinimum As Integer
        Get
            Return _NbreCaratereMinimum
        End Get
        Set(ByVal Value As Integer)
            If _NbreCaratereMinimum <> Value Then
                _isdirty = True
                _NbreCaratereMinimum = Value
            End If
        End Set
    End Property

    <AttributLogData(True, 12)>
    Public Property NbreCaratereMaximal As Integer
        Get
            Return _NbreCaratereMaximal
        End Get
        Set(ByVal Value As Integer)
            If _NbreCaratereMaximal <> Value Then
                _isdirty = True
                _NbreCaratereMaximal = Value
            End If
        End Set
    End Property

    <AttributLogData(True, 13)>
    Public Property EstSautReponse As Boolean
        Get
            Return _EstSautReponse
        End Get
        Set(ByVal Value As Boolean)
            If _EstSautReponse <> Value Then
                _isdirty = True
                _EstSautReponse = Value
            End If
        End Set
    End Property

    Public ReadOnly Property EstSautReponse_Image As String
        Get
            Return Cls_Statut.Statut_Image(_EstSautReponse)
        End Get
    End Property

    <AttributLogData(True, 14)>
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

    <AttributLogData(True, 15)>
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

    ReadOnly Property IsDataDirty() As Boolean
        Get
            Return _isdirty
        End Get
    End Property

#End Region

#Region " Db Access "
    Public Function Insert(ByVal usr As String) As Integer Implements IGeneral.Insert
        _LogData = LogData(Me)
        _id = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_Insert_Questions", _CodeQuestion, _ID_TypeModule, _Libelle, _DetailsQuestion, _IndicationsQuestion, _CodeCategorie, _NomChamps, _TypeQuestion, _CaratereAccepte, _NbreValeurMinimal, _NbreCaratereMinimum, _NbreCaratereMaximal, _EstSautReponse, _QPrecedent, _QSuivant, usr))
        Return _id
    End Function

    Public Function Update(ByVal usr As String) As Integer Implements IGeneral.Update
        _LogData = GetObjectString()
        Return SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_Update_Questions", _id, _CodeQuestion, _ID_TypeModule, _Libelle, _DetailsQuestion, _IndicationsQuestion, _CodeCategorie, _NomChamps, _TypeQuestion, _CaratereAccepte, _NbreValeurMinimal, _NbreCaratereMinimum, _NbreCaratereMaximal, _EstSautReponse, _QPrecedent, _QSuivant, usr)
    End Function

    Private Sub SetProperties(ByVal dr As DataRow)
        _id = TypeSafeConversion.NullSafeLong(dr("ID"))
        _CodeQuestion = TypeSafeConversion.NullSafeString(dr("CodeQuestion"))
        _ID_TypeModule = TypeSafeConversion.NullSafeLong(dr("ID_TypeModule"))
        _Libelle = TypeSafeConversion.NullSafeString(dr("Libelle"))
        _DetailsQuestion = TypeSafeConversion.NullSafeString(dr("DetailsQuestion"))
        _IndicationsQuestion = TypeSafeConversion.NullSafeString(dr("IndicationsQuestion"))
        _CodeCategorie = TypeSafeConversion.NullSafeString(dr("CodeCategorie"))
        _NomChamps = TypeSafeConversion.NullSafeString(dr("NomChamps"))
        _TypeQuestion = TypeSafeConversion.NullSafeLong(dr("TypeQuestion"))
        _CaratereAccepte = TypeSafeConversion.NullSafeInteger(dr("CaratereAccepte"))
        _NbreValeurMinimal = TypeSafeConversion.NullSafeInteger(dr("NbreValeurMinimal"))
        _NbreCaratereMinimum = TypeSafeConversion.NullSafeInteger(dr("NbreCaratereMinimum"))
        _NbreCaratereMaximal = TypeSafeConversion.NullSafeInteger(dr("NbreCaratereMaximal"))
        _EstSautReponse = TypeSafeConversion.NullSafeBoolean(dr("EstSautReponse"))
        _QPrecedent = TypeSafeConversion.NullSafeString(dr("QPrecedent"))
        _QSuivant = TypeSafeConversion.NullSafeString(dr("QSuivant"))
    End Sub

    Private Sub BlankProperties()
        _id = 0
        _ID_TypeModule = 0
        _CodeQuestion = ""
        _Libelle = ""
        _DetailsQuestion = ""
        _IndicationsQuestion = ""
        _CodeCategorie = ""
        __CodeCategorie = Nothing
        _NomChamps = ""
        _TypeQuestion = 0
        __TypeQuestion = Nothing
        _CaratereAccepte = 0
        __CaratereAccepte = Nothing
        _NbreValeurMinimal = 0
        _NbreCaratereMinimum = 0
        _NbreCaratereMaximal = 0
        _EstSautReponse = False
        _QPrecedent = ""
        _QSuivant = ""
        _isdirty = False
    End Sub

    Public Function Read(ByVal _idpass As Long) As Boolean Implements IGeneral.Read
        Try
            If _idpass <> 0 Then
                Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Select_Questions_ByID", _idpass)

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

    Public Function Read_CodeQuestion(ByVal CodeQuestion As String) As Boolean
        Try

            If CodeQuestion <> "" Then
                Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Select_Questions_CodeQuestion", CodeQuestion)

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
            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SP_Delete_Questions", _id)

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

    Public Shared Function SearchAll(Optional ByVal IDModule As Long = 0) As List(Of Cls_Questions)
        Try
            Dim objs As New List(Of Cls_Questions)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_Questions", IDModule)
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_Questions

                obj.SetProperties(r)

                objs.Add(obj)
            Next r
            Return objs

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function SearchAll_NotIn_QuestionModule_ByIDModule(ByVal _CodeModule As String) As List(Of Cls_Questions)
        Try
            Dim objs As New List(Of Cls_Questions)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_Questions_NotIn_QuestionModule_ByIDModule", _CodeModule)
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_Questions
                obj.SetProperties(r)

                objs.Add(obj)
            Next r
            Return objs

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Shared Function SearchAllBy_CodeCategorie(ByVal _codecategorie As String) As List(Of Cls_Questions)
        Try
            Dim objs As New List(Of Cls_Questions)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_Questions_CodeCategorie", _codecategorie)
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_Questions

                obj.SetProperties(r)

                objs.Add(obj)
            Next r
            Return objs
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function SearchAllBy_TypeQuestion(ByVal _typequestion As Long) As List(Of Cls_Questions)
        Try
            Dim objs As New List(Of Cls_Questions)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_Questions_TypeQuestion", _typequestion)
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_Questions

                obj.SetProperties(r)

                objs.Add(obj)
            Next r
            Return objs
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function SearchAllBy_CaratereAccepte(ByVal _caratereaccepte As Integer) As List(Of Cls_Questions)
        Try
            Dim objs As New List(Of Cls_Questions)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_Questions_CaratereAccepte", _caratereaccepte)
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_Questions

                obj.SetProperties(r)

                objs.Add(obj)
            Next r
            Return objs
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Get_CountQuestion_ByTypeModule(ByVal IdTypeModule As Long) As Long
        Dim Nbr As Long = 0
        Try
            Nbr = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_Count_Questions_ByIDTypeModule", IdTypeModule))
            Return Nbr
        Catch ex As Rezo509Exception
            'ErreurLog.WriteError("[" & Username & "] -- >(Class:Annonce) - Methode:Get_NumberAnnonces_IsNotAnnuler_ByUser :-->" & ex.Message)
        End Try
        Return Nbr
    End Function

    Public Shared Function Get_CountQuestion_ByCodeModule(ByVal CodeModule As String) As Long
        Dim Nbr As Long = 0
        Try
            Nbr = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_Count_Questions_ByCodeModule", CodeModule))
            Return Nbr
        Catch ex As Rezo509Exception
            'ErreurLog.WriteError("[" & Username & "] -- >(Class:Annonce) - Methode:Get_NumberAnnonces_IsNotAnnuler_ByUser :-->" & ex.Message)
        End Try
        Return Nbr
    End Function

#End Region

#Region " Other Methods "
    Private Function FoundAlreadyExist_CodeQuestion(ByVal _value As String) As Boolean
        Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Select_Questions_CodeQuestion", _value)
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

        If _Libelle = "" Then
            Throw (New Rezo509Exception("  Libelle Obligatoire"))
        End If

        'If Len(_Libelle) > -1 Then
        'Throw (New Rezo509Exception(" Trop de caractères insérés pour  Libelle  (la longueur doit être inférieure a -1 caractères.  )"))
        'End If

        'If _DetailsQuestion = "" Then
        '    Throw (New Rezo509Exception("  Details Question Obligatoire"))
        'End If

        'If Len(_DetailsQuestion) > -1 Then
        'Throw (New Rezo509Exception(" Trop de caractères insérés pour  Details Question  (la longueur doit être inférieure a -1 caractères.  )"))
        'End If

        'If _IndicationsQuestion = "" Then
        '    Throw (New Rezo509Exception("  Indications Question Obligatoire"))
        'End If

        'If Len(_IndicationsQuestion) > -1 Then
        'Throw (New Rezo509Exception(" Trop de caractères insérés pour  Indications Question  (la longueur doit être inférieure a -1 caractères.  )"))
        'End If

        If _CodeCategorie = "" Then
            Throw (New Rezo509Exception("  Code Categorie Obligatoire"))
        End If

        'If Len(_CodeCategorie) > 100 Then
        'Throw (New Rezo509Exception(" Trop de caractères insérés pour  Code Categorie  (la longueur doit être inférieure a 100 caractères.  )"))
        'End If

        If _NomChamps = "" Then
            Throw (New Rezo509Exception("  Nom Champs Obligatoire"))
        End If

        'If Len(_NomChamps) > 100 Then
        'Throw (New Rezo509Exception(" Trop de caractères insérés pour  Nom Champs  (la longueur doit être inférieure a 100 caractères.  )"))
        'End If

        'If _TypeQuestion = 0 Then
        '    Throw (New Rezo509Exception("  Type Question Obligatoire"))
        'End If

        'If _CaratereAccepte = 0 Then
        '    Throw (New Rezo509Exception("  Caratere Accepte Obligatoire"))
        'End If

        If _NbreValeurMinimal = 0 Then
            Throw (New Rezo509Exception("  Nbre Valeur Minimal Obligatoire."))
        End If

        If _NbreCaratereMinimum = 0 Then
            Throw (New Rezo509Exception("  Nbre Caratere Minimum Obligatoire."))
        End If

        If _NbreCaratereMaximal = 0 Then
            Throw (New Rezo509Exception("  Nbre Caratere Maximal Obligatoire."))
        End If

        'If _QPrecedent = "" Then
        '    Throw (New Rezo509Exception("  Q Precedent Obligatoire"))
        'End If

        'If Len(_QPrecedent) > 100 Then
        'Throw (New Rezo509Exception(" Trop de caractères insérés pour  Q Precedent  (la longueur doit être inférieure a 100 caractères.  )"))
        'End If

        If _QSuivant = "" Then
            Throw (New Rezo509Exception("  Q Suivant Obligatoire"))
        End If

        'If Len(_QSuivant) > 100 Then
        'Throw (New Rezo509Exception(" Trop de caractères insérés pour  Q Suivant  (la longueur doit être inférieure a 100 caractères.  )"))
        'End If


        If FoundAlreadyExist_CodeQuestion(CodeQuestion) Then
            Throw (New Rezo509Exception("Ce CodeQuestion est déjà enregistré."))
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
        Return LogData(New Cls_Questions(Me.ID))
    End Function

    Function LogData(obj As Cls_Questions) As String
        Return LogStringBuilder.BuildLogStringHTML(obj)
    End Function

    Function LogData() As String
        Return LogStringBuilder.BuildLogStringHTML(Me)
    End Function
#End Region

End Class


