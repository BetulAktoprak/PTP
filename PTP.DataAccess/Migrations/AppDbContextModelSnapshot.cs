﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PTP.DataAccess;

#nullable disable

namespace PTP.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PTP.EntityLayer.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("PersonnelId")
                        .HasColumnType("int");

                    b.Property<int>("ProcessId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PersonnelId");

                    b.HasIndex("ProcessId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("PTP.EntityLayer.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("PTP.EntityLayer.Models.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DocumentDescriptions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int?>("PersonnelId")
                        .HasColumnType("int");

                    b.Property<int?>("ProcessId")
                        .HasColumnType("int");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonnelId");

                    b.HasIndex("ProcessId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("PTP.EntityLayer.Models.Personnel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Personnels");
                });

            modelBuilder.Entity("PTP.EntityLayer.Models.Process", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int?>("PersonnelId")
                        .HasColumnType("int");

                    b.Property<int>("ProcessStageId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PersonnelId");

                    b.HasIndex("ProcessStageId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Processes");
                });

            modelBuilder.Entity("PTP.EntityLayer.Models.ProcessPersonnel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AssignedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("PersonnelId")
                        .HasColumnType("int");

                    b.Property<int>("ProcessId")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PersonnelId");

                    b.HasIndex("ProcessId");

                    b.ToTable("ProcessPersonnels");
                });

            modelBuilder.Entity("PTP.EntityLayer.Models.ProcessStage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ColorHex")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProcessStages");
                });

            modelBuilder.Entity("PTP.EntityLayer.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClientName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Priority")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProcessStageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("ProjectRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProjectSize")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("PTP.EntityLayer.Models.ProjectPersonnel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("CanComment")
                        .HasColumnType("bit");

                    b.Property<bool>("CanCreate")
                        .HasColumnType("bit");

                    b.Property<bool>("CanDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("CanRead")
                        .HasColumnType("bit");

                    b.Property<bool>("CanUpdate")
                        .HasColumnType("bit");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("PersonnelId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PersonnelId");

                    b.HasIndex("ProjectId", "PersonnelId")
                        .IsUnique();

                    b.ToTable("ProjectPersonnels");
                });

            modelBuilder.Entity("PTP.EntityLayer.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PersonnelProject", b =>
                {
                    b.Property<int>("PersonnelsId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectsId")
                        .HasColumnType("int");

                    b.HasKey("PersonnelsId", "ProjectsId");

                    b.HasIndex("ProjectsId");

                    b.ToTable("PersonnelProject");
                });

            modelBuilder.Entity("PTP.EntityLayer.Models.Comment", b =>
                {
                    b.HasOne("PTP.EntityLayer.Models.Personnel", "Personnel")
                        .WithMany()
                        .HasForeignKey("PersonnelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PTP.EntityLayer.Models.Process", "Process")
                        .WithMany("Comments")
                        .HasForeignKey("ProcessId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Personnel");

                    b.Navigation("Process");
                });

            modelBuilder.Entity("PTP.EntityLayer.Models.Customer", b =>
                {
                    b.HasOne("PTP.EntityLayer.Models.User", "User")
                        .WithOne("Customer")
                        .HasForeignKey("PTP.EntityLayer.Models.Customer", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PTP.EntityLayer.Models.Document", b =>
                {
                    b.HasOne("PTP.EntityLayer.Models.Personnel", "Personnel")
                        .WithMany()
                        .HasForeignKey("PersonnelId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("PTP.EntityLayer.Models.Process", "Process")
                        .WithMany("Documents")
                        .HasForeignKey("ProcessId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("PTP.EntityLayer.Models.Project", "Project")
                        .WithMany("Documents")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("PTP.EntityLayer.Models.User", "User")
                        .WithMany("Documents")
                        .HasForeignKey("UserId");

                    b.Navigation("Personnel");

                    b.Navigation("Process");

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PTP.EntityLayer.Models.Personnel", b =>
                {
                    b.HasOne("PTP.EntityLayer.Models.User", "User")
                        .WithOne("Personnel")
                        .HasForeignKey("PTP.EntityLayer.Models.Personnel", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PTP.EntityLayer.Models.Process", b =>
                {
                    b.HasOne("PTP.EntityLayer.Models.Personnel", null)
                        .WithMany("Processes")
                        .HasForeignKey("PersonnelId");

                    b.HasOne("PTP.EntityLayer.Models.ProcessStage", "ProcessStage")
                        .WithMany("Processes")
                        .HasForeignKey("ProcessStageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PTP.EntityLayer.Models.Project", "Project")
                        .WithMany("Processes")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ProcessStage");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("PTP.EntityLayer.Models.ProcessPersonnel", b =>
                {
                    b.HasOne("PTP.EntityLayer.Models.Personnel", "Personnel")
                        .WithMany("ProcessPersonnels")
                        .HasForeignKey("PersonnelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PTP.EntityLayer.Models.Process", "Process")
                        .WithMany("ProcessPersonnels")
                        .HasForeignKey("ProcessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Personnel");

                    b.Navigation("Process");
                });

            modelBuilder.Entity("PTP.EntityLayer.Models.ProcessStage", b =>
                {
                    b.HasOne("PTP.EntityLayer.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("PTP.EntityLayer.Models.Project", b =>
                {
                    b.HasOne("PTP.EntityLayer.Models.Customer", "Customer")
                        .WithMany("Projects")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("PTP.EntityLayer.Models.ProjectPersonnel", b =>
                {
                    b.HasOne("PTP.EntityLayer.Models.Personnel", "Personnel")
                        .WithMany("ProjectPersonnels")
                        .HasForeignKey("PersonnelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PTP.EntityLayer.Models.Project", "Project")
                        .WithMany("ProjectPersonnels")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Personnel");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("PersonnelProject", b =>
                {
                    b.HasOne("PTP.EntityLayer.Models.Personnel", null)
                        .WithMany()
                        .HasForeignKey("PersonnelsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PTP.EntityLayer.Models.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PTP.EntityLayer.Models.Customer", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("PTP.EntityLayer.Models.Personnel", b =>
                {
                    b.Navigation("ProcessPersonnels");

                    b.Navigation("Processes");

                    b.Navigation("ProjectPersonnels");
                });

            modelBuilder.Entity("PTP.EntityLayer.Models.Process", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Documents");

                    b.Navigation("ProcessPersonnels");
                });

            modelBuilder.Entity("PTP.EntityLayer.Models.ProcessStage", b =>
                {
                    b.Navigation("Processes");
                });

            modelBuilder.Entity("PTP.EntityLayer.Models.Project", b =>
                {
                    b.Navigation("Documents");

                    b.Navigation("Processes");

                    b.Navigation("ProjectPersonnels");
                });

            modelBuilder.Entity("PTP.EntityLayer.Models.User", b =>
                {
                    b.Navigation("Customer");

                    b.Navigation("Documents");

                    b.Navigation("Personnel");
                });
#pragma warning restore 612, 618
        }
    }
}
