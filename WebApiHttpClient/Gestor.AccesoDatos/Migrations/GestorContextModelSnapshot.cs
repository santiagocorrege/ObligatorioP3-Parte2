﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Gestor.AccesoDatos.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gestor.AccesoDatos.Migrations
{
    [DbContext(typeof(GestorContext))]
    partial class GestorContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Gestor.LogicaNegocio.Articulos.Articulo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("PrecioActual")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.HasIndex("Codigo")
                        .IsUnique();

                    b.HasIndex("Nombre")
                        .IsUnique();

                    b.ToTable("Articulos");
                });

            modelBuilder.Entity("Gestor.LogicaNegocio.Entidades.MovimientoStock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArticuloId")
                        .HasColumnType("int");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaMovimiento")
                        .HasColumnType("Date");

                    b.Property<int>("TipoMovimientoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArticuloId");

                    b.HasIndex("TipoMovimientoId");

                    b.ToTable("MovimientosStock");
                });

            modelBuilder.Entity("Gestor.LogicaNegocio.Entidades.Parametro", b =>
                {
                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Valor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Nombre");

                    b.ToTable("Parametros");
                });

            modelBuilder.Entity("Gestor.LogicaNegocio.Entidades.TipoMovimiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Coeficiente")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Nombre")
                        .IsUnique();

                    b.ToTable("TiposMovimientos");
                });

            modelBuilder.Entity("SistemaAutenticacion.Entidades.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ComplexProperty<Dictionary<string, object>>("Password", "SistemaAutenticacion.Entidades.Usuario.Password#Password", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Encriptada")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Valor")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Usuarios");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Usuario");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("SistemaAutenticacion.Entidades.Administrador", b =>
                {
                    b.HasBaseType("SistemaAutenticacion.Entidades.Usuario");

                    b.HasDiscriminator().HasValue("Administrador");
                });

            modelBuilder.Entity("SistemaAutenticacion.Entidades.Encargado", b =>
                {
                    b.HasBaseType("SistemaAutenticacion.Entidades.Usuario");

                    b.HasDiscriminator().HasValue("Encargado");
                });

            modelBuilder.Entity("Gestor.LogicaNegocio.Entidades.MovimientoStock", b =>
                {
                    b.HasOne("Gestor.LogicaNegocio.Articulos.Articulo", "Articulo")
                        .WithMany("HistorialMovimientos")
                        .HasForeignKey("ArticuloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gestor.LogicaNegocio.Entidades.TipoMovimiento", "TipoMovimiento")
                        .WithMany()
                        .HasForeignKey("TipoMovimientoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Articulo");

                    b.Navigation("TipoMovimiento");
                });

            modelBuilder.Entity("Gestor.LogicaNegocio.Articulos.Articulo", b =>
                {
                    b.Navigation("HistorialMovimientos");
                });
#pragma warning restore 612, 618
        }
    }
}