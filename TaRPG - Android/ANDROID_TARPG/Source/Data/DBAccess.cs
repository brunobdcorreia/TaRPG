using PCLExt.FileStorage;
using PCLExt.FileStorage.Folders;
using SQLite;
using System.IO;
using Mono.Data.Sqlite;
namespace ANDROID_TARPG
{
    class DBAccess
    {

        private static string charFilePath;

        public static SQLiteConnection charDBConnection;
        public static SqliteConnection charDBConnectionReader;

        public static void InitializeDB()
        {

            var pasta = new LocalRootFolder();
            var arquivo = pasta.CreateFile("TARPG_CHARACTERDATA.db", CreationCollisionOption.OpenIfExists);
            charFilePath = arquivo.Path;
            charDBConnectionReader = new SqliteConnection("data source=" + charFilePath);
            charDBConnection = new SQLiteConnection(charFilePath);
            CharacterDB.Initialize();
        }


        public static void CloseCharDBConnection()
        {
            charDBConnection.Close();
        }

        public static void Execute(string x)
        {
            charDBConnection.Execute(x);
        }

        public static void Query(string x)
        {
        }

        public static void OpenCharDBConnectionReader()
        {
            charDBConnectionReader.Open();
        }


        public static void CloseCharDBConnectionReader()
        {
            charDBConnectionReader.Close();
        }



    }
}