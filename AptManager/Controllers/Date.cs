using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptManager.Controllers
{
    public static class Date
    {
        public static int GetCurrentDay()
        {
            int currentDay = DateTime.Now.Day;
            return currentDay;
        }


    }
}