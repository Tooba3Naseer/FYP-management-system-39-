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
    public partial class StudentgroupManagment : Form
    {
        public StudentgroupManagment()
        {
            InitializeComponent();
        }

        private void Create_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormationOfGroups fg = new FormationOfGroups();
            fg.ShowDialog();
            this.Close();
        }
    }
}
