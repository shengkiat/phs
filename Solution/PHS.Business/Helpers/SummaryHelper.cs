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

        static Dictionary<string, List<string>> EventSummaryLabelMap = new Dictionary<string, List<string>> {
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
            Constants.Summary_Category_Label_Name_ReferredFrom,
            Constants.Summary_Category_Label_Name_ReferralReason,
            Constants.Summary_Category_Label_Name_ReferralSummary,
            Constants.Summary_Category_Label_Name_MedicalHistory,
            Constants.Summary_Category_Label_Name_SmokingAndAlcoholUse

        };

        static List<string> PTSummaryCategoryNameList = new List<string>()
        {
            Constants.Summary_Category_Label_Name_PTConsult
        }; 

        static List<string> Cog2ndCategoryNameList = new List<string>()
        {
            Constants.Summary_Category_Label_Name_Cog2nd_AMT,
            Constants.Summary_Category_Label_Name_Cog2nd_EBAS
        };

        static Dictionary<string, List<String>> PTConsultSummaryLabelMap = new Dictionary<string, List<string>>
        {{
            Constants.Summary_Category_Label_Name_PTConsult, new List<string>
            {
                Constants.Summary_Field_Name_PTCon_ParQ_Result,
                Constants.Summary_Field_Name_PTCon_PhysAct_Result,
                Constants.Summary_Field_Name_PTCon_Frail_Result,
                Constants.Summary_Field_Name_PTCon_SPPB_Result,
                Constants.Summary_Field_Name_PTCon_TUG_Result
    }
}

        };

        static Dictionary<string, List<string>> DoctorSummaryLabelMap = new Dictionary<string, List<string>> {
            { Constants.Summary_Category_Label_Name_ReferredFrom, new List<string> {

                Constants.Summary_Field_Name_PersonalCancerHistory,
                Constants.Summary_Field_Name_FamilyHistory,
                Constants.Summary_Field_Name_FamilyCancerHistory,
                Constants.Summary_Field_Name_Ref_FINHEALTH,
                Constants.Summary_Field_Name_Ref_Geri_Vision_Doc
            }},
            { Constants.Summary_Category_Label_Name_ReferralReason, new List<string> {

                Constants.Summary_Field_Name_Ref_HxTaking_HealthCon,
                Constants.Summary_Field_Name_Ref_HxTaking_SysReview,
                Constants.Summary_Field_Name_Ref_HxTaking_PastMed,
                Constants.Summary_Field_Name_Ref_FamHist,
                Constants.Summary_Field_Name_Ref_SocHist,
                Constants.Summary_Field_Name_Ref_FinStatus,
                Constants.Summary_Field_Name_Ref_UrinaryInc,
                Constants.Summary_Field_Name_Ref_Vitals
            }},
            { Constants.Summary_Category_Label_Name_ReferralSummary, new List<string> {
                Constants.Summary_Field_Name_RefSummary_DocCon,
                Constants.Summary_Field_Name_RefSummary_DocCon_MedIssueReason,
                Constants.Summary_Field_Name_RefSummary_DocCon_Reason,
                Constants.Summary_Field_Name_RefSummary_SocialSupport,
                Constants.Summary_Field_Name_RefSummary_SocialSupport_Reason
            }},
            { Constants.Summary_Category_Label_Name_MedicalHistory, new List<string> {
                Constants.Summary_Field_Name_PastMedicalHistory,
                Constants.Summary_Field_Name_PMHX,
                Constants.Summary_Field_Name_FamilyHistory_MedCond,
                Constants.Summary_Field_Name_FamilyHistory_Cancer

    }},
            { Constants.Summary_Category_Label_Name_SmokingAndAlcoholUse, new List<string> {
                Constants.Summary_Field_Name_CurrentlySmoke,
                Constants.Summary_Field_Name_NoOfPackYear,
                Constants.Summary_Field_Name_SmokeBefore
            }},
            { Constants.Summary_Category_Label_Name_Cog2nd_AMT, new List<string> {
                Constants.Summary_Field_Name_Cog2nd_AMT_Ref,
                Constants.Summary_Field_Name_Cog2nd_AMT_TotalScore
            }},
            { Constants.Summary_Category_Label_Name_Cog2nd_EBAS, new List<string> {
                Constants.Summary_Field_Name_Cog2nd_EBAS_Ref,
                Constants.Summary_Field_Name_Cog2nd_EBAS_TotalScore

            }}
        };

        public static bool IsFieldNameAndCategoryFoundInEventSummaryMap(String summaryCategory, String fieldName) {
            bool Result = false;
            if (EventSummaryLabelMap.ContainsKey(summaryCategory))
            {
                List<string> SummaryFieldNameList = EventSummaryLabelMap[summaryCategory];
                if (SummaryFieldNameList.Contains(fieldName)){
                    Result = true;
                }
            }

            return Result;
        }

        public static bool IsFieldNameAndCategoryFoundInDoctorSummaryMap(String summaryCategory, String fieldName)
        {
            bool Result = false;
            if (DoctorSummaryLabelMap.ContainsKey(summaryCategory))
            {
                List<string> SummaryFieldNameList = DoctorSummaryLabelMap[summaryCategory];
                if (SummaryFieldNameList.Contains(fieldName)){
                    Result = true;
                }
            }

            if (PTConsultSummaryLabelMap.ContainsKey(summaryCategory))
            {
                List<string> SummaryFieldNameList = PTConsultSummaryLabelMap[summaryCategory];
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

        public static List<String> GetPTSummaryCatgoryNameList()
        {
            return PTSummaryCategoryNameList; 
        }
    }
}
