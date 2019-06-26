using System.Collections.Generic;
using System.Linq;
using SQLite;
using Mono.Data.Sqlite;
using ANDROID_TARPG;
using ANDROID_TARPG.Source.Connection;
using System.Globalization;

namespace ANDROID_TARPG
{
    class CharacterDB
    {
        private static string CreateTableQuery = @"CREATE TABLE IF NOT EXISTS [Character] (
	[ID]	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	[Name]	TEXT NOT NULL,
	[Alignment]	TEXT,
	[ExperiencePoints]	INTEGER NOT NULL,
	[Level]	INTEGER NOT NULL,
	[Age]	INTEGER NOT NULL,
	[HeightFeet]	TEXT NOT NULL,
    [HeightInches]  TEXT NOT NULL,
	[Weight]	TEXT NOT NULL,
	[CharacterBackstory]	TEXT,
	[Appearance]	TEXT,
	[CharacterClass]	TEXT NOT NULL,
	[CharacterRace]	TEXT NOT NULL,
    [Strength] INTEGER NOT NULL,
    [Dexterity] INTEGER NOT NULL,
    [Constitution] INTEGER NOT NULL,
    [Intelligence] INTEGER NOT NULL,
    [Wisdom] INTEGER NOT NULL,
    [Charisma] INTEGER NOT NULL,
    [StrMod] INTEGER NOT NULL,
    [DexMod] INTEGER NOT NULL,
    [ConMod] INTEGER NOT NULL,
    [IntMod] INTEGER NOT NULL,
    [WisMod] INTEGER NOT NULL,
    [ChaMod] INTEGER NOT NULL

    )";

        public static void Initialize()
        {
            DBAccess.Execute(CreateTableQuery);       

            RecoverCharacters();
        }

        public static void InsertCharacter(Character C)
        {
            DBAccess.OpenCharDBConnectionReader();

            using (SqliteCommand com = new SqliteCommand())            {

                com.CommandText = InsertCommand(C);
                com.Connection = DBAccess.charDBConnectionReader;
                com.ExecuteNonQuery();
            }
            DBAccess.CloseCharDBConnectionReader();

            //Client.SendData(InsertCommand(C));
        }

        public static void SendCharacter(Character C)
        {
            Client.SendData(InsertCommand(C));
        }

        public static void DeleteCharacter(Character character)
        {
            DBAccess.OpenCharDBConnectionReader();

            using (SqliteCommand com = new SqliteCommand())
            {

                com.CommandText = "DELETE FROM Character Where Name = " +character.Name;
                com.Connection = DBAccess.charDBConnectionReader;
                com.ExecuteNonQuery();
            }
            DBAccess.CloseCharDBConnectionReader();
           
            //Client.SendData("DELETE FROM Character Where Name = " + character.Name);
            CreatedCharacters.DeleteCharacter(character);

        }

        public static void DeleteCharacterbyID(Character C)
        {
            DBAccess.OpenCharDBConnectionReader();

            using (SqliteCommand com = new SqliteCommand())
            {

                com.CommandText = "DELETE FROM Character Where ID = " +C.CharacterID;
                com.Connection = DBAccess.charDBConnectionReader;
                com.ExecuteNonQuery();
            }
            DBAccess.CloseCharDBConnectionReader();

            //Client.SendData("DELETE FROM Character Where ID = " + C.CharacterID);
            CreatedCharacters.DeleteCharacter(C);
         
        }

        public static void RecoverCharacters()
        {
            DBAccess.OpenCharDBConnectionReader();

            using (SqliteCommand com = new SqliteCommand())
            {
                com.CommandText = "Select * FROM Character";
                com.Connection = DBAccess.charDBConnectionReader;
                com.ExecuteNonQuery();

                using (SqliteDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CreatedCharacters.AddCharacter(GetCharacterModel(reader));
                    }
                }

                
            }

