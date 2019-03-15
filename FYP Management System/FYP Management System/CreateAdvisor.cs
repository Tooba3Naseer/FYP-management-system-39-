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
    public partial class CreateAdvisor : Form
    {
        string conURL = "Data Source=(local);Initial Catalog=ProjectA;User ID=sa;Password=9876145";
        public CreateAdvisor()
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


        // validation for contact no, contact no should contain all digits or - or +
        private bool contactNoValid(String contact)
        {

            int length = contact.Length;
            for (int i = 0; i < length; i++) // condition for last digits
            {
                if (Char.IsDigit(contact[i]) || contact[i] == '-' || contact[i] == '+')
                { }
                else
                    return false;
            }
            return true;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CreateAdvisor_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conURL);

            // connection opens
       
            con.Open();

            buffer = Advisor.adId; // get id from advisor form for update
            if (buffer >= 0)
            {
                String cmdText1 = "SELECT FirstName, LastName,Contact, Email,DateOfBirth FROM Person WHERE Id = @Id";
                SqlCommand c1 = new SqlCommand(cmdText1, con);
                c1.Parameters.Add(new SqlParameter("@Id", buffer));
                SqlDataReader reader1 = c1.ExecuteReader();

                while (reader1.Read())
                {
                    textBoxName.Text = reader1["FirstName"].ToString();
                    textBoxLast.Text = reader1["LastName"].ToString();
                    textBoxContact.Text = reader1["Contact"].ToString();
                    textBoxEmail.Text = reader1["Email"].ToString();

                    dateTimePicker1.Text = reader1["DateOfBirth"].ToString();

                }
                reader1.Close();
                String cmdText2 = "SELECT Salary, (SELECT Value FROM Lookup WHERE Category = 'DESIGNATION' AND Id = Designation) AS 'Designation' FROM Advisor WHERE Id = @Id";
                SqlCommand c2 = new SqlCommand(cmdText2, con);
                c2.Parameters.Add(new SqlParameter("@Id", buffer));
                SqlDataReader reader2 = c2.ExecuteReader();
                while (reader2.Read())
                { textBoxSalary.Text = reader2["Salary"].ToString();
                    comboBox1.Text = reader2["Designation"].ToString();
                }
                reader2.Close();
                Advisor.adId = -1;
            }
            con.Close();
        }

        // when save button clicks
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conURL);

            // connection opens
            con.Open();



            if (buffer < 0) // for creation of new data
            {
                try
                {
                    // here check whether boxes are empty for not
                    if (String.IsNullOrEmpty(textBoxName.Text) || String.IsNullOrEmpty(textBoxEmail.Text) || String.IsNullOrEmpty(comboBox1.Text))
                    { MessageBox.Show("Fill First Name, Email, Designation must"); }
                    else
                    {


                        if (isalphaTest(textBoxName.Text) && (String.IsNullOrEmpty(textBoxLast.Text) || (!String.IsNullOrEmpty(textBoxLast.Text) && (isalphaTest(textBoxLast.Text)))) && Convert.ToDouble(textBoxSalary.Text) >= 0 && (String.IsNullOrEmpty(textBoxContact.Text) || (!String.IsNullOrEmpty(textBoxContact.Text) && contactNoValid(textBoxContact.Text))))
                        {



                            // getting value of radio button
                            string val = "";
                            bool isCheck = male.Checked;
                            if (isCheck)
                                val = male.Text;
                            bool isCheck2 = female.Checked;
                            if (isCheck2)
                                val = female.Text;
                            System.Text.RegularExpressions.Regex expr = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
                            if (expr.IsMatch(textBoxEmail.Text))
                            {

                                string cmdText = "INSERT INTO Person (FirstName,LastName, Contact, Email, DateOfBirth, Gender ) VALUES (@FirstName,@LastName, @Contact, @Email, @DateOfBirth, (SELECT Id FROM Lookup WHERE Category = 'Gender' AND Value = @Value))";

                                SqlCommand c = new SqlCommand(cmdText, con);

                                c.Parameters.Add(new SqlParameter("@FirstName", textBoxName.Text));
                                c.Parameters.Add(new SqlParameter("@LastName", textBoxLast.Text));
                                c.Parameters.Add(new SqlParameter("@Contact", textBoxContact.Text));

                                c.Parameters.Add(new SqlParameter("@Email", textBoxEmail.Text));



                                c.Parameters.Add(new SqlParameter("@DateOfBirth", dateTimePicker1.Text));

                                c.Parameters.Add(new SqlParameter("@Value", val));
                                //execute it
                                int result = c.ExecuteNonQuery();
                                if (result < 0)
                                    MessageBox.Show("Error");



                                string cmdText3 = "INSERT INTO Advisor (Id, Designation, Salary) VALUES((SELECT Id FROM Person WHERE Email = @Email AND Contact = @Contact AND FirstName = @FirstName), (SELECT Id FROM Lookup WHERE Category = 'DESIGNATION' AND Value = @Value), @Salary)";
                                SqlCommand c3 = new SqlCommand(cmdText3, con);
                                c3.Parameters.Add(new SqlParameter("@FirstName", textBoxName.Text));

                                c3.Parameters.Add(new SqlParameter("@Contact", textBoxContact.Text));
                                c3.Parameters.Add(new SqlParameter("@Email", textBoxEmail.Text));
                                c3.Parameters.Add(new SqlParameter("@Value", comboBox1.Text));
                                c3.Parameters.Add(new SqlParameter("@Salary", textBoxSalary.Text));
                                c3.ExecuteNonQuery();

                                // connection closed
                                // show dialog box if added

                                MessageBox.Show("Successfully Added");
                                con.Close();
                                this.Hide();
                                Advisor datap = new Advisor();
                                datap.ShowDialog();
                                this.Close(); // close the form
                            }
                            else
                            {
                                throw new ArgumentNullException();
                            }


                        }
                        else
                        {
                            throw new ArgumentNullException();
                        }


                    }


              


                }
                catch (Exception)
                {
                    MessageBox.Show("Enter Name, Email, Salary in correct Format!!  Name should be all alphabets, no extra spaces. email should have at least 4 chars before @ . contact should have digits");
                }

            }

            else  // for updation of data
            {
                try
                {

                    if (isalphaTest(textBoxName.Text) && (String.IsNullOrEmpty(textBoxLast.Text) || (!String.IsNullOrEmpty(textBoxLast.Text) && (isalphaTest(textBoxLast.Text)))) && Convert.ToDouble(textBoxSalary.Text) >= 0 && (String.IsNullOrEmpty(textBoxContact.Text) || (!String.IsNullOrEmpty(textBoxContact.Text) && contactNoValid(textBoxContact.Text))))
                    {
                        string val = "";
                        bool isCheck = male.Checked;
                        if (isCheck)
                            val = male.Text;
                        bool isCheck2 = female.Checked;
                        if (isCheck2)
                            val = female.Text;
                        System.Text.RegularExpressions.Regex expr = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
                        if (expr.IsMatch(textBoxEmail.Text))
                        {

                            string cmdText2 = "Update Person SET FirstName = @FirstName ,LastName =@LastName , Contact = @Contact, Email = @Email, DateOfBirth = @DateOfBirth, Gender = (SELECT Id FROM Lookup WHERE Category = 'Gender' AND Value = @Value) WHERE Id = @Id";
                            SqlCommand c2 = new SqlCommand(cmdText2, con);
                            c2.Parameters.Add(new SqlParameter("@Id", buffer));
                            c2.Parameters.Add(new SqlParameter("@FirstName", textBoxName.Text));
                            c2.Parameters.Add(new SqlParameter("@LastName", textBoxLast.Text));
                            c2.Parameters.Add(new SqlParameter("@Contact", textBoxContact.Text));

                            c2.Parameters.Add(new SqlParameter("@Email", textBoxEmail.Text));



                            c2.Parameters.Add(new SqlParameter("@DateOfBirth", dateTimePicker1.Text));

                            c2.Parameters.Add(new SqlParameter("@Value", val));

                            c2.ExecuteNonQuery();

                            string cmdText3 = "Update Advisor SET Designation = (SELECT Id FROM Lookup WHERE Category = 'DESIGNATION' AND Value = @Value), Salary = @Salary where Id = @Id";
                            SqlCommand c3 = new SqlCommand(cmdText3, con);
                            c3.Parameters.Add(new SqlParameter("@Id", buffer));
                            c3.Parameters.Add(new SqlParameter("@Value", comboBox1.Text));
                            c3.Parameters.Add(new SqlParameter("@Salary", textBoxSalary.Text));
                            c3.ExecuteNonQuery();
                            MessageBox.Show("Successfully Updated!!");

                            con.Close();
                            this.Hide();
                            Advisor datap = new Advisor();
                            datap.ShowDialog();
                            this.Close(); // close the form
                        }
                        else
                        {
                            throw new ArgumentNullException();
                        }
                    }
                    else
                    {
                        throw new ArgumentNullException();
                    }

                    
                }

                catch (Exception)
                {
                    MessageBox.Show("Enter Name, Email, Salary in correct Format!!  Name should be all alphabets, no extra spaces and Enter Name, Email, Designation must. email should have at least 4 chars before @. contact should have digits!!");
                }
            }

        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            Advisor ps = new Advisor();
            ps.ShowDialog();
            this.Close();
        }
    }
}
