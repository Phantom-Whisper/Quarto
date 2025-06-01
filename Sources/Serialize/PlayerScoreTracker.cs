using Manager;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Serialize
{
    /// <summary>
    /// Handles serialization and deserialization of player scores to and from an XML file.
    /// Ensures the target directory exists and manages file reading/writing errors.
    /// </summary>
    public class PlayerScoreTracker : ISerialize
    {
        private static readonly string filePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "Quarto", "Data");

        private const string xmlFile = "ScoreTracker.xml";
        private readonly string path = Path.Combine(filePath, xmlFile);

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerScoreTracker"/> class.
        /// Ensures the directory for storing the XML file exists.
        /// </summary>
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
                Debug.WriteLine($"Error creating directory: {ex.Message}");
            }
        }

        /// <summary>
        /// Loads and deserializes data from the XML file.
        /// Returns null if the file does not exist, is empty, or deserialization fails.
        /// </summary>
        /// <typeparam name="T">The type of data to deserialize.</typeparam>
        /// <returns>The deserialized data of type <typeparamref name="T"/>, or null if loading fails.</returns>
        public T? Load<T>()
        {
            if (!File.Exists(path) || new FileInfo(path).Length == 0)
                return default;

            try
            {
                using FileStream stream = new FileStream(path, FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                var result = serializer.Deserialize(stream);
                if (result is null) return default;
                return (T)result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading file: {ex.Message}");
                return default;
            }
        }

        /// <summary>
        /// Serializes and saves data to the XML file, overwriting existing content.
        /// </summary>
        /// <typeparam name="T">The type of data to serialize.</typeparam>
        /// <param name="data">The data object to serialize and save.</param>
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
                Debug.WriteLine($"Error saving file: {ex.Message}");
            }
        }
    }
}
