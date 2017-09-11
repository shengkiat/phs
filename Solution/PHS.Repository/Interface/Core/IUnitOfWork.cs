
namespace PHS.Repository.Interface.Core
{
    using System;
    using System.Data.Common;

    public interface IUnitOfWork : IDisposable
    {
        bool EnableAuditLog { get; set; }

        IUserRepository Users { get; }

        IMasterAddressRepository MasterAddress { get; }

        IFormRepository FormRepository { get; }

        IEventRepository Events { get; }

        IModalityRepository Modalities { get; }

        IParticipantRepository Participants { get; }

        IParticipantJourneyModalityRepository ParticipantJourneyModalities { get; }

        IPreRegistrationRepository PreRegistrations { get; }

        ITemplateFieldValueRepository TemplateFieldValues { get;  }

        IStandardReferenceRepository StandardReferences { get; }

        IReferenceRangeRepository ReferenceRanges { get; }

        ISummaryRepository Summaries { get; }

        int Complete();

 
    }
}
