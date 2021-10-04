using MaranataBlank.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MaranataBlank.Services
{
    class MockGeneralDataAnalysisServices : IGeneralDataAnalysisServices
    {
        public async Task<LocationStatistics> AnalyzeDataForLocation(LocationGeoDetails e)
        {
            await Task.Delay(2500);

            LocationStatistics stats = new LocationStatistics();
            stats.Population = 2960048;
            stats.Recovered = 1386;
            stats.Infected = 7977;
            stats.Died = 3;
            stats.Source = "Department of Health"; 

            return stats;
        }

    }
}
