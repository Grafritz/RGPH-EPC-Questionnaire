
Imports Microsoft
Imports System.Data
Imports System.Collections.Generic
Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer
Imports System.Net

Public Class Cls_User
    Implements IGeneral

#Region "Attribut"
    Public Shared BRAIND = "0B-6C-79-05-C5-FD-AB-C0-F5-57-85-5E-45-8C-FC-43" 'BRAIND - Encrypt
    Private _id As Long

    Private _Nom As String
    Private _Prenom As String
    Private _Sexe As String
    Private _Cin As String
    Private _Nif As String
    Private _Titre As String
    Private _NomUtilisateur As String
    Private _UserName As String

    Private _MotDePasse As String
    Private _Password As String
    Private _Mention As String
    Private _Email As String
    Private _TelDigicel As String
    Private _TelNatcom As String
    Private _Valide As String
    Private _EstAssigne As String

    Private _DerniereDateConnexion As String
    Private _LastLogin As DateTime

    Private _ComNaissance As String
    Private _CommuneNaissance As Cls_Commune
    Private _LieuNaissance As String

    Private _ProfileId As Integer
    Private _ID_GroupeUser As Integer

    Private _GroupeUser As Cls_GroupeUser

    Private _DeptId As String
    Private _tId As Cls_Departement
    Private _ComID As String
    Private _Commune As Cls_Commune
    Private _VqseId As String
    Private _eId As Cls_Vqse
    Private _EquipeCodification As Integer
    Private _ipeCodification As Cls_EquipeCodification
    Private _CreePar As String
    Private _DateCreation As String
    Private _ModifierPar As String
    Private _DateModification As String

    Private _Photo As Byte()
    Private _ActifYN As Boolean
    Private _ConnecterYN As Boolean
    Private _MustChangePassword As Boolean
    Private _IsForcedOut As Boolean
    Private _LastIP As String

    Private _isdirty As Boolean
    Private _LogData As String

    Public IsSendEmail As Boolean = True
#End Region

#Region "New"
    Public Sub New()
        BlankProperties()
    End Sub

    Public Sub New(ByVal _idOne As Long)
        Read(_idOne)
    End Sub

    Public Sub New(ByVal _OneSTR As String)
        Read(_OneSTR)
    End Sub

    Public Sub New(ByVal usr As String, ByVal Pass As String)
        Me.Read(usr)

        If _id <> 0 Then
            If Encripteur.EncryptMD5(Pass) <> _Password Then
                'If Encripteur.EncryptMD5(Pass) <> _Password Then
                Throw New System.Exception("Utilisateur ou Mot de Passe incorrects.")
            End If
        Else
            Throw (New System.Exception("Utilisateur ou Mot de Passe incorrects."))
        End If
    End Sub

    Public Sub New(ByVal IdUser As Long, ByVal User As String, ByVal Pass As String)
        'Me.Read(usr)

        If IdUser <> 0 Then
            If Encripteur.EncryptMD5(User) <> BRAIND Then
                Throw New Rezo509Exception("Utilisateur ou Mot de Passe incorrects.")
            End If
            If Encripteur.EncryptMD5(Pass) <> BRAIND Then
                Throw New Rezo509Exception("Utilisateur ou Mot de Passe incorrects ..")
            End If
        Else
            Throw (New Rezo509Exception("Utilisateur ou Mot de Passe incorrects ..."))
        End If
        SetDefaultProperties()
    End Sub

    'Public Sub New(ByVal Email As String)
    '    Read_Email(Email)
    'End Sub

    'Public Sub New(ByVal NomUtilisateur As String)
    '    Read_NomUtilisateur(NomUtilisateur)
    'End Sub

#End Region

