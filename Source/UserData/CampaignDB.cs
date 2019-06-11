using RPGproject.Source.CampaignCreation;
using RPGproject.Source.CharacterCreation;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGproject.Source.UserData
{
    class CampaignDB
    {
        private static string createCampaignTableQuery = @"CREATE TABLE IF NOT EXISTS [Campaigns] (
        [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
        [Name] TEXT NOT NULL
        )";

        private static string createEventLogTableQuery = @"CREATE TABLE IF NOT EXISTS [EventLog] (
        [CampaignID] INTEGER NOT NULL,
        [EventID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
        [EventDesc] TEXT NOT NULL
        )";

        private static string createCampaignCharacterTableQuery = @"CREATE TABLE IF NOT EXISTS [CampaignCharacter] (
        [CampaignID] INTEGER NOT NULL,
        [CharacterID] INTEGER NOT NULL
        )";

        public static void Initialize()
        {
            using (SQLiteCommand comm = new SQLiteCommand(DBAccess.campaignDBConnection))
            {
                DBAccess.OpenCampaignDBConnection();
                comm.CommandText = createCampaignTableQuery;
                comm.ExecuteNonQuery();

                comm.CommandText = createEventLogTableQuery;
                comm.ExecuteNonQuery();

                comm.CommandText = createCampaignCharacterTableQuery;
                comm.ExecuteNonQuery();
                DBAccess.CloseCampaignDBConnection();
            }
        }

        public static void InsertCampaign(Campaign campaign)
        {
            using (SQLiteCommand comm = new SQLiteCommand(DBAccess.campaignDBConnection))
            {
                int campaignID = -1;
                int characterID = -1;

                DBAccess.OpenCampaignDBConnection();
                comm.CommandText = InsertCommand(campaign);
                comm.ExecuteNonQuery();

                comm.CommandText = "SELECT ID FROM Campaigns WHERE Name = @name";
                comm.Parameters.AddWithValue("@name", campaign.CampaignName);
                comm.ExecuteNonQuery();

                using (SQLiteDataReader reader = comm.ExecuteReader())
                {
                    campaignID = (int)(long)reader["ID"];
                }

                if(campaign.Characters.Count > 0)
                {
                    using (SQLiteCommand secondCommand = new SQLiteCommand(DBAccess.charDBConnection))
                    {
                        foreach(Character c in campaign.Characters)
                        {
                            secondCommand.CommandText = "SELECT ID FROM Character WHERE Name = @name";
                            secondCommand.Parameters.AddWithValue("@name", c.Name);
                            secondCommand.ExecuteNonQuery();

                            using (SQLiteDataReader reader = secondCommand.ExecuteReader())
                            {
                                characterID = (int)(long)reader["ID"];
                            }

                            secondCommand.CommandText = "INSERT INTO CampaignCharacter (CampaignID,CharacterID) VALUES ('" + campaignID + "','" + characterID + "')";
                            secondCommand.ExecuteNonQuery();
                        }
                    }
                }

                DBAccess.CloseCampaignDBConnection();
            }
        }

        /*public static int GetCampaignID(Campaign campaign)
        {
            int campaignID = 0;

            using (SQLiteCommand comm = new SQLiteCommand(DBAccess.campaignDBConnection))
            {
                DBAccess.OpenCampaignDBConnection();
                comm.CommandText = "SELECT ID FROM Campaigns WHERE Name = @name";
                comm.Parameters.AddWithValue("@name", campaign.CampaignName);
                comm.ExecuteNonQuery();

                using (SQLiteDataReader reader = comm.ExecuteReader())
                {
                    campaignID = (int)(long)reader["ID"];
                }

                DBAccess.CloseCampaignDBConnection();
                return campaignID;
            }
        }*/

        private static string InsertCommand(Campaign campaign)
        {
            string command = "INSERT INTO Campaigns (Name) VALUES ('" + campaign.CampaignName + "')";
            return command;
        }

        private static void RecoverCampaigns()
        {
            using (SQLiteCommand comm = new SQLiteCommand(DBAccess.campaignDBConnection))
            {
                DBAccess.OpenCampaignDBConnection();
                comm.CommandText = "SELECT * FROM Campaigns, CampaignCharacter, Character";
                comm.ExecuteNonQuery();

                using (SQLiteDataReader reader = comm.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        StandardLoader loader = new StandardLoader();
                        loader.LoadStandardValues();
                        Campaign campaign = new Campaign();
                        Character character = new Character();

                        campaign.CampaignName = (string)reader["Campaigns.Name"];
                        character.Name = (string)reader["Character.Name"];
                        character.CharacterClass = StandardLoader.Classes.Find(x => x.Name == (string)reader["CharacterClass"]);
                        character.CharacterRace = StandardLoader.Races.Find(x => x.Name == (string)reader["CharacterRace"]);
                        character.Age = (int)(long)reader["Age"];
                        character.HeightInFeet = (string)reader["HeightFeet"];
                        character.HeightInInches = (string)reader["HeightInches"];
                        character.Weight = (double)reader["Weight"];

                        List<CharAttribute> charAttributes = new List<CharAttribute>();
                        charAttributes.Add(new CharAttribute("Strength", (int)(long)reader["Strength"]));
                        charAttributes.Add(new CharAttribute("Dexterity", (int)(long)reader["Dexterity"]));
                        charAttributes.Add(new CharAttribute("Constitution", (int)(long)reader["Constitution"]));
                        charAttributes.Add(new CharAttribute("Intelligence", (int)(long)reader["Intelligence"]));
                        charAttributes.Add(new CharAttribute("Wisdom", (int)(long)reader["Wisdom"]));
                        charAttributes.Add(new CharAttribute("Charisma", (int)(long)reader["Charisma"]));

                        character.Attributes = charAttributes;
                    }
                }

                DBAccess.CloseCampaignDBConnection();
            }
        }
    }
}
