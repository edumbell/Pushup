using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pushup.Models
{
    public class VariableData
    {
        public VariableData()
        {
            RecentRawData = new Dictionary<DateTime, decimal>();
            History = new Dictionary<DateTime, SummaryPoint>();
            DisplayHistory = true;
            SummaryGranularityMinutes = 1;
            SummaryDisplayCount = 10;
        }

        // used by API to identify the variable, and displayed on dashboard
        public string Name { get; set; }

        public Dictionary<DateTime, decimal> RecentRawData {get; set;}

        public Dictionary<DateTime, SummaryPoint> History {get; set;}

        public bool DisplayHistory { get; set; }

        // amount of time that will be "chunked" into a min/max/median history
        // = amount of raw data displayed
        public int SummaryGranularityMinutes { get; set; }
        // how many history summaries to display for this variable.
        // (SummaryGranularityMinutes * SummaryDisplayCount = total minutes of history shown
        public int SummaryDisplayCount { get; set; }
    }

    public class SummaryPoint
    {
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public decimal Median { get; set; }

    }
}