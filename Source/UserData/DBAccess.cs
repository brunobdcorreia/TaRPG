using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Windows.Storage;
using System.IO;

namespace RPGproject.Source.UserData
{
    class DBAccess
    {
        private static readonly string campaignFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "TARPG_CAMPAIGNDATA.sqlite");
        private static readonly string charFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "TARPG_CHARACTERDATA.sqlite");
        public static SQLiteConnection campaignDBConnection = new SQLiteConnection("data source=" + campaignFilePath);
        public static SQLiteConnection charDBConnection = new SQLiteConnection("data source=" + charFilePath);

        public static void InitializeDB()
        {
            if (!File.Exists(charFilePath))
            {
                SQLiteConnection.CreateFile(charFilePath);
            }

            if(!File.Exists(campaignFilePath))
            {
                SQLiteConnection.CreateFile(campaignFilePath); 
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

        public static void OpenCampaignDBConnection()
        {
            campaignDBConnection.Open();
        }

        public static void CloseCampaignDBConnection()
        {
            campaignDBConnection.Close();
        }
    }
}