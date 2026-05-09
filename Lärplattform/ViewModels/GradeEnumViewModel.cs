using System.Text.Json.Serialization;

namespace Lärplattform.ViewModels
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum GradeEnumViewModel
    {
        A = 0,

        C = 1,

        F = 2,
    }
}
