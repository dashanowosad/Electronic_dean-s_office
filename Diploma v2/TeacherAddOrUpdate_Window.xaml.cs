using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Diploma_v2.DopWindow.Teacher;

namespace Diploma_v2
{
    public partial class TeacherAddOrUpdate_Window : Window
    {
        private TeacherWindowController _teacherWindowController;
        private int id_teach;
        private string disciplines = "";
        public TeacherAddOrUpdate_Window(int id)
        {
            this.id_teach = id;
            this._teacherWindowController = new TeacherWindowController();
            InitializeComponent();
        }

        private void LoadComboBox(object sender, RoutedEventArgs e)
        {
            this._teacherWindowController.LoadComboBox(this.Faculty, this.Science_step, this.Titles);
            string s;
            if (this.id_teach != -1)
            {
                s = this._teacherWindowController.LoadInformation(this.id_teach);
                
                string[] str = s.Split('|');
                this.Faculty.Text = str[0];
                this.FIO.Text = str[1];
                this.Science_step.Text = str[2];
                this.Titles.Text = str[3];
                this.disciplines = str[4];
                
                this.disciplines = this.disciplines.Replace(",",  "," + System.Environment.NewLine);
                this.ListDiscipline.Text = this.disciplines;
                this.Titles.IsDropDownOpen = false;
            }
        }

        private void DownList(object sender, KeyEventArgs e)
        {
            ComboBox comboBox = (ComboBox) sender;
            comboBox.IsDropDownOpen = true;
        }

        private void InputComboBoxText(object sender, TextChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox) sender;
            comboBox.IsDropDownOpen = true;
            this._teacherWindowController.Search(comboBox);
        }

        private void ChooseDisciple(object sender, RoutedEventArgs e)
        {
            this.disciplines = this.ListDiscipline.Text;
            this.disciplines = this.disciplines.Replace(System.Environment.NewLine, "");
            
            this._teacherWindowController.AddNewDiscipline(this.disciplines);
            
            Discipline_Window disciplineWindow = new Discipline_Window(this.disciplines);
            disciplineWindow.Owner = this; 
            if (this.id_teach == -1)
                disciplineWindow.Title = "Добавление предметов для преподавателя";
            else
                disciplineWindow.Title = "Редактирование предметов для преподавателя";
            disciplineWindow.Show();
            disciplineWindow.Closed += (s, eventarg) =>
            {
                this.disciplines = disciplineWindow.GetDisciplines();
                this.disciplines = this.disciplines.Replace(",", "," + System.Environment.NewLine);
                this.ListDiscipline.Text = this.disciplines;
            };
        }

        private void AddOrUpdate(object sender, RoutedEventArgs e)
        {
            this.disciplines = this.ListDiscipline.Text;
            this.disciplines = this.disciplines.Replace(System.Environment.NewLine, "");
            string add = "";
            add += "'" + this.FIO.Text + "',";
            add += "'" + this.Science_step.Text + "',";
            add += "'" + this.Titles.Text + "'";

            if (this.id_teach == -1)
            {
                this._teacherWindowController.AddNewTeacher(add, this.disciplines, this.Faculty.Text);
                MessageBox.Show("Добавление успешно!");
            }
            else
            {
                this._teacherWindowController.UpdateTeacher(add, this.disciplines, this.Faculty.Text, this.id_teach);
                MessageBox.Show("Изменения применены!");
            }
        }
    }
}