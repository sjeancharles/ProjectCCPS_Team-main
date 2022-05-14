using CCPS_Web_Edu_Update;
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
    public partial class AjouterClasseDansLaSessionCourante : System.Web.UI.Page
    {
        BaseDeDonnees donnees = new BaseDeDonnees();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(IsPostBack))
            {
                RemplirDropdownClasse();
                RemplirDropdownJourDeClasse();
                SelectDate();
                RemplirDropProfesseur();
            }
        }

        void RemplirDropdownClasse()
        {
            try
            {
                string sql = "SELECT ClasseID, NomClasse FROM Classes";
                donnees = new BaseDeDonnees();
                //DataTable ds = new DataTable();
                DataSet ds = new DataSet();
                ds = donnees.GetDataSet(sql);
                this.NomClasse.DataSource = ds;
                NomClasse.DataValueField = "ClasseID";
                NomClasse.DataTextField = "NomClasse";
                NomClasse.DataBind();
                NomClasse.Items.Insert(0, new ListItem("Choisir la Classe", "0"));
            }
            catch (Exception ex)
            {
                string sError = ex.Message.ToString();
                Debug.WriteLine(sError);
                sError = "";
            }
        }

        void RemplirDropdownJourDeClasse()
        {
            try
            {
                string sql = "SELECT JourID,JourDescription FROM JoursDeClasses";
                donnees = new BaseDeDonnees();
                //DataTable ds = new DataTable();
                DataSet ds = new DataSet();
                ds = donnees.GetDataSet(sql);
                this.dJourDeClasse.DataSource = ds;
                dJourDeClasse.DataValueField = "JourID";
                dJourDeClasse.DataTextField = "JourDescription";
                dJourDeClasse.DataBind();
                dJourDeClasse.Items.Insert(0, new ListItem("Choisir le(s) jour(s) de classe", "0"));
            }
            catch (Exception ex)
            {
                string sError = ex.Message.ToString();
                Debug.WriteLine(sError);
                sError = "";
            }

        }

        void SelectDate()
        {
            try
            {
                String ChaineDeConnexion = ConfigurationManager.ConnectionStrings["connection"].ToString();
                string sSql = "SELECT SessionDateID, Convert(varchar, SessionDateDebut) + ' - ' + Convert(varchar,SessionDateFin) AS SessionDate from DatesSessionCourante WHERE Actif = 1  ORDER BY SessionDateDebut DESC";
                donnees.GetDataReader(sSql);
                SqlDataAdapter da = new SqlDataAdapter(sSql, ChaineDeConnexion);
                DataTable dTable = new DataTable();
                da.Fill(dTable);

                DataTableReader dr = dTable.CreateDataReader();


                if (dr != null)
                {
                    dr.Read();
                    string[] sTemp = dr["SessionDate"].ToString().Split('-');
                    lblDateDebut.InnerText = "Date Debut de la session : " + sTemp[0].Trim();
                    lblDateFin.Text = "Date fin de La session : " + sTemp[1].Trim();
                }
            }
            catch (Exception ex)
            {
                string sError = ex.Message.ToString();
                Debug.WriteLine(sError);
                sError = "";
            }

        }

        void RemplirDropProfesseur()
        {
            try
            {
                string sSql = "SELECT -1 as PersonneID, 'Choisir professeur pour la classe' as Nom UNION SELECT PersonneID, Nom + ', ' + Prenom AS Nom from Personnes WHERE Professeur = 1 AND DelPersonne = 0";
                donnees = new BaseDeDonnees();
                //DataTable ds = new DataTable();
                DataSet ds = new DataSet();
                ds = donnees.GetDataSet(sSql);
                this.DrpProfesseurName.DataSource = ds;
                DrpProfesseurName.DataValueField = "PersonneID";
                DrpProfesseurName.DataTextField = "Nom";
                DrpProfesseurName.DataBind();
                //dJourDeClasse.Items.Insert(0, new ListItem("Choisir le(s) jour(s) de classe", "0"));
            }
            catch (Exception ex)
            {
                string sError = ex.Message.ToString();
                Debug.WriteLine(sError);
                sError = "";
            }
        }

        void RemplirDropHeure()
        {
            try
            {
                donnees = new BaseDeDonnees();
                string sSql = string.Format("SELECT -1 as HeureID, 'Cliquez pour Choisir' as HeureDescription UNION SELECT HeureID, HeureDescription from HeuresDeClasses H, Classes C WHERE C.ClasseID = {0} AND H.Categorie = C.Categorie", NomClasse.SelectedValue.ToString());
                // String sSql = "SELECT -1 as HeureID, 'Cliquez pour Choisir' as HeureDescription UNION SELECT HeureID, HeureDescription from HeuresDeClasses H, Classes C WHERE C.ClasseID = ClasseID AND H.Categorie = C.Categorie";
                //DataTable ds = new DataTable();
                DataSet ds = new DataSet();
                ds = donnees.GetDataSet(sSql);
                this.DropHeureDeClasse.DataSource = ds;
                DropHeureDeClasse.DataValueField = "HeureID";
                DropHeureDeClasse.DataTextField = "HeureDescription";
                DropHeureDeClasse.DataBind();
                // DropHeureDeClasse.Items.Insert(0, new ListItem("Choisir l'heure de classe", "0"));
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
            //if (dropSalleDeClasse.SelectedIndex == -1)
            //    bValeur = false;
            if (DrpProfesseurName.SelectedIndex == -1)
                bValeur = false;
            if (dJourDeClasse.SelectedIndex == -1)
                bValeur = false;
            if (DropHeureDeClasse.SelectedIndex == -1)
                bValeur = false;

            if (lblDateDebut.InnerText == string.Empty)
                bValeur = false;
            if (lblDateFin.Text == string.Empty)
                bValeur = false;

            if (txtMaxEtudiant.Text == string.Empty)
                bValeur = false;
            if (txtMontant.Text == string.Empty)
                bValeur = false;
            return bValeur;
        }

        void ResetToutBagay()
        {
            NomClasse.SelectedIndex = -1;
            dJourDeClasse.SelectedIndex = -1;
            //dropSalleDeClasse.SelectedIndex = -1;
            DrpProfesseurName.SelectedIndex = -1;
            DropHeureDeClasse.SelectedIndex = -1;
            txtMaxEtudiant.Text = string.Empty;
            txtMontant.Text = string.Empty;
        }

        protected void BtnAddClasse_Click(object sender, EventArgs e)
        {
            donnees = new BaseDeDonnees();
            string[] sTemp = new string[2];
            // Split date Debut and Date fin
            String ChaineDeConnexion = ConfigurationManager.ConnectionStrings["connection"].ToString();
            string sSql = "SELECT SessionDateID, Convert(varchar, SessionDateDebut) + ' - ' + Convert(varchar,SessionDateFin) AS SessionDate from DatesSessionCourante WHERE Actif = 1  ORDER BY SessionDateDebut DESC";
            donnees.GetDataReader(sSql);
            SqlDataAdapter da = new SqlDataAdapter(sSql, ChaineDeConnexion);
            DataTable dTable = new DataTable();
            da.Fill(dTable);

            //DataTableReader dt = donnees;

            DataTableReader dr = dTable.CreateDataReader();


            if (dr != null)
            {
                dr.Read();
                sTemp = dr["SessionDate"].ToString().Split('-');
                lblDateDebut.InnerText = sTemp[0].Trim();
                lblDateFin.Text = sTemp[1].Trim();
            }

            // Fin
            try
            {
                if (!ToutBagayPaAnfom())
                {
                    lblError.Text = ("Toutes les Informations sont obligatoires Pour Continuer !!!");
                }
                else
                {
                    lblError.Text = string.Empty;
                    string sSql1 = string.Format("INSERT INTO Sessions(ClasseID, ProfesseurID, MaxEtudiants, JourRencontre, Heures, " +
                        "MontantParticipation, DateCommence, DateFin, byUsername) VALUES ({0},{1},{2},'{3}','{4}',{5},'{6}','{7}','{8}')",
                        NomClasse.SelectedItem.Value, DrpProfesseurName.SelectedItem.Value, txtMaxEtudiant.Text, dJourDeClasse.SelectedItem.Text,
                        DropHeureDeClasse.SelectedItem.Text, txtMontant.Text, lblDateDebut.InnerText, lblDateFin.Text, BaseDeDonnees.GetWindowsUser()); //donnees.GetWindowsUser()

                    if (donnees.IssueCommand(sSql1))
                    {
                        lblSucces.Text = "Données sauvegardees avec succes !!";
                        ResetToutBagay();   // Pou ka rantre lòt donnees
                    }
                    else
                    {
                        lblError.Text = "ERROR: Données PAS Sauvegardées -- Voir Un Technicien!!!";
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ("ERROR: Données PAS Sauvegardées " + ex);
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            ResetToutBagay();
        }

        protected void NomClasse_SelectedIndexChanged1(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;
            RemplirDropHeure();
        }
    }
}