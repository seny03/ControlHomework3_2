﻿using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace DataUtils
{
    public class AutoSaver
    {
        private DateTime _lastUpdateTime;
        private string _path;

        public AutoSaver(string path, List<Patient> allData)
        {
            foreach (Patient patient in allData)
            {
                patient.Updated += Updated;
            }
            _path = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_tmp.json";
            _lastUpdateTime = DateTime.Now;
        }

        private void Save(List<Patient> allPatients)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
            };
            File.WriteAllText(_path, JsonSerializer.Serialize(allPatients, options));
        }

        private void Updated(object? sender, UpdatedEventArgs updated)
        {
            if ((updated.ChangeDateTime - lastUpdateTime).TotalSeconds <= 15)
            {
                lastUpdateTime = updated.ChangeDateTime;
                SaveTmp(updated.patients);
            }
        }
    }
}