using System.Windows;
using System.Windows.Controls;
namespace Diploma_v2.DopWindow.AddDiscipline
{
    public class AddDisciplineClass_Window
    {
        private ComboBoxTeacherContent _comboBoxTeacherContent;
        private ComboBoxFromReferenceExams _comboBoxFromReferenceExams;
        public AddDisciplineClass_Window(string StudDb)
        {
            this._comboBoxTeacherContent = new ComboBoxTeacherContent(StudDb);
            this._comboBoxFromReferenceExams = new ComboBoxFromReferenceExams(StudDb);
        }

        public void Add_New_Discipline(Window MainWindow, ComboBox discipline, ComboBox disc1, ComboBox disc2, ComboBox disc3)
        {
            Diploma_v2.AddDiscipline_Window addDisciplineWindow = new Diploma_v2.AddDiscipline_Window();
            addDisciplineWindow.Owner = MainWindow;
            addDisciplineWindow.Title = "Добавление нового предмета";
            addDisciplineWindow.Show();
            
            addDisciplineWindow.Closed += (s, eventarg) =>
            {
                this._comboBoxTeacherContent.SelectDiscipline(discipline);
                this._comboBoxFromReferenceExams.DisciplineComboBox(disc1);
                this._comboBoxFromReferenceExams.DisciplineComboBox(disc2);
                this._comboBoxFromReferenceExams.DisciplineComboBox(disc3);
            };
        }
    }
}