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
    public partial class Evaluation : Form
    {
        string conURL = "Data Source=(local);Initial Catalog=ProjectA;User ID=sa;Password=9876145";
     
        public Evaluation()
        {
            InitializeComponent();
        }

        DataTable dataTable;
        public static int id = -1;
        private void EvaluationData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           

            EvaluationData.Rows[e.RowIndex].ReadOnly = true;


            int noOfRows = EvaluationData.RowCount;
            if (e.ColumnIndex == 4 && e.RowIndex >= 0 && e.RowIndex != (noOfRows - 1))
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


                    string cmdText = "DELETE FROM Evaluation WHERE Id = @Id";

                    SqlCommand c = new SqlCommand(cmdText, con);
                   

                    int Idy = Convert.ToInt32(EvaluationData.Rows[e.RowIndex].Cells[0].Value);
                    c.Parameters.Add(new SqlParameter("@Id", Idy));
                    //execute it

                    int result = c.ExecuteNonQuery();
                    if (result < 0)
                        MessageBox.Show("Error");

                    // connection closed
                    con.Close();

                    // update grid after deletion
                    EvaluationData.DataSource = null;
                    EvaluationData.Rows.Clear();
                    EvaluationData.Columns.Clear();
                    update();

                    // show dialog box if added in table of database
                    MessageBox.Show("Successfully Deleted!!");

                }
                else if (dialogResult == DialogResult.No)
                {
                    
                }
            }

            if (e.ColumnIndex == 5 && e.RowIndex >= 0 && e.RowIndex != (noOfRows - 1))
            {
                id = Convert.ToInt32(EvaluationData.Rows[e.RowIndex].Cells[0].Value);
                this.Hide();
                Create_Evaluation create = new Create_Evaluation();
                create.ShowDialog();
                this.Close();


            }
            else { }
        }

        private void Evaluation_Load(object sender, EventArgs e)
        {
            update();
        }

        // this will load the data of table in data grid view
        public void update()
        {

            dataTable = new DataTable();
            SqlConnection conn = new SqlConnection(conURL);
            conn.Open();
            String str = "SELECT * FROM Evaluation";
            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            sda.Fill(dataTable);  // fill the data table with this data

            EvaluationData.DataSource = dataTable; // assign to data grid




            sda.Update(dataTable);
            conn.Close();

            //add button column
            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            button.HeaderText = "Delete Data";
            button.Name = "button";
            button.Text = "DELETE";
            button.UseColumnTextForButtonValue = true;
            EvaluationData.Columns.Add(button);

            //add button column
            DataGridViewButtonColumn button1 = new DataGridViewButtonColumn();
            button1.HeaderText = "Update Data";
            button1.Name = "button1";
            button1.Text = "UPDATE";
            button1.UseColumnTextForButtonValue = true;
            EvaluationData.Columns.Add(button1);

            
            // adjust their widths when the data changes.
            EvaluationData.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

        }

        private void Create_Click(object sender, EventArgs e)
        {

            this.Hide();
            Create_Evaluation cr = new Create_Evaluation();
            cr.ShowDialog();
            this.Close();
        }

        // searching and filtering on the basis of name
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxSearch.Text))
            {
                EvaluationData.DataSource = null;
                EvaluationData.Rows.Clear();
                EvaluationData.Columns.Clear();

                dataTable = new DataTable();
                SqlConnection conn = new SqlConnection(conURL);
                conn.Open();
                String str = "SELECT * FROM Evaluation where Name = @Name";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.Add(new SqlParameter("@Name", textBoxSearch.Text));
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                sda.Fill(dataTable);  // fill the data table with this data

                EvaluationData.DataSource = dataTable; // assign to data grid




                sda.Update(dataTable);
                conn.Close();

                //add button column
                DataGridViewButtonColumn button = new DataGridViewButtonColumn();
                button.HeaderText = "Delete Data";
                button.Name = "button";
                button.Text = "DELETE";
                button.UseColumnTextForButtonValue = true;
                EvaluationData.Columns.Add(button);

                //add button column
                DataGridViewButtonColumn button1 = new DataGridViewButtonColumn();
                button1.HeaderText = "Update Data";
                button1.Name = "button1";
                button1.Text = "UPDATE";
                button1.UseColumnTextForButtonValue = true;
                EvaluationData.Columns.Add(button1);


                // adjust their widths when the data changes.
                EvaluationData.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);


            }
            else
            {
                EvaluationData.DataSource = null;
                EvaluationData.Rows.Clear();
                EvaluationData.Columns.Clear();
                update();

            }

            }

        private void textBoxSearch_Validated(object sender, EventArgs e)
        {

        }

        private void textBoxSearch_VisibleChanged(object sender, EventArgs e)
        {

        }
    }
}
