using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
namespace Diploma_v2
{
    public class OrderDB
    {
        private MySqlConnection conn;
        public OrderDB(string connStr)
        {
            this.conn = new MySqlConnection(connStr);
        }
        
        public void CreateOrderDB()
        {
            this.conn.Open();
            
            List<String> query = new List<String>();
            //зачисление
            query.Add( "INSERT INTO order_student VALUES (1, '№9/41-17', 'О зачислении', '2017-07-17', 1)");
            query.Add( "INSERT INTO order_student VALUES (2, '№9/41-17', 'О зачислении', '2017-07-17', 2)");
            query.Add( "INSERT INTO order_student VALUES (3, '№22/45-17', 'О зачислении', '2017-07-17', 3)");
            query.Add( "INSERT INTO order_student VALUES (4, '№4/879o-18', 'О зачислении', '2018-08-03', 4)");
            query.Add( "INSERT INTO order_student VALUES (5, '№2/352-18', 'О зачислении', '2018-08-03', 5)");
            query.Add( "INSERT INTO order_student VALUES (6, '№4/879o-18', 'О зачислении', '2018-08-03', 6)");
            query.Add( "INSERT INTO order_student VALUES (7, '№7/13-19', 'О зачислении', '2019-08-15', 7)");
            query.Add( "INSERT INTO order_student VALUES (8, '№6/182-19', 'О зачислении', '2019-08-15', 8)");
            query.Add( "INSERT INTO order_student VALUES (9, '№6/182-19', 'О зачислении', '2019-08-15', 9)");
            query.Add( "INSERT INTO order_student VALUES (10, '№5/12-19', 'О зачислении', '2019-08-15', 10)");
            //прочее
            query.Add( "INSERT INTO order_student VALUES (11, '№132/40o-18', 'Об отчислении', '2018-06-10', 4)");
            query.Add( "INSERT INTO order_student VALUES (12, '№2/15-20', 'Об отчислении', '2020-03-25', 6)");
            query.Add( "INSERT INTO order_student VALUES (13, '№7/14-18', 'О взятии академического отпуска', '2018-11-02', 1)");
            query.Add( "INSERT INTO order_student VALUES (14, '№25/3-20', 'О назначении научного руководителя', '2020-05-24', 8)");
            query.Add( "INSERT INTO order_student VALUES (15, '№33/8-21', 'О взятии академического отпуска', '2021-03-17', 8)");
            query.Add( "INSERT INTO order_student VALUES (16, '№213/9-20', 'О переводе', '2020-08-16', 10)");
            foreach (var i in query) 
            {
                MySqlCommand command = new MySqlCommand(i, this.conn);
                command.ExecuteNonQuery();
            }
            this.conn.Close();
        }
        
        public void LoadOrder(DataGrid dataGridView1, string id_student)
        {
            this.conn.Open();
            string query = @"SELECT 
                id_order AS '№',
               Номер, 
               Приказ,
               DATE_FORMAT(Дата, '%d/%m/%Y') AS 'Дата'
            FROM order_student
               WHERE id_student";
            query += '=' + id_student;
            MySqlCommand command = new MySqlCommand(query, this.conn);

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);


            DataTable ds = new DataTable("order");
            adapter.Fill(ds);

            dataGridView1.ItemsSource = ds.DefaultView;
            
            this.conn.Close();
        }

        public void DeleteOrders()
        {
            this.conn.Open();
            string query = "DELETE FROM order_student";

            MySqlCommand command = new MySqlCommand(query, this.conn);
            command.ExecuteNonQuery();

            this.conn.Close();
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