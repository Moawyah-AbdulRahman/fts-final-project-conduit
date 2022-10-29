using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace conduit.db.Migrations
{
    public partial class ChangeTablesNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleUser");

            migrationBuilder.DropTable(
                name: "UserUser");

            migrationBuilder.CreateTable(
                name: "ArticleEntityUserEntity",
                columns: table => new
                {
                    FavoriteArticlesId = table.Column<long>(type: "bigint", nullable: false),
                    FavouritingUsersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleEntityUserEntity", x => new { x.FavoriteArticlesId, x.FavouritingUsersId });
                    table.ForeignKey(
                        name: "FK_ArticleEntityUserEntity_Articles_FavoriteArticlesId",
                        column: x => x.FavoriteArticlesId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ArticleEntityUserEntity_Users_FavouritingUsersId",
                        column: x => x.FavouritingUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "UserEntityUserEntity",
                columns: table => new
                {
                    FollowedUsersId = table.Column<long>(type: "bigint", nullable: false),
                    FollowersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntityUserEntity", x => new { x.FollowedUsersId, x.FollowersId });
                    table.ForeignKey(
                        name: "FK_UserEntityUserEntity_Users_FollowedUsersId",
                        column: x => x.FollowedUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserEntityUserEntity_Users_FollowersId",
                        column: x => x.FollowersId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleEntityUserEntity_FavouritingUsersId",
                table: "ArticleEntityUserEntity",
                column: "FavouritingUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEntityUserEntity_FollowersId",
                table: "UserEntityUserEntity",
                column: "FollowersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleEntityUserEntity");

            migrationBuilder.DropTable(
                name: "UserEntityUserEntity");

            migrationBuilder.CreateTable(
                name: "ArticleUser",
                columns: table => new
                {
                    FavoriteArticlesId = table.Column<long>(type: "bigint", nullable: false),
                    FavouritingUsersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleUser", x => new { x.FavoriteArticlesId, x.FavouritingUsersId });
                    table.ForeignKey(
                        name: "FK_ArticleUser_Articles_FavoriteArticlesId",
                        column: x => x.FavoriteArticlesId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ArticleUser_Users_FavouritingUsersId",
                        column: x => x.FavouritingUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "UserUser",
                columns: table => new
                {
                    FollowedUsersId = table.Column<long>(type: "bigint", nullable: false),
                    FollowersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUser", x => new { x.FollowedUsersId, x.FollowersId });
                    table.ForeignKey(
                        name: "FK_UserUser_Users_FollowedUsersId",
                        column: x => x.FollowedUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserUser_Users_FollowersId",
                        column: x => x.FollowersId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleUser_FavouritingUsersId",
                table: "ArticleUser",
                column: "FavouritingUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_UserUser_FollowersId",
                table: "UserUser",
                column: "FollowersId");
        }
    }
}
