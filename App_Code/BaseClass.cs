using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Base
{
    /// <summary>
    /// Summary description for BaseClass
    /// </summary>
    public class BaseClass : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("~/Zaloguj.aspx");
            }
            
        }
    }
}