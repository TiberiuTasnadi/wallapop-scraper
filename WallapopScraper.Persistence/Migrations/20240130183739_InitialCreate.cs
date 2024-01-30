using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WallapopScraper.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkerConfiguration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    CardClass = table.Column<string>(type: "TEXT", nullable: false),
                    TitleClass = table.Column<string>(type: "TEXT", nullable: false),
                    PriceClass = table.Column<string>(type: "TEXT", nullable: false),
                    DataClass = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerConfiguration", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkerConfiguration");
        }
    }
}
