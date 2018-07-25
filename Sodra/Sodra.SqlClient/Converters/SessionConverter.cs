using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodra.SqlClient.Converters
{
    public class SessionConverter
    {
        public static DTO.Session DomainToDTO(Session session)
        {
            List<DTO.Log> logs = new List<DTO.Log>();
            if(session.Logs != null)
                foreach (var l in session.Logs)
                    logs.Add(LogConverter.DomainToDTO(l));
            List<DTO.Character> characters = new List<DTO.Character>();
            if (session.Characters != null)
                foreach (var c in session.Characters)
                    characters.Add(CharacterConverter.DomainToDTO(c));
            DTO.Session result = new DTO.Session()
            {
                ID = session.SessionID,
                Name = session.Name,
                Logs = logs.ToArray(),
                Characters = characters.ToArray()
            };
            return result;
        }
    }
}
