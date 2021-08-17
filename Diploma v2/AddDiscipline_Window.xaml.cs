using System.Windows;
using Diploma_v2.DopWindow.AddDiscipline;
namespace Diploma_v2
{
    public partial class AddDiscipline_Window : Window
    {
        private AddDiscipline _addDiscipline;
        private string StudDb = "server=localhost;user=root;database=diplom;password=Crostels325;";
        public AddDiscipline_Window()
        {
            this._addDiscipline = new AddDiscipline(StudDb);
            InitializeComponent();
        }

        private void AddDiscipline(object sender, RoutedEventArgs e)
        {
            if (this.NameDisc.Text == "")
            {
                MessageBox.Show("Введите название прдмета");
                return;
            }

            if (this._addDiscipline.FindDiscipline(this.NameDisc.Text) == -1)
            {
                this._addDiscipline.Add(this.NameDisc.Text);
                MessageBox.Show("Предмет успешно добавлен!");
            }
            
            else
            {
                MessageBox.Show("Данный предмет уже существует");
            }
        }
    }
}