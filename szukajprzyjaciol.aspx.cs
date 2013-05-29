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
public partial class szukajprzyjaciol : BaseClass
{
    public static int mssgPerPage = 10;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BuildCheckBox();
            BuildWojewodztwaCheckBox();
            Usr.FillWygladDropDownList(DDLWyglad);
        }
        if (IsPostBack)
            BuildPaginationLinks("1");
    }

    protected void AddWojewodztwoParameter(object sender, EventArgs e)
    {

        List<string> wojewodztwa = new List<string>();
        CheckBoxList checkedListBox1 = (CheckBoxList)pnlWojewodztwa.FindControl("cblWojewodztwa");

        foreach (ListItem li in checkedListBox1.Items)
        {
            if (li.Selected)
            {
                if (li.Text == "Cała Polska")
                {
                    wojewodztwa = GetAllWojewodztwa();
                    break;

                }
                else
                {
                    wojewodztwa.Add(li.Value);

                }

            }
            else if(checkedListBox1.Items.Count==0)
            { 
                wojewodztwa = GetAllWojewodztwa(); 
            }

        }

        SqlDataSource1.SelectParameters["wojewodztwo_id"].DefaultValue= string.Join(",", wojewodztwa);
    }

    private List<string> GetAllWojewodztwa()
    {
        SqlConnection con = new SqlConnection(Usr.ConnectionString);
        List<string> wojewodztwa = new List<string>();

        con.Open();

        SqlCommand cmd = new SqlCommand("SELECT wojewodztwo_id FROM Wojewodztwa WHERE wojewodztwo_id <> '17' AND wojewodztwo_id IS NOT NULL", con);
        try
        {
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                    wojewodztwa.Add(reader.GetInt32(0).ToString());
                reader.Close();
            }
        }
        catch (Exception ex)
        {

            HttpContext.Current.Trace.Write(ex.Message);
        }
        finally
        {
            con.Close();
        }
        return wojewodztwa;
    }

    protected void BuildWojewodztwaCheckBox()
    {

        CheckBoxList checkedListBox1 = (CheckBoxList)pnlWojewodztwa.FindControl("cblWojewodztwa");
        SqlConnection con = new SqlConnection(Usr.ConnectionString);
        con.Open();

        SqlCommand cmd = new SqlCommand("SELECT wojewodztwo_id, wojewodztwo FROM Wojewodztwa WHERE wojewodztwo_id IS NOT NULL ORDER BY wojewodztwo_id DESC", con);
        SqlDataReader reader = cmd.ExecuteReader();

        checkedListBox1.DataSource = reader;
        checkedListBox1.DataTextField = "wojewodztwo";
        checkedListBox1.DataValueField = "wojewodztwo_id";
        checkedListBox1.DataBind();
        con.Close();

    }


    protected void FillSzukane(object sender, CommandEventArgs e)
    {

        DataClassesDataContext db = new DataClassesDataContext(Usr.ConnectionString);
        string userid = Session["userid"].ToString();
        int gender = int.Parse(DropDownList1.SelectedValue);
        int dateDown = int.Parse(DropDownList2.SelectedValue);
        int dateUp = int.Parse(DropDownList3.SelectedValue);
        List<int> wojewodztwa_id = new List<int>();
        foreach(ListItem li in cblWojewodztwa.Items){
            if (li.Selected && (int.Parse(li.Value) == 17))
            {
                for (int ii = 1; ii < 17; ii++)
                    wojewodztwa_id.Add(ii);
                break;
            }
            else
            {
                if (li.Selected)
                    wojewodztwa_id.Add(int.Parse(li.Value));
            }
          }

        CheckBoxList checkedListBox1 = (CheckBoxList)pnlCustomers.FindControl("cblCustomerList");
        List<int> sporty_id = new List<int>();
        foreach (ListItem li in checkedListBox1.Items)
        {
            if (li.Selected)
                sporty_id.Add(int.Parse(li.Value));

        }
        int wygladId = 0;
        if(DDLWyglad.SelectedValue != "")
        wygladId = int.Parse(DDLWyglad.SelectedValue);


        int toSkip = (int.Parse(e.CommandArgument.ToString()) - 1) * mssgPerPage;
        int toTake = mssgPerPage;

        var szukajResults = Usr.FillSzukane(userid, dateDown, dateUp, gender, wojewodztwa_id,toSkip,toTake,sporty_id,wygladId);
        ListView1.DataSource = szukajResults.ToList();
        ListView1.DataBind();
        BuildPaginationLinks(e.CommandArgument.ToString());
    }


    


    protected void BuildPaginationLinks(string PageNumber)
    {

        string userid = Session["userid"].ToString();
        int gender = int.Parse(DropDownList1.SelectedValue);
        int dateDown = int.Parse(DropDownList2.SelectedValue);
        int dateUp = int.Parse(DropDownList3.SelectedValue);
        List<int> wojewodztwa_id = new List<int>();
        foreach (ListItem li in cblWojewodztwa.Items)
        {
            if (li.Selected && (int.Parse(li.Value) == 17))
            {
                for (int zz = 1; zz < 17; zz++)
                    wojewodztwa_id.Add(zz);
                break;
            }
            else
            {
                if (li.Selected)
                    wojewodztwa_id.Add(int.Parse(li.Value));
            }
        }
        CheckBoxList checkedListBox1 = (CheckBoxList)pnlCustomers.FindControl("cblCustomerList");
        List<int> sporty_id = new List<int>();
        foreach (ListItem li in checkedListBox1.Items)
        {
            if (li.Selected)
                sporty_id.Add(int.Parse(li.Value));

        }
        int wygladId=0;
        if(DDLWyglad.SelectedValue!="")
        wygladId = int.Parse(DDLWyglad.SelectedValue);

        int numberOfFound = Usr.GetNumberOfSzukane(userid,dateDown,dateUp,gender,wojewodztwa_id,sporty_id,wygladId);

        int pagesNumberMax = (int)Math.Ceiling((decimal)numberOfFound / mssgPerPage);
        int page = int.Parse(PageNumber);
        Usr.BuildPagination(links, pagesNumberMax, page, FillSzukane);
    }

    protected void BuildCheckBox()
    {


        //dodaje do checkboxlist elementy
        // Dictionary<int, string> sportyCat = DataSource();
        CheckBoxList checkedListBox1 = (CheckBoxList)pnlCustomers.FindControl("cblCustomerList");
        Dictionary<int, string> sporty = Usr.SelectSportIdSportOpis();
        checkedListBox1.DataSource = sporty;
        checkedListBox1.DataTextField = "Value";
        checkedListBox1.DataValueField = "key";
        checkedListBox1.DataBind();

        //        foreach (KeyValuePair<int, string> pair in sportyCat)
        //         {
        //            
        //         checkedListBox1.Items.Add(new ListItem(pair.Key.ToString(),pair.Value));
        //        }
        //idz do metody zaznaczającej dane z bazy


    }

  

}