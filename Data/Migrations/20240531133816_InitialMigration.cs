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
            migrationBuilder.CreateTable(
                name: "Caller_Tunes",
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
                    table.PrimaryKey("PK_Caller_Tunes", x => x.Tune_Id);
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
                    Contact_Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Feedback_Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Newsletter_Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Package_Off_Net_Mins = table.Column<int>(type: "int", nullable: false),
                    Package_On_Net_Mins = table.Column<int>(type: "int", nullable: false),
                    Package_Data = table.Column<int>(type: "int", nullable: false),
                    Package_SMS = table.Column<int>(type: "int", nullable: false),
                    Package_Duration = table.Column<int>(type: "int", nullable: false),
                    Package_Price = table.Column<int>(type: "int", nullable: false),
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
                    Recharge_Tax_Amount = table.Column<double>(type: "float", nullable: false),
                    Recharge_Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recharges", x => x.Recharge_Id);
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
                        name: "FK_Services_Caller_Tunes_Caller_Tune_Id",
                        column: x => x.Caller_Tune_Id,
                        principalTable: "Caller_Tunes",
                        principalColumn: "Tune_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Package_Transactions",
                columns: table => new
                {
                    PackageTransaction_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Package_Id = table.Column<int>(type: "int", nullable: false),
                    Mobile_Number = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Package_Transactions", x => x.PackageTransaction_Id);
                    table.ForeignKey(
                        name: "FK_Package_Transactions_AspNetUsers_User_Id",
                        column: x => x.User_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Package_Transactions_Packages_Package_Id",
                        column: x => x.Package_Id,
                        principalTable: "Packages",
                        principalColumn: "Package_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recharge_Transactions",
                columns: table => new
                {
                    RechargeTransaction_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Recharge_Id = table.Column<int>(type: "int", nullable: false),
                    Mobile_Number = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recharge_Transactions", x => x.RechargeTransaction_Id);
                    table.ForeignKey(
                        name: "FK_Recharge_Transactions_Recharges_Recharge_Id",
                        column: x => x.Recharge_Id,
                        principalTable: "Recharges",
                        principalColumn: "Recharge_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Package_Transactions_Package_Id",
                table: "Package_Transactions",
                column: "Package_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Package_Transactions_User_Id",
                table: "Package_Transactions",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Recharge_Transactions_Recharge_Id",
                table: "Recharge_Transactions",
                column: "Recharge_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Services_Caller_Tune_Id",
                table: "Services",
                column: "Caller_Tune_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Services_User_Id",
                table: "Services",
                column: "User_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Newsletter");

            migrationBuilder.DropTable(
                name: "Package_Transactions");

            migrationBuilder.DropTable(
                name: "Recharge_Transactions");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "Recharges");

            migrationBuilder.DropTable(
                name: "Caller_Tunes");
        }
    }
}
