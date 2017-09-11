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
        public const string Internal_Form_Type_PTSummary = "SUM_PT";
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

        public const string PreRegistration_Field_Name_Opt1 = "OPTION1";
        public const string PreRegistration_Field_Name_Opt2 = "OPTION2";
        public const string PreRegistration_Field_Name_Opt3 = "OPTION3";
        public const string PreRegistration_Field_Name_Opt4 = "OPTION4";
        public const string PreRegistration_Field_Name_Opt5 = "OPTION5";
        public const string PreRegistration_Field_Name_Opt6 = "OPTION6";
        public const string PreRegistration_Field_Name_Opt7 = "OPTION7";
        public const string PreRegistration_Field_Name_Opt8 = "OPTION8";
        public const string PreRegistration_Field_Name_Opt9 = "OPTION9";
        public const string PreRegistration_Field_Name_Opt10 = "OPTION10";

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

        public const string Registration_Field_Name_Opt1 = "OPTION1";
        public const string Registration_Field_Name_Opt2 = "OPTION2";
        public const string Registration_Field_Name_Opt3 = "OPTION3";
        public const string Registration_Field_Name_Opt4 = "OPTION4";
        public const string Registration_Field_Name_Opt5 = "OPTION5";
        public const string Registration_Field_Name_Opt6 = "OPTION6";
        public const string Registration_Field_Name_Opt7 = "OPTION7";
        public const string Registration_Field_Name_Opt8 = "OPTION8";
        public const string Registration_Field_Name_Opt9 = "OPTION9";
        public const string Registration_Field_Name_Opt10 = "OPTION10";

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

        //Refer from - for Doctor Consult
        public const string Summary_Field_Name_Ref_Health = "REF_FROM_HEALTH";
        public const string Summary_Field_Name_Ref_SysReview = "REF_FROM_SYSREVIEW";
        public const string Summary_Field_Name_Ref_SocialHistory = "REF_FROM_SOCIALHEALTH";
        public const string Summary_Field_Name_Ref_FINHEALTH = "REF_FROM_FINHEALTH";

        public const string Summary_Field_Name_Ref_HxTaking = "REF_FROM_HXTAKING";
        public const string Summary_Field_Name_Ref_HxTaking_Cond = "REF_FROM_HXTAKING_COND";
        public const string Summary_Field_Name_Ref_HxTaking_Reasoon = "REF_FROM_HXTAKING_REASON";

        public const string Summary_Field_Name_Ref_HxTaking_SS = "REF_FROM_HXTAKING_SS";
        public const string Summary_Field_Name_Ref_HxTaking_SS_Reason = "REF_FROM_HXTAKING_SS_REASON";

        public const string Summary_Field_Name_Ref_Geri_Vision_Doc = "REF_FROM_GERI_VISION_DOC";
        public const string Summary_Field_Name_Ref_Geri_Vision_OT = "REF_FROM_GERI_VISION_OT";

        public const string Summary_Field_Name_Ref_HxTaking_HealthCon = "REF_FROM_HXTAKING_HEALTHCON";
        public const string Summary_Field_Name_Ref_HxTaking_SysReview = "REF_FROM_HXTAKING_SYSREVIEW";
        public const string Summary_Field_Name_Ref_HxTaking_PastMed = "REF_FROM_HXTAKING_PASTMED";
        public const string Summary_Field_Name_Ref_FamHist = "REF_FROM_FAMHIST";
        public const string Summary_Field_Name_Ref_SocHist = "REF_FROM_SOCHIST";
        public const string Summary_Field_Name_Ref_FinStatus = "REF_FROM_FINSTATUS";
        public const string Summary_Field_Name_Ref_UrinaryInc = "REF_FROM_URINARYINC";
        public const string Summary_Field_Name_Ref_Vitals = "REF_FROM_VITALS";

        // Referral Summary 
        public const string Summary_Field_Name_RefSummary_DocCon = "REF_FROM_REFERRAL_DOCCON";
        public const string Summary_Field_Name_RefSummary_DocCon_MedIssueReason = "REF_FROM_REFERRAL_DOCCON_MEDISSUE_REASON";
        public const string Summary_Field_Name_RefSummary_DocCon_Reason = "REF_FROM_REFERRAL_DOCCON_REASON";
        public const string Summary_Field_Name_RefSummary_SocialSupport = "REF_FROM_REFERRAL_SOCIAL_SUPPORT";
        public const string Summary_Field_Name_RefSummary_SocialSupport_Reason = "REF_FROM_REFERRAL_SOCIAL_SUPPORT_REASON";



        //Congnitive 2nd Tier
        public const string Summary_Field_Name_Cog2nd_AMT_Ref = "Cog2nd_AMT_Ref";
        public const string Summary_Field_Name_Cog2nd_AMT_TotalScore = "Cog2nd_AMT_TotalScore";

        public const string Summary_Field_Name_Cog2nd_EBAS_Ref = "Cog2nd_EBAS_Ref";
        public const string Summary_Field_Name_Cog2nd_EBAS_TotalScore = "Cog2nd_EBAS_TotalScore";


        //PT Consult Fields
        public const string Summary_Field_Name_PTCon_ParQ_Result = "PT_ParQ_Result";
        public const string Summary_Field_Name_PTCon_PhysAct_Result = "PT_PhysAct_Result";
        public const string Summary_Field_Name_PTCon_Frail_Result = "PT_Frail_Result";
        public const string Summary_Field_Name_PTCon_SPPB_Result = "PT_SPPB_Result";
        public const string Summary_Field_Name_PTCon_TUG_Result = "PT_TUG_Result";


        //Event Summary Category
        public const string Summary_Category_Label_Name_CardiovascularHealth = "Cardiovascular Health";
        public const string Summary_Category_Label_Name_Obesity = "Obesity, Metabolic Syndrome & Diabetes";
        public const string Summary_Category_Label_Name_GastrointestinalHealth = "Gastrointestinal Health";
        public const string Summary_Category_Label_Name_LifestyleChoices = "Lifestyle Choices";
        public const string Summary_Category_Label_Name_GeriatricsAndMentalHealth = "Geriatrics and Mental Health";
        public const string Summary_Category_Label_Name_RenalHealth = "Renal Health";
        public const string Summary_Category_Label_Name_Cancers = "Cancers";

        //Referral Summary
        public const string Summary_Category_Label_Name_ReferralReason = "Referral Reason"; 
        public const string Summary_Category_Label_Name_ReferralSummary = "Referral Summary"; 

        //Doctor Summary Category
        public const string Summary_Category_Label_Name_MedicalHistory = "Medical History";
        public const string Summary_Category_Label_Name_SmokingAndAlcoholUse = "Smoking and Alcohol Use";
        public const string Summary_Category_Label_Name_ReferredFrom = "Referred From";

        //Doctor Consult Referral 
        public const string Summary_Category_Label_Name_DoctorConsult = "Doctor Consult";
        public const string Summary_Category_Label_Name_DoctorConsult_HxTaking = "Doctor Consult - History Taking";
        public const string Summary_Category_Label_Name_DoctorConsult_Geri_Vision = "Doctor Consult - Geriatrics - Vision";

        //Social Support Referral
        public const string Summary_Category_Label_Name_SocialSupport = "Social Support";
        public const string Summary_Category_Label_Name_SocialSupport_HxTaking = "Social Support - History Taking";

        //Cognitive Second Tier
        public const string Summary_Category_Label_Name_Cog2nd = "Cognitive 2nd Tier";
        public const string Summary_Category_Label_Name_Cog2nd_AMT = "Cognitive 2nd Tier - AMT";
        public const string Summary_Category_Label_Name_Cog2nd_EBAS = "Cognitive 2nd Tier - EBAS";

        //PT Consult
        public const string Summary_Category_Label_Name_PTConsult = "PT Consult"; 

        public const string Summary_Type_Event = "ESY";
        public const string Summary_Type_Doctor = "DSY";
        public const string Summary_Type_All = "SUM";
        public const string Summary_Type_PT = "SUM_PT";

        public const string Summary_Type_Cog2nd = "COG2ND";
        public const string Summary_Type_PTConsult = "PTCONSULT";
        public const string Summary_Type_OTConsult = "OTCONSULT";
        public const string Summary_Type_Exhibition = "EXHIBITION";
        public const string Summary_Type_SocialSup = "SOCIALSUP"; 


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
            TEXTBOXNUMBER,
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
