using CCPS_Web_Edu_Update;
using System;
using System.Web.UI.WebControls;

namespace Web_CCPS_APP
{
    public partial class EtudiantGraduee : System.Web.UI.Page
    {
        BaseDeDonnees donne = new BaseDeDonnees();
        protected void Page_Load(object sender, EventArgs e)
        {
            String sqlDa = "SELECT Nom, Prenom, DateCreee FROM Personnes";

            gridviewId.DataSource = donne.GetDataSet(sqlDa);

            gridviewId.DataBind();
        }

        public void ChercherEtudiant()
        {      
            String sqlDa = "select DISTINCT Nom +', ' + Prenom as NomComplet, Nom, Prenom, DateCreee FROM Personnes where Nom LIKE '%' +'" + Recherche.Text + "'+ '%' OR Prenom LIKE '%'+'" + Recherche.Text + "' + '%' OR Nom+' '+ Prenom LIKE '%'+'" + Recherche.Text + "'+ '%'";

            donne = new BaseDeDonnees();
            gridviewId.DataSource = donne.GetDataSet(sqlDa);

            gridviewId.DataBind();
        }


        protected void RechercheEdu()
        {
            String sqlDa = "SELECT DISTINCT Nom, Prenom, DateCreee FROM Personnes WHERE Nom LIKE '%' + '" + Recherche.Text + "' + '%' OR Prenom LIKE '%' + '" + Recherche.Text + "' ";
            donne = new BaseDeDonnees();
            gridviewId.DataSource = donne.GetDataSet(sqlDa);

            gridviewId.DataBind();
        }
        protected void Recherche_TextChanged1(object sender, EventArgs e)
        {
            ChercherEtudiant();
        }


        protected void DisplayData()
        {
            String sqlDa = "SELECT Nom, Prenom, DateCreee FROM Personnes";            
            donne = new BaseDeDonnees();            
            gridviewId.DataSource = donne.GetDataSet(sqlDa);           
            gridviewId.DataBind();
        }
        protected void gridviewId_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridviewId.PageIndex = e.NewPageIndex;
            DisplayData();
            RechercheEdu();
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            ChercherEtudiant();
        }
    }
}