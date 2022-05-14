using CCPS_Web_Edu_Update;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_CCPS_APP
{
    public partial class EditSession : System.Web.UI.Page
    {
        private BaseDeDonnees donnees = new BaseDeDonnees();
        protected void Page_Load(object sender, EventArgs e)
        {

#if !DEBUG
        if (!DB_Access.IsCurrentUserInGroup("Administrators"))
        {
            Response.Redirect("../NoAccess.htm");
        }
#endif


            if (!IsPostBack)
            {
                Label lblControl = (Label)Master.FindControl("lblPageName");
                if (lblControl != null)
                {
                    lblControl.Text = "Modifier Classes Existantes!!!";
                }

                RemplirClasses();
                RemplirProfesseur();
                RemplirJourRencontre();
                RemplirHeures();
                // RemplirSessions() Doit être exécuté en dernier
                RemplirSessions();
            }
        }

        void RemplirSessions()
        {
            try
            {
               // SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
                string sSql = "SELECT -1 as SessionID, '' as Heures, '' as JourRencontre, '' as DateCommence,  '' as DateFin, 0 as MaxEtudiants, 0 as MontantParticipation, 'Cliquez pour Choisir' as Description UNION " +
                    " SELECT SessionID, Heures, JourRencontre, DateCommence, DateFin, MaxEtudiants, MontantParticipation, JourRencontre + '-' + Heures  + '(' + CAST(DateCommence as nvarchar(10)) + '  -  ' + CAST(DateFin as nvarchar(10)) + ')' as Description from Sessions " +
                    "WHERE Actif = 1";
                clSession clUneSession = null;
                List<clSession> listSession = new List<clSession>();
               // ArrayList arSessions = new ArrayList();

                SqlDataReader dr = donnees.GetDataReader(sSql);
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        clUneSession = new clSession();
                        clUneSession.SessionID = Int16.Parse(dr["SessionID"].ToString());
                        clUneSession.Heures = dr["Heures"].ToString();
                        clUneSession.JourRencontre = dr["JourRencontre"].ToString();
                        clUneSession.DateCommence = dr["DateCommence"].ToString();
                        clUneSession.DateFin = dr["DateFin"].ToString();
                        clUneSession.MaxEtudiants = Int16.Parse(dr["MaxEtudiants"].ToString());
                        clUneSession.Montant = Double.Parse(dr["MontantParticipation"].ToString());
                        clUneSession.Description = dr["Description"].ToString();
                        listSession.Add(clUneSession);
                        // arSessions.Add(clUneSession);
                    }

                    cbo_A_Modifier.DataSource = listSession;        //listSession;
                    cbo_A_Modifier.DataValueField = "SessionID";
                    cbo_A_Modifier.DataTextField = "Description";
                    cbo_A_Modifier.DataBind();
                }
            }
            catch (Exception ex)
            {
                string sError = ex.Message.ToString();
                Debug.WriteLine(sError);
                sError = "";
            }
        }

        void RemplirClasses()
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
                string sSql = "SELECT -1 as ClasseID, 'Cliquez pour Choisir' as NomClasse UNION SELECT ClasseID, NomClasse from Classes";

                SqlDataAdapter da = new SqlDataAdapter(sSql, myConnection);
                DataTable dTable = new DataTable();
                da.Fill(dTable);

                NomClasse.DataSource = dTable;
                NomClasse.DataValueField = "ClasseID";
                NomClasse.DataTextField = "NomClasse";

                NomClasse.DataBind(); ;
            }
            catch (Exception ex)
            {
                string sError = ex.Message.ToString();
                Debug.WriteLine(sError);
                sError = "";
            }
        }

        void RemplirProfesseur()
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
                string sSql = "SELECT -1 as PersonneID, 'Cliquez pour Choisir' as Nom UNION SELECT PersonneID, Nom + ', ' + Prenom AS Nom from Personnes WHERE Professeur = 1";

                SqlDataAdapter da = new SqlDataAdapter(sSql, myConnection);
                DataTable dTable = new DataTable();
                da.Fill(dTable);

                DrpProfesseurName.DataSource = dTable;
                DrpProfesseurName.DataValueField = "PersonneID";
                DrpProfesseurName.DataTextField = "Nom";

                DrpProfesseurName.DataBind();
            }
            catch (Exception ex)
            {
                string sError = ex.Message.ToString();
                Debug.WriteLine(sError);
                sError = "";
            }
        }

        void RemplirJourRencontre()
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
                string sSql = "SELECT -1 as JourID, 'Cliquez pour Choisir' as JourDescription UNION SELECT JourID, JourDescription from JoursDeClasses";

                SqlDataAdapter da = new SqlDataAdapter(sSql, myConnection);
                DataTable dTable = new DataTable();
                da.Fill(dTable);

                dJourDeClasse.DataSource = dTable;
                dJourDeClasse.DataValueField = "JourID";
                dJourDeClasse.DataTextField = "JourDescription";

                dJourDeClasse.DataBind();
            }
            catch (Exception ex)
            {
                string sError = ex.Message.ToString();
                Debug.WriteLine(sError);
                sError = "";
            }
        }

        protected void WriteErrorMessageToLabel(String Message, Boolean Error)
        {
            lblMessage.Text = Message;
            if (Error)
            {
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        void RemplirHeures()
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
                string sSql = "SELECT -1 as HeureID, 'Cliquez pour Choisir' as HeureDescription UNION SELECT HeureID, HeureDescription from HeuresDeClasses";

                SqlDataAdapter da = new SqlDataAdapter(sSql, myConnection);
                DataTable dTable = new DataTable();
                da.Fill(dTable);

                DropHeureDeClasse.DataSource = dTable;
                DropHeureDeClasse.DataValueField = "HeureID";
                DropHeureDeClasse.DataTextField = "HeureDescription";

                DropHeureDeClasse.DataBind();
            }
            catch (Exception ex)
            {
                string sError = ex.Message.ToString();
                Debug.WriteLine(sError);
                sError = "";
            }
        }

        bool ToutBagayPaAnfom()
        {
            bool bValeur = true;

            if (NomClasse.SelectedIndex == -1)
                bValeur = false;
            if (DrpProfesseurName.SelectedIndex == -1)
                bValeur = false;
            if (dJourDeClasse.SelectedIndex == -1)
                bValeur = false;
            if (DropHeureDeClasse.SelectedIndex == -1)
                bValeur = false;

            if (lblDateDebut.Text == string.Empty)
                bValeur = false;
            //if (calDebut.getGregorianDateText == string.Empty)
            //    bValeur = false;
            if (lblDateFin.Text == string.Empty)
                bValeur = false;

            if (txtMaxEtudiant.Text == string.Empty)
                bValeur = false;
            if (txtMontant.Text == string.Empty)
                bValeur = false;
            return bValeur;
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            ResetToutBagay();
        }

        protected void BtnAddClasse_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";

            try
            {
                donnees = new BaseDeDonnees();
                if (!ToutBagayPaAnfom())
                {
                    WriteErrorMessageToLabel("Toutes les Informations sont obligatoires Pour Continuer !!!",false);
                }
                else
                {
                    SqlParameter nomClassParam = new SqlParameter("@ClasseID", DbType.Int32.ToString());
                    nomClassParam.Value = NomClasse.SelectedItem.Value;

                    SqlParameter ProfParam = new SqlParameter("@ProfesseurID", DbType.Int32.ToString());
                    ProfParam.Value = DrpProfesseurName.SelectedItem.Value;

                    SqlParameter maxEtudianParam = new SqlParameter("@MaxEtudiants", DbType.Int32.ToString());
                    maxEtudianParam.Value = txtMaxEtudiant.Text;

                    SqlParameter JourRenParam = new SqlParameter("@JourRencontre", DbType.Int32.ToString());
                    JourRenParam.Value = dJourDeClasse.SelectedItem.Text;

                    SqlParameter HeureParam = new SqlParameter("@Heures", DbType.Int32.ToString());
                    HeureParam.Value = DropHeureDeClasse.SelectedItem.Text;

                    SqlParameter montantParam = new SqlParameter("@MontantParticipation", DbType.Int32.ToString());
                    montantParam.Value = txtMontant.Text; 
                    
                    SqlParameter creerparUserParam = new SqlParameter("@byUsername", DbType.Int32.ToString());
                    creerparUserParam.Value = BaseDeDonnees.GetWindowsUser();


                    string sSql1 = string.Format("UPDATE Sessions SET ClasseID =@ClasseID, ProfesseurID =@ProfesseurID, MaxEtudiants =@MaxEtudiants, JourRencontre =@JourRencontre, Heures =@Heures, " +
                        "MontantParticipation=@MontantParticipation, byUsername = @byUsername WHERE SessionID={0}", cbo_A_Modifier.SelectedValue.ToString());

                    if (donnees.IssueCommandWithParams(sSql1, nomClassParam, ProfParam, maxEtudianParam, JourRenParam, HeureParam, montantParam, creerparUserParam))
                    {
                        WriteErrorMessageToLabel("Données Modifié avec succes !! ", true);
                        ResetToutBagay();   // Pou ka rantre lòt donnees
                    }
                    else
                    {
                        WriteErrorMessageToLabel("ERROR: Données n'est pas modifié -- Voir Un Technicien!!!", false);
                    }
                }

            }
            catch
            {
                WriteErrorMessageToLabel("ERROR: -- Voir Un Technicien!!! ",false);
            }
        }

        void ResetToutBagay()
        {
            NomClasse.SelectedIndex = -1;
            dJourDeClasse.SelectedIndex = -1;
            DrpProfesseurName.SelectedIndex = -1;
            DropHeureDeClasse.SelectedIndex = -1;
            txtMaxEtudiant.Text = string.Empty;
            txtMontant.Text = string.Empty;
        }
        protected void cbo_A_Modifier_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            try
            {
                //Ajuster les autres listes           
                string sSql = "SELECT * FROM Sessions WHERE SessionID = " + cbo_A_Modifier.SelectedValue.ToString();
                SqlDataReader dr = donnees.GetDataReader(sSql);
                if (dr != null)
                {
                    if (dr.Read())
                    {
                        string sClasseID = dr["ClasseID"].ToString();
                        string sHeures = dr["Heures"].ToString();
                        string sJourRencontre = dr["JourRencontre"].ToString();
                        string sMaxEtudiants = dr["MaxEtudiants"].ToString();
                        string sMontantParticipation = dr["MontantParticipation"].ToString();
                        string sProfesseurID = dr["ProfesseurID"].ToString();
                        string sDateCommence = dr["DateCommence"].ToString();
                        string sDateFin = dr["DateFin"].ToString();
                        // string sDescription = dr["Description"].ToString();

                        // Mise à Jour de textboxes
                        string sOldSelection = "";
                        txtMaxEtudiant.Text = sMaxEtudiants;
                        txtMontant.Text = sMontantParticipation;
                        // Mise à Jour de dropdownboxes
                        if (NomClasse.Items.FindByValue(sClasseID) != null && sClasseID != string.Empty)
                        {
                            sOldSelection = NomClasse.SelectedValue.ToString();
                            NomClasse.Items.FindByValue(sClasseID).Selected = true;
                            if (sOldSelection != sClasseID) // Pour ne pas désélectionner le nouveau choix
                                NomClasse.Items.FindByValue(sOldSelection).Selected = false;
                        }
                        sOldSelection = "";
                        if (DrpProfesseurName.Items.FindByValue(sProfesseurID) != null && sProfesseurID != string.Empty)
                        {
                            sOldSelection = DrpProfesseurName.SelectedValue.ToString();
                            DrpProfesseurName.Items.FindByValue(sProfesseurID).Selected = true;
                            if (sOldSelection != sProfesseurID) // Pour ne pas désélectionner le nouveau choix
                                DrpProfesseurName.Items.FindByValue(sOldSelection).Selected = false;
                        }
                        sOldSelection = "";
                        if (DropHeureDeClasse.Items.FindByText(sHeures) != null && sHeures != string.Empty)
                        {
                            sOldSelection = DropHeureDeClasse.SelectedItem.Text;
                            DropHeureDeClasse.Items.FindByText(sHeures).Selected = true;
                            if (sOldSelection != sHeures) // Pour ne pas désélectionner le nouveau choix
                                DropHeureDeClasse.Items.FindByText(sOldSelection).Selected = false;
                        }
                        sOldSelection = "";
                        if (dJourDeClasse.Items.FindByText(sJourRencontre) != null && sJourRencontre != string.Empty)
                        {
                            sOldSelection = dJourDeClasse.SelectedItem.Text;
                            dJourDeClasse.Items.FindByText(sJourRencontre).Selected = true;
                            if (sOldSelection != sJourRencontre) // Pour ne pas désélectionner le nouveau choix
                                dJourDeClasse.Items.FindByText(sOldSelection).Selected = false;
                        }
                        
                        lblDateDebut.Text += "<strong> &nbsp" + sDateCommence.Substring(0, 9).ToString() +"</strong>";
                        lblDateFin.Text += "<strong> &nbsp" + sDateFin.Substring(0, 10).ToString() + "</strong>";
                    }
                    else
                    {
                        WriteErrorMessageToLabel("ERROR: Données PAS Sauvegardées -- Voir Un Technicien!!!",false);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteErrorMessageToLabel("ERROR: " + ex.Message.ToString(), false);
            }
        }
    }

    public class clSession
    {
        private Int16 sessionid;
        public Int16 SessionID
        {
            get { return sessionid; }
            set { sessionid = value; }
        }
        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private string jourrencontre;
        public string JourRencontre
        {
            get { return jourrencontre; }
            set { jourrencontre = value; }
        }
        private string heures;
        public string Heures
        {
            get { return heures; }
            set { heures = value; }
        }
        private string professeur;
        public string Professeur
        {
            get { return professeur; }
            set { professeur = value; }
        }
        private string dateCommence;
        public string DateCommence
        {
            get { return dateCommence; }
            set { dateCommence = value; }
        }
        private string dateFin;
        public string DateFin
        {
            get { return dateFin; }
            set { dateFin = value; }
        }
        private int maxEtudiants;
        public int MaxEtudiants
        {
            get { return maxEtudiants; }
            set { maxEtudiants = value; }
        }
        private double montant;
        public double Montant
        {
            get { return montant; }
            set { montant = value; }
        }
    }
}