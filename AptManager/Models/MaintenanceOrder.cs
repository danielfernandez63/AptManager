using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AptManager.Models
{
    public class MaintenanceOrder
    {
        [Key]
        public int OrderId { get; set; }

        [ForeignKey("HousingUnit")]
        [Display(Name = "Unit Number")]
        public int UnitId { get; set; }
        public HousingUnit HousingUnit { get; set; }

        [ForeignKey("Worker")]
        [Display(Name = "Assigned worker")]
        public int? WorkerId { get; set; }
        public Worker Worker { get; set; }

        [Display(Name = "Name Of Repair")]
        public string Name { get; set; }

        [Display(Name = "Description Of Work To Be Done")]
        public string Description { get; set; }

        [Display(Name = "Repair Due Date")]
        public DateTime? DueDate { get; set; }

        [Display(Name = "Work Completed")]
        public bool? IsCompleted { get; set; }
    }
}