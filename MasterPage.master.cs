using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Web.Security;
using UsrCode;


public partial class MasterPage : System.Web.UI.MasterPage
{


    protected string UploadFolderPath = "~/photos/";
    public static string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FriendsConnectionString"].ConnectionString;


    protected void Page_Load(object sender, EventArgs e)
    {
        GetUserName();
        if (!IsPostBack)
        {
            user.Text = Session["username"].ToString();

            string username=user.Text;

            string pathFromDB = Usr.GetProfilePicture(Session["userid"].ToString());


            string usrphoto = Server.MapPath("./") + pathFromDB;


            if (pathFromDB != null && System.IO.File.Exists(usrphoto))
            {
                photo.ImageUrl = pathFromDB;
            }
            else
            {
                photo.ImageUrl = Usr.defaultUserPhoto;

            }


            string userid = Session["userid"].ToString();
            int numberOfMssg = Usr.NumberOfNotifications(userid);
            if (numberOfMssg > 0)
            {
                Label myLabel = this.FindControl("NowaWiadomosc") as Label;
                myLabel.Text = numberOfMssg.ToString();


            }

            Label OdwiedziliTotal = this.FindControl("OdwiedziliTotal") as Label;
            Label OdwiedziliNew = this.FindControl("OdwiedziliNew") as Label;
            if (GetNumberOfNewVisits() == 0)
            {
                OdwiedziliNew.Visible = false;
            }
            else
            {
                OdwiedziliNew.Text = GetNumberOfNewVisits().ToString() + "/";
            }
            OdwiedziliTotal.Text = Usr.GetNumberOfAllVisits(userid).ToString();

        }
     
        


    }

    protected string GetUserName()
    {
        return Session["username"].ToString();
    }


    protected string GetUserId()
    {
        return Session["userid"].ToString();
    }

    protected void MenuGlowne_MenuItemClick(object sender, MenuEventArgs e)
    {

    }

    //pobiera liczbę nowych wizyt na naszym profilu
    protected int GetNumberOfNewVisits()
    {
        string userid = Session["userid"].ToString();
        int numberOfVisits=0;
        SqlConnection con = new SqlConnection(ConnectionString);
        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Viewed where OdwiedzanyId = @userid AND viewed = 0", con);
        cmd.Parameters.AddWithValue("@userid", userid);
        con.Open();
        try
        {
            numberOfVisits = (int)cmd.ExecuteScalar();
                
           
        }
        catch (Exception ex)
        {

            HttpContext.Current.Trace.Write(ex.Message);
            numberOfVisits = 0;
        }
        finally
        {
            con.Close();
        }
        if (numberOfVisits > 0)
        {
            return numberOfVisits;
        }
        else
            return 0;


    }

    
    protected void LogOut(object sender, EventArgs e)
    {
        if (HttpContext.Current.Request.Cookies["UserSzukaj"] != null)
        {
            HttpCookie myCookie = new HttpCookie("UserSzukaj");
            myCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(myCookie);
        }
        FormsAuthentication.SignOut();
        Session.Abandon();
        Response.Redirect("~/Zaloguj.aspx");
    }





}
