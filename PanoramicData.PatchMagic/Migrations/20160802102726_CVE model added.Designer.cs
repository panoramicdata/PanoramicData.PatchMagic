using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PanoramicData.PatchMagic;

namespace PanoramicData.PatchMagic.Migrations
{
    [DbContext(typeof(CodeContext))]
    [Migration("20160802102726_CVE model added")]
    partial class CVEmodeladded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PanoramicData.PatchMagic.Models.Cpe.CpeItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Cpe23Name");

                    b.Property<string>("Name");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("CpeItems");
                });

            modelBuilder.Entity("PanoramicData.PatchMagic.Models.Cpe.Reference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CpeItemId");

                    b.Property<string>("Href");

                    b.HasKey("Id");

                    b.HasIndex("CpeItemId");

                    b.ToTable("References");
                });

            modelBuilder.Entity("PanoramicData.PatchMagic.Models.Cve.BaseMetrics", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessComplexity");

                    b.Property<string>("AccessVector");

                    b.Property<int>("Authentication");

                    b.Property<int>("AvailabilityImpact");

                    b.Property<int>("ConfidentialityImpact");

                    b.Property<int?>("EntryId")
                        .IsRequired();

                    b.Property<DateTime>("GeneratedOn");

                    b.Property<int>("IntegrityImpact");

                    b.Property<float>("Score");

                    b.Property<string>("Source");

                    b.HasKey("Id");

                    b.HasIndex("EntryId")
                        .IsUnique();

                    b.ToTable("BaseMetrics");
                });

            modelBuilder.Entity("PanoramicData.PatchMagic.Models.Cve.CveReferences", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("EntryId")
                        .IsRequired();

                    b.Property<int>("ReferenceType");

                    b.Property<string>("Source");

                    b.HasKey("Id");

                    b.HasIndex("EntryId");

                    b.ToTable("CveReferences");
                });

            modelBuilder.Entity("PanoramicData.PatchMagic.Models.Cve.Entry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CveId")
                        .IsRequired();

                    b.Property<string>("CweId");

                    b.Property<DateTime?>("LastModifiedDateTime");

                    b.Property<DateTime?>("PublisheDateTime")
                        .IsRequired();

                    b.Property<string>("Summary");

                    b.HasKey("Id");

                    b.ToTable("Entries");
                });

            modelBuilder.Entity("PanoramicData.PatchMagic.Models.Cve.LogicalTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Negate");

                    b.Property<int>("Operator");

                    b.Property<int?>("VulnerableConfigurationId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("VulnerableConfigurationId");

                    b.ToTable("LogicalTest");
                });

            modelBuilder.Entity("PanoramicData.PatchMagic.Models.Cve.ReferencesList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CveReferencesId")
                        .IsRequired();

                    b.Property<string>("Href");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("CveReferencesId");

                    b.ToTable("ReferencesLists");
                });

            modelBuilder.Entity("PanoramicData.PatchMagic.Models.Cve.VulnerableConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("EntryId")
                        .IsRequired();

                    b.Property<string>("VcId");

                    b.HasKey("Id");

                    b.HasIndex("EntryId")
                        .IsUnique();

                    b.ToTable("VulnerableConfigurations");
                });

            modelBuilder.Entity("PanoramicData.PatchMagic.Models.Cve.VulnerableProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Product");

                    b.Property<int?>("VulnerableSoftwareListId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("VulnerableSoftwareListId");

                    b.ToTable("VulnerableProducts");
                });

            modelBuilder.Entity("PanoramicData.PatchMagic.Models.Cve.VulnerableSoftwareList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("EntryId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("EntryId")
                        .IsUnique();

                    b.ToTable("VulnerableSoftwareLists");
                });

            modelBuilder.Entity("PanoramicData.PatchMagic.Models.Cpe.Reference", b =>
                {
                    b.HasOne("PanoramicData.PatchMagic.Models.Cpe.CpeItem", "CpeItem")
                        .WithMany("References")
                        .HasForeignKey("CpeItemId");
                });

            modelBuilder.Entity("PanoramicData.PatchMagic.Models.Cve.BaseMetrics", b =>
                {
                    b.HasOne("PanoramicData.PatchMagic.Models.Cve.Entry", "Entry")
                        .WithOne("BaseMetrics")
                        .HasForeignKey("PanoramicData.PatchMagic.Models.Cve.BaseMetrics", "EntryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PanoramicData.PatchMagic.Models.Cve.CveReferences", b =>
                {
                    b.HasOne("PanoramicData.PatchMagic.Models.Cve.Entry", "Entry")
                        .WithMany("References")
                        .HasForeignKey("EntryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PanoramicData.PatchMagic.Models.Cve.LogicalTest", b =>
                {
                    b.HasOne("PanoramicData.PatchMagic.Models.Cve.VulnerableConfiguration", "VulnerableConfiguration")
                        .WithMany("LogicalTests")
                        .HasForeignKey("VulnerableConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PanoramicData.PatchMagic.Models.Cve.ReferencesList", b =>
                {
                    b.HasOne("PanoramicData.PatchMagic.Models.Cve.CveReferences", "CveReferences")
                        .WithMany("ReferenceList")
                        .HasForeignKey("CveReferencesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PanoramicData.PatchMagic.Models.Cve.VulnerableConfiguration", b =>
                {
                    b.HasOne("PanoramicData.PatchMagic.Models.Cve.Entry", "Entry")
                        .WithOne("VulnerableConfiguration")
                        .HasForeignKey("PanoramicData.PatchMagic.Models.Cve.VulnerableConfiguration", "EntryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PanoramicData.PatchMagic.Models.Cve.VulnerableProduct", b =>
                {
                    b.HasOne("PanoramicData.PatchMagic.Models.Cve.VulnerableSoftwareList", "VulnerableSoftwareList")
                        .WithMany("VulnerableProducts")
                        .HasForeignKey("VulnerableSoftwareListId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PanoramicData.PatchMagic.Models.Cve.VulnerableSoftwareList", b =>
                {
                    b.HasOne("PanoramicData.PatchMagic.Models.Cve.Entry", "Entry")
                        .WithOne("VulnerableSoftwareList")
                        .HasForeignKey("PanoramicData.PatchMagic.Models.Cve.VulnerableSoftwareList", "EntryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
