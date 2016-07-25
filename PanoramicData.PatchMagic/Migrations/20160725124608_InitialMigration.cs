using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PanoramicData.PatchMagic.Migrations
{
	public partial class InitialMigration : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "CpeItems",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					Cpe23Name = table.Column<string>(nullable: true),
					Name = table.Column<string>(nullable: true),
					Title = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_CpeItems", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "References",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					CpeItemId = table.Column<int>(nullable: true),
					Href = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_References", x => x.Id);
					table.ForeignKey(
						name: "FK_References_CpeItems_CpeItemId",
						column: x => x.CpeItemId,
						principalTable: "CpeItems",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateIndex(
				name: "IX_References_CpeItemId",
				table: "References",
				column: "CpeItemId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "References");

			migrationBuilder.DropTable(
				name: "CpeItems");
		}
	}
}
