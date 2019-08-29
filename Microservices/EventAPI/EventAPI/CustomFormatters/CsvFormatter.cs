using EventAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAPI.CustomFormatters
{
    public class CsvFormatter : TextOutputFormatter
    {
        public CsvFormatter()
        {
            this.SupportedEncodings.Add(Encoding.UTF8);
            this.SupportedEncodings.Add(Encoding.Unicode);
            this.SupportedMediaTypes.Add("text/csv");
            this.SupportedMediaTypes.Add("application/csv");
        }
        protected override bool CanWriteType(Type type)
        {
            if (typeof(EventInfo).IsAssignableFrom(type) || typeof(IEnumerable<EventInfo>).IsAssignableFrom(type))
            {
                return true;
            }
            return false;
        }
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var buffer = new StringBuilder();
            var response = context.HttpContext.Response;

            if (context.Object is EventInfo) //if item is single eventinfo object
            {
                var eventInfo = context.Object as EventInfo;
                buffer.AppendLine("Id, EventTitle,StartDate, EndDate,Location,Organizer,RegistrationUrl");
                buffer.AppendLine($"{eventInfo.Id},{eventInfo.EventTitle},{eventInfo.StartDate},{eventInfo.EndDate},{eventInfo.Organizer},{eventInfo.Id},{eventInfo.RegistrationUrl}");

            }
            else if (context.Object is IEnumerable<EventInfo>) //if array of eventinfo object
            {
                var events = context.Object as IEnumerable<EventInfo>;
                buffer.AppendLine("Id, EventTitle,StartDate, EndDate,Location,Organizer,RegistrationUrl");
                foreach(var ev in events)
                {
                    buffer.AppendLine($"{ev.Id},{ev.EventTitle},{ev.StartDate},{ev.EndDate},{ev.Organizer},{ev.Id},{ev.RegistrationUrl}");
                }         

            }
            await response.WriteAsync(buffer.ToString());
            
        }
    }
}
