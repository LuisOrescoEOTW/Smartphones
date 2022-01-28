﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Smartphones.Models;

namespace Smartphones.Migrations
{
    [DbContext(typeof(SmartphoneContext))]
    [Migration("20220128163304_Creo las tablas")]
    partial class Creolastablas
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("SensorTelefono", b =>
                {
                    b.Property<int>("SensoresSensorId")
                        .HasColumnType("int");

                    b.Property<int>("TelefonosTelefonoId")
                        .HasColumnType("int");

                    b.HasKey("SensoresSensorId", "TelefonosTelefonoId");

                    b.HasIndex("TelefonosTelefonoId");

                    b.ToTable("SensorTelefono");
                });

            modelBuilder.Entity("Smartphones.Models.App", b =>
                {
                    b.Property<int>("AppId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.HasKey("AppId");

                    b.ToTable("App");
                });

            modelBuilder.Entity("Smartphones.Models.Instalacion", b =>
                {
                    b.Property<int>("InstalacionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AppId")
                        .HasColumnType("int");

                    b.Property<bool>("Exitosa")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime");

                    b.Property<int>("OperarioId")
                        .HasColumnType("int");

                    b.Property<int>("TelefonoId")
                        .HasColumnType("int");

                    b.HasKey("InstalacionId");

                    b.HasIndex("AppId");

                    b.HasIndex("OperarioId");

                    b.HasIndex("TelefonoId");

                    b.ToTable("Instalacion");
                });

            modelBuilder.Entity("Smartphones.Models.Operario", b =>
                {
                    b.Property<int>("OperarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Apellido")
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.HasKey("OperarioId");

                    b.ToTable("Operario");
                });

            modelBuilder.Entity("Smartphones.Models.Sensor", b =>
                {
                    b.Property<int>("SensorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.HasKey("SensorId");

                    b.ToTable("Sensor");
                });

            modelBuilder.Entity("Smartphones.Models.Telefono", b =>
                {
                    b.Property<int>("TelefonoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Marca")
                        .HasColumnType("text");

                    b.Property<string>("Modelo")
                        .HasColumnType("text");

                    b.Property<float>("Precio")
                        .HasColumnType("float");

                    b.HasKey("TelefonoId");

                    b.ToTable("Telefono");
                });

            modelBuilder.Entity("SensorTelefono", b =>
                {
                    b.HasOne("Smartphones.Models.Sensor", null)
                        .WithMany()
                        .HasForeignKey("SensoresSensorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Smartphones.Models.Telefono", null)
                        .WithMany()
                        .HasForeignKey("TelefonosTelefonoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Smartphones.Models.Instalacion", b =>
                {
                    b.HasOne("Smartphones.Models.App", "App")
                        .WithMany("Instalaciones")
                        .HasForeignKey("AppId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Smartphones.Models.Operario", "Operario")
                        .WithMany("Instalaciones")
                        .HasForeignKey("OperarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Smartphones.Models.Telefono", "Telefono")
                        .WithMany("Instalaciones")
                        .HasForeignKey("TelefonoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("App");

                    b.Navigation("Operario");

                    b.Navigation("Telefono");
                });

            modelBuilder.Entity("Smartphones.Models.App", b =>
                {
                    b.Navigation("Instalaciones");
                });

            modelBuilder.Entity("Smartphones.Models.Operario", b =>
                {
                    b.Navigation("Instalaciones");
                });

            modelBuilder.Entity("Smartphones.Models.Telefono", b =>
                {
                    b.Navigation("Instalaciones");
                });
#pragma warning restore 612, 618
        }
    }
}
