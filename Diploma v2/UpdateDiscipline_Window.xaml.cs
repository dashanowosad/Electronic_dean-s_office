using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Diploma_v2.DopWindow.AddDiscipline;
using Diploma_v2.DopWindow.DeleteDiscipline;
namespace Diploma_v2
{
    public partial class UpdateDiscipline_Window : Window
    {
        private LoadComboBoxUpdateDiscipline _loadComboBoxUpdateDiscipline;
        private AddDiscipline _addDiscipline;
        private string StudDb = "server=localhost;user=root;database=diplom;password=Crostels325;";
        public UpdateDiscipline_Window()
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

        private void UpdDiscipline(object sender, RoutedEventArgs e)
        {

            if (this._addDiscipline.FindDiscipline(this.OldDiscipline.Text) == -1)
            {
                MessageBox.Show("Исходного предмета не существует");
                return;
            }
            if (this.OldDiscipline.Text == "" || this.NewDiscipline.Text == "")
            {
                MessageBox.Show("Не все поля заполнены");
                return;
            }
                if (this._addDiscipline.FindDiscipline(this.NewDiscipline.Text) == -1)
            {
                int id = this._addDiscipline.FindDiscipline(this.OldDiscipline.Text);
                this._addDiscipline.DeleteDiscipline(this.OldDiscipline.Text);
                this._addDiscipline.Add(this.NewDiscipline.Text, id);
                MessageBox.Show("Предмет успешно изменен!");
            }
            else
            {
                MessageBox.Show("Данный предмет уже существует");
            }
        }

        private void LoadInfo(object sender, RoutedEventArgs e)
        {
            this._loadComboBoxUpdateDiscipline.Load(this.OldDiscipline);
        }
    }
}