using PHS.Business.Interface;
using PHS.DB;
using PHS.Repository;
using PHS.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.Implementation
{
    public class AddressManager : BaseManager, IAddressManager, IManagerFactoryBase<IAddressManager>
    {
        public AddressManager() : base(null)
        {
        }

        public AddressManager(PHSUser loginUser) : base(loginUser)
        {
        }

        public IAddressManager Create(PHSUser loginUser)
        {
            return new AddressManager(loginUser);
        }

        public MasterAddress FindAddress(string postalCode)
        {
            using (var unitOfWork = new UnitOfWork(new PHSContext()))
            {
                var address = unitOfWork.MasterAddress.Find(u => u.PostalCode.Equals(postalCode)).FirstOrDefault();

                if (address != null)
                {
                    return address;
                }
            }

            return null;
        }
    }
}
