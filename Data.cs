using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Game
{
    static class Data
    {
        private const string relativePath = "Jaboom/Data";
        private static readonly string dataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments, Environment.SpecialFolderOption.Create),
            relativePath);

        private static string FullPath => Path.Combine(dataPath, relativePath); 

        public static bool Save<T>(T data, string fileName)
        {
            if (!Directory.Exists(FullPath))
            {
                Directory.CreateDirectory(FullPath);
            }
            FileStream fs = new FileStream(Path.Combine(FullPath, $"{fileName}.life"), FileMode.Create);

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