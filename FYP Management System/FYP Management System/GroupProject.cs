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
    public partial class GroupProject : Form
    {
        string conURL = "Data Source=(local);Initial Catalog=ProjectA;User ID=sa;Password=9876145";
        public GroupProject()
        {
            InitializeComponent();
        }
        DataTable dataTable;
        DataTable dataTable2;
        public static int groupId = -1;
        public static int projectId = -1;
        private void GroupProject_Load(object sender, EventArgs e)
        {
            update();
        }

        public void update()
        {
            dataTable2 = new DataTable();
            SqlConnection conn = new SqlConnection(conURL);
            conn.Open();
            String str1 = "SELECT Id AS 'Group Id' , Created_On AS 'Created On' FROM [Group]";
            SqlCommand cmd2 = new SqlCommand(str1, conn);
            SqlDataAdapter sda2 = new SqlDataAdapter();
            sda2.SelectCommand = cmd2;
            sda2.Fill(dataTable2);
            dataGridgroup.DataSource = dataTable2;

            //add button column
            DataGridViewButtonColumn button3 = new DataGridViewButtonColumn();
            button3.HeaderText = "ASSIGN PROJECT";
            button3.Name = "button3";
            button3.Text = "ASSIGN";
            button3.UseColumnTextForButtonValue = true;
            dataGridgroup.Columns.Add(button3);

            //view button column
            DataGridViewButtonColumn button9 = new DataGridViewButtonColumn();
            button9.HeaderText = "VIEW GROUP MEMBERS";
            button9.Name = "button9";
            button9.Text = "VIEW";
            button9.UseColumnTextForButtonValue = true;
            dataGridgroup.Columns.Add(button9);


            dataTable = new DataTable();
            String str = "SELECT ProjectId , GroupId , Title AS 'Project Title', AssignmentDate AS 'Assignment Date' FROM Project Join GroupProject ON ProjectId = Id";
            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            sda.Fill(dataTable);  // fill the data table with this data

            groupProjectDatagrid.DataSource = dataTable; // assign to data grid

            conn.Close();



            //add button column
            DataGridViewButtonColumn button1 = new DataGridViewButtonColumn();
            button1.HeaderText = "UPDATE DATA";
            button1.Name = "button1";
            button1.Text = "UPDATE";
            button1.UseColumnTextForButtonValue = true;
            groupProjectDatagrid.Columns.Add(button1);

            DataGridViewButtonColumn button8 = new DataGridViewButtonColumn();
            button8.HeaderText = "DELETE DATA";
            button8.Name = "button8";
            button8.Text = "DELETE";
            button8.UseColumnTextForButtonValue = true;
            groupProjectDatagrid.Columns.Add(button8);


            // adjust their widths when the data changes.
            //StudentData.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void dataGridgroup_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridgroup.Rows[e.RowIndex].ReadOnly = true;
            int noOfRows = dataGridgroup.RowCount;


            if (e.ColumnIndex == 2 && e.RowIndex >= 0 && e.RowIndex != (noOfRows - 1)) // when click on add button
            {
                groupId = Convert.ToInt32(dataGridgroup.Rows[e.RowIndex].Cells[0].Value);
                this.Hide();
                AssignProject create = new AssignProject();

                create.ShowDialog();
                this.Close();


            }

            if (e.ColumnIndex == 3 && e.RowIndex >= 0 && e.RowIndex != (noOfRows - 1)) // when click on view button
            {
                groupId = Convert.ToInt32(dataGridgroup.Rows[e.RowIndex].Cells[0].Value);
                this.Hide();
                ShowMembers create = new ShowMembers();

                create.ShowDialog();
                this.Close();


            }

            else { }
        }

        private void groupProjectDatagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            groupProjectDatagrid.Rows[e.RowIndex].ReadOnly = true;
            int noOfRows = groupProjectDatagrid.RowCount;
            if (e.ColumnIndex == 4 && e.RowIndex >= 0 && e.RowIndex != (noOfRows - 1))
            {
                projectId = Convert.ToInt32(groupProjectDatagrid.Rows[e.RowIndex].Cells[0].Value);
                groupId = Convert.ToInt32(groupProjectDatagrid.Rows[e.RowIndex].Cells[1].Value);
                this.Hide();
                AssignProject st = new AssignProject();
                st.ShowDialog();
                this.Close();
            }

            if (e.ColumnIndex == 5 && e.RowIndex >= 0 && e.RowIndex != (noOfRows - 1)) // when click on del button
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure that you want to delete it?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    SqlConnection con = new SqlConnection(conURL);

                    // connection open

                    con.Open();



                    string cmdText = "DELETE FROM GroupProject WHERE GroupId = @GroupId AND ProjectId =@ProjectId ";

                    SqlCommand c = new SqlCommand(cmdText, con);


                    int pId = Convert.ToInt32(groupProjectDatagrid.Rows[e.RowIndex].Cells[0].Value);
                    int gid = Convert.ToInt32(groupProjectDatagrid.Rows[e.RowIndex].Cells[1].Value);
                    c.Parameters.Add(new SqlParameter("@GroupId", gid));
                    c.Parameters.Add(new SqlParameter("@ProjectId", pId));
                    // execute it

                    c.ExecuteNonQuery();
                    // connection closed

                    con.Close();

                    dataGridgroup.DataSource = null;
                    dataGridgroup.Rows.Clear();
                    dataGridgroup.Columns.Clear();
                    groupProjectDatagrid.DataSource = null;
                    groupProjectDatagrid.Rows.Clear();
                    groupProjectDatagrid.Columns.Clear();
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
                dataGridgroup.DataSource = null;
                dataGridgroup.Rows.Clear();
                dataGridgroup.Columns.Clear();
                groupProjectDatagrid.DataSource = null;
                groupProjectDatagrid.Rows.Clear();
                groupProjectDatagrid.Columns.Clear();

                dataTable2 = new DataTable();
                SqlConnection conn = new SqlConnection(conURL);
                conn.Open();
                String str1 = "SELECT [Group].Id AS 'Group Id' , Created_On AS 'Created On' FROM [Group] JOIN (Project JOIN GroupProject ON ProjectId = Id) ON [Group].Id = GroupId where Title = @Title";
                SqlCommand cmd2 = new SqlCommand(str1, conn);
                cmd2.Parameters.Add(new SqlParameter("@Title", textBoxSearch.Text));
                SqlDataAdapter sda2 = new SqlDataAdapter();
                sda2.SelectCommand = cmd2;
                sda2.Fill(dataTable2);
                dataGridgroup.DataSource = dataTable2;

                //add button column
                DataGridViewButtonColumn button3 = new DataGridViewButtonColumn();
                button3.HeaderText = "ASSIGN PROJECT";
                button3.Name = "button3";
                button3.Text = "ASSIGN";
                button3.UseColumnTextForButtonValue = true;
                dataGridgroup.Columns.Add(button3);

                //view button column
                DataGridViewButtonColumn button9 = new DataGridViewButtonColumn();
                button9.HeaderText = "VIEW GROUP MEMBERS";
                button9.Name = "button9";
                button9.Text = "VIEW";
                button9.UseColumnTextForButtonValue = true;
                dataGridgroup.Columns.Add(button9);


                dataTable = new DataTable();
                String str = "SELECT ProjectId , GroupId , Title AS 'Project Title', AssignmentDate AS 'Assignment Date' FROM Project Join GroupProject ON ProjectId = Id where Title = @Title";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.Add(new SqlParameter("@Title", textBoxSearch.Text));
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                sda.Fill(dataTable);  // fill the data table with this data

                groupProjectDatagrid.DataSource = dataTable; // assign to data grid

                conn.Close();



                //add button column
                DataGridViewButtonColumn button1 = new DataGridViewButtonColumn();
                button1.HeaderText = "UPDATE DATA";
                button1.Name = "button1";
                button1.Text = "UPDATE";
                button1.UseColumnTextForButtonValue = true;
                groupProjectDatagrid.Columns.Add(button1);

                DataGridViewButtonColumn button8 = new DataGridViewButtonColumn();
                button8.HeaderText = "DELETE DATA";
                button8.Name = "button8";
                button8.Text = "DELETE";
                button8.UseColumnTextForButtonValue = true;
                groupProjectDatagrid.Columns.Add(button8);
            }
            else
            {
                dataGridgroup.DataSource = null;
                dataGridgroup.Rows.Clear();
                dataGridgroup.Columns.Clear();
                groupProjectDatagrid.DataSource = null;
                groupProjectDatagrid.Rows.Clear();
                groupProjectDatagrid.Columns.Clear();
                update();
            }
        }
    }
}
