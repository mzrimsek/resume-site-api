﻿// <auto-generated />
using Integration.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Integration.EntityFramework.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20171006165848_AddSkillsTable")]
    partial class AddSkillsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("Integration.EntityFramework.Models.JobDatabaseModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("State")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("Integration.EntityFramework.Models.JobProjectDatabaseModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("JobId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("JobProjects");
                });

            modelBuilder.Entity("Integration.EntityFramework.Models.LanguageDatabaseModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Rating");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Integration.EntityFramework.Models.SchoolDatabaseModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("Degree");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Major")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("State")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("Integration.EntityFramework.Models.SkillDatabaseModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("LanguageId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Rating");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("Integration.EntityFramework.Models.JobProjectDatabaseModel", b =>
                {
                    b.HasOne("Integration.EntityFramework.Models.JobDatabaseModel", "Job")
                        .WithMany()
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Integration.EntityFramework.Models.SkillDatabaseModel", b =>
                {
                    b.HasOne("Integration.EntityFramework.Models.LanguageDatabaseModel", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
