using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Data.DTOs
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum LocationEnumDTO
    {
        Gym = 0,
        Room101 = 1,
        Room102 = 2,
        ComputerLab = 3,
        Library = 4,
    }
}
