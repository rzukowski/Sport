using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using Classes;

namespace UsrCode
{
    /// <summary>
    /// Summary description for Usr
    /// </summary>
    public class Usr
    {
        //connection string pobrany z web.config
        public static string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FriendsConnectionString"].ConnectionString;// @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Raf\Documents\Visual Studio 2012\WebSites\WebSite5\App_Data\ASPNETDB.MDF;Integrated Security=True;User Instance=True";
        public static string websiteMail = "ktoszukatenznajdzie@gmail.com";
        public static string websiteMailPassword = "kozajemarchew234";
        public static string smtpAddress = "smtp.gmail.com";
        public static int emailPort = 587;
        public static string[] extensions = { ".gif", ".jpg", ".png" };
        public static int maxImgHeight = 700;
        public static int maxImgWidth = 700;
        public static int maxRozmiar = 3000000;


        public static int maxPhotosPerUploadNewProfilePicture = 2;
        //name of default photo

        public static string defaultUserPhoto = "photos\\default.jpg";

        public static DataClassesDataContext db = new DataClassesDataContext(Usr.ConnectionString);

        //dodanie do ulubionych - dodanie do tabeli friends
        public static bool AddFriend(String userid, String friendid)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into friends values (@userid, @friendid)", con);
                cmd.Parameters.AddWithValue("@userid", userid);
                cmd.Parameters.AddWithValue("@friendid", friendid);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Trace.Write(ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }


        //wysłanie wiadomości - zapis do tabeli message

        public static bool SendMssg(String fromuserid, String touserid, String message)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into message (sendid,receivid,mssg,sentdate) values (@userid, @touserid,@message,getdate())", con);
                cmd.Parameters.AddWithValue("@userid", fromuserid);
                cmd.Parameters.AddWithValue("@touserid", touserid);
                cmd.Parameters.AddWithValue("@message", message);
                cmd.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand("insert into message_sent (sender_id,receiver_id,mssg,sentdate) values (@userid, @touserid,@message,getdate())", con);
                cmd2.Parameters.AddWithValue("@userid", fromuserid);
                cmd2.Parameters.AddWithValue("@touserid", touserid);
                cmd2.Parameters.AddWithValue("@message", message);
                cmd2.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Trace.Write(ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        //usunięcie z listy ulubionych
        public static bool DeleteFriend(String userid, String friendid)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from friends where userid = @userid and friendid = @friendid", con);
                cmd.Parameters.AddWithValue("@userid", userid);
                cmd.Parameters.AddWithValue("@friendid", friendid);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Trace.Write(ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        //usuwanie wiadomości
        public static bool DeleteMssg(String msgid)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from message where msgid = @msgid", con);
                cmd.Parameters.AddWithValue("@msgid", msgid);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Trace.Write(ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }


        public static bool IncrementNotification(String userid)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE notifications SET numberOfNotifications=(numberOfNotifications + 1) WHERE userid=@userid", con);
                cmd.Parameters.AddWithValue("@userid", userid);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Trace.Write(ex.Message);
                return false;

            }
            finally
            {
                con.Close();
            }

        }

