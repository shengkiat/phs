using PHS.DB;
using System.Linq;
using PHS.Repository.Interface;
using PHS.Repository.Repository.Core;
using System.Data.Entity;
using System.Collections.Generic;
using System;

namespace PHS.Repository.Repository
{
    public class FollowUpConfigurationRepository : Repository<FollowUpConfiguration>, IFollowUpConfigurationRepository
    {
        public FollowUpConfigurationRepository(DbContext datacontext) : base(datacontext)
        {
        }

        public FollowUpConfiguration GetFollowUpConfiguration(int id)
        {
            return dbContext.Set<FollowUpConfiguration>().Where(u => u.FollowUpConfigurationID == id).Include(b =>b.FollowUpGroups).FirstOrDefault();
        }

        public IEnumerable<FollowUpConfiguration> GetAllFollowUpConfigurations()
        {
            return dbContext.Set<FollowUpConfiguration>().ToList();
        }
        public IEnumerable<FollowUpConfiguration> GetAllFollowUpConfigurationsByEventID(int eventid)
        {
            var result =  dbContext.Set<FollowUpConfiguration>().Include(fu => fu.PHSEvent).Where(fu => fu.PHSEventID == eventid);
            return result;
        }
    }
}
