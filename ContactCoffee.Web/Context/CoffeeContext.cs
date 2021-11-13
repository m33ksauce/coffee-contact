namespace ContactCoffee.Web.Context
{
    using ContactCoffee.Web.Models;
    using Microsoft.EntityFrameworkCore;

    public class CoffeeContext : DbContext
    {
        public CoffeeContext(DbContextOptions<CoffeeContext> options)
        : base(options)
        {
        }

        public DbSet<SurveyResultsModel> SurveyResults { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SurveyResultsModel>()
                .ToTable("SurveyResults")
                .HasKey(m => m.SurveyID);
        }
    }
}