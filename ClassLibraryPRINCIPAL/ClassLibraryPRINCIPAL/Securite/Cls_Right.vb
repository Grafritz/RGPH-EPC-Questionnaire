' @Brain Development
' www.Rezo509.com
' Friday 11-11-2011

Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Net

Public Class Cls_Right
    Implements IGeneral

#Region " Attribut "
    Private _id As Long
    Private _nomright As String

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
    Public Property DESCRIPTION_TACHE() As String
        Get
            Return _nomright
        End Get
        Set(ByVal Value As String)
            If LCase(_nomright) <> LCase(Value) Then
                _isdirty = True
                _nomright = Value
            End If
        End Set
    End Property

    Public ReadOnly Property IsDataDirty() As Boolean
        Get
            Return _isdirty
        End Get
    End Property

    Public Shared ReadOnly Property maxId() As Long
        Get
            Return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SS_Max_IdRight"))
        End Get
    End Property
#End Region

#Region " Db Access "
    Private Function Insert(ByVal usr As String) As Integer Implements IGeneral.Insert
        _id = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SR_Insert_Right", _nomright, usr))
        Return _id
    End Function

    Private Function Update(ByVal usr As String) As Integer Implements IGeneral.Update
        Return SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SR_Update_Right", _id, _nomright, usr)
    End Function

    Public Function Read(ByVal idval As Long) As Boolean Implements IGeneral.Read
        If idval <> 0 Then
            Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_Select_Right_ById", idval)

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
        _id = TypeSafeConversion.NullSafeLong(r("Right_ID"))
        _nomright = TypeSafeConversion.NullSafeString(r("Privilege"))

        _isdirty = False
    End Sub

    Private Sub BlankProperties()
        _id = 0
        _nomright = ""

        _isdirty = False
    End Sub

    Public Sub Delete() Implements IGeneral.Delete
        SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SR_Delete_Right", _id)
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
        Dim objs As New Cls_RightCollections
        Dim r As DataRow
        Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_ListAll_Rights")

        For Each r In ds.Tables(0).Rows
            Dim obj1 As New Cls_Right

            obj1.SetProperties(r)

            objs.Add(obj1)
        Next r

        Return objs
    End Function

    Public Shared Function SearchRightsByGroup(ByVal grp As Long) As Cls_RightCollections
        Dim objs As New Cls_RightCollections
        Dim r As DataRow
        Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_ListAll_Rights_ByGroupe", grp)

        For Each r In ds.Tables(0).Rows
            Dim obj1 As New Cls_Right

            obj1.SetProperties(r)

            objs.Add(obj1)
        Next r

        Return objs
    End Function

    Public Shared Function SearchRightsNotAssignToGroup(ByVal grp As Long) As Cls_RightCollections
        Dim objs As New Cls_RightCollections
        Dim r As DataRow
        Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_ListAll_Rights_NotIn_GroupeRight", grp)

        For Each r In ds.Tables(0).Rows
            Dim obj1 As New Cls_Right

            obj1.SetProperties(r)

            objs.Add(obj1)
        Next r

        Return objs
    End Function
#End Region

#Region " Other Methods "
    Private Function RightValueExists(ByVal value As String) As Boolean
        Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_Select_Right_ByName", value)

        If ds.Tables(0).Rows.Count < 1 Then
            Return False
        Else
            If _id = 0 Then
                Return True
            Else
                If ds.Tables(0).Rows(0).Item("Right_ID") <> _id Then
                    Return True
                Else
                    Return False
                End If
            End If
        End If
    End Function

    Private Sub Validation()
        If _nomright = "" Then
            Throw (New System.Exception("La description de Droit n'est pas renseignée."))
        End If

        If Len(_nomright) > 50 Then
            Throw (New System.Exception("Description de Droit trop longue (doit être inférieure à 50 caractères)"))
        End If

        If RightValueExists(_nomright) Then
            Throw (New System.Exception("Description de Droit déjà enregistrée."))
        End If
    End Sub

    Public Overloads Function addObjets(ByVal obj As Cls_Privilege, ByVal usr As String) As Boolean
        Try
            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SR_Insert_ObjetRight", _id, obj.ID, usr)

            Return True
        Catch
            Return False
        End Try
    End Function

    Public Overloads Function addObjets(ByVal obj As String, ByVal usr As String) As Boolean
        Try
            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SR_Insert_ObjectRight_ByName_Object", _id, obj, usr)

            Return True
        Catch
            Return False
        End Try
    End Function

    Public Overloads Function removeObjets(ByVal obj As Cls_Privilege) As Boolean
        Try
            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SR_Delete_ObjectRight_ById_Groupe_AND_ById_Right", _id, obj.ID)

            Return True
        Catch
            Return False
        End Try
    End Function

    Public Overloads Function removeObjets(ByVal obj As String) As Boolean
        Try
            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SR_Delete_ObjectRight_ById_AND_NameObject", _id, obj)

            Return True
        Catch
            Return False
        End Try
    End Function

    Public Function GetObjectString() As String Implements IGeneral.GetObjectString
        Return LogStringBuilder.BuildLogString(Me)
    End Function
#End Region


End Class
