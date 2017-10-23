using PHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.Interface
{
    public interface IFollowUpConfigurationManager
    {
        IEnumerable<FollowUpConfiguration> GetAllFUConfiguration();
        FollowUpConfiguration GetFUConfigurationByID(int id, out string message);
        bool NewFollowUpConfiguration(FollowUpConfiguration model, out string message);
        bool UpdateFollowUpConfiguration(FollowUpConfiguration model);
        bool DeleteFollowUpConfiguration(int id, out string message);
    }
}
