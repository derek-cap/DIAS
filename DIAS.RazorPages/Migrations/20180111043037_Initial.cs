using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DIAS.RazorPages.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Studies",
                columns: table => new
                {
                    InstanceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateTime = table.Column<DateTime>(nullable: true),
                    Modality = table.Column<string>(maxLength: 10, nullable: true),
                    PatientAge = table.Column<string>(maxLength: 5, nullable: true),
                    PatientID = table.Column<string>(maxLength: 20, nullable: true),
                    PatientName = table.Column<string>(maxLength: 20, nullable: true),
                    PatientSex = table.Column<int>(nullable: false),
                    RecordState = table.Column<int>(nullable: false),
                    StudyDescription = table.Column<string>(maxLength: 200, nullable: true),
                    StudyID = table.Column<string>(maxLength: 30, nullable: true),
                    StudyUID = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studies", x => x.InstanceId);
                });

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    InstanceId = table.Column<int>(nullable: false),
                    BodyPartExamined = table.Column<string>(maxLength: 50, nullable: true),
                    ImageCount = table.Column<int>(nullable: false),
                    IsContrast = table.Column<bool>(nullable: false),
                    Kernel = table.Column<string>(maxLength: 20, nullable: true),
                    ProtocolName = table.Column<string>(maxLength: 100, nullable: true),
                    RecordState = table.Column<int>(nullable: false),
                    SeriesDescription = table.Column<string>(maxLength: 200, nullable: true),
                    SeriesNumber = table.Column<string>(maxLength: 20, nullable: true),
                    SeriesType = table.Column<string>(maxLength: 20, nullable: true),
                    SeriesUID = table.Column<string>(maxLength: 100, nullable: false),
                    StudyUID = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.InstanceId);
                    table.ForeignKey(
                        name: "FK_Series_Studies_InstanceId",
                        column: x => x.InstanceId,
                        principalTable: "Studies",
                        principalColumn: "InstanceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    InstanceId = table.Column<int>(nullable: false),
                    DcmFileName = table.Column<string>(maxLength: 200, nullable: true),
                    ImageNumber = table.Column<int>(nullable: false),
                    ImageUID = table.Column<string>(maxLength: 100, nullable: false),
                    Matrix = table.Column<string>(maxLength: 20, nullable: true),
                    SeriesUID = table.Column<string>(maxLength: 100, nullable: false),
                    SliceThickness = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.InstanceId);
                    table.ForeignKey(
                        name: "FK_Images_Series_InstanceId",
                        column: x => x.InstanceId,
                        principalTable: "Series",
                        principalColumn: "InstanceId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Series");

            migrationBuilder.DropTable(
                name: "Studies");
        }
    }
}
