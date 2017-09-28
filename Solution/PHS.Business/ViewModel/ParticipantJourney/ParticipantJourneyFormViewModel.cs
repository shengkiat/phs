﻿using PHS.Business.Helpers;
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

            return result;
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

                foreach (var summary in Participant.Summaries)
                {
                    if (summary != null && summary.PHSEventID.Equals(Event.PHSEventID))
                    {
                        if (SummaryHelper.IsFieldNameAndCategoryFoundInSummaryMap(summaryLabelMap, summaryCategoryName, summary.Label))
                        {
                            SummaryViewModel sumview = new SummaryViewModel(summary);

                            if (summary.StandardReferenceID != null && summary.StandardReferenceID > 0
                                && summary.SummaryValue != null)
                            {

                                if (SummaryHelper.IsJson(summary.SummaryValue))
                                {
                                    if (summary.Label.Equals("HxTakingField5"))
                                    {
                                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                                        BMIViewModel bmi = serializer.Deserialize<BMIViewModel>(summary.SummaryValue as string);
                                        if (bmi.BodyMassIndex != null)
                                        {
                                            sumview.SummaryValue = bmi.BodyMassIndex;
                                        }
                                    }
                                }

                                ReferenceRange referenceRange = null;

                                using (var StandardReferenceManager = new StandardReferenceManager())
                                {
                                    string message = string.Empty;
                                    referenceRange = StandardReferenceManager.GetReferenceRange(summary.StandardReferenceID.GetValueOrDefault(),
                                        sumview.SummaryValue, out message);
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
