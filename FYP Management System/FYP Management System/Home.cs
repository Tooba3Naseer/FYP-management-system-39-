﻿using System;
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
    }
}