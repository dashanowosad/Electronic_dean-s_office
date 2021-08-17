using System.Windows;
using System.Windows.Controls;

namespace Diploma_v2.WorkWithDB
{
    public class Student_Window
    {
        private StudentDB _studentDb;
        private ComboBoxsFromReferenceAboutStudent _comboBoxsFromReferenceAboutStudent;
        public Student_Window(string connStr)
        {
            this._studentDb = new StudentDB(connStr);
            this._comboBoxsFromReferenceAboutStudent = new ComboBoxsFromReferenceAboutStudent(connStr);
        }
        public void Insert_New_Student_Or_Update(Window MainWindow, DataGrid studenGrid, ComboBox stComboBox, int id = -1)
        {
            
            StudentAddOrUpdate_Window studentAddOrUpdateWindow = new StudentAddOrUpdate_Window(id);
            studentAddOrUpdateWindow.Owner = MainWindow; 
            if (id == -1)
                studentAddOrUpdateWindow.Title = "Добавление нового студента";
            else
                studentAddOrUpdateWindow.Title = "Редактирование студента";
            studentAddOrUpdateWindow.Show();
            
            studentAddOrUpdateWindow.Closed += (s, eventarg) =>
            {
                this._studentDb.LoadStudent(studenGrid);
                this._comboBoxsFromReferenceAboutStudent.StudentComboBox(stComboBox);
            };
        }
    }
}