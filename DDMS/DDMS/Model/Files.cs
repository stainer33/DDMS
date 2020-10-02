using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DDMS.Model
{
    class Files
    {
        public int id
        { get; set; }
        public string name 
        { get; set; }
        public int userId
        { get; set; }

      public Files(String name,int userId)
        {
            this.name = name;
            this.userId = userId;
        }
    }
}
