using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using RPGproject.Source.CampaignCreation;

namespace RPGproject.Source.Data
{
    class Campaign_CharacterDB
    {
        private static string CreateTableQuery = @"CREATE TABLE IF NOT EXISTS [Campaign_Character] (
        [ID]	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	    [Campaign_ID]	INTEGER NOT NULL,
	    [Character_ID]	INTEGER NOT NULL    
	    )";

        private static string insertCampaign_Character = "INSERT INTO Campaign_Character (Campaign_ID,Character_ID) VALUES (@a1,@a2)";

        public static void Initialize()
        {
            using (SQLiteCommand com = new SQLiteCommand(DBAcess.con))
            {
                DBAcess.Open();
                com.CommandText = Campaign_CharacterDB.CreateTableQuery;
                com.ExecuteNonQuery();
                DBAcess.Close();
            }
        }

        public static void InsertCreatedCampaign(Campaign C)
        {
            using (SQLiteCommand com = new SQLiteCommand(DBAcess.con))
            {
                DBAcess.Open();
                for (int i = 0; i < C.Characters.Count; i++) { 
                    com.CommandText = Campaign_CharacterDB.insertCampaign_Character;
                    com.Parameters.AddWithValue("@a1",C.CampaignName );
                    com.Parameters.AddWithValue("@a2", C.Characters[i].characterID);
                    com.ExecuteNonQuery();
                }
                DBAcess.Close();
            }
        }
    }
}
