using PHS.DB;
using PHS.Repository.Context;
using PHS.Repository.Interface;
using PHS.Repository.Repository.Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PHS.Repository.Repository
{
    public class SummaryRepository : Repository<Summary>, ISummaryRepository
    {
        public SummaryRepository(DbContext context) : base(context)
        {
        }

        public Summary FindSummary(int phsEventId, int participantID, int modalityID, int templateFieldID)
        {
            return dbContext.Set<Summary>().Where(s => s.PHSEventID.Equals(phsEventId)
                    && s.ParticipantID.Equals(participantID)
                    && s.ModalityID.Equals(modalityID)
                    && s.TemplateFieldID.Equals(templateFieldID)).FirstOrDefault();
        }
    }
}