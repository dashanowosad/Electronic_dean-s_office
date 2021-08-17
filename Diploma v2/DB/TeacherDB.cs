using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;

namespace Diploma_v2
{
    public class TeacherDB
    {
        private MySqlConnection conn;
        public TeacherDB(string connStr)
        {
            this.conn = new MySqlConnection(connStr);
        }
        public void CreateTeacherDB()
        {
            this.conn.Open();
            
            List<String> query = new List<String>();
            //ректорат
            query.Add( "INSERT INTO teacher VALUES (1, 'Б.Г.Хаиров', 'д.э.н.', 'доц.б и.о. ректора', 0, 0, 1)");
            query.Add( "INSERT INTO teacher VALUES (2, 'О.В.Фалько', 'к.э.н.', 'и.о. проректора по экономике и финансам', 0, 0, 2)");
            query.Add( "INSERT INTO teacher VALUES (3, 'М.Л.Соловьев', 'к.э.н.', 'проректор по общим вопросам', 0, 0, 3)");
            
            //история и философия
            query.Add( "INSERT INTO teacher VALUES (4, 'В.Ш.Сабиров', 'д.филос.н.', 'проф., зав.каф. философии и истории', 13, 1, 4)");
            query.Add( "INSERT INTO teacher VALUES (5, 'В.Ш.Сабиров', 'д.филос.н.', 'проф., зав.каф. философии и истории', 14, 1, 4)");
            query.Add( "INSERT INTO teacher VALUES (6, 'В.С.Ежов', 'д.филос.н.', 'доц. каф. философии и истории', 14, 1, 5)");
            query.Add( "INSERT INTO teacher VALUES (7, 'В.С.Ежов', 'д.филос.н.', 'доц. каф. философии и истории', 15, 1, 5)");
            query.Add( "INSERT INTO teacher VALUES (8, 'В.С.Ежов', 'д.филос.н.', 'доц. каф. философии и истории', 16, 1, 5)");
            query.Add( "INSERT INTO teacher VALUES (9, 'О.С.Соина', 'д.филос.н.', 'проф. каф. философии и истории', 13, 1, 6)");
            query.Add( "INSERT INTO teacher VALUES (10, 'О.С.Соина', 'д.филос.н.', 'проф. каф. философии и истории', 14, 1, 6)");
            //иностранный
            query.Add( "INSERT INTO teacher VALUES (11, 'Т.С.Ильина', 'к.т.н.', 'доц. зав. каф. иностранных языков', 5, 2, 7)");
            query.Add( "INSERT INTO teacher VALUES (12, 'Т.С.Ильина', 'к.т.н.', 'доц. зав. каф. иностранных языков', 17, 2, 7)");
            query.Add( "INSERT INTO teacher VALUES (13, 'И.А.Загороднова', 'к.т.н.', 'ст.преп. каф. иностранных языков', 5, 2, 8)");
            //экономика
            query.Add( "INSERT INTO teacher VALUES (14, 'Н.Л.Казначеева', 'д.э.н.', 'доц., зав.каф. ЭТ', 6, 3, 9)");
            query.Add( "INSERT INTO teacher VALUES (15, 'Н.Л.Казначеева', 'д.э.н.', 'доц., зав.каф. ЭТ', 18, 3, 9)");
            query.Add( "INSERT INTO teacher VALUES (16, 'Н.Л.Казначеева', 'д.э.н.', 'доц., зав.каф. ЭТ', 19, 3, 9)");
            query.Add( "INSERT INTO teacher VALUES (17, 'М.В.Облаухова', 'к.э.н.', 'доц. каф. ЭТ', 20, 3, 10)");
            query.Add( "INSERT INTO teacher VALUES (18, 'М.В.Облаухова', 'к.э.н.', 'доц. каф. ЭТ', 21, 3, 10)");
            query.Add( "INSERT INTO teacher VALUES (19, 'С.П.Анофриков', 'к.э.н.', 'доц. каф. ЭТ', 22, 3, 11)");
            query.Add( "INSERT INTO teacher VALUES (20, 'С.П.Анофриков', 'к.э.н.', 'доц. каф. ЭТ', 23, 3, 11)");
            //информатика
            query.Add( "INSERT INTO teacher VALUES (21, 'В.Б.Барахнин', 'д.т.н.', 'проф. каф. ПМ и К', 24, 4, 12)");
            query.Add( "INSERT INTO teacher VALUES (22, 'В.Б.Барахнин', 'д.т.н.', 'проф. каф. ПМ и К', 25, 4, 12)");
            query.Add( "INSERT INTO teacher VALUES (23, 'В.Б.Барахнин', 'д.т.н.', 'проф. каф. ПМ и К', 26, 4, 12)");
            query.Add( "INSERT INTO teacher VALUES (24, 'О.А.Бах', 'к.т.н.', 'доц. каф. ПМ и К', 8, 4, 13)");
            query.Add( "INSERT INTO teacher VALUES (25, 'О.А.Бах', 'к.т.н.', 'доц. каф. ПМ и К', 12, 4, 13)");
            query.Add( "INSERT INTO teacher VALUES (26, 'О.А.Бах', 'к.т.н.', 'доц. каф. ПМ и К', 27, 4, 13)");
            query.Add( "INSERT INTO teacher VALUES (27, 'Е.А.Малков', 'д.физ.-мат.н.', 'проф. каф. ПМ и К', 3, 4, 14)");
            query.Add( "INSERT INTO teacher VALUES (28, 'Е.А.Малков', 'д.физ.-мат.н.', 'проф. каф. ПМ и К', 8, 4, 14)");
            //выч. сист.
            query.Add( "INSERT INTO teacher VALUES (29, 'А.В.Ефимов', 'к.т.н.', 'доцент каф. вычислительных систем', 10, 5, 15)");
            query.Add( "INSERT INTO teacher VALUES (30, 'А.В.Ефимов', 'к.т.н.', 'доцент каф. вычислительных систем', 28, 5, 15)");
            query.Add( "INSERT INTO teacher VALUES (31, 'С.Г.Пудов', 'д.физ.-мат.н.', 'доц. каф. вычислительных систем', 29, 4, 16)");
            query.Add( "INSERT INTO teacher VALUES (32, 'М.Г.Курносов', 'д.т.н.', 'проф. каф. вычислительных систем', 30, 4, 17)");
            query.Add( "INSERT INTO teacher VALUES (33, 'М.Г.Курносов', 'д.т.н.', 'проф. каф. вычислительных систем', 31, 4, 17)");
            foreach (var i in query)
            {
                MySqlCommand command = new MySqlCommand(i, this.conn);
                command.ExecuteNonQuery();
            }
            this.conn.Close();
        }
        
