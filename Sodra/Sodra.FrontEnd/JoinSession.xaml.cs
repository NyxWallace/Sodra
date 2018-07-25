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
    /// Interaction logic for JoinSession.xaml
    /// </summary>
    public partial class JoinSession : Window
    {
        public JoinSession()
        {
            DataContext = this;
            InitializeComponent();
        }

        public string CharacterName { get; set; }

        private void Ok_click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CharacterName))
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
