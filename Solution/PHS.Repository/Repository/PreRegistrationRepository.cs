using PHS.DB;
using PHS.Repository.Interface;
using PHS.Repository.Repository.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace PHS.Repository.Repository
{
    public class PreRegistrationRepository : Repository<PreRegistration>, IPreRegistrationRepository
    {
        public PreRegistrationRepository(DbContext context) : base(context)
        {
        }

        public PreRegistration FindPreRegistration(Expression<Func<PreRegistration, bool>> predicate)
        {
            return dbContext.Set<PreRegistration>().Where(predicate).FirstOrDefault();
        }
    }
}
