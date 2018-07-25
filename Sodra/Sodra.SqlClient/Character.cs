using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodra.SqlClient
{
    public class Character
    {
        public int CharacterID { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public int PlayerID { get; set; }
        public int SessionID { get; set; }
        public Class Class { get; set; }
        public Race Race { get; set; }
        public string ImagePath { get; set; }

        public virtual Player Player { get; set; }
        public virtual Session Session { get; set; }

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
