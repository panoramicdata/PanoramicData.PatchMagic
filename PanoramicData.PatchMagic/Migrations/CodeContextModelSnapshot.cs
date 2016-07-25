using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PanoramicData.PatchMagic;

namespace PanoramicData.PatchMagic.Migrations
{
    [DbContext(typeof(CodeContext))]
    partial class CodeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PanoramicData.PatchMagic.CpeItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Cpe23Name");

                    b.Property<string>("Name");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("CpeItems");
                });

            modelBuilder.Entity("PanoramicData.PatchMagic.Reference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CpeItemId");

                    b.Property<string>("Href");

                    b.HasKey("Id");

                    b.HasIndex("CpeItemId");

                    b.ToTable("References");
                });

            modelBuilder.Entity("PanoramicData.PatchMagic.Reference", b =>
                {
                    b.HasOne("PanoramicData.PatchMagic.CpeItem", "CpeItem")
                        .WithMany("References")
                        .HasForeignKey("CpeItemId");
                });
        }
    }
}
