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
        

            Persons = new PersonRepository(_context);
            MasterAddress = new MasterAddressRepository(_context);
            FormRepository = new FormRepository(_context);
            Events = new EventRepository(_context);
            Modalities = new ModalityRepository(_context);
            Participant = new ParticipantRepository(_context);
            TemplateFieldValues = new TemplateFieldValueRepository(_context);
        }

        public IPersonRepository Persons { get; private set; }
        public IMasterAddressRepository MasterAddress { get; private set; }

        public IFormRepository FormRepository { get; private set; }

        public IEventRepository Events { get; private set; }

        public IModalityRepository Modalities { get; private set; }

        public IParticipantRepository Participant { get; private set; }

        public ITemplateFieldValueRepository TemplateFieldValues { get; private set; }

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
