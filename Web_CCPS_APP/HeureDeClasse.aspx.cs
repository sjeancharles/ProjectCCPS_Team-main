using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CCPS_Web_Edu_Update;


namespace Web_CCPS_APP
{
    public partial class HeureDeClasse : System.Web.UI.Page
    {
        private readonly BaseDeDonnees donnees = new BaseDeDonnees();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMessage.Text = "";
                //Classe Categorie
                RemplirDropCategorie();
            }
            
        }

        protected void AddHeure_Click(object sender, EventArgs e)
        {
            if (txtHeure.Text == String.Empty)
            {
                WriteErrorMessageToLabel("Le champ Heure est obligatoire !", false);
            }
            else if (DroClasseCat.SelectedValue == "0")
            {

                WriteErrorMessageToLabel("Le champ Catégorie est obligatoire !", false);
            }         
            else
            {
                try
                {
                    String sql = "Insert into HeuresDeClasses(HeureDescription,Categorie) values(@HeureDescription,@Categorie)";

                    SqlParameter HDesc = new SqlParameter("@HeureDescription", DbType.String.ToString());
                    HDesc.Value = txtHeure.Text;

                    SqlParameter HCategorie = new SqlParameter("@Categorie", DbType.String.ToString());
                    HCategorie.Value = DroClasseCat.SelectedItem.Text;

                    bool bRes = donnees.IssueCommandWithParams(sql, HDesc, HCategorie);
                    if(bRes)
                    {
                        WriteErrorMessageToLabel("Succès: Votre donnée est enregistrer ", true);
                        ClearFonction();
                    }
                    else
                    {
                        WriteErrorMessageToLabel("ERREUR: Donnée n'est pas enregistrer ", false);
                    }
                }
                catch (Exception ex)
                {
                    WriteErrorMessageToLabel("ERREUR: Voir un techenicien " + ex.Message, false);
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFonction();

        }
        public void ClearFonction()
        {
            txtHeure.Text = "";
            DroClasseCat.SelectedValue = "0";
        }

        public void RemplirDropCategorie()
        {
            try
            {
                if (DroClasseCat.SelectedValue == "0")
                {
                    WriteErrorMessageToLabel("Vous devez choisir une catégorie", false);
                }
                DroClasseCat.Items.Insert(0, new ListItem("Choisissez une catégorie", "0"));
                DroClasseCat.Items.Insert(1, new ListItem("Informatique", "1"));
                DroClasseCat.Items.Insert(2, new ListItem("Anglais", "2"));
                DroClasseCat.Items.Insert(3, new ListItem("Plomberie", "3"));
            }
            catch (Exception ex)
            {
                WriteErrorMessageToLabel("ERREUR: " + ex.Message, false);
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
    }

}