﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RpgApi.Data;

namespace RpgApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20211106161038_MigracaoPersonagemHabilidade")]
    partial class MigracaoPersonagemHabilidade
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RpgApi.Models.Arma", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Dano")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Personagem")
                        .HasColumnType("int");

                    b.Property<int>("PersonagemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonagemId")
                        .IsUnique();

                    b.ToTable("Armas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Dano = 50,
                            Nome = "Arco e Flecha",
                            Personagem = 0,
                            PersonagemId = 1
                        },
                        new
                        {
                            Id = 2,
                            Dano = 60,
                            Nome = "Bardiche",
                            Personagem = 0,
                            PersonagemId = 2
                        },
                        new
                        {
                            Id = 3,
                            Dano = 55,
                            Nome = "Espada",
                            Personagem = 0,
                            PersonagemId = 5
                        },
                        new
                        {
                            Id = 4,
                            Dano = 55,
                            Nome = "Grosses Messer",
                            Personagem = 0,
                            PersonagemId = 3
                        },
                        new
                        {
                            Id = 5,
                            Dano = 50,
                            Nome = "Adaga",
                            Personagem = 0,
                            PersonagemId = 6
                        });
                });

            modelBuilder.Entity("RpgApi.Models.Disputa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AtacanteId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataDisputa")
                        .HasColumnType("datetime2");

                    b.Property<string>("Narracao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OponenteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Disputas");
                });

            modelBuilder.Entity("RpgApi.Models.Habilidade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Dano")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Habilidades");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Dano = 70,
                            Nome = "Drenar Energia"
                        },
                        new
                        {
                            Id = 2,
                            Dano = 55,
                            Nome = "Envenenar"
                        },
                        new
                        {
                            Id = 3,
                            Dano = 60,
                            Nome = "Investida Poderosa"
                        },
                        new
                        {
                            Id = 4,
                            Dano = 50,
                            Nome = "Sangramento"
                        },
                        new
                        {
                            Id = 5,
                            Dano = 65,
                            Nome = "Percepção às Cegas"
                        });
                });

            modelBuilder.Entity("RpgApi.Models.Personagem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Classe")
                        .HasColumnType("int");

                    b.Property<int>("Defesa")
                        .HasColumnType("int");

                    b.Property<int>("Derrotas")
                        .HasColumnType("int");

                    b.Property<int>("Disputas")
                        .HasColumnType("int");

                    b.Property<int>("Forca")
                        .HasColumnType("int");

                    b.Property<byte[]>("FotoPersonagem")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("Inteligencia")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PontosVida")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<int>("Vitorias")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Personagens");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Classe = 1,
                            Defesa = 100,
                            Derrotas = 0,
                            Disputas = 0,
                            Forca = 150,
                            Inteligencia = 55,
                            Nome = "Frodo",
                            PontosVida = 100,
                            Vitorias = 0
                        },
                        new
                        {
                            Id = 2,
                            Classe = 1,
                            Defesa = 150,
                            Derrotas = 0,
                            Disputas = 0,
                            Forca = 200,
                            Inteligencia = 60,
                            Nome = "Sam",
                            PontosVida = 100,
                            Vitorias = 0
                        },
                        new
                        {
                            Id = 3,
                            Classe = 3,
                            Defesa = 50,
                            Derrotas = 0,
                            Disputas = 0,
                            Forca = 100,
                            Inteligencia = 65,
                            Nome = "Galadriel",
                            PontosVida = 100,
                            Vitorias = 0
                        },
                        new
                        {
                            Id = 4,
                            Classe = 2,
                            Defesa = 100,
                            Derrotas = 0,
                            Disputas = 0,
                            Forca = 130,
                            Inteligencia = 50,
                            Nome = "Gandalf",
                            PontosVida = 100,
                            Vitorias = 0
                        },
                        new
                        {
                            Id = 5,
                            Classe = 1,
                            Defesa = 150,
                            Derrotas = 0,
                            Disputas = 0,
                            Forca = 200,
                            Inteligencia = 55,
                            Nome = "Hobbit",
                            PontosVida = 100,
                            Vitorias = 0
                        },
                        new
                        {
                            Id = 6,
                            Classe = 3,
                            Defesa = 60,
                            Derrotas = 0,
                            Disputas = 0,
                            Forca = 110,
                            Inteligencia = 60,
                            Nome = "Celeborn",
                            PontosVida = 100,
                            Vitorias = 0
                        },
                        new
                        {
                            Id = 7,
                            Classe = 2,
                            Defesa = 130,
                            Derrotas = 0,
                            Disputas = 0,
                            Forca = 165,
                            Inteligencia = 60,
                            Nome = "Radagast",
                            PontosVida = 100,
                            Vitorias = 0
                        });
                });

            modelBuilder.Entity("RpgApi.Models.PersonagemHabilidade", b =>
                {
                    b.Property<int>("PersonagemId")
                        .HasColumnType("int");

                    b.Property<int>("HabilidadeId")
                        .HasColumnType("int");

                    b.HasKey("PersonagemId", "HabilidadeId");

                    b.HasIndex("HabilidadeId");

                    b.ToTable("PersonagemHabilidades");
                });

            modelBuilder.Entity("RpgApi.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataAcesso")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Foto")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Latitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Longitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("MyProperty")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Pass")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Perfil")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Jogador");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("RpgApi.Models.Arma", b =>
                {
                    b.HasOne("RpgApi.Models.Personagem", "Personagens")
                        .WithOne("Arma")
                        .HasForeignKey("RpgApi.Models.Arma", "PersonagemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Personagens");
                });

            modelBuilder.Entity("RpgApi.Models.Personagem", b =>
                {
                    b.HasOne("RpgApi.Models.Usuario", "Usuario")
                        .WithMany("Personagens")
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("RpgApi.Models.PersonagemHabilidade", b =>
                {
                    b.HasOne("RpgApi.Models.Habilidade", "Habilidade")
                        .WithMany()
                        .HasForeignKey("HabilidadeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RpgApi.Models.Personagem", "Personagem")
                        .WithMany("PersonagemHabilidades")
                        .HasForeignKey("PersonagemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Habilidade");

                    b.Navigation("Personagem");
                });

            modelBuilder.Entity("RpgApi.Models.Personagem", b =>
                {
                    b.Navigation("Arma");

                    b.Navigation("PersonagemHabilidades");
                });

            modelBuilder.Entity("RpgApi.Models.Usuario", b =>
                {
                    b.Navigation("Personagens");
                });
#pragma warning restore 612, 618
        }
    }
}
