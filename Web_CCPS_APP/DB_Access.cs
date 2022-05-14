using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Collections;
using System.Text;
using System.Web.Mail;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.IO;
using Microsoft.Win32;
using System.Web.Hosting;
using System.Web.Profile;
using System.Web.ClientServices.Providers;


/// <summary>
/// Helper class to access Database
/// </summary>
public partial class DB_Access
{
    // Add property for error reporting:



    public DB_Access()
    {
    }

    //public static XmlDataSource GetMenuDataSource(string sRole, string sMapPath)
    //{
    //    XmlDataSource objData = new XmlDataSource();
    //    objData.XPath = "siteMap/siteMapNode";
    //    switch (sRole.ToLower())
    //    {
    //        case "admin":
    //            objData.DataFile = sMapPath + @"\App_Data\Admin.sitemap";
    //            break;
    //        case "leader":
    //            objData.DataFile = sMapPath + @"\App_Data\Leader.sitemap";
    //            break;
    //        case "host":
    //            objData.DataFile = sMapPath + @"\App_Data\Host.sitemap";
    //            break;
    //        default:
    //            objData.DataFile = sMapPath + @"\Web.Sitemap";
    //            break;
    //    }
    //    objData.DataBind();
    //    return objData;
    //}

    public static String GetHeaderPrintString()
    {
        /*****************************************************************************************************
        Created By: Ferdous Md. Jannatul, Sr. Software Engineer
        Created On: 10 December 2005
        Last Modified: 13 April 2006
        ******************************************************************************************************/
        //Generating Pop-up Print Preview page
        return " " +
        " function getPrint(print_area)" +
        " {	" +
        " 	//Creating new page" +
        " 	var pp = window.open();" +
        " 	//Adding HTML opening tag with <HEAD> … </HEAD> portion " +
        " 	pp.document.writeln('<HTML><HEAD><title>Print Preview</title><LINK href=Styles.css  type='text/css' rel='stylesheet'>')" +
        " 	pp.document.writeln('<LINK href=PrintStyle.css  type='text/css' rel='stylesheet' media='print'><base target='_self'></HEAD>')" +
        " 	//Adding Body Tag" +
        " 	pp.document.writeln('<body MS_POSITIONING='GridLayout' bottomMargin='0' leftMargin='0' topMargin='0' rightMargin='0'>');" +
        " 	//Adding form Tag" +
        " 	pp.document.writeln('<form  method='post'>');" +
        " 	//Creating two buttons Print and Close within a table" +
        " 	pp.document.writeln('<TABLE width=100%><TR><TD></TD></TR><TR><TD align=right><INPUT ID='PRINT' type='button' value='Print' onclick='javascript:location.reload(true);window.print();'><INPUT ID='CLOSE' type='button' value='Close' onclick='window.close();'></TD></TR><TR><TD></TD></TR></TABLE>');" +
        " 	//Writing print area of the calling page" +
        " 	pp.document.writeln(document.getElementById(print_area).innerHTML);" +
        " 	//Ending Tag of </form>, </body> and </HTML> " +
        " 	pp.document.writeln('</form></body></HTML>'); " +
        "}		 ";

    }



    /// <summary>
    /// Base function used by other functions to get Taux Gourdes/Dollar US
    /// Return a double or 0.0 if error
    /// </summary>
    /// <returns>double</returns>
    public static double GetTauxGourdesDollarUS()
    {
        double dtRetVal = 0.0;
        SqlConnection sqlConn = null;
        SqlCommand SqlCmd;

        try
        {
            string sSqlConnString = ConfigurationManager.ConnectionStrings["connection"].ToString();
            sqlConn = new System.Data.SqlClient.SqlConnection(sSqlConnString);
            sqlConn.Open();

            SqlCmd = new SqlCommand("SELECT * FROM Parametres WHERE ID = 1000", sqlConn);
            SqlDataReader dtReader = SqlCmd.ExecuteReader(CommandBehavior.CloseConnection);

            if (dtReader.Read())
            {
                dtRetVal = double.Parse(dtReader["Taux"].ToString());
            }

            sqlConn = null;
        }
        catch (Exception ex)
        {
            dtRetVal = 0.0;
            Debug.WriteLine(ex.Message);
        }

        return dtRetVal;
    }

    /// <summary>
    /// Base function used by other functions to get Taux Gourdes/Dollar US
    /// Return a double or 0.0 if error
    /// </summary>
    /// <returns>double</returns>
    public static double GetTauxGourdesDollarUS(int DepenseID)
    {
        double dtRetVal = 0.0;

        // Get Taux from Depense entry
        dtRetVal = GetTauxGourdesDollarUSFromDepense(DepenseID);
        if (dtRetVal > 0.0)
        {
            return dtRetVal;
        }

        // Get Taux from General Settings
        return GetTauxGourdesDollarUS();
    }

    /// <summary>
    /// 
    /// Return a double or 0.0 if error
    /// </summary>
    /// <returns>double</returns>
    public static double GetTauxGourdesDollarUSFromDepense(int DepenseID)
    {
        double dtRetVal = 0.0;
        SqlConnection sqlConn = null;
        SqlCommand SqlCmd;

        try
        {
            string sSqlConnString = ConfigurationManager.ConnectionStrings["connection"].ToString();
            sqlConn = new System.Data.SqlClient.SqlConnection(sSqlConnString);
            sqlConn.Open();

            SqlCmd = new SqlCommand();
            SqlCmd.Connection = sqlConn;
            SqlCmd.CommandText = "SELECT Taux_Interet FROM Depenses WHERE DepenseID = " + DepenseID;
            dtRetVal = Double.Parse(SqlCmd.ExecuteScalar().ToString());

            sqlConn.Close();
            sqlConn = null;
            SqlCmd = null;
        }
        catch (Exception ex)
        {
            dtRetVal = 0.0;
            Debug.WriteLine(ex.Message);
        }
        return dtRetVal;
    }


    /// <summary>
    /// Base function used by other functions to access the database and return a DataReader
    /// </summary>
    /// <returns></returns>
    public static SqlDataReader GetDataReader(string csSQL)
    {
        SqlDataReader dtRetVal = null;
        SqlConnection sqlConn = null;
        SqlCommand SqlCmd;

        try
        {
            string sSqlConnString = ConfigurationManager.ConnectionStrings["connection"].ToString();
            sqlConn = new System.Data.SqlClient.SqlConnection(sSqlConnString);
            sqlConn.Open();

            SqlCmd = new SqlCommand(csSQL, sqlConn);
            dtRetVal = SqlCmd.ExecuteReader(CommandBehavior.CloseConnection);

            sqlConn = null;
        }
        catch (Exception ex)
        {
            dtRetVal = null;
            Debug.WriteLine(ex.Message);
        }

        return dtRetVal;
    }

