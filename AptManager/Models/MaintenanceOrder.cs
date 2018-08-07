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
        public int UnitId { get; set; }
        public HousingUnit HousingUnit { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; }
    }
}