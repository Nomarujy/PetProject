﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Portfolio.Data.Context;

#nullable disable

namespace Portfolio.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Portfolio.Areas.News.Models.Post.PostModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AuthorEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EditedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPubleched")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("PubleshedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorEmail");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Portfolio.Models.Authorization.Group", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Name");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Portfolio.Models.Authorization.Permision", b =>
                {
                    b.Property<string>("Category")
                        .HasColumnType("text");

                    b.Property<bool>("Create")
                        .HasColumnType("boolean");

                    b.Property<bool>("Delete")
                        .HasColumnType("boolean");

                    b.Property<bool>("Read")
                        .HasColumnType("boolean");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<bool>("Update")
                        .HasColumnType("boolean");

                    b.HasKey("Category");

                    b.HasIndex("RoleId");

                    b.ToTable("Permisions");
                });

            modelBuilder.Entity("Portfolio.Models.Authorization.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("GroupName")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("GroupName");

                    b.HasIndex("Name");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "User"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("Portfolio.Models.Authorization.User", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("Baned")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Email");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Portfolio.Models.Contact.ContactModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("PK_Id");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("Portfolio.Areas.News.Models.Post.PostModel", b =>
                {
                    b.HasOne("Portfolio.Models.Authorization.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Portfolio.Models.Authorization.Permision", b =>
                {
                    b.HasOne("Portfolio.Models.Authorization.Role", "Role")
                        .WithMany("Permisions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Portfolio.Models.Authorization.Role", b =>
                {
                    b.HasOne("Portfolio.Models.Authorization.Group", "Group")
                        .WithMany("Roles")
                        .HasForeignKey("GroupName");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Portfolio.Models.Authorization.User", b =>
                {
                    b.HasOne("Portfolio.Models.Authorization.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Portfolio.Models.Authorization.Group", b =>
                {
                    b.Navigation("Roles");
                });

            modelBuilder.Entity("Portfolio.Models.Authorization.Role", b =>
                {
                    b.Navigation("Permisions");
                });
#pragma warning restore 612, 618
        }
    }
}
