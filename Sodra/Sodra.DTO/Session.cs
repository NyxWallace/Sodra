using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodra.DTO
{
    public class Session
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Log[] Logs { get; set; }
        public Character[] Characters { get; set; }
    }
}
