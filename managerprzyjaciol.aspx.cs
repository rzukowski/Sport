using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Base;
using UsrCode;
public partial class managerprzyjaciol : BaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        String userid = Session["userid"].ToString();
        String friendid = ddlFriends.SelectedItem.Value;
        if (Usr.DeleteFriend(userid, friendid))
            Response.Redirect("default.aspx");
        else
            lbl.Text = "Błąd usuwania!";
    }
    
}