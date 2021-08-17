using System.Windows.Controls;
namespace Diploma_v2.DopWindow.Orders
{
    public class OrderWindowController
    {
        private string StudDb = "server=localhost;user=root;database=diplom;password=Crostels325;";
        private LoadInfoOrder _loadInfoOrder;
        private AddOrUpdateOrder _addOrUpdateOrder;

        public OrderWindowController()
        {
            this._loadInfoOrder = new LoadInfoOrder(this.StudDb);
            this._addOrUpdateOrder = new AddOrUpdateOrder(this.StudDb);
        }

        public void LoadInformation(ComboBox orders)
        {
            this._loadInfoOrder.Load_Orders(orders);
        }

        public void Search(ComboBox comboBox)
        {
            comboBox.Text = comboBox.Text.Replace("\'", "");
            comboBox.Text = comboBox.Text.Replace("\"", "");
            string q = @"SELECT  Приказ FROM order_student 
                GROUP BY Приказ
                HAVING Приказ LIKE '%" + comboBox.Text + "%'" +
                       " ORDER BY Приказ";
            this._loadInfoOrder.FindFromComboBox(comboBox, q);
        }

        public void AddNewOrder(string add, int idStud)
        {
            this._addOrUpdateOrder.AddNewOrder(add, idStud);
        }

        public string LoadInfo(int idOrder, int idStud)
        {
            return this._addOrUpdateOrder.InfoAboutMark(idOrder, idStud);
        }

        public void Update(string add, int idStud, int idOrder)
        {
            this._addOrUpdateOrder.DeleteOrder(idOrder);
            this._addOrUpdateOrder.AddNewOrder(add, idStud, idOrder);
        }
    }
}