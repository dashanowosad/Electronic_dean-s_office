using System.Windows;
using System.Windows.Controls;
namespace Diploma_v2.DopWindow.DelDiscipline
{
    public class DelDisciplineClass_Window
    {
        private TeacherDB teacher;
        private ComboBoxTeacherContent _comboBoxTeacherContent;
        private ComboBoxFromReferenceExams _comboBoxFromReferenceExams;
        public DelDisciplineClass_Window(string StudDb)
        {
            this.teacher = new TeacherDB(StudDb);
            this._comboBoxTeacherContent = new ComboBoxTeacherContent(StudDb);
            this._comboBoxFromReferenceExams = new ComboBoxFromReferenceExams(StudDb);
        }

        public void Del_Discipline(Window MainWindow, DataGrid teacherTable, ComboBox discipline, ComboBox disc1, ComboBox disc2, ComboBox disc3)
        {
            DelDiscipline_Window delDisciplineWindow = new DelDiscipline_Window();
            delDisciplineWindow.Owner = MainWindow;
            delDisciplineWindow.Title = "Удаление предмета";
            delDisciplineWindow.Show();
            
            delDisciplineWindow.Closed += (s, eventarg) =>
            {
                this.teacher.LoadTeacher(teacherTable);
                
                this._comboBoxTeacherContent.SelectDiscipline(discipline);
                this._comboBoxFromReferenceExams.DisciplineComboBox(disc1);
                this._comboBoxFromReferenceExams.DisciplineComboBox(disc2);
                this._comboBoxFromReferenceExams.DisciplineComboBox(disc3);
                
            };
        }
    }
}