using System.Collections.Generic;
using Microsoft.ApplicationInsights;

namespace Findier.Windows.Services
{
    internal interface IInsightsService
    {
        TelemetryClient Client { get; }
        InsightsService.InsightsStopwatchEvent TrackTimeEvent(string name, IDictionary<string, string> properties = null);
        void TrackPageView(string name, string parameter);
    }
}