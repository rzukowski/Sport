<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Zaloguj.aspx.cs" Inherits="Zaloguj" %>

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
        <div style="width:307px; margin-left:auto; margin-right:auto">
       </div>
          
<div id="ObszarLogowania">
 Login: <asp:TextBox ID="UserName" runat="server"></asp:TextBox><br />
   Hasło: <asp:TextBox ID="UserPass" runat="server" TextMode="Password"></asp:TextBox><br />
    Zapamiętaj <asp:CheckBox ID="RememberMe" runat="server" /><br />
          <asp:Button ID="Loguj" runat="server" OnClick="TryToLog" Text="Zaloguj" />
          </div>
          <div id="Center">
   <p><a href="odzyskajhaslo.aspx">Zapomniałem hasła</a></p>
     <p>Nie masz konta? <a href="Zarejestruj.aspx">Zarejestruj się</a></p>
              <p> <a href="ChangePass.aspx">Zresetuj hasło</a></p>
      <p><asp:Label runat="server" ID="Wrong" Visible="false" Text="Złe hasło lub login"></asp:Label>      </p>

              
              </div>
        </form>

    
   
    </body>
</html>
