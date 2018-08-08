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
        //[HttpPost]
        public ActionResult TwilioMessage()
        {
            //TEST CREDENTIALS
            const string accountSid = "AC915abf9cbec69d0a7262a3f3a5ee2275";
            const string authToken = "af2dd04276e788fef20f5fce7e700af6";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "This is the ship that made the Kessel Run in fourteen parsecs?",
                from: new Twilio.Types.PhoneNumber("+19206266861"),
                to: new Twilio.Types.PhoneNumber("+16086306751")
                );
                return RedirectToAction("TwilioTesting");

            //Customer customer = new Customer();
            //customer.CustomerNumber();
            //string toPhoneNumber = customer.sendToNumber;
            //string sendFromNumber = "+19205450383";

            //int deliveryTime = 37;

            //string words = "Thank you for your order, " + customer.customerName + "! Your order's estimated arrival time is " + deliveryTime + " minutes";
            //// Find your Account Sid and Auth Token at twilio.com/console
            //const string accountSid = "ACf309d8bdb8d911c97fa8855013d33c1a";
            //const string authToken = "442b02a8ee25201565d6c92237d53f81";
            //TwilioClient.Init(accountSid, authToken);

            //var to = new PhoneNumber(toPhoneNumber);
            //var message = MessageResource.Create(
            //    to,
            //    from: new PhoneNumber(sendFromNumber),
            //    body: words);

            //Console.WriteLine(message.Sid);
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