' @Brain Development
' www.Rezo509.com
' Friday 03-02-2012

Imports BRAIN_DEVLOPMENT
Imports BRAIN_DEVLOPMENT.DataAccessLayer
Imports Microsoft.VisualBasic
Imports System.Collections.Generic


Public Class Cls_Statut
    Implements IGeneral

#Region " Attribut "
    Private _id As Long
    Private _Descriptions As String

    Private _isdirty As Boolean = False

    Public Enum Statut
        EnCours = 1
        Valider = 2
        Rejeter = 3
        Expirer = 4
        Annuler = 5
    End Enum
#End Region

#Region " New "
    Public Sub New()
        BlankProperties()
    End Sub

    Public Sub New(ByVal _idOne As Long)
        Read(_idOne)
    End Sub

    Public Sub New(ByVal dr As Data.DataRow)
        Me.SetProperties(dr)
    End Sub
#End Region

#Region " Properties "

    Public ReadOnly Property ID() As Long Implements BRAIN_DEVLOPMENT.IGeneral.ID
        Get
            Return _id
        End Get
    End Property

    Public Property DESCRIPTIONS() As String
        Get
            Return _Descriptions
        End Get
        Set(ByVal value As String)
            If _Descriptions <> value Then
                _isdirty = True
                _Descriptions = value
            End If
        End Set
    End Property

    Public ReadOnly Property IS_DIRTY() As Boolean
        Get
            Return _isdirty
        End Get
    End Property
#End Region

