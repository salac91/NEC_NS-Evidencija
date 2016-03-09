using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEC_NS_Evidencija.Backend.DBLayer.Infrastructure.Common;
using NEC_NS_Evidencija.Backend.DatabaseLayer;

namespace NEC_NS_Evidencija.Backend.DBLayer.Infrastructure.Repositories
{
    public class TrainingRepository  : RepositoryBase<TRAINING>, ITrainingRepository
    {

        public TrainingRepository(IDatabaseFactory databaseFactory)
                : base(databaseFactory)
            {

            }
        }

        public interface ITrainingRepository : IRepository<TRAINING>
        {
        }
    
}
