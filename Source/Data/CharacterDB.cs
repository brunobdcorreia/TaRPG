using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using RPGproject.Source.CharacterCreation;
using RPGproject.Source.UserData;

namespace RPGproject.Source.Data
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
	[Height]	TEXT NOT NULL,
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

        public static void Initilalize()
        {

           using (SQLiteCommand com = new SQLiteCommand(DBAcess.con))
            {
                DBAcess.Open();
                com.CommandText = CharacterDB.CreateTableQuery;
                com.ExecuteNonQuery();
                DBAcess.Close();
                   
            }
            recoverCharacters();
        }

        public static void insertCharacter(Character C){
             using (SQLiteCommand com = new SQLiteCommand(DBAcess.con))
              {
                DBAcess.Open();
                com.CommandText = command(C);
                com.ExecuteNonQuery();
                DBAcess.Close();

              }         
     
        }

        public static void deleteCharacter(int ID)
        {
            using (SQLiteCommand com = new SQLiteCommand(DBAcess.con))
            {
                DBAcess.Open();
                com.CommandText = "DELETE FROM Character Where ID = @id";
                com.Parameters.AddWithValue("@id", ID);
                com.ExecuteNonQuery();
                DBAcess.Close();

            }
        }

        public static void recoverCharacters()
        {
            using (SQLiteCommand com = new SQLiteCommand(DBAcess.con))
            {
                DBAcess.Open();
                com.CommandText = "Select * FROM Character";
                com.ExecuteNonQuery();
                using (SQLiteDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        CreatedCharacters.AddCharacter(getCharacterModel(reader));

                    }
                }
                DBAcess.Close();

            }
        }

        private static string command(Character C)
        {
            string command = "INSERT INTO Character (Name,Alignment,ExperiencePoints,Level,Age,Height,Weight," +
                "CharacterBackstory,CharacterClass,CharacterRace,Strength,Dexterity,Constitution,Intelligence,Wisdom,Charisma) Values ('" + C.Name + "','" 
                + C.Alignment + "','" + C.ExperiencePoints + "','" + C.Level + "','" + C.Age + "','" + C.Height + "','" + C.Weight + "','" + C.getCharacterBackstory() + "','" 
                + C.CharacterClass.Name + "','" + C.CharacterRace.Name + "','" + C.Attributes.Find(x => x.Name == "Strength").Value + "','" + C.Attributes.Find(x => x.Name == "Dexterity").Value + "','"
                + C.Attributes.Find(x => x.Name == "Constitution").Value + "','" + C.Attributes.Find(x => x.Name == "Intelligence").Value + "','"
                + C.Attributes.Find(x => x.Name == "Wisdom").Value + "','" + C.Attributes.Find(x => x.Name == "Charisma").Value + "')";
           // System.Diagnostics.Debug.WriteLine(command + "\n" + C.PlayerName);
            return command;
        }

        private static Character getCharacterModel(SQLiteDataReader reader)
        {
            StandardLoader loader = new StandardLoader();
            loader.LoadStandardValues();



            Character a = new Character();
            a.characterID = (int)(long)reader["ID"];
            a.Name = (string)reader["Name"];
            a.Alignment = (string)reader["Alignment"];
            a.Age = (int)(long)reader["Age"];
            a.Height = (string)reader["height"];
            a.Weight = (double)reader["Weight"];
            a.CharacterBackstory = (string)reader["CharacterBackstory"];


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
