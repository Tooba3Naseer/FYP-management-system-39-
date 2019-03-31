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
    public partial class InsertProject : Form
    {
        string conURL = "Data Source=(local);Initial Catalog=ProjectA;User ID=sa;Password=9876145";
        public InsertProject()
        {
            InitializeComponent();
        }
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
        private void Save_Click(object sender, EventArgs e)
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
                    if (String.IsNullOrEmpty(textBoxtitle.Text))
                    { MessageBox.Show("Fill title must"); }
                    else
                    {


                        if (isalphaTest(textBoxtitle.Text) && (String.IsNullOrEmpty(textBoxdes.Text) || (!String.IsNullOrEmpty(textBoxdes.Text) && (isalphaTest(textBoxdes.Text)))))
                        {




                            // command store in string then execute it by passing into sqlcommand object

                            string cmdText = "INSERT INTO Project (Description,Title) VALUES (@Description, @Title)";

                            SqlCommand c = new SqlCommand(cmdText, con);
                            
                            c.Parameters.Add(new SqlParameter("@Description", textBoxdes.Text));
                            c.Parameters.Add(new SqlParameter("@Title", textBoxtitle.Text));
                            //execute it
                            int result = c.ExecuteNonQuery();
                            if (result < 0)
                                MessageBox.Show("Error");

                            
                            // show dialog box if added in table of database
                            MessageBox.Show("Successfully Added");
                            con.Close();
                            this.Hide();
                            Project datap = new Project();
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
                    MessageBox.Show("Enter Description/Title in correct Format!!.. Title , Description contains all alphabets");
                }

            }

            else
            {
                try
                {
                    
                    if (isalphaTest(textBoxtitle.Text) && (String.IsNullOrEmpty(textBoxdes.Text) || (!String.IsNullOrEmpty(textBoxdes.Text) && (isalphaTest(textBoxdes.Text)))))
                    {
                        string cmdText2 = "Update Project SET Description = @Description, Title = @Title WHERE Id = @Id";
                        SqlCommand c2 = new SqlCommand(cmdText2, con);
                        c2.Parameters.Add(new SqlParameter("@Id", ID));
                        c2.Parameters.Add(new SqlParameter("@Description", textBoxdes.Text));


                        c2.Parameters.Add(new SqlParameter("@Title", textBoxtitle.Text));
                        c2.ExecuteNonQuery();
                        MessageBox.Show("Successfully Updated");
                        con.Close();
                        this.Hide();
                        Project datap = new Project();
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
                    MessageBox.Show("Enter Description/Title in correct Format!!.. Title , Description contains all alphabets");
                }

            }

       
        }

        private void InsertProject_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conURL);

            // connection opens
            // purpose of checker it to find whether user enter all data in correct format or not
            // if not in correct format then show exception and handle in catch secti
            con.Open();

            buffer = Project.id;
            if (buffer >= 0)
            {
                string cmdText1 = "SELECT Description, Title FROM Project WHERE Id = @Id";
                SqlCommand c1 = new SqlCommand(cmdText1, con);
                c1.Parameters.Add(new SqlParameter("@Id", buffer));
                SqlDataReader reader1 = c1.ExecuteReader();

                while (reader1.Read())
                {
                    textBoxdes.Text = reader1["Description"].ToString();
                    textBoxtitle.Text = reader1["Title"].ToString();
                }
                reader1.Close();
                Project.id = -1;
            }
            con.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            Project datap = new Project();
            datap.ShowDialog();
            this.Close();// close the form
        }
    }
}
