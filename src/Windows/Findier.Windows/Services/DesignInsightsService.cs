using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights;

namespace Findier.Client.Windows.Services
{
    internal class DesignInsightsService : IInsightsService
    {
        public TelemetryClient Client { get; }

        public InsightsService.InsightsStopwatchEvent TrackTimeEvent(string name,
            IDictionary<string, string> properties = null)
        {
            throw new NotImplementedException();
        }

        public void TrackPageView(string name, string parameter)
        {
            throw new NotImplementedException();
        }
    }
}