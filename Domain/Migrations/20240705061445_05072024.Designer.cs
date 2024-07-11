﻿// <auto-generated />
using System;
using Domain.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Domain.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240705061445_05072024")]
    partial class _05072024
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Comment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
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

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Domain.Entities.Connection", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<string>("ConnectionStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ReceiverId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SenderId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("UpdatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId", "ReceiverId")
                        .IsUnique();

                    b.ToTable("Connection", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Interaction", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("InteractionType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("LastUpdatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Interactions");
                });

            modelBuilder.Entity("Domain.Entities.MajorTask", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
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

                    b.Property<int>("Position")
                        .HasColumnType("integer");

                    b.Property<string>("TaskTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PhaseId");

                    b.ToTable("MajorTasks");
                });

            modelBuilder.Entity("Domain.Entities.MinorTask", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DeadLine")
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

                    b.Property<int>("Position")
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

            modelBuilder.Entity("Domain.Entities.Phase", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
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

                    b.Property<int>("Position")
                        .HasColumnType("integer");

                    b.Property<string>("ProjectId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Phases");
                });

            modelBuilder.Entity("Domain.Entities.Post", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<int>("CommentCount")
                        .HasColumnType("integer");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("InteractionCount")
                        .HasColumnType("integer");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("LastUpdatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PostStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Domain.Entities.PostImage", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
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

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("PostImages");
                });

            modelBuilder.Entity("Domain.Entities.Project", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastUpdatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("LastUpdatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TeamId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
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

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Taskboard", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
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

                    b.Property<int>("Position")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PhaseId");

                    b.ToTable("Taskboards");
                });

            modelBuilder.Entity("Domain.Entities.Team", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
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

                    b.Property<string>("TeamLeaderId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Domain.Entities.TeamUser", b =>
                {
                    b.Property<string>("TeamId")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleInTeam")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("TeamId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("TeamUser", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("Bio")
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("CoverPhoto")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("LastUpdatedTime")
                        .HasColumnType("timestamp with time zone");

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

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<DateTimeOffset?>("Verified")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.UserRole", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
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

            modelBuilder.Entity("Domain.Entities.Comment", b =>
                {
                    b.HasOne("Domain.Entities.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Connection", b =>
                {
                    b.HasOne("Domain.Entities.User", "Receiver")
                        .WithMany("ReceivedConnections")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "Sender")
                        .WithMany("SentConnections")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Domain.Entities.MajorTask", b =>
                {
                    b.HasOne("Domain.Entities.Phase", "Phase")
                        .WithMany("MajorTasks")
                        .HasForeignKey("PhaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Phase");
                });

            modelBuilder.Entity("Domain.Entities.MinorTask", b =>
                {
                    b.HasOne("Domain.Entities.MajorTask", "MajorTask")
                        .WithMany("MinorTasks")
                        .HasForeignKey("MajorTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Taskboard", "Taskboard")
                        .WithMany("MinorTasks")
                        .HasForeignKey("TaskboardId");

                    b.Navigation("MajorTask");

                    b.Navigation("Taskboard");
                });

            modelBuilder.Entity("Domain.Entities.Phase", b =>
                {
                    b.HasOne("Domain.Entities.Project", "Project")
                        .WithMany("Phases")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Domain.Entities.Post", b =>
                {
                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.PostImage", b =>
                {
                    b.HasOne("Domain.Entities.Post", "Post")
                        .WithMany("PostImages")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Domain.Entities.Project", b =>
                {
                    b.HasOne("Domain.Entities.Team", "Team")
                        .WithMany("Projects")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Domain.Entities.Taskboard", b =>
                {
                    b.HasOne("Domain.Entities.Phase", "Phase")
                        .WithMany()
                        .HasForeignKey("PhaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Phase");
                });

            modelBuilder.Entity("Domain.Entities.TeamUser", b =>
                {
                    b.HasOne("Domain.Entities.Team", "Team")
                        .WithMany("TeamUsers")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("TeamUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.UserRole", b =>
                {
                    b.HasOne("Domain.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("InteractionPost", b =>
                {
                    b.HasOne("Domain.Entities.Interaction", null)
                        .WithMany()
                        .HasForeignKey("InteractionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Post", null)
                        .WithMany()
                        .HasForeignKey("PostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.MajorTask", b =>
                {
                    b.Navigation("MinorTasks");
                });

            modelBuilder.Entity("Domain.Entities.Phase", b =>
                {
                    b.Navigation("MajorTasks");
                });

            modelBuilder.Entity("Domain.Entities.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("PostImages");
                });

            modelBuilder.Entity("Domain.Entities.Project", b =>
                {
                    b.Navigation("Phases");
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Domain.Entities.Taskboard", b =>
                {
                    b.Navigation("MinorTasks");
                });

            modelBuilder.Entity("Domain.Entities.Team", b =>
                {
                    b.Navigation("Projects");

                    b.Navigation("TeamUsers");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("Posts");

                    b.Navigation("ReceivedConnections");

                    b.Navigation("SentConnections");

                    b.Navigation("TeamUsers");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
