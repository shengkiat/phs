using PHS.Business.Implementation;
using PHS.DB;
using PHS.DB.ViewModels.Form;
using PHS.FormBuilder.ViewModel;
using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static PHS.Common.Constants;

namespace PHS.Business.Extensions
{
    public static class HtmlExtensions
    {

        /// <summary>
        /// Pulls messages directly from tempdata and displays them on the page
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static IHtmlString WriteMessages(this HtmlHelper helper, string css = "")
        {
            string messages = GetMessages(helper, css);
            return new HtmlString(messages);
        }

        /// <summary>
        /// Pulls messages directly from tempdata and displays them on the page
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string GetMessages(this HtmlHelper helper, string css = "")
        {
            StringBuilder sb = new StringBuilder("");
            var errors = helper.ViewContext.TempData["error"];
            var success = helper.ViewContext.TempData["success"];
            var info = helper.ViewContext.TempData["info"];
            var notice = helper.ViewContext.TempData["notice"];

            if (errors != null && !errors.ToString().IsNullOrEmpty())
            {
                sb.AppendFormat("<div class=\"error clear {0}\">", css);
                sb.Append(errors.ToString());
                sb.Append("</div>");
            }
            if (success != null && !success.ToString().IsNullOrEmpty())
            {
                sb.AppendFormat("<div class=\"success clear {0}\">", css);
                sb.Append(success.ToString());
                sb.Append("</div>");
            }

            if (info != null && !info.ToString().IsNullOrEmpty())
            {
                sb.AppendFormat("<div class=\"info clear {0}\">", css);
                sb.Append(info.ToString());
                sb.Append("</div>");
            }

            if (notice != null && !notice.ToString().IsNullOrEmpty())
            {
                sb.AppendFormat("<div class=\"notice clear {0}\">", css);
                sb.Append(notice.ToString());
                sb.Append("</div>");
            }

            return sb.ToString();
        }

        public static IHtmlString Tip(this HtmlHelper helper, string text, string css = "tip")
        {
            return "<div class=\"{0}\">{1}</div>".FormatWith(css, text).ToHtmlString();
        }

        public static string GetContextCssClass(this HtmlHelper helper)
        {
            var controller = helper.ViewContext.Controller.ValueProvider.GetValue("controller").RawValue;
            var action = helper.ViewContext.Controller.ValueProvider.GetValue("action").RawValue;

            return "{0}-{1}".FormatWith(controller, action).ToLower();
        }

        /// <summary>
        /// returns the entire image tag for the spinner with the default
        /// dom id [id]
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static IHtmlString Spinner(this HtmlHelper helper, string id, bool hide = true)
        {
            string hideCss = hide ? "hide" : "";
            return new HtmlString("<img class='spinner {0}' id='{1}' src='/Content/images/spinner.gif' alt='loader' />".FormatWith(hideCss, id));
        }

        /// <summary>
        /// returns the entire image tag for the spinner with the default
        /// dom id "spinner"
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static IHtmlString Spinner(this HtmlHelper helper)
        {
            return Spinner(helper, "spinner");
        }

        /// <summary>
        /// Returns the path to the spacer image
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string SpacerImage(this HtmlHelper helper)
        {
            return "/Content/images/spacer.gif";
        }

        /// <summary>
        /// Find Save value(Text) using EntryId(GUID) of saved submission
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="model"></param>
        /// <param name="fieldType"></param>
        /// <param name="returnIfNull"></param>
        /// <returns></returns>
        public static string GetSubmittedTextValue(this HtmlHelper helper, TemplateFieldViewModel model, string fieldType = "", string returnIfNull = "")
        {
            string tempValue = GetTempFormValue(helper, model, fieldType, returnIfNull);

            if (!string.IsNullOrEmpty(tempValue))
            {
                return tempValue;
            }

            if (string.IsNullOrEmpty(model.EntryId) || model.EntryId.Equals(Guid.Empty.ToString()))
            {
                if (!model.IsValueRequiredForRegistration)
                {
                    return "";
                }

                else
                {
                    return GetTempRegistrationFormValue(helper, model, fieldType);
                }
            }

            else
            {
                using (var formManager = new FormManager())
                {

                    switch (model.FieldType)
                    {
                        case TemplateFieldType.ADDRESS:

                            var addressValue = formManager.FindSaveValue(model.EntryId, model.TemplateFieldID ?? default(int));

                            AddressViewModel address = addressValue.FromJson<AddressViewModel>();

                            if (address != null)
                            {
                                if (fieldType == "Blk")
                                {
                                    return address.Blk;
                                }
                                else if (fieldType == "Unit")
                                {
                                    return address.Unit;
                                }
                                else if (fieldType == "StreetAddress")
                                {
                                    return address.StreetAddress;
                                }
                                else if (fieldType == "ZipCode")
                                {
                                    return address.ZipCode;
                                }
                            }
                            break;

                        case TemplateFieldType.BMI:

                            var bmiValue = formManager.FindSaveValue(model.EntryId, model.TemplateFieldID ?? default(int));

                            BMIViewModel bmi = bmiValue.FromJson<BMIViewModel>();

                            if (fieldType == "Weight")
                            {
                                return bmi.Weight;
                            }
                            else if (fieldType == "Height")
                            {
                                return bmi.Height;
                            }

                            break;

                        case TemplateFieldType.BIRTHDAYPICKER:
                            var birthdayValue = formManager.FindSaveValue(model.EntryId, model.TemplateFieldID ?? default(int));

                            if (!string.IsNullOrEmpty(birthdayValue))
                            {
                                string[] values = birthdayValue.Split("/");

                                if (fieldType == "Day")
                                {
                                    int dayValue = int.Parse(values[0]);
                                    return (dayValue < 10) ? ("0" + dayValue) : values[0];
                                }

                                else if (fieldType == "Month")
                                {
                                    return values[1];
                                }

                                else if (fieldType == "Year")
                                {
                                    return values[2].Substring(0, 4);
                                }
                            }

                            else
                            {
                                return "";
                            }

                            break;

                        default:
                            return formManager.FindSaveValue(model.EntryId, model.TemplateFieldID ?? default(int));
                    }
                }
            }

            return "";

        }

