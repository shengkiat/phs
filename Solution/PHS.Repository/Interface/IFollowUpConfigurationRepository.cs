using PHS.DB;
using PHS.Repository.Interface.Core;
using System.Collections.Generic;

namespace PHS.Repository.Interface
{
    public interface IFollowUpConfigurationRepository : IRepository<FollowUpConfiguration>
    {
        FollowUpConfiguration GetFollowUpConfiguration(int id);
        IEnumerable<FollowUpConfiguration> GetAllFollowUpConfigurations();
        IEnumerable<FollowUpConfiguration> GetAllFollowUpConfigurationsByEventID(int eventid);
        FollowUpConfiguration GetDeployedFollowUpConfiguration(int eventid);

    }

}