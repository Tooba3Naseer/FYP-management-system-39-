using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FYP_Management_System
{
    public partial class Home : Form
    {
        
        public Home()
        {
            InitializeComponent();
        }
        // goes to manage students
        private void Mstudents_Click(object sender, EventArgs e)
        {
            
            PersonStudent st = new PersonStudent();
            st.ShowDialog();
            
        }
        // goes to manage advisors
        private void Madvisors_Click(object sender, EventArgs e)
        {
            Advisor st = new Advisor();
            st.ShowDialog();
        
        }
        // goes to manage projects form
        private void Mprojects_Click(object sender, EventArgs e)
        {
            
            Project st = new Project();
            st.ShowDialog();
          
        }
        // goes to manage evaluations form
        private void MevalButton_Click(object sender, EventArgs e)
        {
            
            Evaluation st = new Evaluation();
            st.ShowDialog();
          
        }
        // goes to manage studdent group
        private void buttonSg_Click(object sender, EventArgs e)
        {
            StudentgroupManagment sg = new StudentgroupManagment();
            sg.ShowDialog();
        }
        // goes to manage project advisors
        private void button1_Click(object sender, EventArgs e)
        {
            ProjectAdvisorManagement pa = new ProjectAdvisorManagement();
            pa.ShowDialog();
        }
        // goes to manage group project
        private void button2_Click(object sender, EventArgs e)
        {
            GroupProject gp = new GroupProject();
            gp.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MarkEvaluationGroup eg = new MarkEvaluationGroup();
            eg.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Report1 eg = new Report1();
            eg.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Report2 eg = new Report2();
            eg.ShowDialog();
        }
    }
}
