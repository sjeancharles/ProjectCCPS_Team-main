using CCPS_Web_Edu_Update;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Web_CCPS_APP
{
    public partial class AjouterEtudiantDansClasse : System.Web.UI.Page
    {
        private readonly BaseDeDonnees donnees = new BaseDeDonnees();
        protected void Page_Load(object sender, EventArgs e)
        {
            //var GetID1 = Convert.ToInt32(Session["PersonneID"]);
           // Response.Write("Hello " + GetID1);

            if (!this.IsPostBack)
            {
                Label lblControl = (Label)Master.FindControl("MasterPage");
                if (lblControl != null)
                {
                    lblControl.Text = "Inscrire un Etudiant dans une Classe Déterminée";
                }

                RemplirSujet();
                
            }

        }

        /// <summary>
        /// Cette méthode permet d'afficher la liste de options dans une dropdownliste
        /// </summary>
        public void RemplirSujet()
        {
            try
            {
                if (ListeCours.SelectedValue != null)
                {
                    string sSql = "SELECT '1-Choisissez un Sujet' as Categorie UNION SELECT DISTINCT Categorie from Classes group by Categorie Order By Categorie";
                    DataSet ds = new DataSet();
                    ds = donnees.GetDataSet(sSql);
                    this.ListeCours.DataSource = ds.Tables[0];
                    ListeCours.DataTextField = "Categorie";
                    ListeCours.DataBind();
                }
                
            }catch(Exception ex)
            {
                WriteErrorMessageToLabel("ERROR: Check la connection du base de données ou voir un technicien , Assurez-vous que votre sql services est bien start dans votre pc/server. \n " + ex.Message, false);
            }
        }

        /// <summary>
        /// C'est une methode qui permet de choisir les cours offertes par CCPS
        /// </summary>
        private void RemplirClasse()
        {
            try
            {
                string sSql = string.Format("SELECT -1 as ClasseID, '2-Choisissez Une Classe' as NomClasse UNION SELECT ClasseID, NomClasse from Classes WHERE Categorie ='{0}'", ListeCours.SelectedValue);

                ListeNiveau.DataSource = donnees.GetDataSet(sSql);
                ListeNiveau.DataValueField = "ClasseID";
                ListeNiveau.DataTextField = "NomClasse";
                ListeNiveau.DataBind();

            }
            catch(Exception ex)
            {
                WriteErrorMessageToLabel("ERREUR:Pas de classe disponible dans la session ou voir un technicien " + ex.Message, false);
            }
           
        }


        /// <summary>
        /// La Methode affiche la liste des horaires dans une dropdownliste
        /// </summary>
        private void RemplirHoraire()
        {
            try 
            {
                string sSql = "Select -1 As SessionID, '3-Choisissez Un Horaire' As SessionName from Sessions UNION Select SessionID, JourRencontre + ': ' + Heures As SessionName FROM Sessions WHERE Actif = 1 AND ClasseID = " + ListeNiveau.SelectedValue;
                ListeHoraire.DataSource = donnees.GetDataSet(sSql);
                ListeHoraire.DataValueField = "SessionID";
                ListeHoraire.DataTextField = "SessionName";
                ListeHoraire.DataBind();
            }
            catch(Exception ex)
            {
                WriteErrorMessageToLabel("ERREUR: Pas d'Horaire disponible dans la session Courante ou voir un technicien. " + ex, false);
            }
        }

        /// <summary>
        /// La methode affiche la liste de tous les etudiants dans une listbox
        /// </summary>
        /// <param name="sql"></param>
        private void RemplirEtudiants(String sSql)
        {
           // sSql = "SELECT PersonneID, Nom + ', ' + Prenom as NomComplet, Nom, Prenom FROM Personnes ORDER BY Nom, Prenom";
            try 
            {
                DataSet ds = new DataSet();
                ds = donnees.GetDataSet(sSql);
                lstClasse.DataSource = ds.Tables[0];
                lstClasse.DataValueField = "PersonneID";
                lstClasse.DataTextField = "NomComplet";
                lstClasse.DataBind();
            }
           catch(Exception ex)
            {
                WriteErrorMessageToLabel("Pas d'etudiant eligible pour le niveau selectionner " + ex.Message,false);
            }
        }


        /// <summary>
        /// C'est la methode qui permet de faire une recherche par nom ou par prenom.
        /// </summary>

        public void SearchBox()
        {
            String search = Seach.Text.Trim().ToLower().ToString();
            if (!string.IsNullOrEmpty(search) && search.Contains(lstClasse.Text))
            {
                String sSql = "select DISTINCT PersonneID, Nom +' ' + Prenom +'  -->  '+ Convert(varchar, DDN) as NomComplet, Nom, Prenom FROM Personnes where Nom LIKE '%' +'" + search + "'+ '%' OR Prenom LIKE '%'+'" + search + "' + '%' OR Nom+' '+ Prenom LIKE '%'+'" + search + "'+ '%'";
                DataSet ds = new DataSet();
                ds = donnees.GetDataSet(sSql);
                this.lstClasse.DataSource = ds.Tables[0];
                lstClasse.DataValueField = "PersonneID";
                lstClasse.DataTextField = "NomComplet";
                lstClasse.DataBind();
            }
            else if(!string.IsNullOrEmpty(search))
            {
                WriteErrorMessageToLabel("Le nom que vous chercher n'existe pas dans la classe ou cherchez par nom ou prénom",false);
            }

        }

        protected void ListeCours_SelectedIndexChanged(object sender, EventArgs e)
        {
            RemplirClasse();
        }

        protected void ListeNiveau_SelectedIndexChanged(object sender, EventArgs e)
        {
            RemplirListeSessions();
            lstClasse.Items.Clear();
        }

        protected void ListeHoraire_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ListeHoraire.SelectedValue == "-1")
                {
                    WriteErrorMessageToLabel("Choisissez Un haraire D'Abord!", false);
                }

                //Chéquez si la classe est déjà remplie
                else if (SessionRemplie())
                {
                    lblMessage.Text = ("Classe Remplie, Plus de places !!!");
                    //progressbarId.Text = porogressbar();

                    btnTerminer.Enabled = false;
                }

                else
                {
                    int iNiveauClasse = donnees.GetScalar("SELECT NiveauClasse FROM Classes WHERE ClasseID = " + int.Parse(ListeNiveau.SelectedValue));
                    string sSql;
                    if (iNiveauClasse == 1)
                    {
                        // Tout moun: Korije aprè
                        sSql = String.Format("select PersonneID, Nom + ', ' + Prenom as NomComplet FROM Personnes  WHERE NiveauClasse = {0} ORDER BY NomComplet", iNiveauClasse);
                        RemplirEtudiants(sSql);
                    }
                    else if (iNiveauClasse > 1)
                    {
                        // Cas Anciens étudiants: sa classe est classniveau - 1
                        sSql = string.Format("select DISTINCT PersonneID, Nom + ', ' + Prenom as NomComplet  FROM Personnes WHERE PersonneID in " +
                            " (SELECT E.PersonneID FROM EtudiantsCourants E, Classes C, Sessions S WHERE E.SessionID = S.SessionID AND S.ClasseID = C.ClasseID " +
                            " AND C.Categorie = '{0}' AND ((E.Admis = 1 AND C.NiveauClasse = {1}) OR (E.Admis = 0 AND C.NiveauClasse = {2})))  ORDER BY NomComplet ", ListeCours.SelectedValue.ToString(), iNiveauClasse - 1, iNiveauClasse);

                        RemplirEtudiants(sSql);

                    }
                    else
                    {
                        WriteErrorMessageToLabel("Les Conditions sont-elles réunies?\n 1- Vérifier si l'étudiant n'est pas déjà inscrit dans la classe. \n 2-Avez-vous choisi les options à gauche de ta fenêtre? 3- Vérifier si l'étudiant est élligible pour la classe\n 4- Ou voir un technicien. ", false);

                    }
                }
            }catch(Exception ex)
            {
                WriteErrorMessageToLabel("ERROR: Check la connection du base de données ou voir un technicien , Assurez-vous que votre sql services est bien start dans votre pc/server. \n " + ex.Message, false);
            }
        }


        private bool RemplirListeSessions()
        {
            bool bRetVal = false;

            try
            {
               RemplirHoraire();
            }
            catch (Exception ex)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message + "');", true);

                bRetVal = false;
            }

            return bRetVal;
        }

        private bool SessionRemplie()
        {
            bool bRetval;
            try
            {
                int iSessionID = int.Parse(ListeHoraire.SelectedValue.ToString());
                int iMax = donnees.GetScalar("Select MaxEtudiants FROM Sessions WHERE SessionID = " + iSessionID);
                int iNombreInscrits = donnees.GetScalar("Select COUNT(SessionID) FROM EtudiantsCourants WHERE SessionID = " + iSessionID);
                //Progresse bar code start
                // progress bar code end
                return iNombreInscrits >= iMax;

            }
            catch (Exception ex)
            {
                WriteErrorMessageToLabel("Les Conditions sont-elles réunies?\n 1- Vérifier si l'étudiant n'est pas déjà indcrit dans la classe.\n 2- Vérifier si l'étudiant est élligible pour la classe\n 3-Ou voir un technicien. " + ex.Message,false);

                bRetval = false;
            }

            return bRetval;
        }

        /// <summary>
        /// La methode enregistre les etudiants dans une classe.
        /// </summary>
        public void RemplirClasseChoisie()
        {
            try
            {
                String sSql = String.Format("SELECT P.PersonneID, P.Nom + ', ' + P.Prenom + '----' + replace(convert(nvarchar,Isnull(P.DDN,'')), '1900-01-01', 'N/A')  + '----' + Isnull(P.Telephone1,'N/A')  as NomComplet " +
                "FROM Personnes P, EtudiantsCourants E WHERE P.PersonneID = E.PersonneID AND Etudiant = 1 AND E.SessionID = {0} ORDER BY NomComplet", ListeHoraire.SelectedValue.ToString());
                DataSet ds = new DataSet();
                ds = donnees.GetDataSet(sSql);
                this.lstClasse.DataSource = ds.Tables[0];
                lstClasse.DataValueField = "PersonneID";
                lstClasse.DataTextField = "NomComplet";
                lstClasse.DataBind();
            }catch(Exception ex)
            {
                WriteErrorMessageToLabel("ERROR: Check la connection du base de données ou voir un technicien , Assurez-vous que votre sql services est bien start dans votre pc/server. \n " + ex.Message, false);
            }
        }

        protected void lstClasse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnTerminer_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            if (SessionRemplie())
            {
                lblMessage.Text = ("");
            }
            else
            {
                if (ListeCours.SelectedValue.ToLower() == "1-choisissez un sujet")
                {
                    WriteErrorMessageToLabel("Choisissez Un Sujet D'Abord!",false);
                }

                else if (ListeNiveau.SelectedValue == "-1")
                {
                    WriteErrorMessageToLabel("Choisissez Une classe D'Abord!",false);
                }

                else
                {
                    try
                    {
                        SqlParameter paramSessionID = new SqlParameter("@SessionID", SqlDbType.Int);
                        paramSessionID.Value = int.Parse(ListeHoraire.SelectedValue);
                        SqlParameter paramStudentID = new SqlParameter("@PersonneID", SqlDbType.Int);
                        paramStudentID.Value = int.Parse(lstClasse.SelectedValue);
                        SqlParameter paramUserId = new SqlParameter("@CreeParUsername", SqlDbType.VarChar);
                        paramUserId.Value = BaseDeDonnees.GetWindowsUser();

                        // Save info in ClassSession table
                        string sSql = "INSERT INTO EtudiantsCourants (SessionID, PersonneID, CreeParUsername) VALUES (@SessionID, @PersonneID, @CreeParUsername)";


                        if (donnees.IssueCommandWithParams(sSql, paramSessionID, paramStudentID, paramUserId))
                        {

                            WriteErrorMessageToLabel("Succès: étudiant inscrit avec succès!!!",true);
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "myscript", "setTimeout(function(){location.href='Personne.aspx';},1000);", true);
                        }
                        else
                        {
                            WriteErrorMessageToLabel("Les Conditions sont-elles réunies?\n 1- Vérifier si l'étudiant n'est pas déjà inscrit dans la classe. \n 2-Avez-vous choisi les options à gauche de ta fenêtre? 3- Vérifier si l'étudiant est élligible pour la classe\n 4- Ou voir un technicien. ",false);
                        }

                        //Redirect to another page

                    }
                    catch (Exception ex)
                    {
                        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ex", "alert('" + ex.Message + "');", true);
                        WriteErrorMessageToLabel("Les Conditions sont-elles réunies?\n 1- Vérifier si vous avez choisi l'option, " +
                            "l'horaire et la classe vous voulez inscrire l'étudiant.\n 2- Vérifier si l'étudiant est élligible pour la classe " + ex.Message,false);


                    }
                }
            }
        }

        protected void Seach_TextChanged(object sender, EventArgs e)
        {
            SearchBox();

        }

       //string porogressbar()
       // {
       //     try
       //     {
       //         String ProBar = "";
       //         int total = 100;
       //         int iSessionID = int.Parse(ListeHoraire.SelectedValue.ToString());
       //         int iMax = donnees.GetScalar("Select MaxEtudiants FROM Sessions WHERE SessionID = " + iSessionID);
       //         int iNombreInscrits = donnees.GetScalar("Select COUNT(SessionID) FROM EtudiantsCourants WHERE SessionID = " + iSessionID);

       //         int width_bar = 75;//(iNombreInscrits * total) / iMax;
       //         ProBar += String.Format("<div class='progress' style='height:25px;'>" +
       //                                       "<div class='progress-bar progress-bar-striped bg-success' runat='server'" +
       //                                          " role='progressbar' aria-valuenow='25' style='width:" + width_bar + "%;'" +
       //                                          "aria-valuemin='0' aria-valuemax='" + total + ">" + iNombreInscrits +
       //                                       "</div>" +
       //                                   "</div>");


       //     }catch(Exception ex)
       //     {
       //         WriteErrorMessageToLabel("ERROR: Check la connection du base de données ou voir un technicien , Assurez-vous que votre sql services est bien start dans votre pc/server. \n " + ex.Message, false);
       //     }
       //    return ProBar;
       // }

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