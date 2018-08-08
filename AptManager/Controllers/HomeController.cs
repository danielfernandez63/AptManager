using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace AptManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Attractions()
        {
            return View();
        }

        public ActionResult TwilioTesting()
        {
            return View();
        }

        public ActionResult TwilioMessage()
        {
            //TEST CREDENTIALS
            const string accountSid = "AC2e158f49c90fb7b1447425bc7e2707f1";
            const string authToken = "2a297079bf5f2b20bd93d0849396c99e";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "This is the ship that made the Kessel Run in fourteen parsecs?",
                from: new Twilio.Types.PhoneNumber("+15005550006"),
                to: new Twilio.Types.PhoneNumber("+8473877981")
                );
                return RedirectToAction("TwilioTesting");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}