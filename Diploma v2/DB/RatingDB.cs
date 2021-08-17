using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Diploma_v2
{
    public class RatingDB
    {
        private MySqlConnection conn;
        public RatingDB(string connStr)
        {
            this.conn = new MySqlConnection(connStr);
        }
        public void CreateRatingDB()
        {
            this.conn.Open();
            
            List<String> query = new List<String>();
            query.Add( "INSERT INTO rating VALUES (1, 'Отлично')");
            query.Add( "INSERT INTO rating VALUES (2, 'Хорошо')");
            query.Add( "INSERT INTO rating VALUES (3, 'Удовлетворительно')");
            query.Add( "INSERT INTO rating VALUES (4, 'Не удовлетворительно')");
            query.Add( "INSERT INTO rating VALUES (5, 'Зачтено')");
            query.Add( "INSERT INTO rating VALUES (6, 'Не зачтено')");
           
            foreach (var i in query)
            {
                MySqlCommand command = new MySqlCommand(i, this.conn);
                command.ExecuteNonQuery();
            }
            this.conn.Close();
        }
        public void DeleteRating()
        {
            this.conn.Open();
            string query = "DELETE FROM rating";
           
            MySqlCommand command = new MySqlCommand(query, this.conn);
            command.ExecuteNonQuery();
            
            this.conn.Close();
        }
    }
}