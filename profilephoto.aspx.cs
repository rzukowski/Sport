using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Base;
public partial class profilephoto : BaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected string UploadFolderPath = "~/photos/";


    protected void ProcessUpload(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "size", "top.$get(\"" + uploadResult.ClientID + "\").innerHTML = 'Uploaded size: " + AsyncFileUpload1.FileBytes.Length.ToString() + "';", true);
        string fileName = Server.MapPath("./") + "photos\\" + Session["username"] + "image.jpg";
        AsyncFileUpload1.FileName.ToString();
        AsyncFileUpload1.SaveAs(fileName);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "image", "top.$get(\"photo\").src = 'photos/" + Session["username"] + "image.jpg" + "';", true);
      //  ScriptManager.RegisterClientScriptBlock(AsyncFileUpload1, AsyncFileUpload1.GetType(), "img",
      //      "top.document.getElementById('photos').src='"+fileName+"';",
      //      true);
    }
}
