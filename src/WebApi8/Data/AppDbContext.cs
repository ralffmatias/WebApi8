using Microsoft.EntityFrameworkCore;
using WebApi8.Models;

namespace WebApi8.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 

        }

        public DbSet<AutorModel> autores { get; set; }
        public DbSet<LivroModel> livros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<AutorModel>(entity =>
            {
                entity.HasKey(e => e.id_autor);

                entity.Property(e => e.nm_autor).HasColumnType("varchar(150)").IsRequired();
            });

            modelBuilder.Entity<LivroModel>(entity =>
            {
                entity.HasKey(e => e.id_livro);

                entity.Property(e => e.nm_titulo).HasColumnType("varchar(150)").IsRequired();

                entity.HasOne(e => e.Autor).WithMany(a => a.Livros).HasForeignKey(b => b.id_autor);
            });
        }

    }
}
