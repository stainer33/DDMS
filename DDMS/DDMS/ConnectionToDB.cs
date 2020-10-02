using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace DDMS
{
    class ConnectionToDB
    {
      public  MySqlConnection con = new MySqlConnection("Server=localhost;Database=ddms;Uid=root;Pwd=stainer33;");
       // SqlConnection con = new SqlConnection(@"Data Source= localhost;Initial Catalog= ddms; User Id= root; Password= stainer33");
       
      public  MySqlCommand cmd;
      public MySqlDataReader dataReader;
      public DataTable dataTable = new DataTable();
      public MySqlDataAdapter adapter;

        public void Manipulate(string query)
        {
            con.Open();
            cmd = new MySqlCommand(query, con);
            dataReader=cmd.ExecuteReader();
            con.Close();
        }

    
        public DataTable Retrieve (string query)
        {
            adapter = new MySqlDataAdapter();
            cmd = new MySqlCommand(query, con);
            adapter.SelectCommand = cmd;
            adapter.Fill(dataTable);
            return dataTable;
        }
    }
}
