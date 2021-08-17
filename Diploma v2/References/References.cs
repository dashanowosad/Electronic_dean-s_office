using System;
using System.IO;
using System.Windows;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
namespace Diploma_v2.References
{
    public class References
    {
        protected MySqlConnection conn;
        protected string connStr = "server=localhost;user=root;database=diplom;password=Crostels325;";
        protected Document doc;
        protected PdfPTable table;

        protected string StudentID;
        protected string StudentName;
        protected string StudentNameDP;
        protected string dateStart;
        protected string dateEnd;
        protected string step;
        protected string formPay;
        protected string formStudy;
        protected string direction;
        protected string gender;
        protected string transferOrder;

        protected string ScienceWotkName;
        protected string ChiefName;

        protected string Discipline1;
        protected string Discipline2;
        protected string Discipline3;
        protected string Com1;
        protected string Com2;
        protected string Com3;
        protected string Mark1;
        protected string Mark2;
        protected string Mark3;
        protected string Date1;
        protected string Date2;
        protected string Date3;

        protected SaveFileDialog _saveFileDialog;
        public References()
        {
            this.conn = new MySqlConnection(connStr);
        }
        protected bool FindInformationAboutStudent()
        {
            this.conn.Open();
            string query = @"SELECT 
                id_student,
                DATE_FORMAT(Дата_поступления, '%d.%m.%Y'), 
                DATE_FORMAT(Дата_окончания, '%d.%m.%Y'), 
                Степень_диплома, 
                Форма_обучения, 
                Форма_оплаты,
                CONCAT(CONCAT(CONCAT(Номер, ' «'), Название), '»'),
                ФИО_дат_падеж,
                пол
            FROM student, field_study
                WHERE student.id_direction = field_study.id_direction AND ФИО = ";
            query += "'" + this.StudentName + "'";
            MySqlCommand command = new MySqlCommand(query, this.conn);
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    this.StudentID = reader[0].ToString();
                    this.dateStart = reader[1].ToString();
                    this.dateEnd = reader[2].ToString();
                    this.step = reader[3].ToString();
                    this.formStudy = reader[4].ToString();
                    this.formPay = reader[5].ToString();
                    this.direction = reader[6].ToString();
                    this.StudentNameDP = reader[7].ToString();
                    this.gender = reader[8].ToString();
                }

                this.formStudy = this.formStudy.Substring(0, this.formStudy.Length - 2) + "ой";
                this.formPay = this.formPay.Substring(0, this.formPay.Length - 2) + "ой";
                
                reader.Close();
                this.conn.Close();
                return true;
            }
            
            reader.Close();
            this.conn.Close();
            return false;
        }

        protected bool FindTeacher(string nameTeacher)
        {
            this.conn.Open();
            string query = @"SELECT 
                id_one_teacher
            FROM teacher WHERE Фамилия_инициалы = ";
            query += "'" +nameTeacher + "' GROUP BY id_one_teacher";
            MySqlCommand command = new MySqlCommand(query, this.conn);
            string id_teach = "";
            try
            {
                id_teach = command.ExecuteScalar().ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                this.conn.Close();
                return false;
            }
            this.conn.Close();
            return true;
        }
        
        public string FindComittee(string Discip)
        {
            
            string Comm = "";
            
            this.conn.Open();
            string query = @"SELECT 
                discipline.id_discipline
            FROM discipline, comittee
               WHERE discipline.id_discipline = comittee.id_discipline GROUP BY Название HAVING Название =";
            query += "'" +Discip + "'";
            MySqlCommand command = new MySqlCommand(query, this.conn);
            string id_discip = "";
            try
            {
                id_discip = command.ExecuteScalar().ToString();
            }
            catch 
            {
                this.conn.Close();
                return "-1";
            }

//председатель            
            query = @"SELECT 
                Фамилия_инициалы,
                Ученая_степень,
                Звания_должности
            FROM comittee
               WHERE Должность_комиссии =";
            query += "'председатель' AND id_discipline = " + id_discip;
            command = new MySqlCommand(query, this.conn);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Comm += "Председатель: " + reader[0].ToString() + ", "
                            + reader[1].ToString() + ", "
                            + reader[2].ToString() + "\nЧлены комиссии: ";
                }
            }
            reader.Close();
//члены комиссии            
            query = @"SELECT 
                Фамилия_инициалы,
                Ученая_степень,
                Звания_должности
            FROM comittee
               WHERE Должность_комиссии =";
            query += "'член комиссии' AND id_discipline = " + id_discip;
            command = new MySqlCommand(query, this.conn);
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Comm += reader[0].ToString() + ", "
                                                 + reader[1].ToString().Replace("-", "") + ", "
                                                 + reader[2].ToString().Replace("-", "") + "; ";
                }

                Comm = Comm.Substring(0, Comm.Length - 1);
            }
            reader.Close();
            this.conn.Close();
            return Comm; 
        }
        
        protected void FindTransferOrder()
        {
            this.conn.Open();
            string query = @"SELECT 
                Номер,
                DATE_FORMAT(Дата, '%d.%m.%Y')
            FROM order_student
               WHERE id_student =";
            query += this.StudentID + " AND Приказ = 'О зачислении'";
            MySqlCommand command = new MySqlCommand(query, this.conn);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                this.transferOrder = reader[0].ToString() + " от " + reader[1].ToString();
            }

            reader.Close();
            
            this.conn.Close();
        }
        
        protected void CreateCellText(string text = "", int size = 0, bool bold = false, int align = 0, Image image = null,
                                        int pl = 0, int pt = 0, int pr = 0, int pb = 0, int rowspan = 1, int border = 0)
        {
            PdfPCell cell;
            if (image == null)
            {
                cell = new PdfPCell(new Paragraph(text, this.ChangeFont(size, bold)));
            }
            else
            {
                image.ScalePercent(40);
                cell = new PdfPCell(image);
            }

            if (border == 0)
                cell.Border = 0;
            cell.HorizontalAlignment = align;
            cell.PaddingLeft = pl;
            cell.PaddingTop = pt;
            cell.PaddingRight = pr;
            cell.PaddingBottom = pb;
            cell.Rowspan = rowspan;
            
            this.table.AddCell(cell);   
        }

        protected void CreateText(string text, int size, bool bold, int aligment = 0)
        {
            Paragraph p = new Paragraph(text, ChangeFont(size, bold));
            p.Alignment = aligment;
            this.doc.Add(p);
        }
        
        protected Font ChangeFont(int size, bool bold)
        {
            string fg = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "times.TTF");
            BaseFont fgBaseFont = BaseFont.CreateFont(fg, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            Font fgFont;
            if (bold)
                fgFont = new Font(fgBaseFont, size, Font.BOLD, BaseColor.BLACK);
            else
                fgFont = new Font(fgBaseFont, size, Font.NORMAL, BaseColor.BLACK);
            return fgFont;
        }
    }
    
    
    
}