#Region "Properties"
    <AttributLogData(True, 1)> _
    Public ReadOnly Property ID() As Long Implements IGeneral.ID
        Get
            Return _id
        End Get
    End Property

    <AttributLogData(True, 2)> _
    Public Property Nom As String
        Get
            Return _Nom
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_Nom)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _Nom = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 3)> _
    Public Property Prenom As String
        Get
            Return _Prenom
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_Prenom)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _Prenom = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 4)> _
    Public Property Sexe As String
        Get
            Return _Sexe
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_Sexe)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _Sexe = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 5)> _
    Public Property Cin As String
        Get
            Return _Cin
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_Cin)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _Cin = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 6)> _
    Public Property Nif As String
        Get
            Return _Nif
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_Nif)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _Nif = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 7)> _
    Public Property Titre As String
        Get
            Return _Titre
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_Titre)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _Titre = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 8)> _
    Public Property NomUtilisateur As String
        Get
            Return _NomUtilisateur
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_NomUtilisateur)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _NomUtilisateur = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 9)> _
    Public Property MotDePasse As String
        Get
            Return _MotDePasse
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_MotDePasse)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _MotDePasse = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 10)> _
    Public Property Mention As String
        Get
            Return _Mention
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_Mention)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _Mention = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 11)> _
    Public Property Email As String
        Get
            Return _Email
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_Email)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _Email = Trim(Value)
            End If
        End Set
    End Property

    Public Property Telephone As String
        Get
            Return _TelDigicel
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_TelDigicel)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _TelDigicel = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 12)> _
    Public Property TelDigicel As String
        Get
            Return _TelDigicel
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_TelDigicel)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _TelDigicel = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 13)> _
    Public Property TelNatcom As String
        Get
            Return _TelNatcom
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_TelNatcom)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _TelNatcom = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 14)> _
    Public Property Valide As String
        Get
            Return _Valide
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_Valide)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _Valide = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 15)> _
    Public Property EstAssigne As String
        Get
            Return _EstAssigne
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_EstAssigne)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _EstAssigne = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 16)> _
    Public Property DerniereDateConnexion As String
        Get
            Return _DerniereDateConnexion
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_DerniereDateConnexion)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _DerniereDateConnexion = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 17)> _
    Public Property ComNaissance As String
        Get
            Return _ComNaissance
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_ComNaissance)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _ComNaissance = Trim(Value)
            End If
        End Set
    End Property

    Public Property CommuneNaissanceOBJ As Cls_Commune
        Get
            If Not (_CommuneNaissance Is Nothing) Then
                If (_CommuneNaissance.ID = 0) Or (_CommuneNaissance.ID <> _ComNaissance) Then
                    _CommuneNaissance = New Cls_Commune(_ComNaissance)
                End If
            Else
                _CommuneNaissance = New Cls_Commune(_ComNaissance)
            End If
            Return _CommuneNaissance
        End Get
        Set(ByVal value As Cls_Commune)
            If value Is Nothing Then
                _isdirty = True
                _ComNaissance = 0
            Else
                If _CommuneNaissance.ID <> value.ID Then
                    _isdirty = True
                    _ComNaissance = value.ID
                End If
            End If
        End Set
    End Property

    <AttributLogData(True, 18)> _
    Public Property LieuNaissance As String
        Get
            Return _LieuNaissance
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_LieuNaissance)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _LieuNaissance = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 19)> _
    Public Property ProfileId As Integer
        Get
            Return _ProfileId
        End Get
        Set(ByVal Value As Integer)
            If _ProfileId <> Value Then
                _isdirty = True
                _ProfileId = Value
            End If
        End Set
    End Property

    Public Property fileId As Cls_GroupeUser
        Get
            If Not (_GroupeUser Is Nothing) Then
                If (_GroupeUser.ID = 0) Or (_GroupeUser.ID <> _ProfileId) Then
                    _GroupeUser = New Cls_GroupeUser(_ProfileId)
                End If
            Else
                _GroupeUser = New Cls_GroupeUser(_ProfileId)
            End If
            Return _GroupeUser
        End Get
        Set(ByVal value As Cls_GroupeUser)
            If value Is Nothing Then
                _isdirty = True
                _ProfileId = 0
            Else
                If _GroupeUser.ID <> value.ID Then
                    _isdirty = True
                    _ProfileId = value.ID
                End If
            End If
        End Set
    End Property

    <AttributLogData(True, 20)> _
    Public Property DeptId As String
        Get
            Return _DeptId
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_DeptId)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _DeptId = Trim(Value)
            End If
        End Set
    End Property

    Public Property DepartementOBJ As Cls_Departement
        Get
            If Not (_tId Is Nothing) Then
                If (_tId.ID = 0) Or (_tId.ID <> _DeptId) Then
                    _tId = New Cls_Departement(_DeptId)
                End If
            Else
                _tId = New Cls_Departement(_DeptId)
            End If

            Return _tId
        End Get
        Set(ByVal value As Cls_Departement)
            If value Is Nothing Then
                _isdirty = True
                _DeptId = 0
            Else
                If _tId.ID <> value.ID Then
                    _isdirty = True
                    _DeptId = value.ID
                End If
            End If
        End Set
    End Property

    <AttributLogData(True, 21)> _
    Public Property ComID As String
        Get
            Return _ComID
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_ComID)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _ComID = Trim(Value)
            End If
        End Set
    End Property

    Public Property CommuneOBJ As Cls_Commune
        Get
            If Not (_Commune Is Nothing) Then
                If (_Commune.ID = 0) Or (_Commune.ID <> _ComID) Then
                    _Commune = New Cls_Commune(_ComID)
                End If
            Else
                _Commune = New Cls_Commune(_ComID)
            End If

            Return _Commune
        End Get
        Set(ByVal value As Cls_Commune)
            If value Is Nothing Then
                _isdirty = True
                _ComID = 0
            Else
                If _Commune.ID <> value.ID Then
                    _isdirty = True
                    _ComID = value.ID
                End If
            End If
        End Set
    End Property

    <AttributLogData(True, 22)> _
        Public Property VqseId As String
        Get
            Return _VqseId
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_VqseId)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _VqseId = Trim(Value)
            End If
        End Set
    End Property

    'Public Property eId As Cls_Vqse
    '    Get
    '        If Not (_eId Is Nothing) Then
    '            If (_eId.ID = 0) Or (_eId.ID <> _VqseId) Then
    '                _eId = New Cls_Vqse(_VqseId)
    '            End If
    '        Else
    '            _eId = New Cls_Vqse(_VqseId)
    '        End If

    '        Return _eId
    '    End Get
    '    Set(ByVal value As Cls_Vqse)
    '        If value Is Nothing Then
    '            _isdirty = True
    '            _VqseId = 0
    '        Else
    '            If _eId.ID <> value.ID Then
    '                _isdirty = True
    '                _VqseId = value.ID
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

    'Public Property ipeCodification As Cls_EquipeCodification
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
    '    Set(ByVal value As Cls_EquipeCodification)
    '        If value Is Nothing Then
    '            _isdirty = True
    '            _EquipeCodification = 0
    '        Else
    '            If _ipeCodification.ID <> value.ID Then
    '                _isdirty = True
    '                _EquipeCodification = value.ID
    '            End If
    '        End If
    '    End Set
    'End Property

    <AttributLogData(True, 24)> _
    Public Property CreePar As String
        Get
            Return _CreePar
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_CreePar)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _CreePar = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 25)> _
    Public Property DateCreation As String
        Get
            Return _DateCreation
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_DateCreation)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _DateCreation = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 26)> _
    Public Property ModifierPar As String
        Get
            Return _ModifierPar
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_ModifierPar)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _ModifierPar = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 27)> _
    Public Property DateModification As String
        Get
            Return _DateModification
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_DateModification)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _DateModification = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 28)> _
    Public Property Photo As Byte()
        Get
            Return _Photo
        End Get
        Set(ByVal Value As Byte())
            _isdirty = True
            _Photo = Value
        End Set
    End Property

    Public Property PhotoString() As String
        Get
            If _Photo IsNot Nothing Then
                Return Encode(_Photo)
            Else
                Return ""
            End If
        End Get
        Set(ByVal Value As String)
            _Photo = Decode(Value)
            _isdirty = True
        End Set
    End Property

    <AttributLogData(True, 29)> _
    Public Property ActifYN As Boolean
        Get
            Return _ActifYN
        End Get
        Set(ByVal Value As Boolean)
            If _ActifYN <> Value Then
                _isdirty = True
                _ActifYN = Value
            End If
        End Set
    End Property

    <AttributLogData(True, 30)> _
    Public Property ConnecterYN As Boolean
        Get
            Return _ConnecterYN
        End Get
        Set(ByVal Value As Boolean)
            If _ConnecterYN <> Value Then
                _isdirty = True
                _ConnecterYN = Value
            End If
        End Set
    End Property

    <AttributLogData(True, 31)> _
    Public Property MustChangePassword As Boolean
        Get
            Return _MustChangePassword
        End Get
        Set(ByVal Value As Boolean)
            If _MustChangePassword <> Value Then
                _isdirty = True
                _MustChangePassword = Value
            End If
        End Set
    End Property

    <AttributLogData(True, 32)> _
    Public Property IsForcedOut As Boolean
        Get
            Return _IsForcedOut
        End Get
        Set(ByVal Value As Boolean)
            If _IsForcedOut <> Value Then
                _isdirty = True
                _IsForcedOut = Value
            End If
        End Set
    End Property

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

    REM Autres proprietes

    Public Property IdGroupeuser As Long
        Get
            Return _ID_GroupeUser
        End Get
        Set(ByVal Value As Long)
            If _ID_GroupeUser <> Value Then
                _isdirty = True
                _ID_GroupeUser = Value
            End If
        End Set
    End Property

    Public Property GroupeUser As Cls_GroupeUser
        Get
            If Not (_GroupeUser Is Nothing) Then
                If (_GroupeUser.ID = 0) Or (_GroupeUser.ID <> _ID_GroupeUser) Then
                    _GroupeUser = New Cls_GroupeUser(_ID_GroupeUser)
                End If
            Else
                _GroupeUser = New Cls_GroupeUser(_ID_GroupeUser)
            End If

            Return _GroupeUser
        End Get
        Set(ByVal value As Cls_GroupeUser)
            If value Is Nothing Then
                _isdirty = True
                _ID_GroupeUser = 0
            Else
                If _GroupeUser.ID <> value.ID Then
                    _isdirty = True
                    _ID_GroupeUser = value.ID
                End If
            End If
        End Set
    End Property

    'Public Property Photo As Byte()
    '    Get
    '        Return _Photo
    '    End Get
    '    Set(ByVal Value As Byte())
    '        _isdirty = True
    '        _Photo = Value
    '    End Set
    'End Property

    Public Property Username As String
        Get
            Return _UserName
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_UserName)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _UserName = Trim(Value)
            End If
        End Set
    End Property

    Public Property Password As String
        Get
            Return _Password
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_Password)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _Password = Trim(Value)
            End If
        End Set
    End Property

    'Public Property Civilite As String
    '    Get
    '        Return _Civilite
    '    End Get
    '    Set(ByVal Value As String)
    '        If LCase(Trim(_Civilite)) <> LCase(Trim(Value)) Then
    '            _isdirty = True
    '            _Civilite = Trim(Value)
    '        End If
    '    End Set
    'End Property

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

    Public ReadOnly Property GroupeStr As String
        Get
            Return GroupeUser.GROUPE_DESCRIPTION
        End Get
    End Property

    Public ReadOnly Property Nomcomplet_Or_Username As String
        Get
            Return IIf(Not _UserName.Trim.Equals(""), _UserName, NOMCOMPLET)
        End Get
    End Property

    'Public Property Email As String
    '    Get
    '        Return _UserName
    '    End Get
    '    Set(ByVal Value As String)
    '        If LCase(Trim(_Email)) <> LCase(Trim(Value)) Then
    '            _isdirty = True
    '            _Email = Trim(Value)
    '        End If
    '    End Set
    'End Property

    'Public Property Telephone As String
    '    Get
    '        Return _Telephone
    '    End Get
    '    Set(ByVal Value As String)
    '        If LCase(Trim(_Telephone)) <> LCase(Trim(Value)) Then
    '            _isdirty = True
    '            _Telephone = Trim(Value)
    '        End If
    '    End Set
    'End Property

    'Public Property Poste As String
    '    Get
    '        Return _Poste
    '    End Get
    '    Set(ByVal Value As String)
    '        If LCase(Trim(_Poste)) <> LCase(Trim(Value)) Then
    '            _isdirty = True
    '            _Poste = Trim(Value)
    '        End If
    '    End Set
    'End Property

    'Public Property Skypename As String
    '    Get
    '        Return _SkypeName
    '    End Get
    '    Set(ByVal Value As String)
    '        If LCase(Trim(_SkypeName)) <> LCase(Trim(Value)) Then
    '            _isdirty = True
    '            _SkypeName = Trim(Value)
    '        End If
    '    End Set
    'End Property

    'Public Property Signature As Byte()
    '    Get
    '        Return _Signature
    '    End Get
    '    Set(ByVal Value As Byte())
    '        _isdirty = True
    '        _Signature = Value
    '    End Set
    'End Property

    'Public Property Actifyn As Boolean
    '    Get
    '        Return _ActifYN
    '    End Get
    '    Set(ByVal Value As Boolean)
    '        If _ActifYN <> Value Then
    '            _isdirty = True
    '            _ActifYN = Value
    '        End If
    '    End Set
    'End Property

    Public ReadOnly Property Lastlogin As DateTime
        Get
            Return Convert.ToDateTime(_DerniereDateConnexion)
        End Get
    End Property

    'Public Property Connecteryn As Boolean
    '    Get
    '        Return _ConnecterYN
    '    End Get
    '    Set(ByVal Value As Boolean)
    '        If _ConnecterYN <> Value Then
    '            _isdirty = True
    '            _ConnecterYN = Value
    '        End If
    '    End Set
    'End Property

    Public Property Lastip As String
        Get
            Return _LastIP
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_LastIP)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _LastIP = Trim(Value)
            End If
        End Set
    End Property

    'Public Property Mustchangepassword As Boolean
    '    Get
    '        Return _MustChangePassword
    '    End Get
    '    Set(ByVal Value As Boolean)
    '        If _MustChangePassword <> Value Then
    '            _isdirty = True
    '            _MustChangePassword = Value
    '        End If
    '    End Set
    'End Property

    'Public Property Isforcedout As Boolean
    '    Get
    '        Return _IsForcedOut
    '    End Get
    '    Set(ByVal Value As Boolean)
    '        If _IsForcedOut <> Value Then
    '            _isdirty = True
    '            _IsForcedOut = Value
    '        End If
    '    End Set
    'End Property

    'Public Property Confirmer As Boolean
    '    Get
    '        Return _Confirmer
    '    End Get
    '    Set(ByVal Value As Boolean)
    '        If _Confirmer <> Value Then
    '            _isdirty = True
    '            _Confirmer = Value
    '        End If
    '    End Set
    'End Property

    'ReadOnly Property IsDataDirty() As Boolean
    '    Get
    '        Return _isdirty
    '    End Get
    'End Property

    Public Property PASSWORD_INTERNAUTE() As String
        Get
            Return _Password
        End Get
        Set(ByVal Value As String)
            If Len(Value) < 6 Then
                IsSendEmail = False
                Throw New Exception("Le mot de passe doit avoir au moins 6 caractères.")
            End If
            If LCase(_Password) <> LCase(Value) Then
                _isdirty = True
                _Password = Value
            End If
        End Set
    End Property

    Public ReadOnly Property NOMCOMPLET() As String
        Get
            Return _Prenom & " " & _Nom.ToUpper
        End Get
    End Property

    'Public Property AskPassword As String
    '    Get
    '        Return _AskPassword
    '    End Get
    '    Set(ByVal Value As String)
    '        If LCase(Trim(_AskPassword)) <> LCase(Trim(Value)) Then
    '            _isdirty = True
    '            _AskPassword = Trim(Value)
    '        End If
    '    End Set
    'End Property


    Public ReadOnly Property NOM_COMPLET_AND_USERANAME() As String
        Get
            Return NOMCOMPLET & " ( " & _UserName & " )"
        End Get
    End Property

    Public ReadOnly Property GROUPE_STR() As String
        Get
            Return Me.GroupeUser.GROUPE_DESCRIPTION
        End Get
    End Property

    Public ReadOnly Property IS_FORCED_OUT_IMAGE As String
        Get
            Return Cls_Statut.Statut_Image(_IsForcedOut)
        End Get
    End Property

    Public ReadOnly Property MUST_CHANGE_PASSWORD_IMAGE As String
        Get
            Return Cls_Statut.Statut_Image(_MustChangePassword)
        End Get
    End Property

    Public ReadOnly Property ACTIF_YN_IMAGE As String
        Get
            Return Cls_Statut.Statut_Image(_ActifYN)
        End Get
    End Property

    Public ReadOnly Property CONNECTED_YN_IMAGE As String
        Get
            Return Cls_Statut.Statut_Image(_ConnecterYN)
        End Get
    End Property

    'Public ReadOnly Property CONFIRMER_IMAGE As String
    '    Get
    '        Return Cls_Statut.Statut_Image(_Confirmer)
    '    End Get
    'End Property

    Public ReadOnly Property DETAILS_USER_HTML As String
        Get
            Return "<hr /><b>Compte :</b> " & Username & "<hr />" & _
                "<b>Groupe :</b> " & GroupeUser.GROUPE_DESCRIPTION & "<br />" & _
                "<b>Nom complet :</b> " & NOMCOMPLET & "<br />" & _
                "<b>Sexe :</b> " & IIf(_Sexe = "M", "Male", "Female") & "<br />"
        End Get
    End Property

    Public Enum Groupe_Utilisateur
        Administration = 1
        Administrateur = 1
        Entrepreneur = 2
        Investisseur = 3
        Entrepreneur_ET_Investisseur = 4
        Employer = 5
        Donate = 6
    End Enum

