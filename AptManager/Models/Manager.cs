﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptManager.Models
{
    public class Manager
    {
        [Key]
        public int ManagerId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}