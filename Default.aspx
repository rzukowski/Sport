<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="Untitled Page" %>

<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.WebControls" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
    $(document).ready(function () {
            BindEvents();
    });
        </script>
    <script type="text/jscript" src="Scripts/OnHoverShowOpis.js"></script>
    <asp:Panel ID="Panel1" BorderStyle="None" runat="server">
        <p class="ulubieni">Ulubieni</p>
        <hr width="50%" />
   <asp:ListView ID="ListView1" runat="server" DataSourceID="SqlDataSource1" groupitemcount = "3">
     <LayoutTemplate>
            <table cellpadding="5pt" id="users">
            <tr id="groupPlaceHolder" runat="server">
             </tr>
            </table>
          </LayoutTemplate>
          <GroupTemplate>
            <tr>
             <td id="itemPlaceHolder" runat="server" />
            </td>
            </tr>
          </GroupTemplate>
  
          <ItemTemplate>
            <td id="prof">
                <h4>
                  <%# Eval("username") %></h4>
                 <img src='photos/<%# Eval("username") %>image.jpg' alt="" id='<%# Eval("username") %>p' width="100px" height="100px" />  <br />
              <a href='showprofile.aspx?userid=<%# Eval("userid") %>&<%# Eval("username") %>'>Pokaż profil</a>
                <br />
              <p class="opis" id='<%# Eval("username") %>'><%# Eval("opis") %></p>
              
              <a href='wyslijwiadomosc.aspx?userid=<%# Eval("userid") %>'>Wyślij wiadomość</a>
              </td>
          </ItemTemplate>
    </asp:ListView>
    <p/>
   
    <p/>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:FriendsConnectionString %>" 
            SelectCommand="PokazUlubione" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:SessionParameter Name="userid" SessionField="userid" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    </p>
<p>
        </asp:Panel>

</asp:Content>

