﻿// <auto-generated />
using ComputerGamesShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace ComputerGamesShop.Migrations
{
    [DbContext(typeof(ComputerGamesShopContext))]
    partial class ComputerGamesShopContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ComputerGamesShop.Models.Game", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(400);

                    b.Property<int>("Genre");

                    b.Property<string>("Image");

                    b.Property<bool>("IsMultiplayer");

                    b.Property<double>("Price");

                    b.Property<int>("PublisherID");

                    b.Property<DateTime>("ReleaseDate");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(45);

                    b.HasKey("ID");

                    b.HasIndex("PublisherID");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("ComputerGamesShop.Models.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerId");

                    b.Property<DateTime>("OrderDate");

                    b.Property<int>("StoreID");

                    b.HasKey("OrderID");

                    b.HasIndex("CustomerId");

                    b.HasIndex("StoreID");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("ComputerGamesShop.Models.OrderItems", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("gameId");

                    b.Property<int>("orderId");

                    b.HasKey("ID");

                    b.HasIndex("gameId");

                    b.HasIndex("orderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("ComputerGamesShop.Models.Publisher", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("FoundedDate");

                    b.Property<string>("Image");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Specialty");

                    b.HasKey("ID");

                    b.ToTable("Publisher");
                });

            modelBuilder.Entity("ComputerGamesShop.Models.Store", b =>
                {
                    b.Property<int>("StoreID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Latitude")
                        .IsRequired();

                    b.Property<string>("Longitude")
                        .IsRequired();

                    b.Property<string>("StoreCity")
                        .IsRequired();

                    b.Property<string>("StoreName")
                        .IsRequired();

                    b.Property<string>("StoreStreet")
                        .IsRequired();

                    b.Property<string>("StoresPhoneNumber");

                    b.HasKey("StoreID");

                    b.ToTable("Store");
                });

            modelBuilder.Entity("ComputerGamesShop.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("City");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<int>("Gender");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(8);

                    b.Property<string>("PhoneNumber");

                    b.Property<int>("Role");

                    b.Property<string>("Street");

                    b.HasKey("UserID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ComputerGamesShop.Models.Game", b =>
                {
                    b.HasOne("ComputerGamesShop.Models.Publisher", "Publisher")
                        .WithMany("Games")
                        .HasForeignKey("PublisherID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ComputerGamesShop.Models.Order", b =>
                {
                    b.HasOne("ComputerGamesShop.Models.User", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ComputerGamesShop.Models.Store", "Store")
                        .WithMany()
                        .HasForeignKey("StoreID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ComputerGamesShop.Models.OrderItems", b =>
                {
                    b.HasOne("ComputerGamesShop.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("gameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ComputerGamesShop.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("orderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
