using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeployGame.Migrations
{
    /// <inheritdoc />
    public partial class CreateLinkToDownloadGameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
migrationBuilder.CreateTable(
                name: "link_to_download_game",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Link = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Avalible = table.Column<int>(nullable: false, defaultValue: 0),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_link_to_download_game", x => x.Id);
                    table.UniqueConstraint("AK_link_to_download_game_Link", x => x.Link);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
migrationBuilder.DropTable(
                name: "link_to_download_game");
        }
    }
}
