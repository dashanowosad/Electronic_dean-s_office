using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
namespace Diploma_v2.DopWindow.Statistic
{
    public class GetStatistic
    {
        private MySqlConnection conn;
        public GetStatistic(string connStr)
        {
            this.conn = new MySqlConnection(connStr);
        }

        public float SrStudent(TextBlock result, string studentText)
        {
            this.conn.Open();
            string query = "SELECT id_student FROM student WHERE ФИО = '" + studentText + "'";
            
            MySqlCommand command = new MySqlCommand(query, this.conn);
            string id = "-1";
            try
            {
                id = command.ExecuteScalar().ToString();
            }
            catch
            {
                this.conn.Close();
                MessageBox.Show("Данного студента не существует");
                return -1;
            }

            query = "SELECT Отметка FROM mark, rating WHERE id_student = " + id +
                    " AND mark.id_rating = rating.id_rating";
            command = new MySqlCommand(query, this.conn);
            MySqlDataReader reader = command.ExecuteReader();

            float sum = 0f;
            int count = 0;
            while (reader.Read())
            {
                if (reader[0].ToString().Equals("Отлично"))
                {
                    sum += 5f;
                    ++count;
                }

                if (reader[0].ToString().Equals("Хорошо"))
                {
                    sum += 4f;
                    ++count;
                }
                if (reader[0].ToString().Equals("Удовлетворительно"))
                {
                    sum += 3f;
                    ++count;
                }
                if (reader[0].ToString().Equals("Не удовлетворительно"))
                {
                    sum += 2f;
                    ++count;
                }
            }

            sum /= count;
            reader.Close();
            this.conn.Close();
            result.Text = sum.ToString();
            return sum;
        }

        public string GetStudentInGroup(string groupText)
        {
            this.conn.Open();
            string s = "";
            string query = "SELECT ФИО FROM student WHERE Группа = '" + groupText + "'";
            
            MySqlCommand command = new MySqlCommand(query, this.conn);
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                    s += reader[0].ToString() + "|";
            }

            s = s.Substring(0, s.Length - 1);
            reader.Close();
            this.conn.Close();

            return s;
        }

        public int FiveStudent(string studentText)
        {
            this.conn.Open();
            string query = "SELECT id_student FROM student WHERE ФИО = '" + studentText + "'";
            
            MySqlCommand command = new MySqlCommand(query, this.conn);
            string id = "-1";
            try
            {
                id = command.ExecuteScalar().ToString();
            }
            catch
            {
                this.conn.Close();
                MessageBox.Show("Данного студента не существует");
                return -1;
            }

            query = "SELECT Отметка FROM mark, rating WHERE id_student = " + id +
                    " AND mark.id_rating = rating.id_rating";
            command = new MySqlCommand(query, this.conn);
            MySqlDataReader reader = command.ExecuteReader();
            
            int count5 = 0, count4 = 0, count3 = 0, count2 = 0;
            while (reader.Read())
            {
                if (reader[0].ToString().Equals("Отлично"))
                    ++count5;

                if (reader[0].ToString().Equals("Хорошо"))
                    ++count4;
                if (reader[0].ToString().Equals("Удовлетворительно"))
                    ++count3;
                if (reader[0].ToString().Equals("Не удовлетворительно"))
                    ++count2;
            }
            
            reader.Close();
            this.conn.Close();
            if (count4 == 0 && count3 == 0 && count2 == 0 && count5 != 0)
                return 1;
            return 0;
        }

        public int NoFiveStudent(string studentText)
        {
            this.conn.Open();
            string query = "SELECT id_student FROM student WHERE ФИО = '" + studentText + "'";
            
            MySqlCommand command = new MySqlCommand(query, this.conn);
            string id = "-1";
            try
            {
                id = command.ExecuteScalar().ToString();
            }
            catch
            {
                this.conn.Close();
                MessageBox.Show("Данного студента не существует");
                return -1;
            }

            query = "SELECT Отметка FROM mark, rating WHERE id_student = " + id +
                    " AND mark.id_rating = rating.id_rating";
            command = new MySqlCommand(query, this.conn);
            MySqlDataReader reader = command.ExecuteReader();
            
            int count2 = 0;
            while (reader.Read())
            {
              
                if (reader[0].ToString().Equals("Не удовлетворительно"))
                    ++count2;
            }
            
            reader.Close();
            this.conn.Close();
            if (count2 != 0)
                return 1;
            return 0;
        }
    }
}