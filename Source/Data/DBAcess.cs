using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Windows.Storage;
using System.IO;

namespace RPGproject.Source.Data
{
    class DBAcess
    {
        public static readonly string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, "TARPG_CHARACTERDATA.sqlite");
        public static SQLiteConnection con = new SQLiteConnection("data source="+path);
        public static void initializeDB()
        {
            if (!File.Exists(path))
            {
                SQLiteConnection.CreateFile(path);
            }
            CharacterDB.Initilalize();
        }

        public static void Open()
        {
            con.Open();
        }
        public static void Close()
        {
            con.Close();
        }
    }
}
