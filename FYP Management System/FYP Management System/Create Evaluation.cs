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
    public partial class Create_Evaluation : Form
    {
        string conURL = "Data Source=(local);Initial Catalog=ProjectA;User ID=sa;Password=9876145";
        public Create_Evaluation()
        {
            InitializeComponent();
        }
        // require for the purpose of update
        public static int buffer = -1;


        // it is for validation of names, names should contains all alphabets and contain no extra spaces
        private bool isalphaTest(String name)
        {
            if (String.IsNullOrWhiteSpace(name))  // built-in function that checks that is it just null or white space in string
                return false;
            for (int i = 0; i < name.Length; i++)
            {
                if ((Char.IsLetter(name[i])) || (name[i] == ' ' && Char.IsLetter(name[i - 1]) && Char.IsLetter(name[i + 1])))
                    continue;
                else
                    return false;
            }
            return true;
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {

        }
        // when click on save button
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conURL);

            // connection opens
            con.Open();

            int ID = buffer;

            if (ID < 0) // insertion
            {
                try
                {
                    // here check whether boxes are empty for not
                    if (String.IsNullOrEmpty(textBoxName.Text) || String.IsNullOrEmpty(textBoxMarks.Text) || String.IsNullOrEmpty(textBoxWeghtage.Text))
                    { MessageBox.Show("Fill all boxes must"); }
                    else
                    {


                        if (isalphaTest(textBoxName.Text) && Convert.ToInt32(textBoxWeghtage.Text) >= 0 && Convert.ToInt32(textBoxMarks.Text) >= 0)
                        {



                           
                            // command store in string then execute it by passing into sqlcommand object

                            string cmdText = "INSERT INTO Evaluation (Name, TotalMarks, TotalWeightage) VALUES (@Name, @TotalMarks, @TotalWeightage)";

                            SqlCommand c = new SqlCommand(cmdText, con);
                            
                            c.Parameters.Add(new SqlParameter("@Name", textBoxName.Text));
                            c.Parameters.Add(new SqlParameter("@TotalMarks", textBoxMarks.Text));
                            c.Parameters.Add(new SqlParameter("@TotalWeightage", textBoxWeghtage.Text));
                            //execute it
                            int result = c.ExecuteNonQuery();
                            if (result < 0)
                                MessageBox.Show("Error");

                          
                            // show dialog box if added in table of database
                            MessageBox.Show("Successfully Added");
                            // connection closed
                            con.Close();
                            this.Hide();
                            Evaluation datap = new Evaluation();
                            datap.ShowDialog();
                            this.Close(); // close the form
                        }
                        else
                        {
                            throw new ArgumentNullException();
                        }


                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Enter Name,Total marks, total weightage in correct Format!! marks and weightage can't be negative. No extra spaces in names");
                }

            }

            else // updation
            {
                try
                {

                    if (isalphaTest(textBoxName.Text) && Convert.ToInt32(textBoxWeghtage.Text) >= 0 && Convert.ToInt32(textBoxMarks.Text) >= 0)
                    {
                        string cmdText2 = "Update Evaluation SET Name = @Name, TotalMarks = @TotalMarks, TotalWeightage = @TotalWeightage WHERE Id = @Id";
                        SqlCommand c2 = new SqlCommand(cmdText2, con);
                        c2.Parameters.Add(new SqlParameter("@Id", ID));
                        c2.Parameters.Add(new SqlParameter("@Name", textBoxName.Text));
                        c2.Parameters.Add(new SqlParameter("@TotalMarks", textBoxMarks.Text));
                        c2.Parameters.Add(new SqlParameter("@TotalWeightage", textBoxWeghtage.Text));
                        c2.ExecuteNonQuery();
                        MessageBox.Show("Successfully Updated");
                        con.Close();
                        this.Hide();
                        Evaluation datap = new Evaluation();
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
                    MessageBox.Show("Enter Name,Total marks, total weightage in correct Format!! marks and weightage can't be negative. No extra spaces in names");
                }

            }

          

        }

        private void Create_Evaluation_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conURL);

            // connection opens
            
            con.Open();

            buffer = Evaluation.id; // get evalaution id from evaluation form for updation on that id
            if (buffer >= 0)  // for updation show all previous data in textboxes
            {
                string cmdText1 = "SELECT Name, TotalMarks, TotalWeightage FROM Evaluation WHERE Id = @Id";
                SqlCommand c1 = new SqlCommand(cmdText1, con);
                c1.Parameters.Add(new SqlParameter("@Id", buffer));
                SqlDataReader reader1 = c1.ExecuteReader();

                while (reader1.Read())
                {
                    textBoxName.Text = reader1["Name"].ToString();
                    textBoxMarks.Text = reader1["TotalMarks"].ToString();
                    textBoxWeghtage.Text = reader1["TotalWeightage"].ToString();
                }
                reader1.Close();
                Evaluation.id = -1;
            }
            con.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            Evaluation datap = new Evaluation();
            datap.ShowDialog();
            this.Close();// close the form
        }
    }
}
