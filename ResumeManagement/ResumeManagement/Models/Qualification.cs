using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ResumeManagement.Models
{
    public class Qualification
    {
        public Qualification()
        {
            this.QualificationEntries = new List<QualificationEntry>();
        }
        public int QualificationId { get; set; }
        [Required, StringLength(50), Display(Name = "Qualification")]
        public string QualificationName { get; set; }
        public ICollection<QualificationEntry> QualificationEntries { get; set; }
    }
}