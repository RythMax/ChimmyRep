using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TommyAPI.Data.Migrations
{
    public partial class NewAPITest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Apellidos = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Dirección = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    Cédula = table.Column<string>(type: "nchar(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoodTypes",
                columns: table => new
                {
                    FoodType_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Detalle = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodTypes", x => x.FoodType_ID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID_Orden = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOrden = table.Column<DateTime>(nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal (18, 2)", nullable: false),
                    ValorImpuesto = table.Column<decimal>(type: "decimal (18, 2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal (18, 2)", nullable: false),
                    Anotaciones = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID_Orden);
                });

            migrationBuilder.CreateTable(
                name: "PayingMethods",
                columns: table => new
                {
                    Method_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormaDePago = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayingMethods", x => x.Method_Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Status_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Status_ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    ID_Comidas = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripción = table.Column<string>(maxLength: 40, nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    FoodType_ID = table.Column<int>(nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal (18, 2)", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal (18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.ID_Comidas);
                    table.ForeignKey(
                        name: "FK_Foods_FoodTypes_FoodType_ID",
                        column: x => x.FoodType_ID,
                        principalTable: "FoodTypes",
                        principalColumn: "FoodType_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Invoice_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    Method_Id = table.Column<int>(nullable: false),
                    FechaFactura = table.Column<DateTime>(nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal (18, 2)", nullable: false),
                    ValorImpuesto = table.Column<decimal>(type: "decimal (18, 2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal (18, 2)", nullable: false),
                    Nombres = table.Column<string>(maxLength: 100, nullable: false),
                    Apellidos = table.Column<string>(maxLength: 100, nullable: false),
                    Dirección = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Invoice_ID);
                    table.ForeignKey(
                        name: "FK_Invoices_PayingMethods_Method_Id",
                        column: x => x.Method_Id,
                        principalTable: "PayingMethods",
                        principalColumn: "Method_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Queues",
                columns: table => new
                {
                    Pedido_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    Method_Id = table.Column<int>(nullable: false),
                    FechaFactura = table.Column<DateTime>(nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal (18, 2)", nullable: false),
                    ValorImpuesto = table.Column<decimal>(type: "decimal (18, 2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal (18, 2)", nullable: false),
                    Nombres = table.Column<string>(maxLength: 100, nullable: true),
                    Apellidos = table.Column<string>(maxLength: 100, nullable: true),
                    Dirección = table.Column<string>(maxLength: 150, nullable: true),
                    Status_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Queues", x => x.Pedido_ID);
                    table.ForeignKey(
                        name: "FK_Queues_PayingMethods_Method_Id",
                        column: x => x.Method_Id,
                        principalTable: "PayingMethods",
                        principalColumn: "Method_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Queues_Statuses_Status_ID",
                        column: x => x.Status_ID,
                        principalTable: "Statuses",
                        principalColumn: "Status_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Queues_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    CartId = table.Column<string>(nullable: false),
                    ID_Comidas = table.Column<int>(nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    PreciodeCarro = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Username = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.CartId);
                    table.ForeignKey(
                        name: "FK_Carts_Foods_ID_Comidas",
                        column: x => x.ID_Comidas,
                        principalTable: "Foods",
                        principalColumn: "ID_Comidas",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Comidas = table.Column<int>(nullable: false),
                    CantidadDeArticulos = table.Column<decimal>(type: "decimal (18, 2)", nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "decimal (18, 2)", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal (18, 2)", nullable: false),
                    ID_Orden = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailID);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Foods_ID_Comidas",
                        column: x => x.ID_Comidas,
                        principalTable: "Foods",
                        principalColumn: "ID_Comidas",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_ID_Orden",
                        column: x => x.ID_Orden,
                        principalTable: "Orders",
                        principalColumn: "ID_Orden",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDetails",
                columns: table => new
                {
                    InvoiceDetail_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Comidas = table.Column<int>(nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal (18, 2)", nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "decimal (18, 2)", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal (18, 2)", nullable: false),
                    Invoice_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDetails", x => x.InvoiceDetail_ID);
                    table.ForeignKey(
                        name: "FK_InvoiceDetails_Foods_ID_Comidas",
                        column: x => x.ID_Comidas,
                        principalTable: "Foods",
                        principalColumn: "ID_Comidas",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceDetails_Invoices_Invoice_ID",
                        column: x => x.Invoice_ID,
                        principalTable: "Invoices",
                        principalColumn: "Invoice_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QueueDetails",
                columns: table => new
                {
                    QueueDetail_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Comidas = table.Column<int>(nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal (18,2)", nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "decimal (18, 2)", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal (18, 2)", nullable: false),
                    Pedido_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueueDetails", x => x.QueueDetail_ID);
                    table.ForeignKey(
                        name: "FK_QueueDetails_Foods_ID_Comidas",
                        column: x => x.ID_Comidas,
                        principalTable: "Foods",
                        principalColumn: "ID_Comidas",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QueueDetails_Queues_Pedido_ID",
                        column: x => x.Pedido_ID,
                        principalTable: "Queues",
                        principalColumn: "Pedido_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ID_Comidas",
                table: "Carts",
                column: "ID_Comidas");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_FoodType_ID",
                table: "Foods",
                column: "FoodType_ID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_ID_Comidas",
                table: "InvoiceDetails",
                column: "ID_Comidas");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_Invoice_ID",
                table: "InvoiceDetails",
                column: "Invoice_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_Method_Id",
                table: "Invoices",
                column: "Method_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_UserId",
                table: "Invoices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ID_Comidas",
                table: "OrderDetails",
                column: "ID_Comidas");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ID_Orden",
                table: "OrderDetails",
                column: "ID_Orden");

            migrationBuilder.CreateIndex(
                name: "IX_QueueDetails_ID_Comidas",
                table: "QueueDetails",
                column: "ID_Comidas");

            migrationBuilder.CreateIndex(
                name: "IX_QueueDetails_Pedido_ID",
                table: "QueueDetails",
                column: "Pedido_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Queues_Method_Id",
                table: "Queues",
                column: "Method_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Queues_Status_ID",
                table: "Queues",
                column: "Status_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Queues_UserId",
                table: "Queues",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "InvoiceDetails");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "QueueDetails");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Queues");

            migrationBuilder.DropTable(
                name: "FoodTypes");

            migrationBuilder.DropTable(
                name: "PayingMethods");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
