using Microsoft.EntityFrameworkCore;
using NewApi.Models;

namespace NewApi.Data {

    public class NewApiContext : DbContext {
        
        public NewApiContext(DbContextOptions<NewApiContext> options)
            : base(options)
        {
            
        }

        public DbSet<Pessoa> Pessoas { get; set; }

        public DbSet<Grupo> Grupos {get; set;}

         public DbSet<Car> Cars {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoa>().HasKey(p => p.id);

            modelBuilder.Entity<Pessoa>()
                .Property(p => p.nome)
                .HasMaxLength(100);

            modelBuilder.Entity<Pessoa>()
                .Property(p => p.telefone)
                .HasMaxLength(10);
                
            modelBuilder.Entity<Grupo>().HasKey(p => p.id);

            modelBuilder.Entity<Grupo>()
                .Property(p => p.nome)
                .HasMaxLength(100);

            modelBuilder.Entity<Pessoa>()
                .HasOne(p => p.grupo)
                .WithMany(p => p.pessoas);

            modelBuilder.Entity<Car>()
                .HasKey(p => p.id);
        

            base.OnModelCreating(modelBuilder);
        }

    }

}