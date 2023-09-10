using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystemProject.Entities
{
    public class Student
    {
        public int Id { get; set; } 

        public string Name { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }

        public List<StudentAndCourseRelation> CoursesOfStudent { get; set; } 

        public List<Attendance> AttendanceRecordsOfStudent { get; set; } 
    }
}
