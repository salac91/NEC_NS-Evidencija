using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEC_NS_Evidencija.Backend.DBLayer.Infrastructure.Common
{
    public interface IUnitOfWork
    {
        int Commit();
    }
}
