namespace DataUtils
{
    public static class DataProcessing
    {
        public static List<Patient> Sort(List<Patient> data, string columnName)
        {
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

        public static List<Patient> Filter(List<Patient> data, string columnName, object value)
        {
            List<Patient> filteredData = columnName switch
            {
                "patient_id" => data.Where(x => x.PatientId == (int)value).ToList(),
                "name" => data.Where(x => x.Name == (string)value).ToList(),
                "age" => data.Where(x => x.Age == (int)value).ToList(),
                "gender" => data.Where(x => x.Gender == (string)value).ToList(),
                "diagnosis" => data.Where(x => x.Diagnosis == (string)value).ToList(),
                "heart_rate" => data.Where(x => x.HeartRate == (int)value).ToList(),
                "temperature" => data.Where(x => x.Temperature == (double)value).ToList(),
                "oxygen_saturation" => data.Where(x => x.OxygenSaturation == (int)value).ToList(),
                _ => throw new ArgumentException($"Некорректное название колонки: {columnName}")
            };

            return filteredData;
        }
    }
}
