using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHS.Repository.Interface;
using PHS.Repository.Interface.Core;
using PHS.Repository.Context;
using PHS.Repository.Repository.Core;
using PHS.Repository.Repository;
using PHS.DB;
using ActiveLearning.Repository.Repository;

namespace PHS.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PHSContext _context;

        public UnitOfWork(PHSContext context)
        {
            _context = context;

            Persons = new PersonRepository(_context);
        }

        public IPersonRepository Persons { get; private set; }


        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public PHSContext ActiveLearningContext
        {
            get { return _context as PHSContext; }
        }
    }
}
