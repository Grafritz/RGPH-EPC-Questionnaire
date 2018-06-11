REM Generate By [GENERIC 12] Application *******
REM  Class Cls_SR_USER

REM Date:15-Mar-2013 7h:i:53m
Imports Microsoft
Imports System.Data
Imports System.Collections.Generic
Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer
Imports System.Net

Public Class Cls_User
    Implements IGeneral

#Region "Attribut"
    Public Shared SYGASNA = "0B-6C-79-05-C5-FD-AB-C0-F5-57-85-5E-45-8C-FC-43" 'BRAIND - Encrypt
    Private _id As Long

    Private _Id_Ecole As Long
    'Private _Ecole As Cls_Ecole
    Private _ID_GroupeUser As Long
    Private _GroupeUser As Cls_GroupeUser
    Private _Photo As Byte()
    Private _UserName As String
    Private _Password As String
    Private _Civilite As String
    Private _Nom As String
    Private _Prenom As String
    Private _Sexe As String
    Private _Telephone As String
    Private _DateNaissance As Nullable(Of Date)
    Private _ID_PaysNaissance As Long
    Private _ID_Occupation As Long
    'Private _Occupation As Cls_Occupation
    Private _ID_Departement As Long
    Private _Departement As Cls_Departement
    Private _ID_Commune As Long
    Private _Commune As Cls_Commune
    Private _ID_PaysAdresse As Long
    Private _PaysAdresse As Cls_Pays
    Private _AdresseRue As String
    Private _AdresseCity As String
    Private _AdresseProvince As String
    Private _AdresseCodePostal As String
    Private _AlerteNouveaute As Boolean
    Private _AutreAlerte As Boolean
    Private _ActifYN As Boolean
    Private _LastLogin As DateTime
    Private _ConnecterYN As Boolean
    Private _LastIP As String
    Private _MustChangePassword As Boolean
    Private _IsForcedOut As Boolean
    Private _Confirmer As Boolean
    Private _isdirty As Boolean
    Public _LogData As String


    'Public IsSendEmail As Boolean = True

    Private _CreatedBy As String
    Private _DateCreated As DateTime


#Region "FIREBASE 24-11-2017 | 04:33 PM"
    Private _IdAppFireBase As String
    Private _IdFirebaseToken As String
#End Region
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
            If Encripteur.Encrypt(Pass) <> _Password Then
                Throw New Rezo509Exception("Utilisateur ou Mot de Passe incorrects.")
            End If
        Else
            Throw (New Rezo509Exception("Utilisateur ou Mot de Passe incorrects."))
        End If
    End Sub

    Public Sub New(ByVal IdUser As Long, ByVal User As String, ByVal Pass As String)
        'Me.Read(usr)

        If IdUser <> 0 Then
            If Encripteur.Encrypt(User) <> SYGASNA Then
                Throw New Rezo509Exception("Utilisateur ou Mot de Passe incorrects.")
            End If
            If Encripteur.Encrypt(Pass) <> SYGASNA Then
                Throw New Rezo509Exception("Utilisateur ou Mot de Passe incorrects ..")
            End If
        Else
            Throw (New Rezo509Exception("Utilisateur ou Mot de Passe incorrects ..."))
        End If
        SetDefaultProperties()
    End Sub

#End Region

