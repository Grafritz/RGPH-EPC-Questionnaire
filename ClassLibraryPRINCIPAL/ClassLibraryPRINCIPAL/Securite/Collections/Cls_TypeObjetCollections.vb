' @Brain Development
' www.Rezo509.com
' Saturday 12-11-2011

Imports System
Imports System.Collections

Public Class Cls_TypeObjetCollections
    Inherits BrainBaseArrayList

    Default Public Shadows Property Item(ByVal index As Integer) As Cls_TypeObjet
        Get
            Return MyBase.Item(index)
        End Get
        Set(ByVal Value As Cls_TypeObjet)
            MyBase.Item(index) = Value
        End Set
    End Property

End Class
