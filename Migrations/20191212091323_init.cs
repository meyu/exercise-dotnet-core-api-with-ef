using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace exercise_dotnet_core_api_with_ef.Migrations {
    public partial class init : Migration {
        protected override void Up (MigrationBuilder migrationBuilder) {
            // migrationBuilder.CreateTable(
            //     name: "Person",
            //     columns: table => new
            //     {
            //         ID = table.Column<long>(nullable: false),
            //         LastName = table.Column<string>(type: "nvarchar(50)", nullable: false),
            //         FirstName = table.Column<string>(type: "nvarchar(50)", nullable: false),
            //         HireDate = table.Column<byte[]>(type: "datetime", nullable: true),
            //         EnrollmentDate = table.Column<byte[]>(type: "datetime", nullable: true),
            //         Discriminator = table.Column<string>(type: "nvarchar(128)", nullable: false, defaultValueSql: "'Instructor'")
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Person", x => x.ID);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "Department",
            //     columns: table => new
            //     {
            //         DepartmentID = table.Column<long>(nullable: false),
            //         Name = table.Column<string>(type: "nvarchar(50)", nullable: true),
            //         Budget = table.Column<byte[]>(type: "money", nullable: false),
            //         StartDate = table.Column<byte[]>(type: "datetime", nullable: false),
            //         InstructorID = table.Column<long>(type: "int", nullable: true),
            //         RowVersion = table.Column<byte[]>(type: "rowversion", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Department", x => x.DepartmentID);
            //         table.ForeignKey(
            //             name: "FK_Department_Person_InstructorID",
            //             column: x => x.InstructorID,
            //             principalTable: "Person",
            //             principalColumn: "ID",
            //             onDelete: ReferentialAction.Restrict);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "OfficeAssignment",
            //     columns: table => new
            //     {
            //         InstructorID = table.Column<long>(type: "int", nullable: false),
            //         Location = table.Column<string>(type: "nvarchar(50)", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_OfficeAssignment", x => x.InstructorID);
            //         table.ForeignKey(
            //             name: "FK_OfficeAssignment_Person_InstructorID",
            //             column: x => x.InstructorID,
            //             principalTable: "Person",
            //             principalColumn: "ID",
            //             onDelete: ReferentialAction.Restrict);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "Course",
            //     columns: table => new
            //     {
            //         CourseID = table.Column<long>(nullable: false),
            //         Title = table.Column<string>(type: "nvarchar(50)", nullable: true),
            //         Credits = table.Column<long>(type: "int", nullable: false),
            //         DepartmentID = table.Column<long>(type: "int", nullable: false, defaultValueSql: "1")
            //             .Annotation("Sqlite:Autoincrement", true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Course", x => x.CourseID);
            //         table.ForeignKey(
            //             name: "FK_Course_Department_DepartmentID",
            //             column: x => x.DepartmentID,
            //             principalTable: "Department",
            //             principalColumn: "DepartmentID",
            //             onDelete: ReferentialAction.Cascade);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "CourseInstructor",
            //     columns: table => new
            //     {
            //         CourseID = table.Column<long>(type: "int", nullable: false),
            //         InstructorID = table.Column<long>(type: "int", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_CourseInstructor", x => new { x.CourseID, x.InstructorID });
            //         table.ForeignKey(
            //             name: "FK_CourseInstructor_Course_CourseID",
            //             column: x => x.CourseID,
            //             principalTable: "Course",
            //             principalColumn: "CourseID",
            //             onDelete: ReferentialAction.Cascade);
            //         table.ForeignKey(
            //             name: "FK_CourseInstructor_Person_InstructorID",
            //             column: x => x.InstructorID,
            //             principalTable: "Person",
            //             principalColumn: "ID",
            //             onDelete: ReferentialAction.Cascade);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "Enrollment",
            //     columns: table => new
            //     {
            //         EnrollmentID = table.Column<long>(nullable: false),
            //         CourseID = table.Column<long>(type: "int", nullable: false),
            //         StudentID = table.Column<long>(type: "int", nullable: false),
            //         Grade = table.Column<long>(type: "int", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Enrollment", x => x.EnrollmentID);
            //         table.ForeignKey(
            //             name: "FK_Enrollment_Course_CourseID",
            //             column: x => x.CourseID,
            //             principalTable: "Course",
            //             principalColumn: "CourseID",
            //             onDelete: ReferentialAction.Cascade);
            //         table.ForeignKey(
            //             name: "FK_Enrollment_Person_StudentID",
            //             column: x => x.StudentID,
            //             principalTable: "Person",
            //             principalColumn: "ID",
            //             onDelete: ReferentialAction.Cascade);
            //     });

            // migrationBuilder.CreateIndex(
            //     name: "Course_IX_DepartmentID",
            //     table: "Course",
            //     column: "DepartmentID");

            // migrationBuilder.CreateIndex(
            //     name: "CourseInstructor_IX_CourseID",
            //     table: "CourseInstructor",
            //     column: "CourseID");

            // migrationBuilder.CreateIndex(
            //     name: "CourseInstructor_IX_InstructorID",
            //     table: "CourseInstructor",
            //     column: "InstructorID");

            // migrationBuilder.CreateIndex(
            //     name: "Department_IX_InstructorID",
            //     table: "Department",
            //     column: "InstructorID");

            // migrationBuilder.CreateIndex(
            //     name: "Enrollment_IX_CourseID",
            //     table: "Enrollment",
            //     column: "CourseID");

            // migrationBuilder.CreateIndex(
            //     name: "Enrollment_IX_StudentID",
            //     table: "Enrollment",
            //     column: "StudentID");

            // migrationBuilder.CreateIndex(
            //     name: "OfficeAssignment_IX_InstructorID",
            //     table: "OfficeAssignment",
            //     column: "InstructorID");
        }

        protected override void Down (MigrationBuilder migrationBuilder) {
            // migrationBuilder.DropTable(
            //     name: "CourseInstructor");

            // migrationBuilder.DropTable(
            //     name: "Enrollment");

            // migrationBuilder.DropTable(
            //     name: "OfficeAssignment");

            // migrationBuilder.DropTable(
            //     name: "Course");

            // migrationBuilder.DropTable(
            //     name: "Department");

            // migrationBuilder.DropTable(
            //     name: "Person");
        }
    }
}