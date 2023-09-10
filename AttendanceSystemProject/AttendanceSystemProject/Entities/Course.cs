using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystemProject.Entities
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Fees { get; set; }

        public Teacher Teacher { get; set; }

        

        public List<StudentAndCourseRelation> StudentsOfCourse { get; set; }

        public List<ClassSchedule> ClassSchedulesofaCourse { get; set; }

    }
}