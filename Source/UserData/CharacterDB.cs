using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using RPGproject.Source.CharacterCreation;
using RPGproject.Source.UserData;
using System.Diagnostics;

namespace RPGproject.Source.UserData
{
    class CharacterDB
    {
        private static string CreateTableQuery = @"CREATE TABLE IF NOT EXISTS [Character] (
	[ID]	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    [CampaignID] INTEGER NOT NULL,
	[Name]	TEXT NOT NULL,
	[Alignment]	TEXT,
	[ExperiencePoints]	INTEGER NOT NULL,
	[Level]	INTEGER NOT NULL,
	[Age]	INTEGER NOT NULL,
	[HeightFeet]	TEXT NOT NULL,
    [HeightInches]  TEXT NOT NULL,
	[Weight]	REAL NOT NULL,
	[CharacterBackstory]	TEXT,
	[Appearance]	TEXT,
	[CharacterClass]	TEXT NOT NULL,
	[CharacterRace]	TEXT NOT NULL,
    [Strength] INTEGER NOT NULL,
    [Dexterity] INTEGER NOT NULL,
    [Constitution] INTEGER NOT NULL,
    [Intelligence] INTEGER NOT NULL,
    [Wisdom] INTEGER NOT NULL,
    [Charisma] INTEGER NOT NULL
    )";

        public static void Initialize()
        {
            using (SQLiteCommand comm = new SQLiteCommand(DBAccess.charDBConnection))
            {
                DBAccess.OpenCharDBConnection();
                comm.CommandText = CharacterDB.CreateTableQuery;
                comm.ExecuteNonQuery();
                DBAccess.CloseCharDBConnection();
            }

            RecoverCharacters();
        }

        public static void InsertCharacter(Character C)
        {
            using (SQLiteCommand com = new SQLiteCommand(DBAccess.charDBConnection))
            {
                DBAccess.OpenCharDBConnection();
                com.CommandText = InsertCommand(C);
                com.ExecuteNonQuery();
                DBAccess.CloseCharDBConnection();
                Debug.WriteLine("Inserido com sucesso.");
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
                        Debug.WriteLine(CreatedCharacters.UserCharacters.Count);
                    }
                }

                DBAccess.CloseCharDBConnection();
            }
        }

        private static string InsertCommand(Character C)
        {
            string command = "INSERT INTO Character (CampaignID,Name,Alignment,ExperiencePoints,Level,Age,HeightFeet,HeightInches,Weight," +
                "CharacterClass,CharacterRace,Strength,Dexterity,Constitution,Intelligence,Wisdom,Charisma) Values ('" + -1 + "','" + C.Name + "','"
                + C.Alignment + "','" + C.ExperiencePoints + "','" + C.Level + "','" + C.Age + "','" + C.HeightInFeet + "','" + C.HeightInInches + "','" + C.Weight + "','" + C.CharacterClass.Name + 
                "','" + C.CharacterRace.Name + "','" + C.Attributes.Find(x => x.Name == "Strength").Value + "','" + C.Attributes.Find(x => x.Name == "Dexterity").Value + "','"
                + C.Attributes.Find(x => x.Name == "Constitution").Value + "','" + C.Attributes.Find(x => x.Name == "Intelligence").Value + "','"
                + C.Attributes.Find(x => x.Name == "Wisdom").Value + "','" + C.Attributes.Find(x => x.Name == "Charisma").Value + "')";

            return command;
        }

        private static Character GetCharacterModel(SQLiteDataReader reader)
        {
            StandardLoader loader = new StandardLoader();
            loader.LoadStandardValues();

            Character a = new Character();
            a.CharacterID = (int)(long)reader["ID"];
            a.Name = (string)reader["Name"];
            a.Alignment = (string)reader["Alignment"];
            a.Age = (int)(long)reader["Age"];
            a.HeightInFeet = (string)reader["HeightFeet"];
            a.HeightInInches = (string)reader["HeightInches"];
            a.Weight = (double)reader["Weight"];
            //a.CharacterBackstory = (string)reader["CharacterBackstory"];

            List<CharAttribute> Attributes = new List<CharAttribute>();
            Attributes.Add(new CharAttribute("Strength", (int)(long)reader["Strength"]));
            Attributes.Add(new CharAttribute("Dexterity", (int)(long)reader["Dexterity"]));
            Attributes.Add(new CharAttribute("Constitution", (int)(long)reader["Constitution"]));
            Attributes.Add(new CharAttribute("Intelligence", (int)(long)reader["Intelligence"]));
            Attributes.Add(new CharAttribute("Wisdom", (int)(long)reader["Wisdom"]));
            Attributes.Add(new CharAttribute("Charisma", (int)(long)reader["Charisma"]));

            a.Attributes = Attributes;

            string selectedClass = (string)reader["CharacterClass"];
            string selectedRace = (string)reader["CharacterRace"];

            foreach (Class c in StandardLoader.Classes)
            {
                if (selectedClass.Equals(c.Name))
                {
                    a.CharacterClass = c;
                    break;
                }
            }

            foreach (Race r in StandardLoader.Races)
            {
                if (selectedRace.Equals(r.Name))
                {
                    a.CharacterRace = r;
                    break;
                }
            }

            return a;
        }
    }
}
