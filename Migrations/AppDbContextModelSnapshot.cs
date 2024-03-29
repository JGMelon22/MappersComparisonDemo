﻿// <auto-generated />
using MappersWebApiDemo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MappersWebApiDemo.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("MappersWebApiDemo.Models.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("produto_id");

                    b.Property<bool>("Disponivel")
                        .HasColumnType("BOOLEAN")
                        .HasColumnName("disponivel");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("nome");

                    b.Property<float>("Preco")
                        .HasColumnType("REAL")
                        .HasColumnName("preco");

                    b.HasKey("Id")
                        .HasName("pk_produto");

                    b.HasIndex("Id")
                        .HasDatabaseName("idx_produto_id");

                    b.ToTable("produtos", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
