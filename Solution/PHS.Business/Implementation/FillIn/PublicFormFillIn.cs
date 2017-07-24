﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHS.Repository.Interface.Core;
using PHS.DB.ViewModels.Form;
using PHS.Business.Extensions;
using System.Web.Mvc;
using PHS.DB;

using static PHS.Common.Constants;

namespace PHS.Business.Implementation.FillIn
{
    class PublicFormFillIn : BaseFormFillIn
    {
        public PublicFormFillIn(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override void HandleAdditionalInsert(TemplateViewModel templateView, Template template, FormCollection formCollection, Guid entryId)
        {
            IDictionary<string, object> values = new Dictionary<string, object>();

            foreach (var field in templateView.Fields)
            {
                var value = field.SubmittedValue(formCollection);

                if (!string.IsNullOrEmpty(field.PreRegistrationFieldName))
                {
                    values.Add(field.PreRegistrationFieldName, value);
                }
            }

            if (Public_Form_Type_PreRegistration.Equals(template.Form.PublicFormType))
            {
                PreRegistration preRegistration = new PreRegistration();

                preRegistration.EntryId = entryId;
                preRegistration.CreatedDateTime = DateTime.Now;

                preRegistration.Citizenship = getStringValue(values, PreRegistration_Field_Name_Citizenship);
                preRegistration.HomeNumber = getStringValue(values, PreRegistration_Field_Name_HomeNumber);
                preRegistration.MobileNumber = getStringValue(values, PreRegistration_Field_Name_MobileNumber);
                preRegistration.DateOfBirth = getDateTimeValue(values, PreRegistration_Field_Name_DateOfBirth);
                preRegistration.Nric = getStringValue(values, PreRegistration_Field_Name_Nric);
                preRegistration.PreferedTime = getStringValue(values, PreRegistration_Field_Name_PreferedTime);
                preRegistration.Race = getStringValue(values, PreRegistration_Field_Name_Race);
                preRegistration.Salutation = getStringValue(values, PreRegistration_Field_Name_Salutation);
                preRegistration.Address = getStringValue(values, PreRegistration_Field_Name_Address);
                preRegistration.Language = getStringValue(values, PreRegistration_Field_Name_Language);
                preRegistration.FullName = getStringValue(values, PreRegistration_Field_Name_FullName);
                preRegistration.Gender = getStringValue(values, PreRegistration_Field_Name_Gender);

                UnitOfWork.PreRegistrations.Add(preRegistration);
            }
        }

        private string getStringValue(IDictionary<string, object> values, string key)
        {
            string result = string.Empty;
            if (values.ContainsKey(key) && values[key] != null)
            {
                result = values[key].ToString();
            }

            return result;
        }

        private Nullable<System.DateTime> getDateTimeValue(IDictionary<string, object> values, string key)
        {
            Nullable<System.DateTime> result = null;
            if (values.ContainsKey(key) && values[key] != null)
            {
                result = Convert.ToDateTime(values[key].ToString());
            }

            return result;
        }
    }
}
