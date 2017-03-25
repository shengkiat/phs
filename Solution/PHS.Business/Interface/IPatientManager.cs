﻿using PHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.Interface
{
    public interface IPatientManager : IDisposable
    {
        IList<Patient> GetPatientsByNric(string icFirstDigit, string icNumber, string icLastDigit, out string message);
    }
}
