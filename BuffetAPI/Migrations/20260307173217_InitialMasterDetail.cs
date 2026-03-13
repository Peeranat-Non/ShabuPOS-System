using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuffetAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMasterDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Expense",
                columns: table => new
                {
                    Exp_id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Exp_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Exp_time = table.Column<TimeSpan>(type: "time", nullable: true),
                    Exp_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expense", x => x.Exp_id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LineUserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.MemberId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Pro_id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Pro_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Pro_category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Pro_quan = table.Column<int>(type: "int", nullable: true),
                    Pro_stock = table.Column<int>(type: "int", nullable: true),
                    Pro_unit = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Pro_price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Pro_image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Pro_id);
                });

            migrationBuilder.CreateTable(
                name: "Shop",
                columns: table => new
                {
                    ShopId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ShopName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShopAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ShopPhone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shop", x => x.ShopId);
                });

            migrationBuilder.CreateTable(
                name: "fee",
                columns: table => new
                {
                    fee_id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Exp_id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    fee_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fee_time = table.Column<TimeSpan>(type: "time", nullable: true),
                    fee_total = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fee", x => x.fee_id);
                    table.ForeignKey(
                        name: "FK_fee_Expense_Exp_id",
                        column: x => x.Exp_id,
                        principalTable: "Expense",
                        principalColumn: "Exp_id");
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    MenuId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MenuName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MenuPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    MenuImage = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Pro_id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.MenuId);
                    table.ForeignKey(
                        name: "FK_Menu_Product_Pro_id",
                        column: x => x.Pro_id,
                        principalTable: "Product",
                        principalColumn: "Pro_id");
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ShopId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    EmployName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmployPosition = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EmploySdate = table.Column<DateOnly>(type: "date", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployId);
                    table.ForeignKey(
                        name: "FK_Employee_Shop_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shop",
                        principalColumn: "ShopId");
                });

            migrationBuilder.CreateTable(
                name: "Purchase_Order",
                columns: table => new
                {
                    Po_id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Po_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Po_time = table.Column<TimeSpan>(type: "time", nullable: true),
                    Po_employee = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Po_status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Po_approver = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Po_Buyreq = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase_Order", x => x.Po_id);
                    table.ForeignKey(
                        name: "FK_Purchase_Order_Employee_Po_employee",
                        column: x => x.Po_employee,
                        principalTable: "Employee",
                        principalColumn: "EmployId");
                });

            migrationBuilder.CreateTable(
                name: "Purchase_Requisition",
                columns: table => new
                {
                    PR_ID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Employ_ID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PR_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PR_Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase_Requisition", x => x.PR_ID);
                    table.ForeignKey(
                        name: "FK_Purchase_Requisition_Employee_Employ_ID",
                        column: x => x.Employ_ID,
                        principalTable: "Employee",
                        principalColumn: "EmployId");
                });

            migrationBuilder.CreateTable(
                name: "Purchase_Order_Detail",
                columns: table => new
                {
                    PO_Detail_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Po_id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Pro_id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Order_Qty = table.Column<int>(type: "int", nullable: false),
                    Unit_Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Total_Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase_Order_Detail", x => x.PO_Detail_ID);
                    table.ForeignKey(
                        name: "FK_Purchase_Order_Detail_Product_Pro_id",
                        column: x => x.Pro_id,
                        principalTable: "Product",
                        principalColumn: "Pro_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Purchase_Order_Detail_Purchase_Order_Po_id",
                        column: x => x.Po_id,
                        principalTable: "Purchase_Order",
                        principalColumn: "Po_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    Stock_ID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Pro_id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Exp_id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Po_id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Stock_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Stock_Time = table.Column<TimeSpan>(type: "time", nullable: true),
                    Transaction_Type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Stock_Qty = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.Stock_ID);
                    table.ForeignKey(
                        name: "FK_Stock_Expense_Exp_id",
                        column: x => x.Exp_id,
                        principalTable: "Expense",
                        principalColumn: "Exp_id");
                    table.ForeignKey(
                        name: "FK_Stock_Product_Pro_id",
                        column: x => x.Pro_id,
                        principalTable: "Product",
                        principalColumn: "Pro_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Stock_Purchase_Order_Po_id",
                        column: x => x.Po_id,
                        principalTable: "Purchase_Order",
                        principalColumn: "Po_id");
                });

            migrationBuilder.CreateTable(
                name: "Purchase_Requisition_Detail",
                columns: table => new
                {
                    PR_Detail_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PR_ID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Pro_id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Request_Qty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase_Requisition_Detail", x => x.PR_Detail_ID);
                    table.ForeignKey(
                        name: "FK_Purchase_Requisition_Detail_Product_Pro_id",
                        column: x => x.Pro_id,
                        principalTable: "Product",
                        principalColumn: "Pro_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Purchase_Requisition_Detail_Purchase_Requisition_PR_ID",
                        column: x => x.PR_ID,
                        principalTable: "Purchase_Requisition",
                        principalColumn: "PR_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Income",
                columns: table => new
                {
                    Income_id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Income_payment = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Income_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Income_time = table.Column<TimeSpan>(type: "time", nullable: true),
                    Income_description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Income_amount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Income", x => x.Income_id);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    DetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderHeaderId = table.Column<int>(type: "int", nullable: false),
                    MenuId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    OrderQuantity = table.Column<int>(type: "int", nullable: true),
                    ItemStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.DetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "MenuId");
                });

            migrationBuilder.CreateTable(
                name: "OrderHeader",
                columns: table => new
                {
                    OrderHeaderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    OrderDate = table.Column<DateOnly>(type: "date", nullable: true),
                    OrderTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    OrderTable = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    TotalStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHeader", x => x.OrderHeaderId);
                });

            migrationBuilder.CreateTable(
                name: "Package",
                columns: table => new
                {
                    Package_ID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Service_ID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Package_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Package_Price = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Package", x => x.Package_ID);
                });

            migrationBuilder.CreateTable(
                name: "Promotion",
                columns: table => new
                {
                    Promo_ID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Package_ID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Promo_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Promo_Discount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotion", x => x.Promo_ID);
                    table.ForeignKey(
                        name: "FK_Promotion_Package_Package_ID",
                        column: x => x.Package_ID,
                        principalTable: "Package",
                        principalColumn: "Package_ID");
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    ServiceId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EmployId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ServiceDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ServiceTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    ServiceNumberPeople = table.Column<int>(type: "int", nullable: true),
                    PackageId = table.Column<string>(type: "nvarchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.ServiceId);
                    table.ForeignKey(
                        name: "FK_Service_Employee_EmployId",
                        column: x => x.EmployId,
                        principalTable: "Employee",
                        principalColumn: "EmployId");
                    table.ForeignKey(
                        name: "FK_Service_Package_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Package",
                        principalColumn: "Package_ID");
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Pay_id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Pay_forkey = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Pay_mem = table.Column<int>(type: "int", maxLength: 10, nullable: true),
                    Pay_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Pay_time = table.Column<TimeSpan>(type: "time", nullable: true),
                    Pay_amont = table.Column<int>(type: "int", nullable: true),
                    Pay_channel = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Pay_id);
                    table.ForeignKey(
                        name: "FK_Payment_Members_Pay_mem",
                        column: x => x.Pay_mem,
                        principalTable: "Members",
                        principalColumn: "MemberId");
                    table.ForeignKey(
                        name: "FK_Payment_Promotion_Pay_forkey",
                        column: x => x.Pay_forkey,
                        principalTable: "Promotion",
                        principalColumn: "Promo_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ShopId",
                table: "Employee",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_fee_Exp_id",
                table: "fee",
                column: "Exp_id");

            migrationBuilder.CreateIndex(
                name: "IX_Income_Income_payment",
                table: "Income",
                column: "Income_payment");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_Pro_id",
                table: "Menu",
                column: "Pro_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_MenuId",
                table: "OrderDetail",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderHeaderId",
                table: "OrderDetail",
                column: "OrderHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeader_ServiceId",
                table: "OrderHeader",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Package_Service_ID",
                table: "Package",
                column: "Service_ID",
                unique: true,
                filter: "[Service_ID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_Pay_forkey",
                table: "Payment",
                column: "Pay_forkey");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_Pay_mem",
                table: "Payment",
                column: "Pay_mem");

            migrationBuilder.CreateIndex(
                name: "IX_Promotion_Package_ID",
                table: "Promotion",
                column: "Package_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_Order_Po_employee",
                table: "Purchase_Order",
                column: "Po_employee");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_Order_Detail_Po_id",
                table: "Purchase_Order_Detail",
                column: "Po_id");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_Order_Detail_Pro_id",
                table: "Purchase_Order_Detail",
                column: "Pro_id");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_Requisition_Employ_ID",
                table: "Purchase_Requisition",
                column: "Employ_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_Requisition_Detail_PR_ID",
                table: "Purchase_Requisition_Detail",
                column: "PR_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_Requisition_Detail_Pro_id",
                table: "Purchase_Requisition_Detail",
                column: "Pro_id");

            migrationBuilder.CreateIndex(
                name: "IX_Service_EmployId",
                table: "Service",
                column: "EmployId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_PackageId",
                table: "Service",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_Exp_id",
                table: "Stock",
                column: "Exp_id");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_Po_id",
                table: "Stock",
                column: "Po_id");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_Pro_id",
                table: "Stock",
                column: "Pro_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Income_Payment_Income_payment",
                table: "Income",
                column: "Income_payment",
                principalTable: "Payment",
                principalColumn: "Pay_id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_OrderHeader_OrderHeaderId",
                table: "OrderDetail",
                column: "OrderHeaderId",
                principalTable: "OrderHeader",
                principalColumn: "OrderHeaderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeader_Service_ServiceId",
                table: "OrderHeader",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Package_Service_Service_ID",
                table: "Package",
                column: "Service_ID",
                principalTable: "Service",
                principalColumn: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Shop_ShopId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Package_Service_Service_ID",
                table: "Package");

            migrationBuilder.DropTable(
                name: "fee");

            migrationBuilder.DropTable(
                name: "Income");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "Purchase_Order_Detail");

            migrationBuilder.DropTable(
                name: "Purchase_Requisition_Detail");

            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "OrderHeader");

            migrationBuilder.DropTable(
                name: "Purchase_Requisition");

            migrationBuilder.DropTable(
                name: "Expense");

            migrationBuilder.DropTable(
                name: "Purchase_Order");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Promotion");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Shop");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Package");
        }
    }
}
