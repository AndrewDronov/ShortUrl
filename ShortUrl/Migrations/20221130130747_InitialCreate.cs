using Microsoft.EntityFrameworkCore.Migrations;

namespace ShortUrl.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "url",
                columns: table => new
                {
                    token = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false, defaultValueSql: "\"substring\"(md5((random())::text), 0, 9)"),
                    link = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("url_pk", x => x.token);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "url");
        }
    }
}
