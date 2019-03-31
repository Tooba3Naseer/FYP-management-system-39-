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
    public partial class StudentgroupManagment : Form
    {
        string conURL = "Data Source=(local);Initial Catalog=ProjectA;User ID=sa;Password=9876145";
        public StudentgroupManagment()
        {
            InitializeComponent();
        }
        public static int groupID = -1;
        public static int stID = -1;
        DataTable dataTable;
        DataTable dataTable2;
        private void Create_Click(object sender, EventArgs e)
        {

            SqlConnection conn = new SqlConnection(conURL);
            conn.Open();
            DateTime today = DateTime.Today;
            string cmdText = "INSERT INTO [Group] (Created_On) VALUES (@Created_On)";

            SqlCommand c = new SqlCommand(cmdText, conn);
            c.Parameters.Add(new SqlParameter("@Created_On", today));
            // execute it

            int result = c.ExecuteNonQuery();
            if (result < 0)
                MessageBox.Show("Error");

            dataGridGroup.DataSource = null;
            dataGridGroup.Rows.Clear();
            dataGridGroup.Columns.Clear();
            StudentGroupDatagrid.DataSource = null;
            StudentGroupDatagrid.Rows.Clear();
            StudentGroupDatagrid.Columns.Clear();
            update();
        }

        private void StudentGroupDatagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            StudentGroupDatagrid.Rows[e.RowIndex].ReadOnly = true;
            int noOfRows = StudentGroupDatagrid.RowCount;
            if (e.ColumnIndex == 5 && e.RowIndex >= 0 && e.RowIndex != (noOfRows - 1)) // when click on update button
            {
                stID = Convert.ToInt32(StudentGroupDatagrid.Rows[e.RowIndex].Cells[1].Value);
                groupID = Convert.ToInt32(StudentGroupDatagrid.Rows[e.RowIndex].Cells[0].Value);
                this.Hide();
                AddStudent st = new AddStudent();
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
                    con.Open();


                    try
                    {

                        string cmdText = "DELETE FROM GroupStudent WHERE GroupId = @GroupId AND StudentId = @StudentId";

                        SqlCommand c = new SqlCommand(cmdText, con);


                        int Idy = Convert.ToInt32(StudentGroupDatagrid.Rows[e.RowIndex].Cells[0].Value);
                        int stid = Convert.ToInt32(StudentGroupDatagrid.Rows[e.RowIndex].Cells[1].Value);
                        c.Parameters.Add(new SqlParameter("@GroupId", Idy));
                        c.Parameters.Add(new SqlParameter("@StudentId", stid));
                        // execute it

                        c.ExecuteNonQuery();

                        
                        // connection closed

                        con.Close();

                        dataGridGroup.DataSource = null;
                        dataGridGroup.Rows.Clear();
                        dataGridGroup.Columns.Clear();
                        StudentGroupDatagrid.DataSource = null;
                        StudentGroupDatagrid.Rows.Clear();
                        StudentGroupDatagrid.Columns.Clear();
                        update();

                        MessageBox.Show("Successfully Deleted");
                    }
                    catch (Exception)

                    {

                        MessageBox.Show("Error");
                    }

                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }


        }

        public void update()
        {
            dataTable2 = new DataTable();
            SqlConnection conn = new SqlConnection(conURL);
            conn.Open();
            String str1 = "SELECT Id, Created_On AS 'Created On' FROM [Group]";
            SqlCommand cmd2 = new SqlCommand(str1, conn);
            SqlDataAdapter sda2 = new SqlDataAdapter();
            sda2.SelectCommand = cmd2;
            sda2.Fill(dataTable2);
            dataGridGroup.DataSource = dataTable2;

            //add button column
            DataGridViewButtonColumn button3 = new DataGridViewButtonColumn();
            button3.HeaderText = "ADD STUDENT";
            button3.Name = "button3";
            button3.Text = "ADD";
            button3.UseColumnTextForButtonValue = true;
            dataGridGroup.Columns.Add(button3);


            //add button column
            DataGridViewButtonColumn button4 = new DataGridViewButtonColumn();
            button4.HeaderText = "DELETE GROUP";
            button4.Name = "button4";
            button4.Text = "DELETE";
            button4.UseColumnTextForButtonValue = true;
            dataGridGroup.Columns.Add(button4);


            dataTable = new DataTable();
            String str = "SELECT [Group].Id AS 'Group Id', StudentId AS 'Student Id', RegistrationNo AS 'Registration No', (SELECT Value FROM [Lookup] where Category = 'Status' AND Status = Id) AS 'Status', AssignmentDate AS 'Assignment Date' FROM (GroupStudent Join Student ON GroupStudent.StudentId=Student.Id) JOIN [Group] ON [Group].Id=GroupStudent.GroupId ";
            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            sda.Fill(dataTable);  // fill the data table with this data

            StudentGroupDatagrid.DataSource = dataTable; // assign to data grid

            conn.Close();

           

            //add button column
            DataGridViewButtonColumn button1 = new DataGridViewButtonColumn();
            button1.HeaderText = "Update Data";
            button1.Name = "button1";
            button1.Text = "UPDATE";
            button1.UseColumnTextForButtonValue = true;
            StudentGroupDatagrid.Columns.Add(button1);

            DataGridViewButtonColumn buttont = new DataGridViewButtonColumn();
            buttont.HeaderText = "Delete Data";
            buttont.Name = "buttont";
            buttont.Text = "DELETE";
            buttont.UseColumnTextForButtonValue = true;
            StudentGroupDatagrid.Columns.Add(buttont);


            // adjust their widths when the data changes.
            //StudentData.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void StudentgroupManagment_Load(object sender, EventArgs e)
        {
            update();
        }

        private void dataGridGroup_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridGroup.Rows[e.RowIndex].ReadOnly = true;
            int noOfRows = dataGridGroup.RowCount;
            if (e.ColumnIndex == 3 && e.RowIndex >= 0 && e.RowIndex != (noOfRows - 1)) // when click on del button
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure that you want to delete it?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    SqlConnection con = new SqlConnection(conURL);

                    // connection opens
                    con.Open();


                    try
                    { 

                    string cmdText = "DELETE FROM GroupStudent WHERE GroupId = @GroupId";

                    SqlCommand c = new SqlCommand(cmdText, con);


                    int Idy = Convert.ToInt32(dataGridGroup.Rows[e.RowIndex].Cells[0].Value);
                    c.Parameters.Add(new SqlParameter("@GroupId", Idy));
                    // execute it

                    c.ExecuteNonQuery();

                    string cmdText2 = "DELETE FROM [Group] WHERE Id = @Id";
                    SqlCommand c2 = new SqlCommand(cmdText2, con);
                    c2.Parameters.Add(new SqlParameter("@Id", Idy));
                    c2.ExecuteNonQuery();
                    // connection closed

                    con.Close();

                    dataGridGroup.DataSource = null;
                    dataGridGroup.Rows.Clear();
                    dataGridGroup.Columns.Clear();
                    StudentGroupDatagrid.DataSource = null;
                    StudentGroupDatagrid.Rows.Clear();
                    StudentGroupDatagrid.Columns.Clear();
                    update();

                    MessageBox.Show("Successfully Deleted");
                }
                     catch (Exception)

                    {

                        MessageBox.Show("Project might be assign to this group or this group might be some record in evaluation. So delete that record then delete it here");
                    }

                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }

            if (e.ColumnIndex == 2 && e.RowIndex >= 0 && e.RowIndex != (noOfRows - 1)) // when click on add button
            {
                groupID = Convert.ToInt32(dataGridGroup.Rows[e.RowIndex].Cells[0].Value);
                this.Hide();
                AddStudent create = new AddStudent();

                create.ShowDialog();
                this.Close();


            }
            else { }
        
    }
        // this is for searching purpose, searching based on regNo, when user enter complete regNo, then user able 
        // to see filtered rows
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(textBoxSearch.Text))
            {
                dataGridGroup.DataSource = null;
                dataGridGroup.Rows.Clear();
                dataGridGroup.Columns.Clear();
                StudentGroupDatagrid.DataSource = null;
                StudentGroupDatagrid.Rows.Clear();
                StudentGroupDatagrid.Columns.Clear();


                dataTable2 = new DataTable();
                SqlConnection conn = new SqlConnection(conURL);
                conn.Open();
                String str1 = "SELECT [Group].Id, Created_On AS 'Created On'  FROM (GroupStudent Join Student ON GroupStudent.StudentId=Student.Id) JOIN [Group] ON [Group].Id=GroupStudent.GroupId where RegistrationNo= @RegistrationNo";
                
                SqlCommand cmd2 = new SqlCommand(str1, conn);
                cmd2.Parameters.Add(new SqlParameter("@RegistrationNo", textBoxSearch.Text));
                SqlDataAdapter sda2 = new SqlDataAdapter();
                sda2.SelectCommand = cmd2;
                sda2.Fill(dataTable2);
                dataGridGroup.DataSource = dataTable2;

                //add button column
                DataGridViewButtonColumn button3 = new DataGridViewButtonColumn();
                button3.HeaderText = "ADD STUDENT";
                button3.Name = "button3";
                button3.Text = "ADD";
                button3.UseColumnTextForButtonValue = true;
                dataGridGroup.Columns.Add(button3);


                //add button column
                DataGridViewButtonColumn button4 = new DataGridViewButtonColumn();
                button4.HeaderText = "DELETE GROUP";
                button4.Name = "button4";
                button4.Text = "DELETE";
                button4.UseColumnTextForButtonValue = true;
                dataGridGroup.Columns.Add(button4);


                dataTable = new DataTable();
                String str = "SELECT [Group].Id AS 'Group Id', StudentId AS 'Student Id', RegistrationNo AS 'Registration No', (SELECT Value FROM [Lookup] where Category = 'Status' AND Status = Id) AS 'Status', AssignmentDate AS 'Assignment Date' FROM (GroupStudent Join Student ON GroupStudent.StudentId=Student.Id) JOIN [Group] ON [Group].Id=GroupStudent.GroupId where RegistrationNo= @RegistrationNo";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.Add(new SqlParameter("@RegistrationNo", textBoxSearch.Text));
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                sda.Fill(dataTable);  // fill the data table with this data

                StudentGroupDatagrid.DataSource = dataTable; // assign to data grid

                conn.Close();



                //add button column
                DataGridViewButtonColumn button1 = new DataGridViewButtonColumn();
                button1.HeaderText = "Update Data";
                button1.Name = "button1";
                button1.Text = "UPDATE";
                button1.UseColumnTextForButtonValue = true;
                StudentGroupDatagrid.Columns.Add(button1);

                DataGridViewButtonColumn buttont = new DataGridViewButtonColumn();
                buttont.HeaderText = "Delete Data";
                buttont.Name = "buttont";
                buttont.Text = "DELETE";
                buttont.UseColumnTextForButtonValue = true;
                StudentGroupDatagrid.Columns.Add(buttont);


                // adjust their widths when the data changes.
                //StudentData.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            else
            {
                dataGridGroup.DataSource = null;
                dataGridGroup.Rows.Clear();
                dataGridGroup.Columns.Clear();
                StudentGroupDatagrid.DataSource = null;
                StudentGroupDatagrid.Rows.Clear();
                StudentGroupDatagrid.Columns.Clear();
                update();
            }
        }
    }
}
