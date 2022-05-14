using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using CCPS_Web_Edu_Update;

namespace Web_CCPS_APP
{
    public partial class Personne : System.Web.UI.Page
    {
        BaseDeDonnees donnees = new BaseDeDonnees();
        string sPersonneID = string.Empty;
        int etu;
        int prof;
        int addstaf;
        string sEditFlag = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
            //string sPersonneID = string.Empty;
            lblMessage.Text = "";
            try
                {
                    sPersonneID = Request.QueryString["PersonneID"];
                    sEditFlag = Request.QueryString["EditFlag"];
                    txtPersonneID.Text = sPersonneID; 
                    // Enlever le cookie
                    HttpCookie cookie = new HttpCookie("PersonneID", string.Empty); // Second method, just in case
                    Response.Cookies.Set(cookie);
                    Response.Cookies.Remove("PersonneID");                          // First method
                }
                catch (Exception Excep)
                {
                    Debug.WriteLine(Excep.Message);
                }

            //RemplirListeDisciplines();
            //
            if (sPersonneID == string.Empty)     // Pas de correction - Nouvelle Entrée
            {
                txtPrenom.Focus();
            }
            else
            {
                sEditFlag = Request.QueryString["EditFlag"];
                // Enlever le cookie
                Response.Cookies.Remove("EditFlag");                          // First method
                HttpCookie cookieEditFlag = new HttpCookie("EditFlag", string.Empty); // Second method, just in case
                Response.Cookies.Set(cookieEditFlag);
                if (sEditFlag == "1")
                {
                    RemplirInfoPourCorrection(sPersonneID);
                }
            }
        }

        // Remplir les input des information pour etre corriger

