using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
public partial class Default2 : System.Web.UI.Page
{

    public static string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FriendsConnectionString"].ConnectionString;


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //sprawdzenie czy użytkownik o danej nazwie i mailu istnieje
    //jeśli tak - resetowanie hasła i wysłanie maila z nowym hasłem (automatycznie wygenerowany guid)
    protected void Sent_Click(object sender, EventArgs e)
    {

        string username = Username.Text;
        string email = mail.Text;
        bool validated = Usr.CheckUserByEmailAndName(username, email);

        if (!validated)
            wrong.Visible = true;

        else
        {
            string newpass = Guid.NewGuid().ToString();
            try
            {
                if(Usr.SaveNewPassToDb(newpass, username))
                    SendEmail(email, newpass);
            }
            catch (Exception err)
            {

                HttpContext.Current.Trace.Write(err.Message);
            }
            
        }

    }

    //metoda wysyłająca email

    //hasło, nazwa konta - w pliku Usr.cs
    protected void SendEmail(string email,string newpass)
    {
        string text = "Nowe hasło to: " + newpass + " Aby się zalogować, zapraszamy na stronę główną SzukajToZnajdziej.pl";
        MailMessage objMail = new MailMessage(Usr.websiteMail, email, "Nowe hasło", text);
        NetworkCredential objNC = new NetworkCredential(Usr.websiteMail, Usr.websiteMailPassword);
        SmtpClient objsmtp = new SmtpClient(Usr.smtpAddress,Usr.emailPort); // for gmail
        objsmtp.EnableSsl = true;
        objsmtp.Credentials = objNC;
        objsmtp.Send(objMail);
        wrong.Text = "Email z nowym hasłem został wysłany na: " + email;
        wrong.Visible = true;
    }

}