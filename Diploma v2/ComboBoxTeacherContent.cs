using System.Windows.Controls;
using MySql.Data.MySqlClient;
namespace Diploma_v2
{
    public class ComboBoxTeacherContent
    {
        private MySqlConnection conn;
        public ComboBoxTeacherContent(string connStr)
        {
            this.conn = new MySqlConnection(connStr);
        }

        public void SelectFaculty(ComboBox comboBox)
        {
            this.conn.Open();
            string query = @"SELECT 
               Название_факультета 
            FROM faculty";
      
            MySqlCommand command = new MySqlCommand(query, this.conn);
            MySqlDataReader reader = command.ExecuteReader();

            
            comboBox.Items.Clear();
            comboBox.Items.Add("Все");
            while (reader.Read())
                comboBox.Items.Add(reader[0].ToString());
            
            reader.Close();

            comboBox.SelectedIndex = 0;

            this.conn.Close();
        }

        public void SelectDiscipline(ComboBox comboBox)
        {
            this.conn.Open();
            string query = @"SELECT 
               Название
            FROM discipline ORDER BY Название";
      
            MySqlCommand command = new MySqlCommand(query, this.conn);
            MySqlDataReader reader = command.ExecuteReader();

            comboBox.Items.Clear();
            while (reader.Read())
                comboBox.Items.Add(reader[0].ToString());

            reader.Close();

            comboBox.SelectedIndex = 0;

            this.conn.Close();
        }
    }
}