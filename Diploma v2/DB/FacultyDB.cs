using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
namespace Diploma_v2
{
    public class FacultyDB
    {
        private MySqlConnection conn;
        public FacultyDB(string connStr)
        {
            this.conn = new MySqlConnection(connStr);
        }
        public void CreateFacultyDB()
        {
            this.conn.Open();
            
            List<String> query = new List<String>();
            query.Add( "INSERT INTO faculty VALUES (0,'-')");
            query.Add( "INSERT INTO faculty VALUES (1,'Философии и истории')");
            query.Add( "INSERT INTO faculty VALUES (2,'Иностранных и руского языка')");
            query.Add( "INSERT INTO faculty VALUES (3,'Экономической теории')");
            query.Add( "INSERT INTO faculty VALUES (4,'Прикладной математики и кибернетики')");
            query.Add( "INSERT INTO faculty VALUES (5,'Вычислительных систем')");
            
            foreach (var i in query) 
            {
                MySqlCommand command = new MySqlCommand(i, this.conn);
                command.ExecuteNonQuery();
            }
            this.conn.Close();
        }
        public void DeleteFaculty()
        {
            this.conn.Open();
            string query = "DELETE FROM faculty";
           
            MySqlCommand command = new MySqlCommand(query, this.conn);
            command.ExecuteNonQuery();
            
            this.conn.Close();
        }
    }
}