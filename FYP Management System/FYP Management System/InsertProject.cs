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
            try
            {
                // here check whether boxes are empty for not
                if ( String.IsNullOrEmpty(textBoxtitle.Text))
                { MessageBox.Show("Fill title must"); }
                else
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

                    if (isalphaTest(textBoxtitle.Text) && (String.IsNullOrEmpty(textBoxdes.Text) || (!String.IsNullOrEmpty(textBoxdes.Text) && (isalphaTest(textBoxdes.Text)))))
                    {



                        //int id = Convert.ToInt32(textBoxid.Text);
                        // sql command store in string then call it by passing into sqlcommand object

                        string cmdText = "INSERT INTO Project (Description,Title) VALUES (@Description, @Title)";

                        SqlCommand c = new SqlCommand(cmdText, con);
                        //c.Parameters.Add(new SqlParameter("@Id", textBoxid.Text));
                        c.Parameters.Add(new SqlParameter("@Description", textBoxdes.Text));
                        c.Parameters.Add(new SqlParameter("@Title", textBoxtitle.Text));
                        //Read the command and execute it

                        int result = c.ExecuteNonQuery();
                        if (result < 0)
                            MessageBox.Show("Error");

                         // connection closed
                                     // show dialog box if added in table of database
                        MessageBox.Show("Successfully Added");
                    }
                    else
                    {
                        throw new ArgumentNullException();
                    }
                    con.Close();
                    this.Hide();
                      Project datap = new Project();
                      datap.ShowDialog();
                      this.Close(); // close the form
                    
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Enter Description/Title in correct Format!!");
            }

        }
    }
}
