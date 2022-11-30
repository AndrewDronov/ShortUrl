using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ShortUrl.Models
{
    public partial class ShortUrlContext : DbContext
    {
        public ShortUrlContext()
        {
        }

        public ShortUrlContext(DbContextOptions<ShortUrlContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Url> Urls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("server=localhost;port=5432;database=short_url;user id=postgres;password=;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "ru_RU.UTF-8");

            modelBuilder.Entity<Url>(entity =>
            {
                entity.HasKey(e => e.Token)
                    .HasName("url_pk");

                entity.ToTable("url");

                entity.Property(e => e.Token)
                    .HasMaxLength(8)
                    .HasColumnName("token")
                    .HasDefaultValueSql("\"substring\"(md5((random())::text), 0, 9)");

                entity.Property(e => e.Link)
                    .IsRequired()
                    .HasColumnName("link");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
