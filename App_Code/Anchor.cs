using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classes
{
    /// <summary>
    /// Summary description for Anchor
    /// </summary>
    public class Anchor
    {
        public string CssClass = "";
        public string Text = "";
        public string FunctionOnClick = "";
        public string ID = "";
        public string CreateHtml()
        {

            return "<a id=\'" + ID + "\' class=\'" + this.CssClass + "\' onClick=\"" + FunctionOnClick + "\">" + Text + "</a>";

        }
    }


    public class Div
    {
        public string text = "<div>contentText</div>";
        public void AddControll(string controlContent)
        {
            this.text = this.text.Replace("contentText", controlContent + "contentText");


        }


        public string CreateDivHtml()
        {
            return this.text.Replace("contentText", "");


        }


    }
}