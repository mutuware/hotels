﻿// <auto-generated />
using System;
using Hotels.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Hotels.Data.Migrations
{
    [DbContext(typeof(HotelsContext))]
    [Migration("20241126131005_ModelChanges2")]
    partial class ModelChanges2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("Hotels.Data.Entities.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("HotelId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RoomId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.HasIndex("RoomId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("Hotels.Data.Entities.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("Hotels.Data.Entities.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("TEXT");

                    b.Property<int?>("HotelId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("Rooms");

                    b.HasDiscriminator().HasValue("Room");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Hotels.Data.Entities.DeluxeRoom", b =>
                {
                    b.HasBaseType("Hotels.Data.Entities.Room");

                    b.HasDiscriminator().HasValue("DeluxeRoom");
                });

            modelBuilder.Entity("Hotels.Data.Entities.DoubleRoom", b =>
                {
                    b.HasBaseType("Hotels.Data.Entities.Room");

                    b.HasDiscriminator().HasValue("DoubleRoom");
                });

            modelBuilder.Entity("Hotels.Data.Entities.SingleRoom", b =>
                {
                    b.HasBaseType("Hotels.Data.Entities.Room");

                    b.HasDiscriminator().HasValue("SingleRoom");
                });

            modelBuilder.Entity("Hotels.Data.Entities.Booking", b =>
                {
                    b.HasOne("Hotels.Data.Entities.Hotel", "Hotel")
                        .WithMany()
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hotels.Data.Entities.Room", "Room")
                        .WithMany("Bookings")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Hotels.Data.Entities.Room", b =>
                {
                    b.HasOne("Hotels.Data.Entities.Hotel", null)
                        .WithMany("Rooms")
                        .HasForeignKey("HotelId");
                });

            modelBuilder.Entity("Hotels.Data.Entities.Hotel", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("Hotels.Data.Entities.Room", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}