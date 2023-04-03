using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraphTask.Migrations
{
    /// <inheritdoc />
    public partial class GraphTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Graph",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Graph", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Edge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EndNode = table.Column<int>(type: "int", nullable: false),
                    StartNode = table.Column<int>(type: "int", nullable: false),
                    GraphId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Edge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Edge_Graph_GraphId",
                        column: x => x.GraphId,
                        principalTable: "Graph",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Edge_GraphId",
                table: "Edge",
                column: "GraphId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Edge");

            migrationBuilder.DropTable(
                name: "Graph");
        }
    }
}
