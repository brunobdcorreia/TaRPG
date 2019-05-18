using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace RPGproject
{
    public static class DataAccess
    {
        public static void InitializeDatabase()
        {
            using (SqliteConnection db = new SqliteConnection("Filename=TestSqlite.db"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT EXISTS MyTable (Primary_Key INTEGER PRIMARY KEY, Text_Entry NVARCHAR(2048) NULL)";
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                try
                {
                    createTable.ExecuteReader();
                }
                catch (SqliteException e)
                {
         
                }
            }
        }
    }
}
