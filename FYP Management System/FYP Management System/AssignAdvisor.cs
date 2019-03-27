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
    public partial class AssignAdvisor : Form
    {
        string conURL = "Data Source=(local);Initial Catalog=ProjectA;User ID=sa;Password=9876145";
        public AssignAdvisor()
        {
            InitializeComponent();
        }
        public static int buffer = -1;
        private void AssignAdvisor_Load(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(conURL);
            con.Open();
            string cmdText1 = "SELECT FirstName FROM Advisor JOIN Person ON Advisor.Id = Person.Id";
            SqlCommand c1 = new SqlCommand(cmdText1, con);
            
            SqlDataReader reader1 = c1.ExecuteReader();

            while (reader1.Read())
            {
                comboBoxNames.Items.Add(reader1["FirstName"].ToString());
                
            }
            reader1.Close();

            string cmdText3 = "SELECT LastName FROM Advisor JOIN Person ON Advisor.Id = Person.Id";
            SqlCommand c3 = new SqlCommand(cmdText3, con);

            SqlDataReader reader3 = c3.ExecuteReader();

            while (reader3.Read())
            {
                comboBoxLast.Items.Add(reader3["LastName"].ToString());

            }
            reader3.Close();

            string cmdText2 = "SELECT Value FROM [Lookup] where Category = 'ADVISOR_ROLE'";
            SqlCommand c2 = new SqlCommand(cmdText2, con);

            SqlDataReader reader2 = c2.ExecuteReader();

            while (reader2.Read())
            {
                comboBoxAdvisor.Items.Add(reader2["Value"].ToString());

            }
            reader2.Close();

            buffer = ProjectAdvisorManagement.AdvisorID; // get id from advisor form for update.. if we dont make buffer then when we open simple create, it will show data
            if (buffer > 0 && ProjectAdvisorManagement.projectID > 0)
            {
                String cmdText5 = "SELECT FirstName, LastName , (SELECT Value FROM [Lookup] where [Lookup].Id = AdvisorRole AND Category = 'ADVISOR_ROLE') AS 'AdvisorRole' FROM (ProjectAdvisor JOIN (Advisor JOIN Person ON Advisor.Id = Person.Id) ON ProjectAdvisor.AdvisorId = Advisor.Id) where AdvisorId = @AdvisorId AND ProjectId = @ProjectId";
                SqlCommand c5 = new SqlCommand(cmdText5, con);
                c5.Parameters.Add(new SqlParameter("@AdvisorId", buffer));
                c5.Parameters.Add(new SqlParameter("@ProjectId", ProjectAdvisorManagement.projectID));
                SqlDataReader reader5 = c5.ExecuteReader();

                while (reader5.Read())
                {
                    comboBoxNames.Text = reader5["FirstName"].ToString();
                    comboBoxLast.Text = reader5["LastName"].ToString();
                    comboBoxAdvisor.Text = reader5["AdvisorRole"].ToString();

                }
                reader5.Close();

                ProjectAdvisorManagement.AdvisorID = -1;
            }
            con.Close();
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
                    if (String.IsNullOrEmpty(comboBoxNames.Text) || String.IsNullOrEmpty(comboBoxAdvisor.Text))
                    { MessageBox.Show("Fill first name and advisor role must"); }
                    else
                    {

                        // command store in string then execute it by passing into sqlcommand object

                        string cmdText = "INSERT INTO ProjectAdvisor (AdvisorId, ProjectId, AdvisorRole, AssignmentDate) VALUES ((SELECT Advisor.Id FROM (Person JOIN Advisor ON Advisor.Id = Person.Id) where FirstName=@FirstName AND LastName = @LastName), @ProjectId, (SELECT Id FROM [Lookup] where Category = 'ADVISOR_ROLE' AND Value = @Value), @AssignmentDate)";

                        SqlCommand c = new SqlCommand(cmdText, con);

                        c.Parameters.Add(new SqlParameter("@FirstName", comboBoxNames.Text));
                        c.Parameters.Add(new SqlParameter("@LastName", comboBoxLast.Text));
                        c.Parameters.Add(new SqlParameter("@ProjectId", ProjectAdvisorManagement.projectID));
                        c.Parameters.Add(new SqlParameter("@Value", comboBoxAdvisor.Text));
                        c.Parameters.Add(new SqlParameter("@AssignmentDate", DateTime.Now));
                        //execute it
                        int result = c.ExecuteNonQuery();
                        if (result < 0)
                            MessageBox.Show("Error");


                        // show dialog box if added in table of database
                        MessageBox.Show("Successfully Added");
                        con.Close();
                        this.Hide();
                        ProjectAdvisorManagement datap = new ProjectAdvisorManagement();
                        datap.ShowDialog();
                        this.Close(); // close the form




                    }
                }

                catch (Exception)
                {
                    MessageBox.Show("Enter name and role in correct Format!!");
                }

            }

            else
            {
                try
                {

                    if (!String.IsNullOrEmpty(comboBoxNames.Text) && !String.IsNullOrEmpty(comboBoxAdvisor.Text))
                    {
                        string cmdText2 = "Update ProjectAdvisor SET AdvisorId = (SELECT Advisor.Id FROM Person JOIN Advisor ON Advisor.Id = Person.Id where FirstName = @FirstName AND LastName = @LastName), AdvisorRole= (SELECT Id FROM [Lookup] where Category = 'ADVISOR_ROLE' AND Value = @Value) WHERE ProjectId = @ProjectId AND AdvisorId = @AdvisorId";
                        SqlCommand c2 = new SqlCommand(cmdText2, con);
                        c2.Parameters.Add(new SqlParameter("@ProjectId", ProjectAdvisorManagement.projectID));
                        c2.Parameters.Add(new SqlParameter("@AdvisorId", ID));
                        c2.Parameters.Add(new SqlParameter("@FirstName", comboBoxNames.Text));
                        c2.Parameters.Add(new SqlParameter("@LastName", comboBoxLast.Text));
                        c2.Parameters.Add(new SqlParameter("@Value", comboBoxAdvisor.Text));
                       



                        c2.ExecuteNonQuery();
                        MessageBox.Show("Successfully Updated");
                        con.Close();
                        this.Hide();
                        ProjectAdvisorManagement datap = new ProjectAdvisorManagement();
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
                    MessageBox.Show("Enter name and role in correct Format!!");
                }
            }
            }

            private void Cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            ProjectAdvisorManagement datap = new ProjectAdvisorManagement();
            datap.ShowDialog();
            this.Close();// close the form
        }

        private void comboBoxLast_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
