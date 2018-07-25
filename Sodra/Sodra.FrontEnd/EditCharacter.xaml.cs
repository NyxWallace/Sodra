using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sodra.FrontEnd
{
    /// <summary>
    /// Interaction logic for EditCharacter.xaml
    /// </summary>
    public partial class EditCharacter : Window, INotifyPropertyChanged
    {
        public EditCharacter(DTO.Character character)
        {
            Character = new DTO.Character()
            {
                ID = character.ID,
                Name = character.Name,
                Health = character.Health,
                Class = character.Class,
                Race = character.Race,
                Player = new DTO.Player() { ID = character.Player.ID }
                
            };
            DataContext = this;
            InitializeComponent();
        }

        public DTO.Character Character { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Fill_enums(object sender, RoutedEventArgs e)
        {
            classes.ItemsSource = Enum.GetValues(typeof(DTO.Class)).Cast<DTO.Class>();
            races.ItemsSource = Enum.GetValues(typeof(DTO.Race)).Cast<DTO.Race>();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Ok_click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        private void Cancel_click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        private void Change_image_click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                Character.ImagePath = dlg.FileName;
                OnPropertyChanged("Character");
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
