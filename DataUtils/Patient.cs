using System.Text.Json;
using System.Text.Json.Serialization;

namespace DataUtils
{
    public class Patient
    {
        private int _heartRate, _oxygenSaturation;
        private double _temperature;
        private List<Doctor> _doctors = new List<Doctor>();

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
                OnFieldChange?.Invoke(this, new StateEventArgs(previousState, IsNormalState));
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
                OnFieldChange?.Invoke(this, new StateEventArgs(previousState, IsNormalState));
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
                OnFieldChange?.Invoke(this, new StateEventArgs(previousState, IsNormalState));
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
                        OnFieldChange += doctor.ChangeAppointmentCount;
                    }
                }
            }
        }

        public bool IsNormalState => (36 <= Temperature && Temperature <= 38) && (60 <= HeartRate && HeartRate <= 100) &&
            (95 <= OxygenSaturation && OxygenSaturation <= 100);

        event EventHandler<StateEventArgs>? OnFieldChange;

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
            Name = Gender = Diagnosis = string.Empty;
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