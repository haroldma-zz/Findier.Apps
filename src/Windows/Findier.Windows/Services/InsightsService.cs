﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace Findier.Windows.Services
{
    public class InsightsService : IInsightsService
    {
        public InsightsService()
        {
            Client = new TelemetryClient();
#if !DEBUG
            IsTrackingEnabled = true;
#else
            IsTrackingEnabled = false;
#endif
        }

        public TelemetryClient Client { get; }

        public bool IsTrackingEnabled { get; }

        public void TrackEvent(
            string name,
            IDictionary<string, string> properties = null,
            IDictionary<string, double> metrics = null)
        {
            if (!IsTrackingEnabled)
            {
                return;
            }
            Client.TrackEvent(name, properties, metrics);
        }

        public void TrackPageView(string name)
        {
            if (!IsTrackingEnabled)
            {
                return;
            }
            var telemetry = new PageViewTelemetry(name);
            Client.TrackPageView(telemetry);
        }

        public InsightsStopwatchEvent TrackTimeEvent(string name, IDictionary<string, string> properties = null)
        {
            return new InsightsStopwatchEvent(Client, name, properties, IsTrackingEnabled);
        }

        public class InsightsStopwatchEvent : IDisposable
        {
            private readonly TelemetryClient _client;
            private readonly bool _isTrackingEnabled;
            private IDictionary<string, string> _properties;

            internal InsightsStopwatchEvent(
                TelemetryClient client,
                string eventName,
                IDictionary<string, string> properties,
                bool isTrackingEnabled)
            {
                _client = client;
                _properties = properties;
                _isTrackingEnabled = isTrackingEnabled;
                EventName = eventName;
                Stopwatch = new Stopwatch();
                Stopwatch.Start();
            }

            public string EventName { get; }

            public Stopwatch Stopwatch { get; }

            public void AddProperty(string name, string value)
            {
                if (_properties == null)
                {
                    _properties = new Dictionary<string, string>();
                }
                _properties.Add(name, value);
            }

            public void Dispose()
            {
                Stop();
            }

            public void Stop()
            {
                Stopwatch.Stop();

                if (_isTrackingEnabled)
                {
                    var metrics = new Dictionary<string, double>
                    { { "Processing time", Stopwatch.Elapsed.TotalMilliseconds } };
                    _client.TrackEvent(EventName, _properties, metrics);
                }

                Debug.WriteLine($"InsightsStopwatchEvent: {EventName} [{Stopwatch.Elapsed}]");
            }
        }
    }
}