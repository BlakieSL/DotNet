﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using preparation_test_2.Context;

#nullable disable

namespace preparation_test_2.Migrations
{
    [DbContext(typeof(BoatReservationServiceDbContext))]
    partial class BoatReservationServiceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("preparation_test_2.Models.BoatStandard", b =>
                {
                    b.Property<int>("IdBoatStandard")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdBoatStandard"));

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdBoatStandard");

                    b.ToTable("BoatStandards");
                });

            modelBuilder.Entity("preparation_test_2.Models.Client", b =>
                {
                    b.Property<int>("IdClient")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdClient"));

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("IdClientCategory")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Pesel")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdClient");

                    b.HasIndex("IdClientCategory");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("preparation_test_2.Models.ClientCategory", b =>
                {
                    b.Property<int>("IdClientCategory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdClientCategory"));

                    b.Property<int>("DiscountPerc")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdClientCategory");

                    b.ToTable("ClientCategories");
                });

            modelBuilder.Entity("preparation_test_2.Models.Reservation", b =>
                {
                    b.Property<int>("IdReservation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdReservation"));

                    b.Property<string>("CancelReason")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTo")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Fulfilled")
                        .HasColumnType("bit");

                    b.Property<int>("IdBoatStandard")
                        .HasColumnType("int");

                    b.Property<int>("IdClient")
                        .HasColumnType("int");

                    b.Property<int>("NumOfBoats")
                        .HasColumnType("int");

                    b.Property<float?>("Price")
                        .HasColumnType("real");

                    b.HasKey("IdReservation");

                    b.HasIndex("IdBoatStandard");

                    b.HasIndex("IdClient");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("preparation_test_2.Models.Sailboat", b =>
                {
                    b.Property<int>("IdSailboat")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSailboat"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("IdBoatStandard")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("IdSailboat");

                    b.HasIndex("IdBoatStandard");

                    b.ToTable("Sailboats");
                });

            modelBuilder.Entity("preparation_test_2.Models.Sailboat_Reservation", b =>
                {
                    b.Property<int>("IdReservation")
                        .HasColumnType("int");

                    b.Property<int>("IdSailboat")
                        .HasColumnType("int");

                    b.HasKey("IdReservation", "IdSailboat");

                    b.HasIndex("IdSailboat");

                    b.ToTable("SailboatReservations");
                });

            modelBuilder.Entity("preparation_test_2.Models.Client", b =>
                {
                    b.HasOne("preparation_test_2.Models.ClientCategory", "ClientCategory")
                        .WithMany("Clients")
                        .HasForeignKey("IdClientCategory")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClientCategory");
                });

            modelBuilder.Entity("preparation_test_2.Models.Reservation", b =>
                {
                    b.HasOne("preparation_test_2.Models.BoatStandard", "BoatStandard")
                        .WithMany("Reservations")
                        .HasForeignKey("IdBoatStandard")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("preparation_test_2.Models.Client", "Client")
                        .WithMany("Reservations")
                        .HasForeignKey("IdClient")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BoatStandard");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("preparation_test_2.Models.Sailboat", b =>
                {
                    b.HasOne("preparation_test_2.Models.BoatStandard", "BoatStandard")
                        .WithMany("Sailboats")
                        .HasForeignKey("IdBoatStandard")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BoatStandard");
                });

            modelBuilder.Entity("preparation_test_2.Models.Sailboat_Reservation", b =>
                {
                    b.HasOne("preparation_test_2.Models.Reservation", "Reservation")
                        .WithMany("SailboatReservations")
                        .HasForeignKey("IdReservation")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("preparation_test_2.Models.Sailboat", "Sailboat")
                        .WithMany("SailboatReservations")
                        .HasForeignKey("IdSailboat")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Reservation");

                    b.Navigation("Sailboat");
                });

            modelBuilder.Entity("preparation_test_2.Models.BoatStandard", b =>
                {
                    b.Navigation("Reservations");

                    b.Navigation("Sailboats");
                });

            modelBuilder.Entity("preparation_test_2.Models.Client", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("preparation_test_2.Models.ClientCategory", b =>
                {
                    b.Navigation("Clients");
                });

            modelBuilder.Entity("preparation_test_2.Models.Reservation", b =>
                {
                    b.Navigation("SailboatReservations");
                });

            modelBuilder.Entity("preparation_test_2.Models.Sailboat", b =>
                {
                    b.Navigation("SailboatReservations");
                });
#pragma warning restore 612, 618
        }
    }
}