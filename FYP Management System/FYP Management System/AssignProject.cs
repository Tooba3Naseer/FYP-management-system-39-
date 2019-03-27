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
    public partial class AssignProject : Form
    {
        string conURL = "Data Source=(local);Initial Catalog=ProjectA;User ID=sa;Password=9876145";
        public static int buffer;
        public AssignProject()
        {
            InitializeComponent();
        }

        private void save_Click(object sender, EventArgs e)
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
                    if (String.IsNullOrEmpty(comboBoxtitle.Text))
                    { MessageBox.Show("Fill title must"); }
                    else
                    {

                        // command store in string then execute it by passing into sqlcommand object

                        string cmdText = "INSERT INTO GroupProject (ProjectId, GroupId, AssignmentDate) VALUES ((SELECT Id FROM Project where Title = @Title),@GroupId, @AssignmentDate)";

                        SqlCommand c = new SqlCommand(cmdText, con);

                        c.Parameters.Add(new SqlParameter("@Title", comboBoxtitle.Text));
                        
                        c.Parameters.Add(new SqlParameter("@GroupId", GroupProject.groupId));
                        
                        c.Parameters.Add(new SqlParameter("@AssignmentDate", DateTime.Now));
                        //execute it
                        int result = c.ExecuteNonQuery();
                        if (result < 0)
                            MessageBox.Show("Error");


                        // show dialog box if added in table of database
                        MessageBox.Show("Successfully Added");
                        con.Close();
                        this.Hide();
                        GroupProject datap = new GroupProject();
                        datap.ShowDialog();
                        this.Close(); // close the form




                    }
                }

                catch (Exception)
                {
                    MessageBox.Show("Enter title in correct Format!!. Title should be from drop down list");
                }

            }

            else
            {
                try
                {

                    if (!String.IsNullOrEmpty(comboBoxtitle.Text))
                    {
                        string cmdText2 = "Update GroupProject SET ProjectId = (SELECT Id FROM Project where Title = @Title), AssignmentDate = @AssignmentDate WHERE ProjectId = @ProjectId AND GroupId = @GroupId";
                        SqlCommand c2 = new SqlCommand(cmdText2, con);
                        c2.Parameters.Add(new SqlParameter("@ProjectId", ID));
                        c2.Parameters.Add(new SqlParameter("@GroupId", GroupProject.groupId));
                        c2.Parameters.Add(new SqlParameter("@Title", comboBoxtitle.Text));
                        c2.Parameters.Add(new SqlParameter("@AssignmentDate", DateTime.Now));
 
                        c2.ExecuteNonQuery();
                        MessageBox.Show("Successfully Updated");
                        con.Close();
                        this.Hide();
                        GroupProject datap = new GroupProject();
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
                    MessageBox.Show("Enter title in correct Format!!. Title should be from drop down list");
                }
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            GroupProject datap = new GroupProject();
            datap.ShowDialog();
            this.Close();// close the form
        }

        private void AssignProject_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conURL);
            con.Open();
            string cmdText1 = "SELECT Title FROM Project";
            SqlCommand c1 = new SqlCommand(cmdText1, con);

            SqlDataReader reader1 = c1.ExecuteReader();

            while (reader1.Read())
            {
                comboBoxtitle.Items.Add(reader1["Title"].ToString());

            }
            reader1.Close();
            buffer = GroupProject.projectId; // get id from advisor form for update.. if we dont make buffer then when we open simple create, it will show data
            if (buffer > 0 && GroupProject.groupId > 0)
            {
                String cmdText5 = "SELECT Title FROM GroupProject Join Project ON ProjectId =Id where ProjectId = @ProjectId AND GroupId = @GroupId";
                SqlCommand c5 = new SqlCommand(cmdText5, con);
                c5.Parameters.Add(new SqlParameter("@GroupId", GroupProject.groupId));
                c5.Parameters.Add(new SqlParameter("@ProjectId", buffer));
                SqlDataReader reader5 = c5.ExecuteReader();

                while (reader5.Read())
                {
                    comboBoxtitle.Text = reader5["Title"].ToString();
                   

                }
                reader5.Close();

                GroupProject.projectId = -1;
            }
            con.Close();
        }
    }
}
