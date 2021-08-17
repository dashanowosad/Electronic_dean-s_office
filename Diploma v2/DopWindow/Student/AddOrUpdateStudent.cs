using System;
using MySql.Data.MySqlClient;

namespace Diploma_v2.WorkWithDB
{
    public class AddOrUpdateStudent
    {
        private MySqlConnection conn;
        public AddOrUpdateStudent(string connStr)
        {
            this.conn = new MySqlConnection(connStr);
        }

        public int FindFaculty(string direction)
        {
            this.conn.Open();
            string[] str = direction.Split('-');
            str[0] = str[0].Replace(" ", "");
            string query = @"SELECT 
                id_direction
            FROM field_study
                WHERE Номер =";
            query += "'" + str[0] + "'";
            MySqlCommand command = new MySqlCommand(query, this.conn);
            int id_direction;
            try
            {
                id_direction = (int) command.ExecuteScalar();
            }
            catch (Exception e)
            {
                id_direction = -1;
            }
            
            this.conn.Close();
            return id_direction;
        }

        public int AddDirection(string direction)
        {
            this.conn.Open();
            string query = @"SELECT 
                MAX(id_direction)
            FROM field_study";
            MySqlCommand command = new MySqlCommand(query, this.conn);
            int id_direction = (int) command.ExecuteScalar();
            ++id_direction;
        
            string[] str = direction.Split('-');
            query = "INSERT INTO field_study VALUES (" + id_direction +",'" + str[0] + "','" + str[1] + "')";
            command = new MySqlCommand(query, this.conn);
            command.ExecuteNonQuery();
            this.conn.Close();
            
            return id_direction;
        }

        public void AddStudent(string add, int idStud = -1)
        {
            this.conn.Open();

            string query;
            int id_student;
            MySqlCommand command;
            if (idStud == -1)
            {
                query = @"SELECT 
                MAX(id_student)
            FROM student";
                command = new MySqlCommand(query, this.conn);
                id_student = (int) command.ExecuteScalar();
                ++id_student;
            }

            else
                id_student = idStud;
            query = "INSERT INTO student VALUES (" + id_student + ", " + add + ")";
            
            command = new MySqlCommand(query, this.conn);
            command.ExecuteNonQuery();
            this.conn.Close();
        }

        public void DeleteStudent(int idStud)
        {
            this.conn.Open();
            
            string query = @"DELETE FROM student WHERE id_student = ";
            query += idStud;
            MySqlCommand command = new MySqlCommand(query, this.conn);
            command.ExecuteNonQuery();
            
            this.conn.Close();
        }
    }
}