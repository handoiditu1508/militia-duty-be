﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MilitiaDuty.Data;

#nullable disable

namespace MilitiaDuty.Migrations
{
    [DbContext(typeof(MilitiaContext))]
    partial class MilitiaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("MilitiaDuty.Models.Assignments.Mission", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Missions");
                });

            modelBuilder.Entity("MilitiaDuty.Models.Assignments.Shift", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DutyDateId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<uint>("MilitiaId")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("TaskId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DutyDateId");

                    b.HasIndex("MilitiaId");

                    b.HasIndex("TaskId");

                    b.ToTable("Shifts");
                });

            modelBuilder.Entity("MilitiaDuty.Models.Assignments.Task", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<ushort>("EndMinute")
                        .HasColumnType("INTEGER");

                    b.Property<ushort>("MilitiaNumber")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("MissionId")
                        .HasColumnType("INTEGER");

                    b.Property<ushort>("StartMinute")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MissionId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("MilitiaDuty.Models.DutyDates.DutyDate", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsFullDutyDate")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("DutyDates");
                });

            modelBuilder.Entity("MilitiaDuty.Models.DutyDates.MilitiaDutyDate", b =>
                {
                    b.Property<uint>("MilitiaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DutyDateId")
                        .HasColumnType("TEXT");

                    b.HasKey("MilitiaId", "DutyDateId");

                    b.HasIndex("DutyDateId");

                    b.ToTable("MilitiaDutyDates");
                });

            modelBuilder.Entity("MilitiaDuty.Models.Militias.Militia", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<ushort>("AssignmentScore")
                        .HasColumnType("INTEGER");

                    b.Property<ushort>("DutyDateScore")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Militias");
                });

            modelBuilder.Entity("MilitiaDuty.Models.Rules.MilitiaRule", b =>
                {
                    b.Property<uint>("MilitiaId")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("RuleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("MilitiaId", "RuleId");

                    b.HasIndex("RuleId");

                    b.ToTable("MilitiaRules");
                });

            modelBuilder.Entity("MilitiaDuty.Models.Rules.Rule", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<ushort?>("NumberValue")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Weekdays")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Rules");
                });

            modelBuilder.Entity("MilitiaDuty.Models.Rules.TaskRule", b =>
                {
                    b.Property<uint>("TaskId")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("RuleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("TaskId", "RuleId");

                    b.HasIndex("RuleId");

                    b.ToTable("TaskRules");
                });

            modelBuilder.Entity("MilitiaDuty.Models.Assignments.Shift", b =>
                {
                    b.HasOne("MilitiaDuty.Models.DutyDates.DutyDate", "DutyDate")
                        .WithMany("Shifts")
                        .HasForeignKey("DutyDateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MilitiaDuty.Models.Militias.Militia", "Militia")
                        .WithMany("Shifts")
                        .HasForeignKey("MilitiaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MilitiaDuty.Models.Assignments.Task", "Task")
                        .WithMany("Shifts")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DutyDate");

                    b.Navigation("Militia");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("MilitiaDuty.Models.Assignments.Task", b =>
                {
                    b.HasOne("MilitiaDuty.Models.Assignments.Mission", "Mission")
                        .WithMany("Tasks")
                        .HasForeignKey("MissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mission");
                });

            modelBuilder.Entity("MilitiaDuty.Models.DutyDates.MilitiaDutyDate", b =>
                {
                    b.HasOne("MilitiaDuty.Models.DutyDates.DutyDate", null)
                        .WithMany()
                        .HasForeignKey("DutyDateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MilitiaDuty.Models.Militias.Militia", null)
                        .WithMany()
                        .HasForeignKey("MilitiaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MilitiaDuty.Models.Rules.MilitiaRule", b =>
                {
                    b.HasOne("MilitiaDuty.Models.Militias.Militia", null)
                        .WithMany()
                        .HasForeignKey("MilitiaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MilitiaDuty.Models.Rules.Rule", null)
                        .WithMany()
                        .HasForeignKey("RuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MilitiaDuty.Models.Rules.TaskRule", b =>
                {
                    b.HasOne("MilitiaDuty.Models.Rules.Rule", null)
                        .WithMany()
                        .HasForeignKey("RuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MilitiaDuty.Models.Assignments.Task", null)
                        .WithMany()
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MilitiaDuty.Models.Assignments.Mission", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("MilitiaDuty.Models.Assignments.Task", b =>
                {
                    b.Navigation("Shifts");
                });

            modelBuilder.Entity("MilitiaDuty.Models.DutyDates.DutyDate", b =>
                {
                    b.Navigation("Shifts");
                });

            modelBuilder.Entity("MilitiaDuty.Models.Militias.Militia", b =>
                {
                    b.Navigation("Shifts");
                });
#pragma warning restore 612, 618
        }
    }
}
