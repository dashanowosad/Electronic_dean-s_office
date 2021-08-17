using System.Windows.Controls;
using MySql.Data.MySqlClient;
namespace Diploma_v2.DopWindow.Teacher
{
    public class LoadInfoTeacher
    {
        private MySqlConnection conn;
        public LoadInfoTeacher(string connStr)
        {
            this.conn = new MySqlConnection(connStr);
        }
        public void LoadComboBoxFaculty(ComboBox comboBox)
        {
            this.conn.Open();
            string query = @"SELECT 
                Название_факультета 
            FROM faculty 
            ORDER BY Название_факультета";
            MySqlCommand command = new MySqlCommand(query, this.conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                comboBox.Items.Add(reader[0].ToString());
            reader.Close();
            
            this.conn.Close();
        }

        public void LoadComboBoxStep(ComboBox comboBox)
        {
            this.conn.Open();
            string query = @"SELECT 
                Ученая_степень 
            FROM teacher 
            GROUP BY Ученая_степень
            ORDER BY Ученая_степень";
            MySqlCommand command = new MySqlCommand(query, this.conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                comboBox.Items.Add(reader[0].ToString());
            reader.Close();
            
            this.conn.Close();
        }
        public void LoadComboBoxTitles(ComboBox comboBox)
        {
            this.conn.Open();
            string query = @"SELECT 
                Звания_должности 
            FROM teacher 
            GROUP BY Звания_должности
            ORDER BY Звания_должности";
            MySqlCommand command = new MySqlCommand(query, this.conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                comboBox.Items.Add(reader[0].ToString());
            reader.Close();
            
            this.conn.Close();
        }

        public void FindFromComboBox(ComboBox comboBox, string q)
        {
            this.conn.Open();
            string query = q;
            
            MySqlCommand command = new MySqlCommand(query, this.conn);
            MySqlDataReader reader = command.ExecuteReader();
            
            comboBox.Items.Clear();
            while (reader.Read())
                comboBox.Items.Add(reader[0].ToString());
            reader.Close();

            this.conn.Close();
        }
        public string LoadInformation(int id)
        {
            string str = "";
            this.conn.Open();
            string query = @"SELECT 
                Название_факультета,
                Фамилия_инициалы, 
                Ученая_степень, 
                Звания_должности, 
                GROUP_CONCAT(Название)
            FROM teacher, discipline, faculty
                WHERE teacher.id_discipline = discipline.id_discipline AND
                      teacher.id_faculty = faculty.id_faculty AND id_one_teacher = ";
            query += id + " GROUP BY Фамилия_инициалы";
            
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