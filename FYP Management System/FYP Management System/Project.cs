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
    public partial class Project : Form
    {
        string conURL = "Data Source=(local);Initial Catalog=ProjectA;User ID=sa;Password=9876145";
        public Project()
        {
            InitializeComponent();
        }
        DataTable dataTable;
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Project_Load(object sender, EventArgs e)
        {
            dataTable = new DataTable(); // create data table oject
            SqlConnection conn = new SqlConnection(conURL);
            conn.Open();
            String str = "SELECT * FROM Project";
            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            sda.Fill(dataTable);  // fill the data table with this data

            //BindingSource source = new BindingSource();
            //source.DataSource = dataTable;

            ProjectData.DataSource = dataTable; // assign to data grid
            sda.Update(dataTable);
            conn.Close();
        }

        private void SearchText_TextChanged(object sender, EventArgs e)
        {
            DataView dataView = new DataView(dataTable);
            dataView.RowFilter = string.Format("Title LIKE '%{0}%'", SearchText.Text);
            ProjectData.DataSource = dataView;
        }

        private void ProjectData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Create_Click(object sender, EventArgs e)
        {
            this.Hide();
            InsertProject create = new InsertProject();
            create.ShowDialog();
            this.Close();
        }
    }
}
