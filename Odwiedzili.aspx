<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPage.master" Title="Untitled Page" CodeFile="Odwiedzili.aspx.cs" Inherits="Odwiedzili" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <script type="text/jscript" src="Scripts/OnHoverShowOpis.js"></script>
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
            <asp:ListView ID="ListView1" runat="server"  groupitemcount = "1" border-width="0" >
     <LayoutTemplate>
            <div id="users" >
            <div id="groupPlaceHolder" runat="server">
             </div>
            </div>
          </LayoutTemplate>
          <GroupTemplate>
            <div id="itemPlaceHolder" runat="server">
            </div>
          </GroupTemplate>
  
          <ItemTemplate>

                <div class="OdwiedziliList">
                    
            <span class="Username"><%# Eval("username") %></span>
               <p>Odwiedził: <%# Eval("Data") %></p>
             <img src='photos/<%# Eval("username") %>image.jpg' alt="NoPhoto" id='<%# Eval("username") %>p' class="UserPhoto" width="100px" height="100px" />  
                    <ul class="ListedUserAttr">
                        <li>Wiek: <asp:Label ID="Label1" runat="server" Text='<%# Eval("Wiek") %>'></asp:Label></li>
                        <li>Województwo: <asp:Label ID="Wojewodztwo" runat="server" Text='<%# Eval("wojewodztwo") %>'></asp:Label></li>
                        <li>Płeć: <asp:Label ID="Plec" runat="server" Text='<%# Eval("plec") %>'></asp:Label></li>
             <li> <a href='showprofile.aspx?userid=<%# Eval("userid") %>&<%# Eval("username") %>'>Pokaż profil</a></li>
                
           <li>  <a href='wyslijwiadomosc.aspx?userid=<%# Eval("userid") %>'>Wyślij wiadomość</a></li>

                        </ul>
                   <p class="opis" id='<%# Eval("username") %>'><%# Eval("opis") %></p>
                  </div>

              
          </ItemTemplate>
    </asp:ListView>

            <div id="links" runat="server"></div>
        </ContentTemplate>
  

        </asp:UpdatePanel>

  
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:FriendsConnectionString %>" 
            SelectCommand="PokazOdwiedzone" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:SessionParameter Name="userid" SessionField="userid" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
</asp:Content>
