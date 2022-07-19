using Microsoft.EntityFrameworkCore.Migrations;

namespace TomyChimmy.Migrations
{
    public partial class Backagain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Queues_Pedido_ID",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_Pedido_ID",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Pedido_ID",
                table: "Carts");

            migrationBuilder.AlterColumn<string>(
                name: "Nombres",
                table: "Queues",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Dirección",
                table: "Queues",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Apellidos",
                table: "Queues",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nombres",
                table: "Queues",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Dirección",
                table: "Queues",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Apellidos",
                table: "Queues",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Pedido_ID",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_Pedido_ID",
                table: "Carts",
                column: "Pedido_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Queues_Pedido_ID",
                table: "Carts",
                column: "Pedido_ID",
                principalTable: "Queues",
                principalColumn: "Pedido_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
