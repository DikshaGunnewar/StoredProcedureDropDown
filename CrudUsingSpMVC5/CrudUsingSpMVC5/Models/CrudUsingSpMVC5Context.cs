using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CrudUsingSpMVC5.Models
{
    public class CrudUsingSpMVC5Context : DbContext
    {
       
        public CrudUsingSpMVC5Context() : base("name=MyConnection")
        {}

        #region--Create Tables--
        /// <summary>
        /// Created Two Tables For DropDown City And State
        /// </summary>
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }

        #endregion

        #region --OnModelCreating Method --
        /// <summary>
        /// written Code to map CustomerVM table instance and 
        /// seprating CustomerVM Data into 4 Tables and 
        /// Created Stored procedure
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
 
            modelBuilder.Entity<CustomerVM>().Map(e =>
            {
                e.Properties(p => new { p.Name, p.Email });
                e.ToTable("Customers");
            }).Map(e =>
            {
                e.Properties(p => new { p.CurrentAddress, p.PermanantAddress });
                e.ToTable("Address");
            }).Map(e =>
            {
                e.Properties(p => new { p.State });
                e.ToTable("States");
            }).Map(e =>
            {
                e.Properties(p => new { p.City });
                e.ToTable("Cities");
            }).MapToStoredProcedures();
        }
        #endregion


        public System.Data.Entity.DbSet<CrudUsingSpMVC5.Models.CustomerVM> CustomerVMs { get; set; }
    }
}
