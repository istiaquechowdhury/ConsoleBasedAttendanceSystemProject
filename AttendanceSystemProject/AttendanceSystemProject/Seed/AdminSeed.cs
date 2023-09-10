using AttendanceSystemProject.Entities;
using AttendanceSystemProject.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystemProject.Seed
{
    public static class AdminSeed
    {

        public static List<User> Users
        {
            get
            {
                return new List<User>()
                {
                    new User{ Id = 1,Name = "Admin", Password ="123456",UserType = UserType.Admin},

                };
            }
        }
    }
}
