﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodra.SqlClient
{
    public class Player
    {
        public int PlayerID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Character> Characters { get; set; }
    }
}
