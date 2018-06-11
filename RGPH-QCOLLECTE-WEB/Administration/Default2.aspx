<%@ Page Language="VB" %>

<!DOCTYPE html>

<script runat="server">

    Protected Sub Page_Load(sender As Object, e As EventArgs)
        
        Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri
        ' http://localhost:1302/TESTERS/Default6.aspx

        Dim path As String = HttpContext.Current.Request.Url.AbsolutePath
        '// /TESTERS/Default6.aspx

        Dim host As String = HttpContext.Current.Request.Url.Host
        '// localhost

        'Response.Write("<br/> <hr /> Host: " + HttpContext.Current.Request.Url.Host)
        'Response.Write("<br/> <hr /> Authority: " + HttpContext.Current.Request.Url.Authority)
        'Response.Write("<br/><hr />  AbsolutePath: " + HttpContext.Current.Request.Url.AbsolutePath)
        'Response.Write("<br/><hr />  ApplicationPath: " + HttpContext.Current.Request.ApplicationPath)
        'Response.Write("<br/><hr />  AbsoluteUri: " + HttpContext.Current.Request.Url.AbsoluteUri)
        'Response.Write("<br/> <hr /> PathAndQuery: " + HttpContext.Current.Request.Url.PathAndQuery)

        Dim strPathAndQuery As String = HttpContext.Current.Request.Url.PathAndQuery
        Dim strUrl As String = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/")
        'Response.Write("<br /><hr /> strUrl(PathAndQuery): " + strUrl)
        'Response.Write("<br /><hr /> getServerName_VirtualName: " + getServerName_VirtualName())

    End Sub
    
    Public Shared Function getIPAdresse() As String
        Dim strHostName As String
        Dim strIPAddress As String
        strHostName = System.Net.Dns.GetHostName()
        strIPAddress = System.Net.Dns.GetHostByName(strHostName).AddressList(0).ToString()
        Return strIPAddress
    End Function

    Public Shared Function getHostName() As String
        Return System.Net.Dns.GetHostName()
    End Function

    Public Shared Function AppVirtualPath() As String
        Return System.Web.HttpRuntime.AppDomainAppVirtualPath
    End Function

    Public Shared Function getServerName_VirtualName() As String
        Dim strPathAndQuery As String = HttpContext.Current.Request.Url.PathAndQuery
        Dim strUrl As String = HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/")
        Dim VirtualPath As String = System.Web.HttpRuntime.AppDomainAppVirtualPath.Replace("/", "")
        If Not VirtualPath.Trim.Equals("") Then
            strUrl &= VirtualPath & "/"
        End If
        Return strUrl
    End Function

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    
    This file list some runtime information on the server .
		<hr>
    <table width="100%" border="1" cellpadding="4" cellspacing="0" style="border-collapse: collapse;">
        <tr>
            <td style="background-color: steelblue; color: white">Name</td>
            <td style="background-color: steelblue; color: white">Value</td>
        </tr>
        <tr>
            <td>GC.GetTotalMemory(true)</td>
            <td><%=GC.GetTotalMemory(true).ToString("###,###")%></td>
        </tr>
        <tr>
            <td>DateTime.Now</td>
            <%
                Dim timezone__1 As Integer = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours
            %>
            <td><%=DateTime.Now%>
					(
					<%--<%=(If(timezone__1 >= 0, "+", "-")) + timezone__1 %>
					<%=(IIf(timezone__1 >= 0, "+", "-")) + timezone__1 %>--%>
					)
            </td>
        </tr>
        <tr>
            <td>DN Version</td>
            <td><%=Environment.Version%></td>
        </tr>
        <tr>
            <td>OS Version</td>
            <td><%=Environment.OSVersion%></td>
        </tr>
        <tr>
            <td>CurrentCulture</td>
            <td><%=System.Globalization.CultureInfo.CurrentCulture%></td>
        </tr>
        <tr>
            <td>CurrentUICulture</td>
            <td><%=System.Globalization.CultureInfo.CurrentUICulture%></td>
        </tr>
        <tr>
            <td>ASP.NET - HTTP_HOST</td>
            <td><%=Request.ServerVariables("HTTP_HOST")%></td>
        </tr>
        <tr>
            <td>ASP.NET - SERVER_NAME</td>
            <td><%=Request.ServerVariables("SERVER_NAME")%></td>
        </tr>
        <tr>
            <td>ASP.NET - SERVER_SOFTWARE</td>
            <td><%=Request.ServerVariables("SERVER_SOFTWARE")%></td>
        </tr>
        <tr>
            <td>ASP.NET - AppDomainAppVirtualPath</td>
            <td><%=System.Web.HttpRuntime.AppDomainAppVirtualPath%></td>
        </tr>
        <tr>
            <td style="background-color: steelblue; color: white">Name</td>
            <td style="background-color: steelblue; color: white">Value</td>
        </tr>
        <tr>
            <td>AbsoluteUri </td>
            <td><%=HttpContext.Current.Request.Url.AbsoluteUri%></td>
        </tr>
        <tr>
            <td>getServerName_VirtualName()</td>
            <td><%=getServerName_VirtualName()%></td>
        </tr>
        <tr>
            <td> AbsolutePathtd</td>
            <td><%=HttpContext.Current.Request.Url.AbsolutePath%></td>
        </tr>
        <tr>
            <td>PathAndQuery </td>
            <td><%=HttpContext.Current.Request.Url.PathAndQuery%></td>
        </tr>
        <tr>
            <td>HttpContext.Current.Request.Url.Host</td>
            <td><%=HttpContext.Current.Request.Url.Host%></td>
        </tr>
        <tr>
            <td>Authority </td>
            <td><%=HttpContext.Current.Request.Url.Authority%></td>
        </tr>
        <tr>
            <td>HttpContext.Current.Request.ApplicationPath </td>
            <td><%=HttpContext.Current.Request.ApplicationPath%></td>
        </tr>
        <tr>
            <td>getHostServerName</td>
            <td><%=[Global].getHostServerName()%></td>
        </tr>
    </table>
    </form>
</body>
</html>
