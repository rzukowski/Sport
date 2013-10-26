<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EventsCreate.aspx.cs" Inherits="EventsCreate" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      
<!-- 
	OR if you want to use the calendar in a right-to-left website
	just use the other CSS file instead and don't forget to switch g_jsDatePickDirectionality variable to "rtl"!
	
	<link rel="stylesheet" type="text/css" media="all" href="jsDatePick_ltr.css" />
-->

    <script type="text/javascript">
        var userid = '<%= Session["userid"].ToString() %>';

        var categories = '<%= GetEventsCategories() %>';
     
        //DO ZROBIENIA
        //dodanie listy kategori
        function CreateCategoryComboBox(categoriesString) {

            var cat = JSON.parse(categoriesString);
            ret = "<select>";
            for (key in cat) {

                ret += "<option value=\""+key+"\">" + cat[key] + "</option>";


            }

            ret += "</select>";
            return ret;

        }

        
        function StreetChange(element,prefix) {


            $(element).keyup(function () {
                
                if (!(this.value.match('^'+prefix))) {
                    this.value = prefix + this.value;
                }
            });

            $(element).blur(function () {
                
                if (!(this.value.match('^' + prefix))) {
                    this.value = prefix + this.value;
                }
            });

        }
        var map;

        function SzukajLokacji() {

            var addressArr = [$("#Miasto").val()];
            addressArr[1] = $("#Ulica").val();
            address = addressArr.join(',');

            var geocoder = new google.maps.Geocoder();
            geocoder.geocode( { 'address': address}, function(results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                   
                    map.setCenter(results[0].geometry.location);
                    placeMarker(results[0].geometry.location, map);
                } else {
                    alert("Geocode was not successful for the following reason: " + status);
                }
            });
        

        }

        var markerNumber = 0;
        var newDateCal = function () {

            new JsDatePick({
                useMode: 2,
                target: "data",
                dateFormat: "%d-%M-%Y"
                /*selectedDate:{				This is an example of what the full configuration offers.
                    day:5,						For full documentation about these settings please see the full version of the code.
                    month:9,
                    year:2006
                },
                yearsRange:[1978,2020],
                limitToToday:false,
                cellColorScheme:"beige",
                dateFormat:"%m-%d-%Y",
                imgPath:"img/",
                weekStartDay:1*/
            })
        }

        var contentForNewEvent = "<div class=\"infoWindowContent\" id='content_MARKERNUMBER' ><form>\
            <p>Nazwa: <input type=\"text\" name=\"NazwaWydarzenia\"></input></p>\
               <p>Opis: <textarea class=\"OpisText\"></textarea></p>"+
              "<p>Data:<input type=\"text\" size=\"12\" id=\"data\" name='data'></input></p>" +
              "<p>Limit miejsc:<input type=\"checkbox\" value='1' onclick=\"limitMiejsc(this)\">Tak</input></p>\
                <p style=\"display:none;\" class='miejsca'>Ilość miejsc: <input type=\"text\" name=\"placeLimit\" class=\"d\"></input></p>\
               <p class='Kraj'>Kraj: </p>\
                <p class='Miasto'>Miasto: </p>\
                <p class='Ulica'>Ulica: </p>\
                <p class='Wojewodztwo'>Województwo: </p>\
                <p class='Kategoria'>Kategoria: " + CreateCategoryComboBox(categories) + "</p>\
               </form><a onclick=\"SaveEvent(this,'"+userid+"')\">Save</a></div>";
        
        


        function initialize() {
            
            var mapOptions = {
                center: new google.maps.LatLng(52.238589, 21.025772),
                zoom: 8,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            map = new google.maps.Map(document.getElementById("map-canvas"),
                mapOptions);

            google.maps.event.addListener(map, 'click', function (event) {
                placeMarker(event.latLng, map);

            });

          

        }

        function placeMarker(location, map) {
            markerNumber++;
            var ret = "";
            var marker = new google.maps.Marker({
                position: location,
                map: map
            });

            var geocoder = new google.maps.Geocoder();
            geocoder.geocode({ 'latLng': location }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    if (results[0]) {
                        ret += "<span style=\"display:none;\" id='Address" + markerNumber + "'>";
                        ret += "<span id='FormatedAddress" + markerNumber + "'>" + results[0].formatted_address + "</span>";
                        ret += (SearchForResultsComponent(results[0], "country") != null) ? "<span id='Country" + markerNumber + "'>" + SearchForResultsComponent(results[0], "country") + "</span>" : "";
                        ret += (SearchForResultsComponent(results[0], "locality") != null) ? "<span id='City" + markerNumber + "'>" + SearchForResultsComponent(results[0], "locality") + "</span>" : "";
                        ret += (SearchForResultsComponent(results[0], "route") != null) ? "<span id='Street" + markerNumber + "'>" + SearchForResultsComponent(results[0], "route") + "</span>" : "";
                        ret += (SearchForResultsComponent(results[0], "administrative_area_level_1") != null) ? "<span id='Wojewodztwo" + markerNumber + "'>" + SearchForResultsComponent(results[0], "administrative_area_level_1") + "</span>" : "";
                        ret += (SearchForResultsComponent(results[0], "street_number") != null) ? "<span id='StreetAddress" + markerNumber + "'>" + SearchForResultsComponent(results[0], "street_number") + "</span>" : "";

                        // var street = SearchForStreet(results[0])
                        ret += "</span>";
                       


                    }
                }
            });
            
            //left click on new marker - open info window
            google.maps.event.addListener(marker, 'click', function () {
                

                var infowindow = new google.maps.InfoWindow({
                    content: contentForNewEvent.replace("_MARKERNUMBER", markerNumber) + ret + "<span style=\"display:none;\">" + location + "</div>",
                    maxWidth:800
                });
                infowindow.open(map, marker);
                google.maps.event.addListener(infowindow, 'domready', function () {
                    $(document).find('.infoWindowContent').parent().css('overflow-x', 'hidden');
                    newDateCal();

                   
                    //HERE function to get values from hidden span
                    AssignHiddenSpansToInput(markerNumber);
                });
            });

            //right click on marker to remove it
            google.maps.event.addListener(marker, 'rightclick', function () {

                this.setMap(null);
        });

            //set center to marker position
            map.panTo(location);
            
        }

        function AssignHiddenSpansToInput(markerNumber) {

            if ($("#Country" + markerNumber).text() != "") {
                var parent = $("#content" + markerNumber).find('.Kraj');
                parent.html(parent.text() + "<span>"+$("#Country" + markerNumber).text()+"</span>");
            }
            else {
                $("#content" + markerNumber).find('.Kraj').remove();

            }


            if ($("#City" + markerNumber).text() != "") {
               
                var parent = $("#content" + markerNumber).find('.Miasto');

                parent.html(parent.text() + "<span>" + $("#City" + markerNumber).text() + "</span>");
            }
            else {
               
                $("#content" + markerNumber).find('.Miasto').remove();

            }


            if ($("#Street" + markerNumber).text() != "") {
                var address = "";
                var parent = $("#content" + markerNumber).find('.Ulica');
                if ($("#StreetAddress" + markerNumber).text() != "") {
                    
                    address = $("#StreetAddress" + markerNumber).text();
                    alert(address);
                }
                if (address == "") {
                    parent.html(parent.text() + "<input name=\"Ulica\" value=\"" + $("#Street" + markerNumber).text() + "\"/>");
                    StreetChange(parent.children(),$("#Street" + markerNumber).text())
                }
                else
                    parent.html(parent.text() + "<span>" + $("#Street" + markerNumber).text() + " " + address + "</span>");
            }
            else {
                $("#content" + markerNumber).find('.Ulica').remove();

            }


            if ($("#Wojewodztwo" + markerNumber).text() != "") {
                var parent = $("#content" + markerNumber).find('.Wojewodztwo');

                parent.html(parent.text() + "<span>" + $("#Wojewodztwo" + markerNumber).text() + "</span>");
            }
            else {
                $("#content" + markerNumber).find('.Wojewodztwo').remove();

            }


        }

        function SearchForResultsComponent(results,type) {
           
            for (var ii = 0; ii < results.address_components.length; ii++) {

                if(results.address_components[ii].types[0]==type)
                    return results.address_components[ii].long_name

            }
            return null
        }


      

        google.maps.event.addDomListener(window, 'load', initialize);


    </script>
    <div id="popup_boxEvents" class="popupWindow">	
	
	<a class="popupBoxClose">X</a>	
</div>
      <div id="searchLocation"> 
          <p>Miasto: <input id="Miasto" /></p>
          <p>Ulica: <input id="Ulica" /></p>
          <a id="szukaj" onclick="SzukajLokacji()">Szukaj</a>

      </div>

    <div id="map-canvas"/>

</asp:Content>
