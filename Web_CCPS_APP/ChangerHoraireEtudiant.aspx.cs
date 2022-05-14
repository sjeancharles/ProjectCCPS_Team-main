using System;
using CCPS_Web_Edu_Update;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


namespace Web_CCPS_APP
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        BaseDeDonnees donnees = new BaseDeDonnees();
        protected void Page_Load(object sender, EventArgs e)
        {            
            this.MaintainScrollPositionOnPostBack = true;
            if (!this.IsPostBack)
            {
                Label lblControl = (Label)Master.FindControl("lblPageName");
                if (lblControl != null)
                {
                    lblControl.Text = "Changer Horaire d'Un Etudiant";
                }
                RemplirEtudiants();
                RemplirListeClassesSessions();
            }
        }

        void RemplirEtudiants()
        {
            lblMessage.Text = "";

            try
            {

                string sqlDa = "SELECT DISTINCT Nom, Prenom, P.PersonneID, Nom + ', ' + Prenom as NomComplet FROM Personnes P, EtudiantsCourants E WHERE P.PersonneID = E.PersonneID AND Etudiant = 1 ORDER BY Nom, Prenom, NomComplet, P.PersonneID";
                donnees = new BaseDeDonnees();
                lstTousEtudiants.DataSource = donnees.GetDataSet(sqlDa);
                lstTousEtudiants.DataValueField = "PersonneID";
                lstTousEtudiants.DataTextField = "NomComplet";

                lstTousEtudiants.DataBind();
            }
            catch (Exception ex)
            {
                WriteErrorMessageToLabel("ERREUR: Contactez un techniciens: " + ex.Message,false);
            }
        }

        void RemplirListeClassesSessions()
        {
            lblMessage.Text = "";

            try
            {

                string sqlDa = "SELECT SessionID, JourRencontre + ': ' + Heures As SessionName FROM Sessions WHERE Actif = 1";


                donnees = new BaseDeDonnees();
                lstToutesClasses.DataSource = donnees.GetDataSet(sqlDa);
                lstToutesClasses.DataValueField = "SessionID";
                lstToutesClasses.DataTextField = "SessionName";

                lstToutesClasses.DataBind();
            }
            catch (Exception ex)
            {
                WriteErrorMessageToLabel("ERREUR: Contactez un techniciens: " + ex.Message,false);
            }
        }


        protected void btnChangez_Click(object sender, EventArgs e)
        {
            if (lstToutesClasses.SelectedIndex < 0 || lstClassesEtudiant.SelectedIndex < 0)
            {
                WriteErrorMessageToLabel("Choisissez classes à enlever et ajouter!",false);
                return;
            }

            lblMessage.Text = "";

            string sSql = string.Format("UPDATE EtudiantsCourants SET SessionID = {0} WHERE EtudiantsCourantsID = {1}", lstToutesClasses.SelectedValue.ToString(), lstClassesEtudiant.SelectedValue.ToString());
            try
            {
                donnees = new BaseDeDonnees();
                if (donnees.IssueCommand(sSql))
                {


                    //lblError.Text = "Changement à Succès";

                    RemplirClassePourEtudiantChoissi();
                    WriteErrorMessageToLabel("Changement à Succès ",true);


                }
                else
                {
                    WriteErrorMessageToLabel("ERREUR: Pas de Changement!",false);
                }
            }
            catch (Exception ex)
            {
                WriteErrorMessageToLabel(string.Format("ERREUR: Voir un Technicien ({0})", ex.Message),false);
            }
        }

        void RemplirClassePourEtudiantChoissi()
        {
            //
            lblMessage.Text = "";

            try
            {

                string sSql = "SELECT E.EtudiantsCourantsID, S.SessionID, S.JourRencontre + ': ' + S.Heures As SessionName  FROM Sessions S,Classes C, EtudiantsCourants E WHERE S.ClasseID = C.ClasseID AND S.SessionID = E.SessionID AND S.Actif = 1 AND PersonneID = " + lstTousEtudiants.SelectedValue;

                donnees = new BaseDeDonnees();

                lstClassesEtudiant.DataSource = donnees.GetDataSet(sSql);
                lstClassesEtudiant.DataValueField = "EtudiantsCourantsID";
                lstClassesEtudiant.DataTextField = "SessionName";

                lstClassesEtudiant.DataBind();
            }
            catch (Exception ex)
            {
                WriteErrorMessageToLabel("ERREUR: Contactez un techniciens: " + ex.Message,false);
            }

        }


        protected void lstTousEtudiants_SelectedIndexChanged1(object sender, EventArgs e)
        {
            RemplirClassePourEtudiantChoissi();
        }

        protected void Recherche_TextChanged1(object sender, EventArgs e)
        {
            String sqlDa = "select PersonneID, DISTINCT Nom +', ' + Prenom as NomComplet, Nom, Prenom, DateCreee FROM Personnes where Nom LIKE '%' +'" + Recherche.Text + "'+ '%' OR Prenom LIKE '%'+'" + Recherche.Text + "' + '%' OR Nom+' '+ Prenom LIKE '%'+'" + Recherche.Text + "'+ '%'";
            BaseDeDonnees donne = new BaseDeDonnees();
            lstTousEtudiants.DataSource = donne.GetDataSet(sqlDa);

            lstTousEtudiants.DataBind();
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
    }
}