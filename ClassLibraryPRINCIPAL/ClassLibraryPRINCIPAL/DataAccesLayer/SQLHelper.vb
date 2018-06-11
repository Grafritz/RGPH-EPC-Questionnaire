' @Brain Development
' 12-08-2011

Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Configuration

Namespace DataAccessLayer
    Public NotInheritable Class SqlHelper
        Private Sub New()
        End Sub 'New

        Private Shared Sub AttachParameters(ByVal command As SqlCommand, ByVal commandParameters() As SqlParameter)
            Dim p As SqlParameter
            For Each p In commandParameters
                'check for derived output value with no value assigned
                If p.Direction = ParameterDirection.InputOutput And p.Value Is Nothing Then
                    p.Value = Nothing
                End If
                command.Parameters.Add(p)
            Next p
        End Sub 'AttachParameters

        Private Shared Sub AssignParameterValues(ByVal commandParameters() As SqlParameter, ByVal parameterValues() As Object)

            Dim i As Short
            Dim j As Short

            If (commandParameters Is Nothing) And (parameterValues Is Nothing) Then
                'do nothing if we get no data
                Return
            End If

            ' we must have the same number of values as we pave parameters to put them in
            If commandParameters.Length <> parameterValues.Length Then
                Throw New ArgumentException("Parameter count does not match Parameter Value count.")
            End If

            'value array
            j = commandParameters.Length - 1
            For i = 0 To j
                commandParameters(i).Value = parameterValues(i)
            Next

        End Sub 'AssignParameterValues

        Private Shared Sub PrepareCommand(ByVal command As SqlCommand, _
                                          ByVal connection As SqlConnection, _
                                          ByVal transaction As SqlTransaction, _
                                          ByVal commandType As CommandType, _
                                          ByVal commandText As String, _
                                          ByVal commandParameters() As SqlParameter)

            'if the provided connection is not open, we will open it
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If

            'associate the connection with the command
            command.Connection = connection

            'set the command text (stored procedure name or SQL statement)
            command.CommandText = commandText

            'if we were provided a transaction, assign it.
            If Not (transaction Is Nothing) Then
                command.Transaction = transaction
            End If

            'set the command type
            command.CommandType = commandType

            'attach the command parameters if they are provided
            If Not (commandParameters Is Nothing) Then
                AttachParameters(command, commandParameters)
            End If

            Return
        End Sub 'PrepareCommand

        Public Overloads Shared Function ExecuteNonQuery(ByVal connectionString As String, _
                                                         ByVal commandType As CommandType, _
                                                         ByVal commandText As String, _
                                                         ByVal ParamArray commandParameters() As SqlParameter) As Integer
            'create & open a SqlConnection, and dispose of it after we are done.
            Dim cn As New SqlConnection(connectionString)
            Try
                cn.Open()
                'call the overload that takes a connection in place of the connection string
                Return ExecuteNonQuery(cn, commandType, commandText, commandParameters)
            Finally
                cn.Dispose()
            End Try
        End Function 'ExecuteNonQuery

        Public Overloads Shared Function ExecuteNonQuery(ByVal connectionString As String, _
                                                         ByVal spName As String, _
                                                         ByVal ParamArray parameterValues() As Object) As Integer
            Dim commandParameters As SqlParameter()

            'if we receive parameter values, we need to figure out where they go
            If Not (parameterValues Is Nothing) And parameterValues.Length > 0 Then
                'pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)

                commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName)

                'assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues)

                'call the overload that takes an array of SqlParameters
                Return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, commandParameters)
                'otherwise we can just call the SP without params
            Else
                Return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName)
            End If
        End Function 'ExecuteNonQuery

        Public Overloads Shared Function ExecuteNonQuery(ByVal connection As SqlConnection, _
                                                        ByVal commandType As CommandType, _
                                                        ByVal commandText As String, _
                                                        ByVal ParamArray commandParameters() As SqlParameter) As Integer

            'create a command and prepare it for execution
            Dim cmd As New SqlCommand
            Dim retval As Integer

            PrepareCommand(cmd, connection, CType(Nothing, SqlTransaction), commandType, commandText, commandParameters)

            'finally, execute the command.
            retval = cmd.ExecuteNonQuery()

            'detach the SqlParameters from the command object, so they can be used again
            cmd.Parameters.Clear()

            Return retval

        End Function 'ExecuteNonQuery

        Public Overloads Shared Function ExecuteDataset(ByVal connectionString As String, _
                                                        ByVal commandType As CommandType, _
                                                        ByVal commandText As String, _
                                                        ByVal ParamArray commandParameters() As SqlParameter) As DataSet
            'create & open a SqlConnection, and dispose of it after we are done.
            Dim cn As New SqlConnection(connectionString)
            Try
                cn.Open()

                'call the overload that takes a connection in place of the connection string
                Return ExecuteDataset(cn, commandType, commandText, commandParameters)
            Finally
                cn.Dispose()
            End Try
        End Function 'ExecuteDataset

        Public Overloads Shared Function ExecuteDataset(ByVal connectionString As String, _
                                                        ByVal spName As String, _
                                                        ByVal ParamArray parameterValues() As Object) As DataSet

            Dim commandParameters As SqlParameter()

            'if we receive parameter values, we need to figure out where they go
            If Not (parameterValues Is Nothing) And parameterValues.Length > 0 Then
                'pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName)

                'assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues)

                'call the overload that takes an array of SqlParameters
                Return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, commandParameters)
                'otherwise we can just call the SP without params
            Else
                Return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName)
            End If
        End Function 'ExecuteDataset

        Public Overloads Shared Function ExecuteDataset(ByVal connection As SqlConnection, _
                                                        ByVal commandType As CommandType, _
                                                        ByVal commandText As String, _
                                                        ByVal ParamArray commandParameters() As SqlParameter) As DataSet

            'create a command and prepare it for execution
            Dim cmd As New SqlCommand
            Dim ds As New DataSet
            Dim da As SqlDataAdapter

            PrepareCommand(cmd, connection, CType(Nothing, SqlTransaction), commandType, commandText, commandParameters)

            'create the DataAdapter & DataSet
            da = New SqlDataAdapter(cmd)

            'fill the DataSet using default values for DataTable names, etc.
            da.Fill(ds)

            'detach the SqlParameters from the command object, so they can be used again
            cmd.Parameters.Clear()

            'return the dataset
            Return ds

        End Function 'ExecuteDataset

        Public Overloads Shared Function ExecuteScalar(ByVal connectionString As String, _
                                                       ByVal commandType As CommandType, _
                                                       ByVal commandText As String, _
                                                       ByVal ParamArray commandParameters() As SqlParameter) As Object
            'create & open a SqlConnection, and dispose of it after we are done.
            Dim cn As New SqlConnection(connectionString)
            Try
                cn.Open()

                'call the overload that takes a connection in place of the connection string
                Return ExecuteScalar(cn, commandType, commandText, commandParameters)
            Finally
                cn.Dispose()
            End Try
        End Function 'ExecuteScalar

        Public Overloads Shared Function ExecuteScalar(ByVal connectionString As String, _
                                                       ByVal spName As String, _
                                                       ByVal ParamArray parameterValues() As Object) As Object
            Dim commandParameters As SqlParameter()

            'if we receive parameter values, we need to figure out where they go
            If Not (parameterValues Is Nothing) And parameterValues.Length > 0 Then
                'pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName)

                'assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues)

                'call the overload that takes an array of SqlParameters
                Return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, commandParameters)
                'otherwise we can just call the SP without params
            Else
                Return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName)
            End If
        End Function 'ExecuteScalar

        Public Overloads Shared Function ExecuteScalar(ByVal connection As SqlConnection, _
                                                       ByVal commandType As CommandType, _
                                                       ByVal commandText As String, _
                                                       ByVal ParamArray commandParameters() As SqlParameter) As Object
            'create a command and prepare it for execution
            Dim cmd As New SqlCommand
            Dim retval As Object

            PrepareCommand(cmd, connection, CType(Nothing, SqlTransaction), commandType, commandText, commandParameters)

            'execute the command & return the results
            retval = cmd.ExecuteScalar()

            'detach the SqlParameters from the command object, so they can be used again
            cmd.Parameters.Clear()

            Return retval

        End Function 'ExecuteScalar
    End Class 'SqlHelper

    Public NotInheritable Class SqlHelperParameterCache
        Public Const KeyEncoding As String = "REZO509BrainDevelopment6389KJHDJHR873LHD0287298R389738UFHHF873HHWFTWR7803HDHD76"

        Private Sub New()
        End Sub 'New 

        Private Shared paramCache As Hashtable = Hashtable.Synchronized(New Hashtable)

        Private Shared Function DiscoverSpParameterSet(ByVal connectionString As String, _
                                                       ByVal spName As String, _
                                                       ByVal includeReturnValueParameter As Boolean, _
                                                       ByVal ParamArray parameterValues() As Object) As SqlParameter()

            Dim cn As New SqlConnection(connectionString)
            Dim cmd As SqlCommand = New SqlCommand(spName, cn)
            Dim discoveredParameters() As SqlParameter

            Try
                cn.Open()
                cmd.CommandType = CommandType.StoredProcedure
                SqlCommandBuilder.DeriveParameters(cmd)
                If Not includeReturnValueParameter Then
                    cmd.Parameters.RemoveAt(0)
                End If

                discoveredParameters = New SqlParameter(cmd.Parameters.Count - 1) {}
                cmd.Parameters.CopyTo(discoveredParameters, 0)
            Finally
                cmd.Dispose()
                cn.Dispose()

            End Try

            Return discoveredParameters

        End Function 'DiscoverSpParameterSet

        Private Shared Function CloneParameters(ByVal originalParameters() As SqlParameter) As SqlParameter()

            Dim i As Short
            Dim j As Short = originalParameters.Length - 1
            Dim clonedParameters(j) As SqlParameter

            For i = 0 To j
                clonedParameters(i) = CType(CType(originalParameters(i), ICloneable).Clone, SqlParameter)
            Next

            Return clonedParameters
        End Function 'CloneParameters

        Public Shared Sub CacheParameterSet(ByVal connectionString As String, _
                                            ByVal commandText As String, _
                                            ByVal ParamArray commandParameters() As SqlParameter)
            Dim hashKey As String = connectionString + ":" + commandText

            paramCache(hashKey) = commandParameters
        End Sub 'CacheParameterSet

        Public Shared Function GetCachedParameterSet(ByVal connectionString As String, ByVal commandText As String) As SqlParameter()
            Dim hashKey As String = connectionString + ":" + commandText
            Dim cachedParameters As SqlParameter() = CType(paramCache(hashKey), SqlParameter())

            If cachedParameters Is Nothing Then
                Return Nothing
            Else
                Return CloneParameters(cachedParameters)
            End If
        End Function 'GetCachedParameterSet

        Public Overloads Shared Function GetSpParameterSet(ByVal connectionString As String, ByVal spName As String) As SqlParameter()
            Return GetSpParameterSet(connectionString, spName, False)
        End Function 'GetSpParameterSet 

        Public Overloads Shared Function GetSpParameterSet(ByVal connectionString As String, _
                                                           ByVal spName As String, _
                                                           ByVal includeReturnValueParameter As Boolean) As SqlParameter()

            Dim cachedParameters() As SqlParameter
            Dim hashKey As String

            hashKey = connectionString + ":" + spName
            If includeReturnValueParameter = True Then
                hashKey = hashKey + ":include ReturnValue Parameter"
            End If

            cachedParameters = CType(paramCache(hashKey), SqlParameter())

            If (cachedParameters Is Nothing) Then
                paramCache(hashKey) = DiscoverSpParameterSet(connectionString, spName, includeReturnValueParameter)
                cachedParameters = CType(paramCache(hashKey), SqlParameter())

            End If

            Return CloneParameters(cachedParameters)

        End Function 'GetSpParameterSet

        Public Shared Function BuildConfigDB() As String
            Dim keysize As clsAESV2.KeySize = clsAESV2.KeySize.Bits128 ' keysize.Bits128
            Dim a As New clsAESV2(keysize, KeyEncoding)

            Dim _cat As String = a.Decrypt(System.Configuration.ConfigurationManager.AppSettings("Catalog"))
            Dim _user As String = a.Decrypt(System.Configuration.ConfigurationManager.AppSettings("UserID"))
            Dim _pass As String = a.Decrypt(System.Configuration.ConfigurationManager.AppSettings("PassID"))
            Dim _server As String = System.Configuration.ConfigurationManager.AppSettings("ServerName")
            Dim _valRet As String

            _valRet = System.Configuration.ConfigurationManager.AppSettings("ConStrg") & ";data source=" & _server & ";initial catalog=" & _cat & ";user id=" & _user & ";password=" & _pass

            Return _valRet
        End Function

        Public Shared Function BuildConfigDB2() As String
            Dim keysize As clsAESV2.KeySize = clsAESV2.KeySize.Bits128 ' keysize.Bits128
            Dim a As New clsAESV2(keysize, KeyEncoding)

            Dim _cat As String = a.Decrypt(System.Configuration.ConfigurationManager.AppSettings("Catalog2"))
            Dim _user As String = a.Decrypt(System.Configuration.ConfigurationManager.AppSettings("UserID2"))
            Dim _pass As String = a.Decrypt(System.Configuration.ConfigurationManager.AppSettings("PassID2"))
            Dim _server As String = System.Configuration.ConfigurationManager.AppSettings("ServerName2")
            Dim _valRet As String

            _valRet = System.Configuration.ConfigurationManager.AppSettings("ConStrg") & ";data source=" & _server & ";initial catalog=" & _cat & ";user id=" & _user & ";password=" & _pass

            Return _valRet
        End Function


        Public Shared Function GetEncriptedDatabaseParameter(ByVal dts As DatabaseParameter) As String
            Dim keysize As clsAESV2.KeySize = clsAESV2.KeySize.Bits128 ' keysize.Bits128
            Dim a As New clsAESV2(keysize, KeyEncoding)
            Dim _tmp As String = ""

            Select Case dts
                Case DatabaseParameter.IDUtilisateur
                    _tmp = a.Decrypt(System.Configuration.ConfigurationManager.AppSettings("UserID"))
                Case DatabaseParameter.MotDePasse
                    _tmp = a.Decrypt(System.Configuration.ConfigurationManager.AppSettings("PassID"))
                Case DatabaseParameter.NomDatabase
                    _tmp = a.Decrypt(System.Configuration.ConfigurationManager.AppSettings("Catalog"))
                Case DatabaseParameter.NomServeur
                    _tmp = a.Decrypt(System.Configuration.ConfigurationManager.AppSettings("ServerName"))
            End Select

            Return _tmp
        End Function

        Public Shared Function EncryptDataTmp(ByVal _str As String) As String
            Dim keysize As clsAESV2.KeySize = clsAESV2.KeySize.Bits128 'keysize.Bits128
            Dim a As New clsAESV2(keysize, KeyEncoding)
            Dim _valRet As String

            _valRet = CType(a, clsAESV2).Encrypt(_str)

            Return _valRet
        End Function

        Public Shared Function DecryptDataTmp(ByVal _str As String) As String
            Dim keysize As clsAESV2.KeySize = clsAESV2.KeySize.Bits128 'keysize.Bits128
            Dim a As New clsAESV2(keysize, KeyEncoding)
            Dim _valRet As String

            _valRet = CType(a, clsAESV2).Decrypt(_str)

            Return _valRet
        End Function  

        Public Shared Function DecryptGbl(ByVal passedval As String) As String
            Dim keysize As clsAESV2.KeySize = clsAESV2.KeySize.Bits128 'keysize.Bits128
            Dim a As New clsAESV2(keysize, KeyEncoding)

            Dim _ret As String = a.Decrypt(passedval)

            Return _ret
        End Function

        Public Shared Function Encrypt128(ByVal passedval As String) As String
            Dim keysize As clsAESV2_2.KeySize
            keysize = clsAESV2_2.KeySize.Bits128

            Dim a = New clsAESV2_2(keysize, KeyEncoding)
            Dim _ret As String = a.Encrypt(passedval)

            Return _ret
        End Function

        Public Shared Function Decrypt128(ByVal passedval As String) As String
            Dim keysize As clsAESV2_2.KeySize
            keysize = clsAESV2_2.KeySize.Bits128

            Dim a = New clsAESV2_2(keysize, KeyEncoding)
            Dim _ret As String = a.Decrypt(passedval)

            Return _ret
        End Function

        Public Shared Function AES_Encrypt(ByVal input As String) As String
            Dim pass As String = KeyEncoding
            Dim AES As New System.Security.Cryptography.RijndaelManaged
            Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
            Dim encrypted As String = ""
            Try
                Dim hash(31) As Byte
                Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
                Array.Copy(temp, 0, hash, 0, 16)
                Array.Copy(temp, 0, hash, 15, 16)
                AES.Key = hash
                'AES.Mode = Security.Cryptography.CipherMode.ECB
                AES.Mode = System.Security.Cryptography.CipherMode.ECB
                Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
                Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(input)
                encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
                Return encrypted
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Shared Function AES_Decrypt(ByVal input As String) As String
            Dim pass As String = KeyEncoding
            Dim AES As New System.Security.Cryptography.RijndaelManaged
            Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
            Dim decrypted As String = ""
            Try
                Dim hash(31) As Byte
                Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
                Array.Copy(temp, 0, hash, 0, 16)
                Array.Copy(temp, 0, hash, 15, 16)
                AES.Key = hash
                'AES.Mode = Security.Cryptography.CipherMode.ECB
                AES.Mode = System.Security.Cryptography.CipherMode.ECB
                Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
                Dim Buffer As Byte() = Convert.FromBase64String(input)
                decrypted = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
                Return decrypted
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

    End Class 'SqlHelperParameterCache
End Namespace 'ASPNET.StarterKit.Reports.DataAccessLayer
