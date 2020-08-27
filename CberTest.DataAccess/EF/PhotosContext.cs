using CberTest.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CberTest.DataAccess.EF
{
    public class PhotosContext : DbContext, IUnitOfWork
    {
        private EfRepository<Photo, string> photosRepository;

        public PhotosContext(DbContextOptions<PhotosContext> options) : base(options)
        {
            photosRepository = new EfRepository<Photo, string>(Photos);
        }

        public DbSet<Photo> Photos { get; set; }

        public Task<int> CompleteAsync()
        {
            return SaveChangesAsync();
        }

        public IAsyncRepository<Photo, string> PhotoRepository => photosRepository;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photo>().HasKey(p => p.Id);
            modelBuilder.Entity<Photo>().Property(p => p.Id).HasMaxLength(32);
            modelBuilder.Entity<Photo>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<Photo>().Property(p => p.Description).IsRequired();
            modelBuilder.Entity<Photo>().Property(p => p.Created).IsRequired();

            modelBuilder.Entity<Photo>().HasOne(p => p.File).WithOne(p => p.Photo);

            modelBuilder.Entity<MediaFile>().HasIndex(p => p.FileType);
            modelBuilder.Entity<MediaFile>().Property(p => p.FileType).IsRequired();
            modelBuilder.Entity<MediaFile>().Property(p => p.Content).IsRequired();
            modelBuilder.Entity<MediaFile>().Property(p => p.ContentType).IsRequired();
            modelBuilder.Entity<MediaFile>().HasKey(f => f.PhotoId);
            modelBuilder.Entity<MediaFile>().Property(f => f.PhotoId).HasMaxLength(32);

            base.OnModelCreating(modelBuilder);
        }
    }
}
