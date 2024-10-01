﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using Project3.Context;

#nullable disable

namespace Project3.Migrations
{
    [DbContext(typeof(LocalDbContext))]
    [Migration("20240622191929_First")]
    partial class First
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Project1.Models.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClientId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClientId");

                    b.ToTable("Clients");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Client");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Project1.Models.Contract", b =>
                {
                    b.Property<int>("ContractId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContractId"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdClient")
                        .HasColumnType("int");

                    b.Property<int>("IdSoftwareSystem")
                        .HasColumnType("int");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSigned")
                        .HasColumnType("bit");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SupportedYears")
                        .HasColumnType("int");

                    b.HasKey("ContractId");

                    b.HasIndex("IdClient");

                    b.HasIndex("IdSoftwareSystem");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("Project1.Models.Discount", b =>
                {
                    b.Property<int>("DiscountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DiscountId"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("DiscountId");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("Project1.Models.SoftwareSystem", b =>
                {
                    b.Property<int>("SoftwareSystemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SoftwareSystemId"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SoftwareSystemId");

                    b.ToTable("SoftwareSystems");
                });

            modelBuilder.Entity("Project1.Models.SoftwareSystem_Discount", b =>
                {
                    b.Property<int>("IdDiscount")
                        .HasColumnType("int");

                    b.Property<int>("IdSoftwareSystem")
                        .HasColumnType("int");

                    b.HasKey("IdDiscount", "IdSoftwareSystem");

                    b.HasIndex("IdSoftwareSystem");

                    b.ToTable("SoftwareSystemDiscounts");
                });

            modelBuilder.Entity("Project1.Models.Company", b =>
                {
                    b.HasBaseType("Project1.Models.Client");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KRS")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Company");
                });

            modelBuilder.Entity("Project1.Models.Individual", b =>
                {
                    b.HasBaseType("Project1.Models.Client");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PESEL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("Individual");
                });

            modelBuilder.Entity("Project1.Models.Contract", b =>
                {
                    b.HasOne("Project1.Models.Client", "Client")
                        .WithMany("Contracts")
                        .HasForeignKey("IdClient")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project1.Models.SoftwareSystem", "SoftwareSystem")
                        .WithMany("Contracts")
                        .HasForeignKey("IdSoftwareSystem")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("SoftwareSystem");
                });

            modelBuilder.Entity("Project1.Models.SoftwareSystem_Discount", b =>
                {
                    b.HasOne("Project1.Models.Discount", "Discount")
                        .WithMany("SoftwareSystemDiscounts")
                        .HasForeignKey("IdDiscount")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project1.Models.SoftwareSystem", "SoftwareSystem")
                        .WithMany("SoftwareSystemDiscounts")
                        .HasForeignKey("IdSoftwareSystem")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Discount");

                    b.Navigation("SoftwareSystem");
                });

            modelBuilder.Entity("Project1.Models.Client", b =>
                {
                    b.Navigation("Contracts");
                });

            modelBuilder.Entity("Project1.Models.Discount", b =>
                {
                    b.Navigation("SoftwareSystemDiscounts");
                });

            modelBuilder.Entity("Project1.Models.SoftwareSystem", b =>
                {
                    b.Navigation("Contracts");

                    b.Navigation("SoftwareSystemDiscounts");
                });
#pragma warning restore 612, 618
        }
    }
}
