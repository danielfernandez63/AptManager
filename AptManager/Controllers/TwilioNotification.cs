using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace AptManager.Controllers
{
    public class TwilioNotification
    {
        public void TwilioMessage(string phoneNumber, string messageText)
        {
            //TEST CREDENTIALS
            const string accountSid = "AC915abf9cbec69d0a7262a3f3a5ee2275";
            const string authToken = "af2dd04276e788fef20f5fce7e700af6";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: messageText,
                from: new Twilio.Types.PhoneNumber("+19206266861"),
                to: new Twilio.Types.PhoneNumber("+" + phoneNumber)
                );
            //return RedirectToAction("TwilioTesting");

        }
    }
}