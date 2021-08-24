using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gab.WebAppNet5.Data.Migrations
{
    public partial class CatalogParentIdNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Catalog_Catalog_ParentId",
                table: "Catalog");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentId",
                table: "Catalog",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Catalog_Catalog_ParentId",
                table: "Catalog",
                column: "ParentId",
                principalTable: "Catalog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Catalog_Catalog_ParentId",
                table: "Catalog");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentId",
                table: "Catalog",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Catalog_Catalog_ParentId",
                table: "Catalog",
                column: "ParentId",
                principalTable: "Catalog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
