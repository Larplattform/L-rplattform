using System.Text.Json.Serialization;

namespace Lärplattform.ViewModels
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum LocationEnumViewModel
    {
        Gym = 0,
        Room101 = 1,
        Room102 = 2,
        ComputerLab = 3,
        Library = 4,
    }
}
