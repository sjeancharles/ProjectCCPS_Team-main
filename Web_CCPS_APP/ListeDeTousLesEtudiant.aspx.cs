using CCPS_Web_Edu_Update;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_CCPS_APP
{
    public partial class ListeDeTousLesEtudiant : System.Web.UI.Page
    {
        BaseDeDonnees donnees = new BaseDeDonnees();
        protected void Page_Load(object sender, EventArgs e)
        {
            liste_de_tous_les_etudiant();
            if (!this.IsPostBack)
            {
                Label lblControl = (Label)Master.FindControl("lblPageName");
                if (lblControl != null)
                {
                    lblControl.Text = "Liste Complete des Etudiants";
                }
                RemplirEtudiants();
            }
        }


        void RemplirEtudiants()
        {

            lblError.InnerText = "";
            try
            {
                SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
                //string sSql = "SELECT PersonneID, Nom + ', ' + Prenom as NomComplet FROM Personnes WHERE Etudiant = 1 ORDER BY Nom, Prenom";
                string sSql = "SELECT PersonneID, Nom + ', ' + Prenom + ', ' + DDN + ', ' +Telephone1 as NomComplet  FROM Personnes WHERE Etudiant = 1 ORDER BY Nom, Prenom";

                SqlDataAdapter da = new SqlDataAdapter(sSql, myConnection);
                DataTable dTable = new DataTable();
                da.Fill(dTable);

                lstTousEtudiants.DataSource = dTable;
                lstTousEtudiants.DataValueField = "PersonneID";
                lstTousEtudiants.DataTextField = "NomComplet";

                lstTousEtudiants.DataBind(); ;
            }
            catch (Exception ex)
            {
                lblError.InnerText = "ERREUR: Contactez un techniciens: " + ex.Message;
            }
        }


        void DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedValue == "Etudiants dans la session courante")
            {
                String sqlDa = "SELECT Nom  FROM Personnes WHERE Etudiant = 1 order by Nom ASC";
                donnees = new BaseDeDonnees();
                lstTousEtudiants.DataSource = donnees.GetDataSet(sqlDa);
                lstTousEtudiants.DataTextField = "Nom";
                lstTousEtudiants.DataBind();

            }

            else if (DropDownList1.SelectedValue == "Tous les Etudiants")
            {
                string sqlDa = "SELECT PersonneID, Nom + ', ' + Prenom + ', ' + DDN + ', ' +Telephone1 as NomComplet  FROM Personnes WHERE Etudiant = 1 ORDER BY Nom, Prenom";
                donnees = new BaseDeDonnees();
                lstTousEtudiants.DataSource = donnees.GetDataSet(sqlDa);
                lstTousEtudiants.DataTextField = "Prenom";
                lstTousEtudiants.DataBind();
            }
            else{
                liste_de_tous_les_etudiant();
            }
        }


        public void SearchBox()
        {

            String search1 = txtSearch.Text.Trim().ToLower().ToString();
            if (!string.IsNullOrEmpty(search1) && search1.Contains(lstTousEtudiants.Text))
            {
                String sSql = "select DISTINCT PersonneID, Nom +', ' + Prenom as NomComplet, Nom, Prenom FROM Personnes where Nom LIKE '%' +'" + search1 + "'+ '%' OR Prenom LIKE '%'+'" + search1 + "' + '%' OR Nom+' '+ Prenom LIKE '%'+'" + search1 + "'+ '%'";
                donnees = new BaseDeDonnees();
                lstTousEtudiants.DataSource = donnees.GetDataSet(sSql);
                lstTousEtudiants.DataTextField = "NomComplet";
                lstTousEtudiants.DataBind();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "MessageBox", "alert('Le nom que vous cherchez n'existe pas.');", true);
            }

        }


        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchBox();
        }


        protected void btnEditez_Click(object sender, EventArgs e)
        {
            if (lstTousEtudiants.SelectedIndex <= 0)
                lblError.InnerText = "Choisissez d'abord un Etudiant";
            else
                Response.Redirect("Inscription_Etudiant.aspx?pid=" + lstTousEtudiants.SelectedValue);
        }



        void liste_de_tous_les_etudiant()
        {
            string sqlDa = "SELECT PersonneID, Nom + ', ' + Prenom + ', ' +Telephone1 as NomComplet  FROM Personnes WHERE Etudiant = 1 ORDER BY Nom, Prenom";
            donnees = new BaseDeDonnees();
            lstTousEtudiants.DataSource = donnees.GetDataSet(sqlDa);
            lstTousEtudiants.DataValueField = "NomComplet";
            lstTousEtudiants.DataTextField = "NomComplet";
            lstTousEtudiants.DataBind();         

        }
    }
}