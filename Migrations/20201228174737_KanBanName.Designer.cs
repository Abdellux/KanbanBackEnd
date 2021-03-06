﻿// <auto-generated />
using KanbanApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KanbanApi.Migrations
{
    [DbContext(typeof(KanbanDbContext))]
    [Migration("20201228174737_KanBanName")]
    partial class KanBanName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("KanbanApi.Models.AssignedTask", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long>("ColumnTaskId")
                        .HasColumnType("bigint");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ColumnTaskId");

                    b.HasIndex("UserId");

                    b.ToTable("AssignedTasks");
                });

            modelBuilder.Entity("KanbanApi.Models.ColumnTask", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("KanbanColumnId")
                        .HasColumnType("bigint");

                    b.Property<bool>("affected")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("KanbanColumnId");

                    b.ToTable("ColumnTasks");
                });

            modelBuilder.Entity("KanbanApi.Models.Kanban", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<int>("ColumnsNumber")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("state")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Kanbans");
                });

            modelBuilder.Entity("KanbanApi.Models.KanbanColumn", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long>("KanbanId")
                        .HasColumnType("bigint");

                    b.Property<string>("Titre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("KanbanId");

                    b.ToTable("KanbanColumns");
                });

            modelBuilder.Entity("KanbanApi.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("KanbanApi.Models.UserKanban", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long>("KanbanId")
                        .HasColumnType("bigint");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("KanbanId");

                    b.HasIndex("UserId");

                    b.ToTable("UserKanbans");
                });

            modelBuilder.Entity("KanbanApi.Models.AssignedTask", b =>
                {
                    b.HasOne("KanbanApi.Models.ColumnTask", "ColumnTask")
                        .WithMany("AssignedTasks")
                        .HasForeignKey("ColumnTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KanbanApi.Models.User", "User")
                        .WithMany("AssignedTasks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ColumnTask");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KanbanApi.Models.ColumnTask", b =>
                {
                    b.HasOne("KanbanApi.Models.KanbanColumn", "KanbanColumn")
                        .WithMany("ColumnTasks")
                        .HasForeignKey("KanbanColumnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KanbanColumn");
                });

            modelBuilder.Entity("KanbanApi.Models.KanbanColumn", b =>
                {
                    b.HasOne("KanbanApi.Models.Kanban", "Kanban")
                        .WithMany("KanbanColumns")
                        .HasForeignKey("KanbanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kanban");
                });

            modelBuilder.Entity("KanbanApi.Models.UserKanban", b =>
                {
                    b.HasOne("KanbanApi.Models.Kanban", "Kanban")
                        .WithMany("UserKanbans")
                        .HasForeignKey("KanbanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KanbanApi.Models.User", "User")
                        .WithMany("UserKanbans")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kanban");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KanbanApi.Models.ColumnTask", b =>
                {
                    b.Navigation("AssignedTasks");
                });

            modelBuilder.Entity("KanbanApi.Models.Kanban", b =>
                {
                    b.Navigation("KanbanColumns");

                    b.Navigation("UserKanbans");
                });

            modelBuilder.Entity("KanbanApi.Models.KanbanColumn", b =>
                {
                    b.Navigation("ColumnTasks");
                });

            modelBuilder.Entity("KanbanApi.Models.User", b =>
                {
                    b.Navigation("AssignedTasks");

                    b.Navigation("UserKanbans");
                });
#pragma warning restore 612, 618
        }
    }
}
