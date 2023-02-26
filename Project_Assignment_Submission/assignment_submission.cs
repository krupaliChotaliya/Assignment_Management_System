using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Project_Assignment_Submission
{
    public partial class assignment_submission : UserControl
    {
        SqlConnection con;
        SqlCommand cmd;
       
        SqlDataAdapter da;
        DataTable dt;
        String query;
        public assignment_submission()
        {
            InitializeComponent();
            con = new SqlConnection(@"Data Source=DESKTOP-1R31CPH\SQLEXPRESS;Initial Catalog=ams;Integrated Security=True");
            txtsubmitiondate.Text = System.DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtboxsem.Enabled = false;

        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

       

        private void btnsubmit_Click(object sender, EventArgs e)
        {
           
            try
            {
                con.Open();
                string assignmentQuery = "select assignmentId from assignment where title = '" + combotitle.SelectedItem.ToString() + "'";

                cmd = new SqlCommand(assignmentQuery, con);
                string assignmentId = cmd.ExecuteScalar().ToString();

                string query = "update report set status='submitted' where studentId= '" + int.Parse(txtboxstudId.Text) + "' AND  assignmentId= '" + int.Parse(assignmentId) + "'";
                cmd = new SqlCommand(query, con);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Submitted Sucessfully!!");

                }
                con.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

           
          
        }
      

    

        private void txtboxstudId_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtboxstudId.Text != "")
                {

                    String name1, name2;
                    string sem;

                    String stud_id = txtboxstudId.Text;
                    int id = int.Parse(stud_id);

                    con.Open();
                    query = "select firstname from student where id='" + id + "'";
                    cmd = new SqlCommand(query, con);
                    name1 = (string)cmd.ExecuteScalar();
                    con.Close();

                    con.Open();
                    query = "select lastname from student where id='" + id + "'";
                    cmd = new SqlCommand(query, con);
                    name2 = (string)cmd.ExecuteScalar();
                    con.Close();

                    con.Open();
                    query = "select currentSemester from student where id='" + id + "'";
                    cmd = new SqlCommand(query, con);
                    sem = cmd.ExecuteScalar().ToString();
                    con.Close();

                    txtboxstudname.Text = name1 + " " + name2;
                    txtboxsem.Text = sem;


                    if (txtboxsem.Text != "")
                    {

                        combosubname.Items.Clear();
                        combosubname.ResetText();

                        combotitle.Items.Clear();
                        combotitle.ResetText();


                        con.Open();
                        query = "select subjectname from subject where semester='" + int.Parse(sem) + "'";
                        cmd = new SqlCommand(query, con);
                        da = new SqlDataAdapter(query, con);
                        dt = new DataTable();
                        da.Fill(dt);
                        foreach (DataRow dr in dt.Rows)
                        {
                            combosubname.Items.Add(dr["subjectname"].ToString());

                        }

                        if (combosubname.Items.Count > 0)
                        {
                            combosubname.SelectedIndex = 0;
                        }
                        con.Close();
                    }

                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

      
        private void displayAssignment()
        {
            try
            {
                String subjectname = combosubname.GetItemText(combosubname.SelectedItem);
                query = "select title from assignment where subjectName ='" + subjectname + "'";
                cmd = new SqlCommand(query, con);
                da = new SqlDataAdapter(query, con);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    combotitle.Items.Add(dr["title"].ToString());

                }


                if (combotitle.Items.Count > 0)
                {
                    combotitle.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
           

        }

        private void combosubname_SelectedValueChanged(object sender, EventArgs e)
        {
            combotitle.Items.Clear();
            combotitle.ResetText();

            displayAssignment();
        }

      
    }
}
