using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Classes
{
        static class Data
        {
            private const string relativePath = "jacksonmiller/highscores";
            private static readonly string dataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments, Environment.SpecialFolderOption.Create),
                relativePath);

            public static bool Save<T>(T data, string fileName)
            {
                FileStream fs = new FileStream(Path.Combine(dataPath, $"{fileName}.jpp"), FileMode.Create);

                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    formatter.Serialize(fs, data);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                    return false;
                }

                fs.Close();
                return true;
            }

            public static T Load<T>(string fileName)
            {
                FileStream fs = new FileStream("DataFile.dat", FileMode.Open);
                T data;
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    data = (T)formatter.Deserialize(fs);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                    throw;
                }

                fs.Close();
                return data;
            }
        }
}
