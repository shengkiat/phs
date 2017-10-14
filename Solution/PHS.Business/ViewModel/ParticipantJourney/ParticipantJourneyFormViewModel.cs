using PHS.Business.Helpers;
using PHS.Business.Implementation;
using PHS.Business.ViewModel.SummaryCategory;
using PHS.Common;
using PHS.DB;
using PHS.FormBuilder.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace PHS.Business.ViewModel.ParticipantJourney
{
    public class ParticipantJourneyFormViewModel
    {

        public ParticipantJourneyFormViewModel(Participant participant, int PHSEventId)
        {
            Participant = participant;
            Event = participant.PHSEvents.Where(e => e.PHSEventID == PHSEventId).FirstOrDefault();
        }

        private PHSEvent Event { get; }

        private Participant Participant { get; }

        public int SelectedModalityId { get; set; }

        public List<Form> GetModalityFormsForTabs()
        {
            List<Form> result = new List<Form>();

            foreach (var modality in Event.Modalities)
            {
                if (modality.ModalityID.Equals(SelectedModalityId))
                {
                    result = modality.Forms.ToList();
                }
            }

            return result.OrderBy(f=>f.Title).ToList();
        }

        public bool isSummarySelected()
        {
            bool result = false;
            //foreach (var modality in Event.Modalities)
            //{
            //    if (modality.ModalityID.Equals(SelectedModalityId))
            //    {
            //        result = modality.Name.Equals("Summary");
            //    }
            //}
            return result;
        }

        public List<SummaryCategoryViewModel> GetSummaryCategories(string summaryType)
        {
            List<SummaryCategoryViewModel> result = new List<SummaryCategoryViewModel>();

            List<string> categoryNames = null;
            Dictionary<string, List<string>> summaryLabelMap = null;

            using (var summaryMappingManager = new SummaryMappingManager())
            {
                categoryNames = summaryMappingManager.GetAllCategoryNamesBySummaryType(summaryType);
                summaryLabelMap = summaryMappingManager.GetSummaryLabelMapBySummaryType(summaryType);
            }

            if (categoryNames == null)
            {
                return result;
            }

            foreach (var summaryCategoryName in categoryNames)
            {
                SummaryCategoryViewModel sumCategoryViewModel = new SummaryCategoryViewModel(summaryCategoryName);

                if(summaryCategoryName.Contains("Gastrointestinal Health"))
                {
                    sumCategoryViewModel.Highlight = true;
                }

                foreach (var summary in Participant.Summaries)
                {
                    if (summary != null && summary.PHSEventID.Equals(Event.PHSEventID))
                    {
                        if (SummaryHelper.IsFieldNameAndCategoryFoundInSummaryMap(summaryLabelMap, summaryCategoryName, summary.Label))
                        {
                            SummaryViewModel sumview = new SummaryViewModel(summary);

                            if(sumCategoryViewModel.Highlight != true)
                            {

                               // summary.TemplateFieldID;
                                if (SummaryHelper.IsHighlightCategoryRequired(summaryCategoryName, summary.TemplateField.SummaryFieldName, summary.SummaryValue))
                                {
                                    sumCategoryViewModel.Highlight = true;
                                }
                            }


                            if (summary.StandardReferenceID != null && summary.StandardReferenceID > 0
                                && summary.SummaryValue != null)
                            {

                                if (SummaryHelper.IsJson(summary.SummaryValue))
                                {

                                    //if (summary.StandardReferenceID == 1) //Systolic Blood Pressure 
                                    //{
                                    //    sumview.SummaryValue = summary.SummaryValue;
                                    //    sumview.SummaryInnerValue = summary.SummaryValue;
                                    //}else if (summary.StandardReferenceID == 2) //Diastolic Blood Pressure 
                                    //{
                                    //        sumview.SummaryValue = summary.SummaryValue;
                                    //        sumview.SummaryInnerValue = summary.SummaryValue;
                                    //}else if (summary.StandardReferenceID == 3) //Sugar 
                                    //{
                                    //    sumview.SummaryValue = summary.SummaryValue;
                                    //    sumview.SummaryInnerValue = summary.SummaryValue;
                                    //}else 
                                    
                                    if (summary.StandardReferenceID == 4) //BMI
                                    {
                                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                                        BMIViewModel bmi = serializer.Deserialize<BMIViewModel>(summary.SummaryValue as string);
                                        if (bmi.BodyMassIndex != null)
                                        {
                                            //Weight: 50, Height: 180, BodyMassIndex: 15.43  (UNDERWEIGHT)
                                            sumview.SummaryValue = "Weight: " + bmi.Weight + ", Height: " + bmi.Height + ", BodyMassIndex: " + bmi.BodyMassIndex;
                                            sumview.SummaryInnerValue = bmi.BodyMassIndex;
                                        }
                                    }
                                }else
                                {
                                    sumview.SummaryInnerValue = summary.SummaryValue;
                                }

                                ReferenceRange referenceRange = null;

                                using (var StandardReferenceManager = new StandardReferenceManager())
                                {
                                    string message = string.Empty;
                                    referenceRange = StandardReferenceManager.GetReferenceRange(summary.StandardReferenceID.GetValueOrDefault(),
                                        sumview.SummaryInnerValue, out message);
                                }

                                if (referenceRange != null)
                                {
                                    sumview.Result = referenceRange.Result;
                                    sumview.Highlight = referenceRange.Highlight;
                                }
                            }

                            if (sumview != null)
                            {
                                sumCategoryViewModel.AddSummary(sumview);
                            }
                        }
                    }
                }

                result.Add(sumCategoryViewModel);
            }

            return result;
        }

    }
}
