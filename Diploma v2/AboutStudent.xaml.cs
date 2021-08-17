using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Diploma_v2.DopWindow.Statistic;
namespace Diploma_v2
{
    public partial class AboutStudent : Window
    {
        private int flag;
        private string id = "-1";
        private LoadComboBoxStatistic _loadComboBoxStatistic;
        private GetStatistic getStatistic;
        string StudDb = "server=localhost;user=root;database=diplom;password=Crostels325;";
        public AboutStudent(int flag)
        {
            this.flag = flag;
            this._loadComboBoxStatistic = new LoadComboBoxStatistic(this.StudDb);
            this.getStatistic = new GetStatistic(this.StudDb);
            InitializeComponent();
        }

        private void InputComboBoxText(object sender, TextChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox) sender;
            comboBox.IsDropDownOpen = true;
            string q ="";
            if (comboBox.Name.Equals("Field_study"))
            {
                q = @"SELECT Номер, Название FROM field_study WHERE 
                Название LIKE '%" + comboBox.Text + "%'" + 
                    "OR Номер LIKE '%" + comboBox.Text + "%'" +
                    " ORDER BY Название"; 
                this._loadComboBoxStatistic.Search(q, this.Field_study);
            }
            
            if (comboBox.Name.Equals("Student") && id.Equals("-1"))
            {
                q = @"SELECT ФИО FROM student WHERE ФИО LIKE '%" + comboBox.Text + "%'" + " ORDER BY ФИО"; 
                this._loadComboBoxStatistic.LoadComboBoxs(q, this.Student);
            }
            else if (comboBox.Name.Equals("Student") && !id.Equals("-1"))
            {
                q = @"SELECT ФИО FROM student WHERE id_direction = " + id + 
                    " AND ФИО LIKE '%" + comboBox.Text + "%'" + " ORDER BY ФИО"; 
                this._loadComboBoxStatistic.LoadComboBoxs(q, this.Student);
            }

            if (comboBox.Name.Equals("Group") && id.Equals("-1"))
            {
                q = @"SELECT Группа FROM student GROUP BY Группа ORDER BY Группа"; 
                this._loadComboBoxStatistic.LoadComboBoxs(q, this.Group);
            }
            else if (comboBox.Name.Equals("Group") && !id.Equals("-1"))
            {
                q = @"SELECT Группа FROM student WHERE id_direction = " + id +" GROUP BY Группа ORDER BY Группа"; 
                this._loadComboBoxStatistic.LoadComboBoxs(q, this.Group);
            }
        }

        private void DownList(object sender, KeyEventArgs e)
        {
            ComboBox comboBox = (ComboBox) sender;
            comboBox.IsDropDownOpen = true;
        }

        private void LoadInfo(object sender, RoutedEventArgs e)
        {
            string q; 
            if (flag == 0)
            {
                this.WrapPanel.Visibility = Visibility.Visible;
                q = "SELECT ФИО FROM student ORDER BY ФИО";
                this._loadComboBoxStatistic.LoadComboBoxs(q, this.Student);
            }
            q = "SELECT Группа FROM student GROUP BY Группа ORDER BY Группа";
            this._loadComboBoxStatistic.LoadComboBoxs(q, this.Group);
            q = "SELECT Номер, Название FROM field_study ORDER BY Название";
            this._loadComboBoxStatistic.LoadComboBoxs(q, this.Field_study);
        }

        private void ChangeField_Study(object sender, SelectionChangedEventArgs e)
        {
            string s = this.Field_study.Text;
            string id = "-1";
            string q;
            if (s.Contains("-"))
            {
                string[] str = s.Split('-');
                str[0] = str[0].Replace(" ", "");
                id = this._loadComboBoxStatistic.SelectIdField_Study(str[0]);
            }
            if (id.Equals("-1"))
            {
                q = @"SELECT ФИО FROM student ORDER BY ФИО"; 
                this._loadComboBoxStatistic.LoadComboBoxs(q, this.Student);
                
                q = @"SELECT Группа FROM student GROUP BY Группа ORDER BY Группа"; 
                this._loadComboBoxStatistic.LoadComboBoxs(q, this.Group);
            }
            else if (!id.Equals("-1"))
            {
                q = @"SELECT ФИО FROM student WHERE id_direction = " + id + 
                    " ORDER BY ФИО"; 
                this._loadComboBoxStatistic.LoadComboBoxs(q, this.Student);
                
                q = @"SELECT Группа FROM student WHERE id_direction = " + id +" GROUP BY Группа ORDER BY Группа"; 
                this._loadComboBoxStatistic.LoadComboBoxs(q, this.Group);
            }

            this.id = id;
        }
        
        private void ChangeGroup(object sender, SelectionChangedEventArgs e)
        {
            string s = this.Group.Text;
            string q;
            if (!this.Group.Text.Equals("Все"))
            {
                q = @"SELECT ФИО FROM student WHERE Группа = '" + s +
                           "' ORDER BY ФИО";
                this._loadComboBoxStatistic.LoadComboBoxs(q, this.Student);
            }
            else
            {
                if (!this.id.Equals("-1"))
                {
                    q = @"SELECT ФИО FROM student WHERE id_direction = " + id +
                        " ORDER BY ФИО";
                    this._loadComboBoxStatistic.LoadComboBoxs(q, this.Student);
                }
                else
                {
                    q = @"SELECT ФИО FROM student  ORDER BY ФИО";
                    this._loadComboBoxStatistic.LoadComboBoxs(q, this.Student);
                }
            }
        }

        private void GetResut(object sender, RoutedEventArgs e)
        {
            if (this.Field_study.Text.Equals("") || this.Group.Text.Equals(""))
            {
                MessageBox.Show("Не все поля заполнены");
                return;
            }
            if (this.Student.Text.Equals("Все") && this.Group.Text.Equals("Все"))
            {
                MessageBox.Show("Не верное заполнение");
                return;
            }
//средний балл
            if (this.flag == 0)
            {
                if (this.Student.Text.Equals(""))
                {
                    MessageBox.Show("Не все поля заполнены");
                    return;
                }
                    //по студенту            
                if (!this.Student.Text.Equals("Все"))
                    this.getStatistic.SrStudent(this.Result, this.Student.Text);
    //по группе
                if (this.Student.Text.Equals("Все"))
                {
                    string s = this.getStatistic.GetStudentInGroup(this.Group.Text);
                    string[] str = s.Split('|');
                    float res = 0;
                    foreach (var i in str)
                    {
                        res += this.getStatistic.SrStudent(this.Result, i);
                    }

                    res /= str.Length;
                    this.Result.Text = res.ToString();
                }
                    
            }
//кол-во отличников            
            else if (this.flag == 1)
            {
                if (!this.Group.Text.Equals("Все"))
                {
                    string s = this.getStatistic.GetStudentInGroup(this.Group.Text);
                    string[] str = s.Split('|');
                    int res = 0;
                    foreach (var i in str)
                    {
                        res += this.getStatistic.FiveStudent(i);
                    }

                    this.Result.Text = res.ToString();

                }
                else
                {
                    MessageBox.Show("Не верное заполнение");
                }
            }
//кол-во не успевающих
            else if (this.flag == 2)
            {
                if (!this.Group.Text.Equals("Все"))
                {
                    string s = this.getStatistic.GetStudentInGroup(this.Group.Text);
                    string[] str = s.Split('|');
                    int res = 0;
                    foreach (var i in str)
                    {
                        res += this.getStatistic.NoFiveStudent(i);
                    }

                    this.Result.Text = res.ToString();

                }
                else
                {
                    MessageBox.Show("Не верное заполнение");
                }
            }
        }
    }
}