#Region "Properties"

    <AttributLogData(True, 1)> _
    Public ReadOnly Property ID() As Long Implements IGeneral.ID
        Get
            Return _id
        End Get
    End Property

    Public Property IdUser As Long
        Get
            Return _id
        End Get
        Set(ByVal Value As Long)
            If _id <> Value Then
                _id = Value
            End If
        End Set
    End Property

    Public Property ID_Ecole As Long
        Get
            Return _Id_Ecole
        End Get
        Set(ByVal Value As Long)
            If _Id_Ecole <> Value Then
                _isdirty = True
                _Id_Ecole = Value
            End If
        End Set
    End Property

    'Public Property Ecole As Cls_Ecole
    '    Get
    '        If Not (_Ecole Is Nothing) Then
    '            If (_Ecole.ID = 0) Or (_Ecole.ID <> _Id_Ecole) Then
    '                _Ecole = New Cls_Ecole(_Id_Ecole)
    '            End If
    '        Else
    '            _Ecole = New Cls_Ecole(_Id_Ecole)
    '        End If
    '        Return _Ecole
    '    End Get
    '    Set(ByVal value As Cls_Ecole)
    '        If Value Is Nothing Then
    '            _isdirty = True
    '            _Id_Ecole = 0
    '        Else
    '            If _Ecole.ID <> Value.ID Then
    '                _isdirty = True
    '                _Id_Ecole = Value.ID
    '            End If
    '        End If
    '    End Set
    'End Property

    <AttributLogData(True, 2)> _
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


    Public Property ID_Group As Long
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

    Public Property Photo As Byte()
        Get
            Return _Photo
        End Get
        Set(ByVal Value As Byte())
            _isdirty = True
            _Photo = Value
        End Set
    End Property


    <AttributLogData(True, 3)> _
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

    Public ReadOnly Property Email As String
        Get
            Return _UserName.ToLower
        End Get
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

    Public Property Civilite As String
        Get
            Return _Civilite
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_Civilite)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _Civilite = Trim(Value)
            End If
        End Set
    End Property

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

    Public ReadOnly Property SexeIcon_FontAwesome As String
        Get
            Dim sexeval As String = "<i class=""fa fa-female""></i> "
            If _Sexe.Equals("Masculin") OrElse _Sexe.Equals("M") Then
                sexeval = "<i class=""fa fa-male""></i> "
            End If
            Return sexeval
        End Get
    End Property

    Public ReadOnly Property Email_Adresse_NomComplet_Telephone As String
        Get
            Return Username & "" & NOMCOMPLET & "" & _AdresseRue & "" & Telephone
        End Get
    End Property

    Public ReadOnly Property GroupeStr As String
        Get
            Return GroupeUser.GROUPE_DESCRIPTION
        End Get
    End Property

    Public ReadOnly Property Civilite_Nom As String
        Get
            Return _Civilite & " " & _Nom
        End Get
    End Property

    Public ReadOnly Property Civilite_Prenom As String
        Get
            Return _Civilite & " " & _Prenom
        End Get
    End Property

    Public ReadOnly Property Civilite_Nomcomplet_Or_Username As String
        Get
            Return _Civilite & " " & IIf(NOMCOMPLET.Trim.Equals(""), _UserName, NOMCOMPLET)
        End Get
    End Property

    Public ReadOnly Property Civilite_Nomcomplet_Et_Username As String
        Get
            Return _Civilite & " " & NOM_COMPLET_AND_USERANAME
        End Get
    End Property

    Public ReadOnly Property Nomcomplet_Or_Username As String
        Get
            Return IIf(Not NOMCOMPLET.Trim.Equals(""), NOMCOMPLET, _UserName)
        End Get
    End Property

    Public ReadOnly Property Nomcomplet_et_Username As String
        Get
            Return NOMCOMPLET & "<br />" & _UserName
        End Get
    End Property
    Public Property Telephone As String
        Get
            Return _Telephone
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_Telephone)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _Telephone = Trim(Value)
            End If
        End Set
    End Property

    Public Property Datenaissance As Nullable(Of Date)
        Get
            Return _DateNaissance
        End Get
        Set(ByVal Value As Nullable(Of Date))
            If Value.HasValue AndAlso _DateNaissance.HasValue AndAlso Value.Value <> _DateNaissance.Value Then
                _isdirty = True
                _DateNaissance = Value
            ElseIf (Not Value.HasValue AndAlso _DateNaissance.HasValue) OrElse (Value.HasValue AndAlso Not _DateNaissance.HasValue) Then
                _isdirty = True
                _DateNaissance = Value
            End If
        End Set
    End Property

    Public Property IdPaysnaissance As Long
        Get
            Return _ID_PaysNaissance
        End Get
        Set(ByVal Value As Long)
            If _ID_PaysNaissance <> Value Then
                _isdirty = True
                _ID_PaysNaissance = Value
            End If
        End Set
    End Property

    Public Property IdOccupation As Long
        Get
            Return _ID_Occupation
        End Get
        Set(ByVal Value As Long)
            If _ID_Occupation <> Value Then
                _isdirty = True
                _ID_Occupation = Value
            End If
        End Set
    End Property

    'Public Property Occupation As Cls_Occupation

    '    Get
    '        If Not (_Occupation Is Nothing) Then
    '            If (_Occupation.ID = 0) Or (_Occupation.ID <> _ID_Occupation) Then
    '                _Occupation = New Cls_Occupation(_ID_Occupation)
    '            End If
    '        Else
    '            _Occupation = New Cls_Occupation(_ID_Occupation)
    '        End If

    '        Return _Occupation
    '    End Get
    '    Set(ByVal value As Cls_Occupation)
    '        If Value Is Nothing Then
    '            _isdirty = True
    '            _ID_Occupation = 0
    '        Else
    '            If _Occupation.ID <> Value.ID Then
    '                _isdirty = True
    '                _ID_Occupation = Value.ID
    '            End If
    '        End If
    '    End Set
    'End Property

    Public Property IdDepartement As Long
        Get
            Return _ID_Departement
        End Get
        Set(ByVal Value As Long)
            If _ID_Departement <> Value Then
                _isdirty = True
                _ID_Departement = Value
            End If
        End Set
    End Property

    Public Property Departement As Cls_Departement

        Get
            If Not (_Departement Is Nothing) Then
                If (_Departement.ID = 0) Or (_Departement.ID <> _ID_Departement) Then
                    _Departement = New Cls_Departement(_ID_Departement)
                End If
            Else
                _Departement = New Cls_Departement(_ID_Departement)
            End If

            Return _Departement
        End Get
        Set(ByVal value As Cls_Departement)
            If value Is Nothing Then
                _isdirty = True
                _ID_Departement = 0
            Else
                If _Departement.ID <> value.ID Then
                    _isdirty = True
                    _ID_Departement = value.ID
                End If
            End If
        End Set
    End Property

    Public Property IdCommune As Long
        Get
            Return _ID_Commune
        End Get
        Set(ByVal Value As Long)
            If _ID_Commune <> Value Then
                _isdirty = True
                _ID_Commune = Value
            End If
        End Set
    End Property

    Public Property Commune As Cls_Commune

        Get
            If Not (_Commune Is Nothing) Then
                If (_Commune.ID = 0) Or (_Commune.ID <> _ID_Commune) Then
                    _Commune = New Cls_Commune(_ID_Commune)
                End If
            Else
                _Commune = New Cls_Commune(_ID_Commune)
            End If

            Return _Commune
        End Get
        Set(ByVal value As Cls_Commune)
            If value Is Nothing Then
                _isdirty = True
                _ID_Commune = 0
            Else
                If _Commune.ID <> value.ID Then
                    _isdirty = True
                    _ID_Commune = value.ID
                End If
            End If
        End Set
    End Property

    Public Property IdPaysadresse As Long
        Get
            Return _ID_PaysAdresse
        End Get
        Set(ByVal Value As Long)
            If _ID_PaysAdresse <> Value Then
                _isdirty = True
                _ID_PaysAdresse = Value
            End If
        End Set
    End Property

    Public Property PaysAdresse As Cls_Pays

        Get
            If Not (_PaysAdresse Is Nothing) Then
                If (_PaysAdresse.ID = 0) Or (_PaysAdresse.ID <> _ID_PaysAdresse) Then
                    _PaysAdresse = New Cls_Pays(_ID_PaysAdresse)
                End If
            Else
                _PaysAdresse = New Cls_Pays(_ID_PaysAdresse)
            End If

            Return _PaysAdresse
        End Get
        Set(ByVal value As Cls_Pays)
            If value Is Nothing Then
                _isdirty = True
                _ID_PaysAdresse = 0
            Else
                If _PaysAdresse.ID <> value.ID Then
                    _isdirty = True
                    _ID_PaysAdresse = value.ID
                End If
            End If
        End Set
    End Property

    Public Property Adresserue As String
        Get
            Return _AdresseRue
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_AdresseRue)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _AdresseRue = Trim(Value)
            End If
        End Set
    End Property

    Public Property Adressecity As String
        Get
            Return _AdresseCity
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_AdresseCity)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _AdresseCity = Trim(Value)
            End If
        End Set
    End Property

    Public Property Adresseprovince As String
        Get
            Return _AdresseProvince
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_AdresseProvince)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _AdresseProvince = Trim(Value)
            End If
        End Set
    End Property

    Public Property Adressecodepostal As String
        Get
            Return _AdresseCodePostal
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_AdresseCodePostal)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _AdresseCodePostal = Trim(Value)
            End If
        End Set
    End Property

    Public Property Alertenouveaute As Boolean
        Get
            Return _AlerteNouveaute
        End Get
        Set(ByVal Value As Boolean)
            If _AlerteNouveaute <> Value Then
                _isdirty = True
                _AlerteNouveaute = Value
            End If
        End Set
    End Property

    Public Property Autrealerte As Boolean
        Get
            Return _AutreAlerte
        End Get
        Set(ByVal Value As Boolean)
            If _AutreAlerte <> Value Then
                _isdirty = True
                _AutreAlerte = Value
            End If
        End Set
    End Property

    Public Property Actifyn As Boolean
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

    Public Property Lastlogin As DateTime
        Get
            Return _LastLogin
        End Get
        Set(ByVal Value As DateTime)
            If _LastLogin <> Value Then
                _isdirty = True
                _LastLogin = Value
            End If
        End Set
    End Property

    Public Property Connecteryn As Boolean
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

    Public Property Mustchangepassword As Boolean
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

    Public Property Isforcedout As Boolean
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

    Public Property Confirmer As Boolean
        Get
            Return _Confirmer
        End Get
        Set(ByVal Value As Boolean)
            If _Confirmer <> Value Then
                _isdirty = True
                _Confirmer = Value
            End If
        End Set
    End Property

    ReadOnly Property IsDataDirty() As Boolean
        Get
            Return _isdirty
        End Get
    End Property

    Public Property PASSWORD_INTERNAUTE() As String
        Get
            Return _Password
        End Get
        Set(ByVal Value As String)
            If Len(Value) < 6 Then
                'IsSendEmail = False
                Throw New Rezo509Exception("Le mot de passe doit avoir au moins 6 caractères.")
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

    Public ReadOnly Property CONFIRMER_IMAGE As String
        Get
            Return Cls_Statut.Statut_Image(_Confirmer)
        End Get
    End Property


    Public ReadOnly Property DateCreated As DateTime
        Get
            Return _DateCreated
        End Get
    End Property

    Public ReadOnly Property CreatedBy As String
        Get
            Return _CreatedBy
        End Get
    End Property

    'Public ReadOnly Property AdresseComplet_forSearch As String
    '    Get
    '        Return _CreatedBy & " / " & _Adresse & ", " & PAYS_STR & ", " & COMMUNE_OBJ.DEPARTEMENT_STR_OrAnd_COMMUNE_STR
    '    End Get
    'End Property

    Public ReadOnly Property ADRESSE_COMPLET() As String
        Get
            Dim val As String = ""
            'If IdPaysadresse = Cls_Pays.Pays.Haiti Then
            '    val = "" & _AdresseRue & ", " & Departement.DEPARTEMENT & " / " & Commune.COMMUNE & ", " & _AdresseCodePostal & ", " & PaysAdresse.NOM_PAYS
            'Else
            '    val = "" & _AdresseRue & ", " & _AdresseCity & " / " & _AdresseProvince & ", " & _AdresseCodePostal & ", " & PaysAdresse.NOM_PAYS
            'End If
            Return val
        End Get
    End Property

    Public ReadOnly Property DETAILS_USER_HTML As String
        Get
            Return "<hr /><b>Compte :</b> " & Username & "<hr />" & _
                "<b>Groupe :</b> " & GroupeUser.GROUPE_DESCRIPTION & "<br />" & _
                "<b>Nom complet :</b> " & NOMCOMPLET & "<br />" & _
                "<b>Sexe :</b> " & IIf(_Sexe = "M", "Male", "Female") & "<br />" & _
                "<b>Adresse :</b> " & ADRESSE_COMPLET & "<br />"
        End Get
    End Property

    Public Enum Groupe_Utilisateur
        Administration = 1
        Super_Super_Administrateur = 1
        Entrepreneur = 2
        Investisseur = 3
        Entrepreneur_ET_Investisseur = 4
        Employer = 5
        Donate = 6
    End Enum


