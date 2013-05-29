<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" Title="Wiadomosci" CodeFile="wiadomosci.aspx.cs" Inherits="wiadomosci"%>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>

<script runat="server">

</script>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



    <h2>Skrzynka pocztowa</h2>


    <asp:ScriptManager   
            ID="ScriptManager1"  
            runat="server"  
            >  
        </asp:ScriptManager>  
    <asp:UpdatePanel runat="server" id="UpdatePanel1" updatemode="Conditional">
        <ContentTemplate>
            <script type="text/javascript">
                Sys.Application.add_load(BindEvents);
     </script>
    <asp:DataList ID="DataList1" runat="server" Width="100%">
        <ItemTemplate>
          <table width="100%" border="1">
          <tr>
          <td width="100px">
          <img src='photos/<%# Eval("username") %>image.jpg' alt="No Photo" width="100px" height="100px" />
          </td>
          <td>
          Wiadomość od: <span class="wiadod"> <a href='showprofile.aspx?userid=<%# Eval("userid") %>&username=<%# Eval("username") %>'><%# Eval("username") %></a></span>   [<%# Eval("sentdate") %>]
          <pre><%# Eval("mssg") %></pre>
          <a href='wyslijwiadomosc.aspx?userid=<%# Eval("userid")%>'>Odpowiedz</a> 
          &nbsp;
          <a href='usunwiadomosc.aspx?msgid=<%# Eval("msgid")%>'>Usuń</a>
          </td>
          </tr>
          </table>
        </ItemTemplate>
    </asp:DataList>
            <asp:HiddenField runat="server" ID="CurrPage" />
             <div id="links" runat="server"></div>
        </ContentTemplate>
  

        </asp:UpdatePanel>

    <p align="center"> <a href="javascript: history.go(-1)">Powrót</a></p>
    
</asp:Content>

