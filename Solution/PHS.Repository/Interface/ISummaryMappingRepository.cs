using PHS.DB;
using PHS.Repository.Interface.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Repository.Interface
{
    public interface ISummaryMappingRepository : IRepository<SummaryMapping>
    {
        IEnumerable<SummaryMapping> GetSummaryMappingBySummaryType(string summaryType);
        IEnumerable<SummaryMapping> GetSummaryMappingByCategoryNameAndSummaryType(string categoryName, string summaryType);
    }

}
