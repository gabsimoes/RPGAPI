using Microsoft.EntityFrameworkCore;
using RpgApi.Models;
using RpgApi.Models.Enums;

namespace RpgApi.Data
{
    public class DataContext : DbContext //mapeia os DBs
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Personagem> Personagens { get; set; } //mapeia as tabelas no DB (como se fosse uma rota)
        public DbSet<Arma> Armas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Habilidade> Habilidades { get; set; }
        public DbSet<PersonagemHabilidade> PersonagemHabilidades { get; set; }

        public DbSet<Disputa> Disputas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Personagem>().HasData
            (
                new Personagem() { Id = 1, Nome = "Frodo", PontosVida=100, Forca=150, Defesa=100, Inteligencia=55, Classe=ClasseEnum.Cavaleiro}, 
                new Personagem() { Id = 2, Nome = "Sam", PontosVida=100, Forca=200, Defesa=150, Inteligencia=60, Classe=ClasseEnum.Cavaleiro},    
                new Personagem() { Id = 3, Nome = "Galadriel", PontosVida=100, Forca=100, Defesa=50, Inteligencia=65, Classe=ClasseEnum.Clerigo },
                new Personagem() { Id = 4, Nome = "Gandalf", PontosVida=100, Forca=130, Defesa=100, Inteligencia=50, Classe=ClasseEnum.Mago },
                new Personagem() { Id = 5, Nome = "Hobbit", PontosVida=100, Forca=200, Defesa=150, Inteligencia=55, Classe=ClasseEnum.Cavaleiro },
                new Personagem() { Id = 6, Nome = "Celeborn", PontosVida=100, Forca=110, Defesa=60, Inteligencia=60, Classe=ClasseEnum.Clerigo },
                new Personagem() { Id = 7, Nome = "Radagast", PontosVida=100, Forca=165, Defesa=130, Inteligencia=60, Classe=ClasseEnum.Mago }

            );

            modelBuilder.Entity<Arma>().HasData
            (
                new Arma() { Id = 1, Nome = "Arco e Flecha", Dano = 50, PersonagemId = 1 },
                new Arma() { Id = 2, Nome = "Bardiche", Dano = 60, PersonagemId = 2 },
                new Arma() { Id = 3, Nome = "Espada", Dano = 55, PersonagemId = 5 },
                new Arma() { Id = 4, Nome = "Grosses Messer", Dano = 55, PersonagemId = 3},
                new Arma() { Id = 5, Nome = "Adaga", Dano = 50, PersonagemId = 6}
            );

            modelBuilder.Entity<PersonagemHabilidade>() 
                .HasKey(ph => new {ph.PersonagemId, ph.HabilidadeId}); //haskey indica que há um relacionamento
            
            modelBuilder.Entity<Habilidade>().HasData
            (
                new Habilidade() { Id = 1, Nome = "Drenar Energia", Dano = 70 },
                new Habilidade() { Id = 2, Nome = "Envenenar", Dano = 55 },
                new Habilidade() { Id = 3, Nome = "Investida Poderosa", Dano = 60 },
                new Habilidade() { Id = 4, Nome = "Sangramento", Dano = 50 },
                new Habilidade() { Id = 5, Nome = "Percepção às Cegas", Dano = 65 }
            );

            modelBuilder.Entity<Usuario>().Property(u => u.Perfil).HasDefaultValue("Jogador");
        }

    }

}