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
            Constants.Summary_Category_Label_Name_DoctorConsult_BloodPressure,
            Constants.Summary_Category_Label_Name_DoctorConsult_BMI,
            Constants.Summary_Category_Label_Name_DoctorConsult_Phleblotomy,
            Constants.Summary_Category_Label_Name_DoctorConsult_MedHistory,
            Constants.Summary_Category_Label_Name_DoctorConsult_SocHistory,
            Constants.Summary_Category_Label_Name_DoctorConsult_UrinaryIncon,
            Constants.Summary_Category_Label_Name_DoctorConsult_MentalHealth,
            Constants.Summary_Category_Label_Name_DoctorConsult_AMT,
            Constants.Summary_Category_Label_Name_DoctorConsult_PTMemo,
            Constants.Summary_Category_Label_Name_DoctorConsult_OTMemo,
            Constants.Summary_Category_Label_Name_DoctorConsult_Vision
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
            Constants.Summary_Category_Label_Name_OTConsult,
            Constants.Summary_Category_Label_Name_OTConsult_Vision,
            Constants.Summary_Category_Label_Name_OTConsult_Questionnaire,
            Constants.Summary_Category_Label_Name_PTCon_SPPB_Result,
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
                Constants.Summary_Field_Name_Geri_Field0,
                Constants.Summary_Field_Name_Geri_Field1,
                Constants.Summary_Field_Name_Geri_Field2,
                Constants.Summary_Field_Name_Geri_Field3,
                Constants.Summary_Field_Name_Geri_Field4,
                Constants.Summary_Field_Name_Geri_Field5,
                Constants.Summary_Field_Name_Geri_Field6,
                Constants.Summary_Field_Name_Geri_Field7,
                Constants.Summary_Field_Name_Geri_Field8,
                Constants.Summary_Field_Name_Geri_Field9,
                Constants.Summary_Field_Name_Geri_Field10,
                Constants.Summary_Field_Name_Geri_Field11,
                Constants.Summary_Field_Name_Geri_Field12,
                Constants.Summary_Field_Name_Geri_Field13,
                Constants.Summary_Field_Name_Geri_Field14,
                Constants.Summary_Field_Name_Geri_Field15,
                Constants.Summary_Field_Name_Geri_Field16                
            }},
            { Constants.Summary_Category_Label_Name_Cog2nd_EBAS, new List<string> {
                Constants.Summary_Field_Name_Geri_Field17,
                Constants.Summary_Field_Name_Geri_Field18,
                Constants.Summary_Field_Name_Geri_Field19,
                Constants.Summary_Field_Name_Geri_Field20,
                Constants.Summary_Field_Name_Geri_Field21,
                Constants.Summary_Field_Name_Geri_Field22,
                Constants.Summary_Field_Name_Geri_Field23,
                Constants.Summary_Field_Name_Geri_Field24,
                Constants.Summary_Field_Name_Geri_Field25,
                Constants.Summary_Field_Name_Geri_Field26,
                Constants.Summary_Field_Name_Geri_Field27,
                Constants.Summary_Field_Name_Geri_Field28,
                Constants.Summary_Field_Name_Geri_Field29,
                Constants.Summary_Field_Name_Geri_Field30,
                Constants.Summary_Field_Name_Geri_Field31,
                Constants.Summary_Field_Name_Geri_Field32
            }}
        };

        public static Dictionary<string, List<String>> PTConsultSummaryLabelMap = new Dictionary<string, List<string>> {
            { Constants.Summary_Category_Label_Name_PTConsult, new List<string> {
                Constants.Summary_Field_Name_Geri_Field108,
                Constants.Summary_Field_Name_Geri_Field109,
                Constants.Summary_Field_Name_Geri_Field110,
                Constants.Summary_Field_Name_Geri_Field111
            }},
            { Constants.Summary_Category_Label_Name_PTCon_ParQ_Result, new List<string> {
                Constants.Summary_Field_Name_Geri_Field43,
                Constants.Summary_Field_Name_Geri_Field44,
                Constants.Summary_Field_Name_Geri_Field45,
                Constants.Summary_Field_Name_Geri_Field46,
                Constants.Summary_Field_Name_Geri_Field47,
                Constants.Summary_Field_Name_Geri_Field48,
                Constants.Summary_Field_Name_Geri_Field49,
                Constants.Summary_Field_Name_Geri_Field50,
                Constants.Summary_Field_Name_Geri_Field51
    }},
             { Constants.Summary_Category_Label_Name_PTCon_PhysAct_Result, new List<string> {
                Constants.Summary_Field_Name_Geri_Field52,
                Constants.Summary_Field_Name_Geri_Field53,
                Constants.Summary_Field_Name_Geri_Field54,
                Constants.Summary_Field_Name_Geri_Field55,
                Constants.Summary_Field_Name_Geri_Field56,
                Constants.Summary_Field_Name_Geri_Field57,
                Constants.Summary_Field_Name_Geri_Field58,
                Constants.Summary_Field_Name_Geri_Field59
    }},
             { Constants.Summary_Category_Label_Name_PTCon_Frail_Result, new List<string> {
                Constants.Summary_Field_Name_Geri_Field60,
                Constants.Summary_Field_Name_Geri_Field61,
                Constants.Summary_Field_Name_Geri_Field62,
                Constants.Summary_Field_Name_Geri_Field63,
                Constants.Summary_Field_Name_Geri_Field64,
                Constants.Summary_Field_Name_Geri_Field65,
                Constants.Summary_Field_Name_Geri_Field66,
                Constants.Summary_Field_Name_Geri_Field67,
                Constants.Summary_Field_Name_Geri_Field68,
                Constants.Summary_Field_Name_Geri_Field69,
                Constants.Summary_Field_Name_Geri_Field70
    }},
             { Constants.Summary_Category_Label_Name_PTCon_SPPB_Result, new List<string> {
                Constants.Summary_Field_Name_Geri_Field85,
                Constants.Summary_Field_Name_Geri_Field86,
                Constants.Summary_Field_Name_Geri_Field87,
                Constants.Summary_Field_Name_Geri_Field88,
                Constants.Summary_Field_Name_Geri_Field89,
                Constants.Summary_Field_Name_Geri_Field90,
                Constants.Summary_Field_Name_Geri_Field91,
                Constants.Summary_Field_Name_Geri_Field92,
                Constants.Summary_Field_Name_Geri_Field93,
                Constants.Summary_Field_Name_Geri_Field94,
                Constants.Summary_Field_Name_Geri_Field95,
                Constants.Summary_Field_Name_Geri_Field96,
                Constants.Summary_Field_Name_Geri_Field97,
                Constants.Summary_Field_Name_Geri_Field98,
                Constants.Summary_Field_Name_Geri_Field99,
                Constants.Summary_Field_Name_Geri_Field100
    }},
             { Constants.Summary_Category_Label_Name_PTCon_TUG_Result, new List<string> {
                Constants.Summary_Field_Name_Geri_Field101,
                Constants.Summary_Field_Name_Geri_Field102,
                Constants.Summary_Field_Name_Geri_Field103,
                Constants.Summary_Field_Name_Geri_Field104,
                Constants.Summary_Field_Name_Geri_Field105,
                Constants.Summary_Field_Name_Geri_Field106,
                Constants.Summary_Field_Name_Geri_Field107
    }}

        };

        public static Dictionary<string, List<String>> OTConsultSummaryLabelMap = new Dictionary<string, List<string>> {
            {Constants.Summary_Category_Label_Name_OTConsult, new List<string> {
                Constants.Summary_Field_Name_Geri_Field114,
                Constants.Summary_Field_Name_Geri_Field115,
                Constants.Summary_Field_Name_Geri_Field116,
                Constants.Summary_Field_Name_Geri_Field117
            }},
            { Constants.Summary_Category_Label_Name_OTConsult_Vision, new List<string> {
                Constants.Summary_Field_Name_Geri_Field33,
                Constants.Summary_Field_Name_Geri_Field34,
                Constants.Summary_Field_Name_Geri_Field35,
                Constants.Summary_Field_Name_Geri_Field36,
                Constants.Summary_Field_Name_Geri_Field37,
                Constants.Summary_Field_Name_Geri_Field38,
                Constants.Summary_Field_Name_Geri_Field39,
                Constants.Summary_Field_Name_Geri_Field40,
                Constants.Summary_Field_Name_Geri_Field41,
                Constants.Summary_Field_Name_Geri_Field42
            }},
            { Constants.Summary_Category_Label_Name_OTConsult_Questionnaire, new List<string> {
                Constants.Summary_Field_Name_Geri_Field71,
                Constants.Summary_Field_Name_Geri_Field72,
                Constants.Summary_Field_Name_Geri_Field73,
                Constants.Summary_Field_Name_Geri_Field74,
                Constants.Summary_Field_Name_Geri_Field75,
                Constants.Summary_Field_Name_Geri_Field76,
                Constants.Summary_Field_Name_Geri_Field77,
                Constants.Summary_Field_Name_Geri_Field78,
                Constants.Summary_Field_Name_Geri_Field79,
                Constants.Summary_Field_Name_Geri_Field80,
                Constants.Summary_Field_Name_Geri_Field81,
                Constants.Summary_Field_Name_Geri_Field82,
                Constants.Summary_Field_Name_Geri_Field83,
                Constants.Summary_Field_Name_Geri_Field84
    }},

             { Constants.Summary_Category_Label_Name_PTCon_SPPB_Result, new List<string> {
                Constants.Summary_Field_Name_Geri_Field85,
                Constants.Summary_Field_Name_Geri_Field86,
                Constants.Summary_Field_Name_Geri_Field87,
                Constants.Summary_Field_Name_Geri_Field88,
                Constants.Summary_Field_Name_Geri_Field89,
                Constants.Summary_Field_Name_Geri_Field90,
                Constants.Summary_Field_Name_Geri_Field91,
                Constants.Summary_Field_Name_Geri_Field92,
                Constants.Summary_Field_Name_Geri_Field93,
                Constants.Summary_Field_Name_Geri_Field94,
                Constants.Summary_Field_Name_Geri_Field95,
                Constants.Summary_Field_Name_Geri_Field96,
                Constants.Summary_Field_Name_Geri_Field97,
                Constants.Summary_Field_Name_Geri_Field98,
                Constants.Summary_Field_Name_Geri_Field99,
                Constants.Summary_Field_Name_Geri_Field100
    }},
             { Constants.Summary_Category_Label_Name_PTCon_TUG_Result, new List<string> {
                Constants.Summary_Field_Name_Geri_Field101,
                Constants.Summary_Field_Name_Geri_Field102,
                Constants.Summary_Field_Name_Geri_Field103,
                Constants.Summary_Field_Name_Geri_Field104,
                Constants.Summary_Field_Name_Geri_Field105,
                Constants.Summary_Field_Name_Geri_Field106,
                Constants.Summary_Field_Name_Geri_Field107
    }}

        };

        public static Dictionary<string, List<String>> ExhibitionSummaryLabelMap = new Dictionary<string, List<string>> {
            { Constants.Summary_Category_Label_Name_Exhibition_Cardio, new List<string> {
                Constants.Summary_Field_Name_HxTaking_Field0,
                Constants.Summary_Field_Name_HxTaking_Field1,
                Constants.Summary_Field_Name_HxTaking_Field2,
                Constants.Summary_Field_Name_HxTaking_Field3,
                Constants.Summary_Field_Name_HxTaking_Field4
    }},

             { Constants.Summary_Category_Label_Name_Exhibition_Obesity, new List<string> {
                Constants.Summary_Field_Name_HxTaking_Field5,
                Constants.Summary_Field_Name_HxTaking_Field3,
                Constants.Summary_Field_Name_HxTaking_Field4
    }},
             { Constants.Summary_Category_Label_Name_Exhibition_Gastrio, new List<string> {

    }},
            { Constants.Summary_Category_Label_Name_Exhibition_Lifestyle, new List<string> {
                Constants.Summary_Field_Name_HxTaking_Field2,
                Constants.Summary_Field_Name_HxTaking_Field6
    }},
            { Constants.Summary_Category_Label_Name_Exhibition_Geri, new List<string> {
                Constants.Summary_Field_Name_Geri_Field13,
                Constants.Summary_Field_Name_Geri_Field27,
                Constants.Summary_Field_Name_Geri_Field36,
                Constants.Summary_Field_Name_Geri_Field37,
                Constants.Summary_Field_Name_Reg_Field0
    }},
            { Constants.Summary_Category_Label_Name_Exhibition_Renal, new List<string> {
                Constants.Summary_Field_Name_HxTaking_Field7
    }},
            { Constants.Summary_Category_Label_Name_Exhibition_Cancer, new List<string> {
                Constants.Summary_Field_Name_HxTaking_Field8,
                Constants.Summary_Field_Name_HxTaking_Field9
    }},
            { Constants.Summary_Category_Label_Name_Exhibition_SocialSupp, new List<string> {
                Constants.Summary_Field_Name_HxTaking_Field10,
                Constants.Summary_Field_Name_SocialSupport_Field0,
                Constants.Summary_Field_Name_SocialSupport_Field1,
                Constants.Summary_Field_Name_SocialSupport_Field2,
                Constants.Summary_Field_Name_SocialSupport_Field3
    }},

        };

        public static Dictionary<string, List<String>> SocialSupportSummaryLabelMap = new Dictionary<string, List<string>> {
            { Constants.Summary_Category_Label_Name_SocialSupp_CHAS, new List<string> {
                Constants.Summary_Field_Name_Reg_Field1,
                Constants.Summary_Field_Name_Reg_Field2,
                Constants.Summary_Field_Name_Reg_Field3,
                Constants.Summary_Field_Name_Reg_Field4,
                Constants.Summary_Field_Name_Reg_Field5,
                Constants.Summary_Field_Name_Reg_Field6,
                Constants.Summary_Field_Name_Reg_Field7,
                Constants.Summary_Field_Name_HxTaking_Field11
    }},          

        };

        public static Dictionary<string, List<string>> DoctorSummaryLabelMap = new Dictionary<string, List<string>> {
            { Constants.Summary_Category_Label_Name_DoctorConsult_ReasonForReferral, new List<string> {
               Constants.Summary_Field_Name_HxTaking_Field6,
               Constants.Summary_Field_Name_HxTaking_Field7,
               Constants.Summary_Field_Name_HxTaking_Field8
            }},
            { Constants.Summary_Category_Label_Name_DoctorConsult_BloodPressure, new List<string> {
                Constants.Summary_Field_Name_HxTaking_Field0,
                Constants.Summary_Field_Name_HxTaking_Field1
            }},
            { Constants.Summary_Category_Label_Name_DoctorConsult_BMI, new List<string> {
                Constants.Summary_Field_Name_HxTaking_Field5
            }},
            { Constants.Summary_Category_Label_Name_DoctorConsult_Phleblotomy, new List<string> {
                Constants.Summary_Field_Name_Phlebo_Field0,
                Constants.Summary_Field_Name_Phlebo_Field1
            }},
            { Constants.Summary_Category_Label_Name_DoctorConsult_MedHistory, new List<string> {
                Constants.Summary_Field_Name_HxTaking_Field3,
                Constants.Summary_Field_Name_HxTaking_Field12,
                Constants.Summary_Field_Name_HxTaking_Field8,
                Constants.Summary_Field_Name_HxTaking_Field4,
                Constants.Summary_Field_Name_HxTaking_Field9,
                Constants.Summary_Field_Name_HxTaking_Field13
            }},
            { Constants.Summary_Category_Label_Name_DoctorConsult_SocHistory, new List<string> {
                Constants.Summary_Field_Name_HxTaking_Field2,
                Constants.Summary_Field_Name_HxTaking_Field16,
                Constants.Summary_Field_Name_HxTaking_Field17,
                Constants.Summary_Field_Name_HxTaking_Field14,
                Constants.Summary_Field_Name_HxTaking_Field15,
                Constants.Summary_Field_Name_HxTaking_Field6,
                Constants.Summary_Field_Name_HxTaking_Field18,
                Constants.Summary_Field_Name_HxTaking_Field19
            }},
            { Constants.Summary_Category_Label_Name_DoctorConsult_UrinaryIncon, new List<string> {
                Constants.Summary_Field_Name_HxTaking_Field7
            }},
            { Constants.Summary_Category_Label_Name_DoctorConsult_MentalHealth, new List<string> {

            }},
            { Constants.Summary_Category_Label_Name_Cog2nd_AMT, new List<string> {
                Constants.Summary_Field_Name_Geri_Field0,
                Constants.Summary_Field_Name_Geri_Field1,
                Constants.Summary_Field_Name_Geri_Field2,
                Constants.Summary_Field_Name_Geri_Field3,
                Constants.Summary_Field_Name_Geri_Field4,
                Constants.Summary_Field_Name_Geri_Field5,
                Constants.Summary_Field_Name_Geri_Field6,
                Constants.Summary_Field_Name_Geri_Field7,
                Constants.Summary_Field_Name_Geri_Field8,
                Constants.Summary_Field_Name_Geri_Field9,
                Constants.Summary_Field_Name_Geri_Field10,
                Constants.Summary_Field_Name_Geri_Field11,
                Constants.Summary_Field_Name_Geri_Field12,
                Constants.Summary_Field_Name_Geri_Field13,
                Constants.Summary_Field_Name_Geri_Field14,
                Constants.Summary_Field_Name_Geri_Field15,
                Constants.Summary_Field_Name_Geri_Field16
            }},
             { Constants.Summary_Category_Label_Name_DoctorConsult_PTMemo, new List<string> {
                Constants.Summary_Field_Name_Geri_Field109
            }},
            { Constants.Summary_Category_Label_Name_DoctorConsult_OTMemo, new List<string> {
                Constants.Summary_Field_Name_Geri_Field115
            }},
            { Constants.Summary_Category_Label_Name_DoctorConsult_Vision, new List<string> {
                Constants.Summary_Field_Name_Geri_Field33,
                Constants.Summary_Field_Name_Geri_Field34,
                Constants.Summary_Field_Name_Geri_Field35,
                Constants.Summary_Field_Name_Geri_Field36,
                Constants.Summary_Field_Name_Geri_Field37,
                Constants.Summary_Field_Name_Geri_Field38,
                Constants.Summary_Field_Name_Geri_Field39,
                Constants.Summary_Field_Name_Geri_Field40,
                Constants.Summary_Field_Name_Geri_Field41,
                Constants.Summary_Field_Name_Geri_Field42
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