        public static bool IsDisplayField(this HtmlHelper helper, TemplateFieldViewModel model)
        {
            bool result = true;

            if (model.ConditionTemplateFieldID.HasValue)
            {
                if (string.IsNullOrEmpty(model.ConditionCriteria) || string.IsNullOrEmpty(model.ConditionOptions))
                {
                    result = true;
                }

                else if (string.IsNullOrEmpty(model.EntryId) || model.EntryId.Equals(Guid.Empty.ToString()))
                {
                    result = false;
                }
                
                else
                {
                    using (var formManager = new FormManager())
                    {
                        TemplateFieldViewModel conditionTemplateFieldViewModel = formManager.FindTemplateField(model.ConditionTemplateFieldID.Value);

                        if (conditionTemplateFieldViewModel != null)
                        {

                            conditionTemplateFieldViewModel.EntryId = model.EntryId;

                            string conditionTemplateFieldValue = GetSubmittedTextValue(helper, conditionTemplateFieldViewModel);

                            if (model.ConditionCriteria.Equals("=="))
                            {

                                if (model.ConditionOptions.Contains(conditionTemplateFieldValue))
                                {
                                    result = true;
                                }

                                else
                                {
                                    result = false;
                                }
                            }

                            else if (model.ConditionCriteria.Equals("!="))
                            {
                                if (model.ConditionOptions.Contains(conditionTemplateFieldValue))
                                {
                                    result = false;
                                }

                                else
                                {
                                    result = true;
                                }
                            }
                        }
                        
                    }
                }
            }

            return result;
        }

        public static string GetTempFormValue(this HtmlHelper helper, TemplateFieldViewModel model, string fieldType = "", string returnIfNull = "")
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SubmitFields[{0}].".FormatWith(model.DomId));
            if (fieldType.IsNullOrEmpty())
            {
                sb.Append(model.FieldType.ToString().ToTitleCase());
            }
            else
            {
                sb.Append(fieldType);
            }
            var item = helper.ViewData[sb.ToString().ToLower()];

            if (item != null)
            {
                if (!string.IsNullOrEmpty(item.ToString()))
                {
                    return item.ToString();
                }
            }

            return returnIfNull;
        }

        public static string GetTempRegistrationFormValue(this HtmlHelper helper, TemplateFieldViewModel model, string fieldType = "", string returnIfNull = "")
        {
            if (!string.IsNullOrEmpty(model.RegistrationFieldName))
            {
                using (var participantjourneyManager = new ParticipantJourneyManager())
                {
                    var participant = participantjourneyManager.FindParticipant(model.ParticipantNric);

                    if (participant != null)
                    {
                        var value = GetValueFromParticipant(helper, model.RegistrationFieldName, participant);

                        if (value != null)
                        {
                            switch (model.FieldType)
                            {
                                case TemplateFieldType.ADDRESS:

                                    AddressViewModel addressViewModel = AddressViewModel.Initialize(participant.Address);

                                    if (fieldType == "Blk")
                                    {
                                        return addressViewModel.Blk;
                                    }
                                    else if (fieldType == "Unit")
                                    {
                                        return addressViewModel.Unit;
                                    }
                                    else if (fieldType == "StreetAddress")
                                    {
                                        return addressViewModel.StreetAddress;
                                    }
                                    else if (fieldType == "ZipCode")
                                    {
                                        return participant.PostalCode;
                                    }

                                    break;

                                case TemplateFieldType.BIRTHDAYPICKER:
                                    var birthdayValue = participant.DateOfBirth.Value.ToString();

                                    if (!string.IsNullOrEmpty(birthdayValue))
                                    {
                                        string[] values = birthdayValue.Split("/");

                                        if (fieldType == "Day")
                                        {
                                            int dayValue = int.Parse(values[0]);
                                            return (dayValue < 10) ? ("0" + dayValue) : values[0];
                                        }

                                        else if (fieldType == "Month")
                                        {
                                            return values[1];
                                        }

                                        else if (fieldType == "Year")
                                        {
                                            return values[2].Substring(0, 4);
                                        }
                                    }

                                    else
                                    {
                                        return "";
                                    }

                                    break;

                                default:
                                    return value.ToString();
                            }
                        }
                    }

                }
            }

            return returnIfNull;
        }

