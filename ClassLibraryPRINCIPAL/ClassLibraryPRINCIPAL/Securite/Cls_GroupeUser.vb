' @Brain Development
' www.Rezo509.com
' Saturday 05-11-2011

Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Net

Public Class Cls_GroupeUser
    Implements IGeneral

#Region " Attribut "
    Private _id As Long
    Private _Groupe_Description As String
    Private _PageDefault As String

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
    Public Property GROUPE_DESCRIPTION() As String
        Get
            Return _Groupe_Description
        End Get
        Set(ByVal Value As String)
            If LCase(_Groupe_Description) <> LCase(Value) Then
                _isdirty = True
                _Groupe_Description = Value
            End If
        End Set
    End Property

    Public Property PageDefault() As String
        Get
            Return _PageDefault
        End Get
        Set(ByVal Value As String)
            If LCase(_PageDefault) <> LCase(Value) Then
                _isdirty = True
                _PageDefault = Value
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
            Return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SS_Max_IdGroupe"))
        End Get
    End Property
#End Region

#Region " Db Access "
    Private Function Insert(ByVal usr As String) As Integer Implements IGeneral.Insert
        _id = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelperParameterCache.BuildConfigDB(), "SR_Insert_Groupe", _Groupe_Description, PageDefault, usr))
        Return _id
    End Function

    Private Function Update(ByVal usr As String) As Integer Implements IGeneral.Update
        Return SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SR_Update_Groupe", _id, _Groupe_Description, PageDefault, usr)
    End Function

    Public Function Read(ByVal idval As Long) As Boolean Implements IGeneral.Read
        If idval <> 0 Then
            Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_Select_GroupeUser_ById", idval)

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

    Private Sub SetProperties(ByVal dr As DataRow)
        _id = TypeSafeConversion.NullSafeLong(dr("ID_Group"))
        _Groupe_Description = TypeSafeConversion.NullSafeString(dr("Groupe_Description"))
        _PageDefault = TypeSafeConversion.NullSafeString(dr("PageDefault"))

        _isdirty = False
    End Sub

    Private Sub BlankProperties()
        _id = 0
        _Groupe_Description = ""
        _PageDefault = ""

        _isdirty = False
    End Sub

    Public Sub Delete() Implements IGeneral.Delete
        SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SR_Delete_Groupe", _id)
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
        Dim objs As New Cls_GroupeUserCollections
        Dim r As DataRow
        Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_ListAll_Groupe")

        For Each r In ds.Tables(0).Rows
            Dim obj1 As New Cls_GroupeUser

            obj1.SetProperties(r)

            objs.Add(obj1)
        Next r

        Return objs
    End Function

    Public Shared Function SearchAll() As List(Of Cls_GroupeUser)
        Dim objs As New List(Of Cls_GroupeUser)
        Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_ListAll_Groupe")
        Dim r As Data.DataRow

        For Each r In ds.Tables(0).Rows
            Dim obj As New Cls_GroupeUser

            obj.SetProperties(r)

            objs.Add(obj)
        Next r

        Return objs
    End Function
#End Region

#Region " Other Methods "
    Private Function GroupeValueExists(ByVal value As String) As Boolean
        Dim ds As DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SR_Select_Groupe_ByName", value)

        If ds.Tables(0).Rows.Count < 1 Then
            Return False
        Else
            If _id = 0 Then
                Return True
            Else
                If ds.Tables(0).Rows(0).Item("ID_Group") <> _id Then
                    Return True
                Else
                    Return False
                End If
            End If
        End If
    End Function

    Private Sub Validation()
        If _Groupe_Description = "" Then
            Throw (New System.Exception("La description de Groupe n'est pas renseignée."))
        End If

        If Len(_Groupe_Description) > 50 Then
            Throw (New System.Exception("Description de Groupe trop longue (doit être inférieure à 50 caractères)"))
        End If

        If GroupeValueExists(_Groupe_Description) Then
            Throw (New System.Exception("Description de Groupe déjà enregistrée."))
        End If
    End Sub

    Public Overloads Function addRights(ByVal rgt As Cls_Right, ByVal usr As String) As Boolean
        Try
            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SR_Insert_Groupe_Right", _id, rgt.ID, usr)

            Return True
        Catch
            Return False
        End Try
    End Function

    Public Overloads Function addRights(ByVal rgt As String, ByVal usr As String) As Boolean
        Try
            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SR_Insert_Groupe_Right_ByName_Right", _id, rgt, usr)

            Return True
        Catch
            Return False
        End Try
    End Function

    Public Overloads Function removeRights(ByVal rgt As Cls_Right) As Boolean
        Try
            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SR_Delete_Groupe_Right_ById_Groupe_AND_ById_Right", _id, rgt.ID)

            Return True
        Catch
            Return False
        End Try
    End Function

    Public Overloads Function removeRights(ByVal rgt As String) As Boolean
        Try
            SqlHelper.ExecuteNonQuery(SqlHelperParameterCache.BuildConfigDB(), "SR_Delete_Groupe_Right_ById_AND_NameRight", _id, rgt)

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
