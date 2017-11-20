using PHS.Common;
using PHS.FormBuilder.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace PHS.Business.Helpers
{
    class SummaryHelper
    {
        //    static List<string> EventSummaryCategoryNameList = new List<string>()
        //    {
        //        Constants.Summary_Category_Label_Name_CardiovascularHealth,
        //        Constants.Summary_Category_Label_Name_Obesity,
        //        Constants.Summary_Category_Label_Name_LifestyleChoices,
        //        Constants.Summary_Category_Label_Name_Cancers
        //    };

        //    public static Dictionary<string, List<string>> EventSummaryLabelMap = new Dictionary<string, List<string>> {
        //        { Constants.Summary_Category_Label_Name_CardiovascularHealth, new List<string> {
        //            Constants.Summary_Field_Name_CurrentlySmoke,
        //            Constants.Summary_Field_Name_FamilyHistory,
        //            Constants.Summary_Field_Name_PastMedicalHistory
        //        }},
        //        { Constants.Summary_Category_Label_Name_Obesity, new List<string> {
        //            Constants.Summary_Field_Name_PastMedicalHistory,
        //            Constants.Summary_Field_Name_FamilyHistory
        //        }},
        //        { Constants.Summary_Category_Label_Name_LifestyleChoices, new List<string> {
        //            Constants.Summary_Field_Name_CurrentlySmoke
        //        }},
        //        { Constants.Summary_Category_Label_Name_Cancers, new List<string> {
        //            Constants.Summary_Field_Name_PersonalCancerHistory,
        //            Constants.Summary_Field_Name_FamilyHistory
        //        }}
        //    };


        //    static List<string> DoctorSummaryCategoryNameList = new List<string>()
        //    {
        //        Constants.Summary_Category_Label_Name_DoctorConsult_ReasonForReferral,
        //        Constants.Summary_Category_Label_Name_DoctorConsult_BloodPressure,
        //        Constants.Summary_Category_Label_Name_DoctorConsult_BMI,
        //        Constants.Summary_Category_Label_Name_DoctorConsult_Phleblotomy,
        //        Constants.Summary_Category_Label_Name_DoctorConsult_MedHistory,
        //        Constants.Summary_Category_Label_Name_DoctorConsult_SocHistory,
        //        Constants.Summary_Category_Label_Name_DoctorConsult_UrinaryIncon,
        //        Constants.Summary_Category_Label_Name_DoctorConsult_MentalHealth,
        //        Constants.Summary_Category_Label_Name_DoctorConsult_AMT,
        //        Constants.Summary_Category_Label_Name_DoctorConsult_PTMemo,
        //        Constants.Summary_Category_Label_Name_DoctorConsult_OTMemo,
        //        Constants.Summary_Category_Label_Name_DoctorConsult_Vision
        //    };

        //    static List<string> PTSummaryCategoryNameList = new List<string>()
        //    {
        //        Constants.Summary_Category_Label_Name_PTConsult,
        //        Constants.Summary_Category_Label_Name_PTCon_ParQ_Result,
        //        Constants.Summary_Category_Label_Name_PTCon_PhysAct_Result,
        //        Constants.Summary_Category_Label_Name_PTCon_Frail_Result,
        //        Constants.Summary_Category_Label_Name_PTCon_SPPB_Result,
        //        Constants.Summary_Category_Label_Name_PTCon_TUG_Result
        //    };

        //    static List<string> OTSummaryCategoryNameList = new List<string>()
        //    {
        //        Constants.Summary_Category_Label_Name_OTConsult,
        //        Constants.Summary_Category_Label_Name_OTConsult_Vision,
        //        Constants.Summary_Category_Label_Name_OTConsult_Questionnaire,
        //        Constants.Summary_Category_Label_Name_PTCon_SPPB_Result,
        //        Constants.Summary_Category_Label_Name_OTConsult_TUG_Result
        //    };

        //    static List<string> Cog2SummaryCategoryNameList = new List<string>()
        //    {
        //        Constants.Summary_Category_Label_Name_Cog2nd_AMT,
        //        Constants.Summary_Category_Label_Name_Cog2nd_EBAS
        //    };

        //    static List<string> ExhibitionSummaryCategoryNameList = new List<string>()
        //    {
        //        Constants.Summary_Category_Label_Name_Exhibition_Cardio,
        //        Constants.Summary_Category_Label_Name_Exhibition_Obesity,
        //        Constants.Summary_Category_Label_Name_Exhibition_Gastrio,
        //        Constants.Summary_Category_Label_Name_Exhibition_Lifestyle,
        //        Constants.Summary_Category_Label_Name_Exhibition_Geri,
        //        Constants.Summary_Category_Label_Name_Exhibition_Renal,
        //        Constants.Summary_Category_Label_Name_Exhibition_Cancer,
        //        Constants.Summary_Category_Label_Name_Exhibition_SocialSupp
        //    };

        //    static List<string> SocialSuppSummaryCategoryNameList = new List<string>()
        //    {
        //        Constants.Summary_Category_Label_Name_SocialSupp_CHAS
        //    };

        //    public static Dictionary<string, List<string>> Cog2SummaryLabelMap = new Dictionary<string, List<string>> {
        //        { Constants.Summary_Category_Label_Name_Cog2nd_AMT, new List<string> {
        //            Constants.Summary_Field_Name_Geri_Field0,
        //            Constants.Summary_Field_Name_Geri_Field1,
        //            Constants.Summary_Field_Name_Geri_Field2,
        //            Constants.Summary_Field_Name_Geri_Field3,
        //            Constants.Summary_Field_Name_Geri_Field4,
        //            Constants.Summary_Field_Name_Geri_Field5,
        //            Constants.Summary_Field_Name_Geri_Field6,
        //            Constants.Summary_Field_Name_Geri_Field7,
        //            Constants.Summary_Field_Name_Geri_Field8,
        //            Constants.Summary_Field_Name_Geri_Field9,
        //            Constants.Summary_Field_Name_Geri_Field10,
        //            Constants.Summary_Field_Name_Geri_Field11,
        //            Constants.Summary_Field_Name_Geri_Field12,
        //            Constants.Summary_Field_Name_Geri_Field13,
        //            Constants.Summary_Field_Name_Geri_Field14,
        //            Constants.Summary_Field_Name_Geri_Field15,
        //            Constants.Summary_Field_Name_Geri_Field16                
        //        }},
        //        { Constants.Summary_Category_Label_Name_Cog2nd_EBAS, new List<string> {
        //            Constants.Summary_Field_Name_Geri_Field17,
        //            Constants.Summary_Field_Name_Geri_Field18,
        //            Constants.Summary_Field_Name_Geri_Field19,
        //            Constants.Summary_Field_Name_Geri_Field20,
        //            Constants.Summary_Field_Name_Geri_Field21,
        //            Constants.Summary_Field_Name_Geri_Field22,
        //            Constants.Summary_Field_Name_Geri_Field23,
        //            Constants.Summary_Field_Name_Geri_Field24,
        //            Constants.Summary_Field_Name_Geri_Field25,
        //            Constants.Summary_Field_Name_Geri_Field26,
        //            Constants.Summary_Field_Name_Geri_Field27,
        //            Constants.Summary_Field_Name_Geri_Field28,
        //            Constants.Summary_Field_Name_Geri_Field29,
        //            Constants.Summary_Field_Name_Geri_Field30,
        //            Constants.Summary_Field_Name_Geri_Field31,
        //            Constants.Summary_Field_Name_Geri_Field32
        //        }}
        //    };

        //    public static Dictionary<string, List<String>> PTConsultSummaryLabelMap = new Dictionary<string, List<string>> {
        //        { Constants.Summary_Category_Label_Name_PTConsult, new List<string> {
        //            Constants.Summary_Field_Name_Geri_Field108,
        //            Constants.Summary_Field_Name_Geri_Field109,
        //            Constants.Summary_Field_Name_Geri_Field110,
        //            Constants.Summary_Field_Name_Geri_Field111
        //        }},
        //        { Constants.Summary_Category_Label_Name_PTCon_ParQ_Result, new List<string> {
        //            Constants.Summary_Field_Name_Geri_Field43,
        //            Constants.Summary_Field_Name_Geri_Field44,
        //            Constants.Summary_Field_Name_Geri_Field45,
        //            Constants.Summary_Field_Name_Geri_Field46,
        //            Constants.Summary_Field_Name_Geri_Field47,
        //            Constants.Summary_Field_Name_Geri_Field48,
        //            Constants.Summary_Field_Name_Geri_Field49,
        //            Constants.Summary_Field_Name_Geri_Field50,
        //            Constants.Summary_Field_Name_Geri_Field51
        //}},
        //         { Constants.Summary_Category_Label_Name_PTCon_PhysAct_Result, new List<string> {
        //            Constants.Summary_Field_Name_Geri_Field52,
        //            Constants.Summary_Field_Name_Geri_Field53,
        //            Constants.Summary_Field_Name_Geri_Field54,
        //            Constants.Summary_Field_Name_Geri_Field55,
        //            Constants.Summary_Field_Name_Geri_Field56,
        //            Constants.Summary_Field_Name_Geri_Field57,
        //            Constants.Summary_Field_Name_Geri_Field58,
        //            Constants.Summary_Field_Name_Geri_Field59
        //}},
        //         { Constants.Summary_Category_Label_Name_PTCon_Frail_Result, new List<string> {
        //            Constants.Summary_Field_Name_Geri_Field60,
        //            Constants.Summary_Field_Name_Geri_Field61,
        //            Constants.Summary_Field_Name_Geri_Field62,
        //            Constants.Summary_Field_Name_Geri_Field63,
        //            Constants.Summary_Field_Name_Geri_Field64,
        //            Constants.Summary_Field_Name_Geri_Field65,
        //            Constants.Summary_Field_Name_Geri_Field66,
        //            Constants.Summary_Field_Name_Geri_Field67,
        //            Constants.Summary_Field_Name_Geri_Field68,
        //            Constants.Summary_Field_Name_Geri_Field69,
        //            Constants.Summary_Field_Name_Geri_Field70
        //}},
        //         { Constants.Summary_Category_Label_Name_PTCon_SPPB_Result, new List<string> {
        //            Constants.Summary_Field_Name_Geri_Field85,
        //            Constants.Summary_Field_Name_Geri_Field86,
        //            Constants.Summary_Field_Name_Geri_Field87,
        //            Constants.Summary_Field_Name_Geri_Field88,
        //            Constants.Summary_Field_Name_Geri_Field89,
        //            Constants.Summary_Field_Name_Geri_Field90,
        //            Constants.Summary_Field_Name_Geri_Field91,
        //            Constants.Summary_Field_Name_Geri_Field92,
        //            Constants.Summary_Field_Name_Geri_Field93,
        //            Constants.Summary_Field_Name_Geri_Field94,
        //            Constants.Summary_Field_Name_Geri_Field95,
        //            Constants.Summary_Field_Name_Geri_Field96,
        //            Constants.Summary_Field_Name_Geri_Field97,
        //            Constants.Summary_Field_Name_Geri_Field98,
        //            Constants.Summary_Field_Name_Geri_Field99,
        //            Constants.Summary_Field_Name_Geri_Field100
        //}},
        //         { Constants.Summary_Category_Label_Name_PTCon_TUG_Result, new List<string> {
        //            Constants.Summary_Field_Name_Geri_Field101,
        //            Constants.Summary_Field_Name_Geri_Field102,
        //            Constants.Summary_Field_Name_Geri_Field103,
        //            Constants.Summary_Field_Name_Geri_Field104,
        //            Constants.Summary_Field_Name_Geri_Field105,
        //            Constants.Summary_Field_Name_Geri_Field106,
        //            Constants.Summary_Field_Name_Geri_Field107
        //}}

        //    };

        //    public static Dictionary<string, List<String>> OTConsultSummaryLabelMap = new Dictionary<string, List<string>> {
        //        {Constants.Summary_Category_Label_Name_OTConsult, new List<string> {
        //            Constants.Summary_Field_Name_Geri_Field114,
        //            Constants.Summary_Field_Name_Geri_Field115,
        //            Constants.Summary_Field_Name_Geri_Field116,
        //            Constants.Summary_Field_Name_Geri_Field117
        //        }},
        //        { Constants.Summary_Category_Label_Name_OTConsult_Vision, new List<string> {
        //            Constants.Summary_Field_Name_Geri_Field33,
        //            Constants.Summary_Field_Name_Geri_Field34,
        //            Constants.Summary_Field_Name_Geri_Field35,
        //            Constants.Summary_Field_Name_Geri_Field36,
        //            Constants.Summary_Field_Name_Geri_Field37,
        //            Constants.Summary_Field_Name_Geri_Field38,
        //            Constants.Summary_Field_Name_Geri_Field39,
        //            Constants.Summary_Field_Name_Geri_Field40,
        //            Constants.Summary_Field_Name_Geri_Field41,
        //            Constants.Summary_Field_Name_Geri_Field42
        //        }},
        //        { Constants.Summary_Category_Label_Name_OTConsult_Questionnaire, new List<string> {
        //            Constants.Summary_Field_Name_Geri_Field71,
        //            Constants.Summary_Field_Name_Geri_Field72,
        //            Constants.Summary_Field_Name_Geri_Field73,
        //            Constants.Summary_Field_Name_Geri_Field74,
        //            Constants.Summary_Field_Name_Geri_Field75,
        //            Constants.Summary_Field_Name_Geri_Field76,
        //            Constants.Summary_Field_Name_Geri_Field77,
        //            Constants.Summary_Field_Name_Geri_Field78,
        //            Constants.Summary_Field_Name_Geri_Field79,
        //            Constants.Summary_Field_Name_Geri_Field80,
        //            Constants.Summary_Field_Name_Geri_Field81,
        //            Constants.Summary_Field_Name_Geri_Field82,
        //            Constants.Summary_Field_Name_Geri_Field83,
        //            Constants.Summary_Field_Name_Geri_Field84
        //}},

        //         { Constants.Summary_Category_Label_Name_PTCon_SPPB_Result, new List<string> {
        //            Constants.Summary_Field_Name_Geri_Field85,
        //            Constants.Summary_Field_Name_Geri_Field86,
        //            Constants.Summary_Field_Name_Geri_Field87,
        //            Constants.Summary_Field_Name_Geri_Field88,
        //            Constants.Summary_Field_Name_Geri_Field89,
        //            Constants.Summary_Field_Name_Geri_Field90,
        //            Constants.Summary_Field_Name_Geri_Field91,
        //            Constants.Summary_Field_Name_Geri_Field92,
        //            Constants.Summary_Field_Name_Geri_Field93,
        //            Constants.Summary_Field_Name_Geri_Field94,
        //            Constants.Summary_Field_Name_Geri_Field95,
        //            Constants.Summary_Field_Name_Geri_Field96,
        //            Constants.Summary_Field_Name_Geri_Field97,
        //            Constants.Summary_Field_Name_Geri_Field98,
        //            Constants.Summary_Field_Name_Geri_Field99,
        //            Constants.Summary_Field_Name_Geri_Field100
        //}},
        //         { Constants.Summary_Category_Label_Name_PTCon_TUG_Result, new List<string> {
        //            Constants.Summary_Field_Name_Geri_Field101,
        //            Constants.Summary_Field_Name_Geri_Field102,
        //            Constants.Summary_Field_Name_Geri_Field103,
        //            Constants.Summary_Field_Name_Geri_Field104,
        //            Constants.Summary_Field_Name_Geri_Field105,
        //            Constants.Summary_Field_Name_Geri_Field106,
        //            Constants.Summary_Field_Name_Geri_Field107
        //}}

        //    };

        //    public static Dictionary<string, List<String>> ExhibitionSummaryLabelMap = new Dictionary<string, List<string>> {
        //        { Constants.Summary_Category_Label_Name_Exhibition_Cardio, new List<string> {
        //            Constants.Summary_Field_Name_HxTaking_Field0,
        //            Constants.Summary_Field_Name_HxTaking_Field1,
        //            Constants.Summary_Field_Name_HxTaking_Field2,
        //            Constants.Summary_Field_Name_HxTaking_Field3,
        //            Constants.Summary_Field_Name_HxTaking_Field4
        //}},

        //         { Constants.Summary_Category_Label_Name_Exhibition_Obesity, new List<string> {
        //            Constants.Summary_Field_Name_HxTaking_Field5,
        //            Constants.Summary_Field_Name_HxTaking_Field3,
        //            Constants.Summary_Field_Name_HxTaking_Field4
        //}},
        //         { Constants.Summary_Category_Label_Name_Exhibition_Gastrio, new List<string> {

        //}},
        //        { Constants.Summary_Category_Label_Name_Exhibition_Lifestyle, new List<string> {
        //            Constants.Summary_Field_Name_HxTaking_Field2,
        //            Constants.Summary_Field_Name_HxTaking_Field6
        //}},
        //        { Constants.Summary_Category_Label_Name_Exhibition_Geri, new List<string> {
        //            Constants.Summary_Field_Name_Geri_Field13,
        //            Constants.Summary_Field_Name_Geri_Field27,
        //            Constants.Summary_Field_Name_Geri_Field36,
        //            Constants.Summary_Field_Name_Geri_Field37,
        //            Constants.Summary_Field_Name_Reg_Field0
        //}},
        //        { Constants.Summary_Category_Label_Name_Exhibition_Renal, new List<string> {
        //            Constants.Summary_Field_Name_HxTaking_Field7
        //}},
        //        { Constants.Summary_Category_Label_Name_Exhibition_Cancer, new List<string> {
        //            Constants.Summary_Field_Name_HxTaking_Field8,
        //            Constants.Summary_Field_Name_HxTaking_Field9
        //}},
        //        { Constants.Summary_Category_Label_Name_Exhibition_SocialSupp, new List<string> {
        //            Constants.Summary_Field_Name_HxTaking_Field10,
        //            Constants.Summary_Field_Name_SocialSupport_Field0,
        //            Constants.Summary_Field_Name_SocialSupport_Field1,
        //            Constants.Summary_Field_Name_SocialSupport_Field2,
        //            Constants.Summary_Field_Name_SocialSupport_Field3
        //}},

        //    };

        //    public static Dictionary<string, List<String>> SocialSupportSummaryLabelMap = new Dictionary<string, List<string>> {
        //        { Constants.Summary_Category_Label_Name_SocialSupp_CHAS, new List<string> {
        //            Constants.Summary_Field_Name_Reg_Field1,
        //            Constants.Summary_Field_Name_Reg_Field2,
        //            Constants.Summary_Field_Name_Reg_Field3,
        //            Constants.Summary_Field_Name_Reg_Field4,
        //            Constants.Summary_Field_Name_Reg_Field5,
        //            Constants.Summary_Field_Name_Reg_Field6,
        //            Constants.Summary_Field_Name_Reg_Field7,
        //            Constants.Summary_Field_Name_HxTaking_Field11
        //}},          

        //    };

        //    public static Dictionary<string, List<string>> DoctorSummaryLabelMap = new Dictionary<string, List<string>> {
        //        { Constants.Summary_Category_Label_Name_DoctorConsult_ReasonForReferral, new List<string> {
        //           Constants.Summary_Field_Name_HxTaking_Field6,
        //           Constants.Summary_Field_Name_HxTaking_Field7,
        //           Constants.Summary_Field_Name_HxTaking_Field8
        //        }},
        //        { Constants.Summary_Category_Label_Name_DoctorConsult_BloodPressure, new List<string> {
        //            Constants.Summary_Field_Name_HxTaking_Field0,
        //            Constants.Summary_Field_Name_HxTaking_Field1
        //        }},
        //        { Constants.Summary_Category_Label_Name_DoctorConsult_BMI, new List<string> {
        //            Constants.Summary_Field_Name_HxTaking_Field5
        //        }},
        //        { Constants.Summary_Category_Label_Name_DoctorConsult_Phleblotomy, new List<string> {
        //            Constants.Summary_Field_Name_Phlebo_Field0,
        //            Constants.Summary_Field_Name_Phlebo_Field1
        //        }},
        //        { Constants.Summary_Category_Label_Name_DoctorConsult_MedHistory, new List<string> {
        //            Constants.Summary_Field_Name_HxTaking_Field3,
        //            Constants.Summary_Field_Name_HxTaking_Field12,
        //            Constants.Summary_Field_Name_HxTaking_Field8,
        //            Constants.Summary_Field_Name_HxTaking_Field4,
        //            Constants.Summary_Field_Name_HxTaking_Field9,
        //            Constants.Summary_Field_Name_HxTaking_Field13
        //        }},
        //        { Constants.Summary_Category_Label_Name_DoctorConsult_SocHistory, new List<string> {
        //            Constants.Summary_Field_Name_HxTaking_Field2,
        //            Constants.Summary_Field_Name_HxTaking_Field16,
        //            Constants.Summary_Field_Name_HxTaking_Field17,
        //            Constants.Summary_Field_Name_HxTaking_Field14,
        //            Constants.Summary_Field_Name_HxTaking_Field15,
        //            Constants.Summary_Field_Name_HxTaking_Field6,
        //            Constants.Summary_Field_Name_HxTaking_Field18,
        //            Constants.Summary_Field_Name_HxTaking_Field19
        //        }},
        //        { Constants.Summary_Category_Label_Name_DoctorConsult_UrinaryIncon, new List<string> {
        //            Constants.Summary_Field_Name_HxTaking_Field7
        //        }},
        //        { Constants.Summary_Category_Label_Name_DoctorConsult_MentalHealth, new List<string> {

        //        }},
        //        { Constants.Summary_Category_Label_Name_Cog2nd_AMT, new List<string> {
        //            Constants.Summary_Field_Name_Geri_Field0,
        //            Constants.Summary_Field_Name_Geri_Field1,
        //            Constants.Summary_Field_Name_Geri_Field2,
        //            Constants.Summary_Field_Name_Geri_Field3,
        //            Constants.Summary_Field_Name_Geri_Field4,
        //            Constants.Summary_Field_Name_Geri_Field5,
        //            Constants.Summary_Field_Name_Geri_Field6,
        //            Constants.Summary_Field_Name_Geri_Field7,
        //            Constants.Summary_Field_Name_Geri_Field8,
        //            Constants.Summary_Field_Name_Geri_Field9,
        //            Constants.Summary_Field_Name_Geri_Field10,
        //            Constants.Summary_Field_Name_Geri_Field11,
        //            Constants.Summary_Field_Name_Geri_Field12,
        //            Constants.Summary_Field_Name_Geri_Field13,
        //            Constants.Summary_Field_Name_Geri_Field14,
        //            Constants.Summary_Field_Name_Geri_Field15,
        //            Constants.Summary_Field_Name_Geri_Field16
        //        }},
        //         { Constants.Summary_Category_Label_Name_DoctorConsult_PTMemo, new List<string> {
        //            Constants.Summary_Field_Name_Geri_Field109
        //        }},
        //        { Constants.Summary_Category_Label_Name_DoctorConsult_OTMemo, new List<string> {
        //            Constants.Summary_Field_Name_Geri_Field115
        //        }},
        //        { Constants.Summary_Category_Label_Name_DoctorConsult_Vision, new List<string> {
        //            Constants.Summary_Field_Name_Geri_Field33,
        //            Constants.Summary_Field_Name_Geri_Field34,
        //            Constants.Summary_Field_Name_Geri_Field35,
        //            Constants.Summary_Field_Name_Geri_Field36,
        //            Constants.Summary_Field_Name_Geri_Field37,
        //            Constants.Summary_Field_Name_Geri_Field38,
        //            Constants.Summary_Field_Name_Geri_Field39,
        //            Constants.Summary_Field_Name_Geri_Field40,
        //            Constants.Summary_Field_Name_Geri_Field41,
        //            Constants.Summary_Field_Name_Geri_Field42
        //        }},
        //    };

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

        public static bool IsJson(string input)
        {
            if (input == null)
                return false;
            input = input.Trim();
            return input.StartsWith("{") && input.EndsWith("}")
                   || input.StartsWith("[") && input.EndsWith("]");
        }

        public static bool IsHighlightCategoryRequired(String summaryCategory, String fieldName, string fieldValue)
        {
            bool Result = false;



            //////////////// Cardiovascular Health  /////////////////////
            if (summaryCategory.ToUpper().Equals("Cardiovascular Health".ToUpper()))
            {
                if (fieldName.ToUpper().Equals("HxTakingField79".ToUpper())) //Average Reading Systolic (average of closest 2 readings)
                {
                    if (isLargeThanOrEqualExpectIntValue(fieldValue, 120)) //If above 120
                    {
                        return true;
                    }
                }

                if (fieldName.ToUpper().Equals("HxTakingField80".ToUpper())) //Average Reading Diastolic (average of closest 2 readings)
                {
                    if (isLargeThanOrEqualExpectIntValue(fieldValue, 80)) //If above 80
                    {
                        return true;
                    }
                }

                if (fieldName.ToUpper().Equals("HxTakingField2".ToUpper())) //Is the participant currently smoking?
                {
                    if (isEqualsYes(fieldValue))
                    {
                        return true;
                    }
                }

                if (fieldName.ToUpper().Equals("HxTakingField3".ToUpper()))//What are the participant's past medical conditions
                {
                    if (isHypertension(fieldValue))
                    {
                        return true;
                    }
                }

                if (fieldName.ToUpper().Equals("HxTakingField25".ToUpper())) //Is there positive family history (AMONG FIRST DEGREE RELATIVES) for the following medical conditions?
                {
                    if (isHypertension(fieldValue))
                    {
                        return true;
                    }
                }
            }

            /////////////////////// Obesity, Metabolic Syndrome & Diabetes /////////
            if (summaryCategory.ToUpper().Equals("Obesity, Metabolic Syndrome & Diabetes".ToUpper()))
            {
                if (fieldName.ToUpper().Equals("HxTakingField83".ToUpper())) //BMI
                {
                    if (isBMIMatched(fieldValue))
                    {
                        return true;
                    }
                }

                if (fieldName.ToUpper().Equals("HxTakingField3".ToUpper()))//What are the participant's past medical conditions
                {
                    if (isDiabetesOrHyperlipidemia(fieldValue))
                    {
                        return true;
                    }
                }

                if (fieldName.ToUpper().Equals("HxTakingField25".ToUpper())) //Is there positive family history (AMONG FIRST DEGREE RELATIVES) for the following medical conditions?
                {
                    if (isDiabetesOrHyperlipidemia(fieldValue))
                    {
                        return true;
                    }
                }
            }

            ////////////////////// Lifestyle Choices ////////////////////////
            if (summaryCategory.ToUpper().Equals("Lifestyle Choices".ToUpper()))
            {
                if (fieldName.ToUpper().Equals("HxTakingField2".ToUpper())) //Is the participant currently smoking?
                {
                    if (isEqualsYes(fieldValue))
                    {
                        return true;
                    }
                }

                if (fieldName.ToUpper().Equals("HxTakingField40".ToUpper())) //Does the participant drink alcohol regularly?
                {
                    if (isEqualsYes(fieldValue))
                    {
                        return true;
                    }
                }
            }

            /////////////////  Geriatrics and Mental Health /////////////////
            if (summaryCategory.ToUpper().Equals("Geriatrics and Mental Health".ToUpper()))
            {
                if (fieldName.ToUpper().Equals("GeriField14".ToUpper())) //Total Score: //1. Cognitive Screening - AMT
                {
                    if (isLessThanExpectIntValue(fieldValue, 8)) // if score below 8.
                    {
                        return true;
                    }
                }

                if (fieldName.ToUpper().Equals("GeriField30".ToUpper())) //Total Score: //1. Cognitive Screening - EBAS-DEP
                {
                    if (isLargeThanOrEqualExpectIntValue(fieldValue, 3)) //If >/= 3
                    {
                        return true;
                    }
                }

                if (fieldName.ToUpper().Equals("RegField1".ToUpper())) //Age // 18/10/1982 12:00:00 AM
                {
                    int pos = fieldValue.LastIndexOf("/") + 1;
                    fieldValue = fieldValue.Substring(pos, fieldName.Length - pos);
                    //fieldValue = "18/10/1952 12:00:00 AM";
                    //DateTime dob = DateTime.ParseExact(fieldValue, "dd/MM/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                    try
                    {
                        DateTime dob = DateTime.ParseExact(fieldValue, "yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        if (dob != null)
                        {
                            DateTime now = DateTime.Today;
                            int age = now.Year - dob.Year;
                            if (age >= 55)//If >/=55
                            {
                                return true;
                            }
                        }
                    }
                    catch (FormatException e)
                    {
                        e.ToString();
                    }
                }

            }

            /////////////////  Renal Health /////////////////
            if (summaryCategory.ToUpper().Equals("Renal Health".ToUpper()))
            {
                if (fieldName.ToUpper().Equals("HxTakingField68".ToUpper())) //ICIQ Score (A+B+C)
                {
                    if (isLargeThanOrEqualExpectIntValue(fieldValue, 1)) //If ICIQ >/= 1
                    {
                        return true;
                    }
                }

            }

            /////////////////  Cancers /////////////////
            if (summaryCategory.ToUpper().Equals("Cancers".ToUpper()))
            {
                if (fieldName.ToUpper().Equals("HxTakingField16".ToUpper())) //Personal history of cancer (please select all that apply)
                {
                    if (!isEqualsNone(fieldValue)) //If any (other than NONE) is checked
                    {
                        return true;
                    }
                }

                if (fieldName.ToUpper().Equals("HxTakingField27".ToUpper())) //Is there positive family history (AMONG FIRST DEGREE RELATIVES) for the following cancers?
                {
                    if (!isEqualsNone(fieldValue)) //If any (other than NONE) is checked
                    {
                        return true;
                    }
                }
            }

            /////////////////  Social Support /////////////////

    
            if (summaryCategory.ToUpper().Equals("Social Support".ToUpper()))
            {
                if (fieldName.ToUpper().Equals("SocialSupportField1".ToUpper())) //If you feel that the participant requires further social support, please check this box.
                {
                    if (isEqualsYes(fieldValue)) //If checked
                    {
                        return true;
                    }
                }

                if (fieldName.ToUpper().Equals("HxTakingField97".ToUpper())) //If checked, label text to appear for referral.
                {
                    if (isEqualsYes(fieldValue)) //If checked
                    {
                        return true;
                    }
                }

                if (fieldName.ToUpper().Equals("GeriField34".ToUpper())) //If checked, label text to appear for referral.
                {
                    if (isEqualsYes(fieldValue)) //If checked
                    {
                        return true;
                    }
                }

                if (fieldName.ToUpper().Equals("GeriField91".ToUpper())) //If checked, label text to appear for referral.
                {
                    if (isEqualsYes(fieldValue)) //If checked
                    {
                        return true;
                    }
                }

                if (fieldName.ToUpper().Equals("GeriField156".ToUpper())) //If checked, label text to appear for referral.
                {
                    if (isEqualsYes(fieldValue)) //If checked
                    {
                        return true;
                    }
                }

            }

            return Result;
        }

        private static bool isEqualsYes(string fieldValue)
        {
            bool result = false;
            if (fieldValue != null)
            {
                result = fieldValue.ToUpper().Equals("Yes".ToUpper());
            }

            return result;
        }

        private static bool isEqualsNone(string fieldValue)
        {
            bool result = false;
            if (fieldValue != null)
            {
                result = fieldValue.ToUpper().Equals("None".ToUpper());
            }

            return result;
        }

        private static bool isLessThanExpectIntValue(string fieldValue, int expectValue)
        {
            bool result = false;
            
            if (fieldValue != null)
            {
                int fieldInt = 0;
                try
                {
                    fieldInt = Int32.Parse(fieldValue);
                }
                catch (FormatException e)
                {
                    e.ToString();
                    fieldInt = 0;
                }
                
                if (fieldInt != 0 && fieldInt < expectValue)
                {
                    result = true;
                }
            }

            return result;
        }

        private static bool isLargeThanOrEqualExpectIntValue(string fieldValue, int expectValue)
        {
            bool result = false;

            if (fieldValue != null)
            {
                int fieldInt = 0;
                try
                {
                    fieldInt = Int32.Parse(fieldValue);
                }
                catch (FormatException e)
                {
                    e.ToString();
                    fieldInt = 0;
                }

                if (fieldInt != 0 && fieldInt >= expectValue)
                {
                    result = true;
                }
            }

            return result;
        }

        private static bool isHypertension(string fieldValue)
        {
            bool result = false;
            if (fieldValue != null)
            {
                if (fieldValue.ToUpper().Contains("HYPERTENSION")
                    || fieldValue.ToUpper().Contains("HYPERLIPIDEMIA")
                    || fieldValue.ToUpper().Contains("ISCHAEMIC")
                    || fieldValue.ToUpper().Contains("STROKE")
                    )//If Hypertension, Hyperlipidemia, Ischaemic Heart Disease, or Stroke is selected
                {
                    result = true;
                }
            }

            return result;
        }

        private static bool isDiabetesOrHyperlipidemia(string fieldValue)
        {
            bool result = false;
            if (fieldValue != null)
            {
                if (fieldValue.ToUpper().Contains("DIABETES MELLITUS")
                || fieldValue.ToUpper().Contains("HYPERLIPIDEMIA")
                )//If Diabetes Mellitus and/or Hyperlipidemia is selected
                {
                    result = true;
                }
            }

            return result;
        }

        private static bool isBMIMatched(string fieldValue)
        {
            bool result = false;
            if (fieldValue != null)
            {
                if (IsJson(fieldValue))
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    BMIViewModel bmi = serializer.Deserialize<BMIViewModel>(fieldValue as string);
                    if (bmi != null && bmi.BodyMassIndex != null)
                    {
                        float ftValue = 0;

                        try
                        {
                            ftValue = float.Parse(bmi.BodyMassIndex, CultureInfo.InvariantCulture.NumberFormat);
                            if (ftValue >= 23)
                            {
                                result = true;// If BMI >/= 23
                            }
                        }
                        catch (FormatException e)
                        {
                            e.ToString();
                            ftValue = 0;
                        }
                    }
                }
            }

            return result;
        }
    }
}
