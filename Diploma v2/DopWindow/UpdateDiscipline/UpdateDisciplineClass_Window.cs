using System.Windows;
using System.Windows.Controls;
namespace Diploma_v2.DopWindow.DeleteDiscipline
{
    public class UpdateDisciplineClass_Window
    {
        private ComboBoxTeacherContent _comboBoxTeacherContent;
        private ComboBoxFromReferenceExams _comboBoxFromReferenceExams;
        public UpdateDisciplineClass_Window(string StudDb)
        {
            this._comboBoxTeacherContent = new ComboBoxTeacherContent(StudDb);
            this._comboBoxFromReferenceExams = new ComboBoxFromReferenceExams(StudDb);
        }

        public void Update_Discipline(Window MainWindow, ComboBox discipline, ComboBox disc1, ComboBox disc2, ComboBox disc3)
        {
            UpdateDiscipline_Window updateDisciplineWindow = new UpdateDiscipline_Window();
            updateDisciplineWindow.Owner = MainWindow;
            updateDisciplineWindow.Title = "Изменение предмета предмета";
            updateDisciplineWindow.Show();
            
            updateDisciplineWindow.Closed += (s, eventarg) =>
            {
                this._comboBoxTeacherContent.SelectDiscipline(discipline);
                this._comboBoxFromReferenceExams.DisciplineComboBox(disc1);
                this._comboBoxFromReferenceExams.DisciplineComboBox(disc2);
                this._comboBoxFromReferenceExams.DisciplineComboBox(disc3);
            };
        }
    }
}