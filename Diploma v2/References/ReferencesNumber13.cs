using System;
using System.IO;
using System.Windows;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
namespace Diploma_v2.References
{
    public class ReferencesNumber13 : References
    {
        public ReferencesNumber13(string StudName, string Dist1, string Dist2, string Dist3,
            string M1, string M2, string M3, string D1, string D2, string D3,
            string ScienceWotkName, string ChiefName)
        {
            
            this.StudentName = StudName;
            this.Discipline1 = Dist1;
            this.Discipline2 = Dist2;
            this.Discipline3 = Dist3;
            this.Mark1 = M1;
            this.Mark2 = M2;
            this.Mark3 = M3;
            this.Date1 = D1;
            this.Date2 = D2;
            this.Date3 = D3;
            this.ScienceWotkName = ScienceWotkName;
            this.ChiefName = ChiefName;
            
            this.conn = new MySqlConnection(connStr);
            this.Com1 = this.FindComittee(this.Discipline1);
            this.Com2 = this.FindComittee(this.Discipline2);
            this.Com3 = this.FindComittee(this.Discipline3);
            this._saveFileDialog = new SaveFileDialog();
            this._saveFileDialog.Filter = "pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
            
            if (!this.Check())
                return;
            
            if (!this.FindInformationAboutStudent())
            {
                MessageBox.Show("Студента не существует");
                return;
            }
            
            if (!this.FindTeacher(this.ScienceWotkName))
            {
                MessageBox.Show("Проректора по научной работе не существует");
                return;
            }
            
            if (!this.FindTeacher(this.ChiefName))
            {
                MessageBox.Show("Начальника отдела ОПКВК не существует");
                return;
            }
            
            
            _saveFileDialog.ShowDialog();
            if (_saveFileDialog.FileName == string.Empty)
                return;
            string filename = _saveFileDialog.FileName;

            this.doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(filename, FileMode.Create));
            // Rectangle rec = new Rectangle(PageSize.A4.Rotate()); 
            // doc.SetPageSize(rec); 
            doc.Open();
            
            this.AddText();
            
            doc.Close();
        }
        
        private bool Check()
        {
            if (this.Discipline1.Equals("") || this.Discipline2.Equals("") || this.Discipline3.Equals("") || 
                this.StudentName.Equals("") || this.ScienceWotkName.Equals("") || this.ChiefName.Equals("") ||
                this.Mark1.Equals("") || this.Mark2.Equals("") || this.Mark3.Equals("") ||
                this.Date1.Equals("") || this.Date2.Equals("") || this.Date3.Equals(""))
            {
                MessageBox.Show("Не все поля заполнены");
                return false;
            }
            
            if (this.Com1.Equals("-1") || this.Com2.Equals("-1") || this.Com3.Equals("-1"))
            {
                MessageBox.Show("Предмета или комиссии по нему не существует");
                return false;
            }

            return true;
        }
        private void AddText()
        { 
//шапка            
            this.CreateText("Федеральное агентство связи", 12, false, 1);
            this.CreateText("Федеральное государственное бюджетное учреждение высшего образования", 12, false, 1);
            this.CreateText("«Сибирский государственный университет телекоммуникаций и информатики»", 12, false, 1);
            this.CreateText("Новосибирск 630102 ул. Кирова 86", 12, false, 1);
            this.CreateText("Тел. (383)2698359, факс (383) 2698359", 12, false, 1);
            this.CreateText("E-mail: aspirant@sibsutis.ru", 12, false, 1);
            this.CreateText(Environment.NewLine+"СПРАВКА № 13", 12, false, 1);

            this.CreateText("Форма 2.2", 12, false, 2);
            if (this.gender.Equals("ж"))
                this.CreateText("Выдана " + this.StudentNameDP + " в том, что она сдала кандидатские экзамены по специальности " + this.direction + " и получила следующие оценки:", 12, false);
            else
                this.CreateText("Выдана " + this.StudentNameDP + " в том, что он сдал кандидатские экзамены по специальности " + this.direction + " и получил следующие оценки:", 12, false);
//таблица            
            float[] columnWidths = {0.3f, 0.28f, 0.49f};
            this.table = new PdfPTable(columnWidths);
            this.table.WidthPercentage = 100;
            this.table.SpacingBefore = 20;
            
            //1 строка
            this.CreateCellText("Наименование дисциплины",12, false, 1, null, 10, 0, 0, 10, 1, 1);
            this.CreateCellText("Оценка и дата сдачи экзамена",12, false, 1, null, 10, 0, 0, 10, 1, 1);
            this.CreateCellText("Фамилия, инициалы, ученая степень, звания и должности председателя и членов комиссии",12,
                false, 1, null, 10, 0, 10, 10, 1, 1);
    //2 строка
            this.CreateCellText("1. " +this.Discipline1,12, false, 0, null, 10, 0, 0, 0, 1, 1);
            this.CreateCellText(this.Mark1 + Environment.NewLine + this.Date1 + " г.",12, false, 0, null, 10, 0, 0, 0, 1, 1);
            this.CreateCellText(this.Com1,12, false, 0, null, 10, 0, 0, 5, 1, 1);
    //3 строка
            this.CreateCellText("2. " +this.Discipline2,12, false, 0, null, 10, 0, 0, 0, 1, 1);
            this.CreateCellText(this.Mark2 + Environment.NewLine + this.Date2+ " г.",12, false, 0, null, 10, 0, 0, 0, 1, 1);
            this.CreateCellText(this.Com2,12, false, 0, null, 10, 0, 0, 5, 1, 1);
    //4 строка
            this.CreateCellText("3. " +this.Discipline3,12, false, 0, null, 10, 0, 0, 0, 1, 1);
            this.CreateCellText(this.Mark3 + Environment.NewLine + this.Date1+ " г.",12, false, 0, null, 10, 0, 0, 0, 1, 1);
            this.CreateCellText(this.Com3,12, false, 0, null, 10, 0, 0, 5, 1, 1);
    
            doc.Add(this.table);
            
//завершающая часть
            this.CreateText("Выдана на основании подлинных протоколов, хранящихся в архиве учебного заведения по месту сдачи экзамена.", 12, false);
            
            this.table = new PdfPTable(2);
            this.table.WidthPercentage = 100;
            this.table.SpacingBefore = 20;
            
    //проректор        
            this.CreateCellText("Проректор по научной работе",12);
            this.CreateCellText(this.ScienceWotkName,12, false, 2);
    //начальник
            this.CreateCellText("Начальник отдела ОПКВК",12,false,0, null, 0, 20);
            this.CreateCellText(this.ChiefName,12, false, 2, null, 0, 20);
            
            doc.Add(this.table);
            
//дата
            this.CreateText(Environment.NewLine + DateTime.Now.ToLongDateString(), 12, false, 2);
        }
    }
}