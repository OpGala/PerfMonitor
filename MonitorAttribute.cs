using System;

namespace PerfMonitor
{
      [AttributeUsage(AttributeTargets.Method)]
      public sealed class MonitorAttribute : Attribute
      {
            public string Name { get; }

            public MonitorAttribute(string name)
            {
                  this.Name = name;
            }
      }
}