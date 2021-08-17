using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Diploma_v2.WorkWithDB;

namespace Diploma_v2
{
    public partial class StudentAddOrUpdate_Window : Window
    {
        private StudenWindowController _studenWindowController;
        private int Id_stud;
   
        public StudentAddOrUpdate_Window(int i)
        {
            this._studenWindowController = new StudenWindowController();
            this.Id_stud = i;
            InitializeComponent();
        }

        private void LoadComboBoxField_study(object sender, RoutedEventArgs e)
        {
            this._studenWindowController.LoadAllComboBoxStudentWindow(this.Field_Study);

            if (this.Id_stud != -1)
            {
                string s = this._studenWindowController.GetInfoAboutStudent(this.Id_stud);
                string[] str = s.Split('|');
                this.FIO.Text = str[0];
                this.FIOd.Text = str[1];
                if (str[2].Equals('ж'))
                    this.genderG.IsChecked = true;
                else
                    this.genderM.IsChecked = true;
                this.Field_Study.Text = str[3];
                this.dateS.Text = str[4];
                this.dateE.Text = str[5];
                this.Step.Text = str[6];
                this.FormStudy.Text = str[7];
                this.FormPay.Text = str[8];
                this.Group.Text = str[9];
                this.Field_Study.IsDropDownOpen = false;
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
            this._studenWindowController.Search(comboBox);
        }

        private void Add_Or_UpdateStudent(object sender, RoutedEventArgs e)
        {
            string add;
            string date1 = "", date2 = "";
            string[] d = this.dateS.Text.Split('.');
            if (d.Length == 3)
                date1 = d[2] + '-' + d[1] + '-' + d[0];
            d = this.dateE.Text.Split('.');
            if (d.Length == 3)
                date2 = d[2] + '-' + d[1] + '-' + d[0];
            
            add = "'" + this.FIO.Text + "',";
            add += "'" + date1 + "',";
            add += "'" + date2 + "',";
            add += "'" + this.Step.Text + "',";
            add += "'" + this.FormStudy.Text + "',";
            add += "'" + this.FormPay.Text + "',";
            add += "'" + this.FIOd.Text + "',";
            if (this.genderG.IsChecked == true)
                add += "'ж',";
            else if (this.genderM.IsChecked == true)
                add += "'м',";
            add += "'" + this.Group.Text + "',";
            try
            {
                if (this.Id_stud == -1)
                {
                    this._studenWindowController.AddNewStudent(add, this.Field_Study.Text);
                    MessageBox.Show("Добавление успешно!");
                }
                else
                {
                    this._studenWindowController.DeleteStudent(this.Id_stud);
                    this._studenWindowController.AddNewStudent(add, this.Field_Study.Text, Id_stud);
                    MessageBox.Show("Изменения применены!");
                }
            }
            catch 
            {
                MessageBox.Show("Не все поля заполнены");
            }
        }
    }
}