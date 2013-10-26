
function SaveEvent(object,userid) {
    //var eventDescription=
    
    var eventDate = $(object).parent().find("input[name=data]").val();
    eventDate = DateStringToDateNumber(eventDate);
    var eventPosition = $(object).parent().next().next().html();
    var eventDescription = $(object).parent().find("textarea").val();
    var eventName = $(object).parent().find("input[name=NazwaWydarzenia]").val();
    var eventLimit = $(object).parent().find("input[name=placeLimit]").val();
    var country = $(object).parent().find(".Kraj").children().text();
    var city = $(object).parent().find(".Miasto").children().text();
    var street = $(object).parent().find(".Ulica").children().text();
    if (street =="") {
        street = $(object).parent().find("input[name=Ulica]").val();

    }
    alert(eventPosition);
    var wojewodztwo = $(object).parent().find(".Wojewodztwo").children().text();
    var category = $(object).parent().find(".Kategoria").children().find('option:selected').val();
 
    alert("eventdate: "+ eventDate);

  

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Events.asmx/SaveNewEvent",
        data: "{'userid':'" + userid + "','eventDate':'" + eventDate + "','eventPosition':'" + eventPosition + "','eventDescription':'" + eventDescription + "','eventName':'" + eventName + "','eventLimit':'" + eventLimit + "','country':'" + country + "','city':'"+city+"','street':'"+street+"','category':'"+category+"'}",
        beforeSend:
            function () {

                ShowLoadingDiv();

            },
        success:
         function (msg) {
             alert('success');

             loadPopupBox2("#popup_boxEvents");
             $("#popup_boxEvents").append("Nowe wydarzenie: " + msg.d + " zostało zapisane");


         },
        complete:
           function () {
               HideDivAndOpacityToOne();
          
           },
        error:
         function (XMLHttpRequest, textStatus, errorThrown) {
             alert('error');
             HideLoadingDiv()

         }
    });
}

function limitMiejsc(object) {

    if ($(object).is(":checked")) {

        $(object).parent().next().fadeIn();

    }
    else {
        $(object).parent().next().fadeOut();
    }
}


function DateStringToDateNumber(dateString) {

    var months = {
        "STY": "01",
        "LUT": "02",
        "MAR": "03",
        "KWI": "04",
        "MAJ": "05",
        "CZE": "06",
        "LIP": "07",
        "SIE": "08",
        "WRZ": "09",
        "PAŹ": "10",
        "LIS": "11",
        "GRU":"12"
    }

    for (key in months) {

        if (dateString.indexOf(key) != -1)
            dateString = dateString.replace(key, months[key]);


    }

    return dateString;
}

function HideDivAndOpacityToOne() {

    $('#Downloading').remove()
    $("#MainContainer").css({ // this is just for style		
        "opacity": "1"
    });;

}