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
    public partial class JourDeClasse : System.Web.UI.Page
    {
        private readonly BaseDeDonnees donnees = new BaseDeDonnees();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMessage.Text = "";
            }
        }

        protected void btnAddJour_Click(object sender, EventArgs e)
        {
            if (txtJour.Text == String.Empty)
            {

                WriteErrorMessageToLabel("Le champ Jour est obligatoire !", false);
            }
            else
            {

                try
                {
                    String sql = "Insert into JoursDeClasses(JourDescription,Remarque) values(@JourDescription,@Remarque)";

                    SqlParameter jDescParam = new SqlParameter("@JourDescription", DbType.String.ToString());
                    jDescParam.Value = txtJour.Text.Trim();

                    SqlParameter reParam = new SqlParameter("@Remarque", DbType.String.ToString());
                    reParam.Value = txtRemarque.Text.Trim();

                    bool bRes = donnees.IssueCommandWithParams(sql, jDescParam, reParam);
                    if (bRes == true)
                    {
                        txtJour.Text = "";
                        txtRemarque.Text = "";
                        WriteErrorMessageToLabel("Succès: Jour ajouté avec succès!!!", true);
                    }
                    else
                    {
                        WriteErrorMessageToLabel("Le jour n'a pas été ajouté ", false);
                    }
                }
                catch (Exception ex)
                {
                    WriteErrorMessageToLabel("Error de Connection voir un technicien " + ex.Message, false);
                }
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtJour.Text = "";
            txtRemarque.Text = "";
        }
    }
}