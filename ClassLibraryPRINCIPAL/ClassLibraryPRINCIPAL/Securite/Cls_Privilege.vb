' @Brain Development
' www.Rezo509.com
' Thursday 10-11-2011

Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Net

Public Class Cls_Privilege
    Implements IGeneral

#Region " Attribut "
    Private _id As Long

    Private _object As String
    Private _objectdesc As String
    Private _objecttype As String
    Private _ID_Modules As Long
    Private _IsProduitsRezo509 As Boolean
    Private _ModuleCls As Cls_SR_MODULES

    Private _isdirty As Boolean = False
#End Region

#Region " New "
    Public Sub New()
        BlankProperties()
    End Sub

    Public Sub New(ByVal _idOne As Long)
        Read(_idOne)
    End Sub
#End Region

#Region " Properties "
    <AttributLogData(True, 1)> _
    Public ReadOnly Property ID() As Long Implements IGeneral.ID
        Get
            Return _id
        End Get
    End Property

    <AttributLogData(True, 2)> _
    Public Property NOM_OBJET() As String
        Get
            Return _object
        End Get
        Set(ByVal Value As String)
            If LCase(_object) <> LCase(Value) Then
                _isdirty = True
                _object = Value
            End If
        End Set
    End Property

    <AttributLogData(True, 3)> _
    Public Property NomService As String
        Get
            Return _object
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_object)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _object = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 4)> _
    Public Property DESCRIPTION_OBJET() As String
        Get
            Return _objectdesc
        End Get
        Set(ByVal Value As String)
            If LCase(_objecttype) <> LCase(Value) Then
                _isdirty = True
                _objectdesc = Value
            End If
        End Set
    End Property

    <AttributLogData(True, 5)> _
    Public Property DescriptionService As String
        Get
            Return _objectdesc
        End Get
        Set(ByVal Value As String)
            If LCase(Trim(_objectdesc)) <> LCase(Trim(Value)) Then
                _isdirty = True
                _objectdesc = Trim(Value)
            End If
        End Set
    End Property

    <AttributLogData(True, 6)> _
    Public Property TYPE_OBJET() As String
        Get
            Return _objecttype
        End Get
        Set(ByVal Value As String)
            If LCase(_objectdesc) <> LCase(Value) Then
                _isdirty = True
                _objecttype = Value
            End If
        End Set
    End Property

    <AttributLogData(True, 7)> _
    Public Property ID_Modules() As String
        Get
            Return _ID_Modules
        End Get
        Set(ByVal Value As String)
            If LCase(_ID_Modules) <> LCase(Value) Then
                _isdirty = True
                _ID_Modules = Value
            End If
        End Set
    End Property

    <AttributLogData(True, 8)> _
    Public Property IsProduitsRezo509() As Boolean
        Get
            Return _IsProduitsRezo509
        End Get
        Set(ByVal Value As Boolean)
            If LCase(_IsProduitsRezo509) <> LCase(Value) Then
                _isdirty = True
                _IsProduitsRezo509 = Value
            End If
        End Set
    End Property

    Public Property MODULES_OBJ As Cls_SR_MODULES

        Get
            If Not (_ModuleCls Is Nothing) Then
                If (_ModuleCls.ID = 0) Or (_ModuleCls.ID <> _ID_Modules) Then
                    _ModuleCls = New Cls_SR_MODULES(_ID_Modules)
                End If
            Else
                _ModuleCls = New Cls_SR_MODULES(_ID_Modules)
            End If

            Return _ModuleCls
        End Get
        Set(ByVal value As Cls_SR_MODULES)
            If value Is Nothing Then
                _isdirty = True
                _ID_Modules = 0
            Else
                If _ModuleCls.ID <> value.ID Then
                    _isdirty = True
                    _ID_Modules = value.ID
                End If
            End If
        End Set
    End Property

    Public ReadOnly Property ModulesSTR() As String
        Get
            Return MODULES_OBJ.NomModule
        End Get
    End Property

    Public ReadOnly Property IsDataDirty() As Boolean
        Get
            Return _isdirty
        End Get
    End Property
#End Region

