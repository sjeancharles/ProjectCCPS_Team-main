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
    public partial class SalleDeClasse : System.Web.UI.Page
    {
        BaseDeDonnees donnees = new BaseDeDonnees();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                lblMessage.Text = "";
            }
        }

        protected void btnSalleName_Click(object sender, EventArgs e)
        {
            if (txtSallDeClasse.Text == String.Empty)
            {

                WriteErrorMessageToLabel("Le champ Nom de la salle est obligatoire !", false);
            }
            else
            {
                try
                {

                    SqlParameter nParam = new SqlParameter("@NomDuSalle", DbType.String.ToString());
                    nParam.Value = txtSallDeClasse.Text.Trim();
                    SqlParameter NbrsParam = new SqlParameter("@NombreDePersonne", DbType.Int32);
                    NbrsParam.Value = txtNbres.Text.Trim();
                    SqlParameter dParam = new SqlParameter("@SalleDescription", DbType.String.ToString());
                    dParam.Value = txtDescriptionSalle.Text.Trim();

                    String sqlAnn = "Insert into SalleDeClasses(NomDuSalle,NombreDePersonne,SalleDescription) values(@NomDuSalle,@NombreDePersonne,@SalleDescription)";

                    bool bRes;

                    if (bRes = donnees.IssueCommandWithParams(sqlAnn, nParam, NbrsParam, dParam))
                    {
                        WriteErrorMessageToLabel("Succès!!! ", true);

                        txtSallDeClasse.Text = "";
                        txtNbres.Text = "";
                        txtDescriptionSalle.Text = "";
                    }
                    else
                    {
                        WriteErrorMessageToLabel("Error: Info n'est pas enregistrer", false);
                    }
                }
                catch (Exception ex)
                {
                    WriteErrorMessageToLabel("Error de Connection voir un technicien " + ex.Message, false);
                }
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtSallDeClasse.Text = "";
            txtNbres.Text = "";
            txtDescriptionSalle.Text = "";
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