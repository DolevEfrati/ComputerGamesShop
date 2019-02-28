using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ComputerGamesShop.Migrations
{
    public partial class orderItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Order_OrderID",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_OrderID",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "Game");

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    gameId = table.Column<int>(nullable: false),
                    orderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrderItems_Game_gameId",
                        column: x => x.gameId,
                        principalTable: "Game",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Order_orderId",
                        column: x => x.orderId,
                        principalTable: "Order",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_gameId",
                table: "OrderItems",
                column: "gameId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_orderId",
                table: "OrderItems",
                column: "orderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.AddColumn<int>(
                name: "OrderID",
                table: "Game",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Game_OrderID",
                table: "Game",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Order_OrderID",
                table: "Game",
                column: "OrderID",
                principalTable: "Order",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
