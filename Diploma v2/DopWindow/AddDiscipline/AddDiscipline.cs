using MySql.Data.MySqlClient;
namespace Diploma_v2.DopWindow.AddDiscipline
{
    public class AddDiscipline
    {
        private MySqlConnection conn;
        public AddDiscipline(string studDb)
        {
            this.conn = new MySqlConnection(studDb);
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
        public void Add(string s, int idDisc = -1)
        {
            int id = MaxId("SELECT MAX(id_discipline) FROM discipline");
            this.conn.Open();
            if (idDisc != -1)
                id = idDisc;
            string query = "INSERT INTO discipline VALUES(" + id + ",'" + s + "')";
            MySqlCommand command = new MySqlCommand(query, this.conn);
            command.ExecuteNonQuery();
            this.conn.Close();
            
        }

        public int FindDiscipline(string s)
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
        
        public int DeleteDiscipline(string s)
        {
            int id;
            this.conn.Open();
            string query = "SELECT id_discipline FROM discipline WHERE Название = '" + s + "'";
            MySqlCommand command = new MySqlCommand(query, this.conn);
            id = (int) command.ExecuteScalar();
            
            query = "DELETE FROM discipline WHERE Название = '" + s + "'";
            command = new MySqlCommand(query, this.conn);
            command.ExecuteNonQuery();
            this.conn.Close();
            return id;
        }

        public void DeleteDisciplineFromTeacher(int id)
        {
            this.conn.Open();
            string query = "DELETE FROM teacher WHERE id_discipline = " + id;
            MySqlCommand command = new MySqlCommand(query, this.conn);
            command.ExecuteNonQuery();
            this.conn.Close();
        }
    }
}