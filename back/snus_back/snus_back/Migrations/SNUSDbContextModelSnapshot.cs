﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using snus_back.data_access;

#nullable disable

namespace snus_back.Migrations
{
    [DbContext(typeof(SNUSDbContext))]
    partial class SNUSDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true);

            modelBuilder.Entity("snus_back.Models.Alarm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AnalogInputId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Priority")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("AnalogInputId");

                    b.ToTable("Alarms");
                });

            modelBuilder.Entity("snus_back.Models.AlarmRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AlarmId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TagId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AlarmId");

                    b.HasIndex("TagId");

                    b.ToTable("AlarmRecords");
                });

            modelBuilder.Entity("snus_back.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("IOAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Tags");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Tag");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("snus_back.Models.TagRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("TagId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.Property<double>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("TagId");

                    b.ToTable("TagRecords");
                });

            modelBuilder.Entity("snus_back.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("snus_back.Models.AnalogInput", b =>
                {
                    b.HasBaseType("snus_back.Models.Tag");

                    b.Property<double>("HighLimit")
                        .HasColumnType("REAL");

                    b.Property<bool>("IsScanOn")
                        .HasColumnType("INTEGER");

                    b.Property<double>("LowLimit")
                        .HasColumnType("REAL");

                    b.Property<int>("ScanTime")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.ToTable("Tags", t =>
                        {
                            t.Property("HighLimit")
                                .HasColumnName("AnalogInput_HighLimit");

                            t.Property("IsScanOn")
                                .HasColumnName("AnalogInput_IsScanOn");

                            t.Property("LowLimit")
                                .HasColumnName("AnalogInput_LowLimit");

                            t.Property("ScanTime")
                                .HasColumnName("AnalogInput_ScanTime");

                            t.Property("Unit")
                                .HasColumnName("AnalogInput_Unit");
                        });

                    b.HasDiscriminator().HasValue("AnalogInput");
                });

            modelBuilder.Entity("snus_back.Models.AnalogOutput", b =>
                {
                    b.HasBaseType("snus_back.Models.Tag");

                    b.Property<double>("HighLimit")
                        .HasColumnType("REAL");

                    b.Property<double>("LowLimit")
                        .HasColumnType("REAL");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("AnalogOutput");
                });

            modelBuilder.Entity("snus_back.Models.DigitalInput", b =>
                {
                    b.HasBaseType("snus_back.Models.Tag");

                    b.Property<bool>("IsScanOn")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ScanTime")
                        .HasColumnType("INTEGER");

                    b.HasDiscriminator().HasValue("DigitalInput");
                });

            modelBuilder.Entity("snus_back.Models.DigitalOutput", b =>
                {
                    b.HasBaseType("snus_back.Models.Tag");

                    b.HasDiscriminator().HasValue("DigitalOutput");
                });

            modelBuilder.Entity("snus_back.Models.Alarm", b =>
                {
                    b.HasOne("snus_back.Models.AnalogInput", null)
                        .WithMany("Alarms")
                        .HasForeignKey("AnalogInputId");
                });

            modelBuilder.Entity("snus_back.Models.AlarmRecord", b =>
                {
                    b.HasOne("snus_back.Models.Alarm", "Alarm")
                        .WithMany()
                        .HasForeignKey("AlarmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("snus_back.Models.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Alarm");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("snus_back.Models.TagRecord", b =>
                {
                    b.HasOne("snus_back.Models.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("snus_back.Models.AnalogInput", b =>
                {
                    b.Navigation("Alarms");
                });
#pragma warning restore 612, 618
        }
    }
}
