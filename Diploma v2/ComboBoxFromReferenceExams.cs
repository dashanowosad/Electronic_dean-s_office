using System.Windows.Controls;
using MySql.Data.MySqlClient;
namespace Diploma_v2
{
    public class ComboBoxFromReferenceExams
    {
        private MySqlConnection conn;
        public ComboBoxFromReferenceExams(string connStr)
        {
            this.conn = new MySqlConnection(connStr);
        }

        public void DisciplineComboBox(ComboBox comboBox)
        {
            comboBox.Text = comboBox.Text.Replace("\'", "");
            comboBox.Text = comboBox.Text.Replace("\"", "");
            this.conn.Open();
            string query = @"SELECT Название FROM discipline, comittee
            WHERE discipline.id_discipline = comittee.id_discipline GROUP BY Название ORDER BY Название";
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
       
        public void MarkComboBox(ComboBox comboBox)
        {
            this.conn.Open();
            string query = @"SELECT Отметка FROM rating GROUP BY Отметка";
            MySqlCommand command = new MySqlCommand(query, this.conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                comboBox.Items.Add(reader[0].ToString());
            reader.Close();

            this.conn.Close();
        }
    }
}