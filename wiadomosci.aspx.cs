using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Base;
public partial class wiadomosci : BaseClass
{
    

    public static int mssgPerPage = 10;
    protected void Page_Load(object sender, EventArgs e)
    {
       

        String userid = Session["userid"].ToString();
        Usr.DecrementNotification(userid);
        int i = 1;
        if(!IsPostBack)
            FillSkrzynka(null, new CommandEventArgs("name", i));
        if (IsPostBack)
            GetNumberOfViewed();
    }


    protected void FillSkrzynka(object sender, CommandEventArgs e)
    {

        string userid = Session["userid"].ToString();

        DataClassesDataContext db = new DataClassesDataContext(Usr.ConnectionString);



        int numberToSkip = (int.Parse(e.CommandArgument.ToString()) - 1) * mssgPerPage;

        var odp = (from user in db.aspnet_Users
                   join x in db.messages on user.UserId equals x.sendid
                   where x.receivid.ToString() == userid.ToString() orderby x.sentdate descending
                   select new { userid = x.sendid, msgid = x.msgid, sentdate = x.sentdate, mssg = x.mssg, username = user.UserName }).Skip(numberToSkip).Take(mssgPerPage);
        CurrPage.Value = e.CommandArgument.ToString();
        GetNumberOfViewed();
        DataList1.DataSource = odp.ToList();
        DataList1.DataBind();


    }

    protected void GetNumberOfViewed()
    {
        string userid = Session["userid"].ToString();
        int numberOfVisits = Usr.GetNumberOfReceivedMssg(userid);

        int pagesNumberMax = (int)Math.Ceiling((decimal)numberOfVisits / mssgPerPage);
        int page = int.Parse(CurrPage.Value);


        Usr.BuildPagination(links, pagesNumberMax, page, FillSkrzynka);
    }
}