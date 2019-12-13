﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using exercise_dotnet_core_api_with_ef.Models;

namespace exercise_dotnet_core_api_with_ef.Migrations
{
    [DbContext(typeof(ContosoUniversityContext))]
    partial class ContosoUniversityContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0");

            modelBuilder.Entity("exercise_dotnet_core_api_with_ef.Models.Course", b =>
                {
                    b.Property<long>("CourseId")
                        .HasColumnName("CourseID")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Credits")
                        .HasColumnType("int");

                    b.Property<long>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DepartmentID")
                        .HasColumnType("int")
                        .HasDefaultValueSql("1");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CourseId");

                    b.HasIndex("DepartmentId")
                        .HasName("Course_IX_DepartmentID");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("exercise_dotnet_core_api_with_ef.Models.CourseInstructor", b =>
                {
                    b.Property<long>("CourseId")
                        .HasColumnName("CourseID")
                        .HasColumnType("int");

                    b.Property<long>("InstructorId")
                        .HasColumnName("InstructorID")
                        .HasColumnType("int");

                    b.HasKey("CourseId", "InstructorId");

                    b.HasIndex("CourseId")
                        .HasName("CourseInstructor_IX_CourseID");

                    b.HasIndex("InstructorId")
                        .HasName("CourseInstructor_IX_InstructorID");

                    b.ToTable("CourseInstructor");
                });

            modelBuilder.Entity("exercise_dotnet_core_api_with_ef.Models.Department", b =>
                {
                    b.Property<long>("DepartmentId")
                        .HasColumnName("DepartmentID")
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Budget")
                        .IsRequired()
                        .HasColumnType("money");

                    b.Property<long?>("InstructorId")
                        .HasColumnName("InstructorID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)");

                    b.Property<byte[]>("RowVersion")
                        .IsRequired()
                        .HasColumnType("rowversion");

                    b.Property<byte[]>("StartDate")
                        .IsRequired()
                        .HasColumnType("datetime");

                    b.HasKey("DepartmentId");

                    b.HasIndex("InstructorId")
                        .HasName("Department_IX_InstructorID");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("exercise_dotnet_core_api_with_ef.Models.Enrollment", b =>
                {
                    b.Property<long>("EnrollmentId")
                        .HasColumnName("EnrollmentID")
                        .HasColumnType("INTEGER");

                    b.Property<long>("CourseId")
                        .HasColumnName("CourseID")
                        .HasColumnType("int");

                    b.Property<long?>("Grade")
                        .HasColumnType("int");

                    b.Property<long>("StudentId")
                        .HasColumnName("StudentID")
                        .HasColumnType("int");

                    b.HasKey("EnrollmentId");

                    b.HasIndex("CourseId")
                        .HasName("Enrollment_IX_CourseID");

                    b.HasIndex("StudentId")
                        .HasName("Enrollment_IX_StudentID");

                    b.ToTable("Enrollment");
                });

            modelBuilder.Entity("exercise_dotnet_core_api_with_ef.Models.OfficeAssignment", b =>
                {
                    b.Property<long>("InstructorId")
                        .HasColumnName("InstructorID")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("InstructorId");

                    b.HasIndex("InstructorId")
                        .HasName("OfficeAssignment_IX_InstructorID");

                    b.ToTable("OfficeAssignment");
                });

            modelBuilder.Entity("exercise_dotnet_core_api_with_ef.Models.Person", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnName("ID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(128)")
                        .HasDefaultValueSql("'Instructor'");

                    b.Property<byte[]>("EnrollmentDate")
                        .HasColumnType("datetime");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<byte[]>("HireDate")
                        .HasColumnType("datetime");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("exercise_dotnet_core_api_with_ef.Models.Course", b =>
                {
                    b.HasOne("exercise_dotnet_core_api_with_ef.Models.Department", "Department")
                        .WithMany("Course")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("exercise_dotnet_core_api_with_ef.Models.CourseInstructor", b =>
                {
                    b.HasOne("exercise_dotnet_core_api_with_ef.Models.Course", "Course")
                        .WithMany("CourseInstructor")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("exercise_dotnet_core_api_with_ef.Models.Person", "Instructor")
                        .WithMany("CourseInstructor")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("exercise_dotnet_core_api_with_ef.Models.Department", b =>
                {
                    b.HasOne("exercise_dotnet_core_api_with_ef.Models.Person", "Instructor")
                        .WithMany("Department")
                        .HasForeignKey("InstructorId");
                });

            modelBuilder.Entity("exercise_dotnet_core_api_with_ef.Models.Enrollment", b =>
                {
                    b.HasOne("exercise_dotnet_core_api_with_ef.Models.Course", "Course")
                        .WithMany("Enrollment")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("exercise_dotnet_core_api_with_ef.Models.Person", "Student")
                        .WithMany("Enrollment")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("exercise_dotnet_core_api_with_ef.Models.OfficeAssignment", b =>
                {
                    b.HasOne("exercise_dotnet_core_api_with_ef.Models.Person", "Instructor")
                        .WithOne("OfficeAssignment")
                        .HasForeignKey("exercise_dotnet_core_api_with_ef.Models.OfficeAssignment", "InstructorId")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
