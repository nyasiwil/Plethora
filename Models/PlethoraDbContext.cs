using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Plethora.Models
{
    public partial class PlethoraDbContext : DbContext
    {
        public PlethoraDbContext()
        {
        }

        public PlethoraDbContext(DbContextOptions<PlethoraDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ArticleTypes> ArticleTypes { get; set; }
        public virtual DbSet<Articles> Articles { get; set; }
        public virtual DbSet<Highlights> Highlights { get; set; }
        public virtual DbSet<Statuses> Statuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=Plethora;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleTypes>(entity =>
            {
                entity.HasKey(e => e.ArticleTypeId)
                    .HasName("PK__Article___2EDB5307167A612C");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Title).IsUnicode(false);
            });

            modelBuilder.Entity<Articles>(entity =>
            {
                entity.HasKey(e => e.ArticleId)
                    .HasName("PK__Articles__500A46B61A099164");

                entity.Property(e => e.BodyCopy)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ByLine)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Intro)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Title)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.ArticleType)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.ArticleTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Articles__Articl__5EBF139D");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Articles__Status__6383C8BA");
            });

            modelBuilder.Entity<Highlights>(entity =>
            {
                entity.HasKey(e => e.HighlightId)
                    .HasName("PK__Highligh__2C02D923F15595CF");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Title)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Highlights)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Highlight__Statu__5070F446");
            });

            modelBuilder.Entity<Statuses>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PK__Statuses__519009AC6480AA1D");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Title).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
