using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace DataUtils
{
    /// <summary>
    /// Предоставляет функционал для автоматического сохранения данных пациентов в файл JSON.
    /// </summary>
    public class AutoSaver
    {
        private DateTime _lastUpdateTime;
        private string _path;

        /// <summary>
        /// Инициализирует новый экземпляр класса AutoSaver.
        /// </summary>
        /// <param name="path">Путь к файлу JSON для сохранения данных.</param>
        /// <param name="logging">Определяет, включено ли логирование (по умолчанию true).</param>
        public AutoSaver(string? path, bool logging = true)
        {
            _path = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path)) + "_tmp.json";
            _lastUpdateTime = DateTime.Now;
            Patient.Updated += Updated;

            if (logging)
            {
                Console.WriteLine($"[!] Автосохранение будет производиться в файл: \"{_path}\"");
            }
        }
        /// <summary>
        /// Сохраняет список пациентов в файл JSON.
        /// </summary>
        /// <param name="allPatients">Список пациентов для сохранения.</param>
        private void Save(List<Patient> allPatients)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
            };
            File.WriteAllText(_path, JsonSerializer.Serialize(allPatients, options));
        }
        /// <summary>
        /// Обрабатывает событие, возникающее при обновлении данных о пациентах.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="updated">Аргументы события, содержащие обновленные данные.</param>
        private void Updated(object? sender, UpdatedEventArgs updated)
        {
            if ((updated.ChangeDateTime - _lastUpdateTime).TotalSeconds <= 15)
            {
                Save(updated.AllData);
            }
            _lastUpdateTime = updated.ChangeDateTime;
        }
    }
}
