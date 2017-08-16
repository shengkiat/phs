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

        static Dictionary<string, List<string>> DoctorSummaryLabelMap = new Dictionary<string, List<string>> {
            //TODO replace with doctor Summary category and Field
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

        public static bool IsFieldNameAndCategoryFoundInEventSummaryMap(String summaryCategory, String fieldName) {
            bool Result = false;
            if(EventSummaryLabelMap[summaryCategory] != null)
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
            if (DoctorSummaryLabelMap[summaryCategory] != null)
            {
                List<string> SummaryFieldNameList = DoctorSummaryLabelMap[summaryCategory];
                if (SummaryFieldNameList.Contains(fieldName)){
                    Result = true;
                }
            }

            return Result;
        }

        public static List<string> GetEventSummaryCategoryNameList()
        {
            return EventSummaryCategoryNameList;
        }
    }
}
