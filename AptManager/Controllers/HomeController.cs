﻿using System;
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

        public async System.Threading.Tasks.Task<ActionResult> Attractions()
        {
            var request = new Yelp.Api.Models.SearchRequest();
            request.Latitude = 43.034274;
            request.Longitude = -87.911465;
            request.Term = "Restaurants, Bars";
            request.MaxResults = 10;
            request.Radius = 1610;
            request.OpenNow = true;
            var client = new Yelp.Api.Client("d76-ipn8brnI7BsOm7yk_X0Xa7-RTXpO8v4G93RcqMA9FRT3AdFbGsV8MkvAW6Q9ww-0YikIX7lDNgHfZ-6yDrfgG28FrU3PAj4TTUD1YT9mJO-hkAxqrKl-IzxrW3Yx");
            var results = await client.SearchBusinessesAllAsync(request);
            return View(results);
        }

        public ActionResult TwilioTesting()
        {
            return View();
        }
        //[HttpPost]
       

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