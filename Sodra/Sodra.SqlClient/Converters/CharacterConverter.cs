using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodra.SqlClient.Converters
{
    public class CharacterConverter
    {
        public static DTO.Character DomainToDTO(Character character)
        {
            DTO.Character result = new DTO.Character()
            {
                ID = character.CharacterID,
                Name = character.Name,
                Health = character.Health.ToString(),
                Class = (DTO.Class)character.Class,
                Race = (DTO.Race)character.Race,
                ImagePath = character.ImagePath,
                Player = PlayerConverter.DomainToDto(character.Player)
            };
            return result;
        }

        public static Character DTOToDomain(DTO.Character character)
        {
            Character result = new Character()
            {
                CharacterID = character.ID,
                Name = character.Name,
                Health = int.Parse(character.Health),
                Class = (Class)character.Class,
                Race = (Race)character.Race,
                ImagePath = character.ImagePath,
                PlayerID = character.Player.ID,
                SessionID = character.SessionID
            };
            return result;
        }

        public static void MapDTOToDomain(DTO.Character character, Character result)
        {
            result.CharacterID = character.ID;
            result.Name = character.Name;
            result.Health = int.Parse(character.Health);
            result.Class = (Class)character.Class;
            result.Race = (Race)character.Race;
            result.ImagePath = character.ImagePath;
            result.PlayerID = character.Player.ID;
        }
    }
}