    /// <summary>
    /// Base function used by other functions to access the database and return a DataReader
    /// </summary>
    /// <returns></returns>
    public static SqlDataReader GetDataReaderWithParams(string csSQL, params SqlParameter[] paramList)
    {
        SqlDataReader dtRetVal = null;
        SqlConnection sqlConn = null;
        SqlCommand SqlCmd = null;

        try
        {
            string sSqlConnString = ConfigurationManager.ConnectionStrings["connection"].ToString();
            sqlConn = new System.Data.SqlClient.SqlConnection(sSqlConnString);
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
    public static bool IssueCommand(string csSQL)
    {
        SqlConnection sqlConn = null;
        SqlCommand SqlCmd;
        bool bRetVal = false;

        try
        {
            string sSqlConnString = ConfigurationManager.ConnectionStrings["connection"].ToString();
            sqlConn = new System.Data.SqlClient.SqlConnection(sSqlConnString);
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
    public static bool IssueCommandWithParams(string csSQL, params SqlParameter[] paramList)
    {
        SqlConnection sqlConn = null;
        SqlCommand SqlCmd = null;
        bool bRetVal = false;

        try
        {
            string sSqlConnString = ConfigurationManager.ConnectionStrings["connection"].ToString();
            sqlConn = new System.Data.SqlClient.SqlConnection(sSqlConnString);
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
    public static int GetScalar(string csSql)
    {
        int iRetVal = 0;
        SqlConnection sqlConn = null;
        SqlCommand SqlCmd;

        try
        {
            string sSqlConnString = ConfigurationManager.ConnectionStrings["connection"].ToString();
            sqlConn = new System.Data.SqlClient.SqlConnection(sSqlConnString);
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
    public static double GetScalarDouble(string csSql)
    {
        double dRetVal = 0;
        SqlConnection sqlConn = null;
        SqlCommand SqlCmd;

        try
        {
            string sSqlConnString = ConfigurationManager.ConnectionStrings["connection"].ToString();
            sqlConn = new System.Data.SqlClient.SqlConnection(sSqlConnString);
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
    public static int GetScalarWithParams(string csSQL, params SqlParameter[] paramList)
    {
        SqlCommand SqlCmd = null;
        SqlCommand SqlCmdNewID = null;
        SqlConnection sqlConn = null;
        Int32 bRetVal = 0;

        try
        {
            string sSqlConnString = ConfigurationManager.ConnectionStrings["connection"].ToString();
            sqlConn = new System.Data.SqlClient.SqlConnection(sSqlConnString);
            sqlConn.Open();

            SqlCmd = new SqlCommand(csSQL, sqlConn);
            SqlCmdNewID = new SqlCommand("SELECT @@IDENTITY", sqlConn);
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
    public static int GetScalarWithParams_old(string csSql, params SqlParameter[] paramList)
    {
        int iRetVal = 0;
        SqlConnection sqlConn = null;
        SqlCommand SqlCmd = null;

        try
        {
            string sSqlConnString = ConfigurationManager.ConnectionStrings["connection"].ToString();
            sqlConn = new System.Data.SqlClient.SqlConnection(sSqlConnString);
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
    public static DataSet GetDataSet(String sqlParam)
    {
        SqlConnection sqlConn = null;
        string sSql = sqlParam;
        DataSet ds = null;

        bool bRet = true;

        try
        {
            ds = new DataSet();
            sSql = sSql.Trim();

            string ConnString = ConfigurationManager.ConnectionStrings["connection"].ToString();
            sqlConn = new SqlConnection(ConnString);
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
    static public DateTime GetStartDateOfCurrentSession()
    {
        DateTime retValue = new DateTime();

        SqlConnection sqlConn = null;
        SqlCommand SqlCmd;

        try
        {
            string sSqlConnString = ConfigurationManager.ConnectionStrings["connection"].ToString();
            sqlConn = new System.Data.SqlClient.SqlConnection(sSqlConnString);
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
    static public DateTime GetEndDateOfCurrentSession()
    {
        DateTime retValue = new DateTime();

        SqlConnection sqlConn = null;
        SqlCommand SqlCmd;

        try
        {
            string sSqlConnString = ConfigurationManager.ConnectionStrings["connection"].ToString();
            sqlConn = new System.Data.SqlClient.SqlConnection(sSqlConnString);
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
    /// Retrieve dynamic content for a specific page
    /// </summary>
    /// <returns></returns>
    static public string GetDynamicContent(string sSiteName, string sPageName, string sSectionName)
    {
        SqlConnection sqlConn = null;
        SqlCommand SqlCmd = null;
        string sRetVal = "";

        try
        {
            SqlDataReader dtVal = null;

            string sSqlConnString = ConfigurationManager.ConnectionStrings["connection"].ToString();
            sqlConn = new System.Data.SqlClient.SqlConnection(sSqlConnString);
            sqlConn.Open();

            SqlCmd = new SqlCommand();
            SqlCmd.Connection = sqlConn;
            SqlCmd.CommandType = CommandType.Text;
            SqlCmd.CommandText = "SELECT SectionContent FROM DynamicContent WHERE SiteName = @SiteName AND  PageName = @PageName AND SectionName =@SectionName";

            SqlParameter paramSiteName = new SqlParameter("@SiteName", SqlDbType.VarChar);
            paramSiteName.Value = sSiteName;
            SqlCmd.Parameters.Add(paramSiteName);

            SqlParameter paramPageName = new SqlParameter("@PageName", SqlDbType.VarChar);
            paramPageName.Value = sPageName;
            SqlCmd.Parameters.Add(paramPageName);

            SqlParameter paramSectionName = new SqlParameter("@SectionName", SqlDbType.VarChar);
            paramSectionName.Value = sSectionName;
            SqlCmd.Parameters.Add(paramSectionName);


            dtVal = SqlCmd.ExecuteReader();
            if (dtVal != null)
            {
                if (dtVal.Read())
                {
                    sRetVal = dtVal["SectionContent"].ToString();
                }
                dtVal.Close();
                dtVal = null;
            }
        }
        catch (Exception ex)
        {
            if (SqlCmd != null)
                SqlCmd.Parameters.Clear();

            Debug.WriteLine(ex.Message);
        }
        finally
        {
            sqlConn.Close();
        }

        return sRetVal;
    }

    /// <summary>
    /// Retrieve dynamic content for a specific page
    /// </summary>
    /// <returns></returns>
    static public bool SetDynamicContent(string sSiteName, string sPageName, string sSectionName, string sSectionContent)
    {
        SqlConnection sqlConn = null;
        bool bRetVal = false;
        SqlCommand SqlCmd = null;

        try
        {
            //SqlDataReader dtVal = null;

            string sSqlConnString = ConfigurationManager.ConnectionStrings["connection"].ToString();
            sqlConn = new System.Data.SqlClient.SqlConnection(sSqlConnString);
            sqlConn.Open();

            SqlCmd = new SqlCommand();
            SqlCmd.Connection = sqlConn;
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.CommandText = "UPDATE DynamicContent SET SectionContent = '@SectionContent' WHERE SiteName = '@SiteName' AND  PageName = '@PageName' AND SectionName ='@SectionName'";

            SqlParameter paramContent = new SqlParameter("@SectionContent", SqlDbType.VarChar);
            paramContent.Value = sSectionContent;
            SqlCmd.Parameters.Add(paramContent);

            SqlParameter paramSiteName = new SqlParameter("@SiteName", SqlDbType.VarChar);
            paramSiteName.Value = sSiteName;
            SqlCmd.Parameters.Add(paramSiteName);

            SqlParameter paramPageName = new SqlParameter("@PageName", SqlDbType.VarChar);
            paramPageName.Value = sPageName;
            SqlCmd.Parameters.Add(paramPageName);

            SqlParameter paramSectionName = new SqlParameter("@SectionName", SqlDbType.VarChar);
            paramSectionName.Value = sSectionName;
            SqlCmd.Parameters.Add(paramSectionName);

            SqlCmd.ExecuteNonQuery();
            SqlCmd = null;
            bRetVal = true;
        }
        catch (Exception ex)
        {
            if (SqlCmd != null)
                SqlCmd.Parameters.Clear();

            Debug.WriteLine(ex.Message);
        }
        finally
        {
            sqlConn.Close();
        }

        return bRetVal;
    }

    /// <summary>
    /// Retrieve dynamic content for a specific page
    /// </summary>
    /// <returns></returns>
    static public string GetDynamicContentbyID(string sIdContent)
    {
        SqlConnection sqlConn = null;
        string sRetVal = "";
        SqlCommand SqlCmd = null;

        try
        {
            SqlDataReader dtVal = null;

            string sSqlConnString = ConfigurationManager.ConnectionStrings["connection"].ToString();
            sqlConn = new System.Data.SqlClient.SqlConnection(sSqlConnString);
            sqlConn.Open();

            SqlCmd = new SqlCommand();
            SqlCmd.Connection = sqlConn;
            SqlCmd.CommandType = CommandType.Text;
            SqlCmd.CommandText = "SELECT SectionContent FROM DynamicContent WHERE IdContent = @IdContent";

            SqlParameter paramIdContent = new SqlParameter("@IdContent", SqlDbType.Int);
            paramIdContent.Value = int.Parse(sIdContent);
            SqlCmd.Parameters.Add(paramIdContent);

            dtVal = SqlCmd.ExecuteReader();
            if (dtVal != null)
            {
                if (dtVal.Read())
                {
                    sRetVal = dtVal["SectionContent"].ToString();
                }
                dtVal.Close();
                dtVal = null;
            }
        }
        catch (Exception ex)
        {
            if (SqlCmd != null)
                SqlCmd.Parameters.Clear();

            Debug.WriteLine(ex.Message);
        }
        finally
        {
            sqlConn.Close();
        }

        return sRetVal;
    }

    /// <summary>
    /// Retrieve Student FullName
    /// </summary>
    /// <returns></returns>
    static public string GetStudentFullName(string sStudentID)
    {
        SqlConnection sqlConn = null;
        string sRetVal = "";
        SqlCommand SqlCmd = null;

        try
        {
            SqlDataReader dtVal = null;

            string sSqlConnString = ConfigurationManager.ConnectionStrings["connection"].ToString();
            sqlConn = new System.Data.SqlClient.SqlConnection(sSqlConnString);
            sqlConn.Open();

            SqlCmd = new SqlCommand();
            SqlCmd.Connection = sqlConn;
            SqlCmd.CommandType = CommandType.Text;
            SqlCmd.CommandText = "SELECT Prenom + ' ' + Nom as NomComplet FROM Personnes WHERE PersonneID = @PersonneID";

            SqlParameter paramStudentID = new SqlParameter("@PersonneID", SqlDbType.Int);
            paramStudentID.Value = int.Parse(sStudentID);
            SqlCmd.Parameters.Add(paramStudentID);

            dtVal = SqlCmd.ExecuteReader();
            if (dtVal != null)
            {
                if (dtVal.Read())
                {
                    sRetVal = dtVal["NomComplet"].ToString();
                }
                dtVal.Close();
                dtVal = null;
            }
        }
        catch (Exception ex)
        {
            if (SqlCmd != null)
                SqlCmd.Parameters.Clear();

            Debug.WriteLine(ex.Message);
        }
        finally
        {
            sqlConn.Close();
        }

        return sRetVal;
    }

    /// <summary>
    /// Retrieve dynamic content for a specific page
    /// </summary>
    /// <returns></returns>
    static public bool SetDynamicContentbyID(string sIdContent, string sSectionContent)
    {
        SqlConnection sqlConn = null;
        bool bRetVal = false;
        SqlCommand SqlCmd = null;

        try
        {
            //SqlDataReader dtVal = null;

            string sSqlConnString = ConfigurationManager.ConnectionStrings["connection"].ToString();
            sqlConn = new System.Data.SqlClient.SqlConnection(sSqlConnString);
            sqlConn.Open();

            SqlCmd = new SqlCommand();
            SqlCmd.Connection = sqlConn;
            SqlCmd.CommandType = CommandType.Text;
            SqlCmd.CommandText = "UPDATE DynamicContent SET SectionContent = @SectionContent WHERE IdContent = @IdContent";

            SqlParameter paramContent = new SqlParameter("@SectionContent", SqlDbType.VarChar);
            paramContent.Value = sSectionContent.Replace("\r", "").Replace("\n", "").Replace("\t", "");
            SqlCmd.Parameters.Add(paramContent);

            SqlParameter paramIdContent = new SqlParameter("@IdContent", SqlDbType.Int);
            paramIdContent.Value = int.Parse(sIdContent);
            SqlCmd.Parameters.Add(paramIdContent);

            SqlCmd.ExecuteNonQuery();
            SqlCmd = null;
            bRetVal = true;
        }
        catch (Exception ex)
        {
            if (SqlCmd != null)
                SqlCmd.Parameters.Clear();

            Debug.WriteLine(ex.Message);
        }
        finally
        {
            sqlConn.Close();
        }

        return bRetVal;
    }

    /// <summary>
    /// Create Expense Category
    /// </summary>
    /// <returns></returns>
	public static bool AddExpenseCategory(string sCategoryName, string sCategoryDesc)
    {
        string sSql = "INSERT ExpenseCategory (CategoryName, CategoryDesc) VALUES (@CategoryName, @CategoryDesc)";
        SqlParameter paramCategoryName = new SqlParameter("@CategoryName", SqlDbType.VarChar);
        paramCategoryName.Value = sCategoryName;
        SqlParameter paramCategoryDesc = new SqlParameter("@CategoryDesc", SqlDbType.VarChar);
        paramCategoryDesc.Value = sCategoryDesc;

        return IssueCommandWithParams(sSql, paramCategoryName, paramCategoryDesc);
    }

    /// <summary>
    /// Check if a class exist
    /// </summary>
    /// <param name="sClassOf"></param>
    /// <returns></returns>
    public static bool ExistClassOf(string sClassOf)
    {
        string sSql = "SELECT Count(ClassId) FROM ClassOf Where Name = @ClassName";
        SqlParameter paramClassOf = new SqlParameter("@ClassName", SqlDbType.VarChar);
        paramClassOf.Value = sClassOf;

        int iFound = GetScalarWithParams(sSql, paramClassOf);
        return iFound > 0;
    }

    /// <summary>
    /// Add a new class
    /// </summary>
    /// <param name="sClassOf"></param>
    /// <returns></returns>
    public static bool AddClassOf(string sClassOf)
    {
        string sSql = "INSERT ClassOf (Name) VALUES (@ClassName)";
        SqlParameter paramClassOf = new SqlParameter("@ClassName", SqlDbType.VarChar);
        paramClassOf.Value = sClassOf;

        return IssueCommandWithParams(sSql, paramClassOf);
    }

    /// <summary>
    /// Check if a user exist in database
    /// </summary>
    /// <param name="sEmail"></param>
    /// <returns></returns>
    public static bool IsUserValid(string sEmail)
    {
        bool bRetVal = false;
        string sSql = "SELECT Count(Email) FROM Users Where Email = @Email";
        SqlParameter paramEmail = new SqlParameter("@Email", SqlDbType.VarChar);
        paramEmail.Value = sEmail;

        int iFound = GetScalarWithParams(sSql, paramEmail);

        bRetVal = iFound > 0;

        return bRetVal;
    }

    /// <summary>
    /// Check if a user exist in database
    /// </summary>
    /// <param name="sEmail"></param>
    /// <returns>bool</returns>
    public static bool IsUserValid(string sUserName, string sPassword)
    {
        bool bRetVal = false;
        string sSql = "SELECT Count(UserEmail) FROM Users Where UserName = @UserName and UserPassword = @Password";
        SqlParameter paramUserName = new SqlParameter("@UserName", SqlDbType.VarChar);
        SqlParameter paramPassword = new SqlParameter("@Password", SqlDbType.VarChar);
        paramUserName.Value = sUserName;
        paramPassword.Value = sPassword;

        int iFound = GetScalarWithParams(sSql, paramUserName, paramPassword);

        bRetVal = iFound > 0;

        return bRetVal;
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
    public static bool IsUserStudent(string sEmail)
    {
        bool bRetVal = false;
        string sSql = "SELECT Count(StudentId) FROM Students Where Email = @Email";
        SqlParameter paramEmail = new SqlParameter("@Email", SqlDbType.VarChar);
        paramEmail.Value = sEmail;

        int iFound = GetScalarWithParams(sSql, paramEmail);

        bRetVal = iFound > 0;

        return bRetVal;
    }

    /// <summary>
    /// Check if a user is an User
    /// </summary>
    /// <param name="sEmail"></param>
    /// <returns></returns>
    public static bool IsUserUser(string sEmail)
    {
        bool bRetVal = false;
        string sSql = "SELECT Count(UserId) FROM Users Where Email = @Email";
        SqlParameter paramEmail = new SqlParameter("@Email", SqlDbType.VarChar);
        paramEmail.Value = sEmail;

        int iFound = GetScalarWithParams(sSql, paramEmail);

        bRetVal = iFound > 0;

        return bRetVal;
    }
    /// <summary>
    /// Get the password given an email (or username)
    /// </summary>
    /// <param name="sEmail"></param>
    /// <returns></returns>
    private static string GetUserPassword(string sEmail)
    {
        string sRetVal = "";
        try
        {
            string sSql = "SELECT Password FROM Users Where Email = @Email";
            SqlParameter paramEmail = new SqlParameter("@Email", SqlDbType.VarChar);
            paramEmail.Value = sEmail;

            //SqlDataReader dt = GetDataReaderWithParams(sSql, paramEmail);
            //if (dt.Read())
            //{
            //    XCryptEngine MyScriptEngine = null;

            //    MyScriptEngine = new XCryptEngine();
            //    MyScriptEngine.Algorithm = XCryptEngine.AlgorithmType.DES;
            //    MyScriptEngine.InitializeEngine();
            //    sRetVal = MyScriptEngine.Decrypt(dt["Password"].ToString());// After Script Engine Init
            //    MyScriptEngine.DestroyEngine();
            //}
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        return sRetVal;
    }

    /// <summary>
    /// Use to send password to user via email
    /// </summary>
    /// <param name="sEmail"></param>
    /// <param name="sPassword"></param>
    /// <returns></returns>
    public static bool SendMailToUser(string sEmail, string sPassword)
    {
        bool bRetVal = false;
        try
        {
            // Send Mail to User
            string csMessage = "This is your requested password for the HG Site: ";
            if (sPassword == string.Empty)
            {
                sPassword = GetUserPassword(sEmail);
            }

            //// Command line argument must the the SMTP host.
            //SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["SMTP_HOST"].ToString());
            //// Specify the e-mail sender.
            //MailAddress from = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["HG_MAIL_FROM"].ToString());
            //// Set destinations for the e-mail message.
            //MailAddress to = new MailAddress(sEmail);
            //// Specify the message content.
            //MailMessage message = new MailMessage(from, to);
            //message.Body = string.Format("<html><body>From {0}<table></tr><td>{1}</td><td>{2}</td></tr></table></body></html>", sEmail, csMessage, sPassword);
            //message.Subject = System.Configuration.ConfigurationManager.AppSettings["PasswordMail_Subject"].ToString();
            //message.IsBodyHtml = true;
            //client.Host = ConfigurationManager.AppSettings["SMTP_HOST"].ToString();
            //client.UseDefaultCredentials = true;
            //client.Send(message);
            //// Clean up.
            //message.Dispose();
            SmtpMail.SmtpServer = System.Configuration.ConfigurationManager.AppSettings["SMTP_HOST"];   // Server name may needed or can be empty
            System.Web.Mail.MailMessage msg = new MailMessage();
            msg.Body = string.Format("<html><body><table></tr><td>{0}</td><td>{1}</td></tr></table></body></html>", csMessage, sPassword);
            msg.To = sEmail;
            msg.From = ConfigurationManager.AppSettings["HG_MAIL_FROM"].ToString();
            msg.Subject = ConfigurationManager.AppSettings["PasswordMail_Subject"].ToString();
            msg.BodyFormat = System.Web.Mail.MailFormat.Html;
            SmtpMail.Send(msg);
            msg = null;

            bRetVal = true;

        }
        catch (Exception ex)
        {
            bRetVal = false;
            Debug.WriteLine(ex.Message);
        }
        return bRetVal;
    }

    /// <summary>
    /// Create New Student
    /// </summary>
    /// <returns></returns>
    public static bool CreateStudentWithLoginAccount(string csSQL, params SqlParameter[] paramList)
    {
        SqlConnection sqlConn = null;
        SqlCommand SqlCmd = null;
        SqlTransaction sqlTran = null;
        bool bRetVal = false;

        try
        {
            string sSqlConnString = ConfigurationManager.ConnectionStrings["connection"].ToString();
            sqlConn = new System.Data.SqlClient.SqlConnection(sSqlConnString);
            sqlConn.Open();

            Guid guidUserId = Guid.NewGuid();

            SqlParameter paramUserId = new SqlParameter("@UserId", SqlDbType.UniqueIdentifier);
            paramUserId.Value = guidUserId;

            sqlTran = sqlConn.BeginTransaction("CreateNewStudent");
            SqlCmd = new SqlCommand();
            SqlCmd.CommandText = "INSERT INTO Users (UserId, Email, Password, RoleId) VALUES (@UserId, @Email, @Password, @RoleId)";
            SqlCmd.Transaction = sqlTran;

            // Add parameters
            for (int iIndex = 0; iIndex < paramList.Length; iIndex++)
            {
                SqlCmd.Parameters.Add(paramList[iIndex]);
            }

            SqlCmd.Connection = sqlConn;
            SqlCmd.ExecuteNonQuery();   // Create New User
            SqlCmd.CommandText = csSQL;
            SqlCmd.ExecuteNonQuery();   // Create New Student
            sqlTran.Commit();
            bRetVal = true;
            SqlCmd.Parameters.Clear();
            SqlCmd = null;
        }
        catch (Exception ex)
        {
            sqlTran.Rollback();
            if (SqlCmd != null)
                SqlCmd.Parameters.Clear();

            Debug.WriteLine(ex.Message);
        }
        finally
        {
            sqlConn.Close();
            sqlConn = null;
        }

        return bRetVal;
    }

    /// <summary>
    /// Create New User
    /// </summary>
    /// <returns>bool</returns>
    public static bool CreateUserWithLoginAccount(string csSQL, params SqlParameter[] paramList)
    {
        SqlConnection sqlConn = null;
        SqlCommand SqlCmd = null;
        SqlTransaction sqlTran = null;
        bool bRetVal = false;

        try
        {
            string sSqlConnString = ConfigurationManager.ConnectionStrings["connection"].ToString();
            sqlConn = new System.Data.SqlClient.SqlConnection(sSqlConnString);
            sqlConn.Open();

            Guid guidUserId = Guid.NewGuid();

            SqlParameter paramUserId = new SqlParameter("@UserId", SqlDbType.UniqueIdentifier);
            paramUserId.Value = guidUserId;

            sqlTran = sqlConn.BeginTransaction("CreateNewUser");
            SqlCmd = new SqlCommand();
            SqlCmd.CommandText = "INSERT INTO Users (UserId, Email, Password, RoleId) VALUES (@UserId, @Email, @Password, @RoleId)";
            SqlCmd.Transaction = sqlTran;

            // Add parameters
            for (int iIndex = 0; iIndex < paramList.Length; iIndex++)
            {
                SqlCmd.Parameters.Add(paramList[iIndex]);
            }

            SqlCmd.Connection = sqlConn;
            SqlCmd.ExecuteNonQuery();   // Create New User
            SqlCmd.CommandText = csSQL;
            SqlCmd.ExecuteNonQuery();   // Create New User
            sqlTran.Commit();
            bRetVal = true;
            SqlCmd.Parameters.Clear();
            SqlCmd = null;
        }
        catch (Exception ex)
        {
            sqlTran.Rollback();
            if (SqlCmd != null)
                SqlCmd.Parameters.Clear();

            Debug.WriteLine(ex.Message);
        }
        finally
        {
            sqlConn.Close();
            sqlConn = null;
        }

        return bRetVal;
    }

    /// <summary>
    /// Create New File Record when FileId is needed
    /// </summary>
    /// <returns></returns>
    public static int CreateNewFileEntry(string sStudentId, string stringsEmail,
                                         string sFilename, string sFileType, int iFileSize, bool bPublicFile, string sReqId)
    {
        int iUploadId = 0;
        string sSqlConnString = ConfigurationManager.ConnectionStrings["connection"].ToString();

        SqlConnection sqlConn = null;

        try
        {
            sqlConn = new SqlConnection(sSqlConnString);
            // Create Adapter
            SqlDataAdapter dbAdapt = new SqlDataAdapter("SELECT * FROM UploadedFiles", sqlConn);

            // We need this to get an ID back from the database
            dbAdapt.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            // Create and initialize CommandBuilder
            SqlCommandBuilder dbCB = new SqlCommandBuilder(dbAdapt);
            // Open Connection
            sqlConn.Open();

            // New DataSet
            DataSet dbSet = new DataSet();
            // Populate DataSet with data
            dbAdapt.Fill(dbSet, "UploadedFiles");
            // Get reference to our table
            DataTable dbTable = dbSet.Tables["UploadedFiles"];
            // Create new row
            DataRow dbRow = dbTable.NewRow();

            // Store data in the row
            dbRow["FileName"] = sFilename;
            dbRow["FileType"] = sFileType;
            dbRow["FileSize"] = iFileSize;
            dbRow["UploadDate"] = DateTime.Now.Date.ToShortDateString();
            dbRow["Student_UserId"] = new Guid(sStudentId);
            dbRow["PublicFile"] = bPublicFile;
            dbRow["ReqId"] = sReqId;

            // Add row to table
            dbTable.Rows.Add(dbRow);

            // Update data source
            dbAdapt.Update(dbSet, "UploadedFiles");

            // Get new UploadId
            if (!dbRow.IsNull("UploadId"))
                iUploadId = (int)dbRow["UploadId"];
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            // Close connection
            sqlConn.Close();
        }

        // Return UploadId
        return iUploadId;
    }

    /// <summary>
    /// Create New File Record when FileId is needed
    /// </summary>
    /// <returns></returns>
    public static int CreateNewChatMessage(string sMessage, string sSentBy)
    {
        int iMessageId = 0;
        string sSqlConnString = ConfigurationManager.ConnectionStrings["connection"].ToString();

        SqlConnection sqlConn = null;

        try
        {
            sqlConn = new SqlConnection(sSqlConnString);
            // Create Adapter
            SqlDataAdapter dbAdapt = new SqlDataAdapter("SELECT * FROM ChatMessages", sqlConn);

            // We need this to get an ID back from the database
            dbAdapt.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            // Create and initialize CommandBuilder
            SqlCommandBuilder dbCB = new SqlCommandBuilder(dbAdapt);
            // Open Connection
            sqlConn.Open();

            // New DataSet
            DataSet dbSet = new DataSet();
            // Populate DataSet with data
            dbAdapt.Fill(dbSet, "ChatMessages");
            // Get reference to our table
            DataTable dbTable = dbSet.Tables["ChatMessages"];
            // Create new row
            DataRow dbRow = dbTable.NewRow();
            // Store data in the row
            dbRow["Message"] = sMessage;
            dbRow["SentBy"] = sSentBy;
            dbRow["MessageDate"] = DateTime.Now.Date.ToShortDateString();
            // Add row to table
            dbTable.Rows.Add(dbRow);

            // Update data source
            dbAdapt.Update(dbSet, "ChatMessages");

            // Get new MessageId
            if (!dbRow.IsNull("MessageId"))
                iMessageId = (int)dbRow["MessageId"];
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            // Close connection
            sqlConn.Close();
        }

        // Return MessageId
        return iMessageId;
    }

    /// <summary>
    /// Get all events for a given month of a given year
    /// </summary>
    /// <param name="iMonth"></param>
    /// <param name="iYear"></param>
    /// <returns></returns>
    public static Hashtable GetMonthEvents(int iMonth, int iYear)
    {
        Hashtable htRetVal = null;
        try
        {
            int iStartDate = 0, iEndDate = 0;
            htRetVal = new Hashtable();

            string sSql = "SELECT * FROM Events WHERE EventYear = @EventYear AND EventMonth = @EventMonth ORDER BY StartDate";
            SqlParameter paramEventYear = new SqlParameter("@EventYear", SqlDbType.Int);
            paramEventYear.Value = iYear;
            SqlParameter paramEventMonth = new SqlParameter("@EventMonth", SqlDbType.Int);
            paramEventMonth.Value = iMonth;

            SqlDataReader dtTemp = GetDataReaderWithParams(sSql, paramEventYear, paramEventMonth);
            while (dtTemp.Read())
            {
                string EventName = dtTemp["eventname"].ToString();
                string LastDay = string.Empty, LastMonth = string.Empty;

                iStartDate = DateTime.Parse(dtTemp["StartDate"].ToString()).Day;
                iEndDate = DateTime.Parse(dtTemp["EndDate"].ToString()).Day;
                LastMonth = DateTime.Parse(dtTemp["EndDate"].ToString()).Month.ToString();

                // Loop through the recordset to fill the hashtable
                for (int iDay = iStartDate; iDay <= iEndDate; iDay++)
                {
                    LastDay = iDay.ToString();

                    if (htRetVal.Contains(LastMonth + LastDay)) // Entry already exist in hastable for this day
                    {
                        // Add more info to the entry
                        EventName = htRetVal[LastMonth + LastDay].ToString() + "\n" + dtTemp["eventname"].ToString();
                        htRetVal.Remove(LastMonth + LastDay);           // delete previous entry
                        htRetVal.Add(LastMonth + LastDay, EventName);   // add entry back
                    }
                    else
                    {
                        // First time writing entry for this day
                        EventName = dtTemp["eventname"].ToString();
                        htRetVal.Add(LastMonth + LastDay, EventName);
                    }
                    //LastDay = dtTemp["day"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            htRetVal = null;
        }

        return htRetVal;
    }

}

//=============================== File Helper   =========================================
/*
 */

#region FileHelper
public class FileHelper
{
    /// <summary>
    /// Writes file to current folder
    /// </summary>
    /// <param name="strPath"></param>
    /// <param name="Buffer"></param>
    public static bool WriteToFile(string strPath, ref byte[] Buffer)
    {
        bool bRetVal = false;

        try
        {
            // Create a file
            FileStream newFile = new FileStream(strPath, FileMode.Create);

            // Write data to the file
            newFile.Write(Buffer, 0, Buffer.Length);

            // Close file
            newFile.Close();
            bRetVal = true;
        }
        catch (Exception ex)
        {
            bRetVal = false;
            Debug.WriteLine(ex.Message);
        }

        return bRetVal;
    }

    /// <summary>
    /// Returns the type of specified file.
    /// </summary>
    /// <param name="node"></param>
    public static void GetFileTypeAndIcon(string sExtension, out string sOutFileType, out string OutIconExe)
    {
        sOutFileType = String.Empty;
        OutIconExe = String.Empty;
        if (!sExtension.StartsWith("."))
            sExtension = "." + sExtension;
        try
        {
            // Get File Type
            RegistryKey rk = Registry.ClassesRoot.OpenSubKey(sExtension);
            string sTemp = rk.GetValue("").ToString();  // Get Default value
            rk.Close();
            rk = Registry.ClassesRoot.OpenSubKey(sTemp);
            sOutFileType = rk.GetValue("").ToString();
            rk.Close();
            // Get File Icon exe application
            rk = Registry.ClassesRoot.OpenSubKey(sTemp + @"\DefaultIcon");
            sTemp = rk.GetValue("").ToString(); // Get Default value
            OutIconExe = sTemp.Split(',')[0];
            rk.Close();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}
#endregion

//=============================== User Role Provider =========================================
/*

This provider works with the following schema for the tables of role data.

CREATE TABLE Roles
(
  Rolename Text (255) NOT NULL,
  ApplicationName Text (255) NOT NULL,
    CONSTRAINT PKRoles PRIMARY KEY (Rolename, ApplicationName)
)

CREATE TABLE UsersInRoles
(
  Username Text (255) NOT NULL,
  Rolename Text (255) NOT NULL,
  ApplicationName Text (255) NOT NULL,
    CONSTRAINT PKUsersInRoles PRIMARY KEY (Username, Rolename, ApplicationName)
)

*/

public sealed class HGRoleProvider : RoleProvider
{

    //
    // Global connection string, generic exception message, event log info.
    //

    //private string rolesTable = "Roles";
    //private string usersInRolesTable = "UsersInRoles";

    //private string eventSource = "HGRoleProvider";
    //private string eventLog = "Application";
    //private string exceptionMessage = "An exception occurred. Please check the Event Log.";

    //private ConnectionStringSettings pConnectionStringSettings;
    //private string connectionString;


    //
    // If false, exceptions are thrown to the caller. If true,
    // exceptions are written to the event log.
    //

    //private bool pWriteExceptionsToEventLog = false;

    //public bool WriteExceptionsToEventLog
    //{
    //    get { return pWriteExceptionsToEventLog; }
    //    set { pWriteExceptionsToEventLog = value; }
    //}



    //
    // System.Configuration.Provider.ProviderBase.Initialize Method
    //

    public override void Initialize(string name, NameValueCollection config)
    {
        //
        // Initialize values from web.config.
        //

        if (config == null)
            throw new ArgumentNullException("config");

        if (name == null || name.Length == 0)
            name = "HGRoleProvider";

        if (String.IsNullOrEmpty(config["description"]))
        {
            config.Remove("description");
            config.Add("description", "HomeGroup Role provider");
        }

        // Initialize the abstract base class.
        base.Initialize(name, config);

        if (config["applicationName"] == null || config["applicationName"].Trim() == "")
        {
            pApplicationName = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath;
        }
        else
        {
            pApplicationName = config["applicationName"];
        }


        //if (config["writeExceptionsToEventLog"] != null)
        //{
        //    if (config["writeExceptionsToEventLog"].ToUpper() == "TRUE")
        //    {
        //        pWriteExceptionsToEventLog = true;
        //    }
        //}


        //    //
        //    // Initialize OdbcConnection.
        //    //

        //    pConnectionStringSettings = ConfigurationManager.
        //      ConnectionStrings[config["connectionStringName"]];

        //    if (pConnectionStringSettings == null || pConnectionStringSettings.ConnectionString.Trim() == "")
        //    {
        //        throw new ProviderException("Connection string cannot be blank.");
        //    }

        //    connectionString = pConnectionStringSettings.ConnectionString;
    }

    //
    // System.Web.Security.RoleProvider properties.
    //


    private string pApplicationName;


    public override string ApplicationName
    {
        get { return pApplicationName; }
        set { pApplicationName = value; }
    }

    //
    // System.Web.Security.RoleProvider methods.
    //

    //
    // RoleProvider.AddUsersToRoles
    //

    public override void AddUsersToRoles(string[] usernames, string[] rolenames)
    {
        //foreach (string rolename in rolenames)
        //{
        //    if (!RoleExists(rolename))
        //    {
        //        throw new ProviderException("Role name not found.");
        //    }
        //}

        //foreach (string username in usernames)
        //{
        //    if (username.IndexOf(',') > 0)
        //    {
        //        throw new ArgumentException("User names cannot contain commas.");
        //    }

        //    foreach (string rolename in rolenames)
        //    {
        //        if (IsUserInRole(username, rolename))
        //        {
        //            throw new ProviderException("User is already in role.");
        //        }
        //    }
        //}


        //OdbcCommand cmd = new OdbcCommand("INSERT INTO [" + usersInRolesTable + "]" +
        //        " (Username, Rolename, ApplicationName) " +
        //        " Values(?, ?, ?)", conn);

        //SqlParameter userParm = cmd.Parameters.Add("@Username",  OdbcType.VarChar, 255);
        //SqlParameter roleParm = cmd.Parameters.Add("@Rolename", OdbcType.VarChar, 255);
        //cmd.Parameters.Add("@ApplicationName", OdbcType.VarChar, 255).Value = ApplicationName;

        //string sSqlConnString = ConfigurationManager.ConnectionStrings["connection"].ToString();
        //sqlConn = new System.Data.SqlClient.SqlConnection(sSqlConnString);

        //try
        //{
        //    sqlConn.Open();
        //    tran = sqlConn.BeginTransaction();
        //    cmd.Transaction = tran;

        //    foreach (string username in usernames)
        //    {
        //        foreach (string rolename in rolenames)
        //        {
        //            userParm.Value = username;
        //            roleParm.Value = rolename;
        //            cmd.ExecuteNonQuery();
        //        }
        //    }

        //    tran.Commit();
        //}
        //catch (OdbcException e)
        //{
        //    try
        //    {
        //        tran.Rollback();
        //    }
        //    catch { }


        //    if (WriteExceptionsToEventLog)
        //    {
        //        WriteToEventLog(e, "AddUsersToRoles");
        //    }
        //    else
        //    {
        //        throw e;
        //    }
        //}
        //finally
        //{
        //    sqlConn.Close();
        //}
    }


    //
    // RoleProvider.CreateRole
    //

    public override void CreateRole(string rolename)
    {
        //if (rolename.IndexOf(',') > 0)
        //{
        //    throw new ArgumentException("Role names cannot contain commas.");
        //}

        //if (RoleExists(rolename))
        //{
        //    throw new ProviderException("Role name already exists.");
        //}

        //OdbcConnection sqlConn = new OdbcConnection(sqlConnectionString);
        //OdbcCommand cmd = new OdbcCommand("INSERT INTO [" + rolesTable + "]" +
        //        " (Rolename, ApplicationName) " +
        //        " Values(?, ?)", sqlConn);

        //cmd.Parameters.Add("@Rolename", OdbcType.VarChar, 255).Value = rolename;
        //cmd.Parameters.Add("@ApplicationName", OdbcType.VarChar, 255).Value = ApplicationName;

        //try
        //{
        //    sqlConn.Open();

        //    cmd.ExecuteNonQuery();
        //}
        //catch (OdbcException e)
        //{
        //    if (WriteExceptionsToEventLog)
        //    {
        //        WriteToEventLog(e, "CreateRole");
        //    }
        //    else
        //    {
        //        throw e;
        //    }
        //}
        //finally
        //{
        //    sqlConn.Close();
        //}
    }


    //
    // RoleProvider.DeleteRole
    //

    public override bool DeleteRole(string rolename, bool throwOnPopulatedRole)
    {
        //if (!RoleExists(rolename))
        //{
        //    throw new ProviderException("Role does not exist.");
        //}

        //if (throwOnPopulatedRole && GetUsersInRole(rolename).Length > 0)
        //{
        //    throw new ProviderException("Cannot delete a populated role.");
        //}

        //OdbcConnection sqlConn = new OdbcConnection(sqlConnectionString);
        //OdbcCommand cmd = new OdbcCommand("DELETE FROM [" + rolesTable + "]" +
        //        " WHERE Rolename = ? AND ApplicationName = ?", sqlConn);

        //cmd.Parameters.Add("@Rolename", OdbcType.VarChar, 255).Value = rolename;
        //cmd.Parameters.Add("@ApplicationName", OdbcType.VarChar, 255).Value = ApplicationName;


        //OdbcCommand cmd2 = new OdbcCommand("DELETE FROM [" + usersInRolesTable + "]" +
        //        " WHERE Rolename = ? AND ApplicationName = ?", sqlConn);

        //cmd2.Parameters.Add("@Rolename", OdbcType.VarChar, 255).Value = rolename;
        //cmd2.Parameters.Add("@ApplicationName", OdbcType.VarChar, 255).Value = ApplicationName;

        //OdbcTransaction tran = null;

        //try
        //{
        //    sqlConn.Open();
        //    tran = sqlConn.BeginTransaction();
        //    cmd.Transaction = tran;
        //    cmd2.Transaction = tran;

        //    cmd2.ExecuteNonQuery();
        //    cmd.ExecuteNonQuery();

        //    tran.Commit();
        //}
        //catch (OdbcException e)
        //{
        //    try
        //    {
        //        tran.Rollback();
        //    }
        //    catch { }


        //    if (WriteExceptionsToEventLog)
        //    {
        //        WriteToEventLog(e, "DeleteRole");

        //        return false;
        //    }
        //    else
        //    {
        //        throw e;
        //    }
        //}
        //finally
        //{
        //    sqlConn.Close();
        //}

        return true;
    }


    //
    // RoleProvider.GetAllRoles
    //

    public override string[] GetAllRoles()
    {
        string tmpRoleNames = "";

        string sSql = "SELECT RoleName FROM dbo.Roles";

        SqlDataReader dt = DB_Access.GetDataReader(sSql);
        if (dt != null)
        {
            while (dt.Read())
            {
                tmpRoleNames += dt["RoleName"].ToString() + ",";
            }
            if (tmpRoleNames.Length > 0)
            {
                // Remove trailing comma.
                tmpRoleNames = tmpRoleNames.Substring(0, tmpRoleNames.Length - 1);
                return tmpRoleNames.Split(',');
            }
        }

        return new string[0];
    }


    //
    // RoleProvider.GetRolesForUser
    //

    public override string[] GetRolesForUser(string username)
    {
        string tmpRoleNames = "";

        string sSql = "SELECT r.RoleName FROM dbo.Users u INNER JOIN dbo.Roles r ON u.RoleId = r.RoleId WHERE Email = @Email ";

        SqlParameter paramEmail = new SqlParameter("@Email", SqlDbType.NVarChar);
        paramEmail.Value = username;

        SqlDataReader dt = DB_Access.GetDataReaderWithParams(sSql, paramEmail);
        if (dt != null)
        {
            while (dt.Read())
            {
                tmpRoleNames += dt.GetString(0) + ",";
            }
        }


        if (tmpRoleNames.Length > 0)
        {
            // Remove trailing comma.
            tmpRoleNames = tmpRoleNames.Substring(0, tmpRoleNames.Length - 1);
            return tmpRoleNames.Split(',');
        }

        return new string[0];
    }


    //
    // RoleProvider.GetUsersInRole
    //

    public override string[] GetUsersInRole(string rolename)
    {
        //string tmpUserNames = "";

        //OdbcConnection sqlConn = new OdbcConnection(sqlConnectionString);
        //OdbcCommand cmd = new OdbcCommand("SELECT Username FROM [" + usersInRolesTable + "]" +
        //          " WHERE Rolename = ? AND ApplicationName = ?", sqlConn);

        //cmd.Parameters.Add("@Rolename", OdbcType.VarChar, 255).Value = rolename;
        //cmd.Parameters.Add("@ApplicationName", OdbcType.VarChar, 255).Value = ApplicationName;

        //OdbcDataReader reader = null;

        //try
        //{
        //    sqlConn.Open();

        //    reader = cmd.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        tmpUserNames += reader.GetString(0) + ",";
        //    }
        //}
        //catch (OdbcException e)
        //{
        //    if (WriteExceptionsToEventLog)
        //    {
        //        WriteToEventLog(e, "GetUsersInRole");
        //    }
        //    else
        //    {
        //        throw e;
        //    }
        //}
        //finally
        //{
        //    if (reader != null) { reader.Close(); }
        //    sqlConn.Close();
        //}

        //if (tmpUserNames.Length > 0)
        //{
        //    // Remove trailing comma.
        //    tmpUserNames = tmpUserNames.Substring(0, tmpUserNames.Length - 1);
        //    return tmpUserNames.Split(',');
        //}

        return new string[0];
    }


    //
    // RoleProvider.IsUserInRole
    //

    public override bool IsUserInRole(string username, string rolename)
    {
        bool userIsInRole = false;

        SqlParameter paramEmail = new SqlParameter("@Email", SqlDbType.NVarChar);
        paramEmail.Value = username;
        SqlParameter paramRolename = new SqlParameter("@Rolename", SqlDbType.NVarChar);
        paramRolename.Value = rolename;

        string sSql = "SELECT u.UserId, u.MobilePIN, u.Email, r.RoleName FROM dbo.Users u " +
            " LEFT OUTER JOIN dbo.Roles r ON u.RoleId = r.RoleId " +
            " WHERE u.Email = @Email AND u.RoleName = @RoleName ";

        SqlDataReader dt = DB_Access.GetDataReaderWithParams(sSql, paramEmail);
        if (dt != null)
        {
            if (dt.Read())
            {
                if (dt["Email"].ToString().Trim().ToLower().CompareTo(username.ToLower()) == 0)
                {
                    userIsInRole = true;
                }
            }
        }

        return userIsInRole;
    }


    //
    // RoleProvider.RemoveUsersFromRoles
    //

    public override void RemoveUsersFromRoles(string[] usernames, string[] rolenames)
    {
        foreach (string rolename in rolenames)
        {
            if (!RoleExists(rolename))
            {
                throw new ProviderException("Role name not found.");
            }
        }

        foreach (string username in usernames)
        {
            foreach (string rolename in rolenames)
            {
                if (!IsUserInRole(username, rolename))
                {
                    throw new ProviderException("User is not in role.");
                }
            }
        }


        //OdbcConnection sqlConn = new OdbcConnection(sqlConnectionString);
        //OdbcCommand cmd = new OdbcCommand("DELETE FROM Roles" +
        //        " WHERE Username = ? AND Rolename = ?", sqlConn);

        //OdbcParameter userParm = cmd.Parameters.Add("@Username", OdbcType.VarChar, 255);
        //OdbcParameter roleParm = cmd.Parameters.Add("@Rolename", OdbcType.VarChar, 255);
        //cmd.Parameters.Add("@ApplicationName", OdbcType.VarChar, 255).Value = ApplicationName;

        //OdbcTransaction tran = null;

        //try
        //{
        //    sqlConn.Open();
        //    tran = sqlConn.BeginTransaction();
        //    cmd.Transaction = tran;

        //    foreach (string username in usernames)
        //    {
        //        foreach (string rolename in rolenames)
        //        {
        //            userParm.Value = username;
        //            roleParm.Value = rolename;
        //            cmd.ExecuteNonQuery();
        //        }
        //    }

        //    tran.Commit();
        //}
        //catch (OdbcException e)
        //{
        //    try
        //    {
        //        tran.Rollback();
        //    }
        //    catch { }


        //    if (WriteExceptionsToEventLog)
        //    {
        //        WriteToEventLog(e, "RemoveUsersFromRoles");
        //    }
        //    else
        //    {
        //        throw e;
        //    }
        //}
        //finally
        //{
        //    sqlConn.Close();
        //}
    }


    //
    // RoleProvider.RoleExists
    //

    public override bool RoleExists(string rolename)
    {
        bool exists = false;

        SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString());
        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Roles" +
                  " WHERE Rolename = ? ", sqlConn);

        cmd.Parameters.Add("@Rolename", SqlDbType.VarChar, 255).Value = rolename;

        try
        {
            sqlConn.Open();

            int numRecs = (int)cmd.ExecuteScalar();

            if (numRecs > 0)
            {
                exists = true;
            }
        }
        catch (SqlException e)
        {
            //if (WriteExceptionsToEventLog)
            //{
            WriteToEventLog(e, "RoleExists");
            //}
            //else
            //{
            //    throw e;
            //}
        }
        finally
        {
            sqlConn.Close();
        }

        return exists;
    }

    //
    // RoleProvider.FindUsersInRole
    //

    public override string[] FindUsersInRole(string rolename, string usernameToMatch)
    {
        //OdbcConnection sqlConn = new OdbcConnection(sqlConnectionString);
        //OdbcCommand cmd = new OdbcCommand("SELECT Username FROM [" + usersInRolesTable + "] " +
        //          "WHERE Username LIKE ? AND RoleName = ? AND ApplicationName = ?", sqlConn);
        //cmd.Parameters.Add("@UsernameSearch", OdbcType.VarChar, 255).Value = usernameToMatch;
        //cmd.Parameters.Add("@RoleName", OdbcType.VarChar, 255).Value = rolename;
        //cmd.Parameters.Add("@ApplicationName", OdbcType.VarChar, 255).Value = pApplicationName;

        //string tmpUserNames = "";
        //OdbcDataReader reader = null;

        //try
        //{
        //    sqlConn.Open();

        //    reader = cmd.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        tmpUserNames += reader.GetString(0) + ",";
        //    }
        //}
        //catch (OdbcException e)
        //{
        //    if (WriteExceptionsToEventLog)
        //    {
        //        WriteToEventLog(e, "FindUsersInRole");
        //    }
        //    else
        //    {
        //        throw e;
        //    }
        //}
        //finally
        //{
        //    if (reader != null) { reader.Close(); }

        //    sqlConn.Close();
        //}

        //if (tmpUserNames.Length > 0)
        //{
        //    // Remove trailing comma.
        //    tmpUserNames = tmpUserNames.Substring(0, tmpUserNames.Length - 1);
        //    return tmpUserNames.Split(',');
        //}

        return new string[0];
    }

    //
    // WriteToEventLog
    //   A helper function that writes exception detail to the event log. Exceptions
    // are written to the event log as a security measure to avoid private database
    // details from being returned to the browser. If a method does not return a status
    // or boolean indicating the action succeeded or failed, a generic exception is also 
    // thrown by the caller.
    //

    private void WriteToEventLog(SqlException e, string action)
    {
        //EventLog log = new EventLog();
        //log.Source = eventSource;
        //log.Log = eventLog;

        //string message = exceptionMessage + "\n\n";
        //message += "Action: " + action + "\n\n";
        //message += "Exception: " + e.ToString();

        //log.WriteEntry(message);
    }

}