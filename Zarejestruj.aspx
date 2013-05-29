<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Zarejestruj.aspx.cs" Inherits="Zarejestruj" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Zarejestruj sie</title>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="Logo">
        <span class="log">ktoszuka-tenznajdzie.pl</span><div class="men"></div></div>
      

       
    <div style="width:307px; margin-left:auto; margin-right:auto">
    
   
        <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" AnswerLabelText="Odpowiedź:" AnswerRequiredErrorMessage="Odpowiedź jest wymagana." CancelButtonText="Anuluj" CompleteSuccessText="Twoje konto zostało utworzone." ConfirmPasswordCompareErrorMessage="Hasło oraz potwierdzenie hasła muszą do siebie pasować." ConfirmPasswordLabelText="Potwierdzenie hasła:" ConfirmPasswordRequiredErrorMessage="Potwierdzenie hasła jest wymagane." ContinueButtonText="Kontynuuj" CreateUserButtonText="Utwórz użytkownika" DuplicateEmailErrorMessage="E-mail w użyciu. Podaj inny e-mail." DuplicateUserNameErrorMessage="Podana nazwa już w użyciu. Podaj inną nazwę użytkownika." EmailRegularExpressionErrorMessage="Podaj poprawny e-mail." EmailRequiredErrorMessage="E-mail wymagany" FinishCompleteButtonText="Utwórz" FinishPreviousButtonText="Poprzedni" InvalidAnswerErrorMessage="Proszę podać poprawne hasło" InvalidEmailErrorMessage="Proszę podać poprawny email" InvalidPasswordErrorMessage="Minimalna długość hasła: {0}. Wymagany symbol nie alfanumeryczny: {1}." InvalidQuestionErrorMessage="Proszę podać inne pytanie" PasswordLabelText="Hasło: " PasswordRegularExpressionErrorMessage="Proszę podać inne hasło" PasswordRequiredErrorMessage="Hasło jest wymagane" QuestionLabelText="Pytanie zabezpieczające:" QuestionRequiredErrorMessage="Pytanie zabezpieczające jest wymagane." UnknownErrorMessage="Twoje konto nie zostało utworzone." UserNameLabelText="Nazwa użytkownika:" UserNameRequiredErrorMessage="Nazwa użytkownika jest wymagana." ContinueDestinationPageUrl="~/Zaloguj.aspx" BackColor="#99FF99" BorderWidth="1px" Font-Bold="True" Font-Overline="False" ForeColor="White">
            <WizardSteps>
                <asp:CreateUserWizardStep runat="server" >
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td align="center" colspan="2">Zarejstruj się</td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Nazwa użytkownika:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="UserName" runat="server" BackColor="#CCFF99"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="Nazwa użytkownika jest wymagana." ToolTip="Nazwa użytkownika jest wymagana." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Hasło: </asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Password" runat="server" TextMode="Password" BackColor="#CCFF99"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Hasło jest wymagane" ToolTip="Hasło jest wymagane" ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Potwierdzenie hasła:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" BackColor="#CCFF99"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword" ErrorMessage="Potwierdzenie hasła jest wymagane." ToolTip="Potwierdzenie hasła jest wymagane." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Email" runat="server" BackColor="#CCFF99"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" ErrorMessage="E-mail wymagany" ToolTip="E-mail wymagany" ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="QuestionLabel" runat="server" AssociatedControlID="Question">Pytanie zabezpieczające:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Question" runat="server" BackColor="#CCFF99"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" ControlToValidate="Question" ErrorMessage="Pytanie zabezpieczające jest wymagane." ToolTip="Pytanie zabezpieczające jest wymagane." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer">Odpowiedź:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Answer" runat="server" BackColor="#CCFF99"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" ControlToValidate="Answer" ErrorMessage="Odpowiedź jest wymagana." ToolTip="Odpowiedź jest wymagana." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="Hasło oraz potwierdzenie hasła muszą do siebie pasować." ValidationGroup="CreateUserWizard1"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" style="color:Red;">
                                    <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:CreateUserWizardStep>
                <asp:CompleteWizardStep runat="server" >
                    <ContentTemplate>
                        <table style="color:White;background-color:#99FF99;font-size:100%;font-weight:bold;text-decoration:none;">
                            <tr>
                                <td align="center" colspan="2">&nbsp;</td>
                            </tr>
                            <tr>
                                <td>Twoje konto zostało utworzone.</td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:Button ID="ContinueButton" runat="server" CausesValidation="False" CommandName="Continue" Text="Kontynuuj" ValidationGroup="CreateUserWizard1" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:CompleteWizardStep>
            </WizardSteps>
        </asp:CreateUserWizard> </div>
    </form>
</body>
</html>
