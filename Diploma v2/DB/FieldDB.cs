using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
namespace Diploma_v2
{
    public class FieldDB
    {
        private MySqlConnection conn;

        public FieldDB(string connStr)
        {
            this.conn = new MySqlConnection(connStr);
        }
        public void CreateFieldDB()
        {
            this.conn.Open();
            
            List<String> query = new List<String>();
            query.Add( "INSERT INTO field_study VALUES (1, '09.06.01', 'Информатика и вычислительная техника')");
            query.Add( "INSERT INTO field_study VALUES (2, '09.03.02', 'Информационные системы и технологии')");
            query.Add( "INSERT INTO field_study VALUES (3, '11.04.02', 'Инфокоммуникационные технологии и системы связи')");
            query.Add( "INSERT INTO field_study VALUES (4, '11.04.01', 'Радиотехника')");
            
            foreach (var i in query)
            {
                MySqlCommand command = new MySqlCommand(i, this.conn);
                command.ExecuteNonQuery();
            }
            this.conn.Close();
        }
        
        public void DeleteField()
        {
            this.conn.Open();
            string query = "DELETE FROM field_study";
           
            MySqlCommand command = new MySqlCommand(query, this.conn);
            command.ExecuteNonQuery();
            
            this.conn.Close();
        }
    }
}