#Region "FIREBASE 24-11-2017 | 04:33 PM"
    Public Property IdAppFireBase() As String
        Get
            Return _IdAppFireBase
        End Get
        Set(ByVal Value As String)
            If _IdAppFireBase <> Value Then
                _isdirty = True
                _IdAppFireBase = Value
            End If
        End Set
    End Property

    Public Property IdFirebaseToken() As String
        Get
            Return _IdFirebaseToken
        End Get
        Set(ByVal Value As String)
            If _IdFirebaseToken <> Value Then
                _isdirty = True
                _IdFirebaseToken = Value
            End If
        End Set
    End Property
#End Region

#End Region

#Region " Db Access "
    Public Function Insert(ByVal usr As String) As Integer Implements IGeneral.Insert
        _LogData = LogData(Me)
        _id = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SR_Insert_User", _Id_Ecole _
                                                      , IIf(_Photo IsNot Nothing, _Photo, DBNull.Value) _
                                                      , _ID_GroupeUser, _UserName _
                                                      , Encripteur.Encrypt(_Password), _Civilite, _Nom, _Prenom, _Sexe, _Telephone _
                                                      , IIf(_DateNaissance Is Nothing, DBNull.Value, _DateNaissance), _ID_PaysNaissance _
                                                      , _ID_Occupation, _ID_Departement, _ID_Commune, _ID_PaysAdresse, _AdresseRue, _AdresseCity _
                                                      , _AdresseProvince, _AdresseCodePostal, _AlerteNouveaute, _AutreAlerte, _ActifYN, _Confirmer _
                                                      , _IdAppFireBase, _IdFirebaseToken, usr))
        Return _id
    End Function

    Public Function Update(ByVal usr As String) As Integer Implements IGeneral.Update
        _LogData = GetObjectString()
        Return SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SR_Update_User", _id, _Id_Ecole _
                                                      , IIf(_Photo IsNot Nothing, _Photo, DBNull.Value) _
                                                      , _ID_GroupeUser, _UserName _
                                                      , _Civilite, _Nom, _Prenom, _Sexe, _Telephone _
                                                      , IIf(_DateNaissance Is Nothing, DBNull.Value, _DateNaissance), _ID_PaysNaissance _
                                                      , _ID_Occupation, _ID_Departement, _ID_Commune, _ID_PaysAdresse, _AdresseRue, _AdresseCity _
                                                      , _AdresseProvince, _AdresseCodePostal, _AlerteNouveaute, _AutreAlerte, _ActifYN, _Confirmer _
                                                      , _IdAppFireBase, _IdFirebaseToken, usr)
    End Function

    Public Sub SetProperties(ByVal dr As DataRow)
        _id = TypeSafeConversion.NullSafeLong(dr("ID_User"))
        _Id_Ecole = TypeSafeConversion.NullSafeLong(dr("ID_Ecole"))
        _ID_GroupeUser = TypeSafeConversion.NullSafeLong(dr("ID_GroupeUser"))
        If dr("Photo") IsNot DBNull.Value Then
            _Photo = dr("Photo")
        Else
            _Photo = Nothing
        End If
        _UserName = TypeSafeConversion.NullSafeString(dr("UserName"))
        _Password = TypeSafeConversion.NullSafeString(dr("Password"))
        _Civilite = TypeSafeConversion.NullSafeString(dr("Civilite"))
        _Nom = TypeSafeConversion.NullSafeString(dr("Nom"))
        _Prenom = TypeSafeConversion.NullSafeString(dr("Prenom"))
        _Sexe = TypeSafeConversion.NullSafeString(dr("Sexe"))
        _Telephone = TypeSafeConversion.NullSafeString(dr("Telephone"))
        _DateNaissance = TypeSafeConversion.NullSafeDate(dr("DateNaissance"))
        _ID_PaysNaissance = TypeSafeConversion.NullSafeLong(dr("ID_PaysNaissance"))
        _ID_Occupation = TypeSafeConversion.NullSafeLong(dr("ID_Occupation"))
        _ID_Departement = TypeSafeConversion.NullSafeLong(dr("ID_Departement"))
        _ID_Commune = TypeSafeConversion.NullSafeLong(dr("ID_Commune"))
        _ID_PaysAdresse = TypeSafeConversion.NullSafeLong(dr("ID_PaysAdresse"))
        _AdresseRue = TypeSafeConversion.NullSafeString(dr("AdresseRue"))
        _AdresseCity = TypeSafeConversion.NullSafeString(dr("AdresseCity"))
        _AdresseProvince = TypeSafeConversion.NullSafeString(dr("AdresseProvince"))
        _AdresseCodePostal = TypeSafeConversion.NullSafeString(dr("AdresseCodePostal"))
        _AlerteNouveaute = TypeSafeConversion.NullSafeBoolean(dr("AlerteNouveaute"))
        _AutreAlerte = TypeSafeConversion.NullSafeBoolean(dr("AutreAlerte"))
        _ActifYN = TypeSafeConversion.NullSafeBoolean(dr("ActifYN"))
        _LastLogin = TypeSafeConversion.NullSafeDate(dr("LastLogin"))
        _ConnecterYN = TypeSafeConversion.NullSafeBoolean(dr("ConnecterYN"))
        _LastIP = TypeSafeConversion.NullSafeString(dr("LastIP"))
        _MustChangePassword = TypeSafeConversion.NullSafeBoolean(dr("MustChangePassword"))
        _IsForcedOut = TypeSafeConversion.NullSafeBoolean(dr("IsForcedOut"))
        _Confirmer = TypeSafeConversion.NullSafeBoolean(dr("Confirmer"))

        _DateCreated = TypeSafeConversion.NullSafeDate(dr("DateCreated"))
        _CreatedBy = TypeSafeConversion.NullSafeString(dr("CreatedBy"))

        _IdAppFireBase = TypeSafeConversion.NullSafeString(dr("IdAppFireBase"))
        _IdFirebaseToken = TypeSafeConversion.NullSafeString(dr("IdFirebaseToken"))
    End Sub

    Public Sub BlankProperties()
        _id = 0
        _Id_Ecole = 0
        _ID_GroupeUser = 0
        _GroupeUser = Nothing

        _Photo = Nothing
        _UserName = ""
        _Password = ""
        _Civilite = ""
        _Nom = ""
        _Prenom = ""
        _Sexe = ""
        _Telephone = ""
        _DateNaissance = Now
        _ID_PaysNaissance = 0
        _ID_Occupation = 0
        '_Occupation = Nothing
        _ID_Departement = 0
        _Departement = Nothing
        _ID_Commune = 0
        _Commune = Nothing
        _ID_PaysAdresse = 0
        _PaysAdresse = Nothing
        _AdresseRue = ""
        _AdresseCity = ""
        _AdresseProvince = ""
        _AdresseCodePostal = ""
        _AlerteNouveaute = False
        _AutreAlerte = False
        _ActifYN = False
        _LastLogin = Now
        _ConnecterYN = False
        _LastIP = ""
        _MustChangePassword = False
        _IsForcedOut = False
        _Confirmer = False


        _IdAppFireBase = ""
        _IdFirebaseToken = ""
    End Sub

    Public Sub SetDefaultProperties()
        _id = 1
        _Id_Ecole = 1
        _ID_GroupeUser = Groupe_Utilisateur.Super_Super_Administrateur

        _UserName = "info@brain-dev.net"
        _Password = ""
        _Civilite = ""
        _Nom = "Admin"
        _Prenom = "Super"
        _Sexe = "M"
        _Telephone = "49258080"
        _DateNaissance = Now
        _AlerteNouveaute = False
        _AutreAlerte = False
        _ActifYN = True
        _LastLogin = Now
        _ConnecterYN = True
        _LastIP = ""
        _MustChangePassword = False
        _IsForcedOut = False
        _Confirmer = True

        _Photo = Nothing
        _ID_PaysNaissance = 0
        _ID_Occupation = 0
        _ID_Departement = 0
        _Departement = Nothing
        _ID_Commune = 0
        _Commune = Nothing
        _ID_PaysAdresse = 0
        _PaysAdresse = Nothing
        _AdresseRue = ""
        _AdresseCity = ""
        _AdresseProvince = ""
        _AdresseCodePostal = ""

        _IdAppFireBase = ""
        _IdFirebaseToken = ""
    End Sub

    Public Overloads Function Read(ByVal idval As Long) As Boolean Implements IGeneral.Read
        If idval <> 0 Then
            Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_Select_User_ById", idval)

            If ds.Tables(0).Rows.Count < 1 Then
                BlankProperties()
                Return False
            End If

            SetProperties(ds.Tables(0).Rows(0))
        Else
            BlankProperties()
        End If

        Return True
    End Function

    Public Overloads Function Read(ByVal usr As String) As Boolean
        Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_Select_User_ByUserName", usr)

        If ds.Tables(0).Rows.Count < 1 Then
            BlankProperties()
            Return False
        End If

        SetProperties(ds.Tables(0).Rows(0))

        Return True
    End Function

    Public Function Read_UserName(ByVal UserName As String) As Boolean
        Try
            If UserName <> "" Then
                Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "Sp_SelectSR_USER_ByID", UserName)
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
            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SR_Delete_User", _id)
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
    Public Function Save_Client(ByVal usr As String) As Integer
        Dim val As Integer = 0
        If _isdirty Then
            Validation_Client()

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

    Public Function ChangeGroupeStatut(ByVal usr As String) As Integer
        Return 0 ' SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SR_Update_User", _id, _ID_GroupeUser, _UserName, _Civilite, _Nom, _Prenom, _Sexe, _Telephone, IIf(_DateNaissance Is Nothing, DBNull.Value, _DateNaissance), _ID_PaysNaissance, _ID_Occupation, _ID_Departement, _ID_Commune, _ID_PaysAdresse, _AdresseRue, _AdresseCity, _AdresseProvince, _AdresseCodePostal, _AlerteNouveaute, _AutreAlerte, _ActifYN, _Confirmer, usr)
    End Function