#Region " DB Access "
    Private Function Insert(ByVal usr As String) As Integer Implements IGeneral.Insert
        _id = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SR_Insert_Object", _object, _objectdesc, _objecttype, _ID_Modules, _IsProduitsRezo509, usr))
        Return _id
    End Function

    Private Function Update(ByVal usr As String) As Integer Implements IGeneral.Update
        Return SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SR_Update_Object", _id, _object, _objectdesc, _objecttype, _ID_Modules, _IsProduitsRezo509, usr)
    End Function

    Public Function Read(ByVal idval As Long) As Boolean Implements IGeneral.Read
        If idval <> 0 Then
            Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_Select_Object_ById", idval)

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

    Private Sub SetProperties(ByVal r As DataRow)
        _id = Convert.ToInt32(r("Object_ID"))
        _object = Convert.ToString(r("Object"))
        _objectdesc = Convert.ToString(r("ObjectDescription"))
        _objecttype = Convert.ToString(r("ObjectType"))
        _ID_Modules = TypeSafeConversion.NullSafeLong(r("ID_Modules"))
        _IsProduitsRezo509 = TypeSafeConversion.NullSafeBoolean(r("IsProduitsRezo509"))

        _isdirty = False
    End Sub

    Private Sub BlankProperties()
        _id = 0
        _object = ""
        _objectdesc = ""
        _objecttype = ""
        _ID_Modules = 0
        _IsProduitsRezo509 = False

        _isdirty = False
    End Sub

    Public Overloads Sub Delete() Implements IGeneral.Delete
        SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SR_Delete_Object", _id)
    End Sub

    Public Overloads Shared Sub Delete(ByVal id As Long)
        SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SR_Delete_Object", id)
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
        If _isdirty Then
            Validation()

            If _id = 0 Then
                Return Insert(usr)
            Else
                If _id > 0 Then
                    Return Update(usr)
                Else
                    _id = 0
                    Return False
                End If
            End If
        End If

        _isdirty = False
    End Function
#End Region

#Region " Search "
    Public Function Search() As System.Collections.ICollection Implements IGeneral.Search
        Dim objs As New Cls_PrivilegeCollections
        Dim r As DataRow
        Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_ListAll_Objects")

        For Each r In ds.Tables(0).Rows
            Dim obj1 As New Cls_Privilege

            obj1.SetProperties(r)

            objs.Add(obj1)
        Next r

        Return objs
    End Function

    Public Shared Function SearchAll(ByVal IsProduitRezo509 As Boolean) As List(Of Cls_Privilege)
        Dim objs As New List(Of Cls_Privilege)
        Dim r As Data.DataRow
        Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_ListAll_Objects_IsProduitRezo509", IsProduitRezo509)

        For Each r In ds.Tables(0).Rows
            Dim obj As New Cls_Privilege

            obj.SetProperties(r)

            objs.Add(obj)
        Next r

        Return objs
    End Function

    Public Shared Function SearchAll_LIKE(ByVal Critere_String As String, ByVal TypeObjSearch As String, ByVal IDModule As Long) As Cls_PrivilegeCollections
        Dim objs As New Cls_PrivilegeCollections
        Dim r As Data.DataRow
        Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_Search_Object_Like", Critere_String, TypeObjSearch, IDModule)

        For Each r In ds.Tables(0).Rows
            Dim obj1 As New Cls_Privilege

            obj1.SetProperties(r)

            objs.Add(obj1)
        Next r

        Return objs
    End Function

    Public Shared Function SearchObjectsByRight(ByVal idright As Long) As Cls_PrivilegeCollections
        Dim objs As New Cls_PrivilegeCollections
        Dim r As DataRow
        Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_ListAll_Objects_In_Right", idright)

        For Each r In ds.Tables(0).Rows
            Dim obj1 As New Cls_Privilege

            obj1.SetProperties(r)

            objs.Add(obj1)
        Next r

        Return objs
    End Function

    Public Shared Function SearchObjectsByTypeAndRight(ByVal _typeobj As String, ByVal idright As Long) As Cls_PrivilegeCollections
        Dim objs As New Cls_PrivilegeCollections
        Dim r As DataRow
        Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_ListAll_Objects_In_Right_ByType", idright, _typeobj)

        For Each r In ds.Tables(0).Rows
            Dim obj1 As New Cls_Privilege

            obj1.SetProperties(r)

            objs.Add(obj1)
        Next r

        Return objs
    End Function


    Public Shared Function SearchObjectsByTypeAndRight(ByVal _typeobj As String, ByVal IDModule As Long, ByVal idright As Long) As Cls_PrivilegeCollections
        Dim objs As New Cls_PrivilegeCollections
        Dim r As DataRow
        Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_ListAll_Objects_In_Right_ByType_ByID_Modules", idright, IDModule, _typeobj)

        For Each r In ds.Tables(0).Rows
            Dim obj1 As New Cls_Privilege

            obj1.SetProperties(r)

            objs.Add(obj1)
        Next r

        Return objs
    End Function

    Public Shared Function SearchObjetsNotAssignToRight(ByVal _typeobj As String, ByVal idright As Long) As Cls_PrivilegeCollections
        Dim objs As New Cls_PrivilegeCollections
        Dim r As DataRow
        Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_ListAll_Objets_NotIn_Right", idright, _typeobj)

        For Each r In ds.Tables(0).Rows
            Dim obj1 As New Cls_Privilege

            obj1.SetProperties(r)

            objs.Add(obj1)
        Next r

        Return objs
    End Function

    Public Shared Function SearchObjetsNotAssignToRight(ByVal _typeobj As String, ByVal IDModule As Long, ByVal idright As Long) As Cls_PrivilegeCollections
        Dim objs As New Cls_PrivilegeCollections
        Dim r As DataRow
        Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_ListAll_Objets_NotIn_Right_ByID_Modules", idright, IDModule, _typeobj)

        For Each r In ds.Tables(0).Rows
            Dim obj1 As New Cls_Privilege

            obj1.SetProperties(r)

            objs.Add(obj1)
        Next r

        Return objs
    End Function

    Public Shared Function SearchAll_ObjetSystem_ByObjectType(ByVal _ObjectType As String) As List(Of Cls_Privilege)
        Dim objs As New List(Of Cls_Privilege)
        Dim r As DataRow
        Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_ListeAll_Objets_ByObjectType", _ObjectType)

        For Each r In ds.Tables(0).Rows
            Dim obj1 As New Cls_Privilege

            obj1.SetProperties(r)

            objs.Add(obj1)
        Next r

        Return objs
    End Function

