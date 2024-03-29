﻿// <auto-generated />
using System;
using System.Collections.Generic;
using MasterclassApiTest.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MasterclassApiTest.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MasterclassApiTest.Entities.Klant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Achternaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Bsn")
                        .HasColumnType("int");

                    b.Property<string>("DisplayNaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("GeboorteDatum")
                        .HasColumnType("datetime2");

                    b.Property<string>("Geslacht")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LaatstIngelogd")
                        .HasColumnType("datetime2");

                    b.Property<string>("LoginNaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("OverlijdensDatum")
                        .HasColumnType("datetime2");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelefoonNummer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Voorletters")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Wachtwoord")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ComplexProperty<Dictionary<string, object>>("Adres", "MasterclassApiTest.Entities.Klant.Adres#Adres", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Huisnummer")
                                .HasColumnType("int");

                            b1.Property<string>("HuisnummerToevoeging")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Postcode")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Straat")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Woonplaats")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");
                        });

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Klanten");
                });

            modelBuilder.Entity("MasterclassApiTest.Entities.Medewerker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DisplayNaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LaatstIngelogd")
                        .HasColumnType("datetime2");

                    b.Property<string>("LoginNaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Wachtwoord")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Medewerkers");
                });

            modelBuilder.Entity("MasterclassApiTest.Entities.Overboeking", b =>
                {
                    b.Property<int>("VolgNummer")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VolgNummer"));

                    b.Property<int>("Bedrag")
                        .HasColumnType("int");

                    b.Property<DateTime>("BoekDatum")
                        .HasColumnType("datetime2");

                    b.Property<string>("TegenRekening")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VanRekening")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VolgNummer");

                    b.ToTable("Overboekingen");
                });

            modelBuilder.Entity("MasterclassApiTest.Entities.Rekening", b =>
                {
                    b.Property<string>("RekeningNummer")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("BeginDatum")
                        .HasColumnType("datetime2");

                    b.Property<int>("KlantNummer")
                        .HasColumnType("int");

                    b.Property<string>("Saldo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RekeningNummer");

                    b.HasIndex("KlantNummer");

                    b.ToTable("Rekeningen");
                });

            modelBuilder.Entity("MasterclassApiTest.Entities.Rekening", b =>
                {
                    b.HasOne("MasterclassApiTest.Entities.Klant", "Klant")
                        .WithMany("Rekeningen")
                        .HasForeignKey("KlantNummer")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Klant");
                });

            modelBuilder.Entity("MasterclassApiTest.Entities.Klant", b =>
                {
                    b.Navigation("Rekeningen");
                });
#pragma warning restore 612, 618
        }
    }
}