#End Region

#Region " Search "
    Public Function Search() As System.Collections.ICollection Implements IGeneral.Search
        Return SearchAll()
    End Function

    Public Shared Function SearchAll(Optional ByVal ActifYN As String = "FO") As List(Of Cls_User)
        Try
            Dim objs As New List(Of Cls_User)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_ListAll_User", ActifYN)
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


    Public Shared Function SetIsHaveOtherService_User_ById(ByVal ID_USER As Long, ByVal _IsHaveOtherService As Boolean) As Long
        Return SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SR_IsHaveOtherService_USER_ById", ID_USER, _IsHaveOtherService)
    End Function

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

#End Region

#Region " Other Methods "
    Private Function LoginValueExists(ByVal value As String) As Boolean
        Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_Select_User_ByUserName", value)

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
    End Function

    Public Shared Function LoginExists(ByVal _id As String, ByVal Username As String) As Boolean
        Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_Select_User_ByUserName", Username)

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
    End Function

    Private Sub Validation()
        If _UserName = "" Then
            'IsSendEmail = False
            Throw (New Rezo509Exception("Le Code d'Utilisateur n'est pas renseigné."))
        End If

        'If _ID_Group = 0 Then
        '    Throw (New Rezo509Exception("Le Groupe n'est pas renseigné."))
        'End If

        'If Len(_UserName) > 50 Then
        '    Throw (New Rezo509Exception("Code d'Utilisateur trop long (doit être inférieur à 20 caractères)"))
        'End If

        If LoginValueExists(_UserName) Then
            'IsSendEmail = False
            Throw (New Rezo509Exception("Code Utilisateur [ " & _UserName & " ] déjà enregistré."))
        End If
    End Sub

    Private Sub Validation_Client()
        If _UserName = "" Then
            Throw (New Rezo509Exception("Le Code d'Utilisateur n'est pas renseigné."))
        End If

        If _Id_Ecole = 0 Then
            Throw (New Rezo509Exception("Ecole obligatoire"))
        End If

        If _ID_GroupeUser = 0 Then
            Throw (New Rezo509Exception("Le Groupe n'est pas renseigné."))
        End If

        If FoundName() Then
            Throw (New Rezo509Exception("Code Utilisateur [ " & _UserName & " ] est déjà enregistré."))
        End If
    End Sub

    Private Function FoundName() As Boolean
        Try
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_Select_User_ByID_Ecole_ByUserName", _Id_Ecole, _UserName)
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
            'ErreurLog.WriteError(ex.Message)
            Throw ex
        End Try
    End Function

    Public Function ChangePassword(ByVal oldpass As String, ByVal newpass As String, ByVal verifnewpass As String) As String
        Dim tmp As String = ""

        If Encripteur.Encrypt(oldpass) = Me.Password Then
            If Encripteur.Encrypt(newpass) <> Encripteur.Encrypt(oldpass) Then
                If newpass = verifnewpass Then
                    If Len(newpass) >= 6 Then
                        Try
                            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SR_Update_Password_User", _id, Encripteur.Encrypt(newpass), Me.Username)

                            _Password = Encripteur.Encrypt(newpass)
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
                    SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SR_Update_Password_User", _id, Encripteur.Encrypt(newpass), Me.Username)

                    _Password = Encripteur.Encrypt(newpass)
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
                SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SR_Update_Password_User", _id, Encripteur.Encrypt(_UserName), _u)

                _Password = Encripteur.Encrypt(_UserName)
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
        valret = TypeSafeConversion.NullSafeLong(SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SR_Insert_UserActivite_InRezo", Me.Username, actv, _TmpIPadd, Left(descactv, 2000)))
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

    Public Function GetObjectString() As String Implements IGeneral.GetObjectString
        Return LogData(New Cls_User(Me.ID))
    End Function

    Function LogData(obj As Cls_User) As String
        Return LogStringBuilder.BuildLogStringHTML(obj)
    End Function
#End Region

End Class


