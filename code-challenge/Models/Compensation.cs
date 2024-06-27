using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;

namespace challenge.Models.Comp
{
    
    public class Compensation
    {
        [Key]public string CompensationId { get; set; }
        [ForeignKey("EmployeeId")]public string EmployeeId { get; set; }
        public int salary { get; set; }
        public DateTime effectiveDate { get; set; }

    }
}