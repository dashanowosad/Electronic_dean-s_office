using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
namespace Diploma_v2
{
    public class MarkDB
    {
        private MySqlConnection conn;
        public MarkDB(string connStr)
        {
            this.conn = new MySqlConnection(connStr);
        }
        
        public void CreateMarkDB()
        {
            this.conn.Open();
            
            List<String> query = new List<String>();
            //аспиранты
            query.Add( "INSERT INTO mark VALUES (1, 1, 'Первый семестр', 'Зачет', 5, '2017-12-25', 1)");
            query.Add( "INSERT INTO mark VALUES (2, 2, 'Второй семестр', 'Экзамен', 2, '2018-06-12',  1)");
            query.Add( "INSERT INTO mark VALUES (3, 3, 'Третий семестр', 'Экзамен', 1, '2020-01-17', 1)");
            query.Add( "INSERT INTO mark VALUES (4, 4, 'Четвертый семестр', 'Экзамен', 3, '2020-07-03', 1)");
            query.Add( "INSERT INTO mark VALUES (5, 1, 'Первый семестр', 'Зачет', 5, '2017-12-25', 2)");
            query.Add( "INSERT INTO mark VALUES (6, 2, 'Второй семестр', 'Экзамен', 3, '2018-06-12', 2)");
            query.Add( "INSERT INTO mark VALUES (7, 3, 'Третий семестр', 'Экзамен',1, '2020-01-17', 2)");
            query.Add( "INSERT INTO mark VALUES (8, 4, 'Четвертый семестр', 'Экзамен', 1, '2020-07-03', 2)");
            query.Add( "INSERT INTO mark VALUES (9, 1, 'Первый семестр', 'Зачет', 5, '2017-12-25', 3)");
            query.Add( "INSERT INTO mark VALUES (10, 2, 'Второй семестр', 'Экзамен', 1, '2018-06-15', 3)");
            query.Add( "INSERT INTO mark VALUES (11, 3, 'Третий семестр', 'Экзамен', 2, '2020-01-15', 3)");
            query.Add( "INSERT INTO mark VALUES (12, 4, 'Четвертый семестр', 'Экзамен', 2, '2020-06-22', 3)");
            //бакалавриат
            query.Add( "INSERT INTO mark VALUES (13, 5, 'Первый семестр', 'Зачет', 5, '2018-12-17', 4)");
            query.Add( "INSERT INTO mark VALUES (14, 6, 'Второй семестр', 'Экзамен', 2, '2019-06-20', 4)");
            query.Add( "INSERT INTO mark VALUES (15, 7, 'Третий семестр', 'Зачет', 5, '2020-12-26', 4)");
            query.Add( "INSERT INTO mark VALUES (16, 8, 'Четвертый семестр', 'Экзамен', 2, '2021-06-13', 4)");
            query.Add( "INSERT INTO mark VALUES (17, 5, 'Первый семестр', 'Зачет', 5, '2018-12-25', 5)");
            query.Add( "INSERT INTO mark VALUES (18, 6, 'Второй семестр', 'Экзамен', 1, '2019-06-23', 5)");
            query.Add( "INSERT INTO mark VALUES (19, 7, 'Третий семестр', 'Зачет', 5, '2020-12-23', 5)");
            query.Add( "INSERT INTO mark VALUES (20, 8, 'Четвертый семестр', 'Экзамен', 3, '2021-07-04', 5)");
            query.Add( "INSERT INTO mark VALUES (21, 5, 'Первый семестр', 'Зачет', 5, '2018-12-23', 6)");
            query.Add( "INSERT INTO mark VALUES (22, 6, 'Второй семестр', 'Экзамен', 1, '2019-07-05', 6)");
            query.Add( "INSERT INTO mark VALUES (23, 7, 'Третий семестр', 'Зачет', 5, '2020-12-17', 6)");
            query.Add( "INSERT INTO mark VALUES (24, 8, 'Четвертый семестр', 'Экзамен', 1, '2021-05-22', 6)");
            //магистр1
            query.Add( "INSERT INTO mark VALUES (25, 9, 'Первый семестр', 'Зачет', 5, '2019-12-13', 7)");
            query.Add( "INSERT INTO mark VALUES (26, 10, 'Второй семестр', 'Экзамен', 1, '2020-05-23', 7)");
            query.Add( "INSERT INTO mark VALUES (27, 9, 'Первый семестр', 'Зачет', 5, '2019-12-13', 8)");
            query.Add( "INSERT INTO mark VALUES (28, 10, 'Второй семестр', 'Экзамен', 1, '2020-05-23', 8)");

            //магистр2
            query.Add( "INSERT INTO mark VALUES (29, 11, 'Первый семестр', 'Зачет', 5, '2019-12-24', 9)");
            query.Add( "INSERT INTO mark VALUES (30, 12, 'Второй семестр', 'Экзамен', 3, '2020-06-17', 9)");
            query.Add( "INSERT INTO mark VALUES (31, 11, 'Первый семестр', 'Зачет', 5, '2019-12-24', 10)");
            query.Add( "INSERT INTO mark VALUES (32, 12, 'Второй семестр', 'Экзамен', 2, '2020-07-02', 10)");
            foreach (var i in query)
            {
                MySqlCommand command = new MySqlCommand(i, this.conn);
                command.ExecuteNonQuery();
            }
            this.conn.Close();
        }
        public void LoadMark(DataGrid dataGridView1, string id_student)
        {
            try
            {
                this.conn.Open();
                string query = @"SELECT 
                id_mark AS '№',
               Название AS 'Дисциплина', 
               Период_контроля AS 'Период контроля',
               Вид_контроля AS 'Вид контроля',
               Отметка,
               DATE_FORMAT(Дата_получения, '%d/%m/%Y') AS 'Дата получения'
            FROM mark, discipline, rating
               WHERE mark.id_rating = rating.id_rating AND
                     mark.id_discipline = discipline.id_discipline AND id_student";
                query += '=' + id_student;
                MySqlCommand command = new MySqlCommand(query, this.conn);

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);


                DataTable ds = new DataTable("mark");
                adapter.Fill(ds);

                dataGridView1.ItemsSource = ds.DefaultView;

                this.conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void DeleteMarks()
        {
            this.conn.Open();
            string query = "DELETE FROM mark";
           
            MySqlCommand command = new MySqlCommand(query, this.conn);
            command.ExecuteNonQuery();
            
            this.conn.Close();
        }
        
        public void DeleteMark(int idMark)
        {
            this.conn.Open();
            string query = "DELETE FROM mark WHERE id_mark = " + idMark;
           
            MySqlCommand command = new MySqlCommand(query, this.conn);
            command.ExecuteNonQuery();
            
            this.conn.Close();
        }
    }
}