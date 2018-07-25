using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodra.SqlClient.Repositories
{
    public class RepositoryProxy
    {
        private SodraContext _db = null;
        private static RepositoryProxy _instance;

        private PlayerRepository _playerRepository = null;
        private SessionRepository _sessionRepository = null;
        private LogRepository _logRepository = null;
        private CharacterRepository _characterRepository = null;

        private RepositoryProxy()
        {
            _db = new SodraContext();
            _playerRepository = new PlayerRepository(_db);
            _sessionRepository = new SessionRepository(_db);
            _logRepository = new LogRepository(_db);
            _characterRepository = new CharacterRepository(_db);
        }

        public PlayerRepository PlayerRepository { get { return _playerRepository; } }
        public SessionRepository SessionRepository { get { return _sessionRepository; } }
        public LogRepository LogRepository { get { return _logRepository; } }
        public CharacterRepository CharacterRepository {  get { return _characterRepository; } }

        public static RepositoryProxy Instance
        {
            get
            {
                if (_instance == null)
                {
                    if (_instance == null)
                        _instance = new RepositoryProxy();
                }
                return _instance;
            }
        }
    }
}
