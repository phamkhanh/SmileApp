using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmileApp.Model.Models;

namespace SmileApp.Data
{
    public class SmileAppDbContext : IdentityDbContext<SmileAppUser>
    {
        public SmileAppDbContext() : base("SmileAppConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Error> Errors { set; get; }

        public DbSet<SmileAppGroup> SmileAppGroups { set; get; }
        public DbSet<SmileAppRole> SmileAppRoles { set; get; }
        public DbSet<SmileAppRoleGroup> SmileAppRoleGroups { set; get; }
        public DbSet<SmileAppUserGroup> SmileAppUserGroups { set; get; }

        public static SmileAppDbContext Create()
        {
            return new SmileAppDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId }).ToTable("SmileAppUserRoles");
            builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId).ToTable("SmileAppUserLogins");
            builder.Entity<IdentityRole>().ToTable("SmileAppRoles");
            builder.Entity<IdentityUserClaim>().HasKey(i => i.UserId).ToTable("SmileAppUserClaims");
        }
    }
}