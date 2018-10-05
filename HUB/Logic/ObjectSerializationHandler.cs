namespace HUB.Logic
{
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;

    public class ObjectSerializationHandler
    {
        public static void SerializeObject(object obj)
        {
            IFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(Directory.GetCurrentDirectory() + "/data.bin", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(stream, obj);
            }
        }

        public static object DeserializeObject(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    IFormatter formatter = new BinaryFormatter();
                    using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        return formatter.Deserialize(stream);
                    }
                }
                catch
                {
                    
                }
            }
            return null;
        }
    }
}
