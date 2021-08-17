using System.Windows.Controls;
using MySql.Data.MySqlClient;
namespace Diploma_v2.DopWindow.Orders
{
    public class LoadInfoOrder
    {
        private MySqlConnection conn;
        public LoadInfoOrder(string connStr)
        {
            this.conn = new MySqlConnection(connStr);
        }
        
        public void Load_Orders(ComboBox comboBox)
        {
            this.conn.Open();
            string query = @"SELECT 
                Приказ 
            FROM order_student 
            GROUP BY Приказ
            ORDER BY Приказ";
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
                if (!reader[0].Equals('-'))
                    comboBox.Items.Add(reader[0].ToString());
            reader.Close();

            this.conn.Close();
        }
    }
}