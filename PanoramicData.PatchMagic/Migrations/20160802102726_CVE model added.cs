using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PanoramicData.PatchMagic.Migrations
{
    public partial class CVEmodeladded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CveId = table.Column<string>(nullable: false),
                    CweId = table.Column<string>(nullable: true),
                    LastModifiedDateTime = table.Column<DateTime>(nullable: true),
                    PublisheDateTime = table.Column<DateTime>(nullable: false),
                    Summary = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BaseMetrics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccessComplexity = table.Column<int>(nullable: false),
                    AccessVector = table.Column<string>(nullable: true),
                    Authentication = table.Column<int>(nullable: false),
                    AvailabilityImpact = table.Column<int>(nullable: false),
                    ConfidentialityImpact = table.Column<int>(nullable: false),
                    EntryId = table.Column<int>(nullable: false),
                    GeneratedOn = table.Column<DateTime>(nullable: false),
                    IntegrityImpact = table.Column<int>(nullable: false),
                    Score = table.Column<float>(nullable: false),
                    Source = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseMetrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseMetrics_Entries_EntryId",
                        column: x => x.EntryId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CveReferences",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EntryId = table.Column<int>(nullable: false),
                    ReferenceType = table.Column<int>(nullable: false),
                    Source = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CveReferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CveReferences_Entries_EntryId",
                        column: x => x.EntryId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VulnerableConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EntryId = table.Column<int>(nullable: false),
                    VcId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VulnerableConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VulnerableConfigurations_Entries_EntryId",
                        column: x => x.EntryId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VulnerableSoftwareLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EntryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VulnerableSoftwareLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VulnerableSoftwareLists_Entries_EntryId",
                        column: x => x.EntryId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReferencesLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CveReferencesId = table.Column<int>(nullable: false),
                    Href = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferencesLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReferencesLists_CveReferences_CveReferencesId",
                        column: x => x.CveReferencesId,
                        principalTable: "CveReferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogicalTest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Negate = table.Column<bool>(nullable: false),
                    Operator = table.Column<int>(nullable: false),
                    VulnerableConfigurationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogicalTest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogicalTest_VulnerableConfigurations_VulnerableConfigurationId",
                        column: x => x.VulnerableConfigurationId,
                        principalTable: "VulnerableConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VulnerableProducts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Product = table.Column<string>(nullable: true),
                    VulnerableSoftwareListId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VulnerableProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VulnerableProducts_VulnerableSoftwareLists_VulnerableSoftwareListId",
                        column: x => x.VulnerableSoftwareListId,
                        principalTable: "VulnerableSoftwareLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseMetrics_EntryId",
                table: "BaseMetrics",
                column: "EntryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CveReferences_EntryId",
                table: "CveReferences",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_LogicalTest_VulnerableConfigurationId",
                table: "LogicalTest",
                column: "VulnerableConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferencesLists_CveReferencesId",
                table: "ReferencesLists",
                column: "CveReferencesId");

            migrationBuilder.CreateIndex(
                name: "IX_VulnerableConfigurations_EntryId",
                table: "VulnerableConfigurations",
                column: "EntryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VulnerableProducts_VulnerableSoftwareListId",
                table: "VulnerableProducts",
                column: "VulnerableSoftwareListId");

            migrationBuilder.CreateIndex(
                name: "IX_VulnerableSoftwareLists_EntryId",
                table: "VulnerableSoftwareLists",
                column: "EntryId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseMetrics");

            migrationBuilder.DropTable(
                name: "LogicalTest");

            migrationBuilder.DropTable(
                name: "ReferencesLists");

            migrationBuilder.DropTable(
                name: "VulnerableProducts");

            migrationBuilder.DropTable(
                name: "VulnerableConfigurations");

            migrationBuilder.DropTable(
                name: "CveReferences");

            migrationBuilder.DropTable(
                name: "VulnerableSoftwareLists");

            migrationBuilder.DropTable(
                name: "Entries");
        }
    }
}
