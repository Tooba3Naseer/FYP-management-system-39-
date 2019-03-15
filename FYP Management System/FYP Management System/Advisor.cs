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
    public partial class Advisor : Form
    {
        string conURL = "Data Source=(local);Initial Catalog=ProjectA;User ID=sa;Password=9876145";
        public Advisor()
        {
            InitializeComponent();
        }
        DataTable dataTable;
        public static int adId = -1;
        private void Advisor_Load(object sender, EventArgs e)
        {
            update();
        }

        public void update()
        {
            dataTable = new DataTable();
            SqlConnection conn = new SqlConnection(conURL);
            conn.Open();
            String str = "SELECT Person.Id, FirstName, LastName,Contact, Email,DateOfBirth,(SELECT Value FROM Lookup WHERE Category = 'GENDER' AND Id = Gender) AS 'Gender', (SELECT Value FROM Lookup WHERE Category = 'DESIGNATION' AND Id = Designation) AS 'Designation', Salary FROM Person Join Advisor ON Person.Id=Advisor.Id";
            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            sda.Fill(dataTable);  // fill the data table with this data

            AdvisorData.DataSource = dataTable; // assign to data grid





            conn.Close();

            //add button column
            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            button.HeaderText = "Delete Data";
            button.Name = "button";
            button.Text = "DELETE";
            button.UseColumnTextForButtonValue = true;
            AdvisorData.Columns.Add(button);

            //add button column
            DataGridViewButtonColumn button1 = new DataGridViewButtonColumn();
            button1.HeaderText = "Update Data";
            button1.Name = "button1";
            button1.Text = "UPDATE";
            button1.UseColumnTextForButtonValue = true;
            AdvisorData.Columns.Add(button1);

            // Configure the details DataGridView so that its columns automatically 
            // adjust their widths when the data changes.
           // AdvisorData.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void Create_Click(object sender, EventArgs e)
        {
            this.Hide();
            CreateAdvisor st = new CreateAdvisor();
            st.ShowDialog();
            this.Close();
        }

        // searching and filtering on the basis of FirstName
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            DataView dataView = new DataView(dataTable);
            dataView.RowFilter = string.Format("FirstName LIKE '%{0}%'", textBoxSearch.Text);
            AdvisorData.DataSource = dataView;
        }

        private void AdvisorData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AdvisorData.DataSource = null;
            AdvisorData.Rows.Clear();
            AdvisorData.Columns.Clear();
            update();
            AdvisorData.Rows[e.RowIndex].ReadOnly = true;


            int noOfRows = AdvisorData.RowCount;
            if (e.ColumnIndex == 9 && e.RowIndex >= 0 && e.RowIndex != (noOfRows - 1)) // when click on del button
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


                    string cmdText = "DELETE FROM Advisor WHERE Id = @Id";

                    SqlCommand c = new SqlCommand(cmdText, con);

                    int Idy = Convert.ToInt32(AdvisorData.Rows[e.RowIndex].Cells[0].Value);
                    c.Parameters.Add(new SqlParameter("@Id", Idy));
                    //execute it

                    int result = c.ExecuteNonQuery();
                    if (result < 0)
                        MessageBox.Show("Error");
                    string cmdText2 = "DELETE FROM Person WHERE Id = @Id";
                    SqlCommand c2 = new SqlCommand(cmdText2, con);
                    c2.Parameters.Add(new SqlParameter("@Id", Idy));
                    c2.ExecuteNonQuery();
                    // connection closed
                   
                    con.Close();

                    AdvisorData.DataSource = null;
                    AdvisorData.Rows.Clear();
                    AdvisorData.Columns.Clear();
                    update();

                    // show dialog box if del
                    MessageBox.Show("Successfully Deleted");

                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }

            if (e.ColumnIndex == 10 && e.RowIndex >= 0 && e.RowIndex != (noOfRows - 1)) // when click on update button
            {
                adId = Convert.ToInt32(AdvisorData.Rows[e.RowIndex].Cells[0].Value);
                this.Hide();
                CreateAdvisor create = new CreateAdvisor();

                create.ShowDialog();
                this.Close();


            }
            else { }
        }

    }
}
