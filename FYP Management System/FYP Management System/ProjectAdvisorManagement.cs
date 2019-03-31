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
    public partial class ProjectAdvisorManagement : Form
    {
        string conURL = "Data Source=(local);Initial Catalog=ProjectA;User ID=sa;Password=9876145";
        public ProjectAdvisorManagement()
        {
            InitializeComponent();
        }
        DataTable dataTable;
        DataTable dataTable2;
        public static int projectID = -1;
        public static int AdvisorID = -1;
        private void ProjectAdvisorManagement_Load(object sender, EventArgs e)
        {
            update();
        }

        public void update()
        {
            dataTable2 = new DataTable();
            SqlConnection conn = new SqlConnection(conURL);
            conn.Open();
            String str1 = "SELECT Id AS 'ProjectId' , Title AS 'Project Title' FROM Project";
            SqlCommand cmd2 = new SqlCommand(str1, conn);
            SqlDataAdapter sda2 = new SqlDataAdapter();
            sda2.SelectCommand = cmd2;
            sda2.Fill(dataTable2);
            dataGridproject.DataSource = dataTable2;

            //add button column
            DataGridViewButtonColumn button3 = new DataGridViewButtonColumn();
            button3.HeaderText = "ASSIGN ADVISOR";
            button3.Name = "button3";
            button3.Text = "ASSIGN";
            button3.UseColumnTextForButtonValue = true;
            dataGridproject.Columns.Add(button3);


            dataTable = new DataTable();
            String str = "SELECT ProjectId , AdvisorId , FirstName + ' ' + LastName AS 'Name', (SELECT Value FROM [Lookup] where Category = 'ADVISOR_ROLE' AND AdvisorRole = Id) AS 'Advisor Role', AssignmentDate AS 'Assignment Date' FROM (ProjectAdvisor Join Advisor ON AdvisorId=Id) JOIN Person ON Person.Id=Advisor.Id ";
            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            sda.Fill(dataTable);  // fill the data table with this data

            ProjectAdvisorDatagrid.DataSource = dataTable; // assign to data grid

            conn.Close();



            //add button column
            DataGridViewButtonColumn button1 = new DataGridViewButtonColumn();
            button1.HeaderText = "UPDATE DATA";
            button1.Name = "button1";
            button1.Text = "UPDATE";
            button1.UseColumnTextForButtonValue = true;
            ProjectAdvisorDatagrid.Columns.Add(button1);

            DataGridViewButtonColumn button8 = new DataGridViewButtonColumn();
            button8.HeaderText = "DELETE DATA";
            button8.Name = "button8";
            button8.Text = "DELETE";
            button8.UseColumnTextForButtonValue = true;
            ProjectAdvisorDatagrid.Columns.Add(button8);


            // adjust their widths when the data changes.
            //StudentData.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void dataGridproject_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridproject.Rows[e.RowIndex].ReadOnly = true;
            int noOfRows = dataGridproject.RowCount;
            

            if (e.ColumnIndex == 2 && e.RowIndex >= 0 && e.RowIndex != (noOfRows - 1)) // when click on assign button
            {
                projectID = Convert.ToInt32(dataGridproject.Rows[e.RowIndex].Cells[0].Value);
                this.Hide();
                AssignAdvisor create = new AssignAdvisor();

                create.ShowDialog();
                this.Close();


            }
           
            else { }
        }

        private void ProjectAdvisorDatagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProjectAdvisorDatagrid.Rows[e.RowIndex].ReadOnly = true;
            int noOfRows = ProjectAdvisorDatagrid.RowCount;
            if (e.ColumnIndex == 5 && e.RowIndex >= 0 && e.RowIndex != (noOfRows - 1))  // when click on update
            {
                projectID = Convert.ToInt32(ProjectAdvisorDatagrid.Rows[e.RowIndex].Cells[0].Value);
                AdvisorID = Convert.ToInt32(ProjectAdvisorDatagrid.Rows[e.RowIndex].Cells[1].Value);
                this.Hide();
                AssignAdvisor st = new AssignAdvisor();
                st.ShowDialog();
                this.Close();
            }

            if (e.ColumnIndex == 6 && e.RowIndex >= 0 && e.RowIndex != (noOfRows - 1)) // when click on del button
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure that you want to delete it?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    SqlConnection con = new SqlConnection(conURL);

                    // connection opens



                    try
                    {

                        con.Open();


                    }
                    catch (Exception)

                    {

                        MessageBox.Show("error");
                    }


                    string cmdText = "DELETE FROM ProjectAdvisor WHERE AdvisorId = @AdvisorId AND ProjectId =@ProjectId ";

                    SqlCommand c = new SqlCommand(cmdText, con);


                    int pId = Convert.ToInt32(ProjectAdvisorDatagrid.Rows[e.RowIndex].Cells[0].Value);
                    int Aid = Convert.ToInt32(ProjectAdvisorDatagrid.Rows[e.RowIndex].Cells[1].Value);
                    c.Parameters.Add(new SqlParameter("@AdvisorId", Aid));
                    c.Parameters.Add(new SqlParameter("@ProjectId", pId));
                    // execute it

                    c.ExecuteNonQuery();
                    // connection closed

                    con.Close();

                    ProjectAdvisorDatagrid.DataSource = null;
                    ProjectAdvisorDatagrid.Rows.Clear();
                    ProjectAdvisorDatagrid.Columns.Clear();
                    dataGridproject.DataSource = null;
                    dataGridproject.Rows.Clear();
                    dataGridproject.Columns.Clear();
                    update();

                    MessageBox.Show("Successfully Deleted!!");

                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }

        }
        // this is for searching purpose, searching based on title, when user enter complete title name, then user able 
        // to see filtered rows
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxSearch.Text))
            {
                ProjectAdvisorDatagrid.DataSource = null;
                ProjectAdvisorDatagrid.Rows.Clear();
                ProjectAdvisorDatagrid.Columns.Clear();
                dataGridproject.DataSource = null;
                dataGridproject.Rows.Clear();
                dataGridproject.Columns.Clear();

                dataTable2 = new DataTable();
                SqlConnection conn = new SqlConnection(conURL);
                conn.Open();
                String str1 = "SELECT Id AS 'ProjectId' , Title AS 'Project Title' FROM Project where Title = @Title";
                SqlCommand cmd2 = new SqlCommand(str1, conn);
                cmd2.Parameters.Add(new SqlParameter("@Title", textBoxSearch.Text));
                SqlDataAdapter sda2 = new SqlDataAdapter();
                sda2.SelectCommand = cmd2;
                sda2.Fill(dataTable2);
                dataGridproject.DataSource = dataTable2;

                //add button column
                DataGridViewButtonColumn button3 = new DataGridViewButtonColumn();
                button3.HeaderText = "ASSIGN ADVISOR";
                button3.Name = "button3";
                button3.Text = "ASSIGN";
                button3.UseColumnTextForButtonValue = true;
                dataGridproject.Columns.Add(button3);


                dataTable = new DataTable();
                String str = "SELECT ProjectId , AdvisorId , FirstName + ' ' + LastName AS 'Name', (SELECT Value FROM [Lookup] where Category = 'ADVISOR_ROLE' AND AdvisorRole = Id) AS 'Advisor Role', AssignmentDate AS 'Assignment Date' FROM (ProjectAdvisor Join Advisor ON AdvisorId=Id) JOIN Person ON Person.Id=Advisor.Id where ProjectId = (SELECT Id FROM Project where Title = @Title)";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.Add(new SqlParameter("@Title", textBoxSearch.Text));
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                sda.Fill(dataTable);  // fill the data table with this data

                ProjectAdvisorDatagrid.DataSource = dataTable; // assign to data grid

                conn.Close();



                //add button column
                DataGridViewButtonColumn button1 = new DataGridViewButtonColumn();
                button1.HeaderText = "UPDATE DATA";
                button1.Name = "button1";
                button1.Text = "UPDATE";
                button1.UseColumnTextForButtonValue = true;
                ProjectAdvisorDatagrid.Columns.Add(button1);

                DataGridViewButtonColumn button8 = new DataGridViewButtonColumn();
                button8.HeaderText = "DELETE DATA";
                button8.Name = "button8";
                button8.Text = "DELETE";
                button8.UseColumnTextForButtonValue = true;
                ProjectAdvisorDatagrid.Columns.Add(button8);


            }
            else
            {
                ProjectAdvisorDatagrid.DataSource = null;
                ProjectAdvisorDatagrid.Rows.Clear();
                ProjectAdvisorDatagrid.Columns.Clear();
                dataGridproject.DataSource = null;
                dataGridproject.Rows.Clear();
                dataGridproject.Columns.Clear();
                update();
            }
        }
    }
}
