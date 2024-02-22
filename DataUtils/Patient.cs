using System.Text.Json.Serialization;

namespace DataUtils
{
    public class Patient
    {
        [JsonPropertyName("patient_id")]
        public int PatientId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("age")]
        public int Age { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("diagnosis")]
        public string Diagnosis { get; set; }

        [JsonPropertyName("heart_rate")]
        public int HeartRate { get; set; }

        [JsonPropertyName("temperature")]
        public double Temperature { get; set; }

        [JsonPropertyName("oxygen_saturation")]
        public int OxygenSaturation { get; set; }

        [JsonPropertyName("doctors")]
        public List<Doctor> Doctors { get; set; }

        public Patient(int patientId, string name, int age, string gender, string diagnosis, int heartRate, double temperature, int oxygenSaturation, List<Doctor> doctors)
        {
            PatientId = patientId;
            Name = name;
            Age = age;
            Gender = gender;
            Diagnosis = diagnosis;
            HeartRate = heartRate;
            Temperature = temperature;
            OxygenSaturation = oxygenSaturation;
            Doctors = doctors;
        }

        public Patient()
        {
            Doctors = new List<Doctor>();
            Console.WriteLine(Name);
            Console.WriteLine((Name is null));
        }
    }
}