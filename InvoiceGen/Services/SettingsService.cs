using System;
using System.IO;
using System.Text.Json;
using InvoiceGen.Models;

namespace InvoiceGen.Services
{
    public class SettingsService
    {
        private readonly string settingsFolder;
        private readonly string settingsFile;

        public SettingsService()
        {
            settingsFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "InvoiceGen"
            );

            settingsFile = Path.Combine(settingsFolder, "settings.json");
        }

        public void SaveSettings(BusinessSettings settings)
        {
            Directory.CreateDirectory(settingsFolder);

            string json = JsonSerializer.Serialize(
                settings,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                }
            );

            File.WriteAllText(settingsFile, json);
        }

        public BusinessSettings LoadSettings()
        {
            if (!File.Exists(settingsFile))
            {
                return new BusinessSettings();
            }

            string json = File.ReadAllText(settingsFile);

            return JsonSerializer.Deserialize<BusinessSettings>(json)
                   ?? new BusinessSettings();
        }
    }
}