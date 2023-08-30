using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ResumeManagement.Models
{
    public class ResumeDbContext : DbContext
    {
        public ResumeDbContext() : base("ResumeDbContext") { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<QualificationEntry> QualificationEntries { get; set; }
    }
}