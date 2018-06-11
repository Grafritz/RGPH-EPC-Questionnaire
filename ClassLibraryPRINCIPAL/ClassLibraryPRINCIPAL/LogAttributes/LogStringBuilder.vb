' @Brain Development
' 13-08-2011

Imports System.Reflection
Imports System.Text

Public Class LogStringBuilder
    Public Shared Function BuildLogString(ByVal Obj As Object) As String
        Dim propInfo As PropertyInfo
        Dim propInfos() As PropertyInfo
        Dim AttColl() As AttributLogData

        propInfos = Obj.GetType.GetProperties

        Dim result(propInfos.GetLength(0)) As String
        result(0) = "Classe:" & Obj.GetType.Name
        For Each propInfo In propInfos
            AttColl = propInfo.GetCustomAttributes(AttributLogData.GetClassType, False)
            If AttColl.Length > 0 Then
                If AttColl(0).AllowLog Then
                    result(AttColl(0).Order) = String.Concat(";", propInfo.Name, ":", CStr(propInfo.GetValue(Obj, Nothing)))
                End If
            End If
        Next
        Return String.Join("", result)
    End Function

    Public Shared Function BuildLogStringChangesOnly(ByVal Obj As Object, ByVal Obj2 As Object) As String
        Dim propInfo As PropertyInfo
        Dim propInfos() As PropertyInfo
        Dim AttColl() As AttributLogData

        propInfos = Obj.GetType.GetProperties

        Dim result(propInfos.GetLength(0)) As String
        result(0) = "<span style='color:#B94A48;' ></span>" & "<span style='color:#B94A48;font-weight:bold;' >" & Obj.GetType.Name.Replace("Cls_", "").Replace("_", " ") & " :-:</span>"
        For Each propInfo In propInfos
            AttColl = propInfo.GetCustomAttributes(AttributLogData.GetClassType, False)
            If AttColl.Length > 0 Then
                If AttColl(0).AllowLog Then
                    result(AttColl(0).Order) = String.Concat("<span style='color:red;' > * </span>", "<b>" & propInfo.Name, ":</b>", CStr(propInfo.GetValue(Obj, Nothing)))

                    'If ObjectString.Equals("") Then
                    '    ObjectString &= "<b>" & _property.Name & "</b>" & ":" & _property.GetValue(_old, Nothing)
                    'Else
                    '    ObjectString &= "<br /><b>" & _property.Name & "</b>" & ":" & _property.GetValue(_old, Nothing)
                    'End If
                End If
            End If
        Next
        Return String.Join("", result)
    End Function

    Public Shared Function BuildLogStringHTML(ByVal Obj As Object) As String
        Dim propInfo As PropertyInfo
        Dim propInfos() As PropertyInfo
        Dim AttColl() As AttributLogData

        propInfos = Obj.GetType.GetProperties

        Dim result(propInfos.GetLength(0)) As String
        result(0) = "<span style='color:#B94A48;' ></span>" & "<span style='color:#B94A48;font-weight:bold;' >" & Obj.GetType.Name.Replace("Cls_", "").Replace("_", " ") & " :-:</span>"
        For Each propInfo In propInfos
            AttColl = propInfo.GetCustomAttributes(AttributLogData.GetClassType, False)
            If AttColl.Length > 0 Then
                If AttColl(0).AllowLog Then
                    result(AttColl(0).Order) = String.Concat("<span style='color:red;' > * </span>", "<b>" & propInfo.Name, ":</b>", CStr(propInfo.GetValue(Obj, Nothing)))

                    'If ObjectString.Equals("") Then
                    '    ObjectString &= "<b>" & _property.Name & "</b>" & ":" & _property.GetValue(_old, Nothing)
                    'Else
                    '    ObjectString &= "<br /><b>" & _property.Name & "</b>" & ":" & _property.GetValue(_old, Nothing)
                    'End If
                End If
                End If
        Next
        Return String.Join("", result)
    End Function
End Class
