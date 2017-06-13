﻿using PHS.Business.Implementation;
using PHS.DB.ViewModels.Forms;
using PHS.FormBuilder.ViewModel;
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
        public static string GetSubmittedTextValue(this HtmlHelper helper, FormFieldViewModel model, string fieldType = "", string returnIfNull = "")
        {
            if (model.EntryId == null || model.EntryId == "")
            {
                return "";
            }

            using (var formManager = new FormManager())
            {

                switch (model.FieldType)
                {
                    case FieldType.ADDRESS:

                        var addressValue = formManager.FindSaveValue(model.EntryId, model.Id ?? default(int));

                        AddressViewModel address = addressValue.FromJson<AddressViewModel>();

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

                        break;

                    case FieldType.BMI:

                        var bmiValue = formManager.FindSaveValue(model.EntryId, model.Id ?? default(int));

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

                    default:
                        return formManager.FindSaveValue(model.EntryId, model.Id ?? default(int));
                }
            }


            //if (model.FieldType == FieldType.ADDRESS)
            //{
            //    if (fieldType == "StreetAddress")
            //    {
            //        return "Andy Lau";
            //    }
            //}

            return "";

        }

        public static string GetTempFormValue(this HtmlHelper helper, FormFieldViewModel model, string fieldType = "", string returnIfNull = "")
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

        public static bool IsAnyTempFormValueSelected(this HtmlHelper helper, FormFieldViewModel model)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SubmitFields[{0}].".FormatWith(model.DomId));
            sb.Append(model.FieldType.ToString());
            var key = sb.ToString().ToLower();

            return helper.ViewData.Any(vd => string.Compare(vd.Key, key, true) == 0);

        }

        public static bool IsTempFormValueSelected(this HtmlHelper helper, FormFieldViewModel model, string value)
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
        public static bool IsSubmittedValueSelected(this HtmlHelper helper, FormFieldViewModel model, string value)
        {
            if (model.EntryId == null || model.EntryId == "")
            {
                return false;
            }

            if (model.FieldType != FieldType.CHECKBOX && model.FieldType != FieldType.RADIOBUTTON)
            {
                return false;
            }

            using (var formManager = new FormManager())
            {

                var selectedValue = formManager.FindSaveValue(model.EntryId, model.Id ?? default(int));

                if (selectedValue == null)
                {
                    return false;
                }

                string[] options = selectedValue.Split(",");
                foreach (string option in options)
                {
                    if (value == option)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        public static bool IsSubmittedValueSelected(this HtmlHelper helper, FormFieldViewModel model, string value, int index)
        {
            if (model.EntryId == null || model.EntryId == "")
            {
                return false;
            }

            if (model.FieldType != FieldType.CHECKBOX && model.FieldType != FieldType.RADIOBUTTON && model.FieldType != FieldType.MATRIX)
            {
                return false;
            }

            using (var formManager = new FormManager())
            {

                var selectedValue = formManager.FindSaveValue(model.EntryId, model.Id ?? default(int));

                if (selectedValue == null)
                {
                    return false;
                }

                string[] options = selectedValue.Split(",");

                if (value == options[index])
                {
                    return true;
                }
            }

            return false;
        }










    }
}