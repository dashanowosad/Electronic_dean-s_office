using System.Windows;
namespace Diploma_v2.DopWindow.AreYouShure
{
    public class Shure_Window
    {
        private string result = "";
        private AreYouShure_Window areYouShureWindow;

        public void Are_You_Shure(Window MainWindow, string text)
        {
            areYouShureWindow = new AreYouShure_Window(text);
            areYouShureWindow.Owner = MainWindow;
            areYouShureWindow.Title = "Сообщение";
            
            areYouShureWindow.ShowDialog();
        }

        public string GetResult()
        {
            this.result = areYouShureWindow.GetChoose();
            return this.result;
        }
    }
}