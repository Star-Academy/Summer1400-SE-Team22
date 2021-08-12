using Microsoft.EntityFrameworkCore.Migrations;

namespace SampleLibrary.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    WordContent = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.WordContent);
                });

            migrationBuilder.CreateTable(
                name: "WordInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WordContent = table.Column<string>(type: "varchar(50)", nullable: true),
                    FileName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordInfo_Word",
                        column: x => x.WordContent,
                        principalTable: "Words",
                        principalColumn: "WordContent",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WordInfos_WordContent",
                table: "WordInfos",
                column: "WordContent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WordInfos");

            migrationBuilder.DropTable(
                name: "Words");
        }
    }
}
