using SeoManager.Entities;
using System.Data.Entity;

namespace SeoManager.Dal
{
    public class ApplicationContext : DbContext
    {
        private const string ConnectionString = @"Data Source=HIEU_LENOVO_LAP\SQLEXPRESS;Initial Catalog=SeoManager;User ID=sa;Password=12345678";
        public ApplicationContext() : base(ConnectionString)
        {

        }
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<VaiTro> VaiTros { get; set; }
        public DbSet<Domain> Domains { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<LinkAndWord> LinkAndWords { get; set; }
        public DbSet<BackLinkAndWord> BackLinkAndWords { get; set; }
        public DbSet<History> Historys { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NguoiDung>().ToTable("NguoiDung");
            modelBuilder.Entity<VaiTro>().ToTable("VaiTro");
            modelBuilder.Entity<Domain>().ToTable("Domain");
            modelBuilder.Entity<Link>().ToTable("Link");
            modelBuilder.Entity<Word>().ToTable("Word");
            modelBuilder.Entity<LinkAndWord>().ToTable("LinkAndWord");
            modelBuilder.Entity<BackLinkAndWord>().ToTable("BackLinkAndWord");
            modelBuilder.Entity<History>().ToTable("History");


            modelBuilder.Entity<BackLinkAndWord>().Property(p => p.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            //modelBuilder.Entity<BackLinkAndWord>().HasRequired(t => t.Link).WithMany(t=>t.BacklinkAndWord).HasForeignKey(t => new { t.Id, t.LinkId, t.LinkToId, t.WordId });
            //modelBuilder.Entity<BackLinkAndWord>().HasRequired(t => t.LinkTo).WithMany(t => t.BacklinkAndWord).HasForeignKey(t => new { t.LinkId, t.LinkToId, t.WordId });
            //modelBuilder.Entity<BackLinkAndWord>().HasRequired(t => t.Word).WithMany(t=>t.BacklinkAndWord).HasForeignKey(t => new { t.LinkId, t.LinkToId, t.WordId });


            base.OnModelCreating(modelBuilder);
        }


    }
}