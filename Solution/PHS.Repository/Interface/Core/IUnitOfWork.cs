
namespace PHS.Repository.Interface.Core
{
    using System;
    using System.Data.Common;

    public interface IUnitOfWork : IDisposable
    {
        bool EnableAuditLog { get; set; }

        IPersonRepository Persons { get; }
        IMasterAddressRepository MasterAddress { get; }

        IFormRepository FormRepository { get; }

        IEventRepository Events { get; }

        IModalityRepository Modalities { get; }

        IEventPatientRepository EventPatient { get; }

        ITemplateFieldValueRepository TemplateFieldValues { get;  }



        int Complete();

 
    }
}
