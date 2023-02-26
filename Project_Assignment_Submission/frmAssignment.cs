using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Assignment_Submission
{
    public partial class frmAssignment : Form
    {
        public frmAssignment()
        {
            InitializeComponent();
        }

        private void frmAssignment_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle= FormBorderStyle.None;

            student1.Visible = false;
            subject1.Visible = false;
            assignmentPage1.Visible = false;
            addAssignment1.Visible = false;
            assignment_submission1.Visible = false;
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void btnstudent_Click_1(object sender, EventArgs e)
        {
            student1.Visible = true;
            subject1.Visible = false;
            assignmentPage1.Visible = false;
            addAssignment1.Visible=false;
            assignment_submission1.Visible = false;
        }

        private void btnhome_Click_1(object sender, EventArgs e)
        {
            student1.Visible = false;
            subject1.Visible = false;
            assignmentPage1.Visible = false;
            addAssignment1.Visible = false;
            assignment_submission1.Visible = false;
        }

        private void btnsubject_Click(object sender, EventArgs e)
        {
            assignmentPage1.Visible = false;
            student1.Visible = false;
            subject1.Visible = true;
            addAssignment1.Visible = false;
            assignment_submission1.Visible = false;
        }

        private void btnassignment_Click(object sender, EventArgs e)
        {
            student1.Visible = false;
            subject1.Visible = false;
            assignmentPage1.Visible = true;
            addAssignment1.Visible = false;
            assignment_submission1.Visible = false;
        }

        private void btnAddNewAssign_Click(object sender, EventArgs e)
        {
            student1.Visible = false;
            subject1.Visible = false;
            assignmentPage1.Visible = false;
            addAssignment1.Visible = true;
            assignment_submission1.Visible = false;
        }

        private void btnsubmission_Click(object sender, EventArgs e)
        {
            assignment_submission1.Visible = true;
            student1.Visible = false;
            subject1.Visible = false;
            assignmentPage1.Visible = false;
            addAssignment1.Visible = false;
        }
    }
}
