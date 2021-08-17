using System.Windows;

namespace Diploma_v2.DopWindow.AboutComittee
{
    public class AboutComitteeClass_Window
    {
        public void Show_Comittees(Window MainWindow)
        {
            AboutComittee_Window aboutComitteeWindow = new AboutComittee_Window();
            aboutComitteeWindow.Owner = MainWindow;
            aboutComitteeWindow.Show();
        }
    }
}