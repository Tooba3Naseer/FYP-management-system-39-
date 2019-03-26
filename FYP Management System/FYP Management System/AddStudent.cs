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
    public partial class AddStudent : Form
    {
        string conURL = "Data Source=(local);Initial Catalog=ProjectA;User ID=sa;Password=9876145";
        public static int buffer = -1;
        public AddStudent()
        {
            InitializeComponent();
        }

        // validation for regNo, regNo pattern should be like '2016-CS-888' or 2017-IME-333'
        private bool isRegNovalid(String regno)
        {
            bool check = false;
            int length = regno.Length;
            if (length == 9 || length == 10 || length >= 11)  // it should have these lengths
            {
                for (int i = 0; i < 4; i++)
                {
                    if (Char.IsDigit(regno[i])) // condition for first 4 charcaters
                        check = true;
                    else
                        return false;

                }
                // condition for dashes and dept name like CS or CE
                if ((regno[4] == '-') && Char.IsLetter(regno[5]) && Char.IsLetter(regno[6]) && (Char.IsLetter(regno[7]) || regno[7] == '-') && (Char.IsDigit(regno[8]) || regno[8] == '-'))
                    check = true;
                else
                    return false;
                for (int i = 9; i < length; i++) // condition for last digits
                {
                    if (Char.IsDigit(regno[i]))
                        check = true;
                    else
                        return false;
                }

            }
            return check;
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
                    if (String.IsNullOrEmpty(textBoxRegNo.Text) || String.IsNullOrEmpty(comboBox1.Text))
                    { MessageBox.Show("Fill all boxes must"); }
                    else
                    {


                        if (isRegNovalid(textBoxRegNo.Text))
                        {


                        int groupID = StudentgroupManagment.groupID;
                        DateTime now = DateTime.Now;

                        // command store in string then execute it by passing into sqlcommand object

                        string cmdText = "INSERT INTO GroupStudent (GroupId, StudentId, Status, AssignmentDate) VALUES (@GroupId,(SELECT Id FROM Student where RegistrationNo = @RegistrationNo) ,(SELECT Id FROM [Lookup] where Category = 'STATUS' AND Value = @Value), @AssignmentDate )";

                            SqlCommand c = new SqlCommand(cmdText, con);
                            
                            c.Parameters.Add(new SqlParameter("@GroupId", groupID));
                            c.Parameters.Add(new SqlParameter("@RegistrationNo", textBoxRegNo.Text));
                            c.Parameters.Add(new SqlParameter("@Value", comboBox1.Text));
                            c.Parameters.Add(new SqlParameter("@AssignmentDate", now));
                           //execute it
                            int result = c.ExecuteNonQuery();
                            if (result < 0)
                                MessageBox.Show("Error");

                            
                            // show dialog box if added in table of database
                            MessageBox.Show("Successfully Added");
                            StudentgroupManagment.groupID = -1;
                            con.Close();
                            this.Hide();
                            StudentgroupManagment sgm= new StudentgroupManagment();
                            sgm.ShowDialog();
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
                    MessageBox.Show("Enter RegNo in correct Format and RegNo should be present in Student record!!");
                }

            }

            else
            {
                try
                {

                    if (!String.IsNullOrEmpty(textBoxRegNo.Text) && !String.IsNullOrEmpty(comboBox1.Text) && isRegNovalid(textBoxRegNo.Text))
                    {
                        string cmdText2 = "Update GroupStudent SET StudentId = (SELECT Id FROM Student where RegistrationNo = @RegistrationNo), Status= (SELECT Id FROM [Lookup] where Category = 'STATUS' AND Value = @Value) WHERE StudentId = @StudentId AND GroupId = @GroupId";
                        SqlCommand c2 = new SqlCommand(cmdText2, con);
                        c2.Parameters.Add(new SqlParameter("@StudentId", ID));
                        c2.Parameters.Add(new SqlParameter("@GroupId", StudentgroupManagment.groupID));
                        
                        c2.Parameters.Add(new SqlParameter("@Value", comboBox1.Text));
                        c2.Parameters.Add(new SqlParameter("@RegistrationNo", textBoxRegNo.Text));


                        
                        c2.ExecuteNonQuery();
                        MessageBox.Show("Successfully Updated");
                        con.Close();
                        this.Hide();
                        StudentgroupManagment datap = new StudentgroupManagment();
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
                    MessageBox.Show("Enter RegNo in correct Format and RegNo should be present in Student record!!");
                }

            } 
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            StudentgroupManagment sgm = new StudentgroupManagment();
            sgm.ShowDialog();
            this.Close(); // close the form
        }

        private void AddStudent_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conURL);

            // connection opens

            con.Open();

            buffer = StudentgroupManagment.stID; // get id from advisor form for update.. if we dont make buffer then when we open simple create, it will show data
            if (buffer > 0 && StudentgroupManagment.groupID > 0)
            {
                String cmdText1 = "SELECT (SELECT RegistrationNo FROM Student where StudentId = Id) AS 'reg no', (SELECT Value FROM [Lookup] where Id = Status AND Category = 'STATUS') AS 'status' FROM GroupStudent WHERE StudentId = @StudentId AND GroupId = @GroupId";
                SqlCommand c1 = new SqlCommand(cmdText1, con);
                c1.Parameters.Add(new SqlParameter("@StudentId", buffer));
                c1.Parameters.Add(new SqlParameter("@GroupId", StudentgroupManagment.groupID));
                SqlDataReader reader1 = c1.ExecuteReader();

                while (reader1.Read())
                {
                    textBoxRegNo.Text = reader1["reg no"].ToString();
                    comboBox1.Text = reader1["status"].ToString();
           

                }
                reader1.Close();
        
                StudentgroupManagment.stID = -1;
            }
            con.Close();
        }
    }
}