        public static bool DecrementNotification(String userid)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update notifications set numberOfNotifications=0 where userid=@userid", con);
                cmd.Parameters.AddWithValue("@userid", userid);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Trace.Write(ex.Message);
                return false;

            }
            finally
            {
                con.Close();
            }

        }

        public static int NumberOfNotifications(String userid)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select numberOfNotifications from notifications where userid=@userid", con);
                cmd.Parameters.AddWithValue("@userid", userid);
                SqlDataReader myReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                myReader.Read();
                int number = myReader.GetInt32(0);
                if (number > 0) return number;
                else return 0;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Trace.Write(ex.Message);
                return 0;

            }
            finally
            {
                con.Close();
            }


        }

        public static List<string> GetSporty(String userid)
        {
            List<string> sporty = new List<string>();
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT sport_opis FROM Sport WHERE Sport.sport_id IN (SELECT sport_id FROM user_sport WHERE userid = @userid)", con);
            cmd.Parameters.AddWithValue("@userid", userid);
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        sporty.Add(reader.GetString(0));
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
            return sporty;
        }

        //sprawdza czy user przez ostatnią godzinę odwiedzał danego usera (jeśli tak, to nie zapisujemy informacji o odwiedzinach w bazie)
        public static bool GetTime(string odwiedzanyid, string odwiedzilid, DateTime date)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            bool hasRows;
            SqlCommand cmd = new SqlCommand("SELECT * FROM Viewed where OdwiedzanyId = @odwiedzanyid AND OdwiedzilId= @odwiedzilid AND DATEDIFF(hour,Data,@date)<1 ", con);
            cmd.Parameters.AddWithValue("@odwiedzanyid", odwiedzanyid);
            cmd.Parameters.AddWithValue("@odwiedzilid", odwiedzilid);
            cmd.Parameters.AddWithValue("@date", date);
            con.Open();

            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                        hasRows = true;
                    else
                        hasRows = false;
                }
            }
            catch (Exception ex)
            {

                HttpContext.Current.Trace.Write(ex.Message);
                hasRows = true;
            }
            finally
            {
                con.Close();
            }

            return hasRows;

        }


        public static void SaveVisited(string odwiedzanyid, string odwiedzilid)
        {


            DateTime date = DateTime.Now;
            SqlConnection con = new SqlConnection(ConnectionString);
            if (!Usr.GetTime(odwiedzanyid, odwiedzilid, date))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("INSERT INTO Viewed (OdwiedzanyId,OdwiedzilId,Data,viewed) VALUES (@odwiedzanyid,@odwiedzilid,@date,0)", con);
                    cmd.Parameters.AddWithValue("@odwiedzanyid", odwiedzanyid);
                    cmd.Parameters.AddWithValue("@odwiedzilid", odwiedzilid);
                    cmd.Parameters.AddWithValue("@date", date);
                    con.Open();
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
        }

        public static Dictionary<int, string> SelectSportIdSportOpis()
        {
            Dictionary<int, string> sporty = new Dictionary<int, string>();
            SqlDataReader reader = null;
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT DISTINCT [sport_id],[sport_opis] FROM [Sport]", con);
            con.Open();
            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    sporty.Add(reader.GetInt32(0), reader.GetString(1).ToString());

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
            return sporty;

        }


        public static void ExecuteCommandInsert(string id, string userid)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO user_sport VALUES (@userid,@id)", con);
            cmd.Parameters.AddWithValue("@userid", userid);
            cmd.Parameters.AddWithValue("@id", id);
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


        /// <summary>
        /// Deletes user sports 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userid"></param>
        public static void ExecuteCommandDelete(string id, string userid)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM user_sport WHERE userid=@userid AND sport_id = @id", con);
            cmd.Parameters.AddWithValue("@userid", userid);
            cmd.Parameters.AddWithValue("@id", id);
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

        /// <summary>
        /// Selects user budowa_ciala
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static int SelectBudowaCiala(String userid)
        {
            int budowa = 0;
            SqlDataReader reader = null;
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT budowa_ciala_id FROM Wyglad WHERE userid = @userid", con);
            cmd.Parameters.AddWithValue("@userid", userid);
            con.Open();

            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                    budowa = reader.GetInt32(0);

            }

            catch (Exception ex)
            {
                HttpContext.Current.Trace.Write(ex.Message);

            }

            finally
            {
                con.Close();

            }

            return budowa;


        }

        public static string SelectOpisCiala(String userid)
        {
            string budowa = null;
            SqlDataReader reader = null;
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT budowa_ciala_opis FROM Budowa WHERE budowa_ciala_id = (SELECT budowa_ciala_id FROM Wyglad WHERE userid = @userid)", con);
            cmd.Parameters.AddWithValue("@userid", userid);
            con.Open();

            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                    budowa = reader.GetString(0);

            }

            catch (Exception ex)
            {
                HttpContext.Current.Trace.Write(ex.Message);

            }

            finally
            {
                con.Close();

            }

            return budowa;


        }


        public static int GetWzrost(String userid)
        {
            int wzrost = 0;
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT wzrost FROM Wyglad where userid=@userid", con);
            cmd.Parameters.AddWithValue("@userid", userid);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    wzrost = reader.GetInt32(0);
                reader.Close();

            }
            catch (Exception ex)
            {
                HttpContext.Current.Trace.Write(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return wzrost;


        }

        public static int GetWaga(string userid)
        {
            int waga = 0;
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT waga FROM Wyglad where userid=@userid", con);
            cmd.Parameters.AddWithValue("@userid", userid);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    waga = reader.GetInt32(0);
                reader.Close();

            }
            catch (Exception ex)
            {
                HttpContext.Current.Trace.Write(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return waga;

        }

        public static int GetPlecId(string userid)
        {

            int plec = 0;
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT plec_id FROM user_profile WHERE userid = @userid", con);
            cmd.Parameters.AddWithValue("@userid", userid);
            con.Open();

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    try { plec = reader.GetInt32(0); }
                    catch { }
                reader.Close();


            }

            catch (Exception ex)
            {
                HttpContext.Current.Trace.Write(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return plec;


        }

        /// <summary>
        /// updates user personal data (opis,dataurodzi,plec,wojewodztwo)
        /// </summary>
        /// <param name="opis"></param>
        /// <param name="userid"></param>
        /// <param name="dataUrodzin"></param>
        /// <param name="plec"></param>
        /// <param name="wojewodztwo"></param>
        public static void UpdateDane(string opis, string userid, string dataUrodzin, int plec, int wojewodztwo)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("UPDATE user_profile set plec_id=@plec, birthdate=@birthdate, wojewodztwo_id=@wojewodztwo_id, opis=@opis  WHERE userid=@userid", con);
            con.Open();
            cmd.Parameters.AddWithValue("@plec", plec);
            cmd.Parameters.AddWithValue("@userid", userid);
            cmd.Parameters.AddWithValue("@wojewodztwo_id", wojewodztwo);
            cmd.Parameters.AddWithValue("@opis", opis);
            cmd.Parameters.AddWithValue("@birthdate", dataUrodzin);

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

        /// <summary>
        /// gets user opis
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>

        public static string GetOpis(string userid)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT opis FROM user_profile WHERE userid=@userid", con);
            cmd.Parameters.AddWithValue("@userid", userid);
            string opis = null;
            con.Open();
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                        opis = reader.GetString(0);

                }
                catch { }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Trace.Write(ex.Message);
            }
            finally
            {

                con.Close();
            }
            return opis;


        }

        public static int GetUserWojewodztwo(string userid)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT wojewodztwo_id FROM user_profile WHERE userid = @userid", con);
            cmd.Parameters.AddWithValue("@userid", userid);
            int wojewodztwo = 17;
            con.Open();
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    try { wojewodztwo = reader.GetInt32(0); }
                    catch { }
                reader.Close();


            }

            catch (Exception ex)
            {
                HttpContext.Current.Trace.Write(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return wojewodztwo;
        }


        public static string GetUserBirthday(string userid)
        {
            string birthdate = null;
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT birthdate=(SUBSTRING(CAST(birthdate AS VARCHAR),0,11)) FROM user_profile WHERE userid = @userid", con);
            cmd.Parameters.AddWithValue("@userid", userid);
            con.Open();

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    try { birthdate = reader.GetString(0); }
                    catch { }
                reader.Close();


            }

            catch (Exception ex)
            {
                HttpContext.Current.Trace.Write(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return birthdate;


        }

        /// <summary>
        /// gets user salt used to hash his password
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static string GetSaltFromUser(string username)
        {

            string salt = null;
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT PasswordSalt FROM aspnet_Membership WHERE userid = (SELECT userid FROM aspnet_Users WHERE UserName=@username)", con);
            cmd.Parameters.AddWithValue("@username", username);
            con.Open();

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    try { salt = reader.GetString(0); }
                    catch { }
                reader.Close();


            }

            catch (Exception ex)
            {
                HttpContext.Current.Trace.Write(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return salt;

        }


        /// <summary>
        /// gets user token - if user choose to use cookie, hashed token is stored in database and cookie
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>

        public static string GetUserToken(string username)
        {

            string token = null;
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT Token FROM aspnet_Membership WHERE userid = (SELECT userid FROM aspnet_Users WHERE UserName=@username)", con);
            cmd.Parameters.AddWithValue("@username", username);
            con.Open();

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    try { token = reader.GetString(0); }
                    catch { }
                reader.Close();


            }

            catch (Exception ex)
            {
                HttpContext.Current.Trace.Write(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return token;

        }

        public static bool SaveTokenToDb(string token, string username)
        {

            bool updated = false;
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("UPDATE aspnet_Membership SET Token=@token WHERE userid= (SELECT userid from aspnet_Users WHERE username=@username)", con);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@token", token);
            con.Open();

            try
            {
                if (cmd.ExecuteNonQuery() > 0)
                    updated = true;


            }

            catch (Exception ex)
            {
                HttpContext.Current.Trace.Write(ex.Message);
                updated = false;
            }
            finally
            {
                con.Close();
            }

            return updated;

        }

        /// <summary>
        /// gets total number of visits
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static int GetNumberOfAllVisits(string userid)
        {
            int numberOfVisits = 0;
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM Viewed where OdwiedzanyId = @userid", con);
            cmd.Parameters.AddWithValue("@userid", userid);
            con.Open();
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        numberOfVisits = reader.GetInt32(0);
                    reader.Close();
                }
            }
            catch (Exception ex)
            {

                HttpContext.Current.Trace.Write(ex.Message);
                numberOfVisits = 0;
            }
            finally
            {
                con.Close();
            }
            if (numberOfVisits > 0)
            {
                return numberOfVisits;
            }
            else
                return 0;

        }

        public static int GetNumberOfSentMessages(string userid)
        {
            int sentMssg = 0;
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM message_sent where sender_id = @userid", con);
            cmd.Parameters.AddWithValue("@userid", userid);
            con.Open();
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        sentMssg = reader.GetInt32(0);
                    reader.Close();
                }
            }
            catch (Exception ex)
            {

                HttpContext.Current.Trace.Write(ex.Message);
                sentMssg = 0;
            }
            finally
            {
                con.Close();
            }
            if (sentMssg > 0)
            {
                return sentMssg;
            }
            else
                return 0;



        }

        public static int GetNumberOfReceivedMssg(string userid)
        {
            int receivedMssg = 0;
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM message where receivid = @userid", con);
            cmd.Parameters.AddWithValue("@userid", userid);
            con.Open();
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        receivedMssg = reader.GetInt32(0);
                    reader.Close();
                }
            }
            catch (Exception ex)
            {

                HttpContext.Current.Trace.Write(ex.Message);
                receivedMssg = 0;
            }
            finally
            {
                con.Close();
            }
            if (receivedMssg > 0)
            {
                return receivedMssg;
            }
            else
                return 0;


        }


        public static bool CheckUserByEmailAndName(string username, string email)
        {
            bool valid = false;
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM aspnet_Membership am JOIN aspnet_Users au ON au.UserId=am.UserId where am.email = @email AND au.UserName = @username", con);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@email", email);
            con.Open();

            try
            {

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    if (reader.GetInt32(0) == 1)
                        valid = true;
            }
            catch (Exception ex)
            {

                HttpContext.Current.Trace.Write(ex.Message);

            }

            finally
            {
                con.Close();
            }
            return valid;





        }

        public static bool SaveNewPassToDb(string pass, string username)
        {

            string salt = GetSaltFromUser(username);
            string hash = CreateHash(pass, salt);
            bool updated = false;
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("UPDATE aspnet_Membership SET Password = @password where userid = (Select userid from aspnet_Users WHERE username = @username)", con);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", hash);
            con.Open();
            try
            {
                cmd.ExecuteNonQuery();
                updated = true;
            }
            catch (Exception ex)
            {

                HttpContext.Current.Trace.Write(ex.Message);

            }
            finally
            {
                con.Close();

            }
            return updated;
        }


        /// <summary>
        /// creates md5 hash
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string CreateHash(string password, string salt)
        {

            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] src = Convert.FromBase64String(salt);
            byte[] dst = new byte[src.Length + bytes.Length];
            System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);

            HashAlgorithm hash = HashAlgorithm.Create("SHA1");

            byte[] hashed = hash.ComputeHash(dst);

            string hashedPass = Convert.ToBase64String(hashed);

            return hashedPass;

        }

        /// <summary>
        /// stored procedure - gets number of all searched profiles
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="dateDown"></param>
        /// <param name="dateUp"></param>
        /// <param name="gender"></param>
        /// <param name="wojewodztwa_id"></param>
        /// <param name="sporty_id"></param>
        /// <param name="budowaId"></param>
        /// <returns></returns>
        public static int GetNumberOfSzukane(string userid, int dateDown, int dateUp, int gender, List<int> wojewodztwa_id, List<int> sporty_id, int budowaId)
        {



            var szukajResults = (from x in db.user_profiles
                                 join p in db.plecs on x.plec_id equals p.plec_Id
                                 join u in db.aspnet_Users on x.userid equals u.UserId
                                 let years = DateTime.Now.Year - x.birthdate.Value.Year
                                 //get the date of the birthday this year
                                 let sportyUsera = (from s in db.user_sports where s.userid == x.userid select s.sport_id)
                                 let inter = (from zz in sportyUsera where sporty_id.Contains(zz) select zz)
                                 let budowaUsera = (from z in db.Wyglads where z.userid == x.userid select z.budowa_ciala_id.Value).First()
                                 let birthdayThisYear = x.birthdate.Value.AddYears(years)
                                 let age = birthdayThisYear > DateTime.Now ? years - 1 : years
                                 where
                                     p.plec_Id == gender
                                     && age < dateUp && age > dateDown &&
                                     wojewodztwa_id.Contains(x.wojewodztwo_id.Value) &&
                                     x.userid.ToString() != userid &&
                                     (sporty_id.Count() == 0 || inter.ToList().Count() != 0)
                                     && (budowaId == 0 || budowaUsera == budowaId)
                                 select new { username = u.UserName }).Count();

            return szukajResults;


        }


        /// <summary>
        /// stored procedure - returns all users which met given condition
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="dateDown"></param>
        /// <param name="dateUp"></param>
        /// <param name="gender"></param>
        /// <param name="wojewodztwa_id"></param>
        /// <param name="toSkip"></param>
        /// <param name="toTake"></param>
        /// <param name="sporty_id"></param>
        /// <param name="budowaId"></param>
        /// <returns></returns>
        public static IQueryable<System.Object> FillSzukane(string userid, int dateDown, int dateUp, int gender, List<int> wojewodztwa_id, int toSkip, int toTake, List<int> sporty_id, int budowaId)
        {

            var results = (from x in db.user_profiles
                           join p in db.plecs on x.plec_id equals p.plec_Id
                           join u in db.aspnet_Users on x.userid equals u.UserId
                           let years = DateTime.Now.Year - x.birthdate.Value.Year
                           //get the date of the birthday this year
                           let sportyUsera = (from s in db.user_sports where s.userid == x.userid select s.sport_id)
                           let inter = (from zz in sportyUsera where sporty_id.Contains(zz) select zz)
                           let budowaUsera = (from z in db.Wyglads where z.userid == x.userid select z.budowa_ciala_id.Value).First()
                           let birthdayThisYear = x.birthdate.Value.AddYears(years)
                           let age = birthdayThisYear > DateTime.Now ? years - 1 : years
                           where
                               p.plec_Id == gender
                               && age < dateUp && age > dateDown &&
                               wojewodztwa_id.Contains(x.wojewodztwo_id.Value) &&
                               x.userid.ToString() != userid &&
                               (sporty_id.Count() == 0 || inter.ToList().Count() != 0)
                               && (budowaId == 0 || budowaUsera == budowaId)
                           select new { username = u.UserName, opis = x.opis, userid = x.userid }).Skip(toSkip).Take(toTake);

            return results;




        }


        /// <summary>
        /// used to build pagination links
        /// </summary>
        /// <param name="linksContainer"></param>
        /// <param name="pagesNumberMax"></param>
        /// <param name="page"></param>
        /// <param name="nameOfHandler"></param>

        public static void BuildPagination(Control linksContainer, int pagesNumberMax, int page, CommandEventHandler nameOfHandler)
        {
            linksContainer.Controls.Clear();

            if (pagesNumberMax > 4)
            {

                LinkButton anchor = new LinkButton();
                LinkButton anchor2 = new LinkButton();
                LinkButton anchor3 = new LinkButton();
                LinkButton anchor4 = new LinkButton();

                //jesli to pierwsza strona
                if (page == 1)
                {
                    //pokaz 1 i 2, postaw '...' jako link do ostatniej strony
                    anchor.CssClass = "aktywnyPag";
                    page += 1;

                    anchor.Text = 1.ToString();
                    anchor2.Text = (page).ToString();
                    anchor3.Text = (page + 1).ToString();
                    anchor4.Text = "...";

                }
                //jesli aktywna strona większa od 1 oraz mniejsza od maxPageNumber - 1 to:
                else if (page > 1 && page < pagesNumberMax - 1)
                {
                    //'...' jako pierwsza strona '...' jako ostatnia strona, pokaz obecny page oraz page+1
                    anchor2.CssClass = "aktywnyPag";
                    anchor.Text = "...";
                    anchor2.Text = (page).ToString();
                    anchor3.Text = (page + 1).ToString();
                    anchor4.Text = "...";
                }

                    //jeśli aktywna strona to pageMax lub pageMax-1 to:
                else if (page == pagesNumberMax || page == (pagesNumberMax - 1))
                {
                    //'...' jako pierwszy, (pagesNumberMax-1) oraz pagesNumberMax jako numery


                    if (page == pagesNumberMax) { anchor4.CssClass = "aktywnyPag"; page -= 2; } else { anchor3.CssClass = "aktywnyPag"; page -= 1; }


                    anchor.Text = "...";
                    anchor2.Text = (page).ToString();
                    anchor3.Text = (page + 1).ToString();
                    anchor4.Text = pagesNumberMax.ToString();

                }

                anchor.ID = "link" + (1);
                anchor2.ID = "link" + (page);
                anchor3.ID = "link" + (page + 1);
                anchor4.ID = "link" + (pagesNumberMax);

                anchor.Command += new CommandEventHandler(nameOfHandler);
                anchor.CommandArgument = 1.ToString();
                anchor2.Command += new CommandEventHandler(nameOfHandler);
                anchor2.CommandArgument = (page).ToString();

                anchor3.Command += new CommandEventHandler(nameOfHandler);
                anchor3.CommandArgument = (page + 1).ToString();

                anchor4.Command += new CommandEventHandler(nameOfHandler);
                anchor4.CommandArgument = (pagesNumberMax).ToString();

                linksContainer.Controls.Add(anchor);
                linksContainer.Controls.Add(anchor2);
                linksContainer.Controls.Add(anchor3);
                linksContainer.Controls.Add(anchor4);



            }
            else
            {
                for (int ii = 0; ii < pagesNumberMax; ii++)
                {
                    LinkButton anchor = new LinkButton();
                    // anchor.PostBackUrl = "Odwiedzili.aspx?odwiedzili="+(ii+1);
                    anchor.Text = (ii + 1).ToString();
                    anchor.ID = "link" + (ii + 1);

                    anchor.Command += new CommandEventHandler(nameOfHandler);
                    anchor.CommandArgument = (ii + 1).ToString();
                    if ((ii + 1) == page)
                        anchor.CssClass = "aktywnyPag";
                    linksContainer.Controls.Add(anchor);


                }


            }
        }

        /// <summary>
        /// builds pagination links and return them as html string
        /// </summary>
        /// <param name="pagesNumberMax"></param>
        /// <param name="page"></param>
        /// <param name="functionString"></param>
        /// <returns></returns>

        public static string BuildHtmlPagination(int pagesNumberMax, int page, string functionString)
        {

            Div div = new Div();

            if (pagesNumberMax > 4)
            {

                Anchor anchor = new Anchor();
                Anchor anchor2 = new Anchor();
                Anchor anchor3 = new Anchor();
                Anchor anchor4 = new Anchor();

                //jesli to pierwsza strona
                if (page == 1)
                {
                    //pokaz 1 i 2, postaw '...' jako link do ostatniej strony
                    anchor.CssClass = "aktywnyPag";
                    page += 1;

                    anchor.Text = 1.ToString();
                    anchor2.Text = (page).ToString();
                    anchor3.Text = (page + 1).ToString();
                    anchor4.Text = "...";

                }
                //jesli aktywna strona większa od 1 oraz mniejsza od maxPageNumber - 1 to:
                else if (page > 1 && page < pagesNumberMax - 1)
                {
                    //'...' jako pierwsza strona '...' jako ostatnia strona, pokaz obecny page oraz page+1
                    anchor2.CssClass = "aktywnyPag";
                    anchor.Text = "...";
                    anchor2.Text = (page).ToString();
                    anchor3.Text = (page + 1).ToString();
                    anchor4.Text = "...";
                }

                    //jeśli aktywna strona to pageMax lub pageMax-1 to:
                else if (page == pagesNumberMax || page == (pagesNumberMax - 1))
                {
                    //'...' jako pierwszy, (pagesNumberMax-1) oraz pagesNumberMax jako numery


                    if (page == pagesNumberMax) { anchor4.CssClass = "aktywnyPag"; page -= 2; } else { anchor3.CssClass = "aktywnyPag"; page -= 1; }


                    anchor.Text = "...";
                    anchor2.Text = (page).ToString();
                    anchor3.Text = (page + 1).ToString();
                    anchor4.Text = pagesNumberMax.ToString();

                }

                anchor.ID = "link" + (1);
                anchor2.ID = "link" + (page);
                anchor3.ID = "link" + (page + 1);
                anchor4.ID = "link" + (pagesNumberMax);

                anchor.FunctionOnClick = functionString.Replace("this", "1");
                anchor.FunctionOnClick = 1.ToString();

                anchor2.FunctionOnClick = functionString.Replace("this", page.ToString());

                anchor3.FunctionOnClick = functionString.Replace("this", page.ToString() + 1);


                anchor4.FunctionOnClick = functionString.Replace("this", pagesNumberMax.ToString());

                div.AddControll(anchor.CreateHtml());
                div.AddControll(anchor2.CreateHtml());
                div.AddControll(anchor3.CreateHtml());
                div.AddControll(anchor4.CreateHtml());





            }
            else
            {
                for (int ii = 0; ii < pagesNumberMax; ii++)
                {
                    Anchor anchor = new Anchor();
                    // anchor.PostBackUrl = "Odwiedzili.aspx?odwiedzili="+(ii+1);
                    anchor.Text = (ii + 1).ToString();
                    anchor.ID = "link" + (ii + 1);

                    anchor.FunctionOnClick += functionString.Replace("this", (ii + 1).ToString());

                    if ((ii + 1) == page)
                        anchor.CssClass = "aktywnyPag";
                    div.AddControll(anchor.CreateHtml());


                }


            }
            return div.CreateDivHtml();

        }

        public static RadioButtonList FillWygladDropDownList(RadioButtonList DDLWyglad)
        {

            SqlConnection con = new SqlConnection(Usr.ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT budowa_ciala_id, budowa_ciala_opis FROM Budowa", con);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                DDLWyglad.DataSource = reader;
                DDLWyglad.DataTextField = "budowa_ciala_opis";
                DDLWyglad.DataValueField = "budowa_ciala_id";
                DDLWyglad.DataBind();
            }
            catch (Exception ex)
            {
                HttpContext.Current.Trace.Write(ex.Message);

            }
            finally
            {
                con.Close();

            }

            return DDLWyglad;

        }

        /// <summary>
        /// saves pictures to database
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="path"></param>
        /// <param name="isProfilePicture"></param>
        /// <returns></returns>

        public static bool SaveFileToDatabase(string userid, string path, bool isProfilePicture)
        {

            int profilePicture = 0;
            if (isProfilePicture != false)
            {
                profilePicture = 1;

            }
            bool updated = false;
            SqlConnection con = new SqlConnection(Usr.ConnectionString);
            SqlCommand cmd = new SqlCommand("Insert into Galeria VALUES(@userid,@path,@profilowe)", con);
            cmd.Parameters.AddWithValue("@userid", userid);
            cmd.Parameters.AddWithValue("@path", path);
            cmd.Parameters.AddWithValue("@profilowe", profilePicture);
            con.Open();

            try
            {
                if (cmd.ExecuteNonQuery() == 1)
                    updated = true;

            }
            catch (Exception ex)
            {

                HttpContext.Current.Trace.Write(ex.Message);

            }

            finally
            {
                con.Close();
            }

            return updated;


        }

        //public static bool ResetProfilePictureInDB(String userid)
        //{
        //    bool updated = false;
        //    SqlConnection con = new SqlConnection(Usr.ConnectionString);
        //    SqlCommand cmd = new SqlCommand("UPDATE Galeria set profilowe=0 WHERE userid=@userid", con);
        //    cmd.Parameters.AddWithValue("@userid", userid);

        //    con.Open();

        //    try
        //    {
        //        if (cmd.ExecuteNonQuery() == 1)
        //            updated = true;

        //    }
        //    catch (Exception ex)
        //    {

        //        HttpContext.Current.Trace.Write(ex.Message);

        //    }

        //    finally
        //    {
        //        con.Close();
        //    }

        //    return updated;



        //}


        public static List<string> GetAllUserPictures(string userid)
        {
            List<string> photos = new List<string>();
            SqlConnection con = new SqlConnection(Usr.ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT path FROM Galeria where userid = @userid", con);
            cmd.Parameters.AddWithValue("@userid", userid);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    photos.Add(reader.GetString(0));


            }
            catch (Exception ex)
            {

                HttpContext.Current.Trace.Write(ex.Message);

            }

            finally
            {

                con.Close();
            }


            return photos;


        }

        /// <summary>
        /// Gets all user picture with take and skip arguments in Linq query
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="paginatedPageNumber"></param>
        /// <returns></returns>

        public static List<string> GetAllUserPicturesPaginated(string userid, int paginatedPageNumber)
        {
            var photos = (from g in db.Galerias where g.profilowe == 0 && g.userid.ToString() == userid select g.path)
                .Skip((paginatedPageNumber - 1) * Usr.maxPhotosPerUploadNewProfilePicture).Take(maxPhotosPerUploadNewProfilePicture);

            List<string> phUser = new List<string>();

            foreach (string photo in photos)
            {
                phUser.Add(photo);

            }

            return phUser;

        }

        /// <summary>
        /// gets user profile picture
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static string GetProfilePicture(string userid)
        {

            string pathToUserPhoto = null;
            SqlConnection con = new SqlConnection(Usr.ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT path FROM Galeria where userid = @userid and profilowe=1", con);
            cmd.Parameters.AddWithValue("@userid", userid);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    pathToUserPhoto = reader.GetString(0);


            }
            catch (Exception ex)
            {

                HttpContext.Current.Trace.Write(ex.Message);

            }

            finally
            {

                con.Close();
            }
            if (pathToUserPhoto == null)
                pathToUserPhoto = defaultUserPhoto;

            return pathToUserPhoto;


        }


        public static int CountAllUserPictures(string userid)
        {

            var numberOfUserPictures = (from g in db.Galerias where g.userid.ToString() == userid select g.path).Count();

            return (int)numberOfUserPictures;

        }

        public static bool ChangeProfilePicture(string userid, string picturePath)
        {
            bool changed = false;

            SqlConnection con = new SqlConnection(Usr.ConnectionString);
            con.Open();
            
            SqlCommand cmd = new SqlCommand("UPDATE Galeria SET profilowe=1 where userid = @userid and path = @path", con);
            cmd.Parameters.AddWithValue("@userid", userid);
            cmd.Parameters.AddWithValue("@path", picturePath);
            try
            {
                int chang = cmd.ExecuteNonQuery();
                if(chang==1)
                    changed = true;


            }
            catch (Exception ex)
            {

                HttpContext.Current.Trace.Write(ex.Message);

            }

            finally
            {

                con.Close();
            }

            return changed;



        }

        public static string GetEventsCategories()
        {

            SqlConnection con = new SqlConnection(Usr.ConnectionString);
            con.Open();

            Dictionary<int, string> results = new Dictionary<int, string>();
            

            SqlCommand cmd = new SqlCommand("SELECT id,description FROM EventsCategories", con);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(reader.GetInt32(0), reader.GetString(1));

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

            string str =  DictionaryToJson(results);
            return str;


        }

        public static string DictionaryToJson(Dictionary<int,string> dict)
        {
            var results = dict.Select(d => string.Format("\"{0}\": \"{1}\"", d.Key, d.Value));
            return "{" + string.Join(",",results) +"}";


        }




    }
}