using System.Windows.Controls;

namespace Diploma_v2.DopWindow.DisciplineAndMarks
{
    public class DisciplineWindowController
    {
        private string StudDb = "server=localhost;user=root;database=diplom;password=Crostels325;";
        private LoadInfoDiscipline _loadInfoDiscipline;
        private AddOrUpdateDisciplineAndMark _addOrUpdateDisciplineAndMark;

        public DisciplineWindowController()
        {
            this._loadInfoDiscipline = new LoadInfoDiscipline(this.StudDb);
            this._addOrUpdateDisciplineAndMark = new AddOrUpdateDisciplineAndMark(this.StudDb);
        }

        public void LoadAllComboBoxDisciplineAndMark(ComboBox discipline, ComboBox mark)
        {
            this._loadInfoDiscipline.Load_Discipline(discipline);
            this._loadInfoDiscipline.Load_Mark(mark);
        }

        public void Search(ComboBox comboBox)
        {
            string q = "";
            comboBox.Text = comboBox.Text.Replace("\'", "");
            comboBox.Text = comboBox.Text.Replace("\"", "");
            if (comboBox.Name.Equals("Discipline"))
            {
                q = @"SELECT  Название FROM discipline WHERE 
                Название LIKE '%" + comboBox.Text + "%'" +
                    " ORDER BY Название";
            }
            else 
            {
                q = @"SELECT  Отметка FROM rating WHERE 
                Отметка LIKE '%" + comboBox.Text + "%'" +
                    " ORDER BY Отметка";
            }

            this._loadInfoDiscipline.FindFromComboBox(comboBox, q);
        }

        public int FindInSQL(ComboBox comboBox)
        {
            string q = "";
            comboBox.Text = comboBox.Text.Replace("\'", "");
            comboBox.Text = comboBox.Text.Replace("\"", "");
            if (comboBox.Name.Equals("Mark"))
                q = "SELECT id_rating FROM rating WHERE Отметка = '" + comboBox.Text + "'";
            else if (comboBox.Name.Equals("Discipline"))
                q = "SELECT id_discipline FROM discipline WHERE Название = '" + comboBox.Text + "'";
            int id = this._addOrUpdateDisciplineAndMark.Find(q);
            if (id == -1)
            {
                if (comboBox.Name.Equals("Mark"))
                    q = "SELECT MAX(id_rating) FROM rating";
                else if (comboBox.Name.Equals("Discipline"))
                    q = "SELECT MAX(id_discipline) FROM discipline";
                id = this._addOrUpdateDisciplineAndMark.MaxId(q);
                
                if (comboBox.Name.Equals("Mark"))
                    q = "INSERT INTO rating VALUES (" + id +",'" + comboBox.Text + "')";
                else if (comboBox.Name.Equals("Discipline"))
                    q = "INSERT INTO discipline VALUES (" + id +",'" + comboBox.Text + "')";
                this._addOrUpdateDisciplineAndMark.AddDisciplinOrRating(q);
            }
            return id;
        }

        public void FinalAdd(string add)
        {
            this._addOrUpdateDisciplineAndMark.FinalAdd(add);
        }

        public string LoadInfo(int idMark, int idStud)
        {
            return this._loadInfoDiscipline.InfoAboutMark(idMark, idStud);
        }

        public void Update(string add, int idMark)
        {
            this._addOrUpdateDisciplineAndMark.DeleteMark(idMark);
            this._addOrUpdateDisciplineAndMark.FinalAdd(add, idMark);
        }
    }
}