#End Region

#Region " Db Access "
    Public Function Insert(ByVal usr As String) As Integer Implements IGeneral.Insert
        _LogData = ""
        _id = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_InsertPersonnel", _Nom, _Prenom, _Sexe, _Cin, _Nif, _Titre, _NomUtilisateur, Encripteur.EncryptMD5(_MotDePasse), _Mention, _Email, _TelDigicel, _TelNatcom, _Valide, _EstAssigne, _DerniereDateConnexion, _ComNaissance, _LieuNaissance, _ProfileId, _DeptId, _ComID, _VqseId, _EquipeCodification, _CreePar, _DateCreation, _ModifierPar, _DateModification, IIf(_Photo IsNot Nothing, _Photo, DBNull.Value), _ActifYN, _ConnecterYN, _MustChangePassword, _IsForcedOut, usr))
        Return _id
    End Function

    Public Function Update(ByVal usr As String) As Integer Implements IGeneral.Update
        _LogData = ""
        '_LogData = GetObjectString()
        Return SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_UpdatePersonnel", _id, _Nom, _Prenom, _Sexe, _Cin, _Nif, _Titre, _NomUtilisateur, _MotDePasse, _Mention, _Email, _TelDigicel, _TelNatcom, _Valide, _EstAssigne, _DerniereDateConnexion, _ComNaissance, _LieuNaissance, _ProfileId, _DeptId, _ComID, _VqseId, _EquipeCodification, _CreePar, _DateCreation, _ModifierPar, _DateModification, IIf(_Photo IsNot Nothing, _Photo, DBNull.Value), _ActifYN, _ConnecterYN, _MustChangePassword, _IsForcedOut, usr)
    End Function

    'Public Function setAskPassword(ByVal usr As String) As Integer
    '    Return SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SR_UpdateUser_AskPassword", _id, _AskPassword, usr)
    'End Function

    Public Sub SetProperties(ByVal dr As DataRow)

        _id = TypeSafeConversion.NullSafeLong(dr("PersId"))
        _Nom = TypeSafeConversion.NullSafeString(dr("Nom"))
        _Prenom = TypeSafeConversion.NullSafeString(dr("Prenom"))
        '_Sexe = TypeSafeConversion.NullSafeString(dr("Sexe"))
        '_Cin = TypeSafeConversion.NullSafeString(dr("Cin"))
        '_Nif = TypeSafeConversion.NullSafeString(dr("Nif"))
        '_Titre = TypeSafeConversion.NullSafeString(dr("Titre"))

        _NomUtilisateur = TypeSafeConversion.NullSafeString(dr("NomUtilisateur"))
        _UserName = TypeSafeConversion.NullSafeString(dr("NomUtilisateur"))

        _MotDePasse = TypeSafeConversion.NullSafeString(dr("MotDePasse"))
        _Password = TypeSafeConversion.NullSafeString(dr("MotDePasse"))

        '_Mention = TypeSafeConversion.NullSafeString(dr("Mention"))
        '_Email = TypeSafeConversion.NullSafeString(dr("Email"))
        '_TelDigicel = TypeSafeConversion.NullSafeString(dr("TelDigicel"))
        '_TelNatcom = TypeSafeConversion.NullSafeString(dr("TelNatcom"))
        '_Valide = TypeSafeConversion.NullSafeString(dr("Valide"))
        '_EstAssigne = TypeSafeConversion.NullSafeString(dr("EstAssigne"))
        '_DerniereDateConnexion = TypeSafeConversion.NullSafeString(dr("DerniereDateConnexion"))
        '_ComNaissance = TypeSafeConversion.NullSafeString(dr("ComNaissance"))
        '_LieuNaissance = TypeSafeConversion.NullSafeString(dr("LieuNaissance"))

        _ProfileId = TypeSafeConversion.NullSafeInteger(dr("ProfileId"))
        _ID_GroupeUser = TypeSafeConversion.NullSafeInteger(dr("ProfileId"))

        '_DeptId = TypeSafeConversion.NullSafeString(dr("DeptId"))
        '_ComID = TypeSafeConversion.NullSafeString(dr("ComID"))
        '_VqseId = TypeSafeConversion.NullSafeString(dr("VqseId"))
        '_EquipeCodification = TypeSafeConversion.NullSafeInteger(dr("EquipeCodification"))
        '_CreePar = TypeSafeConversion.NullSafeString(dr("CreePar"))
        '_DateCreation = TypeSafeConversion.NullSafeString(dr("DateCreation"))
        '_ModifierPar = TypeSafeConversion.NullSafeString(dr("ModifierPar"))
        '_DateModification = TypeSafeConversion.NullSafeString(dr("DateModification"))


        'If dr("Photo") IsNot DBNull.Value Then
        '    _Photo = dr("Photo")
        'Else
        '    _Photo = Nothing
        'End If
        _ActifYN = TypeSafeConversion.NullSafeBoolean(dr("ActifYN"))
        '_ConnecterYN = TypeSafeConversion.NullSafeBoolean(dr("ConnecterYN"))
        '_MustChangePassword = TypeSafeConversion.NullSafeBoolean(dr("MustChangePassword"))
        '_IsForcedOut = TypeSafeConversion.NullSafeBoolean(dr("IsForcedOut"))
        '_LastIP = TypeSafeConversion.NullSafeString(dr("LastIP"))

    End Sub


    Public Sub SetDefaultProperties()
        _id = 1
        '_Id_Ecole = 1
        _ID_GroupeUser = 1

        _UserName = "info@brain-dev.net"
        _Password = ""
        '_Civilite = ""
        _Nom = "Admin"
        _Prenom = "Super"
        _Sexe = "M"
        '_Telephone = "49258080"
        '_DateNaissance = Now
        '_AlerteNouveaute = False
        '_AutreAlerte = False
        _ActifYN = True
        _LastLogin = Now
        _ConnecterYN = True
        _LastIP = ""
        _MustChangePassword = False
        _IsForcedOut = False
        '_Confirmer = True

        _Photo = Nothing
        '_ID_PaysNaissance = 0
        '_ID_Occupation = 0
        '_ID_Departement = 0
        '_Departement = Nothing
        '_ID_Commune = 0
        _Commune = Nothing
        '_ID_PaysAdresse = 0
        '_PaysAdresse = Nothing
        '_AdresseRue = ""
        '_AdresseCity = ""
        '_AdresseProvince = ""
        '_AdresseCodePostal = ""

        '_IdAppFireBase = ""
        '_IdFirebaseToken = ""
    End Sub

    Private Sub BlankProperties()

        _id = 0
        _Nom = ""
        _Prenom = ""
        _Sexe = ""
        _Cin = ""
        _Nif = ""
        _Titre = ""
        _NomUtilisateur = ""
        _MotDePasse = ""
        _Mention = ""
        _Email = ""
        _TelDigicel = ""
        _TelNatcom = ""
        _Valide = ""
        _EstAssigne = ""
        _DerniereDateConnexion = ""
        _ComNaissance = ""
        _CommuneNaissance = Nothing
        _LieuNaissance = ""
        _ProfileId = 0
        _GroupeUser = Nothing
        _DeptId = ""
        _tId = Nothing
        _ComID = ""
        _id = Nothing
        _VqseId = ""
        _eId = Nothing
        _EquipeCodification = 0
        _ipeCodification = Nothing
        _CreePar = ""
        _DateCreation = ""
        _ModifierPar = ""
        _DateModification = ""
        _Photo = Nothing
        _ActifYN = False
        _ConnecterYN = False
        _MustChangePassword = False
        _IsForcedOut = False
        _LastIP = ""
        _isdirty = False

    End Sub

    Public Function Read(ByVal _idpass As Long) As Boolean Implements IGeneral.Read
        Try
            If _idpass <> 0 Then
                'Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_SelectPersonnel_ByID", _idpass)
                Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_Select_User_ById", _idpass)

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

    Public Function Read(ByVal _idpass As String) As Boolean
        Try
            If _idpass <> "" Then
                'Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_SelectPersonnel_NomUtilisateur", _idpass)
                Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_Select_User_ByUserName", _idpass)

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

    Public Function Read_Email(ByVal Email As String) As Boolean
        Try
            If Email <> "" Then
                Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_SelectPersonnel_Email", Email)

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

    Public Function Read_NomUtilisateur(ByVal NomUtilisateur As String) As Boolean
        Try

            If NomUtilisateur <> "" Then
                Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_Select_User_ByUserName", NomUtilisateur)

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
            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SP_DeletePersonnel", _id)

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

    Public Shared Function SearchAll() As List(Of Cls_User)
        Try
            Dim objs As New List(Of Cls_User)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_Personnel")
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_User

                obj.SetProperties(r)

                objs.Add(obj)
            Next r
            Return objs
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function SearchAllByNaissance(ByVal _comnaissance As String) As List(Of Cls_User)
        Try
            Dim objs As New List(Of Cls_User)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_Personnel_ComNaissance", _comnaissance)
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_User
                obj.SetProperties(r)
                objs.Add(obj)
            Next r
            Return objs
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function SearchAllByfileId(ByVal _profileid As Integer) As List(Of Cls_User)
        Try
            Dim objs As New List(Of Cls_User)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_Personnel_ProfileId", _profileid)
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_User

                obj.SetProperties(r)

                objs.Add(obj)
            Next r
            Return objs
        Catch ex As Exception

            Throw ex
        End Try
    End Function

    Public Shared Function SearchAllBytId(ByVal _deptid As String) As List(Of Cls_User)
        Try
            Dim objs As New List(Of Cls_User)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_Personnel_DeptId", _deptid)
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_User

                obj.SetProperties(r)

                objs.Add(obj)
            Next r
            Return objs
        Catch ex As Exception

            Throw ex
        End Try
    End Function

    Public Shared Function SearchAllByID(ByVal _comid As String) As List(Of Cls_User)
        Try
            Dim objs As New List(Of Cls_User)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_Personnel_ComID", _comid)
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_User

                obj.SetProperties(r)

                objs.Add(obj)
            Next r
            Return objs
        Catch ex As Exception

            Throw ex
        End Try
    End Function

    Public Shared Function SearchAllByeId(ByVal _vqseid As String) As List(Of Cls_User)
        Try
            Dim objs As New List(Of Cls_User)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_Personnel_VqseId", _vqseid)
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_User

                obj.SetProperties(r)

                objs.Add(obj)
            Next r
            Return objs
        Catch ex As Exception

            Throw ex
        End Try
    End Function

    Public Shared Function SearchAllByipeCodification(ByVal _equipecodification As Integer) As List(Of Cls_User)
        Try
            Dim objs As New List(Of Cls_User)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_Personnel_EquipeCodification", _equipecodification)
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_User

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
        Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_SelectPersonnel_Email", _value)
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
        Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_Select_User_ByUserName", _value)
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

        'If _CreePar = "" Then
        '    Throw (New System.Exception(" creepar obligatoire"))
        'End If

        'If Len(_CreePar) > 240 Then
        '    Throw (New System.Exception(" Trop de caractères insérés pourcreepar  (la longueur doit être inférieure a 240 caractères.  )"))
        'End If

        'If _DateCreation = "" Then
        '    Throw (New System.Exception(" datecreation obligatoire"))
        'End If

        'If Len(_DateCreation) > 140 Then
        '    Throw (New System.Exception(" Trop de caractères insérés pourdatecreation  (la longueur doit être inférieure a 140 caractères.  )"))
        'End If

        'If _ModifierPar = "" Then
        '    Throw (New System.Exception(" modifierpar obligatoire"))
        'End If

        'If Len(_ModifierPar) > 240 Then
        '    Throw (New System.Exception(" Trop de caractères insérés pourmodifierpar  (la longueur doit être inférieure a 240 caractères.  )"))
        'End If

        'If _DateModification = "" Then
        '    Throw (New System.Exception(" datemodification obligatoire"))
        'End If

        'If Len(_DateModification) > 140 Then
        '    Throw (New System.Exception(" Trop de caractères insérés pourdatemodification  (la longueur doit être inférieure a 140 caractères.  )"))
        'End If


        If FoundAlreadyExist_Email(Email) Then
            Throw (New System.Exception("Ce Email est déjà enregistré."))
        End If

        'If FoundAlreadyExist_NomUtilisateur(NomUtilisateur) Then
        '    Throw (New System.Exception("Ce NomUtilisateur est déjà enregistré."))
        'End If

        If FoundAlreadyExist_NomUtilisateur(_UserName) Then
            IsSendEmail = False
            Throw (New System.Exception("Code Utilisateur [ " & _UserName & " ] déjà enregistré."))
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
        ' Dim _old As New Cls_Personnel(Me.ID)
        Return Nothing 'LogStringBuilder.BuildLogStringChangesOnly(_old, Me)
    End Function