        public void LoadTeacher(DataGrid dataGridView1)
        {
            this.conn.Open();
            string query = @"SELECT 
                id_one_teacher AS '№',
                Название_факультета AS 'Название факультета',
                Фамилия_инициалы AS 'Фамилия и инициалы', 
                Ученая_степень AS 'Ученая Степень', 
                Звания_должности AS 'Звания и должности', 
                GROUP_CONCAT(Название) AS 'Название дисциплин(ы)'
            FROM teacher, discipline, faculty
                WHERE teacher.id_discipline = discipline.id_discipline AND
                      teacher.id_faculty = faculty.id_faculty
                GROUP BY Фамилия_инициалы";
            MySqlCommand command = new MySqlCommand(query, this.conn);
            
            // MySqlDataReader reader = command.ExecuteReader();
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            
            DataTable ds = new DataTable("teacher");
            adapter.Fill(ds);
            
            dataGridView1.ItemsSource = ds.DefaultView;
            
            this.conn.Close();
        }

        public int TeacherByFaculty(DataGrid dataGridView1, string faculty_name)
        {
            if (faculty_name == "Все")
            {
                this.LoadTeacher(dataGridView1);
                return 0;
            }

            this.conn.Open();
            string query = @"SELECT 
                id_one_teacher AS '№',
                Название_факультета AS 'Название факультета',
                Фамилия_инициалы AS 'Фамилия и инициалы', 
                Ученая_степень AS 'Ученая Степень', 
                Звания_должности AS 'Звания и должности', 
                GROUP_CONCAT(Название) AS 'Название дисциплин(ы)'
            FROM teacher, discipline, faculty
                WHERE teacher.id_discipline = discipline.id_discipline AND
                      teacher.id_faculty = faculty.id_faculty AND
                      Название_факультета";
            query += "= '" + faculty_name + "'" + "GROUP BY Фамилия_инициалы";
             
            MySqlCommand command = new MySqlCommand(query, this.conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);

            DataTable ds = new DataTable("teacher");
            adapter.Fill(ds);

            dataGridView1.ItemsSource = ds.DefaultView;

            this.conn.Close();
            return 0;
        }

        public void FindTeacher(DataGrid dataGridView1, string column, string like)
        {
            try
            {
                switch (column)
                {
                    case "Название факультета":
                        column = "Название_факультета";
                        break;
                    case "Фамилия и инициалы":
                        column = "Фамилия_инициалы";
                        break;
                    case "Ученая степень":
                        column = "Ученая_степень";
                        break;
                    case "Звания и должности":
                        column = "Звания_должности";
                        break;
                    case "Название дисциплин(ы)":
                        column = "Название";
                        break;
                }

                this.conn.Open();
                string query = @"SELECT 
                id_one_teacher AS '№',
                Название_факультета AS 'Название факультета',
                Фамилия_инициалы AS 'Фамилия и инициалы', 
                Ученая_степень AS 'Ученая Степень', 
                Звания_должности AS 'Звания и должности', 
                GROUP_CONCAT(Название) AS 'Название дисциплин(ы)'
            FROM teacher, discipline, faculty
                WHERE teacher.id_discipline = discipline.id_discipline AND
                      teacher.id_faculty = faculty.id_faculty AND ";
                query += column + " LIKE '%" + like + "%'" + "GROUP BY Фамилия_инициалы";

                MySqlCommand command = new MySqlCommand(query, this.conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                DataTable ds = new DataTable("teacher");
                adapter.Fill(ds);

                dataGridView1.ItemsSource = ds.DefaultView;

                this.conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        
        public void DeleteTeachers()
        {
            this.conn.Open();
            string query = "DELETE FROM teacher";
           
            MySqlCommand command = new MySqlCommand(query, this.conn);
            command.ExecuteNonQuery();
            
            this.conn.Close();
        }
        
        public void DeleteTeacher(int id)
        {
            this.conn.Open();
            string query = "DELETE FROM teacher WHERE id_one_teacher = " + id;
           
            MySqlCommand command = new MySqlCommand(query, this.conn);
            command.ExecuteNonQuery();
            
            this.conn.Close();
        }
    }
}