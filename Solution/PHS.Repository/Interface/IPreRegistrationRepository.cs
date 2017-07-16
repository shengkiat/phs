using PHS.DB;
using PHS.Repository.Interface.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Repository.Interface
{
    public interface IPreRegistrationRepository : IRepository<PreRegistration>
    {
        PreRegistration FindPreRegistration(Expression<Func<PreRegistration, bool>> predicate);
    }
}
