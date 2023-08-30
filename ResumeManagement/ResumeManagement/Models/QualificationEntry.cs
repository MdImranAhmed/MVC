using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ResumeManagement.Models
{
    public class QualificationEntry
    {
        public int QualificationEntryId { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [ForeignKey("Qualification")]
        public int QualificationId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Qualification Qualification { get; set; }
    }
}