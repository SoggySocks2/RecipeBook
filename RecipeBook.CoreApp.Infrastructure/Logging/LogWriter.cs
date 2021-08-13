using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using RecipeBook.SharedKernel.Contracts;

namespace RecipeBook.CoreApp.Infrastructure.Logging
{
    public class LogWriter : ILogWriter
    {
        private readonly TelemetryClient _telemetryClient;

        public LogWriter(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }

        public void LogCritical(string message)
        {
            _telemetryClient.TrackTrace(message, SeverityLevel.Critical);
        }

        public void LogError(string message)
        {
            _telemetryClient.TrackTrace(message, SeverityLevel.Error);
        }

        public void LogWarning(string message)
        {
            _telemetryClient.TrackTrace(message, SeverityLevel.Warning);
        }

        public void LogInformation(string message)
        {
            _telemetryClient.TrackTrace(message, SeverityLevel.Information);
        }

        public void LogDebug(string message)
        {
            _telemetryClient.TrackTrace(message, SeverityLevel.Verbose);
        }
    }
}
