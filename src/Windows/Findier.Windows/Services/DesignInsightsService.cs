using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights;

namespace Findier.Windows.Services
{
    internal class DesignInsightsService : IInsightsService
    {
        public TelemetryClient Client { get; }

        public void TrackEvent(
            string name,
            IDictionary<string, string> properties = null,
            IDictionary<string, double> metrics = null)
        {
            throw new NotImplementedException();
        }

        public void TrackPageView(string name)
        {
            throw new NotImplementedException();
        }

        public InsightsService.InsightsStopwatchEvent TrackTimeEvent(
            string name,
            IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }
    }
}