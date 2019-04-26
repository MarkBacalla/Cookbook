using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cookbook.Data.Models
{
    public partial class CookbookContext : DbContext
    {
        public CookbookContext()
        {
        }

        public CookbookContext(DbContextOptions<CookbookContext> options)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Ingredient> Ingredient { get; set; }
        public virtual DbSet<Recipe> Recipe { get; set; }
        public virtual DbSet<RelRecipeIngredient> RelRecipeIngredient { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder
                    .UseLazyLoadingProxies()
                    //.UseSqlServer("Data Source=DELL-PC\\SQLEXPRESS;Initial Catalog=Cookbook;Integrated Security=True");
                    .UseSqlite("Data Source=cookbook.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                //entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Recipe)
                    .HasForeignKey<Recipe>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recipe_User");
            });

            modelBuilder.Entity<RelRecipeIngredient>(entity =>
            {
                entity.HasKey(e => new { e.RecipeId, e.IngredientId });

                entity.ToTable("relRecipeIngredient");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.RelRecipeIngredient)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_relRecipeIngredient_Ingredient");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.RelRecipeIngredient)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_relRecipeIngredient_Recipe");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
