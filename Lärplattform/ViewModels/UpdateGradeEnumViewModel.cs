using System.Text.Json.Serialization;

namespace Lärplattform.ViewModels
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UpdateGradeEnumViewModel
    {
        A = 0,

        C = 1,

        F = 2,
    }
}
