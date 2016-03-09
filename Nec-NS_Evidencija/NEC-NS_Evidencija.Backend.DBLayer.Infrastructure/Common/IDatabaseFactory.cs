using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEC_NS_Evidencija.Backend.DatabaseLayer;

namespace NEC_NS_Evidencija.Backend.DBLayer.Infrastructure.Common
{
    public interface IDatabaseFactory : IDisposable
    {
        MyEntities Get();
    }
}
