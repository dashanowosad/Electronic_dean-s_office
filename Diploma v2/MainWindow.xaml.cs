using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Diploma_v2.DopWindow.AreYouShure;
using Diploma_v2.References;

namespace Diploma_v2
{
    public partial class MainWindow
    {
        private DataBaseController dbControll;
        private FillAndWorkWithComittee _fillAndWorkWithComittee;
        private Shure_Window _shureWindow;
        public MainWindow()
        {
            this.dbControll = new DataBaseController();
            this._fillAndWorkWithComittee = new FillAndWorkWithComittee();
            this._shureWindow = new Shure_Window();
            
            InitializeComponent();
        }

        private void TabItemStud(object sender, RoutedEventArgs e)
        {
            this.dbControll.LoadStud(this.TableStud);
        }

        private void ClearTables(object sender, RoutedEventArgs e)
        {
            this.dbControll.RebootAll(this.TableStud, this.TableTeacher, this.ComboBoxFaculty, this.ComboBoxDiscipline);
        }

        private void GetInfoFromStudent(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            DataRowView dataRow = (DataRowView)this.TableStud.SelectedItem;
            
            if (dataRow != null)
            {
                
                string id_stud = dataRow.Row.ItemArray[0].ToString();
                this.dbControll.LoadMark(this.TableMark, id_stud);
                this.dbControll.LoadOrder(this.TableOrder, id_stud);
            }
        }

        private void TabItemTeach(object sender, RoutedEventArgs e)
        {
            this.dbControll.LoadTeach(this.TableTeacher, this.ComboBoxFaculty, this.ComboBoxDiscipline);
        }
        
        private void DisplayTableByFaculty(object sender, SelectionChangedEventArgs e)
        {
            if (this.ComboBoxFaculty.Items.Count != 0)
            {
                string faculty_name = this.ComboBoxFaculty.SelectedValue.ToString();
                this.dbControll.ReloadTeacherTable(this.TableTeacher, faculty_name);
            }
        }

        private void FindTeacher(object sender, TextChangedEventArgs e)
        {
            string column_name = this.ComboBoxSearch.SelectedValue.ToString();
            column_name = column_name.Replace("System.Windows.Controls.ComboBoxItem: ", "");
            string value = this.TextBoxSearch.Text;
            value = value.Replace("'", "");
            value = value.Replace("\"", "");
            this.dbControll.FindTeacher(this.TableTeacher, column_name, value);
        }

        private void AddTeacherToComittee(object sender, RoutedEventArgs e)
        {
            string status = this.ComboBoxStatus.SelectedValue.ToString();
            status = status.Replace("System.Windows.Controls.ComboBoxItem: ", "");
            this._fillAndWorkWithComittee.AddToTable(this.TableCommittee, this.TableTeacher, status);
        }

        private void DeleteOneMemberOfComittee(object sender, RoutedEventArgs e)
        {
            this._fillAndWorkWithComittee.DeleteOneFromComittee(this.TableCommittee);
        }

        private void DeleteAllFromComittee(object sender, RoutedEventArgs e)
        {
            this._fillAndWorkWithComittee.DeleteAll(this.TableCommittee);
        }

        private void CreateComittee(object sender, RoutedEventArgs e)
        {
            string discipline = this.ComboBoxDiscipline.Text;
            this.dbControll.AddNewComittee(this, this.TableCommittee, discipline);
            this._fillAndWorkWithComittee.ClearList();
            
            this.TabItemReference(sender, e);
        }

        private void TabItemReference(object sender, RoutedEventArgs e)
        {
            this.dbControll.LoadReferenceItems(this.ComboBoxRefStud, this.ComboBoxRefScienceWork, this.ComboBoxRefChief,
                this.Discipline1, this.Mark1, this.Discipline2, this.Mark2,
                this.Discipline3, this.Mark3);
        }
        
        private void InputStudText(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            ComboBox comboBox = (ComboBox) sender;
            comboBox.IsDropDownOpen = true;
            this.dbControll.SearchReferenceItemsAboutStudent(comboBox);
        }

        private void DownList(object sender, KeyEventArgs e)
        {
            ComboBox comboBox = (ComboBox) sender;
            comboBox.IsDropDownOpen = true;
        }

        private void CreateReferenceAboutStudent(object sender, RoutedEventArgs e)
        {
            string StudentName = this.ComboBoxRefStud.Text;
            string ScienceWotkName = this.ComboBoxRefScienceWork.Text;
            string ChiefName = this.ComboBoxRefChief.Text;
            ReferenceAboutStudent referenceAboutStudent =
                new ReferenceAboutStudent(StudentName, ScienceWotkName, ChiefName);
        }
        
