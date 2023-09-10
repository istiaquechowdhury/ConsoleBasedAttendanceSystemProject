using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceSystemProject.Authentication;
using AttendanceSystemProject.Entities;
using AttendanceSystemProject.Entities.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AttendanceSystemProject
{
    public class Program
    {

        public static void Main()
        {

           
            Console.WriteLine("Welcome to the Attendance System!");


            while (true)
            {
                Console.Write("Username: ");
                string username = Console.ReadLine();

                Console.Write("Password: ");
                string password = Console.ReadLine();

                if (Admin.AuthenticateUser(username, password))
                {
                    Console.WriteLine("Press 1 for *Create Teacher");
                    Console.WriteLine("Press 2 for *Create Student");
                    Console.WriteLine("Press 3 for *Create Course and Assigining A Teacher on that Course");
                    Console.WriteLine("Press 4 for *Assign Students to the Course(StudentAndCourseRelationTable)");
                    Console.WriteLine("Press 5 for *Set ClassSchedule for Course");
                    Console.WriteLine("Press 6 for *Edit Teacher");
                    Console.WriteLine("Press 7 for *Delete Teacher");
                    Console.WriteLine("Press 8 for *Edit Course");
                    Console.WriteLine("Press 9 for *Delete Course");
                    Console.WriteLine("Press 10 for *Edit Student");
                    Console.WriteLine("Press 11 for *Delete Student");
                    





                    int UserChoice = int.Parse(Console.ReadLine());

                    if (UserChoice == 1)
                    {

                        CreateTeacher();

                    }
                    else if (UserChoice == 2)
                    {
                        CreateStudent();

                    }
                    else if (UserChoice == 3)
                    {
                        CreateCourse();

                    }
                    else if (UserChoice == 4)
                    {
                        AssignStudentsToCourse();

                    }
                    else if (UserChoice == 5)
                    {
                        SetClassSchedule();

                    }
                    else if (UserChoice == 6)
                    {
                        EditTeacher();
                    }
                    else if (UserChoice == 7)
                    {
                        DeleteTeacher();

                    }
                    else if (UserChoice == 8)
                    {
                        EditCourse();

                    }
                    else if (UserChoice == 9)
                    {
                        DeleteCourse();

                    }
                    else if (UserChoice == 10)
                    {
                        EditStudent();
                    }
                    else if (UserChoice == 11)
                    {
                        DeleteStudent();
                    }




                }
                else if (StudentLogin(username, password))
                {

                }

                else if(TeacherLogin(username, password))
                {
                    using var dbContext = new AttendanceDbContext();
                    var teacher = dbContext.Teachers
                        .Include(t => t.TeachersCourses)
                        .FirstOrDefault(t => t.UserName == username && t.Password == password);

                    if (teacher != null)
                    {
                        DisplayAttendanceReportsForTeacher(teacher);
                    }
                    else
                    {
                        Console.WriteLine("Invalid login credentials for teacher.");
                    }
                }

                   
                    



                

                
               
                
                










            }

        }
        public static void CreateTeacher()
        {
            AttendanceDbContext Context = new AttendanceDbContext();
            Console.WriteLine("Creating a new teacher:");

            Console.Write("Teacher Name: ");
            string name = Console.ReadLine();

            Console.Write("Teacher Username: ");
            string username = Console.ReadLine();

            Console.Write("Teacher Password: ");
            string password = Console.ReadLine();

          
            Teacher newTeacher = new Teacher
            {
                Name = name,
                UserName = username,
                Password = password
            };

            
            Context.Teachers.Add(newTeacher);

          
            Context.SaveChanges();

            Console.WriteLine("Teacher created and saved to the database successfully!");
        }


        public static void CreateCourse()
        {
            Console.Write("Enter Course Name: ");
            string courseName = Console.ReadLine();

            Console.Write("Enter Course Fees: ");
            int courseFees = int.Parse(Console.ReadLine());

            Console.Write("Enter Teacher ID: ");
            int teacherId = int.Parse(Console.ReadLine()); 

            using var Context = new AttendanceDbContext();


            var teacher = Context.Teachers.Find(teacherId);

            if (teacher == null)
            {
                Console.WriteLine("Teacher not found.");
                return;
            }

            var newCourse = new Course
            {
                Name = courseName,
                Fees = courseFees,
                Teacher = teacher, 
            };

            Context.Courses.Add(newCourse);
            Context.SaveChanges();

            Console.WriteLine("Course saved to the database.");
        }


        public static void CreateStudent()
        {
            Console.Write("Enter Student Name: ");
            string studentName = Console.ReadLine();

            Console.Write("Enter Student Username: ");
            string studentUsername = Console.ReadLine();

            Console.Write("Enter Student Password: ");
            string studentPassword = Console.ReadLine();

            using var dbContext = new AttendanceDbContext();
            var newStudent = new Student
            {
                Name = studentName,
                UserName = studentUsername,
                Password = studentPassword,
            };

            dbContext.Students.Add(newStudent);
            dbContext.SaveChanges();

            Console.WriteLine("Student created successfully.");
        }
        public static void AssignStudentsToCourse()
        {
            Console.Write("Enter Course ID: ");
            int courseId = int.Parse(Console.ReadLine());

            Console.Write("Enter Student IDs (comma-separated): ");
            string studentIdsInput = Console.ReadLine();

            using var dbContext = new AttendanceDbContext();
            var course = dbContext.Courses.Include(c => c.StudentsOfCourse).FirstOrDefault(c => c.Id == courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            var studentIds = studentIdsInput.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var studentId in studentIds)
            {
                if (int.TryParse(studentId, out int id))
                {
                    var student = dbContext.Students.Find(id);
                    if (student != null)
                    {
                        var studentCourseRelation = new StudentAndCourseRelation
                        {
                            StudentId = student.Id,
                            CourseId = course.Id
                        };

                        dbContext.StudentAndCourseRelations.Add(studentCourseRelation);
                    }
                    else
                    {
                        Console.WriteLine($"Student with ID {id} not found.");
                    }
                }
                else
                {
                    Console.WriteLine($"Invalid student ID: {studentId}");
                }
            }

            dbContext.SaveChanges();

            Console.WriteLine("Students assigned to the course.");


        }

       public static void SetClassSchedule()
       {
            Console.Write("Enter Course ID: ");
            int courseId = int.Parse(Console.ReadLine());

            using var dbContext = new AttendanceDbContext();
            var course = dbContext.Courses.Include(c => c.ClassSchedulesofaCourse).FirstOrDefault(c => c.Id == courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            Console.Write("Enter Day: ");
            string dayInput = Console.ReadLine();
            if (!Enum.TryParse(dayInput, out Day day))
            {
                Console.WriteLine("Invalid day.");
                return;
            }

            Console.Write("Enter Start Time (hh:mm tt): ");
            if (!DateTime.TryParseExact(Console.ReadLine(), "hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startTime))
            {
                Console.WriteLine("Invalid start time format.");
                return;
            }

            Console.Write("Enter End Time (hh:mm tt): ");
            if (!DateTime.TryParseExact(Console.ReadLine(), "hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endTime))
            {
                Console.WriteLine("Invalid end time format.");
                return;
            }

            Console.Write("Enter Total Number of Classes: ");
            int totalClasses = int.Parse(Console.ReadLine());

            var classSchedule = new ClassSchedule
            {
                Course = course,
                Day = day,
                StartTime = startTime,
                EndTime = endTime,
                TotalNumberOfClasses = totalClasses
            };

            course.ClassSchedulesofaCourse.Add(classSchedule);
            dbContext.SaveChanges();

            Console.WriteLine("Class schedule set for the course.");
       }

        static void EditTeacher()
        {
            Console.Write("Enter Teacher ID to edit: ");
            int teacherId = int.Parse(Console.ReadLine());

            using var dbContext = new AttendanceDbContext();
            var teacher = dbContext.Teachers.Find(teacherId);

            if (teacher == null)
            {
                Console.WriteLine("Teacher not found.");
                return;
            }

            Console.Write("Enter new Name: ");
            teacher.Name = Console.ReadLine();

            Console.Write("Enter new Username: ");
            teacher.UserName = Console.ReadLine();

            Console.Write("Enter new Password: ");
            teacher.Password = Console.ReadLine();

            dbContext.SaveChanges();

            Console.WriteLine("Teacher information updated.");
        }


        static void DeleteTeacher()
        {
            Console.Write("Enter Teacher ID to delete: ");
            int teacherId = int.Parse(Console.ReadLine());

            using var dbContext = new AttendanceDbContext();
            var teacher = dbContext.Teachers.Find(teacherId);

            if (teacher == null)
            {
                Console.WriteLine("Teacher not found.");
                return;
            }

            dbContext.Teachers.Remove(teacher);
            dbContext.SaveChanges();

            Console.WriteLine("Teacher deleted.");
        }

        static void EditCourse()
        {
            Console.Write("Enter Course ID to edit: ");
            int courseId = int.Parse(Console.ReadLine());

            using var dbContext = new AttendanceDbContext();
            var course = dbContext.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            Console.Write("Enter new Name: ");
            course.Name = Console.ReadLine();

            Console.Write("Enter new Fees: ");
            course.Fees = int.Parse(Console.ReadLine());

            dbContext.SaveChanges();

            Console.WriteLine("Course information updated.");
        }

        static void DeleteCourse()
        {
            Console.Write("Enter Course ID to delete: ");
            int courseId = int.Parse(Console.ReadLine());

            using var dbContext = new AttendanceDbContext();
            var course = dbContext.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            dbContext.Courses.Remove(course);
            dbContext.SaveChanges();

            Console.WriteLine("Course deleted.");
        }

        static void EditStudent()
        {
            Console.Write("Enter Student ID to edit: ");
            int studentId = int.Parse(Console.ReadLine());

            using var dbContext = new AttendanceDbContext();
            var student = dbContext.Students.Find(studentId);

            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            Console.Write("Enter new Name: ");
            student.Name = Console.ReadLine();

            Console.Write("Enter new Username: ");
            student.UserName = Console.ReadLine();

            Console.Write("Enter new Password: ");
            student.Password = Console.ReadLine();

            dbContext.SaveChanges();

            Console.WriteLine("Student information updated.");
        }
        static void DeleteStudent()
        {
            Console.Write("Enter Student ID to delete: ");
            int studentId = int.Parse(Console.ReadLine());

            using var dbContext = new AttendanceDbContext();
            var student = dbContext.Students.Find(studentId);

            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            dbContext.Students.Remove(student);
            dbContext.SaveChanges();

            Console.WriteLine("Student deleted.");
        }

        
        static bool StudentLogin(string username, string password)
        {
            using var dbContext = new AttendanceDbContext();
            var student = dbContext.Students
                .Include(s => s.CoursesOfStudent)
                .FirstOrDefault(s => s.UserName.ToLower() == username.ToLower() && s.Password == password);

            if (student != null)
            {
                Console.WriteLine("Login successful. Welcome, " + student.Name);
                //ShowStudentMenu(student);
                return true;
            }
            else
            {
                return false;
            }
        }
        static bool TeacherLogin(string username, string password)
        {
            using var dbContext = new AttendanceDbContext();
            var teacher = dbContext.Teachers.FirstOrDefault(t => t.UserName == username && t.Password == password);

            if (teacher != null)
            {
                Console.WriteLine("Login successful. Welcome, " + teacher.Name);
                // Implement teacher menu and functionalities
                return true;
            }
            else
            {

                return false;
            }
        }

        public static void DisplayAttendanceReportsForTeacher(Teacher teacher)
        {
            using var dbContext = new AttendanceDbContext();

            Console.WriteLine($"Attendance Reports for Teacher: {teacher.Name}\n");

            foreach (var course in teacher.TeachersCourses)
            {
                Console.WriteLine($"Course: {course.Name}\n");

                var attendanceRecords = dbContext.Attendances
                    .Include(a => a.Student)
                    .Where(a => a.Course.Id == course.Id)
                    .OrderBy(a => a.AttandanceDate)
                    .ToList();

                foreach (var record in attendanceRecords)
                {
                    string status = record.IsPresent ? "✔ Present" : "❌ Absent";
                    Console.WriteLine($"Date: {record.AttandanceDate:yyyy-MM-dd HH:mm:ss}, Student: {record.Student.Name}, Status: {status}");
                }

                Console.WriteLine();
            }
        }










    }
}


