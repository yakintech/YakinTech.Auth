using OWINAuth.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OWINAuth.Models.Context
{
    public class ProjectContext:DbContext
    {
        public ProjectContext()
        {
            Database.Connection.ConnectionString = "Server=DESKTOP-IPLQK1T;Database=ProjectSampleDB;Integrated Security=true";
        }

        public DbSet<ApplicationUser> Users { get; set; }

    }
}