        private void CreateReferenceNumber13(object sender, RoutedEventArgs e)
        {
            string StudentName = this.ComboBoxRefStud.Text;
            string Discipline1 = this.Discipline1.Text;
            string Discipline2 = this.Discipline2.Text;
            string Discipline3 = this.Discipline3.Text;
            
            string Mark1 = this.Mark1.Text;
            string Mark2 = this.Mark2.Text;
            string Mark3 = this.Mark3.Text;

            string Date1 = this.datePicker1.Text;
            string Date2 = this.datePicker1.Text;
            string Date3 = this.datePicker1.Text;
            string ScienceWotkName = this.ComboBoxRefScienceWork.Text;
            string ChiefName = this.ComboBoxRefChief.Text;
            ReferencesNumber13 referencesNumber13 = new ReferencesNumber13(StudentName,
                Discipline1, Discipline2, Discipline3,
                Mark1, Mark2, Mark3,
                Date1, Date2, Date3,
                ScienceWotkName, ChiefName);
        }
        
        private void InputExamsText(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            ComboBox comboBox = (ComboBox) sender;
            comboBox.IsDropDownOpen = true;
            this.dbControll.SearchReferenceItemsExams(comboBox);
        }

        private void ChangeButtonEnabled(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox) sender;

            if (comboBox.Name.Contains("1"))
                this.Comittee1.IsEnabled = true;
            if (comboBox.Name.Contains("2"))
                this.Comittee2.IsEnabled = true;
            if (comboBox.Name.Contains("3"))
                this.Comittee3.IsEnabled = true;
        }

        private void Ins_Student(object sender, RoutedEventArgs e)
        {
            this.TableMark.ItemsSource = null;
            this.TableOrder.ItemsSource = null;
            this.dbControll.Ins_Student(this, this.TableStud, this.ComboBoxRefStud);
        }
        
