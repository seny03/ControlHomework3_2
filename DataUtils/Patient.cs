using System.Text.Json;
using System.Text.Json.Serialization;

namespace DataUtils
{
    public class Patient
    {
        private int _heartRate = 60;
        private int _oxygenSaturation = 95;
        private double _temperature = 36.0;
        private List<Doctor> _doctors = new List<Doctor>();

        [JsonPropertyName("patient_id")]
        public int PatientId { get; private set; }

        [JsonPropertyName("name")]
        public string Name { get; private set; }

        [JsonPropertyName("age")]
        public int Age { get; private set; }

        [JsonPropertyName("gender")]
        public string Gender { get; private set; }

        [JsonPropertyName("diagnosis")]
        public string Diagnosis { get; private set; }

        [JsonPropertyName("heart_rate")]
        public int HeartRate
        {
            get
            {
                return _heartRate;
            }
            set
            {
                bool previousState = IsNormalState;
                _heartRate = value;
                ImportantFieldUpdated?.Invoke(this, new StateEventArgs(previousState, IsNormalState));
            }
        }

        [JsonPropertyName("temperature")]
        public double Temperature
        {
            get
            {
                return _temperature;
            }
            set
            {
                bool previousState = IsNormalState;
                _temperature = value;
                ImportantFieldUpdated?.Invoke(this, new StateEventArgs(previousState, IsNormalState));
            }
        }

        [JsonPropertyName("oxygen_saturation")]
        public int OxygenSaturation
        {
            get
            {
                return _oxygenSaturation;
            }
            set
            {
                bool previousState = IsNormalState;
                _oxygenSaturation = value;
                ImportantFieldUpdated?.Invoke(this, new StateEventArgs(previousState, IsNormalState));
            }
        }

        [JsonPropertyName("doctors")]
        public List<Doctor> Doctors
        {
            get => _doctors;
            set
            {
                _doctors = value;
                if (_doctors is not null)
                {
                    foreach (var doctor in _doctors)
                    {
                        ImportantFieldUpdated += doctor.ChangeAppointmentCount;
                    }
                }
            }
        }

        public bool IsNormalState => (36 <= Temperature && Temperature <= 38) && (60 <= HeartRate && HeartRate <= 100) &&
            (95 <= OxygenSaturation && OxygenSaturation <= 100);
        public static string[] Properties => new string[] { "patient_id", "name", "age", "gender", "diagnosis", "heart_rate", "temperature", "oxygen_saturation" };
        public static string[] ChangeableProperties => new string[] { "name", "age", "gender", "diagnosis", "heart_rate", "temperature", "oxygen_saturation" };

        private event EventHandler<StateEventArgs>? ImportantFieldUpdated;
        public event EventHandler<UpdatedEventArgs>? Updated;

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

        public Patient(JsonElement json)
        {
            if (json.TryGetProperty("patient_id", out var patientIdElement) && patientIdElement.TryGetInt32(out var patientId) &&
                json.TryGetProperty("name", out var nameElement) && nameElement.GetString() is not null &&
                json.TryGetProperty("age", out var ageElement) && ageElement.TryGetInt32(out var age) &&
                json.TryGetProperty("gender", out var genderElement) && genderElement.GetString() is not null &&
                json.TryGetProperty("diagnosis", out var diagnosisElement) && diagnosisElement.GetString() is not null &&
                json.TryGetProperty("heart_rate", out var heartRateElement) && heartRateElement.TryGetInt32(out var heartRate) &&
                json.TryGetProperty("temperature", out var temperatureElement) && temperatureElement.TryGetDouble(out var temperature) &&
                json.TryGetProperty("oxygen_saturation", out var oxygenSaturationElement) && oxygenSaturationElement.TryGetInt32(out var oxygenSaturation) &&
                json.TryGetProperty("doctors", out var doctorsElement))
            {
                PatientId = patientId;
                Name = nameElement.GetString();
                Age = age;
                Gender = genderElement.GetString();
                Diagnosis = diagnosisElement.GetString();
                HeartRate = heartRate;
                Temperature = temperature;
                OxygenSaturation = oxygenSaturation;

                var doctors = new List<Doctor>();
                foreach (var doctorJson in doctorsElement.EnumerateArray())
                {
                    doctors.Add(new Doctor(doctorJson));
                }
                Doctors = doctors;
            }
            else
            {
                throw new ArgumentException("Объект Patient задан некорретно.");
            }
        }

        public Patient()
        {
            Doctors = new List<Doctor>();
            Name = Gender = Diagnosis = string.Empty;
        }

        public void ChangeField(string fieldName, object value, List<Patient> allData)
        {
            if (value is null)
            {
                throw new ArgumentNullException("field");
            }

            switch (fieldName)
            {
                case "name":
                    Name = Convert.ToString(value) ?? "";
                    break;
                case "age":
                    Age = Convert.ToInt32(value);
                    break;
                case "gender":
                    Gender = Convert.ToString(value) ?? "";
                    break;
                case "diagnosis":
                    Diagnosis = Convert.ToString(value) ?? "";
                    break;
                case "heart_rate":
                    HeartRate = Convert.ToInt32(value);
                    break;
                case "temperature":
                    Temperature = Convert.ToDouble(value);
                    break;
                case "oxygen_saturation":
                    OxygenSaturation = Convert.ToInt32(value);
                    break;
                default:
                    throw new ArgumentException($"Некорректное назвние поля: {fieldName}");
            }

            Updated?.Invoke(this, new UpdatedEventArgs(DateTime.Now, allData));
        }

        /// <summary>
        /// Преобразование объекта в Json строку.
        /// </summary>
        /// <returns>Json строка.</returns>
        public string ToJSON()
        {
            return JsonSerializer.Serialize(this);
        }

        public string[] ToArray() => new string[] { PatientId.ToString(), Name, Age.ToString(), Gender,
            Diagnosis, HeartRate.ToString(), $"{Temperature:f2}", OxygenSaturation.ToString() };
    }
}