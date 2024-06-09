using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineMobileRecharge.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "CallerTunes",
                columns: table => new
                {
                    Tune_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tune_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tune_Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tune_Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tune_Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallerTunes", x => x.Tune_Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Contact_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contact_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact_Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact_Intrest = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact_Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Added = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Contact_Id);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Feedback_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Feedback_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Feedback_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Feedback_Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Added = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Feedback_Id);
                });

            migrationBuilder.CreateTable(
                name: "Newsletter",
                columns: table => new
                {
                    Newsletter_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Newsletter_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Added = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Newsletter", x => x.Newsletter_Id);
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Package_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Package_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Package_Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Package_Off_Net_Mins = table.Column<long>(type: "bigint", nullable: false),
                    Package_On_Net_Mins = table.Column<long>(type: "bigint", nullable: false),
                    Package_Data = table.Column<long>(type: "bigint", nullable: false),
                    Package_SMS = table.Column<long>(type: "bigint", nullable: false),
                    Package_Duration = table.Column<int>(type: "int", nullable: false),
                    Package_Price = table.Column<double>(type: "float", nullable: false),
                    Package_Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Package_Id);
                });

            migrationBuilder.CreateTable(
                name: "Recharges",
                columns: table => new
                {
                    Recharge_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Recharge_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recharge_Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recharge_Amount = table.Column<double>(type: "float", nullable: false),
                    Recharge_Price = table.Column<double>(type: "float", nullable: false),
                    Recharge_Tax_Rate = table.Column<double>(type: "float", nullable: false),
                    Recharge_Taxed_Amount = table.Column<double>(type: "float", nullable: false),
                    Recharge_Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recharges", x => x.Recharge_Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxRates",
                columns: table => new
                {
                    Tax_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tax_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tax_Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tax_Rate = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxRates", x => x.Tax_Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Service_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Do_Not_Disturb = table.Column<bool>(type: "bit", nullable: false),
                    Caller_Tune_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Service_Id);
                    table.ForeignKey(
                        name: "FK_Services_AspNetUsers_User_Id",
                        column: x => x.User_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Services_CallerTunes_Caller_Tune_Id",
                        column: x => x.Caller_Tune_Id,
                        principalTable: "CallerTunes",
                        principalColumn: "Tune_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTransactions",
                columns: table => new
                {
                    ServiceTransaction_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Session_Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tune_Id = table.Column<int>(type: "int", nullable: false),
                    User_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Mobile_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Transaction_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTransactions", x => x.ServiceTransaction_Id);
                    table.ForeignKey(
                        name: "FK_ServiceTransactions_AspNetUsers_User_Id",
                        column: x => x.User_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceTransactions_CallerTunes_Tune_Id",
                        column: x => x.Tune_Id,
                        principalTable: "CallerTunes",
                        principalColumn: "Tune_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackageTransactions",
                columns: table => new
                {
                    PackageTransaction_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Session_Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Package_Id = table.Column<int>(type: "int", nullable: false),
                    Mobile_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Transaction_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageTransactions", x => x.PackageTransaction_Id);
                    table.ForeignKey(
                        name: "FK_PackageTransactions_AspNetUsers_User_Id",
                        column: x => x.User_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackageTransactions_Packages_Package_Id",
                        column: x => x.Package_Id,
                        principalTable: "Packages",
                        principalColumn: "Package_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RechargeTransactions",
                columns: table => new
                {
                    RechargeTransaction_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Session_Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recharge_Id = table.Column<int>(type: "int", nullable: false),
                    Mobile_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Transaction_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RechargeTransactions", x => x.RechargeTransaction_Id);
                    table.ForeignKey(
                        name: "FK_RechargeTransactions_Recharges_Recharge_Id",
                        column: x => x.Recharge_Id,
                        principalTable: "Recharges",
                        principalColumn: "Recharge_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomRechargeTransactions",
                columns: table => new
                {
                    CRecharge_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CRecharge_Price = table.Column<double>(type: "float", nullable: false),
                    Tax_Id = table.Column<int>(type: "int", nullable: false),
                    TaxRateTax_Id = table.Column<int>(type: "int", nullable: false),
                    Mobile_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recharge_Type = table.Column<int>(type: "int", nullable: false),
                    Transaction_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomRechargeTransactions", x => x.CRecharge_Id);
                    table.ForeignKey(
                        name: "FK_CustomRechargeTransactions_TaxRates_TaxRateTax_Id",
                        column: x => x.TaxRateTax_Id,
                        principalTable: "TaxRates",
                        principalColumn: "Tax_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomRechargeTransactions_TaxRateTax_Id",
                table: "CustomRechargeTransactions",
                column: "TaxRateTax_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PackageTransactions_Package_Id",
                table: "PackageTransactions",
                column: "Package_Id");

            migrationBuilder.CreateIndex(
                name: "IX_PackageTransactions_User_Id",
                table: "PackageTransactions",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_RechargeTransactions_Recharge_Id",
                table: "RechargeTransactions",
                column: "Recharge_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Services_Caller_Tune_Id",
                table: "Services",
                column: "Caller_Tune_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Services_User_Id",
                table: "Services",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTransactions_Tune_Id",
                table: "ServiceTransactions",
                column: "Tune_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTransactions_User_Id",
                table: "ServiceTransactions",
                column: "User_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "CustomRechargeTransactions");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Newsletter");

            migrationBuilder.DropTable(
                name: "PackageTransactions");

            migrationBuilder.DropTable(
                name: "RechargeTransactions");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "ServiceTransactions");

            migrationBuilder.DropTable(
                name: "TaxRates");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "Recharges");

            migrationBuilder.DropTable(
                name: "CallerTunes");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
