using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Demo.Models
{
    public partial class PluralsightDemoContext : DbContext
    {
        public PluralsightDemoContext()
        {
        }

        public PluralsightDemoContext(DbContextOptions<PluralsightDemoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<Participant> Participant { get; set; }
        public virtual DbSet<PickToClick> PickToClick { get; set; }
        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<PlayerStatForGame> PlayerStatForGame { get; set; }
        public virtual DbSet<PluralsightUsers> PluralsightUsers { get; set; }
        public virtual DbSet<Roster> Roster { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

              //  optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=PluralsightDemo;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.DateOfGame).HasColumnType("datetime");
            });

            modelBuilder.Entity<Participant>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Id).IsRequired();

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NickName).HasMaxLength(50);
            });

            modelBuilder.Entity<PickToClick>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PlayerStatForGame>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Rbis).HasColumnName("RBIs");
            });

            modelBuilder.Entity<PluralsightUsers>(entity =>
            {
                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Roster>(entity =>
            {
                entity.HasNoKey();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
