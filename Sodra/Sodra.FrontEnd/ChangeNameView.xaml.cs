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
    /// Interaction logic for ChangeNameView.xaml
    /// </summary>
    public partial class ChangeNameView : Window
    {
        public ChangeNameView(string name)
        {
            NewName = name;
            DataContext = this;
            InitializeComponent();
        }

        public string NewName { get; set; }

        private void Ok_click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Cancel_click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
