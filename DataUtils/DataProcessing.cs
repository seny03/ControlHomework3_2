namespace DataUtils
{
    public static class DataProcessing
    {
        public static List<Patient> Sort(List<Patient>? data, string columnName)
        {
            if (data is null)
            {
                return new List<Patient>();
            }
            List<Patient> sortedData = columnName switch
            {
                "patient_id" => data.OrderBy(x => x.PatientId).ToList(),
                "name" => data.OrderBy(x => x.Name).ToList(),
                "age" => data.OrderBy(x => x.Age).ToList(),
                "gender" => data.OrderBy(x => x.Gender).ToList(),
                "diagnosis" => data.OrderBy(x => x.Diagnosis).ToList(),
                "heart_rate" => data.OrderBy(x => x.HeartRate).ToList(),
                "temperature" => data.OrderBy(x => x.Temperature).ToList(),
                "oxygen_saturation" => data.OrderBy(x => x.OxygenSaturation).ToList(),
                _ => throw new ArgumentException($"Некорректное название колонки: {columnName}")
            };
            return sortedData;
        }

        public static List<Patient> Filter(List<Patient>? data, string columnName, object? value)
        {
            if (data is null)
            {
                return new List<Patient>();
            }
            try
            {
                List<Patient> filteredData = columnName switch
                {
                    "patient_id" => data.Where(x => x.PatientId == Convert.ToInt32(value)).ToList(),
                    "name" => data.Where(x => x.Name == Convert.ToString(value)).ToList(),
                    "age" => data.Where(x => x.Age == Convert.ToInt32(value)).ToList(),
                    "gender" => data.Where(x => x.Gender == Convert.ToString(value)).ToList(),
                    "diagnosis" => data.Where(x => x.Diagnosis == Convert.ToString(value)).ToList(),
                    "heart_rate" => data.Where(x => x.HeartRate == Convert.ToInt32(value)).ToList(),
                    "temperature" => data.Where(x => x.Temperature == Convert.ToDouble(value)).ToList(),
                    "oxygen_saturation" => data.Where(x => x.OxygenSaturation == Convert.ToInt32(value)).ToList(),
                    _ => throw new ArgumentException($"Некорректное название колонки: {columnName}")
                };

                return filteredData;
            }
            // Обработка ошибки преобразования типов
            catch (Exception ex) when (ex is FormatException || ex is InvalidCastException)
            {
                return new List<Patient>();
            }
        }
    }
}
