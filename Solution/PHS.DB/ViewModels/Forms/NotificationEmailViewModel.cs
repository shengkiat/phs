using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PHS.DB.ViewModels.Forms
{
    public class NotificationEmailViewModel
    {
        public string FormName { get; set; }
        public IDictionary<string, TemplateFieldValueViewModel> Entries { get; set; }
        public string Email { get; set; }

        public NotificationEmailViewModel()
        {
            this.Entries = new Dictionary<string, TemplateFieldValueViewModel>();
        }
    }
}