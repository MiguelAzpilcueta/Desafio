using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio.Migrations
{
    /// <inheritdoc />
    public partial class CambioEnCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VENTA_CLIENTE_ClienteIdCliente",
                table: "VENTA");

            migrationBuilder.RenameColumn(
                name: "ClienteIdCliente",
                table: "VENTA",
                newName: "ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_VENTA_ClienteIdCliente",
                table: "VENTA",
                newName: "IX_VENTA_ClienteId");

            migrationBuilder.RenameColumn(
                name: "IdCliente",
                table: "CLIENTE",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Stock",
                table: "PRODUCTOS",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "PRODUCTOS",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "PRODUCTOS",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<Guid>(
                name: "GUID",
                table: "CLIENTE",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_VENTA_CLIENTE_ClienteId",
                table: "VENTA",
                column: "ClienteId",
                principalTable: "CLIENTE",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VENTA_CLIENTE_ClienteId",
                table: "VENTA");

            migrationBuilder.DropColumn(
                name: "GUID",
                table: "CLIENTE");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "VENTA",
                newName: "ClienteIdCliente");

            migrationBuilder.RenameIndex(
                name: "IX_VENTA_ClienteId",
                table: "VENTA",
                newName: "IX_VENTA_ClienteIdCliente");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CLIENTE",
                newName: "IdCliente");

            migrationBuilder.AlterColumn<int>(
                name: "Stock",
                table: "PRODUCTOS",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "PRODUCTOS",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "PRODUCTOS",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VENTA_CLIENTE_ClienteIdCliente",
                table: "VENTA",
                column: "ClienteIdCliente",
                principalTable: "CLIENTE",
                principalColumn: "IdCliente");
        }
    }
}
