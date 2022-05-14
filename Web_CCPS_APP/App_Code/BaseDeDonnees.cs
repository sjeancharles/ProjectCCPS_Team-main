using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Web_CCPS_APP
{
    public class BaseDeDonnees
    {
        public String ChaineDeConnexion = ConfigurationManager.ConnectionStrings["connection"].ToString();
        public BaseDeDonnees(String cdc)
        {
            ChaineDeConnexion = cdc;
        }

        public BaseDeDonnees() { }
        /// <summary>
        /// Accéder and return a DataReader
        /// </summary>
        /// <returns></returns>
        public SqlDataReader GetDataReader(string csSQL)
        {
            SqlCommand SqlCmd;

            SqlDataReader dtValeursRetournees;
            try
            {
                SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection(ChaineDeConnexion);
                sqlConn.Open();

                SqlCmd = new SqlCommand(csSQL, sqlConn);
                dtValeursRetournees = SqlCmd.ExecuteReader(CommandBehavior.CloseConnection);

                sqlConn = null;
            }
            catch (Exception ex)
            {
                dtValeursRetournees = null;
                Debug.WriteLine(ex.Message);
            }

            return dtValeursRetournees;
        }

        /// <summary>
        /// Base function used by other functions to access the database and return a DataReader
        /// </summary>
        /// <returns></returns>
        public SqlDataReader GetDataReader(string csSQL, SqlConnection sqlConn)
        {
            SqlDataReader dtRetVal = null;
            //SqlConnection sqlConn = null;
            SqlCommand SqlCmd;

            try
            {
                //sqlConn = new System.Data.SqlClient.SqlConnection(mConnectionString);
                //sqlConn.Open();

                SqlCmd = new SqlCommand(csSQL, sqlConn);
                dtRetVal = SqlCmd.ExecuteReader(CommandBehavior.CloseConnection);

                //sqlConn = null;
            }
            catch (Exception ex)
            {
                dtRetVal = null;
                Debug.WriteLine(ex.Message);
                //if (sqlConn.State == ConnectionState.Open)
                //    sqlConn.Close();
            }

            return dtRetVal;
        }

        /// <summary>
        /// Base function used by other functions to access the database and return a DataReader
        /// </summary>
        /// <returns></returns>
        public SqlDataReader GetDataReaderWithParams(string csSQL, SqlConnection sqlConn, params SqlParameter[] paramList)
        {
            SqlDataReader dtRetVal = null;
            SqlCommand SqlCmd = null;

            try
            {
                //sqlConn = new System.Data.SqlClient.SqlConnection(mConnectionString);
                //sqlConn.Open();

                SqlCmd = new SqlCommand(csSQL, sqlConn);
                // Add parameters
                for (int iIndex = 0; iIndex < paramList.Length; iIndex++)
                {
                    SqlCmd.Parameters.Add(paramList[iIndex]);
                }
                dtRetVal = SqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
                SqlCmd.Parameters.Clear();
                SqlCmd = null;
                //sqlConn = null;
            }
            catch (Exception ex)
            {
                dtRetVal = null;
                if (SqlCmd != null)
                    SqlCmd.Parameters.Clear();

                Debug.WriteLine(ex.Message);
                //if (sqlConn.State == ConnectionState.Open)
                //    sqlConn.Close();
            }

            return dtRetVal;
        }

        /// <summary>
        /// Base function used by other functions to access the database and return a DataReader
        /// </summary>
        /// <returns></returns>
        public SqlDataReader GetDataReaderAvecParametres(string csSQL, params SqlParameter[] paramList)
        {
            SqlCommand SqlCmd = null;

            SqlDataReader dtRetVal;
            try
            {
                SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection(ChaineDeConnexion);
                sqlConn.Open();

                SqlCmd = new SqlCommand(csSQL, sqlConn);
                // Add parameters
                for (int iIndex = 0; iIndex < paramList.Length; iIndex++)
                {
                    SqlCmd.Parameters.Add(paramList[iIndex]);
                }
                dtRetVal = SqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
                SqlCmd.Parameters.Clear();
                SqlCmd = null;
                sqlConn = null;
            }
            catch (Exception ex)
            {
                dtRetVal = null;
                if (SqlCmd != null)
                    SqlCmd.Parameters.Clear();

                Debug.WriteLine(ex.Message);
            }

            return dtRetVal;
        }

        /// <summary>
        /// Helps to issue query command
        /// </summary>
        /// <returns></returns>
        public bool IssueCommand(string csSQL)
        {
            SqlCommand SqlCmd;
            bool bRetVal = false;

            try
            {

                SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection(ChaineDeConnexion);
                sqlConn.Open();

                SqlCmd = new SqlCommand(csSQL, sqlConn);
                SqlCmd.ExecuteNonQuery();
                SqlCmd = null;
                sqlConn.Close();
                sqlConn = null;
                bRetVal = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return bRetVal;
        }

        /// <summary>
        /// Helps to issue query command
        /// </summary>
        /// <returns></returns>
        public bool IssueCommandWithParams(string csSQL, params SqlParameter[] paramList)
        {
            SqlCommand SqlCmd = null;
            bool bRetVal = false;

            try
            {

                SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection(ChaineDeConnexion);
                sqlConn.Open();

                SqlCmd = new SqlCommand(csSQL, sqlConn);
                // Add parameters
                for (int iIndex = 0; iIndex < paramList.Length; iIndex++)
                {
                    SqlCmd.Parameters.Add(paramList[iIndex]);
                }

                SqlCmd.ExecuteNonQuery();
                bRetVal = true;
                SqlCmd.Parameters.Clear();
                SqlCmd = null;
                sqlConn.Close();
                sqlConn = null;
            }
            catch (Exception ex)
            {
                if (SqlCmd != null)
                    SqlCmd.Parameters.Clear();

                Debug.WriteLine(ex.Message);
            }

            return bRetVal;
        }

        /// <summary>
        /// Retrieve the scalar from query paramater
        /// </summary>
        /// <param name="csSql"></param>
        /// <returns></returns>
        public int GetScalar(string csSql)
        {
            SqlCommand SqlCmd;

            int iRetVal;
            try
            {
                SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection(ChaineDeConnexion);
                sqlConn.Open();

                SqlCmd = new SqlCommand();
                SqlCmd.Connection = sqlConn;
                SqlCmd.CommandText = csSql;
                iRetVal = int.Parse(SqlCmd.ExecuteScalar().ToString());

                sqlConn.Close();
                sqlConn = null;
                SqlCmd = null;
            }
            catch (Exception ex)
            {
                iRetVal = -1;
                Debug.WriteLine(ex.Message);
            }
            return iRetVal;
        }

        /// <summary>
        /// Retrieve the scalar from query paramater
        /// </summary>
        /// <param name="csSql"></param>
        /// <returns></returns>
        public double GetScalarDouble(string csSql)
        {
            SqlCommand SqlCmd;

            double dRetVal;
            try
            {
                SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection(ChaineDeConnexion);
                sqlConn.Open();

                SqlCmd = new SqlCommand();
                SqlCmd.Connection = sqlConn;
                SqlCmd.CommandText = csSql;
                dRetVal = double.Parse(SqlCmd.ExecuteScalar().ToString());

                sqlConn.Close();
                sqlConn = null;
                SqlCmd = null;
            }
            catch (Exception ex)
            {
                dRetVal = -1;
                Debug.WriteLine(ex.Message);
            }
            return dRetVal;
        }

        /// <summary>
        /// Helps to issue query command and returns identity value created
        /// Date: Avril 2013
        /// </summary>
        /// <returns></returns>
        public int GetScalarWithParams(string csSQL, params SqlParameter[] paramList)
        {
            SqlCommand SqlCmd = null;
            Int32 bRetVal = 0;

            try
            {
                SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection(ChaineDeConnexion);
                sqlConn.Open();

                SqlCmd = new SqlCommand(csSQL, sqlConn);
                SqlCommand SqlCmdNewID = new SqlCommand("SELECT @@IDENTITY", sqlConn);
                // Add parameters
                for (int iIndex = 0; iIndex < paramList.Length; iIndex++)
                {
                    SqlCmd.Parameters.Add(paramList[iIndex]);
                }

                SqlCmd.ExecuteScalar();
                bRetVal = Convert.ToInt32(SqlCmdNewID.ExecuteScalar().ToString());
                SqlCmd.Parameters.Clear();
                SqlCmd = null;
            }
            catch (Exception ex)
            {
                if (SqlCmd != null)
                    SqlCmd.Parameters.Clear();

                Debug.WriteLine(ex.Message);
            }

            return bRetVal;
        }

        /// <summary>
        /// Retrieve the scalar from query paramater
        /// </summary>
        /// <param name="csSql"></param>
        /// <returns></returns>
        public int GetScalarWithParams_old(string csSql, params SqlParameter[] paramList)
        {
            SqlCommand SqlCmd = null;

            int iRetVal;
            try
            {
                SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection(ChaineDeConnexion);
                sqlConn.Open();

                SqlCmd = new SqlCommand();
                SqlCmd.Connection = sqlConn;
                SqlCmd.CommandText = csSql;

                // Add parameters
                for (int iIndex = 0; iIndex < paramList.Length; iIndex++)
                {
                    SqlCmd.Parameters.Add(paramList[iIndex]);
                }

                iRetVal = int.Parse(SqlCmd.ExecuteScalar().ToString());

                sqlConn.Close();
                SqlCmd.Parameters.Clear();
                sqlConn = null;
                SqlCmd = null;
            }
            catch (Exception ex)
            {
                iRetVal = -1;
                if (SqlCmd != null)
                    SqlCmd.Parameters.Clear();

                Debug.WriteLine(ex.Message);
            }
            return iRetVal;
        }

        /// <summary>
        /// Get a dataset from DB
        /// </summary>
        /// <returns></returns>
        public DataSet GetDataSet(String sqlParam)
        {
            string sSql = sqlParam;
            DataSet ds = null;

            bool bRet = true;

            try
            {
                ds = new DataSet();
                sSql = sSql.Trim();


                SqlConnection sqlConn = new SqlConnection(ChaineDeConnexion);
                sqlConn.Open();

                // Get TSL_List table
                SqlDataAdapter da = new SqlDataAdapter(sSql, sqlConn);
                da.Fill(ds, "TABLE1");
                sqlConn.Close();
                sqlConn = null;
            }
            catch (Exception ex)
            {
                bRet = false;
                Debug.WriteLine(ex.Message);
            }

            if (!bRet)
                return null;

            return ds;

        }

        /// <summary>
        /// Base function returning the Start Date of the current Session
        /// </summary>
        /// <returns>DateTime</returns>
        public DateTime GetStartDateOfCurrentSession()
        {
            DateTime retValue = new DateTime();
            SqlCommand SqlCmd;

            try
            {

                SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection(ChaineDeConnexion);
                sqlConn.Open();

                SqlCmd = new SqlCommand("SELECT SessionDateDebut FROM DatesSessionCourante WHERE Actif = 1", sqlConn);
                SqlDataReader dtReader = SqlCmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (dtReader.Read())
                {
                    retValue = DateTime.Parse(dtReader["SessionDateDebut"].ToString());
                }

                sqlConn = null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return retValue;
        }

        /// <summary>
        /// Base function returning the End Date of the current Session
        /// </summary>
        /// <returns>DateTime</returns>
        public DateTime GetEndDateOfCurrentSession()
        {
            DateTime retValue = new DateTime();
            SqlCommand SqlCmd;

            try
            {

                SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection(ChaineDeConnexion);
                sqlConn.Open();

                SqlCmd = new SqlCommand("SELECT SessionDateFin FROM DatesSessionCourante WHERE Actif = 1", sqlConn);
                SqlDataReader dtReader = SqlCmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (dtReader.Read())
                {
                    retValue = DateTime.Parse(dtReader["SessionDateFin"].ToString());
                }

                sqlConn = null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return retValue;
        }

        /// <summary>
        /// Check if current user is in Group sGroupRole
        /// </summary>
        /// <param name="sGroupOrRole"></param>
        /// <returns>bool</returns>
        public static bool IsCurrentUserInGroup(string sGroupOrRole)
        {
            return HttpContext.Current.User.IsInRole(sGroupOrRole);
        }

        /// <summary>
        /// Check if a user is a student
        /// </summary>
        /// <param name="sEmail"></param>
        /// <returns>string</returns>
        public static string GetWindowsUser()
        {
            string sWindowsUser = HttpContext.Current.User.Identity.Name;
            sWindowsUser = sWindowsUser.Substring(sWindowsUser.IndexOf('\\') + 1);

            if (sWindowsUser == string.Empty)
            {
                sWindowsUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                sWindowsUser = sWindowsUser.Substring(sWindowsUser.IndexOf('\\') + 1);
            }
            return sWindowsUser;
        }

        /// <summary>
        /// Check if a user is a student
        /// </summary>
        /// <param name="sEmail"></param>
        /// <returns>bool</returns>
        public bool IsUserStudent(string sEmail)
        {
            string sSql = "SELECT Count(StudentId) FROM Students Where Email = @Email";
            SqlParameter paramEmail = new SqlParameter("@Email", SqlDbType.VarChar);
            paramEmail.Value = sEmail;

            int iFound = GetScalarWithParams(sSql, paramEmail);

            bool bRetVal = iFound > 0;

            return bRetVal;
        }

        /// <summary>
        /// Check if a user is an User
        /// </summary>
        /// <param name="sEmail"></param>
        /// <returns></returns>
        public bool IsUserUser(string sEmail)
        {
            string sSql = "SELECT Count(UserId) FROM Users Where Email = @Email";
            SqlParameter paramEmail = new SqlParameter("@Email", SqlDbType.VarChar);
            paramEmail.Value = sEmail;

            int iFound = GetScalarWithParams(sSql, paramEmail);

            bool bRetVal = iFound > 0;

            return bRetVal;
        }
        /// <summary>
        /// Get the password given an email (or username)
        /// </summary>
        /// <param name="sEmail"></param>
        /// <returns></returns>
        //private  string GetUserPassword(string sEmail)
        //{
        //    string sRetVal = "";
        //    try
        //    {
        //        string sSql = "SELECT Password FROM Users Where Email = @Email";
        //        SqlParameter paramEmail = new SqlParameter("@Email", SqlDbType.VarChar);
        //        paramEmail.Value = sEmail;

        //        SqlDataReader dt = GetDataReaderWithParams(sSql, paramEmail);
        //        if (dt.Read())
        //        {
        //            XCryptEngine MyScriptEngine = null;

        //            MyScriptEngine = new XCryptEngine();
        //            MyScriptEngine.Algorithm = XCryptEngine.AlgorithmType.DES;
        //            MyScriptEngine.InitializeEngine();
        //            sRetVal = MyScriptEngine.Decrypt(dt["Password"].ToString());// After Script Engine Init
        //            MyScriptEngine.DestroyEngine();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.Message);
        //    }

        //    return sRetVal;
        //}

        /// <summary>
        /// Use to send password to user via email
        /// </summary>
        /// <param name="sEmail"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        //public static bool SendMailToUser(string sEmail, string sPassword)
        //{
        //    bool bRetVal = false;
        //    try
        //    {
        //        // Send Mail to User
        //        string csMessage = "This is your requested password for the HG Site: ";
        //        if (sPassword == string.Empty)
        //        {
        //            sPassword = GetUserPassword(sEmail);
        //        }

        //        //// Command line argument must the the SMTP host.
        //        //SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["SMTP_HOST"].ToString());
        //        //// Specify the e-mail sender.
        //        //MailAddress from = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["HG_MAIL_FROM"].ToString());
        //        //// Set destinations for the e-mail message.
        //        //MailAddress to = new MailAddress(sEmail);
        //        //// Specify the message content.
        //        //MailMessage message = new MailMessage(from, to);
        //        //message.Body = string.Format("<html><body>From {0}<table></tr><td>{1}</td><td>{2}</td></tr></table></body></html>", sEmail, csMessage, sPassword);
        //        //message.Subject = System.Configuration.ConfigurationManager.AppSettings["PasswordMail_Subject"].ToString();
        //        //message.IsBodyHtml = true;
        //        //client.Host = ConfigurationManager.AppSettings["SMTP_HOST"].ToString();
        //        //client.UseDefaultCredentials = true;
        //        //client.Send(message);
        //        //// Clean up.
        //        //message.Dispose();
        //        SmtpMail.SmtpServer = System.Configuration.ConfigurationManager.AppSettings["SMTP_HOST"];   // Server name may needed or can be empty
        //        System.Web.Mail.MailMessage msg = new MailMessage();
        //        msg.Body = string.Format("<html><body><table></tr><td>{0}</td><td>{1}</td></tr></table></body></html>", csMessage, sPassword);
        //        msg.To = sEmail;
        //        msg.From = ConfigurationManager.AppSettings["HG_MAIL_FROM"].ToString();
        //        msg.Subject = ConfigurationManager.AppSettings["PasswordMail_Subject"].ToString();
        //        msg.BodyFormat = System.Web.Mail.MailFormat.Html;
        //        SmtpMail.Send(msg);
        //        msg = null;

        //        bRetVal = true;

        //    }
        //    catch (Exception ex)
        //    {
        //        bRetVal = false;
        //        Debug.WriteLine(ex.Message);
        //    }
        //    return bRetVal;
        //}

    }
}
