using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sodra.FrontEnd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public static readonly string ConnectionString = "http://localhost:24739/sodra";
        public static readonly string ConnectionString = "http://localhost:8020/sodra";

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        private string _playerName = "NyxWallace";

        private ObservableCollection<DTO.Session> _games;

        public string PlayerName
        {
            get
            {
                return _playerName;
            }
            set
            {
                _playerName = value;
                Games.Clear();
                list.Visibility = Visibility.Collapsed;
            }
        }
        public int SessionID { get; set; }

        public ObservableCollection<DTO.Session> Games
        {
            get
            {
                if (_games == null)
                    _games = new ObservableCollection<DTO.Session>();
                return _games;
            }
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(PlayerName))
            {
                MessageBox.Show("Player name can't be empty", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            CreateSession createSession = new CreateSession();
            var result = createSession.ShowDialog();
            if (result.Value)
            {
                JObject JSession = JObject.FromObject(
                    new DTO.Session()
                    {
                        Name = createSession.SessionName,
                        Characters = new DTO.Character[] { new DTO.Character() { Name = createSession.CharacterName, Player = new DTO.Player() { Name = PlayerName } } }
                    });
                HttpContent Content = new System.Net.Http.StringContent(JSession.ToString());
                Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                HttpClientHandler clientHandler = new HttpClientHandler();
                HttpClient client = new HttpClient(clientHandler);

                string connectionString = string.Format("{0}/session/create", ConnectionString);
                HttpResponseMessage response = client.PostAsync(connectionString, Content).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string responseContent = response.Content.ReadAsStringAsync().Result;
                    DTO.Session session = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Session>(responseContent);
                    SessionView sessionView = new SessionView(session, PlayerName);
                    sessionView.Show();
                }
                else
                {
                    MessageBox.Show("Server error!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }
        private void Load_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(PlayerName))
            {
                MessageBox.Show("Player name can't be empty", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            HttpClientHandler clientHandler = new HttpClientHandler();
            HttpClient client = new HttpClient(clientHandler);

            string connectionString = string.Format("{0}/player/{1}/sessions", ConnectionString, PlayerName);
            HttpResponseMessage response = client.GetAsync(connectionString).Result;
            string responseContent = response.Content.ReadAsStringAsync().Result;

            if(response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show(response.StatusCode.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            List<DTO.Session> sessions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTO.Session>>(responseContent);
            Games.Clear();
            if (sessions.Count > 0)
                list.Visibility = Visibility.Visible;
            else
                list.Visibility = Visibility.Hidden;
            foreach (DTO.Session s in sessions)
                Games.Add(s);
        }
        private void Join_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(PlayerName))
            {
                MessageBox.Show("Player name can't be empty", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            JoinSession joinSession = new JoinSession();
            if (!joinSession.ShowDialog().Value)
            {
                return;
            }
            HttpClientHandler clientHandler = new HttpClientHandler();
            HttpClient client = new HttpClient(clientHandler);
            JObject JCharacter = JObject.FromObject(new DTO.Character() { Name = joinSession.CharacterName, Player = new DTO.Player() { Name = PlayerName } });
            HttpContent Content = new System.Net.Http.StringContent(JCharacter.ToString());
            Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            string connectionString = string.Format("{0}/session/{1}/join", ConnectionString, SessionID);
            HttpResponseMessage response = client.PutAsync(connectionString, Content).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string responseContent = response.Content.ReadAsStringAsync().Result;
                DTO.Session session = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Session>(responseContent);
                SessionView sessionView = new SessionView(session, PlayerName);
                sessionView.Show();
            }
            else
            {
                MessageBox.Show("Server error!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        private void listView_Click(object sender, MouseButtonEventArgs e)
        {
            DTO.Session item = (sender as ListView).SelectedItem as DTO.Session;
            if (item != null)
            {
                if (string.IsNullOrEmpty(PlayerName))
                {
                    MessageBox.Show("Player name can't be empty", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                HttpClientHandler clientHandler = new HttpClientHandler();
                HttpClient client = new HttpClient(clientHandler);
                string connectionString = string.Format("{0}/session/{1}", ConnectionString, item.ID);
                HttpResponseMessage response = client.GetAsync(connectionString).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string responseContent = response.Content.ReadAsStringAsync().Result;
                    DTO.Session session = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.Session>(responseContent);
                    SessionView sessionView = new SessionView(session, PlayerName);
                    sessionView.Show();
                }
                else
                {
                    MessageBox.Show("Server error!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
