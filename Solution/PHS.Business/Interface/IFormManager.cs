﻿using PHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.Interface
{
    public interface IFormManager
    {
        List<form> FindAllForms();
        form FindForm(int formID);

    }
}