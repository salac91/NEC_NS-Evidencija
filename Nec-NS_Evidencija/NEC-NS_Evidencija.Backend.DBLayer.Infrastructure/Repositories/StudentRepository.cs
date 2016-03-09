using NEC_NS_Evidencija.Backend.DBLayer.Infrastructure.Common;
using NEC_NS_Evidencija.Backend.DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEC_NS_Evidencija.Backend.DBLayer.Infrastructure.Repositories
{
    public class StudentRepository : RepositoryBase<STUDENT>, IStudentRepository
    {

        public StudentRepository(IDatabaseFactory databaseFactory)
                : base(databaseFactory)
            {

            }
        }

        public interface IStudentRepository : IRepository<STUDENT>
        {
        }
  
}
