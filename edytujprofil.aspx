<%@ Language="C#" MasterPageFile="~/MasterPage.master" Title="Untitled Page" CodeFile="edytujprofil.aspx.cs" Inherits="edytujprofil" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>
<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.WebControls" tagprefix="asp" %>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" src="Scripts/gallery.js"></script>
    <script type="text/javascript" src="Scripts/validation_dane.js"></script>
    <script type="text/javascript" src="Scripts/ValidateUpload.js"></script>
    <script type="text/javascript">


        //funkcja do zakładek
        $(document).ready(function () {



            $('.thumb').each(function () {

                $(this).click(function () {
                    
                    loadPopupBox($(this).attr('src'),"#popup_box");

                });

            })

            //dla każdego miejsca z tabami
            $('.zakladki').each(function () {
                var $li = $(this).children('li');
                //przy wejsciu na strone ukrywamy tresc tabow i pokazujemy tylko aktywny
                $li.each(function () { //pętla po wszystkich tabach
                    var $trescTaba = $($(this).children('a').attr('href')); //pobieramy blok o id pobranym z linka-taba
                    if ($(this).hasClass('active')) { //jeżeli ten tab ma klasę aktywną
                        $trescTaba.show(); //to pobrany przed chwilą blok pokazujemy
                    } else {
                        $trescTaba.hide(); //jeżeli takiej klasy nie ma to blok ukrywamy
                    }
                });

                $li.click(function () { $(this).children('a').click() });
                //po kliknięciu na link...
                $li.children('a').click(function () {
                    //usuwamy z tabów klasę active
                    $li.removeClass('active');
                    $li.children('a').removeClass('active');
                    //ukrywamy wszystkie taby               
                    $li.each(function () {
                        $($(this).children('a').attr('href')).hide();
                    });
                    //ustawiamy klikniętemu tabowi klasę aktywną
                    $(this).parent().addClass('active');
                    $(this).addClass('active');
                    $($(this).attr('href')).show("slow");
                    //nie chcemy wykonać domyślnej akcji dla linka
                    return false;
                });
            });
        });
      

    </script>
    <script type="text/javascript" src="Scripts/validation_dane.js" ></script>
    <h2 class="ulubieni">Edytuj profil</h2>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
    </asp:ScriptManager>
    <ul class="zakladki">
        <li class="active"><a href="#Zdjecie" class="active">Zmiana zdjęcia</a></li>
        <li><a href="#Zainteresowania">Zainteresowania</a></li>
        <li><a href="#Dane">Dane osobowe</a></li>
        
        <li><a href="#Wyglad">Wygląd</a></li>
</ul>

    <div id="Wyglad">
        <P>
               <asp:UpdateProgress ID="updProgress"
        runat="server" AssociatedUpdatePanelID="UpdatePanel2">
            <ProgressTemplate>           
                 <div id="update">Zapisywanie<br /><img alt="" src="images/ajax-loader.gif" /></div> 
              <div id="greyout">    </div>   
                 
            </ProgressTemplate>
        </asp:UpdateProgress>
            <asp:UpdatePanel runat="server" id="UpdatePanel2" updatemode="Conditional">
         <ContentTemplate>
       Budowa ciała: <asp:RadioButtonList ID="DDLWyglad" runat="server" DataTextField="budowa_opis" DataValueField="budowa_id" Width="200px" AppendDataBoundItems="True"></asp:RadioButtonList>
            <p>
       Wzrost: <asp:TextBox ID="TBWzrost" runat="server" Height="22px" Width="20px"></asp:TextBox>cm
                </p>
            <p>
        Waga: <asp:TextBox ID="TBWaga" runat="server" Height="22px" Width="20px"></asp:TextBox>kg
            </p>
             <p>
        BMI: <asp:Label runat="server" id="LBmi"></asp:Label>
            </p>
            <asp:Button ID="UpdateWyglad" runat="server" Text="Zmień" OnClick="Update_Wyglad"/>
             <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    
    </asp:UpdateProgress>
             </ContentTemplate>
                
             </asp:UpdatePanel>
        </P>
    </div>

    <div id="Zainteresowania">
    <%-- Check box rozwijany ze sportami --%>
   <h4 class="center"> Sport</h4>
    <asp:UpdateProgress ID="UpdateProgress2"
        runat="server" AssociatedUpdatePanelID="UpdatePanel">
            <ProgressTemplate>   
                <div id="update">Zapisywanie<br /><img alt="" src="images/ajax-loader.gif" /></div> 
              <div id="greyout">    </div>  
            </ProgressTemplate>
        </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" id="UpdatePanel" updatemode="Conditional">
         <ContentTemplate>
     <div id="divDDL" class="divDDL" runat="server">
        Proszę wybrać…
       </div>
    <asp:Panel ID="pnlCustomers" runat="server" CssClass="MultipleSelectionDDL">
            <asp:CheckBoxList ID="cblCustomerList" runat="server" onclick="CountSelected(this)">
            </asp:CheckBoxList>
        </asp:Panel>
        <br />
        <cc1:PopupControlExtender ID="pceSelections" runat="server" TargetControlID="divDDL"
               PopupControlID="pnlCustomers" Position="Bottom" OffsetY="-1" >
        </cc1:PopupControlExtender>
    <p class="center" >
         <asp:Button ID="Button2" runat="server" Text="Zmień" OnClick="Update_User_Sport_Table"/>

        </p>
             </ContentTemplate>
        </asp:UpdatePanel>



   
        </div>
    <div id="Dane">
        <asp:UpdateProgress ID="UpdateProgress3"
        runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>           
               <div id="update">Zapisywanie<br /><img alt="" src="images/ajax-loader.gif" /></div> 
              <div id="greyout">    </div>           
            </ProgressTemplate>
        </asp:UpdateProgress>
     
        <asp:UpdatePanel runat="server" id="UpdatePanel1" updatemode="Conditional">
         <ContentTemplate>
             <cc1:CalendarExtender ID="CalendarExtender1" TargetControlId="DataUrodzin" runat="server" Format="yyyy-MM-dd"></cc1:CalendarExtender>
