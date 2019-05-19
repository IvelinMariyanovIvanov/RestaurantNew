using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Restaurant.Data.Migrations
{
    public partial class RenameShoppingCartToPurchasedProduct1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_AspNetUsers_ApplicationUserId",
                table: "ShoppingCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_MenuItems_MenuItemId",
                table: "ShoppingCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCarts",
                table: "ShoppingCarts");

            migrationBuilder.RenameTable(
                name: "ShoppingCarts",
                newName: "PurchsedProducts");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCarts_MenuItemId",
                table: "PurchsedProducts",
                newName: "IX_PurchsedProducts_MenuItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCarts_ApplicationUserId",
                table: "PurchsedProducts",
                newName: "IX_PurchsedProducts_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchsedProducts",
                table: "PurchsedProducts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchsedProducts_AspNetUsers_ApplicationUserId",
                table: "PurchsedProducts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchsedProducts_MenuItems_MenuItemId",
                table: "PurchsedProducts",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchsedProducts_AspNetUsers_ApplicationUserId",
                table: "PurchsedProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchsedProducts_MenuItems_MenuItemId",
                table: "PurchsedProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchsedProducts",
                table: "PurchsedProducts");

            migrationBuilder.RenameTable(
                name: "PurchsedProducts",
                newName: "ShoppingCarts");

            migrationBuilder.RenameIndex(
                name: "IX_PurchsedProducts_MenuItemId",
                table: "ShoppingCarts",
                newName: "IX_ShoppingCarts_MenuItemId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchsedProducts_ApplicationUserId",
                table: "ShoppingCarts",
                newName: "IX_ShoppingCarts_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCarts",
                table: "ShoppingCarts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_AspNetUsers_ApplicationUserId",
                table: "ShoppingCarts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_MenuItems_MenuItemId",
                table: "ShoppingCarts",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
