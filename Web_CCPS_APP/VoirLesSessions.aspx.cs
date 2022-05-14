using CCPS_Web_Edu_Update;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_CCPS_APP
{
    public partial class VoirLesSessions : System.Web.UI.Page
    {
        BaseDeDonnees donnees = new BaseDeDonnees();
        private SqlConnection conn = new SqlConnection("Data Source= DESKTOP-GBQB9HI\\MSSQLSERVER1; Integrated Security=true;Initial Catalog= CCPAP_Web_Edu; uid=''; Password=''; ");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGridview();
            }
        }

        protected void BindGridview()
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT SessionDateID, SessionDateDebut, SessionDateFin, Remarque, Actif FROM DatesSessionCourante", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
               
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvDetails.DataSource = ds;
                    gvDetails.DataBind();
                }
                else
                {
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                    gvDetails.DataSource = ds;
                    gvDetails.DataBind();
                    int columncount = gvDetails.Rows[0].Cells.Count;
                    gvDetails.Rows[0].Cells.Clear();
                    gvDetails.Rows[0].Cells.Add(new TableCell());
                    gvDetails.Rows[0].Cells[0].ColumnSpan = columncount;
                    gvDetails.Rows[0].Cells[0].Text = "No Records Found";
                }
                conn.Close();
                //lblPagePos.Text = "You are in page " + (GridView1.PageIndex + 1).ToString() + " of " + GridView1.PageCount;
            }
            catch (Exception ex)
            {
                WriteErrorMessageToLabel("Error...!!! Check your connection " + ex.Message,false);
            }
        }
        protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //Get back write the empty input error
            if (e.CommandName.Equals("AddNew"))
            {
                TextBox txtname = (TextBox)gvDetails.FooterRow.FindControl("txtDateDebut");
                TextBox txtprice = (TextBox)gvDetails.FooterRow.FindControl("txtDateFin");
                TextBox txtRemarque = (TextBox)gvDetails.FooterRow.FindControl("txtRemarque");
                TextBox txtActif = (TextBox)gvDetails.FooterRow.FindControl("txtActif");

                SqlParameter dateDebut = new SqlParameter("@SessionDateDebut", DbType.Date.ToString());
                dateDebut.Value = txtname.Text.ToString();

                SqlParameter dateFin = new SqlParameter("@SessionDateFin", DbType.Date.ToString());
                dateFin.Value = txtprice.Text.ToString();

                SqlParameter remarque = new SqlParameter("@Remarque", DbType.String.ToString());
                remarque.Value = txtRemarque.Text.ToString();

                SqlParameter actif = new SqlParameter("@Actif", DbType.Int32);
                actif.Value = int.Parse(txtActif.Text.ToString());


                using (SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString()))
                {
                    try
                    {
                        sqlconn.Open();
                        //Update to set the oldSession Actif to zero
                        String sqlup = "UPDATE DatesSessionCourante set Actif = 0 Where Actif = 1";
                        bool firstUpdate = donnees.IssueCommand(sqlup);
                        if (firstUpdate == true)
                        {
                            String sSql = String.Format("INSERT INTO DatesSessionCourante(SessionDateDebut,SessionDateFin,Remarque,Actif) VALUES(" +
                            "@SessionDateDebut,@SessionDateFin,@Remarque,@Actif)");

                            bool bResult = donnees.IssueCommandWithParams(sSql, dateDebut, dateFin, remarque, actif);
                            if (bResult == true)
                            { 
                                WriteErrorMessageToLabel("Succès une nouvelle ligne est ajouter de votre base de donnée !!!", true);
                                gvDetails.EditIndex = -1;
                                BindGridview();
                                donnees.IssueCommand("UPDATE Sessions set Actif = 0 Where Actif = 1");
                            }
                            else
                            {
                                WriteErrorMessageToLabel("Error....Donnée n'est pas ajouter dans le base de donnée", false);
                            }
                        }
                    }catch(Exception ex)
                    {
                        WriteErrorMessageToLabel("Error check connection ou voir un technicien....!!! " + ex.Message,false);
                    }
                }
               // crudoperations("INSERT", txtname.Text, txtprice.Text, 0);
            }
        }
        protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDetails.EditIndex = e.NewEditIndex;
            BindGridview();
        }
        protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDetails.EditIndex = -1;
            BindGridview();
        }
        protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDetails.PageIndex = e.NewPageIndex;
            BindGridview();
        }
        protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int SessionDateID = Convert.ToInt32(gvDetails.DataKeys[e.RowIndex].Values["SessionDateID"].ToString());
                TextBox txtDateDebut = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtDateDebut");
                TextBox txtDateFin = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtDateFin");
                TextBox txtRemarque = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtRemarque");
                TextBox txtActif = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtActif");

                SqlParameter dateDebut = new SqlParameter("@SessionDateDebut", DbType.Date.ToString());
                dateDebut.Value = txtDateDebut.Text.ToString();

                SqlParameter dateFin = new SqlParameter("@SessionDateFin", DbType.Date.ToString());
                dateFin.Value = txtDateFin.Text.ToString();

                SqlParameter remarque = new SqlParameter("@Remarque", DbType.String.ToString());
                remarque.Value = txtRemarque.Text.ToString();

                SqlParameter actif = new SqlParameter("@Actif", DbType.Int32);
                actif.Value = int.Parse(txtActif.Text.ToString());


                using (SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString()))
                {

                    sqlconn.Open();
                    String sqlup = "Update DatesSessionCourante set Actif =0 WHERE Actif = 1";
                    bool firstUpdate = donnees.IssueCommand(sqlup);
                    if (firstUpdate == true)
                    {
                        String sSql = String.Format("UPDATE DatesSessionCourante SET SessionDateDebut=@SessionDateDebut, SessionDateFin=@SessionDateFin," +
                                                     "Remarque=@Remarque,Actif=@Actif WHERE SessionDateID ={0}", SessionDateID);

                        bool bResult = donnees.IssueCommandWithParams(sSql, dateDebut, dateFin, remarque, actif);
                        if (bResult == true)
                        {
                            String sSqSecond = "UPDATE Sessions SET DateCommence=@SessionDateDebut, DateFin=@SessionDateFin WHERE Actif = 1";
                            bool secondUpdate = donnees.IssueCommandWithParams(sSqSecond, dateDebut, dateFin);
                            if (secondUpdate == true)
                            {
                                WriteErrorMessageToLabel("Succès: Donnée est mise à jour !!!", true);
                                gvDetails.EditIndex = -1;
                                BindGridview();
                            }
                            else
                            {
                                WriteErrorMessageToLabel("ERROR: Donnée n'est pas mise à jour !!!", false);
                            }
                        }
                        else
                        {
                            WriteErrorMessageToLabel("ERROR: Donnée n'est pas mise à jour !!!", false);
                        }
                    }
                    else
                    {
                        WriteErrorMessageToLabel("ERROR: Donnée n'est pas mise à jour !!!", false);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteErrorMessageToLabel("Error check connection ou voir un technicien....!!! " + ex.Message, false);
            }
        }
        protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int SessionDateID = Convert.ToInt32(gvDetails.DataKeys[e.RowIndex].Values["SessionDateID"].ToString());
           
            using (SqlConnection sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString()))
            {
                try
                {
                    sqlconn.Open();
                    String sSql = String.Format("DELETE FROM DatesSessionCourante WHERE SessionDateID ={0}", SessionDateID);

                    bool bResult = donnees.IssueCommand(sSql);
                    if (bResult == true)
                    {
                        WriteErrorMessageToLabel("Donnée delete avec Succès....!!! ",true);
                        gvDetails.EditIndex = -1;
                        BindGridview();
                    }
                    else
                    {
                        WriteErrorMessageToLabel("Error....!!! ",false);
                    }
                }
                catch (Exception ex)
                {
                    WriteErrorMessageToLabel("Error check connection ou voir un technicien....!!! " + ex.Message, false);
                }
            }
        }
        protected void crudoperations(string status, string productname, string price, int productid)
        {
            using (SqlConnection con = new SqlConnection("Data Source=Suresh;Integrated Security=true;Initial Catalog=MySampleDB"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("crudoperations", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (status == "INSERT")
                {
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@productname", productname);
                    cmd.Parameters.AddWithValue("@price", price);
                }
                else if (status == "UPDATE")
                {
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@productname", productname);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@productid", productid);
                }
                else if (status == "DELETE")
                {
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@productid", productid);
                }
                cmd.ExecuteNonQuery();
                // lblresult.ForeColor = Color.Green;
                WriteErrorMessageToLabel(productname + " details " + status.ToLower() + "d successfully",true);
                gvDetails.EditIndex = -1;
                BindGridview();
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