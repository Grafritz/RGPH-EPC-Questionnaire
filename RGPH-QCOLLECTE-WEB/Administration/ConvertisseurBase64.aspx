<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ConvertisseurBase64.aspx.vb" Inherits="FireBase_ConvertisseurBase64" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>.:: Encode Base 64 ::.</title>
</head>
<body>
    <form id="form1" runat="server"> 
        <asp:Label ID="LabelMessage" runat="server"></asp:Label><br />
        <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
    <div>    
        <asp:Label ID="Label1" runat="server" Text="Text Normal"></asp:Label><br />
        <asp:TextBox ID="txtNormal" runat="server" TextMode="MultiLine" Height="120px" Width="100%"></asp:TextBox>   <br />   
        <asp:Button ID="btnEncodeBase64" runat="server" Text="EncodeBase64" />  
         <asp:Button ID="btnEncripteurEncrypt" runat="server" Text="Encrypt" /> <br /><hr /> 

        <asp:Label ID="Label2" runat="server" Text="TEXT EncodeBase64"></asp:Label><br />
        <asp:TextBox ID="txtEncodeBase64" runat="server" TextMode="MultiLine" Height="120px" Width="100%"></asp:TextBox>  <br />   
        <asp:Button ID="BtnDecodeBase64" runat="server" Text="DecodeBase64" /> <br /><hr /> 

        <asp:Label ID="Label3" runat="server" Text="TEXT DecodeBase64"></asp:Label><br />
        <asp:TextBox ID="txtDecodeBase64" runat="server" Enabled="false"  TextMode="MultiLine" Height="120px" Width="100%"></asp:TextBox> 
    </div>
    </form>
</body>
</html>
