<%@ Page Language="C#" AutoEventWireup="true" CodeFile="zmienhaslo.aspx.cs" Inherits="zmienhaslo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Zmień hasło</title>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            height: 56px;
        }
        .auto-style2 {
            height: 42px;
        }
    </style>
</head>

<body> <div id="Logo">
    <form id="form1" runat="server">
        <span class="log">ktoszuka-tenznajdzie.pl</span><div class="men"><asp:Menu ID="MenuGlowne" runat="server" DataSourceID="MapaSerwisu" Orientation="Horizontal" CssClass="men" EnableTheming="True">
            <DynamicHoverStyle CssClass="ZaznaczonyElementMenu" />
            <DynamicMenuItemStyle CssClass="ElementMenuRozwijanego" />
            <StaticHoverStyle CssClass="ZaznaczonyElementMenu" />
            <StaticMenuItemStyle CssClass="ElementMenuNadrzednego" />
        </asp:Menu></div></div>
    <div style="width:407px; margin-left:auto; margin-right:auto; margin-top:10px">
    <asp:changepassword ID="Changepassword1" runat="server" BackColor="#99FF99" BorderColor="#CCCC99" BorderStyle="Double" BorderWidth="1px" CancelButtonText="Anuluj" ChangePasswordButtonText="Zmień hasło" ChangePasswordFailureText="Niepoprawne hasło lub nowe hasło niepoprawne. Minimalna długość nowego hasła: {0}. Wymagany symbol: {1}." ChangePasswordTitleText="Zmiana hasła" ConfirmNewPasswordLabelText="Potwierdzenie nowego hasła:" ConfirmPasswordCompareErrorMessage="Stare i nowe hasło muszą do siebie pasować." ConfirmPasswordRequiredErrorMessage="Potwierdzenie hasła wymagane." Font-Names="Verdana" Font-Size="10pt" NewPasswordLabelText="Nowe hasło:" NewPasswordRegularExpressionErrorMessage="Proszę podać nowe hasło." NewPasswordRequiredErrorMessage="Nowe hasło jest wymagane." PasswordRequiredErrorMessage="Hasło wymagane." SuccessText="Hasło zostało zmienione." SuccessTitleText="Zmiana hasła zakończona." UserNameLabelText="Nazwa użytkownika." UserNameRequiredErrorMessage="Wymagana nazwa użytkownika." CancelDestinationPageUrl="~/Zaloguj.aspx" PasswordLabelText="Hasło:" Height="214px" Width="357px" ContinueButtonText="Kontynuuj" ContinueDestinationPageUrl="~/Default.aspx">
        <ChangePasswordTemplate>
            <table cellpadding="1" cellspacing="0" style="border-collapse:collapse;">
                <tr>
                    <td>
                        <table cellpadding="0" style="height:214px;width:357px;">
                            <tr>
                                <td align="center" colspan="2" style="color:White;background-color:#99FF99;font-weight:bold;">Zmiana hasła</td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword" Font-Bold="True" ForeColor="White">Hasło:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="CurrentPassword" runat="server" ForeColor="#99FF99" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword" ErrorMessage="Hasło wymagane." ToolTip="Hasło wymagane." ValidationGroup="Changepassword1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword" Font-Bold="True" ForeColor="White">Nowe hasło:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="NewPassword" runat="server" ForeColor="#99FF99" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword" ErrorMessage="Nowe hasło jest wymagane." ToolTip="Nowe hasło jest wymagane." ValidationGroup="Changepassword1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="auto-style2">
                                    <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword" Font-Bold="True" ForeColor="White">Potwierdzenie nowego hasła:</asp:Label>
                                </td>
                                <td class="auto-style2">
                                    <asp:TextBox ID="ConfirmNewPassword" runat="server" ForeColor="#99FF99" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword" ErrorMessage="Potwierdzenie hasła wymagane." ToolTip="Potwierdzenie hasła wymagane." ValidationGroup="Changepassword1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" Display="Dynamic" ErrorMessage="Stare i nowe hasło muszą do siebie pasować." ValidationGroup="Changepassword1"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" style="color:Red;">
                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Button ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword" Text="Zmień hasło" ValidationGroup="Changepassword1" />
                                </td>
                                <td>
                                    <asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Anuluj" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ChangePasswordTemplate>
        <TextBoxStyle ForeColor="#99FF99" />
        <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" />
        </asp:changepassword>
    </div>
        
    </form>
    <p align="center"> <a href="javascript: history.go(-1)">Powrót</a></p>
      
            <asp:SiteMapDataSource ID="MapaSerwisu" runat="server" ShowStartingNode="False" />
       
       </body>
</html>
