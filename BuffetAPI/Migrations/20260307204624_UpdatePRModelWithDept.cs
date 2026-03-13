using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuffetAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePRModelWithDept : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "Purchase_Requisition_Detail",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PR_Date",
                table: "Purchase_Requisition",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PR_Department",
                table: "Purchase_Requisition",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remark",
                table: "Purchase_Requisition_Detail");

            migrationBuilder.DropColumn(
                name: "PR_Department",
                table: "Purchase_Requisition");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PR_Date",
                table: "Purchase_Requisition",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
