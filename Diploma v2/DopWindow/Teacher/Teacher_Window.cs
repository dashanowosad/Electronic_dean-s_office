using System.Windows;
using System.Windows.Controls;

namespace Diploma_v2.DopWindow.Teacher
{
    public class Teacher_Window
    {
        private TeacherDB teacher;
        private ComboBoxTeacherContent _comboBoxTeacherContent;
        private ComboBoxFromReferenceExams _comboBoxFromReferenceExams;
        private ComboBoxsFromReferenceAboutStudent _comboBoxsFromReferenceAboutStudent;

        public Teacher_Window(string StudDb)
        {
            this.teacher = new TeacherDB(StudDb);
            this._comboBoxTeacherContent = new ComboBoxTeacherContent(StudDb);
            this._comboBoxFromReferenceExams = new ComboBoxFromReferenceExams(StudDb);
            this._comboBoxsFromReferenceAboutStudent = new ComboBoxsFromReferenceAboutStudent(StudDb);
        }

        public void Insert_New_Teacher_Or_Update(Window MainWindow, DataGrid teacherTable, ComboBox faculty, ComboBox discipline,
            ComboBox disc1, ComboBox disc2, ComboBox disc3, ComboBox chef, ComboBox science_w, int id = -1)
        {
            TeacherAddOrUpdate_Window teacherAddOrUpdateWindow = new TeacherAddOrUpdate_Window(id);
            teacherAddOrUpdateWindow.Owner = MainWindow; 
            if (id == -1)
                teacherAddOrUpdateWindow.Title = "Добавление нового преподавателя";
            else
                teacherAddOrUpdateWindow.Title = "Редактирование преподавателя";
            teacherAddOrUpdateWindow.Show();
            
            teacherAddOrUpdateWindow.Closed += (s, eventarg) =>
            {
                this.teacher.LoadTeacher(teacherTable);
                

                this._comboBoxTeacherContent.SelectFaculty(faculty);
                this._comboBoxTeacherContent.SelectDiscipline(discipline);
                
                this._comboBoxFromReferenceExams.DisciplineComboBox(disc1);
                this._comboBoxFromReferenceExams.DisciplineComboBox(disc2);
                this._comboBoxFromReferenceExams.DisciplineComboBox(disc3);
                
                this._comboBoxsFromReferenceAboutStudent.ScienceWorkANDChiefComboBox(chef);
                this._comboBoxsFromReferenceAboutStudent.ScienceWorkANDChiefComboBox(science_w);
            };
        }
    }
}