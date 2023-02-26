using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Project_Assignment_Submission
{
    public partial class add_assignment : UserControl
    {
        SqlConnection con;
        SqlCommand cmd;
       
        SqlDataAdapter da;
        DataTable dt;
        String query;
        public add_assignment()
        {
            InitializeComponent();
            con = new SqlConnection(@"Data Source=DESKTOP-1R31CPH\SQLEXPRESS;Initial Catalog=ams;Integrated Security=True");
            displaysubject();
            txtboxassigndate.Text = System.DateTime.Now.Date.ToString("dd/MM/yyyy");
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            int assignmentId=0;
          String subject=  combosub.SelectedItem.ToString();
          String semester=  comboSem.SelectedItem.ToString();
          String due_date =  dateTimePicker1.Value.ToString("dd/MM/yyyy");
           
            int sem= int.Parse(semester);
            try
            {
             
                con.Open();
                string dueDate = dateTimePicker1.Value.ToString("dd/MM/yyyy");
                string query = "insert into assignment (subjectName,semester,assign_date,due_date,title,summary) values('" + subject + "','" + sem + "','" + txtboxassigndate.Text + "','"+ due_date + "','" + txtboxtitle.Text + "','" + txtboxsummary.Text + "')";
                SqlCommand cmd = new SqlCommand(query, con);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("record added sucessfully");
                    con.Close();

                    query = "select assignmentId,semester from assignment order by assignmentId desc OFFSET 0 ROWS FETCH FIRST 1 ROWS ONLY";

                    da = new SqlDataAdapter(query,con);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if(dt.Rows.Count > 0)
                    {
                       foreach( DataRow row in dt.Rows )
                       {
                            assignmentId = int.Parse(row[0].ToString());
                            sem = int.Parse(row[1].ToString());
                            
                        }
                    }

                    
                    query = "select id from student where currentSemester='" + sem + "'";
                    da = new SqlDataAdapter(query, con);

                    dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        con.Open();
                        foreach (DataRow row in dt.Rows)
                        {
                            int Id = int.Parse(row[0].ToString());

                            query = "insert into report (assignmentId,studentId) values('" + assignmentId + "','" + Id + "')";
                            cmd= new SqlCommand(query, con);
                            cmd.ExecuteNonQuery();
                        }
                        con.Close();
                    }

                }

                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
        private void displaysubject()
        {

            String sem = comboSem.GetItemText(comboSem.SelectedItem);
           
            combosub.Items.Clear();
            combosub.ResetText();

            try
            {
                query = "select subjectname from subject where semester='" + sem + "'";
                cmd = new SqlCommand(query, con);
                da = new SqlDataAdapter(query, con);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    combosub.Items.Add(dr["subjectname"].ToString());

                }

                if (combosub.Items.Count > 0)
                {
                    combosub.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        private void comboSem_SelectedValueChanged(object sender, EventArgs e)
        {
            displaysubject();
        }
    }
}