#End Region

#Region " Other Methods "
    Private Function ObjectValueExists(ByVal value As String) As Boolean
        Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_Select_Object_ByName", value)

        If ds.Tables(0).Rows.Count < 1 Then
            Return False
        Else
            If _id = 0 Then
                Return True
            Else
                If ds.Tables(0).Rows(0).Item("Object_ID") <> _id Then
                    Return True
                Else
                    Return False
                End If
            End If
        End If
    End Function

    Private Sub Validation()
        If _object = "" Then
            Throw (New System.Exception("Objet n'est pas renseigné."))
        End If

        If _ID_Modules = 0 Then
            Throw (New System.Exception("Le module n'est pas renseigné."))
        End If

        If _objectdesc = "" Then
            Throw (New System.Exception("Description Objet n'est pas renseignée."))
        End If

        If _objecttype = "" Then
            Throw (New System.Exception("Type Objet n'est pas renseigné."))
        End If

        If Len(_object) > 100 Then
            Throw (New System.Exception("Objet trop long (doit être inférieur à 100 caractères)"))
        End If

        If Len(_objectdesc) > 200 Then
            Throw (New System.Exception("Description Objet trop longue (doit être inférieure à 200 caractères)"))
        End If

        If Len(_objecttype) > 20 Then
            Throw (New System.Exception("Type d'Objet trop long (doit être inférieur à 20 caractères)"))
        End If

        If ObjectValueExists(_object) Then
            Throw (New System.Exception("Objet déjà enregistrée."))
        End If
    End Sub

    Public Shared Function VerifyRightOnObject1(ByVal ObjectRequested As String, ByVal _grpUser As Long) As Boolean
        Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_Select_Object_ByGroup_ByName", _grpUser, ObjectRequested)
        Dim valret As Boolean

        If ds.Tables(0).Rows.Count > 0 Then
            valret = True
        Else
            valret = False
        End If

        Return valret
    End Function

    Public Shared Function VerifyRightOnObject(ByVal ObjectRequested As String, ByVal _grpUser As Long) As Boolean
        Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_Select_Object_ByGroup_ByName_Actif", _grpUser, ObjectRequested)
        Dim valret As Boolean

        If ds.Tables(0).Rows.Count > 0 Then
            valret = True
        Else
            valret = False
        End If

        Return valret
    End Function

    Public Function GetObjectString() As String Implements IGeneral.GetObjectString
        Return LogStringBuilder.BuildLogString(Me)
    End Function
#End Region


End Class
