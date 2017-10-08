using PHS.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using PHS.DB;
using PHS.Repository;
using PHS.Repository.Context;
using System.IO;
using OfficeOpenXml;
using PHS.DB.ViewModels.Form;
using PHS.Business.Extensions;
using System.Transactions;
using System.Web.Mvc;
using PHS.Common;
using static PHS.Common.Constants;
using PHS.DB.ViewModels;
using System.Web;
using System.Collections;

namespace PHS.Business.Implementation
{
    public class SummaryMappingManager : BaseManager, ISummaryMappingManager, IManagerFactoryBase<ISummaryMappingManager>
    {
        public SummaryMappingManager() : base(null)
        {
        }

        public SummaryMappingManager(PHSUser loginUser) : base(loginUser)
        {
        }

        public ISummaryMappingManager Create(PHSUser loginUser)
        {
            return new SummaryMappingManager(loginUser);
        }

        public List<string> GetAllCategoryNamesBySummaryType(string summaryType)
        {
            List<string> categoryNames = null;

            using (var unitOfWork = CreateUnitOfWork())
            {
                IEnumerable<SummaryMapping> sumMaps = unitOfWork.SummaryMappings.GetSummaryMappingBySummaryType(summaryType);
                if(sumMaps != null && (sumMaps.ToList().Count > 0))
                {
                    categoryNames = new List<string>();
                }

                foreach(SummaryMapping sumMap in sumMaps){
                    if (!categoryNames.Contains(sumMap.CategoryName))
                    {
                        categoryNames.Add(sumMap.CategoryName);
                    }
                }
            }

            return categoryNames;
        }

        public Dictionary<string, List<string>> GetSummaryLabelMapBySummaryType(String summaryType)
        {
            Dictionary<string, List<string>> summaryLabelMap = new Dictionary<string, List<string>>();

            List<string> categoryNames = GetAllCategoryNamesBySummaryType(summaryType);
            if (categoryNames == null)
                return summaryLabelMap;

            using (var unitOfWork = CreateUnitOfWork())
            {
                foreach(string categoryName in categoryNames)
                {
                    IEnumerable<SummaryMapping> sumMaps = unitOfWork.SummaryMappings.GetSummaryMappingByCategoryNameAndSummaryType(categoryName, summaryType);
                    List<string> summaryFieldNames = new List<string>();
                    foreach(SummaryMapping sumMap in sumMaps)
                    {
                        summaryFieldNames.Add(sumMap.SummaryFieldName);
                    }

                    summaryLabelMap.Add(categoryName, summaryFieldNames);
                }
            }

            return summaryLabelMap;
        }

    }
}
