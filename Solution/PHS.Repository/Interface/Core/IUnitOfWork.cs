
namespace PHS.Repository.Interface.Core
{
    using System;
    using System.Data.Common;

    public interface IUnitOfWork : IDisposable
    {
        bool EnableAuditLog { get; set; }

        IPersonRepository Persons { get; }



        int Complete();

 
    }
}
