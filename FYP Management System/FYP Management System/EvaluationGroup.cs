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
    public partial class EvaluationGroup : Form
    {
        string conURL = "Data Source=(local);Initial Catalog=ProjectA;User ID=sa;Password=9876145";
        public static int buffer = -1;
        public EvaluationGroup()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void EvaluationGroup_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conURL);
            con.Open();
            string cmdText1 = "SELECT Name FROM Evaluation";
            SqlCommand c1 = new SqlCommand(cmdText1, con);

            SqlDataReader reader1 = c1.ExecuteReader();

            while (reader1.Read())
            {
                comboBoxNames.Items.Add(reader1["Name"].ToString());

            }
            reader1.Close();
            buffer = MarkEvaluationGroup.evaluationId; // get id from advisor form for update.. if we dont make buffer then when we open simple create, it will show data
            if (buffer > 0 && MarkEvaluationGroup.groupId > 0)
            {
                String cmdText5 = "SELECT Name, ObtainedMarks FROM GroupEvaluation Join Evaluation ON EvaluationId =Id where EvaluationId = @EvaluationId AND GroupId = @GroupId";
                SqlCommand c5 = new SqlCommand(cmdText5, con);
                c5.Parameters.Add(new SqlParameter("@GroupId", MarkEvaluationGroup.groupId));
                c5.Parameters.Add(new SqlParameter("@EvaluationId", buffer));
                SqlDataReader reader5 = c5.ExecuteReader();

                while (reader5.Read())
                {
                    comboBoxNames.Text = reader5["Name"].ToString();
                    textBoxMarks.Text = reader5["ObtainedMarks"].ToString();

                }
                reader5.Close();

                MarkEvaluationGroup.evaluationId = -1;
            }
            con.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            MarkEvaluationGroup datap = new MarkEvaluationGroup();
            datap.ShowDialog();
            this.Close();// close the form
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conURL);

            // connection opens
            con.Open();

            int ID = buffer;

            if (ID < 0)
            {
                try
                {

                    // here check whether boxes are empty for not
                    if (String.IsNullOrEmpty(comboBoxNames.Text) || String.IsNullOrEmpty(textBoxMarks.Text) || Convert.ToInt32(textBoxMarks.Text) < 0)
                    { MessageBox.Show("Fill all boxes must and Marks should'nt be negative"); }
                    else
                    {
                        string cmdText1 = "SELECT TotalMarks FROM Evaluation where Name = @Name";
                        SqlCommand c1 = new SqlCommand(cmdText1, con);
                        c1.Parameters.Add(new SqlParameter("@Name", comboBoxNames.Text));

                        SqlDataReader reader1 = c1.ExecuteReader();

                        while (reader1.Read())
                        {

                            if (Convert.ToInt32(reader1["TotalMarks"]) >= Convert.ToInt32(textBoxMarks.Text))
                            {
                                // command store in string then execute it by passing into sqlcommand object




                            }
                            else
                            {
                                throw new ArgumentNullException();
                            }

                        }
                        reader1.Close();
                        string cmdText = "INSERT INTO GroupEvaluation (GroupId, EvaluationId, ObtainedMarks , EvaluationDate) VALUES (@GroupId,(SELECT Id FROM Evaluation where Name = @Name), @ObtainedMarks, @EvaluationDate)";

                        SqlCommand c = new SqlCommand(cmdText, con);

                        c.Parameters.Add(new SqlParameter("@Name", comboBoxNames.Text));
                        c.Parameters.Add(new SqlParameter("@ObtainedMarks", textBoxMarks.Text));
                        c.Parameters.Add(new SqlParameter("@GroupId", MarkEvaluationGroup.groupId));

                        c.Parameters.Add(new SqlParameter("@EvaluationDate", DateTime.Now));

                        //execute it
                        int result = c.ExecuteNonQuery();
                        if (result < 0)
                            MessageBox.Show("Error");


                        // show dialog box if added in table of database
                        MessageBox.Show("Successfully Added!!");
                        con.Close();
                        this.Hide();
                        MarkEvaluationGroup datap = new MarkEvaluationGroup();
                        datap.ShowDialog();
                        this.Close(); // close the form




                    }
                }

                catch (Exception)
                {
                    MessageBox.Show("Enter Name in correct Format!!. Name should be from drop down list.. Obtained Marks should be less then total Marks");
                }

            }

            else
            {
                try
                {
                    // here check whether boxes are empty for not
                    if (String.IsNullOrEmpty(comboBoxNames.Text) || String.IsNullOrEmpty(textBoxMarks.Text) || Convert.ToInt32(textBoxMarks.Text) < 0)
                    { MessageBox.Show("Fill all boxes must and Marks should'nt be negative"); }

                    if(!String.IsNullOrEmpty(comboBoxNames.Text) && !String.IsNullOrEmpty(textBoxMarks.Text) && Convert.ToInt32(textBoxMarks.Text) >= 0)
                    {

                        string cmdText1 = "SELECT TotalMarks FROM Evaluation where Name = @Name";
                        SqlCommand c1 = new SqlCommand(cmdText1, con);
                        c1.Parameters.Add(new SqlParameter("@Name", comboBoxNames.Text));

                        SqlDataReader reader1 = c1.ExecuteReader();

                        while (reader1.Read())
                        {

                            if (Convert.ToInt32(reader1["TotalMarks"]) >= Convert.ToInt32(textBoxMarks.Text))
                            {
                                // command store in string then execute it by passing into sqlcommand object




                            }
                            else
                            {
                                throw new ArgumentNullException();
                            }

                        }
                        reader1.Close();
                        string cmdText2 = "Update GroupEvaluation SET EvaluationId = (SELECT Id FROM Evaluation where Name = @Name), ObtainedMarks = @ObtainedMarks WHERE EvaluationId = @EvaluationId AND GroupId = @GroupId";
                        SqlCommand c2 = new SqlCommand(cmdText2, con);
                        c2.Parameters.Add(new SqlParameter("@EvaluationId", ID));
                        c2.Parameters.Add(new SqlParameter("@GroupId",MarkEvaluationGroup.groupId ));
                        c2.Parameters.Add(new SqlParameter("@Name", comboBoxNames.Text));
                        c2.Parameters.Add(new SqlParameter("@ObtainedMarks", textBoxMarks.Text));

                        c2.ExecuteNonQuery();
                        MessageBox.Show("Successfully Updated!!");
                        con.Close();
                        this.Hide();
                        MarkEvaluationGroup datap = new MarkEvaluationGroup();
                        datap.ShowDialog();
                        this.Close(); // close the form

                    }
                
                    else
                    {
                        throw new ArgumentNullException();
                    }
                }
                

                catch (Exception)
                {
                    MessageBox.Show("Enter Name in correct Format!!. Name should be from drop down list.. Obtained Marks should be less then total Marks");
                }
            }

        }
    }
}
