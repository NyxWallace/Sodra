namespace Sodra.DTO
{
    public class Character
    {
        private string _imagePath;

        public int ID { get; set; }
        public string Name { get; set; }
        public string Health { get; set; }
        public int SessionID { get; set; }
        public Player Player { get; set; }
        public Class Class { get; set; }
        public Race Race { get; set; }
        public string ImagePath
        {
            get
            {
                if(string.IsNullOrEmpty(_imagePath))
                    _imagePath =  "Resources\\Senua.jpg";
                return _imagePath;
            }
            set
            {
                _imagePath = value;
            }
        }
    }

    public enum Race
    {
        Human,
        Elf,
        Orc
    }

    public enum Class
    {
        Warrior,
        Cleric,
        Bard,
        Druid,
        Warlock,
        Mage,
        Barbarian
    }
}