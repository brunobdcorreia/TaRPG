using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;



namespace ANDROID_TARPG
{
    class DBAccess
    {

        private static readonly string charFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "TARPG_CHARACTERDATA.sqlite");


        public static SQLiteConnection charDBConnection = new SQLiteConnection("data source=" + charFilePath);


        public static void InitializeDB()
        {
            if (!File.Exists(charFilePath))
            {
                SQLiteConnection.CreateFile(charFilePath);
            }

            CharacterDB.Initialize();
        }

        public static void OpenCharDBConnection()
        {
            charDBConnection.Open();
        }

        public static void CloseCharDBConnection()
        {
            charDBConnection.Close();
        }

  


    }
}