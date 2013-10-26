//function to give red border on selected picture in 'changePhototFromGallery' div (MasterPage)

function SelectPicture(object) {
    if (!$(object).hasClass("selected")) {
        var classOfDiv = $(object).parent();
        $(classOfDiv).children(".selected").removeClass("selected").addClass("notSelected");
        $(object).removeClass("notSelected").addClass("selected");

    }


}


function ajaxChangeProfilePic(userid) {
    var newPicPath = $(".selected").attr('src');

    $.ajaxFileUpload
    (
        {
            url: 'ChangeProfilePic.ashx',
            secureuri: false,
            dataType: 'json',
            data: { picPath: "'" + newPicPath + "'", id: "'" + userid + "'" },
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