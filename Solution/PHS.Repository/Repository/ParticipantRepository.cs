using PHS.DB;
using PHS.Repository.Interface;
using PHS.Repository.Repository.Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace PHS.Repository.Repository
{
    public class ParticipantRepository : Repository<Participant>, IParticipantRepository
    {
        public ParticipantRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Participant> FindParticipants(Expression<Func<Participant, bool>> predicate)
        {
            return dbContext.Set<Participant>().Where(predicate).Include(x => x.PHSEvents);
        }

        public Participant FindParticipant(string nric, int phsEventId)
        {
            return find(p => p.Nric.Equals(nric) && p.PHSEvents.Any(e => e.PHSEventID == phsEventId));
        }

        public Participant FindParticipant(string nric)
        {
            return find(p => p.Nric.Equals(nric));
        }

        public Participant FindParticipant(int participantId)
        {
            return find(p => p.ParticipantID == participantId);
        }

        public void AddParticipantWithPHSEvent(Participant participant, PHSEvent phsEvent)
        {
            Add(participant);
            participant.PHSEvents.Add(phsEvent);
        }

        public void AddPHSEventToParticipant(Participant participant, PHSEvent phsEvent)
        {
            dbContext.Entry(participant).State = EntityState.Modified;
            participant.PHSEvents.Add(phsEvent);
        }

        private Participant find(Expression<Func<Participant, bool>> predicate)
        {
            return dbContext.Set<Participant>().Where(predicate).Include(p => p.PHSEvents.Select(e => e.Modalities.Select(y => y.Forms))).Include(p => p.Summaries.Select(s => s.TemplateField)).FirstOrDefault();
        }

        public IEnumerable<Participant> findparticipants()
        {
            //event:(3),modality:registration(1),new registration form(2), template:, templatefiled:gender(80),templatevalue : male
            return dbContext.Set<Participant>().Include(p => p.ParticipantJourneyModalities
                                               .Select(pj => pj.Form.Templates
                                               .Select(tmp => tmp.TemplateFields
                                               .Select(tf => tf.TemplateFieldValues))))
                                               .Where(p => p.ParticipantJourneyModalities.Any(pj => pj.PHSEventID == 3 && pj.ModalityID == 1 && pj.FormID == 1 
                                               && pj.Form.Templates.Any(tmp => tmp.TemplateFields.Any(tf => tf.TemplateFieldID == 80 && tf.TemplateFieldValues.Any(tv => tv.Value == "Male")))));
        }
    }
}
