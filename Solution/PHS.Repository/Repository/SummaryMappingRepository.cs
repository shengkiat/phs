using PHS.DB;
using PHS.Repository.Context;
using PHS.Repository.Interface;
using PHS.Repository.Repository.Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PHS.Repository.Repository
{
    public class SummaryMappingRepository : Repository<SummaryMapping>, ISummaryMappingRepository
    {
        public SummaryMappingRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<SummaryMapping> GetSummaryMappingBySummaryType(string summaryType)
        {
            return dbContext.Set<SummaryMapping>().Where(s => s.SummaryType == summaryType).OrderBy(x => x.SummaryMappingID);
        }

        public IEnumerable<SummaryMapping> GetSummaryMappingByCategoryNameAndSummaryType(string categoryName, string summaryType)
        {
            return dbContext.Set<SummaryMapping>().Where(s => s.SummaryType == summaryType && s.CategoryName == categoryName).OrderBy(x => x.SummaryMappingID);
        }
    }
}