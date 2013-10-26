
//to manage profile picture
function BindEventsGallery() {
    $(document).ready(function () {

        $('html').click(function () {

           // HideProfilePictureMenu();
        });
        
        $("#photoDiv").mouseover(function () {
          

            $("#showProfilePictureMenu").show();


        }).mouseout(function () {

           // $("#profilePictureMenu").removeClass("active");
            $("#showProfilePictureMenu").hide();
        });



        $('.popupBoxClose').click(function () {
            unloadPopupBox($(this).closest('div'));
        });

       
        $("#profilPictureClickMenu").click(function () {

            if ($("#profilePictureMenu").hasClass("active")) {
                $("#profilePictureMenu").removeClass("active");

            }
            else {

                $("#profilePictureMenu").addClass("active");

            }

        });

        $("#profilPictureClickMenu").click(function () {
            //if ($("#profilePictureMenu").is(":visible"))
            //    $("#profilePictureMenu").fadeOut("slow");

        });




    });
}

/// invoke scriptservice.asmx
function getData(serviceURL, controlLocation, divId, username,userid) {

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: serviceURL,
        data: "{'controlLocation':'" + controlLocation + "', 'username':'"+username+"','userid':'"+userid+"'}",
        beforeSend:
            function(){

                ShowLoadingDiv();

            },
        success:
         function (msg) {
     
             HideLoadingDiv()
             $(divId).empty();
             $(divId).append(msg.d);


         },
          complete:
             function () {
                
                 loadPopupBox2(divId);
             },
        error:
         function (XMLHttpRequest, textStatus, errorThrown) {
             alert('error');
             HideLoadingDiv()
            
         }
    });
}

function getData2(serviceURL,divId,paginatedPage,userid) {

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: serviceURL,
        data: "{'userid':'" + userid + "','paginatedPageNumbers':'" + paginatedPage + "'}",
        beforeSend:
            function () {

                ShowLoadingDiv();

            },
        success:
         function (msg) {

             HideLoadingDiv()
             $(divId).empty();

             $(divId).append(msg.d);
           

         },
        complete:
           function () {

               loadPopupBox2(divId);
           },
        error:
         function (XMLHttpRequest, textStatus, errorThrown) {
             alert('error');
             HideLoadingDiv()

         }
    });
}

function ShowMenu(object) {

    object.append("#vertmenu");
    $("#vertmenu").show();


}

function HideDiv(divId) {

    $(divId).fadeOut("fast");

}


function unloadPopupBox(divId) {	// TO Unload the Popupbox
    $(divId).fadeOut("fast");
    $(divId).find('img').remove();
    $("#MainContainer").css({ // this is just for style		
        "opacity": "1"
    });
}

function loadPopupBox(img,divId) {// To Load the Popupbox

    $(divId).append("<img src='" + img + "'/>");

    $(divId).fadeIn("slow");

    $("#MainContainer").css({ // this is just for style
        "opacity": "0.3"
    });
}


function loadPopupBox2(divId) {// To Load the Popupbox
    alert('hej');
    $(divId).fadeIn("slow");

    $("#MainContainer").css({ // this is just for style
        "opacity": "0.3"
    });
}


function ShowLoadingDiv() {

    $('body').append('<div id="Downloading"><br /><img alt="" src="images/ajax-loader.gif" /></div>');
    $("#MainContainer").css({ // this is just for style
        "opacity": "0.3"
    });


}

function HideLoadingDiv() {
    
    $('#Downloading').remove();

}


function BindChangeProfilePictureFromGallery() {






}

