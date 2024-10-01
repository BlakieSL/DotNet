using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project3.Migrations
{
    /// <inheritdoc />
    public partial class Update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Clients_IdClient",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_SoftwareSystems_IdSoftwareSystem",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_SoftwareSystemDiscounts_Discounts_IdDiscount",
                table: "SoftwareSystemDiscounts");

            migrationBuilder.DropForeignKey(
                name: "FK_SoftwareSystemDiscounts_SoftwareSystems_IdSoftwareSystem",
                table: "SoftwareSystemDiscounts");

            migrationBuilder.RenameColumn(
                name: "IdSoftwareSystem",
                table: "SoftwareSystemDiscounts",
                newName: "SoftwareSystemId");

            migrationBuilder.RenameColumn(
                name: "IdDiscount",
                table: "SoftwareSystemDiscounts",
                newName: "DiscountId");

            migrationBuilder.RenameIndex(
                name: "IX_SoftwareSystemDiscounts_IdSoftwareSystem",
                table: "SoftwareSystemDiscounts",
                newName: "IX_SoftwareSystemDiscounts_SoftwareSystemId");

            migrationBuilder.RenameColumn(
                name: "IdSoftwareSystem",
                table: "Contracts",
                newName: "SoftwareSystemId");

            migrationBuilder.RenameColumn(
                name: "IdClient",
                table: "Contracts",
                newName: "IndividualId");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_IdSoftwareSystem",
                table: "Contracts",
                newName: "IX_Contracts_SoftwareSystemId");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_IdClient",
                table: "Contracts",
                newName: "IX_Contracts_IndividualId");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "SoftwareSystems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "SoftwareSystems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Paid",
                table: "Contracts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_SoftwareSystems_CompanyId",
                table: "SoftwareSystems",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Clients_IndividualId",
                table: "Contracts",
                column: "IndividualId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_SoftwareSystems_SoftwareSystemId",
                table: "Contracts",
                column: "SoftwareSystemId",
                principalTable: "SoftwareSystems",
                principalColumn: "SoftwareSystemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SoftwareSystemDiscounts_Discounts_DiscountId",
                table: "SoftwareSystemDiscounts",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "DiscountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SoftwareSystemDiscounts_SoftwareSystems_SoftwareSystemId",
                table: "SoftwareSystemDiscounts",
                column: "SoftwareSystemId",
                principalTable: "SoftwareSystems",
                principalColumn: "SoftwareSystemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SoftwareSystems_Clients_CompanyId",
                table: "SoftwareSystems",
                column: "CompanyId",
                principalTable: "Clients",
                principalColumn: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Clients_IndividualId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_SoftwareSystems_SoftwareSystemId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_SoftwareSystemDiscounts_Discounts_DiscountId",
                table: "SoftwareSystemDiscounts");

            migrationBuilder.DropForeignKey(
                name: "FK_SoftwareSystemDiscounts_SoftwareSystems_SoftwareSystemId",
                table: "SoftwareSystemDiscounts");

            migrationBuilder.DropForeignKey(
                name: "FK_SoftwareSystems_Clients_CompanyId",
                table: "SoftwareSystems");

            migrationBuilder.DropIndex(
                name: "IX_SoftwareSystems_CompanyId",
                table: "SoftwareSystems");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "SoftwareSystems");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "SoftwareSystems");

            migrationBuilder.DropColumn(
                name: "Paid",
                table: "Contracts");

            migrationBuilder.RenameColumn(
                name: "SoftwareSystemId",
                table: "SoftwareSystemDiscounts",
                newName: "IdSoftwareSystem");

            migrationBuilder.RenameColumn(
                name: "DiscountId",
                table: "SoftwareSystemDiscounts",
                newName: "IdDiscount");

            migrationBuilder.RenameIndex(
                name: "IX_SoftwareSystemDiscounts_SoftwareSystemId",
                table: "SoftwareSystemDiscounts",
                newName: "IX_SoftwareSystemDiscounts_IdSoftwareSystem");

            migrationBuilder.RenameColumn(
                name: "SoftwareSystemId",
                table: "Contracts",
                newName: "IdSoftwareSystem");

            migrationBuilder.RenameColumn(
                name: "IndividualId",
                table: "Contracts",
                newName: "IdClient");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_SoftwareSystemId",
                table: "Contracts",
                newName: "IX_Contracts_IdSoftwareSystem");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_IndividualId",
                table: "Contracts",
                newName: "IX_Contracts_IdClient");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Clients_IdClient",
                table: "Contracts",
                column: "IdClient",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_SoftwareSystems_IdSoftwareSystem",
                table: "Contracts",
                column: "IdSoftwareSystem",
                principalTable: "SoftwareSystems",
                principalColumn: "SoftwareSystemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SoftwareSystemDiscounts_Discounts_IdDiscount",
                table: "SoftwareSystemDiscounts",
                column: "IdDiscount",
                principalTable: "Discounts",
                principalColumn: "DiscountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SoftwareSystemDiscounts_SoftwareSystems_IdSoftwareSystem",
                table: "SoftwareSystemDiscounts",
                column: "IdSoftwareSystem",
                principalTable: "SoftwareSystems",
                principalColumn: "SoftwareSystemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
