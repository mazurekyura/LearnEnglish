using LearnEnglish.EfStuff.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.EfStuff
{
    public class LearnEnglishDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<BankCard> BankCards { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Book> Books { get; set; }

        public LearnEnglishDbContext(DbContextOptions options) : base(options)
        {                
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(UserConfigure);
            modelBuilder.Entity<UserProfile>(UserProfileConfigure);
            modelBuilder.Entity<Lesson>(LessonConfigure);
        }
        private void UserConfigure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(x => x.UserProfile)
                .WithOne(x => x.Owner)
                .HasForeignKey<UserProfile>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.BankCards)
                .WithOne(x => x.Owner)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Books)
                .WithOne(x => x.Creater);

            builder.Property(x => x.Role).HasDefaultValue(Role.User);

            builder.Property(x => x.Language).HasDefaultValue(Language.English);
        }

        private void UserProfileConfigure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.Property(x => x.FullName)
                .HasComputedColumnSql("[FirstName] + ' ' + [LastName]");

            builder.Property(x => x.FirstName).HasDefaultValue("FirstName");
            builder.Property(x => x.LastName).HasDefaultValue("LastName");
        }

        private void LessonConfigure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasMany(x => x.Users)
                .WithMany(x => x.Lessons);                
        }
    }
}