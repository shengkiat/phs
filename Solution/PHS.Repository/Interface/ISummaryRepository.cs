using PHS.DB;
using PHS.Repository.Interface.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Repository.Interface
{
    public interface ISummaryRepository : IRepository<Summary>
    {
        Summary FindSummary(int phsEventId, int participantID, int modalityID, int templateFieldID);
    }
    
}
