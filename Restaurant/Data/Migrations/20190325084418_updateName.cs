using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Restaurant.Data.Migrations
{
    public partial class updateName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItes_Categories_CategoryId",
                table: "MenuItes");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItes_SubCategories_SubCategoryId",
                table: "MenuItes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItes",
                table: "MenuItes");

            migrationBuilder.RenameTable(
                name: "MenuItes",
                newName: "MenuItems");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItes_SubCategoryId",
                table: "MenuItems",
                newName: "IX_MenuItems_SubCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItes_CategoryId",
                table: "MenuItems",
                newName: "IX_MenuItems_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItems",
                table: "MenuItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Categories_CategoryId",
                table: "MenuItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_SubCategories_SubCategoryId",
                table: "MenuItems",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Categories_CategoryId",
                table: "MenuItems");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_SubCategories_SubCategoryId",
                table: "MenuItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItems",
                table: "MenuItems");

            migrationBuilder.RenameTable(
                name: "MenuItems",
                newName: "MenuItes");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItems_SubCategoryId",
                table: "MenuItes",
                newName: "IX_MenuItes_SubCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItems_CategoryId",
                table: "MenuItes",
                newName: "IX_MenuItes_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItes",
                table: "MenuItes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItes_Categories_CategoryId",
                table: "MenuItes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItes_SubCategories_SubCategoryId",
                table: "MenuItes",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
