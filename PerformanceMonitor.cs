using System.Collections.Generic;
using System.Diagnostics;

namespace PerfMonitor
{
      public static class PerformanceMonitor
      {
            private static readonly Dictionary<string, List<long>> FunctionTimings = new Dictionary<string, List<long>>();
            private static readonly Dictionary<string, Stopwatch> Stopwatches = new Dictionary<string, Stopwatch>();

            public static void StartMonitoring(string functionName)
            {
                  if (!Stopwatches.ContainsKey(functionName))
                  {
                        Stopwatches[functionName] = new Stopwatch();
                  }

                  Stopwatches[functionName].Restart();
            }

            public static void StopMonitoring(string functionName)
            {
                  if (Stopwatches.ContainsKey(functionName))
                  {
                        Stopwatches[functionName].Stop();
                        if (!FunctionTimings.ContainsKey(functionName))
                        {
                              FunctionTimings[functionName] = new List<long>();
                        }

                        FunctionTimings[functionName].Add(Stopwatches[functionName].ElapsedMilliseconds);
                  }
            }

            public static List<long> GetTimings(string functionName)
            {
                  return FunctionTimings.GetValueOrDefault(functionName);
            }

            public static void ClearTimings(string functionName)
            {
                  if (FunctionTimings.TryGetValue(functionName, out List<long> timing))
                  {
                        timing.Clear();
                  }
            }
      }
}