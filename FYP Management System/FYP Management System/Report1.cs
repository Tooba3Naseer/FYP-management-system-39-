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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace FYP_Management_System
{
    public partial class Report1 : Form
    {
        string conURL = "Data Source=(local);Initial Catalog=ProjectA;User ID=sa;Password=9876145";
        public Report1()
        {
            InitializeComponent();
        }

        private void Report1_Load(object sender, EventArgs e)
        {
            DataTable dataTable2 = new DataTable();
            SqlConnection conn = new SqlConnection(conURL);
            conn.Open();
            String str1 = "SELECT Title AS 'Project Title', FirstName+ ' ' + LastName AS 'Advisor Name' ,AssignmentDate AS 'Assignment Date' , (SELECT Value From [Lookup] where [Lookup].Id = AdvisorRole AND Category = 'ADVISOR_ROLE') AS 'Advisor Role' FROM ((Project JOIN ProjectAdvisor ON ProjectId = Id) JOIN Advisor ON AdvisorId = Advisor.Id) JOIN Person ON Person.Id = Advisor.Id";
            SqlCommand cmd2 = new SqlCommand(str1, conn);
            SqlDataAdapter sda2 = new SqlDataAdapter();
            sda2.SelectCommand = cmd2;
            sda2.Fill(dataTable2);
            dataGridreport.DataSource = dataTable2;

            
           /* String str2 = "SELECT Title AS 'Project Title', FirstName+ ' ' + LastName AS 'Student Name' , RegistrationNo AS 'Registration No', (SELECT Value From [Lookup] where [Lookup].Id = Status AND Category = 'Status') AS 'Student Status' FROM (((Project JOIN GroupProject ON ProjectId = Id) JOIN GroupStudent ON GroupStudent.GroupId = GroupProject.GroupId) JOIN Student ON StudentId = Student.Id) JOIN Person ON Person.Id = Student.Id";
            SqlCommand cmd3 = new SqlCommand(str2, conn);
            SqlDataAdapter sda3 = new SqlDataAdapter();
            sda3.SelectCommand = cmd3;
            sda3.Fill(dataTable2);
            Datagridreport2.DataSource = dataTable2;*/
            conn.Close();
        }

        public void gridTOpdf(DataGridView dataGridView, string filename)
        {
            BaseFont baseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
            PdfPTable pdfT = new PdfPTable(dataGridView.Columns.Count);
            pdfT.DefaultCell.Padding = 3;
            pdfT.WidthPercentage = 100;
            pdfT.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfT.DefaultCell.BorderWidth = 1;
            iTextSharp.text.Font txt = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.NORMAL);
            foreach (DataGridViewColumn col in dataGridView.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(col.HeaderText, txt));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                pdfT.AddCell(cell);
            }
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                foreach(DataGridViewCell cell in row.Cells)
                {
                    pdfT.AddCell(new Phrase(cell.Value.ToString(), txt));
                }
            }

            var svfileDialog = new SaveFileDialog();
            svfileDialog.FileName = filename;
            svfileDialog.DefaultExt = ".pdf";
            if(svfileDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(svfileDialog.FileName, FileMode.Create)) 
                {
                    Document doc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(doc, stream);
                    doc.Open();
                    string str = string.Format("Date: {0}", DateTime.Now.Date);
                    
                    Paragraph paragraph = new Paragraph("                                                 Advisor Report\n"+ "                                                 " + str+"\n\n");
                    doc.Add(paragraph);
                    

                    doc.Add(pdfT);
                    doc.Close();
                    stream.Close();
                }
            }
        }
        private void Save_Click(object sender, EventArgs e)
        {

            gridTOpdf(dataGridreport, "report");
            





        }
    }
}
