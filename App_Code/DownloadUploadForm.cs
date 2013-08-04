///////////////////////////////////////////////////////////////////////////////////
///	Description: Web Service for accessing control html via Ajax
/// Documentation and more information at http://weblogs.asp.net/sanjeevagarwal/  
/// Copyright (c) 2008, Sanjeev Agarwal.
/// ---------------------------------------------------------------------------------
///	Date of Change	: 24/07/2008
///	Author			: Sanjeev Agarwal
/// ---------------------------------------------------------------------------------
//////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Services.Protocols;

/// <summary>
/// Summary description for ScriptService
/// To access from ASP.NET AJAX remember to mentioned 
/// System.Web.Script.Services.ScriptService() attribute
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService()]
public class DownloadUploadForm : System.Web.Services.WebService
{

    public DownloadUploadForm()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    /// <summary>
    /// Get User Control Html
    /// use EnableSession=true if you are using session variables
    /// </summary>
    /// <returns>Html Table</returns>
    [WebMethod(EnableSession = true)]
    public string GetControlHtml(string controlLocation,string username, string userid)
    {
        // Create instance of the page control
        Page page = new Page();

        // Create instance of the user control
        UserControl userControl = (UserControl)page.LoadControl(controlLocation);

        //Disabled ViewState- If required
        //userControl.EnableViewState = false;
        
        //Form control is mandatory on page control to process User Controls
        HtmlForm form = new HtmlForm();

        //Add user control to the form
        form.Controls.Add(userControl);
        
        //Add form to the page 
        page.Controls.Add(form);

        //Write the control Html to text writer
        StringWriter textWriter = new StringWriter();

        //execute page on server 
        HttpContext.Current.Server.Execute(page, textWriter, false);
        
        // Clean up code and return html
       // string html = Regex.Replace(textWriter.ToString(), "(.*(\r|\n|\r\n))*.*<div id=.{1}wrapperAjax.{1} />", "", RegexOptions.IgnoreCase);

        string html = textWriter.ToString().Replace("userName", username).Replace("userid", userid).Replace("(.*(\r|\n|\r\n))*.*<div id=.{1}wrapperAjax.{1} />","");

        return html;
    }

    /// <summary>
    /// Removes Form tags using Regular Expression
    /// </summary>
    private string CleanHtml(string html)
    {
        try
        {
            return Regex.Replace(html, "(.*(\r|\n|\r\n))*.*<div id=.{1}wrapperAjax.{1} />", "", RegexOptions.IgnoreCase);
        }
        catch (Exception ex)
        {
            Exception ex2 = ex;
        }
        return null;
    }

}

