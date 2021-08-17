using System.Windows;
using System.Windows.Controls;
using Diploma_v2.DopWindow.AboutComittee;
namespace Diploma_v2
{
    public partial class AboutComittee_Window : Window
    {
        private LoadComittee _loadComittee;
        private string StudDb = "server=localhost;user=root;database=diplom;password=Crostels325;";
        public AboutComittee_Window()
        {
            this._loadComittee = new LoadComittee(this.StudDb);
            InitializeComponent();
        }

        private void LoadComittee(object sender, RoutedEventArgs e)
        {
            
            string id = this._loadComittee.FindDisciplineID();
            string Comm = "";
            if (id != "")
            {
                string[] id_discipline = id.Split(',');
                foreach (var i in id_discipline)
                {
                    Comm = this._loadComittee.LoadText(i);
                    if (Comm != "")
                    {
                        string[] s = Comm.Split('|');
                        TextBlock textBlock = new TextBlock();
                        textBlock.Text = s[0];
                        textBlock.FontSize = 14;
                        textBlock.FontWeight = FontWeights.Bold;
                        textBlock.Margin = new Thickness(10);
                        this.StackPanel.Children.Add(textBlock);
                        
                        textBlock = new TextBlock();
                        textBlock.Text = s[1];
                        textBlock.FontSize = 14;
                        textBlock.Margin = new Thickness(10, 0, 10, 10);
                        this.StackPanel.Children.Add(textBlock);
                    }
                }
            }
        }
    }
}