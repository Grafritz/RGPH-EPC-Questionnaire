REM Generate By [GENERIC 12] Application *******
REM  Class Cls_SR_USER

REM Date:15-Mar-2013 7h:i:53m
Imports Microsoft
Imports System.Data
Imports System.Collections.Generic
Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer
Imports System.Net

Public Class Cls_User2
    Implements IGeneral

#Region "Attribut"
    Private _id As Long

    Private _ID_GroupeUser As Long
    Private _GroupeUser As Cls_GroupeUser
    Private _Photo As Byte()
    Private _UserName As String
    Private _Password As String
    Private _Civilite As String
    Private _Nom As String
    Private _Prenom As String
    Private _NomComplet As String
    Private _Email As String
    Private _Sexe As String
    Private _Telephone As String
    Private _Poste As String
    Private _SkypeName As String
    Private _Signature As Byte()
    Private _ActifYN As Boolean
    Private _LastLogin As DateTime
    Private _ConnecterYN As Boolean
    Private _LastIP As String
    Private _MustChangePassword As Boolean
    Private _IsForcedOut As Boolean
    Private _AskPassword As String
    Private _Confirmer As Boolean
    Private _isdirty As Boolean

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
            If Encripteur.Encrypt(Pass) <> _Password Then
                Throw New System.Exception("Utilisateur ou Mot de Passe incorrects.")
            End If
        Else
            Throw (New System.Exception("Utilisateur ou Mot de Passe incorrects."))
        End If
    End Sub
#End Region

#Region "Properties"
    Public ReadOnly Property ID() As Long Implements IGeneral.ID
        Get
            Return _id
        End Get
    End Property

    Public ReadOnly Property IdReunionparticipant() As Long
        Get
            Return _id
        End Get
    End Property

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
            If Value Is Nothing Then
                _isdirty = True
                _ID_GroupeUser = 0
            Else
                If _GroupeUser.ID <> Value.ID Then
                    _isdirty = True
                    _ID_GroupeUser = Value.ID
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

    Public ReadOnly Property GroupeStr As String
        Get
            Return GroupeUser.GROUPE_DESCRIPTION
        End Get
    End Property

    Public ReadOnly Property Nomcomplet_Or_Username As String
        Get
            Return IIf(Not _UserName.Trim.Equals(""), _UserName, Nomcomplet)
        End Get
    End Property

    Public Property Email As String
        Get
            Return _UserName
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
            Return _Telephone
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_Telephone)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _Telephone = Trim(Value)
            End If
        End Set
    End Property

    Public Property Poste As String
        Get
            Return _Poste
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_Poste)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _Poste = Trim(Value)
            End If
        End Set
    End Property

    Public Property Skypename As String
        Get
            Return _SkypeName
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_SkypeName)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _SkypeName = Trim(Value)
            End If
        End Set
    End Property

    Public Property Signature As Byte()
        Get
            Return _Signature
        End Get
        Set(ByVal Value As Byte())
            _isdirty = True
            _Signature = Value
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

    Public Property AskPassword As String
        Get
            Return _AskPassword
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_AskPassword)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _AskPassword = Trim(Value)
            End If
        End Set
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
        _id = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SR_Insert_User", _ID_GroupeUser, IIf(_Photo IsNot Nothing, _Photo, DBNull.Value), _UserName, Encripteur.Encrypt(_Password), _Civilite, _Nom, _Prenom, _Sexe, _Telephone, _ActifYN, _Confirmer, _Poste, usr))
        Return _id
    End Function

    Public Function Update(ByVal usr As String) As Integer Implements IGeneral.Update
        Return SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SR_Update_User", _id, _ID_GroupeUser, IIf(_Photo IsNot Nothing, _Photo, DBNull.Value), _UserName, _Civilite, _Nom, _Prenom, _Sexe, _Telephone, _ActifYN, _Confirmer, _MustChangePassword, _Poste, usr)
    End Function

    Public Function setAskPassword(ByVal usr As String) As Integer
        Return SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SR_UpdateUser_AskPassword", _id, _AskPassword, usr)
    End Function

    Public Sub SetProperties(ByVal dr As DataRow)
        _id = TypeSafeConversion.NullSafeLong(dr("ID_User"))
        _ID_GroupeUser = TypeSafeConversion.NullSafeLong(dr("ID_GroupeUser"))
        If dr("Photo") IsNot DBNull.Value Then
            _Photo = dr("Photo")
        Else
            _Photo = Nothing
        End If
        _UserName = TypeSafeConversion.NullSafeString(dr("UserName"))
        _Password = TypeSafeConversion.NullSafeString(dr("Password"))

        _Email = TypeSafeConversion.NullSafeString(dr("UserName"))
        _Civilite = TypeSafeConversion.NullSafeString(dr("Civilite"))
        _Nom = TypeSafeConversion.NullSafeString(dr("Nom"))
        _Prenom = TypeSafeConversion.NullSafeString(dr("Prenom"))
        _Sexe = TypeSafeConversion.NullSafeString(dr("Sexe"))
        _Telephone = TypeSafeConversion.NullSafeString(dr("Telephone"))
        _ActifYN = TypeSafeConversion.NullSafeBoolean(dr("ActifYN"))
        _LastLogin = TypeSafeConversion.NullSafeDate(dr("LastLogin"))
        _ConnecterYN = TypeSafeConversion.NullSafeBoolean(dr("ConnecterYN"))
        _LastIP = TypeSafeConversion.NullSafeString(dr("LastIP"))
        _MustChangePassword = TypeSafeConversion.NullSafeBoolean(dr("MustChangePassword"))
        _IsForcedOut = TypeSafeConversion.NullSafeBoolean(dr("IsForcedOut"))
        _Confirmer = TypeSafeConversion.NullSafeBoolean(dr("Confirmer"))

        _AskPassword = TypeSafeConversion.NullSafeString(dr("AskPassword"))
        _Poste = TypeSafeConversion.NullSafeString(dr("Poste"))
    End Sub

    Public Sub BlankProperties()
        _id = 0
        _ID_GroupeUser = 0
        _Photo = Nothing
        _Poste = ""
        _UserName = ""
        _Password = ""
        _Civilite = ""
        _Nom = ""
        _Prenom = ""
        _Sexe = ""
        _Telephone = ""
        _ActifYN = False
        _LastLogin = Now
        _ConnecterYN = False
        _LastIP = ""
        _MustChangePassword = False
        _IsForcedOut = False
        _AskPassword = ""
        _Confirmer = False
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
            IsSendEmail = False
            Throw (New System.Exception("Le Code d'Utilisateur n'est pas renseigné."))
        End If

        'If _ID_Group = 0 Then
        '    Throw (New System.Exception("Le Groupe n'est pas renseigné."))
        'End If

        'If Len(_UserName) > 50 Then
        '    Throw (New System.Exception("Code d'Utilisateur trop long (doit être inférieur à 20 caractères)"))
        'End If

        If LoginValueExists(_UserName) Then
            IsSendEmail = False
            Throw (New System.Exception("Code Utilisateur [ " & _UserName & " ] déjà enregistré."))
        End If
    End Sub

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

    Public Function GetObjectString() As String Implements IGeneral.GetObjectString
        Return LogStringBuilder.BuildLogString(Me)
    End Function
#End Region

End Class


