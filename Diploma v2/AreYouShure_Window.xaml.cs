using System.Windows;
using System.Windows.Controls;

namespace Diploma_v2
{
    public partial class AreYouShure_Window : Window
    {
        private string result;
        public AreYouShure_Window(string text)
        {
            InitializeComponent();
            this.Text.Text = text;
        }

        private void ClickButton(object sender, RoutedEventArgs e)
        {
            Button button = (Button) sender;
            this.result = button.Name;
            this.Close();
        }

        public string GetChoose()
        {
            return this.result;
        }
    }
}