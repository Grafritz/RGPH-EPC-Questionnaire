
Imports Microsoft
Imports System.Data
Imports System.Collections.Generic
Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer

Public Class Cls_Personnel
    Implements IGeneral

#Region "Attribut"
    Private _id As Long

    Private _Nom As String
    Private _Prenom As String
    Private _Sexe As String
    Private _Cin As String
    Private _Nif As String
    Private _Titre As String
    Private _NomUtilisateur As String
    Private _MotDePasse As String
    Private _Mention As String
    Private _Email As String
    Private _TelDigicel As String
    Private _TelNatcom As String
    Private _Valide As String
    Private _EstAssigne As String
    Private _DerniereDateConnexion As String
    Private _ComNaissance As String
    Private _Naissance As Cls_Commune
    Private _LieuNaissance As String
    Private _ProfileId As Integer
    'Private _fileId As Cls_fileId
    'Private _DeptId As String
    'Private _tId As Cls_tId
    'Private _ComID As String
    'Private _ID As Cls_ID
    Private _VqseId As String
    'Private _eId As Cls_eId
    Private _EquipeCodification As Integer
    'Private _ipeCodification As Cls_ipeCodification
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

    'Public Sub New(ByVal Email As String)
    '    Read_Email(Email)
    'End Sub

    Public Sub New(ByVal NomUtilisateur As String)
        Read_NomUtilisateur(NomUtilisateur)
    End Sub

#End Region

