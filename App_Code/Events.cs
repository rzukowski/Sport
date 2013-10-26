using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using UsrCode;

/// <summary>
/// Summary description for Events
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class Events : System.Web.Services.WebService {

    public Events () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string SaveNewEvent(string userid, string eventDate, string eventPosition, string eventDescription, string eventName, string eventLimit, string country, string city, string street,string category)
    {
        string lat = eventPosition.Split(',')[0].Replace("(","");
        string lng = eventPosition.Split(',')[1].Replace(")","");

        Guid guid = Guid.NewGuid();
        SqlConnection con = new SqlConnection(Usr.ConnectionString);
        con.Open();

        SqlCommand cmd = new SqlCommand("INSERT INTO eventDetails VALUES(@guid,@eventDescription,@eventDate,@freeSlots,@takenSlots,@limits,@street,@city,@country,@lat,@lng,@name)", con);
        cmd.Parameters.AddWithValue("@guid", guid);
        cmd.Parameters.AddWithValue("@eventDescription", eventDescription);
        cmd.Parameters.AddWithValue("@eventDate", eventDate);
        if (eventLimit != "")
        {
            cmd.Parameters.AddWithValue("@freeSlots", eventLimit);
            cmd.Parameters.AddWithValue("@takenSlots", 0);
            cmd.Parameters.AddWithValue("@limits", eventLimit);

        }
        else
        {
            cmd.Parameters.AddWithValue("@freeSlots", "");
            cmd.Parameters.AddWithValue("@takenSlots", "");
            cmd.Parameters.AddWithValue("@limits","");


        }
        cmd.Parameters.AddWithValue("@street", street);
        cmd.Parameters.AddWithValue("@city", city);
        cmd.Parameters.AddWithValue("@country", country);
        cmd.Parameters.AddWithValue("@lat", lat);
        cmd.Parameters.AddWithValue("@lng", lng);
        cmd.Parameters.AddWithValue("@name", eventName);
        
        foreach (SqlParameter Parameter in cmd.Parameters)
        {
            if (Parameter.Value == "")
            {
                Parameter.Value = DBNull.Value;
            }
        }

        try
        {
            cmd.ExecuteNonQuery();
        }
        catch
        {


        }
        return eventName;

    }
    
}