#Region " Db Access "
    Public Function Insert(ByVal usr As String) As Integer Implements BRAIN_DEVLOPMENT.IGeneral.Insert
        Return Nothing
    End Function

    Public Function Update(ByVal usr As String) As Integer Implements BRAIN_DEVLOPMENT.IGeneral.Update
        Return Nothing
    End Function

    Public Function Read(ByVal _ID_Statut As Long) As Boolean Implements BRAIN_DEVLOPMENT.IGeneral.Read
        Try
            If _ID_Statut <> 0 Then
                Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Select_Statut_ById", _ID_Statut)

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
            ErreurLog.WriteError(ex.Message)
            Throw ex
        End Try

    End Function

    Public Function Read(ByVal TitreAnn As String) As Boolean
        Try
            ''''---------- NOT YET -----------
            Dim ds As Data.DataSet
            If TitreAnn.Length <> 0 Then
                ds = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Select_Annonces_ByTitre", TitreAnn)

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
            ErreurLog.WriteError(ex.Message)
            Throw ex
        End Try
    End Function

    Private Sub SetProperties(ByVal dr As Data.DataRow)
        Try
            _id = TypeSafeConversion.NullSafeLong(dr("ID_Statut"))
            _Descriptions = TypeSafeConversion.NullSafeString(dr("Descriptions"))

            _isdirty = False
        Catch ex As Exception
            ErreurLog.WriteError(ex.Message)
            Throw ex
        End Try

    End Sub

    Private Sub BlankProperties()
        _id = 0
        _Descriptions = ""

        _isdirty = False
    End Sub

    Public Sub Delete() Implements BRAIN_DEVLOPMENT.IGeneral.Delete

    End Sub

    Public Function Refresh() As Boolean Implements BRAIN_DEVLOPMENT.IGeneral.Refresh
        If _id = 0 Then
            Return False
        Else
            Read(_id)
            Return True
        End If
    End Function

    Public Function Save(ByVal usr As String) As Integer Implements BRAIN_DEVLOPMENT.IGeneral.Save
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
    Public Function Search() As System.Collections.ICollection Implements BRAIN_DEVLOPMENT.IGeneral.Search
        Return Nothing
    End Function

    Public Shared Function ConvertBool_To_OuiNon(ByVal Statut As Boolean)
        Dim _Str As String = ""
        If Statut Then
            _Str = "<span style='color:Green;'><b>Oui</b></span>"
        Else
            _Str = "<span style='color:red;'><b>Non</b></span>"
        End If
        Return _Str
    End Function

    Public Shared Function ConvertBool1_To_OuiNon(ByVal Statut As Boolean)
        Dim _Str As String = ""
        If Not Statut Then
            _Str = "<span style='color:Green;'><b>Oui</b></span>"
        Else
            _Str = "<span style='color:red;'><b>Non</b></span>"
        End If
        Return _Str
    End Function

    Public Shared Function Statut_String(ByVal Statut As Long)
        Dim _Str As String = ""
        Select Case Statut
            Case Cls_Statut.Statut.EnCours
                _Str = "<span style='color:red;'><b>En Cours</b></span>"
            Case Cls_Statut.Statut.Valider
                _Str = "<span style='color:Green;'><b>Valider</b></span>"
            Case Cls_Statut.Statut.Rejeter
                _Str = "<span style='color:IndianRed;'><b>Rejeter</b></span>"
            Case Cls_Statut.Statut.Expirer
                _Str = "<span style='color:LightSkyBlue;'><b>Expirer</b></span>"
            Case Cls_Statut.Statut.Annuler
                _Str = "<span style='color:red;'><b>En Cours</b></span>"
        End Select
        Return _Str
    End Function

    Public Shared Function Statut_Image(ByVal _Statut As Boolean)
        If _statut Then
            Return "~/images/accept.png"
        Else
            Return "~/images/cancel.png"
        End If
    End Function

#End Region

#Region " Other Methods "

    Private Function FoundName() As Boolean
        Try
            '    Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(SqlHelperParameterCache.BuildConfigDB(), "SP_Select_Annonces_ByTitre", _Description)
            '    If ds.Tables(0).Rows.Count < 1 Then
            '        Return False
            '    Else
            '        If _id = 0 Then
            '            Return True
            '        Else
            '            If ds.Tables(0).Rows(0).Item("ID_Annonce") <> _id Then
            '                Return True
            '            Else
            Return False
            '            End If
            '        End If
            '    End If
        Catch ex As Exception
            ErreurLog.WriteError(ex.Message)
            Throw ex
        End Try

    End Function

    Private Sub Validation()
        If _Descriptions = "" Then
            Throw (New System.Exception("Description non renseignée"))
        End If

        If FoundName() Then
            Throw (New System.Exception("Description déjà !!!"))
        End If
    End Sub

    Public Function GetObjectString() As String Implements IGeneral.GetObjectString
        Return LogStringBuilder.BuildLogString(Me)
    End Function

    Public Shared Function UTF8_WORD(ByVal Sentences As String) As String
        Dim val As String = ""
        val = Sentences.Replace("à", "a").Replace("â", "a").Replace("à", "a").Replace("å", "a").Replace("À", "a").Replace("Á", "a").Replace("Â", "a").Replace("Ã", "a").Replace("Ä", "a").Replace("Å", "a")
        val = val.Replace("é", "e").Replace("è", "e").Replace("ê", "e").Replace("ë", "e").Replace("È", "e").Replace("É", "e").Replace("ē", "e").Replace("Ē", "e").Replace("Ë", "e").Replace("Ê", "e")
        val = val.Replace("ò", "o").Replace("ó", "o").Replace("ô", "o").Replace("õ", "o").Replace("ö", "o").Replace("Ò", "o").Replace("Ó", "o").Replace("Ô", "o").Replace("Õ", "o").Replace("Ö", "o")
        val = val.Replace("ï", "i").Replace("ì", "i").Replace("í", "i").Replace("î", "i")
        val = val.Replace("ù", "u").Replace("ú", "u").Replace("û", "u").Replace("ü", "u").Replace(".", "").Replace("%", "").Replace("#", "").Replace("@", "").Replace("$", "").Replace("æ", "").Replace("↔", "")
        val = val.Replace("'", "").Replace("ç", "c").Replace("+", "_").Replace("*", "_").Replace("\", "").Replace("/", "").Replace("(", "").Replace(")", "").Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", "").Replace("|", "")
        val = val.Replace("<", "").Replace(">", "").Replace(" ", "_").Replace("""", "").Replace("^", "").Replace("`", "").Replace("~", "").Replace("!", "")
        val = val.Replace(":", "").Replace("___", "-").Replace("__", "-") '.Replace("_", "-")
        Return val
    End Function
#End Region

End Class