        private static object GetValueFromParticipant(this HtmlHelper helper, string registrationFieldName, Participant participant)
        {
            object result = null;
            switch (registrationFieldName)
            {
                case Registration_Field_Name_FullName:
                    result = participant.FullName;
                    break;
                case Registration_Field_Name_DateOfBirth:
                    result = participant.DateOfBirth;
                    break;
                case Registration_Field_Name_Gender:
                    result = participant.Gender;
                    break;
                case Registration_Field_Name_HomeNumber:
                    result = participant.HomeNumber;
                    break;
                case Registration_Field_Name_Language:
                    result = participant.Language;
                    break;
                case Registration_Field_Name_MobileNumber:
                    result = participant.MobileNumber;
                    break;
                case Registration_Field_Name_Address:
                    result = participant.Address;
                    break;
                case Registration_Field_Name_Citizenship:
                    result = participant.Citizenship;
                    break;
                case Registration_Field_Name_Nric:
                    result = participant.Nric;
                    break;
                case Registration_Field_Name_Race:
                    result = participant.Race;
                    break;
                case Registration_Field_Name_Salutation:
                    result = participant.Salutation;
                    break;
            }

            return result;
        }

        public static bool IsAnyTempFormValueSelected(this HtmlHelper helper, TemplateFieldViewModel model)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SubmitFields[{0}].".FormatWith(model.DomId));
            sb.Append(model.FieldType.ToString());
            var key = sb.ToString().ToLower();

            return helper.ViewData.Any(vd => string.Compare(vd.Key, key, true) == 0);

        }

        public static bool IsTempFormValueSelected(this HtmlHelper helper, TemplateFieldViewModel model, string value)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SubmitFields[{0}].".FormatWith(model.DomId));
            sb.Append(model.FieldType.ToString().ToTitleCase());
            var key = sb.ToString().ToLower();

            if (helper.ViewData[key] != null)
            {
                var values = helper.ViewData[key].ToString().Split(",");
                if (values.Any(v => string.Compare(v.Trim(), value.Trim(), true) == 0))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Find if the checkbox is checked from saved submission
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="model"></param>
        /// <param name="value"></param>
        /// <returns>True if checkbox is checked</returns>
        public static bool IsSubmittedValueSelected(this HtmlHelper helper, TemplateFieldViewModel model, string value)
        {
            if (model.FieldType != TemplateFieldType.CHECKBOX && model.FieldType != TemplateFieldType.RADIOBUTTON)
            {
                return false;
            }

            var selectedValue = string.Empty;

            if (string.IsNullOrEmpty(model.EntryId) || model.EntryId.Equals(Guid.Empty.ToString()))
            {
                if (!model.IsValueRequiredForRegistration)
                {
                    return false;
                }

                else
                {
                    selectedValue = GetTempRegistrationFormValue(helper, model);
                }
            }

            else
            {
                using (var formManager = new FormManager())
                {
                    selectedValue = formManager.FindSaveValue(model.EntryId, model.TemplateFieldID ?? default(int));
                }
            }

            if (string.IsNullOrEmpty(selectedValue))
            {
                return false;
            }

            string[] options = selectedValue.Split(",");
            foreach (string option in options)
            {
                if (value.Trim() == option.Trim())
                {
                    return true;
                }
            }

            return false;
        }


        public static bool IsSubmittedValueSelected(this HtmlHelper helper, TemplateFieldViewModel model, string value, int index)
        {
            if (model.FieldType != TemplateFieldType.CHECKBOX && model.FieldType != TemplateFieldType.RADIOBUTTON && model.FieldType != TemplateFieldType.MATRIX)
            {
                return false;
            }

            var selectedValue = string.Empty;

            if (string.IsNullOrEmpty(model.EntryId) || model.EntryId.Equals(Guid.Empty.ToString()))
            {
                if (!model.IsValueRequiredForRegistration)
                {
                    return false;
                }

                else
                {
                    selectedValue = GetTempRegistrationFormValue(helper, model);
                }
            }

            else
            {
                using (var formManager = new FormManager())
                {
                    selectedValue = formManager.FindSaveValue(model.EntryId, model.TemplateFieldID ?? default(int));
                }
            }

            if (string.IsNullOrEmpty(selectedValue))
            {
                return false;
            }

            string[] options = selectedValue.Split(",");

            if (value.Trim() == options[index].Trim())
            {
                return true;
            }

            return false;
        }
    }
}