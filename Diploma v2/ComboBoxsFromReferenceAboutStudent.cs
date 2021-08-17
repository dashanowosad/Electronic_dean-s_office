using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
namespace Diploma_v2
{
    public class ComboBoxsFromReferenceAboutStudent
    {
        private MySqlConnection conn;
        public ComboBoxsFromReferenceAboutStudent(string connStr)
        {
            this.conn = new MySqlConnection(connStr);
        }
        
        public void StudentComboBox(ComboBox comboBox)
        {
            this.conn.Open();
            string query = @"SELECT ФИО FROM student ORDER BY ФИО";
            MySqlCommand command = new MySqlCommand(query, this.conn);
            MySqlDataReader reader = command.ExecuteReader();
            comboBox.Items.Clear();
            while (reader.Read())
                comboBox.Items.Add(reader[0].ToString());
            reader.Close();
            
            this.conn.Close();
        }
        
        public void ScienceWorkANDChiefComboBox(ComboBox comboBox)
        {
            comboBox.Text = comboBox.Text.Replace("\'", "");
            comboBox.Text = comboBox.Text.Replace("\"", "");
            this.conn.Open();
            string query = @"SELECT Фамилия_инициалы FROM teacher GROUP BY id_one_teacher ORDER BY Фамилия_инициалы";
            MySqlCommand command = new MySqlCommand(query, this.conn);
            MySqlDataReader reader = command.ExecuteReader();
            comboBox.Items.Clear();
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
                comboBox.Items.Add(reader[0].ToString());
            reader.Close();

            this.conn.Close();
        }
    }
}