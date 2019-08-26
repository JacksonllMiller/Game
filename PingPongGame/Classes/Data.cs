using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace PingPongGame.Classes
{
    static class Data
    {
        private const string relativePath = "jacksonmiller/highscores";
        private static readonly string dataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments, Environment.SpecialFolderOption.Create),
            relativePath);

        public static bool Save<T>(T data, string fileName)
        {
            if (!Directory.Exists(dataPath))
                Directory.CreateDirectory(dataPath);

            using (FileStream fs = new FileStream(Path.Combine(dataPath, $"{fileName}.jpp"), FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, data);
            }

            return true;
        }

        public static T Load<T>(string fileName)
        {
            T data;
            using (FileStream fs = new FileStream(Path.Combine(dataPath, $"{fileName}.jpp"), FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                data = (T)formatter.Deserialize(fs);
            }

            return data;
        }
    }

    [Serializable]
    public struct HighScore
    {
        public string Name { get; private set; }
        public int Score { get; private set; }

        public HighScore(string name, int score)
        {
            Name = name;
            Score = score; 
        }
    }
}
