﻿// <auto-generated />
using Blog.Models;
using Blog.Models.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Blog.Models.Migrations
{
    [DbContext(typeof(BlogDbContext))]
    [Migration("20171019152249_addSysRoleOperate")]
    partial class addSysRoleOperate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("Blog.Models.BlogAnnouncement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime?>("CreateTime");

                    b.Property<bool>("Enable");

                    b.Property<int>("Level");

                    b.Property<DateTime?>("UpdateTime");

                    b.HasKey("Id");

                    b.ToTable("BlogAnnouncement");
                });

            modelBuilder.Entity("Blog.Models.BlogArticle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<string>("CategoryName");

                    b.Property<int?>("CommentNum");

                    b.Property<string>("Content");

                    b.Property<DateTime?>("CreateTime");

                    b.Property<string>("ImgUrl");

                    b.Property<bool?>("Recommend");

                    b.Property<string>("Remark");

                    b.Property<bool?>("Stick");

                    b.Property<string>("Submitter");

                    b.Property<string>("Title");

                    b.Property<int?>("Traffic");

                    b.Property<DateTime?>("UpdateTime");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("BlogArticle");
                });

            modelBuilder.Entity("Blog.Models.BlogCategory", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("BodyContent")
                        .HasMaxLength(8000);

                    b.Property<int>("CategoryType");

                    b.Property<DateTime?>("CreateTime");

                    b.Property<bool>("Enable");

                    b.Property<string>("ImgUrl");

                    b.Property<bool>("IsNav");

                    b.Property<string>("LinkUrl")
                        .HasMaxLength(500);

                    b.Property<string>("Meta_Description")
                        .HasMaxLength(255);

                    b.Property<string>("Meta_Keywords")
                        .HasMaxLength(255);

                    b.Property<string>("Name");

                    b.Property<int?>("Pid");

                    b.Property<string>("Remark");

                    b.Property<int?>("Sort");

                    b.Property<DateTime?>("UpdateTime");

                    b.HasKey("Id");

                    b.ToTable("BlogCategory");
                });

            modelBuilder.Entity("Blog.Models.Friendslinks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreateTime");

                    b.Property<string>("Email");

                    b.Property<bool>("Enable");

                    b.Property<string>("Logo");

                    b.Property<int>("Sort");

                    b.Property<DateTime?>("UpdateTime");

                    b.Property<string>("Url");

                    b.Property<string>("WebName");

                    b.HasKey("Id");

                    b.ToTable("Friendslinks");
                });

            modelBuilder.Entity("Blog.Models.Resource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abstract");

                    b.Property<DateTime?>("CreateTime");

                    b.Property<string>("Demolink");

                    b.Property<string>("Downloadlink");

                    b.Property<string>("Name");

                    b.Property<string>("ResourceCoverSrc");

                    b.Property<string>("Submitter");

                    b.Property<int>("Type");

                    b.Property<DateTime?>("UpdateTime");

                    b.HasKey("Id");

                    b.ToTable("Resource");
                });

            modelBuilder.Entity("Blog.Models.RolePermission", b =>
                {
                    b.Property<int>("RoleId");

                    b.Property<int>("ModuleId");

                    b.Property<DateTime?>("CreateTime");

                    b.Property<int>("Id");

                    b.Property<DateTime?>("UpdateTime");

                    b.HasKey("RoleId", "ModuleId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("ModuleId");

                    b.ToTable("RolePermission");
                });

            modelBuilder.Entity("Blog.Models.SysModule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AreaName");

                    b.Property<string>("ControllerName");

                    b.Property<DateTime?>("CreateTime");

                    b.Property<string>("Icon");

                    b.Property<bool>("IsSysMenu");

                    b.Property<string>("Name");

                    b.Property<string>("PName");

                    b.Property<int>("Pid");

                    b.Property<int>("Type");

                    b.Property<DateTime?>("UpdateTime");

                    b.HasKey("Id");

                    b.ToTable("SysModule");
                });

            modelBuilder.Entity("Blog.Models.SysModuleAction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime?>("CreateTime");

                    b.Property<bool>("Enable");

                    b.Property<int>("ModuleId");

                    b.Property<string>("Name");

                    b.Property<DateTime?>("UpdateTime");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.ToTable("SysModuleAction");
                });

            modelBuilder.Entity("Blog.Models.SysRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreateTime");

                    b.Property<string>("Name");

                    b.Property<string>("Remark");

                    b.Property<DateTime?>("UpdateTime");

                    b.HasKey("Id");

                    b.ToTable("SysRole");
                });

            modelBuilder.Entity("Blog.Models.SysRoleOperate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Action");

                    b.Property<string>("Area");

                    b.Property<string>("Controller");

                    b.Property<DateTime?>("CreateTime");

                    b.Property<bool>("Enable");

                    b.Property<int>("RoleId");

                    b.Property<DateTime?>("UpdateTime");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("SysRoleOperate");
                });

            modelBuilder.Entity("Blog.Models.SysUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreateTime");

                    b.Property<int>("ErrorCount");

                    b.Property<string>("IP");

                    b.Property<string>("Icon");

                    b.Property<DateTime?>("LastErrTime");

                    b.Property<string>("LoginName")
                        .HasMaxLength(20);

                    b.Property<string>("Password")
                        .HasMaxLength(20);

                    b.Property<string>("RealName");

                    b.Property<string>("Remark");

                    b.Property<int>("Status");

                    b.Property<DateTime?>("UpdateTime");

                    b.HasKey("Id");

                    b.ToTable("SysUser");
                });

            modelBuilder.Entity("Blog.Models.SysUserInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreateTime");

                    b.Property<int>("ErrorCount");

                    b.Property<DateTime?>("LastErrTime");

                    b.Property<string>("LoginName")
                        .HasMaxLength(20);

                    b.Property<string>("Password")
                        .HasMaxLength(20);

                    b.Property<string>("RealName");

                    b.Property<string>("Remark");

                    b.Property<int>("Status");

                    b.Property<DateTime?>("UpdateTime");

                    b.HasKey("Id");

                    b.ToTable("SysUserInfo");
                });

            modelBuilder.Entity("Blog.Models.SysUserRole", b =>
                {
                    b.Property<int>("RoleId");

                    b.Property<int>("UserId");

                    b.Property<DateTime?>("CreateTime");

                    b.Property<bool>("Enable");

                    b.Property<int>("Id");

                    b.Property<DateTime?>("UpdateTime");

                    b.HasKey("RoleId", "UserId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("SysUserRole");
                });

            modelBuilder.Entity("Blog.Models.TimeLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime?>("CreateTime");

                    b.Property<DateTime?>("UpdateTime");

                    b.HasKey("Id");

                    b.ToTable("TimeLine");
                });

            modelBuilder.Entity("Blog.Models.BlogArticle", b =>
                {
                    b.HasOne("Blog.Models.BlogCategory", "BlogCategory")
                        .WithMany("BlogArticles")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Blog.Models.RolePermission", b =>
                {
                    b.HasOne("Blog.Models.SysModule", "SysModule")
                        .WithMany("RolePermissions")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Blog.Models.SysRole", "SysRole")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Blog.Models.SysModuleAction", b =>
                {
                    b.HasOne("Blog.Models.SysModule", "SysModule")
                        .WithMany("SysModuleActions")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Blog.Models.SysRoleOperate", b =>
                {
                    b.HasOne("Blog.Models.SysRole", "SysRole")
                        .WithMany("SysRoleOperate")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Blog.Models.SysUserRole", b =>
                {
                    b.HasOne("Blog.Models.SysRole", "SysRole")
                        .WithMany("SysUserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Blog.Models.SysUser", "SysUser")
                        .WithMany("SysUserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
