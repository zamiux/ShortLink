using Microsoft.EntityFrameworkCore;
using ShortLink.Domain.Models.Account;
using ShortLink.Domain.Models.Link;
using System.Linq;

namespace ShortLink.Infra.Data.Context
{
    public class ShortLinkDbContext : DbContext
    {
        #region Ctor
        public ShortLinkDbContext(DbContextOptions<ShortLinkDbContext> options):base(options) { }
        #endregion

        #region DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Brower> Browers { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Os> Os { get; set; }
        public DbSet<RequestUrl> RequestUrls { get; set; }
        public DbSet<ShortUrl> ShortUrls { get; set; }
        #endregion

        #region On Model Creating - Delete Restrict
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relation in modelBuilder.Model.GetEntityTypes()
                .SelectMany(s=>s.GetForeignKeys())
                )
            {
                relation.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
