using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHS.Repository.Interface;
using PHS.Repository.Context;

using System.Transactions;
using PHS.Repository.Repository.Core;
using PHS.DB;

namespace ActiveLearning.Repository.Repository
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(PHSContext context) : base(context)
        {
        }
    }
}

