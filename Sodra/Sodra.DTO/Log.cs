using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodra.DTO
{
    public class Log
    {
        public int ID { get; set; }
        public int SessionID { get; set; }
        public Character Character { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Time { get { return String.Format("{0:HH:mm:ss}\n{1:d/m/yy}", TimeStamp, TimeStamp); } }
    }
}
