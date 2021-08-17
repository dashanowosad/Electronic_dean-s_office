using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Diploma_v2.DopWindow.DisciplineAndMarks;

namespace Diploma_v2
{
    public partial class DisciplineAddOrUpdate_Window : Window
    {
        private int id_stud;
        private int id_mark;
        private DisciplineWindowController _disciplineWindowController;
        public DisciplineAddOrUpdate_Window(int idStud, int idMark)
        {
            this.id_stud = idStud;
            this.id_mark = idMark;
            this._disciplineWindowController = new DisciplineWindowController();
            InitializeComponent();
        }

        private void LoadAllForms(object sender, RoutedEventArgs e)
        {
            this._disciplineWindowController.LoadAllComboBoxDisciplineAndMark(this.Discipline, this.Mark);
            string str;
            if (id_mark != -1)
            {
                 str = this._disciplineWindowController.LoadInfo(this.id_mark, this.id_stud);
                 string[] s = str.Split('|');
                 this.Discipline.Text = s[0];
                 this.Period.Text = s[1];
                 this.Type.Text = s[2];
                 this.Mark.Text = s[3];
                 this.Date.Text = s[4];
                 this.Mark.IsDropDownOpen = false;
            }
        }

        private void InputComboBoxText(object sender, TextChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox) sender;
            comboBox.IsDropDownOpen = true;
            this._disciplineWindowController.Search(comboBox);
        }

        private void DownList(object sender, KeyEventArgs e)
        {
            ComboBox comboBox = (ComboBox) sender;
            comboBox.IsDropDownOpen = true;
        }
        
        private void Add_Or_UpdateDisciplineAndMark(object sender, RoutedEventArgs e)
        {
            int id_mark = this._disciplineWindowController.FindInSQL(this.Mark);
            int id_discipline = this._disciplineWindowController.FindInSQL(this.Discipline);

            string date1;
            string[] d = this.Date.Text.Split('.');
            date1 = d[2] + '-' + d[1] + '-' + d[0];
            
            string add = "";
            add += id_discipline + ", ";
            add += "'" + this.Period.Text + "',";
            add += "'" + this.Type.Text + "', ";
            add += id_mark + ", ";
            add += "'" + date1 + "',";
            add += this.id_stud;

            if (this.id_mark == -1)
            {
                this._disciplineWindowController.FinalAdd(add);
                MessageBox.Show("Добавление успешно!");
            }
            else
            {
                this._disciplineWindowController.Update(add, this.id_mark);
                MessageBox.Show("Изменения применены!");
            }
        }
    }
}