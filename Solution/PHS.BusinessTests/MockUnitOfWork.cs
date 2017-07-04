using PHS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHS.Repository.Context;

namespace PHS.BusinessTests
{
    public class MockUnitOfWork : UnitOfWork
    {
        public MockUnitOfWork(PHSContext context) : base(context)
        {
        }

        public override void Dispose()
        {
            //_context.Dispose();
        }
    }
}
