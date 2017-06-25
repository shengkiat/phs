using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.DB.ViewModels
{
    public class eventIndexdata
    {
        public IEnumerable<PHSEvent> events { get; set; }
        public IEnumerable<Modality> modalities { get; set; }
        public IEnumerable<Template> Forms { get; set; }
    }
}
