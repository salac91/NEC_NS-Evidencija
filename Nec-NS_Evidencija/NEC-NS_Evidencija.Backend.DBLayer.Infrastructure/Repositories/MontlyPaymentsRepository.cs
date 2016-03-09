using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEC_NS_Evidencija.Backend.DBLayer.Infrastructure.Common;
using NEC_NS_Evidencija.Backend.DatabaseLayer;

namespace NEC_NS_Evidencija.Backend.DBLayer.Infrastructure.Repositories
{
    public class MontlyPaymentsRepository  : RepositoryBase<MONTLYPAYMENT>, IMontlyPaymentsRepository
    {

        public MontlyPaymentsRepository(IDatabaseFactory databaseFactory)
                : base(databaseFactory)
            {

            }
        }

        public interface IMontlyPaymentsRepository : IRepository<MONTLYPAYMENT>
        {
        }
    
}
