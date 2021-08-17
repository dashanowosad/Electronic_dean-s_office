using System.Windows.Controls;
using MySql.Data.MySqlClient;
namespace Diploma_v2.DopWindow.Statistic
{
    public class LoadComboBoxStatistic
    {
        private MySqlConnection conn;
        public LoadComboBoxStatistic(string connStr)
        {
            this.conn = new MySqlConnection(connStr);
        }

        public void Search(string q, ComboBox comboBox)
        {
            comboBox.Text = comboBox.Text.Replace("\'", "");
            comboBox.Text = comboBox.Text.Replace("\"", "");
            comboBox.Items.Clear();
            this.conn.Open();
            string query = q;
            
            MySqlCommand command = new MySqlCommand(query, this.conn);
            MySqlDataReader reader = command.ExecuteReader();
            
            
            comboBox.Items.Add("Все");
            while (reader.Read())
                if (comboBox.Name.Equals("Field_study"))
                    comboBox.Items.Add(reader[0].ToString() + " - " + reader[1].ToString());
                else
                    comboBox.Items.Add(reader[0].ToString());
            reader.Close();

            this.conn.Close();
        }

        public void LoadComboBoxs(string s, ComboBox fieldStudy)
        {
            this.Search(s, fieldStudy);
        }

        public string SelectIdField_Study(string number)
        {
            this.conn.Open();
            string query = @"SELECT 
                id_direction 
            FROM field_study WHERE Номер = '" + number + "'";
            MySqlCommand command = new MySqlCommand(query, this.conn);
            
            string id = "-1";
            try
            {
                id = command.ExecuteScalar().ToString();
            }
            catch
            {
                this.conn.Close();
                return id;
            }
            
            this.conn.Close();
            return id;
        }
    }
}