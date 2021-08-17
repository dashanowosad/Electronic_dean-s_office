using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Diploma_v2.DopWindow.AddDiscipline;
using Diploma_v2.DopWindow.DeleteDiscipline;
namespace Diploma_v2
{
    public partial class DelDiscipline_Window : Window
    {
        private LoadComboBoxUpdateDiscipline _loadComboBoxUpdateDiscipline;
        private AddDiscipline _addDiscipline;
        private string StudDb = "server=localhost;user=root;database=diplom;password=Crostels325;";
        public DelDiscipline_Window()
        {
            this._loadComboBoxUpdateDiscipline = new LoadComboBoxUpdateDiscipline(this.StudDb);
            this._addDiscipline = new AddDiscipline(this.StudDb);
            InitializeComponent();
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
            this._loadComboBoxUpdateDiscipline.Search(comboBox);
        }
        private void LoadInfo(object sender, RoutedEventArgs e)
        {
            this._loadComboBoxUpdateDiscipline.Load(this.Discipline);
        }

        private void DelDiscipline(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = this._addDiscipline.DeleteDiscipline(this.Discipline.Text);
             
                this._addDiscipline.DeleteDisciplineFromTeacher(id);
                this._loadComboBoxUpdateDiscipline.Load(this.Discipline);
                MessageBox.Show("Дисциплина успешно удалена!");
            }
            catch 
            {
                MessageBox.Show("Дисциплины не существует");
            }
        }
    }
}