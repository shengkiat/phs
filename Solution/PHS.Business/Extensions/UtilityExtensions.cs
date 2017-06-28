using PHS.Common;
using PHS.DB.ViewModels.Forms;
using PHS.FormBuilder.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PHS.Business.Extensions
{
    public static class UtilityExtensions
    {
        public static void LoadOnce(this RelatedEnd entity)
        {
            if (!entity.IsLoaded)
            {
                entity.Load();
            }
        }

        public static bool IsNullOrEmpty(this string target)
        {
            return string.IsNullOrEmpty(target);
        }

        public static void Each<T>(this IEnumerable<T> ie, Action<T, int> action)
        {
            var i = 0;

            foreach (var e in ie)
            {
                action(e, i++);
            }
        }

        public static string OutputIfFalse(this bool? target, string valueToOutput, string elseOutput = "")
        {
            if (!target.HasValue || target.Value == false)
            {
                return valueToOutput;
            }

            return elseOutput;
        }

        public static string OutputIfTrue(this bool target, string valueToOutput, string elseOutput = "")
        {
            if (target)
            {
                return valueToOutput;
            }

            return elseOutput;
        }

        public static string OutputIfFalse(this bool target, string valueToOutput, string elseOutput = "")
        {
            if (!target)
            {
                return valueToOutput;
            }

            return elseOutput;
        }


        public static IEnumerable<SelectListItem> InsertAtStart(this IList<SelectListItem> list, string itemName)
        {
            return new SelectList(new string[] { itemName }).Concat(list);
        }

        public static IEnumerable<SelectListItem> InsertAtStart(this IList<SelectListItem> list, string name, string value)
        {
            return new List<SelectListItem>(new[] { new SelectListItem { Value = value, Text = name } }).Concat(list);
        }

        public static IEnumerable<SelectListItem> InsertAtStart(this IEnumerable<SelectListItem> list, string itemName)
        {
            return new SelectList(new string[] { itemName }).Concat(list);
        }

        public static IEnumerable<SelectListItem> InsertAtStart(this IEnumerable<SelectListItem> list, string name, string value)
        {
            return new List<SelectListItem>(new[] { new SelectListItem { Value = value, Text = name } }).Concat(list);
        }

        #region FormExtensions



        public static string SubmittedFieldValue(this FormCollection form, int domId, string field)
        {
            return form[SubmittedFieldName(domId, field)];
        }

        public static string SubmittedFieldName(this TemplateFieldViewModel field)
        {
            return SubmittedFieldName(field.DomId, field.FieldType.ToString().ToTitleCase());
        }

        public static string SubmittedFieldName(int domId, string field)
        {
            return "SubmitFields[{0}].{1}".FormatWith(domId, field);
        }

        public static string FormFieldValue(this FormCollection form, int domId, string field)
        {
            string a = form["Fields[{0}].{1}".FormatWith(domId, field)];

            return form["Fields[{0}].{1}".FormatWith(domId, field)];
        }

        public static bool IsEditMode(this TemplateFieldViewModel field)
        {
            return field.Mode == Constants.TemplateFieldMode.EDIT;
        }

        public static string ValidationId(this TemplateFieldViewModel field)
        {
            return "{0}-{1}".FormatWith(field.FieldType.ToString().ToLower(), field.TemplateFieldID.ToString());
        }

        public static void AssignInputValues(this TemplateViewModel evtForm, FormCollection form)
        {
            if (evtForm.Fields.Any())
            {
                evtForm.Fields.Each((field, index) =>
                {
                    field.InputValue = SubmittedValue(field, form);
                });
            }
        }

        public static bool SubmittedValueIsValid(this TemplateFieldViewModel field, FormCollection form)
        {
            var fType = field.FieldType.ToString().ToTitleCase();
            string value = "";
            switch (field.FieldType)
            {
                case Constants.TemplateFieldType.NRICPICKER:
                    string icNumber = form.SubmittedFieldValue(field.DomId, fType.ToTitleCase());
                    // string icFirstDigit = form.SubmittedFieldValue(field.DomId, "FirstDigit");
                    // string icLastDigit = form.SubmittedFieldValue(field.DomId, "LastDigit");

                    return NricChecker.IsNRICValid(icNumber);

                case Constants.TemplateFieldType.CHECKBOX:
                    value = form.SubmittedFieldValue(field.DomId, fType.ToTitleCase());
                    if (!value.IsNullOrEmpty() && value.Contains("OthersOption"))
                    {
                        string othersOptionValue = form.SubmittedFieldValue(field.DomId, "OthersOption");
                        if (othersOptionValue.IsNullOrEmpty()) { return false; }
                    }
                    return true;
                case Constants.TemplateFieldType.RADIOBUTTON:
                    value = form.SubmittedFieldValue(field.DomId, fType.ToTitleCase());
                    if (!value.IsNullOrEmpty() && value.Equals("OthersOption"))
                    {
                        value = form.SubmittedFieldValue(field.DomId, "OthersOption");
                        if (value.IsNullOrEmpty()) { return false; }
                    }
                    return true;
                case Constants.TemplateFieldType.EMAIL:
                    value = form.SubmittedFieldValue(field.DomId, fType.ToTitleCase());
                    if (value.IsNullOrEmpty()) { return true; }
                    return value.IsValidEmail();
                case Constants.TemplateFieldType.ADDRESS:
                    return true;
                case Constants.TemplateFieldType.PHONE:
                    var area = form.SubmittedFieldValue(field.DomId, "AreaCode");
                    var number = form.SubmittedFieldValue(field.DomId, "Number");
                    if (area.IsNullOrEmpty() && number.IsNullOrEmpty())
                    {
                        return true;
                    }
                    else if ((area.IsNullOrEmpty() && !number.IsNullOrEmpty()) || (!area.IsNullOrEmpty() && number.IsNullOrEmpty()))
                    {
                        return false;
                    }
                    else
                    {
                        return area.IsNumeric() && number.IsNumeric();
                    }
                case Constants.TemplateFieldType.BIRTHDAYPICKER:
                    var day = form.SubmittedFieldValue(field.DomId, "Day");
                    var month = form.SubmittedFieldValue(field.DomId, "Month");
                    var year = form.SubmittedFieldValue(field.DomId, "Year");

                    if (day.IsNullOrEmpty() && month.IsNullOrEmpty() && year.IsNullOrEmpty())
                    {
                        return true;
                    }

                    var dateValue = "{0}-{1}-{2}".FormatWith(month, day, year);
                    var format = new string[] { "M-dd-yyyy" };
                    DateTime date;
                    return DateTime.TryParseExact(dateValue, "M-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault, out date);
                case Constants.TemplateFieldType.FILEPICKER:
                    HttpPostedFile file = HttpContext.Current.Request.Files[SubmittedFieldName(field.DomId, fType.ToTitleCase())];
                    var maxSize = field.MaxFileSize * 1024;
                    var minSize = field.MinFileSize * 1024;
                    var validExtensions = field.ValidFileExtensions;

                    if (file != null && file.ContentLength > 0)
                    {
                        var extension = System.IO.Path.GetExtension(file.FileName);
                        // check filesize is within range of Max and Min
                        if (!(file.ContentLength >= minSize && file.ContentLength <= maxSize))
                        {
                            return false;
                        }

                        // check file extension is valid
                        if (!validExtensions.IsNullOrEmpty())
                        {
                            var validExtensionArr = validExtensions.Split(",").Select(ext => ext.Trim()).ToList();
                            bool isValidExt = false;
                            foreach (var ext in validExtensionArr)
                            {
                                var updatedExt = ext;
                                if (!ext.StartsWith("."))
                                {
                                    updatedExt = "." + ext;
                                }

                                if (updatedExt.IsTheSameAs(extension))
                                {
                                    isValidExt = true;
                                }
                            }

                            return isValidExt;
                        }
                    }

                    return true;
            }

            return true;
        }

        public static string Format(this TemplateFieldValueViewModel value, bool stripHtml = false)
        {
            switch (value.FieldType)
            {
                case Constants.TemplateFieldType.BMI:
                    BMIViewModel bmi;
                    if (!string.IsNullOrEmpty(value.Value) && value.Value.Length > 0)
                    {
                        bmi = value.Value.FromJson<BMIViewModel>();
                        return bmi.Format();
                    }
                    return "--";

                case Constants.TemplateFieldType.BIRTHDAYPICKER:
                    DateTime dateVal;
                    if (!string.IsNullOrEmpty(value.Value) && DateTime.TryParse(value.Value, out dateVal))
                    {
                        return dateVal.ToString("dddd, MMMM dd, yyyy");
                    }
                    return value.Value.ToString("--");
                case Constants.TemplateFieldType.ADDRESS:
                    AddressViewModel address;
                    if (!string.IsNullOrEmpty(value.Value) && value.Value.Length > 0)
                    {
                        address = value.Value.FromJson<AddressViewModel>();
                        return address.Format();
                    }
                    return "--";
                case Constants.TemplateFieldType.CHECKBOX:
                    if (!string.IsNullOrEmpty(value.Value) && value.Value.Length > 0)
                    {
                        if (!stripHtml)
                        {
                            var values = value.Value.Split(',');
                            StringBuilder sb = new StringBuilder("<ul class=\"vertical-list selected-checkbox-list\"");
                            values.Each((val, index) =>
                            {
                                sb.AppendFormat("<li>{0}</li>", val);
                            });
                            return sb.ToString();
                        }
                        else
                        {
                            return value.Value;
                        }
                    }
                    return "";

                case Constants.TemplateFieldType.FILEPICKER:

                    return "";

                    //if (!stripHtml)
                    //{
                    //    if (!string.IsNullOrEmpty(value.Value))
                    //    {
                    //        var fileValueObject = value.Value.FromJson<FileValueObject>();

                    //        if (!string.IsNullOrEmpty(fileValueObject.FileName))
                    //        {
                    //            var imagePreviewClass = "";
                    //            var imagePreviewAttribute = "";
                    //            var downloadPath = "/Forms/file/download/{0}".FormatWith(value.Id);
                    //            if (fileValueObject.IsImage())
                    //            {
                    //                imagePreviewClass = "img-tip";
                    //                if (fileValueObject.IsSavedInCloud)
                    //                {
                    //                    imagePreviewAttribute = "data-image-path='http://{0}.s3.amazonaws.com/{1}'".FormatWith(WebConfig.Get("awsbucket"), fileValueObject.SaveName);
                    //                }
                    //                else
                    //                {
                    //                    imagePreviewAttribute = "data-image-path='{0}'".FormatWith(fileValueObject.ImageViewPath());
                    //                }
                    //            }


                    //            StringBuilder sb = new StringBuilder();
                    //            sb.Append("<ul class='horizontal-list'><li class='file-icon-item'>");
                    //            sb.AppendFormat("<a href='{0}' class='{1}' {2}>", downloadPath, imagePreviewClass, imagePreviewAttribute);
                    //            sb.AppendFormat("<img src='/content/images/spacer.gif' class='image-bg fm-file-icon fm-file-{0}-icon' alt='file icon' />", fileValueObject.Extension.Replace(".", ""));
                    //            sb.AppendFormat("</a>");
                    //            sb.Append("</li><li class='file-name-item'>");
                    //            sb.AppendFormat("<a href='{0}'>{1}</a>", downloadPath, fileValueObject.FileName.LimitWithElipses(30));
                    //            sb.Append("</li></ul>");
                    //            return sb.ToString();
                    //        }
                    //    }

                    //    return "";
                    //}
                    //else
                    //{

                    //    if (!string.IsNullOrEmpty(value.Value))
                    //    {
                    //        var fileValueObject = value.Value.FromJson<FileValueObject>();
                    //        return fileValueObject.FileName;
                    //    }
                    //    else
                    //    {
                    //        return "";
                    //    }

                    //}


            }

            return value.Value;
        }

        //public static bool IsImage(this FileValueObject obj)
        //{

        //    try
        //    {
        //        if (obj.IsSavedInCloud)
        //        {
        //            // To Retrieve from cloud
        //        }
        //        else
        //        {
        //            Image.FromFile(HttpContext.Current.Server.MapPath(obj.FullFilePath()));
        //        }
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }

        //}

        //public static string FullFilePath(this FileValueObject obj)
        //{

        //    var path = "";
        //    if (obj != null)
        //    {
        //        path = HttpContext.Current.Server.MapPath(obj.SavePath).ConcatWith("\\", obj.SaveName);

        //        return path;
        //    }

        //    return "";
        //}

        //public static string ImageViewPath(this FileValueObject obj)
        //{
        //    if (obj != null)
        //    {
        //        return "/FileUploads/".ConcatWith(obj.SaveName);
        //    }

        //    return "";
        //}

        public static bool IsEmptyOrWhiteSpace(this string target)
        {
            return string.IsNullOrEmpty(target) || string.IsNullOrWhiteSpace(target);
        }

        public static string SubmittedValue(this TemplateFieldViewModel field, FormCollection form)
        {
            var fType = field.FieldType.ToString().ToTitleCase();
            string value = "";
            switch (field.FieldType)
            {
                case Constants.TemplateFieldType.MATRIX:
                    StringBuilder builder = new StringBuilder();

                    for (int x = 0; x < 100; x++)
                    {
                        value = form.SubmittedFieldValue(field.DomId, "[" + x + "].RadioButton");

                        if (value == null)
                        {
                            break;
                        }

                        if (x == 0)
                        {
                            builder.Append(value);
                        }
                        else
                        {
                            builder.Append("," + value);
                        }
                    }

                    value = builder.ToString();

                    break;

                case Constants.TemplateFieldType.BMI:
                    var bmi = new BMIViewModel
                    {
                        Weight = form.SubmittedFieldValue(field.DomId, "Weight"),
                        Height = form.SubmittedFieldValue(field.DomId, "Height"),
                    };

                    if (bmi.Weight == "" || bmi.Height == "")
                    {
                        value = "";
                    }
                    else
                    {
                        value = bmi.ToJson();
                    }

                    break;

                case Constants.TemplateFieldType.NRICPICKER:
                    string icNumber = form.SubmittedFieldValue(field.DomId, fType.ToTitleCase());
                    //string icFirstDigit = form.SubmittedFieldValue(field.DomId, "FirstDigit");
                    //  string icLastDigit = form.SubmittedFieldValue(field.DomId, "LastDigit");

                    value = icNumber;
                    break;

                case Constants.TemplateFieldType.EMAIL:
                    value = form.SubmittedFieldValue(field.DomId, fType.ToTitleCase());
                    break;
                case Constants.TemplateFieldType.ADDRESS:
                    var address = new AddressViewModel
                    {
                        Blk = form.SubmittedFieldValue(field.DomId, "Blk"),
                        Unit = form.SubmittedFieldValue(field.DomId, "Unit"),
                        StreetAddress = form.SubmittedFieldValue(field.DomId, "StreetAddress"),
                        ZipCode = form.SubmittedFieldValue(field.DomId, "ZipCode"),
                    };

                    if (address.Blk.IsEmptyOrWhiteSpace() && address.StreetAddress.IsEmptyOrWhiteSpace())
                    {
                        value = "";
                    }
                    else
                    {
                        value = address.ToJson();
                    }
                    break;
                case Constants.TemplateFieldType.BIRTHDAYPICKER:
                    var day = form.SubmittedFieldValue(field.DomId, "Day");
                    var month = form.SubmittedFieldValue(field.DomId, "Month");
                    var year = form.SubmittedFieldValue(field.DomId, "Year");

                    if (day.IsNullOrEmpty() && month.IsNullOrEmpty() && year.IsNullOrEmpty())
                    {
                        value = "";
                    }
                    else
                    {
                        var dateValue = "{0}-{1}-{2}".FormatWith(month, day, year);
                        var format = new string[] { "M-dd-yyyy" };
                        DateTime date;
                        if (DateTime.TryParseExact(dateValue, "M-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault, out date))
                        {
                            value = date.ToString();
                        }
                        else
                        {
                            value = "";
                        }

                    }
                    break;
                case Constants.TemplateFieldType.CHECKBOX:
                    value = form.SubmittedFieldValue(field.DomId, fType.ToTitleCase());
                    if (!value.IsNullOrEmpty() && value.Contains("OthersOption"))
                    {
                        string othersOptionValue = form.SubmittedFieldValue(field.DomId, "OthersOption");
                        value = value.Replace("OthersOption", othersOptionValue);
                    }
                    break;
                case Constants.TemplateFieldType.PHONE:
                    var area = form.SubmittedFieldValue(field.DomId, "AreaCode");
                    var number = form.SubmittedFieldValue(field.DomId, "Number");
                    if (string.IsNullOrEmpty(area) && string.IsNullOrEmpty(number))
                    {
                        value = "";
                    }
                    else
                    {
                        value = "{0}-{1}".FormatWith(area, number);
                    }
                    break;
                case Constants.TemplateFieldType.DROPDOWNLIST:
                    value = form.SubmittedFieldValue(field.DomId, fType.ToTitleCase());
                    break;
                case Constants.TemplateFieldType.RADIOBUTTON:
                    value = form.SubmittedFieldValue(field.DomId, fType.ToTitleCase());
                    if (!value.IsNullOrEmpty() && value.Equals("OthersOption"))
                    {
                        value = form.SubmittedFieldValue(field.DomId, "OthersOption");
                    }
                    break;
                case Constants.TemplateFieldType.FULLNAME:
                    var fName = form.SubmittedFieldValue(field.DomId, "FirstName");
                    var lName = form.SubmittedFieldValue(field.DomId, "LastName");
                    var initials = form.SubmittedFieldValue(field.DomId, "Initials");
                    if (string.IsNullOrEmpty(fName) && string.IsNullOrEmpty(lName))
                    {
                        value = "";
                    }
                    else
                    {
                        value = "{0} {1} {2}".FormatWith(fName, initials, lName);
                    }
                    break;
                case Constants.TemplateFieldType.TEXTAREA:
                    value = form.SubmittedFieldValue(field.DomId, fType.ToTitleCase());
                    break;
                case Constants.TemplateFieldType.TEXTBOX:
                    value = form.SubmittedFieldValue(field.DomId, fType.ToTitleCase());
                    break;
                case Constants.TemplateFieldType.SIGNATURE:
                    value = form.SubmittedFieldValue(field.DomId, fType.ToTitleCase());
                    break;
                case Constants.TemplateFieldType.FILEPICKER:
                    //HttpPostedFile file = HttpContext.Current.Request.Files[SubmittedFieldName(field.DomId, fType.ToTitleCase())];
                    //value = "";
                    //if (file != null && file.ContentLength > 0)
                    //{
                    //    var extension = Path.GetExtension(file.FileName);

                    //    var valueObject = new FileValueObject()
                    //    {
                    //        FileName = file.FileName,
                    //        SaveName = Guid.NewGuid().ToString() + extension,
                    //        SavePath = WebConfig.Get("filesavepath"),
                    //        IsSavedInCloud = UtilityHelper.UseCloudStorage(),
                    //        Extension = extension
                    //    };

                    //    value = valueObject.ToJson();
                    //}
                    break;

            }

            return value;
        }

        //public static FileValueObject GetFileValueFromJsonObject(this string value)
        //{
        //    try
        //    {
        //        return value.FromJson<FileValueObject>();
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        public static void SetFieldErrors(this TemplateFieldViewModel field)
        {
            field.Errors = "Invalid entry submitted for {0}".FormatWith(field.Label);
            if (field.FieldType == Constants.TemplateFieldType.FILEPICKER)
            {
                field.Errors = field.Errors.ConcatWith(", file must be betweeen {0}kb and {1}kb large ".FormatWith(field.MinFileSize.ToString(), field.MaxFileSize.ToString()));
                if (!string.IsNullOrEmpty(field.ValidFileExtensions))
                {
                    field.Errors = field.Errors.ConcatWith(" and have the extensions {0}.".FormatWith(field.ValidFileExtensions));
                }
                else
                {
                    field.Errors = field.Errors.ConcatWith(".");
                }
            }
            else if (field.FieldType == Constants.TemplateFieldType.NRICPICKER)
            {
                field.Errors = "Invalid NRIC";
            }

        }

        // for generic interface IEnumerable<T>
        public static string ToString<T>(this IEnumerable<T> source, string separator)
        {
            if (source == null)
                throw new ArgumentException("Parameter source can not be null.");

            if (string.IsNullOrEmpty(separator))
                throw new ArgumentException("Parameter separator can not be null or empty.");

            string[] array = source.Where(n => n != null).Select(n => n.ToString()).ToArray();

            return string.Join(separator, array);
        }

        public static string Format(this AddressViewModel addr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0},", addr.Blk);

            if (!addr.Unit.IsNullOrEmpty())
            {
                sb.AppendFormat(" {0},", addr.Unit);
            }

            sb.AppendFormat(" {0}, ", addr.StreetAddress);
            sb.AppendFormat(" {0} ", addr.ZipCode);

            return sb.ToString();
        }

        public static string Format(this BMIViewModel bmi)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Weight(kg): {0},", bmi.Weight);

            sb.AppendFormat(" Height(cm): {0}, ", bmi.Height);
            sb.AppendFormat(" BMI: {0} ", bmi.BodyMassIndex);

            return sb.ToString();
        }

        #endregion

    }
}