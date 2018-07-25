using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodra.SqlClient.Converters
{
    public class PlayerConverter
    {
        public static DTO.Player DomainToDto(Player player)
        {
            DTO.Player result = new DTO.Player()
            {
                ID = player.PlayerID,
                Name = player.Name
            };
            return result;
        }
    }
}
