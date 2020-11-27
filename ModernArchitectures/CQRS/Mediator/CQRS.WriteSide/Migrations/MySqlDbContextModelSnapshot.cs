﻿// <auto-generated />
using CQRS.WriteSide.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CQRS.WriteSide.Migrations
{
    [DbContext(typeof(MySqlDbContext))]
    partial class MySqlDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CQRS.WriteSide.Database.ReadModel.PersonDetailsRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("PersonDetails");
                });

            modelBuilder.Entity("CQRS.WriteSide.Database.ReadModel.PersonListItemRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AddressesCount")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("OriginalId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OriginalId");

                    b.ToTable("PersonList");
                });

            modelBuilder.Entity("CQRS.WriteSide.Database.WriteModel.AddressRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AddressType")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Number")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<string>("PostalCode")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Street")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("CQRS.WriteSide.Database.WriteModel.PersonRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("CQRS.WriteSide.Database.WriteModel.AddressRecord", b =>
                {
                    b.HasOne("CQRS.WriteSide.Database.WriteModel.PersonRecord", "Person")
                        .WithMany("Addresses")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
