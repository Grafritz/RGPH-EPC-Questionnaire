' @Brain Development
' www.Rezo509.com
' Friday 11-11-2011

Imports System
Imports System.Collections

Public Class Cls_PrivilegeCollections
    Inherits BrainBaseArrayList

    Default Public Shadows Property Item(ByVal index As Integer) As Cls_Privilege
        Get
            Return MyBase.Item(index)
        End Get
        Set(ByVal Value As Cls_Privilege)
            MyBase.Item(index) = Value
        End Set
    End Property
End Class
