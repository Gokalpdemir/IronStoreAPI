using Serilog.Core;
using Serilog.Events;

namespace ETıcaretAPI.API.Configurations.ColumnWriters
{
    public class UsernameColumnWriter : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var (username, value) = logEvent.Properties.FirstOrDefault(p => p.Key == "UserName");
            if(value != null)
            {
                LogEventProperty getValue = propertyFactory.CreateProperty(username, value);
                logEvent.AddPropertyIfAbsent(getValue);
            }
        }
    }
}
