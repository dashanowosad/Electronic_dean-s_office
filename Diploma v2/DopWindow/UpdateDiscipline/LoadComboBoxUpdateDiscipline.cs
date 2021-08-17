using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
namespace Diploma_v2.DopWindow.DeleteDiscipline
{
    public class LoadComboBoxUpdateDiscipline
    {
        private MySqlConnection conn;
        public LoadComboBoxUpdateDiscipline(string connStr)
        {
            this.conn = new MySqlConnection(connStr);
        }
        public void Load(ComboBox comboBox)
        {
            comboBox.Items.Clear();
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

        public void Search(ComboBox comboBox)
        {
            comboBox.Text = comboBox.Text.Replace("\'", "");
            comboBox.Text = comboBox.Text.Replace("\"", "");
            comboBox.Items.Clear();
            this.conn.Open();
            string query =  "SELECT Название FROM discipline WHERE Название LIKE '%" + comboBox.Text + "%' ORDER BY Название";
            
            MySqlCommand command = new MySqlCommand(query, this.conn);
            MySqlDataReader reader = command.ExecuteReader();
            
            
            while (reader.Read())
                if (reader[0].ToString() != "-")
                    comboBox.Items.Add(reader[0].ToString());
            reader.Close();

            this.conn.Close();
        }
    }
}