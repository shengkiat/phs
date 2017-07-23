using PHS.DB;
using PHS.Repository.Interface.Core;

namespace PHS.Repository.Interface
{
    public interface IStandardReferenceRepository : IRepository<StandardReference>
    {
        StandardReference GetStandardReference(int? id);
    }
}
