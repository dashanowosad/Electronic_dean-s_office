using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
namespace Diploma_v2.DopWindow.Teacher
{
    public class TeacherWindowController
    {
        private string StudDb = "server=localhost;user=root;database=diplom;password=Crostels325;";
        private LoadInfoTeacher _loadInfoTeacher;
        private AddOrUpdateTeacher _addOrUpdateTeacher;

        public TeacherWindowController()
        {
            this._loadInfoTeacher = new LoadInfoTeacher(this.StudDb);
            this._addOrUpdateTeacher = new AddOrUpdateTeacher(this.StudDb);
        }

        public void LoadComboBox(ComboBox faculty, ComboBox step, ComboBox titles)
        {
            this._loadInfoTeacher.LoadComboBoxFaculty(faculty);
            this._loadInfoTeacher.LoadComboBoxStep(step);
            this._loadInfoTeacher.LoadComboBoxTitles(titles);
        }

        public void Search(ComboBox comboBox)
        {
            string q = "";
            comboBox.Text = comboBox.Text.Replace("\'", "");
            comboBox.Text = comboBox.Text.Replace("\"", "");
            if (comboBox.Name.Equals("Faculty"))
            {
                q = @"SELECT  Название_факультета FROM faculty WHERE 
                Название_факультета LIKE '%" + comboBox.Text + "%'" +
                    " ORDER BY Название_факультета";
            }
            else if (comboBox.Name.Equals("Science_step"))
            {
                q = @"SELECT  Ученая_степень FROM teacher GROUP BY Ученая_степень
                HAVING Ученая_степень LIKE '%" + comboBox.Text + "%'" +
                    " ORDER BY Ученая_степень";
            }
            else
            {
                q = @"SELECT  Звания_должности FROM teacher GROUP BY Звания_должности
                HAVING Звания_должности LIKE '%" + comboBox.Text + "%'" +
                    " ORDER BY Звания_должности";
            }

            this._loadInfoTeacher.FindFromComboBox(comboBox, q);
        }

        public void AddNewTeacher(string add, string disciplines, string facultyText, int idTeach = -1)
        {
            
            string[] s = disciplines.Split(',');
            string q, q2;
            List<int> id_disc = new List<int>();
            int id;
            int id_facult;
            foreach (var i in s)
            {
                id = this._addOrUpdateTeacher.FindDisciplinesId(i);
                if (id == -1)
                {
                    q = "SELECT MAX(id_discipline) FROM discipline";
                    id = this._addOrUpdateTeacher.AddNewDisciplines(i, q);
                }

                id_disc.Add(id);
            }

            id_facult = this._addOrUpdateTeacher.FindFecultyId(facultyText);
            if (id_facult == -1)
            {
                q = "SELECT MAX(id_faculty) FROM faculty";
                id_facult = this._addOrUpdateTeacher.AddNewFaculty(facultyText, q);
            }

            q2 = "SELECT MAX(id_one_teacher) FROM teacher";
            idTeach = this._addOrUpdateTeacher.MaxId(q2);
            foreach (var i in id_disc)
            {
                q = "SELECT MAX(id_teacher) FROM teacher";
                this._addOrUpdateTeacher.AddNewTeacher(q, add, i, id_facult, idTeach);
            }
            
        }

        public void AddNewDiscipline(string disciplines)
        {
            string[] s = disciplines.Split(',');
            string q;
            int id;
            foreach (var i in s)
            {
                id = this._addOrUpdateTeacher.FindDisciplinesId(i);
                if (id == -1)
                {
                    q = "SELECT MAX(id_discipline) FROM discipline";
                    if (i != "")
                        id = this._addOrUpdateTeacher.AddNewDisciplines(i, q);
                }

            }
        }

        public string LoadInformation(int id)
        {
            return this._loadInfoTeacher.LoadInformation(id);
        }

        public void UpdateTeacher(string add, string disciplines, string facultyText, int idTeach)
        {
            this._addOrUpdateTeacher.DeleteTeacher(idTeach);
            this.AddNewTeacher(add, disciplines, facultyText, idTeach);
        }
    }
}