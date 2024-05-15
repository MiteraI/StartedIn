﻿// <auto-generated />
using System;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StartedIn.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240513045524_130520241039")]
    partial class _130520241039
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DataAccessLayer.Models.Comment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("CommentContent")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("LastUpdatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PostId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Interaction", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("InteractionType")
                        .HasColumnType("integer");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("LastUpdatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Interactions");
                });

            modelBuilder.Entity("DataAccessLayer.Models.MajorTask", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("LastUpdatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PhaseId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("TaskTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PhaseId");

                    b.ToTable("MajorTasks");
                });

            modelBuilder.Entity("DataAccessLayer.Models.MinorTask", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("LastUpdatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("MajorTaskId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("TaskTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TaskboardId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("MajorTaskId");

                    b.HasIndex("TaskboardId");

                    b.ToTable("MinorTasks");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Phase", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("LastUpdatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PhaseName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProjectId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Phases");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Post", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("LastUpdatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("DataAccessLayer.Models.PostImage", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ImageLink")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("LastUpdatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PostId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("PostImages");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Project", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<decimal>("ActualCost")
                        .HasColumnType("numeric");

                    b.Property<int>("ActualDuration")
                        .HasColumnType("integer");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EstimateDuration")
                        .HasColumnType("integer");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("LastUpdatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<float>("Progress")
                        .HasColumnType("real");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("TeamId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("DataAccessLayer.Models.RoleEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("DataAccessLayer.Models.Taskboard", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("LastUpdatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PhaseId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PhaseId");

                    b.ToTable("Taskboards");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Team", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("LastUpdatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("TeamLeaderId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("DataAccessLayer.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<DateTimeOffset?>("Verified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("bio")
                        .HasColumnType("text");

                    b.Property<string>("content")
                        .HasColumnType("text");

                    b.Property<string>("coverPhoto")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("createdTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("lastUpdatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("profilePicture")
                        .HasColumnType("text");

                    b.Property<int>("status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("InteractionPost", b =>
                {
                    b.Property<string>("InteractionsId")
                        .HasColumnType("text");

                    b.Property<string>("PostsId")
                        .HasColumnType("text");

                    b.HasKey("InteractionsId", "PostsId");

                    b.HasIndex("PostsId");

                    b.ToTable("PostInteraction", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("TeamUser", b =>
                {
                    b.Property<string>("TeamsId")
                        .HasColumnType("text");

                    b.Property<string>("UsersId")
                        .HasColumnType("text");

                    b.HasKey("TeamsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("TeamAccount", (string)null);
                });

            modelBuilder.Entity("DataAccessLayer.Models.Comment", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccessLayer.Models.MajorTask", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Phase", "Phase")
                        .WithMany("MajorTasks")
                        .HasForeignKey("PhaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Phase");
                });

            modelBuilder.Entity("DataAccessLayer.Models.MinorTask", b =>
                {
                    b.HasOne("DataAccessLayer.Models.MajorTask", "MajorTask")
                        .WithMany("MinorTasks")
                        .HasForeignKey("MajorTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Models.Taskboard", "Taskboard")
                        .WithMany("MinorTasks")
                        .HasForeignKey("TaskboardId");

                    b.Navigation("MajorTask");

                    b.Navigation("Taskboard");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Phase", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Project", "Project")
                        .WithMany("Phases")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Post", b =>
                {
                    b.HasOne("DataAccessLayer.Models.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccessLayer.Models.PostImage", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Post", "Post")
                        .WithMany("PostImages")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Project", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Team", "Team")
                        .WithMany("Projects")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Taskboard", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Phase", "Phase")
                        .WithMany()
                        .HasForeignKey("PhaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Phase");
                });

            modelBuilder.Entity("InteractionPost", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Interaction", null)
                        .WithMany()
                        .HasForeignKey("InteractionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Models.Post", null)
                        .WithMany()
                        .HasForeignKey("PostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("DataAccessLayer.Models.RoleEntity", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DataAccessLayer.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DataAccessLayer.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("DataAccessLayer.Models.RoleEntity", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DataAccessLayer.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TeamUser", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Team", null)
                        .WithMany()
                        .HasForeignKey("TeamsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccessLayer.Models.MajorTask", b =>
                {
                    b.Navigation("MinorTasks");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Phase", b =>
                {
                    b.Navigation("MajorTasks");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("PostImages");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Project", b =>
                {
                    b.Navigation("Phases");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Taskboard", b =>
                {
                    b.Navigation("MinorTasks");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Team", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("DataAccessLayer.Models.User", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
