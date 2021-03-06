﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AptManager.Models
{
    public class HousingUnit
    {
        [Key]
        [Display(Name = "Room number")]
        public int UnitId { get; set; }

        [Display(Name = "Monthly rent")]
        public int MonthlyRent { get; set; }

        [Display(Name = "Number of bedrooms")]
        public int Bedrooms { get; set; }

        [Display(Name = "Apartment size in square feet")]
        public int SquareFootage { get; set; }

        [Display(Name = "Outdoor Access")]
        public bool OutdoorAccess { get; set; }



    }
}