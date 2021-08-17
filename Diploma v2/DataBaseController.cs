using System;
using System.Windows;
using System.Windows.Controls;
using Diploma_v2.DopWindow.AboutComittee;
using Diploma_v2.DopWindow.AddDiscipline;
using Diploma_v2.DopWindow.DelDiscipline;
using Diploma_v2.DopWindow.DeleteDiscipline;
using Diploma_v2.DopWindow.DisciplineAndMarks;
using Diploma_v2.DopWindow.InfoAboutOneComittee;
using Diploma_v2.DopWindow.Orders;
using Diploma_v2.DopWindow.Statistic;
using Diploma_v2.DopWindow.Teacher;
using Diploma_v2.WorkWithDB;

namespace Diploma_v2
{
    public class DataBaseController
    {
        string StudDb = "server=localhost;user=root;database=diplom;password=Crostels325;";
        
        private FieldDB _fieldDb;
        private StudentDB _studentDb;
        private MarkDB _markDb;
        private RatingDB _ratingDb;
        private OrderDB _orderDb;
        private DisciplineDB _disciplineDb;
        private TeacherDB _teacherDb;
        private FacultyDB _facultyDb;
        private ComboBoxTeacherContent _comboBoxTeacherContent;
        private ComitteeDB _comittee;
        private ComboBoxsFromReferenceAboutStudent _comboBoxsFromReferenceAboutStudent;
        private ComboBoxFromReferenceExams _comboBoxFromReferenceExams;
        private Student_Window _studentInsUptDel;
        private DisciplineAndMarks_Window _disciplineAndMarksWindow;
        private Order_Window _orderWindow;
        private Teacher_Window _teacherWindow;
        private AddDisciplineClass_Window _addDisciplineClassWindow;
        private UpdateDisciplineClass_Window _updateDisciplineClassWindow;
        private DelDisciplineClass_Window _delDisciplineClassWindow;
        private InfoAboutOneComittee_Window _infoAboutOneComitteeWindow;
        private AboutComitteeClass_Window _aboutComitteeClassWindow;
        private Statistic_Window _statisticWindow;
        public DataBaseController()
        {
            this._fieldDb = new FieldDB(this.StudDb);
            this._studentDb = new StudentDB(this.StudDb);
            this._markDb = new MarkDB(this.StudDb);
            this._ratingDb = new RatingDB(this.StudDb);
            this._orderDb = new OrderDB(this.StudDb);
            this._disciplineDb = new DisciplineDB(this.StudDb);
            this._teacherDb = new TeacherDB(this.StudDb);
            this._facultyDb = new FacultyDB(this.StudDb);
            this._comboBoxTeacherContent = new ComboBoxTeacherContent(this.StudDb);
            this._comittee = new ComitteeDB(this.StudDb);
            this._comboBoxsFromReferenceAboutStudent = new ComboBoxsFromReferenceAboutStudent(this.StudDb);
            this._comboBoxFromReferenceExams = new ComboBoxFromReferenceExams(this.StudDb);
            this._studentInsUptDel = new Student_Window(this.StudDb);
            this._disciplineAndMarksWindow = new DisciplineAndMarks_Window(this.StudDb);
            this._orderWindow = new Order_Window(this.StudDb);
            this._teacherWindow = new Teacher_Window(this.StudDb);
            this._addDisciplineClassWindow = new AddDisciplineClass_Window(this.StudDb);
            this._updateDisciplineClassWindow = new UpdateDisciplineClass_Window(this.StudDb);
            this._delDisciplineClassWindow = new DelDisciplineClass_Window(this.StudDb);
            this._infoAboutOneComitteeWindow = new InfoAboutOneComittee_Window(this.StudDb);
            this._aboutComitteeClassWindow = new AboutComitteeClass_Window();
            this._statisticWindow = new Statistic_Window();
        }

        public void LoadStud(DataGrid table)
        {
            this._studentDb.LoadStudent(table);
        }
        
        public void LoadTeach(DataGrid table, ComboBox comboBox1, ComboBox comboBox2)
        {
            this._teacherDb.LoadTeacher(table);

            this._comboBoxTeacherContent.SelectFaculty(comboBox1);
            this._comboBoxTeacherContent.SelectDiscipline(comboBox2);
        }

        public void FindTeacher(DataGrid table, string column, string like)
        {
            this._teacherDb.FindTeacher(table, column, like);
        }

        public void ReloadTeacherTable(DataGrid table, string faculty_name)
        {
            this._teacherDb.TeacherByFaculty(table, faculty_name);
        }

