﻿// <auto-generated />
using Domino.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Domino.Migrations
{
    [DbContext(typeof(DominodbContext))]
    [Migration("20220816105908_Migration-1")]
    partial class Migration1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domino.Model.Admin", b =>
                {
                    b.Property<int>("AdminID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminID"), 1L, 1);

                    b.Property<string>("AdminName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdminID");

                    b.ToTable("admin");
                });

            modelBuilder.Entity("Domino.Model.Cart", b =>
                {
                    b.Property<int>("CartID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartID"), 1L, 1);

                    b.Property<string>("CartTypeID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<int>("PizzaID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<float>("UnitPrice")
                        .HasColumnType("real");

                    b.HasKey("CartID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("PizzaID");

                    b.ToTable("cart");
                });

            modelBuilder.Entity("Domino.Model.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerID"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CartTypeID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("MobileNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerID");

                    b.ToTable("customers");
                });

            modelBuilder.Entity("Domino.Model.Payment", b =>
                {
                    b.Property<int>("PaymentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentID"), 1L, 1);

                    b.Property<int>("CardNumber")
                        .HasColumnType("int");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<float>("TotalAmount")
                        .HasColumnType("real");

                    b.HasKey("PaymentID");

                    b.HasIndex("CustomerID");

                    b.ToTable("payments");
                });

            modelBuilder.Entity("Domino.Model.Pizza", b =>
                {
                    b.Property<int>("PizzaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PizzaID"), 1L, 1);

                    b.Property<string>("PizzaName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("PizzaID");

                    b.ToTable("pizza");
                });

            modelBuilder.Entity("Domino.Model.Receipt", b =>
                {
                    b.Property<int>("ReceiptID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReceiptID"), 1L, 1);

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<int>("PaymentID")
                        .HasColumnType("int");

                    b.HasKey("ReceiptID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("PaymentID");

                    b.ToTable("receipts");
                });

            modelBuilder.Entity("Domino.Model.Cart", b =>
                {
                    b.HasOne("Domino.Model.Customer", "customer")
                        .WithMany("cart")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domino.Model.Pizza", "pizza")
                        .WithMany("cart")
                        .HasForeignKey("PizzaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("customer");

                    b.Navigation("pizza");
                });

            modelBuilder.Entity("Domino.Model.Payment", b =>
                {
                    b.HasOne("Domino.Model.Customer", "customer")
                        .WithMany("Payment")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("customer");
                });

            modelBuilder.Entity("Domino.Model.Receipt", b =>
                {
                    b.HasOne("Domino.Model.Customer", "customer")
                        .WithMany("Receipt")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domino.Model.Payment", "payment")
                        .WithMany("Receipt")
                        .HasForeignKey("PaymentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("customer");

                    b.Navigation("payment");
                });

            modelBuilder.Entity("Domino.Model.Customer", b =>
                {
                    b.Navigation("Payment");

                    b.Navigation("Receipt");

                    b.Navigation("cart");
                });

            modelBuilder.Entity("Domino.Model.Payment", b =>
                {
                    b.Navigation("Receipt");
                });

            modelBuilder.Entity("Domino.Model.Pizza", b =>
                {
                    b.Navigation("cart");
                });
#pragma warning restore 612, 618
        }
    }
}
