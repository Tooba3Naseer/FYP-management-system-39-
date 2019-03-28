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
    public partial class MarkEvaluationGroup : Form
    {
        string conURL = "Data Source=(local);Initial Catalog=ProjectA;User ID=sa;Password=9876145";
        public MarkEvaluationGroup()
        {
            InitializeComponent();
        }
        DataTable dataTable;
        DataTable dataTable2;
        public static int groupId = -1;
        public static int evaluationId = -1;
        private void MarkEvaluationGroup_Load(object sender, EventArgs e)
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
            button3.HeaderText = "GROUP EVALUATION";
            button3.Name = "button3";
            button3.Text = "EVALUATE";
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
            String str = "SELECT GroupId , EvaluationId, Name, TotalMarks AS 'Total Marks', TotalWeightage AS 'Total Weightage', ObtainedMarks AS 'Obtained Marks', EvaluationDate AS 'Evaluation Date' FROM GroupEvaluation Join Evaluation ON EvaluationId = Id";
            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            sda.Fill(dataTable);  // fill the data table with this data

            groupEvaluationDatagrid.DataSource = dataTable; // assign to data grid

            conn.Close();



            //add button column
            DataGridViewButtonColumn button1 = new DataGridViewButtonColumn();
            button1.HeaderText = "UPDATE DATA";
            button1.Name = "button1";
            button1.Text = "UPDATE";
            button1.UseColumnTextForButtonValue = true;
            groupEvaluationDatagrid.Columns.Add(button1);

            DataGridViewButtonColumn button8 = new DataGridViewButtonColumn();
            button8.HeaderText = "DELETE DATA";
            button8.Name = "button8";
            button8.Text = "DELETE";
            button8.UseColumnTextForButtonValue = true;
            groupEvaluationDatagrid.Columns.Add(button8);


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
                EvaluationGroup create = new EvaluationGroup();

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

        private void groupEvaluationDatagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            groupEvaluationDatagrid.Rows[e.RowIndex].ReadOnly = true;
            int noOfRows = groupEvaluationDatagrid.RowCount;
            if (e.ColumnIndex == 7 && e.RowIndex >= 0 && e.RowIndex != (noOfRows - 1))
            {
                groupId = Convert.ToInt32(groupEvaluationDatagrid.Rows[e.RowIndex].Cells[0].Value);
                evaluationId = Convert.ToInt32(groupEvaluationDatagrid.Rows[e.RowIndex].Cells[1].Value);

                this.Hide();
                EvaluationGroup st = new EvaluationGroup();
                st.ShowDialog();
                this.Close();
            }

            if (e.ColumnIndex == 8 && e.RowIndex >= 0 && e.RowIndex != (noOfRows - 1)) // when click on del button
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure that you want to delete it?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    SqlConnection con = new SqlConnection(conURL);

                    // connection open

                    con.Open();



                    string cmdText = "DELETE FROM GroupEvaluation WHERE GroupId = @GroupId AND EvaluationId =@EvaluationId ";

                    SqlCommand c = new SqlCommand(cmdText, con);


                    int gId = Convert.ToInt32(groupEvaluationDatagrid.Rows[e.RowIndex].Cells[0].Value);
                    int eid = Convert.ToInt32(groupEvaluationDatagrid.Rows[e.RowIndex].Cells[1].Value);
                    c.Parameters.Add(new SqlParameter("@GroupId", gId));
                    c.Parameters.Add(new SqlParameter("@EvaluationId", eid));
                    // execute it

                    c.ExecuteNonQuery();
                    // connection closed

                    con.Close();

                    dataGridgroup.DataSource = null;
                    dataGridgroup.Rows.Clear();
                    dataGridgroup.Columns.Clear();
                    groupEvaluationDatagrid.DataSource = null;
                    groupEvaluationDatagrid.Rows.Clear();
                    groupEvaluationDatagrid.Columns.Clear();
                    update();

                    MessageBox.Show("Successfully Deleted!!");

                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxSearch.Text))
            {
                dataGridgroup.DataSource = null;
                dataGridgroup.Rows.Clear();
                dataGridgroup.Columns.Clear();
                groupEvaluationDatagrid.DataSource = null;
                groupEvaluationDatagrid.Rows.Clear();
                groupEvaluationDatagrid.Columns.Clear();

                dataTable2 = new DataTable();
                SqlConnection conn = new SqlConnection(conURL);
                conn.Open();
                String str1 = "SELECT [Group].Id AS 'Group Id' , Created_On AS 'Created On' FROM ([Group] JOIN GroupEvaluation ON GroupId= Id) Join Evaluation ON EvaluationId = Evaluation.Id where Name = @Name";
                SqlCommand cmd2 = new SqlCommand(str1, conn);
                cmd2.Parameters.Add(new SqlParameter("@Name", textBoxSearch.Text));
                SqlDataAdapter sda2 = new SqlDataAdapter();
                sda2.SelectCommand = cmd2;
                sda2.Fill(dataTable2);
                dataGridgroup.DataSource = dataTable2;

                //add button column
                DataGridViewButtonColumn button3 = new DataGridViewButtonColumn();
                button3.HeaderText = "GROUP EVALUATION";
                button3.Name = "button3";
                button3.Text = "EVALUATE";
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
                String str = "SELECT GroupId , EvaluationId, Name, TotalMarks AS 'Total Marks', TotalWeightage AS 'Total Weightage', ObtainedMarks AS 'Obtained Marks', EvaluationDate AS 'Evaluation Date' FROM GroupEvaluation Join Evaluation ON EvaluationId = Id where Name = @Name";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.Add(new SqlParameter("@Name", textBoxSearch.Text));
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                sda.Fill(dataTable);  // fill the data table with this data

                groupEvaluationDatagrid.DataSource = dataTable; // assign to data grid

                conn.Close();



                //add button column
                DataGridViewButtonColumn button1 = new DataGridViewButtonColumn();
                button1.HeaderText = "UPDATE DATA";
                button1.Name = "button1";
                button1.Text = "UPDATE";
                button1.UseColumnTextForButtonValue = true;
                groupEvaluationDatagrid.Columns.Add(button1);

                DataGridViewButtonColumn button8 = new DataGridViewButtonColumn();
                button8.HeaderText = "DELETE DATA";
                button8.Name = "button8";
                button8.Text = "DELETE";
                button8.UseColumnTextForButtonValue = true;
                groupEvaluationDatagrid.Columns.Add(button8);

            }
            else
            {
                dataGridgroup.DataSource = null;
                dataGridgroup.Rows.Clear();
                dataGridgroup.Columns.Clear();
                groupEvaluationDatagrid.DataSource = null;
                groupEvaluationDatagrid.Rows.Clear();
                groupEvaluationDatagrid.Columns.Clear();
                update();
            }
        }
    }
}
