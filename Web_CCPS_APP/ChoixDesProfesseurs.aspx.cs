using CCPS_Web_Edu_Update;
using System;
using System.Collections;
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
    public partial class ChoixDesProfesseurs : System.Web.UI.Page
    {
        private BaseDeDonnees donnees = new BaseDeDonnees();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SelectActifProf();
                lblMessage.Text = "";
            }
        }


        protected void SaveProfChoisir_Click(object sender, EventArgs e)
        {
            try
            {
                List<ListItem> selected = ChProfActifID.Items.Cast<ListItem>().Where(li => li.Selected).ToList();
                int n = ChProfActifID.Items.Count; int a = 0;
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager
                            .ConnectionStrings["connection"].ConnectionString;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        conn.Open();

                        foreach (ListItem item in ChProfActifID.Items)
                        {
                            n -= 1;

                            if (item.Selected == true)
                            {
                                String Sql = string.Format("UPDATE Personnes SET Etudiant = 0, AdminStaff = 1, Actif = 1 WHERE PersonneID = {0}", item.Value);
                                cmd.CommandText = Sql;
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@PersonneID", item.Value);
                                a = cmd.ExecuteNonQuery();

                            }

                        }
                        if (a == 1 && n == 0)
                        {
                            WriteErrorMessageToLabel("Succès,votre base de donnée est mise a jour !!!", true);
                            Textarea1.InnerText = "";
                            ChProfActifID.SelectedIndex = -1;
                            conn.Close();
                        }
                        else if (a == 0 || selected == null)
                        {
                            WriteErrorMessageToLabel("ERROR: Votre base de donnée n'est pas mise a jour, Assurez-vous que au moins un nom est séléctionné de la liste", false);
                        }
                    }
                }
            }catch(Exception ex)
            {
                WriteErrorMessageToLabel("ERROR: Erreur de connection, voir un technicien !!! " + ex.Message, false);

            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            ChProfActifID.SelectedIndex = -1;
            lblMessage.Text = "";
            Textarea1.InnerText = "";
        }

        protected void ChProfActifID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string str = "";
                int i = 0;
                if (ChProfActifID.Items.Count > 0)
                {

                    foreach (ListItem item in ChProfActifID.Items)
                    {
                        
                        try
                        {
                            if (item.Selected)
                            {
                                i += 1;
                                str += i + ". " + item.Text + "\n";
                            }
                           // donnees.IssueCommand(string.Format("UPDATE Personnes SET AdminStaff = 1,Actif = 1 WHERE PersonneID = {0} AND DelPersonne = 0", ChProfActifID.SelectedItem.Value));
                        }
                        catch (Exception ex)
                        {
                            WriteErrorMessageToLabel("Vous devez faire un choix " + ex.Message,false);
                        }
                    }

                   // List<ListItem> selected = ChProfActifID.Items.Cast<ListItem>().Where(li => li.Selected).ToList();

                }
                else if (ChProfActifID.SelectedIndex != -1)
                {
                    
                    ChProfActifID.SelectedIndex = -1;
                    ChProfActifID.SelectedIndex = 0;
                }
               

                WriteErrorMessageToLabel("Vous séléctionnez: " + i + " Professeur(s)", true);
                Textarea1.InnerText = str;

            }catch(Exception ex)
            {
                WriteErrorMessageToLabel("ERROR: Check la connection du base de données ou voir un technicien , Assurez-vous que votre sql services est bien start dans votre pc/server. \n " + ex.Message, false);

            }
        }

        public void SelectActifProf()
        {
            try
            {
                string sSql = "SELECT CONCAT (Nom,Prenom) , Nom +' '+ Prenom as NomProfActif,PersonneID FROM Personnes WHERE Professeur = 1 AND DelPersonne = 0 ";

                donnees = new BaseDeDonnees();
                //DataTable ds = new DataTable();
                DataSet ds = new DataSet();
                ds = donnees.GetDataSet(sSql);
                this.ChProfActifID.DataSource = ds;
                ChProfActifID.DataValueField = "PersonneID";
                ChProfActifID.DataTextField = "NomProfActif";
                ChProfActifID.DataBind();
            }
           catch(Exception ex)
            {
                WriteErrorMessageToLabel("Error: " + ex.Message,false);
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