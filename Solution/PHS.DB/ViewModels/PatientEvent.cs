using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.DB
{
    public class PatientEvent
    {
        public @event Event { get; set; }
        public string Nric { get; set; }
        public string FullName { get; set; }
        public string EventId { get { return Event.ID.ToString(); } }
    }
}
