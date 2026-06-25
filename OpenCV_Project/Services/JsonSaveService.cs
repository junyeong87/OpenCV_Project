using OpenCV_Project.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OpenCV_Project.Services
{
    internal class JsonSaveService
    {
        public void SaveResults(IEnumerable<InspectionResult> logs)
        {
            string json = JsonSerializer.Serialize(
                logs,
                new JsonSerializerOptions
                {
                    WriteIndented = true,
                }
            );


            File.WriteAllText("logs.json", json);
        }
    }
}
