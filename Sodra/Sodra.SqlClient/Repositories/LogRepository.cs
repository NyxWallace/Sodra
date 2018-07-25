using Sodra.SqlClient.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodra.SqlClient.Repositories
{
    public class LogRepository
    {
        SodraContext _db = null;

        public LogRepository(SodraContext db)
        {
            _db = db;
        }

        public Log Get(int ID)
        {
            return _db.Logs.Where(l => l.LogID == ID).FirstOrDefault();
        }

        public Log Add(DTO.Log log)
        {
            Log domainLog = LogConverter.DTOToDomain(log);
            _db.Logs.Add(domainLog);
            _db.SaveChanges();
            return Get(domainLog.LogID);
        }
    }
}
