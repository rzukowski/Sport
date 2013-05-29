<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" Title="Untitled Page" CodeFile="showprofile.aspx.cs" Inherits="showprofile"%>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" src="Scripts/gallery.js"></script>
    
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:FriendsConnectionString %>" 
        SelectCommand="SzczegolyProf" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:QueryStringParameter Name="userid" QueryStringField="userid" 
                Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:FormView ID="FormView1" runat="server" DataSourceID="SqlDataSource1" OnItemCreated="FillLabels">
        <ItemTemplate>
           <h3><%# Eval("username") %></h3>
            <img src='photos/<%# Eval("username") %>image.jpg' alt="No Photo" width="100px" height="100px" />
            <p />
            <span class="black">Płeć:</span> <%# Eval("plec") %>
            <br />
            <span class="black">Wojewodztwo: <%# Eval("wojewodztwo") %><br />

          <span class="black">Wiek:</span> <%# Eval("Wiek") %>
            <br />
                <asp:Label ID="Wyglad" runat="server"><br /></asp:Label>
                
                <asp:Label ID="Wzrost" runat="server"><br /></asp:Label>
                
                <asp:Label ID="Waga" runat="server"> <br /></asp:Label>
                
           <span class="black">O mnie:</span> <%# Eval("opis") %><br />
            <span class="black">Uprawiany sport:</span> <asp:Label ID="Sporty" runat="server"></asp:Label><br />
            <a href='wyslijwiadomosc.aspx?userid=<%# Eval("userid") %>'> Wyslij wiadomość</a>
         </ItemTemplate>
        
</asp:FormView>
    <div runat="server" id="insideGaleria"></div>
    <p align="center">
    <a href="javascript: history.go(-1)">Powrót</a></p>

</asp:Content>
