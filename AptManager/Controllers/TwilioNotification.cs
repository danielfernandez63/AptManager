using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace AptManager.Controllers
{
    public static class TwilioNotification
    {
        public static void TwilioMessage(string phoneNumber, string messageText)
        {
            
            string accountSid = APIKeys.GetAccountSid();
            string authToken = APIKeys.GetAuthToken();

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: messageText,
                from: new Twilio.Types.PhoneNumber("+19206266861"),
                to: new Twilio.Types.PhoneNumber("+" + phoneNumber)
                );
        }

        public static string GetManagerNumber()
        {
            string ManagerNumber = "16086306751";
            return ManagerNumber;
        }
    }
}