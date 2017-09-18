using PHS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.Helpers
{
    class SummaryHelper
    {
        static List<string> EventSummaryCategoryNameList = new List<string>()
        {
            Constants.Summary_Category_Label_Name_CardiovascularHealth,
            Constants.Summary_Category_Label_Name_Obesity,
            Constants.Summary_Category_Label_Name_LifestyleChoices,
            Constants.Summary_Category_Label_Name_Cancers
        };

        public static Dictionary<string, List<string>> EventSummaryLabelMap = new Dictionary<string, List<string>> {
            { Constants.Summary_Category_Label_Name_CardiovascularHealth, new List<string> {
                Constants.Summary_Field_Name_CurrentlySmoke,
                Constants.Summary_Field_Name_FamilyHistory,
                Constants.Summary_Field_Name_PastMedicalHistory
            }},
            { Constants.Summary_Category_Label_Name_Obesity, new List<string> {
                Constants.Summary_Field_Name_PastMedicalHistory,
                Constants.Summary_Field_Name_FamilyHistory
            }},
            { Constants.Summary_Category_Label_Name_LifestyleChoices, new List<string> {
                Constants.Summary_Field_Name_CurrentlySmoke
            }},
            { Constants.Summary_Category_Label_Name_Cancers, new List<string> {
                Constants.Summary_Field_Name_PersonalCancerHistory,
                Constants.Summary_Field_Name_FamilyHistory
            }}
        };


        static List<string> DoctorSummaryCategoryNameList = new List<string>()
        {
            Constants.Summary_Category_Label_Name_DoctorConsult_ReasonForReferral,
            Constants.Summary_Category_Label_Name_DoctorConsult_HealthConcerns,
            Constants.Summary_Category_Label_Name_DoctorConsult_ReferralSummary,
            Constants.Summary_Category_Label_Name_DoctorConsult_BloodPressure,
            Constants.Summary_Category_Label_Name_DoctorConsult_BMI,
            Constants.Summary_Category_Label_Name_DoctorConsult_Phleblotomy,
            Constants.Summary_Category_Label_Name_DoctorConsult_MedHistory,
            Constants.Summary_Category_Label_Name_DoctorConsult_SocHistory,
            Constants.Summary_Category_Label_Name_DoctorConsult_UrinaryIncon,
            Constants.Summary_Category_Label_Name_DoctorConsult_MentalHealth,
            Constants.Summary_Category_Label_Name_DoctorConsult_AMT,
            Constants.Summary_Category_Label_Name_DoctorConsult_PTMemo, 
            Constants.Summary_Category_Label_Name_DoctorConsult_OTMemo

        };

        static List<string> PTSummaryCategoryNameList = new List<string>()
        {
            Constants.Summary_Category_Label_Name_PTConsult,
            Constants.Summary_Category_Label_Name_PTCon_ParQ_Result,
            Constants.Summary_Category_Label_Name_PTCon_PhysAct_Result,
            Constants.Summary_Category_Label_Name_PTCon_Frail_Result,
            Constants.Summary_Category_Label_Name_PTCon_SPPB_Result,
            Constants.Summary_Category_Label_Name_PTCon_TUG_Result
        };

        static List<string> OTSummaryCategoryNameList = new List<string>()
        {
            Constants.Summary_Category_Label_Name_OTConsult_Questionnaire,
            Constants.Summary_Category_Label_Name_OTConsult_SPPB_Result,
            Constants.Summary_Category_Label_Name_OTConsult_TUG_Result
        };

        static List<string> Cog2SummaryCategoryNameList = new List<string>()
        {
            Constants.Summary_Category_Label_Name_Cog2nd_AMT,
            Constants.Summary_Category_Label_Name_Cog2nd_EBAS
        };

        static List<string> ExhibitionSummaryCategoryNameList = new List<string>()
        {
            Constants.Summary_Category_Label_Name_Exhibition_Cardio,
            Constants.Summary_Category_Label_Name_Exhibition_Obesity,
            Constants.Summary_Category_Label_Name_Exhibition_Gastrio,
            Constants.Summary_Category_Label_Name_Exhibition_Lifestyle,
            Constants.Summary_Category_Label_Name_Exhibition_Geri,
            Constants.Summary_Category_Label_Name_Exhibition_Renal,
            Constants.Summary_Category_Label_Name_Exhibition_Cancer,
            Constants.Summary_Category_Label_Name_Exhibition_SocialSupp
        };

        static List<string> SocialSuppSummaryCategoryNameList = new List<string>()
        {
            Constants.Summary_Category_Label_Name_SocialSupp_CHAS
        };

        public static Dictionary<string, List<string>> Cog2SummaryLabelMap = new Dictionary<string, List<string>> {
            { Constants.Summary_Category_Label_Name_Cog2nd_AMT, new List<string> {
                Constants.Summary_Field_Name_Cog2nd_AMT_Ref,
                Constants.Summary_Field_Name_Cog2nd_AMT_TotalScore,
                Constants.Summary_Field_Name_Cog2nd_AMT_Result1,
                Constants.Summary_Field_Name_Cog2nd_AMT_Result2,
                Constants.Summary_Field_Name_Cog2nd_AMT_Result3,
                Constants.Summary_Field_Name_Cog2nd_AMT_Result4,
                Constants.Summary_Field_Name_Cog2nd_AMT_Result5,
                Constants.Summary_Field_Name_Cog2nd_AMT_Result6,
                Constants.Summary_Field_Name_Cog2nd_AMT_Result7,
                Constants.Summary_Field_Name_Cog2nd_AMT_Result8,
                Constants.Summary_Field_Name_Cog2nd_AMT_Result9,
                Constants.Summary_Field_Name_Cog2nd_AMT_Result10,
                Constants.Summary_Field_Name_Cog2nd_AMT_Result11,
                Constants.Summary_Field_Name_Cog2nd_AMT_Result12,
                Constants.Summary_Field_Name_Cog2nd_AMT_Result13,
                Constants.Summary_Field_Name_Cog2nd_AMT_Result14,
                Constants.Summary_Field_Name_Cog2nd_AMT_Result15,
                Constants.Summary_Field_Name_Cog2nd_AMT_Result16,
                Constants.Summary_Field_Name_Cog2nd_AMT_Result17,
                Constants.Summary_Field_Name_Cog2nd_AMT_Result18,
                Constants.Summary_Field_Name_Cog2nd_AMT_Result19,
                Constants.Summary_Field_Name_Cog2nd_AMT_Result20
            }},
            { Constants.Summary_Category_Label_Name_Cog2nd_EBAS, new List<string> {
                Constants.Summary_Field_Name_Cog2nd_EBAS_Ref,
                Constants.Summary_Field_Name_Cog2nd_EBAS_TotalScore,
                Constants.Summary_Field_Name_Cog2nd_EBAS_Result1,
                Constants.Summary_Field_Name_Cog2nd_EBAS_Result2,
                Constants.Summary_Field_Name_Cog2nd_EBAS_Result3,
                Constants.Summary_Field_Name_Cog2nd_EBAS_Result4,
                Constants.Summary_Field_Name_Cog2nd_EBAS_Result5,
                Constants.Summary_Field_Name_Cog2nd_EBAS_Result6,
                Constants.Summary_Field_Name_Cog2nd_EBAS_Result7,
                Constants.Summary_Field_Name_Cog2nd_EBAS_Result8,
                Constants.Summary_Field_Name_Cog2nd_EBAS_Result9,
                Constants.Summary_Field_Name_Cog2nd_EBAS_Result10,
                Constants.Summary_Field_Name_Cog2nd_EBAS_Result11,
                Constants.Summary_Field_Name_Cog2nd_EBAS_Result12,
                Constants.Summary_Field_Name_Cog2nd_EBAS_Result13,
                Constants.Summary_Field_Name_Cog2nd_EBAS_Result14,
                Constants.Summary_Field_Name_Cog2nd_EBAS_Result15,
                Constants.Summary_Field_Name_Cog2nd_EBAS_Result16,
                Constants.Summary_Field_Name_Cog2nd_EBAS_Result17,
                Constants.Summary_Field_Name_Cog2nd_EBAS_Result18,
                Constants.Summary_Field_Name_Cog2nd_EBAS_Result19,
                Constants.Summary_Field_Name_Cog2nd_EBAS_Result20

            }}
        };

        public static Dictionary<string, List<String>> PTConsultSummaryLabelMap = new Dictionary<string, List<string>> {
            { Constants.Summary_Category_Label_Name_PTCon_ParQ_Result, new List<string> {
                Constants.Summary_Field_Name_PTCon_ParQ_Result,
                Constants.Summary_Field_Name_PTCon_ParQ_Result1,
                Constants.Summary_Field_Name_PTCon_ParQ_Result2,
                Constants.Summary_Field_Name_PTCon_ParQ_Result3,
                Constants.Summary_Field_Name_PTCon_ParQ_Result4,
                Constants.Summary_Field_Name_PTCon_ParQ_Result5,
                Constants.Summary_Field_Name_PTCon_ParQ_Result6,
                Constants.Summary_Field_Name_PTCon_ParQ_Result7,
                Constants.Summary_Field_Name_PTCon_ParQ_Result8,
                Constants.Summary_Field_Name_PTCon_ParQ_Result9,
                Constants.Summary_Field_Name_PTCon_ParQ_Result10,
                Constants.Summary_Field_Name_PTCon_PhysAct_Result,
                Constants.Summary_Field_Name_PTCon_Frail_Result,
                Constants.Summary_Field_Name_PTCon_SPPB_Result,
                Constants.Summary_Field_Name_PTCon_TUG_Result
    }},
             { Constants.Summary_Category_Label_Name_PTCon_PhysAct_Result, new List<string> {
                Constants.Summary_Field_Name_PTCon_PhysAct_Result,
                Constants.Summary_Field_Name_PTCon_PhysAct_Result1,
                Constants.Summary_Field_Name_PTCon_PhysAct_Result2,
                Constants.Summary_Field_Name_PTCon_PhysAct_Result3,
                Constants.Summary_Field_Name_PTCon_PhysAct_Result4,
                Constants.Summary_Field_Name_PTCon_PhysAct_Result5,
                Constants.Summary_Field_Name_PTCon_PhysAct_Result6,
                Constants.Summary_Field_Name_PTCon_PhysAct_Result7,
                Constants.Summary_Field_Name_PTCon_PhysAct_Result8,
                Constants.Summary_Field_Name_PTCon_PhysAct_Result9,
                Constants.Summary_Field_Name_PTCon_PhysAct_Result10,
                Constants.Summary_Field_Name_PTCon_PhysAct_Result,
                Constants.Summary_Field_Name_PTCon_Frail_Result,
                Constants.Summary_Field_Name_PTCon_SPPB_Result,
                Constants.Summary_Field_Name_PTCon_TUG_Result
    }},
             { Constants.Summary_Category_Label_Name_PTCon_Frail_Result, new List<string> {
                Constants.Summary_Field_Name_PTCon_Frail_Result,
                Constants.Summary_Field_Name_PTCon_Frail_Result1,
                Constants.Summary_Field_Name_PTCon_Frail_Result2,
                Constants.Summary_Field_Name_PTCon_Frail_Result3,
                Constants.Summary_Field_Name_PTCon_Frail_Result4,
                Constants.Summary_Field_Name_PTCon_Frail_Result5,
                Constants.Summary_Field_Name_PTCon_Frail_Result6,
                Constants.Summary_Field_Name_PTCon_Frail_Result7,
                Constants.Summary_Field_Name_PTCon_Frail_Result8,
                Constants.Summary_Field_Name_PTCon_Frail_Result9,
                Constants.Summary_Field_Name_PTCon_Frail_Result10
    }},
             { Constants.Summary_Category_Label_Name_PTCon_SPPB_Result, new List<string> {
                Constants.Summary_Field_Name_PTCon_SPPB_Result,
                Constants.Summary_Field_Name_PTCon_SPPB_Result1,
                Constants.Summary_Field_Name_PTCon_SPPB_Result2,
                Constants.Summary_Field_Name_PTCon_SPPB_Result3,
                Constants.Summary_Field_Name_PTCon_SPPB_Result4,
                Constants.Summary_Field_Name_PTCon_SPPB_Result5,
                Constants.Summary_Field_Name_PTCon_SPPB_Result6,
                Constants.Summary_Field_Name_PTCon_SPPB_Result7,
                Constants.Summary_Field_Name_PTCon_SPPB_Result8,
                Constants.Summary_Field_Name_PTCon_SPPB_Result9,
                Constants.Summary_Field_Name_PTCon_SPPB_Result10
    }},
             { Constants.Summary_Category_Label_Name_PTCon_TUG_Result, new List<string> {
                Constants.Summary_Field_Name_PTCon_TUG_Result,
                Constants.Summary_Field_Name_PTCon_TUG_Result1,
                Constants.Summary_Field_Name_PTCon_TUG_Result2,
                Constants.Summary_Field_Name_PTCon_TUG_Result3,
                Constants.Summary_Field_Name_PTCon_TUG_Result4,
                Constants.Summary_Field_Name_PTCon_TUG_Result5,
                Constants.Summary_Field_Name_PTCon_TUG_Result6,
                Constants.Summary_Field_Name_PTCon_TUG_Result7,
                Constants.Summary_Field_Name_PTCon_TUG_Result8,
                Constants.Summary_Field_Name_PTCon_TUG_Result9,
                Constants.Summary_Field_Name_PTCon_TUG_Result10
    }}

        };

        public static Dictionary<string, List<String>> OTConsultSummaryLabelMap = new Dictionary<string, List<string>> {
            { Constants.Summary_Category_Label_Name_OTConsult_Questionnaire, new List<string> {
                Constants.Summary_Field_Name_OTCon_Questionnaire_Result1,
                Constants.Summary_Field_Name_OTCon_Questionnaire_Result2,
                Constants.Summary_Field_Name_OTCon_Questionnaire_Result3,
                Constants.Summary_Field_Name_OTCon_Questionnaire_Result4,
                Constants.Summary_Field_Name_OTCon_Questionnaire_Result5,
                Constants.Summary_Field_Name_OTCon_Questionnaire_Result6,
                Constants.Summary_Field_Name_OTCon_Questionnaire_Result7,
                Constants.Summary_Field_Name_OTCon_Questionnaire_Result8,
                Constants.Summary_Field_Name_OTCon_Questionnaire_Result9,
                Constants.Summary_Field_Name_OTCon_Questionnaire_Result10
    }},
            
             { Constants.Summary_Category_Label_Name_PTCon_SPPB_Result, new List<string> {
                Constants.Summary_Field_Name_PTCon_SPPB_Result,
                Constants.Summary_Field_Name_PTCon_SPPB_Result1,
                Constants.Summary_Field_Name_PTCon_SPPB_Result2,
                Constants.Summary_Field_Name_PTCon_SPPB_Result3,
                Constants.Summary_Field_Name_PTCon_SPPB_Result4,
                Constants.Summary_Field_Name_PTCon_SPPB_Result5,
                Constants.Summary_Field_Name_PTCon_SPPB_Result6,
                Constants.Summary_Field_Name_PTCon_SPPB_Result7,
                Constants.Summary_Field_Name_PTCon_SPPB_Result8,
                Constants.Summary_Field_Name_PTCon_SPPB_Result9,
                Constants.Summary_Field_Name_PTCon_SPPB_Result10
    }},
             { Constants.Summary_Category_Label_Name_PTCon_TUG_Result, new List<string> {
                Constants.Summary_Field_Name_PTCon_TUG_Result,
                Constants.Summary_Field_Name_PTCon_TUG_Result1,
                Constants.Summary_Field_Name_PTCon_TUG_Result2,
                Constants.Summary_Field_Name_PTCon_TUG_Result3,
                Constants.Summary_Field_Name_PTCon_TUG_Result4,
                Constants.Summary_Field_Name_PTCon_TUG_Result5,
                Constants.Summary_Field_Name_PTCon_TUG_Result6,
                Constants.Summary_Field_Name_PTCon_TUG_Result7,
                Constants.Summary_Field_Name_PTCon_TUG_Result8,
                Constants.Summary_Field_Name_PTCon_TUG_Result9,
                Constants.Summary_Field_Name_PTCon_TUG_Result10
    }}

        };

        public static Dictionary<string, List<String>> ExhibitionSummaryLabelMap = new Dictionary<string, List<string>> {
            { Constants.Summary_Category_Label_Name_Exhibition_Cardio, new List<string> {
                Constants.Summary_Field_Name_Exhibition_Result1,
                Constants.Summary_Field_Name_Exhibition_Result2,
                Constants.Summary_Field_Name_Exhibition_Result3,
                Constants.Summary_Field_Name_Exhibition_Result4,
                Constants.Summary_Field_Name_Exhibition_Result5
    }},

             { Constants.Summary_Category_Label_Name_Exhibition_Obesity, new List<string> {
                Constants.Summary_Field_Name_Exhibition_Result6,
                Constants.Summary_Field_Name_Exhibition_Result7,
                Constants.Summary_Field_Name_Exhibition_Result8,
                Constants.Summary_Field_Name_Exhibition_Result9,
                Constants.Summary_Field_Name_Exhibition_Result10,
    }},
             { Constants.Summary_Category_Label_Name_Exhibition_Gastrio, new List<string> {

    }},
            { Constants.Summary_Category_Label_Name_Exhibition_Lifestyle, new List<string> {
                Constants.Summary_Field_Name_Exhibition_Result11,
                Constants.Summary_Field_Name_Exhibition_Result12,
                Constants.Summary_Field_Name_Exhibition_Result13,
                Constants.Summary_Field_Name_Exhibition_Result14,
                Constants.Summary_Field_Name_Exhibition_Result15,
    }},
            { Constants.Summary_Category_Label_Name_Exhibition_Geri, new List<string> {
                Constants.Summary_Field_Name_Exhibition_Result16,
                Constants.Summary_Field_Name_Exhibition_Result17,
                Constants.Summary_Field_Name_Exhibition_Result18,
                Constants.Summary_Field_Name_Exhibition_Result19,
                Constants.Summary_Field_Name_Exhibition_Result20
    }},
            { Constants.Summary_Category_Label_Name_Exhibition_Renal, new List<string> {
                Constants.Summary_Field_Name_Exhibition_Result21,
                Constants.Summary_Field_Name_Exhibition_Result22,
                Constants.Summary_Field_Name_Exhibition_Result23
    }},
            { Constants.Summary_Category_Label_Name_Exhibition_Cancer, new List<string> {
                Constants.Summary_Field_Name_Exhibition_Result24,
                Constants.Summary_Field_Name_Exhibition_Result25,
                Constants.Summary_Field_Name_Exhibition_Result26
    }},
            { Constants.Summary_Category_Label_Name_Exhibition_SocialSupp, new List<string> {
                Constants.Summary_Field_Name_Exhibition_Result27,
                Constants.Summary_Field_Name_Exhibition_Result28,
                Constants.Summary_Field_Name_Exhibition_Result29
    }},

        };

        public static Dictionary<string, List<String>> SocialSupportSummaryLabelMap = new Dictionary<string, List<string>> {
            { Constants.Summary_Category_Label_Name_SocialSupp_CHAS, new List<string> {
                Constants.Sumamry_Field_Name_SocialSupp_Result1
    }},          

        };

        public static Dictionary<string, List<string>> DoctorSummaryLabelMap = new Dictionary<string, List<string>> {
            { Constants.Summary_Category_Label_Name_DoctorConsult_ReasonForReferral, new List<string> {
                Constants.Summary_Field_Name_DocConsult_ReferredTo1,
                Constants.Summary_Field_Name_DocConsult_ReferredTo2,
                Constants.Summary_Field_Name_DocConsult_ReferredTo3,
                Constants.Summary_Field_Name_DocConsult_ReferredTo4,
                Constants.Summary_Field_Name_DocConsult_ReferredTo5,
                Constants.Summary_Field_Name_DocConsult_ReferredTo6,
                Constants.Summary_Field_Name_DocConsult_ReferredTo7,
                Constants.Summary_Field_Name_DocConsult_ReferredTo8,
                Constants.Summary_Field_Name_DocConsult_ReferredTo9,
                Constants.Summary_Field_Name_DocConsult_ReferredTo10,
                Constants.Summary_Field_Name_DocConsult_ReferredTo11,
                Constants.Summary_Field_Name_DocConsult_ReferredTo12,
                Constants.Summary_Field_Name_DocConsult_ReferredTo13,
                Constants.Summary_Field_Name_DocConsult_ReferredTo14,
                Constants.Summary_Field_Name_DocConsult_ReferredTo15,
                Constants.Summary_Field_Name_DocConsult_ReferredTo16,
                Constants.Summary_Field_Name_DocConsult_ReferredTo17,
                Constants.Summary_Field_Name_DocConsult_ReferredTo18,
                Constants.Summary_Field_Name_DocConsult_ReferredTo19,
                Constants.Summary_Field_Name_DocConsult_ReferredTo20,
                Constants.Summary_Field_Name_DocConsult_ReferredTo21,
                Constants.Summary_Field_Name_DocConsult_ReferredTo22,
                Constants.Summary_Field_Name_DocConsult_ReferredTo23,
                Constants.Summary_Field_Name_DocConsult_ReferredTo24,
                Constants.Summary_Field_Name_DocConsult_ReferredTo25,
                Constants.Summary_Field_Name_DocConsult_ReferredTo26,
                Constants.Summary_Field_Name_DocConsult_ReferredTo27,
                Constants.Summary_Field_Name_DocConsult_ReferredTo28,
                Constants.Summary_Field_Name_DocConsult_ReferredTo29,
                Constants.Summary_Field_Name_DocConsult_ReferredTo30,
                Constants.Summary_Field_Name_DocConsult_ReferredTo31,
                Constants.Summary_Field_Name_DocConsult_ReferredTo32,
                Constants.Summary_Field_Name_DocConsult_ReferredTo33,
                Constants.Summary_Field_Name_DocConsult_ReferredTo34,
                Constants.Summary_Field_Name_DocConsult_ReferredTo35,
                Constants.Summary_Field_Name_DocConsult_ReferredTo36,
                Constants.Summary_Field_Name_DocConsult_ReferredTo37,
                Constants.Summary_Field_Name_DocConsult_ReferredTo38,
                Constants.Summary_Field_Name_DocConsult_ReferredTo39,
                Constants.Summary_Field_Name_DocConsult_ReferredTo40,
                Constants.Summary_Field_Name_DocConsult_ReferredTo41,
                Constants.Summary_Field_Name_DocConsult_ReferredTo42,
                Constants.Summary_Field_Name_DocConsult_ReferredTo43,
                Constants.Summary_Field_Name_DocConsult_ReferredTo44,
                Constants.Summary_Field_Name_DocConsult_ReferredTo45,
                Constants.Summary_Field_Name_DocConsult_ReferredTo46,
                Constants.Summary_Field_Name_DocConsult_ReferredTo47,
                Constants.Summary_Field_Name_DocConsult_ReferredTo48,
                Constants.Summary_Field_Name_DocConsult_ReferredTo49,
                Constants.Summary_Field_Name_DocConsult_ReferredTo50
            }},            

            { Constants.Summary_Category_Label_Name_DoctorConsult_HealthConcerns, new List<string> {

                Constants.Summary_Field_Name_DocConsult_Result1,
                Constants.Summary_Field_Name_DocConsult_Result2,
                Constants.Summary_Field_Name_DocConsult_Result3,
                Constants.Summary_Field_Name_DocConsult_Result4,
                Constants.Summary_Field_Name_DocConsult_Result5
            }},
            { Constants.Summary_Category_Label_Name_DoctorConsult_ReferralSummary, new List<string> {
                Constants.Summary_Field_Name_DocConsult_Result6,
                Constants.Summary_Field_Name_DocConsult_Result7,
                Constants.Summary_Field_Name_DocConsult_Result8,
                Constants.Summary_Field_Name_DocConsult_Result9,
                Constants.Summary_Field_Name_DocConsult_Result10
            }},
            { Constants.Summary_Category_Label_Name_DoctorConsult_BloodPressure, new List<string> {
                Constants.Summary_Field_Name_DocConsult_Result11,
                Constants.Summary_Field_Name_DocConsult_Result12,
                Constants.Summary_Field_Name_DocConsult_Result13,
                Constants.Summary_Field_Name_DocConsult_Result14,
                Constants.Summary_Field_Name_DocConsult_Result15

    }},
            { Constants.Summary_Category_Label_Name_DoctorConsult_BMI, new List<string> {
                Constants.Summary_Field_Name_DocConsult_Result16,
                Constants.Summary_Field_Name_DocConsult_Result17,
                Constants.Summary_Field_Name_DocConsult_Result18,
                Constants.Summary_Field_Name_DocConsult_Result19,
                Constants.Summary_Field_Name_DocConsult_Result20
            }},
            { Constants.Summary_Category_Label_Name_DoctorConsult_Phleblotomy, new List<string> {
                Constants.Summary_Field_Name_DocConsult_Result21
            }},
            { Constants.Summary_Category_Label_Name_DoctorConsult_MedHistory, new List<string> {
                Constants.Summary_Field_Name_DocConsult_Result22,
                Constants.Summary_Field_Name_DocConsult_Result23,
                Constants.Summary_Field_Name_DocConsult_Result24,
                Constants.Summary_Field_Name_DocConsult_Result25,
                Constants.Summary_Field_Name_DocConsult_Result26,
                Constants.Summary_Field_Name_DocConsult_Result27,
                Constants.Summary_Field_Name_DocConsult_Result28,
                Constants.Summary_Field_Name_DocConsult_Result29,
                Constants.Summary_Field_Name_DocConsult_Result30

            }},
            { Constants.Summary_Category_Label_Name_DoctorConsult_SocHistory, new List<string> {
                Constants.Summary_Field_Name_DocConsult_Result31,
                Constants.Summary_Field_Name_DocConsult_Result32,
                Constants.Summary_Field_Name_DocConsult_Result33,
                Constants.Summary_Field_Name_DocConsult_Result34,
                Constants.Summary_Field_Name_DocConsult_Result35,
                Constants.Summary_Field_Name_DocConsult_Result36,
                Constants.Summary_Field_Name_DocConsult_Result37,
                Constants.Summary_Field_Name_DocConsult_Result38,
                Constants.Summary_Field_Name_DocConsult_Result39,
                Constants.Summary_Field_Name_DocConsult_Result40
            }},
            { Constants.Summary_Category_Label_Name_DoctorConsult_UrinaryIncon, new List<string> {
                Constants.Summary_Field_Name_DocConsult_Result41
            }},
            { Constants.Summary_Category_Label_Name_DoctorConsult_MentalHealth, new List<string> {
                
            }},
            { Constants.Summary_Category_Label_Name_DoctorConsult_AMT, new List<string> {
                Constants.Summary_Field_Name_DocConsult_Result42,
                Constants.Summary_Field_Name_DocConsult_Result43,
                Constants.Summary_Field_Name_DocConsult_Result44,
                Constants.Summary_Field_Name_DocConsult_Result45,
                Constants.Summary_Field_Name_DocConsult_Result46,
                Constants.Summary_Field_Name_DocConsult_Result47,
                Constants.Summary_Field_Name_DocConsult_Result48,
                Constants.Summary_Field_Name_DocConsult_Result49,
                Constants.Summary_Field_Name_DocConsult_Result50,
                Constants.Summary_Field_Name_DocConsult_Result51
            }},
            { Constants.Summary_Category_Label_Name_DoctorConsult_PTMemo, new List<string> {
                Constants.Summary_Field_Name_DocConsult_Result52,
                Constants.Summary_Field_Name_DocConsult_Result53,
                Constants.Summary_Field_Name_DocConsult_Result54,
                Constants.Summary_Field_Name_DocConsult_Result55,
                Constants.Summary_Field_Name_DocConsult_Result56,
            }},
            { Constants.Summary_Category_Label_Name_DoctorConsult_OTMemo, new List<string> {

                Constants.Summary_Field_Name_DocConsult_Result57,
                Constants.Summary_Field_Name_DocConsult_Result58,
                Constants.Summary_Field_Name_DocConsult_Result59,
                Constants.Summary_Field_Name_DocConsult_Result60,
                Constants.Summary_Field_Name_DocConsult_Result61
            }},
        };

        //public static bool IsFieldNameAndCategoryFoundInEventSummaryMap(String summaryCategory, String fieldName) {
        //    bool Result = false;
        //    if (EventSummaryLabelMap.ContainsKey(summaryCategory))
        //    {
        //        List<string> SummaryFieldNameList = EventSummaryLabelMap[summaryCategory];
        //        if (SummaryFieldNameList.Contains(fieldName)){
        //            Result = true;
        //        }
        //    }

        //    return Result;
        //}

        //public static bool IsFieldNameAndCategoryFoundInDoctorSummaryMap(String summaryCategory, String fieldName)
        //{
        //    bool Result = false;
        //    if (DoctorSummaryLabelMap.ContainsKey(summaryCategory))
        //    {
        //        List<string> SummaryFieldNameList = DoctorSummaryLabelMap[summaryCategory];
        //        if (SummaryFieldNameList.Contains(fieldName)){
        //            Result = true;
        //        }
        //    }

        //    if (PTConsultSummaryLabelMap.ContainsKey(summaryCategory))
        //    {
        //        List<string> SummaryFieldNameList = PTConsultSummaryLabelMap[summaryCategory];
        //        if (SummaryFieldNameList.Contains(fieldName))
        //        {
        //            Result = true;
        //        }
        //    }

        //    return Result;
        //}

        public static bool IsFieldNameAndCategoryFoundInSummaryMap(Dictionary<string, List<string>> summaryLabelMap, String summaryCategory, String fieldName)
        {
            bool Result = false;
            if (summaryLabelMap.ContainsKey(summaryCategory))
            {
                List<string> SummaryFieldNameList = summaryLabelMap[summaryCategory];
                if (SummaryFieldNameList.Contains(fieldName))
                {
                    Result = true;
                }
            }

            return Result;
        }

        public static List<string> GetEventSummaryCategoryNameList()
        {
            return EventSummaryCategoryNameList;
        }

        public static List<string> GetDoctorSummaryCategoryNameList()
        {
            return DoctorSummaryCategoryNameList;
        }

        public static List<String> GetPTSummaryCategoryNameList()
        {
            return PTSummaryCategoryNameList; 
        }

        public static List<String> GetOTSummaryCategoryNameList()
        {
            return OTSummaryCategoryNameList;
        }

        public static List<String> GetCog2SummaryCategoryNameList()
        {
            return Cog2SummaryCategoryNameList;
        }

        public static List<String> GetExhibitionSummaryCategoryNameList()
        {
            return ExhibitionSummaryCategoryNameList;
        }

        public static List<String> GetSocialSuppSummaryCategoryNameList()
        {
            return SocialSuppSummaryCategoryNameList;
        }
    }
}
