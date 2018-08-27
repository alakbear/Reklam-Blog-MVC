namespace ReklamEvMVCProject.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
            : base("name=MyDbContext")
        {
        }

        public virtual DbSet<CATEGORY> CATEGORY { get; set; }
        public virtual DbSet<PHOTO> PHOTO { get; set; }
        public virtual DbSet<PRODUCT> PRODUCT { get; set; }
        public virtual DbSet<USERS> USERS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CATEGORY>()
                .HasMany(e => e.PRODUCT)
                .WithOptional(e => e.CATEGORY)
                .HasForeignKey(e => e.CATEGORY_ID);

            modelBuilder.Entity<USERS>()
                .HasMany(e => e.PRODUCT)
                .WithOptional(e => e.USERS)
                .HasForeignKey(e => e.USER_ID);
        }
    }
}
