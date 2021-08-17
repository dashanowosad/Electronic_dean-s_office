using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Diploma_v2.DopWindow.AreYouShure;
using MySql.Data.MySqlClient;
namespace Diploma_v2
{
    public class ComitteeDB
    {
        private MySqlConnection conn;
        private List<ColumnTable> list;
        private Shure_Window _shureWindow;

        public ComitteeDB(string connStr)
        {
            this.conn = new MySqlConnection(connStr);
            this._shureWindow = new Shure_Window();
        }
        public void AddNewComittee(MainWindow w, DataGrid dataGrid, string discipline)
        {
            try
            {
                int discipline_index = this.FindDiscipline(discipline);
                if (discipline_index == -1)
                {
                    MessageBox.Show("Error");
                    return;
                }
                int update = this.UpdateComittee(w, discipline_index);
                if (update == 1)
                {
                    this.list = dataGrid.ItemsSource as List<ColumnTable>;
                    this.conn.Open();

                    List<String> query = new List<String>();

                    foreach (var i in this.list)
                    {
                        query.Add(
                            @"INSERT INTO comittee(Фамилия_инициалы, Ученая_степень, Звания_должности, Должность_комиссии, id_discipline)
                                    VALUES('" + i.fio + "','" + i.academic_degree + "','" + i.post + "','" + i.status +
                            "'," + discipline_index + ')');
                    }

                    foreach (var i in query)
                    {
                        MySqlCommand command = new MySqlCommand(i, this.conn);
                        command.ExecuteNonQuery();
                    }

                    this.conn.Close();
                    MessageBox.Show("Комиссия успешно создана!");
                    this.list = null;
                    dataGrid.ItemsSource = null;
                }
            }
            catch (Exception e)
            {
                
                MessageBox.Show("Не добавлено ни одного участника комиссии");
            }
        }

        public int UpdateComittee(MainWindow w, int discipline_index)
        {
            this.conn.Open();
            string query = "SELECT count(*) FROM comittee WHERE id_discipline =" + discipline_index;
            MySqlCommand command = new MySqlCommand(query, this.conn);
            string count =   command.ExecuteScalar().ToString();
            
            if (count != "0")
            {
                this._shureWindow.Are_You_Shure(w, "Заменить комиссию по данному предмету?");
                if (this._shureWindow.GetResult().Equals("Yes"))
                {
                    query = "DELETE FROM comittee WHERE id_discipline =" + discipline_index;
                    command = new MySqlCommand(query, this.conn);
                    command.ExecuteNonQuery();
                    this.conn.Close();
                    return 1;
                }
                else
                {
                    this.conn.Close();
                    return 0;
                }
                
            }
            this.conn.Close();
            return 1;
        }

        private int FindDiscipline(string name)
        {
            int id = -1;
            this.conn.Open();
            string query = "SELECT id_discipline FROM discipline WHERE Название = '" + name + "'";
            
            MySqlCommand command = new MySqlCommand(query, this.conn);
            try
            {
                id = (int) command.ExecuteScalar();
            }
            catch
            {
                id = -1;
            }
            this.conn.Close();
            return id;
        }
        public void DeleteComittee()
        {
            this.conn.Open();
            string query = "DELETE FROM comittee";
           
            MySqlCommand command = new MySqlCommand(query, this.conn);
            command.ExecuteNonQuery();
            
            this.conn.Close();
        }
    }

}