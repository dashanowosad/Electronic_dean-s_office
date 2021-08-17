using System.Windows;
using System.Windows.Controls;
namespace Diploma_v2.DopWindow.Orders
{
    public class Order_Window
    {
        private OrderDB _orderDb;
        public Order_Window(string StudDb)
        {
            this._orderDb = new OrderDB(StudDb);
        }

        public void Insert_New_Order_Or_Update(Window MainWindow, int idStud, DataGrid orderTable, int idOrder = -1)
        {
            OrderAddOrUpdate_Window orderAddOrUpdateWindow = new OrderAddOrUpdate_Window(idStud, idOrder);
            orderAddOrUpdateWindow.Owner = MainWindow; 
            if (idOrder == -1)
                orderAddOrUpdateWindow.Title = "Добавление нового приказа";
            else
                orderAddOrUpdateWindow.Title = "Редактирование приказа";
            orderAddOrUpdateWindow.Show();
            
            orderAddOrUpdateWindow.Closed += (s, eventarg) =>
            {
                this._orderDb.LoadOrder(orderTable, idStud.ToString());
            };
        }

    }
}