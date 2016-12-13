using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pushup.Models;
using Pushup.ApiModels;

namespace Pushup.BusinessLayer
{
    public static class Extensions
    {
        public static decimal Median(this IEnumerable<decimal> collection)
        {
            var temp = collection.Select(x => x).ToArray();
            Array.Sort(temp);
            int count = temp.Length;
            if (count == 0)
            {
                throw new InvalidOperationException("Empty collection");
            }
            else if (count % 2 == 0)
            {
                // count is even, average two middle elements
                var a = temp[count / 2 - 1];
                var b = temp[count / 2];
                return (a + b) / 2m;
            }
            else
            {
                // count is odd, return the middle element
                return temp[count / 2];
            }
        }
    }

    public class DataPointManager
    {
        // temp hack while no DB
        public static List<DataAccount> Accounts = new List<DataAccount>();

        public DataAccount GetOrCreateAccount(string slug)
        {
            var result = Accounts.Where(x => x.Slug == slug).FirstOrDefault();
            if (result == null)
            {
                result = new DataAccount() { Slug = slug };
                Accounts.Add(result);
            }
            return result;
        }

        public VariableData GetOrCreateVariable(DataAccount account, string name)
        {
            var result = account.Variables.Where(x => x.Name == name).FirstOrDefault();
            if (result == null)
            {
                result = new VariableData() { Name = name };
                account.Variables.Add(result);
            }
            return result;
        }

        // todo: handle problem where no data pushed for > summary period resulting in summary of whatever hasn't been summarized yet, regardless of how many hours it's been
        public void AddRawDataPoint(string slug, VariableDataPush point)
        {

            var now = DateTime.Now;
            var account = GetOrCreateAccount(slug);
            var variable = GetOrCreateVariable(account, point.name);
            variable.RecentRawData.Add(now, point.value);
            if (variable.SummaryGranularityMinutes > 0)
            {
                
                DateTime cutoff;
                if (variable.History.Any())
                {
                    var lastSummary = variable.History.OrderBy(x => x.Key).Last();
                    cutoff = lastSummary.Key.AddMinutes(variable.SummaryGranularityMinutes);
                }
                else
                {
                    cutoff = now.AddMinutes(0 - variable.SummaryGranularityMinutes);
                }
                while (cutoff < now)
                {
                    var toSummarize = variable.RecentRawData.Where(x => x.Key < cutoff);
                    var summary = new SummaryPoint();
                    if (toSummarize.Any())
                    {
                        summary.Min = toSummarize.Min(x => x.Value);
                        summary.Max = toSummarize.Max(x => x.Value);
                        summary.Median = toSummarize.Select(x => x.Value).Median();

                        variable.History.Add(cutoff, summary);
                        foreach (var toRemove in toSummarize.ToList())
                        {
                            variable.RecentRawData.Remove(toRemove.Key);
                        }
                    }
                    else
                    {
                        // seems to be a gap. insert blank? do nothing and allow to be lumped in with next period?
                    }
                    cutoff = cutoff.AddMinutes(variable.SummaryGranularityMinutes);
                }
            }
        }
    }
}