﻿// <auto-generated />
using System;
using McU.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace McU.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230209142153_UserCharacterRelationShip")]
    partial class UserCharacterRelationShip
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("McU.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Class")
                        .HasColumnType("integer");

                    b.Property<int>("Defense")
                        .HasColumnType("integer");

                    b.Property<int>("HitPoints")
                        .HasColumnType("integer");

                    b.Property<int>("Intelligence")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Strength")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("McU.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("McU.Models.Character", b =>
                {
                    b.HasOne("McU.Models.User", null)
                        .WithMany("Characters")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("McU.Models.User", b =>
                {
                    b.Navigation("Characters");
                });
#pragma warning restore 612, 618
        }
    }
}
