using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Sodra.DTO;
using System.Text.RegularExpressions;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Windows.Threading;
using System.ComponentModel;

namespace Sodra.FrontEnd
{
    /// <summary>
    /// Interaction logic for Session.xaml
    /// </summary>
    public partial class SessionView : Window, INotifyPropertyChanged
    {
        private string _connectionString
        {
            get
            {
                return MainWindow.ConnectionString;
            }
        }
        public SessionView(DTO.Session session, string name)
        {
            DataContext = this;

            initialize_session(session, name);

            _dispatcherTimer = new DispatcherTimer();

            InitializeComponent();
        }
        private void initialize_session(DTO.Session session, string name)
        {
            Character = session.Characters.Where(p => p.Player.Name == name).First();
            Characters.Clear();
            Logs.Clear();
            foreach (var c in session.Characters)
                if (!c.Equals(Character))
                    Characters.Add(c);
            foreach (var l in session.Logs)
                Logs.Add(l);
            this._ID = session.ID;
            this.SessionName = session.Name;
            OnPropertyChanged("SessionName");
        }

        private int _ID { get; set; }
        private string _message = string.Empty;
        private DispatcherTimer _dispatcherTimer = null;

        private ObservableCollection<DTO.Log> _logs;
        private ObservableCollection<DTO.Character> _characters;

        public event PropertyChangedEventHandler PropertyChanged;

        public string SessionID
        {
            get { return "ID: " + _ID; }
        }
        public string SessionName { get; set; }
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
            }
        }
        public string CharacterName
        {
            get { return Character.Name; }
        }
        public string CharacterHealth
        {
            get
            {
                return Character.Health;
            }
            set
            {
                Character.Health = value;
                EditCharacter(Character);
                OnPropertyChanged("CharacterHealth");
            }
        }
        public string Dice { get; set; }
        public string DiceNumber { get; set; }
        public DTO.Character Character { get; set; }
        public DTO.Character NewCharacter { get; set; }

        public ObservableCollection<DTO.Character> Characters
        {
            get
            {
                if (_characters == null)
                    _characters = new ObservableCollection<DTO.Character>();
                return _characters;
            }
        }
        public ObservableCollection<DTO.Log> Logs
        {
            get
            {
                if (_logs == null)
                    _logs = new ObservableCollection<DTO.Log>();
                return _logs;
            }
            set
            {
                if (value != null)
                    _logs = value;
            }
        }

        static List<Log> SortAscending(List<Log> list)
        {
            list.Sort((a, b) => a.TimeStamp.CompareTo(b.TimeStamp));
            return list;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void PostMessage(string message)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            HttpClient client = new HttpClient(clientHandler);
            DTO.Log log = new DTO.Log() { SessionID = _ID, Character = this.Character, Message = message, TimeStamp = DateTime.Now };
            JObject JLog = JObject.FromObject(log);
            HttpContent Content = new System.Net.Http.StringContent(JLog.ToString());
            Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            string connectionString = string.Format("{0}/session/{1}/add_message", _connectionString, _ID);
            HttpResponseMessage response = client.PutAsync(connectionString, Content).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string responseContent = response.Content.ReadAsStringAsync().Result;
                DTO.Log logResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Log>(responseContent);
                Logs.Add(logResponse);
                Message = string.Empty;
                textBox.Text = string.Empty;
                logs_list.SelectedIndex = logs_list.Items.Count - 1;
                logs_list.ScrollIntoView(logs_list.SelectedItem);
            }
            else
            {
                MessageBox.Show("Server error " + response.StatusCode, "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        private bool EditCharacter(DTO.Character character)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            HttpClient client = new HttpClient(clientHandler);
            JObject JCharacter = JObject.FromObject(character);
            HttpContent Content = new System.Net.Http.StringContent(JCharacter.ToString());
            Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            string connectionString = string.Format("{0}/character/edit", _connectionString);
            HttpResponseMessage response = client.PutAsync(connectionString, Content).Result;
            
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string responseContent = response.Content.ReadAsStringAsync().Result;
                DTO.Character characterResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Character>(responseContent);
                Character = characterResponse;
                Character.ImagePath = character.ImagePath;
                OnPropertyChanged("Character");
                return true;
            }
            else
            {
                MessageBox.Show("Server error " + response.StatusCode, "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void Add_message(object sender, RoutedEventArgs e)
        {
            PostMessage(this.Message);
        }
        private void Roll_dice(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(Dice))
                MessageBox.Show("Dice camps are empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            if (string.IsNullOrEmpty(DiceNumber))
                DiceNumber = "1";
            HttpClientHandler clientHandler = new HttpClientHandler();
            HttpClient client = new HttpClient(clientHandler);

            string connectionString = string.Format("{0}/dice/{1}/quantity/{2}", _connectionString, Dice, DiceNumber);
            HttpResponseMessage response = client.GetAsync(connectionString).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string responseContent = response.Content.ReadAsStringAsync().Result;

                int[] results = Newtonsoft.Json.JsonConvert.DeserializeObject<int[]>(responseContent);

                string message = String.Format("*Lancia {0} d{1} con i risultati: ", DiceNumber, Dice);
                foreach (int i in results)
                    message = message + i + " ";
                message = message + "*";
                PostMessage(message);
            }
            else
            {
                MessageBox.Show("Server error " + response.StatusCode, "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        private void Edit_character(object sender, RoutedEventArgs e)
        {
            EditCharacter editCharacter = new EditCharacter(Character);
            bool result = editCharacter.ShowDialog().Value;
            if (result)
            {
                EditCharacter(editCharacter.Character);
            }
        }
        private void Start_timer(object sender, RoutedEventArgs e)
        {
            _dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            _dispatcherTimer.Start();
        }
        private void Add_HP(object sender, RoutedEventArgs e)
        {
            int health = int.Parse(CharacterHealth);
            CharacterHealth = (++health).ToString();
        }
        private void Sub_HP(object sender, RoutedEventArgs e)
        {
            int health = int.Parse(CharacterHealth);
            CharacterHealth = (--health).ToString();
        }
        private void Edit_session_name(object sender, RoutedEventArgs e)
        {
            ChangeNameView changeNameView = new ChangeNameView(SessionName);
            if (changeNameView.ShowDialog().Value)
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                HttpClient client = new HttpClient(clientHandler);

                string connectionString = string.Format("{0}/session/{1}/change_name/{2}", _connectionString, _ID, changeNameView.NewName);
                HttpResponseMessage response = client.PutAsync(connectionString, null).Result;

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("Server error " + response.StatusCode, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    SessionName = changeNameView.NewName;
                    OnPropertyChanged("SessionName");
                }
            }
        }

        private void list_SelectionChanged(object sender, MouseButtonEventArgs e)
        {
            DTO.Character item = (sender as ListView).SelectedItem as DTO.Character;
            if (item != null)
            {
                CharacterView characterView = new CharacterView(item);
                characterView.Show();
                (sender as ListView).SelectedItems.Clear();
            }
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            HttpClient client = new HttpClient(clientHandler);

            string connectionString = string.Format("{0}/session/{1}", _connectionString, _ID);
            HttpResponseMessage response = client.GetAsync(connectionString).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string responseContent = response.Content.ReadAsStringAsync().Result;
                DTO.Session session = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Session>(responseContent);
                initialize_session(session, this.Character.Player.Name);
            }
            else
            {
                MessageBox.Show("Server error!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
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
