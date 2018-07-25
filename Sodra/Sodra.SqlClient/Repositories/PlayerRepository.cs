using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodra.SqlClient.Repositories
{
    public class PlayerRepository
    {
        private SodraContext _db = null;

        public PlayerRepository(SodraContext db)
        {
            _db = db;
        }

        public Player Get(DTO.Player player)
        {
            Player domainPlayer = _db.Players.Where(p => p.Name == player.Name).FirstOrDefault();

            if (domainPlayer != null)
                return domainPlayer;

            domainPlayer = Add(player);

            return domainPlayer;
        }

        public Player Add(DTO.Player player)
        {
            Player domainPlayer = new Player()
            {
                Name = player.Name
            };
            _db.Players.Add(domainPlayer);
            _db.SaveChanges();
            return domainPlayer;
        }
    }
}
