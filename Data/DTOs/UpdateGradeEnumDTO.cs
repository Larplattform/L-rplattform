using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Data.DTOs
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UpdateGradeEnumDTO
    {
        A = 0,

        C = 1,

        F = 2,
    }
}
