﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Whisper.Data;

#nullable disable

namespace Whisper.Data.Migrations
{
    [DbContext(typeof(WhisperDbContext))]
    [Migration("20250113133853_Migration_Name")]
    partial class Migration_Name
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Whisper.Data.Entities.GroupEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_created");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_updated");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("boolean")
                        .HasColumnName("is_closed");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("title");

                    b.Property<Guid>("owner_id")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("owner_id");

                    b.ToTable("groups", (string)null);
                });

            modelBuilder.Entity("Whisper.Data.Entities.LocationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("country");

                    b.HasKey("Id");

                    b.HasIndex("Country")
                        .IsUnique();

                    b.ToTable("locations", (string)null);
                });

            modelBuilder.Entity("Whisper.Data.Entities.RefreshTokenEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_created");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_updated");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("expire_date");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("token");

                    b.HasKey("Id");

                    b.ToTable("refresh_tokens", (string)null);
                });

            modelBuilder.Entity("Whisper.Data.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("BirthDay")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("birthday");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_created");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_updated");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("boolean")
                        .HasColumnName("is_verified");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)")
                        .HasColumnName("password");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)")
                        .HasColumnName("phone_number");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("surname");

                    b.Property<string>("Username")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("username");

                    b.Property<Guid?>("location_id")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("refresh_token_id")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.HasIndex("location_id");

                    b.HasIndex("refresh_token_id")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Whisper.Data.Entities.UserGroup.UserFollowerGroupsEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uuid")
                        .HasColumnName("group_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("user_followed_groups", (string)null);
                });

            modelBuilder.Entity("Whisper.Data.Entities.UserGroup.UserModeratorGroupsEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uuid")
                        .HasColumnName("group_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("user_moderator_groups", (string)null);
                });

            modelBuilder.Entity("Whisper.Data.Entities.GroupEntity", b =>
                {
                    b.HasOne("Whisper.Data.Entities.UserEntity", "Owner")
                        .WithMany("OwnedGroups")
                        .HasForeignKey("owner_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Whisper.Data.Entities.UserEntity", b =>
                {
                    b.HasOne("Whisper.Data.Entities.LocationEntity", "Location")
                        .WithMany("User")
                        .HasForeignKey("location_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Whisper.Data.Entities.RefreshTokenEntity", "RefreshToken")
                        .WithOne("User")
                        .HasForeignKey("Whisper.Data.Entities.UserEntity", "refresh_token_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Location");

                    b.Navigation("RefreshToken");
                });

            modelBuilder.Entity("Whisper.Data.Entities.UserGroup.UserFollowerGroupsEntity", b =>
                {
                    b.HasOne("Whisper.Data.Entities.GroupEntity", "Group")
                        .WithMany("Followers")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Whisper.Data.Entities.UserEntity", "User")
                        .WithMany("FollowedGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Whisper.Data.Entities.UserGroup.UserModeratorGroupsEntity", b =>
                {
                    b.HasOne("Whisper.Data.Entities.GroupEntity", "Group")
                        .WithMany("Moderators")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Whisper.Data.Entities.UserEntity", "User")
                        .WithMany("ModeratedGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Whisper.Data.Entities.GroupEntity", b =>
                {
                    b.Navigation("Followers");

                    b.Navigation("Moderators");
                });

            modelBuilder.Entity("Whisper.Data.Entities.LocationEntity", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("Whisper.Data.Entities.RefreshTokenEntity", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("Whisper.Data.Entities.UserEntity", b =>
                {
                    b.Navigation("FollowedGroups");

                    b.Navigation("ModeratedGroups");

                    b.Navigation("OwnedGroups");
                });
#pragma warning restore 612, 618
        }
    }
}