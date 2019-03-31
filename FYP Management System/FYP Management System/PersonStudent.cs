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
    public partial class PersonStudent : Form
    {
        string conURL = "Data Source=(local);Initial Catalog=ProjectA;User ID=sa;Password=9876145";
        public PersonStudent()
        {
            InitializeComponent();
        }
        DataTable dataTable;
        public static int stId = -1;  
        private void PersonStudent_Load(object sender, EventArgs e)
        {
            update();

        }

        public void update()
        {
            dataTable = new DataTable();
            SqlConnection conn = new SqlConnection(conURL);
            conn.Open();
            String str = "SELECT Person.Id, FirstName, LastName,Contact, Email,DateOfBirth,(SELECT Value FROM Lookup WHERE Category = 'GENDER' AND Id = Gender) AS 'Gender', RegistrationNo AS 'Registration No' FROM Person Join Student ON Person.Id=Student.Id";
            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            sda.Fill(dataTable);  // fill the data table with this data

            StudentData.DataSource = dataTable; // assign to data grid




            
            conn.Close();

            //add button column
            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            button.HeaderText = "Delete Data";
            button.Name = "button";
            button.Text = "DELETE";
            button.UseColumnTextForButtonValue = true;
            StudentData.Columns.Add(button);

            //add button column
            DataGridViewButtonColumn button1 = new DataGridViewButtonColumn();
            button1.HeaderText = "Update Data";
            button1.Name = "button1";
            button1.Text = "UPDATE";
            button1.UseColumnTextForButtonValue = true;
            StudentData.Columns.Add(button1);

            
            // adjust their widths when the data changes.
           //StudentData.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void Create_Click(object sender, EventArgs e)
        {
            this.Hide();
            Create_Student st = new Create_Student();
            st.ShowDialog();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        
        // searching and filtering on the basis of reg no
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxSearch.Text))
            {
                StudentData.DataSource = null;
                StudentData.Rows.Clear();
                StudentData.Columns.Clear();
                
                dataTable = new DataTable();
                SqlConnection conn = new SqlConnection(conURL);
                conn.Open();
                String str = "SELECT Person.Id, FirstName, LastName,Contact, Email,DateOfBirth,(SELECT Value FROM Lookup WHERE Category = 'GENDER' AND Id = Gender) AS 'Gender', RegistrationNo AS 'Registration No' FROM Person Join Student ON Person.Id=Student.Id where RegistrationNo = @RegistrationNo";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.Add(new SqlParameter("@RegistrationNo", textBoxSearch.Text));
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                sda.Fill(dataTable);  // fill the data table with this data

                StudentData.DataSource = dataTable; // assign to data grid





                conn.Close();

                //add button column
                DataGridViewButtonColumn button = new DataGridViewButtonColumn();
                button.HeaderText = "Delete Data";
                button.Name = "button";
                button.Text = "DELETE";
                button.UseColumnTextForButtonValue = true;
                StudentData.Columns.Add(button);

                //add button column
                DataGridViewButtonColumn button1 = new DataGridViewButtonColumn();
                button1.HeaderText = "Update Data";
                button1.Name = "button1";
                button1.Text = "UPDATE";
                button1.UseColumnTextForButtonValue = true;
                StudentData.Columns.Add(button1);
            }
            else
            {
                StudentData.DataSource = null;
                StudentData.Rows.Clear();
                StudentData.Columns.Clear();
                update();
            }


        }

        private void StudentData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            

            StudentData.Rows[e.RowIndex].ReadOnly = true;
            int noOfRows = StudentData.RowCount;
            if (e.ColumnIndex == 8 && e.RowIndex >= 0 && e.RowIndex != (noOfRows - 1)) // when click on del button
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure that you want to delete it?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    SqlConnection con = new SqlConnection(conURL);

                    // connection opens

                    con.Open();
                    try
                    {

                    string cmdText = "DELETE FROM Student WHERE Id = @Id";

                    SqlCommand c = new SqlCommand(cmdText, con);


                    int Idy = Convert.ToInt32(StudentData.Rows[e.RowIndex].Cells[0].Value);
                    c.Parameters.Add(new SqlParameter("@Id", Idy));
                    // execute it

                    int result = c.ExecuteNonQuery();
                    if (result < 0)
                        MessageBox.Show("Error");
                    string cmdText2 = "DELETE FROM Person WHERE Id = @Id";
                    SqlCommand c2 = new SqlCommand(cmdText2, con);
                    c2.Parameters.Add(new SqlParameter("@Id", Idy));
                    c2.ExecuteNonQuery();
                    // connection closed

                    con.Close();

                    StudentData.DataSource = null;
                    StudentData.Rows.Clear();
                    StudentData.Columns.Clear();
                    update();

                    MessageBox.Show("Successfully Deleted");
                }
                    catch (Exception)

                    {

                        MessageBox.Show("This student might be assign to any project. So first delete that record then delete it from here");
                    }

                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }

            if (e.ColumnIndex == 9 && e.RowIndex >= 0 && e.RowIndex != (noOfRows - 1)) // when click on update button
            {
                stId = Convert.ToInt32(StudentData.Rows[e.RowIndex].Cells[0].Value);
                this.Hide();
                Create_Student create = new Create_Student();
                
                create.ShowDialog();
                this.Close();


            }
            else { }
        }
    }
}
