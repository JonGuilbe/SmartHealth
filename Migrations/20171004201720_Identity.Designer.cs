﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SmartHealth;
using System;

namespace SmartHealth.Migrations
{
    [DbContext(typeof(postgresContext))]
    [Migration("20171004201720_Identity")]
    partial class Identity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:PostgresExtension:adminpack", "'adminpack', '', ''")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("SmartHealth.Test", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ID");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Test");
                });
#pragma warning restore 612, 618
        }
    }
}