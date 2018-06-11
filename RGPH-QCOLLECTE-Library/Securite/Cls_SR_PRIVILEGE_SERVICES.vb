
Imports Microsoft
Imports System.Data
Imports System.Collections.Generic
Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer

Public Class Cls_SR_PRIVILEGE_SERVICES
    Implements IGeneral

#Region "Attribut"
    Private _id As Long

    Private _ID_User As Long
    Private _User As Cls_User
    Private _ID_Services As Long
    Private _Services As Cls_Privilege
    Private _DateAjout As Nullable(Of DateTime)
    Private _DateLimite As Nullable(Of DateTime)
    Private _ActifYN As Boolean
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
    <AttributLogData(True, 1)> _
    Public ReadOnly Property ID() As Long Implements IGeneral.ID
        Get
            Return _id
        End Get
    End Property

    <AttributLogData(True, 2)> _
    Public Property ID_User As Long
        Get
            Return _ID_User
        End Get
        Set(ByVal Value As Long)
            If _ID_User <> Value Then
                _isdirty = True
                _ID_User = Value
            End If
        End Set
    End Property

    Public Property User As Cls_User
        Get
            If Not (_User Is Nothing) Then
                If (_User.ID = 0) Or (_User.ID <> _ID_User) Then
                    _User = New Cls_User(_ID_User)
                End If
            Else
                _User = New Cls_User(_ID_User)
            End If

            Return _User
        End Get
        Set(ByVal value As Cls_User)
            If value Is Nothing Then
                _isdirty = True
                _ID_User = 0
            Else
                If _User.ID <> value.ID Then
                    _isdirty = True
                    _ID_User = value.ID
                End If
            End If
        End Set
    End Property

    <AttributLogData(True, 3)> _
        Public Property ID_Services As Long
        Get
            Return _ID_Services
        End Get
        Set(ByVal Value As Long)
            If _ID_Services <> Value Then
                _isdirty = True
                _ID_Services = Value
            End If
        End Set
    End Property

    Public Property Services As Cls_Privilege
        Get
            If Not (_Services Is Nothing) Then
                If (_Services.ID = 0) Or (_Services.ID <> _ID_Services) Then
                    _Services = New Cls_Privilege(_ID_Services)
                End If
            Else
                _Services = New Cls_Privilege(_ID_Services)
            End If

            Return _Services
        End Get
        Set(ByVal value As Cls_Privilege)
            If value Is Nothing Then
                _isdirty = True
                _ID_Services = 0
            Else
                If _Services.ID <> value.ID Then
                    _isdirty = True
                    _ID_Services = value.ID
                End If
            End If
        End Set
    End Property

    Public ReadOnly Property NomService As String
        Get
            Return Services.NOM_OBJET
        End Get
    End Property

    Public ReadOnly Property DescriptionService As String
        Get
            Return Services.DESCRIPTION_OBJET
        End Get
    End Property

    Public ReadOnly Property Nomcomplet_et_Username As String
        Get
            Return User.Nomcomplet_et_Username
        End Get
    End Property

    <AttributLogData(True, 4)> _
    Public Property DateAjout() As Nullable(Of Date)
        Get
            Return _DateAjout
        End Get
        Set(ByVal Value As Nullable(Of Date))
            If Value.HasValue AndAlso _DateAjout.HasValue AndAlso Value.Value <> _DateAjout.Value Then
                _isdirty = True
                _DateAjout = Value
            ElseIf (Not Value.HasValue AndAlso _DateAjout.HasValue) OrElse (Value.HasValue AndAlso Not _DateAjout.HasValue) Then
                _isdirty = True
                _DateAjout = Value
            End If
        End Set
    End Property

    <AttributLogData(True, 5)> _
    Public Property DateLimite As Nullable(Of Date)
        Get
            Return _DateLimite
        End Get
        Set(ByVal Value As Nullable(Of Date))
            If Value.HasValue AndAlso _DateLimite.HasValue AndAlso Value.Value <> _DateLimite.Value Then
                _isdirty = True
                _DateLimite = Value
            ElseIf (Not Value.HasValue AndAlso _DateLimite.HasValue) OrElse (Value.HasValue AndAlso Not _DateLimite.HasValue) Then
                _isdirty = True
                _DateLimite = Value
            End If
        End Set
    End Property

    <AttributLogData(True, 6)> _
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


    Public ReadOnly Property STATUT_IMAGE As String
        Get
            Return Cls_Statut.Statut_Image(_ActifYN)
        End Get
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
#End Region

