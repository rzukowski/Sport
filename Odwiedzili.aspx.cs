using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using Base;
public partial class Odwiedzili : BaseClass
{
    public static string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FriendsConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {

        //if (ScriptManager.GetCurrent(Page).IsInAsyncPostBack)
        //{
        //    if (UpdatePanel1.IsInPartialRendering)
        //    {
        //        bool yes = true;
        //    }
        //}

        if (!IsPostBack)
        {

            ResetViewed();
            FillOdwiedziliFirst(1);
        }
        GetNumberOfViewed();
    }

    protected void ResetViewed()
    {
       

        string odwiedzanyId= Session["userid"].ToString();
        SqlConnection con = new SqlConnection(ConnectionString);
        SqlCommand cmd = new SqlCommand("UPDATE Viewed SET viewed = 1 WHERE OdwiedzanyId = @odwiedzanyId", con);
        cmd.Parameters.AddWithValue("@odwiedzanyId", odwiedzanyId);
        con.Open();
        try
        {
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            HttpContext.Current.Trace.Write(ex.Message);
        }
        finally
        {
            con.Close();
        }


    }
    protected void FillOdwiedziliFirst(int pageNumber)
    {

        string userid = Session["userid"].ToString();

        DataClassesDataContext db = new DataClassesDataContext(ConnectionString);
        int numberOfRows = (pageNumber-1) * 2;
        var cos = from c in db.PokazOdwiedzone(userid).Skip(numberOfRows).Take(2) select c;

        ListView1.DataSource = cos.ToList();
        ListView1.DataBind();




    }

    protected void FillOdwiedzili(object sender, CommandEventArgs e)
    {

        string userid=Session["userid"].ToString();
        
        DataClassesDataContext db = new DataClassesDataContext(ConnectionString);
        int numberOfRows = (int.Parse(e.CommandArgument.ToString()) -1) * 2;
        var cos = from c in db.PokazOdwiedzone(userid).Skip(numberOfRows).Take(2) select c;

        ListView1.DataSource = cos.ToList();
        ListView1.DataBind();




    }


    protected void GetNumberOfViewed()
    {
        string userid= Session["userid"].ToString();
        int numberOfVisits = Usr.GetNumberOfAllVisits(userid);
        int ii=0;
        int perPage = 2;
        
        for (ii = 0; ii < (numberOfVisits/perPage); ii++)
        {
            
            
            LinkButton anchor = new LinkButton();
           // anchor.PostBackUrl = "Odwiedzili.aspx?odwiedzili="+(ii+1);
            anchor.Text = (ii+1).ToString();
            anchor.ID = "link" + (ii + 1);

            anchor.Command += new CommandEventHandler(FillOdwiedzili);
            anchor.CommandArgument = (ii + 1).ToString();
            links.Controls.Add(anchor);
            
        }

    }




}