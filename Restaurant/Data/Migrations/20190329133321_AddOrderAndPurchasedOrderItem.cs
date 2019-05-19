using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Restaurant.Data.Migrations
{
    public partial class AddOrderAndPurchasedOrderItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchsedProducts");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "MenuItems",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<int>(nullable: false),
                    ApplicationUserId1 = table.Column<string>(nullable: true),
                    Comments = table.Column<string>(nullable: true),
                    CouponCode = table.Column<string>(nullable: true),
                    OrderStatus = table.Column<string>(nullable: true),
                    Orderdate = table.Column<DateTime>(nullable: false),
                    PickUpTime = table.Column<DateTime>(nullable: false),
                    TotalPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_ApplicationUserId1",
                        column: x => x.ApplicationUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    MenuItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchasedOrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Count = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    MenuItemId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    OrderId = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasedOrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchasedOrderItems_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchasedOrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_OrderId",
                table: "MenuItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ApplicationUserId1",
                table: "Orders",
                column: "ApplicationUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedOrderItems_MenuItemId",
                table: "PurchasedOrderItems",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedOrderItems_OrderId",
                table: "PurchasedOrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ApplicationUserId",
                table: "ShoppingCartItems",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_MenuItemId",
                table: "ShoppingCartItems",
                column: "MenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Orders_OrderId",
                table: "MenuItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Orders_OrderId",
                table: "MenuItems");

            migrationBuilder.DropTable(
                name: "PurchasedOrderItems");

            migrationBuilder.DropTable(
                name: "ShoppingCartItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_OrderId",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "MenuItems");

            migrationBuilder.CreateTable(
                name: "PurchsedProducts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    MenuItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchsedProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchsedProducts_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchsedProducts_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchsedProducts_ApplicationUserId",
                table: "PurchsedProducts",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchsedProducts_MenuItemId",
                table: "PurchsedProducts",
                column: "MenuItemId");
        }
    }
}
