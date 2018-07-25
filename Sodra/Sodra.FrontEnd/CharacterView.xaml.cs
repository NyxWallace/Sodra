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
    /// Interaction logic for CharacterView.xaml
    /// </summary>
    public partial class CharacterView : Window
    {
        public CharacterView(DTO.Character character)
        {
            CharName = character.Name;
            Health = character.Health;
            Class = character.Class.ToString();
            Race = character.Race.ToString();
            DataContext = this;
            InitializeComponent();
        }

        public string CharName { get; set; }
        public string Health { get; set; }
        public string Class { get; set; }
        public string Race { get; set; }
    }
}
