using AttendanceSystemProject.Entities;
using AttendanceSystemProject.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystemProject.Authentication
{
    public class Admin
    {
        public static bool AuthenticateUser(string username, string password)
        {
            // Find the user from the seed data
            User user = AdminSeed.Users.FirstOrDefault(u => u.Name == username && u.Password == password);

            // Return true if user is found, otherwise false
            return user != null;
        }

    }
}
