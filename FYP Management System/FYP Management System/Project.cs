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
        public static int id = -1;
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Project_Load(object sender, EventArgs e)
        {
            update();

        }

        // this will load the data of table in data grid view
        public void update()
        {
            
            dataTable = new DataTable(); 
            SqlConnection conn = new SqlConnection(conURL);
            conn.Open();
            String str = "SELECT * FROM Project";
            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            sda.Fill(dataTable);  // fill the data table with this data

            ProjectData.DataSource = dataTable; // assign to data grid

            
            

            sda.Update(dataTable);
            conn.Close();

            //add button column
            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            button.HeaderText = "Delete Data";
            button.Name = "button";
            button.Text = "DELETE";
            button.UseColumnTextForButtonValue = true;
            ProjectData.Columns.Add(button);

            //add button column
            DataGridViewButtonColumn button1 = new DataGridViewButtonColumn();
            button1.HeaderText = "Update Data";
            button1.Name = "button1";
            button1.Text = "UPDATE";
            button1.UseColumnTextForButtonValue = true;
            ProjectData.Columns.Add(button1);

            // Configure the details DataGridView so that its columns automatically 
            // adjust their widths when the data changes.
            ProjectData.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

        }

        

        private void SearchText_TextChanged(object sender, EventArgs e)
        {
            DataView dataView = new DataView(dataTable);
            dataView.RowFilter = string.Format("Title LIKE '%{0}%'", SearchText.Text);
            ProjectData.DataSource = dataView;
        }

        private void ProjectData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProjectData.Rows[e.RowIndex].ReadOnly = true;

            
            int noOfRows = ProjectData.RowCount;
            if (e.ColumnIndex==3 && e.RowIndex >=0 && e.RowIndex != (noOfRows-1))
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure that you want to delete it?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    SqlConnection con = new SqlConnection(conURL);

                    // connection opens
                    // purpose of checker it to find whether user enter all data in correct format or not
                    // if not in correct format then show exception and handle in catch section


                    try
                    {

                        con.Open();


                    }
                    catch (Exception)

                    {

                        MessageBox.Show("error");
                    }


                    string cmdText = "DELETE FROM Project WHERE Id = @Id";

                    SqlCommand c = new SqlCommand(cmdText, con);
                    //c.Parameters.Add(new SqlParameter("@Id", textBoxid.Text));

                    int id = Convert.ToInt32(ProjectData.Rows[e.RowIndex].Cells[0].Value);
                    c.Parameters.Add(new SqlParameter("@Id", id));
                    //Read the command and execute it

                    int result = c.ExecuteNonQuery();
                    if (result < 0)
                        MessageBox.Show("Error");

                    // connection closed
                    // show dialog box if added in table of database
                    MessageBox.Show("Successfully Deleted");
                   
                    ProjectData.DataSource = null;
                    ProjectData.Rows.Clear();
                    ProjectData.Columns.Clear();
                    update();
                    
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }

            if (e.ColumnIndex == 4 && e.RowIndex >= 0 && e.RowIndex != (noOfRows - 1))
            {
                id = Convert.ToInt32(ProjectData.Rows[e.RowIndex].Cells[0].Value);
                this.Hide();
                InsertProject create = new InsertProject();
                create.ShowDialog();
                this.Close();


            }
            else { }
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
