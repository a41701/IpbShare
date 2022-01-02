using IpbShare.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IpbShare.Infrastructure
{
    public class IpbShareDbContext: DbContext
    {
       
            public DbSet<Categoria> Categorias { get; set; }
            public DbSet<Curso> Cursos { get; set; }
            public DbSet<Equipamento> Equipamentos { get; set; }
            public DbSet <Escola> Escolas { get; set; }
            public DbSet <Pais> Paises { get; set; }
            public DbSet <Reserva> Reservas { get; set; }
            public DbSet <Utilizador> Utilizadores { get; set; }

            public string DbPath { get; private set; }

            public IpbShareDbContext()
            {
                var folder = Environment.SpecialFolder.LocalApplicationData;
                var path = Environment.GetFolderPath(folder);
                DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}IpbShareDA.db";
            }

            protected override void OnConfiguring(DbContextOptionsBuilder options)
            {
                options.UseSqlite($"Data Source = {DbPath}");
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                //Categorias
                modelBuilder.Entity<Categoria>().HasIndex(c => c.NomePais).IsUnique(); // uma categoria tem que ser única
                modelBuilder.Entity<Categoria>().Property(c => c.NomePais)
                    .IsRequired() 
                    .HasMaxLength(256);

                //Equipamentos
                modelBuilder.Entity<Equipamento>().HasIndex(eq => eq.NomeEquipamento).IsUnique(); 
                modelBuilder.Entity<Equipamento>().Property(eq => eq.NomeEquipamento)
                    .IsRequired()
                    .HasMaxLength(256);

                //Escola 
                modelBuilder.Entity<Escola>().HasIndex(e => e.NomeEscola).IsUnique(); 
                modelBuilder.Entity<Escola>().Property(e => e.NomeEscola)
                    .IsRequired()
                    .HasMaxLength(256);

                //Pais 
                modelBuilder.Entity<Pais>().HasIndex(p => p.NomePais).IsUnique();
                modelBuilder.Entity<Pais>().Property(p => p.NomePais)
                    .IsRequired()
                    .HasMaxLength(256);

                //Curso 
                modelBuilder.Entity<Curso>().HasIndex(c => c.NomeCurso).IsUnique();
                modelBuilder.Entity<Curso>().Property(c => c.NomeCurso)
                    .IsRequired()
                    .HasMaxLength(256);

                //Utilizador 
                modelBuilder.Entity<Utilizador>().HasIndex(u => u.Email).IsUnique();
                modelBuilder.Entity<Utilizador>().Property(u => u.Nome)
                    .IsRequired()
                    .HasMaxLength(256);

                //Reserva e FK de Reserva
                modelBuilder.Entity<Reserva>().HasKey(r => new { r.EquipamentoId, r.UserId });
                modelBuilder.Entity<Reserva>()
                    .HasOne (r => r.Utilizador)
                    .WithMany(u => u.ReservaList)
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reserva>()
                    .HasOne (r => r.Equipamento)
                    .WithMany(eq => eq.Reservados)
                    .HasForeignKey(r => r.EquipamentoId)
                    .OnDelete(DeleteBehavior.Restrict);

            //Foreign Key de Categoria na Tabela Equipamentos
            modelBuilder.Entity<Equipamento>()
                    .HasOne(eq => eq.Categoria) //1 Equipamento - 1 Categoria
                    .WithMany(c => c.Equipamentos) //1 Categoria - n Equipamentos
                    .HasForeignKey(eq => eq.CategoriaId)
                    .OnDelete(DeleteBehavior.Restrict);

                //Foreign Key de Escola na Tabela Equipamentos
                modelBuilder.Entity<Equipamento>()
                    .HasOne(eq => eq.Escola) //1 Equipamento - 1 Escola
                    .WithMany(e => e.Equipamentos) //1 Escola - n Equipamentos
                    .HasForeignKey(eq => eq.EscolaId)
                    .OnDelete(DeleteBehavior.Restrict);

                //Foreign Key de Escola na Tabela Curso
                modelBuilder.Entity<Curso>()
                    .HasOne(c => c.Escola) //1 Curso - 1 Escola
                    .WithMany(e => e.CursoList) //1 Escola - n Cursos
                    .HasForeignKey(c => c.EscolaId)
                    .OnDelete(DeleteBehavior.Restrict);

                //Foreign Key de Curso na Tabela Utilizador
                modelBuilder.Entity<Utilizador>()
                    .HasOne(u => u.Curso) //1 Utilizador - 1 Curso
                    .WithMany(c => c.UtilizadorList) //1 Curso - n Utilizadores
                    .HasForeignKey(u => u.CursoId)
                    .OnDelete(DeleteBehavior.Restrict);

                //Foreign Key de Pais na Tabela Utilizador
                modelBuilder.Entity<Utilizador> ()
                    .HasOne(u => u.Pais) //1 Utilizador - 1 País
                    .WithMany(p => p.UtilizadorList) // 1 País - n Utilizadores
                    .HasForeignKey(u => u.PaisId)
                    .OnDelete(DeleteBehavior.Restrict);

                 //Inserir dados nas tabelas Pais, Escola e Curso

                modelBuilder.Entity<Pais>().HasData(
                    new Pais { Id = 1, NomePais = "Portugal"});


                modelBuilder.Entity<Escola>().HasData(
                    new Escola { Id = 1, NomeEscola = "ESTIG"});

                modelBuilder.Entity<Curso>().HasData(
                    new Curso { Id = 1, EscolaId = 1, NomeCurso = "EI"});

                //Admin Injection
                modelBuilder.Entity<Utilizador>().HasData(
                        new Utilizador { Id = 1, Nome = "admin", Password = "admin", Email= "admin@ipb.pt", IsAdministrator = true, PaisId = 1, CursoId = 1});
            }
        }
    }
