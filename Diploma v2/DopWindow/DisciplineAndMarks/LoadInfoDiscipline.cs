using System.Windows.Controls;
using MySql.Data.MySqlClient;
namespace Diploma_v2.DopWindow.DisciplineAndMarks
{
    public class LoadInfoDiscipline
    {
        private MySqlConnection conn;
        public LoadInfoDiscipline(string connStr)
        {
            this.conn = new MySqlConnection(connStr);
        }
        
        public void Load_Discipline(ComboBox comboBox)
        {
            this.conn.Open();
            string query = @"SELECT 
                Название 
            FROM discipline ORDER BY Название";
            MySqlCommand command = new MySqlCommand(query, this.conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                if (!reader[0].Equals('-'))
                    comboBox.Items.Add(reader[0].ToString());
            reader.Close();
            
            this.conn.Close();
        }
        
        public void Load_Mark(ComboBox comboBox)
        {
            this.conn.Open();
            string query = @"SELECT 
                Отметка 
            FROM rating ORDER BY Отметка";
            MySqlCommand command = new MySqlCommand(query, this.conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                comboBox.Items.Add(reader[0].ToString());
            reader.Close();
            
            this.conn.Close();
        }

        public void FindFromComboBox(ComboBox comboBox, string q)
        {
            comboBox.Text = comboBox.Text.Replace("\'", "");
            comboBox.Text = comboBox.Text.Replace("\"", "");
            this.conn.Open();
            string query = q;
            
            MySqlCommand command = new MySqlCommand(query, this.conn);
            MySqlDataReader reader = command.ExecuteReader();
            
            comboBox.Items.Clear();
            while (reader.Read())
                if (!reader[0].Equals('-'))
                    comboBox.Items.Add(reader[0].ToString());
            reader.Close();

            this.conn.Close();
        }

        public string InfoAboutMark(int id_mark, int id_stud)
        {
            
            string str = "";
            this.conn.Open();
            string query = @"SELECT 
                Название, 
                Период_контроля,
                Вид_контроля,
                Отметка,
                Дата_получения
            FROM discipline, rating, mark
                WHERE mark.id_discipline = discipline.id_discipline AND 
                        mark.id_rating = rating.id_rating AND 
                        mark.id_student =";
            query += id_stud + " AND id_mark = " + id_mark;
            MySqlCommand command = new MySqlCommand(query, this.conn);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
                str = reader[0].ToString() + '|' + reader[1].ToString() + '|' + reader[2].ToString() + '|' +
                      reader[3].ToString() + '|' + reader[4].ToString();
            reader.Close();
            this.conn.Close();
            return str;
        }
    }
}