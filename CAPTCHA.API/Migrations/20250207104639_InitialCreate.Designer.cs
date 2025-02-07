﻿// <auto-generated />
using System;
using CAPTCHA.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CAPTCHA.API.Migrations
{
    [DbContext(typeof(CAPTCHADbContext))]
    [Migration("20250207104639_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.1");

            modelBuilder.Entity("CAPTCHA.Core.Models.TextImgCAPTCHA", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("AnswerInPlainText")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Attempts")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("UsedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TextImgCAPTCHAs");
                });
#pragma warning restore 612, 618
        }
    }
}
