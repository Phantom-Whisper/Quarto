using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace Serialize
{
    public class GameStateSerializer
    {
        private static readonly string filePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "Quarto", "Data");

        private const string jsonFile = "SaveGame.json";
        private readonly string path = Path.Combine(filePath, jsonFile);

        private readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = true,
            IncludeFields = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public GameStateSerializer()
        {
            try
            {
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur lors de la création du répertoire : {ex.Message}");
            }
        }

        public void Save<T>(T data)
        {
            try
            {
                string json = JsonSerializer.Serialize(data, _options);
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur de sauvegarde JSON : {ex.Message}");
            }
        }

        public T? Load<T>()
        {
            if (!File.Exists(path))
                return default;

            try
            {
                string json = File.ReadAllText(path);
                return JsonSerializer.Deserialize<T>(json, _options);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur de chargement JSON : {ex.Message}");
                return default;
            }
        }
    }
}