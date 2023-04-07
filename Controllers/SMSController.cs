using FullStack.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;

using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace FullStack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmsController : Controller
    {
        string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
        string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");
        public SmsController()
        {
            TwilioClient.Init(accountSid, authToken);
        }

        [HttpGet]
        public IActionResult Get([FromQuery(Name = "date_sent")] string? dateSent)
        {
            //  dateSent: new DateTime(2016, 8, 31, 0, 0, 0),
            ReadMessageOptions options = new ReadMessageOptions();
            options.From = new Twilio.Types.PhoneNumber("+15854604860");
            options.Limit = 100;
            if (!String.IsNullOrEmpty(dateSent))
            {
                try
                {
                    DateTime d = DateTime.ParseExact(dateSent, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
                    options.DateSent = new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, 0);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
                }
            }
            var res = MessageResource.Read(options);
            var f = res.ToList();
            return this.Ok(f);
        }

        [HttpPost]
        public IActionResult Post([FromBody] SendSMSRequestViewModel message)
        {
            return this.Ok();
            try
            {
                var smsData = MessageResource.Create(
                    body: message.message,
                    from: new Twilio.Types.PhoneNumber("+15854604860"),
                    to: new Twilio.Types.PhoneNumber(message.to)
                );

                Console.WriteLine(smsData.Sid);
                Console.WriteLine("Done: " + smsData.Sid);
                return this.Ok();
            }
            catch (Exception ex)
            {

            }
            return this.BadRequest();
        }
    }
}