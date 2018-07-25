using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodra.SqlClient
{
    public class SodraContext : DbContext
    {
        public SodraContext() : base("SodraDB")
        {

        }

        public DbSet<Session> Sessions { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Log>()
                .HasRequired(log => log.Character)
                .WithMany()
                .HasForeignKey(log => log.CharacterID)
                .WillCascadeOnDelete(false);
        }
    }
}
