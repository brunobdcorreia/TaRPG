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

        public static void Initialize()
        {

           using (SQLiteCommand com = new SQLiteCommand(DBAcess.con))
            {
                DBAcess.Open();
                com.CommandText = CharacterDB.CreateTableQuery;
                com.ExecuteNonQuery();
                DBAcess.Close();
                   
            }
            Recover();
        }

        public static void Insert(Character C){
             using (SQLiteCommand com = new SQLiteCommand(DBAcess.con))
              {
                DBAcess.Open();
                com.CommandText = Command(C);
                com.ExecuteNonQuery();
                DBAcess.Close();

              }         
     
        }

        public static void Delete(int ID)
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

        public static int GetIDLast()
        {
            int a;
            using (SQLiteCommand com = new SQLiteCommand(DBAcess.con))
            {
                DBAcess.Open();
                com.CommandText = "Select * FROM Character WHERE ID = (SELECT MAX(ID) FROM Character)";
                com.ExecuteNonQuery();
                using (SQLiteDataReader reader = com.ExecuteReader())
                {
                    reader.Read();
                    a = (int)(long)reader["ID"];
                }
                DBAcess.Close();

            }
            return a;
        }
        public static void Recover()
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

                        CreatedCharacters.AddCharacter(new Character((int)(long)reader["ID"],(string)reader["Name"],(string)reader["Alignment"], (int)(long)reader["Age"],(string)reader["Height"],
                            (double)reader["Weight"],(string)reader["CharacterBackstory"], (int)(long)reader["Strength"], (int)(long)reader["Dexterity"], (int)(long)reader["Constitution"],
                            (int)(long)reader["Intelligence"], (int)(long)reader["Wisdom"], (int)(long)reader["Charisma"],(string)reader["CharacterClass"],(string)reader["CharacterRace"],
                            (int)(long)reader["ExperiencePoints"], (int)(long)reader["Level"]));

                    }
                }
                DBAcess.Close();

            }
        }

        private static string Command(Character C)
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

        
    }
}
