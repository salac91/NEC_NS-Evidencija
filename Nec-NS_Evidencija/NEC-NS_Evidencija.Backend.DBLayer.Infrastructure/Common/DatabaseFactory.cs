using NEC_NS_Evidencija.Backend.DatabaseLayer;
using log4net;

namespace NEC_NS_Evidencija.Backend.DBLayer.Infrastructure.Common
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private MyEntities dataContext;

        private static readonly ILog log = LogManager.GetLogger(typeof(DatabaseFactory));

        public MyEntities Get()
        {
            log.Debug("Getting DbContext");

            if (dataContext == null)
            {
                log.Debug("Creating new DbContext");

                dataContext = new MyEntities();
            }
            return dataContext;
        }
        protected override void DisposeCore()
        {
            log.Debug("Disposing DbContext");

            if (dataContext != null)
                dataContext.Dispose();
        }
    }
   
}
