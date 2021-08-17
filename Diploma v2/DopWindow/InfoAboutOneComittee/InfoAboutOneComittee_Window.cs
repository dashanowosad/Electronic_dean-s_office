using System.Windows;

namespace Diploma_v2.DopWindow.InfoAboutOneComittee
{
    public class InfoAboutOneComittee_Window
    {
        private OrderDB _orderDb;
        public InfoAboutOneComittee_Window(string StudDb)
        {
            this._orderDb = new OrderDB(StudDb);
        }

        public void ShowInfoAboutOneComittee(Window MainWindow, string discipline)
        {
            AboutOneComittee_Window aboutOneComittee = new AboutOneComittee_Window(discipline);
            aboutOneComittee.Owner = MainWindow;
            aboutOneComittee.Show();
        }
    }
}