        protected void RemplirInfoPourCorrection(String sPersonneID)
        {
            using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString()))
            {
                try
                {
                    sqlConn.Open();
                    //String sSql = String.Format("SELECT * FROM Personnes WHERE PersonneID = '{0}'", sPersonneID);
                    String sSql = String.Format("SELECT PersonneID,Prenom,Nom,ISNULL(Telephone1, '') AS Telephone1 ,ISNULL(Telephone2, '') AS Telephone2," +
                        "ISNULL(DDN, '') AS DDN,ISNULL(AdresseRue, '') AS AdresseRue,ISNULL(AdresseExtra, '') AS AdresseExtra,ISNULL(Ville, '') AS Ville," +
                        "ISNULL(Pays, '') AS Pays,DateCreee,ISNULL(Remarque, '') AS Remarque, Etudiant,Professeur, AdminStaff, Photo,ISNULL(UserNameAttribue, '') AS UserNameAttribue," +
                        " CreeParUsername, ISNULL(NumeroRecu, '') AS NumeroRecu,ISNULL(Email, '') AS Email,Sexe,DelPersonne,NiveauEtude,Departement FROM CCPAP_Web_Edu.[dbo].[Personnes] WHERE PersonneID = '{0}'", sPersonneID);

                    SqlDataReader dtTemp = donnees.GetDataReader(sSql, sqlConn);
                    if (dtTemp.Read())
                    {
                        // Fill info
                        txtPersonneID.Text = sPersonneID;
                        txtPrenom.Text = dtTemp["Prenom"].ToString();
                        txtPrenom.Enabled = false;
                        txtNom.Text = dtTemp["Nom"].ToString();
                        txtNom.Enabled = false;
                       

                        txtdate.TextMode = TextBoxMode.SingleLine;

                        if (dtTemp["DDN"].ToString() != String.Empty)
                        {
                            DateTime dt = DateTime.Parse(dtTemp["DDN"].ToString());
                            txtdate.Text = (dt.Day < 10 ? "0" + dt.Day.ToString() : dt.Day.ToString()) + "-" + dt.ToString("MMM") +
                                "-" + dt.Year.ToString();
                        }
                        txtdate.Enabled = false;
                        // 
                        DrpSexe.SelectedItem.Text = dtTemp["Sexe"].ToString();
                        txttelephone.Text = dtTemp["Telephone1"].ToString();
                        Txturgence.Text = dtTemp["Telephone2"].ToString();
                        txtemail.Text = dtTemp["Email"].ToString();
                        txtnumeromaison.Text = dtTemp["AdresseExtra"].ToString();
                        txtrue.Text = dtTemp["AdresseRue"].ToString();
                        txtville.Text = dtTemp["Ville"].ToString();
                        txtRecu.Text = dtTemp["NumeroRecu"].ToString();
                        txtNivEtude.Text = dtTemp["NiveauEtude"].ToString();
                        DropDownDepartement.SelectedItem.Text = dtTemp["Departement"].ToString();
                        txtRemarque.InnerText = dtTemp["Remarque"].ToString();
                        prof =Convert.ToInt32(dtTemp["Professeur"].ToString());
                        etu = Convert.ToInt32(dtTemp["Etudiant"].ToString());
                        addstaf = Convert.ToInt32(dtTemp["AdminStaff"].ToString());


                        if (prof !=0)
                        {
                            rdbProf.Checked = true;                           
                        }
                        if (addstaf != 0 && prof!=0)
                        {
                            rdbProf.Checked = true;
                            chbadStaff.Checked = true;
                        }
                        if(etu !=0)
                        {
                            rdbStu.Checked = true;                          
                        }
                        if (addstaf != 0 && etu !=0)
                        {
                            rdbStu.Checked = true;
                            chbadStaff.Checked = true;
                        }
                        else if(addstaf !=0)
                        {
                            chbadStaff.Checked = true;
                        }
                        else
                        {
                            chbadStaff.Checked = false;
                        }

                    }
                    donnees = null;

                   // SetDisciplineEtudiant(dtTemp["DisciplineID"].ToString());
                }
                catch (Exception ex)
                {
                    lblMessage.Text = string.Format("ERROR : {0} !!!", ex.Message);
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    donnees = null;
                }
            }
        }


        protected void txtNom_TextChanged1(object sender, EventArgs e)
        {
            if (txtPrenom.Text != String.Empty && txtNom.Text != String.Empty)
            {
                String sFirstName = txtPrenom.Text.Trim();
                String sLastName = txtNom.Text.Trim();
                //look if student is already in the system
                // Show liste of student with same name, show DDN and Telephon1 with it.

                // RemplirListeEtudiants(sFirstName, sLastName);
            }
        }

        protected void txtPrenom_TextChanged1(object sender, EventArgs e)
        {
            if (txtPrenom.Text != String.Empty && txtNom.Text != String.Empty)
            {
                String sFirstName = txtPrenom.Text.Trim();
                String sLastName = txtNom.Text.Trim();
                //look if student is already in the system
                // Show liste of student with same name, show DDN and Telephon1 with it.

                // RemplirListeEtudiants(sFirstName, sLastName);
                // sPersonneID = lstEtudiantsExistants.SelectedValue.ToString();
            }
        }

        protected void btnsauvegarder_Click1(object sender, EventArgs e)
        {
            bool a = checkInput();

            try
            {
                //ToutbagayOk();
                if (txtNom.Enabled == false && txtPrenom.Enabled == false && a == true)
                {
                    Edit();
                }
                //else if(checkInput())
                //{
                //    methodesauvegarder();
                //}
               // LoadJScript();

            }
            catch (Exception exeption)
            {

            }

        }

        internal void LoadJScript()
        {
            ClientScriptManager script = Page.ClientScript;
            //prevent duplicate script
            if (!script.IsStartupScriptRegistered(this.GetType(), "HideLabel"))
            {
                script.RegisterStartupScript(this.GetType(), "HideLabel",
                "<script type='text/javascript'>HideLabel('" + lblMessage.ClientID + "')</script>");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if(sEditFlag == "1" && sPersonneID != String.Empty)
            {
                // Return to the path where you're coming from.
                Response.Redirect("AllStudentList.aspx");
                Nettoyer();
            }
            else
            {
                Nettoyer();
            }
        }

        // Les Methe helper
        /// <summary>
        /// methode pou nettoyer ecran soit le moun nan fin anregistre oubyn le moun nan ap anile
        /// </summary>
        public void Nettoyer()
        {
            txtNom.Text = "";
            txtPrenom.Text = "";
            txtdate.Text = "";
            DrpSexe.SelectedIndex = -1;
            txttelephone.Text = "";
            Txturgence.Text = "";
            txtemail.Text = "";
            txtnumeromaison.Text = "";
            txtrue.Text = "";
            txtville.Text = "";
            txtRecu.Text = "";
            txtNivEtude.Text = "";
            txtRemarque.InnerText = "";
            
            lblMessage.Text = "";
            DropDownDepartement.SelectedIndex = -1;
           
        }
     
        public bool ToutbagayOk()
        {
            bool bValeur = true;
            if (DrpSexe.SelectedIndex == -1)
                bValeur = false;
            if (DropDownDepartement.SelectedIndex == -1)
                bValeur = false;

            return bValeur;
        }

        public bool checkInput()
        {
            bool itFine = true;
            if (txtNom.Text == String.Empty)
            {
                WriteErrorMessageToLabel("Le champ nom est obligatoire !", false);
                itFine = false;
            }
            else if (DrpSexe.SelectedItem.Text == "Choisir Sexe")
            {
                WriteErrorMessageToLabel("Choisissez le sexe de la personne !", false);
                itFine = false;
            }
            else if (txtPrenom.Text == string.Empty)
            {
                WriteErrorMessageToLabel("Le champ Prenom est obligatoire !", false);
                itFine = false;
            }
            else if (txtdate.Text == string.Empty)
            {
                WriteErrorMessageToLabel("Le champ Date de naissance est obligatoire !", false);
                itFine = false;
            }
            else if (txtNivEtude.Text == string.Empty)
            {
                WriteErrorMessageToLabel("Le champ Niveau d'étude est obligatoire !", false);
                itFine = false;
            }
            else if (txttelephone.Text == string.Empty)
            {
                WriteErrorMessageToLabel("Le champ Telephone est obligatoire !", false);
                itFine = false;
            }

            else if (DropDownDepartement.SelectedItem.Text == "Choisir Departement")
            {
                WriteErrorMessageToLabel("Choisissez le departement !", false);
                itFine = false;
            }

            return itFine;
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

        /// <summhoary>
        /// Methode pou change enfomasyon ki antre yo an lower to upper case
        /// </summary>
        //////void lowertoupper()
        //////{
        //////    try
        //////    {
        //////        try
        //////        {
        //////            char[] a = txtNom.Text.ToCharArray();
        //////            string a1 = a[0].ToString().ToUpper();
        //////            for (int b = 1; b < a.Length; b++)
        //////                a1 += a[b].ToString().ToLower();
        //////            txtNom.Text = a1;
        //////        }
        //////        catch (EvaluateException e2)
        //////        {

        //////        }
        //////        try
        //////        {
        //////            char[] c = txtPrenom.Text.ToCharArray();
        //////            string c1 = c[0].ToString().ToUpper();
        //////            for (int b = 1; b < c.Length; b++)
        //////                c1 += c[b].ToString().ToLower();
        //////            txtPrenom.Text = c1;
        //////        }
        //////        catch (EvaluateException e3)
        //////        {

        //////        }
        //////        try
        //////        {
        //////            char[] d = txtetude.Text.ToCharArray();
        //////            string d1 = d[0].ToString().ToUpper();
        //////            for (int b = 1; b < d.Length; b++)
        //////                d1 += d[b].ToString().ToLower();
        //////               txtetude.Text = d1;
        //////        }
        //////        catch (EvaluateException e4)
        //////        {

        //////        }

        //////        try
        //////        {
        //////            char[] f = txtrue.Text.ToCharArray();
        //////            string f1 = f[0].ToString().ToUpper();
        //////            for (int b = 1; b < f.Length; b++)
        //////                f1 += f[b].ToString().ToLower();
        //////                txtrue.Text = f1;
        //////        }
        //////        catch (EvaluateException e5)
        //////        {
                
        //////        }
        //////        try
        //////        {
        //////            char[] g = txtville.Text.ToCharArray();
        //////            string g1 = g[0].ToString().ToUpper();
        //////            for (int b = 1; b < g.Length; b++)
        //////                g1 += g[b].ToString().ToLower();
        //////                txtville.Text = g1;
        //////        }
        //////        catch (EvaluateException ex)
        //////        {

        //////        }

        //////    }
        //////    catch (EvaluateException e)
        //////    {

        //////    }

        //////}

        /// <summary>
        /// Methode sauvegarder
        /// </summary>
        //void methodesauvegarder()
        //{
        //    string sSql = "";
        //    lblMessage.Text = string.Empty;
        //    try
        //    {
        //        ToutbagayOk();
        //        string mydate;
        //        mydate = DateTime.Now.ToString("MM.dd.yyyy");
        //        SqlParameter prenomparam = new SqlParameter("@Prenom", DbType.String.ToString());
        //        prenomparam.Value = txtPrenom.Text.ToString();

        //        SqlParameter nomparam = new SqlParameter("@Nom", DbType.String.ToString());
        //        nomparam.Value = txtNom.Text.ToString();

        //        SqlParameter telephoneparam = new SqlParameter("@Telephone1", DbType.String.ToString());
        //        telephoneparam.Value = txttelephone.Text.ToString();

        //        SqlParameter telephone2param = new SqlParameter("@Telephone2", DbType.String.ToString());
        //        telephone2param.Value = Txturgence.Text.ToString();

        //        SqlParameter ddnparam = new SqlParameter("@DDN", DbType.String.ToString());
        //        ddnparam.Value = txtdate.Text.ToString();

        //        SqlParameter Adresserueparam = new SqlParameter("@AdresseRue", DbType.String.ToString());
        //        Adresserueparam.Value = txtrue.Text.ToString();

        //        SqlParameter numeromaisonparam = new SqlParameter("@AdresseExtra", DbType.String.ToString());
        //        numeromaisonparam.Value = txtnumeromaison.Text.ToString();

        //        SqlParameter Villeparam = new SqlParameter("@Ville", DbType.String.ToString());
        //        Villeparam.Value = txtville.Text.ToString();

        //        SqlParameter datecreparam = new SqlParameter("@DateCreee", DbType.Date);
        //        datecreparam.Value = mydate;

        //        SqlParameter Remarqueparam = new SqlParameter("@Remarque", DbType.String.ToString());
        //        Remarqueparam.Value = txtRemarque.InnerText.ToString();

        //        SqlParameter etudiantparam = new SqlParameter("@Etudiant", DbType.Int32);
        //        if (rdbStu.Checked == true)
        //        {
        //            etudiantparam.Value = 1;
        //        }
        //        else
        //        {
        //            etudiantparam.Value = 0;
        //        }


        //        SqlParameter profparam = new SqlParameter("@Professeur", DbType.Int32);
        //        if (rdbProf.Checked == true)
        //        {
        //            profparam.Value = 1;
        //        }
        //        else
        //        {
        //            profparam.Value = 0;
        //        }

        //        SqlParameter AdminStafparam = new SqlParameter("@AdminStaff", DbType.Int32);
        //        if (chbadStaff.Checked == true)
        //        {
        //            AdminStafparam.Value = 1;
        //        }
        //        else
        //        {
        //            AdminStafparam.Value = 0;
        //        }

        //        SqlParameter paramCreeParUsername = new SqlParameter("@CreeParUsername", SqlDbType.VarChar);
        //        paramCreeParUsername.Value = BaseDeDonnees.GetWindowsUser().ToString();

        //        SqlParameter recuparam = new SqlParameter("@NumeroRecu", DbType.String.ToString());
        //        recuparam.Value = txtRecu.Text.ToString();

        //        SqlParameter emailparam = new SqlParameter("@Email", DbType.String.ToString());
        //        emailparam.Value = txtemail.Text.ToString();

        //        SqlParameter Sexeparam = new SqlParameter("@Sexe", DbType.String.ToString());
        //        Sexeparam.Value = DrpSexe.SelectedItem.Text.ToString();

        //        SqlParameter niveauetudeparam = new SqlParameter("@NiveauEtude", DbType.String.ToString());
        //        niveauetudeparam.Value = txtNivEtude.Text.ToString();

        //        SqlParameter departementparam = new SqlParameter("@Departement", DbType.String.ToString());
        //        departementparam.Value = DropDownDepartement.SelectedItem.Text.ToString();

        //        //Photo param will be there
   
        //        //SqlParameter UserIdparam = new SqlParameter("@UserNameAttribue", DbType.String.ToString());
        //        //UserIdparam.Value = BaseDeDonnees.GetWindowsUser();
              
        //        // Current User
              
        //        if (txtNom.Enabled == true && txtPrenom.Enabled ==true)
        //        {
        //            // methodesauvegarder();
        //            sSql = "INSERT INTO Personnes (Prenom,Nom,Telephone1,Telephone2,DDN,AdresseRue,AdresseExtra,Ville,DateCreee,Remarque,Etudiant,Professeur,AdminStaff,CreeParUsername,NumeroRecu,Email,Sexe,NiveauEtude,Departement) VALUES (" +
        //               "@Prenom,@Nom,@Telephone1,@Telephone2,@DDN,@AdresseRue,@AdresseExtra,@Ville,@DateCreee,@Remarque,@Etudiant,@Professeur,@AdminStaff,@CreeParUsername,@NumeroRecu,@Email,@Sexe,@NiveauEtude,@Departement)";

        //            bool bResult = donnees.IssueCommandWithParams(sSql,prenomparam, nomparam,telephoneparam, telephone2param, ddnparam, numeromaisonparam,Adresserueparam,Villeparam, datecreparam, Remarqueparam,
        //                                                                etudiantparam, profparam,AdminStafparam,paramCreeParUsername,recuparam, emailparam, Sexeparam, niveauetudeparam, departementparam);

        //            //int iIdentity = DB_Access.GetScalarWithParams(sSql, nomparam, prenomparam, ddnparam,
        //            //niveauetudeparam, telephoneparam, emailparam, numeromaisonparam, Villeparam, datecreparam, Adresserueparam, UserIdparam, etudiantparam, profparam, AdminStafparam, Remarqueparam);
        //            if (bResult == true && rdbStu.Checked == true)
        //            {
        //               // sPersonneID = bResult.ToString();
        //                //var Id1 = string.Empty;
        //                Session["@PersonneID"] = bResult;

        //                WriteErrorMessageToLabel(string.Format("'{0} {1}' est ajouté dans le système !", txtPrenom.Text, txtNom.Text),true);
                      
        //                ScriptManager.RegisterStartupScript(this, typeof(Page), "myscript", "setTimeout(function(){location.href='AjouterEtudiantDansClasse.aspx';},1000);", true);

                        

        //                Nettoyer();
                        

        //            }
        //           else if (bResult == true && rdbProf.Checked == true)
        //            {
        //                WriteErrorMessageToLabel(string.Format("'{0} {1}' est ajouté dans le système !", txtPrenom.Text, txtNom.Text),true);
        //                Nettoyer();

        //            }
        //            else
        //            {
        //                WriteErrorMessageToLabel(string.Format("ERROR Adding User '{0} {1}'!", txtPrenom.Text, txtNom.Text),false);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteErrorMessageToLabel("ERROR: " + ex.Message + string.Format("--Specific Error: ('{0} {1}') Dossier Non Modifié!", txtPrenom.Text, txtNom.Text),false);
        //    }

        //}
       

        //Editez informtion sur un etudiant
        protected void Edit()
        {
           
            string mydate;
            //string sSql = "";
            //string sStudentID = "";

            lblMessage.Text = string.Empty;

            sPersonneID = txtPersonneID.Text;
            try
            {
                string sSql = String.Format("UPDATE Personnes SET Prenom=@Prenom,Nom=@Nom, Telephone1=@Telephone1, Telephone2=@Telephone2,DDN=@DDN,AdresseRue=@AdresseRue,AdresseExtra=@AdresseExtra, Ville=@Ville, DateCreee=@DateCreee, Remarque=@Remarque," +
                    "Etudiant=@Etudiant,Professeur=@Professeur, AdminStaff=@AdminStaff,CreeParUsername=@CreeParUsername,NumeroRecu=@NumeroRecu, Email=@Email, Sexe=@Sexe, NiveauEtude=@NiveauEtude, Departement=@Departement WHERE PersonneID ={0}", sPersonneID);

               
                mydate = DateTime.Now.ToString("MM.dd.yyyy");

                SqlParameter prenomparam = new SqlParameter("@Prenom", DbType.String.ToString());
                prenomparam.Value = txtPrenom.Text.ToString();

                SqlParameter nomparam = new SqlParameter("@Nom", DbType.String.ToString());
                nomparam.Value = txtNom.Text.ToString();

                SqlParameter telephoneparam = new SqlParameter("@Telephone1", DbType.String.ToString());
                telephoneparam.Value = txttelephone.Text.ToString();

                SqlParameter telephone2param = new SqlParameter("@Telephone2", DbType.String.ToString());
                telephone2param.Value = Txturgence.Text.ToString();

                SqlParameter ddnparam = new SqlParameter("@DDN", DbType.String.ToString());
                ddnparam.Value = txtdate.Text.ToString();

                SqlParameter Adresserueparam = new SqlParameter("@AdresseRue", DbType.String.ToString());
                Adresserueparam.Value = txtrue.Text.ToString();

                SqlParameter numeromaisonparam = new SqlParameter("@AdresseExtra", DbType.String.ToString());
                numeromaisonparam.Value = txtnumeromaison.Text.ToString();

                SqlParameter Villeparam = new SqlParameter("@Ville", DbType.String.ToString());
                Villeparam.Value = txtville.Text.ToString();

                SqlParameter datecreparam = new SqlParameter("@DateCreee", DbType.Date);
                datecreparam.Value = mydate;

                SqlParameter Remarqueparam = new SqlParameter("@Remarque", DbType.String.ToString());
                Remarqueparam.Value = txtRemarque.InnerText.ToString();

                SqlParameter etudiantparam = new SqlParameter("@Etudiant", DbType.Int32);
                if (rdbStu.Checked == true)
                {
                    etudiantparam.Value = 1;
                }
                else
                {
                    etudiantparam.Value = 0;
                }


                SqlParameter profparam = new SqlParameter("@Professeur", DbType.Int32);
                if (rdbProf.Checked == true)
                {
                    profparam.Value = 1;
                }
                else
                {
                    profparam.Value = 0;
                }

                SqlParameter AdminStafparam = new SqlParameter("@AdminStaff", DbType.Int32);
                if (chbadStaff.Checked == true)
                {
                    AdminStafparam.Value = 1;
                }
                else
                {
                    AdminStafparam.Value = 0;
                }

                SqlParameter paramCreeParUsername = new SqlParameter("@CreeParUsername", SqlDbType.VarChar);
                paramCreeParUsername.Value = BaseDeDonnees.GetWindowsUser().ToString();

                SqlParameter recuparam = new SqlParameter("@NumeroRecu", DbType.String.ToString());
                recuparam.Value = txtRecu.Text.ToString();

                SqlParameter emailparam = new SqlParameter("@Email", DbType.String.ToString());
                emailparam.Value = txtemail.Text.ToString();

                SqlParameter Sexeparam = new SqlParameter("@Sexe", DbType.String.ToString());
                Sexeparam.Value = DrpSexe.SelectedItem.Text.ToString();

                SqlParameter niveauetudeparam = new SqlParameter("@NiveauEtude", DbType.String);
                niveauetudeparam.Value = txtNivEtude.Text.ToString();

                SqlParameter departementparam = new SqlParameter("@Departement", DbType.String.ToString());
                departementparam.Value = DropDownDepartement.SelectedItem.Text.ToString();

                bool bResult = donnees.IssueCommandWithParams(sSql, prenomparam, nomparam, telephoneparam, telephone2param, ddnparam, numeromaisonparam, Adresserueparam, Villeparam, datecreparam, Remarqueparam,
                                                                       etudiantparam, profparam, AdminStafparam, paramCreeParUsername, recuparam, emailparam, Sexeparam, niveauetudeparam, departementparam);

                //int iIdentity = DB_Access.GetScalarWithParams(sSql, nomparam, prenomparam, ddnparam,
                //niveauetudeparam, telephoneparam, emailparam, numeromaisonparam, Villeparam, datecreparam, Adresserueparam, UserIdparam, etudiantparam, profparam, AdminStafparam, Remarqueparam);
                if (bResult == true)
                {
                    // sPersonneID = bResult.ToString();
                    //var Id1 = string.Empty;
                    Session["@PersonneID"] = bResult;

                    WriteErrorMessageToLabel(string.Format("'{0} {1}' est mis à jour dans le système !", txtPrenom.Text, txtNom.Text),true);

                    ScriptManager.RegisterStartupScript(this, typeof(Page), "myscript", "setTimeout(function(){location.href='AllStudentList.aspx';},1000);", true);

                    Nettoyer();

                }
                else
                {
                    WriteErrorMessageToLabel(string.Format("ERROR Updating User '{0} {1}'!", txtPrenom.Text, txtNom.Text),false);
                }           
            }
            catch (Exception ex)
            {
                WriteErrorMessageToLabel("ERROR: " + ex.Message + string.Format("--Specific Error: ('{0} {1}') Dossier Non Modifié!", txtPrenom.Text, txtNom.Text),false);
            }
        }

        protected void chbSatff_CheckedChanged(object sender, EventArgs e)
        {
            if (chbadStaff.Checked == true)
            {
                addstaf = 1;
            }
            else if (chbadStaff.Checked == false)
            {
                addstaf = 0;
            }
        }

        protected void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbProf.Checked == true)
            {
                prof = 1;
                if (chbadStaff.Checked == true)
                {
                    prof = 1;
                    addstaf = 1;
                }
            }
        }
    }
}