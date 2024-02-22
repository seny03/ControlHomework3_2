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
    }
}
