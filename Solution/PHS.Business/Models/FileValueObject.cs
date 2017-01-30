using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.Models
{
    public class FileValueObject
    {
        public string FileName { get; set; }
        public string SavePath { get; set; }
        public string SaveName { get; set; }
        public string Extension { get; set; }
        public bool IsSavedInCloud { get; set; }
    }
}