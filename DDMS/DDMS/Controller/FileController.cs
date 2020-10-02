using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDMS.Model;

namespace DDMS.Controller
{
    class FileController
    {
        ConnectionToDB connection = new ConnectionToDB();
        public void upload(Files file)
        {
            string sql = "insert into files (name, userId) values('" + file.name + "', '" + file.userId + "')";
            connection.Manipulate(sql);
        }

        public bool checkFileName(string name)
        {
            string sql = "select * from files where name = '" + name + "'";
            DataTable dt = connection.Retrieve(sql);
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public DataTable Myfile(int? userId)
        {
            String sql = "select id, name from files where userId=" + userId + "";
           
            return connection.Retrieve(sql);
        }

        public DataTable Othersfile(int? userId)
        {
            String sql = "select f.id, f.name, u.name as uploader from files f join users u on f.userId = u.id where not f.userId=" + userId + "";
            return connection.Retrieve(sql);
        }

        public void delete(int id)
        {
            String sql = "delete from files where id='" + id + "'";
            connection.Manipulate(sql);
        }
        public DataTable searchFile(String name, int? userId)
        {
            String sql = "select * from files where name LIKE '%" + name + "%' and userId=" + userId + "";
            return connection.Retrieve(sql);
        }
    }
}
