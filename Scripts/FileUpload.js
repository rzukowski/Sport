
   function ajaxFileUpload(username,userid) {
   //    $("#loading")
   //.ajaxStart(function () {
   //    $(this).show();
   //})
       
       $.ajaxFileUpload
       (
           {
               url: 'AjaxFileUpload.ashx',
               secureuri: false,
               fileElementId: 'fileToUpload',
               dataType: 'json',
               data: { name: "'" + username + "'", id: "'" + userid + "'" },
               success: function (data) {
                   if (typeof (data.error) != 'undefined') {
                       if (data.error != '') {
                           // alert(data.error);
                           $("#popUpMessage").show();
                           $("#errorOnProfilePictureUploadForm").html(data.error);
                           $("#errorOnProfilePictureUploadForm").show();

                       } else {
                         //  alert(data.msg);
                           window.location.reload();

                       }
                   }
               },
               error: function () {
                   alert('error koniec');

               }
           }
       )

       return false;

   }

