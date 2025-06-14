using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeWorld.Migrations
{
    /// <inheritdoc />
    public partial class newupdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteAnime_Animes_AnimeId",
                table: "FavoriteAnime");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteAnime_Users_UsersId",
                table: "FavoriteAnime");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteAnime",
                table: "FavoriteAnime");

            migrationBuilder.RenameTable(
                name: "FavoriteAnime",
                newName: "FavoriteAnimes");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteAnime_UsersId",
                table: "FavoriteAnimes",
                newName: "IX_FavoriteAnimes_UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteAnime_AnimeId",
                table: "FavoriteAnimes",
                newName: "IX_FavoriteAnimes_AnimeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteAnimes",
                table: "FavoriteAnimes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteAnimes_Animes_AnimeId",
                table: "FavoriteAnimes",
                column: "AnimeId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteAnimes_Users_UsersId",
                table: "FavoriteAnimes",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteAnimes_Animes_AnimeId",
                table: "FavoriteAnimes");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteAnimes_Users_UsersId",
                table: "FavoriteAnimes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteAnimes",
                table: "FavoriteAnimes");

            migrationBuilder.RenameTable(
                name: "FavoriteAnimes",
                newName: "FavoriteAnime");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteAnimes_UsersId",
                table: "FavoriteAnime",
                newName: "IX_FavoriteAnime_UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteAnimes_AnimeId",
                table: "FavoriteAnime",
                newName: "IX_FavoriteAnime_AnimeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteAnime",
                table: "FavoriteAnime",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteAnime_Animes_AnimeId",
                table: "FavoriteAnime",
                column: "AnimeId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteAnime_Users_UsersId",
                table: "FavoriteAnime",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
