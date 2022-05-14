using System.Web;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.Data;
using System;
using CCPS_Web_Edu_Update;

namespace Web_CCPS_APP
{
    public partial class AllStudentList : System.Web.UI.Page
    {
        private SqlConnection conn = new SqlConnection("Data Source= DESKTOP-GBQB9HI\\MSSQLSERVER1; Integrated Security=true;Initial Catalog= CCPAP_Web_Edu; uid=''; Password=''; ");
        String sPersonneID = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Get PersonneID 
                try
                {
                    String sPersonneID =Request.QueryString["PersonneID"];
                    txtPersonneID.Text = sPersonneID;
                    Debug.WriteLine(sPersonneID);
                   
                    try
                    {
                        // Enlever le cookie
                        Response.Cookies.Remove("PersonneID");                              // First method
                        HttpCookie cookie = new HttpCookie("PersonneID", String.Empty);     // Second remove method, in case
                        Response.Cookies.Set(cookie);
                    }
                    catch (Exception NoNeed)
                    {
                        Debug.WriteLine(NoNeed.Message);
                    }
                    //Permission
                    //String sGroup = ConfigurationManager.AppSettings["AdminGroup"].ToString();
                    BaseDeDonnees db = new BaseDeDonnees();
                    using (SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString()))
                    {
                        sqlConn.Open();
                        
                        if (sPersonneID != String.Empty)
                        {
                            // Build Strings Info
                            gvbind();
                        }
                       
                    }
                }
                catch (Exception Excep)
                {
                   
                    Debug.WriteLine(Excep.Message);
                }
            }
        }
        protected void gvbind()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT PersonneID, Nom, Prenom, DateCreee, Telephone1 FROM Personnes Where DelPersonne = 0", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
                else
                {
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    int columncount = GridView1.Rows[0].Cells.Count;
                    GridView1.Rows[0].Cells.Clear();
                    GridView1.Rows[0].Cells.Add(new TableCell());
                    GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
                    GridView1.Rows[0].Cells[0].Text = "No Records Found";
                }
                conn.Close();
                lblPagePos.Text = "You are in page " + (GridView1.PageIndex + 1).ToString() + " of " + GridView1.PageCount;

            }catch(Exception ex)
            {
                WriteErrorMessageToLabel("ERROR: Check la connection ou voir un technicien " + ex.Message, false);
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                conn.Open();
                GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
                int PersonneID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
                string sql = string.Format("Update Personnes SET DelPersonne = 1 where PersonneID = {0}", PersonneID);
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                if (cmd.ExecuteNonQuery() == 1)
                {                  
                    conn.Close();
                    gvbind();
                    WriteErrorMessageToLabel("Data deleted successfuly !!! ",true);
                }
            }
            catch (Exception exception)
            {
               WriteErrorMessageToLabel("ERROR: deleting Data !!! " + exception.Message,false);
            }
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                int PersonneID = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value.ToString());                
                Response.Redirect("Personne.aspx?EditFlag=1&PersonneID=" + PersonneID);
               
            }
            catch(Exception exception)
            {
                WriteErrorMessageToLabel(exception.Message.ToString(),false);
                Debug.WriteLine(exception.Message);
            }

        }
        //protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    try
        //    {
        //        int userid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
        //        GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
        //        Label lblID = (Label)row.FindControl("lblID");
        //        //TextBox txtname=(TextBox)gr.cell[].control[];  
        //        TextBox textName = (TextBox)row.Cells[0].Controls[0];
        //        TextBox textadd = (TextBox)row.Cells[1].Controls[0];
        //        TextBox textc = (TextBox)row.Cells[2].Controls[0];
        //        //TextBox textadd = (TextBox)row.FindControl("txtadd");  
        //        //TextBox textc = (TextBox)row.FindControl("txtc");  
        //        GridView1.EditIndex = -1;
        //        conn.Open();
        //        //SqlCommand cmd = new SqlCommand("SELECT * FROM detail", conn);  
        //        SqlCommand cmd = new SqlCommand("update detail set name='" + textName.Text + "',address='" + textadd.Text + "',country='" + textc.Text + "'where id='" + userid + "'", conn);
        //        cmd.ExecuteNonQuery();
        //        conn.Close();
        //        gvbind();
        //        //GridView1.DataBind();  
        //    }catch(Exception ex)
        //    {
        //        WriteErrorMessageToLabel("ERROR: Check la connection ou voir un technicien " + ex.Message, false);
        //    }
        //}
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            gvbind();
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            gvbind();
        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            this.gvbind();
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