using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGproject.Source.CharacterCreation;
using Windows.Storage;

namespace RPGproject.Source.UserData
{
    class CharacterDBAccess
    {
        public static string DB_PATH = Path.Combine(Path.Combine(ApplicationData.Current.LocalFolder.Path, "UserCharacters.sqlite"));
        public void CreateDatabase(String DB_PATH)
        {
            if(!CheckIfFileExists(DB_PATH).Result)
            {
                using (SQLite.Net.SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DB_PATH))
                {
                    try
                    {
                        conn.CreateTable<Character>();
                    }

                    catch(System.NotSupportedException ex)
                    {
                        Debug.WriteLine(ex.Data);
                    }
                }
            }
        }

        private async Task<bool> CheckIfFileExists(string fileName)
        {
            try
            {
                var store = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return true;
            }

            catch
            {
                return false;
            }
        }

        public void Insert(Character character)
        {
            using (SQLite.Net.SQLiteConnection connection = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), DB_PATH))
            {
                connection.RunInTransaction(() =>
                {
                    connection.Insert(character);
                });
            }
        }
    }
}
