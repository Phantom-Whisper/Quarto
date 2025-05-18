using Manager;
using System.Xml.Serialization;

namespace Serialize
{
    public class PlayerScoreTracker : ISerialize
    {
        private static readonly string filePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "Quarto", "Data");

        private const string xmlFile = "ScoreTracker.xml";
        private readonly string path = Path.Combine(filePath, xmlFile);

        public PlayerScoreTracker()
        {
            try
            {
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating directory: {ex.Message}");
            }
        }

        public T Load<T>()
        {
            if (!File.Exists(path) || new FileInfo(path).Length == 0)
                return default;

            try
            {
                using FileStream stream = new FileStream(path, FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stream);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading file: {ex.Message}");
                return default;
            }
        }

        public void Save<T>(T data)
        {
            try
            {
                using FileStream stream = new FileStream(path, FileMode.Create);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stream, data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file: {ex.Message}");
            }
        }
    }
}
