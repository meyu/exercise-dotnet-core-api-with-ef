using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace exercise_dotnet_core_api_with_ef.Models
{
    public partial class ContosoUniversityContext : DbContext
    {
        public ContosoUniversityContext()
        {
        }

        public ContosoUniversityContext(DbContextOptions<ContosoUniversityContext> options)
            : base(options)
        {
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = this.ChangeTracker.Entries();
            foreach (var entry in entities)
            {
                // TODO: 要如何讓它顯示出來？
                Console.Write("Entity Name: {0}", entry.Entity.GetType().FullName);
                Console.Write("Entity Stattus: {0}", entry.State);
                if (entry.State == EntityState.Modified)
                {
                    // TODO: 時間紀錄怪怪的
                    entry.CurrentValues.SetValues(new { DateModified = DateTime.Now });
                }
            }
            return await base.SaveChangesAsync();
        }

        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<CourseInstructor> CourseInstructor { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Enrollment> Enrollment { get; set; }
        public virtual DbSet<OfficeAssignment> OfficeAssignment { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<VwCourseStudentCount> VwCourseStudentCount { get; set; }
        public virtual DbSet<VwCourseStudents> VwCourseStudents { get; set; }
        public virtual DbSet<VwDepartmentCourseCount> VwDepartmentCourseCount { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Name=DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasIndex(e => e.DepartmentId)
                    .HasName("Course_IX_DepartmentID");

                entity.Property(e => e.CourseId)
                    .HasColumnName("CourseID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Credits).HasColumnType("int");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("DepartmentID")
                    .HasColumnType("int")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Title).HasColumnType("nvarchar(50)");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Course)
                    .HasForeignKey(d => d.DepartmentId);
            });

            modelBuilder.Entity<CourseInstructor>(entity =>
            {
                entity.HasKey(e => new { e.CourseId, e.InstructorId });

                entity.HasIndex(e => e.CourseId)
                    .HasName("CourseInstructor_IX_CourseID");

                entity.HasIndex(e => e.InstructorId)
                    .HasName("CourseInstructor_IX_InstructorID");

                entity.Property(e => e.CourseId)
                    .HasColumnName("CourseID")
                    .HasColumnType("int");

                entity.Property(e => e.InstructorId)
                    .HasColumnName("InstructorID")
                    .HasColumnType("int");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseInstructor)
                    .HasForeignKey(d => d.CourseId);

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.CourseInstructor)
                    .HasForeignKey(d => d.InstructorId);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasIndex(e => e.InstructorId)
                    .HasName("Department_IX_InstructorID");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("DepartmentID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Budget)
                    .IsRequired()
                    .HasColumnType("money");

                entity.Property(e => e.InstructorId)
                    .HasColumnName("InstructorID")
                    .HasColumnType("int");

                entity.Property(e => e.Name).HasColumnType("nvarchar(50)");

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .HasColumnType("rowversion");

                entity.Property(e => e.StartDate)
                    .IsRequired()
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.Department)
                    .HasForeignKey(d => d.InstructorId);
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasIndex(e => e.CourseId)
                    .HasName("Enrollment_IX_CourseID");

                entity.HasIndex(e => e.StudentId)
                    .HasName("Enrollment_IX_StudentID");

                entity.Property(e => e.EnrollmentId)
                    .HasColumnName("EnrollmentID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CourseId)
                    .HasColumnName("CourseID")
                    .HasColumnType("int");

                entity.Property(e => e.Grade).HasColumnType("int");

                entity.Property(e => e.StudentId)
                    .HasColumnName("StudentID")
                    .HasColumnType("int");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Enrollment)
                    .HasForeignKey(d => d.CourseId);

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Enrollment)
                    .HasForeignKey(d => d.StudentId);
            });

            modelBuilder.Entity<OfficeAssignment>(entity =>
            {
                entity.HasKey(e => e.InstructorId);

                entity.HasIndex(e => e.InstructorId)
                    .HasName("OfficeAssignment_IX_InstructorID");

                entity.Property(e => e.InstructorId)
                    .HasColumnName("InstructorID")
                    .HasColumnType("int")
                    .ValueGeneratedNever();

                entity.Property(e => e.Location).HasColumnType("nvarchar(50)");

                entity.HasOne(d => d.Instructor)
                    .WithOne(p => p.OfficeAssignment)
                    .HasForeignKey<OfficeAssignment>(d => d.InstructorId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Discriminator)
                    .IsRequired()
                    .HasColumnType("nvarchar(128)")
                    .HasDefaultValueSql("'Instructor'");

                entity.Property(e => e.EnrollmentDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.HireDate).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnType("nvarchar(50)");
            });

            modelBuilder.Entity<VwCourseStudentCount>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwCourseStudentCount");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.Name).HasColumnType("nvarchar(50)");

                entity.Property(e => e.Title).HasColumnType("nvarchar(50)");
            });

            modelBuilder.Entity<VwCourseStudents>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwCourseStudents");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.CourseTitle).HasColumnType("nvarchar(50)");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.DepartmentName).HasColumnType("nvarchar(50)");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");
            });

            modelBuilder.Entity<VwDepartmentCourseCount>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwDepartmentCourseCount");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.Name).HasColumnType("nvarchar(50)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
