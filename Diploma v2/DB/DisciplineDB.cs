using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
namespace Diploma_v2
{
    public class DisciplineDB
    {
        private MySqlConnection conn;
        public DisciplineDB(string connStr)
        {
            this.conn = new MySqlConnection(connStr);
        }
        public void CreateDisciplineDB()
        {
            this.conn.Open();
            
            List<String> query = new List<String>();
            query.Add( "INSERT INTO discipline VALUES (0, '-')");
            
            query.Add( "INSERT INTO discipline VALUES (1, 'Право')");
            query.Add( "INSERT INTO discipline VALUES (2, 'История')");
            query.Add( "INSERT INTO discipline VALUES (3, 'Программирование')");
            query.Add( "INSERT INTO discipline VALUES (4, 'Математика')");
            query.Add( "INSERT INTO discipline VALUES (5, 'Иностранный язык (английский)')");
            query.Add( "INSERT INTO discipline VALUES (6, 'Экономика')");
            query.Add( "INSERT INTO discipline VALUES (7, 'Русский язык и культура речи')");
            query.Add( "INSERT INTO discipline VALUES (8, 'Операционные системы')");
            query.Add( "INSERT INTO discipline VALUES (9, 'Социология')");
            query.Add( "INSERT INTO discipline VALUES (10, 'Архитектура вычислительных систем')");
            query.Add( "INSERT INTO discipline VALUES (11, 'Безопасность жизнедеятельности')");
            query.Add( "INSERT INTO discipline VALUES (12, 'Теория языков программирования')");
            
            query.Add( "INSERT INTO discipline VALUES (13, 'История и философия науки')");
            query.Add( "INSERT INTO discipline VALUES (14, 'Философия')");
            query.Add( "INSERT INTO discipline VALUES (15, 'Философия техники')");
            query.Add( "INSERT INTO discipline VALUES (16, 'Эстетика в системе философских знаний')");
            query.Add( "INSERT INTO discipline VALUES (17, 'Иностранный язык в профессиональной области')");
            query.Add( "INSERT INTO discipline VALUES (18, 'Макроэкономика')");
            query.Add( "INSERT INTO discipline VALUES (19, 'Экономика отраслевых рынков')");
            query.Add( "INSERT INTO discipline VALUES (20, 'Экономико-математические методы и модели')");
            query.Add( "INSERT INTO discipline VALUES (21, 'Мировая экономика и международные экономические отношения')");
            query.Add( "INSERT INTO discipline VALUES (22, 'Правовое регулирование управленческой деятельности')");
            query.Add( "INSERT INTO discipline VALUES (23, 'Правовые основы защиты интеллектуальной собственности')");
            query.Add( "INSERT INTO discipline VALUES (24, 'Базы данных')");
            query.Add( "INSERT INTO discipline VALUES (25, 'Информатика')");
            query.Add( "INSERT INTO discipline VALUES (26, 'Теоретические основы информатики')");
            query.Add( "INSERT INTO discipline VALUES (27, 'Дискретная математика')");
            query.Add( "INSERT INTO discipline VALUES (28, 'Высокопроизводительные вычислительные системы')");
            query.Add( "INSERT INTO discipline VALUES (29, 'Технологии разработки программного обеспечения')");
            query.Add( "INSERT INTO discipline VALUES (30, 'Параллельное программирование')");
            query.Add( "INSERT INTO discipline VALUES (31, 'Параллельные вычислительные технологии')");
            foreach (var i in query)
            {
                MySqlCommand command = new MySqlCommand(i, this.conn);
                command.ExecuteNonQuery();
            }
            
            this.conn.Close();
        }
        public void DeleteDiscipline()
        {
            this.conn.Open();
            string query = "DELETE FROM discipline";
           
            MySqlCommand command = new MySqlCommand(query, this.conn);
            command.ExecuteNonQuery();
            
            this.conn.Close();
        }
    }
}