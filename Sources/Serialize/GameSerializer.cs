using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Serialize
{
    public class GameSerializer : ISerialize
    {
        private static readonly string _baseFolder = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        "Quarto", "Data", "Games");

        private const string DefaultFileName = "GameSaves.xml";

        public GameSerializer()
        {
            if (!Directory.Exists(_baseFolder))
            {
                Directory.CreateDirectory(_baseFolder);
            }
        }

        public T? Load<T>()
        {
            string filePath = Path.Combine(_baseFolder, DefaultFileName);

            if (!File.Exists(filePath))
                return default;

            var serializer = new XmlSerializer(typeof(T));
            using var stream = new FileStream(filePath, FileMode.Open);
            return (T?)serializer.Deserialize(stream);
        }

        public void Save<T>(T data)
        {
            string filePath = Path.Combine(_baseFolder, DefaultFileName);

            var serializer = new XmlSerializer(typeof(T));
            using var stream = new FileStream(filePath, FileMode.Create);
            serializer.Serialize(stream, data);
        }

        public static void Save<T>(T data, string fileName)
        {
            string filePath = Path.Combine(_baseFolder, fileName);

            var serializer = new XmlSerializer(typeof(T));
            using var stream = new FileStream(filePath, FileMode.Create);
            serializer.Serialize(stream, data);
        }

        public static T? Load<T>(string fileName)
        {
            string filePath = Path.Combine(_baseFolder, fileName);

            if (!File.Exists(filePath))
                return default;

            var serializer = new XmlSerializer(typeof(T));
            using var stream = new FileStream(filePath, FileMode.Open);
            return (T?)serializer.Deserialize(stream);
        }
    }
}