        public void RebootAll(DataGrid table, DataGrid table2, ComboBox comboBox1, ComboBox comboBox2)
        {
            //сброс таблицы направлений
            this._fieldDb.DeleteField();
            this._fieldDb.CreateFieldDB();
            
            //сброс таблицы студентов
            this._studentDb.DeleteStudents();
            this._studentDb.CreateStudentDB();
            this._studentDb.LoadStudent(table);
            
            //сброс дисциплин
            this._disciplineDb.DeleteDiscipline();
            this._disciplineDb.CreateDisciplineDB();
            
            //сброс оценок
            this._markDb.DeleteMarks();
            this._markDb.CreateMarkDB();
            this._ratingDb.DeleteRating();
            this._ratingDb.CreateRatingDB();

            //сброс приказов
            this._orderDb.DeleteOrders();
            this._orderDb.CreateOrderDB();
            
            //сброс таблицы факультетов
            this._facultyDb.DeleteFaculty();
            this._facultyDb.CreateFacultyDB();
            

            //сброс таблицы преподавателей
            this._teacherDb.DeleteTeachers();
            this._teacherDb.CreateTeacherDB();
            this.LoadTeach(table2, comboBox1, comboBox2);

            //сброс таблицы коммиссий
            this._comittee.DeleteComittee();
        }

      

        public void LoadMark(DataGrid table, string id_stud)
        {
            this._markDb.LoadMark(table, id_stud);
        }
        
        public void LoadOrder(DataGrid table, string id_stud)
        {
            this._orderDb.LoadOrder(table, id_stud);
        }

        public void AddNewComittee(MainWindow w, DataGrid dataGrid, string discipline)
        {
            this._comittee.AddNewComittee(w, dataGrid, discipline);
        }

        public void LoadReferenceItems(ComboBox comboBoxStud,ComboBox comboBoxScienceWork, ComboBox comboBoxChief,
                                        ComboBox comboBoxDiscipline1, ComboBox comboBoxRating1, 
                                        ComboBox comboBoxDiscipline2, ComboBox comboBoxRating2,
                                        ComboBox comboBoxDiscipline3, ComboBox comboBoxRating3)
        {
            this._comboBoxsFromReferenceAboutStudent.StudentComboBox(comboBoxStud);
            this._comboBoxsFromReferenceAboutStudent.ScienceWorkANDChiefComboBox(comboBoxScienceWork);
            this._comboBoxsFromReferenceAboutStudent.ScienceWorkANDChiefComboBox(comboBoxChief);
            
            this._comboBoxFromReferenceExams.DisciplineComboBox(comboBoxDiscipline1);
            this._comboBoxFromReferenceExams.MarkComboBox(comboBoxRating1);
            this._comboBoxFromReferenceExams.DisciplineComboBox(comboBoxDiscipline2);
            this._comboBoxFromReferenceExams.MarkComboBox(comboBoxRating2);
            this._comboBoxFromReferenceExams.DisciplineComboBox(comboBoxDiscipline3);
            this._comboBoxFromReferenceExams.MarkComboBox(comboBoxRating3);
        }

        
        public void SearchReferenceItemsAboutStudent(ComboBox comboBox)
        {
            string q = "";
            comboBox.Text = comboBox.Text.Replace("\'", "");
            comboBox.Text = comboBox.Text.Replace("\"", "");
            // MessageBox.Show(comboBox.Text);
            try
            {
                if (comboBox.Name == "ComboBoxRefStud")
                    q = "SELECT ФИО FROM student WHERE ФИО LIKE '%" + comboBox.Text + "%' ORDER BY ФИО";
                else
                    q = "SELECT Фамилия_инициалы FROM teacher WHERE Фамилия_инициалы LIKE '%" + comboBox.Text + "%' GROUP BY Фамилия_инициалы ORDER BY Фамилия_инициалы";
                this._comboBoxsFromReferenceAboutStudent.FindFromComboBox(comboBox, q);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + ' ' + q);
            }
        }

        public void SearchReferenceItemsExams(ComboBox comboBox)
        {
            string q = "";
            comboBox.Text = comboBox.Text.Replace("\'", "");
            comboBox.Text = comboBox.Text.Replace("\"", "");
            if (comboBox.Name.Contains("Discipline"))
                
            {
                q = @"SELECT Название FROM discipline, comittee 
                WHERE discipline.id_discipline = comittee.id_discipline GROUP BY Название
                HAVING Название LIKE '%" + comboBox.Text + "%' ORDER BY Название";
                this._comboBoxFromReferenceExams.FindFromComboBox(comboBox, q);
            }
            else
            {
                q = "SELECT Отметка FROM rating WHERE Отметка LIKE '%" + comboBox.Text + "%' GROUP BY Отметка";
                this._comboBoxFromReferenceExams.FindFromComboBox(comboBox, q);
            }
        }

