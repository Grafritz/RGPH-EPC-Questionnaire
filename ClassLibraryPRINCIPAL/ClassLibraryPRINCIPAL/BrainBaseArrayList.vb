' @Brain Development
' 12-08-2011

Imports System
Imports System.ComponentModel
Imports System.Reflection
Imports System.Collections.Generic

Public Class BrainBaseArrayList
    Inherits ArrayList

    Implements IBindingList

    Private _ListNotIn As BrainBaseArrayList
    Private _sortDirection As TypeSortOrder = TypeSortOrder.Ascending

    Private _IsSorted As Boolean = False

    Public WriteOnly Property SetSortDirection() As TypeSortOrder
        Set(ByVal Value As TypeSortOrder)
            _sortDirection = Value
        End Set
    End Property

    
    Public Overridable Property ListNotMatchSearch() As BrainBaseArrayList
        Get
            Return _ListNotIn
        End Get
        Set(ByVal Value As BrainBaseArrayList)
            _ListNotIn = Value
        End Set
    End Property

    Public Overridable Sub AddIndex(ByVal [property] As System.ComponentModel.PropertyDescriptor) Implements System.ComponentModel.IBindingList.AddIndex
        'Adding Property Dynamiquely
        Throw New NotSupportedException
        ' RaiseEvent ListChanged(Me, New ListChangedEventArgs(ListChangedType.PropertyDescriptorAdded, [property]))
    End Sub

    Public Overridable Function AddNew() As Object Implements System.ComponentModel.IBindingList.AddNew
        Throw New NotSupportedException
    End Function

    Public Overridable ReadOnly Property AllowEdit() As Boolean Implements System.ComponentModel.IBindingList.AllowEdit
        Get
            Return False
        End Get
    End Property

    Public Overridable ReadOnly Property AllowNew() As Boolean Implements System.ComponentModel.IBindingList.AllowNew
        Get
            Return False
        End Get
    End Property

    Public Overridable ReadOnly Property AllowRemove() As Boolean Implements System.ComponentModel.IBindingList.AllowRemove
        Get
            Return False
        End Get
    End Property

    'TODO Greg Function
    Public Overridable Sub ApplySort(ByVal [property] As System.ComponentModel.PropertyDescriptor, ByVal direction As System.ComponentModel.ListSortDirection) Implements System.ComponentModel.IBindingList.ApplySort
        'Make Sort Operation
        Dim _Comparer As ObjectComparer
        _Comparer = New ObjectComparer([property].Name)
        Me.Sort(_Comparer)
        If _sortDirection = ListSortDirection.Descending Then
            Me.Reverse()
        End If
        '----------------------------------------------
        If _sortDirection = TypeSortOrder.Descending Then
            _sortDirection = TypeSortOrder.Ascending
        Else
            _sortDirection = TypeSortOrder.Descending
        End If
        '-----------------------------------------------
        _IsSorted = True
    End Sub

    Public Sub ApplySortSTR(ByVal _propertyname As String, ByVal _dir As String)
        Dim _Comparer As New ObjectComparer(_propertyname)
        Dim direction As System.ComponentModel.ListSortDirection

        Select Case _dir
            Case "ASC"
                direction = ComponentModel.ListSortDirection.Ascending
            Case "DESC"
                direction = ComponentModel.ListSortDirection.Descending
        End Select

        _sortDirection = direction
        Me.Sort(_Comparer)
        If _sortDirection = ListSortDirection.Descending Then
            Me.Reverse()
        End If
        '----------------------------------------------
        If _sortDirection = TypeSortOrder.Descending Then
            _sortDirection = TypeSortOrder.Ascending
        Else
            _sortDirection = TypeSortOrder.Descending
        End If
        '-----------------------------------------------
        _IsSorted = True
    End Sub

    Public Shared Sub ApplySortSTR(Of T)(ByRef plist As List(Of T), ByVal _propertyname As String, ByVal _dir As String)
        Dim _Comparer As New GenericObjectComparer(Of T)(_propertyname)
        Dim direction As System.ComponentModel.ListSortDirection

        plist.Sort(_Comparer)
        Select Case _dir
            Case "ASC"
                direction = ComponentModel.ListSortDirection.Ascending
            Case "DESC"
                direction = ComponentModel.ListSortDirection.Descending
                plist.Reverse()
        End Select
    End Sub

    'TODO Greg Function
    Public Overridable Overloads Function Find(ByVal [property] As System.ComponentModel.PropertyDescriptor, ByVal key As Object) As Integer Implements System.ComponentModel.IBindingList.Find
        Dim Contr As Object
        Dim prop As PropertyInfo
        Dim i As Integer
        i = 0
        For Each Contr In Me
            ' If [property].N Then
            prop = Contr.GetType.GetProperty([property].Name)
            If prop.GetValue(Contr, Nothing) = key Then
                Return i
            End If
            i += 1
        Next
    End Function
    'TODO Greg Function
    Public Overridable Function FindByProperty(ByVal propertyName As String, ByVal key As Object) As Integer
        Dim Contr As Object
        Dim prop As PropertyInfo
        Dim i As Integer
        i = 0
        For Each Contr In Me
            ' If [property].N Then
            prop = Contr.GetType.GetProperty(propertyName)
            If prop.GetValue(Contr, Nothing) = key Then
                Return i
            End If
            i += 1
        Next
    End Function
    'TODO Greg Function read keep all match Object in the Collection and put all the other in the _ListNotIn Collection
    Public Overridable Sub FindCollectionByProperty(ByVal propertyName As String, ByVal key As Object)
        Dim Contr As Object
        Dim prop As PropertyInfo
        Dim arrMatch As New BrainBaseArrayList 'Collection d'entier 
        Dim arrNotMatch As New BrainBaseArrayList 'Collection d'entier 
        '-----------------------------------------
        If Me.ListNotMatchSearch Is Nothing Then
            Me.ListNotMatchSearch = New BrainBaseArrayList
        End If
        Me.AddRange(Me.ListNotMatchSearch)
        If CStr(key) <> "" Then
            For Each Contr In Me
                prop = Contr.GetType.GetProperty(propertyName)
                If Not (CStr(prop.GetValue(Contr, Nothing)).ToUpper().IndexOf(CStr(key).ToUpper) >= 0) Then
                    arrNotMatch.Add(Contr)
                Else
                    arrMatch.Add(Contr)
                End If
            Next
            For Each Contr In arrNotMatch
                Me.Remove(Contr)
            Next
            Me.ListNotMatchSearch = arrNotMatch
        End If
    End Sub

    Public Overridable Sub FindCollectionByPropertyTypeDate(ByVal propertyName As String, ByVal key As Object)
        Dim Contr As Object
        Dim prop As PropertyInfo
        Dim arrMatch As New BrainBaseArrayList 'Collection d'entier 
        Dim arrNotMatch As New BrainBaseArrayList 'Collection d'entier 
        '-----------------------------------------
        If Me.ListNotMatchSearch Is Nothing Then
            Me.ListNotMatchSearch = New BrainBaseArrayList
        End If
        Me.AddRange(Me.ListNotMatchSearch)

        If CStr(key) <> "" And IsDate(key) Then
            For Each Contr In Me
                prop = Contr.GetType.GetProperty(propertyName)
                If Not (prop.GetValue(Contr, Nothing) = CDate(key)) Then
                    arrNotMatch.Add(Contr)
                Else
                    arrMatch.Add(Contr)
                End If
            Next
            For Each Contr In arrNotMatch
                Me.Remove(Contr)
            Next
            Me.ListNotMatchSearch = arrNotMatch
        End If
    End Sub

    Public Overridable ReadOnly Property IsSorted() As Boolean Implements System.ComponentModel.IBindingList.IsSorted
        Get
            Return _IsSorted
        End Get
    End Property

    Public Event ListChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Implements System.ComponentModel.IBindingList.ListChanged

    Public Overridable Sub RemoveIndex(ByVal [property] As System.ComponentModel.PropertyDescriptor) Implements System.ComponentModel.IBindingList.RemoveIndex

    End Sub

    Public Overridable Sub RemoveSort() Implements System.ComponentModel.IBindingList.RemoveSort

    End Sub

    Public Overridable ReadOnly Property SortDirection() As System.ComponentModel.ListSortDirection Implements System.ComponentModel.IBindingList.SortDirection
        Get
            Return _sortDirection
        End Get
    End Property

    Public Overridable ReadOnly Property SortProperty() As System.ComponentModel.PropertyDescriptor Implements System.ComponentModel.IBindingList.SortProperty
        Get
            Return Nothing
        End Get
    End Property

    Public Overridable ReadOnly Property SupportsChangeNotification() As Boolean Implements System.ComponentModel.IBindingList.SupportsChangeNotification
        Get
            Return True
        End Get
    End Property

    Public Overridable ReadOnly Property SupportsSearching() As Boolean Implements System.ComponentModel.IBindingList.SupportsSearching
        Get
            Return True
        End Get
    End Property

    Public Overridable ReadOnly Property SupportsSorting() As Boolean Implements System.ComponentModel.IBindingList.SupportsSorting
        Get
            Return True
        End Get
    End Property

    ' Class ObjectComparer
    Public Class ObjectComparer
        Implements IComparer

        Private _fieldName As String

        Public Sub New(ByVal FieldName As String)
            _fieldName = FieldName
        End Sub

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
            Dim propx As PropertyInfo
            Dim propy As PropertyInfo
            '------------------------
            propx = x.GetType.GetProperty(_fieldName)
            propy = y.GetType.GetProperty(_fieldName)
            '------------------------
            If propx.GetValue(x, Nothing) > propy.GetValue(y, Nothing) Then
                Return 1
            ElseIf propx.GetValue(x, Nothing) < propy.GetValue(y, Nothing) Then
                Return -1
            Else
                Return 0
            End If
        End Function
    End Class ' Class ObjectComparer

    ' Class GenericObjectComparer(Of T)
    Public Class GenericObjectComparer(Of T)
        Implements System.Collections.Generic.IComparer(Of T)


        Private _fieldName As String

        Public Sub New(ByVal FieldName As String)
            _fieldName = FieldName
        End Sub

        Public Function Compare1(ByVal x As T, ByVal y As T) As Integer Implements System.Collections.Generic.IComparer(Of T).Compare
            Dim propx As PropertyInfo
            Dim propy As PropertyInfo
            '------------------------
            propx = x.GetType.GetProperty(_fieldName)
            propy = y.GetType.GetProperty(_fieldName)
            '------------------------
            If propx.GetValue(x, Nothing) > propy.GetValue(y, Nothing) Then
                Return 1
            ElseIf propx.GetValue(x, Nothing) < propy.GetValue(y, Nothing) Then
                Return -1
            Else
                Return 0
            End If
        End Function
    End Class 'End Class GenericObjectComparer(Of T)

End Class
