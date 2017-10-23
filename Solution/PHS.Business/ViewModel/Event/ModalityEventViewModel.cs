using PHS.DB;
using PHS.DB.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PHS.Business.ViewModel.Event
{
    public class ModalityEventViewModel
    {
        public string Name { get; set; }
        public int Position { get; set; }
        public string IconPath { get; set; }
        public bool IsActive { get; set; }
        public bool IsVisible { get; set; }
        public bool IsMandatory { get; set; }
        public bool HasParent { get; set; }
        public string Status { get; set; }
        public List<ModalityRole> ModalityRole { get; set; }
        public int EventID { get; set; }
        public int ModalityID { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }

        public List<ModalityForm> modalityFormList { get; set; }
    }
}