#Region " Db Access "
    Public Function Insert(ByVal usr As String) As Integer Implements IGeneral.Insert
        _LogData = ""
        _id = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_InsertSR_PRIVILEGE_SERVICES", _ID_User, _ID_Services _
                                                         , IIf(_DateAjout Is Nothing, DBNull.Value, _DateAjout) _
                                                         , IIf(_DateLimite Is Nothing, DBNull.Value, _DateLimite) _
                                                         , _ActifYN, usr))
        Return _id
    End Function

    Public Function Update(ByVal usr As String) As Integer Implements IGeneral.Update
        _LogData = ""
        '_LogData = GetObjectString()
        Return SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SP_UpdateSR_PRIVILEGE_SERVICES", _id, _ID_User, _ID_Services _
                                                         , IIf(_DateAjout Is Nothing, DBNull.Value, _DateAjout) _
                                                         , IIf(_DateLimite Is Nothing, DBNull.Value, _DateLimite) _
                                                         , _ActifYN, usr)
    End Function

    Private Sub SetProperties(ByVal dr As DataRow)
        _id = TypeSafeConversion.NullSafeLong(dr("ID"))
        _ID_User = TypeSafeConversion.NullSafeLong(dr("ID_User"))
        _ID_Services = TypeSafeConversion.NullSafeLong(dr("ID_Services"))
        _DateAjout = TypeSafeConversion.NullSafeDate(dr("DateAjout"))
        If _DateAjout.HasValue And _DateAjout.Value.Year < 1753 Then
            _DateAjout = Nothing
        End If
        _DateLimite = TypeSafeConversion.NullSafeDate(dr("DateLimite"))
        If _DateLimite.HasValue And _DateLimite.Value.Year < 1753 Then
            _DateLimite = Nothing
        End If
        _ActifYN = TypeSafeConversion.NullSafeBoolean(dr("ActifYN"))
    End Sub

    Private Sub BlankProperties()
        _id = 0
        _ID_User = 0
        _User = Nothing
        _ID_Services = 0
        _Services = Nothing
        _DateAjout = Nothing
        _DateLimite = Nothing
        _ActifYN = False
        _isdirty = False
    End Sub

    Public Function Read(ByVal _idpass As Long) As Boolean Implements IGeneral.Read
        Try

            If _idpass <> 0 Then
                Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_SelectSR_PRIVILEGE_SERVICES_ByID", _idpass)

                If ds.Tables(0).Rows.Count < 1 Then
                    BlankProperties()
                    Return False
                End If

                SetProperties(ds.tables(0).rows(0))
            Else
                BlankProperties()
            End If
            Return True

        Catch ex As Rezo509Exception

            Throw ex

        End Try

    End Function

    Public Sub Delete() Implements IGeneral.Delete
        Try
            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SP_DeleteSR_PRIVILEGE_SERVICES", _id)

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

    Public Shared Function VerifyRightOnObject(ByVal ObjectRequested As String, ByVal _idUser As Long) As Boolean
        Dim obj As Cls_SR_PRIVILEGE_SERVICES = VerifyRightOnObject_2(ObjectRequested, _idUser)
        Dim valret As Boolean

        If obj IsNot Nothing AndAlso obj.ID > 0 Then
            If obj.DateLimite < Date.Now Then
                valret = False
            Else
                valret = True
            End If
        Else
            valret = False
        End If

        Return valret
    End Function

    Public Shared Function VerifyRightOnObject_2(ByVal ObjectRequested As String, ByVal _idUser As Long) As Cls_SR_PRIVILEGE_SERVICES
        Try
            Dim obj As New Cls_SR_PRIVILEGE_SERVICES
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Select_SR_PRIVILEGE_SERVICES_By_idUser_ByNameService", _idUser, ObjectRequested)
            If ds.Tables(0).Rows.Count < 1 Then
                obj.BlankProperties()
            Else
                For Each r In ds.Tables(0).Rows
                    obj.SetProperties(r)
                Next r
            End If

            Return obj
        Catch ex As Rezo509Exception
            Throw ex
        End Try
    End Function

    Public Shared Function SearchAll() As List(Of Cls_SR_PRIVILEGE_SERVICES)
        Try
            Dim objs As New List(Of Cls_SR_PRIVILEGE_SERVICES)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_SR_PRIVILEGE_SERVICES")
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_SR_PRIVILEGE_SERVICES

                obj.SetProperties(r)

                objs.Add(obj)
            Next r
            Return objs
        Catch ex As Rezo509Exception
            Throw ex
        End Try
    End Function

    Public Shared Function SearchAllByUser(ByVal _id_user As Long) As List(Of Cls_SR_PRIVILEGE_SERVICES)
        Try
            Dim objs As New List(Of Cls_SR_PRIVILEGE_SERVICES)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_SR_PRIVILEGE_SERVICES_ID_User", _id_user)
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_SR_PRIVILEGE_SERVICES

                obj.SetProperties(r)

                objs.Add(obj)
            Next r
            Return objs
        Catch ex As Rezo509Exception

            Throw ex
        End Try
    End Function

    Public Shared Function SearchAllByServices(ByVal _id_services As Long) As List(Of Cls_SR_PRIVILEGE_SERVICES)
        Try
            Dim objs As New List(Of Cls_SR_PRIVILEGE_SERVICES)
            Dim r As Data.DataRow
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_ListAll_SR_PRIVILEGE_SERVICES_ID_Services", _id_services)
            For Each r In ds.Tables(0).Rows
                Dim obj As New Cls_SR_PRIVILEGE_SERVICES

                obj.SetProperties(r)

                objs.Add(obj)
            Next r
            Return objs
        Catch ex As Rezo509Exception

            Throw ex
        End Try
    End Function

#End Region

#Region " Other Methods "
    Private Sub Validation()

        If _ID_User = 0 Then
            Throw (New System.Exception(" Utilisateur obligatoire"))
        End If

        If _ID_Services = 0 Then
            Throw (New System.Exception(" Services obligatoire"))
        End If

        If FoundName() Then
            Throw (New System.Exception("l'utilisateur a déjà ce service."))
        End If
    End Sub

    Private Function FoundName() As Boolean
        Try
            Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Select_SR_PRIVILEGE_SERVICES_ByIdUser_ByIdService", _ID_User, _ID_Services)
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
        Catch ex As Rezo509Exception
            ErreurLog.WriteError(ex.Message)
            Throw ex
        End Try
    End Function

    Public Function Encode(ByVal str As Byte()) As String
        Return Convert.ToBase64String(str)
    End Function

    Public Function Decode(ByVal str As String) As Byte()
        Dim decbuff As Byte() = Convert.FromBase64String(str)
        Return decbuff
    End Function

    Public Function GetObjectString() As String Implements IGeneral.GetObjectString
        'Dim _old As New Cls_SR_PRIVILEGE_SERVICES(Me.ID)
        Return Nothing ' LogStringBuilder.BuildLogStringChangesOnly(_old, Me)
    End Function

#End Region

End Class


