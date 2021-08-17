using System.Windows.Controls;
using MySql.Data.MySqlClient;

namespace Diploma_v2.WorkWithDB
{
    public class LoadInfo
    {
        private MySqlConnection conn;
        public LoadInfo(string connStr)
        {
            this.conn = new MySqlConnection(connStr);
        }

        public void Load_Direction(ComboBox comboBox)
        {
            this.conn.Open();
            string query = @"SELECT 
                Номер, 
                Название 
            FROM field_study ORDER BY Название";
            MySqlCommand command = new MySqlCommand(query, this.conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                comboBox.Items.Add(reader[0].ToString() + " - " + reader[1].ToString());
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
                comboBox.Items.Add(reader[0].ToString() + " - " + reader[1].ToString());
            reader.Close();

            this.conn.Close();
        }

        public string InfoAboutStudent(int id_stud)
        {
            string str = "";
            this.conn.Open();
            string query = @"SELECT 
                ФИО, 
                ФИО_дат_падеж,
                пол,
                CONCAT(CONCAT(Номер, ' - '), Название),
                DATE_FORMAT(Дата_поступления, '%d/%m/%Y'), 
                DATE_FORMAT(Дата_окончания, '%d/%m/%Y'), 
                Степень_диплома, 
                Форма_обучения, 
                Форма_оплаты,
                Группа
            FROM student, field_study
                WHERE student.id_direction = field_study.id_direction AND id_student =";
            query += id_stud;
            MySqlCommand command = new MySqlCommand(query, this.conn);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
                str = reader[0].ToString() + '|' + reader[1].ToString() + '|' + reader[2].ToString() + '|' +
                      reader[3].ToString() + '|' + reader[4].ToString() + '|' + reader[5].ToString() + '|' +
                      reader[6].ToString() + '|' + reader[7].ToString() + '|' + reader[8].ToString() + '|' + reader[9].ToString();
            reader.Close();
            this.conn.Close();
            return str;
        }
    }
}