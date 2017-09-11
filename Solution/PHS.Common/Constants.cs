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

        //Social Support Referral
        public const string Summary_Category_Label_Name_SocialSupport = "Social Support";
        public const string Summary_Category_Label_Name_SocialSupport_HxTaking = "Social Support - History Taking";

        //Cognitive Second Tier
        public const string Summary_Category_Label_Name_Cog2nd = "Cognitive 2nd Tier";
        public const string Summary_Category_Label_Name_Cog2nd_AMT = "Cognitive 2nd Tier - AMT";
        public const string Summary_Category_Label_Name_Cog2nd_EBAS = "Cognitive 2nd Tier - EBAS";

        //PT Consult
        public const string Summary_Category_Label_Name_PTConsult = "PT Consult";
        public const string Summary_Category_Label_Name_PTCon_ParQ_Result = "PAR-Q Results";
        public const string Summary_Category_Label_Name_PTCon_PhysAct_Result = "Physical Activity Levels Results";
        public const string Summary_Category_Label_Name_PTCon_Frail_Result = "Frail Scale";
        public const string Summary_Category_Label_Name_PTCon_SPPB_Result = "SPPB scores";
        public const string Summary_Category_Label_Name_PTCon_TUG_Result = "Time-up and go Results";

        //OT Consult
        public const string Summary_Category_Label_Name_OTConsult = "OT Consult";
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