            DBAccess.CloseCharDBConnectionReader();
        }

        private static string InsertCommand(Character C)
        {
            string command = "INSERT INTO Character (Name,Alignment,ExperiencePoints,Level,Age,HeightFeet,HeightInches,Weight," +
                "CharacterClass,CharacterRace,Strength,Dexterity,Constitution,Intelligence,Wisdom,Charisma, StrMod, DexMod, IntMod, ConMod, WisMod, ChaMod) " +
                "Values ('" + C.Name + "','" + C.Alignment + "','" + C.ExperiencePoints + "','" + C.Level + "','" + C.Age + "','" + C.HeightInFeet + "','" + C.HeightInInches + "','" + C.Weight + "','" + C.CharacterClass.Name + 
                "','" + C.CharacterRace.Name + "','" + C.Attributes.Find(x => x.Name == "Strength").Value + "','" + C.Attributes.Find(x => x.Name == "Dexterity").Value + "','"
                + C.Attributes.Find(x => x.Name == "Constitution").Value + "','" + C.Attributes.Find(x => x.Name == "Intelligence").Value + "','"
                + C.Attributes.Find(x => x.Name == "Wisdom").Value + "','" + C.Attributes.Find(x => x.Name == "Charisma").Value + "','" + C.AttributeModifiers.ElementAt(0) + 
                "','" + C.AttributeModifiers.ElementAt(1) + "','" + C.AttributeModifiers.ElementAt(2) + "','" + C.AttributeModifiers.ElementAt(3) + "','" 
                + C.AttributeModifiers.ElementAt(4) + "','" + C.AttributeModifiers.ElementAt(5) + "')";
            return command;
        }

        private static Character GetCharacterModel(SqliteDataReader reader)
        {
            StandardLoader loader = new StandardLoader();
            loader.LoadStandardValues();

            Character character = new Character();
            character.CharacterID = (int)(long)reader["ID"];
            character.Name = (string)reader["Name"];
            character.Alignment = (string)reader["Alignment"];
            character.Age = (int)(long)reader["Age"];
            character.HeightInFeet = (string)reader["HeightFeet"];
            character.HeightInInches = (string)reader["HeightInches"];
            character.Weight = double.Parse(((string)reader["Weight"]).Replace(',', '.'), CultureInfo.InvariantCulture);
            //a.CharacterBackstory = (string)reader["CharacterBackstory"];

            List<CharAttribute> Attributes = new List<CharAttribute>();
            Attributes.Add(new CharAttribute("Strength", (int)(long)reader["Strength"]));
            Attributes.Add(new CharAttribute("Dexterity", (int)(long)reader["Dexterity"]));
            Attributes.Add(new CharAttribute("Constitution", (int)(long)reader["Constitution"]));
            Attributes.Add(new CharAttribute("Intelligence", (int)(long)reader["Intelligence"]));
            Attributes.Add(new CharAttribute("Wisdom", (int)(long)reader["Wisdom"]));
            Attributes.Add(new CharAttribute("Charisma", (int)(long)reader["Charisma"]));

            character.Attributes = Attributes;

            string selectedClass = (string)reader["CharacterClass"];
            string selectedRace = (string)reader["CharacterRace"];

            foreach (Class c in StandardLoader.Classes)
            {
                if (selectedClass.Equals(c.Name))
                {
                    character.CharacterClass = c;
                    break;
                }
            }

            foreach (Race r in StandardLoader.Races)
            {
                if (selectedRace.Equals(r.Name))
                {
                    character.CharacterRace = r;
                    break;
                }
            }

            List<int> attMods = new List<int>();
            attMods.Add((int)(long)reader["StrMod"]);
            attMods.Add((int)(long)reader["DexMod"]);
            attMods.Add((int)(long)reader["ConMod"]);
            attMods.Add((int)(long)reader["IntMod"]);
            attMods.Add((int)(long)reader["WisMod"]);
            attMods.Add((int)(long)reader["ChaMod"]);

            character.AttributeModifiers = attMods;

            return character;
        }

        public static int GetCharacterId(Character c)
        {
            int characterId = 0;

            DBAccess.OpenCharDBConnectionReader();

            using (SqliteCommand com = new SqliteCommand())
            {
                com.CommandText = "SELECT ID FROM Character WHERE Name = " + c.Name;
                com.Connection = DBAccess.charDBConnectionReader;
                com.ExecuteNonQuery();

                using (SqliteDataReader reader = com.ExecuteReader())
                {
                    characterId = (int)(long)reader["ID"];
                }


            }
             return characterId;          
        }
    }
}
