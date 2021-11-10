using System;
using CoffeeContact.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeContact.Web.Context
{
    public class CoffeeContext : DbContext
    {
        public CoffeeContext(DbContextOptions<CoffeeContext> options) : base(options) {}

        public DbSet<SurveyResultsModel> SurveyResults { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SurveyResultsModel>()
                .ToTable("SurveyResults")
                .HasKey(m => m.SurveyID);
        }
    }
}