#Region "Properties"
    <AttributLogData(True, 1)> _
    Public ReadOnly Property ID() As Long Implements IGeneral.ID
        Get
            Return _id
        End Get
    End Property

    '<AttributLogData(True, 2)> _
    'Public Property Nom As String
    '    Get
    '        Return _Nom
    '    End Get
    '    Set(ByVal Value As String)
    '        If LCase(Trim(_Nom)) <> LCase(Trim(Value)) Then
    '            _isdirty = True
    '            _Nom = Trim(Value)
    '        End If
    '    End Set
    'End Property

    '<AttributLogData(True, 3)> _
    'Public Property Prenom As String
    '    Get
    '        Return _Prenom
    '    End Get
    '    Set(ByVal Value As String)
    '        If LCase(Trim(_Prenom)) <> LCase(Trim(Value)) Then
    '            _isdirty = True
    '            _Prenom = Trim(Value)
    '        End If
    '    End Set
    'End Property

    '<AttributLogData(True, 4)> _
    'Public Property Sexe As String
    '    Get
    '        Return _Sexe
    '    End Get
    '    Set(ByVal Value As String)
    '        If LCase(Trim(_Sexe)) <> LCase(Trim(Value)) Then
    '            _isdirty = True
    '            _Sexe = Trim(Value)
    '        End If
    '    End Set
    'End Property

    '<AttributLogData(True, 5)> _
    'Public Property Cin As String
    '    Get
    '        Return _Cin
    '    End Get
    '    Set(ByVal Value As String)
    '        If LCase(Trim(_Cin)) <> LCase(Trim(Value)) Then
    '            _isdirty = True
    '            _Cin = Trim(Value)
    '        End If
    '    End Set
    'End Property

    '<AttributLogData(True, 6)> _
    'Public Property Nif As String
    '    Get
    '        Return _Nif
    '    End Get
    '    Set(ByVal Value As String)
    '        If LCase(Trim(_Nif)) <> LCase(Trim(Value)) Then
    '            _isdirty = True
    '            _Nif = Trim(Value)
    '        End If
    '    End Set
    'End Property

    '<AttributLogData(True, 7)> _
    'Public Property Titre As String
    '    Get
    '        Return _Titre
    '    End Get
    '    Set(ByVal Value As String)
    '        If LCase(Trim(_Titre)) <> LCase(Trim(Value)) Then
    '            _isdirty = True
    '            _Titre = Trim(Value)
    '        End If
    '    End Set
    'End Property

    '<AttributLogData(True, 8)> _
    'Public Property NomUtilisateur As String
    '    Get
    '        Return _NomUtilisateur
    '    End Get
    '    Set(ByVal Value As String)
    '        If LCase(Trim(_NomUtilisateur)) <> LCase(Trim(Value)) Then
    '            _isdirty = True
    '            _NomUtilisateur = Trim(Value)
    '        End If
    '    End Set
    'End Property

    '<AttributLogData(True, 9)> _
    'Public Property MotDePasse As String
    '    Get
    '        Return _MotDePasse
    '    End Get
    '    Set(ByVal Value As String)
    '        If LCase(Trim(_MotDePasse)) <> LCase(Trim(Value)) Then
    '            _isdirty = True
    '            _MotDePasse = Trim(Value)
    '        End If
    '    End Set
    'End Property

    '<AttributLogData(True, 10)> _
    'Public Property Mention As String
    '    Get
    '        Return _Mention
    '    End Get
    '    Set(ByVal Value As String)
    '        If LCase(Trim(_Mention)) <> LCase(Trim(Value)) Then
    '            _isdirty = True
    '            _Mention = Trim(Value)
    '        End If
    '    End Set
    'End Property

    '<AttributLogData(True, 11)> _
    'Public Property Email As String
    '    Get
    '        Return _Email
    '    End Get
    '    Set(ByVal Value As String)
    '        If LCase(Trim(_Email)) <> LCase(Trim(Value)) Then
    '            _isdirty = True
    '            _Email = Trim(Value)
    '        End If
    '    End Set
    'End Property

    '<AttributLogData(True, 12)> _
    'Public Property TelDigicel As String
    '    Get
    '        Return _TelDigicel
    '    End Get
    '    Set(ByVal Value As String)
    '        If LCase(Trim(_TelDigicel)) <> LCase(Trim(Value)) Then
    '            _isdirty = True
    '            _TelDigicel = Trim(Value)
    '        End If
    '    End Set
    'End Property

    '<AttributLogData(True, 13)> _
    'Public Property TelNatcom As String
    '    Get
    '        Return _TelNatcom
    '    End Get
    '    Set(ByVal Value As String)
    '        If LCase(Trim(_TelNatcom)) <> LCase(Trim(Value)) Then
    '            _isdirty = True
    '            _TelNatcom = Trim(Value)
    '        End If
    '    End Set
    'End Property

    '<AttributLogData(True, 14)> _
    'Public Property Valide As String
    '    Get
    '        Return _Valide
    '    End Get
    '    Set(ByVal Value As String)
    '        If LCase(Trim(_Valide)) <> LCase(Trim(Value)) Then
    '            _isdirty = True
    '            _Valide = Trim(Value)
    '        End If
    '    End Set
    'End Property

    '<AttributLogData(True, 15)> _
    'Public Property EstAssigne As String
    '    Get
    '        Return _EstAssigne
    '    End Get
    '    Set(ByVal Value As String)
    '        If LCase(Trim(_EstAssigne)) <> LCase(Trim(Value)) Then
    '            _isdirty = True
    '            _EstAssigne = Trim(Value)
    '        End If
    '    End Set
    'End Property

    '<AttributLogData(True, 16)> _
    'Public Property DerniereDateConnexion As String
    '    Get
    '        Return _DerniereDateConnexion
    '    End Get
    '    Set(ByVal Value As String)
    '        If LCase(Trim(_DerniereDateConnexion)) <> LCase(Trim(Value)) Then
    '            _isdirty = True
    '            _DerniereDateConnexion = Trim(Value)
    '        End If
    '    End Set
    'End Property

    '<AttributLogData(True, 17)> _
    'Public Property ComNaissance As String
    '    Get
    '        Return _ComNaissance
    '    End Get
    '    Set(ByVal Value As String)
    '        If LCase(Trim(_ComNaissance)) <> LCase(Trim(Value)) Then
    '            _isdirty = True
    '            _ComNaissance = Trim(Value)
    '        End If
    '    End Set
    'End Property

    'Public Property Naissance As Cls_Commune

    '    Get
    '        If Not (_Naissance Is Nothing) Then
    '            If (_Naissance.ID = 0) Or (_Naissance.ID <> _ComNaissance) Then
    '                _Naissance = New Cls_Commune(_ComNaissance)
    '            End If
    '        Else
    '            _Naissance = New Cls_Commune(_ComNaissance)
    '        End If

    '        Return _Naissance
    '    End Get
    '    Set(ByVal value As Cls_Commune)
    '        If value Is Nothing Then
    '            _isdirty = True
    '            _ComNaissance = 0
    '        Else
    '            If _Naissance.ID <> value.ID Then
    '                _isdirty = True
    '                _ComNaissance = value.ID
    '            End If
    '        End If
    '    End Set
    'End Property


    '<AttributLogData(True, 18)> _
    'Public Property LieuNaissance As String
    '    Get
    '        Return _LieuNaissance
    '    End Get
    '    Set(ByVal Value As String)
    '        If LCase(Trim(_LieuNaissance)) <> LCase(Trim(Value)) Then
    '            _isdirty = True
    '            _LieuNaissance = Trim(Value)
    '        End If
    '    End Set
    'End Property

    '<AttributLogData(True, 19)> _
    'Public Property ProfileId As Integer
    '    Get
    '        Return _ProfileId
    '    End Get
    '    Set(ByVal Value As Integer)
    '        If _ProfileId <> Value Then
    '            _isdirty = True
    '            _ProfileId = Value
    '        End If
    '    End Set
    'End Property

    'Public Property fileId As Cls_fileId

    '    Get
    '        If Not (_fileId Is Nothing) Then
    '            If (_fileId.ID = 0) Or (_fileId.ID <> _ProfileId) Then
    '                _fileId = New Cls_fileId(_ProfileId)
    '            End If
    '        Else
    '            _fileId = New Cls_fileId(_ProfileId)
    '        End If

    '        Return _fileId
    '    End Get
    '    Set(ByVal value As Cls_fileId)
    '        If Value Is Nothing Then
    '            _isdirty = True
    '            _ProfileId = 0
    '        Else
    '            If _fileId.ID <> Value.ID Then
    '                _isdirty = True
    '                _ProfileId = Value.ID
    '            End If
    '        End If
    '    End Set
    'End Property


    '<AttributLogData(True, 20)> _
    'Public Property DeptId As String
    '    Get
    '        Return _DeptId
    '    End Get
    '    Set(ByVal Value As String)
    '        If LCase(Trim(_DeptId)) <> LCase(Trim(Value)) Then
    '            _isdirty = True
    '            _DeptId = Trim(Value)
    '        End If
    '    End Set
    'End Property

    'Public Property tId As Cls_tId

    '    Get
    '        If Not (_tId Is Nothing) Then
    '            If (_tId.ID = 0) Or (_tId.ID <> _DeptId) Then
    '                _tId = New Cls_tId(_DeptId)
    '            End If
    '        Else
    '            _tId = New Cls_tId(_DeptId)
    '        End If

    '        Return _tId
    '    End Get
    '    Set(ByVal value As Cls_tId)
    '        If Value Is Nothing Then
    '            _isdirty = True
    '            _DeptId = 0
    '        Else
    '            If _tId.ID <> Value.ID Then
    '                _isdirty = True
    '                _DeptId = Value.ID
    '            End If
    '        End If
    '    End Set
    'End Property


    '<AttributLogData(True, 21)> _
    'Public Property ComID As String
    '    Get
    '        Return _ComID
    '    End Get
    '    Set(ByVal Value As String)
    '        If LCase(Trim(_ComID)) <> LCase(Trim(Value)) Then
    '            _isdirty = True
    '            _ComID = Trim(Value)
    '        End If
    '    End Set
    'End Property

    'Public Property ID As Cls_ID

    '    Get
    '        If Not (_ID Is Nothing) Then
    '            If (_ID.ID = 0) Or (_ID.ID <> _ComID) Then
    '                _ID = New Cls_ID(_ComID)
    '            End If
    '        Else
    '            _ID = New Cls_ID(_ComID)
    '        End If

    '        Return _ID
    '    End Get
    '    Set(ByVal value As Cls_ID)
    '        If Value Is Nothing Then
    '            _isdirty = True
    '            _ComID = 0
    '        Else
    '            If _ID.ID <> Value.ID Then
    '                _isdirty = True
    '                _ComID = Value.ID
    '            End If
    '        End If
    '    End Set
    'End Property


    '<AttributLogData(True, 22)> _
    'Public Property VqseId As String
    '    Get
    '        Return _VqseId
    '    End Get
    '    Set(ByVal Value As String)
    '        If LCase(Trim(_VqseId)) <> LCase(Trim(Value)) Then
    '            _isdirty = True
    '            _VqseId = Trim(Value)
    '        End If
    '    End Set
    'End Property

    'Public Property eId As Cls_eId

    '    Get
    '        If Not (_eId Is Nothing) Then
    '            If (_eId.ID = 0) Or (_eId.ID <> _VqseId) Then
    '                _eId = New Cls_eId(_VqseId)
    '            End If
    '        Else
    '            _eId = New Cls_eId(_VqseId)
    '        End If

    '        Return _eId
    '    End Get
    '    Set(ByVal value As Cls_eId)
    '        If Value Is Nothing Then
    '            _isdirty = True
    '            _VqseId = 0
    '        Else
    '            If _eId.ID <> Value.ID Then
    '                _isdirty = True
    '                _VqseId = Value.ID
    '            End If
    '        End If
    '    End Set
    'End Property


    <AttributLogData(True, 23)> _
    Public Property EquipeCodification As Integer
        Get
            Return _EquipeCodification
        End Get
        Set(ByVal Value As Integer)
            If _EquipeCodification <> Value Then
                _isdirty = True
                _EquipeCodification = Value
            End If
        End Set
    End Property

    'Public Property ipeCodification As Cls_ipeCodification

    '    Get
    '        If Not (_ipeCodification Is Nothing) Then
    '            If (_ipeCodification.ID = 0) Or (_ipeCodification.ID <> _EquipeCodification) Then
    '                _ipeCodification = New Cls_ipeCodification(_EquipeCodification)
    '            End If
    '        Else
    '            _ipeCodification = New Cls_ipeCodification(_EquipeCodification)
    '        End If

    '        Return _ipeCodification
    '    End Get
    '    Set(ByVal value As Cls_ipeCodification)
    '        If Value Is Nothing Then
    '            _isdirty = True
    '            _EquipeCodification = 0
    '        Else
    '            If _ipeCodification.ID <> Value.ID Then
    '                _isdirty = True
    '                _EquipeCodification = Value.ID
    '            End If
    '        End If
    '    End Set
    'End Property


    ReadOnly Property IsDataDirty() As Boolean
        Get
            Return _isdirty
        End Get
    End Property

    Public ReadOnly Property LogData() As String
        Get
            Return _LogData
        End Get
    End Property
