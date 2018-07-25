using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sodra.FrontEnd
{
    /// <summary>
    /// Interaction logic for CreateSession.xaml
    /// </summary>
    public partial class CreateSession : Window
    {
        public CreateSession()
        {
            DataContext = this;
            InitializeComponent();
        }

        public string SessionName { get; set; }
        public string CharacterName { get; set; }

        private void Ok_click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(SessionName) || string.IsNullOrEmpty(CharacterName))
            {
                MessageBox.Show("Fill all the boxes", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            this.DialogResult = true;
        }
        private void Cancel_click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
