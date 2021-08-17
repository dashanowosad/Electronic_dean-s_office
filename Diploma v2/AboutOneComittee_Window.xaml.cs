using System.Windows;

namespace Diploma_v2
{
    public partial class AboutOneComittee_Window : Window
    {
        private string discipline;
        private References.References _references;
        public AboutOneComittee_Window(string discipline)
        {
            this.discipline = discipline;
            this._references = new References.References();

            InitializeComponent();
        }

        private void LoadText(object sender, RoutedEventArgs e)
        {
            this.Info.Text = this._references.FindComittee(this.discipline);
            this.Info.Text = this.Info.Text.Replace("Члены", "\nЧлены");
        }
    }
}