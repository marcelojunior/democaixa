﻿// <auto-generated />
using System;
using Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Api.Migrations
{
    [DbContext(typeof(ContextoApi))]
    partial class ContextoApiModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Dados.ContaCorrente", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Conta")
                        .HasColumnName("conta")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnName("dataalteracao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnName("datacadastro")
                        .HasColumnType("timestamp without time zone");

                    b.Property<double>("Saldo")
                        .HasColumnName("saldo")
                        .HasColumnType("double precision");

                    b.HasKey("Id")
                        .HasName("pk_contas");

                    b.ToTable("contas");
                });
#pragma warning restore 612, 618
        }
    }
}
