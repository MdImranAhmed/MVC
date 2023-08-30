using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ResumeManagement.Models
{
    public class Employee
    {
        public Employee()
        {
            this.QualificationEntries = new List<QualificationEntry>();
        }
        public int EmployeeId { get; set; }
        [Required, StringLength(50), Display(Name = "Client Name")]
        public string EmployeeName { get; set; }
        [Required, Display(Name = "Date of Birth"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        public string Picture { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        public ICollection<QualificationEntry> QualificationEntries { get; set; }
    }
}