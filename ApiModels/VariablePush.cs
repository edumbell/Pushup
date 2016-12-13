using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pushup.ApiModels
{
    public class VariableDataPush
    {
        public string name { get; set; }
        public decimal value { get; set; }

    }

    public class VariableConfigPush
    {
        public VariableConfigPush()
        {
            summaryDisplayCount = 10;
        }

        public string name { get; set; }

        // amount of time that will be "chunked" into a min/max/median history
        // = amount of raw data displayed
        public int summaryGranularityMinutes { get; set; }
        // how many history summaries to display for this variable.
        // (SummaryGranularityMinutes * SummaryDisplayCount = total minutes of history shown
        public int summaryDisplayCount { get; set; }

        // todo:
        public bool displayGraph { get; set; }

    }
}