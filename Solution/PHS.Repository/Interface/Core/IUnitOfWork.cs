using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Repository.Interface.Core
{
    public interface IUnitOfWork : IDisposable
    {

        IPersonRepository Persons { get; }
        int Complete();
    }
}
