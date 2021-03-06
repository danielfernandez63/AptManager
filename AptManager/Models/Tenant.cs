﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace AptManager.Models
{
    public class Tenant
    {
        [Key]
        public int TenantId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("HousingUnit")]
        [Display(Name = "Unit Number")]
        public int? UnitId { get; set; }
        public HousingUnit HousingUnit { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Contact Email")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Rent balance due")]
        public int BalanceDue{ get; set; }

        [Display(Name = "Rent Due Date")]
        public DateTime? RentDueDate { get; set; }

        [Display(Name = "Rent Paid This Month")]
        public bool RentPaid { get; set; }


    }
}