using Serilog.Core;
using Serilog.Events;

namespace E_commerce.API.Configurations
{
    public class CustomUserNameColumn : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            try
            {
                var (username, value) = logEvent.Properties.FirstOrDefault(x => x.Key == "UserName");
                if (value != null)
                {
                    var getValue = propertyFactory.CreateProperty(username, value);
                    logEvent.AddPropertyIfAbsent(getValue);
               
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }
    }
}
