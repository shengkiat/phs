using PHS.Repository.Repository;
using PHS.Repository.Context;
using PHS.Repository.Interface;
using PHS.Repository.Interface.Core;
using System;
using System.Threading.Tasks;
using System.Transactions;

namespace PHS.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PHSContext _context;

     

        public UnitOfWork(PHSContext context)
        {
            _context = context;
        

            Users = new UserRepository(_context);
            MasterAddress = new MasterAddressRepository(_context);
            FormRepository = new FormRepository(_context);
            Events = new EventRepository(_context);
            Modalities = new ModalityRepository(_context);
            Participants = new ParticipantRepository(_context);
            PreRegistrations = new PreRegistrationRepository(_context);
            TemplateFieldValues = new TemplateFieldValueRepository(_context);
            ParticipantJourneyModalities = new ParticipantJourneyModalityRepository(_context);
            StandardReferences = new StandardReferenceRepository(_context);
            ReferenceRanges = new ReferenceRangeRepository(_context);
            Summaries = new SummaryRepository(_context);
            SummaryMappings = new SummaryMappingRepository(_context);
            FollowUpConfigurations = new FollowUpConfigurationRepository(_context);
            FollowUpGroups = new FollowUpGroupRepository(_context);
            ParticipantCallerMappings = new ParticipantCallerMappingRepository(_context);
        }

        public IUserRepository Users { get; private set; }

        public IMasterAddressRepository MasterAddress { get; private set; }

        public IFormRepository FormRepository { get; private set; }

        public IEventRepository Events { get; private set; }

        public IModalityRepository Modalities { get; private set; }

        public IParticipantRepository Participants { get; private set; }

        public IParticipantJourneyModalityRepository ParticipantJourneyModalities { get; private set; }

        public IPreRegistrationRepository PreRegistrations { get; private set; }

        public ITemplateFieldValueRepository TemplateFieldValues { get; private set; }

        public IStandardReferenceRepository StandardReferences { get; private set; }

        public IReferenceRangeRepository ReferenceRanges { get; private set; }

        public ISummaryRepository Summaries { get; private set; }

        public ISummaryMappingRepository SummaryMappings { get; private set; }

        public IFollowUpConfigurationRepository FollowUpConfigurations { get; private set; }

        public IFollowUpGroupRepository FollowUpGroups { get; private set; }

        public IParticipantCallerMappingRepository ParticipantCallerMappings { get; private set; }

        public int Complete()
        {
            int rowsAffected =  _context.SaveChanges();
        

            return rowsAffected;
        }

        public async Task CompleteAsync()
        {
            int isSaved = await _context.SaveChangesAsync();
           
        }

        public virtual void Dispose()
        {
            _context.Dispose();
        }

        public bool EnableAuditLog
        {
            get
            {
                return _context.IsAuditEnabled;
            }

            set
            {
                _context.IsAuditEnabled = value;
            }
        }
        
    }
}
