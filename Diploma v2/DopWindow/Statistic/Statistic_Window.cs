using System.Windows;

namespace Diploma_v2.DopWindow.Statistic
{
    public class Statistic_Window
    {
       
        public Statistic_Window()
        { }

        public void Load_Statistic(Window MainWindow, int flag = 0)
        {
            AboutStudent aboutStudent = new AboutStudent(flag);
            aboutStudent.Owner = MainWindow; 
            if (flag == 0)
                aboutStudent.Title = "Средний балл";
            else if (flag == 1)
                aboutStudent.Title = "Количество отличников";
            else
                aboutStudent.Title = "Количество не успевающих";
            aboutStudent.Show();
            
        }
    }
}