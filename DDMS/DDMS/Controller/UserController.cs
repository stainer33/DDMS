using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDMS.Model;

namespace DDMS.Controller
{
    class UserController
    {
        ConnectionToDB connection = new ConnectionToDB();
     //   User user = new User();
        public bool Login(String email, String password)
        {
            String sql = "select * from users where email='" + email + "' and password='"+password+"'";
            // String sql = "select * from users where email='test'";
            DataTable dt = connection.Retrieve(sql);

            if (dt.Rows.Count > 0)
            {
                User.userId = Convert.ToInt32(dt.Rows[0][0]);
                return true;
            }
            else
            {
                return false;
            }
            //  return dt.Rows.Count;

        }
        public static bool checkEmail(string email)
        {
            ConnectionToDB connection = new ConnectionToDB();
            String sql = "select * from users where email='" + email + "'";
            DataTable dt = connection.Retrieve(sql);

            if (dt.Rows.Count > 0)
            {
                User.userId = Convert.ToInt32(dt.Rows[0][0]);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Register(User user)
        {
            String sql = "insert into users (name,email,password,department,role,profile) values('" + user.name + "','" + user.email + "','" + user.password + "','" + user.department + "','" + user.role + "','" + user.profile + "')";
            connection.Manipulate(sql);
        }

        public DataTable Profile(int? id)
        {
            String sql = "select * from users where id='" + id + "'";
            DataTable dt = connection.Retrieve(sql);
            return dt;
        }

    }
}
