using System.Windows;
using MySql.Data.MySqlClient;
namespace Diploma_v2.DopWindow.Orders
{
    public class AddOrUpdateOrder
    {
        private MySqlConnection conn;
        public AddOrUpdateOrder(string connStr)
        {
            this.conn = new MySqlConnection(connStr);
        }
        
        public int MaxId(string q)
        {
            this.conn.Open();
            string query = q;
            MySqlCommand command = new MySqlCommand(query, this.conn);
            int id = (int) command.ExecuteScalar();
            ++id;
            
            this.conn.Close();
            
            return id;
        }

        public void AddNewOrder(string q, int idStud, int idOrder = -1)
        {
            string query = "SELECT MAX(id_order) FROM order_student";
            int id = this.MaxId(query);
            if (idOrder != -1)
                id = idOrder;
            this.conn.Open();
            query = "INSERT INTO order_student VALUES (";
            query += id + ", " + q + ')';
            MySqlCommand command = new MySqlCommand(query, this.conn);
            command.ExecuteNonQuery();
            this.conn.Close();
        }

        public string InfoAboutMark(int idOrder, int idStud)
        {
            string str = "";
            this.conn.Open();
            string query = @"SELECT 
                Номер, 
                Приказ,
                Дата
            FROM order_student
                WHERE id_student =";
            query += idStud + " AND id_order = " + idOrder;
            MySqlCommand command = new MySqlCommand(query, this.conn);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
                str = reader[0].ToString() + '|' + reader[1].ToString() + '|' + reader[2].ToString();
            reader.Close();
            this.conn.Close();
            return str;
        }

        public void DeleteOrder(int idOrder)
        {
            this.conn.Open();
            string query = "DELETE FROM order_student WHERE id_order = " + idOrder;

            MySqlCommand command = new MySqlCommand(query, this.conn);
            command.ExecuteNonQuery();

            this.conn.Close();
        }
    }
}