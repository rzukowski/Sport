using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Base;
using UsrCode;
public partial class EventsCreate : BaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected string GetEventsCategories()
    {

        return Usr.GetEventsCategories();


    }
}