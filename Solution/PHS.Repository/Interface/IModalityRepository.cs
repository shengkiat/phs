using PHS.DB;
using PHS.Repository.Interface.Core;
using System.Collections.Generic;

namespace PHS.Repository.Interface
{
    public interface  IModalityRepository : IRepository<Modality>
    {
        Modality GetModalityByID(int id);

        Modality GetActiveModalityByID(int id);

        IEnumerable<Modality> GetModalityByFormID(int formId);
    }
}
