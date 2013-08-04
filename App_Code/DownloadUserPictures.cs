using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Services.Protocols;
using UsrCode;
using System.Collections.Generic;

/// <summary>
/// Web method to download user pictures and show them in popUp window
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService()]
public class DownloadUserPictures : System.Web.Services.WebService {

    public DownloadUserPictures () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    public string GetUserPictures(string userid,string paginatedPageNumbers) {
        int paginatedPageNumber = Int32.Parse(paginatedPageNumbers);
        int numberOfPictures = Usr.CountAllUserPictures(userid);

        

        if (numberOfPictures != 0)
        {


            List<string> photos = Usr.GetAllUserPicturesPaginated(userid, paginatedPageNumber);
            string imgsrc = "<a class=\"popupBoxClose\" onclick=\"unloadPopupBox(changePhototFromGallery)\" >X</a>";
            
            foreach (string photo in photos)
                imgsrc += "<img class=\'photoInPopup\' src=\'" + photo + "\' onclick = \"SelectPicture(this)\" />";
            imgsrc += "<br /><a onclick=\"ajaxChangeProfilePic('"+userid+"')\" class='buttonclass'>Zmień zdjęcie</a>";
            string paginatedLinks = "";

            if(numberOfPictures>Usr.maxPhotosPerUploadNewProfilePicture)
            {
                paginatedLinks = CreatePaginatedLinks(paginatedPageNumber);

                
            }

            //in javascript function which recieve response, check if value after '|' is not equal to 0,
            //if not, then pagination links should be replaced
            return imgsrc + "|" + paginatedLinks;
        }

        return "Nie dodałeś żadnych zdjęć do galerii";
    }

    [WebMethod]
    public string CreatePaginatedLinks(int paginatedPageNumber)
    {
        string functionString = "return GetGalleryPictures(this)";

        string numberOfPicturesPaginationLinks = Usr.BuildHtmlPagination(Usr.maxPhotosPerUploadNewProfilePicture, paginatedPageNumber,functionString);
        return numberOfPicturesPaginationLinks;



    }
    
}