<p>     Data urodzin*: <asp:TextBox ID="DataUrodzin" runat="server" Height="22px" Width="100px" ClientIDMode="Static"></asp:TextBox><span id="UrodzinyS" class="Validate"></span></p>
<p>Płeć*: <asp:RadioButtonList ID="Plec" runat="server" Width="900px" RepeatLayout="Flow" RepeatDirection="Horizontal" ClientIDMode="Static"></asp:RadioButtonList><span id="PlecS" class="Validate"></span></p>
<p>Województwo*: <asp:DropDownList ID="Wojewodztwa" runat="server" Width="100px" AppendDataBoundItems="True" ClientIDMode="Static"></asp:DropDownList></p>
<p>Opis*: <asp:TextBox ID="Opis" runat="server" Width="300px" Height="60px" TextMode="multiline" ClientIDMode="Static"></asp:TextBox><span id="OpisS" class="Validate"></span></p>
             <asp:Button ID="Button1" runat="server" Text="Zmień" OnClick="UpdateDane" OnClientClick="return Validate()" ClientIDMode="Static"/> 
 
             </ContentTemplate>
           
        </asp:UpdatePanel>
        
</div>
    

         <br />                   



</p>
     <script type="text/javascript">

         //info o uploadowanym zdjęciu
         function uploadComplete(sender, args) {
             alert("Zdjęcie wrzucone!");
             var imgDisplay = $get("photo");
             window.location.reload()
             // var img = new Image();
             // img.src = "/photos/" + args.get_fileName();
             // imgDisplay.src = img.src;
         }

         //funckja do obliczania zaznaczonych kategorii sport
         function CountSelected() {
             var hdnCount = 0;
             getSelectionCount();
             ChangeDiv();

             //zmiana tekstu diva
             function ChangeDiv() {
                 if (hdnCount > 0) {
                     var div = document.getElementById('<%=divDDL.ClientID%>');
                     if (hdnCount == 1) { div.innerHTML = "1 element"; }
                     else if (hdnCount < 5) { div.innerHTML = hdnCount + " elementy"; }
                     else { div.innerHTML = hdnCount + " elementów"; }

                 }
             }
             //zliczanie zaznaczonych elementów
             function getSelectionCount() {
                 var cbl = document.getElementById('<%=pnlCustomers.FindControl("cblCustomerList").ClientID%>');
                 var browser = navigator.appName;
                 var pos = 0;
                 if (browser.indexOf("Microsoft") >= 0) {
                     pos = 0;
                 }
                 else {
                     pos = 1;
                 }
                 var tbody = cbl.childNodes[pos];
                 var length = (tbody.childNodes.length - pos);
                 hdnCount = 0;
                 for (i = 0; i < length; i++) {
                     var td = tbody.childNodes[i].childNodes[pos];
                     var chk = td.childNodes[0];
                     if (chk.checked) {
                         hdnCount++;
                     }
                 }

             }
         }

         


</script>
    <script type = "text/javascript">
        
</script> 
    <div id="Zdjecie">
            <h3 class="ulubieni" dir="rtl">Zmień zdjęcie</h3>
<h2>Zdjęcie profilowe: </h2>
  
        <br />
   
        <form id="form2">
            <asp:FileUpload id="AsyncFileUpload1" runat="server" />
    <asp:Button runat="server" id="Button3" text="Wyślij" onclick="ProcessUpload" />
    <br /><br />


        </form>
    <asp:Label ID="Label4" runat="server"></asp:Label>
   <asp:Label runat="server" Text=" " ID="Label5" />

        <h2>Galeria</h2>
   
    <asp:FileUpload id="FileUploadControl" runat="server" />
    <asp:Button runat="server" id="UploadButton" text="Wyślij" onclick="UploadButton_Click" />
    <br /><br />
    
        

        <asp:Label ID="zlyImg" runat="server"></asp:Label>
                  <asp:UpdatePanel ID="UpdatePanelGaleria" runat="server"  UpdateMode="Conditional">
        <ContentTemplate>
           <div id="insideGaleria" runat="server" >
            <asp:Label ID="coss"  runat="server"></asp:Label>
               </div>
        </ContentTemplate>


    </asp:UpdatePanel>
        </div>
    
  
     
</asp:Content>

