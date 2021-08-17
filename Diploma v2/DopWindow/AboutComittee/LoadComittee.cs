using MySql.Data.MySqlClient;
namespace Diploma_v2.DopWindow.AboutComittee
{
    public class LoadComittee
    {
        private MySqlConnection conn;
        public LoadComittee(string studDb)
        {
            this.conn = new MySqlConnection(studDb);
        }

        public string FindDisciplineID()
        {
            string id = "";
            this.conn.Open();
            string query = @"SELECT 
                id_discipline
            FROM comittee 
            GROUP BY id_discipline";
            MySqlCommand command = new MySqlCommand(query, this.conn);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    id += reader[0].ToString() + ",";
                }

                id = id.Substring(0, id.Length - 1);
            }

            reader.Close();
            
            this.conn.Close();
            return id;
        }
        
        public string LoadText(string id_discipline)
        {
            
            string Comm = "";
            this.conn.Open();
//председатель            
            string query = @"SELECT 
                Название,
                Фамилия_инициалы,
                Ученая_степень,
                Звания_должности
            FROM comittee, discipline 
            WHERE Должность_комиссии = ";
            query += "'председатель' AND comittee.id_discipline = " + id_discipline;
            query += " AND comittee.id_discipline = discipline.id_discipline";
            MySqlCommand command = new MySqlCommand(query, this.conn);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                Comm += "|Председатель: ";
                while (reader.Read())
                {
                    Comm = reader[0].ToString() + Comm;
                    Comm += reader[1].ToString() + " " + reader[2].ToString() + " " + reader[3].ToString() + "\n";
                }
            }
            reader.Close();
//члены комиссии
            query = @"SELECT 
                Фамилия_инициалы,
                Ученая_степень,
                Звания_должности
            FROM comittee 
            WHERE Должность_комиссии = ";
            query += "'член комиссии' AND id_discipline = " + id_discipline;
            command = new MySqlCommand(query, this.conn);
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                Comm += "Члены комиссии: ";
                while (reader.Read())
                {
                    Comm += "\n" + reader[0].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString() + "; ";
                }
            }
            reader.Close();
            this.conn.Close();
            return Comm;
        }
    }
}