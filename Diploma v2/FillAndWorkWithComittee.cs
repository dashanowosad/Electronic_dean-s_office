using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Diploma_v2
{
    public class FillAndWorkWithComittee
    {
        private List <ColumnTable> list;

        public FillAndWorkWithComittee()
        {
            this.list = new List<ColumnTable>();
        }
        public void AddToTable(DataGrid dataGrid1, DataGrid dataGridTeachers, string status)
        {
            try
            {
                bool doubleAddFlag = false;
                DataRowView dataRow = (DataRowView) dataGridTeachers.SelectedItem;
                string fio = dataRow.Row.ItemArray[2].ToString();
                string academic_degree = dataRow.Row.ItemArray[3].ToString();
                string post = dataRow.Row.ItemArray[4].ToString();

                switch (status)
                {
                    case "председателя":
                        status = "председатель";
                        break;
                    case "члена комиссии":
                        status = "член комиссии";
                        break;
                }

                // MessageBox.Show(fio + ' ' + academic_degree + ' ' + post + ' ' + status);

                if (this.list != null)
                {
                    for (int i = 0; i < this.list.Count; ++i)
                    {
                        if (this.list[i].fio == fio &&
                            this.list[i].academic_degree == academic_degree &&
                            this.list[i].post == post)
                        {
                            MessageBox.Show("Нельзя добавить повторно");
                            doubleAddFlag = true;
                            break;
                        }

                        if (this.list[i].status.Equals("председатель") &&
                            status.Equals("председатель"))
                        {
                       
                            MessageBox.Show("Председатель уже добавлен");
                            doubleAddFlag = true;
                            break;
                        }
                    }
                }
                if (!doubleAddFlag)
                {
                    this.list.Add(new ColumnTable{fio = fio, academic_degree = academic_degree, post = post, status = status});
                    dataGrid1.ItemsSource = null;
                    dataGrid1.ItemsSource = this.list;
                }
                
                
            }
            catch (Exception e)
            {
                MessageBox.Show("Выберите преподавателя, которого хотите добавить");
            }
        }

        public void DeleteOneFromComittee(DataGrid dataGrid)
        {
            int index = dataGrid.SelectedIndex;
            if (index >= 0)
            {
                this.list.RemoveAt(index);
                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = this.list;
            }
        }

        public void DeleteAll(DataGrid dataGrid)
        {
            this.list = new List<ColumnTable>();
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = this.list;
        }

        public void ClearList()
        {
            this.list = new List<ColumnTable>();
        }
    }

    public class ColumnTable
    {
        public string fio { get; set; }
        public string academic_degree { get; set; }
        public string post { get; set; }
        public string status { get; set; }
    }
    
}

