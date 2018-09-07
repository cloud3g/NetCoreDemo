using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NewApi.Data;

namespace NewAPI.Migrations
{
    [DbContext(typeof(NewApiContext))]
    [Migration("20170702224846_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NewApi.Models.Pessoa", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("telefone")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("id");

                    b.ToTable("Pessoas");
                });
        }
    }
}
