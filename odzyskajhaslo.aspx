<%@ Page Language="C#" AutoEventWireup="true" CodeFile="odzyskajhaslo.aspx.cs" Inherits="odzyskajhaslo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Odzyskaj hasło</title>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div id="Logo">
        <span class="log">ktoszuka-tenznajdzie.pl</span></div>
    <form id="form1" runat="server">
    <div style="width:348px; margin-left:auto; margin-right:auto; margin-top:10px">
        <asp:PasswordRecovery ID="PasswordRecovery1" runat="server" AnswerLabelText="Odpowiedź:" AnswerRequiredErrorMessage="Odpowiedź jest wymagana." GeneralFailureText="Niepowodzenie. Spróbuj podobnie." QuestionInstructionText="" QuestionLabelText="Pytanie:" SubmitButtonText="Wyślij" SuccessText="Hasło zostało wysłane." UserNameFailureText="Proszę spróbować ponownie." UserNameInstructionText="Podaj nazwę użytkownika aby odzyskać hasło." UserNameLabelText="Nazwa użytkownika:" UserNameRequiredErrorMessage="Nazwa użytkownika jest wymagana." UserNameTitleText="Zapomniałeś hasła?" BackColor="#99FF99" BorderColor="#CCCC99" Font-Bold="True" ForeColor="White" Height="130px" Width="345px" BorderWidth="1px" QuestionFailureText="Zła odpowiedź" QuestionTitleText="Potwierdzenie tożsamości"></asp:PasswordRecovery>
    </div>
    </form>
    <p align="center"> <a href="Zaloguj.aspx">Powrót</a></p>
</body>
</html>
