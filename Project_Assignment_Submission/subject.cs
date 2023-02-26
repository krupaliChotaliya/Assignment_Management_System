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

namespace Project_Assignment_Submission
{
    public partial class subject : UserControl
    {
        SqlConnection con;
       
        String sub;
        public subject()
        {
            con = new SqlConnection(@"Data Source=DESKTOP-1R31CPH\SQLEXPRESS;Initial Catalog=ams;Integrated Security=True");

            InitializeComponent();
            display();
        }

       
        public void display()
        {
            String query = "select * from subject";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;

            }
        }
      
        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
           
            String a = txtboxsem.GetItemText(txtboxsem.SelectedItem);
            int currentSem = int.Parse(a);


            con.Open();
            string query = "insert into subject (subjectname,semester) values('" + txtboxsubname.Text + "','" + currentSem + "')";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.ExecuteNonQuery();
            MessageBox.Show("record added sucessfully");
            con.Close();
            display();
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
          
            try
            {
                con.Open();

                String a = txtboxsem.GetItemText(txtboxsem.SelectedItem);
                int currentSem = int.Parse(a);
              

                String query = "update subject set subjectname= '" + txtboxsubname.Text + "',semester='" + currentSem + "' where subjectname='" + sub + "' ";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("record has been updated sucessfully");
                con.Close();
                display();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

           

        }

        private void btndelete_Click(object sender, EventArgs e)
        {
           
            con.Open();

            String query = "delete from subject where subjectname='" + txtboxsubname.Text + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("record has been deleted sucessfully");
            con.Close();
            display();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txtboxsubname.Text = row.Cells[0].Value.ToString();
                sub = row.Cells[0].Value.ToString();
                txtboxsem.Text = row.Cells[1].Value.ToString();
           
            }
        }
    }
}
