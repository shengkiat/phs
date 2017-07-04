using PHS.Business.Implementation;
using PHS.Repository;
using PHS.Repository.Context;
using PHS.Repository.Interface.Core;

namespace PHS.BusinessTests.Implementation
{
    public class MockFormManager : FormManager
    {
        private IUnitOfWork _unitOfWork;

        public MockFormManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected override IUnitOfWork CreateUnitOfWork()
        {
            return _unitOfWork;
        }
    }

}
