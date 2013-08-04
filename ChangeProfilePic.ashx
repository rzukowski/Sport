<%@ WebHandler Language="C#" Class="ChangeProfilePic" %>

using System;
using System.Web;
using UsrCode;
public class ChangeProfilePic : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request["picPath"] != null && context.Request["id"] != null)
        {

            string picturePath = context.Request["picPath"].ToString().Replace("'", "");
            string userid = context.Request["id"].ToString().Replace("'", "");

            Usr.ChangeProfilePicture(userid, picturePath);
            
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}