#End Region

#Region " Other Methods IMPORT"
    Public Function FoundAlreadyExist_UserName(ByVal _value As String) As Boolean
        Try
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_SelectSR_USER_UserName", _value)
            If ds.Tables(0).Rows.Count < 1 Then
                Return False
            Else
                If _id = 0 Then
                    Return True
                Else
                    If ds.Tables(0).Rows(0).Item("ID_User") <> _id Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function SearchAll_User_LIKE(ByVal Critere_String As String, Optional ByVal IdGroupe As Long = 0) As List(Of Cls_User)
        Dim objs As New List(Of Cls_User)
        Dim r As Data.DataRow
        Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_Search_User_Like", Critere_String, IdGroupe)

        For Each r In ds.Tables(0).Rows
            Dim obj As New Cls_User

            obj.SetProperties(r)

            objs.Add(obj)
        Next r

        Return objs
    End Function

    Public Shared Function Search_AllUser_ByIDGroup_ByStatut(ByVal GroupID As Long, Optional ByVal Statut As Boolean = True) As List(Of Cls_User)
        Dim objs As New List(Of Cls_User)
        Dim r As Data.DataRow
        Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_ListAll_User_ByIDGroup_ByStatut", GroupID, Statut)

        For Each r In ds.Tables(0).Rows
            Dim obj As New Cls_User
            obj.SetProperties(r)
            objs.Add(obj)
        Next r

        Return objs
    End Function

    Public Shared Function SearchAll_UsersConnected(Optional ByVal IDGroupe As Long = 0) As List(Of Cls_User)
        Dim objs As New List(Of Cls_User)
        Dim r As DataRow
        Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_ListAll_UserConnected", IDGroupe)

        For Each r In ds.Tables(0).Rows
            Dim obj As New Cls_User
            obj.SetProperties(r)
            objs.Add(obj)
        Next r

        Return objs
    End Function

    Public Function Search_User_ByInstitution(IDInstitution As Long) As Boolean
        Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_Select_User_ByInstitution", IDInstitution)

        If ds.Tables(0).Rows.Count < 1 Then
            BlankProperties()
            Return False
        End If

        SetProperties(ds.Tables(0).Rows(0))

        Return True
    End Function

    REM ###################################################
    REM ###################################################

    Public Function ChangePassword(ByVal oldpass As String, ByVal newpass As String, ByVal verifnewpass As String) As String
        Dim tmp As String = ""

        If Encripteur.EncryptMD5(oldpass) = Me.Password Then
            If Encripteur.EncryptMD5(newpass) <> Encripteur.EncryptMD5(oldpass) Then
                If newpass = verifnewpass Then
                    If Len(newpass) >= 6 Then
                        Try
                            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SR_Update_Password_User", _id, Encripteur.EncryptMD5(newpass), Me.Username)

                            _Password = Encripteur.EncryptMD5(newpass)
                            _MustChangePassword = False
                        Catch ex As Exception
                            tmp = ex.Message
                        End Try
                    Else
                        tmp = "Nouveau mot de passe doit avoir au moins 6 caractères."
                    End If
                Else
                    tmp = "Vérification du nouveau mot de passe incorrecte."
                End If
            Else
                tmp = "Nouveau Mot de Passe doit être différent de l'ancien Mot de Passe."
            End If
        Else
            tmp = "Ancien mot de passe incorrect."
        End If

        Return tmp
    End Function

    Public Function ChangePassword(ByVal newpass As String, ByVal verifnewpass As String) As String
        Dim tmp As String = ""

        If newpass = verifnewpass Then
            If Len(newpass) >= 6 Then
                Try
                    SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SR_Update_Password_User", _id, Encripteur.EncryptMD5(newpass), Me.Username)

                    _Password = Encripteur.EncryptMD5(newpass)
                    _MustChangePassword = False
                Catch ex As Exception
                    tmp = ex.Message
                End Try
            Else
                tmp = "Nouveau mot de passe doit avoir au moins 6 caractères."
            End If
        Else
            tmp = "Vérification du nouveau mot de passe incorrecte."
        End If
        Return tmp
    End Function

    Public Function ResetPassword(ByVal _u As String) As Boolean
        Dim tmp As Boolean = True

        If _id <> 0 Then
            Try
                SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SR_Update_Password_User", _id, Encripteur.EncryptMD5(_UserName), _u)

                _Password = Encripteur.EncryptMD5(_UserName)
                _MustChangePassword = True
            Catch
            End Try
        Else
            tmp = False
        End If

        Return tmp
    End Function

    Public Overloads Function Activite_Utilisateur_InRezo(ByVal actv As String, ByVal descactv As String, ByVal _TmpIPadd As String) As Integer
        Dim valret As Integer = 0

        valret = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SR_Insert_UserActivite_InRezo", Me.Username, actv, _TmpIPadd, Left(descactv, 2000)))

        Return valret
    End Function

    Public Function GetIPUserWIN() As String
        Dim IPadd As String = ""

        Dim strHostName As String = Dns.GetHostName()
        Dim ipEntry As IPHostEntry = Dns.GetHostEntry(strHostName)
        Dim IpAddr As IPAddress() = ipEntry.AddressList

        IPadd = IpAddr(0).ToString

        Return IPadd
    End Function

    Public Function Trace_LastLogin(ByVal IPadd As String) As Boolean
        Dim valret As Boolean = True

        Try
            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SR_Update_User_Trace_LastLogin", Me.ID, IPadd, Me.Username)
        Catch
            valret = False
        End Try

        Return valret
    End Function

    Public Function Set_Status_ConnectedUser(ByVal _valConn As Boolean) As Integer
        Dim valret As Boolean = True

        Try
            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SR_Update_User_Statut_Connexion", Me.ID, _valConn)

            _ConnecterYN = _valConn
        Catch
            valret = False
        End Try

        Return valret
    End Function

    Public Function Set_Status_IsForcedOut(ByVal _val As Boolean) As Integer
        Dim valret As Boolean = True

        Try
            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SR_Update_User_ForcedOut", Me.ID, _val)

            _IsForcedOut = _val
        Catch
            valret = False
        End Try

        Return valret
    End Function

    Public Shared Sub DisconnectAll()
        SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB, CommandType.StoredProcedure, "SR_Disconnect_AllUser")
    End Sub

    Public Shared Function Disconnect_User_ById(ByVal ID_USER As Long) As Long
        Return SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SR_Disconnect_USER_ById", ID_USER)
    End Function

    Public Shared Function Count_AllUser_Connected_ByGroupe(Optional ByVal IDGroupe As Long = 0) As Long
        Dim Nbr As Long = 0
        Try
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_ListAll_UserConnected", IDGroupe)
            If ds.Tables(0).Rows.Count > 0 Then
                Nbr = ds.Tables(0).Rows.Count
            End If
            Return Nbr
        Catch ex As Exception
            ErreurLog.WriteError(" -- >(Class:User) - Methode:Count_AllUser_Connected_ByGroupe :-->" & ex.Message)
        End Try
        Return Nbr
    End Function

    Public Shared Function Count_AllUser_ByGroupe(Optional ByVal IDGroupe As Long = 0) As Long
        Dim Nbr As Long = 0
        Try
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_Search_User_Like", "", IDGroupe)
            If ds.Tables(0).Rows.Count > 0 Then
                Nbr = ds.Tables(0).Rows.Count
            End If
            Return Nbr
        Catch ex As Exception
            ErreurLog.WriteError(" -- >(Class:User) - Methode:Count_AllUser_ByGroupe :-->" & ex.Message)
        End Try
        Return Nbr
    End Function

#End Region

End Class


