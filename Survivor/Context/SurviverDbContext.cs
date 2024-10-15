using Microsoft.EntityFrameworkCore;
using Survivor.Entities;

namespace Survivor.Context
{
    public class SurviverDbContext :DbContext
    {
        public SurviverDbContext(DbContextOptions<SurviverDbContext> options) : base(options) 
        { 
           
        }

        public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();

        public DbSet<CompetitorEntity> Competitors => Set<CompetitorEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CompetitorEntity>()
                .HasOne(x => x.Category)
                .WithMany(x => x.Competitors)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
