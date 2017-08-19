using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Common
{
    public class Constants
    {
        public const string User_Role_Doctor_Code = "Doctor";
        public const string User_Role_Volunteer_Code = "Volunteer";
        public const string User_Role_CommitteeMember_Code = "Committee Member";

        public const string Admin = "Admin";

        public static string FILESAVEPATHKEY = "filesavepath";
        public static string FILESAVENAMEKEY = "filesavename";
        public static string DEAULTFILEEXTENSIONS = ".jpg,.png,.gif,.pdf,.bmp,.zip";
        public static int DEAULTMAXFILESIZEINKB = 5000;
        public static int DEAULTMINFILESIZEINKB = 10;

        public const string Internal_Form_Type_Registration = "REG";
        public const string Internal_Form_Type_MegaSortingStation = "MEG";
        public const string Internal_Form_Type_EventSummary = "ESY";
        public const string Internal_Form_Type_DoctorySummary = "DSY";
        public const string Internal_Form_Type_AllSummary = "SUM";

        public const string Public_Form_Type_PreRegistration = "PRE-REGISTRATION";
        public const string Public_Form_Type_OutReach = "OUTREACH";

        public const string PreRegistration_Field_Name_Nric = "NRIC";
        public const string PreRegistration_Field_Name_FullName = "FULLNAME";
        public const string PreRegistration_Field_Name_Salutation = "SALUTATION";
        public const string PreRegistration_Field_Name_HomeNumber = "HOMENUMBER";
        public const string PreRegistration_Field_Name_MobileNumber = "MOBILENUMBER";
        public const string PreRegistration_Field_Name_DateOfBirth = "DATEOFBIRTH";
        public const string PreRegistration_Field_Name_Citizenship = "CITIZENSHIP";
        public const string PreRegistration_Field_Name_Race = "RACE";
        public const string PreRegistration_Field_Name_Language = "LANGUAGE";
        public const string PreRegistration_Field_Name_PreferedTime = "PREFEREDTIME";
        public const string PreRegistration_Field_Name_Address = "ADDRESS";
        public const string PreRegistration_Field_Name_Gender = "GENDER";

        public const string Registration_Field_Name_Nric = "NRIC";
        public const string Registration_Field_Name_FullName = "FULLNAME";
        public const string Registration_Field_Name_Salutation = "SALUTATION";
        public const string Registration_Field_Name_HomeNumber = "HOMENUMBER";
        public const string Registration_Field_Name_MobileNumber = "MOBILENUMBER";
        public const string Registration_Field_Name_DateOfBirth = "DATEOFBIRTH";
        public const string Registration_Field_Name_Citizenship = "CITIZENSHIP";
        public const string Registration_Field_Name_Race = "RACE";
        public const string Registration_Field_Name_Language = "LANGUAGE";
        public const string Registration_Field_Name_Address = "ADDRESS";
        public const string Registration_Field_Name_Gender = "GENDER";

        public const string Summary_Field_Name_CurrentlySmoke = "CURRENTLY_SMOKE";
        public const string Summary_Field_Name_SmokeBefore = "SMOKE_BEFORE";
        public const string Summary_Field_Name_PastMedicalHistory = "PAST_MEDICAL_HISTORY";
        public const string Summary_Field_Name_PMHX = "PMHX";
        public const string Summary_Field_Name_FamilyHistory = "FAMILY_HISTORY";
        public const string Summary_Field_Name_FamilyHistory_MedCond = "FAM_HISTORY_MEDCOND";
        public const string Summary_Field_Name_FamilyHistory_Cancer = "FAM_HISTORY_CANCER";

        public const string Summary_Field_Name_PersonalCancerHistory = "PERSONAL_CANCER_HISTORY";
        public const string Summary_Field_Name_FamilyCancerHistory = "FAMILY_CANCER_HISTORY";
        public const string Summary_Field_Name_NoOfPackYear = "NO_OF_PACK_YEAR";

        public const string Summary_Field_Name_Ref_Health = "REF_FROM_HEALTH";
        public const string Summary_Field_Name_Ref_SysReview = "REF_FROM_SYSREVIEW";
        public const string Summary_Field_Name_Ref_SocialHistory = "REF_FROM_SOCIALHEALTH";
        public const string Summary_Field_Name_Ref_FINHEALTH = "REF_FROM_FINHEALTH";

        //Event Summary Category
        public const string Summary_Category_Label_Name_CardiovascularHealth = "Cardiovascular Health";
        public const string Summary_Category_Label_Name_Obesity = "Obesity, Metabolic Syndrome & Diabetes";
        public const string Summary_Category_Label_Name_GastrointestinalHealth = "Gastrointestinal Health";
        public const string Summary_Category_Label_Name_LifestyleChoices = "Lifestyle Choices";
        public const string Summary_Category_Label_Name_GeriatricsAndMentalHealth = "Geriatrics and Mental Health";
        public const string Summary_Category_Label_Name_RenalHealth = "Renal Health";
        public const string Summary_Category_Label_Name_Cancers = "Cancers";
        public const string Summary_Category_Label_Name_SocialSupport = "Social Support";

        //Doctor Summary Category
        public const string Summary_Category_Label_Name_MedicalHistory = "Medical History";
        public const string Summary_Category_Label_Name_SmokingAndAlcoholUse = "Smoking and Alcohol Use";
        public const string Summary_Category_Label_Name_ReferredFrom = "Referred From";

        public const string Summary_Type_Event = "ESY";
        public const string Summary_Type_Doctor = "DSY";
        public const string Summary_Type_All = "SUM";

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
            HEADERSUB,
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

        public static string OperationFailedDuringDeletingValue(string value)
        {
            return "Operation failed during deleting " + value + ". Please contact system admin";
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
