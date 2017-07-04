using PHS.Business.Implementation;
using PHS.Repository;
using PHS.Repository.Context;
using PHS.Repository.Interface.Core;

namespace PHS.BusinessTests.Implementation
{
    public class MockFormManager : FormManager
    {
        public override IUnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork(new PHSContext());
        }
    }

}
