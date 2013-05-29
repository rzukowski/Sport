<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="wyslijwiadomosc.aspx.cs" Inherits="wyslijwiadomosc" Title="Untitled Page"  Trace="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p class="ulubieni">
        Wyslij wiadomość</p>
    
        Podaj tekst wiadomości:
    <p>
        <asp:TextBox ID="txtMessage" runat="server" Columns="50" Rows="5" 
            TextMode="MultiLine"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="wyslijButton" runat="server" Text="Wyślij" 
            onclick="Wyslij" />
    </p>
    <p>
        <asp:Label ID="lbl" runat="server"></asp:Label>
    </p>
    <p align="center"> <a href="javascript: history.go(-1)">Powrót</a></p>
</asp:Content>

