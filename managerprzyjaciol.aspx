<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" Title="Untitled Page" Inherits="managerprzyjaciol" CodeFile="managerprzyjaciol.aspx.cs"%>

<script runat="server">

   
</script>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>Zarządzanie ulubionymi</h2>
    <p>Wybierz profil do usunięcia z ulubionych :
        <asp:DropDownList ID="ddlFriends" runat="server" 
            DataSourceID="SqlDataSource1" DataTextField="UserName" 
            DataValueField="userid" Height="22px" Width="150px">
        </asp:DropDownList>
&nbsp;<asp:Button ID="btnDelete" runat="server" onclick="btnDelete_Click" 
            Text="Delete" />
  </p>
    <p>
        <asp:Label ID="lbl" runat="server"></asp:Label>
    </p>
    <p>&nbsp;</p>
   <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:FriendsConnectionString %>" SelectCommand="select userid, UserName from aspnet_Users
where userid in (select friendid from friends where userid = @userid)">
        <SelectParameters>
            <asp:SessionParameter Name="userid" SessionField="userid" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

