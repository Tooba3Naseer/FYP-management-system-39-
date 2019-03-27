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

        private void ShowMembers_Load(object sender, EventArgs e)
        {
            DataTable dataTable2 = new DataTable();
            SqlConnection conn = new SqlConnection(conURL);
            conn.Open();
            String str1 = "SELECT StudentId, RegistrationNo AS 'Registration No', (SELECT Value FROM [Lookup] where Category='STATUS' AND [Lookup].Id =Status) AS 'Status', AssignmentDate AS 'Assignment Date'  FROM GroupStudent JOIN Student ON StudentId = Id where GroupId = @GroupId";
            SqlCommand cmd2 = new SqlCommand(str1, conn);
            cmd2.Parameters.Add(new SqlParameter("@GroupId", GroupProject.groupId));
            
            SqlDataAdapter sda2 = new SqlDataAdapter();
            sda2.SelectCommand = cmd2;
            sda2.Fill(dataTable2);
            dataGridStudents.DataSource = dataTable2;
            conn.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            GroupProject gp = new GroupProject();
            gp.ShowDialog();
            this.Close();
        }
    }
}
