using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEC_NS_Evidencija.Backend.DBLayer.Infrastructure.Common
{
    public abstract class RepositoryCore
    {
        public void SetFactory(IDatabaseFactory factory)
        {
            DatabaseFactory = factory;
        }

        protected IDatabaseFactory DatabaseFactory
        {
            get;
            set;
        }
    }
}
