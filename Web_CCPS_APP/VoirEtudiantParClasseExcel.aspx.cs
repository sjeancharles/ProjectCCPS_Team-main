using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Web.Security;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;
using System.Runtime.InteropServices;
//using ClosedXML.Excel;
using ClosedXML.Excel;
using ClosedXML.Utils;
using Label = System.Web.UI.WebControls.Label;

namespace Web_CCPS_APP
{
    public partial class VoirEtudiantParClasseExcel : System.Web.UI.Page
    {
        public const int LOGON32_LOGON_INTERACTIVE = 2;
        public const int LOGON32_PROVIDER_DEFAULT = 0;

        WindowsImpersonationContext impersonationContext;

        [DllImport("advapi32.dll")]
        public static extern int LogonUserA(String lpszUserName,
         String lpszDomain,
        String lpszPassword,
        int dwLogonType,
        int dwLogonProvider,
        ref IntPtr phToken);
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int DuplicateToken(IntPtr hToken,
        int impersonationLevel,
        ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);


        protected void Page_Load(object sender, EventArgs e)
        {
            Label lblControl = (Label)Master.FindControl("lblPageName");
            if (lblControl != null)
            {
                lblControl.Text = "";
            }
        }

        private bool impersonateValidUser(String userName, String domain, String password)
        {
            WindowsIdentity tempWindowsIdentity;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            if (RevertToSelf())
            {
                if (LogonUserA(userName, domain, password, LOGON32_LOGON_INTERACTIVE,
                    LOGON32_PROVIDER_DEFAULT, ref token) != 0)
                {
                    if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                    {
                        tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                        impersonationContext = tempWindowsIdentity.Impersonate();
                        if (impersonationContext != null)
                        {
                            CloseHandle(token);
                            CloseHandle(tokenDuplicate);
                            return true;
                        }
                    }
                }
            }
            if (token != IntPtr.Zero)
                CloseHandle(token);
            if (tokenDuplicate != IntPtr.Zero)
                CloseHandle(tokenDuplicate);
            return false;
        }

