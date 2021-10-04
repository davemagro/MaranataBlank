using MaranataBlank.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MaranataBlank.Services
{
    public interface IGeneralDataAnalysisServices
    {
        Task<LocationStatistics> AnalyzeDataForLocation(LocationGeoDetails e); 
    }
}
