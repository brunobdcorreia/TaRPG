using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using RPGproject.Source.CampaignCreation;

namespace RPGproject.Source.Data
{
    class CampaignDB
    {
        private static string CreateTableQuery = @"CREATE TABLE IF NOT EXISTS [Campaign] (
        [ID]	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	    [Name]	TEXT NOT NULL,
        [Number_Players] INTEGER NOT NULL
	    )";

        private static string insertCampaign = "INSERT INTO Campaign (Name,Number_Players) VALUES (@a1,@a2)";

        public static void Initialize()
        {
            using (SQLiteCommand com = new SQLiteCommand(DBAcess.con))
            {
                DBAcess.Open();
                com.CommandText = CampaignDB.CreateTableQuery;
                com.ExecuteNonQuery();
                DBAcess.Close();
            }
            Campaign_CharacterDB.Initialize();
        }

        public static void Insert(Campaign C)
        {
            using (SQLiteCommand com = new SQLiteCommand(DBAcess.con))
            {
                DBAcess.Open();
                com.CommandText = CampaignDB.insertCampaign;
                com.Parameters.AddWithValue("@a1", C.CampaignName);
                com.Parameters.AddWithValue("@a2", C.NumberOfPlayers);
                com.ExecuteNonQuery();
                DBAcess.Close();
            }
            Campaign_CharacterDB.InsertCreatedCampaign(C);

        }

    }
}
