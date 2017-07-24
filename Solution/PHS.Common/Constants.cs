using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Common
{
    public class Constants
    {
        public const string User_Role_Doctor_Code = "D";
        public const string User_Role_Volunteer_Code = "V";
        public const string User_Role_Admin_Code = "A";

        public const string Admin = "Admin";

        public static string FILESAVEPATHKEY = "filesavepath";
        public static string FILESAVENAMEKEY = "filesavename";
        public static string DEAULTFILEEXTENSIONS = ".jpg,.png,.gif,.pdf,.bmp,.zip";
        public static int DEAULTMAXFILESIZEINKB = 5000;
        public static int DEAULTMINFILESIZEINKB = 10;

        public const string Public_Form_Type_PreRegistration = "PRE-REGISTRATION";
        public const string Public_Form_Type_OutReach = "OUTREACH";

        public const string PreRegistration_Field_Name_Nric = "NRIC";
        public const string PreRegistration_Field_Name_FullName = "FULLNAME";
        public const string PreRegistration_Field_Name_Salutation = "SALUTATION";
        public const string PreRegistration_Field_Name_ContactNumber = "CONTACTNUMBER";
        public const string PreRegistration_Field_Name_DateOfBirth = "DATEOFBIRTH";
        public const string PreRegistration_Field_Name_Citizenship = "CITIZENSHIP";
        public const string PreRegistration_Field_Name_Race = "RACE";
        public const string PreRegistration_Field_Name_Language = "LANGUAGE";
        public const string PreRegistration_Field_Name_PreferedTime = "PREFEREDTIME";
        public const string PreRegistration_Field_Name_Address = "ADDRESS";
        public const string PreRegistration_Field_Name_Gender = "GENDER";

        public const string Registration_Field_Name_FullName = "FULLNAME";
        public const string Registration_Field_Name_ContactNumber = "CONTACTNUMBER";
        public const string Registration_Field_Name_DateOfBirth = "DATEOFBIRTH";
        public const string Registration_Field_Name_Language = "LANGUAGE";
        public const string Registration_Field_Name_Gender = "GENDER";

        public enum MessageType
        {
            PROMPT,
            ERROR
        }

        public enum TemplateStatus
        {
            DRAFT,
            PUBLISHED
        }

        public enum TemplateMode
        {
            EDIT,
            INPUT,
            READONLY
        }

        public enum TemplateFieldMode
        {
            EDIT,
            INPUT,
            READONLY
        }

        public enum TemplateFieldType
        {
            TEXTBOX,
            TEXTAREA,
            RADIOBUTTON,
            PASSWORD,
            CHECKBOX,
            H1,
            HEADER,
            DROPDOWNLIST,
            FULLNAME,
            EMAIL,
            ADDRESS,
            PHONE,
            BIRTHDAYPICKER,
            FILEPICKER,
            BMI,
            IMAGE,
            MATRIX,
            NRICPICKER,
            DOCTORMEMO,
            SIGNATURE,
            SECTIONCOLLAPSE
        }

        public static string OperationFailedDuringRetrievingValue(string value)
        {
            return "Operation failed during retrieving " + value + ". Please contact system admin";
        }

        public static string PleaseFillInAllRequiredFields()
        {
            return "Please fill in all required fields";
        }

        public static string ValueIsEmpty(string value)
        {
            return value + " is empty";
        }

        public static string ValueAlreadyExists(string value)
        {
            return value + " already exists";
        }

        public static string OperationFailedDuringAddingValue(string value)
        {
            return "Operation failed during adding " + value + ". Please contact system admin";
        }

        public static string OperationFailedDuringUpdatingValue(string value)
        {
            return "Operation failed during updating " + value + ". Please contact system admin";
        }

        public static string ValueSuccessfuly(string value)
        {
            return value + " successfully";
        }

        public static string ThereIsNoValueFound(string value)
        {
            return "There is no " + value + " found.";
        }
    }
}
