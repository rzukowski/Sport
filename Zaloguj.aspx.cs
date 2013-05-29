using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
public partial class Zaloguj : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //Session.Abandon();
        //Response.Redirect("~/Default.aspx");
        //sprawdz czy jest coookie
        //jesli sie zgadza token to przekierowujemy usera na strone
        //jesli nie wyswietlamy strone logowania
        //jesli jest cookie i ma zly token - kasujemy ciacho i kierujemy usera na strone logowania
        if (!IsPostBack) 
            CheckCookie();
       
        

    }
    protected void TryToLog(object sender, EventArgs e)
    {



        if (Membership.ValidateUser(UserName.Text, UserPass.Text))
        {
            Session.Add("username", UserName.Text);
            Session.Add("userid",
           Membership.GetUser(UserName.Text).ProviderUserKey.ToString());

            //jesli zaznaczona opcja 'Pamiętaj login'
            if (RememberMe.Checked)
            {
                string username = UserName.Text;
                //stworzymy token ktory zapiszemy tez do bazy

                //jesli user ma ciacho z tokenem, to przekierowujemy go na stronę testową,jednoczesnie kasując 
                //stary token i generujemy nowy


                //tworzymy ciacho

                //pobieramy salt z bazy
                string salt = Usr.GetSaltFromUser(username);
                if (salt != null)
                {
                    HttpCookie cookie = CreateAuthCookie(username, salt);
                    Response.Cookies.Add(cookie);
                }


            }


            Response.Redirect("~/Default.aspx");

        }
        else
        {
            Wrong.Visible = true;

        }


    }

    // Create a hash of the given password and salt.
    public string CreateHash(string password, string salt)
    {

        byte[] bytes = Encoding.Unicode.GetBytes(password);
        byte[] src = Convert.FromBase64String(salt);
        byte[] dst = new byte[src.Length + bytes.Length];
        System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
        System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);

        HashAlgorithm hash = HashAlgorithm.Create("SHA1");

        byte[] hashed = hash.ComputeHash(dst);

        string hashedPass = Convert.ToBase64String(hashed);

        return hashedPass.ToUpper();
            
    }

    // Check to see if the given password and salt hash to the same value
    // as the given hash.
    public bool IsMatchingHash(string storedHash, string hash)
    {
        // Recompute the hash from the given auth details, and compare it to
        // the hash provided by the cookie.
        return storedHash.ToUpper() == hash.ToUpper();
    }

    public HttpCookie CreateAuthCookie(string username, string salt)
    {
        // Create the cookie and set its value to the username and a hash of the
        // password and salt. Use a pipe character as a delimiter so we can
        // separate these two elements later.
        //
        Guid guid = Guid.NewGuid(); ;
        HttpCookie cookie = new HttpCookie("UserSzukaj");

        if (Usr.SaveTokenToDb(guid.ToString().ToUpper(),username))
        {
            cookie.Value = username + "|" + CreateHash(guid.ToString().ToUpper(), salt);
            cookie.Expires = DateTime.Now.AddDays(1);
        }
        return cookie;
    }

    //sprawdzenie czy ciacho zawiera token
    public bool IsValidAuthCookie(HttpCookie cookie)
    {
        // Split the cookie value by the pipe delimiter.
        string[] values = cookie.Value.Split('|');
        if (values.Length != 2) return false;

        // Retrieve the username and hash from the split values.
        string username = values[0];
        string tokenSalted = values[1].ToUpper();

        // You'll have to provide your GetPasswordForUser function.
        string tokenUser = Usr.GetUserToken(username);
        string salt = Usr.GetSaltFromUser(username);
        // Check the password and salt against the hash.
        return IsMatchingHash(tokenSalted, CreateHash(tokenUser.ToUpper(),salt));
    }



    protected void CheckCookie()
    {
        if (HttpContext.Current.Request.Cookies["UserSzukaj"] != null)
        {
            // do something
            HttpCookie cookie = HttpContext.Current.Request.Cookies["UserSzukaj"];
            if (IsValidAuthCookie(cookie))
            {
                string[] values = cookie.Value.Split('|');

                string username = values[0];

                Session.Add("username", username);
                Session.Add("userid",
               Membership.GetUser(username).ProviderUserKey.ToString());
                Server.ClearError();
               
                    Response.Redirect("~/Default.aspx", false);

                
                Context.ApplicationInstance.CompleteRequest();

            }
            else
            {
                //destroy cookie
                //continue loading page
                HttpCookie myCookie = new HttpCookie("UserSzukaj");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);


            }
        }

    }
   

}