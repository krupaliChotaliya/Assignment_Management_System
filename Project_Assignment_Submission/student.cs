using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Project_Assignment_Submission
{
    public partial class student : UserControl
    {
        SqlConnection con;
        String id;
       
        public student()
        {
            InitializeComponent();
            con = new SqlConnection(@"Data Source=DESKTOP-1R31CPH\SQLEXPRESS;Initial Catalog=ams;Integrated Security=True");
          
            display();
        }

     

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Visible= false;
        }

        public void display()
        {
            String query = "select * from student";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;

            }
        }

      private void btndelete_Click(object sender, EventArgs e)
        {
            int id_val = int.Parse(id);
            con.Open();


           
            String query = "delete from student where id='" + id_val + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("record has been deleted sucessfully");
            con.Close();
            display();
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            int id_val = int.Parse(id);
               con.Open();

            int rollno = int.Parse(txtboxrollno.Text);
            String a = csem.GetItemText(csem.SelectedItem);
            int currentSem = int.Parse(a);

            String query = "update student set firstname= '" + txtboxfname.Text + "',lastname='" + txtboxlname.Text + "',currentsemester='" + currentSem + "',Rollno='" + rollno + "' where id='" + id_val + "' ";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("record has been updated sucessfully");
            con.Close();
            display();
        }

        private void btnadd_Click_1(object sender, EventArgs e)
        {
            int rollno = int.Parse(txtboxrollno.Text);
            String a = csem.GetItemText(csem.SelectedItem);
            int currentSem = int.Parse(a);

            con.Open();
            string query = "insert into student (firstname,lastname,currentsemester,rollno) values('" + txtboxfname.Text + "','" + txtboxlname.Text + "','" + currentSem + "','" + rollno + "')";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.ExecuteNonQuery();
            MessageBox.Show("record added sucessfully");
            con.Close();
            display();

        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txtboxfname.Text = row.Cells[1].Value.ToString();
                txtboxlname.Text = row.Cells[2].Value.ToString();
                txtboxrollno.Text = row.Cells[4].Value.ToString();
                csem.Text = row.Cells[3].Value.ToString();
                id = row.Cells[0].Value.ToString();
            

            }
        }
    }
}
