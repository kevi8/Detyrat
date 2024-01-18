﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Profile.Models;

#nullable disable

namespace Profile.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Profile.Models.Ftesa", b =>
                {
                    b.Property<int>("FtesaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("NetId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("FtesaId");

                    b.HasIndex("NetId");

                    b.HasIndex("UserId");

                    b.ToTable("Ftesat");
                });

            modelBuilder.Entity("Profile.Models.Net", b =>
                {
                    b.Property<int>("NetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("NetId");

                    b.HasIndex("UserId");

                    b.ToTable("Nets");
                });

            modelBuilder.Entity("Profile.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Profile.Models.Ftesa", b =>
                {
                    b.HasOne("Profile.Models.Net", "Nets")
                        .WithMany("ftesat")
                        .HasForeignKey("NetId");

                    b.HasOne("Profile.Models.User", "IFtuari")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("IFtuari");

                    b.Navigation("Nets");
                });

            modelBuilder.Entity("Profile.Models.Net", b =>
                {
                    b.HasOne("Profile.Models.User", "Creator")
                        .WithMany("Nets")
                        .HasForeignKey("UserId");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Profile.Models.Net", b =>
                {
                    b.Navigation("ftesat");
                });

            modelBuilder.Entity("Profile.Models.User", b =>
                {
                    b.Navigation("Nets");
                });
#pragma warning restore 612, 618
        }
    }
}
