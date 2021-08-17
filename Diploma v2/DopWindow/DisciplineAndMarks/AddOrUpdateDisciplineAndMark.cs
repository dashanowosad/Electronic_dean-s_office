using MySql.Data.MySqlClient;

namespace Diploma_v2.DopWindow.DisciplineAndMarks
{
    public class AddOrUpdateDisciplineAndMark
    {
        private MySqlConnection conn;
        public AddOrUpdateDisciplineAndMark(string connStr)
        {
            this.conn = new MySqlConnection(connStr);
        }

        public int Find(string q)
        {
            int id = -1;
            this.conn.Open();
            string query = q;
            
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

        public void AddDisciplinOrRating(string q)
        {
            this.conn.Open();
            string query = q;
            MySqlCommand command = new MySqlCommand(query, this.conn);
            command.ExecuteNonQuery();
            this.conn.Close();
        }

        public void FinalAdd(string q, int idMark = -1)
        {
            string query = "SELECT MAX(id_mark) FROM mark";
            int id = this.MaxId(query);
            if (idMark != -1)
                id = idMark;
            this.conn.Open();
            query = "INSERT INTO mark VALUES (";
            query += id + ", " + q + ')';
            MySqlCommand command = new MySqlCommand(query, this.conn);
            command.ExecuteNonQuery();
            this.conn.Close();
        }

        public void DeleteMark(int id_mark)
        {
            this.conn.Open();
            string query = "DELETE FROM mark WHERE id_mark = " + id_mark;
            MySqlCommand command = new MySqlCommand(query, this.conn);
            command.ExecuteNonQuery();
            this.conn.Close();
        }
    }
}