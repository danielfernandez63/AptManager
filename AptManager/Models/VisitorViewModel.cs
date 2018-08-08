using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AptManager.Models
{
    public class VisitorViewModel
    {
        public Visitor Visitor { get; set; }

        public Tenant Tenant { get; set; }
 
    }
}