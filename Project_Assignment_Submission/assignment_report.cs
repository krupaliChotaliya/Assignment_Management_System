using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Assignment_Submission
{
    public partial class assignment_report : UserControl
    {
        SqlConnection con;
        SqlCommand cmd;
       
        SqlDataAdapter da;
        DataTable dt;
        String query;
        public assignment_report()
        {
            con = new SqlConnection(@"Data Source=DESKTOP-1R31CPH\SQLEXPRESS;Initial Catalog=ams;Integrated Security=True");
            InitializeComponent();
            
            query = "select subjectname from subject";
            da = new SqlDataAdapter(query, con);
            dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                combosubject.Items.Add(dr["subjectname"].ToString());

            }

            Submitted.Checked= true;

            if (combosubject.Items.Count > 0)
            {
                combosubject.SelectedIndex = 0;

               String a= combosubject.SelectedItem.ToString();

            }


            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            display();
          


        }

       

        public void display()
        {

            dataGridView1.DataSource = null;
            

            if (pending.Checked)
                query = "select st.subjectName,st.title,st.semester,st.due_date,st.studentId,student.firstname,student.lastname," +
                    "st.status from student inner join " +
                    "((select assignment.subjectName,assignment.title,assignment.semester,assignment.due_date," +
                    "report.studentId,report.status from report inner join (Select * from assignment where subjectName= '" +
                    combosubject.SelectedItem.ToString() +
                    "') as assignment on report.assignmentId = assignment.assignmentId where report.status = 'pending')) as st on student.id = st.studentId";
            else
                query = "select st.subjectName,st.title,st.semester,st.due_date,st.studentId,student.firstname,student.lastname," +
                    "st.status from student inner join " +
                    "((select assignment.subjectName,assignment.title,assignment.semester,assignment.due_date," +
                    "report.studentId,report.status from report inner join (Select * from assignment where subjectName= '" +
                    combosubject.SelectedItem.ToString() +
                    "') as assignment on report.assignmentId = assignment.assignmentId where report.status = 'submitted')) as st on student.id = st.studentId";
            
            da = new SqlDataAdapter(query, con);
            dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;

            }




        }

        private void btnclose_Click(object sender, EventArgs e)
        {
          
         
        }

        private void btnclose_Click_1(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void Submitted_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void combosubject_SelectedValueChanged(object sender, EventArgs e)
        {
            display();
        }

        private void pending_Click(object sender, EventArgs e)
        {
            display();
        }

        private void Submitted_Click(object sender, EventArgs e)
        {
            display();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
