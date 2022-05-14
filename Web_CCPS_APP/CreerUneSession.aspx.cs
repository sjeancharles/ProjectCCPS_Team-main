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
    public partial class CreerUneSession : System.Web.UI.Page
    {
        private readonly BaseDeDonnees donnees = new BaseDeDonnees();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMessage.Text = "";
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {

            DateDebut.Text = "";
            DateFin.Text = "";
            SessionRemarque.InnerText = "";
            lblMessage.Text = "";
            OldDateDebut.Text = "";
            OldDateFin.Text = "";
            CheckBox1.Checked = false;

        }

        protected void btnAddSession_Click(object sender, EventArgs e)
        {
            String sSql = String.Empty;
            String sID = String.Empty;
            lblMessage.Text = "";
            if (DateDebut.Text == String.Empty)
            {
                WriteErrorMessageToLabel("Le champ Date debut du session est obligatoire !", false);
            }

            else if (DateFin.Text == String.Empty)
            {
                WriteErrorMessageToLabel("Le champ Date fin du session est obligatoire !", false);
            }
            else
            {

                if (CheckBox1.Checked == false)
                {
                    try
                    {
                        String sqlup = "Update DatesSessionCourante set Actif =0 WHERE Actif = 1";
                        donnees.IssueCommand(sqlup);
                        String sql = "Insert into DatesSessionCourante (SessionDateDebut,SessionDateFin,Remarque) values(@SessionDateDebut,@SessionDateFin,@Remarque)";
                        SqlParameter dateDebut = new SqlParameter("@SessionDateDebut", DbType.String.ToString());
                        dateDebut.Value = DateDebut.Text;
                        SqlParameter dateFin = new SqlParameter("@SessionDateFin", DbType.String.ToString());
                        dateFin.Value = DateFin.Text;
                        SqlParameter remarque = new SqlParameter("@Remarque", DbType.String.ToString());
                        remarque.Value = SessionRemarque.InnerText;
                        bool bResult;//= donnees.IssueCommandWithParams(sql, dateDebut, dateFin, remarque);
                        if (bResult = donnees.IssueCommandWithParams(sql, dateDebut, dateFin, remarque))
                        {
                            // Update Table Sessions pour réfléter le changement
                           // sSql = "UPDATE Sessions SET Actif = 0 WHERE DateCommence IN (SELECT SessionDateDebut FROM DatesSessionCourante WHERE SessionDateID = @SessionDateID) AND DateFin IN (SELECT SessionDateFin FROM DatesSessionCourante WHERE SessionDateID = @SessionDateID)";
                            
                            sSql = "UPDATE Sessions SET Actif = 0 WHERE Actif =1";
                            if (donnees.IssueCommand(sSql))
                            {
                                WriteErrorMessageToLabel("Données enregistré avec succès", true);
                            }
                            else
                            {
                                WriteErrorMessageToLabel("ERREUR Les Classes sont toujours actives !!", false);
                            }


                            CheckBox1.Checked = false;
                            DateDebut.Text = ("");
                            DateFin.Text = ("");
                            SessionRemarque.InnerText = ("");
                        }
                    }
                    catch (Exception ex)
                    {
                        WriteErrorMessageToLabel("ERROR: Erreur de connection voir un technicien " + ex, false);
                    }
                }

                else if (CheckBox1.Checked == true)
                {
                    lblMessage.Text = "";
                    try
                    {
                        String sql = "Update DatesSessionCourante set SessionDateDebut =@SessionDateDebut,SessionDateFin=@SessionDateFin,Remarque=@Remarque WHERE Actif = 1";
                        SqlParameter dateDebut = new SqlParameter("@SessionDateDebut", DbType.String.ToString());
                        dateDebut.Value = DateDebut.Text;
                        SqlParameter dateFin = new SqlParameter("@SessionDateFin", DbType.String.ToString());
                        dateFin.Value = DateFin.Text;
                        SqlParameter remarque = new SqlParameter("@Remarque", DbType.String.ToString());
                        remarque.Value = SessionRemarque.InnerText;
                        bool bResult;//= donnees.IssueCommandWithParams(sql, dateDebut, dateFin, remarque);
                        if (bResult = donnees.IssueCommandWithParams(sql, dateDebut, dateFin, remarque))
                        {
                            String sSqSecond = "UPDATE Sessions SET DateCommence=@SessionDateDebut, DateFin=@SessionDateFin WHERE Actif = 1";
                            bool secondUpdate = donnees.IssueCommandWithParams(sSqSecond, dateDebut, dateFin);
                            if(secondUpdate == true)
                            {
                                WriteErrorMessageToLabel("Update réussir avec succès !!!", true);
                                CheckBox1.Checked = false;
                                DateDebut.Text = ("");
                                DateFin.Text = ("");
                                SessionRemarque.InnerText = ("");
                                debuSessionCourante.Text = "";
                                FinSessionCourante.Text = "";
                                OldDateDebut.Text = "";
                                OldDateFin.Text = "";
                            }
                            else
                            {
                                WriteErrorMessageToLabel("ERREUR Update est éjoué ", false);
                            }                           
                        }
                        else
                        {
                            WriteErrorMessageToLabel("ERREUR Update est éjoué ", false);
                        }
                    }
                    catch(Exception ex)
                    {
                        WriteErrorMessageToLabel("ERREUR de connection voir un technicien!! " + ex, false);
                    }
                }
                else
                {
                    Response.Write("Les dates sont obligatoires");
                }
            }
        }

        private void SelectDate()
        {
            try
            {
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
                    string[] sTemp = dr["SessionDate"].ToString().Split('-');
                    OldDateDebut.Text = "<hr/><strong> Date Debut Du Session Courante :<br/>    " + sTemp[0].Trim() + "</strong><hr/>";
                    OldDateFin.Text = "<strong> Date Fin Du Session Courante : <br>   " + sTemp[1].Trim() + "</strong><hr/>";
                }
            }catch(Exception ex)
            {
                WriteErrorMessageToLabel("ERROR: Check la connection du base de données ou voir un technicien , Assurez-vous que votre sql services est bien start dans votre pc/server. \n " + ex.Message, false);
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

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked == true)
            {
                SelectDate();
            }
            else
            {
                if (CheckBox1.Checked == false)
                {
                    OldDateDebut.Text = "";
                    OldDateFin.Text = "";

                }
            }
           
        }
    }
}