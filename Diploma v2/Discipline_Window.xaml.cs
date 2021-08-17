using System.Windows;
using Diploma_v2.DopWindow.TeacherDisciplin;
namespace Diploma_v2
{
    public partial class Discipline_Window : Window
    {
        private TeacherDisciplineController _teacherDisciplineController;
        private string discipline = "";
        public Discipline_Window(string disciplines)
        {
            this.discipline = disciplines;
            this._teacherDisciplineController = new TeacherDisciplineController();
            InitializeComponent();
        }

        private void LoadInfoDiscipline(object sender, RoutedEventArgs e)
        {
            this._teacherDisciplineController.LoadCheckBoxs(this.StackPanel);
            if (this.discipline != "")
            {
                string[] s = this.discipline.Split(',');
                foreach (var i in s)
                {
                    this._teacherDisciplineController.SelectCheckBox(this.StackPanel, i);
                }
            }
        }

        private void SaveDisciplines(object sender, RoutedEventArgs e)
        {
            this.discipline = this._teacherDisciplineController.SaveDisciplines(this.StackPanel);
            MessageBox.Show("Дисциплины зафиксированы");
        }

        public string GetDisciplines()
        {
            return this.discipline;
        }
    }
}