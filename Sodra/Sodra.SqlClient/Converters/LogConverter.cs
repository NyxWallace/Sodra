using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodra.SqlClient.Converters
{
    public class LogConverter
    {
        public static DTO.Log DomainToDTO(Log log)
        {
            DTO.Log result = new DTO.Log()
            {
                ID = log.LogID,
                Message = log.Message,
                TimeStamp = log.TimeStamp,
                Character = CharacterConverter.DomainToDTO(log.Character),
                SessionID = log.SessionID
            };
            return result;
        }

        public static Log DTOToDomain(DTO.Log log)
        {
            Log result = new Log()
            {
                LogID = log.ID,
                Message = log.Message,
                TimeStamp = log.TimeStamp,
                CharacterID = log.Character.ID,
                SessionID = log.SessionID
            };
            return result;
        }
    }
}
