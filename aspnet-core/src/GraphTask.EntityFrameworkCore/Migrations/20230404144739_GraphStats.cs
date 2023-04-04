using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraphTask.Migrations
{
    /// <inheritdoc />
    public partial class GraphStats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Edge_Graph_GraphId",
                table: "Edge");

            migrationBuilder.AddColumn<double>(
                name: "AverageNumberOfAdjacentNodes",
                table: "Graph",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfNodes",
                table: "Graph",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "GraphId",
                table: "Edge",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Edge_Graph_GraphId",
                table: "Edge",
                column: "GraphId",
                principalTable: "Graph",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Edge_Graph_GraphId",
                table: "Edge");

            migrationBuilder.DropColumn(
                name: "AverageNumberOfAdjacentNodes",
                table: "Graph");

            migrationBuilder.DropColumn(
                name: "NumberOfNodes",
                table: "Graph");

            migrationBuilder.AlterColumn<int>(
                name: "GraphId",
                table: "Edge",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Edge_Graph_GraphId",
                table: "Edge",
                column: "GraphId",
                principalTable: "Graph",
                principalColumn: "Id");
        }
    }
}
