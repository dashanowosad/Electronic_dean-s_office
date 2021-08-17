using System.Windows.Controls;
using MySql.Data.MySqlClient;

namespace Diploma_v2.DopWindow.TeacherDisciplin
{
    public class LoadDiscipline
    {
        private MySqlConnection conn;
        public LoadDiscipline(string connStr)
        {
            this.conn = new MySqlConnection(connStr);
        }

        public void LoadDisciplines(StackPanel stackPanel)
        {
            this.conn.Open();
            string query = @"SELECT 
                Название 
            FROM discipline 
            ORDER BY Название";
            MySqlCommand command = new MySqlCommand(query, this.conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CheckBox checkBox = new CheckBox { Content = reader[0].ToString(), MinHeight = 20, IsChecked = false, FontSize = 14};
                stackPanel.Children.Add(checkBox);
            }

            reader.Close();
            
            this.conn.Close();
        }
    }
}