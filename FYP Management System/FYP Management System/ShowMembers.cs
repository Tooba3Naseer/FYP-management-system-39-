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

namespace FYP_Management_System
{
    public partial class ShowMembers : Form
    {
        string conURL = "Data Source=(local);Initial Catalog=ProjectA;User ID=sa;Password=9876145";
        public ShowMembers()
        {
            InitializeComponent();
        }
        static int buffer = -1;
        private void ShowMembers_Load(object sender, EventArgs e)
        {
            buffer = GroupProject.groupId;
            if (buffer > 0)
            {
                DataTable dataTable2 = new DataTable();
                SqlConnection conn = new SqlConnection(conURL);
                conn.Open();
                String str1 = "SELECT StudentId, RegistrationNo AS 'Registration No', (SELECT Value FROM [Lookup] where Category='STATUS' AND [Lookup].Id =Status) AS 'Status', AssignmentDate AS 'Assignment Date'  FROM GroupStudent JOIN Student ON StudentId = Id where GroupId = @GroupId";
                SqlCommand cmd2 = new SqlCommand(str1, conn);
                cmd2.Parameters.Add(new SqlParameter("@GroupId",buffer ));

                SqlDataAdapter sda2 = new SqlDataAdapter();
                sda2.SelectCommand = cmd2;
                sda2.Fill(dataTable2);
                dataGridStudents.DataSource = dataTable2;
                conn.Close();
                GroupProject.groupId = -1;
            }
            else
            {
                DataTable dataTable2 = new DataTable();
                SqlConnection conn = new SqlConnection(conURL);
                conn.Open();
                String str1 = "SELECT StudentId, RegistrationNo AS 'Registration No', (SELECT Value FROM [Lookup] where Category='STATUS' AND [Lookup].Id =Status) AS 'Status', AssignmentDate AS 'Assignment Date'  FROM GroupStudent JOIN Student ON StudentId = Id where GroupId = @GroupId";
                SqlCommand cmd2 = new SqlCommand(str1, conn);
                cmd2.Parameters.Add(new SqlParameter("@GroupId", MarkEvaluationGroup.groupId));

                SqlDataAdapter sda2 = new SqlDataAdapter();
                sda2.SelectCommand = cmd2;
                sda2.Fill(dataTable2);
                dataGridStudents.DataSource = dataTable2;
                conn.Close();
                MarkEvaluationGroup.groupId = -1;
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (buffer > 0)
            { 
                GroupProject gp = new GroupProject();
                gp.ShowDialog();
            }
            else
            {
                MarkEvaluationGroup mg = new MarkEvaluationGroup();
                mg.ShowDialog();
            }
            this.Close();
        }
    }
}