        private void undoImpersonation()
        {
            impersonationContext.Undo();
        }
        void CreateSpreadSheetProcess()
        {
            string sSql = "";
            string sSessionStartDate = DB_Access.GetStartDateOfCurrentSession().ToString("dd-MMM-yyyy");
            string sSessionEndDate = DB_Access.GetEndDateOfCurrentSession().ToString("dd-MMM-yyyy");

            sSql = "SELECT EC.PersonneID, EC.SessionID, EC.EtudiantsCourantsID, P.Nom, P.Prenom, P.Telephone1, P.UserNameAttribue, S.ClasseID, S.JourRencontre, S.Heures, NC.NomClasse, NC.NiveauClasse " +
                    " FROM  EtudiantsCourants EC, Personnes P, Sessions AS S, Classes AS NC " +
                    " WHERE EC.PersonneID = P.PersonneID AND EC.SessionID = S.SessionID AND S.ClasseID = NC.ClasseID  " +
                    " AND (EC.SessionID IN (SELECT SessionID FROM Sessions WHERE (actif = 1))) AND (S.actif = 1) ORDER BY NC.ClasseID, JourRencontre, Heures, Nom, Prenom";

            int iError = 0;
            int nombreEtudiants = 0;
            try
            {
                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("1");   // need to create a worksheet to start with before knowing the name of the class. We'll delete at the end.
                bool firstWorsheet = true;
                int sheetCount = 0;

                //String numeroCoursOld = "", numeroCours = "", numeroCoursEtHoraire = "";
                String horaire = "", sSessionID = "", sSessionIDOld = "";
                try
                {
                    int iRowIndex = 2;
                    SqlDataReader dt = DB_Access.GetDataReader(sSql);
                    if (dt != null)
                    {
                        dt.Read();
                        do
                        {
                            //numeroCours = dt["NomCours"].ToString();
                            horaire = dt["JourRencontre"].ToString() + "  " + dt["Heures"].ToString();
                            //numeroCoursEtHoraire = numeroCours + horaire;
                            //if (numeroCoursOld != numeroCoursEtHoraire)
                            sSessionID = dt["SessionID"].ToString();
                            if (sSessionIDOld != sSessionID)
                            {
                                if (!firstWorsheet)
                                {
                                    // Format worsheet before creating the new one
                                    nombreEtudiants += 5;  // add # of header lines
                                    for (int i = 1; i <= nombreEtudiants; i++)
                                    {
                                        for (int j = 1; j <= 18; j++)
                                        {
                                            worksheet.Cell(i, j).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                        }
                                    }
                                    //Addition de bordure extérieure
                                    worksheet.Range("A1", "R" + nombreEtudiants.ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                                    nombreEtudiants = 0;
                                }

                                firstWorsheet = false;

                                // Reset counter
                                iRowIndex = 5;
                                //numeroCoursOld = numeroCoursEtHoraire;
                                sSessionIDOld = sSessionID;

                                sheetCount++;
                                worksheet = workbook.Worksheets.Add(sheetCount.ToString() + "-" + dt["NomClasse"].ToString());

                                // Creer Header
                                worksheet.Cell(1, 1).Value = "CALVARY CHRISTIAN PROFESSIONAL SCHOOL - CCPS";
                                worksheet.Cell(2, 1).Value = dt["NomClasse"].ToString();
                                worksheet.Cell(3, 1).Value = horaire;
                                worksheet.Cell(4, 1).Value = "Session : " + sSessionStartDate + " - " + sSessionEndDate;

                                // Names of columns
                                worksheet.Cell(5, 1).Value = "Nom";
                                worksheet.Cell(5, 2).Value = "Prénom";
                                worksheet.Cell(5, 3).Value = "Téléphone";
                                worksheet.Cell(5, 4).Value = "UserName";

                                // Cells merge horizontally
                                worksheet.Range("A1:R1").Merge();
                                worksheet.Range("A1:R1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                                worksheet.Range("A2:R2").Merge();
                                worksheet.Range("A2:R2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                                worksheet.Range("A3:R3").Merge();
                                worksheet.Range("A3:R3").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                                worksheet.Range("A4:R4").Merge();
                                worksheet.Range("A4:R4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                                //ws.Style.Fill.BackgroundColor = XLColor.LightCyan;

                                // Column font weight
                                worksheet.Range("A1", "E1").Style.Font.Bold = true;
                                worksheet.Range("A2", "E2").Style.Font.Bold = true;
                                worksheet.Range("A3", "E3").Style.Font.Bold = true;
                                worksheet.Range("A4", "E4").Style.Font.Bold = true;
                                worksheet.Range("A5", "E5").Style.Font.Bold = true;

                                // Column Width
                                worksheet.Columns("A").Width = 10;
                                worksheet.Columns("B").Width = 12;
                                worksheet.Columns("C").Width = 10;
                                worksheet.Columns("D").Width = 12;

                                worksheet.Columns("E").Width = 5;
                                worksheet.Columns("F").Width = 5;
                                worksheet.Columns("G").Width = 5;
                                worksheet.Columns("H").Width = 5;
                                worksheet.Columns("I").Width = 5;
                                worksheet.Columns("J").Width = 5;
                                worksheet.Columns("K").Width = 5;
                                worksheet.Columns("L").Width = 5;
                                worksheet.Columns("M").Width = 5;
                                worksheet.Columns("N").Width = 5;
                                worksheet.Columns("O").Width = 5;
                                worksheet.Columns("P").Width = 5;
                                worksheet.Columns("Q").Width = 5;
                                worksheet.Columns("R").Width = 5;

                                // Set the height of all rows in the worksheet
                                //worksheet.Rows().Height = 20;

                                // Orientation
                                worksheet.PageSetup.PageOrientation = XLPageOrientation.Landscape;
                                worksheet.PageSetup.Margins.Top = 0.5D;
                                worksheet.PageSetup.Margins.Bottom = 0.5D;
                                worksheet.PageSetup.Margins.Right = 0.5D;
                                worksheet.PageSetup.Margins.Left = 0.5D;
                            }

                            // Creer entrees
                            iRowIndex++;
                            worksheet.Cell(iRowIndex, 1).Value = dt["Nom"].ToString().ToUpper();
                            worksheet.Cell(iRowIndex, 2).Value = dt["Prenom"].ToString();
                            worksheet.Cell(iRowIndex, 3).Value = dt["Telephone1"].ToString();
                            worksheet.Cell(iRowIndex, 4).Value = dt["UserNameAttribue"].ToString();
                            nombreEtudiants++;

                        } while (dt.Read());

                        // format last worsheet
                        nombreEtudiants += 5;  // add # of header lines
                        for (int i = 1; i <= nombreEtudiants; i++)
                        {
                            for (int j = 1; j < 18; j++)
                            {
                                worksheet.Cell(i, j).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            }
                        }
                        //Addition de bordure extérieure
                        worksheet.Range("A1", "R" + nombreEtudiants.ToString()).Style.Border.OutsideBorder = XLBorderStyleValues.Thick;

                        workbook.Worksheets.Delete("1");    // no need anymore

                        String fileName = "c:\\extras\\CCPS_" + DateTime.Today.Day.ToString() + "-" + DateTime.Today.Month.ToString() + "-" +
                            DateTime.Today.Year.ToString() + "-" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() +
                            DateTime.Now.Second.ToString() + ".xlsx";
                        workbook.SaveAs(fileName);
                        lblError.Text = "SpreaSheet Créé: " + fileName;
                        lblError.ForeColor = System.Drawing.Color.Green;
                        lblError.Font.Bold = true;
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    lblError.Text = error;
                }
            }
            catch (Exception theException)
            {
                String errorMessage;
                errorMessage = "Error (" + iError.ToString() + "): ";
                errorMessage = String.Concat(errorMessage, theException.Message);
                lblError.Text = errorMessage;
                lblError.Font.Bold = true;
            }
        }

        protected void btnVoirExcelPage_Click(object sender, EventArgs e)
        {
            //CreateSpreadSheetProcess();

            //HttpRuntime.AppDomainAppPath
            //#if DEBUG
            //String sUserName = "spoteau", sDomain = "spoteau-pc", sPass = "";
            // String sUserName = "administrator", sDomain = "ccpap-dev", sPass = "";
            //#else
            String sUserName = "50947", sDomain = "DESKTOP-GBQB9HI", sPass = "2892";
            //#endif
            //CreateSpreadSheetProcess();
            if (impersonateValidUser(sUserName, sDomain, sPass))
            {
                //Insert your code that runs under the security context of a specific user here.
                CreateSpreadSheetProcess();
                undoImpersonation();
            }
            else
            {
                //Your impersonation failed. Therefore, include a fail-safe mechanism here.
                lblError.Text = "ECHEC: Impersonisation!";
            }
        }
    }
}