using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodra.SqlClient.Repositories
{
    public class SessionRepository
    {
        SodraContext _db = null;

        public SessionRepository(SodraContext db)
        {
            _db = db;
        }

        public Session Get(int ID)
        {
            return _db.Sessions.Where(s => s.SessionID == ID).FirstOrDefault();
        }

        public List<Session> GetPlayerSessions(string playerName)
        {
            List<Session> sessions = new List<Session>();
            List<Character> characters = _db.Characters.Where(c => c.Player.Name == playerName).ToList();
            foreach (var c in characters)
                sessions.Add(c.Session);
            return sessions;
        }

        public Session Add(DTO.Session session)
        {
            Player domainPlayer = RepositoryProxy.Instance.PlayerRepository.Get(session.Characters.First().Player);

            Session domainSession = new Session()
            {
                Name = session.Name,
            };
            _db.Set<Session>().Add(domainSession);
            _db.SaveChanges();

            Character domainCharacter = new Character()
            {
                Name = session.Characters.First().Name,
                Health = 20,
                Player = domainPlayer,
                Session = domainSession
            };
            _db.Set<Character>().Add(domainCharacter);
            _db.SaveChanges();

            return Get(domainSession.SessionID);
        }

        public Session AddPlayer(int sessionID, DTO.Character character)
        {
            Session session = Get(sessionID);
            if (session.Characters.Where(c => c.Player.Name == character.Player.Name).FirstOrDefault() == null)
            {
                Player player = RepositoryProxy.Instance.PlayerRepository.Get(new DTO.Player() { Name = character.Player.Name });
                RepositoryProxy.Instance.CharacterRepository.Add(new DTO.Character()
                {
                    Name = character.Name,
                    Health = "20",
                    Class = DTO.Class.Warrior,
                    Race = DTO.Race.Human,
                    Player = new DTO.Player() { ID = player.PlayerID },
                    SessionID = sessionID
                });
            }
            return session;
        }

        public void Rename(int ID, string name)
        {
            Session session = Get(ID);
            session.Name = name;
            _db.Entry(session).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