#End Region

#Region " Db Access "
    Public Function Insert(ByVal usr As String) As Integer Implements IGeneral.Insert
        _LogData = ""
        '_id = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_Insertonnel", _Nom, _Prenom, _Sexe, _Cin, _Nif, _Titre, _NomUtilisateur, _MotDePasse, _Mention, _Email, _TelDigicel, _TelNatcom, _Valide, _EstAssigne, _DerniereDateConnexion, _ComNaissance, _LieuNaissance, _ProfileId, _DeptId, _ComID, _VqseId, _EquipeCodification, usr))
        Return 0 '_id
    End Function

    Public Function Update(ByVal usr As String) As Integer Implements IGeneral.Update
        _LogData = ""
        '_LogData = GetObjectString()
        Return 0 'SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_Updateonnel", _id, _Nom, _Prenom, _Sexe, _Cin, _Nif, _Titre, _NomUtilisateur, _MotDePasse, _Mention, _Email, _TelDigicel, _TelNatcom, _Valide, _EstAssigne, _DerniereDateConnexion, _ComNaissance, _LieuNaissance, _ProfileId, _DeptId, _ComID, _VqseId, _EquipeCodification, usr)
    End Function

    Private Sub SetProperties(ByVal dr As DataRow)

        '_id = TypeSafeConversion.NullSafeLong(dr("PersId"))
        '_Nom = TypeSafeConversion.NullSafeString(dr("Nom"))
        '_Prenom = TypeSafeConversion.NullSafeString(dr("Prenom"))
        '_Sexe = TypeSafeConversion.NullSafeString(dr("Sexe"))
        '_Cin = TypeSafeConversion.NullSafeString(dr("Cin"))
        '_Nif = TypeSafeConversion.NullSafeString(dr("Nif"))
        '_Titre = TypeSafeConversion.NullSafeString(dr("Titre"))
        '_NomUtilisateur = TypeSafeConversion.NullSafeString(dr("NomUtilisateur"))
        '_MotDePasse = TypeSafeConversion.NullSafeString(dr("MotDePasse"))
        '_Mention = TypeSafeConversion.NullSafeString(dr("Mention"))
        '_Email = TypeSafeConversion.NullSafeString(dr("Email"))
        '_TelDigicel = TypeSafeConversion.NullSafeString(dr("TelDigicel"))
        '_TelNatcom = TypeSafeConversion.NullSafeString(dr("TelNatcom"))
        '_Valide = TypeSafeConversion.NullSafeString(dr("Valide"))
        '_EstAssigne = TypeSafeConversion.NullSafeString(dr("EstAssigne"))
        '_DerniereDateConnexion = TypeSafeConversion.NullSafeString(dr("DerniereDateConnexion"))
        '_ComNaissance = TypeSafeConversion.NullSafeString(dr("ComNaissance"))
        '_Naissance = Nothing
        '_LieuNaissance = TypeSafeConversion.NullSafeString(dr("LieuNaissance"))
        '_ProfileId = TypeSafeConversion.NullSafeInteger(dr("ProfileId"))
        '_fileId = Nothing
        '_DeptId = TypeSafeConversion.NullSafeString(dr("DeptId"))
        '_tId = Nothing
        '_ComID = TypeSafeConversion.NullSafeString(dr("ComID"))
        '_ID = Nothing
        '_VqseId = TypeSafeConversion.NullSafeString(dr("VqseId"))
        '_eId = Nothing
        '_EquipeCodification = TypeSafeConversion.NullSafeInteger(dr("EquipeCodification"))
        '_ipeCodification = Nothing

    End Sub

    Private Sub BlankProperties()

        '_id = 0
        '_Nom = ""
        '_Prenom = ""
        '_Sexe = ""
        '_Cin = ""
        '_Nif = ""
        '_Titre = ""
        '_NomUtilisateur = ""
        '_MotDePasse = ""
        '_Mention = ""
        '_Email = ""
        '_TelDigicel = ""
        '_TelNatcom = ""
        '_Valide = ""
        '_EstAssigne = ""
        '_DerniereDateConnexion = ""
        '_ComNaissance = ""
        '_Naissance = Nothing
        '_LieuNaissance = ""
        '_ProfileId = 0
        '_fileId = Nothing
        '_DeptId = ""
        '_tId = Nothing
        '_ComID = ""
        '_ID = Nothing
        '_VqseId = ""
        '_eId = Nothing
        '_EquipeCodification = 0
        '_ipeCodification = Nothing
        '_isdirty = False

    End Sub

    Public Function Read(ByVal _idpass As Long) As Boolean Implements IGeneral.Read
        Try

            If _idpass <> 0 Then
                Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Selectonnel_ByID", _idpass)

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

    Public Function Read_Email(ByVal Email As String) As Boolean
        Try

            If Email <> "" Then
                Dim ds As Dataset = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Selectonnel_Email", Email)

                If ds.tables(0).Rows.Count < 1 Then
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


    Public Function Read_NomUtilisateur(ByVal NomUtilisateur As String) As Boolean
        Try

            If NomUtilisateur <> "" Then
                Dim ds As Dataset = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Selectonnel_NomUtilisateur", NomUtilisateur)

                If ds.tables(0).Rows.Count < 1 Then
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
            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SP_Deleteonnel", _id)

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

    Public Shared Function SearchAll() As List(Of Cls_Personnel)
        Try
            Dim objs As New List(Of Cls_Personnel)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_onnel")
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_Personnel

                obj.SetProperties(r)

                objs.Add(obj)
            Next r
            Return objs

        Catch ex As Exception

            Throw ex

        End Try

    End Function



    Public Shared Function SearchAllByNaissance(ByVal _comnaissance As String) As List(Of Cls_Personnel)
        Try
            Dim objs As New List(Of Cls_Personnel)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_onnel_ComNaissance", _comnaissance)
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_Personnel

                obj.SetProperties(r)

                objs.Add(obj)
            Next r
            Return objs
        Catch ex As Exception

            Throw ex
        End Try
    End Function

    Public Shared Function SearchAllByfileId(ByVal _profileid As Integer) As List(Of Cls_Personnel)
        Try
            Dim objs As New List(Of Cls_Personnel)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_onnel_ProfileId", _profileid)
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_Personnel

                obj.SetProperties(r)

                objs.Add(obj)
            Next r
            Return objs
        Catch ex As Exception

            Throw ex
        End Try
    End Function

    Public Shared Function SearchAllBytId(ByVal _deptid As String) As List(Of Cls_Personnel)
        Try
            Dim objs As New List(Of Cls_Personnel)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_onnel_DeptId", _deptid)
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_Personnel

                obj.SetProperties(r)

                objs.Add(obj)
            Next r
            Return objs
        Catch ex As Exception

            Throw ex
        End Try
    End Function

    Public Shared Function SearchAllByID(ByVal _comid As String) As List(Of Cls_Personnel)
        Try
            Dim objs As New List(Of Cls_Personnel)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_onnel_ComID", _comid)
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_Personnel

                obj.SetProperties(r)

                objs.Add(obj)
            Next r
            Return objs
        Catch ex As Exception

            Throw ex
        End Try
    End Function

    Public Shared Function SearchAllByeId(ByVal _vqseid As String) As List(Of Cls_Personnel)
        Try
            Dim objs As New List(Of Cls_Personnel)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_onnel_VqseId", _vqseid)
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_Personnel

                obj.SetProperties(r)

                objs.Add(obj)
            Next r
            Return objs
        Catch ex As Exception

            Throw ex
        End Try
    End Function

    Public Shared Function SearchAllByipeCodification(ByVal _equipecodification As Integer) As List(Of Cls_Personnel)
        Try
            Dim objs As New List(Of Cls_Personnel)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_onnel_EquipeCodification", _equipecodification)
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_Personnel

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
    Private Function FoundAlreadyExist_Email(ByVal _value As String) As Boolean
        Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Selectonnel_Email", _value)
        If ds.Tables(0).Rows.Count < 1 Then
            Return False
        Else
            If _id = 0 Then
                Return True
            Else
                If ds.Tables(0).Rows(0).Item("PersId") <> _id Then
                    Return True
                Else
                    Return False
                End If
            End If
        End If
    End Function


    Private Function FoundAlreadyExist_NomUtilisateur(ByVal _value As String) As Boolean
        Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Selectonnel_NomUtilisateur", _value)
        If ds.Tables(0).Rows.Count < 1 Then
            Return False
        Else
            If _id = 0 Then
                Return True
            Else
                If ds.Tables(0).Rows(0).Item("PersId") <> _id Then
                    Return True
                Else
                    Return False
                End If
            End If
        End If
    End Function


    Private Sub Validation()

        'If _Nom = "" Then
        '    Throw (New System.Exception(" nom obligatoire"))
        'End If

        'If Len(_Nom) > 240 Then
        '    Throw (New System.Exception(" Trop de caractères insérés pournom  (la longueur doit être inférieure a 240 caractères.  )"))
        'End If

        'If _Prenom = "" Then
        '    Throw (New System.Exception(" prenom obligatoire"))
        'End If

        'If Len(_Prenom) > 300 Then
        '    Throw (New System.Exception(" Trop de caractères insérés pourprenom  (la longueur doit être inférieure a 300 caractères.  )"))
        'End If

        'If _Sexe = "" Then
        '    Throw (New System.Exception(" sexe obligatoire"))
        'End If

        'If Len(_Sexe) > 2 Then
        '    Throw (New System.Exception(" Trop de caractères insérés poursexe  (la longueur doit être inférieure a 2 caractères.  )"))
        'End If

        'If _Cin = "" Then
        '    Throw (New System.Exception(" cin obligatoire"))
        'End If

        'If Len(_Cin) > 44 Then
        '    Throw (New System.Exception(" Trop de caractères insérés pourcin  (la longueur doit être inférieure a 44 caractères.  )"))
        'End If

        'If _Nif = "" Then
        '    Throw (New System.Exception(" nif obligatoire"))
        'End If

        'If Len(_Nif) > 30 Then
        '    Throw (New System.Exception(" Trop de caractères insérés pournif  (la longueur doit être inférieure a 30 caractères.  )"))
        'End If

        'If _Titre = "" Then
        '    Throw (New System.Exception(" titre obligatoire"))
        'End If

        'If Len(_Titre) > 300 Then
        '    Throw (New System.Exception(" Trop de caractères insérés pourtitre  (la longueur doit être inférieure a 300 caractères.  )"))
        'End If

        'If _NomUtilisateur = "" Then
        '    Throw (New System.Exception(" nomutilisateur obligatoire"))
        'End If

        'If Len(_NomUtilisateur) > 140 Then
        '    Throw (New System.Exception(" Trop de caractères insérés pournomutilisateur  (la longueur doit être inférieure a 140 caractères.  )"))
        'End If

        'If _MotDePasse = "" Then
        '    Throw (New System.Exception(" motdepasse obligatoire"))
        'End If

        'If Len(_MotDePasse) > 510 Then
        '    Throw (New System.Exception(" Trop de caractères insérés pourmotdepasse  (la longueur doit être inférieure a 510 caractères.  )"))
        'End If

        'If _Mention = "" Then
        '    Throw (New System.Exception(" mention obligatoire"))
        'End If

        'If Len(_Mention) > 400 Then
        '    Throw (New System.Exception(" Trop de caractères insérés pourmention  (la longueur doit être inférieure a 400 caractères.  )"))
        'End If

        'If _Email = "" Then
        '    Throw (New System.Exception(" email obligatoire"))
        'End If

        'If Len(_Email) > 400 Then
        '    Throw (New System.Exception(" Trop de caractères insérés pouremail  (la longueur doit être inférieure a 400 caractères.  )"))
        'End If

        'If _TelDigicel = "" Then
        '    Throw (New System.Exception(" teldigicel obligatoire"))
        'End If

        'If Len(_TelDigicel) > 30 Then
        '    Throw (New System.Exception(" Trop de caractères insérés pourteldigicel  (la longueur doit être inférieure a 30 caractères.  )"))
        'End If

        'If _TelNatcom = "" Then
        '    Throw (New System.Exception(" telnatcom obligatoire"))
        'End If

        'If Len(_TelNatcom) > 30 Then
        '    Throw (New System.Exception(" Trop de caractères insérés pourtelnatcom  (la longueur doit être inférieure a 30 caractères.  )"))
        'End If

        'If _Valide = "" Then
        '    Throw (New System.Exception(" valide obligatoire"))
        'End If

        'If Len(_Valide) > 24 Then
        '    Throw (New System.Exception(" Trop de caractères insérés pourvalide  (la longueur doit être inférieure a 24 caractères.  )"))
        'End If

        'If _EstAssigne = "" Then
        '    Throw (New System.Exception(" estassigne obligatoire"))
        'End If

        'If Len(_EstAssigne) > 40 Then
        '    Throw (New System.Exception(" Trop de caractères insérés pourestassigne  (la longueur doit être inférieure a 40 caractères.  )"))
        'End If

        'If _DerniereDateConnexion = "" Then
        '    Throw (New System.Exception(" dernieredateconnexion obligatoire"))
        'End If

        'If Len(_DerniereDateConnexion) > 140 Then
        '    Throw (New System.Exception(" Trop de caractères insérés pourdernieredateconnexion  (la longueur doit être inférieure a 140 caractères.  )"))
        'End If

        'If _ComNaissance = "" Then
        '    Throw (New System.Exception(" comnaissance obligatoire"))
        'End If

        'If Len(_ComNaissance) > 20 Then
        '    Throw (New System.Exception(" Trop de caractères insérés pourcomnaissance  (la longueur doit être inférieure a 20 caractères.  )"))
        'End If

        'If _LieuNaissance = "" Then
        '    Throw (New System.Exception(" lieunaissance obligatoire"))
        'End If

        'If Len(_LieuNaissance) > 140 Then
        '    Throw (New System.Exception(" Trop de caractères insérés pourlieunaissance  (la longueur doit être inférieure a 140 caractères.  )"))
        'End If

        'If _ProfileId = 0 Then
        '    Throw (New System.Exception(" fileid obligatoire"))
        'End If

        'If _DeptId = "" Then
        '    Throw (New System.Exception(" deptid obligatoire"))
        'End If

        'If Len(_DeptId) > 20 Then
        '    Throw (New System.Exception(" Trop de caractères insérés pourdeptid  (la longueur doit être inférieure a 20 caractères.  )"))
        'End If

        'If _ComID = "" Then
        '    Throw (New System.Exception(" comid obligatoire"))
        'End If

        'If Len(_ComID) > 20 Then
        '    Throw (New System.Exception(" Trop de caractères insérés pourcomid  (la longueur doit être inférieure a 20 caractères.  )"))
        'End If

        'If _VqseId = "" Then
        '    Throw (New System.Exception(" vqseid obligatoire"))
        'End If

        'If Len(_VqseId) > 20 Then
        '    Throw (New System.Exception(" Trop de caractères insérés pourvqseid  (la longueur doit être inférieure a 20 caractères.  )"))
        'End If

        'If _EquipeCodification = 0 Then
        '    Throw (New System.Exception(" ipecodification obligatoire"))
        'End If


        'If FoundAlreadyExist_Email(Email) Then
        '    Throw (New System.Exception("Ce Email est déjà enregistré."))
        'End If

        'If FoundAlreadyExist_NomUtilisateur(NomUtilisateur) Then
        '    Throw (New System.Exception("Ce NomUtilisateur est déjà enregistré."))
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
        Dim _old As New Cls_Personnel(Me.ID)

        Return LogStringBuilder.BuildLogStringChangesOnly(_old, Me)
    End Function

#End Region

End Class


