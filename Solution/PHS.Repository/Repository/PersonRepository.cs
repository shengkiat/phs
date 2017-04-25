using PHS.DB;
using PHS.Repository.Context;
using PHS.Repository.Interface;
using PHS.Repository.Repository.Core;

namespace PHS.Repository.Repository
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(PHSContext context) : base(context)
        {
        }
    }
}

