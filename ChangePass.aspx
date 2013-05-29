<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePass.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Logowanie</title>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
    
</head>
    
<body>
    <div id="Logo">
    
       <span class="log">ktoszuka-tenznajdzie.pl</span><div class="men"></div></div>
<form id="form1" runat="server">

    
<div id="ObszarLogowania">
   Login: <asp:TextBox ID="Username" runat="server"></asp:TextBox><br />
   Email: <asp:TextBox ID="mail" runat="server"></asp:TextBox><br />
        <asp:Button ID="Sent" OnClick="Sent_Click" runat="server" Text="Wyślij"/><br />
    <p>Powrót na <a href="Zaloguj.aspx">stronę logowania</a></p><br />
        <asp:Label runat="server" Visible="false" ID="wrong" Text="Zła nazwa użytkownika lub email"></asp:Label>

   </div> </form>

    
</body>
</html>
