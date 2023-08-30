using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ResumeManagement.Models.ViewModels
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel()
        {
            this.QualificationList = new List<int>();
        }
        public int EmployeeId { get; set; }
        [Required, StringLength(50), Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
        [Required, Display(Name = "Date of Birth"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        public string Picture { get; set; }
        [Display(Name = "Image")]
        public HttpPostedFileBase PicturePath { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        public List<int> QualificationList { get; set; }
        
    }
}