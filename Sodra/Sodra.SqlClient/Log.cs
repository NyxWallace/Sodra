using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodra.SqlClient
{
    public class Log
    {
        public int LogID { get; set; }
        public int SessionID { get; set; }
        public int CharacterID { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
        public virtual Character Character { get; set; }
    }
}
