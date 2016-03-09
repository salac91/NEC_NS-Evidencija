using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEC_NS_Evidencija.Backend.DBLayer.Infrastructure.Common
{
    public class TestUtils
    {
        public static void TruncateDatabase()
        {
            var dbFactory = new DatabaseFactory();
            dbFactory.Get().Database.ExecuteSqlCommand("EXEC sp_msforeachtable \"ALTER TABLE ? NOCHECK CONSTRAINT all\"");
            dbFactory.Get().Database.ExecuteSqlCommand("EXEC sp_MSForEachTable \"DELETE FROM ?\"");
            dbFactory.Get().Database.ExecuteSqlCommand("EXEC sp_msforeachtable \"ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all\"");
        }
    }
}
