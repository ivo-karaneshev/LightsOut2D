using System.IO.IsolatedStorage;
using System.IO;
using Newtonsoft.Json;

namespace LightsOut2
{
    public class PlayerManager
    {
        private class Player
        {
            public string Name { get; set; }

            public int? BestTime { get; set; }

            public bool Active { get; set; }
        }

        private Player player;
        private string fileName = "highscores";

        public void InitializePlayer()
        {
            IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForApplication();

            if (savegameStorage.FileExists(fileName))
            {
                using (var fileStream = savegameStorage.OpenFile(fileName, FileMode.Open))
                {
                    var serializer = new JsonSerializer();
                    using (var streamReader = new StreamReader(fileStream))
                    {
                        using (var jsonReader = new JsonTextReader(streamReader))
                        {
                            try
                            {
                                player = serializer.Deserialize<Player>(jsonReader);
                            }
                            catch (JsonSerializationException)
                            {
                                savegameStorage.DeleteFile(fileName);
                            }
                        }
                    }
                }
            }

            if (player == null)
            {
                player = new Player
                {
                    Active = true,
                    Name = "default",
                    BestTime = null
                };
            }
        }

        public int? GetBestTime()
        {
            return player.BestTime;
        }

        public void Save(int time)
        {
            if (!player.BestTime.HasValue || time < player.BestTime)
            {
                player.BestTime = time;
                WriteFile();
            }
        }

        public void WriteFile()
        {
            IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForApplication();

            using (var fileStream = savegameStorage.OpenFile(fileName, FileMode.OpenOrCreate))
            {
                var serializer = new JsonSerializer();
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    using (var jsonWriter = new JsonTextWriter(streamWriter))
                    {
                        serializer.Serialize(jsonWriter, player);
                    }
                }
            }
        }
    }
}