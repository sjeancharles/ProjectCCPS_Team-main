using System;
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
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RemplirOption();
            }
        }

        // La methode RemplirOption
        void RemplirOption()
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
                string sSql = "SELECT DISTINCT Categorie from Classes group by Categorie Order By Categorie";
                SqlDataAdapter da = new SqlDataAdapter(sSql, myConnection);
                DataTable dTable = new DataTable();
                da.Fill(dTable);

                drowpListOption.DataSource = dTable;
                drowpListOption.DataTextField = "Categorie";
                drowpListOption.DataBind();
                drowpListOption.Items.Insert(0, new ListItem("1-Choisissez une option", "0"));
            }
            catch (Exception ex)
            {
                string sError = ex.Message.ToString();
                Debug.WriteLine(sError);
                lblError.Text = sError;
            }
        }// Fin de la methode Remplir Option


        // La methode RemplirClasses
        void RemplirClasses()
        {
            if (drowpListOption.SelectedValue.ToLower() == "1-choisissez une Option")
            {
                lblError.Text = "Choisissez Une Option D'Abord!";
                return;
            }

            try
            {
                SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
                string sSql = string.Format("SELECT -1 as ClasseID, '2-Choisissez Une Classe' as NomClasse UNION SELECT ClasseID, NomClasse from Classes WHERE Categorie ='{0}'", drowpListOption.SelectedValue);

                SqlDataAdapter da = new SqlDataAdapter(sSql, myConnection);
                DataTable dTable = new DataTable();
                da.Fill(dTable);

                DropDownListClasse.DataSource = dTable;
                DropDownListClasse.DataValueField = "ClasseID";
                DropDownListClasse.DataTextField = "NomClasse";
                DropDownListClasse.DataBind();

            }
            catch (Exception ex)
            {
                string sError = ex.Message.ToString();
                Debug.WriteLine(sError);
                sError = "";
            }
        }// Fin de la methode Remplir Classes

        // La methode RemplirListeSessions
        bool RemplirListeSessions()
        {
            bool bRetVal = false;

            lblError.Text = "";

            try
            {
                SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
                string sSql = "Select 0 As SessionID, '3-Choisissez Un Horaire' As SessionName from Sessions UNION Select SessionID, JourRencontre + ': ' + Heures As SessionName FROM Sessions WHERE Actif = 1 AND ClasseID = " + DropDownListClasse.SelectedValue;

                SqlDataAdapter da = new SqlDataAdapter(sSql, myConnection);
                DataTable dTable = new DataTable();
                da.Fill(dTable);

                DropDownListHoraire.DataSource = dTable;
                DropDownListHoraire.DataValueField = "SessionID";
                DropDownListHoraire.DataTextField = "SessionName";
                DropDownListHoraire.DataBind();


            }
            catch (Exception ex)
            {
                lblError.Text = "ERREUR: Contactez un techniciens: " + ex.Message;
                bRetVal = false;
            }

            return bRetVal;
        } // Fin RemplirListeSessions


        // La methode RemplirClasseChoisie
        void RemplirClasseChoisie()
        {
            String sSql = String.Format("SELECT P.PersonneID, P.Nom + ', ' + P.Prenom + '----' + replace(convert(nvarchar,Isnull(P.DDN,'')), '1900-01-01', 'N/A')  + '----' + Isnull(P.Telephone1,'N/A')  as NomComplet " +
                "FROM Personnes P, EtudiantsCourants E WHERE P.PersonneID = E.PersonneID AND Etudiant = 1 AND E.SessionID = {0} ORDER BY NomComplet", DropDownListHoraire.SelectedValue.ToString());

            lblError.Text = "";

            try
            {
                SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);

                SqlDataAdapter da = new SqlDataAdapter(sSql, myConnection);
                DataTable dTable = new DataTable();
                da.Fill(dTable);

                ListeEtudiants.DataSource = dTable;
                ListeEtudiants.DataValueField = "PersonneID";
                ListeEtudiants.DataTextField = "NomComplet";
                ListeEtudiants.DataBind();
                int count = ListeEtudiants.Items.Count;
                lblCount.Text = "Il y a \"" + count + "\" etudiant(s) dans cette classe";
            }
            catch (Exception ex)
            {
                lblError.Text = "ERREUR: Contactez un techniciens: " + ex.Message;
            }

        } // Fin RemplirClasseChoisie


        // La methode ToutBagayPaAnfom pour verifier si toutes les conditions sont respectées
        bool ToutBagayPaAnfom()
        {
            bool bValeur = true;

            if (drowpListOption.SelectedIndex == 0)
                bValeur = false;
            if (DropDownListClasse.SelectedIndex == -1)
                bValeur = false;
            if (DropDownListHoraire.SelectedIndex == -1)
                bValeur = false;

            return bValeur;
        }// Fin de la  methode ToutBagayPaAnfom 


        // La methode Nettoyage 
        void Nettoyage()
        {
            drowpListOption.SelectedIndex = 0;
            DropDownListClasse.Items.Clear();
            DropDownListHoraire.Items.Clear();
            ListeEtudiants.Items.Clear();
            lblError.Text = string.Empty;

        } // Fin Methode Nettoyage


        // drowpListOption
        protected void drowpListOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            RemplirClasses();
            ListeEtudiants.Items.Clear();
            lblError.Text = "";
        } // Fin drowpListOption

        //  DropDownListClasse
        protected void DropDownListClasse_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblError.Text = "";
            RemplirListeSessions();
            ListeEtudiants.Items.Clear();
        }// Fin DropDownListClasse

        //  DropDownListHoraire
        protected void DropDownListHoraire_SelectedIndexChanged(object sender, EventArgs e)
        {
            RemplirClasseChoisie();
            lblError.Text = "";
        }// Fin DropDownListHoraire
    }
}