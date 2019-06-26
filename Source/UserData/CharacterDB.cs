using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using RPGproject.Source.CharacterCreation;
using RPGproject.Source.UserData;
using System.Diagnostics;
using System.Globalization;

namespace RPGproject.Source.UserData
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
    [IntMod] INTEGER NOT NULL,
    [WisMod] INTEGER NOT NULL,
    [ConMod] INTEGER NOT NULL,
    [ChaMod] INTEGER NOT NULL
    )";

        

        public static void Initialize()
        {
            using (SQLiteCommand comm = new SQLiteCommand(DBAccess.charDBConnection))
            {
                DBAccess.OpenCharDBConnection();
                comm.CommandText = CreateTableQuery;
                comm.ExecuteNonQuery();
                DBAccess.CloseCharDBConnection();
            }

            RecoverCharacters();
        }
        public static void executeCommand(string command)
        {
            using (SQLiteCommand com = new SQLiteCommand(DBAccess.charDBConnection))
            {
                DBAccess.OpenCharDBConnection();
                com.CommandText = command;
                com.ExecuteNonQuery();
                DBAccess.CloseCharDBConnection();
            }
        }

        public static void DeleteCharacterbyID(Character C)
        {
            using (SQLiteCommand com = new SQLiteCommand(DBAccess.charDBConnection))
            {
                DBAccess.OpenCharDBConnection();
                com.CommandText = "DELETE FROM Character Where ID = @id";
                com.Parameters.AddWithValue("@id", C.CharacterID);
                com.ExecuteNonQuery();
                CreatedCharacters.DeleteCharacter(C);
                DBAccess.CloseCharDBConnection();
            }
        }

        public static void InsertCharacter(Character C)
        {
            using (SQLiteCommand com = new SQLiteCommand(DBAccess.charDBConnection))
            {
                DBAccess.OpenCharDBConnection();
                com.CommandText = InsertCommand(C);
                com.ExecuteNonQuery();
                DBAccess.CloseCharDBConnection();
            }
        }

        public static void DeleteCharacter(Character character)
        {
            using (SQLiteCommand com = new SQLiteCommand(DBAccess.charDBConnection))
            {
                DBAccess.OpenCharDBConnection();
                com.CommandText = "DELETE FROM Character Where Name = @name";
                com.Parameters.AddWithValue("@name", character.Name);
                com.ExecuteNonQuery();
                CreatedCharacters.DeleteCharacter(character);
                DBAccess.CloseCharDBConnection();
            }
        }

        public static void RecoverCharacters()
        {
            using (SQLiteCommand com = new SQLiteCommand(DBAccess.charDBConnection))
            {
                DBAccess.OpenCharDBConnection();
                com.CommandText = "Select * FROM Character";
                com.ExecuteNonQuery();

                using (SQLiteDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CreatedCharacters.AddCharacter(GetCharacterModel(reader));
                    }
                }

                DBAccess.CloseCharDBConnection();
            }
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

        private static Character GetCharacterModel(SQLiteDataReader reader)
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
            character.Weight = double.Parse(((string)reader["Weight"]).Replace(',','.'), CultureInfo.InvariantCulture);
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

            using (SQLiteCommand comm = new SQLiteCommand(DBAccess.charDBConnection))
            {
                DBAccess.OpenCharDBConnection();
                comm.CommandText = "SELECT ID FROM Character WHERE Name = @name";
                comm.Parameters.AddWithValue("@name", c.Name);
                comm.ExecuteNonQuery();

                using (SQLiteDataReader reader = comm.ExecuteReader())
                {
                    characterId = (int)(long)reader["ID"];
                }
            }

            return characterId;
        }
    }
}
