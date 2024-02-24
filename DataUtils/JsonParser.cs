using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace DataUtils
{
    /// <summary>
    /// Предоставляет методы для чтения и записи данных в формате JSON.
    /// </summary>
    public static class JsonParser
    {
        /// <summary>
        /// Сохранение файлика по пути.
        /// </summary>
        /// <param name="patients">Список с клиентами.</param>
        /// <param name="path">Путь до файлика сохранения.</param>
        public static void WriteJson(string path, List<Patient> patients)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
            };
            File.WriteAllText(path, JsonSerializer.Serialize(patients, options));
        }

        /// <summary>
        /// Считывание пациентов из json.
        /// </summary>
        /// <param name="path">Путь до считывания.</param>
        /// <returns>Список с пациентами.</returns>
        public static List<Patient> ReadJson(string? path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            List<Patient> patients = new List<Patient>();

            JsonElement jsonElement = JsonSerializer.Deserialize<JsonElement>(File.ReadAllText(path));

            foreach (JsonElement patientJson in jsonElement.EnumerateArray())
            {
                Patient patient = new Patient(patientJson);
                patients.Add(patient);
            }

            return patients;
        }
    }
}
