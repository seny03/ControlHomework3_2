using System.Text.Json;
using System.Text.Json.Serialization;

namespace DataUtils
{
    /// <summary>
    /// Представляет информацию о враче.
    /// </summary>
    public class Doctor
    {
        [JsonPropertyName("doctor_id")]
        public int DoctorId { get; private set; }

        [JsonPropertyName("name")]
        public string Name { get; private set; }

        [JsonPropertyName("appointment_count")]
        public int AppointmentCount { get; private set; }
        public List<Patient> patients { get; private set; } = new List<Patient>() { };

        /// <summary>
        /// Инициализирует новый экземпляр класса Doctor с заданными значениями.
        /// </summary>
        /// <param name="doctorId">Идентификатор врача.</param>
        /// <param name="name">Имя врача.</param>
        /// <param name="appointmentCount">Количество оповещений о серьезно больных пациентах.</param>
        public Doctor(int doctorId, string name, int appointmentCount)
        {
            DoctorId = doctorId;
            Name = name;
            AppointmentCount = appointmentCount;
        }
        /// <summary>
        /// Инициализирует новый экземпляр класса Doctor из JSON.
        /// </summary>
        /// <param name="json">JSON-элемент, содержащий данные о враче.</param>
        public Doctor(JsonElement json)
        {
            if (json.TryGetProperty("doctor_id", out var doctorIdElement) && doctorIdElement.TryGetInt32(out var doctorId) &&
                json.TryGetProperty("name", out var nameElement) && nameElement.GetString() is not null &&
                json.TryGetProperty("appointment_count", out var appointmentCountElement) && appointmentCountElement.TryGetInt32(out var appointmentCount))
            {
                DoctorId = doctorId;
                Name = nameElement.GetString();
                AppointmentCount = appointmentCount;
            }
            else
            {
                throw new ArgumentException("Объект Doctor задан некорректно.");
            }
        }

        public Doctor()
        {
            Name = string.Empty;
        }
        /// <summary>
        /// Изменяет количество назначений врача в зависимости от состояния пациента.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="state">Аргументы события: состояние пациента до и после изменяния важного параметра.</param>
        public void ChangeAppointmentCount(object? sender, StateEventArgs state)
        {
            // Если состояние пациента не изменилось, ничего не меняем.
            if (state.PreviousState == state.CurrentState)
            {
                return;
            }
            // Если состояние пациента изменилось, то изменяем AppointmentCount в зависимости от того улучшилось оно или ухудшилось.
            if (state.CurrentState)
            {
                AppointmentCount--;
            }
            else
            {
                AppointmentCount++;
            }
        }

        /// <summary>
        /// Преобразование объекта в Json строку.
        /// </summary>
        /// <returns>Json строка.</returns>
        public string ToJSON()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
