using PHS.Business.ViewModel.Event;
using PHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.Interface
{
    public interface IModalityManager
    {
        IEnumerable<Modality> GetAllModalities(int EventID);
        Modality GetModalityByID(int ID, out string message);
        bool NewModality(ModalityEventViewModel modalityEventView, out string message);
        bool UpdateModality(ModalityEventViewModel modalityEventView, out string message);
    }
}
