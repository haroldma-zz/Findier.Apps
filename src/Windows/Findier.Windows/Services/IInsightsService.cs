using System.Collections.Generic;
using Microsoft.ApplicationInsights;

namespace Findier.Windows.Services
{
    public interface IInsightsService
    {
        TelemetryClient Client { get; }

        void TrackEvent(
            string name,
            IDictionary<string, string> properties = null,
            IDictionary<string, double> metrics = null);

        void TrackPageView(string name);

        InsightsService.InsightsStopwatchEvent TrackTimeEvent(
            string name,
            IDictionary<string, string> properties = null);
    }
}