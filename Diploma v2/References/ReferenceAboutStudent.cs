using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using MessageBox = System.Windows.MessageBox;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;
namespace Diploma_v2.References
{
    public class ReferenceAboutStudent : References
    {
        
        public ReferenceAboutStudent(string StudName, string ScienceWotkName, string ChiefName)
        {
            this.StudentName = StudName;
            this.ScienceWotkName = ScienceWotkName;
            this.ChiefName = ChiefName;
            this.conn = new MySqlConnection(connStr);
            this._saveFileDialog = new SaveFileDialog();
            this._saveFileDialog.Title = "Сохранение справки";
            this._saveFileDialog.DefaultExt = "pdf";
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
            
            this.FindTransferOrder();

            _saveFileDialog.ShowDialog();
            if (_saveFileDialog.FileName == string.Empty)
                return;
            string filename = _saveFileDialog.FileName;
               
            
            this.doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(filename, FileMode.Create));
                
            Rectangle rec = new Rectangle(PageSize.A5.Rotate()); 
            doc.SetPageSize(rec); 
            doc.Open();
            
            this.AddColumns();
            
            doc.Close();
        }

        private bool Check()
        {
            if (this.StudentName.Equals("") || this.ChiefName.Equals("") || this.ScienceWotkName.Equals(""))
            {
                MessageBox.Show("Необходимо заполнить все поля");
                return false;
            }
            

            return true;
        }
        private void AddColumns()
        {
            float[] columnWidths = {2, 2.5f};
            this.table = new PdfPTable(columnWidths);
            this.table.WidthPercentage = 100;
            PdfPCell cell = new PdfPCell();
// 1 строка            
            this.CreateCellText("МИНИСТЕРСТВО ЦИФРОВОГО РАЗВИТИЯ, СВЯЗИ И МАССОВЫХ КОММУНИКАЦИЙ РОССИЙСКОЙ ФЕДЕРАЦИИ",
                7, true, 1);
            
            //пустая
            this.CreateCellText();
            
//2 строка
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance("../../src/image.jpg");
            this.CreateCellText("",0, false, 1, image, 5, 5, 5, 5);

            this.CreateCellText("СПРАВКА",9, false, 0, null, 10, 13);
//3 строка
            this.CreateCellText("ФЕДЕРАЛЬНОЕ ГОСУДАРСТВЕННОЕ БЮДЖЕТНОЕ ОБРАЗОВАТЕЛЬНОЕ УЧРЕЖДЕНИЕ ВЫСШЕГО ОБРАЗОВАНИЯ",
                6, true, 1, null, 10);
            
            //Длинная ячейка
            if (this.gender.Equals("ж"))
                this.CreateCellText("Выдана "+ this.StudentNameDP +" в том, что она является "+ this.step+"кой 3 курса обучающейся по основной профессиональной образовательной программе высшего образования – программе подготовки научно-педагогических кадров в аспирантуре по направлению "+ this.direction+" " + this.formStudy+" формы обучения на "+this.formPay +" основе СибГУТИ (лицензия № 1753 серия 90 Л01 №0008773 от 09 ноября 2015г.). Зачислена приказом "+ this.transferOrder +" г. В соответствии с учебным планом начало обучения с "+ this.dateStart + " г., окончание обучения в аспирантуре "+ this.dateEnd+" г.",
                    12, false, 3, null, 10,0,0,0,9);
            else
                this.CreateCellText("Выдана "+ this.StudentNameDP +" в том, что он является "+ this.step+"ом 3 курса обучающимся по основной профессиональной образовательной программе высшего образования – программе подготовки научно-педагогических кадров в аспирантуре по направлению "+ this.direction+" " + this.formStudy+" формы обучения на "+this.formPay +" основе СибГУТИ (лицензия № 1753 серия 90 Л01 №0008773 от 09 ноября 2015г.). Зачислен приказом "+ this.transferOrder +" г. В соответствии с учебным планом начало обучения с "+ this.dateStart + " г., окончание обучения в аспирантуре "+ this.dateEnd+" г.",
                    12, false, 3, null, 10,0,0,0,9);
//4 строка   
            this.CreateCellText("«СИБИРСКИЙ ГОСУДАРСТВЕННЫЙ УНИВЕРСИТЕТ ТЕЛЕКОММУНИКАЦИЙ И ИНФОРМАТИКИ»",
                8, true, 1, null, 15);
//5 строка
            this.CreateCellText("(СибГУТИ)", 8, true, 1, null);
//6 строка 
            this.CreateCellText("Отдел подготовки кадров высшей квалификации", 7, true, 1, null);
//7 строка
            this.CreateCellText("Кирова ул., д. 86, г. Новосибирск, 630102", 6, false, 1, null);
//8 строка
            this.CreateCellText("т/ф: (383) 269-83-59", 6, false, 1, null);
//9 строка
            this.CreateCellText("http://www.sibsutis.ru", 6, false, 1, null);
//10 строка
            this.CreateCellText("ИНН 5405101327, КПП 540501001, ОКПО 01180010", 6, false, 1, null);
//11 строка
            Chunk ch1 = new Chunk("СПРАВКА №", this.ChangeFont(9, true));
            Chunk ch2 = new Chunk("______от_____________________2021г.", this.ChangeFont(9, false));
            Phrase phrase = new Phrase(ch1);
            phrase.AddRange(new [] {ch2});
            cell = new PdfPCell(
                phrase);
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.PaddingTop = 20;
            cell.Border = 0;
            this.table.AddCell(cell);
//12 строка
            this.CreateCellText();
            
            this.CreateCellText("Справка выдана для предъявления по месту требования.", 12, false, 0, null, 10);
//13 строка
            this.CreateCellText("И. о. проректора по научной работе", 11, false,0, null, 0, 40);
            
            this.CreateCellText(this.ScienceWotkName, 11, false,2, null, 0, 40);
            
//14 строка
            this.CreateCellText("Начальник ОПКВК", 11, false,0, null, 0, 20);
            
            this.CreateCellText(this.ChiefName, 11, false,2, null, 0, 20);
            
            doc.Add(this.table);
        }
    }
}