        private void Upd_Student(object sender, RoutedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)this.TableStud.SelectedItem;
            int id_stud = -1;
            try
            {
                id_stud = (int) dataRow.Row.ItemArray[0];

                this.dbControll.Upd_Student(this, this.TableStud, this.ComboBoxRefStud, id_stud);
                this.TableMark.ItemsSource = null;
                this.TableOrder.ItemsSource = null;
            }
            catch (Exception e1)
            {
                MessageBox.Show("Выберите студента");
            }
        }

        private void Del_Student(object sender, RoutedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)this.TableStud.SelectedItem;
            int id_stud = -1;
            try
            {
                id_stud = (int) dataRow.Row.ItemArray[0];

                this._shureWindow.Are_You_Shure(this, "Вы действительно хотите удалить студента?");
                if (this._shureWindow.GetResult().Equals("Yes"))
                {
                    this.TableMark.ItemsSource = null;
                    this.TableOrder.ItemsSource = null;
                    this.dbControll.Del_Student(id_stud, this.TableStud);
                    MessageBox.Show("Студент успешно удален!");
                }
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message);
                MessageBox.Show("Выберите студента");
            }
        }

        private void Ins_Discipline(object sender, RoutedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)this.TableStud.SelectedItem;
            int id_stud = -1;
            try
            {
                id_stud = (int) dataRow.Row.ItemArray[0];
                this.dbControll.Ins_Discipline(this, id_stud, this.TableMark);
            }
            catch
            {
                MessageBox.Show("Выберите студента");
            }
        }

        private void Upd_Discipline(object sender, RoutedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)this.TableStud.SelectedItem;
            DataRowView dataRow2 = (DataRowView)this.TableMark.SelectedItem;
            int id_stud = -1;
            int id_mark = -1;
            try
            {
                id_stud = (int) dataRow.Row.ItemArray[0];
               
                id_mark = (int) dataRow2.Row.ItemArray[0];
                this.dbControll.Ins_Discipline(this, id_stud, this.TableMark, id_mark);
            }
            catch
            {
                if (id_stud == -1)
                    MessageBox.Show("Выберите студента");
                else if (id_mark == -1)
                    MessageBox.Show("Выберите предмет/оценку");
            }
        }

        private void Del_Discipline(object sender, RoutedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)this.TableStud.SelectedItem;
            DataRowView dataRow2 = (DataRowView)this.TableMark.SelectedItem;
            int id_stud = -1;
            int id_mark = -1;
            try
            {
                id_stud = (int) dataRow.Row.ItemArray[0];
                id_mark = (int) dataRow2.Row.ItemArray[0];
                
                this._shureWindow.Are_You_Shure(this, "Вы действительно хотите удалить предмет/оценку?");
                if (this._shureWindow.GetResult().Equals("Yes"))
                {
                    this.dbControll.Del_Mark(id_stud, id_mark, this.TableMark);
                    MessageBox.Show("Предмет/оценка успешно удален(а)!");
                }
            }
            catch 
            {
                if (id_stud == -1)
                    MessageBox.Show("Выберите студента");
                else if (id_mark == -1)
                    MessageBox.Show("Выберите предмет/оценку");
            }
        }

        private void Ins_Order(object sender, RoutedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)this.TableStud.SelectedItem;
            int id_stud = -1;
            try
            {
                id_stud = (int) dataRow.Row.ItemArray[0];
                this.dbControll.Ins_Order(this, id_stud, this.TableOrder);
            }
            catch
            {
                MessageBox.Show("Выберите студента");
            }
        }

        private void Upd_Order(object sender, RoutedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)this.TableStud.SelectedItem;
            DataRowView dataRow2 = (DataRowView)this.TableOrder.SelectedItem;
            int id_stud = -1;
            int id_order = -1;
            try
            {
                id_stud = (int) dataRow.Row.ItemArray[0];
               
                id_order = (int) dataRow2.Row.ItemArray[0];
                this.dbControll.Upd_Order(this, id_stud, this.TableOrder, id_order);
            }
            catch
            {
                if (id_stud == -1)
                    MessageBox.Show("Выберите студента");
                else if (id_order == -1)
                    MessageBox.Show("Выберите приказ");
            }
        }

        private void Del_Order(object sender, RoutedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)this.TableStud.SelectedItem;
            DataRowView dataRow2 = (DataRowView)this.TableOrder.SelectedItem;
            int id_stud = -1;
            int id_order = -1;
            try
            {
                id_stud = (int) dataRow.Row.ItemArray[0];
                id_order = (int) dataRow2.Row.ItemArray[0];
                this._shureWindow.Are_You_Shure(this, "Вы действительно хотите удалить приказ?");
                if (this._shureWindow.GetResult().Equals("Yes"))
                {
                    this.dbControll.Del_Order(id_stud, id_order, this.TableOrder);
                    MessageBox.Show("Приказ успешно удален!");
                }
            }
            catch 
            {
                if (id_stud == -1)
                    MessageBox.Show("Выберите студента");
                else if (id_order == -1)
                    MessageBox.Show("Выберите приказ");
            }
        }

        private void Ins_Teacher(object sender, RoutedEventArgs e)
        {
            this.dbControll.Ins_Teacher(this, this.TableTeacher, this.ComboBoxFaculty, this.ComboBoxDiscipline,
                this.Discipline1, this.Discipline2, this.Discipline3, 
                this.ComboBoxRefChief, this.ComboBoxRefScienceWork);
        }

        private void Upd_Teacher(object sender, RoutedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)this.TableTeacher.SelectedItem;
            int id_teach = -1;
            try
            {
                id_teach = (int) dataRow.Row.ItemArray[0];
                
                this.dbControll.Upd_Teacher(this, this.TableTeacher, this.ComboBoxFaculty, ComboBoxDiscipline,
                    this.Discipline1, this.Discipline2, this.Discipline3,
                    this.ComboBoxRefChief, this.ComboBoxRefScienceWork, id_teach);
            }
            catch (Exception e1)
            {
               
                MessageBox.Show("Выберите преподавателя");
            }
        }
        
        private void Del_Teacher(object sender, RoutedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)this.TableTeacher.SelectedItem;
            int id_teach = -1;
            try
            {
                id_teach = (int) dataRow.Row.ItemArray[0];

                this._shureWindow.Are_You_Shure(this, "Вы действительно хотите удалить преподавателя?");
                if (this._shureWindow.GetResult().Equals("Yes"))
                {
                    this.dbControll.Del_Teacher(id_teach, this.TableTeacher, this.ComboBoxFaculty, this.ComboBoxDiscipline,
                        this.ComboBoxRefChief, this.ComboBoxRefScienceWork);
                    MessageBox.Show("Преподаватель успешно удален!");
                }
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message);
                MessageBox.Show("Выберите Преподавателя");
            }
        }

        private void Ins_Disc(object sender, RoutedEventArgs e)
        {
            this.dbControll.Ins_Disc(this, this.ComboBoxDiscipline, this.Discipline1, this.Discipline2, this.Discipline3);
        }

        private void Upd_Disc(object sender, RoutedEventArgs e)
        {
            this.dbControll.Upd_Disc(this, this.ComboBoxDiscipline, this.Discipline1, this.Discipline2,
                this.Discipline3);
        }

        private void Del_Disc(object sender, RoutedEventArgs e)
        {
            this.dbControll.Del_Disc(this, this.TableTeacher, this.ComboBoxDiscipline, this.Discipline1, this.Discipline2,
                this.Discipline3);
        }

        private void ShowOneComittee(object sender, RoutedEventArgs e)
        {
            Button button = (Button) sender;
            string discipline = "";
            if (button.Name.Contains("1"))
                discipline = this.Discipline1.Text;
            else if (button.Name.Contains("2"))
                discipline = this.Discipline2.Text;
            else if (button.Name.Contains("3"))
                discipline = this.Discipline3.Text;
            this.dbControll.ShowOneComittee(this, discipline);
        }

        private void Show_Comittee(object sender, RoutedEventArgs e)
        {
            this.dbControll.Show_Comittees(this);
        }

        private void Show_SR(object sender, RoutedEventArgs e)
        {
            this.dbControll.Show_Statistic(this, 0);
        }

        private void Show_Cout_Five(object sender, RoutedEventArgs e)
        {
            this.dbControll.Show_Statistic(this, 1);
        }

        private void Show_Cout_NOFive(object sender, RoutedEventArgs e)
        {
            this.dbControll.Show_Statistic(this, 2);
        }
    }
}