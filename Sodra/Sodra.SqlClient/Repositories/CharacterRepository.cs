using Sodra.SqlClient.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sodra.DTO;

namespace Sodra.SqlClient.Repositories
{
    public class CharacterRepository
    {
        SodraContext _db = null;

        public CharacterRepository(SodraContext db)
        {
            _db = db;
        }

        public Character Get(int ID)
        {
            return _db.Characters.Where(c => c.CharacterID == ID).FirstOrDefault();
        }

        public Character Add(DTO.Character character)
        {
            Character domainCharacter = CharacterConverter.DTOToDomain(character);
            _db.Characters.Add(domainCharacter);
            _db.SaveChanges();
            return domainCharacter;
        }

        public Character Edit(DTO.Character character)
        {
            Character domainCharacter = Get(character.ID);
            CharacterConverter.MapDTOToDomain(character, domainCharacter);
            _db.Entry(domainCharacter).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            return domainCharacter;
        }

        public void Delete(DTO.Character character)
        {
            Character domainCharacter = new Character();
            CharacterConverter.MapDTOToDomain(character, domainCharacter);
            _db.Characters.Remove(domainCharacter);
            _db.SaveChanges();
        }
    }
}
