using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;
using MySql.Data.MySqlClient;

namespace Diploma_v2
{
    public class StudentDB
    {
        private MySqlConnection conn;
        public StudentDB(string connStr)
        {
            this.conn = new MySqlConnection(connStr);
        }
        public void CreateStudentDB()
        {
            this.conn.Open();
            
            List<String> query = new List<String>();
            query.Add( "INSERT INTO student VALUES (1, 'Кремис Анастасия Сергеевна', '2017-09-01', '2021-08-31','аспирант', 'очная', 'бюджет', 'Кремис Анастасии Сергеевне', 'ж', 'ИП-711', 1)");
            query.Add( "INSERT INTO student VALUES (2, 'Юрасов Денис Максимович', '2017-09-01', '2021-08-31','аспирант', 'заочная', 'бюджет', 'Юрасову Денису Максимовичу', 'м', 'ИП-711', 1)");
            query.Add( "INSERT INTO student VALUES (3, 'Макарухина Юлия Александровна', '2017-09-01', '2021-08-31','аспирант', 'заочная', 'целевое', 'Макарухиной Юлии Александровне','ж', 'ИП-712', 1)");
            query.Add( "INSERT INTO student VALUES (4, 'Худяков Дмитрий Викторович', '2018-09-03', '2022-08-31','бакалавр', 'очная', 'бюджет', 'Худякову Дмитрию Викторовичу','м', 'ИВ-81', 2)");
            query.Add( "INSERT INTO student VALUES (5, 'Патрушев Антов Владимирович', '2018-09-03', '2022-08-31','бакалавр', 'заочная', 'платное', 'Патрушеву Антону Владимировичу','м','ИВ-81', 2)");
            query.Add( "INSERT INTO student VALUES (6, 'Красовицкая Анжелика Сергеевна', '2018-09-03', '2022-08-31','бакалавр', 'очная', 'бюджет', 'Красовицкой Анжелике Сепгеевне','ж','ИВ-82', 2)");
            query.Add( "INSERT INTO student VALUES (7, 'Гребнев Вячеслав Тимурович', '2019-09-01', '2022-08-31','магистрант', 'заочная', 'бюджет', 'Гребневу Вячеславу Тимуровичу','м', 'ИТ-95', 3)");
            query.Add( "INSERT INTO student VALUES (8, 'Рогатин Руслан Максимович', '2019-09-01', '2021-08-31','магистрант', 'очная', 'платное', 'Рогатину Руслану Максимовичу','м', 'ИТ-95', 3)");
            query.Add( "INSERT INTO student VALUES (9, 'Козлитина Кристина Юрьевна', '2019-09-01', '2021-08-31','магистрант', 'заочная', 'платное', 'Козлитиной Кристине Юрьевне','ж', 'РТ-91', 4)");
            query.Add( "INSERT INTO student VALUES (10, 'Тайлакова Ксения Сергеевна', '2019-09-01', '2021-08-31','магистрант', 'очная', 'целевое', 'Тайлаковой Ксении Сергевнее','ж', 'РТ-91', 4)");

            foreach (var i in query)
            {
                MySqlCommand command = new MySqlCommand(i, this.conn);
                command.ExecuteNonQuery();
            }
            
            // MessageBox.Show(name);
            this.conn.Close();
        }
        public void LoadStudent(DataGrid dataGridView1)
        {
            this.conn.Open();
            
            string query = @"SELECT 
                id_student AS '№',
                ФИО, 
                DATE_FORMAT(Дата_поступления, '%d/%m/%Y') AS 'Дата поступления', 
                DATE_FORMAT(Дата_окончания, '%d/%m/%Y') AS 'Дата окончания', 
                Степень_диплома AS 'Степень диплома', 
                Форма_обучения AS 'Форма обучения', 
                Форма_оплаты AS 'Форма оплаты',
                CONCAT(CONCAT(CONCAT(Номер, ' «'), Название), '»') AS 'Направление',
                Группа
            FROM student, field_study
                WHERE student.id_direction = field_study.id_direction";
            MySqlCommand command = new MySqlCommand(query, this.conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);

            DataTable ds = new DataTable("student");
            adapter.Fill(ds);

            dataGridView1.ItemsSource = ds.DefaultView;
            
            this.conn.Close();
        }

        public void DeleteStudents()
        {
            this.conn.Open();
            string query = "DELETE FROM Student";
           
            MySqlCommand command = new MySqlCommand(query, this.conn);
            command.ExecuteNonQuery();
            
            this.conn.Close();
        }

        public void DeleteStudent(int id_student)
        {
            this.conn.Open();
            string query = "DELETE FROM Student WHERE id_student = ";
            query += id_student;
           
            MySqlCommand command = new MySqlCommand(query, this.conn);
            command.ExecuteNonQuery();
            
            this.conn.Close();
        }
    }
}