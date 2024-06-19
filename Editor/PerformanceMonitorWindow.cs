using UnityEditor;
using UnityEngine;

namespace PerfMonitor.Editor
{
      public sealed class PerformanceMonitorWindow : EditorWindow
      {
            private string _functionName;

            [MenuItem("Window/Performance Monitor")]
            public static void ShowWindow()
            {
                  GetWindow<PerformanceMonitorWindow>("Performance Monitor");
            }

            private void OnGUI()
            {
                  GUILayout.Label("Performance Monitor", EditorStyles.boldLabel);

                  this._functionName = EditorGUILayout.TextField("Function Name", this._functionName);

                  if (GUILayout.Button("Get Timings"))
                  {
                        var timings = PerformanceMonitor.GetTimings(this._functionName);
                        if (timings != null)
                        {
                              foreach (var time in timings)
                              {
                                    GUILayout.Label($"Execution Time: {time} ms");
                              }
                        }
                        else
                        {
                              GUILayout.Label("No timings found for this function.");
                        }
                  }

                  if (GUILayout.Button("Clear Timings"))
                  {
                        PerformanceMonitor.ClearTimings(this._functionName);
                  }
            }
      }
}