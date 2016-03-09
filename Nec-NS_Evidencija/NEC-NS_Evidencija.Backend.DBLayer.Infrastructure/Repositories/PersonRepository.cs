using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEC_NS_Evidencija.Backend.DBLayer.Infrastructure.Common;
using NEC_NS_Evidencija.Backend.DatabaseLayer;

namespace NEC_NS_Evidencija.Backend.DBLayer.Infrastructure.Repositories
{
    public class PersonRepository : RepositoryBase<PERSON>, IPersonRepository
    {

        public PersonRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }

    public interface IPersonRepository : IRepository<PERSON>
    {
    }
   
}
