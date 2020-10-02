using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDMS.Model
{
    class User
    {
        public static int? userId;
        public int id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string department { get; set; }
        public string role { get; set; }
        public string profile { get; set; }
        public string email { get; set; }
        public User(String name, String password, String department, String role, String profile, String email)
        {
            this.name = name;
            this.password = password;
            this.department = department;
            this.role = role;
            this.profile = profile;
            this.email = email;
        }

    } 
}
