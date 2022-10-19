using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TugasBootcampNET.Migrations
{
    public partial class Query : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE dbo.AllStudentWithChosenCourse
               @CourseID int
                AS
               SELECT      Students.ID, Students.LastName, Students.FirstMidName, Students.EnrollmentDate
               FROM        Students 
               INNER JOIN  Enrollments 
                ON Students.ID = Enrollments.StudentID
               INNER JOIN  Courses 
                ON Enrollments.CourseID = Courses.CourseID
               WHERE       Courses.CourseID=@CourseID");

            migrationBuilder.Sql(@"CREATE PROCEDURE dbo.AllCourseWithChosenStudent
               @StudentID int
                AS
               SELECT      Courses.CourseID, Courses.Title, Courses.Credits
               FROM        Courses 
               INNER JOIN  Enrollments 
                ON Courses.CourseID = Enrollments.CourseID
               INNER JOIN  Students 
                ON Enrollments.StudentID = Students.ID
               WHERE       Students.ID=@StudentID");

            migrationBuilder.Sql(@"CREATE PROCEDURE dbo.AllStudentWithCourse
                AS
               SELECT Students.ID, Students.LastName, Students.FirstMidName, Students.EnrollmentDate, Courses.CourseID, Courses.Title, Courses.Credits
               FROM        Students 
               LEFT JOIN  Enrollments 
                ON Students.ID = Enrollments.StudentID
               LEFT JOIN  Courses 
                ON Enrollments.CourseID = Courses.CourseID
               ORDER BY Students.ID");   
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"drop procedure AllStudentWithChosenCourse");
            migrationBuilder.Sql(@"drop procedure AllCourseWithChosenStudent");
            migrationBuilder.Sql(@"drop procedure AllStudentWithCourse");
        }
    }   
}
