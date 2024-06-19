using System;
using System.Reflection;
using UnityEngine;

namespace PerfMonitor
{
      public class MethodInterceptor : MonoBehaviour
      {
            void Awake()
            {
                  var methods = GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                  foreach (var method in methods)
                  {
                        var monitorAttribute = method.GetCustomAttribute<MonitorAttribute>();
                        if (monitorAttribute != null)
                        {
                              MethodInvoker(monitorAttribute.Name, method);
                        }
                  }
            }

            private void MethodInvoker(string functionName, MethodInfo method)
            {
                  // Create a delegate for the method to be monitored
                  var originalDelegate = (Action)Delegate.CreateDelegate(typeof(Action), this, method);

                  // Create a new delegate with the monitoring logic
                  void MonitoredDelegate()
                  {
                        PerformanceMonitor.StartMonitoring(functionName);
                        originalDelegate();
                        PerformanceMonitor.StopMonitoring(functionName);
                  }

                  // Replace the original method with the monitored one
                  ReplaceMethod(this, method.Name, MonitoredDelegate);
            }

            private static void ReplaceMethod(object target, string methodName, Action newMethod)
            {
                  var field = target.GetType().GetField(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
                  if (field != null)
                  {
                        field.SetValue(target, newMethod);
                  }
            }
      }
}