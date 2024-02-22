using System.Text.Json;
using System.Text.Json.Serialization;

namespace DataUtils
{
    public class Doctor
    {
        [JsonPropertyName("doctor_id")]
        public int DoctorId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("appointment_count")]
        public int AppointmentCount { get; set; }
        public Doctor(int doctorId, string name, int appointmentCount)
        {
            DoctorId = doctorId;
            Name = name;
            AppointmentCount = appointmentCount;
        }

        public Doctor()
        {
            Name = string.Empty;
        }

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
                AppointmentCount++;
            }
            else
            {
                AppointmentCount--;
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
