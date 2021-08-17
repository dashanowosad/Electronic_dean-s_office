using System.Windows.Controls;

namespace Diploma_v2.WorkWithDB
{
    public class StudenWindowController
    {
        string StudDb = "server=localhost;user=root;database=diplom;password=Crostels325;";
        private LoadInfo _loadComboBox;
        private AddOrUpdateStudent _addOrUpdateStudent;

        public StudenWindowController()
        {
            this._loadComboBox = new LoadInfo(this.StudDb);
            this._addOrUpdateStudent = new AddOrUpdateStudent(this.StudDb);
        }

        public void LoadAllComboBoxStudentWindow(ComboBox faculty)
        {
            this._loadComboBox.Load_Direction(faculty);
        }

        public void Search(ComboBox comboBox)
        {
            string q = "";
            comboBox.Text = comboBox.Text.Replace("\'", "");
            comboBox.Text = comboBox.Text.Replace("\"", "");
            q = @"SELECT Номер, Название FROM field_study WHERE 
                Название LIKE '%" + comboBox.Text + "%'" + 
                "OR Номер LIKE '%" + comboBox.Text + "%'" +
                " ORDER BY Название"; 
            this._loadComboBox.FindFromComboBox(comboBox, q);
            
        }

        public void AddNewStudent(string add, string direction, int idStud = -1)
        {
            int faculty_id = this._addOrUpdateStudent.FindFaculty(direction);
            if (faculty_id == -1)
                faculty_id = this._addOrUpdateStudent.AddDirection(direction);
            add += faculty_id;
            
            this._addOrUpdateStudent.AddStudent(add, idStud);
        }

        public string GetInfoAboutStudent(int idStud)
        {
            return this._loadComboBox.InfoAboutStudent(idStud);
        }

        public void DeleteStudent(int idStud)
        {
            this._addOrUpdateStudent.DeleteStudent(idStud);
        }
    }
}