        public void Ins_Student(Window w, DataGrid studentGrid, ComboBox stComboBox)
        {
            this._studentInsUptDel.Insert_New_Student_Or_Update(w, studentGrid, stComboBox);
        }


        public void Upd_Student(Window w, DataGrid studentGrid, ComboBox stComboBox, int id)
        {
            this._studentInsUptDel.Insert_New_Student_Or_Update(w, studentGrid, stComboBox, id);
        }

        public void Del_Student(int idStud, DataGrid table)
        {
            this._studentDb.DeleteStudent(idStud);
            this.LoadStud(table);
        }

        public void Ins_Discipline(MainWindow w, int idStud, DataGrid markTab, int idDiscipline = -1)
        {
            this._disciplineAndMarksWindow.Insert_New_Discipline_Or_Update(w, idStud, markTab, idDiscipline);
        }

        public void Del_Mark(int idStud, int idMark, DataGrid tableMark)
        {
            this._markDb.DeleteMark(idMark);
            this.LoadMark(tableMark, idStud.ToString());
        }

        public void Ins_Order(MainWindow mainWindow, int idStud, DataGrid tableOrder)
        {
            this._orderWindow.Insert_New_Order_Or_Update(mainWindow, idStud, tableOrder);
        }

        public void Upd_Order(MainWindow mainWindow, int idStud, DataGrid tableOrder, int idOrder)
        {
            this._orderWindow.Insert_New_Order_Or_Update(mainWindow, idStud, tableOrder, idOrder);
        }

        public void Del_Order(int idStud, int idOrder, DataGrid tableOrder)
        {
            this._orderDb.DeleteOrder(idOrder);
            this.LoadOrder(tableOrder, idStud.ToString());
        }

        public void Ins_Teacher(MainWindow mainWindow, DataGrid tableTeacher, ComboBox faculty, ComboBox discipline,
            ComboBox disc1, ComboBox disc2, ComboBox disc3,ComboBox chef, ComboBox science_w)
        {
            this._teacherWindow.Insert_New_Teacher_Or_Update(mainWindow, tableTeacher, faculty, discipline, disc1, disc2, disc3, chef, science_w);
        }

        public void Upd_Teacher(MainWindow mainWindow, DataGrid tableTeacher, ComboBox faculty, ComboBox discipline,
            ComboBox disc1, ComboBox disc2, ComboBox disc3, ComboBox chef, ComboBox science_w, int id)
        {
            this._teacherWindow.Insert_New_Teacher_Or_Update(mainWindow, tableTeacher, faculty, discipline, disc1, disc2, disc3, chef, science_w, id);
        }

        public void Del_Teacher(int idTeach, DataGrid tableTeacher, ComboBox comboBox1, ComboBox comboBox2,
            ComboBox chef, ComboBox science_w)
        {
            this._teacherDb.DeleteTeacher(idTeach);
            this.LoadTeach(tableTeacher, comboBox1, comboBox2);
            this._comboBoxsFromReferenceAboutStudent.ScienceWorkANDChiefComboBox(chef);
            this._comboBoxsFromReferenceAboutStudent.ScienceWorkANDChiefComboBox(science_w);
        }

        public void Ins_Disc(MainWindow mainWindow, ComboBox discipline, ComboBox disc1, ComboBox disc2, ComboBox disc3)
        {
            this._addDisciplineClassWindow.Add_New_Discipline(mainWindow, discipline, disc1, disc2, disc3);
        }

        public void Upd_Disc(MainWindow mainWindow, ComboBox discipline, ComboBox disc1, ComboBox disc2, ComboBox disc3)
        {
            this._updateDisciplineClassWindow.Update_Discipline(mainWindow, discipline, disc1, disc2, disc3);
        }

        public void Del_Disc(MainWindow mainWindow, DataGrid tableTeach, ComboBox discipline, ComboBox disc1, ComboBox disc2, ComboBox disc3)
        {
            this._delDisciplineClassWindow.Del_Discipline(mainWindow, tableTeach, discipline, disc1, disc2, disc3);
        }

        public void ShowOneComittee(MainWindow mainWindow, string discipline)
        {
            this._infoAboutOneComitteeWindow.ShowInfoAboutOneComittee(mainWindow, discipline);
        }

        public void Show_Comittees(MainWindow mainWindow)
        {
            this._aboutComitteeClassWindow.Show_Comittees(mainWindow);
        }

        public void Show_Statistic(MainWindow mainWindow, int flag)
        {
            this._statisticWindow.Load_Statistic(mainWindow, flag);
        }
    }
}