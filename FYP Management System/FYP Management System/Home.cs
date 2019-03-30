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

        private void Mstudents_Click(object sender, EventArgs e)
        {
            
            PersonStudent st = new PersonStudent();
            st.ShowDialog();
            
        }

        private void Madvisors_Click(object sender, EventArgs e)
        {
            Advisor st = new Advisor();
            st.ShowDialog();
        
        }

        private void Mprojects_Click(object sender, EventArgs e)
        {
            
            Project st = new Project();
            st.ShowDialog();
          
        }

        private void MevalButton_Click(object sender, EventArgs e)
        {
            
            Evaluation st = new Evaluation();
            st.ShowDialog();
          
        }

        private void buttonSg_Click(object sender, EventArgs e)
        {
            StudentgroupManagment sg = new StudentgroupManagment();
            sg.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProjectAdvisorManagement pa = new ProjectAdvisorManagement();
            pa.ShowDialog();
        }

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
    }
}
