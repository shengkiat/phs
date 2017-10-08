using PHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.Interface
{
    public interface IManagerFactoryBase<TEntity> where TEntity : class
    {
        TEntity Create(PHSUser loginUser);
    }
}
