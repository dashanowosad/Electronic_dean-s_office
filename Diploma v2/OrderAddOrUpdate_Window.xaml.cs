using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Diploma_v2.DopWindow.Orders;
namespace Diploma_v2
{
    public partial class OrderAddOrUpdate_Window : Window
    {
        private int id_stud;
        private int id_order;
        private OrderWindowController _orderWindowController;
        public OrderAddOrUpdate_Window(int idStud, int idOrder)
        {
            this.id_stud = idStud;
            this.id_order = idOrder;
            this._orderWindowController = new OrderWindowController();
            InitializeComponent();
        }


        private void LoadInformation(object sender, RoutedEventArgs e)
        {
            this._orderWindowController.LoadInformation(this.Order);
            if (this.id_order != -1)
            {
                string str = this._orderWindowController.LoadInfo(this.id_order, this.id_stud);
                string[] s = str.Split('|');
                this.Number.Text = s[0];
                this.Order.Text = s[1];
                this.Date.Text = s[2];
                this.Order.IsDropDownOpen = false;
            }
        }

        private void DownList(object sender, KeyEventArgs e)
        {
            ComboBox comboBox = (ComboBox) sender;
            comboBox.IsDropDownOpen = true;
        }

        private void InputComboBoxText(object sender, TextChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox) sender;
            comboBox.IsDropDownOpen = true;
            this._orderWindowController.Search(comboBox);
        }

        private void AddOrUpdateOrder(object sender, RoutedEventArgs e)
        {
            string add = "";
            string date1;
            string[] d = this.Date.Text.Split('.');
            date1 = d[2] + '-' + d[1] + '-' + d[0];
            add += "'" + this.Number.Text + "' , ";
            add += "'" + this.Order.Text + "' , ";
            add += "'" +date1 + "' , ";
            add += this.id_stud;

            try
            {
                if (this.id_order == -1)
                {
                    this._orderWindowController.AddNewOrder(add, id_stud);
                    MessageBox.Show("Добавление успешно!");
                }
                else
                {
                    this._orderWindowController.Update(add, id_stud, id_order);
                    MessageBox.Show("Изменения применены!");
                }
            }
            catch
            {
                MessageBox.Show("Не все поля заполнены");
            }
        }
    }
}