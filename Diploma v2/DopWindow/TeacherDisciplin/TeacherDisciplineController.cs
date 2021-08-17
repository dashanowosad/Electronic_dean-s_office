using System.Windows.Controls;
namespace Diploma_v2.DopWindow.TeacherDisciplin
{
    public class TeacherDisciplineController
    {
        private string StudDb = "server=localhost;user=root;database=diplom;password=Crostels325;";
        private LoadDiscipline loadDiscipline;

        public TeacherDisciplineController()
        {
            this.loadDiscipline = new LoadDiscipline(this.StudDb);
        }
        public void LoadCheckBoxs(StackPanel stackPanel)
        {
            this.loadDiscipline.LoadDisciplines(stackPanel);
        }

        public string SaveDisciplines(StackPanel stackPanel)
        {
            string str = "";
            for (int i = 0; i < stackPanel.Children.Count; ++i)
            {
                CheckBox checkBox = (CheckBox) stackPanel.Children[i];
                if (checkBox.IsChecked == true)
                    str += checkBox.Content + ",";
            }

            str = str.Substring(0, str.Length - 1);
            return str;
        }

        public void SelectCheckBox(StackPanel stackPanel, string s)
        {
            for (int i = 0; i < stackPanel.Children.Count; ++i)
            {
                CheckBox checkBox = (CheckBox) stackPanel.Children[i];
                if (checkBox.Content.Equals(s))
                    checkBox.IsChecked = true;
            }
        }
    }
}