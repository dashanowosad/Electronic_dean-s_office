using MySql.Data.MySqlClient;
namespace Diploma_v2.DopWindow.Teacher
{
    public class AddOrUpdateTeacher
    {
        private MySqlConnection conn;
        public AddOrUpdateTeacher(string connStr)
        {
            this.conn = new MySqlConnection(connStr);
        }

        public int FindDisciplinesId(string s)
        {
            int id = -1;
            this.conn.Open();
            string query = "SELECT id_discipline FROM discipline WHERE Название = '" + s + "'";
            
            MySqlCommand command = new MySqlCommand(query, this.conn);
            try
            {
                id = (int) command.ExecuteScalar();
            }
            catch
            {
                id = -1;
            }
            this.conn.Close();
            return id;
        }

        public int MaxId(string q)
        {
            this.conn.Open();
            string query = q;
            MySqlCommand command = new MySqlCommand(query, this.conn);
            int id = (int) command.ExecuteScalar();
            ++id;
            
            this.conn.Close();
            return id;
        }

        public int FindFecultyId(string s)
        {
            int id = -1;
            this.conn.Open();
            string query = "SELECT id_faculty FROM faculty WHERE Название_факультета = '" + s + "'";
            
            MySqlCommand command = new MySqlCommand(query, this.conn);
            try
            {
                id = (int) command.ExecuteScalar();
            }
            catch
            {
                id = -1;
            }
            this.conn.Close();
            return id;
        }

        public int AddNewFaculty(string s, string q)
        {
            
            int id = MaxId(q);
            this.conn.Open();
            string query = "INSERT INTO faculty VALUES(" + id + ",'" + s + "')";
            MySqlCommand command = new MySqlCommand(query, this.conn);
            command.ExecuteNonQuery();
            this.conn.Close();
            return id;
        }

        public void AddNewTeacher(string q, string s, int idDis, int idFacult, int idTeach = -1)
        {
            int id = MaxId(q);
            this.conn.Open();
            string query = "INSERT INTO teacher VALUES(" + id + "," + s + "," + idDis + "," +idFacult+ "," + idTeach + ")";
            MySqlCommand command = new MySqlCommand(query, this.conn);
            command.ExecuteNonQuery();
            this.conn.Close();
        }

        public int AddNewDisciplines(string s, string q)
        {
            int id = MaxId(q);
            this.conn.Open();
            string query = "INSERT INTO discipline VALUES(" + id + ",'" + s + "')";
            MySqlCommand command = new MySqlCommand(query, this.conn);
            command.ExecuteNonQuery();
            this.conn.Close();
            return id;
        }

        public void DeleteTeacher(int idTeach)
        {
            this.conn.Open();
            
            string query = @"DELETE FROM teacher WHERE id_one_teacher = ";
            query += idTeach;
            MySqlCommand command = new MySqlCommand(query, this.conn);
            command.ExecuteNonQuery();
            
            this.conn.Close();
        }
    }
}