using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBWorker
{
    public class DBSearch
    {
        public static void Search(string articul, string connection_str)
        {
            //string connection = System.Configuration.ConfigurationManager.ConnectionStrings["PathData"].ConnectionString;
            SQLiteConnection m_dbCon = new SQLiteConnection(connection_str);
            SQLiteCommand m_cmd = m_dbCon.CreateCommand();
            m_cmd.CommandText = @"SELECT * FROM PathFiles WHERE file_path like '%" + articul+ "%'";
            m_cmd.CommandType = System.Data.CommandType.Text;

            m_dbCon.Open();
            SQLiteDataReader reader = m_cmd.ExecuteReader();
            DataTable t = new DataTable();
            t.Load(reader);
            m_dbCon.Close();

        }
    }
    
}
