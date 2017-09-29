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

        public const string Export_SubmittedOn = "Submitted On";

        public const string Form_Option_Split = "\\|";
        public const string Form_Option_Split_Concate = "|";

        public static string FILESAVEPATHKEY = "filesavepath";
        public static string FILESAVENAMEKEY = "filesavename";
        public static string DEAULTFILEEXTENSIONS = ".jpg,.png,.gif,.pdf,.bmp,.zip";
        public static int DEAULTMAXFILESIZEINKB = 5000;
        public static int DEAULTMINFILESIZEINKB = 10;

        public const string Internal_Form_Type_Registration = "REG";
        public const string Internal_Form_Type_MegaSortingStation = "MEG";
        public const string Internal_Form_Type_Phlebotomy = "PHLEBOTOMY";
        public const string Internal_Form_Type_EventSummary = "ESY";
        public const string Internal_Form_Type_DoctorySummary = "DSY";
        public const string Internal_Form_Type_Cog2Summary = "COG2";
        public const string Internal_Form_Type_PTSummary = "PTSUM";
        public const string Internal_Form_Type_OTSummary = "OTSUM";
        public const string Internal_Form_Type_SocialSupSummary = "SOCIALSUP";
        public const string Internal_Form_Type_Exhibition = "EXHIBITION";
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
        public const string Summary_Field_Name_Cog2nd_AMT_Result1 = "Cog2nd_AMT_Result1";
        public const string Summary_Field_Name_Cog2nd_AMT_Result2 = "Cog2nd_AMT_Result2";
        public const string Summary_Field_Name_Cog2nd_AMT_Result3 = "Cog2nd_AMT_Result3";
        public const string Summary_Field_Name_Cog2nd_AMT_Result4 = "Cog2nd_AMT_Result4";
        public const string Summary_Field_Name_Cog2nd_AMT_Result5 = "Cog2nd_AMT_Result5";
        public const string Summary_Field_Name_Cog2nd_AMT_Result6 = "Cog2nd_AMT_Result6";
        public const string Summary_Field_Name_Cog2nd_AMT_Result7 = "Cog2nd_AMT_Result7";
        public const string Summary_Field_Name_Cog2nd_AMT_Result8 = "Cog2nd_AMT_Result8";
        public const string Summary_Field_Name_Cog2nd_AMT_Result9 = "Cog2nd_AMT_Result9";
        public const string Summary_Field_Name_Cog2nd_AMT_Result10 = "Cog2nd_AMT_Result10";
        public const string Summary_Field_Name_Cog2nd_AMT_Result11 = "Cog2nd_AMT_Result11";
        public const string Summary_Field_Name_Cog2nd_AMT_Result12 = "Cog2nd_AMT_Result12";
        public const string Summary_Field_Name_Cog2nd_AMT_Result13 = "Cog2nd_AMT_Result13";
        public const string Summary_Field_Name_Cog2nd_AMT_Result14 = "Cog2nd_AMT_Result14";
        public const string Summary_Field_Name_Cog2nd_AMT_Result15 = "Cog2nd_AMT_Result15";
        public const string Summary_Field_Name_Cog2nd_AMT_Result16 = "Cog2nd_AMT_Result16";
        public const string Summary_Field_Name_Cog2nd_AMT_Result17 = "Cog2nd_AMT_Result17";
        public const string Summary_Field_Name_Cog2nd_AMT_Result18 = "Cog2nd_AMT_Result18";
        public const string Summary_Field_Name_Cog2nd_AMT_Result19 = "Cog2nd_AMT_Result19";
        public const string Summary_Field_Name_Cog2nd_AMT_Result20 = "Cog2nd_AMT_Result20";

        public const string Summary_Field_Name_Cog2nd_EBAS_Ref = "Cog2nd_EBAS_Ref";
        public const string Summary_Field_Name_Cog2nd_EBAS_TotalScore = "Cog2nd_EBAS_TotalScore";
        public const string Summary_Field_Name_Cog2nd_EBAS_Result1 = "Cog2nd_EBAS_Result1";
        public const string Summary_Field_Name_Cog2nd_EBAS_Result2 = "Cog2nd_EBAS_Result2";
        public const string Summary_Field_Name_Cog2nd_EBAS_Result3 = "Cog2nd_EBAS_Result3";
        public const string Summary_Field_Name_Cog2nd_EBAS_Result4 = "Cog2nd_EBAS_Result4";
        public const string Summary_Field_Name_Cog2nd_EBAS_Result5 = "Cog2nd_EBAS_Result5";
        public const string Summary_Field_Name_Cog2nd_EBAS_Result6 = "Cog2nd_EBAS_Result6";
        public const string Summary_Field_Name_Cog2nd_EBAS_Result7 = "Cog2nd_EBAS_Result7";
        public const string Summary_Field_Name_Cog2nd_EBAS_Result8 = "Cog2nd_EBAS_Result8";
        public const string Summary_Field_Name_Cog2nd_EBAS_Result9 = "Cog2nd_EBAS_Result9";
        public const string Summary_Field_Name_Cog2nd_EBAS_Result10 = "Cog2nd_EBAS_Result10";
        public const string Summary_Field_Name_Cog2nd_EBAS_Result11 = "Cog2nd_EBAS_Result11";
        public const string Summary_Field_Name_Cog2nd_EBAS_Result12 = "Cog2nd_EBAS_Result12";
        public const string Summary_Field_Name_Cog2nd_EBAS_Result13 = "Cog2nd_EBAS_Result13";
        public const string Summary_Field_Name_Cog2nd_EBAS_Result14 = "Cog2nd_EBAS_Result14";
        public const string Summary_Field_Name_Cog2nd_EBAS_Result15 = "Cog2nd_EBAS_Result15";
        public const string Summary_Field_Name_Cog2nd_EBAS_Result16 = "Cog2nd_EBAS_Result16";
        public const string Summary_Field_Name_Cog2nd_EBAS_Result17 = "Cog2nd_EBAS_Result17";
        public const string Summary_Field_Name_Cog2nd_EBAS_Result18 = "Cog2nd_EBAS_Result18";
        public const string Summary_Field_Name_Cog2nd_EBAS_Result19 = "Cog2nd_EBAS_Result19";
        public const string Summary_Field_Name_Cog2nd_EBAS_Result20 = "Cog2nd_EBAS_Result20";


        //PT Consult Fields
        public const string Summary_Field_Name_PTCon_ParQ_Result = "PT_ParQ_Result";
        public const string Summary_Field_Name_PTCon_ParQ_Result1 = "PT_ParQ_Result1";
        public const string Summary_Field_Name_PTCon_ParQ_Result2 = "PT_ParQ_Result2";
        public const string Summary_Field_Name_PTCon_ParQ_Result3 = "PT_ParQ_Result3";
        public const string Summary_Field_Name_PTCon_ParQ_Result4 = "PT_ParQ_Result4";
        public const string Summary_Field_Name_PTCon_ParQ_Result5 = "PT_ParQ_Result5";
        public const string Summary_Field_Name_PTCon_ParQ_Result6 = "PT_ParQ_Result6";
        public const string Summary_Field_Name_PTCon_ParQ_Result7 = "PT_ParQ_Result7";
        public const string Summary_Field_Name_PTCon_ParQ_Result8 = "PT_ParQ_Result8";
        public const string Summary_Field_Name_PTCon_ParQ_Result9 = "PT_ParQ_Result9";
        public const string Summary_Field_Name_PTCon_ParQ_Result10 = "PT_ParQ_Result10";

        public const string Summary_Field_Name_PTCon_PhysAct_Result = "PT_PhysAct_Result";
        public const string Summary_Field_Name_PTCon_PhysAct_Result1 = "PT_PhysAct_Result1";
        public const string Summary_Field_Name_PTCon_PhysAct_Result2 = "PT_PhysAct_Result2";
        public const string Summary_Field_Name_PTCon_PhysAct_Result3 = "PT_PhysAct_Result3";
        public const string Summary_Field_Name_PTCon_PhysAct_Result4 = "PT_PhysAct_Result4";
        public const string Summary_Field_Name_PTCon_PhysAct_Result5 = "PT_PhysAct_Result5";
        public const string Summary_Field_Name_PTCon_PhysAct_Result6 = "PT_PhysAct_Result6";
        public const string Summary_Field_Name_PTCon_PhysAct_Result7 = "PT_PhysAct_Result7";
        public const string Summary_Field_Name_PTCon_PhysAct_Result8 = "PT_PhysAct_Result8";
        public const string Summary_Field_Name_PTCon_PhysAct_Result9 = "PT_PhysAct_Result9";
        public const string Summary_Field_Name_PTCon_PhysAct_Result10 = "PT_PhysAct_Result10";
        
        public const string Summary_Field_Name_PTCon_Frail_Result1 = "PT_Frail_Result1";
        public const string Summary_Field_Name_PTCon_Frail_Result2 = "PT_Frail_Result2";
        public const string Summary_Field_Name_PTCon_Frail_Result3 = "PT_Frail_Result3";
        public const string Summary_Field_Name_PTCon_Frail_Result4 = "PT_Frail_Result4";
        public const string Summary_Field_Name_PTCon_Frail_Result5 = "PT_Frail_Result5";
        public const string Summary_Field_Name_PTCon_Frail_Result6 = "PT_Frail_Result6";
        public const string Summary_Field_Name_PTCon_Frail_Result7 = "PT_Frail_Result7";
        public const string Summary_Field_Name_PTCon_Frail_Result8 = "PT_Frail_Result8";
        public const string Summary_Field_Name_PTCon_Frail_Result9 = "PT_Frail_Result9";
        public const string Summary_Field_Name_PTCon_Frail_Result10 = "PT_Frail_Result10";

        public const string Summary_Field_Name_PTCon_SPPB_Result1 = "PT_SPPB_Result1";
        public const string Summary_Field_Name_PTCon_SPPB_Result2 = "PT_SPPB_Result2";
        public const string Summary_Field_Name_PTCon_SPPB_Result3 = "PT_SPPB_Result3";
        public const string Summary_Field_Name_PTCon_SPPB_Result4 = "PT_SPPB_Result4";
        public const string Summary_Field_Name_PTCon_SPPB_Result5 = "PT_SPPB_Result5";
        public const string Summary_Field_Name_PTCon_SPPB_Result6 = "PT_SPPB_Result6";
        public const string Summary_Field_Name_PTCon_SPPB_Result7 = "PT_SPPB_Result7";
        public const string Summary_Field_Name_PTCon_SPPB_Result8 = "PT_SPPB_Result8";
        public const string Summary_Field_Name_PTCon_SPPB_Result9 = "PT_SPPB_Result9";
        public const string Summary_Field_Name_PTCon_SPPB_Result10 = "PT_SPPB_Result10";

        public const string Summary_Field_Name_PTCon_TUG_Result1 = "PT_TUG_Result1";
        public const string Summary_Field_Name_PTCon_TUG_Result2 = "PT_TUG_Result2";
        public const string Summary_Field_Name_PTCon_TUG_Result3 = "PT_TUG_Result3";
        public const string Summary_Field_Name_PTCon_TUG_Result4 = "PT_TUG_Result4";
        public const string Summary_Field_Name_PTCon_TUG_Result5 = "PT_TUG_Result5";
        public const string Summary_Field_Name_PTCon_TUG_Result6 = "PT_TUG_Result6";
        public const string Summary_Field_Name_PTCon_TUG_Result7 = "PT_TUG_Result7";
        public const string Summary_Field_Name_PTCon_TUG_Result8 = "PT_TUG_Result8";
        public const string Summary_Field_Name_PTCon_TUG_Result9 = "PT_TUG_Result9";
        public const string Summary_Field_Name_PTCon_TUG_Result10 = "PT_TUG_Result10";

        public const string Summary_Field_Name_PTCon_Frail_Result = "PT_Frail_Result";
        public const string Summary_Field_Name_PTCon_SPPB_Result = "PT_SPPB_Result";
        public const string Summary_Field_Name_PTCon_TUG_Result = "PT_TUG_Result";

        public const string Summary_Field_Name_OTCon_Questionnaire_Result1 = "OT_Questionnaire_Result1";
        public const string Summary_Field_Name_OTCon_Questionnaire_Result2 = "OT_Questionnaire_Result2";
        public const string Summary_Field_Name_OTCon_Questionnaire_Result3 = "OT_Questionnaire_Result3";
        public const string Summary_Field_Name_OTCon_Questionnaire_Result4 = "OT_Questionnaire_Result4";
        public const string Summary_Field_Name_OTCon_Questionnaire_Result5 = "OT_Questionnaire_Result5";
        public const string Summary_Field_Name_OTCon_Questionnaire_Result6 = "OT_Questionnaire_Result6";
        public const string Summary_Field_Name_OTCon_Questionnaire_Result7 = "OT_Questionnaire_Result7";
        public const string Summary_Field_Name_OTCon_Questionnaire_Result8 = "OT_Questionnaire_Result8";
        public const string Summary_Field_Name_OTCon_Questionnaire_Result9 = "OT_Questionnaire_Result9";
        public const string Summary_Field_Name_OTCon_Questionnaire_Result10 = "OT_Questionnaire_Result10";

        public const string Summary_Field_Name_Exhibition_Result1 = "Exhibition_Result1";
        public const string Summary_Field_Name_Exhibition_Result2 = "Exhibition_Result2";
        public const string Summary_Field_Name_Exhibition_Result3 = "Exhibition_Result3";
        public const string Summary_Field_Name_Exhibition_Result4 = "Exhibition_Result4";
        public const string Summary_Field_Name_Exhibition_Result5 = "Exhibition_Result5";
        public const string Summary_Field_Name_Exhibition_Result6 = "Exhibition_Result6";
        public const string Summary_Field_Name_Exhibition_Result7 = "Exhibition_Result7";
        public const string Summary_Field_Name_Exhibition_Result8 = "Exhibition_Result8";
        public const string Summary_Field_Name_Exhibition_Result9 = "Exhibition_Result9";
        public const string Summary_Field_Name_Exhibition_Result10 = "Exhibition_Result10";
        public const string Summary_Field_Name_Exhibition_Result11 = "Exhibition_Result11";
        public const string Summary_Field_Name_Exhibition_Result12 = "Exhibition_Result12";
        public const string Summary_Field_Name_Exhibition_Result13 = "Exhibition_Result13";
        public const string Summary_Field_Name_Exhibition_Result14 = "Exhibition_Result14";
        public const string Summary_Field_Name_Exhibition_Result15 = "Exhibition_Result15";
        public const string Summary_Field_Name_Exhibition_Result16 = "Exhibition_Result16";
        public const string Summary_Field_Name_Exhibition_Result17 = "Exhibition_Result17";
        public const string Summary_Field_Name_Exhibition_Result18 = "Exhibition_Result18";
        public const string Summary_Field_Name_Exhibition_Result19 = "Exhibition_Result19";
        public const string Summary_Field_Name_Exhibition_Result20 = "Exhibition_Result20";
        public const string Summary_Field_Name_Exhibition_Result21 = "Exhibition_Result21";
        public const string Summary_Field_Name_Exhibition_Result22 = "Exhibition_Result22";
        public const string Summary_Field_Name_Exhibition_Result23 = "Exhibition_Result23";
        public const string Summary_Field_Name_Exhibition_Result24 = "Exhibition_Result24";
        public const string Summary_Field_Name_Exhibition_Result25 = "Exhibition_Result25";
        public const string Summary_Field_Name_Exhibition_Result26 = "Exhibition_Result26";
        public const string Summary_Field_Name_Exhibition_Result27 = "Exhibition_Result27";
        public const string Summary_Field_Name_Exhibition_Result28 = "Exhibition_Result28";
        public const string Summary_Field_Name_Exhibition_Result29 = "Exhibition_Result29";
        public const string Summary_Field_Name_Exhibition_Result30 = "Exhibition_Result30";

        public const string Sumamry_Field_Name_SocialSupp_Result1 = "SocialSupp_Result1";

        //For reference to Doctor Consult
        public const string Summary_Field_Name_DocConsult_ReferredTo1 = "DocConsultReferredTo1";
        public const string Summary_Field_Name_DocConsult_ReferredTo2 = "DocConsultReferredTo2";
        public const string Summary_Field_Name_DocConsult_ReferredTo3 = "DocConsultReferredTo3";
        public const string Summary_Field_Name_DocConsult_ReferredTo4 = "DocConsultReferredTo4";
        public const string Summary_Field_Name_DocConsult_ReferredTo5 = "DocConsultReferredTo5";
        public const string Summary_Field_Name_DocConsult_ReferredTo6 = "DocConsultReferredTo6";
        public const string Summary_Field_Name_DocConsult_ReferredTo7 = "DocConsultReferredTo7";
        public const string Summary_Field_Name_DocConsult_ReferredTo8 = "DocConsultReferredTo8";
        public const string Summary_Field_Name_DocConsult_ReferredTo9 = "DocConsultReferredTo9";
        public const string Summary_Field_Name_DocConsult_ReferredTo10 = "DocConsultReferredTo10";
        public const string Summary_Field_Name_DocConsult_ReferredTo11 = "DocConsultReferredTo11";
        public const string Summary_Field_Name_DocConsult_ReferredTo12 = "DocConsultReferredTo12";
        public const string Summary_Field_Name_DocConsult_ReferredTo13 = "DocConsultReferredTo13";
        public const string Summary_Field_Name_DocConsult_ReferredTo14 = "DocConsultReferredTo14";
        public const string Summary_Field_Name_DocConsult_ReferredTo15 = "DocConsultReferredTo15";
        public const string Summary_Field_Name_DocConsult_ReferredTo16 = "DocConsultReferredTo16";
        public const string Summary_Field_Name_DocConsult_ReferredTo17 = "DocConsultReferredTo17";
        public const string Summary_Field_Name_DocConsult_ReferredTo18 = "DocConsultReferredTo18";
        public const string Summary_Field_Name_DocConsult_ReferredTo19 = "DocConsultReferredTo19";
        public const string Summary_Field_Name_DocConsult_ReferredTo20 = "DocConsultReferredTo20";
        public const string Summary_Field_Name_DocConsult_ReferredTo21 = "DocConsultReferredTo21";
        public const string Summary_Field_Name_DocConsult_ReferredTo22 = "DocConsultReferredTo22";
        public const string Summary_Field_Name_DocConsult_ReferredTo23 = "DocConsultReferredTo23";
        public const string Summary_Field_Name_DocConsult_ReferredTo24 = "DocConsultReferredTo24";
        public const string Summary_Field_Name_DocConsult_ReferredTo25 = "DocConsultReferredTo25";
        public const string Summary_Field_Name_DocConsult_ReferredTo26 = "DocConsultReferredTo26";
        public const string Summary_Field_Name_DocConsult_ReferredTo27 = "DocConsultReferredTo27";
        public const string Summary_Field_Name_DocConsult_ReferredTo28 = "DocConsultReferredTo28";
        public const string Summary_Field_Name_DocConsult_ReferredTo29 = "DocConsultReferredTo29";
        public const string Summary_Field_Name_DocConsult_ReferredTo30 = "DocConsultReferredTo30";
        public const string Summary_Field_Name_DocConsult_ReferredTo31 = "DocConsultReferredTo31";
        public const string Summary_Field_Name_DocConsult_ReferredTo32 = "DocConsultReferredTo32";
        public const string Summary_Field_Name_DocConsult_ReferredTo33 = "DocConsultReferredTo33";
        public const string Summary_Field_Name_DocConsult_ReferredTo34 = "DocConsultReferredTo34";
        public const string Summary_Field_Name_DocConsult_ReferredTo35 = "DocConsultReferredTo35";
        public const string Summary_Field_Name_DocConsult_ReferredTo36 = "DocConsultReferredTo36";
        public const string Summary_Field_Name_DocConsult_ReferredTo37 = "DocConsultReferredTo37";
        public const string Summary_Field_Name_DocConsult_ReferredTo38 = "DocConsultReferredTo38";
        public const string Summary_Field_Name_DocConsult_ReferredTo39 = "DocConsultReferredTo39";
        public const string Summary_Field_Name_DocConsult_ReferredTo40 = "DocConsultReferredTo40";
        public const string Summary_Field_Name_DocConsult_ReferredTo41 = "DocConsultReferredTo41";
        public const string Summary_Field_Name_DocConsult_ReferredTo42 = "DocConsultReferredTo42";
        public const string Summary_Field_Name_DocConsult_ReferredTo43 = "DocConsultReferredTo43";
        public const string Summary_Field_Name_DocConsult_ReferredTo44 = "DocConsultReferredTo44";
        public const string Summary_Field_Name_DocConsult_ReferredTo45 = "DocConsultReferredTo45";
        public const string Summary_Field_Name_DocConsult_ReferredTo46 = "DocConsultReferredTo46";
        public const string Summary_Field_Name_DocConsult_ReferredTo47 = "DocConsultReferredTo47";
        public const string Summary_Field_Name_DocConsult_ReferredTo48 = "DocConsultReferredTo48";
        public const string Summary_Field_Name_DocConsult_ReferredTo49 = "DocConsultReferredTo49";
        public const string Summary_Field_Name_DocConsult_ReferredTo50 = "DocConsultReferredTo50";
        public const string Summary_Field_Name_DocConsult_ReferredTo51 = "DocConsultReferredTo51";
        public const string Summary_Field_Name_DocConsult_ReferredTo52 = "DocConsultReferredTo52";
        public const string Summary_Field_Name_DocConsult_ReferredTo53 = "DocConsultReferredTo53";
        public const string Summary_Field_Name_DocConsult_ReferredTo54 = "DocConsultReferredTo54";
        public const string Summary_Field_Name_DocConsult_ReferredTo55 = "DocConsultReferredTo55";
        public const string Summary_Field_Name_DocConsult_ReferredTo56 = "DocConsultReferredTo56";
        public const string Summary_Field_Name_DocConsult_ReferredTo57 = "DocConsultReferredTo57";
        public const string Summary_Field_Name_DocConsult_ReferredTo58 = "DocConsultReferredTo58";
        public const string Summary_Field_Name_DocConsult_ReferredTo59 = "DocConsultReferredTo59";
        public const string Summary_Field_Name_DocConsult_ReferredTo60 = "DocConsultReferredTo60";

        //Doc Consult Results
        public const string Summary_Field_Name_DocConsult_Result1 = "DocConsultResult1";
        public const string Summary_Field_Name_DocConsult_Result2 = "DocConsultResult2";
        public const string Summary_Field_Name_DocConsult_Result3 = "DocConsultResult3";
        public const string Summary_Field_Name_DocConsult_Result4 = "DocConsultResult4";
        public const string Summary_Field_Name_DocConsult_Result5 = "DocConsultResult5";
        public const string Summary_Field_Name_DocConsult_Result6 = "DocConsultResult6";
        public const string Summary_Field_Name_DocConsult_Result7 = "DocConsultResult7";
        public const string Summary_Field_Name_DocConsult_Result8 = "DocConsultResult8";
        public const string Summary_Field_Name_DocConsult_Result9 = "DocConsultResult9";
        public const string Summary_Field_Name_DocConsult_Result10 = "DocConsultResult10";
        public const string Summary_Field_Name_DocConsult_Result11 = "DocConsultResult11";
        public const string Summary_Field_Name_DocConsult_Result12 = "DocConsultResult12";
        public const string Summary_Field_Name_DocConsult_Result13 = "DocConsultResult13";
        public const string Summary_Field_Name_DocConsult_Result14 = "DocConsultResult14";
        public const string Summary_Field_Name_DocConsult_Result15 = "DocConsultResult15";
        public const string Summary_Field_Name_DocConsult_Result16 = "DocConsultResult16";
        public const string Summary_Field_Name_DocConsult_Result17 = "DocConsultResult17";
        public const string Summary_Field_Name_DocConsult_Result18 = "DocConsultResult18";
        public const string Summary_Field_Name_DocConsult_Result19 = "DocConsultResult19";
        public const string Summary_Field_Name_DocConsult_Result20 = "DocConsultResult20";
        public const string Summary_Field_Name_DocConsult_Result21 = "DocConsultResult21";
        public const string Summary_Field_Name_DocConsult_Result22 = "DocConsultResult22";
        public const string Summary_Field_Name_DocConsult_Result23 = "DocConsultResult23";
        public const string Summary_Field_Name_DocConsult_Result24 = "DocConsultResult24";
        public const string Summary_Field_Name_DocConsult_Result25 = "DocConsultResult25";
        public const string Summary_Field_Name_DocConsult_Result26 = "DocConsultResult26";
        public const string Summary_Field_Name_DocConsult_Result27 = "DocConsultResult27";
        public const string Summary_Field_Name_DocConsult_Result28 = "DocConsultResult28";
        public const string Summary_Field_Name_DocConsult_Result29 = "DocConsultResult29";
        public const string Summary_Field_Name_DocConsult_Result30 = "DocConsultResult30";
        public const string Summary_Field_Name_DocConsult_Result31 = "DocConsultResult31";
        public const string Summary_Field_Name_DocConsult_Result32 = "DocConsultResult32";
        public const string Summary_Field_Name_DocConsult_Result33 = "DocConsultResult33";
        public const string Summary_Field_Name_DocConsult_Result34 = "DocConsultResult34";
        public const string Summary_Field_Name_DocConsult_Result35 = "DocConsultResult35";
        public const string Summary_Field_Name_DocConsult_Result36 = "DocConsultResult36";
        public const string Summary_Field_Name_DocConsult_Result37 = "DocConsultResult37";
        public const string Summary_Field_Name_DocConsult_Result38 = "DocConsultResult38";
        public const string Summary_Field_Name_DocConsult_Result39 = "DocConsultResult39";
        public const string Summary_Field_Name_DocConsult_Result40 = "DocConsultResult40";
        public const string Summary_Field_Name_DocConsult_Result41 = "DocConsultResult41";
        public const string Summary_Field_Name_DocConsult_Result42 = "DocConsultResult42";
        public const string Summary_Field_Name_DocConsult_Result43 = "DocConsultResult43";
        public const string Summary_Field_Name_DocConsult_Result44 = "DocConsultResult44";
        public const string Summary_Field_Name_DocConsult_Result45 = "DocConsultResult45";
        public const string Summary_Field_Name_DocConsult_Result46 = "DocConsultResult46";
        public const string Summary_Field_Name_DocConsult_Result47 = "DocConsultResult47";
        public const string Summary_Field_Name_DocConsult_Result48 = "DocConsultResult48";
        public const string Summary_Field_Name_DocConsult_Result49 = "DocConsultResult49";
        public const string Summary_Field_Name_DocConsult_Result50 = "DocConsultResult50";
        public const string Summary_Field_Name_DocConsult_Result51 = "DocConsultResult51";
        public const string Summary_Field_Name_DocConsult_Result52 = "DocConsultResult52";
        public const string Summary_Field_Name_DocConsult_Result53 = "DocConsultResult53";
        public const string Summary_Field_Name_DocConsult_Result54 = "DocConsultResult54";
        public const string Summary_Field_Name_DocConsult_Result55 = "DocConsultResult55";
        public const string Summary_Field_Name_DocConsult_Result56 = "DocConsultResult56";
        public const string Summary_Field_Name_DocConsult_Result57 = "DocConsultResult57";
        public const string Summary_Field_Name_DocConsult_Result58 = "DocConsultResult58";
        public const string Summary_Field_Name_DocConsult_Result59 = "DocConsultResult59";
        public const string Summary_Field_Name_DocConsult_Result60 = "DocConsultResult60";
        public const string Summary_Field_Name_DocConsult_Result61 = "DocConsultResult61";
        public const string Summary_Field_Name_DocConsult_Result62 = "DocConsultResult62";
        public const string Summary_Field_Name_DocConsult_Result63 = "DocConsultResult63";
        public const string Summary_Field_Name_DocConsult_Result64 = "DocConsultResult64";
        public const string Summary_Field_Name_DocConsult_Result65 = "DocConsultResult65";
        public const string Summary_Field_Name_DocConsult_Result66 = "DocConsultResult66";
        public const string Summary_Field_Name_DocConsult_Result67 = "DocConsultResult67";
        public const string Summary_Field_Name_DocConsult_Result68 = "DocConsultResult68";
        public const string Summary_Field_Name_DocConsult_Result69 = "DocConsultResult69";
        public const string Summary_Field_Name_DocConsult_Result70 = "DocConsultResult70";

        public const string Summary_Field_Name_Reg_Field0 = "RegField0";
        public const string Summary_Field_Name_Reg_Field1 = "RegField1";
        public const string Summary_Field_Name_Reg_Field2 = "RegField2";
        public const string Summary_Field_Name_Reg_Field3 = "RegField3";
        public const string Summary_Field_Name_Reg_Field4 = "RegField4";
        public const string Summary_Field_Name_Reg_Field5 = "RegField5";
        public const string Summary_Field_Name_Reg_Field6 = "RegField6";
        public const string Summary_Field_Name_Reg_Field7 = "RegField7";
        public const string Summary_Field_Name_Reg_Field8 = "RegField8";
        public const string Summary_Field_Name_Reg_Field9 = "RegField9";

        public const string Summary_Field_Name_Phlebo_Field0 = "PhelbField0";
        public const string Summary_Field_Name_Phlebo_Field1 = "PhelbField1";

        //Hx Taking
        public const string Summary_Field_Name_HxTaking_Field0 = "HxTakingField0";
        public const string Summary_Field_Name_HxTaking_Field1 = "HxTakingField1";
        public const string Summary_Field_Name_HxTaking_Field2 = "HxTakingField2";
        public const string Summary_Field_Name_HxTaking_Field3 = "HxTakingField3";
        public const string Summary_Field_Name_HxTaking_Field4 = "HxTakingField4";
        public const string Summary_Field_Name_HxTaking_Field5 = "HxTakingField5";
        public const string Summary_Field_Name_HxTaking_Field6 = "HxTakingField6";
        public const string Summary_Field_Name_HxTaking_Field7 = "HxTakingField7";
        public const string Summary_Field_Name_HxTaking_Field8 = "HxTakingField8";
        public const string Summary_Field_Name_HxTaking_Field9 = "HxTakingField9";
        public const string Summary_Field_Name_HxTaking_Field10 = "HxTakingField10";
        public const string Summary_Field_Name_HxTaking_Field11 = "HxTakingField11";
        public const string Summary_Field_Name_HxTaking_Field12 = "HxTakingField12";
        public const string Summary_Field_Name_HxTaking_Field13 = "HxTakingField13";
        public const string Summary_Field_Name_HxTaking_Field14 = "HxTakingField14";
        public const string Summary_Field_Name_HxTaking_Field15 = "HxTakingField15";
        public const string Summary_Field_Name_HxTaking_Field16 = "HxTakingField16";
        public const string Summary_Field_Name_HxTaking_Field17 = "HxTakingField17";
        public const string Summary_Field_Name_HxTaking_Field18 = "HxTakingField18";
        public const string Summary_Field_Name_HxTaking_Field19 = "HxTakingField19";
        public const string Summary_Field_Name_HxTaking_Field20 = "HxTakingField20";
        public const string Summary_Field_Name_HxTaking_Field21 = "HxTakingField21";
        public const string Summary_Field_Name_HxTaking_Field22 = "HxTakingField22";
        public const string Summary_Field_Name_HxTaking_Field23 = "HxTakingField23";
        public const string Summary_Field_Name_HxTaking_Field24 = "HxTakingField24";
        public const string Summary_Field_Name_HxTaking_Field25 = "HxTakingField25";
        public const string Summary_Field_Name_HxTaking_Field26 = "HxTakingField26";
        public const string Summary_Field_Name_HxTaking_Field27 = "HxTakingField27";
        public const string Summary_Field_Name_HxTaking_Field28 = "HxTakingField28";
        public const string Summary_Field_Name_HxTaking_Field29 = "HxTakingField29";
        public const string Summary_Field_Name_HxTaking_Field30 = "HxTakingField30";
        public const string Summary_Field_Name_HxTaking_Field31 = "HxTakingField31";
        public const string Summary_Field_Name_HxTaking_Field32 = "HxTakingField32";
        public const string Summary_Field_Name_HxTaking_Field33 = "HxTakingField33";
        public const string Summary_Field_Name_HxTaking_Field34 = "HxTakingField34";
        public const string Summary_Field_Name_HxTaking_Field35 = "HxTakingField35";
        public const string Summary_Field_Name_HxTaking_Field36 = "HxTakingField36";
        public const string Summary_Field_Name_HxTaking_Field37 = "HxTakingField37";
        public const string Summary_Field_Name_HxTaking_Field38 = "HxTakingField38";
        public const string Summary_Field_Name_HxTaking_Field39 = "HxTakingField39";
        public const string Summary_Field_Name_HxTaking_Field40 = "HxTakingField40";
        public const string Summary_Field_Name_HxTaking_Field41 = "HxTakingField41";
        public const string Summary_Field_Name_HxTaking_Field42 = "HxTakingField42";
        public const string Summary_Field_Name_HxTaking_Field43 = "HxTakingField43";
        public const string Summary_Field_Name_HxTaking_Field44 = "HxTakingField44";
        public const string Summary_Field_Name_HxTaking_Field45 = "HxTakingField45";
        public const string Summary_Field_Name_HxTaking_Field46 = "HxTakingField46";
        public const string Summary_Field_Name_HxTaking_Field47 = "HxTakingField47";
        public const string Summary_Field_Name_HxTaking_Field48 = "HxTakingField48";
        public const string Summary_Field_Name_HxTaking_Field49 = "HxTakingField49";
        public const string Summary_Field_Name_HxTaking_Field50 = "HxTakingField50";
        public const string Summary_Field_Name_HxTaking_Field51 = "HxTakingField51";
        public const string Summary_Field_Name_HxTaking_Field52 = "HxTakingField52";
        public const string Summary_Field_Name_HxTaking_Field53 = "HxTakingField53";
        public const string Summary_Field_Name_HxTaking_Field54 = "HxTakingField54";
        public const string Summary_Field_Name_HxTaking_Field55 = "HxTakingField55";
        public const string Summary_Field_Name_HxTaking_Field56 = "HxTakingField56";
        public const string Summary_Field_Name_HxTaking_Field57 = "HxTakingField57";
        public const string Summary_Field_Name_HxTaking_Field58 = "HxTakingField58";
        public const string Summary_Field_Name_HxTaking_Field59 = "HxTakingField59";
        public const string Summary_Field_Name_HxTaking_Field60 = "HxTakingField60";
        public const string Summary_Field_Name_HxTaking_Field61 = "HxTakingField61";
        public const string Summary_Field_Name_HxTaking_Field62 = "HxTakingField62";
        public const string Summary_Field_Name_HxTaking_Field63 = "HxTakingField63";
        public const string Summary_Field_Name_HxTaking_Field64 = "HxTakingField64";
        public const string Summary_Field_Name_HxTaking_Field65 = "HxTakingField65";
        public const string Summary_Field_Name_HxTaking_Field66 = "HxTakingField66";
        public const string Summary_Field_Name_HxTaking_Field67 = "HxTakingField67";
        public const string Summary_Field_Name_HxTaking_Field68 = "HxTakingField68";
        public const string Summary_Field_Name_HxTaking_Field69 = "HxTakingField69";
        public const string Summary_Field_Name_HxTaking_Field70 = "HxTakingField70";
        public const string Summary_Field_Name_HxTaking_Field71 = "HxTakingField71";
        public const string Summary_Field_Name_HxTaking_Field72 = "HxTakingField72";
        public const string Summary_Field_Name_HxTaking_Field73 = "HxTakingField73";
        public const string Summary_Field_Name_HxTaking_Field74 = "HxTakingField74";
        public const string Summary_Field_Name_HxTaking_Field75 = "HxTakingField75";
        public const string Summary_Field_Name_HxTaking_Field76 = "HxTakingField76";
        public const string Summary_Field_Name_HxTaking_Field77 = "HxTakingField77";
        public const string Summary_Field_Name_HxTaking_Field78 = "HxTakingField78";
        public const string Summary_Field_Name_HxTaking_Field79 = "HxTakingField79";
        public const string Summary_Field_Name_HxTaking_Field80 = "HxTakingField80";
        public const string Summary_Field_Name_HxTaking_Field81 = "HxTakingField81";
        public const string Summary_Field_Name_HxTaking_Field82 = "HxTakingField82";
        public const string Summary_Field_Name_HxTaking_Field83 = "HxTakingField83";
        public const string Summary_Field_Name_HxTaking_Field84 = "HxTakingField84";
        public const string Summary_Field_Name_HxTaking_Field85 = "HxTakingField85";
        public const string Summary_Field_Name_HxTaking_Field86 = "HxTakingField86";
        public const string Summary_Field_Name_HxTaking_Field87 = "HxTakingField87";
        public const string Summary_Field_Name_HxTaking_Field88 = "HxTakingField88";
        public const string Summary_Field_Name_HxTaking_Field89 = "HxTakingField89";
        public const string Summary_Field_Name_HxTaking_Field90 = "HxTakingField90";
        public const string Summary_Field_Name_HxTaking_Field91 = "HxTakingField91";
        public const string Summary_Field_Name_HxTaking_Field92 = "HxTakingField92";
        public const string Summary_Field_Name_HxTaking_Field93 = "HxTakingField93";
        public const string Summary_Field_Name_HxTaking_Field94 = "HxTakingField94";
        public const string Summary_Field_Name_HxTaking_Field95 = "HxTakingField95";
        public const string Summary_Field_Name_HxTaking_Field96 = "HxTakingField96";
        public const string Summary_Field_Name_HxTaking_Field97 = "HxTakingField97";
        public const string Summary_Field_Name_HxTaking_Field98 = "HxTakingField98";
        public const string Summary_Field_Name_HxTaking_Field99 = "HxTakingField99";
        public const string Summary_Field_Name_HxTaking_Field100 = "HxTakingField100";
        public const string Summary_Field_Name_HxTaking_Field101 = "HxTakingField101";
        public const string Summary_Field_Name_HxTaking_Field102 = "HxTakingField102";
        public const string Summary_Field_Name_HxTaking_Field103 = "HxTakingField103";
        public const string Summary_Field_Name_HxTaking_Field104 = "HxTakingField104";
        public const string Summary_Field_Name_HxTaking_Field105 = "HxTakingField105";
        public const string Summary_Field_Name_HxTaking_Field106 = "HxTakingField106";
        public const string Summary_Field_Name_HxTaking_Field107 = "HxTakingField107";
        public const string Summary_Field_Name_HxTaking_Field108 = "HxTakingField108";
        public const string Summary_Field_Name_HxTaking_Field109 = "HxTakingField109";
        public const string Summary_Field_Name_HxTaking_Field110 = "HxTakingField110";
        public const string Summary_Field_Name_HxTaking_Field111 = "HxTakingField111";
        public const string Summary_Field_Name_HxTaking_Field112 = "HxTakingField112";
        public const string Summary_Field_Name_HxTaking_Field113 = "HxTakingField113";
        public const string Summary_Field_Name_HxTaking_Field114 = "HxTakingField114";
        public const string Summary_Field_Name_HxTaking_Field115 = "HxTakingField115";
        public const string Summary_Field_Name_HxTaking_Field116 = "HxTakingField116";
        public const string Summary_Field_Name_HxTaking_Field117 = "HxTakingField117";
        public const string Summary_Field_Name_HxTaking_Field118 = "HxTakingField118";
        public const string Summary_Field_Name_HxTaking_Field119 = "HxTakingField119";
        public const string Summary_Field_Name_HxTaking_Field120 = "HxTakingField120";
        public const string Summary_Field_Name_HxTaking_Field121 = "HxTakingField121";
        public const string Summary_Field_Name_HxTaking_Field122 = "HxTakingField122";
        public const string Summary_Field_Name_HxTaking_Field123 = "HxTakingField123";
        public const string Summary_Field_Name_HxTaking_Field124 = "HxTakingField124";
        public const string Summary_Field_Name_HxTaking_Field125 = "HxTakingField125";
        public const string Summary_Field_Name_HxTaking_Field126 = "HxTakingField126";
        public const string Summary_Field_Name_HxTaking_Field127 = "HxTakingField127";
        public const string Summary_Field_Name_HxTaking_Field128 = "HxTakingField128";
        public const string Summary_Field_Name_HxTaking_Field129 = "HxTakingField129";
        public const string Summary_Field_Name_HxTaking_Field130 = "HxTakingField130";
        public const string Summary_Field_Name_HxTaking_Field131 = "HxTakingField131";
        public const string Summary_Field_Name_HxTaking_Field132 = "HxTakingField132";
        public const string Summary_Field_Name_HxTaking_Field133 = "HxTakingField133";
        public const string Summary_Field_Name_HxTaking_Field134 = "HxTakingField134";
        public const string Summary_Field_Name_HxTaking_Field135 = "HxTakingField135";
        public const string Summary_Field_Name_HxTaking_Field136 = "HxTakingField136";
        public const string Summary_Field_Name_HxTaking_Field137 = "HxTakingField137";
        public const string Summary_Field_Name_HxTaking_Field138 = "HxTakingField138";
        public const string Summary_Field_Name_HxTaking_Field139 = "HxTakingField139";
        public const string Summary_Field_Name_HxTaking_Field140 = "HxTakingField140";
        public const string Summary_Field_Name_HxTaking_Field141 = "HxTakingField141";
        public const string Summary_Field_Name_HxTaking_Field142 = "HxTakingField142";
        public const string Summary_Field_Name_HxTaking_Field143 = "HxTakingField143";
        public const string Summary_Field_Name_HxTaking_Field144 = "HxTakingField144";
        public const string Summary_Field_Name_HxTaking_Field145 = "HxTakingField145";
        public const string Summary_Field_Name_HxTaking_Field146 = "HxTakingField146";
        public const string Summary_Field_Name_HxTaking_Field147 = "HxTakingField147";
        public const string Summary_Field_Name_HxTaking_Field148 = "HxTakingField148";
        public const string Summary_Field_Name_HxTaking_Field149 = "HxTakingField149";

        //Geri Fields 
        public const string Summary_Field_Name_Geri_Field0 = "GeriField0";
        public const string Summary_Field_Name_Geri_Field1 = "GeriField1";
        public const string Summary_Field_Name_Geri_Field2 = "GeriField2";
        public const string Summary_Field_Name_Geri_Field3 = "GeriField3";
        public const string Summary_Field_Name_Geri_Field4 = "GeriField4";
        public const string Summary_Field_Name_Geri_Field5 = "GeriField5";
        public const string Summary_Field_Name_Geri_Field6 = "GeriField6";
        public const string Summary_Field_Name_Geri_Field7 = "GeriField7";
        public const string Summary_Field_Name_Geri_Field8 = "GeriField8";
        public const string Summary_Field_Name_Geri_Field9 = "GeriField9";
        public const string Summary_Field_Name_Geri_Field10 = "GeriField10";
        public const string Summary_Field_Name_Geri_Field11 = "GeriField11";
        public const string Summary_Field_Name_Geri_Field12 = "GeriField12";
        public const string Summary_Field_Name_Geri_Field13 = "GeriField13";
        public const string Summary_Field_Name_Geri_Field14 = "GeriField14";
        public const string Summary_Field_Name_Geri_Field15 = "GeriField15";
        public const string Summary_Field_Name_Geri_Field16 = "GeriField16";
        public const string Summary_Field_Name_Geri_Field17 = "GeriField17";
        public const string Summary_Field_Name_Geri_Field18 = "GeriField18";
        public const string Summary_Field_Name_Geri_Field19 = "GeriField19";
        public const string Summary_Field_Name_Geri_Field20 = "GeriField20";
        public const string Summary_Field_Name_Geri_Field21 = "GeriField21";
        public const string Summary_Field_Name_Geri_Field22 = "GeriField22";
        public const string Summary_Field_Name_Geri_Field23 = "GeriField23";
        public const string Summary_Field_Name_Geri_Field24 = "GeriField24";
        public const string Summary_Field_Name_Geri_Field25 = "GeriField25";
        public const string Summary_Field_Name_Geri_Field26 = "GeriField26";
        public const string Summary_Field_Name_Geri_Field27 = "GeriField27";
        public const string Summary_Field_Name_Geri_Field28 = "GeriField28";
        public const string Summary_Field_Name_Geri_Field29 = "GeriField29";
        public const string Summary_Field_Name_Geri_Field30 = "GeriField30";
        public const string Summary_Field_Name_Geri_Field31 = "GeriField31";
        public const string Summary_Field_Name_Geri_Field32 = "GeriField32";
        public const string Summary_Field_Name_Geri_Field33 = "GeriField33";
        public const string Summary_Field_Name_Geri_Field34 = "GeriField34";
        public const string Summary_Field_Name_Geri_Field35 = "GeriField35";
        public const string Summary_Field_Name_Geri_Field36 = "GeriField36";
        public const string Summary_Field_Name_Geri_Field37 = "GeriField37";
        public const string Summary_Field_Name_Geri_Field38 = "GeriField38";
        public const string Summary_Field_Name_Geri_Field39 = "GeriField39";
        public const string Summary_Field_Name_Geri_Field40 = "GeriField40";
        public const string Summary_Field_Name_Geri_Field41 = "GeriField41";
        public const string Summary_Field_Name_Geri_Field42 = "GeriField42";
        public const string Summary_Field_Name_Geri_Field43 = "GeriField43";
        public const string Summary_Field_Name_Geri_Field44 = "GeriField44";
        public const string Summary_Field_Name_Geri_Field45 = "GeriField45";
        public const string Summary_Field_Name_Geri_Field46 = "GeriField46";
        public const string Summary_Field_Name_Geri_Field47 = "GeriField47";
        public const string Summary_Field_Name_Geri_Field48 = "GeriField48";
        public const string Summary_Field_Name_Geri_Field49 = "GeriField49";
        public const string Summary_Field_Name_Geri_Field50 = "GeriField50";
        public const string Summary_Field_Name_Geri_Field51 = "GeriField51";
        public const string Summary_Field_Name_Geri_Field52 = "GeriField52";
        public const string Summary_Field_Name_Geri_Field53 = "GeriField53";
        public const string Summary_Field_Name_Geri_Field54 = "GeriField54";
        public const string Summary_Field_Name_Geri_Field55 = "GeriField55";
        public const string Summary_Field_Name_Geri_Field56 = "GeriField56";
        public const string Summary_Field_Name_Geri_Field57 = "GeriField57";
        public const string Summary_Field_Name_Geri_Field58 = "GeriField58";
        public const string Summary_Field_Name_Geri_Field59 = "GeriField59";
        public const string Summary_Field_Name_Geri_Field60 = "GeriField60";
        public const string Summary_Field_Name_Geri_Field61 = "GeriField61";
        public const string Summary_Field_Name_Geri_Field62 = "GeriField62";
        public const string Summary_Field_Name_Geri_Field63 = "GeriField63";
        public const string Summary_Field_Name_Geri_Field64 = "GeriField64";
        public const string Summary_Field_Name_Geri_Field65 = "GeriField65";
        public const string Summary_Field_Name_Geri_Field66 = "GeriField66";
        public const string Summary_Field_Name_Geri_Field67 = "GeriField67";
        public const string Summary_Field_Name_Geri_Field68 = "GeriField68";
        public const string Summary_Field_Name_Geri_Field69 = "GeriField69";
        public const string Summary_Field_Name_Geri_Field70 = "GeriField70";
        public const string Summary_Field_Name_Geri_Field71 = "GeriField71";
        public const string Summary_Field_Name_Geri_Field72 = "GeriField72";
        public const string Summary_Field_Name_Geri_Field73 = "GeriField73";
        public const string Summary_Field_Name_Geri_Field74 = "GeriField74";
        public const string Summary_Field_Name_Geri_Field75 = "GeriField75";
        public const string Summary_Field_Name_Geri_Field76 = "GeriField76";
        public const string Summary_Field_Name_Geri_Field77 = "GeriField77";
        public const string Summary_Field_Name_Geri_Field78 = "GeriField78";
        public const string Summary_Field_Name_Geri_Field79 = "GeriField79";
        public const string Summary_Field_Name_Geri_Field80 = "GeriField80";
        public const string Summary_Field_Name_Geri_Field81 = "GeriField81";
        public const string Summary_Field_Name_Geri_Field82 = "GeriField82";
        public const string Summary_Field_Name_Geri_Field83 = "GeriField83";
        public const string Summary_Field_Name_Geri_Field84 = "GeriField84";
        public const string Summary_Field_Name_Geri_Field85 = "GeriField85";
        public const string Summary_Field_Name_Geri_Field86 = "GeriField86";
        public const string Summary_Field_Name_Geri_Field87 = "GeriField87";
        public const string Summary_Field_Name_Geri_Field88 = "GeriField88";
        public const string Summary_Field_Name_Geri_Field89 = "GeriField89";
        public const string Summary_Field_Name_Geri_Field90 = "GeriField90";
        public const string Summary_Field_Name_Geri_Field91 = "GeriField91";
        public const string Summary_Field_Name_Geri_Field92 = "GeriField92";
        public const string Summary_Field_Name_Geri_Field93 = "GeriField93";
        public const string Summary_Field_Name_Geri_Field94 = "GeriField94";
        public const string Summary_Field_Name_Geri_Field95 = "GeriField95";
        public const string Summary_Field_Name_Geri_Field96 = "GeriField96";
        public const string Summary_Field_Name_Geri_Field97 = "GeriField97";
        public const string Summary_Field_Name_Geri_Field98 = "GeriField98";
        public const string Summary_Field_Name_Geri_Field99 = "GeriField99";
        public const string Summary_Field_Name_Geri_Field100 = "GeriField100";
        public const string Summary_Field_Name_Geri_Field101 = "GeriField101";
        public const string Summary_Field_Name_Geri_Field102 = "GeriField102";
        public const string Summary_Field_Name_Geri_Field103 = "GeriField103";
        public const string Summary_Field_Name_Geri_Field104 = "GeriField104";
        public const string Summary_Field_Name_Geri_Field105 = "GeriField105";
        public const string Summary_Field_Name_Geri_Field106 = "GeriField106";
        public const string Summary_Field_Name_Geri_Field107 = "GeriField107";
        public const string Summary_Field_Name_Geri_Field108 = "GeriField108";
        public const string Summary_Field_Name_Geri_Field109 = "GeriField109";
        public const string Summary_Field_Name_Geri_Field110 = "GeriField110";
        public const string Summary_Field_Name_Geri_Field111 = "GeriField111";
        public const string Summary_Field_Name_Geri_Field112 = "GeriField112";
        public const string Summary_Field_Name_Geri_Field113 = "GeriField113";
        public const string Summary_Field_Name_Geri_Field114 = "GeriField114";
        public const string Summary_Field_Name_Geri_Field115 = "GeriField115";
        public const string Summary_Field_Name_Geri_Field116 = "GeriField116";
        public const string Summary_Field_Name_Geri_Field117 = "GeriField117";
        public const string Summary_Field_Name_Geri_Field118 = "GeriField118";
        public const string Summary_Field_Name_Geri_Field119 = "GeriField119";
        public const string Summary_Field_Name_Geri_Field120 = "GeriField120";
        public const string Summary_Field_Name_Geri_Field121 = "GeriField121";
        public const string Summary_Field_Name_Geri_Field122 = "GeriField122";
        public const string Summary_Field_Name_Geri_Field123 = "GeriField123";
        public const string Summary_Field_Name_Geri_Field124 = "GeriField124";
        public const string Summary_Field_Name_Geri_Field125 = "GeriField125";
        public const string Summary_Field_Name_Geri_Field126 = "GeriField126";
        public const string Summary_Field_Name_Geri_Field127 = "GeriField127";
        public const string Summary_Field_Name_Geri_Field128 = "GeriField128";
        public const string Summary_Field_Name_Geri_Field129 = "GeriField129";
        public const string Summary_Field_Name_Geri_Field130 = "GeriField130";
        public const string Summary_Field_Name_Geri_Field131 = "GeriField131";
        public const string Summary_Field_Name_Geri_Field132 = "GeriField132";
        public const string Summary_Field_Name_Geri_Field133 = "GeriField133";
        public const string Summary_Field_Name_Geri_Field134 = "GeriField134";
        public const string Summary_Field_Name_Geri_Field135 = "GeriField135";
        public const string Summary_Field_Name_Geri_Field136 = "GeriField136";
        public const string Summary_Field_Name_Geri_Field137 = "GeriField137";
        public const string Summary_Field_Name_Geri_Field138 = "GeriField138";
        public const string Summary_Field_Name_Geri_Field139 = "GeriField139";
        public const string Summary_Field_Name_Geri_Field140 = "GeriField140";
        public const string Summary_Field_Name_Geri_Field141 = "GeriField141";
        public const string Summary_Field_Name_Geri_Field142 = "GeriField142";
        public const string Summary_Field_Name_Geri_Field143 = "GeriField143";
        public const string Summary_Field_Name_Geri_Field144 = "GeriField144";
        public const string Summary_Field_Name_Geri_Field145 = "GeriField145";
        public const string Summary_Field_Name_Geri_Field146 = "GeriField146";
        public const string Summary_Field_Name_Geri_Field147 = "GeriField147";
        public const string Summary_Field_Name_Geri_Field148 = "GeriField148";
        public const string Summary_Field_Name_Geri_Field149 = "GeriField149";

        //Social Support
        public const string Summary_Field_Name_SocialSupport_Field0 = "SocialSupportField0";
        public const string Summary_Field_Name_SocialSupport_Field1 = "SocialSupportField1";
        public const string Summary_Field_Name_SocialSupport_Field2 = "SocialSupportField2";
        public const string Summary_Field_Name_SocialSupport_Field3 = "SocialSupportField3";




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

        ////Doctor Summary Category
        //public const string Summary_Category_Label_Name_MedicalHistory = "Medical History";
        //public const string Summary_Category_Label_Name_SmokingAndAlcoholUse = "Smoking and Alcohol Use";
        //public const string Summary_Category_Label_Name_ReferredFrom = "Referred From";

        ////Doctor Consult Referral 
        //public const string Summary_Category_Label_Name_DoctorConsult = "Doctor Consult";
        //public const string Summary_Category_Label_Name_DoctorConsult_HxTaking = "Doctor Consult - History Taking";
        //public const string Summary_Category_Label_Name_DoctorConsult_Geri_Vision = "Doctor Consult - Geriatrics - Vision";

        //Doctor Summary Category
        public const string Summary_Category_Label_Name_DoctorConsult = "Doctor Consult";
        public const string Summary_Category_Label_Name_DoctorConsult_ReasonForReferral = "Reason for Referral";
        public const string Summary_Category_Label_Name_DoctorConsult_HealthConcerns = "Health Concerns";
        public const string Summary_Category_Label_Name_DoctorConsult_ReferralSummary = "Referral Summary";
        public const string Summary_Category_Label_Name_DoctorConsult_BloodPressure = "Blood Pressure";
        public const string Summary_Category_Label_Name_DoctorConsult_BMI = "BMI";
        public const string Summary_Category_Label_Name_DoctorConsult_Phleblotomy = "Phleblotomy";
        public const string Summary_Category_Label_Name_DoctorConsult_MedHistory = "Medical History";
        public const string Summary_Category_Label_Name_DoctorConsult_SocHistory = "Social History";
        public const string Summary_Category_Label_Name_DoctorConsult_UrinaryIncon = "Urinary Incontinence";
        public const string Summary_Category_Label_Name_DoctorConsult_MentalHealth = "Mental Health";
        public const string Summary_Category_Label_Name_DoctorConsult_AMT = "AMT";
        public const string Summary_Category_Label_Name_DoctorConsult_PTMemo = "Physical Therapist Memo";
        public const string Summary_Category_Label_Name_DoctorConsult_OTMemo = "Occupational Therapist Memo";
        public const string Summary_Category_Label_Name_DoctorConsult_Vision = "Vision"; 

        //Social Support Referral
        public const string Summary_Category_Label_Name_SocialSupport = "Social Support";
        public const string Summary_Category_Label_Name_SocialSupport_HxTaking = "Social Support - History Taking";

        //Cognitive Second Tier
        public const string Summary_Category_Label_Name_Cog2nd = "Cognitive 2nd Tier";
        public const string Summary_Category_Label_Name_Cog2nd_AMT = "Cognitive 2nd Tier - AMT";
        public const string Summary_Category_Label_Name_Cog2nd_EBAS = "Cognitive 2nd Tier - EBAS";

        //PT Consult
        public const string Summary_Category_Label_Name_PTConsult = "Reasons for referral to Dr's Consult from PT";
        public const string Summary_Category_Label_Name_PTCon_ParQ_Result = "PAR-Q Results";
        public const string Summary_Category_Label_Name_PTCon_PhysAct_Result = "Physical Activity Levels Results";
        public const string Summary_Category_Label_Name_PTCon_Frail_Result = "Frail Scale";
        public const string Summary_Category_Label_Name_PTCon_SPPB_Result = "SPPB scores";
        public const string Summary_Category_Label_Name_PTCon_TUG_Result = "Time-up and go Results";

        //OT Consult
        public const string Summary_Category_Label_Name_OTConsult = "Reasons for referral to Dr's Consult from OT";
        public const string Summary_Category_Label_Name_OTConsult_Vision = "Vision - Snellen's Test Results";
        public const string Summary_Category_Label_Name_OTConsult_Questionnaire = "OT Questionnaire Results"; 
        public const string Summary_Category_Label_Name_OTConsult_SPPB_Result = "SPPB Scores";
        public const string Summary_Category_Label_Name_OTConsult_TUG_Result = "Time-up and go Results";

        //Exhibition
        public const string Summary_Category_Label_Name_Exhibition_Cardio = "Cardiovascular Health";
        public const string Summary_Category_Label_Name_Exhibition_Obesity = "Obesity, Metabolic Syndrome & Diabetes";
        public const string Summary_Category_Label_Name_Exhibition_Gastrio = "Gastrointestinal Health";
        public const string Summary_Category_Label_Name_Exhibition_Lifestyle = "Lifestyle Choices";
        public const string Summary_Category_Label_Name_Exhibition_Geri = "Geriatrics and Mental Health";
        public const string Summary_Category_Label_Name_Exhibition_Renal = "Renal Health";
        public const string Summary_Category_Label_Name_Exhibition_Cancer = "Cancers";
        public const string Summary_Category_Label_Name_Exhibition_SocialSupp = "Social Support";

        //Social Support 
        public const string Summary_Category_Label_Name_SocialSupp_CHAS = "CHAS Card Status"; 


       



        public const string Summary_Type_Event = "ESY";
        public const string Summary_Type_Doctor = "DSY";
        public const string Summary_Type_All = "SUM";
        public const string Summary_Type_PT = "SUM_PT";

        public const string Summary_Type_Cog2 = "COG2";
        public const string Summary_Type_PTSummary = "PTSUM";
        public const string Summary_Type_OTSummary = "OTSUM";
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
